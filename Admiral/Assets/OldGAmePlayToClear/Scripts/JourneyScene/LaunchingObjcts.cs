
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchingObjcts : MonoBehaviour/*, IPointerDownHandler, IPointerUpHandler*/ /*Singleton<LaunchingObjcts> /*IPointerDownHandler*/
{
    //values to pass to shipController script to navigate 
    public Transform lookToPoint;
    public GameObject leftEngine;
    public GameObject rightEngine;
    public GameObject left;
    public GameObject right;

    //is used to prevent the bug of opening double communication panels
    public bool panelIsOn = false;

    //teleportation features
    private string upTeleportTag = "Finish";
    private string downTeleportTag = "Respawn";
    public ParticleSystem teleportationEffect;

    //tags of ships and stations to use initiate communication panels
    private string cruis1Tag = "BullCruis1";
    private string cruis2Tag = "BullCruis2";
    private string cruis3Tag = "BullCruis3";
    private string cruis4Tag = "BullCruis4";
    private string cruisGuardTag = "GuardCruiser";

    private string destr1Tag = "BullDstr1";
    private string destr2Tag = "BullDstr2";
    private string destr3Tag = "BullDstr3";
    private string destr4Tag = "BullDstr4";

    private string StationDarkTag = "BullCruisPlay4";
    private string StationBlueTag = "BullCruisPlay3";
    private string StationRedTag = "BullCruisPlay2";
    private string StationGuardTag = "BullCruisPlay1";

    public GameObject guardsShip;

    public List<Transform> ShipSpawnPoints = new List<Transform>();
    //public Transform galaxySpawn;

    private AudioSource engineSound;
    private AudioSource laserSound;

    //this one is laser to shot to station to capture it
    private LineRenderer laserToGetStation;
    private LineRenderer shipMovingLine;

    //reference to button of shotting the laser to capture the station
    //public RawImage captureLaserButton;

    //NEW GAME PLAY PROPERTIES

    Rigidbody ShipRB;

    //private string cpturedStationTagCloser = "Gun";
    private string cpturedStationTagAway = "Gun";

    //tags of station launch places 
    private string noUpStationPlaceTag = "BullCruisPlay1";
    private string Up1StationPlaceTag = "BullCruisPlay2";
    private string Up2StationPlaceTag = "BullCruisPlay3";
    private string Up3StationPlaceTag = "BullCruisPlay4";

    //this var holds the reference to empty station that is currently under capture to prevent a bug with disactivating the capture effect of player when he/she capturein other station
    //than any of CPU
    public CaptureLine currentEmptyStation;

    //this token (in a form of 2D sprite above the current ship) is used to show the player that his/her cruiser can exchange with station
    //public GameObject portedToken;
    public StationController enemyStation; //this SC instance is used to activate connection with enemy station before attack on SpaceCtrlr class
    public bool isPortedToPlayerStation; //this one is used to signal that player cruiser is ported to it's station and guards can't attack it, only can puul out the energy

    //effect that is capturing the empty station from inside
    private static GameObject captureEffect;
    //this one is to pull the capture effect from the puller
    private List<GameObject> captureLaserListToActivate;
    private static bool captureIsOn;
    private GameObject currentEmtyStation;

    //the properties to paralize player cruiser if energon bullet got the player cruiser
    //public bool isParalized;
    //public float paralizedTime = 9;
    //private float paralizerTimer;
    //public int hitsBeforeParalizer;

    //this one is to pull the paralizer from the puller whent the player bullet hits the energon ship
    //private List<GameObject> ParalizerList;
    //GameObject ParalizerListReal;

    //this game sphere is used as identificator of player journey scene cruisre by color of belonging to. It is assigned onenable of this class
    public List<GameObject> IDColorElements;
    private Color colorOfPlayerCruisMat;

    [HideInInspector]
    public bool isMoving;
    public Vector3 moveToPoint;
    //[SerializeField]
    //private GameObject selectedRing;
    private Outline selectedOutline;

    public float energy;
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

    public int currentCruiserLevel;


    private List<GameObject> BurstList;
    private GameObject BurstReal;

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


    private string Cruis1PlayerTag = "BullCruis1";
    private string Cruis2PlayerTag = "BullCruis2";
    private string Cruis3PlayerTag = "BullCruis3";
    private string Cruis4PlayerTag = "BullCruis4";

    //is used to populate with all ships of station and take only the % of fleet
    private List<string> allCruisersAndDestrs;
    private List<string> cruisersAndDestrAfterReduce;

    public bool playerIsNearEnemyStation;

    private GameObject infoPanelLocal;
    private List<GameObject> infoPanelLocalListToActivate;
    private MiniInfoPanel miniInfoPanelObject;
    private bool isSelected;
    private float speedOfCruiser;

    // Start is called before the first frame update
    void Start()
    {
        engineSound = GetComponent<AudioSource>();
    }

    public void SelectedAndReady()
    {
        SelectingBox.Instance.ifAnyShipChousen = true;
        //SelectingBox.Instance.chosenShipLineRenderer.Add(shipMovingLine);
        SelectingBox.Instance.chosenShipObj.Add(this);
        SpaceCtrlr.Instance.localLaunchingObjects = this;
        SpaceCtrlr.Instance.CruisJourneyReal = gameObject;
        //selectedRing.SetActive(true); 
        selectedOutline.enabled = true;

        infoPanelLocalListToActivate = ObjectPullerJourney.current.GetminiInfoPanelNoEnergyPullList();
        if (infoPanelLocal != null)
        {
            isSelected = false;
            if (infoPanelLocal.activeInHierarchy) infoPanelLocal.SetActive(false);
            infoPanelLocal = null;
        }
        infoPanelLocal = ObjectPullerJourney.current.GetUniversalBullet(infoPanelLocalListToActivate);
        miniInfoPanelObject = infoPanelLocal.GetComponent<MiniInfoPanel>();
        infoPanelLocal.transform.position = SpaceCtrlr.Instance.mainCam.WorldToScreenPoint(transform.position);
        miniInfoPanelObject.fleetCount.text = assessFleetPower().ToString();
        infoPanelLocal.SetActive(true);
        isSelected = true;
    }

    public void giveAShipMoveOrder(Vector3 moveTowards)
    {
        moveToPoint = moveTowards;
        //rotating the ship to look to move direction
        float yRotation = Quaternion.LookRotation(moveToPoint - transform.position, Vector3.up).eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, yRotation + 90, 0);
        //selectedRing.SetActive(false); 
        selectedOutline.enabled = false;
        engineSound.Play();
        isMoving = true;
        isSelected = false;
        if (infoPanelLocal != null)
        {
            if (infoPanelLocal.activeInHierarchy) infoPanelLocal.SetActive(false);
        }
        infoPanelLocal = null;
        shipMovingLine.SetPosition(1, moveTowards);
    }

    public void turnOffSelectedRing()
    {
        selectedOutline.enabled = false;
        //selectedRing.SetActive(false);
        if (infoPanelLocal != null)
        {
            if (infoPanelLocal.activeInHierarchy) infoPanelLocal.SetActive(false);
            infoPanelLocal = null;
        }
    }

    //this method is for assessing the power of station fleet is called from differend classes to asses station power and take a decision of attack(called from attacking CPU cruiser)
    public int assessFleetPower()
    {
        int x = Cruis4 * Constants.Instance.Cruis4Index + Cruis3 * Constants.Instance.Cruis3Index + Cruis2 * Constants.Instance.Cruis2Index + Cruis1 * Constants.Instance.Cruis1Index + CruisG * Constants.Instance.Cruis2Index
            + Destr4 * Constants.Instance.Destr4Index + Destr3 * Constants.Instance.Destr3Index + Destr2 * Constants.Instance.Destr2Index + Destr2Par * Constants.Instance.Destr2Index
            + Destr1 * Constants.Instance.Destr1Index + Destr1Par * Constants.Instance.Destr1Index + DestrG * Constants.Instance.Destr2Index + Gun1 * Constants.Instance.Gun1Index + Gun2 * Constants.Instance.Gun2Index
            + Gun3 * Constants.Instance.Gun3Index + Fighter;
        return x;
    }

    public void disactivatingCurrentShip()
    {
        BurstList = ObjectPullerJourney.current.GetenergonBurstPullList();
        BurstReal = ObjectPullerJourney.current.GetUniversalBullet(BurstList);
        BurstReal.transform.position = transform.position;
        BurstReal.SetActive(true);
        SpaceCtrlr.Instance.paralizedSound.Play();
        SelectingBox.Instance.selectableShips.Remove(this);
        SelectingBox.Instance.chosenShipObj.Remove(this);
        gameObject.SetActive(false);
    }

    public void disactivatingCurrentShipNoBurst()
    {
        SelectingBox.Instance.selectableShips.Remove(this);
        SelectingBox.Instance.chosenShipObj.Remove(this);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        //this one is to prevent a bug of missing reference while instantiating a ship after the battle (missing reference of rigid body)
        foreach (Collider col in GetComponents<Collider>()) col.enabled = false;
        foreach (Collider col in GetComponentsInChildren<Collider>()) col.enabled = false;
        //Lists.shipsOnScene.Remove(gameObject);
        isSelected = false;
        if (infoPanelLocal != null)
        {
            if (infoPanelLocal.activeInHierarchy) infoPanelLocal.SetActive(false);
            infoPanelLocal = null;
        }
        StopAllCoroutines();

    }
    private void OnEnable()
    {
        selectedOutline = GetComponent<Outline>();
        selectedOutline.enabled = false;
        allCruisersAndDestrs = new List<string>();
        cruisersAndDestrAfterReduce = new List<string>();

        //Lists.shipsOnScene.Add(gameObject);
        laserToGetStation = GetComponent<LineRenderer>();
        laserSound = transform.GetChild(0).GetComponent<AudioSource>();

        shipMovingLine = transform.GetChild(0).GetComponent<LineRenderer>();
        shipMovingLine.positionCount = 2;
        shipMovingLine.SetPosition(0, transform.position);
        shipMovingLine.SetPosition(1, transform.position);

        //adding this ship GO to the static lists of ships on scene to hold them while switching the scene and activating them after come back to journey scene
        ////TO DO WITH REMOVING THIS SHIP AFTER IT IS DESTROYED
        //SelectingBox.Instance.selectableShips.Add(this);
        playerIsNearEnemyStation = false;

        ShipRB = GetComponent<Rigidbody>();
        colorOfPlayerCruisMat = SpaceCtrlr.Instance.getProperMatColorByIndex(Lists.playerColor);
        //setting a proper color to colored elements of Player cruiser to make it has the same color that station CPU's has
        for (int i = 0; i < IDColorElements.Count; i++)
        {
            IDColorElements[i].GetComponent<MeshRenderer>().material.SetColor("_Color", colorOfPlayerCruisMat);
        }
        if (Cruis1 > 0)
        {
            currentCruiserLevel = 1;
            speedOfCruiser = Constants.Instance.CPUCruis1Speed;
        }
        else if (Cruis2 > 0)
        {
            currentCruiserLevel = 2;
            speedOfCruiser = Constants.Instance.CPUCruis2Speed;
        }
        else if (Cruis3 > 0)
        {
            currentCruiserLevel = 3;
            speedOfCruiser = Constants.Instance.CPUCruis3Speed;
        }
        else
        {
            currentCruiserLevel = 4;
            speedOfCruiser = Constants.Instance.CPUCruis4Speed;
        }
        StartCoroutine(addThisToSelectable());
    }

    private IEnumerator addThisToSelectable()
    {
        yield return new WaitForSeconds(0.3f);
        SelectingBox.Instance.selectableShips.Add(this);
    }

    public void clearTheFleetButFighters()
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

    public void makePlayerCruiserDefault()
    {
        clearTheFleetButFighters();
        turnOffSelectedRing();
        isMoving = false;


        guardsShip = null;
        currentEmptyStation = null;
        enemyStation = null;
        isPortedToPlayerStation = false;
        currentEmtyStation = null;

        if (captureEffect != null) captureEffect.SetActive(false);
        if (laserToGetStation != null) laserToGetStation.enabled = false;
        captureIsOn = false;
        captureEffect = null;
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

        if (infoPanelLocal) miniInfoPanelObject.fleetCount.text = assessFleetPower().ToString();
    }


    public GameObject getPlayerShip()
    {
        return gameObject;
    }

    public void disactivateCapture()
    {
        //captureLaserButton.color = new Color(1, 0, 0, 0.07f);
        //captureLaserButton.raycastTarget = false;
        disactivateCaptureEffect();
        currentEmptyStation = null;
    }

    //this method to set capture effect false from onPointerUp and for calling from the LaunchingObjcts class in case if the ship is out the station trigger area
    //and also for calling from CaptureLine class to stop it if station is set as player's
    public void disactivateCaptureEffect()
    {
        if (captureEffect != null) captureEffect.SetActive(false);
        if (laserToGetStation != null) laserToGetStation.enabled = false;
        captureIsOn = false;
        captureEffect = null;
        if (currentEmtyStation != null /*&& isEmptyCapturedLocal*/) currentEmtyStation.GetComponent<CaptureLine>().IsPlayerFilling = false;
        //else if (stationThis != null && !isEmptyCapturedLocal) stationThis.GetComponent<StationController>().IsPlayerFilling = false;
    }

    //if player is out of station that is available to capture it disactivates the feature of capturing it, sets the button inactive and disactivate capture effect on CaptureButton class
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(noUpStationPlaceTag) || other.CompareTag(Up1StationPlaceTag) || other.CompareTag(Up2StationPlaceTag) || other.CompareTag(Up3StationPlaceTag)
            || other.CompareTag(cpturedStationTagAway))
        {
            disactivateCapture();
            laserSound.Stop();
        }
        if (other.CompareTag(cpturedStationTagAway))
        {
            StationController sc = other.GetComponent<StationController>();
            sc.OnOffConnectionSygnal(false);
            //if (portedToken.activeInHierarchy) SpaceCtrlr.Instance.connectionDisactivatedSound.Play();
            //portedToken.SetActive(false);
            sc.playerCruiserNear = false;
            sc.playerCruiserPortedToCPUStation = null;
            enemyStation = null;
            //disabling the trigger that the player's cruiser is ported to it's station, this gives a signal to GuardArrack script that it can attack player cruiser
            isPortedToPlayerStation = false;
            playerIsNearEnemyStation = false;
            sc.playerCruiserObject.Remove(this);
        }
    }


    //paralizing player ship after it gets the shot from the energon, is called from DstrEnergonBull method
    //public void paralizingThePlayerShip() {
    //    if (ParalizerListReal == null)
    //    {
    //        //paralizerTimer = 0;
    //        ParalizerList = ObjectPullerJourney.current.GetParalizerJourList();
    //        ParalizerListReal = ObjectPullerJourney.current.GetUniversalBullet(ParalizerList);
    //        ParalizerListReal.transform.position = transform.position;
    //        ParalizerListReal.SetActive(true); 
    //        stopShipFromMoving();
    //        //isParalized = true;
    //        //hitsBeforeParalizer = Constants.Instance.hitsBeforePlayerCruiserIsParalized;
    //        SpaceCtrlr.Instance.paralizedSound.Play();
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        //so if the player's ship met the station that is available to capture is activates capture button and send a signal to CaptureButton class with parameters
        //and sets the player ship property of CaptureLine class to itself
        if ((other.CompareTag(noUpStationPlaceTag) || other.CompareTag(Up1StationPlaceTag) || other.CompareTag(Up2StationPlaceTag) || other.CompareTag(Up3StationPlaceTag))
            && Lists.energyOfPlayer > 0)
        {
            //CaptureButton.setShipParamsForCapture(laserToGetStation, gameObject, other.gameObject, true);
            Vector3 emtyStationPos = other.transform.parent.position;
            currentEmtyStation = other.gameObject;
            laserToGetStation.positionCount = 2; //setting position counts to laser line
            laserToGetStation.SetPosition(0, transform.position);
            laserToGetStation.SetPosition(1, emtyStationPos);
            laserToGetStation.enabled = true; //turning on the laser to capture the station

            //turnin on the capture effect from pull
            if (captureEffect == null)
            {
                captureLaserListToActivate = ObjectPullerJourney.current.GetCaptureEffectPullList();
                captureEffect = ObjectPullerJourney.current.GetUniversalBullet(captureLaserListToActivate);
                //if is capturing the empty station there is necessity to transform parent GO, else no need for that
                captureEffect.transform.position = emtyStationPos;

                captureEffect.transform.rotation = Quaternion.identity;

                captureEffect.SetActive(true);
                captureIsOn = true;

                laserSound.Play();
            }
            other.GetComponent<CaptureLine>().playerShip = gameObject; //passing current ship reference to CaptureLine class to use it proprtiest while building a laser line
            //sending a sygnal to CaptureLine class to start a UI processing with filling the UI line (that show tha process of cpturing the station)
            other.gameObject.GetComponent<CaptureLine>().IsPlayerFilling = true;
            //else stationThis.GetComponent<StationController>().IsPlayerFilling = true;



            //captureLaserButton.color = Color.red;
            //captureLaserButton.raycastTarget = true;

            currentEmptyStation = other.GetComponent<CaptureLine>();

            //portedToken.SetActive(false);
            enemyStation = null;
            SpaceCtrlr.Instance.playerIsNearEnemyStation = false;
            stopShipFromMoving();
        }

        else if (other.CompareTag(cpturedStationTagAway) && !isPortedToPlayerStation)
        {
            StationController sc = other.GetComponent<StationController>();

            if (!sc.isPlayerStation)
            {
                if (sc.ifStationHasCruisers() == 0)
                {
                    sc.clearAllPruductionBeforeUpgradeCPU(); //clearing the station before making it current CPU station
                    sc.StopAllCoroutines(); //stopping all CPU station processes
                    sc.CancelInvoke(); // stop any automatic processes inherited from CPU station

                    //setting a color of player to set a proper colored material to round station sphere. Only if it is not guard core station
                    if (!sc.isGuardCoreStation)
                    {
                        sc.colorOfStationMat = SpaceCtrlr.Instance.getProperMatColorByIndex(Lists.playerColor);
                        sc.setProperStationColor();

                        if (sc.stationCurrentLevel == 0)
                        {
                            //setting start next upgrade energy requirements and next upgrade time, which is from 0 to 1 upgrade
                            if (sc.upgradeCounts > 0)
                            {
                                sc.nextUpgradeEnergyCount = Constants.Instance.enrgy0to1Upgrd;
                            }
                        }
                        else if (sc.stationCurrentLevel == 1)
                        {
                            sc.nextUpgradeEnergyCount = Constants.Instance.enrgy1to2Upgrd;
                            sc.currentUpgradeTime = Constants.Instance.time1to2Upgrd;
                        }
                        else if (sc.stationCurrentLevel == 2)
                        {
                            sc.nextUpgradeEnergyCount = Constants.Instance.enrgy2to3Upgrd;
                            sc.currentUpgradeTime = Constants.Instance.time2to3Upgrd;
                        }
                        else if (sc.stationCurrentLevel == 3)
                        {
                            //only for setting a values to upgrade function properties, in fact there are no any upgrade levels any further
                            sc.nextUpgradeEnergyCount = Constants.Instance.enrgy2to3Upgrd;
                            sc.currentUpgradeTime = Constants.Instance.time2to3Upgrd;
                        }

                        //removing the station that is now player's from the list of CPU stations that are launching the cruiser to scene and give it's params to timer
                        //and resetting the timer
                        //SpaceCtrlr.Instance.allCruisLaunchingCPUStations.Remove(sc);
                        //SpaceCtrlr.Instance.resetTheTimer();
                    }

                    else
                    {
                        if (Lists.isBlackDimension)
                        {
                            sc.stationCurrentLevel = 1;
                            sc.upgradeCounts = 1;
                        }
                        else if (Lists.isBlueDimension)
                        {
                            sc.stationCurrentLevel = 2;
                            sc.upgradeCounts = 2;
                        }
                        else
                        {
                            sc.stationCurrentLevel = 3;
                            sc.upgradeCounts = 3;
                        }
                        //disactivating the guard core station attack timer cause now it is player's station
                        if (SpaceCtrlr.Instance.GuardAttackTimerTitle.gameObject.activeInHierarchy)
                        {
                            //guartCoreStation = null;
                            SpaceCtrlr.Instance.GuardAttackTimerTitle.gameObject.SetActive(false);
                        }
                    }

                    //starting energy production automatically after launch on station of player 
                    //sc.energyProductionLaunche();

                    if (!sc.isGuardCoreStation)
                    {
                        if (!sc.isGuardStation)
                        {
                            //removing this station from proper CPU's stations lists and making a according settings to station and updating scene UI info about defeatet CPU's stations
                            if (sc.CPUNumber == 0)
                            {
                                Lists.CPU1Stations.Remove(sc);
                                SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(sc.CPUNumber + 1, Lists.CPU1Stations.Count, Lists.CPU1CruisersOnScene);
                            }
                            else if (sc.CPUNumber == 1)
                            {
                                Lists.CPU2Stations.Remove(sc);
                                SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(sc.CPUNumber + 1, Lists.CPU2Stations.Count, Lists.CPU2CruisersOnScene);
                            }
                            else if (sc.CPUNumber == 2)
                            {
                                Lists.CPU3Stations.Remove(sc);
                                SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(sc.CPUNumber + 1, Lists.CPU3Stations.Count, Lists.CPU3CruisersOnScene);
                            }
                            else if (sc.CPUNumber == 3)
                            {
                                Lists.CPU4Stations.Remove(sc);
                                SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(sc.CPUNumber + 1, Lists.CPU4Stations.Count, Lists.CPU4CruisersOnScene);
                            }
                        }
                        else Lists.CPUGuardStations.Remove(sc);

                        sc.stationsEnergon.makeEnergonPlayers();
                    }
                    else Lists.CPUGuardStations.Remove(sc);

                    reducingTheFleetOFCruiserOnBattle(0.95f);

                    sc.isCPUStation = false; //cause the CPU got the station on that if block
                    sc.isGuardStation = false; //cause the CPU got the station on that if block
                    sc.isPlayerStation = true; //cause the player got the station on that if block

                    sc.Cruis1 = Cruis1;
                    sc.Cruis2 = Cruis2;
                    sc.Cruis3 = Cruis3;
                    sc.Cruis4 = Cruis4;
                    sc.CruisG = 0;
                    sc.Destr1 = Destr1;
                    sc.Destr2 = Destr2;
                    sc.Destr1Par = Destr1Par;
                    sc.Destr2Par = Destr2Par;
                    sc.Destr3 = Destr3;
                    sc.Destr4 = Destr4;
                    sc.DestrG = 0;
                    sc.Gun1 = Gun1;
                    sc.Gun2 = Gun2;
                    sc.Gun3 = Gun3;
                    sc.Fighter = 0;
                    sc.MiniGun = MiniGun;

                    sc.CPUSceneCruiserLaunchCoroutineIsOn = false; //stops the process of creating the cruiser;


                    //so if met station is not the CPU's it will be removed from other players stations list 
                    Lists.playerStations.Add(sc);

                    //updating the information about human player icons on scene cause player lost it's station
                    //0 is permanent index of player and 1 is need to show that player does not lost it's cruiser, cause it is impossible
                    SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(0, Lists.playerStations.Count, 1);


                    //sc.updateFleetCountToDisplay();


                    SpaceCtrlr.Instance.gainSound.Play();
                    SpaceCtrlr.Instance.changingTheEnergyOfPlayer(true, sc.energyOfStation);

                    sc.disactivateInfoAboutShip();
                    SelectingBox.Instance.selectableStations.Remove(sc);
                    SelectingBox.Instance.selectableStationsPlayer.Add(sc);
                    makePlayerCruiserDefault();
                    Lists.shipsOnScene.Remove(gameObject);
                    SpaceCtrlr.Instance.gainSound.Play();
                    disactivatingCurrentShipNoBurst(); //disactivating the player cruiser by giving all it's fleet to new conquered


                    //so if there left no enemy stations and cruisers on scene player wins
                    if ((Lists.CPU1CruisersOnScene + Lists.CPU2CruisersOnScene + Lists.CPU3CruisersOnScene + Lists.CPU4CruisersOnScene) < 1 && (Lists.CPUGuardStations.Count + Lists.CPU1Stations.Count + Lists.CPU2Stations.Count + Lists.CPU3Stations.Count +
                        Lists.CPU4Stations.Count) < 1) SpaceCtrlr.Instance.youWinTheGameFunction();
                }
                //so real attack to player cruiser will be only if this cruiser of this CPU has at least one cruiser, otherwise there will be only simple battle

                else
                {
                    if ((Cruis1 + Cruis2 + Cruis3 + Cruis4) > 0)
                    {
                        if ((sc.assessFleetPower() > assessFleetPower() && ((float)assessFleetPower() / sc.assessFleetPower()) > 0.7f)
                            || (sc.assessFleetPower() < assessFleetPower() && ((float)sc.assessFleetPower() / assessFleetPower()) > 0.7f))
                        {
                            SpaceCtrlr.Instance.playerIsNearEnemyStation = true;
                            SpaceCtrlr.Instance.CruisJourneyReal = gameObject;
                            playerIsNearEnemyStation = true;
                            sc.playerCruiserNear = true;
                            enemyStation = sc;
                            sc.playerCruiserPortedToCPUStation = gameObject;
                            sc.OnOffConnectionSygnal(true);
                            //if (!portedToken.activeInHierarchy)
                            //{
                            //    portedToken.SetActive(true);
                            //    SpaceCtrlr.Instance.connectionEstablishedSound.Play();
                            //}
                        }
                        else
                        {
                            if (sc.assessFleetPower() < assessFleetPower())
                            {
                                if (sc.isGuardCoreStation)
                                {
                                    SpaceCtrlr.Instance.playerIsNearEnemyStation = true;
                                    SpaceCtrlr.Instance.CruisJourneyReal = gameObject;
                                    playerIsNearEnemyStation = true;
                                    sc.playerCruiserNear = true;
                                    enemyStation = sc;
                                    sc.playerCruiserPortedToCPUStation = gameObject;
                                    sc.OnOffConnectionSygnal(true);
                                }

                                else
                                {
                                    sc.clearAllPruductionBeforeUpgradeCPU(); //clearing the station before making it current CPU station
                                    sc.StopAllCoroutines(); //stopping all CPU station processes
                                    sc.CancelInvoke(); // stop any automatic processes inherited from CPU station

                                    //setting a color of player to set a proper colored material to round station sphere. Only if it is not guard core station
                                    //if (!sc.isGuardCoreStation)
                                    //{
                                        sc.colorOfStationMat = SpaceCtrlr.Instance.getProperMatColorByIndex(Lists.playerColor);
                                        sc.setProperStationColor();

                                        if (sc.stationCurrentLevel == 0)
                                        {
                                            //setting start next upgrade energy requirements and next upgrade time, which is from 0 to 1 upgrade
                                            if (sc.upgradeCounts > 0)
                                            {
                                                sc.nextUpgradeEnergyCount = Constants.Instance.enrgy0to1Upgrd;
                                            }
                                        }
                                        else if (sc.stationCurrentLevel == 1)
                                        {
                                            sc.nextUpgradeEnergyCount = Constants.Instance.enrgy1to2Upgrd;
                                            sc.currentUpgradeTime = Constants.Instance.time1to2Upgrd;
                                        }
                                        else if (sc.stationCurrentLevel == 2)
                                        {
                                            sc.nextUpgradeEnergyCount = Constants.Instance.enrgy2to3Upgrd;
                                            sc.currentUpgradeTime = Constants.Instance.time2to3Upgrd;
                                        }
                                        else if (sc.stationCurrentLevel == 3)
                                        {
                                            //only for setting a values to upgrade function properties, in fact there are no any upgrade levels any further
                                            sc.nextUpgradeEnergyCount = Constants.Instance.enrgy2to3Upgrd;
                                            sc.currentUpgradeTime = Constants.Instance.time2to3Upgrd;
                                        }

                                        //removing the station that is now player's from the list of CPU stations that are launching the cruiser to scene and give it's params to timer
                                        //and resetting the timer
                                        //SpaceCtrlr.Instance.allCruisLaunchingCPUStations.Remove(sc);
                                        //SpaceCtrlr.Instance.resetTheTimer();
                                    //}

                                    //else
                                    //{
                                    //    if (Lists.isBlackDimension)
                                    //    {
                                    //        sc.stationCurrentLevel = 1;
                                    //        sc.upgradeCounts = 1;
                                    //    }
                                    //    else if (Lists.isBlueDimension)
                                    //    {
                                    //        sc.stationCurrentLevel = 2;
                                    //        sc.upgradeCounts = 2;
                                    //    }
                                    //    else
                                    //    {
                                    //        sc.stationCurrentLevel = 3;
                                    //        sc.upgradeCounts = 3;
                                    //    }
                                    //    //disactivating the guard core station attack timer cause now it is player's station
                                    //    if (SpaceCtrlr.Instance.GuardAttackTimerTitle.gameObject.activeInHierarchy)
                                    //    {
                                    //        //guartCoreStation = null;
                                    //        SpaceCtrlr.Instance.GuardAttackTimerTitle.gameObject.SetActive(false);
                                    //    }
                                    //}

                                    //starting energy production automatically after launch on station of player 
                                    //sc.energyProductionLaunche();

                                    //if (!sc.isGuardCoreStation)
                                    //{
                                        if (!sc.isGuardStation)
                                        {
                                            //removing this station from proper CPU's stations lists and making a according settings to station and updating scene UI info about defeatet CPU's stations
                                            if (sc.CPUNumber == 0)
                                            {
                                                Lists.CPU1Stations.Remove(sc);
                                                SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(sc.CPUNumber + 1, Lists.CPU1Stations.Count, Lists.CPU1CruisersOnScene);
                                            }
                                            else if (sc.CPUNumber == 1)
                                            {
                                                Lists.CPU2Stations.Remove(sc);
                                                SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(sc.CPUNumber + 1, Lists.CPU2Stations.Count, Lists.CPU2CruisersOnScene);
                                            }
                                            else if (sc.CPUNumber == 2)
                                            {
                                                Lists.CPU3Stations.Remove(sc);
                                                SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(sc.CPUNumber + 1, Lists.CPU3Stations.Count, Lists.CPU3CruisersOnScene);
                                            }
                                            else if (sc.CPUNumber == 3)
                                            {
                                                Lists.CPU4Stations.Remove(sc);
                                                SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(sc.CPUNumber + 1, Lists.CPU4Stations.Count, Lists.CPU4CruisersOnScene);
                                            }
                                        }
                                        else Lists.CPUGuardStations.Remove(sc);

                                        sc.stationsEnergon.makeEnergonPlayers();
                                    //}
                                    //else Lists.CPUGuardStations.Remove(sc);

                                    sc.isCPUStation = false; //cause the CPU got the station on that if block
                                    sc.isGuardStation = false; //cause the CPU got the station on that if block
                                    sc.isPlayerStation = true; //cause the player got the station on that if block

                                    reducingTheFleetOFCruiserOnBattle(1 - ((float)sc.assessFleetPower() / assessFleetPower() * 0.75f));
                                    sc.Cruis1 = Cruis1;
                                    sc.Cruis2 = Cruis2;
                                    sc.Cruis3 = Cruis3;
                                    sc.Cruis4 = Cruis4;
                                    sc.CruisG = 0;
                                    sc.Destr1 = Destr1;
                                    sc.Destr2 = Destr2;
                                    sc.Destr1Par = Destr1Par;
                                    sc.Destr2Par = Destr2Par;
                                    sc.Destr3 = Destr3;
                                    sc.Destr4 = Destr4;
                                    sc.DestrG = 0;
                                    sc.Gun1 = Gun1;
                                    sc.Gun2 = Gun2;
                                    sc.Gun3 = Gun3;
                                    sc.Fighter = 0;
                                    sc.MiniGun = MiniGun;

                                    sc.CPUSceneCruiserLaunchCoroutineIsOn = false; //stops the process of creating the cruiser;


                                    //so if met station is not the CPU's it will be removed from other players stations list 
                                    Lists.playerStations.Add(sc);

                                    //updating the information about human player icons on scene cause player lost it's station
                                    //0 is permanent index of player and 1 is need to show that player does not lost it's cruiser, cause it is impossible
                                    SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(0, Lists.playerStations.Count, 1);


                                    //sc.updateFleetCountToDisplay();

                                    SpaceCtrlr.Instance.gainSound.Play();
                                    SpaceCtrlr.Instance.changingTheEnergyOfPlayer(true, sc.energyOfStation);

                                    sc.disactivateInfoAboutShip();
                                    SelectingBox.Instance.selectableStations.Remove(sc);
                                    SelectingBox.Instance.selectableStationsPlayer.Add(sc);
                                    makePlayerCruiserDefault();
                                    Lists.shipsOnScene.Remove(gameObject);
                                    disactivatingCurrentShipNoBurst(); //disactivating the player cruiser by giving all it's fleet to new conquered


                                    if ((Lists.CPU1CruisersOnScene + Lists.CPU2CruisersOnScene + Lists.CPU3CruisersOnScene + Lists.CPU4CruisersOnScene) < 1 && (Lists.CPUGuardStations.Count + Lists.CPU1Stations.Count + Lists.CPU2Stations.Count + Lists.CPU3Stations.Count +
                                        Lists.CPU4Stations.Count) < 1) SpaceCtrlr.Instance.youWinTheGameFunction();
                                }
                            }
                            else
                            {
                                sc.reducingTheFleetOFCruiserOnBattle(1 - ((float)assessFleetPower() / sc.assessFleetPower() * 0.75f));
                                float reduceAmount;
                                if (Lists.isBlackDimension) reduceAmount = Constants.Instance.enrgyGainLoseDark * 0.3f;
                                else if (Lists.isBlueDimension) reduceAmount = Constants.Instance.enrgyGainLoseBlue * 0.3f;
                                else reduceAmount = Constants.Instance.enrgyGainLoseRed * 0.3f;
                                SpaceCtrlr.Instance.changingTheEnergyOfPlayer(false, reduceAmount);
                                makePlayerCruiserDefault();
                                Lists.shipsOnScene.Remove(gameObject);
                                disactivatingCurrentShip(); //this disactivation with burst of CPU ship cause it was defeated
                            }
                        }
                    }

                    else
                    {
                        sc.reducingTheFleetOFCruiserOnBattle(0.95f);
                        float reduceAmount;
                        if (Lists.isBlackDimension) reduceAmount = Constants.Instance.enrgyGainLoseDark * 0.3f;
                        else if (Lists.isBlueDimension) reduceAmount = Constants.Instance.enrgyGainLoseBlue * 0.3f;
                        else reduceAmount = Constants.Instance.enrgyGainLoseRed * 0.3f;
                        SpaceCtrlr.Instance.changingTheEnergyOfPlayer(false, reduceAmount);
                        makePlayerCruiserDefault();
                        Lists.shipsOnScene.Remove(gameObject);
                        disactivatingCurrentShip(); //this disactivation with burst of CPU ship cause it was defeated
                    }
                }
            }
            else
            {
                sc.Cruis1 += Cruis1;
                sc.Cruis2 += Cruis2;
                sc.Cruis3 += Cruis3;
                sc.Cruis4 += Cruis4;
                sc.CruisG = 0;
                sc.Destr1 += Destr1;
                sc.Destr1Par += Destr1Par;
                sc.Destr2 += Destr2;
                sc.Destr2Par += Destr2Par;
                sc.Destr3 += Destr3;
                sc.Destr4 += Destr4;
                sc.DestrG = 0;
                sc.Gun1 += Gun1;
                sc.Gun2 += Gun2;
                sc.Gun3 += Gun3;
                sc.MiniGun += MiniGun;
                sc.Fighter = 0;

                sc.updateFleetCountToDisplay();

                makePlayerCruiserDefault();
                Lists.shipsOnScene.Remove(gameObject);
                SpaceCtrlr.Instance.gainSound.Play();
                disactivatingCurrentShipNoBurst();
            }

        }
        //else if (other.CompareTag("Energon"))
        //{
        //    EnergonMngr energonObj = other.GetComponent<EnergonMngr>();
        //    if ((Cruis1 + Cruis2 + Cruis3 + Cruis4) > 0)
        //    {
        //        //so CPU captures the station only if it's fleet stronger than attacked station fleet
        //        if (assessFleetPower() >= energonObj.assessFleetPower())
        //        {
        //            reducingTheFleetOFCruiserOnBattle(1 - ((float)energonObj.assessFleetPower() / assessFleetPower() * 0.75f));
        //            Lists.energonsOnScene.Remove(other.gameObject);
        //            energonObj.disactivatingCurrentShip();
        //            SpaceCtrlr.Instance.gainSound.Play();
        //            SpaceCtrlr.Instance.changingTheEnergyOfPlayer(true, energonObj.energyOfEnergonAndGuard);
        //        }
        //        else
        //        {
        //            energonObj.reducingTheFleetOFCruiserOnBattle(1 - ((float)assessFleetPower() / energonObj.assessFleetPower() * 0.75f));
        //            makePlayerCruiserDefault();
        //            Lists.shipsOnScene.Remove(gameObject);
        //            disactivatingCurrentShip();
        //        }
        //    }
        //    else
        //    {
        //        energonObj.reducingTheFleetOFCruiserOnBattle(0.95f);
        //        makePlayerCruiserDefault();
        //        Lists.shipsOnScene.Remove(gameObject);
        //        disactivatingCurrentShip();
        //    }
        //}
        else if (other.CompareTag(Cruis1PlayerTag) || other.CompareTag(Cruis2PlayerTag) || other.CompareTag(Cruis3PlayerTag) || other.CompareTag(Cruis4PlayerTag))
        {
            LaunchingObjcts otherPlayerShip = other.GetComponent<LaunchingObjcts>();
            if (assessFleetPower() > otherPlayerShip.assessFleetPower())
            {
                Cruis1 += otherPlayerShip.Cruis1;
                Cruis2 += otherPlayerShip.Cruis2;
                Cruis3 += otherPlayerShip.Cruis3;
                Cruis4 += otherPlayerShip.Cruis4;
                CruisG += otherPlayerShip.CruisG;
                Destr1 += otherPlayerShip.Destr1;
                Destr1Par += otherPlayerShip.Destr1Par;
                Destr2 += otherPlayerShip.Destr2;
                Destr2Par += otherPlayerShip.Destr2Par;
                Destr3 += otherPlayerShip.Destr3;
                Destr4 += otherPlayerShip.Destr4;
                DestrG += otherPlayerShip.DestrG;
                Gun1 += otherPlayerShip.Gun1;
                Gun2 += otherPlayerShip.Gun2;
                Gun3 += otherPlayerShip.Gun3;
                Fighter += otherPlayerShip.Fighter;
                otherPlayerShip.makePlayerCruiserDefault();
                if (infoPanelLocal) miniInfoPanelObject.fleetCount.text = assessFleetPower().ToString();

                Lists.shipsOnScene.Remove(otherPlayerShip.gameObject);
                SpaceCtrlr.Instance.gainSound.Play();
                otherPlayerShip.disactivatingCurrentShipNoBurst(); ;
            }
        }



        #region for future if I will decide to use energy balls and alike bonuses
        //if (other.CompareTag("Booster")) {
        //    other.gameObject.SetActive(false); //disactivating the boster to pull it from puller next time
        //    int x = Random.Range(0,3);
        //    if (Lists.isBlackDimension) {
        //        //getting booster point depending on case so if 0 player gets the least booster points if 1 more and if 2 is most 
        //        Lists.boosterOfPlayer += x==0?Constants.Instance.boostrGainDark: x == 1? Constants.Instance.boostrGainDark*1.5f: Constants.Instance.boostrGainDark*2;
        //        SpaceCtrlr.Instance.boosterCount.text = Lists.boosterOfPlayer.ToString("0");
        //        SpaceCtrlr.Instance.boosterCount.color = Color.green; //making a energy count green while player gathers the energy
        //    }
        //    else if (Lists.isBlueDimension)
        //    {
        //        //getting booster point depending on case so if 0 player gets the least booster points if 1 more and if 2 is most 
        //        Lists.boosterOfPlayer += x == 0 ? Constants.Instance.boostrGainBlue: x == 1 ? Constants.Instance.boostrGainBlue * 1.5f : Constants.Instance.boostrGainBlue * 2;
        //        SpaceCtrlr.Instance.boosterCount.text = Lists.boosterOfPlayer.ToString("0");
        //        SpaceCtrlr.Instance.boosterCount.color = Color.green; //making a energy count green while player gathers the energy
        //    }
        //    else if (Lists.isRedDimension)
        //    {
        //        //getting booster point depending on case so if 0 player gets the least booster points if 1 more and if 2 is most 
        //        Lists.boosterOfPlayer += x == 0 ? Constants.Instance.boostrGainRed : x == 1 ? Constants.Instance.boostrGainRed * 1.5f : Constants.Instance.boostrGainRed * 2;
        //        SpaceCtrlr.Instance.boosterCount.text = Lists.boosterOfPlayer.ToString("0");
        //        SpaceCtrlr.Instance.boosterCount.color = Color.green; //making a energy count green while player gathers the energy
        //    }
        //    SpaceCtrlr.Instance.gainSound.Play();
        //    SpaceCtrlr.Instance.boosterCount.transform.parent.gameObject.GetComponent<Animator>().SetBool("tokenActive", true);
        //    SpaceCtrlr.Instance.Invoke("disactAnimAndColorsOfTokens", 2.5f);
        //}
        ////gatherinf the energy from destroyed asteroids
        //if (other.CompareTag("EnergyBall"))
        //{
        //    other.gameObject.SetActive(false); //disactivating the boster to pull it from puller next time
        //    int x = Random.Range(0, 3);
        //    if (Lists.isBlackDimension)
        //    {
        //        //getting booster point depending on case so if 0 player gets the least booster points if 1 more and if 2 is most 
        //        Lists.energyOfPlayer += x == 0 ? Constants.Instance.energyGainDark : x == 1 ? Constants.Instance.energyGainDark * 1.5f : Constants.Instance.energyGainDark * 2;
        //        SpaceCtrlr.Instance.energyCount.text = Lists.energyOfPlayer.ToString("0");
        //        SpaceCtrlr.Instance.energyCount.color = Color.green; //making a energy count green while player gathers the energy
        //    }
        //    else if (Lists.isBlueDimension)
        //    {
        //        //getting booster point depending on case so if 0 player gets the least booster points if 1 more and if 2 is most 
        //        Lists.energyOfPlayer += x == 0 ? Constants.Instance.energyGainBlue : x == 1 ? Constants.Instance.energyGainBlue * 1.5f : Constants.Instance.energyGainBlue * 2;
        //        SpaceCtrlr.Instance.energyCount.text = Lists.energyOfPlayer.ToString("0");
        //        SpaceCtrlr.Instance.energyCount.color = Color.green; //making a energy count green while player gathers the energy
        //    }
        //    else if (Lists.isRedDimension)
        //    {
        //        //getting booster point depending on case so if 0 player gets the least booster points if 1 more and if 2 is most 
        //        Lists.energyOfPlayer += x == 0 ? Constants.Instance.energyGainRed : x == 1 ? Constants.Instance.energyGainRed * 1.5f : Constants.Instance.energyGainRed * 2;
        //        SpaceCtrlr.Instance.energyCount.text = Lists.energyOfPlayer.ToString("0");
        //        SpaceCtrlr.Instance.energyCount.color = Color.green; //making a energy count green while player gathers the energy
        //    }
        //    SpaceCtrlr.Instance.gainSound.Play();
        //    SpaceCtrlr.Instance.energyCount.transform.parent.gameObject.GetComponent<Animator>().SetBool("tokenActive", true);
        //    SpaceCtrlr.Instance.Invoke("disactAnimAndColorsOfTokens", 2.5f);
        //}
        //if (other.CompareTag("BallFromEnergon"))
        //{
        //    for (int i = 0; i < SpaceCtrlr.Instance.aimingObjects.Count; i++)
        //    {
        //        SpaceCtrlr.Instance.aimingObjects[i].GetComponent<EnergonMngr>().isChasingEnergyBall = false;
        //    }
        //    other.gameObject.SetActive(false);
        //    SpaceCtrlr.Instance.energyBallsBigObjects.Remove(other.gameObject.transform);
        //    //if there is another energy ball on scene, player ship that captured previou energy ball sends a sygnal to all guards and energons that there is another energy ball to chase it
        //    if (SpaceCtrlr.Instance.energyBallsBigObjects.Count > 0) {
        //        for (int i = 0; i < SpaceCtrlr.Instance.aimingObjects.Count; i++)
        //        {
        //            EnergonMngr energonMngr = SpaceCtrlr.Instance.aimingObjects[i].GetComponent<EnergonMngr>();
        //            if (!energonMngr.isParalized)
        //            {
        //                energonMngr.startChaseOfEnergyBall();
        //            }
        //        }
        //    }

        //    int x = Random.Range(0, 3);
        //    if (Lists.isBlackDimension)
        //    {
        //        //getting booster point depending on case so if 0 player gets the least booster points if 1 more and if 2 is most 
        //        Lists.energyOfPlayer += x == 0 ? Constants.Instance.energyGainDark * 3 : x == 1 ? Constants.Instance.energyGainDark * 1.5f*3 : Constants.Instance.energyGainDark * 2 * 3;
        //        SpaceCtrlr.Instance.energyCount.text = Lists.energyOfPlayer.ToString("0");
        //        SpaceCtrlr.Instance.energyCount.color = Color.green; //making a energy count green while player gathers the energy
        //    }
        //    else if (Lists.isBlueDimension)
        //    {
        //        //getting booster point depending on case so if 0 player gets the least booster points if 1 more and if 2 is most 
        //        Lists.energyOfPlayer += x == 0 ? Constants.Instance.energyGainBlue * 3 : x == 1 ? Constants.Instance.energyGainBlue * 1.5f * 3 : Constants.Instance.energyGainBlue * 2 * 3;
        //        SpaceCtrlr.Instance.energyCount.text = Lists.energyOfPlayer.ToString("0");
        //        SpaceCtrlr.Instance.energyCount.color = Color.green; //making a energy count green while player gathers the energy
        //    }
        //    else if (Lists.isRedDimension)
        //    {
        //        //getting booster point depending on case so if 0 player gets the least booster points if 1 more and if 2 is most 
        //        Lists.energyOfPlayer += x == 0 ? Constants.Instance.energyGainRed * 3 : x == 1 ? Constants.Instance.energyGainRed * 1.5f * 3 : Constants.Instance.energyGainRed * 2 * 3;
        //        SpaceCtrlr.Instance.energyCount.text = Lists.energyOfPlayer.ToString("0");
        //        SpaceCtrlr.Instance.energyCount.color = Color.green; //making a energy count green while player gathers the energy
        //    }
        //    SpaceCtrlr.Instance.gainSound.Play();
        //    SpaceCtrlr.Instance.energyCount.transform.parent.gameObject.GetComponent<Animator>().SetBool("tokenActive", true);
        //    SpaceCtrlr.Instance.Invoke("disactAnimAndColorsOfTokens", 2.5f);
        //}

        #endregion for future if I will decide to use energy balls and alike bonuses
    }


    // Update is called once per frame

    //to set ship RB velocity to zero to stop the ship from moving while calling any player related panel
    public void stopShipFromMoving()
    {
        ShipRB.velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        //if player lost all of it's energy then capture is disactivated, but only the effect with disabling isPlayerFilling bool. The button will stay active
        if (Lists.energyOfPlayer == 0)
        {
            disactivateCaptureEffect();
        }
        else if (Lists.energyOfPlayer < 0)
        {
            Lists.energyOfPlayer = 0;
            disactivateCaptureEffect();
        }
        //to hold the start position of capture laser on player's ship current position
        if (captureIsOn)
        {
            laserToGetStation.SetPosition(0, laserToGetStation.transform.position);
        }

        if (isMoving)
        {
            shipMovingLine.SetPosition(0, transform.position);
            transform.position = Vector3.MoveTowards(transform.position, moveToPoint, speedOfCruiser);
            if (transform.position == moveToPoint)
            {
                isMoving = false;
                if (engineSound.isPlaying) engineSound.Stop();
            }
        }
    }
    private void Update()
    {
        if (isSelected && infoPanelLocal != null)
        {
            infoPanelLocal.transform.position = SpaceCtrlr.Instance.mainCam.WorldToScreenPoint(transform.position);
        }
    }
}
