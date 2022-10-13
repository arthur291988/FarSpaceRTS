
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationController : MonoBehaviour
{
    [SerializeField]
    private GameObject guardStationLaser;

    //fleet count variables to modify while producing the fleet on station and diplaying the values of these on display while panel of station is open
    //public cause they are called from SpaceCtrlr class by reference to this class
    
    public int Cruis4;
    public int Cruis3;
    public int Cruis2;
    public int Cruis1;
    public int CruisG;
    public int Destr4;
    public int Destr3;
    public int Destr2;
    public int Destr2Par;
    public int Destr1;
    public int Destr1Par;
    public int DestrG;
    public int Gun3;
    public int Gun2;
    public int Gun1;
    public int MiniGun;
    public int Fighter;

    //private const float energyForCPUCruisToCapture = 200;

    public bool cruiserPorted; //trigger to use in SpaceCtrlr class to display the ship fleet calues and to enable an exchange of ship between ship and station

    //this game object is next generation station that comes instead current after upgrade has been finished
    //this values are used both for instatiating CPU ships and CPU stations of next upgrade
    private List<GameObject> stationListToActivate; //to pull the station object from puller
    private GameObject upgradedStationToActivate;
    private StationController upgradedStationInstance;
    private List<GameObject> cruiserListToActivate; //to pull the station object from puller
    private GameObject cruiserToActivate;
    private CPUShipCtrlJourney CPUCruisInstance;

    //variables to hold the information about production plan and display that info on station panel UI while it is open 
    //public cause they are called from SpaceCtrlr class by reference to this class
    public int Cruis4Produc;
    public int Cruis3Produc;
    public int Cruis2Produc;
    public int Cruis1Produc;
    public int Destr4Produc;
    public int Destr3Produc;
    public int Destr2Produc;
    public int Destr2ProducPar;
    public int Destr1Produc;
    public int Destr1ProducPar;
    public int Gun3Produc;
    public int Gun2Produc;
    public int Gun1Produc;
    public int MiniGunProduc;
    public int FighterProduc;

    //this are the increments that will added to itself (from 0) while they are lower than 1 (so the time while they are reaching to one is production time of ship and energy) 
    //higher the value faster the production time and also it changes (becoming faster) with the upgrade level of station
    //public float Cruis4ProductTime;
    //public float Cruis3ProductTime;
    //public float Cruis2ProductTime;
    //public float Cruis1ProductTime;
    //public float Destr4ProductTime;
    //public float Destr3ProductTime;
    //public float Destr2ProductTime;
    //public float Destr2ParProductTime;
    //public float Destr1ProductTime;
    //public float Destr1ParProductTime;
    //public float Gun3ProductTime;
    //public float Gun2ProductTime;
    //public float Gun1ProductTime;
    //public float MiniGunProductTime;
    //public float FighterProducTime;

    //public float EnergyProductTime;

    //these are the strings to understand for script the production plan (the sequence of ship types in production plan)
    private string C1 = "C1";
    private string C2 = "C2";
    private string C3 = "C3";
    private string C4 = "C4";
    private string CG = "CG";
    private string D1 = "D1";
    private string D1P = "D1P";
    private string D2 = "D2";
    private string D2P = "D2P";
    private string D3 = "D3";
    private string D4 = "D4";
    private string DG = "DG";
    private string G1 = "G1";
    private string G2 = "G2";
    private string G3 = "G3";
    private string GM = "GM";
    private string EN = "EN";
    private string FI = "FI";


    //conditions to assign current station to player or one of the CPU players
    public bool isPlayerStation; //to trigger if this class controls player station || though guess I will make different controller for CPU station
    public bool isCPUStation; 
    public bool isGuardStation;

    public bool isGuardCoreStation;
    //public bool isCPU2Station;
    //public bool isCPU3Station;
    //public bool isCPU4Station;

    public int stationCurrentLevel; //current upgrade level of station, there are 4 levels of upgrade
    public int upgradeCounts; //the levels of available upgrade for current station (depends on level where the player is right now)
    public int nextUpgradeEnergyCount; //the volume of energy that necessary to enable the next upgrade of station
    public float upgradeFill; //time to get fromm 0 to 1 by step of currentUpgradeTime which is getting lower each next step of upgrade 
    public float currentUpgradeTime; //step of time to reach 1 to upgrade station which is getting lower each next step of upgrade 
    public bool isUpgrading; //bool that triggers upgrade of station and stops all current production processes until upgrade is finished 

    //this is special int to control the sequence of production when station produces the ships and energy in a sequence to start prepear for attack
    //it is assigned by value in CaptureLine class to set a trigger to first level station and on upgrade finish on update method of this class to set a higher level 
    //station trigger, so higher the station level more the amount of trigger so more the energy production per cycle
    public int sequenceProductionTrigger;

    public Vector3 stationPosition; //this one will be necessary to check where stays this station and pull according data from according save file after player returns to game

    //variables to hold the information about current production process if it is ship production or energy (energy production starts automatically after station is created)
    //and energy production stops only while production of ships is on
    public bool shipProductionIsOn;
    public bool energyProductionIsOn;

    //this list hold the sequence of production plan of ships in form of strings. And values of it are read and compared to solid variables of ship strings that fixed upper in this class
    public List<string> productionPlan;

    //this variable is for tracking current producing product time and switching the production to next product after it reached value of one or more
    public float currentPruductionFillLocal; 

    //this variable holds the increment of current producing product
    public float commontPruductTimeLocal;
    public float commontPruductTimeBoosterLocal=1;
    public float commontBoosterReduceRateLocal;

    //this variable holds the type of current producing product
    //public string CurrentProducedShipLocal;

    //this one is necessary to send a sygnal to update method of SpaceCtrlr calss mostly to update UI text on station panel and somehow unload the CPU of device
    //public bool stationProductionSwitchTrigger;

    //this UI is found while the station is instatntiated (activatd from pull) and is used to display palyer's total energy on journey scene
    private Text energyCount;

    //this bool is called from LaunchingObjcts class and it give a signall that this player cruiser stays near that station which is used on SpaceCtrlr class to process
    public bool playerCruiserNear;
    public List <LaunchingObjcts> playerCruiserObject;

    public int CPUNumber; //this var determine the number of CPU (each instance of this class is managed as some exact CPU station controller or player station)
    //public int fleetIncreaseStep; //there are 4 steps of CPU fleet increas 0 right after creating the station and rest 3 after some time pass

    //this var is used to give a sygnal that current station is on process of launching a scene cruiser
    public bool CPUSceneCruiserLaunchCoroutineIsOn = false;

    public float CPUSceneCruiserLaunchTimer = 0; //this timer counts down the time on update to launche next CPU cruiser on scene
    //public float CPUStationUpgradeTimer; //this timer counts down the time on update to upgrade a station to next lvl
    //public int CPUStationIsUpgradIng = 0; //this timer counts down the time on update to upgrade a station to next lvl

    //to set from spaceCtrlr class while first instantiation of station
    //public Material colorOfStationMat;
    public Color colorOfStationMat;
    public List <GameObject> stationColorSphere; //this parts of station demostrate the color of station

    //[SerializeField]
    //private TextMesh stationFleetCountTextMesh;

    //is used to populate with all ships of station and take only the 30%of fleet
    private List<string> allCruisers;
    private List<string> allDestrs;
    private List<string> cruisersToLaunchCruiser;
    private List<string> destrsToLaunchCruiser;

    //public int destrProductionCount;
    //public float CPUStationFleetIncreaserTimer;

    //is used to populate with all ships of station and take only the % of fleet
    private List<string> allCruisersAndDestrs;
    private List<string> cruisersAndDestrAfterReduce;

    private GameObject infoPanelLocal;
    private List<GameObject> infoPanelLocalListToActivate;
    private MiniInfoPanel miniInfoPanelObject;
    public float energyOfStation;
    public float energyOfStationToUPGradeFoRCPU;
    private bool isSelected;


    private GameObject connectionSygnalUIGO;
    private List<GameObject> connectionSygnalUILocalListToActivate;

    [HideInInspector]
    public GameObject playerCruiserPortedToCPUStation;

    [HideInInspector]
    public EnergonController stationsEnergon;
    private GameObject energonShipReal;

    private Vector3 connectionSygnalPos;

    private void OnEnable()
    {
        //getting the referene to player's total energy displaying UI Text to update it while the static energy variable on Lists class is modified
        /*if (isPlayerStation) */

        allCruisers = new List<string>();
        allDestrs = new List<string>();

        cruisersToLaunchCruiser = new List<string>();
        destrsToLaunchCruiser = new List<string>();

        allCruisersAndDestrs = new List<string>();
        cruisersAndDestrAfterReduce = new List<string>();

        energyCount = SpaceCtrlr.Instance.energyCount;
        //if (!isPlayerStation && CPUSceneCruiserLaunchTimer<=0) launchingACPUCruiserOnScene(); //starting the timer of launching a CPU cruiser on scene automatically after station is launched
        //so the properties of this station class will be reset here only in case if there was not a switch from scene which is checked by if this game object is in AllSceneObjects
        //list. And there it may be only in case if it existed while switching from JourneyScene. Otherwise this class is pulled and properties of it should be reset
        if (Lists.AllSceneObjects.Count == 0 && !Lists.isContinued)
        {
            Cruis4 = 0;
            Cruis3 = 0;
            Cruis2 = 0;
            Cruis1 = 0;
            CruisG = 0;
            Destr4 = 0;
            Destr3 = 0;
            Destr2 = 0;
            Destr2Par = 0;
            Destr1 = 0;
            Destr1Par = 0;
            DestrG = 0;
            Gun3 = 0;
            Gun2 = 0;
            Gun1 = 0;
            MiniGun = 0;
            Fighter = 0;

            if (Lists.isBlackDimension) energyOfStation = Random.Range(Constants.Instance.energyOfStationDark - 30, Constants.Instance.energyOfStationDark + 30);
            else if (Lists.isBlueDimension) energyOfStation = Random.Range(Constants.Instance.energyOfStationBlue - 30, Constants.Instance.energyOfStationBlue + 30);
            else if (Lists.isRedDimension) energyOfStation = Random.Range(Constants.Instance.energyOfStationRed - 30, Constants.Instance.energyOfStationRed + 30);

        }
        //else if (isCPUStation && CPUSceneCruiserLaunchCoroutineIsOn) {
        //    SpaceCtrlr.Instance.allCruisLaunchingCPUStations.Add(this);
        //    SpaceCtrlr.Instance.resetTheTimer();
        //}

        if (isGuardCoreStation) {
            SpaceCtrlr.Instance.GuardTurnSlider.minValue = CPUSceneCruiserLaunchTimer * -1;
            SpaceCtrlr.Instance.GuardTurnSlider.maxValue = 0;
        }

        //CPUStationFleetIncreaserTimer = Constants.Instance.timeCreateDestr;

        StartCoroutine(addThisToSelectable());

    }
    private void OnDisable()
    {
        disactivateInfoAboutShip();
        StopAllCoroutines();
    }
    private IEnumerator addThisToSelectable()
    {
        yield return new WaitForSeconds(0.3f);
        if (!isPlayerStation) SelectingBox.Instance.selectableStations.Add(this);
        else SelectingBox.Instance.selectableStationsPlayer.Add(this);
    }

    private void Start()
    {
        connectionSygnalPos = new Vector3(transform.position.x+10, 5, transform.position.z);
    }

    private void disablingThisStation()
    {
        gameObject.SetActive(false);
        SelectingBox.Instance.selectableStations.Remove(this);
        SelectingBox.Instance.selectableStationsPlayer.Remove(this);
    }
    public void OnOffConnectionSygnal(bool on)
    {
        if (on)
        {
            connectionSygnalUILocalListToActivate = ObjectPullerJourney.current.GetConnectionSygnalUIPullList();
            if (connectionSygnalUIGO != null)
            {
                if (connectionSygnalUIGO.activeInHierarchy) connectionSygnalUIGO.SetActive(false);
                connectionSygnalUIGO = null;
            }
            connectionSygnalUIGO = ObjectPullerJourney.current.GetUniversalBullet(connectionSygnalUILocalListToActivate);
            connectionSygnalUIGO.transform.position = SpaceCtrlr.Instance.mainCam.WorldToScreenPoint(connectionSygnalPos);
            connectionSygnalUIGO.SetActive(true);
            SpaceCtrlr.Instance.connectionEstablishedSound.Play();
        }
        else
        {
            SpaceCtrlr.Instance.connectionDisactivatedSound.Play();
            if (connectionSygnalUIGO!= null && connectionSygnalUIGO.activeInHierarchy) connectionSygnalUIGO.SetActive(false);
            connectionSygnalUIGO = null;
        }
    }
    public void showInfoAboutThisObjectOnCanvas()
    {
        //yield return new WaitForSeconds(0.3f);
        if (isPlayerStation)
        {
            infoPanelLocalListToActivate = ObjectPullerJourney.current.GetminiInfoPanelNoEnergyPullList(); 
            if (infoPanelLocal != null)
            {
                if (infoPanelLocal.activeInHierarchy)
                    infoPanelLocal.SetActive(false);
            }
            infoPanelLocal = ObjectPullerJourney.current.GetUniversalBullet(infoPanelLocalListToActivate);
            miniInfoPanelObject = infoPanelLocal.GetComponent<MiniInfoPanel>();
            infoPanelLocal.transform.position = SpaceCtrlr.Instance.mainCam.WorldToScreenPoint(transform.position);
            //miniInfoPanelObject.energyCount.text = energyOfStation.ToString("0");
            miniInfoPanelObject.fleetCount.text = assessFleetPower().ToString();
            SpaceCtrlr.Instance.chosenStation = this;
        }
        else
        {
            infoPanelLocalListToActivate = ObjectPullerJourney.current.GetMiniInfoPanelPullList();
            if (infoPanelLocal != null)
            {
                if (infoPanelLocal.activeInHierarchy) infoPanelLocal.SetActive(false);
                infoPanelLocal = null;
            }
            infoPanelLocal = ObjectPullerJourney.current.GetUniversalBullet(infoPanelLocalListToActivate);
            miniInfoPanelObject = infoPanelLocal.GetComponent<MiniInfoPanel>();
            infoPanelLocal.transform.position = SpaceCtrlr.Instance.mainCam.WorldToScreenPoint(transform.position);
            miniInfoPanelObject.energyCount.text = energyOfStation.ToString("0");
            miniInfoPanelObject.fleetCount.text = assessFleetPower().ToString();
        }
        isSelected = true;
        infoPanelLocal.SetActive(true);

        updateFleetCountToDisplay();
    }
    public void disactivateInfoAboutShip()
    {
        isSelected = false;
        if (infoPanelLocal != null)
        {
            if (infoPanelLocal.activeInHierarchy) infoPanelLocal.SetActive(false);
            infoPanelLocal = null;
        }
    }

    public void createStationsEnergon()
    {
        energonShipReal = Instantiate(SpaceCtrlr.Instance.energonShip, new Vector3(transform.position.x + 7, -8, transform.position.z + 7), Quaternion.identity);
        Lists.energonsControllablesOnScene.Add(energonShipReal);
        stationsEnergon = energonShipReal.GetComponent<EnergonController>();
        stationsEnergon.energonsStation = this;
        stationsEnergon.energonsStationPosition = transform.position;
        updateStationEnergon(stationCurrentLevel);
        if (isCPUStation) stationsEnergon.makeEnergonCPUs(CPUNumber);
        else if (isPlayerStation) stationsEnergon.makeEnergonPlayers();
    }

    public void updateStationEnergon(int lvl) {
        stationsEnergon.energonLevel = lvl;
        if (lvl == 0)
        {
            stationsEnergon.energonMovingSpeed = Constants.Instance.energonMovingSpeed4; 
            stationsEnergon.energyCapacity = Constants.Instance.energonEnergyCapacityd4;
        }
        else if (lvl == 1)
        {
            stationsEnergon.energonMovingSpeed = Constants.Instance.energonMovingSpeed3;
            stationsEnergon.energyCapacity = Constants.Instance.energonEnergyCapacityd3;
        }
        else if (lvl == 2)
        {
            stationsEnergon.energonMovingSpeed = Constants.Instance.energonMovingSpeed2;
            stationsEnergon.energyCapacity = Constants.Instance.energonEnergyCapacityd2;
        }
        else
        {
            stationsEnergon.energonMovingSpeed = Constants.Instance.energonMovingSpeed1;
            stationsEnergon.energyCapacity = Constants.Instance.energonEnergyCapacityd1;
        }
    }

    private void clearTheFleetButFighters()
    {
        Cruis4 = 0;
        Cruis3 = 0;
        Cruis2 = 0;
        Cruis1 = 0;
        CruisG = 0;
        Destr4 = 0;
        Destr3 = 0;
        Destr2 = 0;
        Destr2Par = 0;
        Destr1 = 0;
        Destr1Par = 0;
        DestrG = 0;
        Gun3 = 0;
        Gun2 = 0;
        Gun1 = 0;
        MiniGun = 0;
    }

    //this method is called from any trigger with active game object to determine reducing of fleet of player ship won the battle
    public void reducingTheFleetOFCruiserOnBattle(float coeffOfReduce)
    {
        allCruisersAndDestrs.Clear();
        cruisersAndDestrAfterReduce.Clear();

        for (int i = 0; i < Cruis1; i++)
        {
            allCruisersAndDestrs.Add(C1);
        }
        for (int i = 0; i < Cruis2; i++)
        {
            allCruisersAndDestrs.Add(C2);
        }
        for (int i = 0; i < Gun3; i++)
        {
            allCruisersAndDestrs.Add(G3);
        }
        for (int i = 0; i < Destr1; i++)
        {
            allCruisersAndDestrs.Add(D1);
        }
        for (int i = 0; i < Destr1Par; i++)
        {
            allCruisersAndDestrs.Add(D1P);
        }
        for (int i = 0; i < CruisG; i++)
        {
            allCruisersAndDestrs.Add(CG);
        }
        for (int i = 0; i < Gun2; i++)
        {
            allCruisersAndDestrs.Add(G2);
        }
        for (int i = 0; i < Gun1; i++)
        {
            allCruisersAndDestrs.Add(G1);
        }
        for (int i = 0; i < Cruis3; i++)
        {
            allCruisersAndDestrs.Add(C3);
        }
        for (int i = 0; i < Destr2; i++)
        {
            allCruisersAndDestrs.Add(D2);
        }
        for (int i = 0; i < Destr2Par; i++)
        {
            allCruisersAndDestrs.Add(D2P);
        }
        for (int i = 0; i < DestrG; i++)
        {
            allCruisersAndDestrs.Add(DG);
        }
        for (int i = 0; i < Cruis4; i++)
        {
            allCruisersAndDestrs.Add(C4);
        }
        for (int i = 0; i < Destr3; i++)
        {
            allCruisersAndDestrs.Add(D3);
        }
        for (int i = 0; i < Destr4; i++)
        {
            allCruisersAndDestrs.Add(D4);
        }

        float x = allCruisersAndDestrs.Count * coeffOfReduce > 1 ? Mathf.Floor(allCruisersAndDestrs.Count * coeffOfReduce) : Mathf.Ceil(allCruisersAndDestrs.Count * coeffOfReduce);
        for (int i = 0; i < x; i++)
        {
            cruisersAndDestrAfterReduce.Add(allCruisersAndDestrs[i]);
        }
        clearTheFleetButFighters();
        for (int i = 0; i < cruisersAndDestrAfterReduce.Count; i++)
        {
            if (cruisersAndDestrAfterReduce[i] == C1)
            {
                Cruis1++;
            }
            else if (cruisersAndDestrAfterReduce[i] == C2)
            {
                Cruis2++;
            }
            else if (cruisersAndDestrAfterReduce[i] == C3)
            {
                Cruis3++;
            }
            else if (cruisersAndDestrAfterReduce[i] == C4)
            {
                Cruis4++;
            }
            else if (cruisersAndDestrAfterReduce[i] == CG)
            {
                CruisG++;
            }
            if (cruisersAndDestrAfterReduce[i] == D1)
            {
                Destr1++;
            }
            else if (cruisersAndDestrAfterReduce[i] == D1P)
            {
                Destr1Par++;
            }
            else if (cruisersAndDestrAfterReduce[i] == D2)
            {
                Destr2++;
            }
            else if (cruisersAndDestrAfterReduce[i] == D2P)
            {
                Destr2Par++;
            }
            else if (cruisersAndDestrAfterReduce[i] == D3)
            {
                Destr3++;
            }
            else if (cruisersAndDestrAfterReduce[i] == D4)
            {
                Destr4++;
            }
            else if (cruisersAndDestrAfterReduce[i] == DG)
            {
                DestrG++;
            }
            else if (cruisersAndDestrAfterReduce[i] == G1)
            {
                DestrG++;
            }
            else if (cruisersAndDestrAfterReduce[i] == G2)
            {
                DestrG++;
            }
            else if (cruisersAndDestrAfterReduce[i] == G3)
            {
                DestrG++;
            }
        }

        if (Fighter > 0) Fighter = (int)Mathf.Ceil(Fighter * 0.4f);

        if (infoPanelLocal)
        {
            if (isPlayerStation)
            {
                miniInfoPanelObject.fleetCount.text = assessFleetPower().ToString();
            }
            else
            {
                miniInfoPanelObject.energyCount.text = energyOfStation.ToString("0");
                miniInfoPanelObject.fleetCount.text = assessFleetPower().ToString();
            }
        }

    }

    //this method is callsed from SpaceCtrlr after instatiating the station for CPU
    public void startProcessesForCPU(int CPUNo) {
        CPUNumber = CPUNo;
        //fleetIncreaseStep = 0;
        //StartCoroutine(launchingACPUCruiserOnScene());
        //CancelInvoke(); //stop invoking any upgrade on a new station of CPU to prevent a bugs of upgrades doublings
        setCPUStationFleetByLvl();
        //if (stationCurrentLevel < upgradeCounts)
        //{
        //    //CPUStationUpgradeTimer = upgradeStationCPUTime() + Constants.Instance.standartTimeBeforeUpgrdProcessStartCPU;
        //    //CPUStationIsUpgradIng = 1; //this int serves as bool (to optimize the game) 0 is false 1 is true. It triggers if station upgrade time countdoun is started on update
        //}
        setProperStationColor(); //only for CPU stations
        //createStationsEnergon();
        //Invoke("upgradeCPUStation", upgradeStationCPUTime() + Lists.standartTimeBeforeUpgrdProcessStartCPU);
    }
    public void startProcessesForCPUFromEmptyStation()
    {
        //if (stationCurrentLevel < upgradeCounts)
        //{
        //    //CPUStationUpgradeTimer = upgradeStationCPUTime() + Constants.Instance.standartTimeBeforeUpgrdProcessStartCPU;
        //    //CPUStationIsUpgradIng = 1; //this int serves as bool (to optimize the game) 0 is false 1 is true. It triggers if station upgrade time countdoun is started on update
        //}
        setProperStationColor(); //only for CPU stations
        //updateFleetCountToDisplay();
        //StartCoroutine(showInfoAboutThisObjectOnCanvas());
    }


    //setting a proper color material to station color sphere
    //0-Red, 1-green, 2-blue, 3-yellow, 4-purple
    public void setProperStationColor() {
        //stationColorSphere.GetComponent<MeshRenderer>().material = colorOfStationMat;
        for (int i = 0; i < stationColorSphere.Count; i++)
        {
            stationColorSphere[i].GetComponent<MeshRenderer>().material.SetColor("_Color", colorOfStationMat);
        }
    }

    //this method makes current station guards one
    public void makeStatioGuards() {
        colorOfStationMat = new Color(2.4f, 2.4f, 2.4f, 1);
        setProperStationColor();

        Lists.CPUGuardStations.Add(this);

        if (isCPUStation)
        {
            if (CPUNumber == 0)
            {
                Lists.CPU1Stations.Remove(this);
                //updating the UI of stations information of player that lost the station 
                SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(1, Lists.CPU1Stations.Count, Lists.CPU1CruisersOnScene);
            }
            else if (CPUNumber == 1)
            {
                Lists.CPU2Stations.Remove(this);
                //updating the UI of stations information of player that lost the station 
                SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(2, Lists.CPU2Stations.Count, Lists.CPU2CruisersOnScene);

            }
            else if (CPUNumber == 2)
            {
                Lists.CPU3Stations.Remove(this);
                //updating the UI of stations information of player that lost the station 
                SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(3, Lists.CPU3Stations.Count, Lists.CPU3CruisersOnScene);
            }
            else if (CPUNumber == 3)
            {
                Lists.CPU4Stations.Remove(this);
                //updating the UI of stations information of player that lost the station 
                SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(4, Lists.CPU4Stations.Count, Lists.CPU4CruisersOnScene);
            }
            if (CPUSceneCruiserLaunchCoroutineIsOn)
            {
                CPUSceneCruiserLaunchTimer = 0; //dropping the timer to prevent the timer on another station to count the cruiser launche
                CPUSceneCruiserLaunchCoroutineIsOn = false; //stops the process of creating the cruiser;
            }
        }
        else {
            Lists.playerStations.Remove(this);
            //updating the UI of stations information of player that lost the station 
            SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(0, Lists.playerStations.Count, 1);
        }

        isPlayerStation = false;
        isCPUStation = false;
        isGuardStation = true;

        //that means this station is Guards, means it is not equal to any CPU number which are 0,1,2,3
        CPUNumber = 5;

        stationsEnergon.makeEnergonCPUs(CPUNumber);
        disactivateInfoAboutShip();

        clearAllPruductionBeforeUpgradeCPU();
        CancelInvoke(); 
        
        Cruis4 = 0;
        Cruis3 = 0;
        Cruis2 = 0;
        Cruis1 = 0;
        CruisG = 0;
        Destr4 = 0;
        Destr3 = 0;
        Destr2 = 0;
        Destr2Par = 0;
        Destr1 = 0;
        Destr1Par = 0;
        DestrG = 0;
        Gun3 = 0;
        Gun2 = 0;
        Gun1 = 0;
        MiniGun = 0;
        Fighter = 0;

        if (Lists.currentLevel == 6)
        {
            CruisG = 5;
            DestrG = 10;
            Gun2 = 1;
            Fighter = 7;
        }
        else if (Lists.currentLevel == 7)
        {
            CruisG = 7;
            DestrG = 13;
            Gun1 = 2;
            Fighter = 10;
        }
        else if (Lists.currentLevel == 9)
        {
            CruisG = 8;
            DestrG = 15;
            Gun1 = 1;
            Gun2 = 1;
            Fighter = 12;
        }
        else if (Lists.currentLevel == 10)
        {
            CruisG = 10;
            DestrG = 20;
            Gun1 = 1;
            Gun2 = 1;
            Fighter = 13;
        }
        //updateFleetCountToDisplay();
        //CPUSceneCruiserLaunchCoroutineIsOn = false;
        //CPUSceneCruiserLaunchTimer = 0;
    }

    //update the display fleet count that is above the station on scene
    public void updateFleetCountToDisplay()
    {
        if (infoPanelLocal)
        {
            if (!isPlayerStation) miniInfoPanelObject.energyCount.text = energyOfStation.ToString("0");
            miniInfoPanelObject.fleetCount.text = assessFleetPower().ToString("0");
        }
    }

    public void setCPUStatioFleetAfterGettingEnergy(float energyGathered, bool isRecursionCallFromStation) {
        float energyLeft;
        if (isRecursionCallFromStation)
        {
            energyLeft = energyGathered;
        }
        else
        {
            if (stationCurrentLevel < upgradeCounts)
            {
                energyLeft = energyGathered * 0.8f;
                energyOfStationToUPGradeFoRCPU += energyGathered - energyLeft;
            }
            else energyLeft = energyGathered;
        }
        if (energyLeft > 0)
        {
            if (stationCurrentLevel == 0)
            {
                if (Cruis4 == 0)
                {
                    if ((energyLeft - Constants.Instance.C4ProdEnergy) > 0)
                    {
                        if (stationFleetCount() < Constants.Instance.shipsLimit)
                        {
                            energyLeft -= Constants.Instance.C4ProdEnergy;
                            Cruis4++;
                        }
                    }
                    else if ((energyLeft - Constants.Instance.D4ProdEnergy) > 0)
                    {
                        if (stationFleetCount() < Constants.Instance.shipsLimit)
                        {
                            energyLeft -= Constants.Instance.D4ProdEnergy;
                            Destr4++;
                        }
                    }
                }
                else
                {
                    if (Random.Range(0, 2) > 0)
                    {
                        if ((energyLeft - Constants.Instance.C4ProdEnergy) > 0)
                        {
                            if (stationFleetCount() < Constants.Instance.shipsLimit)
                            {
                                energyLeft -= Constants.Instance.C4ProdEnergy;
                                Cruis4++;
                            }
                        }
                        else if ((energyLeft - Constants.Instance.D4ProdEnergy) > 0)
                        {
                            if (stationFleetCount() < Constants.Instance.shipsLimit)
                            {
                                energyLeft -= Constants.Instance.D4ProdEnergy;
                                Destr4++;
                            }
                        }
                    }
                    else
                    {
                        if ((energyLeft - Constants.Instance.D4ProdEnergy) > 0)
                        {
                            if (stationFleetCount() < Constants.Instance.shipsLimit)
                            {
                                energyLeft -= Constants.Instance.D4ProdEnergy;
                                Destr4++;
                            }
                        }
                    }
                }
                if (energyLeft > Constants.Instance.D4ProdEnergy&& stationFleetCount() < Constants.Instance.shipsLimit) setCPUStatioFleetAfterGettingEnergy(energyLeft, true);
            }
            else if (stationCurrentLevel == 1)
            {
                if (Cruis3 == 0)
                {
                    if ((energyLeft - Constants.Instance.C3ProdEnergy) > 0)
                    {
                        if (stationFleetCount() < Constants.Instance.shipsLimit)
                        {
                            energyLeft -= Constants.Instance.C3ProdEnergy;
                            Cruis3++;
                        }
                    }
                    else if ((energyLeft - Constants.Instance.G1ProdEnergy) > 0 && (Gun1 + Gun2 + Gun3) < 2)
                    {
                        if (stationFleetCount() < Constants.Instance.shipsLimit)
                        {
                            energyLeft -= Constants.Instance.G1ProdEnergy;
                            Gun1++;
                        }
                    }
                    else if ((energyLeft - Constants.Instance.D3ProdEnergy) > 0)
                    {
                        if (stationFleetCount() < Constants.Instance.shipsLimit)
                        {
                            energyLeft -= Constants.Instance.D3ProdEnergy;
                            Destr3++;
                        }
                    }
                }
                else
                {
                    if (Random.Range(0, 2) > 0)
                    {
                        if ((energyLeft - Constants.Instance.C3ProdEnergy) > 0)
                        {
                            if (stationFleetCount() < Constants.Instance.shipsLimit)
                            {
                                energyLeft -= Constants.Instance.C3ProdEnergy;
                                Cruis3++;
                            }
                        }
                        else if ((energyLeft - Constants.Instance.G1ProdEnergy) > 0 && (Gun1 + Gun2 + Gun3) < 2)
                        {
                            if (stationFleetCount() < Constants.Instance.shipsLimit)
                            {
                                energyLeft -= Constants.Instance.G1ProdEnergy;
                                Gun1++;
                            }
                        }
                        else if ((energyLeft - Constants.Instance.D3ProdEnergy) > 0)
                        {
                            if (stationFleetCount() < Constants.Instance.shipsLimit)
                            {
                                energyLeft -= Constants.Instance.D3ProdEnergy;
                                Destr3++;
                            }
                        }
                    }
                    else
                    {
                        if ((energyLeft - Constants.Instance.G1ProdEnergy) > 0 && (Gun1 + Gun2 + Gun3) < 2)
                        {
                            if (stationFleetCount() < Constants.Instance.shipsLimit)
                            {
                                energyLeft -= Constants.Instance.G1ProdEnergy;
                                Gun1++;
                            }
                        }
                        else if ((energyLeft - Constants.Instance.D3ProdEnergy) > 0)
                        {
                            if (stationFleetCount() < Constants.Instance.shipsLimit)
                            {
                                energyLeft -= Constants.Instance.D3ProdEnergy;
                                Destr3++;
                            }
                        }
                        if (Fighter < 13) Fighter += 3;
                    }
                }
                if (energyLeft > Constants.Instance.D3ProdEnergy && stationFleetCount() < Constants.Instance.shipsLimit) setCPUStatioFleetAfterGettingEnergy(energyLeft, true);
            }
            else if (stationCurrentLevel == 2)
            {
                Gun1 = 0;
                if (Cruis2 == 0)
                {
                    if ((energyLeft - Constants.Instance.C2ProdEnergy) > 0)
                    {
                        if (stationFleetCount() < Constants.Instance.shipsPreLimit)
                        {
                            energyLeft -= Constants.Instance.C2ProdEnergy;
                            Cruis2++;
                        }
                        else if (stationFleetCount() < Constants.Instance.shipsLimit && (Gun1 + Gun2 + Gun3) < 2)
                        {
                            energyLeft -= Constants.Instance.G2ProdEnergy;
                            Gun2++;
                        }
                        else if (stationFleetCount() < Constants.Instance.shipsLimit)
                        {
                            if (UnityEngine.Random.Range(0, 2) > 0)
                            {
                                energyLeft -= Constants.Instance.D2ProdEnergy;
                                Destr2++;
                            }
                            else
                            {
                                energyLeft -= Constants.Instance.D2PProdEnergy;
                                Destr2Par++;
                            }
                        }
                    }
                    else if ((energyLeft - Constants.Instance.G2ProdEnergy) > 0 && (Gun1 + Gun2 + Gun3) < 2)
                    {
                        if (stationFleetCount() < Constants.Instance.shipsLimit)
                        {
                            energyLeft -= Constants.Instance.G2ProdEnergy;
                            Gun2++;
                        }
                    }
                    else if ((energyLeft - Constants.Instance.D2ProdEnergy) > 0)
                    {
                        if (stationFleetCount() < Constants.Instance.shipsLimit)
                        {
                            if (UnityEngine.Random.Range(0, 2) > 0)
                            {
                                energyLeft -= Constants.Instance.D2ProdEnergy;
                                Destr2++;
                            }
                            else
                            {
                                energyLeft -= Constants.Instance.D2PProdEnergy;
                                Destr2Par++;
                            }
                        }
                    }
                }
                else
                {
                    if (UnityEngine.Random.Range(0, 2) > 0)
                    {
                        if ((energyLeft - Constants.Instance.C2ProdEnergy) > 0)
                        {
                            if (stationFleetCount() < Constants.Instance.shipsPreLimit)
                            {
                                energyLeft -= Constants.Instance.C2ProdEnergy;
                                Cruis2++;
                            }
                            else if (stationFleetCount() < Constants.Instance.shipsLimit && (Gun1 + Gun2 + Gun3) < 2)
                            {
                                energyLeft -= Constants.Instance.G2ProdEnergy;
                                Gun2++;
                            }
                            else if (stationFleetCount() < Constants.Instance.shipsLimit)
                            {
                                if (UnityEngine.Random.Range(0, 2) > 0)
                                {
                                    energyLeft -= Constants.Instance.D2ProdEnergy;
                                    Destr2++;
                                }
                                else
                                {
                                    energyLeft -= Constants.Instance.D2PProdEnergy;
                                    Destr2Par++;
                                }
                            }
                        }
                        else if ((energyLeft - Constants.Instance.G2ProdEnergy) > 0 && (Gun1 + Gun2 + Gun3) < 2)
                        {
                            if (stationFleetCount() < Constants.Instance.shipsLimit)
                            {
                                energyLeft -= Constants.Instance.G2ProdEnergy;
                                Gun2++;
                            }
                        }
                        else if ((energyLeft - Constants.Instance.D2ProdEnergy) > 0)
                        {
                            if (stationFleetCount() < Constants.Instance.shipsLimit)
                            {
                                if (UnityEngine.Random.Range(0, 2) > 0)
                                {
                                    energyLeft -= Constants.Instance.D2ProdEnergy;
                                    Destr2++;
                                }
                                else
                                {
                                    energyLeft -= Constants.Instance.D2PProdEnergy;
                                    Destr2Par++;
                                }
                            }
                        }
                    }
                    else
                    {
                        if ((energyLeft - Constants.Instance.G2ProdEnergy) > 0 && (Gun1 + Gun2 + Gun3) < 2)
                        {
                            if (stationFleetCount() < Constants.Instance.shipsLimit)
                            {
                                energyLeft -= Constants.Instance.G2ProdEnergy;
                                Gun2++;
                            }
                        }
                        else if ((energyLeft - Constants.Instance.D2ProdEnergy) > 0)
                        {
                            if (stationFleetCount() < Constants.Instance.shipsLimit)
                            {
                                if (UnityEngine.Random.Range(0, 2) > 0)
                                {
                                    energyLeft -= Constants.Instance.D2ProdEnergy;
                                    Destr2++;
                                }
                                else
                                {
                                    energyLeft -= Constants.Instance.D2PProdEnergy;
                                    Destr2Par++;
                                }
                            }
                        }
                        if (Fighter < 16) Fighter += 3;
                    }
                }
                if (energyLeft > Constants.Instance.D2ProdEnergy && stationFleetCount() < Constants.Instance.shipsLimit) setCPUStatioFleetAfterGettingEnergy(energyLeft, true);
            }
            else if (stationCurrentLevel == 3)
            {
                Gun2 = 0;
                Gun1 = 0;
                if (Cruis1 == 0)
                {
                    if ((energyLeft - Constants.Instance.C1ProdEnergy) > 0)
                    {
                        if (stationFleetCount() < Constants.Instance.shipsPreLimit)
                        {
                            energyLeft -= Constants.Instance.C1ProdEnergy;
                            Cruis1++;
                        }
                        else if (stationFleetCount() < Constants.Instance.shipsLimit && (Gun1 + Gun2 + Gun3) < 2)
                        {
                            energyLeft -= Constants.Instance.G3ProdEnergy;
                            Gun3++;
                        }
                        else if (stationFleetCount() < Constants.Instance.shipsLimit)
                        {
                            if (UnityEngine.Random.Range(0, 2) > 0)
                            {
                                energyLeft -= Constants.Instance.D1ProdEnergy;
                                Destr1++;
                            }
                            else
                            {
                                energyLeft -= Constants.Instance.D1PProdEnergy;
                                Destr1Par++;
                            }
                        }
                    }
                    else if ((energyLeft - Constants.Instance.G3ProdEnergy) > 0 && (Gun1 + Gun2 + Gun3) < 2)
                    {
                        if (stationFleetCount() < Constants.Instance.shipsLimit)
                        {
                            energyLeft -= Constants.Instance.G3ProdEnergy;
                            Gun3++;
                        }
                    }
                    else if ((energyLeft - Constants.Instance.D1ProdEnergy) > 0)
                    {
                        if (stationFleetCount() < Constants.Instance.shipsLimit)
                        {
                            if (UnityEngine.Random.Range(0, 2) > 0)
                            {
                                energyLeft -= Constants.Instance.D1ProdEnergy;
                                Destr1++;
                            }
                            else
                            {
                                energyLeft -= Constants.Instance.D1PProdEnergy;
                                Destr1Par++;
                            }
                        }
                    }
                }
                else
                {
                    if (UnityEngine.Random.Range(0, 2) > 0)
                    {
                        if ((energyLeft - Constants.Instance.C1ProdEnergy) > 0)
                        {
                            if (stationFleetCount() < Constants.Instance.shipsPreLimit)
                            {
                                energyLeft -= Constants.Instance.C1ProdEnergy;
                                Cruis1++;
                            }
                            else if (stationFleetCount() < Constants.Instance.shipsLimit && (Gun1 + Gun2 + Gun3) < 2)
                            {
                                energyLeft -= Constants.Instance.G3ProdEnergy;
                                Gun3++;
                            }
                            else if (stationFleetCount() < Constants.Instance.shipsLimit)
                            {
                                if (UnityEngine.Random.Range(0, 2) > 0)
                                {
                                    energyLeft -= Constants.Instance.D1ProdEnergy;
                                    Destr1++;
                                }
                                else
                                {
                                    energyLeft -= Constants.Instance.D1PProdEnergy;
                                    Destr1Par++;
                                }
                            }
                        }
                        else if ((energyLeft - Constants.Instance.G3ProdEnergy) > 0 && (Gun1 + Gun2 + Gun3) < 2)
                        {
                            if (stationFleetCount() < Constants.Instance.shipsLimit)
                            {
                                energyLeft -= Constants.Instance.G3ProdEnergy;
                                Gun3++;
                            }
                        }
                        else if ((energyLeft - Constants.Instance.D1ProdEnergy) > 0)
                        {
                            if (stationFleetCount() < Constants.Instance.shipsLimit)
                            {
                                if (UnityEngine.Random.Range(0, 2) > 0)
                                {
                                    energyLeft -= Constants.Instance.D1ProdEnergy;
                                    Destr1++;
                                }
                                else
                                {
                                    energyLeft -= Constants.Instance.D1PProdEnergy;
                                    Destr1Par++;
                                }
                            }
                        }
                    }
                    else
                    {
                        if ((energyLeft - Constants.Instance.G3ProdEnergy) > 0 && (Gun1 + Gun2 + Gun3) < 2)
                        {
                            if (stationFleetCount() < Constants.Instance.shipsLimit)
                            {
                                energyLeft -= Constants.Instance.G3ProdEnergy;
                                Gun3++;
                            }
                        }
                        else if ((energyLeft - Constants.Instance.D1ProdEnergy) > 0)
                        {
                            if (stationFleetCount() < Constants.Instance.shipsLimit)
                            {
                                if (UnityEngine.Random.Range(0, 2) > 0)
                                {
                                    energyLeft -= Constants.Instance.D1ProdEnergy;
                                    Destr1++;
                                }
                                else
                                {
                                    energyLeft -= Constants.Instance.D1PProdEnergy;
                                    Destr1Par++;
                                }
                            }
                        }
                        if (Fighter < 18) Fighter += 3;
                    }
                }
                if (energyLeft > Constants.Instance.D1ProdEnergy && stationFleetCount() < Constants.Instance.shipsLimit) setCPUStatioFleetAfterGettingEnergy(energyLeft, true);
            }
        }
        if (!isRecursionCallFromStation)
        {
            if (CPUNumber != 5)
            {
                if (stationFleetCount() > (Constants.Instance.shipsLimit - 8) && (Cruis1 + Cruis2 + Cruis3 + Cruis4) > 2 && stationFleetCount() < Constants.Instance.shipsLimit)
                {
                    //this is not actually the revenge attack, this is purposeful attack of station. so only the last param matters
                    if (Random.Range(0, 3) > 1) launchingAProperCruiserToAttackAStationAsRevenge(0, false, false);
                }
                else if (stationFleetCount() == Constants.Instance.shipsLimit && (Cruis1 + Cruis2 + Cruis3 + Cruis4) > 2)
                {
                    //this is not actually the revenge attack, this is purposeful attack of station. so only the last param matters
                    launchingAProperCruiserToAttackAStationAsRevenge(0, false, false);
                }
                if (energyOfStationToUPGradeFoRCPU > 90 && Lists.emptyStations.Count > 0) launchingAProperCruiserWithFleet();
            }
            else
            {
                if (stationFleetCount() == Constants.Instance.shipsLimit)
                {
                    if (energyOfStation < 1500) energyOfStation += energyLeft;
                    if (energyOfStation > 1500) energyOfStation = 1500;
                }
            }
            if (ifCPUstationHasEnergyToUpgrade()) upgradeCPUStation();
        }
        updateFleetCountToDisplay();
    }

    private bool ifCPUstationHasEnergyToUpgrade() {
        if ((energyOfStationToUPGradeFoRCPU > Constants.Instance.enrgy0to1Upgrd && stationCurrentLevel == 0 && stationCurrentLevel < upgradeCounts)
             || (energyOfStationToUPGradeFoRCPU > Constants.Instance.enrgy1to2Upgrd && stationCurrentLevel == 1 && stationCurrentLevel < upgradeCounts)
             || (energyOfStationToUPGradeFoRCPU > Constants.Instance.enrgy2to3Upgrd && stationCurrentLevel == 2 && stationCurrentLevel < upgradeCounts)) 
            return true;
        else return false;
    }
    
    //gradually increasing the fleet of station according the step of fleet increase, current level of station and currentLevelDifficulty 
    public void setCPUStationFleetByLvl() {

        //clearing all fleet before assigning a new fleet to station
        Cruis4 = 0;
        Cruis3 = 0;
        Cruis2 = 0;
        Cruis1 = 0;
        CruisG = 0;
        Destr4 = 0;
        Destr3 = 0;
        Destr2 = 0;
        Destr2Par = 0;
        Destr1 = 0;
        Destr1Par = 0;
        DestrG = 0;
        Gun3 = 0;
        Gun2 = 0;
        Gun1 = 0;
        MiniGun = 0;
        Fighter = 0;
        if (Lists.currentLevelDifficulty == 0)
        {
            //Cruis4 = Random.Range(4, 8);
            //Destr4 = Random.Range(14, 17);
            Cruis4 = Random.Range(2,4);
            Destr4 = Random.Range(8, 10);
        }
        else if (Lists.currentLevelDifficulty == 1)
        {
            //Cruis4 = Random.Range(7, 10);
            //Destr4 = Random.Range(19, 25);
            Cruis4 = Random.Range(4, 7);
            Destr4 = Random.Range(11, 13);
        }
        else if (Lists.currentLevelDifficulty == 2)
        {
            Cruis4 = Random.Range(10, 13);
            Destr4 = Random.Range(14, 16);
        }
        //updateFleetCountToDisplay();

        #region commented out lines
        //TODO to finish other level station fleets in each step of fleet increasing
        //if (fleetIncreaseStep == 0)
        //{
        //    //TODO to finish other level station fleets
        //    if (stationCurrentLevel == 0)
        //    {
        //        if (Lists.currentLevelDifficulty == 0)
        //        {
        //            Cruis4 = 2;
        //            Destr4 = 7;
        //        }
        //        else if (Lists.currentLevelDifficulty == 1)
        //        {
        //            Cruis4 = 4;
        //            Destr4 = 7;
        //        }
        //        else if (Lists.currentLevelDifficulty == 2)
        //        {
        //            Cruis4 = 7;
        //            Destr4 = 9;
        //        }
        //    }
        //    else if (stationCurrentLevel == 1)
        //    {
        //        if (Lists.currentLevelDifficulty == 0)
        //        {
        //            Cruis4 = 1;
        //            Destr4 = 3;
        //            Cruis3 = 1;
        //            Destr3 = 3;
        //            Fighter = 5;
        //        }
        //        else if (Lists.currentLevelDifficulty == 1)
        //        {
        //            Cruis4 = 1;
        //            Destr4 = 3;
        //            Cruis3 = 1;
        //            Destr3 = 5;
        //            Gun1 = 1;
        //            Fighter = 5;
        //        }
        //        else if (Lists.currentLevelDifficulty == 2)
        //        {
        //            Cruis4 = 3;
        //            Destr4 = 3;
        //            Cruis3 = 2;
        //            Destr3 = 6;
        //            Gun1 = 1;
        //            Fighter = 8;
        //        }
        //    }
        //    else if (stationCurrentLevel == 2)
        //    {
        //        if (Lists.currentLevelDifficulty == 1)
        //        {
        //            Cruis4 = 1;
        //            Destr4 = 1;
        //            Cruis3 = 1;
        //            Destr3 = 2;
        //            Cruis2 = 1;
        //            Destr2 = 2;
        //            Destr2Par = 2;
        //            Gun1 = 1;
        //            Gun2 = 0;
        //            Fighter = 7;
        //        }
        //        else if (Lists.currentLevelDifficulty == 2)
        //        {
        //            Cruis4 = 1;
        //            Destr4 = 1;
        //            Cruis3 = 1;
        //            Destr3 = 2;
        //            Cruis2 = 1;
        //            Destr2 = 4;
        //            Destr2Par = 5;
        //            Gun1 = 2;
        //            Gun2 = 0;
        //            Fighter = 9;
        //        }
        //    }
        //    else if (stationCurrentLevel == 3)
        //    {
        //        if (Lists.currentLevelDifficulty == 1)
        //        {
        //            Cruis4 =2;
        //            Destr4 = 1;
        //            Cruis3 = 1;
        //            Destr3 = 2;
        //            Cruis2 = 1;
        //            Destr2 = 2;
        //            Destr2Par = 2; 
        //            Cruis1 = 0;
        //            Destr1 = 1;
        //            Destr1Par = 1;
        //            Gun1 = 0;
        //            Gun2 = 1;
        //            Gun3 = 0;
        //            Fighter = 8;
        //        }
        //        else if (Lists.currentLevelDifficulty == 2)
        //        {
        //            Cruis4 = 1;
        //            Destr4 = 1;
        //            Cruis3 = 1;
        //            Destr3 = 1;
        //            Cruis2 = 1;
        //            Destr2 = 2;
        //            Destr2Par = 1;
        //            Cruis1 = 0;
        //            Destr1 = 4;
        //            Destr1Par = 5;
        //            Gun1 =1;
        //            Gun2 = 1;
        //            Gun3 = 0;
        //            Fighter = 12;
        //        }
        //    }
        //    fleetIncreaseStep++;
        //    CancelInvoke("setCPUStationFleetByLvl");
        //    updateFleetOfCPUStation();
        //}
        //else if (fleetIncreaseStep == 1)
        //{
        //    if (stationCurrentLevel == 0)
        //    {
        //        if (Lists.currentLevelDifficulty == 0)
        //        {
        //            Cruis4 = 4;
        //            Destr4 = 10;
        //        }
        //        else if (Lists.currentLevelDifficulty == 1)
        //        {
        //            Cruis4 = 5;
        //            Destr4 = 13;
        //        }
        //        else if (Lists.currentLevelDifficulty == 2)
        //        {
        //            Cruis4 = 10;
        //            Destr4 = 15;
        //        }
        //    }
        //    else if (stationCurrentLevel == 1)
        //    {
        //        if (Lists.currentLevelDifficulty == 0)
        //        {
        //            Cruis4 = 1;
        //            Destr4 = 5;
        //            Cruis3 = 2;
        //            Destr3 = 5;
        //            Gun1 = 1;
        //            Fighter = 7;
        //        }
        //        else if (Lists.currentLevelDifficulty == 1)
        //        {
        //            Cruis4 = 1;
        //            Destr4 = 5;
        //            Cruis3 = 3;
        //            Destr3 = 9;
        //            Gun1 = 1;
        //            Fighter = 8;
        //        }
        //        else if (Lists.currentLevelDifficulty == 2)
        //        {
        //            Cruis4 = 4;
        //            Destr4 = 4;
        //            Cruis3 = 4;
        //            Destr3 = 12;
        //            Gun1 = 2;
        //            Fighter = 11;
        //        }
        //    }
        //    else if (stationCurrentLevel == 2)
        //    {
        //        if (Lists.currentLevelDifficulty == 1)
        //        {
        //            Cruis4 = 2;
        //            Destr4 = 2;
        //            Cruis3 = 2;
        //            Destr3 = 3;
        //            Cruis2 = 1;
        //            Destr2 = 4;
        //            Destr2Par = 4;
        //            Gun1 = 2;
        //            Gun2 = 0;
        //            Fighter = 11;
        //        }
        //        else if (Lists.currentLevelDifficulty == 2)
        //        {
        //            Cruis4 = 1;
        //            Destr4 = 2;
        //            Cruis3 = 3;
        //            Destr3 = 3;
        //            Cruis2 = 2;
        //            Destr2 = 7;
        //            Destr2Par = 8;
        //            Gun1 =1;
        //            Gun2 = 1;
        //            Fighter = 12;
        //        }
        //    }
        //    else if (stationCurrentLevel == 3)
        //    {
        //        if (Lists.currentLevelDifficulty == 1)
        //        {
        //            Cruis4 = 2;
        //            Destr4 = 2;
        //            Cruis3 = 2;
        //            Destr3 = 3;
        //            Cruis2 = 1;
        //            Destr2 = 3;
        //            Destr2Par = 3;
        //            Cruis1 = 0;
        //            Destr1 = 2;
        //            Destr1Par = 2;
        //            Gun1 = 1;
        //            Gun2 = 1;
        //            Gun3 = 0;
        //            Fighter = 8;
        //        }
        //        else if (Lists.currentLevelDifficulty == 2)
        //        {
        //            Cruis4 = 1;
        //            Destr4 = 0;
        //            Cruis3 = 3;
        //            Destr3 = 2;
        //            Cruis2 = 1;
        //            Destr2 = 2;
        //            Destr2Par = 2;
        //            Cruis1 = 1;
        //            Destr1 = 8;
        //            Destr1Par = 10;
        //            Gun1 = 1;
        //            Gun2 = 0;
        //            Gun3 = 1;
        //            Fighter = 14;
        //        }
        //    }
        //    fleetIncreaseStep++;
        //    CancelInvoke("setCPUStationFleetByLvl");
        //    updateFleetOfCPUStation();
        //}
        //else if (fleetIncreaseStep == 2)
        //{
        //    if (stationCurrentLevel == 0)
        //    {
        //        if (Lists.currentLevelDifficulty == 0)
        //        {
        //            Cruis4 = 6;
        //            Destr4 = 15;
        //        }
        //        else if (Lists.currentLevelDifficulty == 1)
        //        {
        //            Cruis4 = 8;
        //            Destr4 = 17;
        //        }
        //        else if (Lists.currentLevelDifficulty == 2)
        //        {
        //            Cruis4 = 15;
        //            Destr4 = 20;
        //        }
        //    }
        //    else if (stationCurrentLevel == 1)
        //    {
        //        if (Lists.currentLevelDifficulty == 0)
        //        {
        //            Cruis4 = 3;
        //            Destr4 = 8;
        //            Cruis3 = 4;
        //            Destr3 = 7;
        //            Gun1 = 1;
        //            Fighter = 8;
        //        }
        //        else if (Lists.currentLevelDifficulty == 1)
        //        {
        //            Cruis4 = 2;
        //            Destr4 = 7;
        //            Cruis3 = 5;
        //            Destr3 = 13;
        //            Gun1 = 2;
        //            Fighter = 11;
        //        }
        //        else if (Lists.currentLevelDifficulty == 2)
        //        {
        //            Cruis4 = 5;
        //            Destr4 = 5;
        //            Cruis3 = 5;
        //            Destr3 = 15;
        //            Gun1 = 2;
        //            Fighter = 15;
        //        }
        //    }
        //    else if (stationCurrentLevel == 2)
        //    {
        //        if (Lists.currentLevelDifficulty == 1)
        //        {
        //            Cruis4 = 2;
        //            Destr4 = 3;
        //            Cruis3 = 2;
        //            Destr3 = 4;
        //            Cruis2 = 1;
        //            Destr2 = 5;
        //            Destr2Par = 5;
        //            Gun1 = 1;
        //            Gun2 = 1;
        //            Fighter = 15;
        //        }
        //        else if (Lists.currentLevelDifficulty == 2)
        //        {
        //            Cruis4 = 2;
        //            Destr4 = 2;
        //            Cruis3 = 4;
        //            Destr3 = 5;
        //            Cruis2 = 2;
        //            Destr2 = 8;
        //            Destr2Par = 10;
        //            Gun1 = 0;
        //            Gun2 = 2;
        //            Fighter = 16;
        //        }
        //    }
        //    else if (stationCurrentLevel == 3)
        //    {
        //        if (Lists.currentLevelDifficulty == 1)
        //        {
        //            Cruis4 = 2;
        //            Destr4 = 2;
        //            Cruis3 = 2;
        //            Destr3 = 6;
        //            Cruis2 = 1;
        //            Destr2 = 4;
        //            Destr2Par = 4;
        //            Cruis1 = 0;
        //            Destr1 = 3;
        //            Destr1Par =4;
        //            Gun1 = 0;
        //            Gun2 = 2;
        //            Gun3 = 0;
        //            Fighter =17;
        //        }
        //        else if (Lists.currentLevelDifficulty == 2)
        //        {
        //            Cruis4 = 1;
        //            Destr4 = 0;
        //            Cruis3 = 3;
        //            Destr3 = 2;
        //            Cruis2 = 2;
        //            Destr2 = 2;
        //            Destr2Par = 2;
        //            Cruis1 = 1;
        //            Destr1 = 10;
        //            Destr1Par = 11;
        //            Gun1 = 0;
        //            Gun2 = 1;
        //            Gun3 = 1;
        //            Fighter = 18;
        //        }
        //    }
        //    fleetIncreaseStep++;
        //    CancelInvoke("setCPUStationFleetByLvl");
        //    updateFleetOfCPUStation();
        //}
        //else if (fleetIncreaseStep == 3)
        //{
        //    if (stationCurrentLevel == 0)
        //    {
        //        if (Lists.currentLevelDifficulty == 0)
        //        {
        //            Cruis4 = 8;
        //            Destr4 = 20;
        //        }
        //        else if (Lists.currentLevelDifficulty == 1)
        //        {
        //            Cruis4 = 12;
        //            Destr4 = 26;
        //        }
        //        else if (Lists.currentLevelDifficulty == 2)
        //        {
        //            Cruis4 = 20;
        //            Destr4 = 25;
        //        }
        //    }
        //    else if (stationCurrentLevel == 1)
        //    {
        //        if (Lists.currentLevelDifficulty == 0)
        //        {
        //            Cruis4 = 4;
        //            Destr4 = 11;
        //            Cruis3 = 4;
        //            Destr3 = 10;
        //            Gun1 = 2;
        //            Fighter = 11;
        //        }
        //        else if (Lists.currentLevelDifficulty == 1)
        //        {
        //            Cruis4 = 4;
        //            Destr4 = 9;
        //            Cruis3 = 8;
        //            Destr3 = 16;
        //            Gun1 = 2;
        //            Fighter = 15;
        //        }
        //        else if (Lists.currentLevelDifficulty == 2)
        //        {
        //            Cruis4 = 6;
        //            Destr4 = 6;
        //            Cruis3 = 7;
        //            Destr3 = 24;
        //            Gun1 = 2;
        //            Fighter = 19;
        //        }
        //    }
        //    else if (stationCurrentLevel == 2)
        //    {
        //        if (Lists.currentLevelDifficulty == 1)
        //        {
        //            Cruis4 = 3;
        //            Destr4 = 5;
        //            Cruis3 = 3;
        //            Destr3 = 7;
        //            Cruis2 = 2;
        //            Destr2 = 7;
        //            Destr2Par = 8;
        //            Gun1 = 1;
        //            Gun2 = 1;
        //            Fighter = 18;
        //        }
        //        else if (Lists.currentLevelDifficulty == 2)
        //        {
        //            Cruis4 = 2;
        //            Destr4 = 1;
        //            Cruis3 = 5;
        //            Destr3 = 6;
        //            Cruis2 = 3;
        //            Destr2 = 10;
        //            Destr2Par = 10;
        //            Gun1 = 0;
        //            Gun2 = 2;
        //            Fighter = 19;
        //        }
        //    }
        //    else if (stationCurrentLevel == 3)
        //    {
        //        if (Lists.currentLevelDifficulty == 1)
        //        {
        //            Cruis4 = 2;
        //            Destr4 = 3;
        //            Cruis3 = 2;
        //            Destr3 = 7;
        //            Cruis2 = 1;
        //            Destr2 = 6;
        //            Destr2Par = 6;
        //            Cruis1 = 1;
        //            Destr1 = 5;
        //            Destr1Par = 6;
        //            Gun1 = 1;
        //            Gun2 = 0;
        //            Gun3 = 1;
        //            Fighter = 19;
        //        }
        //        else if (Lists.currentLevelDifficulty == 2)
        //        {
        //            Cruis4 = 0;
        //            Destr4 = 0;
        //            Cruis3 = 0;
        //            Destr3 = 0;
        //            Cruis2 = 3;
        //            Destr2 = 0;
        //            Destr2Par = 0;
        //            Cruis1 =3;
        //            Destr1 = 13;
        //            Destr1Par = 12;
        //            Gun1 = 0;
        //            Gun2 = 0;
        //            Gun3 = 2;
        //            Fighter = 20;
        //        }
        //    }
        //}
        //updateFleetCountToDisplay();

        #endregion commented out lines
    }

    //public void graduallyIncreaseTheFleet() {
    //    destrProductionCount++;
    //    if (stationCurrentLevel == 0)
    //    {
    //        if (stationFleetCount() < Constants.Instance.shipsLimit)
    //        {
    //            if (destrProductionCount > 4)
    //            {
    //                Cruis4++;
    //                destrProductionCount = 0;
    //                CPUStationFleetIncreaserTimer = Constants.Instance.timeCreateDestr;
    //            }
    //            else
    //            {
    //                Destr4++;
    //                if (destrProductionCount==4) CPUStationFleetIncreaserTimer = Constants.Instance.timeCreateCruiser;
    //                else CPUStationFleetIncreaserTimer = Constants.Instance.timeCreateDestr;
    //            }
                
    //        }
            
    //    }
    //    else if (stationCurrentLevel == 1) {
    //        if (stationFleetCount() < Constants.Instance.shipsLimit)
    //        {
    //            if (destrProductionCount > 4)
    //            {
    //                Cruis3++;
    //                destrProductionCount = 0;
    //                CPUStationFleetIncreaserTimer = Constants.Instance.timeCreateDestr;
    //                if (Fighter < 13) Fighter += 3;
    //            }
    //            else
    //            {
    //                if (Gun1 < 2) Gun1++;
    //                else Destr3++;
    //                if (destrProductionCount == 4) CPUStationFleetIncreaserTimer = Constants.Instance.timeCreateCruiser;
    //                else CPUStationFleetIncreaserTimer = Constants.Instance.timeCreateDestr;
    //            }

    //        }
    //    }
    //    else if (stationCurrentLevel == 2)
    //    {
    //        Gun1 = 0;
    //        if (destrProductionCount > 4)
    //        {
    //            if (stationFleetCount() < Constants.Instance.shipsPreLimit)
    //            {
    //                Cruis2++;
    //                destrProductionCount = 0;
    //                CPUStationFleetIncreaserTimer = Constants.Instance.timeCreateDestr;
    //            }
    //            else if (stationFleetCount() < Constants.Instance.shipsLimit) {
    //                if (Gun2 < 2) Gun2++;
    //                else
    //                {
    //                    if (Random.Range(0, 2) < 1) Destr2++;
    //                    else Destr2Par++;
    //                }
    //                CPUStationFleetIncreaserTimer = Constants.Instance.timeCreateDestr;
    //            }
    //            if (Fighter < 16) Fighter += 3;
    //        }
    //        else {
    //            if (stationFleetCount() < Constants.Instance.shipsLimit)
    //            {
    //                if (Gun2 < 2) Gun2++;
    //                else
    //                {
    //                    if (Random.Range(0, 2) < 1) Destr2++;
    //                    else Destr2Par++;
    //                }
    //                if (destrProductionCount == 4) CPUStationFleetIncreaserTimer = Constants.Instance.timeCreateCruiser;
    //                else CPUStationFleetIncreaserTimer = Constants.Instance.timeCreateDestr;
    //            }
    //        }
    //    }
    //    else if (stationCurrentLevel == 3)
    //    {
    //        Gun2 = 0;
    //        Gun1 = 0;
    //        if (destrProductionCount > 4)
    //        {
    //            if (stationFleetCount() < Constants.Instance.shipsPreLimit)
    //            {
    //                Cruis1++;
    //                destrProductionCount = 0;
    //                CPUStationFleetIncreaserTimer = Constants.Instance.timeCreateDestr;
    //            }
    //            else if (stationFleetCount() < Constants.Instance.shipsLimit)
    //            {
    //                if (Gun3 < 2) Gun3++;
    //                else
    //                {
    //                    if (Random.Range(0, 2) < 1) Destr1++;
    //                    else Destr1Par++;
    //                }
    //                CPUStationFleetIncreaserTimer = Constants.Instance.timeCreateDestr;
    //            }
    //            if (Fighter < 18) Fighter += 3;
    //        }
    //        else
    //        {
    //            if (stationFleetCount() < Constants.Instance.shipsLimit)
    //            {
    //                if (Gun3 < 2) Gun3++;
    //                else
    //                {
    //                    if (Random.Range(0, 2) < 1) Destr1++;
    //                    else Destr1Par++;
    //                }
    //                if (destrProductionCount == 4) CPUStationFleetIncreaserTimer = Constants.Instance.timeCreateCruiser;
    //                else CPUStationFleetIncreaserTimer = Constants.Instance.timeCreateDestr;
    //            }
    //        }
    //    }
    //    if (stationFleetCount() > (Constants.Instance.shipsLimit - 8) && (Cruis1 + Cruis2 + Cruis3 + Cruis4) > 2 && stationFleetCount() < Constants.Instance.shipsLimit)
    //    {
    //        //this is not actually the revenge attack, this is purposeful attack of station. so only the last param matters
    //        if (Random.Range(0, 3) > 1) launchingAProperCruiserToAttackAStationAsRevenge(0, false, false);
    //    }
    //    else if (stationFleetCount() == Constants.Instance.shipsLimit && (Cruis1 + Cruis2 + Cruis3 + Cruis4) > 2)
    //    {
    //        //this is not actually the revenge attack, this is purposeful attack of station. so only the last param matters
    //        launchingAProperCruiserToAttackAStationAsRevenge(0, false, false);
    //    }
    //}

    //gradually increasing the fleet of station according the step of fleet encrease, current level of station and currentLevelDifficulty 
    public void setGuardStationFleetByLvl()
    {
        //clearing all fleet before assigning a new fleet to station
        Cruis4 = 0;
        Cruis3 = 0;
        Cruis2 = 0;
        Cruis1 = 0;
        CruisG = 0;
        Destr4 = 0;
        Destr3 = 0;
        Destr2 = 0;
        Destr2Par = 0;
        Destr1 = 0;
        Destr1Par = 0;
        DestrG = 0;
        Gun3 = 0;
        Gun2 = 0;
        Gun1 = 0;
        MiniGun = 0;
        Fighter = 0;

        if (Lists.currentLevel == 3) {
            CruisG = 5;
            DestrG = 13;
            Gun1 = 2;
            Fighter = 9;
        }
        else if (Lists.currentLevel == 5 || Lists.currentLevel == 6)
        {
            CruisG = 7;
            DestrG = 18;
            Gun1 = 1;
            Gun2 = 1;
            Fighter = 11;
        }
        else if (Lists.currentLevel == 7)
        {
            CruisG = 10;
            DestrG = 23;
            Gun2 = 2;
            Fighter = 13;
        }
        else if (Lists.currentLevel == 9)
        {
            CruisG = 20;
            DestrG = 18;
            Gun2 = 1;
            Gun3 = 1;
            Fighter = 15;
        }
        else if (Lists.currentLevel ==10)
        {
            CruisG = 33;
            DestrG = 10;
            Gun2 = 1;
            Gun3 = 1;
            Fighter = 16;
        }
        //updateFleetCountToDisplay();
    }

    private void ChoseTheShipsForLaunchingCruiser(bool isStationAttack)
    {
        allCruisers.Clear();
        allDestrs.Clear();
        cruisersToLaunchCruiser.Clear();
        destrsToLaunchCruiser.Clear();


        for (int i = 0; i < Cruis4; i++)
        {
            allCruisers.Add(C4);
        }
        for (int i = 0; i < Cruis3; i++)
        {
            allCruisers.Add(C3);
        }
        for (int i = 0; i < CruisG; i++)
        {
            allCruisers.Add(CG);
        }
        for (int i = 0; i < Cruis2; i++)
        {
            allCruisers.Add(C2);
        }
        for (int i = 0; i < Cruis1; i++)
        {
            allCruisers.Add(C1);
        }

        for (int i = 0; i < Destr4; i++)
        {
            allDestrs.Add(D4);
        }
        for (int i = 0; i < Destr3; i++)
        {
            allDestrs.Add(D3);
        }
        for (int i = 0; i < DestrG; i++)
        {
            allDestrs.Add(DG);
        }
        for (int i = 0; i < Destr2; i++)
        {
            allDestrs.Add(D2);
        }
        for (int i = 0; i < Destr2Par; i++)
        {
            allDestrs.Add(D2P);
        }
        for (int i = 0; i < Destr1; i++)
        {
            allDestrs.Add(D1);
        }
        for (int i = 0; i < Destr1Par; i++)
        {
            allDestrs.Add(D1P);
        }

        if (isStationAttack)
        {
            if (Gun1 > 0) allDestrs.Add(G1);
            if (Gun2 > 0) allDestrs.Add(G2);
            if (Gun3 > 0) allDestrs.Add(G3);
            if (Fighter > 5)
            {
                for (int i = 0; i < 5; i++)
                {
                    allDestrs.Add(FI);
                }
            }

            float x = allCruisers.Count * Constants.Instance.shareOfCPUStationFleetToLaunchCruiserToAttackStation > 1 ? Mathf.Floor(allCruisers.Count * Constants.Instance.shareOfCPUStationFleetToLaunchCruiserToAttackStation) :
           Mathf.Ceil(allCruisers.Count * Constants.Instance.shareOfCPUStationFleetToLaunchCruiserToAttackStation);
            for (int i = 0; i < x; i++)
            {
                cruisersToLaunchCruiser.Add(allCruisers[i]);
            }
            float y = allDestrs.Count * Constants.Instance.shareOfCPUStationFleetToLaunchCruiserToAttackStation > 1 ? Mathf.Floor(allDestrs.Count * Constants.Instance.shareOfCPUStationFleetToLaunchCruiserToAttackStation) :
                Mathf.Ceil(allDestrs.Count * Constants.Instance.shareOfCPUStationFleetToLaunchCruiserToAttackStation);
            for (int i = 0; i < y; i++)
            {
                destrsToLaunchCruiser.Add(allDestrs[i]);
            }
        }
        else {
            float x = allCruisers.Count * Constants.Instance.shareOfCPUStationFleetToLaunchCruiser > 1 ? Mathf.Floor(allCruisers.Count * Constants.Instance.shareOfCPUStationFleetToLaunchCruiser) :
         Mathf.Ceil(allCruisers.Count * Constants.Instance.shareOfCPUStationFleetToLaunchCruiser);
            for (int i = 0; i < x; i++)
            {
                cruisersToLaunchCruiser.Add(allCruisers[i]);
            }
            float y = allDestrs.Count * Constants.Instance.shareOfCPUStationFleetToLaunchCruiser > 1 ? Mathf.Floor(allDestrs.Count * Constants.Instance.shareOfCPUStationFleetToLaunchCruiser) :
                Mathf.Ceil(allDestrs.Count * Constants.Instance.shareOfCPUStationFleetToLaunchCruiser);
            for (int i = 0; i < y; i++)
            {
                destrsToLaunchCruiser.Add(allDestrs[i]);
            }
        }
    }
    public void launchingAProperCruiserToAttackAStationAsRevenge(int CPUNumberToAttack, bool isPlayerStationToAttack, bool isRevenge)
    {
        if ((CPUNumber == 0 && Lists.CPU1CruisersOnScene<1)|| (CPUNumber == 1 && Lists.CPU2CruisersOnScene < 1)|| (CPUNumber == 2 && Lists.CPU3CruisersOnScene < 1)||(CPUNumber == 3 && Lists.CPU4CruisersOnScene < 1)) {
            ChoseTheShipsForLaunchingCruiser(true);
            if (cruisersToLaunchCruiser.Count > 0)
            {
                if (cruisersToLaunchCruiser.Contains(C1))
                {
                    cruiserListToActivate = ObjectPullerJourney.current.GetCruis1JourneyCPUPullList();
                    cruiserToActivate = ObjectPullerJourney.current.GetUniversalBullet(cruiserListToActivate);
                }
                else if (cruisersToLaunchCruiser.Contains(C2))
                {
                    //launching a proper type of cruiser on scene according the the station upgrade level 
                    cruiserListToActivate = ObjectPullerJourney.current.GetCruis2JourneyCPUPullList();
                    cruiserToActivate = ObjectPullerJourney.current.GetUniversalBullet(cruiserListToActivate);
                }
                else if (cruisersToLaunchCruiser.Contains(CG))
                {
                    //launching a proper type of cruiser on scene according the the station upgrade level 
                    cruiserListToActivate = ObjectPullerJourney.current.GetCruis2JourneyCPUPullList();
                    cruiserToActivate = ObjectPullerJourney.current.GetUniversalBullet(cruiserListToActivate);
                }
                else if (cruisersToLaunchCruiser.Contains(C3))
                {
                    //launching a proper type of cruiser on scene according the the station upgrade level 
                    cruiserListToActivate = ObjectPullerJourney.current.GetCruis3JourneyCPUPullList();
                    cruiserToActivate = ObjectPullerJourney.current.GetUniversalBullet(cruiserListToActivate);
                }
                else if (cruisersToLaunchCruiser.Contains(C4))
                {
                    //launching a proper type of cruiser on scene according the the station upgrade level 
                    cruiserListToActivate = ObjectPullerJourney.current.GetCruis4JourneyCPUPullList();
                    cruiserToActivate = ObjectPullerJourney.current.GetUniversalBullet(cruiserListToActivate);
                }


                //clearing the trail renderers on all components of ship to prevent that wierd effect of long trail
                foreach (TrailRenderer tr in cruiserToActivate.GetComponentsInChildren<TrailRenderer>()) tr.Clear();

                CPUCruisInstance = cruiserToActivate.GetComponent<CPUShipCtrlJourney>();

                CPUCruisInstance.CPUNumber = CPUNumber;
                cruiserToActivate.transform.position = transform.position;
                cruiserToActivate.transform.rotation = Quaternion.identity;

                //setting a proper color to sphere of CPU cruiser to make it has the same color that station CPU's has
                for (int i = 0; i < CPUCruisInstance.IDColorElements.Count; i++)
                {
                    CPUCruisInstance.IDColorElements[i].GetComponent<MeshRenderer>().material.SetColor("_Color", colorOfStationMat);
                }

                Lists.shipsOnScene.Add(cruiserToActivate); //adding the CPU cruiser to the list that will restore cruiser state after getting back to scene if it was in scene while switching
                                                           //this one is used while determining if player win the level, so if there left no enemy cruisers on level player wins
                                                           //a;so while updating the information about UI of this player stations
                /*Lists.enemyCruisersOnScene++;*/
                if (CPUNumber == 0) Lists.CPU1CruisersOnScene++;
                else if (CPUNumber == 1) Lists.CPU2CruisersOnScene++;
                else if (CPUNumber == 2) Lists.CPU3CruisersOnScene++;
                else if (CPUNumber == 3) Lists.CPU4CruisersOnScene++;

                settingACPUCruisFleet(CPUCruisInstance); //setting a fleet of cruiser 
                updateFleetCountToDisplay();
                cruiserToActivate.SetActive(true); //activating the CPU cruiser
                if (isRevenge) CPUCruisInstance.detectingWeakestStationToAttack(CPUNumberToAttack, isPlayerStationToAttack); //giving a task for a CPU cruiser
                else CPUCruisInstance.detectingWeakestStationToAttackFromAll(); //giving a task for a CPU cruiser
            }
        }
        //send a signal to UI of this player stations to start a signal of launched cruiser on scene
        //+ 1 is used cause 0 indes is occupied by player
        //SpaceCtrlr.Instance.turnOnOffLaunchedCruiserIcon(CPUNumber + 1, true);
        //pickARandomStationToLauncheStation(); //starting to launch a new cruiser timer by picking the station among this CPU stations right after it launched the cruiser
        //launchingACPUCruiserOnScene();
    }

    //this one is used only in cases to capture the empty station or to attack energons
    public void launchingAProperCruiserWithFleet()
    {
        if ((CPUNumber == 0 && Lists.CPU1CruisersOnScene < 1) || (CPUNumber == 1 && Lists.CPU2CruisersOnScene < 1) || (CPUNumber == 2 && Lists.CPU3CruisersOnScene < 1) 
            || (CPUNumber == 3 && Lists.CPU4CruisersOnScene < 1))
        {
            ChoseTheShipsForLaunchingCruiser(false);
            if (cruisersToLaunchCruiser.Count > 0)
            {

                if (cruisersToLaunchCruiser.Contains(C1))
                {
                    cruiserListToActivate = ObjectPullerJourney.current.GetCruis1JourneyCPUPullList();
                    cruiserToActivate = ObjectPullerJourney.current.GetUniversalBullet(cruiserListToActivate);
                }
                else if (cruisersToLaunchCruiser.Contains(C2))
                {
                    //launching a proper type of cruiser on scene according the the station upgrade level 
                    cruiserListToActivate = ObjectPullerJourney.current.GetCruis2JourneyCPUPullList();
                    cruiserToActivate = ObjectPullerJourney.current.GetUniversalBullet(cruiserListToActivate);
                }
                else if (cruisersToLaunchCruiser.Contains(CG))
                {
                    //launching a proper type of cruiser on scene according the the station upgrade level 
                    cruiserListToActivate = ObjectPullerJourney.current.GetCruis2JourneyCPUPullList();
                    cruiserToActivate = ObjectPullerJourney.current.GetUniversalBullet(cruiserListToActivate);
                }
                else if (cruisersToLaunchCruiser.Contains(C3))
                {
                    //launching a proper type of cruiser on scene according the the station upgrade level 
                    cruiserListToActivate = ObjectPullerJourney.current.GetCruis3JourneyCPUPullList();
                    cruiserToActivate = ObjectPullerJourney.current.GetUniversalBullet(cruiserListToActivate);
                }
                else if (cruisersToLaunchCruiser.Contains(C4))
                {
                    //launching a proper type of cruiser on scene according the the station upgrade level 
                    cruiserListToActivate = ObjectPullerJourney.current.GetCruis4JourneyCPUPullList();
                    cruiserToActivate = ObjectPullerJourney.current.GetUniversalBullet(cruiserListToActivate);
                }


                //clearing the trail renderers on all components of ship to prevent that wierd effect of long trail
                foreach (TrailRenderer tr in cruiserToActivate.GetComponentsInChildren<TrailRenderer>()) tr.Clear();

                CPUCruisInstance = cruiserToActivate.GetComponent<CPUShipCtrlJourney>();

                CPUCruisInstance.CPUNumber = CPUNumber;
                cruiserToActivate.transform.position = transform.position;
                cruiserToActivate.transform.rotation = Quaternion.identity;

                //setting a proper color to sphere of CPU cruiser to make it has the same color that station CPU's has
                for (int i = 0; i < CPUCruisInstance.IDColorElements.Count; i++)
                {
                    CPUCruisInstance.IDColorElements[i].GetComponent<MeshRenderer>().material.SetColor("_Color", colorOfStationMat);
                }


                Lists.shipsOnScene.Add(cruiserToActivate); //adding the CPU cruiser to the list that will restore cruiser state after getting back to scene if it was in scene while switching
                                                           //this one is used while determining if player win the level, so if there left no enemy cruisers on level player wins
                                                           //a;so while updating the information about UI of this player stations
                /*Lists.enemyCruisersOnScene++;*/
                if (CPUNumber == 0) Lists.CPU1CruisersOnScene++;
                else if (CPUNumber == 1) Lists.CPU2CruisersOnScene++;
                else if (CPUNumber == 2) Lists.CPU3CruisersOnScene++;
                else if (CPUNumber == 3) Lists.CPU4CruisersOnScene++;

                settingACPUCruisFleet(CPUCruisInstance); //setting a fleet of cruiser 
                updateFleetCountToDisplay();
                cruiserToActivate.SetActive(true); //activating the CPU cruiser
                CPUCruisInstance.detectingAnEmptyStationToMove(); //giving a task for a CPU cruiser
            }

        }
        //CPUSceneCruiserLaunchCoroutineIsOn = false; //giving a sygnal that this station is not on process of creating a scene cruiser (to use while upgrade of CPU station)
        //SpaceCtrlr.Instance.allCruisLaunchingCPUStations.Remove(this);
        //SpaceCtrlr.Instance.resetTheTimer();


        //send a signal to UI of this player stations to start a signal of launched cruiser on scene
        //+ 1 is used cause 0 indes is occupied by player
        //SpaceCtrlr.Instance.turnOnOffLaunchedCruiserIcon(CPUNumber + 1, true);
        //pickARandomStationToLauncheStation(); //starting to launch a new cruiser timer by picking the station among this CPU stations right after it launched the cruiser
        //launchingACPUCruiserOnScene();
    }

    //this method launches the laser of guard station and captures all station that are captured to laser
    public void launchingGuardsAttackLaser() {
        //closing any UI panel that is open while guard station attacks
        SpaceCtrlr.Instance.closeAnyPanel();
        List<StationController> attackStation = new List<StationController>();
        StationController sc;
        LineRenderer lr;
        CPUSceneCruiserLaunchCoroutineIsOn = false; //giving a sygnal that this station is not on process of next laser attack movement
        //setting active the laser of guard ststion. It is close automatically by the script that assignet to that game object (DestroyParticle)
        if (!guardStationLaser.activeInHierarchy)
        {
            guardStationLaser.SetActive(true);
        }

        //populating local method's collection to hold only the player's and non guard CPU station's
        for (int i = 0; i < Lists.AllStations.Count; i++) {
            if (!Lists.AllStations[i].isGuardStation) attackStation.Add(Lists.AllStations[i]);
        }

        lr = guardStationLaser.GetComponent<LineRenderer>();
        lr.positionCount = 0; //clearing the line renderer before next attack of guard station with laser

        //so there are only on lvls 6, 7, 9, 10 will be the guard stations with laser attack functions
        if (Lists.currentLevel == 6 || Lists.currentLevel == 7) {

            lr.positionCount = 2;
            //setting start position of line renderer (laser) to the pike of guard station antenna
            lr.SetPosition(0, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z));

            if (attackStation.Count > 0)
            {
                sc = attackStation[Random.Range(0, attackStation.Count)];
                attackStation.Remove(sc);
                lr.SetPosition(1, sc.stationPosition);
                sc.makeStatioGuards(); 

                if (attackStation.Count > 0)
                {
                    lr.positionCount = 3;
                    sc = attackStation[Random.Range(0, attackStation.Count)];
                    attackStation.Remove(sc);
                    lr.SetPosition(2, sc.stationPosition);
                    sc.makeStatioGuards();
                }
            }
            
        }
        else if (Lists.currentLevel == 9 || Lists.currentLevel == 10)
        {
            lr.positionCount = 2;
            lr.SetPosition(0, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z));
            
            if (attackStation.Count > 0)
            {
                sc = attackStation[Random.Range(0, attackStation.Count)];
                attackStation.Remove(sc);
                lr.SetPosition(1, sc.stationPosition);
                sc.makeStatioGuards();

                if (attackStation.Count > 0)
                {
                    lr.positionCount = 3;
                    sc = attackStation[Random.Range(0, attackStation.Count)];
                    attackStation.Remove(sc);
                    lr.SetPosition(2, sc.stationPosition);
                    sc.makeStatioGuards();

                    if (attackStation.Count > 0)
                    {
                        lr.positionCount =4;
                        sc = attackStation[Random.Range(0, attackStation.Count)];
                        attackStation.Remove(sc);
                        lr.SetPosition(3, sc.stationPosition);
                        sc.makeStatioGuards();
                    }
                }
            }
        }
        GuardStationLaserShotTimerSet(); //starting the process of guard station laser attack again
        SpaceCtrlr.Instance.GuardCoreLaser.Play(); //playing laser sound
    }

    //holding a time before launching a next CPU cruiser on scene (first time it is launched only on first station of CPU from SpaceCtrl class while instatiating the level)
    //public void launchingACPUCruiserOnScene()
    //{
    //    if (Lists.currentLevelDifficulty == 0) CPUSceneCruiserLaunchTimer = Random.Range(Constants.Instance.timeOfCPUCruisLauncheMinEasy, Constants.Instance.timeOfCPUCruisLauncheMaxEasy);
    //    else if(Lists.currentLevelDifficulty == 1) CPUSceneCruiserLaunchTimer = Random.Range(Constants.Instance.timeOfCPUCruisLauncheMinMiddle, Constants.Instance.timeOfCPUCruisLauncheMaxMiddle);
    //    else if(Lists.currentLevelDifficulty == 2) CPUSceneCruiserLaunchTimer = Random.Range(Constants.Instance.timeOfCPUCruisLauncheMinHard, Constants.Instance.timeOfCPUCruisLauncheMaxHard);
    //    CPUSceneCruiserLaunchCoroutineIsOn = true; //giving a sygnal that this station is on process of creating a scene cruiser (to use while upgrade of CPU station)
    //    SpaceCtrlr.Instance.allCruisLaunchingCPUStations.Add(this);
    //    SpaceCtrlr.Instance.resetTheTimer();
    //}

    //holding a time before starting the sonar process of guard (first time it is launched only on first station of CPU from SpaceCtrl class while instatiating the level)
    public void GuardStationLaserShotTimerSet()
    {
        if (Lists.currentLevel == 6 || Lists.currentLevel == 7 || Lists.currentLevel == 9 || Lists.currentLevel == 10)
        {
            if (Lists.currentLevel == 6) CPUSceneCruiserLaunchTimer = Constants.Instance.timeOfGuardCruisLauncheEasy;
            if (Lists.currentLevel == 7) CPUSceneCruiserLaunchTimer = Constants.Instance.timeOfGuardCruisLauncheMiddle;
            if (Lists.currentLevel == 9) CPUSceneCruiserLaunchTimer = Constants.Instance.timeOfGuardCruisLauncheMiddle;
            if (Lists.currentLevel == 10) CPUSceneCruiserLaunchTimer = Constants.Instance.timeOfGuardCruisLauncheHard;
            SpaceCtrlr.Instance.GuardTurnSlider.minValue = CPUSceneCruiserLaunchTimer * -1;
            SpaceCtrlr.Instance.GuardTurnSlider.maxValue = 0;
            CPUSceneCruiserLaunchCoroutineIsOn = true;
        }
    }

    //this method sets the newly launched CPU cruiser fleet according to the current level of station fleet
    private void settingACPUCruisFleet(CPUShipCtrlJourney CPUCruisCtrlr) {

        for (int i = 0; i < cruisersToLaunchCruiser.Count; i++) {
            if (cruisersToLaunchCruiser[i] == C1)
            {
                CPUCruisCtrlr.Cruis1++;
                Cruis1--;
            }
            else if (cruisersToLaunchCruiser[i] == C2)
            {
                CPUCruisCtrlr.Cruis2++;
                Cruis2--;
            }
            else if (cruisersToLaunchCruiser[i] == C3)
            {
                CPUCruisCtrlr.Cruis3++;
                Cruis3--;
            }
            else if (cruisersToLaunchCruiser[i] == C4)
            {
                CPUCruisCtrlr.Cruis4++;
                Cruis4--;
            }
            else if (cruisersToLaunchCruiser[i] == CG)
            {
                CPUCruisCtrlr.CruisG++;
                CruisG--;
            }
        }
        for (int i = 0; i < destrsToLaunchCruiser.Count; i++)
        {
            if (destrsToLaunchCruiser[i] == D1)
            {
                CPUCruisCtrlr.Destr1++;
                Destr1--;
            }
            else if (destrsToLaunchCruiser[i] == D1P)
            {
                CPUCruisCtrlr.Destr1Par++;
                Destr1Par--;
            }
            else if (destrsToLaunchCruiser[i] == D2)
            {
                CPUCruisCtrlr.Destr2++;
                Destr2--;
            }
            else if (destrsToLaunchCruiser[i] == D2P)
            {
                CPUCruisCtrlr.Destr2Par++;
                Destr2Par--;
            }
            else if (destrsToLaunchCruiser[i] == D3)
            {
                CPUCruisCtrlr.Destr3++;
                Destr3--;
            }
            else if (destrsToLaunchCruiser[i] == D4)
            {
                CPUCruisCtrlr.Destr4++;
                Destr4--;
            }
            else if (destrsToLaunchCruiser[i] == DG)
            {
                CPUCruisCtrlr.DestrG++;
                DestrG--;
            }
        }
        CPUCruisCtrlr.energy = 200;
    }
    
    //this method is for calling CPU station upgrade method and is called from startProcessesForCPU method while making this station CPUs after standar time passed
    //this method determines the time to upgrade the CPU station according to it's current level, higher the upgrade level longer the upgrade time
    //private float upgradeStationCPUTime() {
    //    float time = 0;
    //    if (stationCurrentLevel == 0) time = Constants.Instance.time0to1UpgrdCPU;
    //    else if (stationCurrentLevel == 1) time = Constants.Instance.time1to2UpgrdCPU;
    //    else if (stationCurrentLevel == 2) time = Constants.Instance.time2to3UpgrdCPU;
    //    return time;
    //}

   

    //this method upgrades the CPU station by disactivating the previous one and activating current
    private void upgradeCPUStation() {
        if (stationCurrentLevel == 0) energyOfStationToUPGradeFoRCPU -= Constants.Instance.enrgy0to1Upgrd;
        else if (stationCurrentLevel == 1) energyOfStationToUPGradeFoRCPU -= Constants.Instance.enrgy1to2Upgrd;
        else if (stationCurrentLevel == 2) energyOfStationToUPGradeFoRCPU -= Constants.Instance.enrgy2to3Upgrd;
        //CPUStationIsUpgradIng = 0; //stop the process of upgradtin on update method on this station but not on upgraded one
        //so here is a logic to upgrade a station from 0 to 1
        if (stationCurrentLevel == 0)
        {
            stationListToActivate = ObjectPullerJourney.current.GetStationDPullList();
            upgradedStationToActivate = ObjectPullerJourney.current.GetUniversalBullet(stationListToActivate);
            upgradedStationInstance = upgradedStationToActivate.GetComponent<StationController>();

            upgradedStationInstance.upgradeCounts = upgradeCounts;
            upgradedStationInstance.stationCurrentLevel = 1;
            upgradedStationInstance.nextUpgradeEnergyCount = Constants.Instance.enrgy1to2Upgrd;
            upgradedStationInstance.currentUpgradeTime = Constants.Instance.time1to2Upgrd;
        }

        //so here is a logic to upgrade a station from 1 to 2
        if (stationCurrentLevel == 1)
        {
            stationListToActivate = ObjectPullerJourney.current.GetStationCPullList();
            upgradedStationToActivate = ObjectPullerJourney.current.GetUniversalBullet(stationListToActivate);
            upgradedStationInstance = upgradedStationToActivate.GetComponent<StationController>();

            upgradedStationInstance.upgradeCounts = upgradeCounts;
            upgradedStationInstance.stationCurrentLevel = 2;
            upgradedStationInstance.nextUpgradeEnergyCount = Constants.Instance.enrgy2to3Upgrd;
            upgradedStationInstance.currentUpgradeTime = Constants.Instance.time2to3Upgrd;
        }
        //so here is a logic to upgrade a station from 2 to 3
        if (stationCurrentLevel == 2)
        {
            stationListToActivate = ObjectPullerJourney.current.GetStationFedPullList();
            upgradedStationToActivate = ObjectPullerJourney.current.GetUniversalBullet(stationListToActivate);
            upgradedStationInstance = upgradedStationToActivate.GetComponent<StationController>();

            upgradedStationInstance.upgradeCounts = upgradeCounts;
            upgradedStationInstance.stationCurrentLevel = 3;
            //only for setting a values to upgrade function properties, in fact there are no any upgrade levels any further
            upgradedStationInstance.nextUpgradeEnergyCount = Constants.Instance.enrgy2to3Upgrd;
            upgradedStationInstance.currentUpgradeTime = Constants.Instance.time2to3Upgrd;

        }

        upgradedStationInstance.isPlayerStation = isPlayerStation;
        upgradedStationInstance.isCPUStation = isCPUStation;
        upgradedStationInstance.isGuardStation = isGuardStation;
        upgradedStationInstance.CPUNumber = CPUNumber;

        upgradedStationInstance.stationPosition = stationPosition;
        upgradedStationToActivate.transform.position = stationPosition;
        upgradedStationToActivate.transform.rotation = Quaternion.identity;


        upgradedStationInstance.colorOfStationMat = colorOfStationMat;

        

        //so upgraded station lose one step of fleet kit
        //if (fleetIncreaseStep == 0) upgradedStationInstance.fleetIncreaseStep = fleetIncreaseStep;
        //else upgradedStationInstance.fleetIncreaseStep = fleetIncreaseStep - 1;


        upgradedStationInstance.startProcessesForCPUFromEmptyStation();

        //SpaceCtrlr.Instance.allCruisLaunchingCPUStations.Remove(this); //removing this station controller from the station candidates to display cruiser launching timer

        //starting the launche of scene cruiser on upgraded station if previous station was on process of launching a cruiser
        upgradedStationInstance.CPUSceneCruiserLaunchCoroutineIsOn = CPUSceneCruiserLaunchCoroutineIsOn;
        //if (CPUSceneCruiserLaunchCoroutineIsOn)
        //{
        //    //starting a process of creating a new cruiser on upgraded station
        //    upgradedStationInstance.launchingACPUCruiserOnScene();
        //}
        CPUSceneCruiserLaunchCoroutineIsOn = false;

        //this line is responsible to close the compare fleet panel while player attacking this station to preven any bugs
        if (SpaceCtrlr.Instance.fleetComparePanel.activeInHierarchy && playerCruiserNear) SpaceCtrlr.Instance.closeAnyPanel();

        //to set false the trigger for setting off the link sprite on player cruiser panel to prepare the Station game object to be used next time from pull
        playerCruiserNear = false;

        //adding current station instance to Static list to process it while saving and loading game data
        if (upgradedStationInstance.CPUNumber == 0)
        {
            Lists.CPU1Stations.Add(upgradedStationInstance);
            Lists.CPU1Stations.Remove(this); //removing current old station instance of class from static station instances list
        }
        else if (upgradedStationInstance.CPUNumber == 1)
        {
            Lists.CPU2Stations.Add(upgradedStationInstance);
            Lists.CPU2Stations.Remove(this); //removing current old station instance of class from static station instances list
        }
        else if(upgradedStationInstance.CPUNumber == 2)
        {
            Lists.CPU3Stations.Add(upgradedStationInstance);
            Lists.CPU3Stations.Remove(this); //removing current old station instance of class from static station instances list
        }
        else if(upgradedStationInstance.CPUNumber == 3)
        {
            Lists.CPU4Stations.Add(upgradedStationInstance);
            Lists.CPU4Stations.Remove(this); //removing current old station instance of class from static station instances list
        }
        else if (upgradedStationInstance.isGuardStation)
        {
            Lists.CPUGuardStations.Add(upgradedStationInstance);
            Lists.CPUGuardStations.Remove(this); //removing current old station instance of class from static station instances list
        }

        Lists.AllStations.Add(upgradedStationInstance);
        Lists.AllStations.Remove(this); //removing current old station instance of class from static station instances list

        //reconnecting the energon of station to upgradet one
        upgradedStationInstance.stationsEnergon = stationsEnergon;
        upgradedStationInstance.stationsEnergon.energonsStation = upgradedStationInstance;
        updateStationEnergon(upgradedStationInstance.stationCurrentLevel);

        upgradedStationToActivate.SetActive(true); 
        
        upgradedStationInstance.Cruis4 = Cruis4;
        upgradedStationInstance.Cruis3 = Cruis3;
        upgradedStationInstance.Cruis2 = Cruis2;
        upgradedStationInstance.Cruis1 = Cruis1;
        upgradedStationInstance.CruisG = CruisG;
        upgradedStationInstance.Destr4 = Destr4;
        upgradedStationInstance.Destr3 = Destr3;
        upgradedStationInstance.Destr2 = Destr2;
        upgradedStationInstance.Destr2Par = Destr2Par;
        upgradedStationInstance.Destr1 = Destr1;
        upgradedStationInstance.Destr1Par = Destr1Par;
        upgradedStationInstance.DestrG = DestrG;
        upgradedStationInstance.Gun1 = Gun1;
        upgradedStationInstance.Gun2 = Gun2;
        upgradedStationInstance.Gun3 = Gun3;
        upgradedStationInstance.Fighter = Fighter;
        //stopping all processes on current CPU station to prevent a bug of impossibility of calling any coroutines on disabled station
        StopAllCoroutines();
        CancelInvoke();

        //resetting the timer of CPU cruiser launche 
        //SpaceCtrlr.Instance.resetTheTimer();

        disablingThisStation(); //setting off current station 

    }

    //this method is necessary to prepare this class and StationController class to upgrade
    public void clearAllPruductionBeforeUpgradeCPU()
    {
        Cruis1Produc = 0;
        Cruis2Produc = 0;
        Cruis3Produc = 0;
        Cruis4Produc = 0;
        Destr1Produc = 0;
        Destr1ProducPar = 0;
        Destr2Produc = 0;
        Destr2ProducPar = 0;
        Destr3Produc = 0;
        Destr4Produc = 0;
        Gun1Produc = 0;
        Gun2Produc = 0;
        Gun3Produc = 0;
        MiniGunProduc = 0;
        FighterProduc = 0;

        productionPlan.Clear();

        //stop all productions
        energyProductionIsOn = false;
        shipProductionIsOn = false;

        currentPruductionFillLocal = 0;
    }

    //this method is for assessing the power of station fleet is called from differend classes to asses station power and take a decision of attack(called from attacking CPU cruiser)
    public int assessFleetPower()
    {
        int x = Cruis4 * Constants.Instance.Cruis4Index + Cruis3 * Constants.Instance.Cruis3Index + Cruis2 * Constants.Instance.Cruis2Index + Cruis1 * Constants.Instance.Cruis1Index + CruisG * Constants.Instance.Cruis2Index
            + Destr4 * Constants.Instance.Destr4Index + Destr3 * Constants.Instance.Destr3Index + Destr2 * Constants.Instance.Destr2Index + Destr2Par * Constants.Instance.Destr2Index
            + Destr1 * Constants.Instance.Destr1Index + Destr1Par * Constants.Instance.Destr1Index + DestrG * Constants.Instance.Destr2Index+ Gun1 * Constants.Instance.Gun1Index + Gun2 * Constants.Instance.Gun2Index
            + Gun3 * Constants.Instance.Gun3Index + Fighter;
        return x;
    }

    //checking if the fleet of station has not surpassed the limit of ships (which is 45 according to spaces on battle field) by taking into account the ships on production
    //fighter are not taken into account they have their own limit. Cruisr 1 and 2 class takes 3 places (because of maneuver features)
    public int stationFleetCount()
    {
        int x = Cruis4 + Cruis4Produc + Cruis3 + Cruis3Produc + Cruis2 * 3 + Cruis2Produc * 3 + Cruis1 * 3 + Cruis1Produc * 3 + CruisG + Destr1 + Destr1Produc + Destr1Par + Destr1ProducPar
            + Destr2 + Destr2Produc + Destr2Par + Destr2ProducPar + Destr3 + Destr3Produc + Destr4 + Destr4Produc + DestrG + Gun1 + Gun1Produc + Gun2 + Gun2Produc + Gun3 + Gun3Produc;
        return x;
    }

    //only ready to go ships count of station
    public int countOfOnlyReadyShips()
    {
        int x = Cruis4 + Cruis3 + Cruis2 + Cruis1+CruisG + Destr1 + Destr1Par + Destr2 + Destr2Par + Destr3 + Destr4 + DestrG + Gun1 + Gun2 + Gun3;
        return x;
    }
    public int countOfOnlyDestrs()
    {
        int x = Destr1 + Destr1Par + Destr2 + Destr2Par + Destr3 + Destr4 + DestrG;
        return x;
    }

    //this check is necessary to provide the station with cruisers cause it will not be able to fight without cruisers
    public int ifStationHasCruisers() {
        int x = Cruis1 + Cruis2 + Cruis3 + Cruis4 + CruisG;
        return x;
    }

    private void Update()
    {

        if (isSelected && infoPanelLocal != null)
        {
            infoPanelLocal.transform.position = SpaceCtrlr.Instance.mainCam.WorldToScreenPoint(transform.position);
        }
        if (playerCruiserNear && connectionSygnalUIGO != null)
        {
            connectionSygnalUIGO.transform.position = SpaceCtrlr.Instance.mainCam.WorldToScreenPoint(connectionSygnalPos);
        }

        
    }

    private void FixedUpdate()
    {
        if (isPlayerStation)
        {
            if (stationCurrentLevel == 0) Lists.energyOfPlayer += Constants.Instance.EnergyProductTimeStation0;
            else if (stationCurrentLevel == 1) Lists.energyOfPlayer += Constants.Instance.EnergyProductTimeStation1;
            else if (stationCurrentLevel == 2) Lists.energyOfPlayer += Constants.Instance.EnergyProductTimeStation2;
            else if (stationCurrentLevel == 3) Lists.energyOfPlayer += Constants.Instance.EnergyProductTimeStation3;

            //updating the UI Texts
            energyCount.text = Lists.energyOfPlayer.ToString("0");
        }
        else if (!isGuardCoreStation)
        {
            if (stationCurrentLevel == 0) energyOfStationToUPGradeFoRCPU += Constants.Instance.EnergyProductTimeStation0;
            else if (stationCurrentLevel == 1) energyOfStationToUPGradeFoRCPU += Constants.Instance.EnergyProductTimeStation1;
            else if (stationCurrentLevel == 2) energyOfStationToUPGradeFoRCPU += Constants.Instance.EnergyProductTimeStation2;
            //if (ifCPUstationHasEnergyToUpgrade()) upgradeCPUStation();
        }

        //this part is responsible to fill out the station upgrading process 
        if (isUpgrading && upgradeFill < 1 && isPlayerStation /*&& stationFillAmount >= 0 */)
        {
            upgradeFill += currentUpgradeTime;
        }
        //so after upgrading fill reaches 1 trigger of upgrade turns to false and activated the next generation station from pull
        //new station receives all the necessary values from current station before it is disactivated
        if (upgradeFill >= 1 && isUpgrading && isPlayerStation)
        {
            isUpgrading = false;

            //so here is a logic to upgrade a station from 0 to 1
            if (stationCurrentLevel == 0)
            {
                stationListToActivate = ObjectPullerJourney.current.GetStationDPullList();
                upgradedStationToActivate = ObjectPullerJourney.current.GetUniversalBullet(stationListToActivate);

                upgradedStationInstance = upgradedStationToActivate.GetComponent<StationController>();
                upgradedStationInstance.stationPosition = stationPosition;
                upgradedStationToActivate.transform.position = stationPosition;
                upgradedStationToActivate.transform.rotation = Quaternion.identity;
                //upgradedStationToActivate.GetComponent<Light>().color = Color.green; //TO DO WITH CHOSEN COLOR

                //these properties are used only on player stations management. CPU stations management is much more easier
                //if (isPlayerStation)
                //{
                //    upgradedStationInstance.Cruis4ProductTime = Constants.Instance.Cruis4ProductTimeStation1;
                //    upgradedStationInstance.Cruis3ProductTime = Constants.Instance.Cruis3ProductTimeStation1;
                //    upgradedStationInstance.Destr4ProductTime = Constants.Instance.Destr4ProductTimeStation1;
                //    upgradedStationInstance.Destr3ProductTime = Constants.Instance.Destr3ProductTimeStation1;
                //    upgradedStationInstance.Gun1ProductTime = Constants.Instance.Gun1ProductTimeStation1;
                //    upgradedStationInstance.MiniGunProductTime = Constants.Instance.MiniGunProductTimeStation1;
                //    //upgradedStationInstance.FighterProducTime = Lists.FighterProductTimeStation1;
                //    upgradedStationInstance.EnergyProductTime = Constants.Instance.EnergyProductTimeStation1;
                //}


                
                upgradedStationInstance.stationCurrentLevel = 1;
                upgradedStationInstance.upgradeCounts = upgradeCounts;
                upgradedStationInstance.nextUpgradeEnergyCount = Constants.Instance.enrgy1to2Upgrd;
                upgradedStationInstance.currentUpgradeTime = Constants.Instance.time1to2Upgrd;
            }

            //so here is a logic to upgrade a station from 1 to 2
            if (stationCurrentLevel == 1)
            {
                stationListToActivate = ObjectPullerJourney.current.GetStationCPullList();
                upgradedStationToActivate = ObjectPullerJourney.current.GetUniversalBullet(stationListToActivate);

                upgradedStationInstance = upgradedStationToActivate.GetComponent<StationController>();
                upgradedStationInstance.stationPosition = stationPosition;
                upgradedStationToActivate.transform.position = stationPosition;
                upgradedStationToActivate.transform.rotation = Quaternion.identity;
                //upgradedStationToActivate.GetComponent<Light>().color = Color.green; //TO DO WITH CHOSEN COLOR

                //if (isPlayerStation)
                //{
                //    upgradedStationInstance.Cruis4ProductTime = Constants.Instance.Cruis4ProductTimeStation2;
                //    upgradedStationInstance.Cruis3ProductTime = Constants.Instance.Cruis3ProductTimeStation2;
                //    upgradedStationInstance.Cruis2ProductTime = Constants.Instance.Cruis2ProductTimeStation2;
                //    upgradedStationInstance.Destr4ProductTime = Constants.Instance.Destr4ProductTimeStation2;
                //    upgradedStationInstance.Destr3ProductTime = Constants.Instance.Destr3ProductTimeStation2;
                //    upgradedStationInstance.Destr2ProductTime = Constants.Instance.Destr2ProductTimeStation2;
                //    upgradedStationInstance.Destr2ParProductTime = Constants.Instance.Destr2ParProductTimeStation2;
                //    upgradedStationInstance.Gun1ProductTime = Constants.Instance.Gun1ProductTimeStation2;
                //    upgradedStationInstance.Gun2ProductTime = Constants.Instance.Gun2ProductTimeStation2;
                //    upgradedStationInstance.MiniGunProductTime = Constants.Instance.MiniGunProductTimeStation2;
                //    upgradedStationInstance.EnergyProductTime = Constants.Instance.EnergyProductTimeStation2;
                //}
                
                upgradedStationInstance.stationCurrentLevel = 2;
                upgradedStationInstance.upgradeCounts = upgradeCounts;
                upgradedStationInstance.nextUpgradeEnergyCount = Constants.Instance.enrgy2to3Upgrd;
                upgradedStationInstance.currentUpgradeTime = Constants.Instance.time2to3Upgrd;

            }
            //so here is a logic to upgrade a station from 2 to 3
            if (stationCurrentLevel == 2)
            {
                stationListToActivate = ObjectPullerJourney.current.GetStationFedPullList();
                upgradedStationToActivate = ObjectPullerJourney.current.GetUniversalBullet(stationListToActivate);

                upgradedStationInstance = upgradedStationToActivate.GetComponent<StationController>();
                upgradedStationInstance.stationPosition = stationPosition;
                upgradedStationToActivate.transform.position = stationPosition;
                upgradedStationToActivate.transform.rotation = Quaternion.identity;
                //upgradedStationToActivate.GetComponent<Light>().color = Color.green; //TO DO WITH CHOSEN COLOR

                //if (isPlayerStation)
                //{
                //    upgradedStationInstance.Cruis4ProductTime = Constants.Instance.Cruis4ProductTimeStation3;
                //    upgradedStationInstance.Cruis3ProductTime = Constants.Instance.Cruis3ProductTimeStation3;
                //    upgradedStationInstance.Cruis2ProductTime = Constants.Instance.Cruis2ProductTimeStation3;
                //    upgradedStationInstance.Cruis1ProductTime = Constants.Instance.Cruis1ProductTimeStation3;
                //    upgradedStationInstance.Destr4ProductTime = Constants.Instance.Destr4ProductTimeStation3;
                //    upgradedStationInstance.Destr3ProductTime = Constants.Instance.Destr3ProductTimeStation3;
                //    upgradedStationInstance.Destr1ProductTime = Constants.Instance.Destr1ProductTimeStation3;
                //    upgradedStationInstance.Destr1ParProductTime = Constants.Instance.Destr1ParProductTimeStation3;
                //    upgradedStationInstance.Destr2ProductTime = Constants.Instance.Destr2ProductTimeStation3;
                //    upgradedStationInstance.Destr2ParProductTime = Constants.Instance.Destr2ParProductTimeStation3;
                //    upgradedStationInstance.Gun1ProductTime = Constants.Instance.Gun1ProductTimeStation3;
                //    upgradedStationInstance.Gun2ProductTime = Constants.Instance.Gun2ProductTimeStation3;
                //    upgradedStationInstance.Gun3ProductTime = Constants.Instance.Gun3ProductTimeStation3;
                //    upgradedStationInstance.MiniGunProductTime = Constants.Instance.MiniGunProductTimeStation3;
                //    //upgradedStationInstance.FighterProducTime = Lists.FighterProductTimeStation3;
                //    upgradedStationInstance.EnergyProductTime = Constants.Instance.EnergyProductTimeStation3;
                //}
                
                upgradedStationInstance.stationCurrentLevel = 3;
                upgradedStationInstance.upgradeCounts = upgradeCounts;
                //only for setting a values to upgrade function properties, in fact there are no any upgrade levels any further
                upgradedStationInstance.nextUpgradeEnergyCount = Constants.Instance.enrgy2to3Upgrd;
                upgradedStationInstance.currentUpgradeTime = Constants.Instance.time2to3Upgrd;

            }

            upgradedStationInstance.isPlayerStation = isPlayerStation;
            upgradedStationInstance.CPUNumber = CPUNumber;

            //reconnecting the energon of station to upgradet one
            stationsEnergon.energonsStation = upgradedStationInstance;
            upgradedStationInstance.stationsEnergon = stationsEnergon;
            updateStationEnergon(upgradedStationInstance.stationCurrentLevel);

            upgradedStationToActivate.SetActive(true);

            //these properties are used only on player stations management. CPU will get its new kit of fleet right after it was upgraded
            if (isPlayerStation)
            {
                upgradedStationInstance.Cruis4 = Cruis4;
                upgradedStationInstance.Cruis3 = Cruis3;
                upgradedStationInstance.Cruis2 = Cruis2;
                upgradedStationInstance.Cruis1 = Cruis1;
                upgradedStationInstance.CruisG = 0;
                upgradedStationInstance.Destr4 = Destr4;
                upgradedStationInstance.Destr3 = Destr3;
                upgradedStationInstance.Destr2 = Destr2;
                upgradedStationInstance.Destr2Par = Destr2Par;
                upgradedStationInstance.Destr1 = Destr1;
                upgradedStationInstance.Destr1Par = Destr1Par;
                upgradedStationInstance.DestrG = 0;
                upgradedStationInstance.Gun1 = Gun1;
                upgradedStationInstance.Gun2 = Gun2;
                upgradedStationInstance.Gun3 = Gun3;
                upgradedStationInstance.MiniGun = MiniGun;
                upgradedStationInstance.Fighter = 0;
                upgradedStationInstance.cruiserPorted = cruiserPorted;
            }


            //to set false the trigger for setting off the link sprite on player cruiser panel to prepare the Station game object to be used next time from pull
            playerCruiserNear = false;

            //starting production of energy automatically on players station after upgrade
            if (upgradedStationInstance.isPlayerStation)
            {
                //upgradedStationInstance.energyProductionLaunche();

                //setting the color of player to upgraded station
                upgradedStationInstance.colorOfStationMat = colorOfStationMat;
                upgradedStationInstance.setProperStationColor();

                //called a method from SpaceCtrlr to update some UI information. playe upgrade sound and disactimate the button component of station icon
                //all the UI components like ships of station. products of station and greed are updated on activatePaneleMethod of SpaceCtrlr class cause new station hits the ship
                //with it's collider again
                SpaceCtrlr.Instance.updateStationUIAfterUpgrade(upgradedStationInstance);
            }

            //adding current station instance to Static list to process it while saving and loading game data
            if (isPlayerStation)
            {
                Lists.playerStations.Add(upgradedStationInstance);
                Lists.playerStations.Remove(this); //removing current old station instance of class from static station instances list
            }

            Lists.AllStations.Add(upgradedStationInstance);
            Lists.AllStations.Remove(this); //removing current old station instance of class from static station instances list

            disablingThisStation(); //setting off current station 

        }

        //controlling a timer to launch a new cruiser
        //if (!isPlayerStation && !isGuardStation)
        //{
        //    if (CPUSceneCruiserLaunchTimer > 0)
        //    {
        //        CPUSceneCruiserLaunchTimer -= Time.deltaTime;
        //        //SpaceCtrlr.Instance.CPUTurnTimer.text = CPUSceneCruiserLaunchTimer.ToString("000");
        //    }
        //    else if (/*CPUSceneCruiserLaunchTimer <= 0 && */CPUSceneCruiserLaunchCoroutineIsOn) launchingAProperCruiserWithFleet();
        //}
        else if (isGuardCoreStation && !isPlayerStation)
        {
            if (CPUSceneCruiserLaunchTimer > 0) CPUSceneCruiserLaunchTimer -= Time.deltaTime;
            if (CPUSceneCruiserLaunchTimer <= 0 && CPUSceneCruiserLaunchCoroutineIsOn) launchingGuardsAttackLaser(); //launchingAProperGuardCruiserWithFleet();
        }

        //controlling a timer to upgrade a CPU station (CPUStationIsUpgradIng int is used as bool 0 is false 1 is true)
        //if (CPUStationUpgradeTimer > 0 && !isPlayerStation) CPUStationUpgradeTimer -= Time.deltaTime;
        //if (CPUStationUpgradeTimer <= 0 && CPUStationIsUpgradIng > 0 && !isPlayerStation) upgradeCPUStation();

        //if (CPUStationFleetIncreaserTimer > 0 && !isPlayerStation && stationFleetCount() != Constants.Instance.shipsLimit) CPUStationFleetIncreaserTimer -= Time.deltaTime;
        //if (CPUStationFleetIncreaserTimer <= 0 && !isPlayerStation && stationFleetCount() != Constants.Instance.shipsLimit) graduallyIncreaseTheFleet();
        
    }

}
