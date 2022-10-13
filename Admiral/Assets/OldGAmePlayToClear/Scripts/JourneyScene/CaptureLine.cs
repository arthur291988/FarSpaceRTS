using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CaptureLine : MonoBehaviour
{

    private Transform cameraLook;
    //references to CPU journey scene cruisers
    private string Cruis1CPUTag = "BullDstrPlay1";
    private string Cruis2CPUTag = "BullDstrPlay2";
    private string Cruis3CPUTag = "BullDstrPlay3";
    private string Cruis4CPUTag = "BullDstrPlay4";
    // Start is called before the first frame update
    public GameObject fillingLine;
    private GameObject realStation;
    public float playerFillAmount;
    public float CPU1FillAmount;
    public float CPU2FillAmount;
    public float CPU3FillAmount;
    public float CPU4FillAmount;
    public GameObject playerShip;
    //public GameObject CPU1Ship;

    private string C1 = "C1";
    private string C2 = "C2";
    private string C3 = "C3";
    private string C4 = "C4";
    private string D1 = "D1";
    private string D1P = "D1P";
    private string D2 = "D2";
    private string D2P = "D2P";
    private string D3 = "D3";
    private string D4 = "D4";
    private string G1 = "G1";
    private string G2 = "G2";
    private string G3 = "G3";
    private string GM = "GM";

    private List<GameObject> station1ListToActivate; //to pull the station object from puller
    public bool IsPlayerFilling; //to trigger a signal if player started to capture the station
    public bool IsCPU1Filling; //to trigger a signal if player started to capture the station
    public bool IsCPU2Filling; //to trigger a signal if player started to capture the station
    public bool IsCPU3Filling; //to trigger a signal if player started to capture the station
    public bool IsCPU4Filling; //to trigger a signal if player started to capture the station

    //tags of station launch places, are used to assign proper type to instantiated station and determine it's level of upgrades
    private string noUpStationPlaceTag = "BullCruisPlay1";
    private string Up1StationPlaceTag = "BullCruisPlay2";
    private string Up2StationPlaceTag = "BullCruisPlay3";
    private string Up3StationPlaceTag = "BullCruisPlay4";

    private Text energyOfPlayer;
    private SpriteRenderer fillingLineSpriteRef; //property to set proper colour to filling line depending on who has more chances to capture this empty place

    StationController stationCtrlInstance; //this contruction is used to assign a proper features to created station

    //this var is used to give a sygnal to CPU cruiser that if this empty station is already chosen by other CPU to capture, if so this cruiser will not give a try to capture 
    //public bool isChosenToCaptureByOther;

    private void OnEnable()
    {
        //fill amount is -6 cause the sprite of mask is 6 units in length. So the filling sprite is also 6 units in lenght and it is -6 units out of mask on x axes
        playerFillAmount = -6;
        CPU1FillAmount = -6;
        CPU2FillAmount = -6;
        CPU3FillAmount = -6;
        CPU4FillAmount = -6;
        IsPlayerFilling = false;
        IsCPU1Filling = false;
        IsCPU2Filling = false;
        IsCPU3Filling = false;
        IsCPU4Filling = false;
        fillingLineSpriteRef = fillingLine.GetComponent<SpriteRenderer>();
        fillingLine.transform.localPosition = new Vector3(-6, 0, 0); //resetting filling line to zero
        //getting the reference to player energy UI 
        energyOfPlayer = SpaceCtrlr.Instance.energyCount;

        cameraLook = Camera.main.transform;
    }

    //taking all the ships from CPU1Cruiser after it took the station to prevent the attack to that station;
    //private void takeAllCPU1ShipsToNewStation(StationController controller) {
    //    controller.Cruis1 = Lists.Cruis1OfCPU1;
    //    Lists.Cruis1OfCPU1 = 0;
    //    controller.Cruis2 = Lists.Cruis2OfCPU1;
    //    Lists.Cruis2OfCPU1 = 0;
    //    controller.Cruis3 = Lists.Cruis3OfCPU1;
    //    Lists.Cruis3OfCPU1 = 0;
    //    controller.Cruis4 = Lists.Cruis4OfCPU1;
    //    Lists.Cruis4OfCPU1 = 0;

    //    controller.Destr1 = Lists.Destr1OfCPU1;
    //    Lists.Destr1OfCPU1 = 0;
    //    controller.Destr1Par = Lists.Destr1OfCPU1Par;
    //    Lists.Destr1OfCPU1Par = 0;
    //    controller.Destr2 = Lists.Destr2OfCPU1;
    //    Lists.Destr2OfCPU1 = 0;
    //    controller.Destr2Par = Lists.Destr2OfCPU1Par;
    //    Lists.Destr2OfCPU1Par = 0;
    //    controller.Destr3 = Lists.Destr3OfCPU1;
    //    Lists.Destr3OfCPU1 = 0;
    //    controller.Destr4 = Lists.Destr4OfCPU1;
    //    Lists.Destr4OfCPU1 = 0;
    //    controller.Gun1 = Lists.Gun1OfCPU1;
    //    Lists.Gun1OfCPU1 = 0;
    //    controller.Gun2 = Lists.Gun2OfCPU1;
    //    Lists.Gun2OfCPU1 = 0;
    //    controller.Gun3 = Lists.Gun3OfCPU1;
    //    Lists.Gun3OfCPU1 = 0;
    //    controller.Fighter = Lists.FighterOfCPU1;
    //    Lists.FighterOfCPU1 = 0;
    //}

    //this method is for resetting the properties of this capture line class to put it back to pull to reuse it later


    void Start()
    {
        //fill amount is -6 cause the sprite of mask is 6 units in length. So the filling sprite is also 6 units in lenght and it is -6 units out of mask on x axes
        playerFillAmount = -6;
        CPU1FillAmount = -6;
        CPU2FillAmount = -6;
        CPU3FillAmount = -6;
        CPU4FillAmount = -6;
        IsPlayerFilling = false;
        IsCPU1Filling = false;
        IsCPU2Filling = false;
        IsCPU3Filling = false;
        IsCPU4Filling = false;
        fillingLineSpriteRef = fillingLine.GetComponent<SpriteRenderer>(); 
        fillingLine.transform.localPosition = new Vector3(-6, 0, 0); //resetting filling line to zero
        //isChosenToCaptureByOther = false;
        //adding the instance of that class to the list of empty stations on static class to process it later by CPUShipCtrlJourney class
        //Lists.emptyStations.Add(this);

        //oly this instance of empty station will not be destroyed 
        //if (gameObject.activeInHierarchy) DontDestroyOnLoad(this);

        //getting the reference to player energy UI 
        energyOfPlayer = SpaceCtrlr.Instance.energyCount;
    }

    public void turnEmptyToCPUStation(int CPUNumber, CPUShipCtrlJourney CPUCruiser) {
        station1ListToActivate = ObjectPullerJourney.current.GetStationAPullList();
        realStation = ObjectPullerJourney.current.GetUniversalBullet(station1ListToActivate);
        stationCtrlInstance = realStation.GetComponent<StationController>();
        realStation.transform.position = gameObject.transform.parent.position;
        realStation.transform.rotation = Quaternion.identity;

        stationCtrlInstance.isCPUStation = true; //cause the CPU1 got the station on that if block
        stationCtrlInstance.isPlayerStation = false; //cause the player got the station on that if block
        stationCtrlInstance.isGuardStation = false;
        //stationCtrlInstance.stationProductionSwitchTrigger = false;
        stationCtrlInstance.CPUNumber = CPUNumber;
        stationCtrlInstance.colorOfStationMat = SpaceCtrlr.Instance.getProperMatColorByIndex(SpaceCtrlr.Instance.colorsOfPlayers[CPUNumber]);


        realStation.SetActive(true);

        stationCtrlInstance.Cruis1 = CPUCruiser.Cruis1;
        stationCtrlInstance.Cruis2 = CPUCruiser.Cruis2;
        stationCtrlInstance.Cruis3 = CPUCruiser.Cruis3;
        stationCtrlInstance.Cruis4 = CPUCruiser.Cruis4;
        stationCtrlInstance.CruisG = CPUCruiser.CruisG;
        stationCtrlInstance.Destr1 = CPUCruiser.Destr1;
        stationCtrlInstance.Destr1Par = CPUCruiser.Destr1Par;
        stationCtrlInstance.Destr2 = CPUCruiser.Destr2;
        stationCtrlInstance.Destr2Par = CPUCruiser.Destr2Par;
        stationCtrlInstance.Destr3 = CPUCruiser.Destr3;
        stationCtrlInstance.Destr4 = CPUCruiser.Destr4;
        stationCtrlInstance.DestrG = CPUCruiser.DestrG;
        stationCtrlInstance.Gun1 = CPUCruiser.Gun1;
        stationCtrlInstance.Gun2 = CPUCruiser.Gun2;
        stationCtrlInstance.Gun3 = CPUCruiser.Gun3;
        stationCtrlInstance.Fighter = CPUCruiser.Fighter;

        //setting the features of newley creadet station
        stationCtrlInstance.stationCurrentLevel = 0; //start level of station is always zero
        stationCtrlInstance.stationPosition = gameObject.transform.parent.position;
        if (CompareTag(noUpStationPlaceTag)) stationCtrlInstance.upgradeCounts = 0;
        else if (CompareTag(Up1StationPlaceTag)) stationCtrlInstance.upgradeCounts = 1;
        else if (CompareTag(Up2StationPlaceTag)) stationCtrlInstance.upgradeCounts = 2;
        else if (CompareTag(Up3StationPlaceTag)) stationCtrlInstance.upgradeCounts = 3;

        //setting start next upgrade energy requirements and next upgrade time, which is from 0 to 1 upgrade
        if (stationCtrlInstance.upgradeCounts > 0)
        {
            stationCtrlInstance.nextUpgradeEnergyCount = Constants.Instance.enrgy0to1Upgrd;
        }
        //CPU1Ship.GetComponent<CPUShipCtrlJourney>().moveCloseToStation(realStation.transform.position);

        //disacitvating the ability for player to capture current station since it is now CPU's, only if player is capturing the same empty station tha CPU captures
        //if (SpaceCtrlr.Instance.localLaunchingObjects.currentEmptyStation == this) SpaceCtrlr.Instance.localLaunchingObjects.disactivateCapture();

        //disacitvating the ability for player to capture current station since it is now CPU's, only if player is capturing the same empty station tha CPU captures
        for (int i = 0; i < Lists.shipsOnScene.Count; i++) {
            if (!Lists.shipsOnScene[i].CompareTag(Cruis1CPUTag) && !Lists.shipsOnScene[i].CompareTag(Cruis2CPUTag) && !Lists.shipsOnScene[i].CompareTag(Cruis3CPUTag) && !Lists.shipsOnScene[i].CompareTag(Cruis4CPUTag)) {
                if (Lists.shipsOnScene[i].GetComponent<LaunchingObjcts>().currentEmptyStation == this) Lists.shipsOnScene[i].GetComponent<LaunchingObjcts>().disactivateCapture();
            }
        }

        if (CPUNumber == 0) Lists.CPU1Stations.Add(stationCtrlInstance);
        if (CPUNumber == 1) Lists.CPU2Stations.Add(stationCtrlInstance);
        if (CPUNumber == 2) Lists.CPU3Stations.Add(stationCtrlInstance);
        if (CPUNumber == 3) Lists.CPU4Stations.Add(stationCtrlInstance);
        Lists.AllStations.Add(stationCtrlInstance);
        Lists.emptyStations.Remove(this); //removing this instance of calss from emty staitions list cause it is not empty any more

        stationCtrlInstance.createStationsEnergon();

        //this one for check if any station of this CPU launhing a cruiser of this just captured station is the only one of it. In case if so, it will start launching a cruiser
        //SpaceCtrlr.Instance.ifCPUStationLaunchesACruiser(stationCtrlInstance.CPUNumber);
        SpaceCtrlr.Instance.CommonApplyAudio.Play();
        //updating the UI of stations information of this player of if it lost the game 
        SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(1, Lists.CPU1Stations.Count, Lists.CPU1CruisersOnScene);

        stationCtrlInstance.startProcessesForCPUFromEmptyStation();
        //stationCtrlInstance.launchingACPUCruiserOnScene(); //starting a process of launching a new cruiser on scene

        gameObject.transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + cameraLook.rotation*Vector3.back, cameraLook.rotation * Vector3.up);

        #region commented out lines
        //so if the player finished the capturing the station it turns off all the capturing features (lasers and effect) and put the real station on capture plase
        //with player parameters. Real station is pulled from the pull of objects that is created on ObjectPullerJourney class
        //if (CPU1FillAmount >= 0)
        //{
        //    //Lists.CPU1SceneCruis.disactivateCaptureEffect(0); //disactivating the CPU cruiser capture effect
        //    //Lists.CPU1SceneCruis.launchANewCruiser(Lists.CPU1SceneCruis.CPUNumber); //starting a process of launching a proper scene cruiser on chosen station (from method)


        //    //send a signal to UI of this player stations to stop a signal of launched cruiser on scene
        //    //+ 1 is used cause 0 indes is occupied by player
        //    //this line is a reserve code to turn off this player cruiser undicator in journey Scene UI if the line right above will not work (it can miss it if cause in 
        //    //station controller stays if condition to check if this CPU has at least one ctation but while capturing a new station it can lost all of it's stations)
        //    //SpaceCtrlr.Instance.turnOnOffLaunchedCruiserIcon(Lists.CPU1SceneCruis.CPUNumber + 1, false);
        //    //Lists.CPU1SceneCruis.gameObject.SetActive(false); //disactivating the CPU cruiser

        //    //Lists.CPU1CruisersOnScene--;
        //    //Lists.shipsOnScene.Remove(Lists.CPU1SceneCruis.gameObject);

        //    station1ListToActivate = ObjectPullerJourney.current.GetStationAPullList();
        //    realStation = ObjectPullerJourney.current.GetUniversalBullet(station1ListToActivate);
        //    stationCtrlInstance = realStation.GetComponent<StationController>();
        //    realStation.transform.position = gameObject.transform.parent.position;
        //    realStation.transform.rotation = Quaternion.identity;
        //    //realStation.GetComponent<Light>().color = Color.green; //TO DO WITH CHOSEN COLOR

        //    ////setting some start features of 0 level station before activating it
        //    //stationCtrlInstance.Cruis4ProductTime = Lists.Cruis4ProductTimeStation0;
        //    //stationCtrlInstance.Destr4ProductTime = Lists.Destr4ProductTimeStation0;
        //    //stationCtrlInstance.EnergyProductTime = Lists.EnergyProductTimeStation0;

        //    stationCtrlInstance.isCPUStation = true; //cause the CPU1 got the station on that if block
        //    stationCtrlInstance.isPlayerStation = false; //cause the player got the station on that if block
        //    stationCtrlInstance.stationProductionSwitchTrigger = false;
        //    stationCtrlInstance.CPUNumber = 0;
        //    stationCtrlInstance.colorOfStationMat = SpaceCtrlr.Instance.getProperMatColorByIndex(SpaceCtrlr.Instance.colorsOfPlayers[stationCtrlInstance.CPUNumber]);
        //    realStation.SetActive(true);


        //    //put all fleet of CPU1 to a newly created station
        //    //takeAllCPU1ShipsToNewStation(stationCtrlInstance);

        //    //setting start production to CPU captured station 
        //    //stationCtrlInstance.populatingProdPlanCPU(C4, 1);
        //    //stationCtrlInstance.populatingProdPlanCPU(D4, 3);

        //    //stationCtrlInstance.launchProductionAuto(stationCtrlInstance.productionPlan[0]);
        //    //stationCtrlInstance.energyProductionLaunche(); //TO DELETE THIS LINE ONLY FOR TESTS

        //    //stationCtrlInstance.sequenceProductionTrigger = Lists.level0StationSequenceProductTrigger(Lists.energyOfCPU1);

        //    //setting the features of newley creadet station
        //    stationCtrlInstance.stationCurrentLevel = 0; //start level of station is always zero
        //    stationCtrlInstance.stationPosition = gameObject.transform.parent.position;
        //    if (CompareTag(noUpStationPlaceTag)) stationCtrlInstance.upgradeCounts = 0;
        //    else if (CompareTag(Up1StationPlaceTag)) stationCtrlInstance.upgradeCounts = 1;
        //    else if (CompareTag(Up2StationPlaceTag)) stationCtrlInstance.upgradeCounts = 2;
        //    else if (CompareTag(Up3StationPlaceTag)) stationCtrlInstance.upgradeCounts = 3;

        //    //setting start next upgrade energy requirements and next upgrade time, which is from 0 to 1 upgrade
        //    if (stationCtrlInstance.upgradeCounts > 0)
        //    {
        //        stationCtrlInstance.nextUpgradeEnergyCount = Constants.Instance.enrgy0to1Upgrd;
        //    }
        //    //CPU1Ship.GetComponent<CPUShipCtrlJourney>().moveCloseToStation(realStation.transform.position);

        //    //disacitvating the ability for player to capture current station since it is now CPU's, only if player is capturing the same empty station tha CPU captures
        //    if (SpaceCtrlr.Instance.localLaunchingObjects.currentEmptyStation == this) SpaceCtrlr.Instance.localLaunchingObjects.disactivateCapture();

        //    //setting the moving around station and waiting features 
        //    //SpaceCtrlr.Instance.CPU1Ctrller.isCapturing = false;
        //    //Lists.StationThisCPU1 = stationCtrlInstance.gameObject;
        //    //SpaceCtrlr.Instance.CPU1Ctrller.moveAroundStation(stationCtrlInstance.transform.position);
        //    //SpaceCtrlr.Instance.CPU1Ctrller.isWaiting = true;

        //    Lists.CPU1Stations.Add(stationCtrlInstance);
        //    Lists.AllStations.Add(stationCtrlInstance);
        //    Lists.emptyStations.Remove(this); //removing this instance of calss from emty staitions list cause it is not empty any more

        //    //this one for check if any station of this CPU launhing a cruiser of this just captured station is the only one of it. In case if so, it will start launching a cruiser
        //    //SpaceCtrlr.Instance.ifCPUStationLaunchesACruiser(stationCtrlInstance.CPUNumber);
        //    SpaceCtrlr.Instance.CommonApplyAudio.Play();
        //    //updating the UI of stations information of this player of if it lost the game 
        //    SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(1, Lists.CPU1Stations.Count, Lists.CPU1CruisersOnScene);

        //    stationCtrlInstance.startProcessesForCPU(stationCtrlInstance.CPUNumber);

        //    gameObject.transform.parent.gameObject.SetActive(false);
        //}
        //if (CPU2FillAmount >= 0)
        //{
        //    //Lists.CPU2SceneCruis.disactivateCaptureEffect(1); //disactivating the CPU cruiser capture effect
        //    //Lists.CPU2SceneCruis.launchANewCruiser(Lists.CPU2SceneCruis.CPUNumber); //starting a process of launching a proper scene cruiser on chosen station (from method)
        //    //send a signal to UI of this player stations to stop a signal of launched cruiser on scene
        //    //+ 1 is used cause 0 indes is occupied by player
        //    //this line is a reserve code to turn off this player cruiser undicator in journey Scene UI if the line right above will not work (it can miss it if cause in 
        //    //station controller stays if condition to check if this CPU has at least one ctation but while capturing a new station it can lost all of it's stations)
        //    //SpaceCtrlr.Instance.turnOnOffLaunchedCruiserIcon(Lists.CPU2SceneCruis.CPUNumber + 1, false);
        //    //Lists.CPU2SceneCruis.gameObject.SetActive(false); //disactivating the CPU cruiser


        //    //Lists.CPU2CruisersOnScene--;
        //    //Lists.shipsOnScene.Remove(Lists.CPU2SceneCruis.gameObject);

        //    station1ListToActivate = ObjectPullerJourney.current.GetStationAPullList();
        //    realStation = ObjectPullerJourney.current.GetUniversalBullet(station1ListToActivate);
        //    stationCtrlInstance = realStation.GetComponent<StationController>();
        //    realStation.transform.position = gameObject.transform.parent.position;
        //    realStation.transform.rotation = Quaternion.identity;

        //    stationCtrlInstance.isCPUStation = true; //cause the CPU1 got the station on that if block
        //    stationCtrlInstance.isPlayerStation = false; //cause the player got the station on that if block
        //    stationCtrlInstance.stationProductionSwitchTrigger = false;
        //    stationCtrlInstance.CPUNumber = 1;
        //    stationCtrlInstance.colorOfStationMat = SpaceCtrlr.Instance.getProperMatColorByIndex(SpaceCtrlr.Instance.colorsOfPlayers[stationCtrlInstance.CPUNumber]);
        //    realStation.SetActive(true);

        //    //setting the features of newley creadet station
        //    stationCtrlInstance.stationCurrentLevel = 0; //start level of station is always zero
        //    stationCtrlInstance.stationPosition = gameObject.transform.parent.position;
        //    if (CompareTag(noUpStationPlaceTag)) stationCtrlInstance.upgradeCounts = 0;
        //    else if (CompareTag(Up1StationPlaceTag)) stationCtrlInstance.upgradeCounts = 1;
        //    else if (CompareTag(Up2StationPlaceTag)) stationCtrlInstance.upgradeCounts = 2;
        //    else if (CompareTag(Up3StationPlaceTag)) stationCtrlInstance.upgradeCounts = 3;

        //    //setting start next upgrade energy requirements and next upgrade time, which is from 0 to 1 upgrade
        //    if (stationCtrlInstance.upgradeCounts > 0)
        //    {
        //        stationCtrlInstance.nextUpgradeEnergyCount = Constants.Instance.enrgy0to1Upgrd;
        //    }
        //    //CPU1Ship.GetComponent<CPUShipCtrlJourney>().moveCloseToStation(realStation.transform.position);

        //    //disacitvating the ability for player to capture current station since it is now CPU's, only if player is capturing the same empty station tha CPU captures
        //    if (SpaceCtrlr.Instance.localLaunchingObjects.currentEmptyStation == this) SpaceCtrlr.Instance.localLaunchingObjects.disactivateCapture();

        //    Lists.CPU2Stations.Add(stationCtrlInstance);
        //    Lists.AllStations.Add(stationCtrlInstance);
        //    Lists.emptyStations.Remove(this); //removing this instance of calss from emty staitions list cause it is not empty any more

        //    //this one for check if any station of this CPU launhing a cruiser of this just captured station is the only one of it. In case if so, it wil start launching a cruiser
        //    //SpaceCtrlr.Instance.ifCPUStationLaunchesACruiser(stationCtrlInstance.CPUNumber);
        //    SpaceCtrlr.Instance.CommonApplyAudio.Play();
        //    stationCtrlInstance.startProcessesForCPU(stationCtrlInstance.CPUNumber);

        //    //updating the UI of stations information of this player of if it lost the game (index of player for CPU players goes with +1 cause 0 index is ocupied by human)
        //    SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(2, Lists.CPU2Stations.Count, Lists.CPU2CruisersOnScene);

        //    gameObject.transform.parent.gameObject.SetActive(false);
        //}
        //if (CPU3FillAmount >= 0)
        //{
        //    //Lists.CPU3SceneCruis.disactivateCaptureEffect(2); //disactivating the CPU cruiser capture effect
        //    //Lists.CPU3SceneCruis.launchANewCruiser(Lists.CPU3SceneCruis.CPUNumber); //starting a process of launching a proper scene cruiser on chosen station (from method)
        //    //send a signal to UI of this player stations to stop a signal of launched cruiser on scene
        //    //+ 1 is used cause 0 indes is occupied by player
        //    //this line is a reserve code to turn off this player cruiser undicator in journey Scene UI if the line right above will not work (it can miss it if cause in 
        //    //station controller stays if condition to check if this CPU has at least one ctation but while capturing a new station it can lost all of it's stations)
        //    //SpaceCtrlr.Instance.turnOnOffLaunchedCruiserIcon(Lists.CPU3SceneCruis.CPUNumber + 1, false);
        //    //Lists.CPU3SceneCruis.gameObject.SetActive(false); //disactivating the CPU cruiser

        //    //Lists.CPU3CruisersOnScene--;
        //    //Lists.shipsOnScene.Remove(Lists.CPU3SceneCruis.gameObject);

        //    station1ListToActivate = ObjectPullerJourney.current.GetStationAPullList();
        //    realStation = ObjectPullerJourney.current.GetUniversalBullet(station1ListToActivate);
        //    stationCtrlInstance = realStation.GetComponent<StationController>();
        //    realStation.transform.position = gameObject.transform.parent.position;
        //    realStation.transform.rotation = Quaternion.identity;

        //    stationCtrlInstance.isCPUStation = true; //cause the CPU1 got the station on that if block
        //    stationCtrlInstance.isPlayerStation = false; //cause the player got the station on that if block
        //    stationCtrlInstance.stationProductionSwitchTrigger = false;
        //    stationCtrlInstance.CPUNumber = 2;
        //    stationCtrlInstance.colorOfStationMat = SpaceCtrlr.Instance.getProperMatColorByIndex(SpaceCtrlr.Instance.colorsOfPlayers[stationCtrlInstance.CPUNumber]);
        //    realStation.SetActive(true);


        //    //setting the features of newley creadet station
        //    stationCtrlInstance.stationCurrentLevel = 0; //start level of station is always zero
        //    stationCtrlInstance.stationPosition = gameObject.transform.parent.position;
        //    if (CompareTag(noUpStationPlaceTag)) stationCtrlInstance.upgradeCounts = 0;
        //    else if (CompareTag(Up1StationPlaceTag)) stationCtrlInstance.upgradeCounts = 1;
        //    else if (CompareTag(Up2StationPlaceTag)) stationCtrlInstance.upgradeCounts = 2;
        //    else if (CompareTag(Up3StationPlaceTag)) stationCtrlInstance.upgradeCounts = 3;

        //    //setting start next upgrade energy requirements and next upgrade time, which is from 0 to 1 upgrade
        //    if (stationCtrlInstance.upgradeCounts > 0)
        //    {
        //        stationCtrlInstance.nextUpgradeEnergyCount = Constants.Instance.enrgy0to1Upgrd;
        //    }
        //    //CPU1Ship.GetComponent<CPUShipCtrlJourney>().moveCloseToStation(realStation.transform.position);

        //    //disacitvating the ability for player to capture current station since it is now CPU's, only if player is capturing the same empty station tha CPU captures
        //    if (SpaceCtrlr.Instance.localLaunchingObjects.currentEmptyStation == this) SpaceCtrlr.Instance.localLaunchingObjects.disactivateCapture();

        //    Lists.CPU3Stations.Add(stationCtrlInstance);
        //    Lists.AllStations.Add(stationCtrlInstance);
        //    Lists.emptyStations.Remove(this); //removing this instance of calss from emty staitions list cause it is not empty any more

        //    //this one for check if any station of this CPU launhing a cruiser of this just captured station is the only one of it. In case if so, it wil start launching a cruiser
        //    //SpaceCtrlr.Instance.ifCPUStationLaunchesACruiser(stationCtrlInstance.CPUNumber);

        //    //updating the UI of stations information of this player of if it lost the game (index of player for CPU players goes with +1 cause 0 index is ocupied by human)
        //    SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(3, Lists.CPU3Stations.Count, Lists.CPU3CruisersOnScene);
        //    SpaceCtrlr.Instance.CommonApplyAudio.Play();
        //    stationCtrlInstance.startProcessesForCPU(stationCtrlInstance.CPUNumber);

        //    gameObject.transform.parent.gameObject.SetActive(false);
        //}
        //if (CPU4FillAmount >= 0)
        //{
        //    //Lists.CPU4SceneCruis.disactivateCaptureEffect(3); //disactivating the CPU cruiser capture effect
        //    //Lists.CPU4SceneCruis.launchANewCruiser(Lists.CPU4SceneCruis.CPUNumber); //starting a process of launching a proper scene cruiser on chosen station (from method)
        //    //send a signal to UI of this player stations to stop a signal of launched cruiser on scene
        //    //+ 1 is used cause 0 indes is occupied by player
        //    //this line is a reserve code to turn off this player cruiser undicator in journey Scene UI if the line right above will not work (it can miss it if cause in 
        //    //station controller stays if condition to check if this CPU has at least one ctation but while capturing a new station it can lost all of it's stations)
        //    //SpaceCtrlr.Instance.turnOnOffLaunchedCruiserIcon(Lists.CPU4SceneCruis.CPUNumber + 1, false);
        //    //Lists.CPU4SceneCruis.gameObject.SetActive(false); //disactivating the CPU cruiser

        //    //Lists.CPU4CruisersOnScene--;
        //    //Lists.shipsOnScene.Remove(Lists.CPU4SceneCruis.gameObject);

        //    station1ListToActivate = ObjectPullerJourney.current.GetStationAPullList();
        //    realStation = ObjectPullerJourney.current.GetUniversalBullet(station1ListToActivate);
        //    stationCtrlInstance = realStation.GetComponent<StationController>();
        //    realStation.transform.position = gameObject.transform.parent.position;
        //    realStation.transform.rotation = Quaternion.identity;

        //    stationCtrlInstance.isCPUStation = true; //cause the CPU1 got the station on that if block
        //    stationCtrlInstance.isPlayerStation = false; //cause the player got the station on that if block
        //    stationCtrlInstance.stationProductionSwitchTrigger = false;
        //    stationCtrlInstance.CPUNumber =3;
        //    stationCtrlInstance.colorOfStationMat = SpaceCtrlr.Instance.getProperMatColorByIndex(SpaceCtrlr.Instance.colorsOfPlayers[stationCtrlInstance.CPUNumber]);
        //    realStation.SetActive(true);


        //    //setting the features of newley creadet station
        //    stationCtrlInstance.stationCurrentLevel = 0; //start level of station is always zero
        //    stationCtrlInstance.stationPosition = gameObject.transform.parent.position;
        //    if (CompareTag(noUpStationPlaceTag)) stationCtrlInstance.upgradeCounts = 0;
        //    else if (CompareTag(Up1StationPlaceTag)) stationCtrlInstance.upgradeCounts = 1;
        //    else if (CompareTag(Up2StationPlaceTag)) stationCtrlInstance.upgradeCounts = 2;
        //    else if (CompareTag(Up3StationPlaceTag)) stationCtrlInstance.upgradeCounts = 3;

        //    //setting start next upgrade energy requirements and next upgrade time, which is from 0 to 1 upgrade
        //    if (stationCtrlInstance.upgradeCounts > 0)
        //    {
        //        stationCtrlInstance.nextUpgradeEnergyCount = Constants.Instance.enrgy0to1Upgrd;
        //    }
        //    //CPU1Ship.GetComponent<CPUShipCtrlJourney>().moveCloseToStation(realStation.transform.position);

        //    //disacitvating the ability for player to capture current station since it is now CPU's, only if player is capturing the same empty station tha CPU captures
        //    if (SpaceCtrlr.Instance.localLaunchingObjects.currentEmptyStation == this) SpaceCtrlr.Instance.localLaunchingObjects.disactivateCapture();

        //    Lists.CPU4Stations.Add(stationCtrlInstance);
        //    Lists.AllStations.Add(stationCtrlInstance);
        //    Lists.emptyStations.Remove(this); //removing this instance of calss from emty staitions list cause it is not empty any more

        //    //this one for check if any station of this CPU launhing a cruiser of this just captured station is the only one of it. In case if so, it will start launching a cruiser
        //    //SpaceCtrlr.Instance.ifCPUStationLaunchesACruiser(stationCtrlInstance.CPUNumber);

        //    //updating the UI of stations information of this player of if it lost the game (index of player for CPU players goes with +1 cause 0 index is ocupied by human)
        //    SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(4, Lists.CPU4Stations.Count, Lists.CPU4CruisersOnScene);
        //    SpaceCtrlr.Instance.CommonApplyAudio.Play();
        //    stationCtrlInstance.startProcessesForCPU(stationCtrlInstance.CPUNumber);

        //    gameObject.transform.parent.gameObject.SetActive(false);
        //}

        #endregion commented out lines
    }

    private void FixedUpdate()
    {
        //startin the capture process by getting a signal from CaptureButton clsss that sets the IsPlayerFilling bool true and going on with it until energy of player is 
        //more that 0;
        if (IsPlayerFilling && playerFillAmount < 0 && Lists.energyOfPlayer > 0)
        {
            if (CPU1FillAmount == -6 && CPU2FillAmount == -6 && CPU3FillAmount == -6 && CPU4FillAmount == -6)
            {
                playerFillAmount += Constants.Instance.timeToCreateStation;
                //upgrading filling line only with value that is more than -6
                fillingLine.transform.localPosition = new Vector3(playerFillAmount, 0, 0);
                if (fillingLineSpriteRef.color != Color.yellow) fillingLineSpriteRef.color = Color.yellow;
            }
            else if (CPU1FillAmount > -6)
            {
                CPU1FillAmount -= Constants.Instance.timeToCreateStation;
                //upgrading filling line only with value that is more than -6
                fillingLine.transform.localPosition = new Vector3(CPU1FillAmount, 0, 0);
            }
            else if (CPU2FillAmount > -6)
            {
                CPU2FillAmount -= Constants.Instance.timeToCreateStation;
                //upgrading filling line only with value that is more than -6
                fillingLine.transform.localPosition = new Vector3(CPU2FillAmount, 0, 0);
            }
            else if (CPU3FillAmount > -6)
            {
                CPU3FillAmount -= Constants.Instance.timeToCreateStation;
                //upgrading filling line only with value that is more than -6
                fillingLine.transform.localPosition = new Vector3(CPU3FillAmount, 0, 0);
            }
            else if (CPU4FillAmount > -6)
            {
                CPU4FillAmount -= Constants.Instance.timeToCreateStation;
                //upgrading filling line only with value that is more than -6
                fillingLine.transform.localPosition = new Vector3(CPU4FillAmount, 0, 0);
            }

            Lists.energyOfPlayer -= Constants.Instance.energyReduceNoTo0;

            energyOfPlayer.text = Lists.energyOfPlayer.ToString("0");
        }
        //so if the player finished the capturing the station it turns off all the capturing features (lasers and effect) and put the real station on capture plase
        //with player parameters. Real station is pulled from the pull of objects that is created on ObjectPullerJourney class
        if (playerFillAmount >= 0)
        {
            playerShip.GetComponent<LaunchingObjcts>().disactivateCapture();
            station1ListToActivate = ObjectPullerJourney.current.GetStationAPullList();
            realStation = ObjectPullerJourney.current.GetUniversalBullet(station1ListToActivate);
            stationCtrlInstance = realStation.GetComponent<StationController>();
            realStation.transform.position = gameObject.transform.parent.position;
            realStation.transform.rotation = Quaternion.identity;

            //setting some start features of 0 level station before activating it
            //stationCtrlInstance.Cruis4ProductTime = Constants.Instance.Cruis4ProductTimeStation0;
            //stationCtrlInstance.Destr4ProductTime = Constants.Instance.Destr4ProductTimeStation0;
            //stationCtrlInstance.EnergyProductTime = Constants.Instance.EnergyProductTimeStation0;

            stationCtrlInstance.isPlayerStation = true; //cause the player got the station on that if block
            stationCtrlInstance.isCPUStation = false; //cause the player got the station on that if block
            //stationCtrlInstance.stationProductionSwitchTrigger = false;
            //setting a color of player to set a proper colored material to round station sphere
            stationCtrlInstance.colorOfStationMat = SpaceCtrlr.Instance.getProperMatColorByIndex(Lists.playerColor);
            stationCtrlInstance.setProperStationColor();

            //stationCtrlInstance.startProcessesForCPU()

            realStation.SetActive(true);

            //starting energy production automatically after launch on station of player 
            //stationCtrlInstance.energyProductionLaunche();

            //setting the features of newley creadet station
            stationCtrlInstance.stationCurrentLevel = 0; //start level of station is always zero
            stationCtrlInstance.stationPosition = gameObject.transform.parent.position; //saving the position of this station to use while pulling the info from saved file
            if (CompareTag(noUpStationPlaceTag)) stationCtrlInstance.upgradeCounts = 0;
            else if (CompareTag(Up1StationPlaceTag)) stationCtrlInstance.upgradeCounts = 1;
            else if (CompareTag(Up2StationPlaceTag)) stationCtrlInstance.upgradeCounts = 2;
            else if (CompareTag(Up3StationPlaceTag)) stationCtrlInstance.upgradeCounts = 3;


            //setting start next upgrade energy requirements and next upgrade time, which is from 0 to 1 upgrade
            if (stationCtrlInstance.upgradeCounts > 0)
            {
                stationCtrlInstance.nextUpgradeEnergyCount = Constants.Instance.enrgy0to1Upgrd;
            }

            Lists.AllStations.Add(stationCtrlInstance);
            Lists.playerStations.Add(stationCtrlInstance);
            Lists.emptyStations.Remove(this); //removing this instance of calss from emty staitions list cause it is not empty any more
            stationCtrlInstance.createStationsEnergon();
            //updating the UI of stations information of this player. 0 is permanent index of human player stations icon on dictionary, and 1 means that player alwas has a cruiser on journey scene 
            SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(0, Lists.playerStations.Count, 1);
            SpaceCtrlr.Instance.CommonApplyAudio.Play();
            gameObject.transform.parent.gameObject.SetActive(false);
        }

        //startin the capture process by getting a signal from CaptureButton clsss that sets the IsPlayerFilling bool true and going on with it until energy of player is 
        //more that 0;
        if (IsCPU1Filling && CPU1FillAmount < 0 /*&& Lists.energyOfCPU1 > 0*/)
        {
            if (playerFillAmount == -6 && CPU2FillAmount == -6 && CPU3FillAmount == -6 && CPU4FillAmount == -6)
            {
                CPU1FillAmount += Constants.Instance.timeToCreateStation;
                //upgrading filling line only with value that is more than -6
                fillingLine.transform.localPosition = new Vector3(CPU1FillAmount, 0, 0);
                //if (fillingLineSpriteRef.color != Color.red) fillingLineSpriteRef.color = Color.red;
            }
            else if (playerFillAmount > -6)
            {
                playerFillAmount -= Constants.Instance.timeToCreateStation;
                //upgrading filling line only with value that is more than -6
                fillingLine.transform.localPosition = new Vector3(playerFillAmount, 0, 0);
            }
            else if (CPU2FillAmount > -6)
            {
                CPU2FillAmount -= Constants.Instance.timeToCreateStation;
                //upgrading filling line only with value that is more than -6
                fillingLine.transform.localPosition = new Vector3(CPU2FillAmount, 0, 0);
            }
            else if (CPU3FillAmount > -6)
            {
                CPU3FillAmount -= Constants.Instance.timeToCreateStation;
                //upgrading filling line only with value that is more than -6
                fillingLine.transform.localPosition = new Vector3(CPU3FillAmount, 0, 0);
            }
            else if (CPU4FillAmount > -6)
            {
                CPU4FillAmount -= Constants.Instance.timeToCreateStation;
                //upgrading filling line only with value that is more than -6
                fillingLine.transform.localPosition = new Vector3(CPU4FillAmount, 0, 0);
            }

            //Lists.energyOfCPU1 -= Constants.Instance.energyReduceNoTo0;
            //Debug.Log(Lists.energyOfCPU1.ToString());
        }
        if (IsCPU2Filling && CPU2FillAmount < 0 /*&& Lists.energyOfCPU2 > 0*/)
        {
            if (playerFillAmount == -6 && CPU1FillAmount == -6 && CPU3FillAmount == -6 && CPU4FillAmount == -6)
            {
                CPU2FillAmount += Constants.Instance.timeToCreateStation;
                //upgrading filling line only with value that is more than -6
                fillingLine.transform.localPosition = new Vector3(CPU2FillAmount, 0, 0);
                //if (fillingLineSpriteRef.color != Color.red) fillingLineSpriteRef.color = Color.red;
            }
            else if (playerFillAmount > -6)
            {
                playerFillAmount -= Constants.Instance.timeToCreateStation;
                //upgrading filling line only with value that is more than -6
                fillingLine.transform.localPosition = new Vector3(playerFillAmount, 0, 0);
            }
            else if (CPU1FillAmount > -6)
            {
                CPU1FillAmount -= Constants.Instance.timeToCreateStation;
                //upgrading filling line only with value that is more than -6
                fillingLine.transform.localPosition = new Vector3(CPU1FillAmount, 0, 0);
            }
            else if (CPU3FillAmount > -6)
            {
                CPU3FillAmount -= Constants.Instance.timeToCreateStation;
                //upgrading filling line only with value that is more than -6
                fillingLine.transform.localPosition = new Vector3(CPU3FillAmount, 0, 0);
            }
            else if (CPU4FillAmount > -6)
            {
                CPU4FillAmount -= Constants.Instance.timeToCreateStation;
                //upgrading filling line only with value that is more than -6
                fillingLine.transform.localPosition = new Vector3(CPU4FillAmount, 0, 0);
            }

            //Lists.energyOfCPU2 -= Constants.Instance.energyReduceNoTo0;
            //Debug.Log(Lists.energyOfCPU1.ToString());
        }
        if (IsCPU3Filling && CPU3FillAmount < 0 /*&& Lists.energyOfCPU3 > 0*/)
        {
            if (playerFillAmount == -6 && CPU1FillAmount == -6 && CPU2FillAmount == -6 && CPU4FillAmount == -6)
            {
                CPU3FillAmount += Constants.Instance.timeToCreateStation;
                //upgrading filling line only with value that is more than -6
                fillingLine.transform.localPosition = new Vector3(CPU3FillAmount, 0, 0);
                //if (fillingLineSpriteRef.color != Color.red) fillingLineSpriteRef.color = Color.red;
            }
            else if (playerFillAmount > -6)
            {
                playerFillAmount -= Constants.Instance.timeToCreateStation;
                //upgrading filling line only with value that is more than -6
                fillingLine.transform.localPosition = new Vector3(playerFillAmount, 0, 0);
            }
            else if (CPU1FillAmount > -6)
            {
                CPU1FillAmount -= Constants.Instance.timeToCreateStation;
                //upgrading filling line only with value that is more than -6
                fillingLine.transform.localPosition = new Vector3(CPU1FillAmount, 0, 0);
            }
            else if (CPU2FillAmount > -6)
            {
                CPU2FillAmount -= Constants.Instance.timeToCreateStation;
                //upgrading filling line only with value that is more than -6
                fillingLine.transform.localPosition = new Vector3(CPU2FillAmount, 0, 0);
            }
            else if (CPU4FillAmount > -6)
            {
                CPU4FillAmount -= Constants.Instance.timeToCreateStation;
                //upgrading filling line only with value that is more than -6
                fillingLine.transform.localPosition = new Vector3(CPU4FillAmount, 0, 0);
            }

            //Lists.energyOfCPU3 -= Constants.Instance.energyReduceNoTo0;
            //Debug.Log(Lists.energyOfCPU1.ToString());
        }
        if (IsCPU4Filling && CPU4FillAmount < 0 /*&& Lists.energyOfCPU4 > 0*/)
        {
            if (playerFillAmount == -6 && CPU1FillAmount == -6 && CPU2FillAmount == -6 && CPU3FillAmount == -6)
            {
                CPU4FillAmount += Constants.Instance.timeToCreateStation;
                //upgrading filling line only with value that is more than -6
                fillingLine.transform.localPosition = new Vector3(CPU4FillAmount, 0, 0);
                //if (fillingLineSpriteRef.color != Color.red) fillingLineSpriteRef.color = Color.red;
            }
            else if (playerFillAmount > -6)
            {
                playerFillAmount -= Constants.Instance.timeToCreateStation;
                //upgrading filling line only with value that is more than -6
                fillingLine.transform.localPosition = new Vector3(playerFillAmount, 0, 0);
            }
            else if (CPU1FillAmount > -6)
            {
                CPU1FillAmount -= Constants.Instance.timeToCreateStation;
                //upgrading filling line only with value that is more than -6
                fillingLine.transform.localPosition = new Vector3(CPU1FillAmount, 0, 0);
            }
            else if (CPU2FillAmount > -6)
            {
                CPU2FillAmount -= Constants.Instance.timeToCreateStation;
                //upgrading filling line only with value that is more than -6
                fillingLine.transform.localPosition = new Vector3(CPU2FillAmount, 0, 0);
            }
            else if (CPU3FillAmount > -6)
            {
                CPU3FillAmount -= Constants.Instance.timeToCreateStation;
                //upgrading filling line only with value that is more than -6
                fillingLine.transform.localPosition = new Vector3(CPU3FillAmount, 0, 0);
            }

            //Lists.energyOfCPU4 -= Constants.Instance.energyReduceNoTo0;
            //Debug.Log(Lists.energyOfCPU1.ToString());
        }
    }
}
