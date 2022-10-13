
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.IO;

public class SpaceCtrlr : Singleton<SpaceCtrlr>
{
    //this is a panel to give some shor tutorial for player
    [SerializeField]
    GameObject TutorialPanel;
    [SerializeField]
    private List<Sprite> TutorSprites;
    private int leftOrRight; //this one is to scroll the info sprites on tutor panel
    private Image TutorialPanelImage; //this one is to hold image of tutor panel
    private Text gameAimText; //this one is to hold text of game aim in tutor panel

    //this materials are to assign as skybox depending on the dimension. The order is 0-black, 1-blue, 2-red
    [SerializeField]
    private List<Material> skyboxes;

    [SerializeField]
    private GameObject GoToMenuButton;

    //[SerializeField]
    //private Text CPUTurnTimerTitle;
    ////[SerializeField]
    ////public Text CPUTurnTimer;
    //[SerializeField]
    //private Slider CPUTurnSlider;
    //private Image CPUSliderFillImg;
    ////private Image CPUSliderHandleImg;
    //private RawImage CPUStationTimerIcon;


    private string fileName = "SaveData"; //file for save game data
    private string fileNameDaraga = "DaragaData";

    [SerializeField]
    public Text GuardAttackTimerTitle;
    //[SerializeField]
    //private Text GuardAttackTimer;

    public Slider GuardTurnSlider;

    public GameObject boosterBackground;
    public GameObject boosterButton;

    //public List<StationController> allCruisLaunchingCPUStations = new List<StationController>();
    public List<StationController> CruisLaunchingCPUStationToDisplay = new List<StationController>();

    [SerializeField]
    private Text TimeTextField;

    //properties to hold the stations count and launched cruisers signal on scene from all palyers
    public List<Text> PlayerStationsCountTxts;
    private Dictionary<int, GameObject> activeStationIcons;



    //to instantiate asteroids on game scene
    public GameObject LargeAsteroid;
    public GameObject SmallAsteroid;
    public GameObject MiniAsteroid;
    public GameObject MicroAsteroid;
    private GameObject asteroidReal;
    private int asteroidCounts; //18;

    //those materials are used to set a proper color to stations
    private Color redColorOfStation;
    private Color greenColorOfStation;
    private Color blueColorOfStation;
    private Color yellowColorOfStation;
    private Color purpleColorOfStation;

    //those materials are used to set a proper color to stations UIs
    private Color redColorOfStationUI;
    private Color greenColorOfStationUI;
    private Color blueColorOfStationUI;
    private Color yellowColorOfStationUI;
    private Color purpleColorOfStationUI;

    //this collection is used to hold a color numbers that left after player chouses it's color. And it is populated on start method
    //0-Red, 1-green, 2-blue, 3-yellow, 4-purple
    public List<int> colorsOfPlayers;

    //virtual camera pbject to follow player and using cinemachine features
    //public GameObject virtualCamera;

    //properties to launch energon ships on scene depending on current level 
    public GameObject energonShip;
    private GameObject energonShipReal;

    //properties to launch guard ships on scene depending on current level 
    public GameObject guardShip;
    private GameObject guardShipReal;

    //this is array to hold all materials to manage their fade property
    //public List<Material> holoMatsToFade;

    //0 station panel, 1 cruiser panel
    public List<Image> panelImagesToChangeTheColor;
    //private bool fadeIn;
    //private bool fadeOut;
    //private float fade;

    //backgound musics to set on journey scene according to dimension
    public AudioClip BlackDimensionMusic;
    public AudioClip BlueDimensionMusic;
    public AudioClip RedDimensionMusic;
    public AudioClip WinDimensionMusic;

    private AudioSource DimensionMusic;

    //properties to launch stations of CPU
    private List<GameObject> stationListToActivate;
    private GameObject CPUStationReal;

    //button to activate after player wins
    public GameObject YouWinLoseButton;
    private Image YouWinLoseButtonImg;
    public Text YouWinLoseButtonTxt;
    public Sprite winSprite;
    public Sprite loseSprite;
    public List<GameObject> sceneUIPorperties;

    public LaunchingObjcts localLaunchingObjects;
    public LaunchingObjcts portedCruiserObject;

    private List<GameObject> cruiserListToActivatePlayer; //to pull the station object from puller
    [HideInInspector]
    public GameObject cruiserToActivatePlayer; //to pull the station object from puller
    public GameObject CruisJourneyReal;
    //public GameObject CruisJourneyRealMaster;
    private Vector3 respawnPointOfCruis;
    private Quaternion RotationOfCruis;

    public StationController chosenStation;

    //space galaxy to spawn them randomely on randome point on scene and with random color
    public GameObject Galaxy;
    private int galaxyCount = 4;
    private GameObject galaxyReal;


    //universal station panel to display the plyer station properties
    public GameObject stationPanel;
    public GameObject StationPanelCloseButton;

    //tags to determine the levl of cruiser and paint the panel of cruiser to decent color
    private string cruis1Tag = "BullCruis1";
    private string cruis2Tag = "BullCruis2";
    private string cruis3Tag = "BullCruis3";
    private string cruis4Tag = "BullCruis4";


    //references to CPU journey scene cruisers
    private string Cruis1CPUTag = "BullDstrPlay1";
    private string Cruis2CPUTag = "BullDstrPlay2";
    private string Cruis3CPUTag = "BullDstrPlay3";
    private string Cruis4CPUTag = "BullDstrPlay4";

    //universal panel to display the fleets of met objects before the fight btwn them
    public GameObject fleetComparePanel;
    public GameObject fleetComparePanelCloseButton;
    public GameObject FightButtonComparePanel;
    public Text fightTxt;
    public Text YourFleetTxt;
    public Text TheirFleetTxt;

    //public AudioSource EngineSound;
    public AudioSource CommonButtonAudio;
    public AudioSource CommonApplyAudio;
    public AudioSource CommonCancelAudio;
    public AudioSource CommonNotEnoughAudio;
    //public AudioSource Telematics;
    public AudioSource WarningSound;
    public AudioSource SwitchSceneSound;
    public AudioSource BoosterSound;
    public AudioSource TurnOnSound;
    public AudioSource GuardCoreLaser;

    public Text energyCount;
    //public Text boosterCount;
    //public static Text energyCountStatic;

    //static text fields to use with differend languages
    public Text spacePort;
    public Text production;
    public Text stationFleet;


    #region compare fleets panel

    //player fleet compare greed
    public Text cruis1PlayerCompare;
    public Text cruis2PlayerCompare;
    public Text cruis3PlayerCompare;
    public Text cruis4PlayerCompare;
    public Text destr1PlayerCompare;
    public Text destr1ParPlayerCompare;
    public Text destr2PlayerCompare;
    public Text destr2ParPlayerCompare;
    public Text destr3PlayerCompare;
    public Text destr4PlayerCompare;
    public Text gun1PlayerCompare;
    public Text gun2PlayerCompare;
    public Text gun3PlayerCompare;
    public Text MiniGunPlayerCompare;
    //is used to dynamically change the size of  greed
    private int playerFleetGreedSizeCompare = 0;
    public RawImage playerFleetGreedCompare;
    private RectTransform playerFleetGreedCompareRect;

    //player fleet compare greed
    public Text cruis1CPUCompare;
    public Text cruis2CPUCompare;
    public Text cruis3CPUCompare;
    public Text cruis4CPUCompare;
    public Text cruisGCPUCompare;
    public Text destr1CPUCompare;
    public Text destr1ParCPUCompare;
    public Text destr2CPUCompare;
    public Text destr2ParCPUCompare;
    public Text destr3CPUCompare;
    public Text destr4CPUCompare;
    public Text destrGCPUCompare;
    public Text gun1CPUCompare;
    public Text gun2CPUCompare;
    public Text gun3CPUCompare;
    public Text FighterCPUCompare;
    //is used to dynamically change the size of  greed
    private int CPUFleetGreedSizeCompare = 0;
    public RawImage CPUFleetGreedCompare;
    private RectTransform CPUFleetGreedCompareRect;


    //this one holds the reference to station that is under attack by player to compare it's fleet with player's one
    private StationController stationReferenceCompare;

    #endregion compare fleets panel

    //fleet of station value text fields
    public Text cruis1ValueStation;
    public Text cruis2ValueStation;
    public Text cruis3ValueStation;
    public Text cruis4ValueStation;
    public Text destr1ValueStation;
    public Text destr1ParValueStation;
    public Text destr2ValueStation;
    public Text destr2ParValueStation;
    public Text destr3ValueStation;
    public Text destr4ValueStation;
    public Text gun1ValueStation;
    public Text gun2ValueStation;
    public Text gun3ValueStation;
    public Text MiniGunValueStation;
    //is used to dynamically change the size of CPU ship fleet greed
    private int fleetGreedSizeStation = 0;
    public RawImage StationFleetGreed;
    private RectTransform StationFleetGreedRect;

    //production of station value text fields
    public Text cruis1ProductStation;
    public Text cruis2ProductStation;
    public Text cruis3ProductStation;
    public Text cruis4ProductStation;
    public Text destr1ProductStation;
    public Text destr1ParProductStation;
    public Text destr2ProductStation;
    public Text destr2ParProductStation;
    public Text destr3ProductStation;
    public Text destr4ProductStation;
    public Text gun1ProductStation;
    public Text gun2ProductStation;
    public Text gun3ProductStation;
    public Text MiniGunProductStation;
    //is used to dynamically change the size of  greed
    private int ProductGreedSizeStation = 0;
    public RawImage StationProductGreed;
    private RectTransform StationProductGreedRect;
    //public Text currentProduction;
    public Image currenProductionFill;
    public Image currenProductionBckgr;

    //space port poperties
    public Text cruis1PortStation;
    public Text cruis2PortStation;
    public Text cruis3PortStation;
    public Text cruis4PortStation;
    public Text destr1PortStation;
    public Text destr1ParPortStation;
    public Text destr2PortStation;
    public Text destr2ParPortStation;
    public Text destr3PortStation;
    public Text destr4PortStation;
    public Text gun1PortStation;
    public Text gun2PortStation;
    public Text gun3PortStation;
    public Text MiniGunPortStation;
    //is used to dynamically change the size of  greed
    private int PortGreedSizeStation = 0;
    public RawImage StationPortGreed;
    public GameObject StationPortObject;
    public GameObject PortedCruiserObj;
    private RectTransform StationPortGreedRect;

    //energy Img
    public Sprite EnergySpr;
    //ship images
    public Sprite Cruis1Spr;
    public Sprite Cruis2Spr;
    public Sprite Cruis3Spr;
    public Sprite Cruis4Spr;
    public Sprite Destr1Spr;
    public Sprite Destr1ParSpr;
    public Sprite Destr2Spr;
    public Sprite Destr2ParSpr;
    public Sprite Destr3Spr;
    public Sprite Destr4Spr;
    public Sprite Gun1Spr;
    public Sprite Gun2Spr;
    public Sprite Gun3Spr;
    public Sprite MiniGunSpr;

    private string C1 = "C1";
    private string C2 = "C2";
    private string C3 = "C3";
    private string C4 = "C4";
    private string CF = "CF";
    private string D1 = "D1";
    private string D1P = "D1P";
    private string D2 = "D2";
    private string D2P = "D2P";
    private string D3 = "D3";
    private string D4 = "D4";
    private string DF = "DF";
    private string G1 = "G1";
    private string G2 = "G2";
    private string G3 = "G3";
    private string GM = "GM";
    private string EN = "EN";

    //private string CurrentProducedShip;

    //this one holds the reference to station currently processing station class to operate with it's properties
    public StationController stationReference;

    public Image stationIcon;
    public Image stationIconBkgr;
    public Text upgradeText;
    public Sprite Station1Spr;
    public Sprite Station2Spr;
    public Sprite Station3Spr;
    public Sprite Station4Spr;
    public Sprite StationGSpr;

    public Image currentCruiser;

    //this one is to pull the capture effect from the puller
    private List<GameObject> emptyStationsListToActivate;

    //FOR DEBUG ONLY PURPOSES
    private GameObject emptyStation;


    //this one is used from StationController class to check if panel is on and make an update of panel UI after it's upgrade. 
    //so if panel is off no update is necessary cause all updates will be made while opening the panel from activateStationPanel method by getting the values of new station
    public bool stationPanelIsEnabled;
    private bool PlayerCruisPanelIsEnabled;
    private bool FleetComparePanelPanelIsEnabled;
    public bool anyPanelIsEnabled;

    //this object is used to show warning messages to player
    public GameObject WarningMsgPanel;
    public Text WarningMsgTxt;

    //this game object is used to disactivate production line if player opened the panel of station with capture line less than 0 (means that the station can produce only 
    // energy)
    //public GameObject productionLineOfStation;

    #region player cruiser panel UI variables

    //panel of playerCruiser where he/she can to manage his/her stations and open the panel of his/her stations\
    public GameObject playerCruiserPanel;
    public GameObject playerCruiserPanelMask;
    //private Image playerCruiserPanelMaskImg;
    public GameObject playerCruiserPanelCloseButton;
    public Text yourStationsPlayerPanelTxt;
    public Text yourFleetPlayerPanelTxt;

    //private int nextStation = 0;
    public List<GameObject> stationButtons;

    //this public dictionary is used pair the proper button with proper station controller instance and to chose the right station by pushin the button later
    private Dictionary<GameObject, StationController> ButtonAndStation;
    private Dictionary<Image, StationController> MiniImgAndStation;
    private Dictionary<Text, StationController> TextAndStation;
    //greet of stations on playerCruiser panel
    private int PlayerStationsGreedSize = 0;
    public RawImage PlayerStationsGreed;
    private RectTransform PlayerStationsGreedRect;

    //player cruis fleet poperties
    public Text cruis1PlayerCruis;
    public Text cruis2PlayerCruis;
    public Text cruis3PlayerCruis;
    public Text cruis4PlayerCruis;
    public Text destr1PlayerCruis;
    public Text destr1ParPlayerCruis;
    public Text destr2PlayerCruis;
    public Text destr2ParPlayerCruis;
    public Text destr3PlayerCruis;
    public Text destr4PlayerCruis;
    public Text gun1PlayerCruis;
    public Text gun2PlayerCruis;
    public Text gun3PlayerCruis;
    public Text MiniGunPlayerCruis;
    //is used to dynamically change the size of  greed
    private int PlayerCruisFleetGreedSize = 0;
    public RawImage PlayerCruisFleetGreed;
    private RectTransform PlayerCruisFleetGreedRect;

    #endregion player cruiser panel UI variables

    //this one is used to trigger if cruiser ported to station 
    public bool playerIsNearEnemyStation;

    #region ship info panel properties

    public GameObject ShipInfoDemoPanelMask;
    public Image ShipInfoDemoPanelImg;

    public GameObject Cruis1Demo;
    public GameObject Cruis2Demo;
    public GameObject Cruis3Demo;
    public GameObject Cruis4Demo;
    public GameObject CruisFedDemo;
    public GameObject Destr1Demo;
    public GameObject Destr1ParDemo;
    public GameObject Destr2Demo;
    public GameObject Destr2ParDemo;
    public GameObject Destr3Demo;
    public GameObject Destr4Demo;
    public GameObject DestrFedDemo;
    public GameObject Gun1Demo;
    public GameObject Gun2Demo;
    public GameObject Gun3Demo;
    public GameObject MiniGunDemo;

    //this variables holds the names of shipt that are displayed on ship info panel
    private string C1Demo = "SPIDER";
    private string C2Demo = "PARAL";
    private string C3Demo = "STORM";
    private string C4Demo = "BLAST";
    private string CFDemo = "GUARD";
    private string D1Demo = "D-1";
    private string D1PDemo = "D-1P";
    private string D2Demo = "D-2";
    private string D2PDemo = "D-2P";
    private string D3Demo = "D-3";
    private string D4Demo = "D-4";
    private string DFDemo = "D-GU";
    private string G1Demo = "G-1";
    private string G2Demo = "G-2";
    private string G3Demo = "G-3";
    private string GMDemo = "G-M";

    public Text shipNameDemo;

    //this collections hold the reference to ship feature title and feature text object from the inspector 
    public List<Text> shipTitles;
    public List<Text> shipFeatures;

    public Image DemoScreenFrame;

    public GameObject DemoPanelClose;

    //this ones are used while launching the CPU cruisers on scene after getting to journey scene with continued data
    private List<GameObject> cruiserListToActivate; //to pull the station object from puller
    private GameObject cruiserToActivate;

    #endregion ship info panel properties

    //this var is used to check is player has just teleported from one dimension to another and accordingly act while puching the YouWin button
    private bool isTeleported;
    private ParticleSystem teleportationEffect;
    private AudioSource teleportationSound;

    //reward ads properties
    [SerializeField]
    private GameObject rewardAdsPanel;
    [SerializeField]
    private Text beforeEnergy;
    [SerializeField]
    private Text afterEnergy;
    //[SerializeField]
    //private Text beforeBooster;
    //[SerializeField]
    //private Text afterBooster;
    [SerializeField]
    private Text whatchVideo;
    private int energyGain;
    //private int boosterGain;
    private int energyGainAfter;
    //private int boosterGainAfter;

    //this list holds a references to objects on scene to make auto aiming to them from the ship of player (asteroids, guards and energons)
    //this list is populated while launching these objects onlaunchAGameShips method and on start
    //public List<Transform> aimingObjects = new List<Transform>();

    //this list holds a references to energy balls on scene to make auto chase of it by guards and energons
    public List<Transform> energyBallsBigObjects = new List<Transform>();

    public bool adsPanelIsOn;

    [SerializeField]
    private AudioSource shotSound;
    public AudioSource paralizedSound;
    public AudioSource gainSound;
    public AudioSource connectionEstablishedSound;
    public AudioSource connectionDisactivatedSound;

    public Camera mainCam;
    public Transform parentCanvas;

    //variable to control camera view from this script
    private CameraManager cameraManager;

    private void Awake()
    {
        //reference to main camera and parent Canvas object of scene to get acces to it from all scene classes mainly to show the information about ship while selecting it 
        mainCam = Camera.main;
        parentCanvas = GameObject.FindGameObjectWithTag("Respawn").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        cameraManager = mainCam.GetComponent<CameraManager>();

        //getting the reference of tutor panel iamge and text objects
        TutorialPanelImage = TutorialPanel.transform.GetChild(0).GetComponent<Image>();
        TutorialPanel.transform.GetChild(1).GetComponent<Text>().text = Constants.Instance.getGameAim();
        TutorialPanel.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = Constants.Instance.getWhatchHowToPlay();

        //turning on the tutorial panel while player start the game first time ever
        if (!File.Exists(Application.persistentDataPath + "/" + fileName + ".art") && !File.Exists(Application.persistentDataPath + "/" + fileNameDaraga + ".art")) openCloseTutorPanel(1);

        //getting teleportation effect if thi level is on border of dimensions
        if (Lists.currentLevel == 3 || Lists.currentLevel == 7 || Lists.currentLevel == 10)
        {
            teleportationEffect = Camera.main.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
            teleportationSound = Camera.main.gameObject.transform.GetChild(0).GetComponent<AudioSource>();
        }

        whatchVideo.text = Constants.Instance.getWatchVideoTxt();

        //properties to display the CPUs next move timer
        //CPUSliderFillImg = CPUTurnSlider.fillRect.GetComponent<Image>();
        //CPUStationTimerIcon = CPUTurnTimerTitle.transform.GetChild(0).GetComponent<RawImage>();

        ////setting the language of timers on journey scene
        //CPUTurnTimerTitle.text = Constants.Instance.getNextAttackOf();
        GuardAttackTimerTitle.text = Constants.Instance.getGuardAttack();

        //stop ship from moving after it gets back from any other scene
        ShipController.isMovePushed = false;

        //this dictionary is used to hold a reference to icons of all game players stations and it is updated on each start via populating it on journeySceneStationsCtrlr
        //method
        activeStationIcons = new Dictionary<int, GameObject>();

        //setting the start energy and booster count of player
        energyCount.text = Lists.energyOfPlayer.ToString("0");
        //boosterCount.text = Lists.boosterOfPlayer.ToString("0");

        //setting station UI text with different languages
        stationFleet.text = Constants.Instance.getStatFleet();
        production.text = Constants.Instance.getProduction();
        spacePort.text = Constants.Instance.getSpacePort();
        //currentProduction.text = Constants.Instance.getCurrentProduction();

        //instantiating the colors of players
        //redColorOfStation = new Color(1.5f, 0.4f, 0.3f, 1f);
        //greenColorOfStation = new Color(1, 1.5f, 0, 1f);
        //blueColorOfStation = new Color(0.3f, 0.8f, 1.5f, 1f);
        //yellowColorOfStation = new Color(2, 1.5f, 0, 1f);
        //purpleColorOfStation = new Color(1.5f, 0.75f, 2, 1f);




        redColorOfStation = new Color(4f, 0f, 0f, 1f);
        greenColorOfStation = new Color(0, 3.5f, 0, 1f);
        yellowColorOfStation = new Color(4, 3.6f, 0, 1f);
        purpleColorOfStation = new Color(3.5f, 0f, 3, 1f);
        blueColorOfStation = new Color(0f, 1.8f, 8f, 1f);

        //instantiating the colors of players stations UIs
        redColorOfStationUI = new Color(1f, 0.15f, 0.15f, 0.7f);
        greenColorOfStationUI = new Color(0.13f, 1, 0.13f, 0.7f);
        blueColorOfStationUI = new Color(0.12f, 0.44f, 1f, 0.7f);
        yellowColorOfStationUI = new Color(1f, 0.92f, 0.13f, 0.7f);
        purpleColorOfStationUI = new Color(0.96f, 0.13f, 1f, 0.7f);

        //set the color of background depending on currend dimension
        if (Lists.isBlackDimension) RenderSettings.skybox = skyboxes[0];
        if (Lists.isBlueDimension) RenderSettings.skybox = skyboxes[1];
        if (Lists.isRedDimension) RenderSettings.skybox = skyboxes[2];

        //setting according dimension music
        DimensionMusic = GetComponent<AudioSource>();
        if (Lists.isBlackDimension)
        {
            DimensionMusic.clip = BlackDimensionMusic;
            DimensionMusic.Play();
        }
        else if (Lists.isBlueDimension)
        {
            DimensionMusic.clip = BlueDimensionMusic;
            DimensionMusic.Play();
        }
        else if (Lists.isRedDimension)
        {
            DimensionMusic.clip = RedDimensionMusic;
            DimensionMusic.Play();
        }

        //getting the rect transform sizes of station greeds once at start to modify them with different station types later
        StationProductGreedRect = StationProductGreed.GetComponent<RectTransform>();
        StationPortGreedRect = StationPortGreed.GetComponent<RectTransform>();
        StationFleetGreedRect = StationFleetGreed.GetComponent<RectTransform>();

        CPUFleetGreedCompareRect = CPUFleetGreedCompare.GetComponent<RectTransform>();
        playerFleetGreedCompareRect = playerFleetGreedCompare.GetComponent<RectTransform>();

        PlayerStationsGreedRect = PlayerStationsGreed.GetComponent<RectTransform>();
        PlayerCruisFleetGreedRect = PlayerCruisFleetGreed.GetComponent<RectTransform>();

        //this property is used to set a special color to station panel according to it's level
        //stationPanelMaskImage = stationPanelMask.GetComponent<Image>();

        //this image is used to set to youWinLoseButton a proper sprite, depending on if palyer lost or win the battle
        YouWinLoseButtonImg = YouWinLoseButton.GetComponent<Image>();

        //DontDestroyOnLoad(captureButton); //to prevent a bug of missing reference after getting back from battle 

        //setting the titles of ship type info panel depending on language. index 2 is HP title and does not need to be translated
        shipTitles[0].text = Constants.Instance.getClassWord();
        shipTitles[1].text = Constants.Instance.getMegaShotWord();
        shipTitles[3].text = Constants.Instance.getFirePowerWord();
        shipTitles[4].text = Constants.Instance.getFeaturesWord();
        shipTitles[5].text = Constants.Instance.getCostWord();



        //0-Red, 1-green, 2-blue, 3-yellow, 4-purple
        colorsOfPlayers = new List<int>();

        //populating the list of rest colors of game players
        //0-Red, 1-green, 2-blue, 3-yellow, 4-purple
        for (int i = 0; i < 5; i++)
        {
            if (Lists.playerColor != i) colorsOfPlayers.Add(i);
        }

        #region game objects activation from pull
        if (Lists.AllSceneObjects.Count == 0)
        {
            //so if the game is not continued there are instantiated zero stations and player cruiser on default position of scene
            if (!Lists.isContinued)
            {
                //launchin the energons and guard ships on scene depending on level
                if (Lists.currentLevel == 1)
                {
                    Lists.currentLevelDifficulty = 0;
                    launchAGameShips(Constants.Instance.level1Params);
                }
                else if (Lists.currentLevel == 2)
                {
                    Lists.currentLevelDifficulty = 0;
                    launchAGameShips(Constants.Instance.level2Params);
                }
                else if (Lists.currentLevel == 3)
                {
                    Lists.currentLevelDifficulty = 1;
                    launchAGameShips(Constants.Instance.level3Params);
                }
                else if (Lists.currentLevel == 4)
                {
                    Lists.currentLevelDifficulty = 1;
                    launchAGameShips(Constants.Instance.level4Params);
                }
                else if (Lists.currentLevel == 5)
                {
                    Lists.currentLevelDifficulty = 2;
                    launchAGameShips(Constants.Instance.level5Params);
                }
                else if (Lists.currentLevel == 6)
                {
                    Lists.currentLevelDifficulty = 1;
                    launchAGameShips(Constants.Instance.level6Params);
                }
                else if (Lists.currentLevel == 7)
                {
                    Lists.currentLevelDifficulty = 2;
                    launchAGameShips(Constants.Instance.level7Params);
                }
                else if (Lists.currentLevel == 8)
                {
                    Lists.currentLevelDifficulty = 1;
                    launchAGameShips(Constants.Instance.level8Params);
                }
                else if (Lists.currentLevel == 9)
                {
                    Lists.currentLevelDifficulty = 2;
                    launchAGameShips(Constants.Instance.level9Params);
                }
                else if (Lists.currentLevel == 10)
                {
                    Lists.currentLevelDifficulty = 2;
                    launchAGameShips(Constants.Instance.level10Params);
                }


                //launching the stations on scene while starting it from zero (not saved data) including guard stations and empty ones
                if (Lists.currentLevel == 1) launchAGameStations(Constants.Instance.level1Params);
                else if (Lists.currentLevel == 2) launchAGameStations(Constants.Instance.level2Params);
                else if (Lists.currentLevel == 3) launchAGameStations(Constants.Instance.level3Params);
                else if (Lists.currentLevel == 4) launchAGameStations(Constants.Instance.level4Params);
                else if (Lists.currentLevel == 5) launchAGameStations(Constants.Instance.level5Params);
                else if (Lists.currentLevel == 6) launchAGameStations(Constants.Instance.level6Params);
                else if (Lists.currentLevel == 7) launchAGameStations(Constants.Instance.level7Params);
                else if (Lists.currentLevel == 8) launchAGameStations(Constants.Instance.level8Params);
                else if (Lists.currentLevel == 9) launchAGameStations(Constants.Instance.level9Params);
                else if (Lists.currentLevel == 10) launchAGameStations(Constants.Instance.level10Params);


                cruiserListToActivatePlayer = ObjectPullerJourney.current.GetCruis4JourneyPlayerPullList();
                cruiserToActivatePlayer = ObjectPullerJourney.current.GetUniversalBullet(cruiserListToActivatePlayer);
                cruiserToActivatePlayer.transform.position = Constants.Instance.playerCruiserStartPos;
                cruiserToActivatePlayer.transform.rotation = Constants.Instance.playerCruiserStartRotation;
                cruiserToActivatePlayer.GetComponent<LaunchingObjcts>().Cruis4 = Lists.Cruis4OfPlayerCruis;
                cruiserToActivatePlayer.GetComponent<LaunchingObjcts>().Destr4 = Lists.Destr4OfPlayerCruis;
                cruiserToActivatePlayer.SetActive(true);
                Lists.shipsOnScene.Add(cruiserToActivatePlayer);
                //CruisJourneyReal = CruisJourneyRealMaster.transform.GetChild(0).gameObject;
                cruiserToActivatePlayer.GetComponent<Collider>().enabled = true;
                //this one is necessary to eneable the collider of energy gather child component of player scene cruiser
                foreach (Collider col in cruiserToActivatePlayer.GetComponentsInChildren<Collider>()) col.enabled = true;
            }
            //and if the game is continued all data about scene stations and cruisers is load from saved data file
            else
            {
                string[] rows = File.ReadAllLines(Application.persistentDataPath + "/" + fileName + ".art");
                int savedStationsNumbers = 0;
                int savedEmptyNumbers = 0;

                //getting the number of stations saved while last quit
                int AllStationsCount;
                if (int.TryParse(getSavedValue(rows, "AllStationsCount"), out AllStationsCount)) savedStationsNumbers = AllStationsCount;
                //getting the number of emptyStations saved while last quit
                int emptyStations;
                if (int.TryParse(getSavedValue(rows, "emptyStations"), out emptyStations)) savedEmptyNumbers = emptyStations;
                //getting the number of cruisers on scene saved while last quit
                //int cruisersOnScene;
                //if (int.TryParse(getSavedValue(rows, "cruisersOnScene"), out cruisersOnScene)) savedCruisersOnSceneNumbers = cruisersOnScene;



                //instantiating real stations on scene
                for (int i = 0; i < savedStationsNumbers; i++)
                {

                    int StationCurrentLevelLoaded = 0;
                    bool StationIsGuardCore;
                    if (bool.TryParse(getSavedValue(rows, "StationIsGuardCore" + i), out StationIsGuardCore))
                    {
                        //next this func determines which level station it should load from file
                        //for GuardCore station there is constant 3D object and it doesn't nepend on StationCurrentLevel param
                        int StationCurrentLevel;
                        if (int.TryParse(getSavedValue(rows, "StationCurrentLevel" + i), out StationCurrentLevel)) StationCurrentLevelLoaded = StationCurrentLevel;

                        //first check loads the bool if current station is not guard core station bool from file
                        if (!StationIsGuardCore)
                        {
                            if (StationCurrentLevelLoaded == 0)
                            {
                                stationListToActivate = ObjectPullerJourney.current.GetStationAPullList();
                                CPUStationReal = ObjectPullerJourney.current.GetUniversalBullet(stationListToActivate);
                            }
                            else if (StationCurrentLevelLoaded == 1)
                            {
                                stationListToActivate = ObjectPullerJourney.current.GetStationDPullList();
                                CPUStationReal = ObjectPullerJourney.current.GetUniversalBullet(stationListToActivate);
                            }
                            else if (StationCurrentLevelLoaded == 2)
                            {
                                stationListToActivate = ObjectPullerJourney.current.GetStationCPullList();
                                CPUStationReal = ObjectPullerJourney.current.GetUniversalBullet(stationListToActivate);
                            }
                            else if (StationCurrentLevelLoaded == 3)
                            {
                                stationListToActivate = ObjectPullerJourney.current.GetStationFedPullList();
                                CPUStationReal = ObjectPullerJourney.current.GetUniversalBullet(stationListToActivate);
                            }

                        }
                        //so if current loaded station is guard core station there will be instantiated guard core station on scene 
                        else
                        {
                            stationListToActivate = ObjectPullerJourney.current.GetStationEPullList();
                            CPUStationReal = ObjectPullerJourney.current.GetUniversalBullet(stationListToActivate);
                            CPUStationReal.transform.position = new Vector3(0, -8, 0);
                            CPUStationReal.transform.rotation = Quaternion.identity;
                        }
                    }

                    //assigning the coordinates of loaded station
                    float StationXPos;
                    if (float.TryParse(getSavedValue(rows, "StationXPos" + i), out StationXPos))
                    {
                        float StationZPos;
                        if (float.TryParse(getSavedValue(rows, "StationZPos" + i), out StationZPos)) CPUStationReal.transform.position = new Vector3(StationXPos, -8, StationZPos);
                    }
                    //rotation is not essential and does not matter
                    CPUStationReal.transform.rotation = Quaternion.identity;

                    //assigning default StationController class object to station 3D object to set it lower in this block with dawnloaded from file parameters
                    StationController sc = CPUStationReal.GetComponent<StationController>();
                    sc.stationPosition = CPUStationReal.transform.position;

                    bool StationIsPlayer;
                    if (bool.TryParse(getSavedValue(rows, "StationIsPlayer" + i), out StationIsPlayer)) sc.isPlayerStation = StationIsPlayer;
                    bool StationIsCPU;
                    if (bool.TryParse(getSavedValue(rows, "StationIsCPU" + i), out StationIsCPU)) sc.isCPUStation = StationIsCPU;
                    bool StationIsGuard;
                    if (bool.TryParse(getSavedValue(rows, "StationIsGuard" + i), out StationIsGuard)) sc.isGuardStation = StationIsGuard;
                    sc.isGuardCoreStation = StationIsGuardCore;

                    //sc.stationProductionSwitchTrigger = false;

                    //setting station corresponding upgrade counts
                    int StationUpgradeCounts;
                    if (int.TryParse(getSavedValue(rows, "StationUpgradeCounts" + i), out StationUpgradeCounts)) sc.upgradeCounts = StationUpgradeCounts;
                    int StationNextUpgradeEnergyCount;
                    if (int.TryParse(getSavedValue(rows, "StationNextUpgradeEnergyCount" + i), out StationNextUpgradeEnergyCount)) sc.nextUpgradeEnergyCount = StationNextUpgradeEnergyCount;
                    float StationCurrentUpgradeTime;
                    if (float.TryParse(getSavedValue(rows, "StationCurrentUpgradeTime" + i), out StationCurrentUpgradeTime)) sc.currentUpgradeTime = StationCurrentUpgradeTime;
                    //float StationCPUStationUpgradeTimer;
                    //if (float.TryParse(getSavedValue(rows, "StationCPUStationUpgradeTimer" + i), out StationCPUStationUpgradeTimer))
                    //    sc.CPUStationUpgradeTimer = StationCPUStationUpgradeTimer;
                    //int StationCPUStationIsUpgradIng;
                    //if (int.TryParse(getSavedValue(rows, "StationCPUStationIsUpgradIng" + i), out StationCPUStationIsUpgradIng))
                    //    sc.CPUStationIsUpgradIng = StationCPUStationIsUpgradIng;

                    //int StationFleetIncreaseStep;
                    //if (int.TryParse(getSavedValue(rows, "StationFleetIncreaseStep" + i), out StationFleetIncreaseStep))
                    //    sc.fleetIncreaseStep = StationFleetIncreaseStep;

                    sc.stationCurrentLevel = StationCurrentLevelLoaded; //setting station corresponding upgrade counts


                    float StationCPUSceneCruiserLaunchTimer;
                    if (float.TryParse(getSavedValue(rows, "StationCPUSceneCruiserLaunchTimer" + i), out StationCPUSceneCruiserLaunchTimer))
                        sc.CPUSceneCruiserLaunchTimer = StationCPUSceneCruiserLaunchTimer;

                    bool StationCPUSceneCruiserLaunchCoroutineIsOn;
                    if (bool.TryParse(getSavedValue(rows, "StationCPUSceneCruiserLaunchCoroutineIsOn" + i), out StationCPUSceneCruiserLaunchCoroutineIsOn))
                        sc.CPUSceneCruiserLaunchCoroutineIsOn = StationCPUSceneCruiserLaunchCoroutineIsOn;

                    bool StationIsUpgrading;
                    if (bool.TryParse(getSavedValue(rows, "StationIsUpgrading" + i), out StationIsUpgrading))
                        sc.isUpgrading = StationIsUpgrading;
                    float StationUpgradeFill;
                    if (float.TryParse(getSavedValue(rows, "StationUpgradeFill" + i), out StationUpgradeFill))
                        sc.upgradeFill = StationUpgradeFill;

                    //next func determines the color of station
                    int StationCPUNumber;
                    if (int.TryParse(getSavedValue(rows, "StationCPUNumber" + i), out StationCPUNumber))
                    {
                        if (StationIsCPU)
                        {
                            sc.colorOfStationMat = getProperMatColorByIndex(colorsOfPlayers[StationCPUNumber]); //setting the next color available on colours list to this CPU station
                            sc.CPUNumber = StationCPUNumber;
                        }
                        else if (StationIsPlayer)
                        {
                            //setting the player color to it's station it is load from saved file on MainMenu script already
                            sc.colorOfStationMat = getProperMatColorByIndex(Lists.playerColor);
                        }
                        else if (StationIsGuard)
                        {
                            sc.colorOfStationMat = new Color(2.4f, 2.4f, 2.4f, 1);
                            sc.CPUNumber = StationCPUNumber;
                        }
                    }
                    if (!StationIsGuardCore) sc.setProperStationColor();

                    //loading the information about fleet of saved station
                    float energyOfStationToUPGradeFoRCPULocal;
                    if (float.TryParse(getSavedValue(rows, "energyOfStationToUPGradeFoRCPU" + i), out energyOfStationToUPGradeFoRCPULocal)) sc.energyOfStationToUPGradeFoRCPU = energyOfStationToUPGradeFoRCPULocal;
                    float StationEnergy;
                    if (float.TryParse(getSavedValue(rows, "StationEnergy" + i), out StationEnergy)) sc.energyOfStation = StationEnergy;
                    int StationCruis4;
                    if (int.TryParse(getSavedValue(rows, "StationCruis4" + i), out StationCruis4)) sc.Cruis4 = StationCruis4;
                    int StationCruis3;
                    if (int.TryParse(getSavedValue(rows, "StationCruis3" + i), out StationCruis3)) sc.Cruis3 = StationCruis3;
                    int StationCruis2;
                    if (int.TryParse(getSavedValue(rows, "StationCruis2" + i), out StationCruis2)) sc.Cruis2 = StationCruis2;
                    int StationCruis1;
                    if (int.TryParse(getSavedValue(rows, "StationCruis1" + i), out StationCruis1)) sc.Cruis1 = StationCruis1;
                    int StationCruisG;
                    if (int.TryParse(getSavedValue(rows, "StationCruisG" + i), out StationCruisG)) sc.CruisG = StationCruisG;
                    int StationDestr4;
                    if (int.TryParse(getSavedValue(rows, "StationDestr4" + i), out StationDestr4)) sc.Destr4 = StationDestr4;
                    int StationDestr3;
                    if (int.TryParse(getSavedValue(rows, "StationDestr3" + i), out StationDestr3)) sc.Destr3 = StationDestr3;
                    int StationDestr2;
                    if (int.TryParse(getSavedValue(rows, "StationDestr2" + i), out StationDestr2)) sc.Destr2 = StationDestr2;
                    int StationDestr2Par;
                    if (int.TryParse(getSavedValue(rows, "StationDestr2Par" + i), out StationDestr2Par)) sc.Destr2Par = StationDestr2Par;
                    int StationDestr1;
                    if (int.TryParse(getSavedValue(rows, "StationDestr1" + i), out StationDestr1)) sc.Destr1 = StationDestr1;
                    int StationDestr1Par;
                    if (int.TryParse(getSavedValue(rows, "StationDestr1Par" + i), out StationDestr1Par)) sc.Destr1Par = StationDestr1Par;
                    int StationDestrG;
                    if (int.TryParse(getSavedValue(rows, "StationDestrG" + i), out StationDestrG)) sc.DestrG = StationDestrG;
                    int StationGun1;
                    if (int.TryParse(getSavedValue(rows, "StationGun1" + i), out StationGun1)) sc.Gun1 = StationGun1;
                    int StationGun2;
                    if (int.TryParse(getSavedValue(rows, "StationGun2" + i), out StationGun2)) sc.Gun2 = StationGun2;
                    int StationGun3;
                    if (int.TryParse(getSavedValue(rows, "StationGun3" + i), out StationGun3)) sc.Gun3 = StationGun3;
                    int StationMiniGun;
                    if (int.TryParse(getSavedValue(rows, "StationMiniGun" + i), out StationMiniGun)) sc.MiniGun = StationMiniGun;
                    int StationFighter;
                    if (int.TryParse(getSavedValue(rows, "StationFighter" + i), out StationFighter)) sc.Fighter = StationFighter;

                    if (StationIsGuardCore && !StationIsPlayer)
                    {
                        Lists.CPUGuardStations.Add(sc);

                        //this features is used to display the guard attack time on timer, so the guard timer will be activated only in case if the level with guard station
                        if (Lists.currentLevel == 6 || Lists.currentLevel == 7 || Lists.currentLevel == 9 || Lists.currentLevel == 10)
                        {
                            Lists.guardCoreStation = sc;
                            GuardAttackTimerTitle.gameObject.SetActive(true);
                            GuardTurnSlider.minValue = sc.CPUSceneCruiserLaunchTimer * -1;
                            GuardTurnSlider.maxValue = 0;
                        }
                    }
                    else
                    {
                        if (StationIsCPU)
                        {
                            if (StationCPUNumber == 0) Lists.CPU1Stations.Add(sc);
                            else if (StationCPUNumber == 1) Lists.CPU2Stations.Add(sc);
                            else if (StationCPUNumber == 2) Lists.CPU3Stations.Add(sc);
                            else if (StationCPUNumber == 3) Lists.CPU4Stations.Add(sc);
                        }
                        else if (StationIsPlayer) Lists.playerStations.Add(sc);
                        else if (StationIsGuard) Lists.CPUGuardStations.Add(sc);
                    }
                    //this line is necessary to prevent null reference exception cause guard core station reference should be saved anyway, whether it players or not 
                    if (StationIsGuardCore && StationIsPlayer) Lists.guardCoreStation = sc;



                    //the station is added to all stations collection anyway
                    Lists.AllStations.Add(sc);

                    CPUStationReal.SetActive(true);

                    //if (StationIsCPU && StationCPUSceneCruiserLaunchCoroutineIsOn)
                    //{
                    //    allCruisLaunchingCPUStations.Add(sc);
                    //    resetTheTimer();
                    //}

                    //starting the fleet increase process if the station fleet increase stam is not already the max
                    //if (StationIsCPU /*&& sc.fleetIncreaseStep < 3*/)
                    //{
                    //    int destrProductionCount;
                    //    if (int.TryParse(getSavedValue(rows, "destrProductionCount" + i), out destrProductionCount)) sc.destrProductionCount = destrProductionCount;
                    //    float CPUStationFleetIncreaserTimer;
                    //    if (float.TryParse(getSavedValue(rows, "CPUStationFleetIncreaserTimer" + i), out CPUStationFleetIncreaserTimer)) sc.CPUStationFleetIncreaserTimer = CPUStationFleetIncreaserTimer;
                        
                    //    //if (Lists.currentLevelDifficulty == 0) sc.Invoke("setCPUStationFleetByLvl", (Constants.Instance.timeOfCPUStationStepEasy - 90));
                    //    //else if (Lists.currentLevelDifficulty == 1) sc.Invoke("setCPUStationFleetByLvl", (Constants.Instance.timeOfCPUStationStepMiddle - 90));
                    //    //else if (Lists.currentLevelDifficulty == 2) sc.Invoke("setCPUStationFleetByLvl", (Constants.Instance.timeOfCPUStationStepHard - 90));
                    //}
                    //starting production process of player station
                    //if (StationIsPlayer)
                    //{

                    //    ////launching next ship to produce if production plan is not 0, else launching the energy production
                    //    //if (sc.productionPlan.Count > 0)
                    //    //{
                    //    //    //sc.launchProductionAuto(sc.productionPlan[0]);
                    //    //}
                    //    ////else sc.energyProductionLaunche();
                    //}
                }

                //instantiating empty stations on scene
                for (int i = 0; i < savedEmptyNumbers; i++)
                {
                    string EmptyTag = getSavedValue(rows, "EmptyTag" + i);
                    if (EmptyTag.Contains("1"))
                    {
                        emptyStationsListToActivate = ObjectPullerJourney.current.GetEmptyStation0PullList();
                        emptyStation = ObjectPullerJourney.current.GetUniversalBullet(emptyStationsListToActivate);
                    }
                    else if (EmptyTag.Contains("2"))
                    {
                        emptyStationsListToActivate = ObjectPullerJourney.current.GetEmptyStation1PullList();
                        emptyStation = ObjectPullerJourney.current.GetUniversalBullet(emptyStationsListToActivate);
                    }
                    else if (EmptyTag.Contains("3"))
                    {
                        emptyStationsListToActivate = ObjectPullerJourney.current.GetEmptyStation2PullList();
                        emptyStation = ObjectPullerJourney.current.GetUniversalBullet(emptyStationsListToActivate);
                    }
                    else if (EmptyTag.Contains("4"))
                    {
                        emptyStationsListToActivate = ObjectPullerJourney.current.GetEmptyStation3PullList();
                        emptyStation = ObjectPullerJourney.current.GetUniversalBullet(emptyStationsListToActivate);
                    }
                    //assigning the coordinates of loaded station
                    float EmptyXPos;
                    if (float.TryParse(getSavedValue(rows, "EmptyXPos" + i), out EmptyXPos))
                    {
                        float EmptyZPos;
                        if (float.TryParse(getSavedValue(rows, "EmptyZPos" + i), out EmptyZPos)) emptyStation.transform.position = new Vector3(EmptyXPos, -8, EmptyZPos);
                    }
                    emptyStation.transform.rotation = Quaternion.identity;
                    emptyStation.SetActive(true);
                    Lists.emptyStations.Add(emptyStation.GetComponentInChildren<CaptureLine>());
                }

                int CPUCruisersOnScene;
                if (int.TryParse(getSavedValue(rows, "CPUCruisersOnScene"), out CPUCruisersOnScene))
                {
                    for (int i = 0; i < CPUCruisersOnScene; i++)
                    {
                        string CruiserName = getSavedValue(rows, "CruiserName" + i);
                        if (CruiserName.Contains("1"))
                        {
                            cruiserListToActivate = ObjectPullerJourney.current.GetCruis1JourneyCPUPullList();
                            cruiserToActivate = ObjectPullerJourney.current.GetUniversalBullet(cruiserListToActivate);
                        }
                        else if (CruiserName.Contains("2"))
                        {
                            cruiserListToActivate = ObjectPullerJourney.current.GetCruis2JourneyCPUPullList();
                            cruiserToActivate = ObjectPullerJourney.current.GetUniversalBullet(cruiserListToActivate);
                        }
                        else if (CruiserName.Contains("3"))
                        {
                            cruiserListToActivate = ObjectPullerJourney.current.GetCruis3JourneyCPUPullList();
                            cruiserToActivate = ObjectPullerJourney.current.GetUniversalBullet(cruiserListToActivate);
                        }
                        else if (CruiserName.Contains("4"))
                        {
                            cruiserListToActivate = ObjectPullerJourney.current.GetCruis4JourneyCPUPullList();
                            cruiserToActivate = ObjectPullerJourney.current.GetUniversalBullet(cruiserListToActivate);
                        }
                        //assigning the coordinates of loaded station
                        float CruiserXPos;
                        if (float.TryParse(getSavedValue(rows, "CruiserXPos" + i), out CruiserXPos))
                        {
                            float CruiserZPos;
                            if (float.TryParse(getSavedValue(rows, "CruiserZPos" + i), out CruiserZPos))
                                cruiserToActivate.transform.position = new Vector3(CruiserXPos, -8, CruiserZPos);
                        }
                        
                        CPUShipCtrlJourney CPUCruisCtrlr = cruiserToActivate.GetComponent<CPUShipCtrlJourney>();

                        //setting CPU number for loadad cruiser
                        int CruiserCPUNumber;
                        if (int.TryParse(getSavedValue(rows, "CruiserCPUNumber" + i), out CruiserCPUNumber)) CPUCruisCtrlr.CPUNumber = CruiserCPUNumber;

                        //assigning the coordinates of loaded point whre this cruiser was heading to while closing the game and saving the data
                        float CruiserMovePointX;
                        if (float.TryParse(getSavedValue(rows, "CruiserMovePointX" + i), out CruiserMovePointX))
                        {
                            float CruiserMovePointZ;
                            if (float.TryParse(getSavedValue(rows, "CruiserMovePointZ" + i), out CruiserMovePointZ))
                                CPUCruisCtrlr.moveToPoint = new Vector3(CruiserMovePointX, -8, CruiserMovePointZ);
                        }
                        float CruiserYRotation;
                        if (float.TryParse(getSavedValue(rows, "CruiserYRotation" + i), out CruiserYRotation))
                        {
                            //+90 is some constant to make cruiser to look into right direction
                            cruiserToActivate.transform.rotation = Quaternion.Euler(0, CruiserYRotation + 90, 0);
                            CPUCruisCtrlr.yRotation = CruiserYRotation;
                        }

                        float CruiserEnergy;
                        if (float.TryParse(getSavedValue(rows, "CruiserEnergy" + i), out CruiserEnergy)) CPUCruisCtrlr.energy = CruiserEnergy;
                        int CruiserCruis4;
                        if (int.TryParse(getSavedValue(rows, "CruiserCruis4" + i), out CruiserCruis4)) CPUCruisCtrlr.Cruis4 = CruiserCruis4;
                        int CruiserCruis3;
                        if (int.TryParse(getSavedValue(rows, "CruiserCruis3" + i), out CruiserCruis3)) CPUCruisCtrlr.Cruis3 = CruiserCruis3;
                        int CruiserCruis2;
                        if (int.TryParse(getSavedValue(rows, "CruiserCruis2" + i), out CruiserCruis2)) CPUCruisCtrlr.Cruis2 = CruiserCruis2;
                        int CruiserCruis1;
                        if (int.TryParse(getSavedValue(rows, "CruiserCruis1" + i), out CruiserCruis1)) CPUCruisCtrlr.Cruis1 = CruiserCruis1;
                        int CruiserCruisG;
                        if (int.TryParse(getSavedValue(rows, "CruiserCruisG" + i), out CruiserCruisG)) CPUCruisCtrlr.CruisG = CruiserCruisG;
                        int CruiserDestr4;
                        if (int.TryParse(getSavedValue(rows, "CruiserDestr4" + i), out CruiserDestr4)) CPUCruisCtrlr.Destr4 = CruiserDestr4;
                        int CruiserDestr3;
                        if (int.TryParse(getSavedValue(rows, "CruiserDestr3" + i), out CruiserDestr3)) CPUCruisCtrlr.Destr3 = CruiserDestr3;
                        int CruiserDestr2;
                        if (int.TryParse(getSavedValue(rows, "CruiserDestr2" + i), out CruiserDestr2)) CPUCruisCtrlr.Destr2 = CruiserDestr2;
                        int CruiserDestr2Par;
                        if (int.TryParse(getSavedValue(rows, "CruiserDestr2Par" + i), out CruiserDestr2Par)) CPUCruisCtrlr.Destr2Par = CruiserDestr2Par;
                        int CruiserDestr1;
                        if (int.TryParse(getSavedValue(rows, "CruiserDestr1" + i), out CruiserDestr1)) CPUCruisCtrlr.Destr1 = CruiserDestr1;
                        int CruiserDestr1Par;
                        if (int.TryParse(getSavedValue(rows, "CruiserDestr1Par" + i), out CruiserDestr1Par)) CPUCruisCtrlr.Destr1Par = CruiserDestr1Par;
                        int CruiserDestrG;
                        if (int.TryParse(getSavedValue(rows, "CruiserDestrG" + i), out CruiserDestrG)) CPUCruisCtrlr.DestrG = CruiserDestrG;
                        int CruiserGun1;
                        if (int.TryParse(getSavedValue(rows, "CruiserGun1" + i), out CruiserGun1)) CPUCruisCtrlr.Gun1 = CruiserGun1;
                        int CruiserGun2;
                        if (int.TryParse(getSavedValue(rows, "CruiserGun2" + i), out CruiserGun2)) CPUCruisCtrlr.Gun2 = CruiserGun2;
                        int CruiserGun3;
                        if (int.TryParse(getSavedValue(rows, "CruiserGun3" + i), out CruiserGun3)) CPUCruisCtrlr.Gun3 = CruiserGun3;
                        int CruiserMiniGun;
                        if (int.TryParse(getSavedValue(rows, "CruiserMiniGun" + i), out CruiserMiniGun)) CPUCruisCtrlr.MiniGun = CruiserMiniGun;
                        int CruiserFighter;
                        if (int.TryParse(getSavedValue(rows, "CruiserFighter" + i), out CruiserFighter)) CPUCruisCtrlr.Fighter = CruiserFighter;


                        //loading the data that determines if cruiser is moving toward something or not
                        bool CruiserIsMoving;
                        if (bool.TryParse(getSavedValue(rows, "CruiserIsMoving" + i), out CruiserIsMoving)) CPUCruisCtrlr.isMoving = CruiserIsMoving;

                        cruiserToActivate.SetActive(true);

                        //setting a proper color to sphere of CPU cruiser to make it has the same color that station CPU's has
                        for (int u = 0; u < CPUCruisCtrlr.IDColorElements.Count; u++)
                        {
                            CPUCruisCtrlr.IDColorElements[u].GetComponent<MeshRenderer>().material.SetColor("_Color", getProperMatColorByIndex(colorsOfPlayers[CPUCruisCtrlr.CPUNumber]));
                        }
                        //adding the CPU cruiser to the list that will restore cruiser state after getting back to scene if it was in scene while switching
                        Lists.shipsOnScene.Add(cruiserToActivate);
                        //this one is used while determining if player win the level, so if there left no enemy cruisers on level player wins
                        //also while updating the information about UI of this player stations
                        /*Lists.enemyCruisersOnScene++;*/
                        if (CPUCruisCtrlr.CPUNumber == 0)
                        {
                            Lists.CPU1CruisersOnScene++;
                        }
                        else if (CPUCruisCtrlr.CPUNumber == 1)
                        {
                            Lists.CPU2CruisersOnScene++;
                        }
                        else if (CPUCruisCtrlr.CPUNumber == 2)
                        {
                            Lists.CPU3CruisersOnScene++;
                        }
                        else if (CPUCruisCtrlr.CPUNumber == 3)
                        {
                            Lists.CPU4CruisersOnScene++;
                        }

                        //turning on the indictaor of cruisers on scene UI 
                        //turnOnOffLaunchedCruiserIcon(CPUCruisCtrlr.CPUNumber + 1, true);
                    }
                }

                int PlayerCruisersOnScene;
                if (int.TryParse(getSavedValue(rows, "PlayerCruisersOnScene"), out PlayerCruisersOnScene))
                {
                    for (int i = 0; i < PlayerCruisersOnScene; i++)
                    {
                        string CruiserName = getSavedValue(rows, "PlayerCruiserName" + i);
                        if (CruiserName.Contains("1"))
                        {
                            cruiserListToActivate = ObjectPullerJourney.current.GetCruis1JourneyPlayerPullList();
                            cruiserToActivate = ObjectPullerJourney.current.GetUniversalBullet(cruiserListToActivate);
                        }
                        else if (CruiserName.Contains("2"))
                        {
                            cruiserListToActivate = ObjectPullerJourney.current.GetCruis2JourneyPlayerPullList();
                            cruiserToActivate = ObjectPullerJourney.current.GetUniversalBullet(cruiserListToActivate);
                        }
                        else if (CruiserName.Contains("3"))
                        {
                            cruiserListToActivate = ObjectPullerJourney.current.GetCruis3JourneyPlayerPullList();
                            cruiserToActivate = ObjectPullerJourney.current.GetUniversalBullet(cruiserListToActivate);
                        }
                        else if (CruiserName.Contains("4"))
                        {
                            cruiserListToActivate = ObjectPullerJourney.current.GetCruis4JourneyPlayerPullList();
                            cruiserToActivate = ObjectPullerJourney.current.GetUniversalBullet(cruiserListToActivate);
                        }

                        LaunchingObjcts PlayerCruisCtrlr = cruiserToActivate.GetComponent<LaunchingObjcts>();


                        //assigning the coordinates of loaded cruiser
                        float PlayerCruiserXPos;
                        if (float.TryParse(getSavedValue(rows, "PlayerCruiserXPos" + i), out PlayerCruiserXPos))
                        {
                            float PlayerCruiserZPos;
                            if (float.TryParse(getSavedValue(rows, "PlayerCruiserZPos" + i), out PlayerCruiserZPos))
                            {
                                float PlayerCruiserYRot;
                                if (float.TryParse(getSavedValue(rows, "PlayerCruiserYRotation" + i), out PlayerCruiserYRot))
                                {
                                    cruiserToActivate.transform.position = new Vector3(PlayerCruiserXPos, -8, PlayerCruiserZPos);
                                    cruiserToActivate.transform.rotation = Quaternion.Euler(0, PlayerCruiserYRot, 0);
                                }
                            }
                        }

                        //assigning the coordinates of loaded point whre this cruiser was heading to while closing the game and saving the data
                        //float CruiserMovePointX;
                        //if (float.TryParse(getSavedValue(rows, "PlayerCruiserMovePointX" + i), out CruiserMovePointX))
                        //{
                        //    float CruiserMovePointZ;
                        //    if (float.TryParse(getSavedValue(rows, "PlayerCruiserMovePointZ" + i), out CruiserMovePointZ))
                        //        PlayerCruisCtrlr.moveToPoint = new Vector3(CruiserMovePointX, -8, CruiserMovePointZ);
                        //}

                        int PlayerCruiserCruis4;
                        if (int.TryParse(getSavedValue(rows, "PlayerCruiserCruis4" + i), out PlayerCruiserCruis4)) PlayerCruisCtrlr.Cruis4 = PlayerCruiserCruis4;
                        int PlayerCruiserCruis3;
                        if (int.TryParse(getSavedValue(rows, "PlayerCruiserCruis3" + i), out PlayerCruiserCruis3)) PlayerCruisCtrlr.Cruis3 = PlayerCruiserCruis3;
                        int PlayerCruiserCruis2;
                        if (int.TryParse(getSavedValue(rows, "PlayerCruiserCruis2" + i), out PlayerCruiserCruis2)) PlayerCruisCtrlr.Cruis2 = PlayerCruiserCruis2;
                        int PlayerCruiserCruis1;
                        if (int.TryParse(getSavedValue(rows, "PlayerCruiserCruis1" + i), out PlayerCruiserCruis1)) PlayerCruisCtrlr.Cruis1 = PlayerCruiserCruis1;
                        int PlayerCruiserCruisG;
                        if (int.TryParse(getSavedValue(rows, "PlayerCruiserCruisG" + i), out PlayerCruiserCruisG)) PlayerCruisCtrlr.CruisG = PlayerCruiserCruisG;
                        int PlayerCruiserDestr4;
                        if (int.TryParse(getSavedValue(rows, "PlayerCruiserDestr4" + i), out PlayerCruiserDestr4)) PlayerCruisCtrlr.Destr4 = PlayerCruiserDestr4;
                        int PlayerCruiserDestr3;
                        if (int.TryParse(getSavedValue(rows, "PlayerCruiserDestr3" + i), out PlayerCruiserDestr3)) PlayerCruisCtrlr.Destr3 = PlayerCruiserDestr3;
                        int PlayerCruiserDestr2;
                        if (int.TryParse(getSavedValue(rows, "PlayerCruiserDestr2" + i), out PlayerCruiserDestr2)) PlayerCruisCtrlr.Destr2 = PlayerCruiserDestr2;
                        int PlayerCruiserDestr2Par;
                        if (int.TryParse(getSavedValue(rows, "PlayerCruiserDestr2Par" + i), out PlayerCruiserDestr2Par)) PlayerCruisCtrlr.Destr2Par = PlayerCruiserDestr2Par;
                        int PlayerCruiserDestr1;
                        if (int.TryParse(getSavedValue(rows, "PlayerCruiserDestr1" + i), out PlayerCruiserDestr1)) PlayerCruisCtrlr.Destr1 = PlayerCruiserDestr1;
                        int PlayerCruiserDestr1Par;
                        if (int.TryParse(getSavedValue(rows, "PlayerCruiserDestr1Par" + i), out PlayerCruiserDestr1Par)) PlayerCruisCtrlr.Destr1Par = PlayerCruiserDestr1Par;
                        int PlayerCruiserDestrG;
                        if (int.TryParse(getSavedValue(rows, "PlayerCruiserDestrG" + i), out PlayerCruiserDestrG)) PlayerCruisCtrlr.DestrG = PlayerCruiserDestrG;
                        int PlayerCruiserGun1;
                        if (int.TryParse(getSavedValue(rows, "PlayerCruiserGun1" + i), out PlayerCruiserGun1)) PlayerCruisCtrlr.Gun1 = PlayerCruiserGun1;
                        int PlayerCruiserGun2;
                        if (int.TryParse(getSavedValue(rows, "PlayerCruiserGun2" + i), out PlayerCruiserGun2)) PlayerCruisCtrlr.Gun2 = PlayerCruiserGun2;
                        int PlayerCruiserGun3;
                        if (int.TryParse(getSavedValue(rows, "PlayerCruiserGun3" + i), out PlayerCruiserGun3)) PlayerCruisCtrlr.Gun3 = PlayerCruiserGun3;
                        int PlayerCruiserMiniGun;
                        if (int.TryParse(getSavedValue(rows, "PlayerCruiserMiniGun" + i), out PlayerCruiserMiniGun)) PlayerCruisCtrlr.MiniGun = PlayerCruiserMiniGun;
                        int PlayerCruiserFighter;
                        if (int.TryParse(getSavedValue(rows, "PlayerCruiserFighter" + i), out PlayerCruiserFighter)) PlayerCruisCtrlr.Fighter = PlayerCruiserFighter;


                        //loading the data that determines if cruiser is moving toward something or not
                        //bool CruiserIsMoving;
                        //if (bool.TryParse(getSavedValue(rows, "CruiserIsMoving" + i), out CruiserIsMoving)) PlayerCruisCtrlr.isMoving = CruiserIsMoving;

                        


                        //adding the CPU cruiser to the list that will restore cruiser state after getting back to scene if it was in scene while switching
                        Lists.shipsOnScene.Add(cruiserToActivate);

                        cruiserToActivate.SetActive(true); 
                        
                        cruiserToActivate.GetComponent<Collider>().enabled = true;
                        //this one is necessary to eneable the collider of energy gather child component of player scene cruiser
                        foreach (Collider col in cruiserToActivate.GetComponentsInChildren<Collider>()) col.enabled = true;

                        //setting a proper color to sphere of CPU cruiser to make it has the same color that station CPU's has
                        for (int u = 0; u < PlayerCruisCtrlr.IDColorElements.Count; u++)
                        {
                            PlayerCruisCtrlr.IDColorElements[u].GetComponent<MeshRenderer>().material.SetColor("_Color", getProperMatColorByIndex(Lists.playerColor));
                        }
                    }
                }


                int EnergonsOnScene;
                if (int.TryParse(getSavedValue(rows, "EnergonsOnScene"), out EnergonsOnScene))
                {
                    for (int i = 0; i < EnergonsOnScene; i++)
                    {
                        energonShipReal = Instantiate(energonShip, new Vector3(0, -8, 0), Quaternion.identity);

                        //assigning the coordinates of loaded station
                        float EnergonXPos;
                        if (float.TryParse(getSavedValue(rows, "EnergonXPos" + i), out EnergonXPos))
                        {
                            float EnergonZPos;
                            if (float.TryParse(getSavedValue(rows, "EnergonZPos" + i), out EnergonZPos))
                                energonShipReal.transform.position = new Vector3(EnergonXPos, -8, EnergonZPos);
                        }

                        EnergonController energonController = energonShipReal.GetComponent<EnergonController>();

                        float EnergonYRotation;
                        if (float.TryParse(getSavedValue(rows, "EnergonYRotation" + i), out EnergonYRotation))
                        {
                            //+90 is some constant to make cruiser to look into right direction
                            energonShipReal.transform.rotation = Quaternion.Euler(0, EnergonYRotation, 0);
                            energonController.yRotation = EnergonYRotation;
                        }

                        float EnergonMovePointX;
                        if (float.TryParse(getSavedValue(rows, "EnergonMovePointX" + i), out EnergonMovePointX)) {
                            float EnergonMovePointZ;
                            if (float.TryParse(getSavedValue(rows, "EnergonMovePointZ" + i), out EnergonMovePointZ))
                            {
                                energonController.moveToPoint = new Vector3(EnergonMovePointX,-8, EnergonMovePointZ);
                            }
                        }

                        int energonLevel;
                        if (int.TryParse(getSavedValue(rows, "energonLevel" + i), out energonLevel)) energonController.energonLevel = energonLevel;
                        bool isPlayerEnergon;
                        if (bool.TryParse(getSavedValue(rows, "isPlayerEnergon" + i), out isPlayerEnergon)) energonController.isPlayerEnergon = isPlayerEnergon;
                        
                        float energyCapacity;
                        if (float.TryParse(getSavedValue(rows, "energyCapacity" + i), out energyCapacity)) energonController.energyCapacity = energyCapacity;
                        float energyOfEnergon;
                        if (float.TryParse(getSavedValue(rows, "energyOfEnergon" + i), out energyOfEnergon)) energonController.energyOfEnergon = energyOfEnergon;
                        float energonMovingSpeed;
                        if (float.TryParse(getSavedValue(rows, "energonMovingSpeed" + i), out energonMovingSpeed)) energonController.energonMovingSpeed = energonMovingSpeed;
                        int CPUNumber;
                        if (int.TryParse(getSavedValue(rows, "CPUNumber" + i), out CPUNumber)) energonController.CPUNumber = CPUNumber;
                        
                        float EnergonsStationXPos;
                        if (float.TryParse(getSavedValue(rows, "EnergonsStationXPos" + i), out EnergonsStationXPos)) {
                            float EnergonsStationZPos;
                            if (float.TryParse(getSavedValue(rows, "EnergonsStationZPos" + i), out EnergonsStationZPos))
                            {
                                for (int y = 0; y < Lists.AllStations.Count; y++) {
                                    if (!Lists.AllStations[y].isGuardCoreStation && Lists.AllStations[y].transform.position.x == EnergonsStationXPos && Lists.AllStations[y].transform.position.z == EnergonsStationZPos) {
                                        energonController.energonsStation = Lists.AllStations[y];
                                        energonController.energonsStationPosition = Lists.AllStations[y].transform.position;
                                        Lists.AllStations[y].stationsEnergon = energonController;
                                        if (Lists.AllStations[y].isPlayerStation)
                                        {
                                            energonController.colorOfEnergonMat = getProperMatColorByIndex(Lists.playerColor);
                                            for (int x = 0; x < energonController.IDColorElements.Count; x++)
                                            {
                                                energonController.IDColorElements[x].GetComponent<MeshRenderer>().material.SetColor("_Color", energonController.colorOfEnergonMat);
                                            }
                                        }
                                        else {
                                            if (Lists.AllStations[y].isCPUStation)
                                            {
                                                energonController.colorOfEnergonMat = getProperMatColorByIndex(colorsOfPlayers[energonController.CPUNumber]);
                                                for (int x = 0; x < energonController.IDColorElements.Count; x++)
                                                {
                                                    energonController.IDColorElements[x].GetComponent<MeshRenderer>().material.SetColor("_Color", energonController.colorOfEnergonMat);
                                                }
                                            }
                                            else if (Lists.AllStations[y].isGuardStation) {
                                                energonController.colorOfEnergonMat = new Color(2.4f, 2.4f, 2.4f, 1);
                                                for (int x = 0; x < energonController.IDColorElements.Count; x++)
                                                {
                                                    energonController.IDColorElements[x].GetComponent<MeshRenderer>().material.SetColor("_Color", energonController.colorOfEnergonMat);
                                                }
                                            }
                                        }
                                        energonController.addThisToSelectable();
                                    }
                                }
                            }
                        }
                        bool EnergonIsMoving;
                        if (bool.TryParse(getSavedValue(rows, "EnergonIsMoving" + i), out EnergonIsMoving)) energonController.isMoving = EnergonIsMoving;
                        if (isPlayerEnergon)
                        {

                            energonShipReal.transform.rotation = Quaternion.Euler(0, Quaternion.LookRotation(energonController.moveToPoint - energonShipReal.transform.position, Vector3.up).eulerAngles.y, 0);
                            energonController.shipMovingLine = energonShipReal.GetComponent<LineRenderer>();
                            energonController.shipMovingLine.positionCount = 2;
                            energonController.shipMovingLine.SetPosition(0, energonShipReal.transform.position);
                            if (EnergonIsMoving) energonController.shipMovingLine.SetPosition(1, energonController.moveToPoint);
                            else energonController.shipMovingLine.SetPosition(1, energonShipReal.transform.position);
                            energonController.shipMovingLine.enabled = true;
                            energonController.engineSound = energonShipReal.GetComponent<AudioSource>();
                        }
                        Lists.energonsControllablesOnScene.Add(energonShipReal);
                    }
                }


                int GuardCruisersOnScene;
                if (int.TryParse(getSavedValue(rows, "GuardCruisersOnScene"), out GuardCruisersOnScene))
                {
                    for (int i = 0; i < GuardCruisersOnScene; i++)
                    {
                        guardShipReal = Instantiate(guardShip, new Vector3(UnityEngine.Random.Range(100f, -100f), -8, UnityEngine.Random.Range(100f, -100f)), Quaternion.identity);

                        //assigning the coordinates of loaded station
                        float GuardXPos;
                        if (float.TryParse(getSavedValue(rows, "GuardXPos" + i), out GuardXPos))
                        {
                            float GuardZPos;
                            if (float.TryParse(getSavedValue(rows, "GuardZPos" + i), out GuardZPos))
                                guardShipReal.transform.position = new Vector3(GuardXPos, -8, GuardZPos);
                        }

                        EnergonMngr GuardMngr = guardShipReal.GetComponent<EnergonMngr>();
                        GuardEnergyGather guardEnrgyGatherMngr = guardShipReal.GetComponentInChildren<GuardEnergyGather>();

                        int GuardLvl;
                        if (int.TryParse(getSavedValue(rows, "GuardLvl" + i), out GuardLvl)) GuardMngr.energonAndGuardLvl = GuardLvl;
                        float nextRotatioLerpGuard;
                        if (float.TryParse(getSavedValue(rows, "nextRotatioLerpGuard" + i), out nextRotatioLerpGuard)) GuardMngr.nextRotatioLerp = nextRotatioLerpGuard;
                        float nextMovingSpeedGuard;
                        if (float.TryParse(getSavedValue(rows, "nextMovingSpeedGuard" + i), out nextMovingSpeedGuard)) GuardMngr.nextMovingSpeed = nextMovingSpeedGuard;
                        float guardChaseTimeMiddle;
                        if (float.TryParse(getSavedValue(rows, "guardChaseTimeMiddle" + i), out guardChaseTimeMiddle)) GuardMngr.guardChaseTimeMiddle = guardChaseTimeMiddle;
                        float guardTranslateModif;
                        if (float.TryParse(getSavedValue(rows, "guardTranslateModif" + i), out guardTranslateModif)) GuardMngr.guardTranslateModif = guardTranslateModif;


                        if (GuardLvl==0) guardEnrgyGatherMngr.energyChageRate = Constants.Instance.energyReduce0;
                        else if (GuardLvl == 1) guardEnrgyGatherMngr.energyChageRate = Constants.Instance.energyReduce1;
                        else if(GuardLvl == 2) guardEnrgyGatherMngr.energyChageRate = Constants.Instance.energyReduce2;
                        else guardEnrgyGatherMngr.energyChageRate = Constants.Instance.energyReduce3;

                        //aimingObjects.Add(energonShipReal.transform);
                        Lists.energonsOnScene.Add(guardShipReal);

                        int energyOfGuard;
                        if (int.TryParse(getSavedValue(rows, "energyOfGuard" + i), out energyOfGuard)) GuardMngr.energyOfEnergonAndGuard = energyOfGuard;
                        int GuardCruis4;
                        if (int.TryParse(getSavedValue(rows, "GuardCruis4" + i), out GuardCruis4)) GuardMngr.Cruis4 = GuardCruis4;
                        int GuardCruis3;
                        if (int.TryParse(getSavedValue(rows, "GuardCruis3" + i), out GuardCruis3)) GuardMngr.Cruis3 = GuardCruis3;
                        int GuardCruis2;
                        if (int.TryParse(getSavedValue(rows, "GuardCruis2" + i), out GuardCruis2)) GuardMngr.Cruis2 = GuardCruis2;
                        int GuardCruis1;
                        if (int.TryParse(getSavedValue(rows, "GuardCruis1" + i), out GuardCruis1)) GuardMngr.Cruis1 = GuardCruis1;
                        int GuardCruisG;
                        if (int.TryParse(getSavedValue(rows, "GuardCruisG" + i), out GuardCruisG)) GuardMngr.CruisG = GuardCruisG;
                        int GuardDestr4;
                        if (int.TryParse(getSavedValue(rows, "GuardDestr4" + i), out GuardDestr4)) GuardMngr.Destr4 = GuardDestr4;
                        int GuardDestr3;
                        if (int.TryParse(getSavedValue(rows, "GuardDestr3" + i), out GuardDestr3)) GuardMngr.Destr3 = GuardDestr3;
                        int GuardDestr2;
                        if (int.TryParse(getSavedValue(rows, "GuardDestr2" + i), out GuardDestr2)) GuardMngr.Destr2 = GuardDestr2;
                        int GuardDestr2Par;
                        if (int.TryParse(getSavedValue(rows, "GuardDestr2Par" + i), out GuardDestr2Par)) GuardMngr.Destr2Par = GuardDestr2Par;
                        int GuardDestr1;
                        if (int.TryParse(getSavedValue(rows, "GuardDestr1" + i), out GuardDestr1)) GuardMngr.Destr1 = GuardDestr1;
                        int GuardDestr1Par;
                        if (int.TryParse(getSavedValue(rows, "GuardDestr1Par" + i), out GuardDestr1Par)) GuardMngr.Destr1Par = GuardDestr1Par;
                        int GuardDestrG;
                        if (int.TryParse(getSavedValue(rows, "GuardDestrG" + i), out GuardDestrG)) GuardMngr.DestrG = GuardDestrG;
                        int GuardGun1;
                        if (int.TryParse(getSavedValue(rows, "GuardGun1" + i), out GuardGun1)) GuardMngr.Gun1 = GuardGun1;
                        int GuardGun2;
                        if (int.TryParse(getSavedValue(rows, "GuardGun2" + i), out GuardGun2)) GuardMngr.Gun2 = GuardGun2;
                        int GuardGun3;
                        if (int.TryParse(getSavedValue(rows, "GuardGun3" + i), out GuardGun3)) GuardMngr.Gun3 = GuardGun3;
                        int GuardMiniGun;
                        if (int.TryParse(getSavedValue(rows, "GuardMiniGun" + i), out GuardMiniGun)) GuardMngr.MiniGun = GuardMiniGun;
                        int GuardFighter;
                        if (int.TryParse(getSavedValue(rows, "GuardFighter" + i), out GuardFighter)) GuardMngr.Fighter = GuardFighter;
                    }
                }

                //resetting the UI info about stations and cruisers on journey scene
                journeySceneStationsIconsCtrlr();

                //making this condition false to make stations clear it's fleet's while enabling from puller after the cruisers captures the stations
                Lists.isContinued = false;
            }



            //localLaunchingObjects = CruisJourneyReal.GetComponent<LaunchingObjcts>();
            //localLaunchingObjects.captureLaserButton = captureButton;

            //virtualCamera.GetComponent<VirtualCamCtrlr>().getThePlayerInstaceToFollow(localLaunchingObjects);



            //CPU1Ctrller.enabled = true;
        }
        else
        {
            foreach (GameObject go in Lists.AllSceneObjects)
            {
                go.SetActive(true);
            };

            //clear allScene objects list after scene back to JourneyScene to prepare AllScene list for next scene switch
            Lists.AllSceneObjects.Clear();

            //updating the UI icons of station after getting back to scene to update information later on activating cruisers on scene loop and
            //even lower while updating the conquered stations information inside these methods 
            journeySceneStationsIconsCtrlr();

            //Invoke("clearAllSceneObjects",1);

            foreach (GameObject go in Lists.shipsOnScene)
            {
                //TODO IF I WILL CHANGE THE TAG OF CPU SHIP
                //this code assigns the non destroyed on load to battle scene player cruiser, to curren instance of cruisJourney and assigns its propertes as well
                //not untagged means that it is player cruiser, cause CPU cruiser tag is untagged
                if (!go.CompareTag(Cruis1CPUTag) && !go.CompareTag(Cruis2CPUTag) && !go.CompareTag(Cruis3CPUTag) && !go.CompareTag(Cruis4CPUTag))
                {
                    if (go != Lists.shipOnAttack)
                    {
                        //CruisJourneyReal = go;
                        //localLaunchingObjects = CruisJourneyReal.GetComponent<LaunchingObjcts>();
                        //localLaunchingObjects.isParalized = false; //turning off paralized effect of player cruiser after getting back from any other scene
                        //localLaunchingObjects.captureLaserButton = captureButton;
                        //virtualCamera.GetComponent<VirtualCamCtrlr>().getThePlayerInstaceToFollow(localLaunchingObjects);
                        go.SetActive(true);

                        //only after player cruiser is enabled all colliders of it are activated to prevent the missing reference bug on LaunchingObjcts calss while 
                        //looking for a CaptureButton object in OnTriggeer enter method, cause it is called begore all
                        go.GetComponent<Collider>().enabled = true;
                        //this one is necessary to eneable the collider of energy gather child component of player scene cruiser
                        foreach (Collider col in go.GetComponentsInChildren<Collider>()) col.enabled = true;
                    }
                }
                //here is necessary to check if the battle was not with guard to prevent a bug of missing reference to enemy cruiser
                else
                {
                    if (!Lists.battleWithGuard && !Lists.PlayerAttacksCPUStation)
                    {
                        //activating only the CPU cruiser that are not attcking player station
                        if (go != Lists.CPUShipOnAttack)
                        {
                            go.SetActive(true);
                            //activating the launched cruiser signal for the CPU cruiser tha was respawned in scene
                            //turnOnOffLaunchedCruiserIcon(go.GetComponent<CPUShipCtrlJourney>().CPUNumber + 1, true);
                        }
                    }
                    else
                    {
                        go.SetActive(true);
                        //activating the launched cruiser signal for the CPU cruiser tha was respawned in scene
                        //turnOnOffLaunchedCruiserIcon(go.GetComponent<CPUShipCtrlJourney>().CPUNumber + 1, true);
                    }

                }

                //only after player cruiser is enabled all colliders of it are activated to prevent the missing reference bug on LaunchingObjcts calss while 
                //looking for a CaptureButton object in OnTriggeer enter method, cause it is called begore all
                //if (!go.CompareTag("Untagged")) foreach (Collider col in go.GetComponents<Collider>()) col.enabled = true;
            }

            for (int i = 0; i < Lists.energonsOnScene.Count; i++)
            {
                //if (Lists.energonsOnScene[i].CompareTag("GCruisOut"))
                //{
                    if (Lists.energonsOnScene[i] != Lists.CPUShipOnAttack) Lists.energonsOnScene[i].SetActive(true);
                //}
                //else Lists.energonsOnScene[i].SetActive(true);
            }
            for (int i = 0; i < Lists.energonsControllablesOnScene.Count; i++)
            {
                Lists.energonsControllablesOnScene[i].SetActive(true);
                Lists.energonsControllablesOnScene[i].GetComponent<EnergonController>().addThisToSelectable();
            }

            //explanation on Lists class
            Lists.PlayerAttacksCPUStation = false;

            //updating the fleets of the participants of the battle after the battle of ship and station
            if (Lists.battleWithStation) updateTheWinnerStationFleetAfterBattle(Lists.stationOnAttack, Lists.isAfterBattleWin);

            //updating the fleets of the participants of the battle after the battle with guard
            if (Lists.battleWithGuard) updateTheFleetAfterBattleWithGuard(Lists.isAfterBattleWin);

            //updating the fleets of the participants of the battle after the battle with CPU cruiser
            if (Lists.battleWithCruiser) updateTheFleetAfterBattleWithCruisers(Lists.CPUShipOnAttack.GetComponent<CPUShipCtrlJourney>(), Lists.isAfterBattleWin);

            //activating the guardCoreStation attack timer after the battle if it is not palyer's station now and if the level is proper
            if ((Lists.currentLevel == 6 || Lists.currentLevel == 7 || Lists.currentLevel == 9 || Lists.currentLevel == 10)
                && !Lists.guardCoreStation.isPlayerStation) GuardAttackTimerTitle.gameObject.SetActive(true);


        }
        #endregion debug only empty stations activation from pull

        //instantiating the station chose by button properties and dictionaries to display mini data about station on palyer cruis panel
        ButtonAndStation = new Dictionary<GameObject, StationController>();
        TextAndStation = new Dictionary<Text, StationController>();
        MiniImgAndStation = new Dictionary<Image, StationController>();

        spawnTheAsteroids(Lists.currentLevelDifficulty);

        int modifactaor = -1;
        //spawning randomely galaxyes on randome points of scene
        for (int i = 0; i < galaxyCount; i++)
        {
            galaxyReal = Instantiate(Galaxy, new Vector3(UnityEngine.Random.Range(20 * modifactaor, 1500 * modifactaor),
                UnityEngine.Random.Range(-350, -650),
                UnityEngine.Random.Range(20, 1500 * modifactaor)), Quaternion.Euler(UnityEngine.Random.Range(0, 30f), 0, UnityEngine.Random.Range(0, -30f)) /*UnityEngine.Random.rotation*/);

            int colSwitch = UnityEngine.Random.Range(0, 4);
            if (colSwitch == 0)
            {
                galaxyReal.GetComponent<MeshRenderer>().material.SetColor("_Color1", new Color(UnityEngine.Random.Range(0.4f, 0.7f), 0, 0, 0));
                galaxyReal.GetComponent<MeshRenderer>().material.SetColor("_Color2", new Color(UnityEngine.Random.Range(0.5f, 0.8f),
                    UnityEngine.Random.Range(0.05f, 0.2f), 0, 0));
            }
            else if (colSwitch == 1)
            {
                galaxyReal.GetComponent<MeshRenderer>().material.SetColor("_Color1", new Color(UnityEngine.Random.Range(0.15f, 0.3f),
                    UnityEngine.Random.Range(0.7f, 0.85f), 0, 0));
                galaxyReal.GetComponent<MeshRenderer>().material.SetColor("_Color2", new Color(UnityEngine.Random.Range(0.6f, 0.8f),
                    UnityEngine.Random.Range(0.5f, 0.7f), 0, 0));
            }
            else if (colSwitch == 2)
            {
                galaxyReal.GetComponent<MeshRenderer>().material.SetColor("_Color1", new Color(0,
                    UnityEngine.Random.Range(0.55f, 0.85f), UnityEngine.Random.Range(0.45f, 0.8f), 0));
                galaxyReal.GetComponent<MeshRenderer>().material.SetColor("_Color2", new Color(0,
                    0.01f, UnityEngine.Random.Range(0.45f, 0.85f), 0));
            }
            else if (colSwitch == 3)
            {
                galaxyReal.GetComponent<MeshRenderer>().material.SetColor("_Color1", new Color(UnityEngine.Random.Range(0.1f, 0.2f),
                    0, UnityEngine.Random.Range(0.45f, 0.85f), 0));
                galaxyReal.GetComponent<MeshRenderer>().material.SetColor("_Color2", new Color(UnityEngine.Random.Range(0.55f, 0.85f),
                    0, UnityEngine.Random.Range(0.45f, 0.65f), 0));
            }

            modifactaor = modifactaor * -1;
        }
        for (int i = 0; i < galaxyCount; i++)
        {
            galaxyReal = Instantiate(Galaxy, new Vector3(UnityEngine.Random.Range(-20 * modifactaor, -1500 * modifactaor),
                UnityEngine.Random.Range(-350, -650),
                UnityEngine.Random.Range(20, 1500 * modifactaor)), UnityEngine.Random.rotation);
            //main = galaxyReal.GetComponent<ParticleSystem>().main;
            //main.startColor = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f, 1f, 1f); //generates randome color for galaxy

            //setting a color grads depending on randome
            //color gradients shoud be close to each other to look more realistic, for example orage with red and yellow with green
            //0 red, 1 yellow/green, 2 blue/cyan, 3 purple/pink
            int colSwitch = UnityEngine.Random.Range(0, 4);
            if (colSwitch == 0)
            {
                galaxyReal.GetComponent<MeshRenderer>().material.SetColor("_Color1", new Color(UnityEngine.Random.Range(0.4f, 0.7f), 0, 0, 0));
                galaxyReal.GetComponent<MeshRenderer>().material.SetColor("_Color2", new Color(UnityEngine.Random.Range(0.5f, 0.8f),
                    UnityEngine.Random.Range(0.05f, 0.2f), 0, 0));
            }
            else if (colSwitch == 1)
            {
                galaxyReal.GetComponent<MeshRenderer>().material.SetColor("_Color1", new Color(UnityEngine.Random.Range(0.15f, 0.3f),
                    UnityEngine.Random.Range(0.7f, 0.85f), 0, 0));
                galaxyReal.GetComponent<MeshRenderer>().material.SetColor("_Color2", new Color(UnityEngine.Random.Range(0.6f, 0.8f),
                    UnityEngine.Random.Range(0.5f, 0.7f), 0, 0));
            }
            else if (colSwitch == 2)
            {
                galaxyReal.GetComponent<MeshRenderer>().material.SetColor("_Color1", new Color(0,
                    UnityEngine.Random.Range(0.55f, 0.85f), UnityEngine.Random.Range(0.45f, 0.8f), 0));
                galaxyReal.GetComponent<MeshRenderer>().material.SetColor("_Color2", new Color(0,
                    0.01f, UnityEngine.Random.Range(0.45f, 0.85f), 0));
            }
            else if (colSwitch == 3)
            {
                galaxyReal.GetComponent<MeshRenderer>().material.SetColor("_Color1", new Color(UnityEngine.Random.Range(0.1f, 0.2f),
                    0, UnityEngine.Random.Range(0.45f, 0.85f), 0));
                galaxyReal.GetComponent<MeshRenderer>().material.SetColor("_Color2", new Color(UnityEngine.Random.Range(0.55f, 0.85f),
                    0, UnityEngine.Random.Range(0.45f, 0.65f), 0));
            }
            modifactaor = modifactaor * -1;
        }

        Lists.colorOfPlayer = getProperMatColorByIndex(Lists.playerColor);

        //this property is used to set a special color to station panel according to it's level
        //playerCruiserPanelMaskImg = playerCruiserPanelMask.GetComponent<Image>();
    }

    //to check if player still have any cruisers on scene
    public int countOfPlayerCruisers() {
        int x = 0;
        for (int i = 0; i < Lists.shipsOnScene.Count; i++) {
            if (!Lists.shipsOnScene[i].CompareTag(Cruis1CPUTag) && !Lists.shipsOnScene[i].CompareTag(Cruis2CPUTag) && !Lists.shipsOnScene[i].CompareTag(Cruis3CPUTag) && !Lists.shipsOnScene[i].CompareTag(Cruis4CPUTag))
            {
                x++;
            }
        }
        return x;
    }

    //this method is used to turn on the signal that the cruiser of players are active and attacking the stations
    //public void turnOnOffLaunchedCruiserIcon(int indexOfPlayer, bool on) {
    //    if (on) activeStationIcons[indexOfPlayer].transform.GetChild(0).gameObject.SetActive(true);
    //    else activeStationIcons[indexOfPlayer].transform.GetChild(0).gameObject.SetActive(false);
    //}

    //this method resets the counts of players stations icons on scene without changing the scene, so it is no called from the start method but from station 
    //controllers, CaptureLine controllers and from CPU journey ship controllers
    //third parameter of method is used only for CPU players, and equls to 1 for human player by default cause human player will be defeated by other functions
    //order of players indexes in scene is following 0-player, 1-CPU1, 2-CPU2 and so on
    public void resetTheCountsOfStationIconsWhileOnScene(int indexOfPlayer, int countsOfStations, int cruisersOnScene) {
        //getting the reference to a text field of station icon of player (any player of scene)
        activeStationIcons[indexOfPlayer].transform.GetChild(1).GetComponent<Text>().text = countsOfStations.ToString();
        //this block checks if CPU player lost the game and disactivates it's staitions icon from scene if true and also removes it from the dictionary
        if (countsOfStations < 1 && cruisersOnScene < 1) {
            activeStationIcons[indexOfPlayer].SetActive(false);
            //activeStationIcons.Remove(indexOfPlayer);
        }
        else if (!activeStationIcons[indexOfPlayer].activeInHierarchy) activeStationIcons[indexOfPlayer].SetActive(true);
    }

    public void changingTheEnergyOfPlayer(bool isIncrease, float changeAamount) {
        if (isIncrease)
        {
            //getting booster point depending on case so if 0 player gets the least booster points if 1 more and if 2 is most 
            Lists.energyOfPlayer += changeAamount;
            energyCount.text = Lists.energyOfPlayer.ToString("0");
            energyCount.color = Color.green; //making a energy count green while player gathers the energy

            gainSound.Play();
            energyCount.transform.parent.gameObject.GetComponent<Animator>().SetBool("tokenActive", true);
            Invoke("disactAnimAndColorsOfTokens", 2.5f);
        }
        else {
            Lists.energyOfPlayer -= changeAamount;
            if (Lists.energyOfPlayer <= 0) Lists.energyOfPlayer = 0;
            energyCount.text = Lists.energyOfPlayer.ToString("0");

            energyCount.color = Color.red;
            energyCount.transform.parent.gameObject.GetComponent<Animator>().SetBool("tokenActive", true);
            Invoke("disactAnimAndColorsOfTokens", 2.5f);
        }
    }

    //reset the text UI that shows the timer till the next CPU cruiser launch and repaint it depending on which CPU is closer to launch the next cruiser
    //public void resetTheTimer() {
    //    List<StationController> cruisLaunchingStations = new List<StationController>();
    //    //populating local method's collection to hold only the stations with 
    //    for (int i = 0; i < Lists.AllStations.Count; i++)
    //    {
    //        if (Lists.AllStations[i].CPUSceneCruiserLaunchCoroutineIsOn) cruisLaunchingStations.Add(Lists.AllStations[i]);
    //    }

    //    for (int i = 0; i < cruisLaunchingStations.Count; i++)
    //    {
    //        //the first station in collection is added as defauld and it's time to launche a cruiser is compared to following ones to determine which station is closest to launche cruiser next
    //        if (i == 0)
    //        {
    //            CruisLaunchingCPUStationToDisplay.Clear();
    //            CruisLaunchingCPUStationToDisplay.Add(cruisLaunchingStations[i]);
    //        }
    //        else if (CruisLaunchingCPUStationToDisplay[0].CPUSceneCruiserLaunchTimer > cruisLaunchingStations[i].CPUSceneCruiserLaunchTimer)
    //        {
    //            CruisLaunchingCPUStationToDisplay.Clear();
    //            CruisLaunchingCPUStationToDisplay.Add(cruisLaunchingStations[i]);
    //        }
    //    }

    //    //for (int i = 0; i < allCruisLaunchingCPUStations.Count;i++) {
    //    //    //the first station in collection is added as defauld and it's time to launche a cruiser is compared to following ones to determine which station is closest to launche cruiser next
    //    //    if (i == 0)
    //    //    {
    //    //        CruisLaunchingCPUStationToDisplay.Clear();
    //    //        CruisLaunchingCPUStationToDisplay.Add(allCruisLaunchingCPUStations[i]);
    //    //    }
    //    //    else if (CruisLaunchingCPUStationToDisplay[0].CPUSceneCruiserLaunchTimer > allCruisLaunchingCPUStations[i].CPUSceneCruiserLaunchTimer) {
    //    //        CruisLaunchingCPUStationToDisplay.Clear();
    //    //        CruisLaunchingCPUStationToDisplay.Add(allCruisLaunchingCPUStations[i]);
    //    //    }
    //    //}


    //    //at least this if statement will prevent the out of range bug in case that there will not be any stations on list that launches the cruisers
    //    if (CruisLaunchingCPUStationToDisplay.Count >0) {
    //        CPUTurnSlider.minValue = CruisLaunchingCPUStationToDisplay[0].CPUSceneCruiserLaunchTimer * -1;
    //        CPUTurnSlider.maxValue = 0;
    //        CPUSliderFillImg.color = getProperStatioUIColorByIndex(colorsOfPlayers[CruisLaunchingCPUStationToDisplay[0].CPUNumber]);
    //        CPUStationTimerIcon.color = getProperStatioUIColorByIndex(colorsOfPlayers[CruisLaunchingCPUStationToDisplay[0].CPUNumber]);
    //        CPUTurnTimerTitle.color = getProperStatioUIColorByIndex(colorsOfPlayers[CruisLaunchingCPUStationToDisplay[0].CPUNumber]);
    //    }
    //}

    //spawns asteroid according to level
    private void spawnTheAsteroids(int lvl) {
        if (lvl == 0)
        {
            asteroidCounts = 7;
            //instantiateing the asteroids on scene
            //for (int i = 0; i < asteroidCounts; i++)
            //{
            //    asteroidReal = Instantiate(LargeAsteroid, new Vector3 (UnityEngine.Random.Range(-55f, 55f), -8, UnityEngine.Random.Range(0, 2) > 0 ? 199f : -199f), UnityEngine.Random.rotation);
            //}
            //for (int i = 0; i < asteroidCounts; i++)
            //{
            //    asteroidReal = Instantiate(SmallAsteroid, new Vector3(UnityEngine.Random.Range(-55f, 55f), -8, UnityEngine.Random.Range(0, 2) > 0 ? 199f : -199f), UnityEngine.Random.rotation);
            //}
            //for (int i = 0; i < asteroidCounts; i++)
            //{
            //    asteroidReal = Instantiate(MiniAsteroid, new Vector3(UnityEngine.Random.Range(-35f, 35f), -8, UnityEngine.Random.Range(0, 2) > 0 ? 199f : -199f), UnityEngine.Random.rotation);
            //}
            //for (int i = 0; i < asteroidCounts; i++)
            //{
            //    asteroidReal = Instantiate(MicroAsteroid, new Vector3(UnityEngine.Random.Range(-35f, 35f), -8, UnityEngine.Random.Range(0, 2) > 0 ? 199f : -199f), UnityEngine.Random.rotation);
            //}
            for (int i = 0; i < asteroidCounts; i++)
            {
                asteroidReal = Instantiate(LargeAsteroid, new Vector3(UnityEngine.Random.Range(-35f, 35f), -8, UnityEngine.Random.Range(0, 2) > 0 ? 199f : -199f), UnityEngine.Random.rotation);
            }
            for (int i = 0; i < asteroidCounts; i++)
            {
                asteroidReal = Instantiate(SmallAsteroid, new Vector3(UnityEngine.Random.Range(-35f, 35f), -8, UnityEngine.Random.Range(0, 2) > 0 ? 199f : -199f), UnityEngine.Random.rotation);
            }
        }
        else if (lvl == 1) {

            asteroidCounts = 12;
            //for (int i = 0; i < asteroidCounts; i++)
            //{
            //    asteroidReal = Instantiate(LargeAsteroid, new Vector3 (UnityEngine.Random.Range(-55f, 55f), -8, UnityEngine.Random.Range(0, 2) > 0 ? 199f : -199f), UnityEngine.Random.rotation);
            //}
            for (int i = 0; i < asteroidCounts; i++)
            {
                asteroidReal = Instantiate(LargeAsteroid, new Vector3(UnityEngine.Random.Range(-35f, 35f), -8, UnityEngine.Random.Range(0, 2) > 0 ? 199f : -199f), UnityEngine.Random.rotation);
            }
            for (int i = 0; i < asteroidCounts; i++)
            {
                asteroidReal = Instantiate(SmallAsteroid, new Vector3(UnityEngine.Random.Range(-45f, 45f), -8, UnityEngine.Random.Range(0, 2) > 0 ? 199f : -199f), UnityEngine.Random.rotation);
            }
            //for (int i = 0; i < asteroidCounts; i++)
            //{
            //    asteroidReal = Instantiate(MiniAsteroid, new Vector3(UnityEngine.Random.Range(-45f, 45f), -8, UnityEngine.Random.Range(0, 2) > 0 ? 199f : -199f), UnityEngine.Random.rotation);
            //}
            //for (int i = 0; i < asteroidCounts; i++)
            //{
            //    asteroidReal = Instantiate(MicroAsteroid, new Vector3(UnityEngine.Random.Range(-45f, 45f), -8, UnityEngine.Random.Range(0, 2) > 0 ? 199f : -199f), UnityEngine.Random.rotation);
            //}
        }
        else if (lvl ==2)
        {
            asteroidCounts = 9;
            for (int i = 0; i < asteroidCounts; i++)
            {
                asteroidReal = Instantiate(LargeAsteroid, new Vector3(UnityEngine.Random.Range(-65f, 65f), -8, UnityEngine.Random.Range(0, 2) > 0 ? 199f : -199f), UnityEngine.Random.rotation);
            }
            for (int i = 0; i < asteroidCounts; i++)
            {
                asteroidReal = Instantiate(SmallAsteroid, new Vector3(UnityEngine.Random.Range(-65f, 65f), -8, UnityEngine.Random.Range(0, 2) > 0 ? 199f : -199f), UnityEngine.Random.rotation);
            }
            //for (int i = 0; i < asteroidCounts; i++)
            //{
            //    asteroidReal = Instantiate(MiniAsteroid, new Vector3(UnityEngine.Random.Range(-65f, 65f), -8, UnityEngine.Random.Range(0, 2) > 0 ? 199f : -199f), UnityEngine.Random.rotation);
            //}
            //for (int i = 0; i < asteroidCounts; i++)
            //{
            //    asteroidReal = Instantiate(MicroAsteroid, new Vector3(UnityEngine.Random.Range(-65f, 65f), -8, UnityEngine.Random.Range(0, 2) > 0 ? 199f : -199f), UnityEngine.Random.rotation);
            //}
        }
    }

    //this method controls the count of all player stations on scene including all CPU stations and player's stations
    //and as well it populates the dictionary that holds the reference to station icons on scene
    //the scheme is following, 0 index is always players index and the rests are the CPU's
    //order of players in scene is following 0-player, 1-CPU1, 2-CPU2 and so on
    public void journeySceneStationsIconsCtrlr() {

        int i = 0;
        //clearing the dictionary that holds the launched cruisers count on scene
        activeStationIcons.Clear();
        //so for player there always will be reserved a station icon on scene
        PlayerStationsCountTxts[i].text = Lists.playerStations.Count.ToString();
        PlayerStationsCountTxts[i].transform.parent.GetComponent<Image>().color = getProperStatioUIColorByIndex(Lists.playerColor);
        PlayerStationsCountTxts[i].transform.parent.gameObject.SetActive(true);
        activeStationIcons.Add(0, PlayerStationsCountTxts[i].transform.parent.gameObject);
        i++;

        if (Lists.CPU1Stations.Count > 0)
        {
            PlayerStationsCountTxts[i].text = Lists.CPU1Stations.Count.ToString();
            PlayerStationsCountTxts[i].transform.parent.GetComponent<Image>().color = getProperStatioUIColorByIndex(colorsOfPlayers[0]);
            PlayerStationsCountTxts[i].transform.parent.gameObject.SetActive(true);
            activeStationIcons.Add(1, PlayerStationsCountTxts[i].transform.parent.gameObject);
            activeStationIcons[1].transform.GetChild(0).GetComponent<RawImage>().color = getProperStatioUIColorByIndex(colorsOfPlayers[0]);
            i++;
        }
        //else PlayerStationsCountTxts[i].transform.parent.gameObject.SetActive(false);

        if (Lists.CPU2Stations.Count > 0)
        {
            PlayerStationsCountTxts[i].text = Lists.CPU2Stations.Count.ToString();
            PlayerStationsCountTxts[i].transform.parent.GetComponent<Image>().color = getProperStatioUIColorByIndex(colorsOfPlayers[1]);
            PlayerStationsCountTxts[i].transform.parent.gameObject.SetActive(true);
            activeStationIcons.Add(2, PlayerStationsCountTxts[i].transform.parent.gameObject);
            activeStationIcons[2].transform.GetChild(0).GetComponent<RawImage>().color = getProperStatioUIColorByIndex(colorsOfPlayers[1]);
            i++;
        }
        //else PlayerStationsCountTxts[i].transform.parent.gameObject.SetActive(false);

        if (Lists.CPU3Stations.Count > 0)
        {
            PlayerStationsCountTxts[i].text = Lists.CPU3Stations.Count.ToString();
            PlayerStationsCountTxts[i].transform.parent.GetComponent<Image>().color = getProperStatioUIColorByIndex(colorsOfPlayers[2]);
            PlayerStationsCountTxts[i].transform.parent.gameObject.SetActive(true);
            activeStationIcons.Add(3, PlayerStationsCountTxts[i].transform.parent.gameObject);
            activeStationIcons[3].transform.GetChild(0).GetComponent<RawImage>().color = getProperStatioUIColorByIndex(colorsOfPlayers[2]);
            i++;
        }
        //else PlayerStationsCountTxts[i].transform.parent.gameObject.SetActive(false);

        if (Lists.CPU4Stations.Count > 0)
        {
            PlayerStationsCountTxts[i].text = Lists.CPU4Stations.Count.ToString();
            PlayerStationsCountTxts[i].transform.parent.GetComponent<Image>().color = getProperStatioUIColorByIndex(colorsOfPlayers[3]);
            PlayerStationsCountTxts[i].transform.parent.gameObject.SetActive(true);
            activeStationIcons.Add(4, PlayerStationsCountTxts[i].transform.parent.gameObject);
            activeStationIcons[4].transform.GetChild(0).GetComponent<RawImage>().color = getProperStatioUIColorByIndex(colorsOfPlayers[3]);
        }
        //PlayerStationsCountTxts[i].transform.parent.gameObject.SetActive(false);
    }
    //this funt is used to set a proper color to the next CPU station UI while instantiating them
    //0-Red, 1-green, 2-blue, 3-yellow, 4-purple
    public Color getProperStatioUIColorByIndex(int colorIndex)
    {
        if (colorIndex == 0) return redColorOfStationUI;
        else if (colorIndex == 1) return greenColorOfStationUI;
        else if (colorIndex == 2) return blueColorOfStationUI;
        else if (colorIndex == 3) return yellowColorOfStationUI;
        else return purpleColorOfStationUI;
    }


    //this method is assigned to camera button on journey scene and changes the camera views on scene from action camera to high view camera
    public void changeViewCamera() {
        //virtualCamera.GetComponent<VirtualCamCtrlr>().changeTheCameraViewOnJourneyCruiser();
        if (mainCam.orthographic)
        {
            mainCam.orthographic = false;
            mainCam.transform.rotation = Quaternion.Euler(60, mainCam.transform.rotation.eulerAngles.y, mainCam.transform.rotation.eulerAngles.z);
            //cameraManager.isPerspective = true;
        }
        else
        {
            mainCam.orthographic = true;
            mainCam.transform.rotation = Quaternion.Euler(90, mainCam.transform.rotation.eulerAngles.y, 0);
            //cameraManager.isPerspective = false;
        }
    }

    //this method launches a stations of CPU and guards as well as empty stations on scene
    private void launchAGameStations(Dictionary<string, int> levelParams) {
        int stationPoointIndex = 0;
        int CPUNumber = 0;

        #region stations
        //instatiating the ready CPU stations on scene
        //TODO WITH assigning the proper CPU number
        for (int i = 0; i < levelParams["Station0"]; i++) {
            stationListToActivate = ObjectPullerJourney.current.GetStationAPullList();
            CPUStationReal = ObjectPullerJourney.current.GetUniversalBullet(stationListToActivate);
            CPUStationReal.transform.position = Constants.Instance.stationPoints[stationPoointIndex];
            CPUStationReal.transform.rotation = Quaternion.identity;
            stationPoointIndex++;

            //TODO WITH SETTINT THE CPU NUMBER ON STATION INSTEAD OF BOOL
            StationController sc = CPUStationReal.GetComponent<StationController>();
            sc.stationPosition = CPUStationReal.transform.position;
            sc.isCPUStation = true; //cause the CPU1 got the station on that if block
            sc.isPlayerStation = false; //cause the player got the station on that if block
            sc.isGuardStation = false; 
            //sc.stationProductionSwitchTrigger = false;
            sc.upgradeCounts = 0; //setting station corresponding upgrade counts
            sc.stationCurrentLevel = 0; //setting station corresponding upgrade counts
            sc.colorOfStationMat = getProperMatColorByIndex(colorsOfPlayers[CPUNumber]); //setting the next color available on colours list to this CPU station
            //sc.showInfoAboutThisObjectOnCanvas();
            CPUStationReal.SetActive(true);

            //starting the process of creating the fleet of station and creating the cruiser of station to give it an orders. The param of func gives the number to CPU
            //(each instance of StationController class is managed as some exact CPU station controller or player station)
            sc.startProcessesForCPU(CPUNumber);
            //sc.launchingACPUCruiserOnScene(); //starting a process of launching a new cruiser on scene


            //starting a launching a cruiser on scene process for the first station of CPU 
            //sc.launchingACPUCruiserOnScene();
            //StartCoroutine(sc.launchingACPUCruiserOnScene());

            if (CPUNumber==0) Lists.CPU1Stations.Add(sc);
            else if (CPUNumber == 1) Lists.CPU2Stations.Add(sc);
            else if (CPUNumber == 2) Lists.CPU3Stations.Add(sc);
            else if (CPUNumber == 3) Lists.CPU4Stations.Add(sc);
            Lists.AllStations.Add(sc);
            CPUNumber++;
        }
        for (int i = 0; i < levelParams["Station1"]; i++)
        {
            stationListToActivate = ObjectPullerJourney.current.GetStationAPullList();
            CPUStationReal = ObjectPullerJourney.current.GetUniversalBullet(stationListToActivate);
            CPUStationReal.transform.position = Constants.Instance.stationPoints[stationPoointIndex];
            CPUStationReal.transform.rotation = Quaternion.identity;
            stationPoointIndex++;

            //TODO WITH SETTINT THE CPU NUMBER ON STATION INSTEAD OF BOOL
            StationController sc = CPUStationReal.GetComponent<StationController>();
            sc.stationPosition = CPUStationReal.transform.position;
            sc.isCPUStation = true; //cause the CPU1 got the station on that if block
            sc.isPlayerStation = false; //cause the player got the station on that if block
            sc.isGuardStation = false;
            //sc.stationProductionSwitchTrigger = false;
            sc.upgradeCounts = 1; //setting station corresponding upgrade counts
            sc.stationCurrentLevel = 0; //setting station corresponding upgrade counts
            sc.colorOfStationMat = getProperMatColorByIndex(colorsOfPlayers[CPUNumber]); //setting the next color available on colours list to this CPU station
            //sc.showInfoAboutThisObjectOnCanvas();
            CPUStationReal.SetActive(true);

            sc.startProcessesForCPU(CPUNumber);

            //sc.launchingACPUCruiserOnScene(); //starting a process of launching a new cruiser on scene
            //starting a launching a cruiser on scene process for the first station of CPU 
            //sc.launchingACPUCruiserOnScene();
            //StartCoroutine(sc.launchingACPUCruiserOnScene());

            if (CPUNumber == 0) Lists.CPU1Stations.Add(sc);
            else if (CPUNumber == 1) Lists.CPU2Stations.Add(sc);
            else if (CPUNumber == 2) Lists.CPU3Stations.Add(sc);
            else if (CPUNumber == 3) Lists.CPU4Stations.Add(sc);
            Lists.AllStations.Add(sc);
            CPUNumber++;
        }
        for (int i = 0; i < levelParams["Station2"]; i++)
        {
            stationListToActivate = ObjectPullerJourney.current.GetStationAPullList();
            CPUStationReal = ObjectPullerJourney.current.GetUniversalBullet(stationListToActivate);
            CPUStationReal.transform.position = Constants.Instance.stationPoints[stationPoointIndex];
            CPUStationReal.transform.rotation = Quaternion.identity;
            stationPoointIndex++;

            //TODO WITH SETTINT THE CPU NUMBER ON STATION INSTEAD OF BOOL
            StationController sc = CPUStationReal.GetComponent<StationController>();
            sc.stationPosition = CPUStationReal.transform.position;
            sc.isCPUStation = true; //cause the CPU1 got the station on that if block
            sc.isPlayerStation = false; //cause the player got the station on that if block
            sc.isGuardStation = false;
            //sc.stationProductionSwitchTrigger = false;
            sc.upgradeCounts = 2; //setting station corresponding upgrade counts
            sc.stationCurrentLevel = 0; //setting station corresponding upgrade counts
            sc.colorOfStationMat = getProperMatColorByIndex(colorsOfPlayers[CPUNumber]); //setting the next color available on colours list to this CPU station
            //sc.showInfoAboutThisObjectOnCanvas();
            CPUStationReal.SetActive(true);

            sc.startProcessesForCPU(CPUNumber);
            //sc.launchingACPUCruiserOnScene(); //starting a process of launching a new cruiser on scene
            //starting a launching a cruiser on scene process for the first station of CPU 
            //sc.launchingACPUCruiserOnScene();
            //StartCoroutine(sc.launchingACPUCruiserOnScene());

            if (CPUNumber == 0) Lists.CPU1Stations.Add(sc);
            else if (CPUNumber == 1) Lists.CPU2Stations.Add(sc);
            else if (CPUNumber == 2) Lists.CPU3Stations.Add(sc);
            else if (CPUNumber == 3) Lists.CPU4Stations.Add(sc);
            Lists.AllStations.Add(sc); 
            CPUNumber++;
        }
        for (int i = 0; i < levelParams["Station3"]; i++)
        {
            stationListToActivate = ObjectPullerJourney.current.GetStationAPullList();
            CPUStationReal = ObjectPullerJourney.current.GetUniversalBullet(stationListToActivate);
            CPUStationReal.transform.position = Constants.Instance.stationPoints[stationPoointIndex];
            CPUStationReal.transform.rotation = Quaternion.identity;
            stationPoointIndex++;

            //TODO WITH SETTINT THE CPU NUMBER ON STATION INSTEAD OF BOOL
            StationController sc = CPUStationReal.GetComponent<StationController>();
            sc.stationPosition = CPUStationReal.transform.position;
            sc.isCPUStation = true; //cause the CPU1 got the station on that if block
            sc.isPlayerStation = false; //cause the player got the station on that if block
            sc.isGuardStation = false;
            //sc.stationProductionSwitchTrigger = false;
            sc.upgradeCounts = 3; //setting station corresponding upgrade counts
            sc.stationCurrentLevel = 0; //setting station corresponding upgrade counts
            sc.colorOfStationMat = getProperMatColorByIndex(colorsOfPlayers[CPUNumber]); //setting the next color available on colours list to this CPU station
            //sc.showInfoAboutThisObjectOnCanvas();
            CPUStationReal.SetActive(true);

            //sc.showInfoAboutThisObjectOnCanvas();
            sc.startProcessesForCPU(CPUNumber);
            //sc.launchingACPUCruiserOnScene(); //starting a process of launching a new cruiser on scene
            //starting a launching a cruiser on scene process for the first station of CPU 
            //sc.launchingACPUCruiserOnScene();
            //StartCoroutine(sc.launchingACPUCruiserOnScene());

            if (CPUNumber == 0) Lists.CPU1Stations.Add(sc);
            else if (CPUNumber == 1) Lists.CPU2Stations.Add(sc);
            else if (CPUNumber == 2) Lists.CPU3Stations.Add(sc);
            else if (CPUNumber == 3) Lists.CPU4Stations.Add(sc);
            Lists.AllStations.Add(sc);
            CPUNumber++;
        }

        for (int i = 0; i < Lists.AllStations.Count; i++) {
            Lists.AllStations[i].createStationsEnergon();
        }
        //this method assigns proper color to scene icons that display the count of players stations 
        journeySceneStationsIconsCtrlr();

        #endregion stations

        #region empty
        //instatiating the empty stations on scene
        for (int i = 0; i < levelParams["Empty0"]; i++)
        {
            emptyStationsListToActivate = ObjectPullerJourney.current.GetEmptyStation0PullList();
            emptyStation = ObjectPullerJourney.current.GetUniversalBullet(emptyStationsListToActivate);
            emptyStation.transform.position = Constants.Instance.stationPoints[stationPoointIndex];
            emptyStation.transform.rotation = Quaternion.identity;
            emptyStation.SetActive(true);
            Lists.emptyStations.Add(emptyStation.GetComponentInChildren<CaptureLine>());
            stationPoointIndex++;
        }
        for (int i = 0; i < levelParams["Empty1"]; i++)
        {
            emptyStationsListToActivate = ObjectPullerJourney.current.GetEmptyStation1PullList();
            emptyStation = ObjectPullerJourney.current.GetUniversalBullet(emptyStationsListToActivate);
            emptyStation.transform.position = Constants.Instance.stationPoints[stationPoointIndex];
            emptyStation.transform.rotation = Quaternion.identity;
            emptyStation.SetActive(true);
            Lists.emptyStations.Add(emptyStation.GetComponentInChildren<CaptureLine>());
            stationPoointIndex++;
        }
        for (int i = 0; i < levelParams["Empty2"]; i++)
        {
            emptyStationsListToActivate = ObjectPullerJourney.current.GetEmptyStation2PullList();
            emptyStation = ObjectPullerJourney.current.GetUniversalBullet(emptyStationsListToActivate);
            emptyStation.transform.position = Constants.Instance.stationPoints[stationPoointIndex];
            emptyStation.transform.rotation = Quaternion.identity;
            emptyStation.SetActive(true);
            Lists.emptyStations.Add(emptyStation.GetComponentInChildren<CaptureLine>());
            stationPoointIndex++;
        }
        for (int i = 0; i < levelParams["Empty3"]; i++)
        {
            emptyStationsListToActivate = ObjectPullerJourney.current.GetEmptyStation3PullList();
            emptyStation = ObjectPullerJourney.current.GetUniversalBullet(emptyStationsListToActivate);
            emptyStation.transform.position = Constants.Instance.stationPoints[stationPoointIndex];
            emptyStation.transform.rotation = Quaternion.identity;
            emptyStation.SetActive(true);
            Lists.emptyStations.Add(emptyStation.GetComponentInChildren<CaptureLine>());
            stationPoointIndex++;
        }

        #endregion empty

        //instatiating the guard station on scene
        for (int i = 0; i < levelParams["GuardStation"]; i++)
        {
            stationListToActivate = ObjectPullerJourney.current.GetStationEPullList();
            CPUStationReal = ObjectPullerJourney.current.GetUniversalBullet(stationListToActivate);
            CPUStationReal.transform.position = new Vector3 (0,-8,0);
            CPUStationReal.transform.rotation = Quaternion.identity;

            //TODO WITH SETTINT THE CPU NUMBER ON STATION INSTEAD OF BOOL
            StationController sc = CPUStationReal.GetComponent<StationController>();
            sc.stationPosition = CPUStationReal.transform.position;
            sc.isCPUStation = false; //cause the CPU1 got the station on that if block
            sc.isPlayerStation = false; //cause the player got the station on that if block
            sc.isGuardStation = true;
            sc.isGuardCoreStation = true;
            //sc.stationProductionSwitchTrigger = false;
            CPUStationReal.SetActive(true);

            //guard station fleet is set only once on instantiation
            sc.setGuardStationFleetByLvl();
            sc.GuardStationLaserShotTimerSet(); 

            Lists.CPUGuardStations.Add(sc);
            Lists.AllStations.Add(sc);

            //this features is used to display the guard attack time on timer, so the guard timer will be activated only in case if the level with guard station
            if (Lists.currentLevel == 6 || Lists.currentLevel == 7 || Lists.currentLevel == 9 || Lists.currentLevel == 10)
            {
                Lists.guardCoreStation = sc;
                GuardAttackTimerTitle.gameObject.SetActive(true);
            }
        }
    }


    private void launchAGameShips(Dictionary<string, int> levelParams)
    {
        //setting the energon ships on scene depending on current level with setting the level corresponding features for ships (speed and rotation lerp)
        //TODO WITH OTER LEVELS AFTER LEVEL DESIGNING
        //for (int i = 0; i < levelParams["Energon0"]; i++)
        //{
        //    energonShipReal = Instantiate(energonShip, new Vector3(UnityEngine.Random.Range(100f, -100f), -8, UnityEngine.Random.Range(100f, -100f)), Quaternion.identity);

        //    EnergonMngr energonMngr = energonShipReal.GetComponent<EnergonMngr>();
        //    //EnergonShotMngr energonShotMngr = energonShipReal.GetComponentInChildren<EnergonShotMngr>();

        //    //energonShotMngr.shotSpeed = Constants.Instance.energonShotSpeed4;
        //    //energonShotMngr.shotBullSpeed = Constants.Instance.energonShotBullSpeed4;
        //    //energonShotMngr.shotCounts = 1;
        //    energonMngr.energonAndGuardLvl = 0;
        //    //energonMngr.energonLife = 3;
        //    //energonMngr.energonLifeStart = energonMngr.energonLife;
        //    energonMngr.setTheFleetAndResourcesOfEnergon();
        //    energonMngr.nextRotatioLerp = Constants.Instance.energonRotationLerp4;
        //    energonMngr.nextMovingSpeed = Constants.Instance.energonMovingSpeed4;
        //    //aimingObjects.Add(energonShipReal.transform);
        //    Lists.energonsOnScene.Add(energonShipReal);
        //}

        ////TODO with painting different level energons to different colors
        //for (int i = 0; i < levelParams["Energon1"]; i++)
        //{
        //    energonShipReal = Instantiate(energonShip, new Vector3(UnityEngine.Random.Range(100f, -100f), -8, UnityEngine.Random.Range(100f, -100f)), Quaternion.identity);

        //    EnergonMngr energonMngr = energonShipReal.GetComponent<EnergonMngr>();
        //    //EnergonShotMngr energonShotMngr = energonShipReal.GetComponentInChildren<EnergonShotMngr>();

        //    //energonShotMngr.shotSpeed = Constants.Instance.energonShotSpeed3;
        //    //energonShotMngr.shotBullSpeed = Constants.Instance.energonShotBullSpeed3;
        //    //energonShotMngr.shotCounts = 1;
        //    energonMngr.energonAndGuardLvl = 1;
        //    energonMngr.setTheFleetAndResourcesOfEnergon();
        //    //energonMngr.energonLife = 4;
        //    //energonMngr.energonLifeStart = energonMngr.energonLife;
        //    energonMngr.nextRotatioLerp = Constants.Instance.energonRotationLerp3;
        //    energonMngr.nextMovingSpeed = Constants.Instance.energonMovingSpeed3;
        //    //aimingObjects.Add(energonShipReal.transform);
        //    Lists.energonsOnScene.Add(energonShipReal);
        //}
        //for (int i = 0; i < levelParams["Energon2"]; i++)
        //{
        //    energonShipReal = Instantiate(energonShip, new Vector3(UnityEngine.Random.Range(100f, -100f), -8, UnityEngine.Random.Range(100f, -100f)), Quaternion.identity);

        //    EnergonMngr energonMngr = energonShipReal.GetComponent<EnergonMngr>();
        //    //EnergonShotMngr energonShotMngr = energonShipReal.GetComponentInChildren<EnergonShotMngr>();

        //    //energonShotMngr.shotSpeed = Constants.Instance.energonShotSpeed2;
        //    //energonShotMngr.shotBullSpeed = Constants.Instance.energonShotBullSpeed2;
        //    //energonShotMngr.shotCounts = 2;
        //    energonMngr.energonAndGuardLvl = 2;
        //    energonMngr.setTheFleetAndResourcesOfEnergon();
        //    //energonMngr.energonLife = 5;
        //    //energonMngr.energonLifeStart = energonMngr.energonLife;
        //    energonMngr.nextRotatioLerp = Constants.Instance.energonRotationLerp2;
        //    energonMngr.nextMovingSpeed = Constants.Instance.energonMovingSpeed2;
        //    //aimingObjects.Add(energonShipReal.transform);
        //    Lists.energonsOnScene.Add(energonShipReal);
        //}
        //for (int i = 0; i < levelParams["Energon3"]; i++)
        //{
        //    energonShipReal = Instantiate(energonShip, new Vector3(UnityEngine.Random.Range(100f, -100f), -8, UnityEngine.Random.Range(100f, -100f)), Quaternion.identity);

        //    EnergonMngr energonMngr = energonShipReal.GetComponent<EnergonMngr>();
        //    //EnergonShotMngr energonShotMngr = energonShipReal.GetComponentInChildren<EnergonShotMngr>();

        //    //energonShotMngr.shotSpeed = Constants.Instance.energonShotSpeed1;
        //    //energonShotMngr.shotBullSpeed = Constants.Instance.energonShotBullSpeed1;
        //    //energonShotMngr.shotCounts = 3;
        //    energonMngr.energonAndGuardLvl = 3;
        //    energonMngr.setTheFleetAndResourcesOfEnergon();
        //    //energonMngr.energonLife = 6;
        //    //energonMngr.energonLifeStart = energonMngr.energonLife;
        //    energonMngr.nextRotatioLerp = Constants.Instance.energonRotationLerp1;
        //    energonMngr.nextMovingSpeed = Constants.Instance.energonMovingSpeed1;
        //    //aimingObjects.Add(energonShipReal.transform);
        //    Lists.energonsOnScene.Add(energonShipReal);
        //}

        //setting the guard ships on scene depending on current level with setting the level corresponding features for ships (speed and rotation lerp)
        //TODO WITH OTER LEVELS AFTER LEVEL DESIGNING
        for (int i = 0; i < levelParams["Guard0"]; i++)
        {
            guardShipReal = Instantiate(guardShip, new Vector3(UnityEngine.Random.Range(100f, -100f), -8, UnityEngine.Random.Range(100f, -100f)), Quaternion.identity);

            GuardEnergyGather guardEnrgyGatherMngr = guardShipReal.GetComponentInChildren<GuardEnergyGather>();
            EnergonMngr energonMngr = guardShipReal.GetComponent<EnergonMngr>();

            energonMngr.energonAndGuardLvl = 0;
            energonMngr.setTheFleetAndResourcesOfGuard();
            //guardShipReal.GetComponentInChildren<GuardAttack>().setTheFleetOfGuard(0); //setting the fleet f guard ship accordin to alevel of guard
            guardEnrgyGatherMngr.energyChageRate = Constants.Instance.energyReduce0;
            energonMngr.nextRotatioLerp = Constants.Instance.energonRotationLerp4;
            energonMngr.nextMovingSpeed = Constants.Instance.guardMovingSpeed4;
            //energonMngr.playerShip = CruisJourneyReal;
            //energonMngr.invokeChaceTime = Constants.Instance.guardChaseInvokeTime4;
            energonMngr.guardChaseTimeMiddle = Constants.Instance.guardChaseTimeMiddle4;
            //energonMngr.energonLife = 3;
            //energonMngr.energonLifeStart = energonMngr.energonLife;
            energonMngr.guardTranslateModif = Constants.Instance.guardChaseSpeed4; //speed of chasing for guard according to it's level
            Lists.energonsOnScene.Add(guardShipReal);
            //aimingObjects.Add(guardShipReal.transform);
        }

        //TODO with painting different level energons to dofferent colors
        for (int i = 0; i < levelParams["Guard1"]; i++)
        {
            guardShipReal = Instantiate(guardShip, new Vector3(UnityEngine.Random.Range(100f, -100f), -8, UnityEngine.Random.Range(100f, -100f)), Quaternion.identity);

            GuardEnergyGather guardEnrgyGatherMngr = guardShipReal.GetComponentInChildren<GuardEnergyGather>();
            EnergonMngr energonMngr = guardShipReal.GetComponent<EnergonMngr>();

            energonMngr.energonAndGuardLvl = 1;
            energonMngr.setTheFleetAndResourcesOfGuard();
            //guardShipReal.GetComponentInChildren<GuardAttack>().setTheFleetOfGuard(1); //setting the fleet f guard ship accordin to alevel of guard
            guardEnrgyGatherMngr.energyChageRate = Constants.Instance.energyReduce1;
            energonMngr.nextRotatioLerp = Constants.Instance.energonRotationLerp3;
            energonMngr.nextMovingSpeed = Constants.Instance.guardMovingSpeed3;
            //energonMngr.playerShip = CruisJourneyReal;
            //energonMngr.invokeChaceTime = Constants.Instance.guardChaseInvokeTime3;
            energonMngr.guardChaseTimeMiddle = Constants.Instance.guardChaseTimeMiddle3;
            //energonMngr.energonLife = 4;
            //energonMngr.energonLifeStart = energonMngr.energonLife;
            energonMngr.guardTranslateModif = Constants.Instance.guardChaseSpeed3; //speed of chasing for guard according to it's level
            Lists.energonsOnScene.Add(guardShipReal);
            //aimingObjects.Add(guardShipReal.transform);
        }
        for (int i = 0; i < levelParams["Guard2"]; i++)
        {
            guardShipReal = Instantiate(guardShip, new Vector3(UnityEngine.Random.Range(100f, -100f), -8, UnityEngine.Random.Range(100f, -100f)), Quaternion.identity);

            GuardEnergyGather guardEnrgyGatherMngr = guardShipReal.GetComponentInChildren<GuardEnergyGather>();
            EnergonMngr energonMngr = guardShipReal.GetComponent<EnergonMngr>();

            energonMngr.energonAndGuardLvl = 2;
            energonMngr.setTheFleetAndResourcesOfGuard();
            //guardShipReal.GetComponentInChildren<GuardAttack>().setTheFleetOfGuard(2); //setting the fleet f guard ship accordin to alevel of guard
            guardEnrgyGatherMngr.energyChageRate = Constants.Instance.energyReduce2;
            energonMngr.nextRotatioLerp = Constants.Instance.energonRotationLerp2;
            energonMngr.nextMovingSpeed = Constants.Instance.guardMovingSpeed2;
            //energonMngr.playerShip = CruisJourneyReal;
            //energonMngr.invokeChaceTime = Constants.Instance.guardChaseInvokeTime2;
            energonMngr.guardChaseTimeMiddle = Constants.Instance.guardChaseTimeMiddle2;

            //energonMngr.energonLife = 5;
            //energonMngr.energonLifeStart = energonMngr.energonLife;
            energonMngr.guardTranslateModif = Constants.Instance.guardChaseSpeed2; //speed of chasing for guard according to it's level
            Lists.energonsOnScene.Add(guardShipReal);
            //aimingObjects.Add(guardShipReal.transform);
        }
        for (int i = 0; i < levelParams["Guard3"]; i++)
        {
            guardShipReal = Instantiate(guardShip, new Vector3(UnityEngine.Random.Range(100f, -100f), -8, UnityEngine.Random.Range(100f, -100f)), Quaternion.identity);

            GuardEnergyGather guardEnrgyGatherMngr = guardShipReal.GetComponentInChildren<GuardEnergyGather>();
            EnergonMngr energonMngr = guardShipReal.GetComponent<EnergonMngr>();

            energonMngr.energonAndGuardLvl = 3;
            energonMngr.setTheFleetAndResourcesOfGuard();
            //guardShipReal.GetComponentInChildren<GuardAttack>().setTheFleetOfGuard(3); //setting the fleet f guard ship accordin to alevel of guard
            guardEnrgyGatherMngr.energyChageRate = Constants.Instance.energyReduce3;
            energonMngr.nextRotatioLerp = Constants.Instance.energonRotationLerp1;
            energonMngr.nextMovingSpeed = Constants.Instance.guardMovingSpeed1;
            //energonMngr.energonLife =6;
            //energonMngr.energonLifeStart = energonMngr.energonLife;
            //energonMngr.playerShip = CruisJourneyReal;
            //energonMngr.invokeChaceTime = Constants.Instance.guardChaseInvokeTime1;
            energonMngr.guardChaseTimeMiddle = Constants.Instance.guardChaseTimeMiddle1;
            energonMngr.guardTranslateModif = Constants.Instance.guardChaseSpeed1; //speed of chasing for guard according to it's level
            Lists.energonsOnScene.Add(guardShipReal);
            //aimingObjects.Add(guardShipReal.transform);
        }

    }


    //this funt is used to set a proper material color to the next CPU station while instantiating them (colors for stations sphere and for CPU cruisers shperes)
    //0-Red, 1-green, 2-blue, 3-yellow, 4-purple
    public Color getProperMatColorByIndex(int colorIndex)
    {
        if (colorIndex == 0) return redColorOfStation;
        else if (colorIndex == 1) return greenColorOfStation;
        else if (colorIndex == 2) return blueColorOfStation;
        else if (colorIndex == 3) return yellowColorOfStation;
        else return purpleColorOfStation;
    }


    //this method is callsed from start method after getting back to journey scene from the battle of player and CPU one of which is station
    public void updateTheWinnerStationFleetAfterBattle(StationController sc, bool win) {
        bool isPlayerStationLocal = sc.isPlayerStation;
        if (sc.isPlayerStation)
        {
            //getting a reference to CPU cruis instance to set it's win gains
            CPUShipCtrlJourney CPUCruisInstance = Lists.CPUShipOnAttack.GetComponent<CPUShipCtrlJourney>();
            if (win)
            {
                sc.Cruis1 -= Lists.C1Lose;
                sc.Cruis2 -= Lists.C2Lose;
                sc.Cruis3 -= Lists.C3Lose;
                sc.Cruis4 -= Lists.C4Lose;
                sc.Destr1 -= Lists.D1Lose;
                sc.Destr1Par -= Lists.D1PLose;
                sc.Destr2 -= Lists.D2Lose;
                sc.Destr2Par -= Lists.D2PLose;
                sc.Destr3 -= Lists.D3Lose;
                sc.Destr4 -= Lists.D4Lose;
                sc.Gun1 -= Lists.G1Lose;
                sc.Gun2 -= Lists.G2Lose;
                sc.Gun3 -= Lists.G3Lose;
                sc.MiniGun -= Lists.MiniGunLose;
                sc.Fighter = 0;
                sc.DestrG = 0;
                sc.CruisG = 0;

                ////make the UI gain effect work after getting back from battle since player station won
                //if (Lists.isBlackDimension) Lists.energyOfPlayer += Constants.Instance.enrgyGainLoseDark;
                //else if (Lists.isBlueDimension) Lists.energyOfPlayer += Constants.Instance.enrgyGainLoseBlue;
                //else if (Lists.isRedDimension) Lists.energyOfPlayer += Constants.Instance.enrgyGainLoseRed;
                //energyCount.text = Lists.energyOfPlayer.ToString("0");
                //energyCount.color = Color.green;

                //if (Lists.isBlackDimension) Lists.boosterOfPlayer += Constants.Instance.boosterGainLoseDark;
                //else if (Lists.isBlueDimension) Lists.boosterOfPlayer += Constants.Instance.boosterGainLoseBlue;
                //else if (Lists.isRedDimension) Lists.boosterOfPlayer += Constants.Instance.boosterGainLoseRed;
                //boosterCount.text = Lists.boosterOfPlayer.ToString("0");
                //boosterCount.color = Color.green;

                //energyCount.transform.parent.gameObject.GetComponent<Animator>().SetBool("tokenActive", true);
                CPUCruisInstance.launchANewCruiser(); //starting a process of launching a proper scene cruiser on chosen station (from method)

                CPUCruisInstance.disactivatingCurrentShipNoBurst();

                //Destroy(CPUCruisInstance.gameObject); //destroying the incatance of CPU cruiser is necessary cause it was saved by DontDestry func while switching the scene

                //TODO WITH OTHER CPU classes
                //if (Lists.shipOnAttack.GetComponent<CPUShipCtrlJourney>())
                //{
                //    Lists.clearTheFleetOfCPU1AfterDefeat();
                //    //reducing the energy of CPU1 after getting back from battle since it lost the battle
                //    if (Lists.isBlackDimension) Lists.energyOfCPU1 -= Lists.enrgyGainLoseDark;
                //    else if (Lists.isBlueDimension) Lists.energyOfCPU1 -= Lists.enrgyGainLoseBlue;
                //    else if (Lists.isRedDimension) Lists.energyOfCPU1 -= Lists.enrgyGainLoseRed;
                //    if (Lists.energyOfCPU1 < 0) Lists.energyOfCPU1 = 0;
                //    changeCPU1Cruiser(); //this method changes the journey cruiser of CPU cause it lost all it's fleet after defeat
                //}

                //invoking the method that disactivates the bool value of screen tokens animations and paint them to white color
                //Invoke("disactAnimAndColorsOfTokens", 2.5f);
            }
            else
            {
                ////make the UI lose effect work after getting back from battle since player station lost
                //if (Lists.isBlackDimension) Lists.energyOfPlayer -= Constants.Instance.enrgyGainLoseDark;
                //else if (Lists.isBlueDimension) Lists.energyOfPlayer -= Constants.Instance.enrgyGainLoseBlue;
                //else if (Lists.isRedDimension) Lists.energyOfPlayer -= Constants.Instance.enrgyGainLoseRed;

                //if (Lists.energyOfPlayer < 0) Lists.energyOfPlayer = 0;

                //energyCount.text = Lists.energyOfPlayer.ToString("0");
                ////energyCount.transform.parent.gameObject.GetComponent<Animator>().SetBool("tokenActive", true);
                //energyCount.color = Color.red;


                //if (Lists.isBlackDimension) Lists.boosterOfPlayer -= Constants.Instance.boosterGainLoseDark;
                //else if (Lists.isBlueDimension) Lists.boosterOfPlayer -= Constants.Instance.boosterGainLoseBlue;
                //else if (Lists.isRedDimension) Lists.boosterOfPlayer -= Constants.Instance.boosterGainLoseRed;
                //if (Lists.boosterOfPlayer < 0) Lists.boosterOfPlayer = 0;
                //boosterCount.text = Lists.boosterOfPlayer.ToString("0");
                //boosterCount.color = Color.red;


                sc.clearAllPruductionBeforeUpgradeCPU(); //clearing the station before making it current CPU station
                sc.isUpgrading = false;
                sc.upgradeFill = 0;
                sc.isCPUStation = true; //cause the CPU got the station on that if block
                sc.isPlayerStation = false; //cause the player got the station on that if block
                sc.CPUNumber = CPUCruisInstance.CPUNumber;
                //sc.stationProductionSwitchTrigger = false;

                //setting the winner color to conquered station
                sc.colorOfStationMat = getProperMatColorByIndex(colorsOfPlayers[CPUCruisInstance.CPUNumber]);

                //so if met station is not the CPU's it will be removed from other players stations list 
                Lists.playerStations.Remove(sc);

                //adding this station to proper CPU's stations lists and making a according settings to station
                if (CPUCruisInstance.CPUNumber == 0)
                {
                    Lists.CPU1Stations.Add(sc);
                    //third param is cruisers count does not matter any more
                    resetTheCountsOfStationIconsWhileOnScene(1, Lists.CPU1Stations.Count, 0);
                }
                else if (CPUCruisInstance.CPUNumber == 1)
                {
                    Lists.CPU2Stations.Add(sc);
                    resetTheCountsOfStationIconsWhileOnScene(2, Lists.CPU2Stations.Count, 0);
                }
                else if (CPUCruisInstance.CPUNumber == 2)
                {
                    Lists.CPU3Stations.Add(sc);
                    resetTheCountsOfStationIconsWhileOnScene(3, Lists.CPU3Stations.Count, 0);
                }
                else if (CPUCruisInstance.CPUNumber == 3)
                {
                    Lists.CPU4Stations.Add(sc);
                    resetTheCountsOfStationIconsWhileOnScene(4, Lists.CPU4Stations.Count, 0);
                }

                sc.Cruis1 = CPUCruisInstance.Cruis1;
                sc.Cruis2 = CPUCruisInstance.Cruis2;
                sc.Cruis3 = CPUCruisInstance.Cruis3;
                sc.Cruis4 = CPUCruisInstance.Cruis4;
                sc.Destr1 = CPUCruisInstance.Destr1;
                sc.Destr2 = CPUCruisInstance.Destr2;
                sc.Destr1Par = CPUCruisInstance.Destr1Par;
                sc.Destr2Par = CPUCruisInstance.Destr2Par;
                sc.Destr3 = CPUCruisInstance.Destr3;
                sc.Destr4 = CPUCruisInstance.Destr4;
                sc.Gun1 = CPUCruisInstance.Gun1;
                sc.Gun2 = CPUCruisInstance.Gun2;
                sc.Gun3 = CPUCruisInstance.Gun3;
                sc.Fighter = CPUCruisInstance.Fighter;
                sc.MiniGun = CPUCruisInstance.MiniGun;


                sc.stationsEnergon.makeEnergonCPUs(CPUCruisInstance.CPUNumber);

                //starting the process of creating the fleet of station and creating the cruiser of station to give it an orders. The param of func gives the number to CPU
                //(each instance of StationController class is managed as some exact CPU station controller or player station)
                sc.startProcessesForCPUFromEmptyStation();
                sc.disactivateInfoAboutShip();
                SelectingBox.Instance.selectableStations.Add(sc);
                SelectingBox.Instance.selectableStationsPlayer.Remove(sc);

                CPUCruisInstance.launchANewCruiser(); //starting a process of launching a proper scene cruiser on chosen station (from method)
                CPUCruisInstance.disactivatingCurrentShipNoBurst();

                //updating the information about human player icons on scene cause player lost it's station
                //0 is permanent index of player and 1 is need to show that player does not lost it's cruiser, cause it is impossible
                resetTheCountsOfStationIconsWhileOnScene(0, Lists.playerStations.Count, 1);

                if (Lists.isBlackDimension) sc.energyOfStation = UnityEngine.Random.Range(Constants.Instance.energyOfStationDark - 30, Constants.Instance.energyOfStationDark + 30);
                else if (Lists.isBlueDimension) sc.energyOfStation = UnityEngine.Random.Range(Constants.Instance.energyOfStationBlue - 30, Constants.Instance.energyOfStationBlue + 30);
                else if (Lists.isRedDimension) sc.energyOfStation = UnityEngine.Random.Range(Constants.Instance.energyOfStationRed - 30, Constants.Instance.energyOfStationRed + 30);

                //condition to check if player lost the game 
                if (!YouWinLoseButton.activeInHierarchy && Lists.playerStations.Count < 1 && countOfPlayerCruisers() < 1 && (Lists.emptyStations.Count < 1 || (Lists.emptyStations.Count > 0 && Lists.energyOfPlayer < 120)))
                {
                    youLoseTheGameFunction();
                }
            }
        }
        else
        {
            LaunchingObjcts playerCruiserObject = Lists.shipOnAttack.GetComponent<LaunchingObjcts>();
            if (win)
            {
                sc.Cruis1 = 0;
                sc.Cruis2 = 0;
                sc.Cruis3 = 0;
                sc.Cruis4 = 0;
                sc.CruisG = 0;
                sc.Destr1 = 0;
                sc.Destr1Par = 0;
                sc.Destr2 = 0;
                sc.Destr2Par = 0;
                sc.Destr3 = 0;
                sc.Destr4 = 0;
                sc.DestrG = 0;
                sc.Gun1 = 0;
                sc.Gun2 = 0;
                sc.Gun3 = 0;
                sc.MiniGun = 0;
                sc.Fighter = 0;



                ////updating the fleet of player cruis since it won cause CPU station was defeated
                //Lists.UpdateFleetOfPlayerCruis();

                ////make the UI gain effect work after getting back fro battle since player cruiser won
                //if (Lists.isBlackDimension) Lists.energyOfPlayer += Constants.Instance.enrgyGainLoseDark;
                //else if (Lists.isBlueDimension) Lists.energyOfPlayer += Constants.Instance.enrgyGainLoseBlue;
                //else if (Lists.isRedDimension) Lists.energyOfPlayer += Constants.Instance.enrgyGainLoseRed;
                //energyCount.text = Lists.energyOfPlayer.ToString("0");
                ////energyCount.transform.parent.gameObject.GetComponent<Animator>().SetBool("tokenActive", true);
                //energyCount.color = Color.green;


                //if (Lists.isBlackDimension) Lists.boosterOfPlayer += Constants.Instance.boosterGainLoseDark;
                //else if (Lists.isBlueDimension) Lists.boosterOfPlayer += Constants.Instance.boosterGainLoseBlue;
                //else if (Lists.isRedDimension) Lists.boosterOfPlayer += Constants.Instance.boosterGainLoseRed;
                //boosterCount.text = Lists.boosterOfPlayer.ToString("0");
                //boosterCount.color = Color.green;

                //setting station features to be managable by player
                sc.clearAllPruductionBeforeUpgradeCPU(); //clearing the station before making it current CPU station
                sc.StopAllCoroutines(); //stopping all CPU station processes
                sc.CancelInvoke(); // stop any automatic processes inherited from CPU station
                //sc.stationProductionSwitchTrigger = false;

                //setting a color of player to set a proper colored material to round station sphere. Only if it is not guard core station
                if (!sc.isGuardCoreStation)
                {
                    sc.colorOfStationMat = getProperMatColorByIndex(Lists.playerColor);
                    sc.setProperStationColor();
                }

                //setting a production times and upgrade features to new player station if it is not guard core station.
                //otherwise, if player conquered the station of guard there is set the station level according to the dimension
                if (!sc.isGuardCoreStation)
                {
                    if (sc.stationCurrentLevel == 0)
                    {
                        //setting start next upgrade energy requirements and next upgrade time, which is from 0 to 1 upgrade
                        if (sc.upgradeCounts > 0)
                        {
                            sc.nextUpgradeEnergyCount = Constants.Instance.enrgy0to1Upgrd;
                        }
                        //setting some start features of 0 level station before activating it
                        //sc.Cruis4ProductTime = Constants.Instance.Cruis4ProductTimeStation0;
                        //sc.Destr4ProductTime = Constants.Instance.Destr4ProductTimeStation0;
                        //sc.EnergyProductTime = Constants.Instance.EnergyProductTimeStation0;
                    }
                    else if (sc.stationCurrentLevel == 1)
                    {
                        sc.nextUpgradeEnergyCount = Constants.Instance.enrgy1to2Upgrd;
                        sc.currentUpgradeTime = Constants.Instance.time1to2Upgrd;

                        //setting some start features of 0 level station before activating it
                        //sc.Cruis4ProductTime = Constants.Instance.Cruis4ProductTimeStation1;
                        //sc.Cruis3ProductTime = Constants.Instance.Cruis3ProductTimeStation1;
                        //sc.Destr4ProductTime = Constants.Instance.Destr4ProductTimeStation1;
                        //sc.Destr3ProductTime = Constants.Instance.Destr3ProductTimeStation1;
                        //sc.Gun1ProductTime = Constants.Instance.Gun1ProductTimeStation1;
                        //sc.MiniGunProductTime = Constants.Instance.MiniGunProductTimeStation1;
                        ////sc.FighterProducTime = Lists.FighterProductTimeStation1;
                        //sc.EnergyProductTime = Constants.Instance.EnergyProductTimeStation1;
                    }
                    else if (sc.stationCurrentLevel == 2)
                    {
                        sc.nextUpgradeEnergyCount = Constants.Instance.enrgy2to3Upgrd;
                        sc.currentUpgradeTime = Constants.Instance.time2to3Upgrd;

                        //setting some start features of 0 level station before activating it
                        //sc.Cruis4ProductTime = Constants.Instance.Cruis4ProductTimeStation2;
                        //sc.Cruis3ProductTime = Constants.Instance.Cruis3ProductTimeStation2;
                        //sc.Cruis2ProductTime = Constants.Instance.Cruis2ProductTimeStation2;
                        //sc.Destr4ProductTime = Constants.Instance.Destr4ProductTimeStation2;
                        //sc.Destr3ProductTime = Constants.Instance.Destr3ProductTimeStation2;
                        //sc.Destr2ProductTime = Constants.Instance.Destr2ProductTimeStation2;
                        //sc.Destr2ParProductTime = Constants.Instance.Destr2ParProductTimeStation2;
                        //sc.Gun1ProductTime = Constants.Instance.Gun1ProductTimeStation2;
                        //sc.Gun2ProductTime = Constants.Instance.Gun2ProductTimeStation2;
                        //sc.MiniGunProductTime = Constants.Instance.MiniGunProductTimeStation2;
                        ////sc.FighterProducTime = Lists.FighterProductTimeStation2;
                        //sc.EnergyProductTime = Constants.Instance.EnergyProductTimeStation2;
                    }
                    else if (sc.stationCurrentLevel == 3)
                    {
                        //only for setting a values to upgrade function properties, in fact there are no any upgrade levels any further
                        sc.nextUpgradeEnergyCount = Constants.Instance.enrgy2to3Upgrd;
                        sc.currentUpgradeTime = Constants.Instance.time2to3Upgrd;

                        //setting some start features of 0 level station before activating it
                        //sc.Cruis4ProductTime = Constants.Instance.Cruis4ProductTimeStation3;
                        //sc.Cruis3ProductTime = Constants.Instance.Cruis3ProductTimeStation3;
                        //sc.Cruis2ProductTime = Constants.Instance.Cruis2ProductTimeStation3;
                        //sc.Cruis1ProductTime = Constants.Instance.Cruis1ProductTimeStation3;
                        //sc.Destr4ProductTime = Constants.Instance.Destr4ProductTimeStation3;
                        //sc.Destr3ProductTime = Constants.Instance.Destr3ProductTimeStation3;
                        //sc.Destr1ProductTime = Constants.Instance.Destr1ProductTimeStation3;
                        //sc.Destr1ParProductTime = Constants.Instance.Destr1ParProductTimeStation3;
                        //sc.Destr2ProductTime = Constants.Instance.Destr2ProductTimeStation3;
                        //sc.Destr2ParProductTime = Constants.Instance.Destr2ParProductTimeStation3;
                        //sc.Gun1ProductTime = Constants.Instance.Gun1ProductTimeStation3;
                        //sc.Gun2ProductTime = Constants.Instance.Gun2ProductTimeStation3;
                        //sc.Gun3ProductTime = Constants.Instance.Gun3ProductTimeStation3;
                        //sc.MiniGunProductTime = Constants.Instance.MiniGunProductTimeStation3;
                        ////sc.FighterProducTime = Lists.FighterProductTimeStation3;
                        //sc.EnergyProductTime = Constants.Instance.EnergyProductTimeStation3;
                    }

                    //removing the station that is now player's from the list of CPU stations that are launching the cruiser to scene and give it's params to timer
                    //and resetting the timer
                    //allCruisLaunchingCPUStations.Remove(sc);
                    //resetTheTimer();
                }
                else
                {
                    if (Lists.isBlackDimension)
                    {
                        sc.stationCurrentLevel = 1;
                        sc.upgradeCounts = 1;
                        //setting some start features of 0 level station before activating it
                        //sc.Cruis4ProductTime = Constants.Instance.Cruis4ProductTimeStation1;
                        //sc.Cruis3ProductTime = Constants.Instance.Cruis3ProductTimeStation1;
                        //sc.Destr4ProductTime = Constants.Instance.Destr4ProductTimeStation1;
                        //sc.Destr3ProductTime = Constants.Instance.Destr3ProductTimeStation1;
                        //sc.Gun1ProductTime = Constants.Instance.Gun1ProductTimeStation1;
                        //sc.MiniGunProductTime = Constants.Instance.MiniGunProductTimeStation1;
                        //sc.EnergyProductTime = Constants.Instance.EnergyProductTimeStation1;
                    }
                    else if (Lists.isBlueDimension)
                    {
                        sc.stationCurrentLevel = 2;
                        sc.upgradeCounts = 2;
                        //setting some start features of 0 level station before activating it
                        //sc.Cruis4ProductTime = Constants.Instance.Cruis4ProductTimeStation2;
                        //sc.Cruis3ProductTime = Constants.Instance.Cruis3ProductTimeStation2;
                        //sc.Cruis2ProductTime = Constants.Instance.Cruis2ProductTimeStation2;
                        //sc.Destr4ProductTime = Constants.Instance.Destr4ProductTimeStation2;
                        //sc.Destr3ProductTime = Constants.Instance.Destr3ProductTimeStation2;
                        //sc.Destr2ProductTime = Constants.Instance.Destr2ProductTimeStation2;
                        //sc.Destr2ParProductTime = Constants.Instance.Destr2ParProductTimeStation2;
                        //sc.Gun1ProductTime = Constants.Instance.Gun1ProductTimeStation2;
                        //sc.Gun2ProductTime = Constants.Instance.Gun2ProductTimeStation2;
                        //sc.MiniGunProductTime = Constants.Instance.MiniGunProductTimeStation2;
                        ////sc.FighterProducTime = Lists.FighterProductTimeStation2;
                        //sc.EnergyProductTime = Constants.Instance.EnergyProductTimeStation2;
                    }
                    else
                    {
                        sc.stationCurrentLevel = 3;
                        sc.upgradeCounts = 3;
                        //setting some start features of 0 level station before activating it
                        //sc.Cruis4ProductTime = Constants.Instance.Cruis4ProductTimeStation3;
                        //sc.Cruis3ProductTime = Constants.Instance.Cruis3ProductTimeStation3;
                        //sc.Cruis2ProductTime = Constants.Instance.Cruis2ProductTimeStation3;
                        //sc.Cruis1ProductTime = Constants.Instance.Cruis1ProductTimeStation3;
                        //sc.Destr4ProductTime = Constants.Instance.Destr4ProductTimeStation3;
                        //sc.Destr3ProductTime = Constants.Instance.Destr3ProductTimeStation3;
                        //sc.Destr1ProductTime = Constants.Instance.Destr1ProductTimeStation3;
                        //sc.Destr1ParProductTime = Constants.Instance.Destr1ParProductTimeStation3;
                        //sc.Destr2ProductTime = Constants.Instance.Destr2ProductTimeStation3;
                        //sc.Destr2ParProductTime = Constants.Instance.Destr2ParProductTimeStation3;
                        //sc.Gun1ProductTime = Constants.Instance.Gun1ProductTimeStation3;
                        //sc.Gun2ProductTime = Constants.Instance.Gun2ProductTimeStation3;
                        //sc.Gun3ProductTime = Constants.Instance.Gun3ProductTimeStation3;
                        //sc.MiniGunProductTime = Constants.Instance.MiniGunProductTimeStation3;
                        ////sc.FighterProducTime = Lists.FighterProductTimeStation3;
                        //sc.EnergyProductTime = Constants.Instance.EnergyProductTimeStation3;
                    }
                    //disactivating the guard core station attack timer cause now it is player's station
                    if (GuardAttackTimerTitle.gameObject.activeInHierarchy)
                    {
                        //guartCoreStation = null;
                        GuardAttackTimerTitle.gameObject.SetActive(false);
                    }
                }

                //starting energy production automatically after launch on station of player 
                //sc.energyProductionLaunche();

                if (!sc.isGuardCoreStation) {
                    if (!sc.isGuardStation)
                    {
                        //removing this station from proper CPU's stations lists and making a according settings to station and updating scene UI info about defeatet CPU's stations
                        if (sc.CPUNumber == 0)
                        {
                            Lists.CPU1Stations.Remove(sc);
                            resetTheCountsOfStationIconsWhileOnScene(sc.CPUNumber + 1, Lists.CPU1Stations.Count, Lists.CPU1CruisersOnScene);
                        }
                        else if (sc.CPUNumber == 1)
                        {
                            Lists.CPU2Stations.Remove(sc);
                            resetTheCountsOfStationIconsWhileOnScene(sc.CPUNumber + 1, Lists.CPU2Stations.Count, Lists.CPU2CruisersOnScene);
                        }
                        else if (sc.CPUNumber == 2)
                        {
                            Lists.CPU3Stations.Remove(sc);
                            resetTheCountsOfStationIconsWhileOnScene(sc.CPUNumber + 1, Lists.CPU3Stations.Count, Lists.CPU3CruisersOnScene);
                        }
                        else if (sc.CPUNumber == 3)
                        {
                            Lists.CPU4Stations.Remove(sc);
                            resetTheCountsOfStationIconsWhileOnScene(sc.CPUNumber + 1, Lists.CPU4Stations.Count, Lists.CPU4CruisersOnScene);
                        }
                    }
                    else Lists.CPUGuardStations.Remove(sc);
                }
                else Lists.CPUGuardStations.Remove(sc);

                sc.isCPUStation = false; //cause the CPU got the station on that if block
                sc.isGuardStation = false; //cause the CPU got the station on that if block
                sc.isPlayerStation = true; //cause the player got the station on that if block

                if (sc.CPUSceneCruiserLaunchCoroutineIsOn)
                {
                    //checking if cunquered station CPU not left without any cruisers launching stations
                    //ifCPUStationLaunchesACruiser(sc.CPUNumber);
                    sc.CPUSceneCruiserLaunchCoroutineIsOn = false; //stops the process of creating the cruiser;
                }

                //so if met station is not the CPU's it will be removed from other players stations list 
                Lists.playerStations.Add(sc);


                if (!sc.isGuardCoreStation) sc.stationsEnergon.makeEnergonPlayers();

                //updating the fleet of player cruis since it won cause CPU station was defeated. This func is here cause there is collider trigger on launching obj class that passes the fleet of player to station
                Lists.UpdateFleetOfPlayerCruis(playerCruiserObject);

                playerCruiserObject.gameObject.SetActive(true);

                //only after player cruiser is enabled all colliders of it are activated to prevent the missing reference bug on LaunchingObjcts calss while 
                //looking for a CaptureButton object in OnTriggeer enter method, cause it is called begore all
                playerCruiserObject.gameObject.GetComponent<Collider>().enabled = true;
                //this one is necessary to eneable the collider of energy gather child component of player scene cruiser
                foreach (Collider col in playerCruiserObject.gameObject.GetComponentsInChildren<Collider>()) col.enabled = true;


                //updating the information about human player icons on scene cause player lost it's station
                //0 is permanent index of player and 1 is need to show that player does not lost it's cruiser, cause it is impossible
                resetTheCountsOfStationIconsWhileOnScene(0, Lists.playerStations.Count, 1);

                sc.disactivateInfoAboutShip();
                SelectingBox.Instance.selectableStations.Remove(sc);
                SelectingBox.Instance.selectableStationsPlayer.Add(sc);
                //so if there left no enemy stations and cruisers on scene player wins
                if ((Lists.CPU1CruisersOnScene + Lists.CPU2CruisersOnScene + Lists.CPU3CruisersOnScene + Lists.CPU4CruisersOnScene) < 1 && (Lists.CPUGuardStations.Count + Lists.CPU1Stations.Count + Lists.CPU2Stations.Count + Lists.CPU3Stations.Count +
                    Lists.CPU4Stations.Count) < 1) youWinTheGameFunction();

                //if (!YouWinLoseButton.activeInHierarchy) changePortedCruiserSprite(); //this method changes the journey cruiser of CPU in case if it lost higher level cruiser while battle
            }
            else
            {
                //following two if conditions reset the fleet of station after it had a battle with this Player cruiser, 
                //so it reduces the fleet level of station (make a step lower to one)
                //and restart the coroutine of increasing the fleet
                //only if it is not a guard station
                //if (!sc.isGuardStation)
                //{
                //    if (sc.fleetIncreaseStep > 0)
                //    {
                //        sc.fleetIncreaseStep--;
                //        sc.setCPUStationFleetByLvl();
                //    }
                //    else
                //    {
                //        sc.setCPUStationFleetByLvl();
                //    }
                //}
                ////make the UI lose effect work after getting back from battle since player cruiser lost
                //if (Lists.isBlackDimension) Lists.energyOfPlayer -= Constants.Instance.enrgyGainLoseDark;
                //else if (Lists.isBlueDimension) Lists.energyOfPlayer -= Constants.Instance.enrgyGainLoseBlue;
                //else if (Lists.isRedDimension) Lists.energyOfPlayer -= Constants.Instance.enrgyGainLoseRed;

                //if (Lists.energyOfPlayer < 0) Lists.energyOfPlayer = 0;

                //energyCount.text = Lists.energyOfPlayer.ToString("0");
                ////energyCount.transform.parent.gameObject.GetComponent<Animator>().SetBool("tokenActive", true);
                //energyCount.color = Color.red;


                //if (Lists.isBlackDimension) Lists.boosterOfPlayer -= Constants.Instance.boosterGainLoseDark;
                //else if (Lists.isBlueDimension) Lists.boosterOfPlayer -= Constants.Instance.boosterGainLoseBlue;
                //else if (Lists.isRedDimension) Lists.boosterOfPlayer -= Constants.Instance.boosterGainLoseRed;
                //if (Lists.boosterOfPlayer < 0) Lists.boosterOfPlayer = 0;
                //boosterCount.text = Lists.boosterOfPlayer.ToString("0");
                //boosterCount.color = Color.red;

                //clearing the fleet of player cruis since it was defeated cause CPU station won
                //Lists.ClearFleetOfPlayerCruis();


                Lists.updateTheFleetOfCPUStationAfterVictory(sc);
                float reduceAmount;
                if (Lists.isBlackDimension) reduceAmount = Constants.Instance.enrgyGainLoseDark * 0.55f;
                else if (Lists.isBlueDimension) reduceAmount = Constants.Instance.enrgyGainLoseBlue * 0.55f;
                else reduceAmount = Constants.Instance.enrgyGainLoseRed * 0.55f;
                changingTheEnergyOfPlayer(false, reduceAmount);
                playerCruiserObject.makePlayerCruiserDefault();
                Lists.shipsOnScene.Remove(playerCruiserObject.gameObject);

                //changePortedCruiserSprite(); //this method changes the journey cruiser of CPU to 4 level cruiser cause player lost the battle

                //condition to check if player lost the game 
                if (!YouWinLoseButton.activeInHierarchy && Lists.playerStations.Count < 1 && countOfPlayerCruisers() < 1 && (Lists.emptyStations.Count < 1 || (Lists.emptyStations.Count > 0 && Lists.energyOfPlayer < 120)))
                {
                    youLoseTheGameFunction();
                    //Destroy(CruisJourneyReal);
                }
            }
        }

        if (!YouWinLoseButton.activeInHierarchy)
        {
            //rewardAdsPanel.transform.localPosition = new Vector2(0,0);
            rewardAdsPanel.SetActive(true); 
            if (Lists.isBlackDimension)
            {
                if (isPlayerStationLocal) {
                    if (win)
                    {
                        beforeEnergy.text = Constants.Instance.enrgyGainLoseDark.ToString("0");
                        //beforeBooster.text = Constants.Instance.boosterGainLoseDark.ToString("0");
                        energyGain = Constants.Instance.enrgyGainLoseDark;
                        //boosterGain = Constants.Instance.boosterGainLoseDark;
                        afterEnergy.text = Constants.Instance.enrgyGainLoseDarkWithAds.ToString("0");
                        //afterBooster.text = Constants.Instance.boosterGainLoseDarkWithAds.ToString("0");
                        energyGainAfter = Constants.Instance.enrgyGainLoseDarkWithAds;
                        //boosterGainAfter = Constants.Instance.boosterGainLoseDarkWithAds;
                    }
                    else
                    {
                        beforeEnergy.color = Color.red;
                        beforeEnergy.text = (Constants.Instance.enrgyGainLoseDark * -1).ToString("0");
                        //beforeBooster.color = Color.red;
                        //beforeBooster.text = (Constants.Instance.boosterGainLoseDark * -1).ToString("0");
                        energyGain = Constants.Instance.enrgyGainLoseDark * -1;
                        //boosterGain = Constants.Instance.boosterGainLoseDark * -1;
                        afterEnergy.text = (Constants.Instance.enrgyGainLoseDarkWithAds * -0.5f).ToString("0");
                        //afterBooster.text = (Constants.Instance.boosterGainLoseDarkWithAds *-0.5f).ToString("0");
                        energyGainAfter = (int)(Constants.Instance.enrgyGainLoseDarkWithAds * -0.5f);
                        //boosterGainAfter = (int)(Constants.Instance.boosterGainLoseDarkWithAds * -0.5f);
                    }
                }
                else
                {
                    if (win)
                    {
                        beforeEnergy.text = sc.energyOfStation.ToString("0");
                        energyGain = (int)sc.energyOfStation;
                        afterEnergy.text = (sc.energyOfStation*1.3f).ToString("0");
                        energyGainAfter = (int)(sc.energyOfStation * 1.3f);
                    }
                    else
                    {
                        beforeEnergy.color = Color.red;
                        beforeEnergy.text = (Constants.Instance.enrgyGainLoseDark * -1).ToString("0");
                        energyGain = Constants.Instance.enrgyGainLoseDark * -1;
                        afterEnergy.text = (Constants.Instance.enrgyGainLoseDarkWithAds * -0.5f).ToString("0");
                        energyGainAfter = (int)(Constants.Instance.enrgyGainLoseDarkWithAds * -0.5f);
                    }
                }

            }
            if (Lists.isBlueDimension)
            {
                if (isPlayerStationLocal)
                {
                    if (win)
                    {
                        beforeEnergy.text = Constants.Instance.enrgyGainLoseBlue.ToString("0");
                        //beforeBooster.text = Constants.Instance.boosterGainLoseDark.ToString("0");
                        energyGain = Constants.Instance.enrgyGainLoseBlue;
                        //boosterGain = Constants.Instance.boosterGainLoseDark;
                        afterEnergy.text = Constants.Instance.enrgyGainLoseBlueWithAds.ToString("0");
                        //afterBooster.text = Constants.Instance.boosterGainLoseDarkWithAds.ToString("0");
                        energyGainAfter = Constants.Instance.enrgyGainLoseBlueWithAds;
                        //boosterGainAfter = Constants.Instance.boosterGainLoseDarkWithAds;
                    }
                    else
                    {
                        beforeEnergy.color = Color.red;
                        beforeEnergy.text = (Constants.Instance.enrgyGainLoseBlue * -1).ToString("0");
                        //beforeBooster.color = Color.red;
                        //beforeBooster.text = (Constants.Instance.boosterGainLoseDark * -1).ToString("0");
                        energyGain = Constants.Instance.enrgyGainLoseBlue * -1;
                        //boosterGain = Constants.Instance.boosterGainLoseDark * -1;
                        afterEnergy.text = (Constants.Instance.enrgyGainLoseBlueWithAds * -0.5f).ToString("0");
                        //afterBooster.text = (Constants.Instance.boosterGainLoseDarkWithAds *-0.5f).ToString("0");
                        energyGainAfter = (int)(Constants.Instance.enrgyGainLoseBlueWithAds * -0.5f);
                        //boosterGainAfter = (int)(Constants.Instance.boosterGainLoseDarkWithAds * -0.5f);
                    }
                }
                else
                {
                    if (win)
                    {
                        beforeEnergy.text = sc.energyOfStation.ToString("0");
                        energyGain = (int)sc.energyOfStation;
                        afterEnergy.text = (sc.energyOfStation * 1.3f).ToString("0");
                        energyGainAfter = (int)(sc.energyOfStation * 1.3f);
                    }
                    else
                    {
                        beforeEnergy.color = Color.red;
                        beforeEnergy.text = (Constants.Instance.enrgyGainLoseBlue * -1).ToString("0");
                        energyGain = Constants.Instance.enrgyGainLoseBlue * -1;
                        afterEnergy.text = (Constants.Instance.enrgyGainLoseDarkWithAds * -0.5f).ToString("0");
                        energyGainAfter = (int)(Constants.Instance.enrgyGainLoseDarkWithAds * -0.5f);
                    }
                }
            }
            if (Lists.isRedDimension)
            {
                if (isPlayerStationLocal)
                {
                    if (win)
                    {
                        beforeEnergy.text = Constants.Instance.enrgyGainLoseRed.ToString("0");
                        //beforeBooster.text = Constants.Instance.boosterGainLoseDark.ToString("0");
                        energyGain = Constants.Instance.enrgyGainLoseRed;
                        //boosterGain = Constants.Instance.boosterGainLoseDark;
                        afterEnergy.text = Constants.Instance.enrgyGainLoseRedWithAds.ToString("0");
                        //afterBooster.text = Constants.Instance.boosterGainLoseDarkWithAds.ToString("0");
                        energyGainAfter = Constants.Instance.enrgyGainLoseRedWithAds;
                        //boosterGainAfter = Constants.Instance.boosterGainLoseDarkWithAds;
                    }
                    else
                    {
                        beforeEnergy.color = Color.red;
                        beforeEnergy.text = (Constants.Instance.enrgyGainLoseRed * -1).ToString("0");
                        //beforeBooster.color = Color.red;
                        //beforeBooster.text = (Constants.Instance.boosterGainLoseDark * -1).ToString("0");
                        energyGain = Constants.Instance.enrgyGainLoseRed * -1;
                        //boosterGain = Constants.Instance.boosterGainLoseDark * -1;
                        afterEnergy.text = (Constants.Instance.enrgyGainLoseRedWithAds * -0.5f).ToString("0");
                        //afterBooster.text = (Constants.Instance.boosterGainLoseDarkWithAds *-0.5f).ToString("0");
                        energyGainAfter = (int)(Constants.Instance.enrgyGainLoseRedWithAds * -0.5f);
                        //boosterGainAfter = (int)(Constants.Instance.boosterGainLoseDarkWithAds * -0.5f);
                    }
                }
                else
                {
                    if (win)
                    {
                        beforeEnergy.text = sc.energyOfStation.ToString("0");
                        energyGain = (int)sc.energyOfStation;
                        afterEnergy.text = (sc.energyOfStation * 1.3f).ToString("0");
                        energyGainAfter = (int)(sc.energyOfStation * 1.3f);
                    }
                    else
                    {
                        beforeEnergy.color = Color.red;
                        beforeEnergy.text = (Constants.Instance.enrgyGainLoseRed * -1).ToString("0");
                        energyGain = Constants.Instance.enrgyGainLoseRed * -1;
                        afterEnergy.text = (Constants.Instance.enrgyGainLoseRedWithAds * -0.5f).ToString("0");
                        energyGainAfter = (int)(Constants.Instance.enrgyGainLoseRedWithAds * -0.5f);
                    }
                }
            }


            //stopping the time to propose to player to whatch video
            //Time.timeScale = 0;
            //Time.fixedDeltaTime = 0;
            adsPanelIsOn = true;
        }


        //energyCount.transform.parent.gameObject.GetComponent<Animator>().SetBool("tokenActive", true);
        //boosterCount.transform.parent.gameObject.GetComponent<Animator>().SetBool("tokenActive", true);
        //Invoke("disactAnimAndColorsOfTokens", 2.5f);

        //reset the condition of checking with whom and whom is about to be a fight, to prevent a bug when launching a scene after getting back from menue if it 
        //was saved
        Lists.battleWithCruiser = false;
        Lists.battleWithStation = false;
        Lists.battleWithGuard = false;
        //sc.updateFleetCountToDisplay();
    }

    //this method is callsed from start method after getting back to journey scene from the battle of player cruiser and guard cruiser
    public void updateTheFleetAfterBattleWithGuard(bool win)
    {
        LaunchingObjcts playerCruiserObject = Lists.shipOnAttack.GetComponent<LaunchingObjcts>();
        EnergonMngr guardObject = Lists.CPUShipOnAttack.GetComponent<EnergonMngr>();
        if (win)
        {
            //updating the fleet of player cruis since it won cause CPU station was defeated
            Lists.UpdateFleetOfPlayerCruis(playerCruiserObject);

            playerCruiserObject.gameObject.SetActive(true);
            changeCruiserSprite(playerCruiserObject); //this method changes the journey cruiser of CPU in case if it lost higher level cruiser while battle

            //only after player cruiser is enabled all colliders of it are activated to prevent the missing reference bug on LaunchingObjcts calss while 
            //looking for a CaptureButton object in OnTriggeer enter method, cause it is called begore all
            playerCruiserObject.gameObject.GetComponent<Collider>().enabled = true;
            //this one is necessary to eneable the collider of energy gather child component of player scene cruiser
            foreach (Collider col in playerCruiserObject.gameObject.GetComponentsInChildren<Collider>()) col.enabled = true;
            Lists.energonsOnScene.Remove(Lists.CPUShipOnAttack);
        }
        else
        {
            ////clearing the fleet of player cruis since it was defeated cause CPU station won
            //Lists.ClearFleetOfPlayerCruis();


            Lists.updateTheFleetOfGuardAfterVictory(guardObject);
            Lists.CPUShipOnAttack.SetActive(true);

            playerCruiserObject.makePlayerCruiserDefault();
            Lists.shipsOnScene.Remove(playerCruiserObject.gameObject);

            //changeCruiserSprite(); //this method changes the journey cruiser of CPU to 4 level cruiser cause player lost the battle

            //condition to check if player lost the game 
            if (!YouWinLoseButton.activeInHierarchy && Lists.playerStations.Count < 1 && countOfPlayerCruisers() < 1 && (Lists.emptyStations.Count < 1 || (Lists.emptyStations.Count > 0 && Lists.energyOfPlayer < 120)))
            {
                youLoseTheGameFunction();
                //Destroy(CruisJourneyReal);
            }
        }

        if (!YouWinLoseButton.activeInHierarchy)
        {
            //rewardAdsPanel.transform.localPosition = new Vector2(0, 0);
            rewardAdsPanel.SetActive(true);
            if (Lists.isBlackDimension)
            {
                if (win)
                {
                    beforeEnergy.text = guardObject.energyOfEnergonAndGuard.ToString("0");
                    //beforeBooster.text = (Constants.Instance.boosterGainLoseDark * 1.6f).ToString("0");
                    energyGain = (int)guardObject.energyOfEnergonAndGuard;
                    //boosterGain = (int)(Constants.Instance.boosterGainLoseDark * 1.6f);
                    afterEnergy.text = (guardObject.energyOfEnergonAndGuard * 1.3f).ToString("0");
                    //afterBooster.text = (Constants.Instance.boosterGainLoseDarkWithAds * 1.6f).ToString("0");
                    energyGainAfter = (int)(guardObject.energyOfEnergonAndGuard * 1.3f);
                    //boosterGainAfter = (int)(Constants.Instance.boosterGainLoseDarkWithAds * 1.6f);
                }
                else
                {
                    beforeEnergy.color = Color.red;
                    beforeEnergy.text = (Constants.Instance.enrgyGainLoseDark * -1.3f).ToString("0");
                    //beforeBooster.color = Color.red;
                    //beforeBooster.text = (Constants.Instance.boosterGainLoseDark * -1.3f).ToString("0");
                    energyGain = (int) (Constants.Instance.enrgyGainLoseDark * -1.3f);
                    //boosterGain = (int) (Constants.Instance.boosterGainLoseDark * -1.3f);
                    afterEnergy.text = (Constants.Instance.enrgyGainLoseDarkWithAds * -0.5f * 1.3f).ToString("0");
                    //afterBooster.text = (Constants.Instance.boosterGainLoseDarkWithAds * -0.5f * 1.3f).ToString("0");
                    energyGainAfter = (int)(Constants.Instance.enrgyGainLoseDarkWithAds * -0.5f * 1.3f);
                    //boosterGainAfter = (int)(Constants.Instance.boosterGainLoseDarkWithAds * -0.5f * 1.3f);
                }
            }
            if (Lists.isBlueDimension)
            {
                if (win)
                {
                    beforeEnergy.text = guardObject.energyOfEnergonAndGuard.ToString("0");
                    //beforeBooster.text = (Constants.Instance.boosterGainLoseDark * 1.6f).ToString("0");
                    energyGain = (int)guardObject.energyOfEnergonAndGuard;
                    //boosterGain = (int)(Constants.Instance.boosterGainLoseDark * 1.6f);
                    afterEnergy.text = (guardObject.energyOfEnergonAndGuard * 1.3f).ToString("0");
                    //afterBooster.text = (Constants.Instance.boosterGainLoseDarkWithAds * 1.6f).ToString("0");
                    energyGainAfter = (int)(guardObject.energyOfEnergonAndGuard * 1.3f);
                    //boosterGainAfter = (int)(Constants.Instance.boosterGainLoseDarkWithAds * 1.6f);
                }
                else
                {
                    beforeEnergy.color = Color.red;
                    beforeEnergy.text = (Constants.Instance.enrgyGainLoseBlue * -1.3f).ToString("0");
                    //beforeBooster.color = Color.red;
                    //beforeBooster.text = (Constants.Instance.boosterGainLoseBlue * -1.3F).ToString("0");
                    energyGain = (int)(Constants.Instance.enrgyGainLoseBlue * -1.3f);
                    //boosterGain = (int)(Constants.Instance.boosterGainLoseBlue * -1.3f);


                    afterEnergy.text = (Constants.Instance.enrgyGainLoseBlueWithAds * -0.5f * 1.3f).ToString("0");
                    //afterBooster.text = (Constants.Instance.boosterGainLoseBlueWithAds * -0.5f * 1.3f).ToString("0");
                    energyGainAfter = (int)(Constants.Instance.enrgyGainLoseBlueWithAds * -0.5f * 1.3f);
                    //boosterGainAfter = (int)(Constants.Instance.boosterGainLoseBlueWithAds * -0.5f * 1.3f);
                }
            }
            if (Lists.isRedDimension)
            {
                if (win)
                {
                    beforeEnergy.text = guardObject.energyOfEnergonAndGuard.ToString("0");
                    //beforeBooster.text = (Constants.Instance.boosterGainLoseDark * 1.6f).ToString("0");
                    energyGain = (int)guardObject.energyOfEnergonAndGuard;
                    //boosterGain = (int)(Constants.Instance.boosterGainLoseDark * 1.6f);
                    afterEnergy.text = (guardObject.energyOfEnergonAndGuard * 1.3f).ToString("0");
                    //afterBooster.text = (Constants.Instance.boosterGainLoseDarkWithAds * 1.6f).ToString("0");
                    energyGainAfter = (int)(guardObject.energyOfEnergonAndGuard * 1.3f);
                    //boosterGainAfter = (int)(Constants.Instance.boosterGainLoseDarkWithAds * 1.6f);
                }
                else
                {
                    beforeEnergy.color = Color.red;
                    beforeEnergy.text = (Constants.Instance.enrgyGainLoseRed * -1.3f).ToString("0");
                    //beforeBooster.color = Color.red;
                    //beforeBooster.text = (Constants.Instance.boosterGainLoseRed * -1.3F).ToString("0");
                    energyGain = (int)(Constants.Instance.enrgyGainLoseRed * -1.3f);
                    //boosterGain = (int)(Constants.Instance.boosterGainLoseRed * -1.3f);


                    afterEnergy.text = (Constants.Instance.enrgyGainLoseRedWithAds * -0.5f * 1.3f).ToString("0");
                    //afterBooster.text = (Constants.Instance.boosterGainLoseRedWithAds * -0.5f * 1.3f).ToString("0");
                    energyGainAfter = (int)(Constants.Instance.enrgyGainLoseRedWithAds * -0.5f * 1.3f);
                    //boosterGainAfter = (int)(Constants.Instance.boosterGainLoseRedWithAds * -0.5f * 1.3f);
                }

            }


            //stopping the time to propose to player to whatch video
            //Time.timeScale = 0;
            //Time.fixedDeltaTime = 0;
            adsPanelIsOn = true;
        }

        //energyCount.transform.parent.gameObject.GetComponent<Animator>().SetBool("tokenActive", true);
        //boosterCount.transform.parent.gameObject.GetComponent<Animator>().SetBool("tokenActive", true);
        //Invoke("disactAnimAndColorsOfTokens", 2.5f);

        //reset the condition of checking with whom and whom is about to be a fight, to prevent a bug when launching a scene after getting back from menue if it was saved
        Lists.battleWithCruiser = false;
        Lists.battleWithStation = false;
        Lists.battleWithGuard = false;
    }

    //this method is callsed from start method after getting back to journey scene from the battle of player cruiser and CPU cruiser
    public void updateTheFleetAfterBattleWithCruisers(CPUShipCtrlJourney CPUShipRef, bool win)
    {
        LaunchingObjcts playerCruiserObject = Lists.shipOnAttack.GetComponent<LaunchingObjcts>();
        if (win)
        {
            //updating the fleet of player cruis since it won cause CPU station was defeated
            Lists.UpdateFleetOfPlayerCruis(playerCruiserObject);

            playerCruiserObject.gameObject.SetActive(true);
            changeCruiserSprite(playerCruiserObject); //this method changes the journey cruiser of CPU in case if it lost higher level cruiser while battle

            //only after player cruiser is enabled all colliders of it are activated to prevent the missing reference bug on LaunchingObjcts calss while 
            //looking for a CaptureButton object in OnTriggeer enter method, cause it is called begore all
            playerCruiserObject.gameObject.GetComponent<Collider>().enabled = true;
            //this one is necessary to eneable the collider of energy gather child component of player scene cruiser
            foreach (Collider col in playerCruiserObject.gameObject.GetComponentsInChildren<Collider>()) col.enabled = true;

            CPUShipRef.launchANewCruiser(); //starting a process of launching a proper scene cruiser on chosen station (from method)

            CPUShipRef.disactivatingCurrentShipNoBurst();

            //Destroy(CPUShipRef.gameObject); //destroying the incatance of CPU cruiser is necessary cause it was saved by DontDestry func while switching the scene
        }
        else
        {
            Lists.updateTheFleetOfCPUCruiserAfterVictory(CPUShipRef); 
            CPUShipRef.gameObject.SetActive(true);

            //LEFT THIS HERE ONLY FOR REFERENCE
            //updating the fleet of CPU cruiser after it won the battle
            //if (CPUShipRef.CPUNumber == 0)
            //{
            //    Lists.updateTheFleetOfCPUCruiserAfterVictory(CPUShipRef);
            //    //destroying the CPU scene cruiser if it lost all it's cruisers in battle even if it won
            //    if ((Lists.Cruis1OfCPU1 + Lists.Cruis2OfCPU1 + Lists.Cruis3OfCPU1 + Lists.Cruis4OfCPU1) < 1)
            //    {
            //        CPUShipRef.launchANewCruiser(); //starting a process of launching a proper scene cruiser on chosen station (from method). NOT ANY MORE IT JUST UPDETES THE UI
            //        Destroy(CPUShipRef.gameObject); //destroying the incatance of CPU cruiser is necessary cause it was saved by DontDestry func while switching the scene
            //    }
            //    else
            //    {
            //        CPUShipRef.gameObject.SetActive(true); //activateing enemy cruiser if it has at least one cruiser for further attacks
            //        //activating the launched cruiser signal for the CPU cruiser tha was respawned in scene
            //        //turnOnOffLaunchedCruiserIcon(CPUShipRef.GetComponent<CPUShipCtrlJourney>().CPUNumber + 1, true);
            //    }
            //}

            ////make the UI lose effect work after getting back from battle since player cruiser lost
            //if (Lists.isBlackDimension) Lists.energyOfPlayer -= Constants.Instance.enrgyGainLoseDark * 0.6f;
            //else if (Lists.isBlueDimension) Lists.energyOfPlayer -= Constants.Instance.enrgyGainLoseBlue * 0.6f;
            //else if (Lists.isRedDimension) Lists.energyOfPlayer -= Constants.Instance.enrgyGainLoseRed * 0.6f;

            //if (Lists.energyOfPlayer < 0) Lists.energyOfPlayer = 0;

            //energyCount.text = Lists.energyOfPlayer.ToString("0");
            ////energyCount.transform.parent.gameObject.GetComponent<Animator>().SetBool("tokenActive", true);
            //energyCount.color = Color.red;



            //if (Lists.isBlackDimension) Lists.boosterOfPlayer -= Constants.Instance.boosterGainLoseDark * 0.6f;
            //else if (Lists.isBlueDimension) Lists.boosterOfPlayer -= Constants.Instance.boosterGainLoseBlue * 0.6f;
            //else if (Lists.isRedDimension) Lists.boosterOfPlayer -= Constants.Instance.boosterGainLoseRed * 0.6f;
            //if (Lists.boosterOfPlayer < 0) Lists.boosterOfPlayer = 0;
            //boosterCount.text = Lists.boosterOfPlayer.ToString("0");
            //boosterCount.color = Color.red;
            playerCruiserObject.makePlayerCruiserDefault();
            Lists.shipsOnScene.Remove(playerCruiserObject.gameObject);

            //condition to check if player lost the game 
            if (!YouWinLoseButton.activeInHierarchy && Lists.playerStations.Count < 1 && countOfPlayerCruisers() < 1 && (Lists.emptyStations.Count < 1 || (Lists.emptyStations.Count > 0 && Lists.energyOfPlayer < 120)))
            {
                youLoseTheGameFunction();
            }
        }

        ////stopping the time to propose to player to whatch video
        //Time.timeScale = 0;
        //Time.fixedDeltaTime = 0;

        if (!YouWinLoseButton.activeInHierarchy)
        {
            //rewardAdsPanel.transform.localPosition = new Vector2(0, 0);

            rewardAdsPanel.SetActive(true);
            if (Lists.isBlackDimension)
            {
                if (win)
                {
                    beforeEnergy.text = (Constants.Instance.enrgyGainLoseDark * 0.6f).ToString("0");
                    //beforeBooster.text = (Constants.Instance.boosterGainLoseDark * 0.6f).ToString("0");
                    energyGain = (int)(Constants.Instance.enrgyGainLoseDark * 0.6f);
                    //boosterGain = (int)(Constants.Instance.boosterGainLoseDark * 0.6f);
                    afterEnergy.text = Constants.Instance.enrgyGainLoseDarkWithAds.ToString("0");
                    //afterBooster.text = Constants.Instance.boosterGainLoseDarkWithAds.ToString("0");
                    energyGainAfter = (int)(Constants.Instance.enrgyGainLoseDarkWithAds * 0.6f);
                    //boosterGainAfter = (int)(Constants.Instance.boosterGainLoseDarkWithAds * 0.6f);
                }
                else
                {
                    beforeEnergy.color = Color.red;
                    beforeEnergy.text = (Constants.Instance.enrgyGainLoseDark * -0.6f).ToString("0");
                    //beforeBooster.color = Color.red;
                    //beforeBooster.text = (Constants.Instance.boosterGainLoseDark * -0.6f).ToString("0");
                    energyGain = (int)(Constants.Instance.enrgyGainLoseDark * -0.6f);
                    //boosterGain = (int)(Constants.Instance.boosterGainLoseDark * -0.6f);
                    afterEnergy.text = (Constants.Instance.enrgyGainLoseDarkWithAds * -0.5f * 0.6f).ToString("0");
                    //afterBooster.text = (Constants.Instance.boosterGainLoseDarkWithAds * -0.5f * 0.6f).ToString("0");
                    energyGainAfter = (int)(Constants.Instance.enrgyGainLoseDarkWithAds * -0.5f * 0.6f);
                    //boosterGainAfter = (int)(Constants.Instance.boosterGainLoseDarkWithAds * -0.5f * 0.6f);
                }
            }
            if (Lists.isBlueDimension)
            {
                if (win)
                {
                    beforeEnergy.text = (Constants.Instance.enrgyGainLoseBlue * 0.6f).ToString("0");
                    //beforeBooster.text = (Constants.Instance.boosterGainLoseBlue * 0.6f).ToString("0");
                    energyGain = (int)(Constants.Instance.enrgyGainLoseBlue * 0.6f);
                    //boosterGain = (int)(Constants.Instance.boosterGainLoseBlue * 0.6f);
                    afterEnergy.text = Constants.Instance.enrgyGainLoseBlueWithAds.ToString("0");
                    //afterBooster.text = Constants.Instance.boosterGainLoseBlueWithAds.ToString("0");
                    energyGainAfter = (int)(Constants.Instance.enrgyGainLoseBlueWithAds * 0.6f);
                    //boosterGainAfter = (int)(Constants.Instance.boosterGainLoseBlueWithAds * 0.6f);
                }
                else
                {
                    beforeEnergy.color = Color.red;
                    beforeEnergy.text = (Constants.Instance.enrgyGainLoseBlue * -0.6f).ToString("0");
                    //beforeBooster.color = Color.red;
                    //beforeBooster.text = (Constants.Instance.boosterGainLoseBlue * -0.6f).ToString("0");
                    energyGain = (int)(Constants.Instance.enrgyGainLoseBlue * -0.6f);
                    //boosterGain = (int)(Constants.Instance.boosterGainLoseBlue * -0.6f);
                    afterEnergy.text = (Constants.Instance.enrgyGainLoseBlueWithAds * -0.5f * 0.6f).ToString("0");
                    //afterBooster.text = (Constants.Instance.boosterGainLoseBlueWithAds * -0.5f * 0.6f).ToString("0");
                    energyGainAfter = (int)(Constants.Instance.enrgyGainLoseBlueWithAds * -0.5f * 0.6f);
                    //boosterGainAfter = (int)(Constants.Instance.boosterGainLoseBlueWithAds * -0.5f * 0.6f);
                }
            }
            if (Lists.isRedDimension)
            {
                if (win)
                {
                    beforeEnergy.text = (Constants.Instance.enrgyGainLoseRed * 0.6f).ToString("0");
                    //beforeBooster.text = (Constants.Instance.boosterGainLoseRed * 0.6f).ToString("0");
                    energyGain = (int)(Constants.Instance.enrgyGainLoseRed * 0.6f);
                    //boosterGain = (int)(Constants.Instance.boosterGainLoseRed * 0.6f);
                    afterEnergy.text = Constants.Instance.enrgyGainLoseRedWithAds.ToString("0");
                    //afterBooster.text = Constants.Instance.boosterGainLoseRedWithAds.ToString("0");
                    energyGainAfter = (int)(Constants.Instance.enrgyGainLoseRedWithAds * 0.6f);
                    //boosterGainAfter = (int)(Constants.Instance.boosterGainLoseRedWithAds * 0.6f);
                }
                else
                {
                    beforeEnergy.color = Color.red;
                    beforeEnergy.text = (Constants.Instance.enrgyGainLoseRed * -0.6f).ToString("0");
                    //beforeBooster.color = Color.red;
                    //beforeBooster.text = (Constants.Instance.boosterGainLoseRed * -0.6f).ToString("0");
                    energyGain = (int)(Constants.Instance.enrgyGainLoseRed * -0.6f);
                    // boosterGain = (int)(Constants.Instance.boosterGainLoseRed * -0.6f);
                    afterEnergy.text = (Constants.Instance.enrgyGainLoseRedWithAds * -0.5f * 0.6f).ToString("0");
                    //afterBooster.text = (Constants.Instance.boosterGainLoseRedWithAds * -0.5f * 0.6f).ToString("0");
                    energyGainAfter = (int)(Constants.Instance.enrgyGainLoseRedWithAds * -0.5f * 0.6f);
                    //boosterGainAfter = (int)(Constants.Instance.boosterGainLoseRedWithAds * -0.5f * 0.6f);
                }
            }


            //stopping the time to propose to player to whatch video
            //Time.timeScale = 0;
            //Time.fixedDeltaTime = 0;
            adsPanelIsOn = true;
        }

        //energyCount.transform.parent.gameObject.GetComponent<Animator>().SetBool("tokenActive", true);
        //boosterCount.transform.parent.gameObject.GetComponent<Animator>().SetBool("tokenActive", true);
        //Invoke("disactAnimAndColorsOfTokens", 2.5f);

        //reset the condition of checking with whom and whom is about to be a fight, to prevent a bug when launching a scene after getting back from menue if it was saved
        Lists.battleWithCruiser = false;
        Lists.battleWithStation = false;
        Lists.battleWithGuard = false;
    }

    //this method is called after getting back to JourneyScene and propose to player (AND HE AGREES) to whatch rewarding ads to increase the gain or decrease the lose 
    public void whatchRewardedProposal() {
        //Time.timeScale = 1;
        //Time.fixedDeltaTime = 0.02f;
        adsPanelIsOn = false;

        //rewardAdsPanel.transform.localPosition = new Vector2(-30000, 0); //putting out the panel of rewarded video

        rewardAdsPanel.SetActive(false);

        //make the UI gain effect work after getting back from battle since player station won
        Lists.energyOfPlayer += energyGainAfter;
        if (Lists.energyOfPlayer < 0) Lists.energyOfPlayer = 0;
        energyCount.text = Lists.energyOfPlayer.ToString("0");


        //Lists.boosterOfPlayer += boosterGainAfter;
        //if (Lists.boosterOfPlayer < 0) Lists.boosterOfPlayer = 0;
        //boosterCount.text = Lists.boosterOfPlayer.ToString("0"); 


        if (energyGainAfter > 0)
        {
            //boosterCount.color = Color.green;
            energyCount.color = Color.green;
        }
        else
        {
            //boosterCount.color = Color.red;
            energyCount.color = Color.red;
        }


        energyCount.transform.parent.gameObject.GetComponent<Animator>().SetBool("tokenActive", true);
        //boosterCount.transform.parent.gameObject.GetComponent<Animator>().SetBool("tokenActive", true);
        Invoke("disactAnimAndColorsOfTokens", 2.5f);
    }
    //this method is called after getting back to JourneyScene and propose to player (AND HE DISAGREE) to whatch rewarding ads to increase the gain or decrease the lose 
    public void skipRewardedProposal()
    {
        //Time.timeScale = 1;
        //Time.fixedDeltaTime = 0.02f;
        //rewardAdsPanel.transform.localPosition = new Vector2(-30000, 0); //putting out the panel of rewarded video
        adsPanelIsOn = false;

        rewardAdsPanel.SetActive(false);

        //make the UI gain effect work after getting back from battle since player station won
        Lists.energyOfPlayer += energyGain;
        if (Lists.energyOfPlayer < 0) Lists.energyOfPlayer = 0;
        energyCount.text = Lists.energyOfPlayer.ToString("0");

        //Lists.boosterOfPlayer += boosterGain;
        //if (Lists.boosterOfPlayer < 0) Lists.boosterOfPlayer = 0;
        //boosterCount.text = Lists.boosterOfPlayer.ToString("0");

        if (energyGain > 0)
        {
            //boosterCount.color = Color.green;
            energyCount.color = Color.green;
        }
        else {
            //boosterCount.color = Color.red;
            energyCount.color = Color.red;
        }
        CommonButtonAudio.Play();
        energyCount.transform.parent.gameObject.GetComponent<Animator>().SetBool("tokenActive", true);
        //boosterCount.transform.parent.gameObject.GetComponent<Animator>().SetBool("tokenActive", true);
        Invoke("disactAnimAndColorsOfTokens", 2.5f);
    }

    //this method is called each time after any CPU station was concured by other to make sure tha at least one station of CPU is in process of launching a scene cruiser
    //public void ifCPUStationLaunchesACruiser(int CPUNumbr) {
    //    int launchingStationsCount = 0;
    //    if (CPUNumbr == 0) {
    //        if (Lists.CPU1Stations.Count > 0)
    //        {
    //            foreach (StationController sc in Lists.CPU1Stations)
    //            {
    //                if (sc.CPUSceneCruiserLaunchCoroutineIsOn)
    //                {
    //                    launchingStationsCount++;
    //                }
    //            }
    //            //checking if CPU has at least one launching station arount if not then it starts the launching cruiser processes in one of them
    //            if (launchingStationsCount < 1)
    //            {
    //                for (int i = 3; i > -1; i--) //first iteration is responsible to chose a highest level station to launche a higest level cruiser 
    //                {
    //                    for (int y = 0; y < Lists.CPU1Stations.Count; y++) //this one iterates chosen level of station upgrade to find a match among existing and brake if found 
    //                    {
    //                        if (Lists.CPU1Stations[y].stationCurrentLevel == i)
    //                        {
    //                            Lists.CPU1Stations[y].launchingACPUCruiserOnScene();
    //                            i = -1; //stopping the outer loop as well in case if cruiser is launched already in one of the stations
    //                            break;
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        else resetTheTimer(); //so if CPU does not have any stations anymore then there should be another repaint function for a case if this CPU was the onle with shortest time to launch the cruiser
    //    }
    //    else if (CPUNumbr == 1)
    //    {
    //        if (Lists.CPU2Stations.Count > 0)
    //        {
    //            foreach (StationController sc in Lists.CPU2Stations)
    //            {
    //                if (sc.CPUSceneCruiserLaunchCoroutineIsOn)
    //                {
    //                    launchingStationsCount++;
    //                }
    //            }
    //            if (launchingStationsCount < 1)
    //            {
    //                for (int i = 3; i > -1; i--) //first iteration is responsible to chose a highest level station to launche a higest level cruiser 
    //                {
    //                    for (int y = 0; y < Lists.CPU2Stations.Count; y++) //this one iterates chosen level of station upgrade to find a match among existing and brake if found 
    //                    {
    //                        if (Lists.CPU2Stations[y].stationCurrentLevel == i)
    //                        {
    //                            Lists.CPU2Stations[y].launchingACPUCruiserOnScene();
    //                            i = -1; //stopping the outer loop as well in case if cruiser is launched already in one of the stations
    //                            //Lists.CPU2Stations[y].StartCoroutine(Lists.CPU2Stations[y].launchingACPUCruiserOnScene());
    //                            break;
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        else resetTheTimer(); //so if CPU does not have any stations anymore then there should be another repaint function for a case if this CPU was the onle with shortest time to launch the cruiser
    //    }
    //    else if (CPUNumbr == 2)
    //    {
    //        if (Lists.CPU3Stations.Count > 0)
    //        {
    //            foreach (StationController sc in Lists.CPU3Stations)
    //            {
    //                if (sc.CPUSceneCruiserLaunchCoroutineIsOn)
    //                {
    //                    launchingStationsCount++;
    //                }
    //            }
    //            if (launchingStationsCount < 1)
    //            {
    //                for (int i = 3; i > -1; i--) //first iteration is responsible to chose a highest level station to launche a higest level cruiser 
    //                {
    //                    for (int y = 0; y < Lists.CPU3Stations.Count; y++) //this one iterates chosen level of station upgrade to find a match among existing and brake if found 
    //                    {
    //                        if (Lists.CPU3Stations[y].stationCurrentLevel == i)
    //                        {
    //                            Lists.CPU3Stations[y].launchingACPUCruiserOnScene();
    //                            i = -1; //stopping the outer loop as well in case if cruiser is launched already in one of the stations
    //                            //Lists.CPU3Stations[y].StartCoroutine(Lists.CPU3Stations[y].launchingACPUCruiserOnScene());
    //                            break;
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        else resetTheTimer(); //so if CPU does not have any stations anymore then there should be another repaint function for a case if this CPU was the onle with shortest time to launch the cruiser
    //    }

    //    else if (CPUNumbr == 3)
    //    {
    //        if (Lists.CPU4Stations.Count > 0)
    //        {
    //            foreach (StationController sc in Lists.CPU4Stations)
    //            {
    //                if (sc.CPUSceneCruiserLaunchCoroutineIsOn)
    //                {
    //                    launchingStationsCount++;
    //                }
    //            }
    //            if (launchingStationsCount < 1)
    //            {
    //                for (int i = 3; i > -1; i--) //first iteration is responsible to chose a highest level station to launche a higest level cruiser 
    //                {
    //                    for (int y = 0; y < Lists.CPU4Stations.Count; y++) //this one iterates chosen level of station upgrade to find a match among existing and brake if found 
    //                    {
    //                        if (Lists.CPU4Stations[y].stationCurrentLevel == i)
    //                        {
    //                            Lists.CPU4Stations[y].launchingACPUCruiserOnScene();
    //                            i = -1; //stopping the outer loop as well in case if cruiser is launched already in one of the stations
    //                            //Lists.CPU4Stations[y].StartCoroutine(Lists.CPU4Stations[y].launchingACPUCruiserOnScene());
    //                            break;
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        else resetTheTimer(); //so if CPU does not have any stations anymore then there should be another repaint function for a case if this CPU was the onle with shortest time to launch the cruiser
    //    }
    //}

    //stops the animation of energy token
    public void disactAnimAndColorsOfTokens()
    {
        energyCount.color = new Color(0.87f, 0.87f, 0.87f, 1); //Color.white;
        //boosterCount.color = new Color(0.87f, 0.87f, 0.87f, 1); //Color.white;
        energyCount.transform.parent.gameObject.GetComponent<Animator>().SetBool("tokenActive", false);
        //boosterCount.transform.parent.gameObject.GetComponent<Animator>().SetBool("tokenActive", false);
    }

    public void activatePanel() {
        if (chosenStation != null) activateStationPanel(true, chosenStation);
        else if (localLaunchingObjects != null) activatePlayerPanel();
        else CommonNotEnoughAudio.Play();
    }

    //this method is called from pushing the connection button on UI of journyScene
    public void activatePlayerPanel()
    {
        //stop player cruiser from moving
        //localLaunchingObjects.stopShipFromMoving();

        //setting time scale to regular while opening any panel on scene 
        //TimeTextField.text = "x0";
        //Time.timeScale = 0;
        //Time.fixedDeltaTime = 0;

        //the panel of managing the stations of player will open only in case if player journey cruiser is not inside of enemy station trigger
        //otherwise this button will open fleets compare panel
        if (!localLaunchingObjects.playerIsNearEnemyStation)
        {
            //so if player panel is still active or station panel as well (means that they had no time to disactivate due that animation of them close) pushing the button 
            //of activating the panel of player cruiser will not activate it. Otherwise there will be a bug on adding the same key of button to the dictionary cause it will not 
            //be cleared
            if (!PlayerCruisPanelIsEnabled && !stationPanelIsEnabled && !IsInvoking("disactivatePanels"))
            {
                CommonButtonAudio.Play(); //player cruiser panel open sound
                yourFleetPlayerPanelTxt.text = Constants.Instance.getYourFleet();
                yourStationsPlayerPanelTxt.text = Constants.Instance.getYourStations();


                //updating the greeds and fleet and stations information of player cruiser panel
                updateGreedsPlayerCruisPanel(localLaunchingObjects);

                //populating special dictionaries to use them to set mini info about stations on cruis panel. The information is set dinamically in update method if cruis panel is open
                foreach (GameObject go in ButtonAndStation.Keys)
                {
                    MiniImgAndStation.Add(go.transform.GetChild(1).GetComponent<Image>(), ButtonAndStation[go]);
                    TextAndStation.Add(go.GetComponentInChildren<Text>(), ButtonAndStation[go]);
                }

                //setting decent color to player cruiser panel depending on it's cruiser level
                if (CruisJourneyReal.CompareTag(cruis4Tag))
                {
                    //holoMatsToFade[0].color = new Color(0, 1, 0, 1);
                    panelImagesToChangeTheColor[1].color = new Color(0, 1, 0, 1);
                    //holoMatsToFade[0].SetColor("_MainColor", new Color(0, 1, 0, 0));
                    //playerCruiserPanelMaskImg.color = 
                }
                else if (CruisJourneyReal.CompareTag(cruis3Tag))
                {
                    panelImagesToChangeTheColor[1].color = new Color(0, 1, 1, 1);
                    //holoMatsToFade[0].SetColor("_MainColor", new Color(0, 1, 1, 0));
                    //playerCruiserPanelMaskImg.color = Color.cyan;
                }
                else if (CruisJourneyReal.CompareTag(cruis2Tag))
                {
                    panelImagesToChangeTheColor[1].color = new Color(0.8f, 0, 1, 1);
                    //holoMatsToFade[0].SetColor("_MainColor", new Color(0.8f, 0, 1, 0));
                    //playerCruiserPanelMaskImg.color = new Color(0.8f, 0, 1, 1);
                }
                else if (CruisJourneyReal.CompareTag(cruis1Tag))
                {
                    panelImagesToChangeTheColor[1].color = new Color(1, 0, 0, 1);
                    //holoMatsToFade[0].SetColor("_MainColor", new Color(1, 0, 0, 0));
                    //holoMatsToFade[0].color = Color.red;
                    //playerCruiserPanelMaskImg.color = Color.red;
                }

                //playerCruiserPanel.GetComponent<Animator>().SetBool("openPanel", true);
                //fade = 0;
                //playerCruiserPanelMaskImg.GetComponent<Mask>().enabled = true;
                //fadeIn = true;
                playerCruiserPanelCloseButton.SetActive(true);
                PlayerCruisPanelIsEnabled = true;

                playerCruiserPanel.SetActive(true);

            }
            else CommonNotEnoughAudio.Play();
        }
        else
        {
            CommonButtonAudio.Play(); //player cruiser panel open sound
            //Telematics.Play(); //player cruiser panel open sound
            activateFleetComparePanel(localLaunchingObjects.enemyStation);
        }
        anyPanelIsEnabled = true;
    }

    //this method is called from LaunchingObjcts class when player meets his/her own station
    public void activateStationPanel(bool panelState, StationController station)
    {
        CommonButtonAudio.Play(); //player cruiser panel open sound
                                  //Telematics.Play(); //player cruiser panel open sound

        anyPanelIsEnabled = true;
        stationReference = station;
        if (stationReference.playerCruiserObject.Count > 0) portedCruiserObject = stationReference.playerCruiserObject[stationReference.playerCruiserObject.Count - 1];
        else portedCruiserObject = null;

            //informing the player if station is vulnerable for attacks (it is so if station has no cruisers)
        if (stationReference.ifStationHasCruisers() < 1) limitReachedWarning(Constants.Instance.getVulnerableTxt());

        stationPanel.SetActive(panelState);
        //stationPanel.transform.localPosition = stationIn;

        stationPanelIsEnabled = panelState;


        if (stationReference.stationCurrentLevel == 0)
        {
            stationIcon.sprite = Station1Spr;
            stationIconBkgr.sprite = Station1Spr;
            //stationPanelMaskImage.color = Color.green;

            panelImagesToChangeTheColor[0].color = Color.green; 
            //holoMatsToFade[0].SetColor("_MainColor", Color.green);
            //so if station is under upgrade process the sprites should be according to next genereation station allready
            if (stationReference.isUpgrading)
            {
                upgradeText.gameObject.SetActive(false);
                stationIcon.sprite = Station2Spr;
                stationIcon.color = new Color(1, 1, 1, 1);
                stationIconBkgr.sprite = Station2Spr;
            }
            else
            {
                stationIcon.fillAmount = 1;
                stationIcon.color = new Color(1, 1, 1, 0.3f);

                if (stationReference.stationCurrentLevel == stationReference.upgradeCounts)
                {
                    upgradeText.gameObject.SetActive(false);
                    upgradeText.transform.parent.GetComponent<Button>().enabled = false;
                }
                else {
                    //setting upgrade button and text active by checking current energy level and if current station has a step to upgrade
                    if (Lists.energyOfPlayer >= stationReference.nextUpgradeEnergyCount /*&& stationReference.stationCurrentLevel < stationReference.upgradeCounts*/)
                    {
                        upgradeText.gameObject.SetActive(true);
                        upgradeText.text = Constants.Instance.getUpGrade();
                        upgradeText.transform.parent.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        upgradeText.transform.parent.GetComponent<Button>().enabled = true;
                    }
                    else
                    {
                        //if (stationReference.stationCurrentLevel < stationReference.upgradeCounts)
                        //{
                        //upgradeText.gameObject.SetActive(false);
                        upgradeText.gameObject.SetActive(true);
                        upgradeText.text = Constants.Instance.enrgy0to1Upgrd.ToString();
                        upgradeText.transform.parent.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                        upgradeText.transform.parent.GetComponent<Button>().enabled = false;
                        //}
                    }
                }
            }
        }
        else if (stationReference.stationCurrentLevel == 1)
        {
            if (!stationReference.isGuardCoreStation)
            {
                stationIcon.sprite = Station2Spr;
                stationIconBkgr.sprite = Station2Spr;
            }
            else {
                stationIcon.sprite = StationGSpr;
                stationIconBkgr.sprite = StationGSpr;
            }

            panelImagesToChangeTheColor[0].color = Color.cyan;
            //holoMatsToFade[0].SetColor("_MainColor", Color.cyan);
            //stationPanelMaskImage.color = Color.cyan;
            //so if station is under upgrade process the sprites should be according to next genereation station allready
            if (stationReference.isUpgrading)
            {
                upgradeText.gameObject.SetActive(false);
                stationIcon.sprite = Station3Spr;
                stationIcon.color = new Color(1, 1, 1, 1);
                stationIconBkgr.sprite = Station3Spr;
            }
            else
            {
                stationIcon.fillAmount = 1;
                stationIcon.color = new Color(1, 1, 1, 0.3f);
                
                if (stationReference.stationCurrentLevel == stationReference.upgradeCounts)
                {
                    upgradeText.gameObject.SetActive(false);
                    upgradeText.transform.parent.GetComponent<Button>().enabled = false;
                }
                else
                {
                    //setting upgrade button and text active by checking current energy level and if current station has a step to upgrade
                    if (Lists.energyOfPlayer >= stationReference.nextUpgradeEnergyCount /*&& stationReference.stationCurrentLevel < stationReference.upgradeCounts*/)
                    {
                        upgradeText.gameObject.SetActive(true);
                        upgradeText.text = Constants.Instance.getUpGrade();
                        upgradeText.transform.parent.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        upgradeText.transform.parent.GetComponent<Button>().enabled = true;
                    }
                    else
                    {
                        //if (stationReference.stationCurrentLevel < stationReference.upgradeCounts)
                        //{

                        upgradeText.gameObject.SetActive(true);
                        upgradeText.text = Constants.Instance.enrgy1to2Upgrd.ToString();
                        upgradeText.transform.parent.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                        upgradeText.transform.parent.GetComponent<Button>().enabled = false;
                        //}
                    }
                }

            }
        }
        else if (stationReference.stationCurrentLevel == 2)
        {
            if (!stationReference.isGuardCoreStation)
            {
                stationIcon.sprite = Station3Spr;
                stationIconBkgr.sprite = Station3Spr;
            }
            else
            {
                stationIcon.sprite = StationGSpr;
                stationIconBkgr.sprite = StationGSpr;
            }


            panelImagesToChangeTheColor[0].color = new Color(0.8f, 0, 1, 1);
            //holoMatsToFade[0].color = new Color(0.8f, 0, 1, 1);
            //stationPanelMaskImage.color = new Color(0.8f, 0, 1, 1);
            //so if station is under upgrade process the sprites should be according to next genereation station allready
            if (stationReference.isUpgrading)
            {
                upgradeText.gameObject.SetActive(false);
                stationIcon.sprite = Station4Spr;
                stationIcon.color = new Color(1, 1, 1, 1);
                stationIconBkgr.sprite = Station4Spr;
            }
            else
            {
                stationIcon.fillAmount = 1;
                stationIcon.color = new Color(1, 1, 1, 0.3f);

                if (stationReference.stationCurrentLevel == stationReference.upgradeCounts)
                {
                    upgradeText.gameObject.SetActive(false);
                    upgradeText.transform.parent.GetComponent<Button>().enabled = false;
                }
                else
                {
                    //setting upgrade button and text active by checking current energy level and if current station has a step to upgrade
                    if (Lists.energyOfPlayer >= stationReference.nextUpgradeEnergyCount /*&& stationReference.stationCurrentLevel < stationReference.upgradeCounts*/)
                    {
                        upgradeText.gameObject.SetActive(true);
                        upgradeText.text = Constants.Instance.getUpGrade();
                        upgradeText.transform.parent.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        upgradeText.transform.parent.GetComponent<Button>().enabled = true;
                    }
                    else
                    {
                        //if (stationReference.stationCurrentLevel < stationReference.upgradeCounts)
                        //{                        
                        upgradeText.gameObject.SetActive(true);
                        upgradeText.text = Constants.Instance.enrgy2to3Upgrd.ToString();
                        upgradeText.transform.parent.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                        upgradeText.transform.parent.GetComponent<Button>().enabled = false;
                        //}
                    }
                }
            }
        }
        else if (stationReference.stationCurrentLevel == 3)
        {
            if (!stationReference.isGuardCoreStation)
            {
                stationIcon.sprite = Station4Spr;
                stationIcon.color = new Color(1, 1, 1, 0.3f);
                stationIconBkgr.sprite = Station4Spr;
            }
            else
            {
                stationIcon.sprite = StationGSpr;
                stationIconBkgr.sprite = StationGSpr;
            }
            upgradeText.gameObject.SetActive(false); //3 lvl station has no options to upgrade any further



            panelImagesToChangeTheColor[0].color = Color.red;
            //holoMatsToFade[0].color = Color.red;
            //stationPanelMaskImage.color = Color.red;
            stationIcon.fillAmount = 1;
            upgradeText.text = "";
        }

        //so if the player cruiser ported to the station it will update the ported cruiser values. Otherwise all ported ship greeds will be disactivated
        if (stationReference.playerCruiserObject.Count > 0)
        {
            setPortedCruiserSprite();
            StationPortObject.SetActive(true);
            PortedCruiserObj.SetActive(true);
            updatePortedShipUIValues();
        }
        else {
            StationPortObject.SetActive(false);
            PortedCruiserObj.SetActive(false);
        }

        updateStationGreeds();
        updateStationUIValues(stationReference);

        StationPanelCloseButton.SetActive(true);
        
    }

    //this method is called from LaunchingObjcts class when player meets enemy station of ship to attack
    public void activateFleetComparePanel(StationController station) {
        CommonButtonAudio.Play(); //player cruiser panel open sound
        //Telematics.Play(); //player cruiser panel open sound
        stationReferenceCompare = station;

        //setting the color of main panel to cyan (chosen standart color for compare fleet panel)
        //holoMatsToFade[0].SetColor("_MainColor", Color.cyan);

        fleetComparePanel.SetActive(true);
        //fleetComparePanel.transform.localPosition = stationIn;

        fightTxt.text = Constants.Instance.getFight();
        YourFleetTxt.text = Constants.Instance.getYourFleet();
        TheirFleetTxt.text = Constants.Instance.getTheirFleet();

        //updating the greeds and fleet information of compared objects
        updateGreedsWhileCompare();

        //activatig the panel function by setting the fade property to 0 and giving a signal to update method for process
        //fade = 0;
        //fleetComparePanel.transform.GetChild(0).GetComponent<Mask>().enabled = true;

        //fleetComparePanel.GetComponent<Animator>().SetBool("openPanel", true);
        fleetComparePanelCloseButton.SetActive(true);
        //so attack button is activated only in case if station has fleet
        if (stationReferenceCompare.ifStationHasCruisers() > 0 && ifPlayerHasACruiser(localLaunchingObjects) > 0) FightButtonComparePanel.SetActive(true);
        else if (stationReferenceCompare.ifStationHasCruisers() > 0 && ifPlayerHasACruiser(localLaunchingObjects) == 0) limitReachedWarning(Constants.Instance.getYouHaveNoCruiserWarning());
        else if (stationReferenceCompare.ifStationHasCruisers() == 0 && ifPlayerHasACruiser(localLaunchingObjects) > 0) limitReachedWarning(Constants.Instance.getTheyHaveNoCruiserWarning());

        anyPanelIsEnabled = true;
        //fadeIn = true;
        FleetComparePanelPanelIsEnabled = true;
    }

    //this method is called publicly from on value change method of slider and it increases time floe or decreases it
    public void manageTheTimeOnJourney() {
        //setting time scale to normal
        if (TimeTextField.text == "x0")
        {
            CommonButtonAudio.Play();
            TimeTextField.text = "x1";
            Time.timeScale = 0;
            Time.fixedDeltaTime = 0;
        }
        //increasing the time flow of scene
        else
        {
            CommonButtonAudio.Play();
            TimeTextField.text = "x0";
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
        }
    }

    //this method sets the correct ported cruiser sprite on opening the panel of station depending on tag of journey cruiser
    private void setPortedCruiserSprite() {
        if (portedCruiserObject.CompareTag("BullCruis4")) {
            currentCruiser.sprite = Cruis4Spr;
        }
        else if (portedCruiserObject.CompareTag("BullCruis3"))
        {
            currentCruiser.sprite = Cruis3Spr;
        }
        else if (portedCruiserObject.CompareTag("BullCruis2"))
        {
            currentCruiser.sprite = Cruis2Spr;
        }
        else if (portedCruiserObject.CompareTag("BullCruis1"))
        {
            currentCruiser.sprite = Cruis1Spr;
        }
    }

    private void passTheFleetOfCruiserToAnother(LaunchingObjcts newOne, LaunchingObjcts oldOne) {
        newOne.Cruis1 = oldOne.Cruis1;
        newOne.Cruis2 = oldOne.Cruis2;
        newOne.Cruis3 = oldOne.Cruis3;
        newOne.Cruis4 = oldOne.Cruis4;
        newOne.Destr1 = oldOne.Destr1;
        newOne.Destr1Par = oldOne.Destr1Par;
        newOne.Destr2 = oldOne.Destr2;
        newOne.Destr2Par = oldOne.Destr2Par;
        newOne.Destr3 = oldOne.Destr3;
        newOne.Destr4 = oldOne.Destr4;
        newOne.Gun1 = oldOne.Gun1;
        newOne.Gun2 = oldOne.Gun2;
        newOne.Gun3 = oldOne.Gun3;
        newOne.MiniGun = oldOne.MiniGun;
    }

    private void changeCruiserSprite(LaunchingObjcts playerCruiserReference)
    {

        //clearing the transforms of energon mngr class transforms references (player ship transforms) before destroying current player ship (for example while changing a scene
        //cruiser of player) to prevent a bug of missing the reference on energonShotMngr class
        //foreach (List<Transform> trList in energonShotAimTransforms) trList.Clear();

        //something is done only if current cruiser is lower that the new transfered to cruiser fleet block
        if (playerCruiserReference.Cruis1 > 0 && playerCruiserReference.currentCruiserLevel != 1)
        {
            LaunchingObjcts newCruiser;
            respawnPointOfCruis = playerCruiserReference.transform.position;
            RotationOfCruis = playerCruiserReference.transform.rotation;
            playerCruiserReference.gameObject.SetActive(false);
            cruiserListToActivatePlayer = ObjectPullerJourney.current.GetCruis1JourneyPlayerPullList();
            cruiserToActivatePlayer = ObjectPullerJourney.current.GetUniversalBullet(cruiserListToActivatePlayer);
            cruiserToActivatePlayer.transform.position = respawnPointOfCruis;
            cruiserToActivatePlayer.transform.rotation = RotationOfCruis;
            newCruiser = cruiserToActivatePlayer.GetComponent<LaunchingObjcts>();

            passTheFleetOfCruiserToAnother(newCruiser, playerCruiserReference);
            playerCruiserReference.makePlayerCruiserDefault();
            newCruiser.currentCruiserLevel = 1;
            cruiserToActivatePlayer.GetComponent<Collider>().enabled = true;
            //this one is necessary to eneable the collider of energy gather child component of player scene cruiser
            foreach (Collider col in cruiserToActivatePlayer.GetComponentsInChildren<Collider>()) col.enabled = true;
            Lists.shipsOnScene.Add(cruiserToActivatePlayer);
            cruiserToActivatePlayer.SetActive(true);
        }
        else if (playerCruiserReference.Cruis2 > 0 && playerCruiserReference.Cruis1 == 0 && playerCruiserReference.currentCruiserLevel != 2  /*&& Lists.JourneyCruisLevel > 2*/)
        {
            LaunchingObjcts newCruiser;
            respawnPointOfCruis = playerCruiserReference.transform.position;
            RotationOfCruis = playerCruiserReference.transform.rotation;
            playerCruiserReference.gameObject.SetActive(false);
            cruiserListToActivatePlayer = ObjectPullerJourney.current.GetCruis2JourneyPlayerPullList();
            cruiserToActivatePlayer = ObjectPullerJourney.current.GetUniversalBullet(cruiserListToActivatePlayer);
            cruiserToActivatePlayer.transform.position = respawnPointOfCruis;
            cruiserToActivatePlayer.transform.rotation = RotationOfCruis;
            newCruiser = cruiserToActivatePlayer.GetComponent<LaunchingObjcts>();
            passTheFleetOfCruiserToAnother(newCruiser, playerCruiserReference);
            playerCruiserReference.makePlayerCruiserDefault();
            newCruiser.currentCruiserLevel = 2;
            cruiserToActivatePlayer.GetComponent<Collider>().enabled = true;
            //this one is necessary to eneable the collider of energy gather child component of player scene cruiser
            foreach (Collider col in cruiserToActivatePlayer.GetComponentsInChildren<Collider>()) col.enabled = true;
            Lists.shipsOnScene.Add(cruiserToActivatePlayer);
            cruiserToActivatePlayer.SetActive(true);
        }
        else if (playerCruiserReference.Cruis3 > 0 && playerCruiserReference.Cruis2 == 0 && playerCruiserReference.Cruis1 == 0 && playerCruiserReference.currentCruiserLevel != 3)
        {
            LaunchingObjcts newCruiser;
            respawnPointOfCruis = playerCruiserReference.transform.position;
            RotationOfCruis = playerCruiserReference.transform.rotation;
            playerCruiserReference.gameObject.SetActive(false);
            cruiserListToActivatePlayer = ObjectPullerJourney.current.GetCruis3JourneyPlayerPullList();
            cruiserToActivatePlayer = ObjectPullerJourney.current.GetUniversalBullet(cruiserListToActivatePlayer);
            cruiserToActivatePlayer.transform.position = respawnPointOfCruis;
            cruiserToActivatePlayer.transform.rotation = RotationOfCruis;
            newCruiser = cruiserToActivatePlayer.GetComponent<LaunchingObjcts>();
            passTheFleetOfCruiserToAnother(newCruiser, playerCruiserReference);
            playerCruiserReference.makePlayerCruiserDefault();
            newCruiser.currentCruiserLevel = 3;
            cruiserToActivatePlayer.GetComponent<Collider>().enabled = true;
            //this one is necessary to eneable the collider of energy gather child component of player scene cruiser
            foreach (Collider col in cruiserToActivatePlayer.GetComponentsInChildren<Collider>()) col.enabled = true;
            Lists.shipsOnScene.Add(cruiserToActivatePlayer);
            cruiserToActivatePlayer.SetActive(true);
        }
        //so the lowes level cruiser will be set only if there are no any higher level cruisers on board and if there are no any cruisers on board at all
        else if (((playerCruiserReference.Cruis3 + playerCruiserReference.Cruis2 + playerCruiserReference.Cruis1) < 1 && playerCruiserReference.currentCruiserLevel != 4)
            || ((playerCruiserReference.Cruis3 + playerCruiserReference.Cruis2 + playerCruiserReference.Cruis1 + playerCruiserReference.Cruis4) < 1 && playerCruiserReference.currentCruiserLevel != 4))
        {
            if ((playerCruiserReference.Destr1 + playerCruiserReference.Destr1Par + playerCruiserReference.Destr2 + playerCruiserReference.Destr2Par + playerCruiserReference.Destr3 + playerCruiserReference.Destr4 +
                playerCruiserReference.Gun1 + playerCruiserReference.Gun2 + playerCruiserReference.Gun3 + playerCruiserReference.MiniGun) < 1)
            {
                playerCruiserReference.makePlayerCruiserDefault();
            }
            else
            {
                LaunchingObjcts newCruiser;
                respawnPointOfCruis = playerCruiserReference.transform.position;
                RotationOfCruis = playerCruiserReference.transform.rotation;
                playerCruiserReference.gameObject.SetActive(false);
                cruiserListToActivatePlayer = ObjectPullerJourney.current.GetCruis4JourneyPlayerPullList();
                cruiserToActivatePlayer = ObjectPullerJourney.current.GetUniversalBullet(cruiserListToActivatePlayer);
                cruiserToActivatePlayer.transform.position = respawnPointOfCruis;
                cruiserToActivatePlayer.transform.rotation = RotationOfCruis;
                newCruiser = cruiserToActivatePlayer.GetComponent<LaunchingObjcts>();
                passTheFleetOfCruiserToAnother(newCruiser, playerCruiserReference);
                playerCruiserReference.makePlayerCruiserDefault();
                newCruiser.currentCruiserLevel = 4;
                cruiserToActivatePlayer.GetComponent<Collider>().enabled = true;
                //this one is necessary to eneable the collider of energy gather child component of player scene cruiser
                foreach (Collider col in cruiserToActivatePlayer.GetComponentsInChildren<Collider>()) col.enabled = true;
                cruiserToActivatePlayer.SetActive(true);
                Lists.shipsOnScene.Add(cruiserToActivatePlayer);
            }
        }

    }

    //this method changes ported cruiser sprite while player exchanges with ships on station panel and changes the journey cruiser game object as well
    private void changePortedCruiserSprite() {

        //clearing the transforms of energon mngr class transforms references (player ship transforms) before destroying current player ship (for example while changing a scene
        //cruiser of player) to prevent a bug of missing the reference on energonShotMngr class
        //foreach (List<Transform> trList in energonShotAimTransforms) trList.Clear();

        //something is done only if current cruiser is lower that the new transfered to cruiser fleet block
        if (portedCruiserObject.Cruis1 > 0 && portedCruiserObject.currentCruiserLevel != 1)
        {
            LaunchingObjcts newCruiser;
            currentCruiser.sprite = Cruis1Spr;
            respawnPointOfCruis = portedCruiserObject.transform.position;
            RotationOfCruis = portedCruiserObject.transform.rotation;
            stationReference.playerCruiserObject.Remove(portedCruiserObject);
            portedCruiserObject.disactivatingCurrentShipNoBurst();
            cruiserListToActivatePlayer = ObjectPullerJourney.current.GetCruis1JourneyPlayerPullList();
            cruiserToActivatePlayer = ObjectPullerJourney.current.GetUniversalBullet(cruiserListToActivatePlayer);
            cruiserToActivatePlayer.transform.position = respawnPointOfCruis;
            cruiserToActivatePlayer.transform.rotation = RotationOfCruis;
            newCruiser = cruiserToActivatePlayer.GetComponent<LaunchingObjcts>();

            stationReference.playerCruiserObject.Add(newCruiser);
            newCruiser.isPortedToPlayerStation=true;

            passTheFleetOfCruiserToAnother(newCruiser, portedCruiserObject);
            portedCruiserObject.makePlayerCruiserDefault();
            portedCruiserObject = newCruiser;
            portedCruiserObject.currentCruiserLevel = 1;
            //cruiserToActivate = Instantiate(Cruis1Journey, respawnPointOfCruis, RotationOfCruis);
            cruiserToActivatePlayer.GetComponent<Collider>().enabled = true;
            //this one is necessary to eneable the collider of energy gather child component of player scene cruiser
            foreach (Collider col in cruiserToActivatePlayer.GetComponentsInChildren<Collider>()) col.enabled = true;
            Lists.shipsOnScene.Add(cruiserToActivatePlayer);
            cruiserToActivatePlayer.SetActive(true);
        }
        else if (portedCruiserObject.Cruis2 > 0 && portedCruiserObject.Cruis1 == 0 && portedCruiserObject.currentCruiserLevel != 2  /*&& Lists.JourneyCruisLevel > 2*/) {
            LaunchingObjcts newCruiser;
            currentCruiser.sprite = Cruis2Spr;
            respawnPointOfCruis = portedCruiserObject.transform.position;
            RotationOfCruis = portedCruiserObject.transform.rotation;
            stationReference.playerCruiserObject.Remove(portedCruiserObject);
            portedCruiserObject.disactivatingCurrentShipNoBurst();
            cruiserListToActivatePlayer = ObjectPullerJourney.current.GetCruis2JourneyPlayerPullList();
            cruiserToActivatePlayer = ObjectPullerJourney.current.GetUniversalBullet(cruiserListToActivatePlayer);
            cruiserToActivatePlayer.transform.position = respawnPointOfCruis;
            cruiserToActivatePlayer.transform.rotation = RotationOfCruis;
            newCruiser = cruiserToActivatePlayer.GetComponent<LaunchingObjcts>();
            stationReference.playerCruiserObject.Add(newCruiser);
            newCruiser.isPortedToPlayerStation = true;
            passTheFleetOfCruiserToAnother(newCruiser, portedCruiserObject);
            portedCruiserObject.makePlayerCruiserDefault();
            portedCruiserObject = newCruiser;
            portedCruiserObject.currentCruiserLevel = 2;
            //cruiserToActivate = Instantiate(Cruis1Journey, respawnPointOfCruis, RotationOfCruis);
            cruiserToActivatePlayer.GetComponent<Collider>().enabled = true;
            //this one is necessary to eneable the collider of energy gather child component of player scene cruiser
            foreach (Collider col in cruiserToActivatePlayer.GetComponentsInChildren<Collider>()) col.enabled = true;
            Lists.shipsOnScene.Add(cruiserToActivatePlayer);
            cruiserToActivatePlayer.SetActive(true);
        }
        else if (portedCruiserObject.Cruis3 > 0 && portedCruiserObject.Cruis2 == 0 && portedCruiserObject.Cruis1 == 0 && portedCruiserObject.currentCruiserLevel != 3)
        {
            LaunchingObjcts newCruiser;
            currentCruiser.sprite = Cruis3Spr;
            respawnPointOfCruis = portedCruiserObject.transform.position;
            RotationOfCruis = portedCruiserObject.transform.rotation;
            stationReference.playerCruiserObject.Remove(portedCruiserObject);
            portedCruiserObject.disactivatingCurrentShipNoBurst();
            cruiserListToActivatePlayer = ObjectPullerJourney.current.GetCruis3JourneyPlayerPullList();
            cruiserToActivatePlayer = ObjectPullerJourney.current.GetUniversalBullet(cruiserListToActivatePlayer);
            cruiserToActivatePlayer.transform.position = respawnPointOfCruis;
            cruiserToActivatePlayer.transform.rotation = RotationOfCruis;
            newCruiser = cruiserToActivatePlayer.GetComponent<LaunchingObjcts>();
            stationReference.playerCruiserObject.Add(newCruiser);
            newCruiser.isPortedToPlayerStation = true;
            passTheFleetOfCruiserToAnother(newCruiser, portedCruiserObject);
            portedCruiserObject.makePlayerCruiserDefault();
            portedCruiserObject = newCruiser;
            portedCruiserObject.currentCruiserLevel = 3;
            //cruiserToActivate = Instantiate(Cruis1Journey, respawnPointOfCruis, RotationOfCruis);
            cruiserToActivatePlayer.GetComponent<Collider>().enabled = true;
            //this one is necessary to eneable the collider of energy gather child component of player scene cruiser
            foreach (Collider col in cruiserToActivatePlayer.GetComponentsInChildren<Collider>()) col.enabled = true;
            Lists.shipsOnScene.Add(cruiserToActivatePlayer);
            cruiserToActivatePlayer.SetActive(true);
        }
        //so the lowes level cruiser will be set only if there are no any higher level cruisers on board and if there are no any cruisers on board at all
        else if (((portedCruiserObject.Cruis3 + portedCruiserObject.Cruis2 + portedCruiserObject.Cruis1) < 1 && portedCruiserObject.currentCruiserLevel != 4)
            || ((portedCruiserObject.Cruis3 + portedCruiserObject.Cruis2 + portedCruiserObject.Cruis1 + portedCruiserObject.Cruis4) < 1 && portedCruiserObject.currentCruiserLevel != 4))
        {
            if ((portedCruiserObject.Destr1 + portedCruiserObject.Destr1Par + portedCruiserObject.Destr2 + portedCruiserObject.Destr2Par + portedCruiserObject.Destr3 + portedCruiserObject.Destr4 +
                portedCruiserObject.Gun1 + portedCruiserObject.Gun2 + portedCruiserObject.Gun3 + portedCruiserObject.MiniGun) < 1)
            {
                StationPortObject.SetActive(false);
                PortedCruiserObj.SetActive(false);
                stationReference.playerCruiserObject.Remove(portedCruiserObject);
                portedCruiserObject.makePlayerCruiserDefault();
                portedCruiserObject.disactivatingCurrentShipNoBurst();
                portedCruiserObject = null;
                //it is necessary to lclose the panel of station to connect next cruiser that ported to station
                if (stationReference.playerCruiserObject.Count > 0) closeAnyPanel();
            }
            else
            {
                LaunchingObjcts newCruiser;
                currentCruiser.sprite = Cruis4Spr;
                respawnPointOfCruis = portedCruiserObject.transform.position;
                RotationOfCruis = portedCruiserObject.transform.rotation;
                stationReference.playerCruiserObject.Remove(portedCruiserObject);
                portedCruiserObject.disactivatingCurrentShipNoBurst();
                cruiserListToActivatePlayer = ObjectPullerJourney.current.GetCruis4JourneyPlayerPullList();
                cruiserToActivatePlayer = ObjectPullerJourney.current.GetUniversalBullet(cruiserListToActivatePlayer);
                cruiserToActivatePlayer.transform.position = respawnPointOfCruis;
                cruiserToActivatePlayer.transform.rotation = RotationOfCruis;
                newCruiser = cruiserToActivatePlayer.GetComponent<LaunchingObjcts>();
                stationReference.playerCruiserObject.Add(newCruiser);
                newCruiser.isPortedToPlayerStation = true;
                passTheFleetOfCruiserToAnother(newCruiser, portedCruiserObject);
                portedCruiserObject.makePlayerCruiserDefault();
                portedCruiserObject = newCruiser;
                portedCruiserObject.currentCruiserLevel = 4;
                //cruiserToActivate = Instantiate(Cruis1Journey, respawnPointOfCruis, RotationOfCruis);
                cruiserToActivatePlayer.GetComponent<Collider>().enabled = true;
                //this one is necessary to eneable the collider of energy gather child component of player scene cruiser
                foreach (Collider col in cruiserToActivatePlayer.GetComponentsInChildren<Collider>()) col.enabled = true;
                cruiserToActivatePlayer.SetActive(true);
                Lists.shipsOnScene.Add(cruiserToActivatePlayer);
            }
        }

    }

    private void updateStationGreeds() {
        //setting zero all greed sizes to make them ready for next opening of station panel
        fleetGreedSizeStation = 0;
        ProductGreedSizeStation = 0;
        PortGreedSizeStation = 0;

        #region setting greed UI icons and greed sizes in common

        #region setting greed UI icons and greed size of station fleet
        //setting station fleet UI parameters and setting it's greed size
        if (stationReference.Cruis1 > 0)
        {
            cruis1ValueStation.transform.parent.gameObject.SetActive(true);
            cruis1ValueStation.text = stationReference.Cruis1.ToString();
            fleetGreedSizeStation++;
        }
        else cruis1ValueStation.transform.parent.gameObject.SetActive(false);

        if (stationReference.Cruis2 > 0)
        {
            cruis2ValueStation.transform.parent.gameObject.SetActive(true);
            cruis2ValueStation.text = stationReference.Cruis2.ToString();
            fleetGreedSizeStation++;
        }
        else cruis2ValueStation.transform.parent.gameObject.SetActive(false);

        if (stationReference.Cruis3 > 0)
        {
            cruis3ValueStation.transform.parent.gameObject.SetActive(true);
            cruis3ValueStation.text = stationReference.Cruis3.ToString();
            fleetGreedSizeStation++;
        }
        else cruis3ValueStation.transform.parent.gameObject.SetActive(false);

        if (stationReference.Cruis4 > 0)
        {
            cruis4ValueStation.transform.parent.gameObject.SetActive(true);
            cruis4ValueStation.text = stationReference.Cruis4.ToString();
            fleetGreedSizeStation++;
        }
        else cruis4ValueStation.transform.parent.gameObject.SetActive(false);

        if (stationReference.Destr1 > 0)
        {
            destr1ValueStation.transform.parent.gameObject.SetActive(true);
            destr1ValueStation.text = stationReference.Destr1.ToString();
            fleetGreedSizeStation++;
        }
        else destr1ValueStation.transform.parent.gameObject.SetActive(false);

        if (stationReference.Destr1Par > 0)
        {
            destr1ParValueStation.transform.parent.gameObject.SetActive(true);
            destr1ParValueStation.text = stationReference.Destr1Par.ToString();
            fleetGreedSizeStation++;
        }
        else destr1ParValueStation.transform.parent.gameObject.SetActive(false);

        if (stationReference.Destr2 > 0)
        {
            destr2ValueStation.transform.parent.gameObject.SetActive(true);
            destr2ValueStation.text = stationReference.Destr2.ToString();
            fleetGreedSizeStation++;
        }
        else destr2ValueStation.transform.parent.gameObject.SetActive(false);

        if (stationReference.Destr2Par > 0)
        {
            destr2ParValueStation.transform.parent.gameObject.SetActive(true);
            destr2ParValueStation.text = stationReference.Destr2Par.ToString();
            fleetGreedSizeStation++;
        }
        else destr2ParValueStation.transform.parent.gameObject.SetActive(false);

        if (stationReference.Destr3 > 0)
        {
            destr3ValueStation.transform.parent.gameObject.SetActive(true);
            destr3ValueStation.text = stationReference.Destr3.ToString();
            fleetGreedSizeStation++;
        }
        else destr3ValueStation.transform.parent.gameObject.SetActive(false);

        if (stationReference.Destr4 > 0)
        {
            destr4ValueStation.transform.parent.gameObject.SetActive(true);
            destr4ValueStation.text = stationReference.Destr4.ToString();
            fleetGreedSizeStation++;
        }
        else destr4ValueStation.transform.parent.gameObject.SetActive(false);

        if (stationReference.Gun1 > 0)
        {
            gun1ValueStation.transform.parent.gameObject.SetActive(true);
            gun1ValueStation.text = stationReference.Gun1.ToString();
            fleetGreedSizeStation++;
        }
        else gun1ValueStation.transform.parent.gameObject.SetActive(false);

        if (stationReference.Gun2 > 0)
        {
            gun2ValueStation.transform.parent.gameObject.SetActive(true);
            gun2ValueStation.text = stationReference.Gun2.ToString();
            fleetGreedSizeStation++;
        }
        else gun2ValueStation.transform.parent.gameObject.SetActive(false);

        if (stationReference.Gun3 > 0)
        {
            gun3ValueStation.transform.parent.gameObject.SetActive(true);
            gun3ValueStation.text = stationReference.Gun3.ToString();
            fleetGreedSizeStation++;
        }
        else gun3ValueStation.transform.parent.gameObject.SetActive(false);

        if (stationReference.MiniGun > 0)
        {
            MiniGunValueStation.transform.parent.gameObject.SetActive(true);
            MiniGunValueStation.text = stationReference.MiniGun.ToString();
            fleetGreedSizeStation++;
        }
        else MiniGunValueStation.transform.parent.gameObject.SetActive(false);

        #endregion setting greed UI icons and greed size of station fleet

        #region setting greed UI icons and greed size of ships that station may produce
        //setting station production values UI parameters and setting it's greed size (TODO WITH OTHER STATION TYPES)

        if (stationReference.stationCurrentLevel == 0)
        {
            cruis1ProductStation.transform.parent.gameObject.SetActive(false);
            cruis2ProductStation.transform.parent.gameObject.SetActive(false);
            cruis3ProductStation.transform.parent.gameObject.SetActive(false);
            destr1ProductStation.transform.parent.gameObject.SetActive(false);
            destr1ParProductStation.transform.parent.gameObject.SetActive(false);
            destr2ProductStation.transform.parent.gameObject.SetActive(false);
            destr2ParProductStation.transform.parent.gameObject.SetActive(false);
            destr3ProductStation.transform.parent.gameObject.SetActive(false);
            gun1ProductStation.transform.parent.gameObject.SetActive(false);
            gun2ProductStation.transform.parent.gameObject.SetActive(false);
            gun3ProductStation.transform.parent.gameObject.SetActive(false);
            MiniGunProductStation.transform.parent.gameObject.SetActive(false);

            cruis4ProductStation.transform.parent.gameObject.SetActive(true);
            cruis4ProductStation.text = stationReference.Cruis4Produc.ToString();
            ProductGreedSizeStation++;

            destr4ProductStation.transform.parent.gameObject.SetActive(true);
            destr4ProductStation.text = stationReference.Destr4Produc.ToString();
            ProductGreedSizeStation++;
        }
        else if (stationReference.stationCurrentLevel == 1)
        {
            cruis1ProductStation.transform.parent.gameObject.SetActive(false);
            cruis2ProductStation.transform.parent.gameObject.SetActive(false);
            destr1ProductStation.transform.parent.gameObject.SetActive(false);
            destr1ParProductStation.transform.parent.gameObject.SetActive(false);
            destr2ProductStation.transform.parent.gameObject.SetActive(false);
            destr2ParProductStation.transform.parent.gameObject.SetActive(false);
            gun2ProductStation.transform.parent.gameObject.SetActive(false);
            gun3ProductStation.transform.parent.gameObject.SetActive(false);
            MiniGunProductStation.transform.parent.gameObject.SetActive(false);

            cruis4ProductStation.transform.parent.gameObject.SetActive(true);
            cruis4ProductStation.text = stationReference.Cruis4Produc.ToString();
            ProductGreedSizeStation++;

            cruis3ProductStation.transform.parent.gameObject.SetActive(true);
            cruis3ProductStation.text = stationReference.Cruis3Produc.ToString();
            ProductGreedSizeStation++;

            destr4ProductStation.transform.parent.gameObject.SetActive(true);
            destr4ProductStation.text = stationReference.Destr4Produc.ToString();
            ProductGreedSizeStation++;

            destr3ProductStation.transform.parent.gameObject.SetActive(true);
            destr3ProductStation.text = stationReference.Destr3Produc.ToString();
            ProductGreedSizeStation++;

            gun1ProductStation.transform.parent.gameObject.SetActive(true);
            gun1ProductStation.text = stationReference.Gun1Produc.ToString();
            ProductGreedSizeStation++;
        }
        else if (stationReference.stationCurrentLevel == 2)
        {
            cruis1ProductStation.transform.parent.gameObject.SetActive(false);
            destr1ProductStation.transform.parent.gameObject.SetActive(false);
            destr1ParProductStation.transform.parent.gameObject.SetActive(false);
            gun3ProductStation.transform.parent.gameObject.SetActive(false);

            cruis4ProductStation.transform.parent.gameObject.SetActive(true);
            cruis4ProductStation.text = stationReference.Cruis4Produc.ToString();
            ProductGreedSizeStation++;

            cruis3ProductStation.transform.parent.gameObject.SetActive(true);
            cruis3ProductStation.text = stationReference.Cruis3Produc.ToString();
            ProductGreedSizeStation++;

            cruis2ProductStation.transform.parent.gameObject.SetActive(true);
            cruis2ProductStation.text = stationReference.Cruis2Produc.ToString();
            ProductGreedSizeStation++;

            destr4ProductStation.transform.parent.gameObject.SetActive(true);
            destr4ProductStation.text = stationReference.Destr4Produc.ToString();
            ProductGreedSizeStation++;

            destr3ProductStation.transform.parent.gameObject.SetActive(true);
            destr3ProductStation.text = stationReference.Destr3Produc.ToString();
            ProductGreedSizeStation++;

            destr2ProductStation.transform.parent.gameObject.SetActive(true);
            destr2ProductStation.text = stationReference.Destr2Produc.ToString();
            ProductGreedSizeStation++;

            destr2ParProductStation.transform.parent.gameObject.SetActive(true);
            destr2ParProductStation.text = stationReference.Destr2ProducPar.ToString();
            ProductGreedSizeStation++;

            gun1ProductStation.transform.parent.gameObject.SetActive(true);
            gun1ProductStation.text = stationReference.Gun1Produc.ToString();
            ProductGreedSizeStation++;

            gun2ProductStation.transform.parent.gameObject.SetActive(true);
            gun2ProductStation.text = stationReference.Gun2Produc.ToString();
            ProductGreedSizeStation++;

            MiniGunProductStation.transform.parent.gameObject.SetActive(true);
            MiniGunProductStation.text = stationReference.MiniGunProduc.ToString();
            ProductGreedSizeStation++;
        }
        else if (stationReference.stationCurrentLevel == 3)
        {
            cruis4ProductStation.transform.parent.gameObject.SetActive(true);
            cruis4ProductStation.text = stationReference.Cruis4Produc.ToString();
            ProductGreedSizeStation++;

            cruis3ProductStation.transform.parent.gameObject.SetActive(true);
            cruis3ProductStation.text = stationReference.Cruis3Produc.ToString();
            ProductGreedSizeStation++;

            cruis2ProductStation.transform.parent.gameObject.SetActive(true);
            cruis2ProductStation.text = stationReference.Cruis2Produc.ToString();
            ProductGreedSizeStation++;

            cruis1ProductStation.transform.parent.gameObject.SetActive(true);
            cruis1ProductStation.text = stationReference.Cruis1Produc.ToString();
            ProductGreedSizeStation++;

            destr4ProductStation.transform.parent.gameObject.SetActive(true);
            destr4ProductStation.text = stationReference.Destr4Produc.ToString();
            ProductGreedSizeStation++;

            destr3ProductStation.transform.parent.gameObject.SetActive(true);
            destr3ProductStation.text = stationReference.Destr3Produc.ToString();
            ProductGreedSizeStation++;

            destr2ProductStation.transform.parent.gameObject.SetActive(true);
            destr2ProductStation.text = stationReference.Destr2Produc.ToString();
            ProductGreedSizeStation++;

            destr2ParProductStation.transform.parent.gameObject.SetActive(true);
            destr2ParProductStation.text = stationReference.Destr2ProducPar.ToString();
            ProductGreedSizeStation++;

            destr1ProductStation.transform.parent.gameObject.SetActive(true);
            destr1ProductStation.text = stationReference.Destr1Produc.ToString();
            ProductGreedSizeStation++;

            destr1ParProductStation.transform.parent.gameObject.SetActive(true);
            destr1ParProductStation.text = stationReference.Destr1ProducPar.ToString();
            ProductGreedSizeStation++;

            gun1ProductStation.transform.parent.gameObject.SetActive(true);
            gun1ProductStation.text = stationReference.Gun1Produc.ToString();
            ProductGreedSizeStation++;

            gun2ProductStation.transform.parent.gameObject.SetActive(true);
            gun2ProductStation.text = stationReference.Gun2Produc.ToString();
            ProductGreedSizeStation++;

            gun3ProductStation.transform.parent.gameObject.SetActive(true);
            gun3ProductStation.text = stationReference.Gun3Produc.ToString();
            ProductGreedSizeStation++;

            MiniGunProductStation.transform.parent.gameObject.SetActive(true);
            MiniGunProductStation.text = stationReference.MiniGunProduc.ToString();
            ProductGreedSizeStation++;
        }

        #endregion setting greed UI icons and greed size of ships that station may produce

        #region setting greed UI icons and greed size of ships that pordet cruiser has
        if (portedCruiserObject != null)
        {
            if (portedCruiserObject.Cruis1 > 0)
            {
                cruis1PortStation.transform.parent.gameObject.SetActive(true);
                cruis1PortStation.text = portedCruiserObject.Cruis1.ToString();
                PortGreedSizeStation++;
            }
            else cruis1PortStation.transform.parent.gameObject.SetActive(false);

            if (portedCruiserObject.Cruis2 > 0)
            {
                cruis2PortStation.transform.parent.gameObject.SetActive(true);
                cruis2PortStation.text = portedCruiserObject.Cruis2.ToString();
                PortGreedSizeStation++;
            }
            else cruis2PortStation.transform.parent.gameObject.SetActive(false);

            if (portedCruiserObject.Cruis3 > 0)
            {
                cruis3PortStation.transform.parent.gameObject.SetActive(true);
                cruis3PortStation.text = portedCruiserObject.Cruis3.ToString();
                PortGreedSizeStation++;
            }
            else cruis3PortStation.transform.parent.gameObject.SetActive(false);

            if (portedCruiserObject.Cruis4 > 0)
            {
                cruis4PortStation.transform.parent.gameObject.SetActive(true);
                cruis4PortStation.text = portedCruiserObject.Cruis4.ToString();
                PortGreedSizeStation++;
            }
            else cruis4PortStation.transform.parent.gameObject.SetActive(false);

            if (portedCruiserObject.Destr1 > 0)
            {
                destr1PortStation.transform.parent.gameObject.SetActive(true);
                destr1PortStation.text = portedCruiserObject.Destr1.ToString();
                PortGreedSizeStation++;
            }
            else destr1PortStation.transform.parent.gameObject.SetActive(false);

            if (portedCruiserObject.Destr1Par > 0)
            {
                destr1ParPortStation.transform.parent.gameObject.SetActive(true);
                destr1ParPortStation.text = portedCruiserObject.Destr1Par.ToString();
                PortGreedSizeStation++;
            }
            else destr1ParPortStation.transform.parent.gameObject.SetActive(false);

            if (portedCruiserObject.Destr2 > 0)
            {
                destr2PortStation.transform.parent.gameObject.SetActive(true);
                destr2PortStation.text = portedCruiserObject.Destr2.ToString();
                PortGreedSizeStation++;
            }
            else destr2PortStation.transform.parent.gameObject.SetActive(false);

            if (portedCruiserObject.Destr2Par > 0)
            {
                destr2ParPortStation.transform.parent.gameObject.SetActive(true);
                destr2ParPortStation.text = portedCruiserObject.Destr2Par.ToString();
                PortGreedSizeStation++;
            }
            else destr2ParPortStation.transform.parent.gameObject.SetActive(false);

            if (portedCruiserObject.Destr3 > 0)
            {
                destr3PortStation.transform.parent.gameObject.SetActive(true);
                destr3PortStation.text = portedCruiserObject.Destr3.ToString();
                PortGreedSizeStation++;
            }
            else destr3PortStation.transform.parent.gameObject.SetActive(false);

            if (portedCruiserObject.Destr4 > 0)
            {
                destr4PortStation.transform.parent.gameObject.SetActive(true);
                destr4PortStation.text = portedCruiserObject.Destr4.ToString();
                PortGreedSizeStation++;
            }
            else destr4PortStation.transform.parent.gameObject.SetActive(false);

            if (portedCruiserObject.Gun1 > 0)
            {
                gun1PortStation.transform.parent.gameObject.SetActive(true);
                gun1PortStation.text = portedCruiserObject.Gun1.ToString();
                PortGreedSizeStation++;
            }
            else gun1PortStation.transform.parent.gameObject.SetActive(false);

            if (portedCruiserObject.Gun2 > 0)
            {
                gun2PortStation.transform.parent.gameObject.SetActive(true);
                gun2PortStation.text = portedCruiserObject.Gun2.ToString();
                PortGreedSizeStation++;
            }
            else gun2PortStation.transform.parent.gameObject.SetActive(false);

            if (portedCruiserObject.Gun3 > 0)
            {
                gun3PortStation.transform.parent.gameObject.SetActive(true);
                gun3PortStation.text = portedCruiserObject.Gun3.ToString();
                PortGreedSizeStation++;
            }
            else gun3PortStation.transform.parent.gameObject.SetActive(false);

            if (portedCruiserObject.MiniGun > 0)
            {
                MiniGunPortStation.transform.parent.gameObject.SetActive(true);
                MiniGunPortStation.text = portedCruiserObject.MiniGun.ToString();
                PortGreedSizeStation++;
            }
            else MiniGunPortStation.transform.parent.gameObject.SetActive(false);
        }
        #endregion setting greed UI icons and greed size of ships that pordet cruiser has

        //increase or decreace the width of greed dynamically depending on count of ship types on differend greeds
        StationFleetGreedRect.sizeDelta = new Vector2(fleetGreedSizeStation * 180, 100f);
        StationProductGreedRect.sizeDelta = new Vector2(ProductGreedSizeStation * 180, 100f);
        StationPortGreedRect.sizeDelta = new Vector2(PortGreedSizeStation * 180, 100f);

        #endregion setting greed UI icons and greed sizes in common
    }

    public void updateStationUIValues(StationController stationController) {

        cruis1ValueStation.text = stationController.Cruis1.ToString();
        cruis2ValueStation.text = stationController.Cruis2.ToString();
        cruis3ValueStation.text = stationController.Cruis3.ToString();
        cruis4ValueStation.text = stationController.Cruis4.ToString();
        destr1ValueStation.text = stationController.Destr1.ToString();
        destr1ParValueStation.text = stationController.Destr1Par.ToString();
        destr2ValueStation.text = stationController.Destr2.ToString();
        destr2ParValueStation.text = stationController.Destr2Par.ToString();
        destr3ValueStation.text = stationController.Destr3.ToString();
        destr4ValueStation.text = stationController.Destr4.ToString();
        gun1ValueStation.text = stationController.Gun1.ToString();
        gun2ValueStation.text = stationController.Gun2.ToString();
        gun3ValueStation.text = stationController.Gun3.ToString();
        MiniGunValueStation.text = stationController.MiniGun.ToString();

        if (stationPanelIsEnabled)
        {
            cruis1ProductStation.text = stationController.Cruis1Produc.ToString();
            cruis2ProductStation.text = stationController.Cruis2Produc.ToString();
            cruis3ProductStation.text = stationController.Cruis3Produc.ToString();
            cruis4ProductStation.text = stationController.Cruis4Produc.ToString();
            destr1ProductStation.text = stationController.Destr1Produc.ToString();
            destr1ParProductStation.text = stationController.Destr1ProducPar.ToString();
            destr2ProductStation.text = stationController.Destr2Produc.ToString();
            destr2ParProductStation.text = stationController.Destr2ProducPar.ToString();
            destr3ProductStation.text = stationController.Destr3Produc.ToString();
            destr4ProductStation.text = stationController.Destr4Produc.ToString();
            gun1ProductStation.text = stationController.Gun1Produc.ToString();
            gun2ProductStation.text = stationController.Gun2Produc.ToString();
            gun3ProductStation.text = stationController.Gun3Produc.ToString();
            MiniGunProductStation.text = stationController.MiniGunProduc.ToString();

            //setting current production ship or energy sprite to fill sprites on opening the panel of station
            //if (stationController.CurrentProducedShipLocal == C1) {
            //    currenProductionBckgr.sprite = Cruis1Spr;
            //    currenProductionFill.sprite = Cruis1Spr;
            //}
            //else if (stationController.CurrentProducedShipLocal == C2)
            //{
            //    currenProductionBckgr.sprite = Cruis2Spr;
            //    currenProductionFill.sprite = Cruis2Spr;
            //}
            //else if (stationController.CurrentProducedShipLocal == C3)
            //{
            //    currenProductionBckgr.sprite = Cruis3Spr;
            //    currenProductionFill.sprite = Cruis3Spr;
            //}
            //else if (stationController.CurrentProducedShipLocal == C4)
            //{
            //    currenProductionBckgr.sprite = Cruis4Spr;
            //    currenProductionFill.sprite = Cruis4Spr;
            //}
            //else if (stationController.CurrentProducedShipLocal == D1)
            //{
            //    currenProductionBckgr.sprite = Destr1Spr;
            //    currenProductionFill.sprite = Destr1Spr;
            //}
            //else if (stationController.CurrentProducedShipLocal == D1P)
            //{
            //    currenProductionBckgr.sprite = Destr1ParSpr;
            //    currenProductionFill.sprite = Destr1ParSpr;
            //}
            //else if (stationController.CurrentProducedShipLocal == D2)
            //{
            //    currenProductionBckgr.sprite = Destr2Spr;
            //    currenProductionFill.sprite = Destr2Spr;
            //}
            //else if (stationController.CurrentProducedShipLocal == D2P)
            //{
            //    currenProductionBckgr.sprite = Destr2ParSpr;
            //    currenProductionFill.sprite = Destr2ParSpr;
            //}
            //else if (stationController.CurrentProducedShipLocal == D3)
            //{
            //    currenProductionBckgr.sprite = Destr3Spr;
            //    currenProductionFill.sprite = Destr3Spr;
            //}
            //else if (stationController.CurrentProducedShipLocal == D4)
            //{
            //    currenProductionBckgr.sprite = Destr4Spr;
            //    currenProductionFill.sprite = Destr4Spr;
            //}
            //else if (stationController.CurrentProducedShipLocal == G1)
            //{
            //    currenProductionBckgr.sprite = Gun1Spr;
            //    currenProductionFill.sprite = Gun1Spr;
            //}
            //else if (stationController.CurrentProducedShipLocal == G2)
            //{
            //    currenProductionBckgr.sprite = Gun2Spr;
            //    currenProductionFill.sprite = Gun2Spr;
            //}
            //else if (stationController.CurrentProducedShipLocal == G3)
            //{
            //    currenProductionBckgr.sprite = Gun3Spr;
            //    currenProductionFill.sprite = Gun3Spr;
            //}
            //else if (stationController.CurrentProducedShipLocal == GM)
            //{
            //    currenProductionBckgr.sprite = MiniGunSpr;
            //    currenProductionFill.sprite = MiniGunSpr;
            //}
            //else if (stationController.CurrentProducedShipLocal == EN)
            //{
            //    currenProductionBckgr.sprite = EnergySpr;
            //    currenProductionFill.sprite = EnergySpr;
            //}
        }
    }

    private void updatePortedShipUIValues()
    {
        cruis1PortStation.text = portedCruiserObject.Cruis1.ToString();
        cruis2PortStation.text = portedCruiserObject.Cruis2.ToString();
        cruis3PortStation.text = portedCruiserObject.Cruis3.ToString();
        cruis4PortStation.text = portedCruiserObject.Cruis4.ToString();
        destr1PortStation.text = portedCruiserObject.Destr1.ToString();
        destr1ParPortStation.text = portedCruiserObject.Destr1Par.ToString();
        destr2PortStation.text = portedCruiserObject.Destr2.ToString();
        destr2ParPortStation.text = portedCruiserObject.Destr2Par.ToString();
        destr3PortStation.text = portedCruiserObject.Destr3.ToString();
        destr4PortStation.text = portedCruiserObject.Destr4.ToString();
        gun1PortStation.text = portedCruiserObject.Gun1.ToString();
        gun2PortStation.text = portedCruiserObject.Gun2.ToString();
        gun3PortStation.text = portedCruiserObject.Gun3.ToString();
        MiniGunPortStation.text = portedCruiserObject.MiniGun.ToString();
    }


    //this method is used to use upgrade the station and it is assigned to according button on station panel UI
    public void upgradeStation()
    {
        //so here one more check if player has enough energy to upgrade even if the button is ready to upgrade, otherwise the button will be disactivated
        if (Lists.energyOfPlayer >= stationReference.nextUpgradeEnergyCount)
        {
            //switch from level 0 station to level 1
            if (stationReference.stationCurrentLevel == 0)
            {
                //withdrawing necessary enegry amount to upgrade the station
                Lists.energyOfPlayer -= Constants.Instance.enrgy0to1Upgrd;
                energyCount.text = Lists.energyOfPlayer.ToString("0"); //upgrading UI that displays energy amount of player

                //setting according sprites to upgrade icon (next generation station)
                stationIcon.sprite = Station2Spr;
                stationIconBkgr.sprite = Station2Spr;

                //hiding upgrade text (the one that above the upgrade icon) and disabling the button component of upgrade station icons
                //upgradeText.gameObject.SetActive(false);
                upgradeText.text = " ";
                upgradeText.transform.parent.GetComponent<Button>().enabled = false;

                //set fill amount of upgrade icon to zero to fill it after on update method
                stationIcon.fillAmount = 0;

                //setting upgrade time step according to the type of upgrade and setting zero the filling amount that will be filled on upgrade method of StationController
                //with step of currentUpgradeTime
                stationReference.currentUpgradeTime = Constants.Instance.time0to1Upgrd;
                stationReference.upgradeFill = 0;

                stationReference.isUpgrading = true; //start the upgrading process by setting true according bool true on according station instance
            }
            //switch from level 1 station to level 2
            else if (stationReference.stationCurrentLevel == 1)
            {
                //withdrawing necessary enegry amount to upgrade the station
                Lists.energyOfPlayer -= Constants.Instance.enrgy1to2Upgrd;
                energyCount.text = Lists.energyOfPlayer.ToString("0"); //upgrading UI that displays energy amount of player

                //setting according sprites to upgrade icon (next generation station)
                stationIcon.sprite = Station3Spr;
                stationIconBkgr.sprite = Station3Spr;

                //hiding upgrade text (the one that above the upgrade icon) and disabling the button component of upgrade station icons
                //upgradeText.gameObject.SetActive(false);
                upgradeText.text = " ";
                upgradeText.transform.parent.GetComponent<Button>().enabled = false;

                //set fill amount of upgrade icon to zero to fill it after on update method
                stationIcon.fillAmount = 0;

                //setting upgrade time step according to the type of upgrade and setting zero the filling amount that will be filled on upgrade method of StationController
                //with step of currentUpgradeTime
                stationReference.currentUpgradeTime = Constants.Instance.time1to2Upgrd;
                stationReference.upgradeFill = 0;

                stationReference.isUpgrading = true; //start the upgrading process by setting true according bool true on according station instance
            }
            //switch from level 2 station to level 3
            else if (stationReference.stationCurrentLevel == 2)
            {
                //withdrawing necessary enegry amount to upgrade the station
                Lists.energyOfPlayer -= Constants.Instance.enrgy2to3Upgrd;
                energyCount.text = Lists.energyOfPlayer.ToString("0"); //upgrading UI that displays energy amount of player

                //setting according sprites to upgrade icon (next generation station)
                stationIcon.sprite = Station4Spr;
                stationIconBkgr.sprite = Station4Spr;

                //hiding upgrade text (the one that above the upgrade icon) and disabling the button component of upgrade station icons
                //upgradeText.gameObject.SetActive(false);
                upgradeText.text = " ";
                upgradeText.transform.parent.GetComponent<Button>().enabled = false;

                //set fill amount of upgrade icon to zero to fill it after on update method
                stationIcon.fillAmount = 0;

                //setting upgrade time step according to the type of upgrade and setting zero the filling amount that will be filled on upgrade method of StationController
                //with step of currentUpgradeTime
                stationReference.currentUpgradeTime = Constants.Instance.time2to3Upgrd;
                stationReference.upgradeFill = 0;

                stationReference.isUpgrading = true; //start the upgrading process by setting true according bool true on according station instance
            }

            TurnOnSound.Play();

            //clearing all production programs to makes it impossible to produce ships while production and requires to set production again ater the upgrade
            //clearAllPruductionBeforeUpgrade();
        }
        else {
            CommonNotEnoughAudio.Play();
            upgradeText.transform.parent.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
            upgradeText.transform.parent.GetComponent<Button>().enabled = false;
        }
    }

    public void updateStationUIAfterUpgrade(StationController station)
    {
        CommonApplyAudio.Play();

        stationIcon.fillAmount = 1;
        upgradeText.transform.parent.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
        upgradeText.transform.parent.GetComponent<Button>().enabled = false;

        //displaying the next upgrade energy requirements
        if (station.stationCurrentLevel == 1) upgradeText.text = Constants.Instance.enrgy1to2Upgrd.ToString();
        else if (station.stationCurrentLevel == 2) upgradeText.text = Constants.Instance.enrgy2to3Upgrd.ToString();

        //this one is necessary to prevent a bug of miss // or null reference exception after the station is upgraded and the SpaceController instance will be removed from 
        //active stations list
        closeAnyPanel();
    }

    //this method is using while checking the limits of production or exchange of ships
    private int playerCruiserShipsCount() {
        //int x = Lists.Cruis4OfPlayerCruis + Lists.Cruis3OfPlayerCruis + Lists.Cruis2OfPlayerCruis * 3 + Lists.Cruis1OfPlayerCruis * 3 + Lists.Destr1OfPlayerCruis
        //    + Lists.Destr1OfPlayerParCruis + Lists.Destr2OfPlayerCruis + Lists.Destr2OfPlayerParCruis + Lists.Destr3OfPlayerCruis + Lists.Destr4OfPlayerCruis +
        //    Lists.Gun1OfPlayerCruis + Lists.Gun2OfPlayerCruis + Lists.Gun3OfPlayerCruis;

        int x = portedCruiserObject.Cruis4 + portedCruiserObject.Cruis3 + portedCruiserObject.Cruis2 * 3 + portedCruiserObject.Cruis1 * 3 + portedCruiserObject.Destr1
            + portedCruiserObject.Destr1Par + portedCruiserObject.Destr2 + portedCruiserObject.Destr2Par + portedCruiserObject.Destr3 + portedCruiserObject.Destr4 +
            portedCruiserObject.Gun1 + portedCruiserObject.Gun2 + portedCruiserObject.Gun3;
        return x;
    }

    //this method is using while checking of player can attack and the attack button should pop up 
    private int ifPlayerHasACruiser(LaunchingObjcts playerCruiser)
    {
        //int x = Lists.Cruis4OfPlayerCruis + Lists.Cruis3OfPlayerCruis + Lists.Cruis2OfPlayerCruis + Lists.Cruis1OfPlayerCruis;
        int x = playerCruiser.Cruis4 + playerCruiser.Cruis3 + playerCruiser.Cruis2 + playerCruiser.Cruis1;
        return x;
    }

    //this method is used to update the compare panel variables while meeting the nex station
    private void updateGreedsWhileCompare()
    {
        //setting zero all greed sizes to make them ready for next opening of compare panel
        playerFleetGreedSizeCompare = 0;
        CPUFleetGreedSizeCompare = 0;

        #region setting greed UI icons and greed sizes in common

        #region setting greed UI icons and greed size of station fleet that is under attack of player
        //setting station fleet UI parameters and setting it's greed size
        if (stationReferenceCompare.Cruis1 > 0)
        {
            cruis1CPUCompare.transform.parent.gameObject.SetActive(true);
            cruis1CPUCompare.text = stationReferenceCompare.Cruis1.ToString();
            CPUFleetGreedSizeCompare++;
        }
        else cruis1CPUCompare.transform.parent.gameObject.SetActive(false);

        if (stationReferenceCompare.Cruis2 > 0)
        {
            cruis2CPUCompare.transform.parent.gameObject.SetActive(true);
            cruis2CPUCompare.text = stationReferenceCompare.Cruis2.ToString();
            CPUFleetGreedSizeCompare++;
        }
        else cruis2CPUCompare.transform.parent.gameObject.SetActive(false);

        if (stationReferenceCompare.Cruis3 > 0)
        {
            cruis3CPUCompare.transform.parent.gameObject.SetActive(true);
            cruis3CPUCompare.text = stationReferenceCompare.Cruis3.ToString();
            CPUFleetGreedSizeCompare++;
        }
        else cruis3CPUCompare.transform.parent.gameObject.SetActive(false);

        if (stationReferenceCompare.Cruis4 > 0)
        {
            cruis4CPUCompare.transform.parent.gameObject.SetActive(true);
            cruis4CPUCompare.text = stationReferenceCompare.Cruis4.ToString();
            CPUFleetGreedSizeCompare++;
        }
        else cruis4CPUCompare.transform.parent.gameObject.SetActive(false);

        if (stationReferenceCompare.CruisG > 0)
        {
            cruisGCPUCompare.transform.parent.gameObject.SetActive(true);
            cruisGCPUCompare.text = stationReferenceCompare.CruisG.ToString();
            CPUFleetGreedSizeCompare++;
        }
        else cruisGCPUCompare.transform.parent.gameObject.SetActive(false);

        if (stationReferenceCompare.Destr1 > 0)
        {
            destr1CPUCompare.transform.parent.gameObject.SetActive(true);
            destr1CPUCompare.text = stationReferenceCompare.Destr1.ToString();
            CPUFleetGreedSizeCompare++;
        }
        else destr1CPUCompare.transform.parent.gameObject.SetActive(false);

        if (stationReferenceCompare.Destr1Par > 0)
        {
            destr1ParCPUCompare.transform.parent.gameObject.SetActive(true);
            destr1ParCPUCompare.text = stationReferenceCompare.Destr1Par.ToString();
            CPUFleetGreedSizeCompare++;
        }
        else destr1ParCPUCompare.transform.parent.gameObject.SetActive(false);

        if (stationReferenceCompare.Destr2 > 0)
        {
            destr2CPUCompare.transform.parent.gameObject.SetActive(true);
            destr2CPUCompare.text = stationReferenceCompare.Destr2.ToString();
            CPUFleetGreedSizeCompare++;
        }
        else destr2CPUCompare.transform.parent.gameObject.SetActive(false);

        if (stationReferenceCompare.Destr2Par > 0)
        {
            destr2ParCPUCompare.transform.parent.gameObject.SetActive(true);
            destr2ParCPUCompare.text = stationReferenceCompare.Destr2Par.ToString();
            CPUFleetGreedSizeCompare++;
        }
        else destr2ParCPUCompare.transform.parent.gameObject.SetActive(false);

        if (stationReferenceCompare.Destr3 > 0)
        {
            destr3CPUCompare.transform.parent.gameObject.SetActive(true);
            destr3CPUCompare.text = stationReferenceCompare.Destr3.ToString();
            CPUFleetGreedSizeCompare++;
        }
        else destr3CPUCompare.transform.parent.gameObject.SetActive(false);

        if (stationReferenceCompare.Destr4 > 0)
        {
            destr4CPUCompare.transform.parent.gameObject.SetActive(true);
            destr4CPUCompare.text = stationReferenceCompare.Destr4.ToString();
            CPUFleetGreedSizeCompare++;
        }
        else destr4CPUCompare.transform.parent.gameObject.SetActive(false);

        if (stationReferenceCompare.DestrG > 0)
        {
            destrGCPUCompare.transform.parent.gameObject.SetActive(true);
            destrGCPUCompare.text = stationReferenceCompare.DestrG.ToString();
            CPUFleetGreedSizeCompare++;
        }
        else destrGCPUCompare.transform.parent.gameObject.SetActive(false);

        if (stationReferenceCompare.Gun1 > 0)
        {
            gun1CPUCompare.transform.parent.gameObject.SetActive(true);
            gun1CPUCompare.text = stationReferenceCompare.Gun1.ToString();
            CPUFleetGreedSizeCompare++;
        }
        else gun1CPUCompare.transform.parent.gameObject.SetActive(false);

        if (stationReferenceCompare.Gun2 > 0)
        {
            gun2CPUCompare.transform.parent.gameObject.SetActive(true);
            gun2CPUCompare.text = stationReferenceCompare.Gun2.ToString();
            CPUFleetGreedSizeCompare++;
        }
        else gun2CPUCompare.transform.parent.gameObject.SetActive(false);

        if (stationReferenceCompare.Gun3 > 0)
        {
            gun3CPUCompare.transform.parent.gameObject.SetActive(true);
            gun3CPUCompare.text = stationReferenceCompare.Gun3.ToString();
            CPUFleetGreedSizeCompare++;
        }
        else gun3CPUCompare.transform.parent.gameObject.SetActive(false);

        if (stationReferenceCompare.Fighter > 0)
        {
            FighterCPUCompare.transform.parent.gameObject.SetActive(true);
            FighterCPUCompare.text = stationReferenceCompare.Fighter.ToString();
            CPUFleetGreedSizeCompare++;
        }
        else FighterCPUCompare.transform.parent.gameObject.SetActive(false);

        #endregion setting greed UI icons and greed size of station fleet

        #region setting greed UI icons and greed size of ships that pordet cruiser has

        if (localLaunchingObjects.Cruis1 > 0)
        {
            cruis1PlayerCompare.transform.parent.gameObject.SetActive(true);
            cruis1PlayerCompare.text = localLaunchingObjects.Cruis1.ToString();
            playerFleetGreedSizeCompare++;
        }
        else cruis1PlayerCompare.transform.parent.gameObject.SetActive(false);

        if (localLaunchingObjects.Cruis2 > 0)
        {
            cruis2PlayerCompare.transform.parent.gameObject.SetActive(true);
            cruis2PlayerCompare.text = localLaunchingObjects.Cruis2.ToString();
            playerFleetGreedSizeCompare++;
        }
        else cruis2PlayerCompare.transform.parent.gameObject.SetActive(false);

        if (localLaunchingObjects.Cruis3 > 0)
        {
            cruis3PlayerCompare.transform.parent.gameObject.SetActive(true);
            cruis3PlayerCompare.text = localLaunchingObjects.Cruis3.ToString();
            playerFleetGreedSizeCompare++;
        }
        else cruis3PlayerCompare.transform.parent.gameObject.SetActive(false);

        if (localLaunchingObjects.Cruis4 > 0)
        {
            cruis4PlayerCompare.transform.parent.gameObject.SetActive(true);
            cruis4PlayerCompare.text = localLaunchingObjects.Cruis4.ToString();
            playerFleetGreedSizeCompare++;
        }
        else cruis4PlayerCompare.transform.parent.gameObject.SetActive(false);

        if (localLaunchingObjects.Destr1 > 0)
        {
            destr1PlayerCompare.transform.parent.gameObject.SetActive(true);
            destr1PlayerCompare.text = localLaunchingObjects.Destr1.ToString();
            playerFleetGreedSizeCompare++;
        }
        else destr1PlayerCompare.transform.parent.gameObject.SetActive(false);

        if (localLaunchingObjects.Destr1Par > 0)
        {
            destr1ParPlayerCompare.transform.parent.gameObject.SetActive(true);
            destr1ParPlayerCompare.text = localLaunchingObjects.Destr1Par.ToString();
            playerFleetGreedSizeCompare++;
        }
        else destr1ParPlayerCompare.transform.parent.gameObject.SetActive(false);

        if (localLaunchingObjects.Destr2 > 0)
        {
            destr2PlayerCompare.transform.parent.gameObject.SetActive(true);
            destr2PlayerCompare.text = localLaunchingObjects.Destr2.ToString();
            playerFleetGreedSizeCompare++;
        }
        else destr2PlayerCompare.transform.parent.gameObject.SetActive(false);

        if (localLaunchingObjects.Destr2Par > 0)
        {
            destr2ParPlayerCompare.transform.parent.gameObject.SetActive(true);
            destr2ParPlayerCompare.text = localLaunchingObjects.Destr2Par.ToString();
            playerFleetGreedSizeCompare++;
        }
        else destr2ParPlayerCompare.transform.parent.gameObject.SetActive(false);

        if (localLaunchingObjects.Destr3 > 0)
        {
            destr3PlayerCompare.transform.parent.gameObject.SetActive(true);
            destr3PlayerCompare.text = localLaunchingObjects.Destr3.ToString();
            playerFleetGreedSizeCompare++;
        }
        else destr3PlayerCompare.transform.parent.gameObject.SetActive(false);

        if (localLaunchingObjects.Destr4 > 0)
        {
            destr4PlayerCompare.transform.parent.gameObject.SetActive(true);
            destr4PlayerCompare.text = localLaunchingObjects.Destr4.ToString();
            playerFleetGreedSizeCompare++;
        }
        else destr4PlayerCompare.transform.parent.gameObject.SetActive(false);

        if (localLaunchingObjects.Gun1 > 0)
        {
            gun1PlayerCompare.transform.parent.gameObject.SetActive(true);
            gun1PlayerCompare.text = localLaunchingObjects.Gun1.ToString();
            playerFleetGreedSizeCompare++;
        }
        else gun1PlayerCompare.transform.parent.gameObject.SetActive(false);

        if (localLaunchingObjects.Gun2 > 0)
        {
            gun2PlayerCompare.transform.parent.gameObject.SetActive(true);
            gun2PlayerCompare.text = localLaunchingObjects.Gun2.ToString();
            playerFleetGreedSizeCompare++;
        }
        else gun2PlayerCompare.transform.parent.gameObject.SetActive(false);

        if (localLaunchingObjects.Gun3 > 0)
        {
            gun3PlayerCompare.transform.parent.gameObject.SetActive(true);
            gun3PlayerCompare.text = localLaunchingObjects.Gun3.ToString();
            playerFleetGreedSizeCompare++;
        }
        else gun3PlayerCompare.transform.parent.gameObject.SetActive(false);

        if (localLaunchingObjects.MiniGun > 0)
        {
            MiniGunPlayerCompare.transform.parent.gameObject.SetActive(true);
            MiniGunPlayerCompare.text = localLaunchingObjects.MiniGun.ToString();
            playerFleetGreedSizeCompare++;
        }
        else MiniGunPlayerCompare.transform.parent.gameObject.SetActive(false);

        #endregion setting greed UI icons and greed size of ships that pordet cruiser has

        //increase or decreace the width of greed dynamically depending on count of ship types on differend greeds
        CPUFleetGreedCompareRect.sizeDelta = new Vector2(CPUFleetGreedSizeCompare * 180, 177f);
        playerFleetGreedCompareRect.sizeDelta = new Vector2(playerFleetGreedSizeCompare * 180, 177f);

        #endregion setting greed UI icons and greed sizes in common
    }

    //this method is used to update the player cruis panel variables while opening the panel
    private void updateGreedsPlayerCruisPanel(LaunchingObjcts chosenCruiser)
    {
        //setting zero all greed sizes to make them ready for next opening of player cruis panel
        PlayerCruisFleetGreedSize = 0;
        PlayerStationsGreedSize = 0;

        #region setting greed UI icons and greed sizes in common

        #region setting greed UI icons and greed size of player cruis fleet 
        //setting player cruis fleet UI parameters and setting it's greed size
        if (chosenCruiser.Cruis1 > 0)
        {
            cruis1PlayerCruis.transform.parent.gameObject.SetActive(true);
            cruis1PlayerCruis.text = chosenCruiser.Cruis1.ToString();
            PlayerCruisFleetGreedSize++;
        }
        else cruis1PlayerCruis.transform.parent.gameObject.SetActive(false);

        if (chosenCruiser.Cruis2 > 0)
        {
            cruis2PlayerCruis.transform.parent.gameObject.SetActive(true);
            cruis2PlayerCruis.text = chosenCruiser.Cruis2.ToString();
            PlayerCruisFleetGreedSize++;
        }
        else cruis2PlayerCruis.transform.parent.gameObject.SetActive(false);

        if (chosenCruiser.Cruis3 > 0)
        {
            cruis3PlayerCruis.transform.parent.gameObject.SetActive(true);
            cruis3PlayerCruis.text = chosenCruiser.Cruis3.ToString();
            PlayerCruisFleetGreedSize++;
        }
        else cruis3PlayerCruis.transform.parent.gameObject.SetActive(false);

        if (chosenCruiser.Cruis4 > 0)
        {
            cruis4PlayerCruis.transform.parent.gameObject.SetActive(true);
            cruis4PlayerCruis.text = chosenCruiser.Cruis4.ToString();
            PlayerCruisFleetGreedSize++;
        }
        else cruis4PlayerCruis.transform.parent.gameObject.SetActive(false);

        if (chosenCruiser.Destr1 > 0)
        {
            destr1PlayerCruis.transform.parent.gameObject.SetActive(true);
            destr1PlayerCruis.text = chosenCruiser.Destr1.ToString();
            PlayerCruisFleetGreedSize++;
        }
        else destr1PlayerCruis.transform.parent.gameObject.SetActive(false);

        if (chosenCruiser.Destr1Par > 0)
        {
            destr1ParPlayerCruis.transform.parent.gameObject.SetActive(true);
            destr1ParPlayerCruis.text = chosenCruiser.Destr1Par.ToString();
            PlayerCruisFleetGreedSize++;
        }
        else destr1ParPlayerCruis.transform.parent.gameObject.SetActive(false);

        if (chosenCruiser.Destr2 > 0)
        {
            destr2PlayerCruis.transform.parent.gameObject.SetActive(true);
            destr2PlayerCruis.text = chosenCruiser.Destr2.ToString();
            PlayerCruisFleetGreedSize++;
        }
        else destr2PlayerCruis.transform.parent.gameObject.SetActive(false);

        if (chosenCruiser.Destr2Par > 0)
        {
            destr2ParPlayerCruis.transform.parent.gameObject.SetActive(true);
            destr2ParPlayerCruis.text = chosenCruiser.Destr2Par.ToString();
            PlayerCruisFleetGreedSize++;
        }
        else destr2ParPlayerCruis.transform.parent.gameObject.SetActive(false);

        if (chosenCruiser.Destr3 > 0)
        {
            destr3PlayerCruis.transform.parent.gameObject.SetActive(true);
            destr3PlayerCruis.text = chosenCruiser.Destr3.ToString();
            PlayerCruisFleetGreedSize++;
        }
        else destr3PlayerCruis.transform.parent.gameObject.SetActive(false);

        if (chosenCruiser.Destr4 > 0)
        {
            destr4PlayerCruis.transform.parent.gameObject.SetActive(true);
            destr4PlayerCruis.text = chosenCruiser.Destr4.ToString();
            PlayerCruisFleetGreedSize++;
        }
        else destr4PlayerCruis.transform.parent.gameObject.SetActive(false);

        if (chosenCruiser.Gun1 > 0)
        {
            gun1PlayerCruis.transform.parent.gameObject.SetActive(true);
            gun1PlayerCruis.text = chosenCruiser.Gun1.ToString();
            PlayerCruisFleetGreedSize++;
        }
        else gun1PlayerCruis.transform.parent.gameObject.SetActive(false);

        if (chosenCruiser.Gun2 > 0)
        {
            gun2PlayerCruis.transform.parent.gameObject.SetActive(true);
            gun2PlayerCruis.text = chosenCruiser.Gun2.ToString();
            PlayerCruisFleetGreedSize++;
        }
        else gun2PlayerCruis.transform.parent.gameObject.SetActive(false);

        if (chosenCruiser.Gun3 > 0)
        {
            gun3PlayerCruis.transform.parent.gameObject.SetActive(true);
            gun3PlayerCruis.text = chosenCruiser.Gun3.ToString();
            PlayerCruisFleetGreedSize++;
        }
        else gun3PlayerCruis.transform.parent.gameObject.SetActive(false);

        if (chosenCruiser.MiniGun > 0)
        {
            MiniGunPlayerCruis.transform.parent.gameObject.SetActive(true);
            MiniGunPlayerCruis.text = chosenCruiser.MiniGun.ToString();
            PlayerCruisFleetGreedSize++;
        }
        else MiniGunPlayerCruis.transform.parent.gameObject.SetActive(false);

        #endregion setting greed UI icons and greed size of station fleet

        #region setting greed UI icons and greed size of player stations 

        //int nextStation = 0;

        //this cycle activates sprite of station on stations greed, sets to it a decent sprite image according to the level of station from StationController class
        //and add default button from the public (preloaded on inspector) list of buttons and the station controller instance with the same ID to process button push
        //on method switchToStation lower
        //for (int i = 0; i < Lists.playerStations.Count; i++) {
        //    if (Lists.playerStations[i].stationCurrentLevel == 0)
        //    {
        //        if (!Lists.playerStations[i].isUpgrading)
        //        {
        //            //fill abount should be set 1 to beat over the update of fillAmount on update while the station was upgrading
        //            stationButtons[nextStation].GetComponent<Image>().fillAmount = 1;
        //            //so if station is not upgrading the sprite of station on cruis panel will be according to the station level, otherwise ot will be upgrading station
        //            stationButtons[nextStation].GetComponent<Image>().sprite = Station1Spr;
        //        }
        //        else
        //        {
        //            stationButtons[nextStation].GetComponent<Image>().sprite = Station2Spr;
        //        }
        //        //so if this station is the one that the player cruiser ported it will show the special token of ported. It is the first child of station icon GO
        //        if (Lists.playerStations[i].playerCruiserNear) {
        //            stationButtons[nextStation].transform.GetChild(0).gameObject.SetActive(true);
        //        }
        //        else stationButtons[nextStation].transform.GetChild(0).gameObject.SetActive(false);

        //        stationButtons[nextStation].SetActive(true);
        //        ButtonAndStation.Add(stationButtons[nextStation], Lists.playerStations[i]);
        //        nextStation++;

        //    }
        //    else if (Lists.playerStations[i].stationCurrentLevel == 1)
        //    {
        //        if (!Lists.playerStations[i].isUpgrading)
        //        {
        //            //fill abount should be set 1 to beat over the update of fillAmount on update while the station was upgrading
        //            stationButtons[nextStation].GetComponent<Image>().fillAmount = 1;
        //            if (!Lists.playerStations[i].isGuardCoreStation)stationButtons[nextStation].GetComponent<Image>().sprite = Station2Spr;
        //            else stationButtons[nextStation].GetComponent<Image>().sprite = StationGSpr;
        //        }
        //        else
        //        {
        //            stationButtons[nextStation].GetComponent<Image>().sprite = Station3Spr;
        //        }

        //        //so if this station is the one that the player cruiser ported it will show the special token of ported. It is the first child of station icon GO
        //        if (Lists.playerStations[i].playerCruiserNear)
        //        {
        //            stationButtons[nextStation].transform.GetChild(0).gameObject.SetActive(true);
        //        }
        //        else stationButtons[nextStation].transform.GetChild(0).gameObject.SetActive(false);

        //        stationButtons[nextStation].SetActive(true);
        //        ButtonAndStation.Add(stationButtons[nextStation], Lists.playerStations[i]);
        //        nextStation++;
        //    }
        //    else if (Lists.playerStations[i].stationCurrentLevel == 2)
        //    {
        //        if (!Lists.playerStations[i].isUpgrading)
        //        {
        //            //fill abount should be set 1 to beat over the update of fillAmount on update while the station was upgrading
        //            stationButtons[nextStation].GetComponent<Image>().fillAmount = 1;
        //            if (!Lists.playerStations[i].isGuardCoreStation) stationButtons[nextStation].GetComponent<Image>().sprite = Station3Spr;
        //            else stationButtons[nextStation].GetComponent<Image>().sprite = StationGSpr;


        //        }
        //        else
        //        {
        //            stationButtons[nextStation].GetComponent<Image>().sprite = Station4Spr;
        //        }

        //        //so if this station is the one that the player cruiser ported it will show the special token of ported. It is the first child of station icon GO
        //        if (Lists.playerStations[i].playerCruiserNear)
        //        {
        //            stationButtons[nextStation].transform.GetChild(0).gameObject.SetActive(true);
        //        }
        //        else stationButtons[nextStation].transform.GetChild(0).gameObject.SetActive(false);

        //        stationButtons[nextStation].SetActive(true);
        //        ButtonAndStation.Add(stationButtons[nextStation], Lists.playerStations[i]);
        //        nextStation++;
        //    }
        //    else if (Lists.playerStations[i].stationCurrentLevel == 3)
        //    {
        //        //fill abount should be set 1 to beat over the update of fillAmount on update while the station was upgrading
        //        stationButtons[nextStation].GetComponent<Image>().fillAmount = 1;
        //        if (!Lists.playerStations[i].isGuardCoreStation) stationButtons[nextStation].GetComponent<Image>().sprite = Station4Spr;
        //        else stationButtons[nextStation].GetComponent<Image>().sprite = StationGSpr;

        //        //so if this station is the one that the player cruiser ported it will show the special token of ported. It is the first child of station icon GO
        //        if (Lists.playerStations[i].playerCruiserNear)
        //        {
        //            stationButtons[nextStation].transform.GetChild(0).gameObject.SetActive(true);
        //        }
        //        else stationButtons[nextStation].transform.GetChild(0).gameObject.SetActive(false);

        //        stationButtons[nextStation].SetActive(true);
        //        ButtonAndStation.Add(stationButtons[nextStation], Lists.playerStations[i]);
        //        nextStation++;
        //    }
        //    PlayerStationsGreedSize++;
        //}

        #endregion setting greed UI icons and greed size of player stations 

        //increase or decreace the width of greed dynamically depending on count of ship types on differend greeds
        PlayerStationsGreedRect.sizeDelta = new Vector2(PlayerStationsGreedSize * 464, 275f);
        PlayerCruisFleetGreedRect.sizeDelta = new Vector2(PlayerCruisFleetGreedSize * 180, 100f);

        #endregion setting greed UI icons and greed sizes in common
    }


    //this coroutine tightly connected with method switchToStation and calls the opening chosen station panel with 0.5 secs lag
    //IEnumerator activateStationPanelCor(bool panelState, StationController station) {
    //    yield return new WaitForSeconds(0.3f);
    //    activateStationPanel(panelState, station);
    //}
    //this method is assigned to station buttons on player cruiser panel
    //public void switchToStation(GameObject station) {

    //    //checks if current pushed button is on special populated dictionary and switch to the station panel with stationController instance taken from the dictionary
    //    //that matches to the key of button instance
    //    foreach (GameObject go in ButtonAndStation.Keys)
    //    {
    //        if (go == station)
    //        {
    //          StartCoroutine(activateStationPanelCor(true, ButtonAndStation[go]));
    //        }
    //    }
    //    closeAnyPanel();
    //    foreach (GameObject go in stationButtons)
    //    {
    //        go.transform.GetChild(0).gameObject.SetActive(false);//disactivating ported token
    //        go.SetActive(false);
    //    }
    //    ButtonAndStation.Clear();
    //    MiniImgAndStation.Clear();
    //    TextAndStation.Clear();
    //}

    //this method is necessary to prepare this class and StationController class to upgrade
    //private void clearAllPruductionBeforeUpgrade() {
    //    stationReference.Cruis1Produc = 0;
    //    stationReference.Cruis2Produc = 0;
    //    stationReference.Cruis3Produc = 0;
    //    stationReference.Cruis4Produc = 0;
    //    stationReference.Destr1Produc = 0;
    //    stationReference.Destr1ProducPar = 0;
    //    stationReference.Destr2Produc = 0;
    //    stationReference.Destr2ProducPar = 0;
    //    stationReference.Destr3Produc = 0;
    //    stationReference.Destr4Produc = 0;
    //    stationReference.Gun1Produc = 0;
    //    stationReference.Gun2Produc = 0;
    //    stationReference.Gun3Produc = 0;
    //    stationReference.MiniGunProduc = 0;

    //    stationReference.productionPlan.Clear();

    //    //stop all productions
    //    stationReference.energyProductionIsOn = false;
    //    stationReference.shipProductionIsOn = false;

    //    stationReference.currentPruductionFillLocal = 0;
    //}

    //this function is for launching the production by players push. It is assigned to buttons of ships with strings that correnspond to ship types
    public void launchProduction(string shipType) {
        //so it is impossible to launch production if station is under process of upgrading
        if (!stationReference.isUpgrading) {
            if (shipType == C1) {

                if (Lists.energyOfPlayer > Constants.Instance.C1ProdEnergy)
                {
                    //this is a ships limit trigger for preventing overproduction. So if player wants to set next production ship class 1 or 2 cruiser the limit is 42 
                    if (stationReference.stationFleetCount() <= Constants.Instance.shipsPreLimit)
                    {
                        //so if there is at least one ship on plan this function will not work
                        //cause this snippet of code is for starting production from zero plan
                        //if (stationReference.productionPlan.Count < 1)
                        //{
                        //    stationReference.Cruis1Produc++; //anyway it is necessary to add an production count to show player what is produced and refused in case of dismission
                        //    currenProductionBckgr.sprite = Cruis1Spr;
                        //    currenProductionFill.sprite = Cruis1Spr;
                        //    stationReference.shipProductionIsOn = true;
                        //    stationReference.energyProductionIsOn = false;
                        //    stationReference.commontPruductTimeLocal = stationReference.Cruis1ProductTime;
                        //    stationReference.currentPruductionFillLocal = 0;
                        //    //currenProductionFill.fillAmount = 0;
                        //    stationReference.productionPlan.Add(C1); //populating production plan with current type of ship
                        //    stationReference.CurrentProducedShipLocal = C1;
                        //}
                        ////so if current production plan is set already this function will only add a next ship to produce to the plan
                        //else
                        //{
                        //    stationReference.Cruis1Produc++;
                        //    stationReference.productionPlan.Add(C1); //populating production plan with current type of ship
                        //}
                        //updating the Text UI params
                        //cruis1ProductStation.text = stationReference.Cruis1Produc.ToString();

                        stationReference.Cruis1++;
                        //consuming the energy of player to produce this type of ship
                        Lists.energyOfPlayer -= Constants.Instance.C1ProdEnergy;
                        energyCount.text = Lists.energyOfPlayer.ToString("0");
                        //disabling the upgrade possibility of station if the level of energy is lower than necessary to upgrade current station
                        if (stationReference.stationCurrentLevel != stationReference.upgradeCounts)
                        {
                            if (Lists.energyOfPlayer < stationReference.nextUpgradeEnergyCount)
                            {
                                //if (stationReference.stationCurrentLevel < stationReference.upgradeCounts)
                                //{
                                //upgradeText.gameObject.SetActive(false);
                                if (stationReference.stationCurrentLevel == 0) upgradeText.text = Constants.Instance.enrgy0to1Upgrd.ToString();
                                else if (stationReference.stationCurrentLevel == 1) upgradeText.text = Constants.Instance.enrgy1to2Upgrd.ToString();
                                else if (stationReference.stationCurrentLevel == 2) upgradeText.text = Constants.Instance.enrgy2to3Upgrd.ToString();
                                upgradeText.transform.parent.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                                upgradeText.transform.parent.GetComponent<Button>().enabled = false;
                                //}
                            }
                        }
                        updatingGreedsOnProductionButton();
                    }
                    //if limits will be surpassed with planning to produce ship there will appear the warning
                    else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
                }
                else
                {
                    limitReachedWarning(Constants.Instance.getNoEnergy());
                }
            }
            else if (shipType == C2)
            {
                if (Lists.energyOfPlayer > Constants.Instance.C2ProdEnergy)
                {
                    if (stationReference.stationFleetCount() <= Constants.Instance.shipsPreLimit)
                    {
                        //so if there is at least one ship on plan this function will not work
                        //cause this snippet of code is for starting production from zero plan
                        //if (stationReference.productionPlan.Count < 1)
                        //{
                        //    stationReference.Cruis2Produc++; //anyway it is necessary to add an production count to show player what is produced and refused in case of dismission
                        //    currenProductionBckgr.sprite = Cruis2Spr;
                        //    currenProductionFill.sprite = Cruis2Spr;
                        //    stationReference.shipProductionIsOn = true;
                        //    stationReference.energyProductionIsOn = false;
                        //    stationReference.commontPruductTimeLocal = stationReference.Cruis2ProductTime;
                        //    stationReference.currentPruductionFillLocal = 0;
                        //    //currenProductionFill.fillAmount = 0;
                        //    stationReference.productionPlan.Add(C2); //populating production plan with current type of ship
                        //    stationReference.CurrentProducedShipLocal = C2;
                        //}
                        ////so if current production plan is set already this function will only add a next ship to produce to the plan
                        //else
                        //{
                        //    stationReference.Cruis2Produc++;
                        //    stationReference.productionPlan.Add(C2); //populating production plan with current type of ship
                        //}
                        ////updating the Text UI params
                        //cruis2ProductStation.text = stationReference.Cruis2Produc.ToString();

                        stationReference.Cruis2++;
                        //consuming the energy of player to produce this type of ship
                        Lists.energyOfPlayer -= Constants.Instance.C2ProdEnergy;
                        energyCount.text = Lists.energyOfPlayer.ToString("0");
                        //disabling the upgrade possibility of station if the level of energy is lower than necessary to upgrade current station
                        if (stationReference.stationCurrentLevel != stationReference.upgradeCounts)
                        {
                            if (Lists.energyOfPlayer < stationReference.nextUpgradeEnergyCount)
                            {
                                //if (stationReference.stationCurrentLevel < stationReference.upgradeCounts)
                                //{
                                //upgradeText.gameObject.SetActive(false);
                                if (stationReference.stationCurrentLevel == 0) upgradeText.text = Constants.Instance.enrgy0to1Upgrd.ToString();
                                else if (stationReference.stationCurrentLevel == 1) upgradeText.text = Constants.Instance.enrgy1to2Upgrd.ToString();
                                else if (stationReference.stationCurrentLevel == 2) upgradeText.text = Constants.Instance.enrgy2to3Upgrd.ToString();
                                upgradeText.transform.parent.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                                upgradeText.transform.parent.GetComponent<Button>().enabled = false;
                                //}
                            }
                        }
                        updatingGreedsOnProductionButton();
                    }
                    //if limits will be surpassed with planning to produce ship there will appear the warning
                    else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
                }
                else
                {
                    limitReachedWarning(Constants.Instance.getNoEnergy());
                }
            }
            else if (shipType == C3)
            {
                if (Lists.energyOfPlayer > Constants.Instance.C3ProdEnergy)
                {
                    //this is a ships limit trigger for preventing overproduction. So if player wants to set next production ship that is not class 1 or 2 cruiser 4
                    //the limit is 45 
                    if (stationReference.stationFleetCount() < Constants.Instance.shipsLimit)
                    {
                        //so if there is at least one ship on plan this function will not work
                        //cause this snippet of code is for starting production from zero plan
                        //if (stationReference.productionPlan.Count < 1)
                        //{
                        //    stationReference.Cruis3Produc++; //anyway it is necessary to add an production count to show player what is produced and refused in case of dismission
                        //    currenProductionBckgr.sprite = Cruis3Spr;
                        //    currenProductionFill.sprite = Cruis3Spr;
                        //    stationReference.shipProductionIsOn = true;
                        //    stationReference.energyProductionIsOn = false;
                        //    stationReference.commontPruductTimeLocal = stationReference.Cruis3ProductTime;
                        //    stationReference.currentPruductionFillLocal = 0;
                        //    //currenProductionFill.fillAmount = 0;
                        //    stationReference.productionPlan.Add(C3); //populating production plan with current type of ship
                        //    stationReference.CurrentProducedShipLocal = C3;
                        //}
                        ////so if current production plan is set already this function will only add a next ship to produce to the plan
                        //else
                        //{
                        //    stationReference.Cruis3Produc++;
                        //    stationReference.productionPlan.Add(C3); //populating production plan with current type of ship
                        //}
                        ////updating the Text UI params
                        //cruis3ProductStation.text = stationReference.Cruis3Produc.ToString();

                        stationReference.Cruis3++;
                        //consuming the energy of player to produce this type of ship
                        Lists.energyOfPlayer -= Constants.Instance.C3ProdEnergy;
                        energyCount.text = Lists.energyOfPlayer.ToString("0");
                        //disabling the upgrade possibility of station if the level of energy is lower than necessary to upgrade current station
                        if (stationReference.stationCurrentLevel != stationReference.upgradeCounts)
                        {
                            if (Lists.energyOfPlayer < stationReference.nextUpgradeEnergyCount)
                            {
                                //if (stationReference.stationCurrentLevel < stationReference.upgradeCounts)
                                //{
                                //upgradeText.gameObject.SetActive(false);
                                if (stationReference.stationCurrentLevel == 0) upgradeText.text = Constants.Instance.enrgy0to1Upgrd.ToString();
                                else if (stationReference.stationCurrentLevel == 1) upgradeText.text = Constants.Instance.enrgy1to2Upgrd.ToString();
                                else if (stationReference.stationCurrentLevel == 2) upgradeText.text = Constants.Instance.enrgy2to3Upgrd.ToString();
                                upgradeText.transform.parent.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                                upgradeText.transform.parent.GetComponent<Button>().enabled = false;
                                //}
                            }
                        }
                        updatingGreedsOnProductionButton();
                    }
                    //if limits will be surpassed with planning to produce ship there will appear the warning
                    else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
                }
                else
                {
                    limitReachedWarning(Constants.Instance.getNoEnergy());
                }
            }
            else if (shipType == C4)
            {
                if (Lists.energyOfPlayer > Constants.Instance.C4ProdEnergy)
                {
                    //this is a ships limit trigger for preventing overproduction. So if player wants to set next production ship that is not class 1 or 2 cruiser 4
                    //the limit is 45 
                    if (stationReference.stationFleetCount() < Constants.Instance.shipsLimit)
                    {
                        //so if there is at least one ship on plan this function will not work
                        //cause this snippet of code is for starting production from zero plan
                        //if (stationReference.productionPlan.Count < 1)
                        //{
                        //    stationReference.Cruis4Produc++; //anyway it is necessary to add an production count to show player what is produced and refused in case of dismission
                        //    currenProductionBckgr.sprite = Cruis4Spr;
                        //    currenProductionFill.sprite = Cruis4Spr;
                        //    stationReference.shipProductionIsOn = true;
                        //    stationReference.energyProductionIsOn = false;
                        //    stationReference.commontPruductTimeLocal = stationReference.Cruis4ProductTime;
                        //    stationReference.currentPruductionFillLocal = 0;
                        //    //currenProductionFill.fillAmount = 0;
                        //    stationReference.productionPlan.Add(C4); //populating production plan with current type of ship
                        //    stationReference.CurrentProducedShipLocal = C4;
                        //}
                        ////so if current production plan is set already this function will only add a next ship to produce to the plan
                        //else
                        //{
                        //    stationReference.Cruis4Produc++;
                        //    stationReference.productionPlan.Add(C4); //populating production plan with current type of ship
                        //}
                        ////updating the Text UI params
                        //cruis4ProductStation.text = stationReference.Cruis4Produc.ToString();

                        stationReference.Cruis4++;
                        //consuming the energy of player to produce this type of ship
                        Lists.energyOfPlayer -= Constants.Instance.C4ProdEnergy;
                        energyCount.text = Lists.energyOfPlayer.ToString("0");
                        //disabling the upgrade possibility of station if the level of energy is lower than necessary to upgrade current station
                        if (stationReference.stationCurrentLevel != stationReference.upgradeCounts)
                        {
                            if (Lists.energyOfPlayer < stationReference.nextUpgradeEnergyCount)
                            {
                                //if (stationReference.stationCurrentLevel < stationReference.upgradeCounts)
                                //{
                                //upgradeText.gameObject.SetActive(false);
                                if (stationReference.stationCurrentLevel == 0) upgradeText.text = Constants.Instance.enrgy0to1Upgrd.ToString();
                                else if (stationReference.stationCurrentLevel == 1) upgradeText.text = Constants.Instance.enrgy1to2Upgrd.ToString();
                                else if (stationReference.stationCurrentLevel == 2) upgradeText.text = Constants.Instance.enrgy2to3Upgrd.ToString();
                                upgradeText.transform.parent.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                                upgradeText.transform.parent.GetComponent<Button>().enabled = false;
                                //}
                            }
                        }
                        updatingGreedsOnProductionButton();
                    }
                    //if limits will be surpassed with planning to produce ship there will appear the warning
                    else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
                }
                else
                {
                    limitReachedWarning(Constants.Instance.getNoEnergy());
                }
            }
            else if (shipType == D3)
            {
                if (Lists.energyOfPlayer > Constants.Instance.D3ProdEnergy)
                {
                    //this is a ships limit trigger for preventing overproduction. So if player wants to set next production ship that is not class 1 or 2 cruiser 4
                    //the limit is 45 
                    if (stationReference.stationFleetCount() < Constants.Instance.shipsLimit)
                    {
                        //so if there is at least one ship on plan this function will not work
                        //cause this snippet of code is for starting production from zero plan
                        //if (stationReference.productionPlan.Count < 1)
                        //{
                        //    stationReference.Destr3Produc++; //anyway it is necessary to add an production count to show player what is produced and refused in case of dismission
                        //    currenProductionBckgr.sprite = Destr3Spr;
                        //    currenProductionFill.sprite = Destr3Spr;
                        //    stationReference.shipProductionIsOn = true;
                        //    stationReference.energyProductionIsOn = false;
                        //    stationReference.commontPruductTimeLocal = stationReference.Destr3ProductTime;
                        //    stationReference.currentPruductionFillLocal = 0;
                        //    //currenProductionFill.fillAmount = 0;
                        //    stationReference.productionPlan.Add(D3); //populating production plan with current type of ship
                        //    stationReference.CurrentProducedShipLocal = D3;
                        //}
                        ////so if current production plan is set already this function will only add a next ship to produce to the plan
                        //else
                        //{
                        //    stationReference.Destr3Produc++;
                        //    stationReference.productionPlan.Add(D3); //populating production plan with current type of ship
                        //}
                        ////updating the Text UI params
                        //destr3ProductStation.text = stationReference.Destr3Produc.ToString();

                        stationReference.Destr3++;
                        //consuming the energy of player to produce this type of ship
                        Lists.energyOfPlayer -= Constants.Instance.D3ProdEnergy;
                        energyCount.text = Lists.energyOfPlayer.ToString("0");
                        //disabling the upgrade possibility of station if the level of energy is lower than necessary to upgrade current station
                        if (stationReference.stationCurrentLevel != stationReference.upgradeCounts)
                        {
                            if (Lists.energyOfPlayer < stationReference.nextUpgradeEnergyCount)
                            {
                                //if (stationReference.stationCurrentLevel < stationReference.upgradeCounts)
                                //{
                                //upgradeText.gameObject.SetActive(false);
                                if (stationReference.stationCurrentLevel == 0) upgradeText.text = Constants.Instance.enrgy0to1Upgrd.ToString();
                                else if (stationReference.stationCurrentLevel == 1) upgradeText.text = Constants.Instance.enrgy1to2Upgrd.ToString();
                                else if (stationReference.stationCurrentLevel == 2) upgradeText.text = Constants.Instance.enrgy2to3Upgrd.ToString();
                                upgradeText.transform.parent.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                                upgradeText.transform.parent.GetComponent<Button>().enabled = false;
                                //}
                            }
                        }
                        updatingGreedsOnProductionButton();
                    }
                    //if limits will be surpassed with planning to produce ship there will appear the warning
                    else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
                }
                else
                {
                    limitReachedWarning(Constants.Instance.getNoEnergy());
                }
            }
            else if (shipType == D2)
            {
                if (Lists.energyOfPlayer > Constants.Instance.D2ProdEnergy)
                {
                    //this is a ships limit trigger for preventing overproduction. So if player wants to set next production ship that is not class 1 or 2 cruiser 4
                    //the limit is 45 
                    if (stationReference.stationFleetCount() < Constants.Instance.shipsLimit)
                    {
                        //so if there is at least one ship on plan this function will not work
                        //cause this snippet of code is for starting production from zero plan
                        //if (stationReference.productionPlan.Count < 1)
                        //{
                        //    stationReference.Destr2Produc++; //anyway it is necessary to add an production count to show player what is produced and refused in case of dismission
                        //    currenProductionBckgr.sprite = Destr2Spr;
                        //    currenProductionFill.sprite = Destr2Spr;
                        //    stationReference.shipProductionIsOn = true;
                        //    stationReference.energyProductionIsOn = false;
                        //    stationReference.commontPruductTimeLocal = stationReference.Destr2ProductTime;
                        //    stationReference.currentPruductionFillLocal = 0;
                        //    //currenProductionFill.fillAmount = 0;
                        //    stationReference.productionPlan.Add(D2); //populating production plan with current type of ship
                        //    stationReference.CurrentProducedShipLocal = D2;
                        //}
                        ////so if current production plan is set already this function will only add a next ship to produce to the plan
                        //else
                        //{
                        //    stationReference.Destr2Produc++;
                        //    stationReference.productionPlan.Add(D2); //populating production plan with current type of ship
                        //}
                        ////updating the Text UI params
                        //destr2ProductStation.text = stationReference.Destr2Produc.ToString();


                        stationReference.Destr2++;

                        //consuming the energy of player to produce this type of ship
                        Lists.energyOfPlayer -= Constants.Instance.D2ProdEnergy;
                        energyCount.text = Lists.energyOfPlayer.ToString("0");
                        //disabling the upgrade possibility of station if the level of energy is lower than necessary to upgrade current station
                        if (stationReference.stationCurrentLevel != stationReference.upgradeCounts)
                        {
                            if (Lists.energyOfPlayer < stationReference.nextUpgradeEnergyCount)
                            {
                                //if (stationReference.stationCurrentLevel < stationReference.upgradeCounts)
                                //{
                                //upgradeText.gameObject.SetActive(false);
                                if (stationReference.stationCurrentLevel == 0) upgradeText.text = Constants.Instance.enrgy0to1Upgrd.ToString();
                                else if (stationReference.stationCurrentLevel == 1) upgradeText.text = Constants.Instance.enrgy1to2Upgrd.ToString();
                                else if (stationReference.stationCurrentLevel == 2) upgradeText.text = Constants.Instance.enrgy2to3Upgrd.ToString();
                                upgradeText.transform.parent.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                                upgradeText.transform.parent.GetComponent<Button>().enabled = false;
                                //}
                            }
                        }
                        updatingGreedsOnProductionButton();
                    }
                    //if limits will be surpassed with planning to produce ship there will appear the warning
                    else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
                }
                else
                {
                    limitReachedWarning(Constants.Instance.getNoEnergy());
                }
            }
            else if (shipType == D2P)
            {
                if (Lists.energyOfPlayer > Constants.Instance.D2PProdEnergy)
                {
                    //this is a ships limit trigger for preventing overproduction. So if player wants to set next production ship that is not class 1 or 2 cruiser 4
                    //the limit is 45 
                    if (stationReference.stationFleetCount() < Constants.Instance.shipsLimit)
                    {
                        //so if there is at least one ship on plan this function will not work
                        //cause this snippet of code is for starting production from zero plan
                        //if (stationReference.productionPlan.Count < 1)
                        //{
                        //    stationReference.Destr2ProducPar++; //anyway it is necessary to add an production count to show player what is produced and refused in case of dismission
                        //    currenProductionBckgr.sprite = Destr2ParSpr;
                        //    currenProductionFill.sprite = Destr2ParSpr;
                        //    stationReference.shipProductionIsOn = true;
                        //    stationReference.energyProductionIsOn = false;
                        //    stationReference.commontPruductTimeLocal = stationReference.Destr2ParProductTime;
                        //    stationReference.currentPruductionFillLocal = 0;
                        //    //currenProductionFill.fillAmount = 0;
                        //    stationReference.productionPlan.Add(D2P); //populating production plan with current type of ship
                        //    stationReference.CurrentProducedShipLocal = D2P;
                        //}
                        ////so if current production plan is set already this function will only add a next ship to produce to the plan
                        //else
                        //{
                        //    stationReference.Destr2ProducPar++;
                        //    stationReference.productionPlan.Add(D2P); //populating production plan with current type of ship
                        //}
                        ////updating the Text UI params
                        //destr2ParProductStation.text = stationReference.Destr2ProducPar.ToString();

                        stationReference.Destr2Par++;
                        //consuming the energy of player to produce this type of ship
                        Lists.energyOfPlayer -= Constants.Instance.D2PProdEnergy;
                        energyCount.text = Lists.energyOfPlayer.ToString("0");
                        //disabling the upgrade possibility of station if the level of energy is lower than necessary to upgrade current station
                        if (stationReference.stationCurrentLevel != stationReference.upgradeCounts)
                        {
                            if (Lists.energyOfPlayer < stationReference.nextUpgradeEnergyCount)
                            {
                                //if (stationReference.stationCurrentLevel < stationReference.upgradeCounts)
                                //{
                                //upgradeText.gameObject.SetActive(false);
                                if (stationReference.stationCurrentLevel == 0) upgradeText.text = Constants.Instance.enrgy0to1Upgrd.ToString();
                                else if (stationReference.stationCurrentLevel == 1) upgradeText.text = Constants.Instance.enrgy1to2Upgrd.ToString();
                                else if (stationReference.stationCurrentLevel == 2) upgradeText.text = Constants.Instance.enrgy2to3Upgrd.ToString();
                                upgradeText.transform.parent.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                                upgradeText.transform.parent.GetComponent<Button>().enabled = false;
                                //}
                            }
                        }
                        updatingGreedsOnProductionButton();
                    }
                    //if limits will be surpassed with planning to produce ship there will appear the warning
                    else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
                }
                else
                {
                    limitReachedWarning(Constants.Instance.getNoEnergy());
                }
            }
            else if (shipType == D1)
            {
                if (Lists.energyOfPlayer > Constants.Instance.D1ProdEnergy)
                {
                    //this is a ships limit trigger for preventing overproduction. So if player wants to set next production ship that is not class 1 or 2 cruiser 4
                    //the limit is 45 
                    if (stationReference.stationFleetCount() < Constants.Instance.shipsLimit)
                    {
                        //so if there is at least one ship on plan this function will not work
                        //cause this snippet of code is for starting production from zero plan
                        //if (stationReference.productionPlan.Count < 1)
                        //{
                        //    stationReference.Destr1Produc++; //anyway it is necessary to add an production count to show player what is produced and refused in case of dismission
                        //    currenProductionBckgr.sprite = Destr1Spr;
                        //    currenProductionFill.sprite = Destr1Spr;
                        //    stationReference.shipProductionIsOn = true;
                        //    stationReference.energyProductionIsOn = false;
                        //    stationReference.commontPruductTimeLocal = stationReference.Destr1ProductTime;
                        //    stationReference.currentPruductionFillLocal = 0;
                        //    //currenProductionFill.fillAmount = 0;
                        //    stationReference.productionPlan.Add(D1); //populating production plan with current type of ship
                        //    stationReference.CurrentProducedShipLocal = D1;
                        //}
                        ////so if current production plan is set already this function will only add a next ship to produce to the plan
                        //else
                        //{
                        //    stationReference.Destr1Produc++;
                        //    stationReference.productionPlan.Add(D1); //populating production plan with current type of ship
                        //}
                        ////updating the Text UI params
                        //destr1ProductStation.text = stationReference.Destr1Produc.ToString();

                        stationReference.Destr1++;
                        //consuming the energy of player to produce this type of ship
                        Lists.energyOfPlayer -= Constants.Instance.D1ProdEnergy;
                        energyCount.text = Lists.energyOfPlayer.ToString("0");
                        //disabling the upgrade possibility of station if the level of energy is lower than necessary to upgrade current station
                        if (stationReference.stationCurrentLevel != stationReference.upgradeCounts)
                        {
                            if (Lists.energyOfPlayer < stationReference.nextUpgradeEnergyCount)
                            {
                                //if (stationReference.stationCurrentLevel < stationReference.upgradeCounts)
                                //{
                                //upgradeText.gameObject.SetActive(false);
                                if (stationReference.stationCurrentLevel == 0) upgradeText.text = Constants.Instance.enrgy0to1Upgrd.ToString();
                                else if (stationReference.stationCurrentLevel == 1) upgradeText.text = Constants.Instance.enrgy1to2Upgrd.ToString();
                                else if (stationReference.stationCurrentLevel == 2) upgradeText.text = Constants.Instance.enrgy2to3Upgrd.ToString();
                                upgradeText.transform.parent.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                                upgradeText.transform.parent.GetComponent<Button>().enabled = false;
                                //}
                            }
                        }
                        updatingGreedsOnProductionButton();
                    }
                    //if limits will be surpassed with planning to produce ship there will appear the warning
                    else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
                }
                else
                {
                    limitReachedWarning(Constants.Instance.getNoEnergy());
                }
            }
            else if (shipType == D1P)
            {
                if (Lists.energyOfPlayer > Constants.Instance.D1PProdEnergy)
                {
                    //this is a ships limit trigger for preventing overproduction. So if player wants to set next production ship that is not class 1 or 2 cruiser 4
                    //the limit is 45 
                    if (stationReference.stationFleetCount() < Constants.Instance.shipsLimit)
                    {
                        //so if there is at least one ship on plan this function will not work
                        //cause this snippet of code is for starting production from zero plan
                        //if (stationReference.productionPlan.Count < 1)
                        //{
                        //    stationReference.Destr1ProducPar++; //anyway it is necessary to add an production count to show player what is produced and refused in case of dismission
                        //    currenProductionBckgr.sprite = Destr1ParSpr;
                        //    currenProductionFill.sprite = Destr1ParSpr;
                        //    stationReference.shipProductionIsOn = true;
                        //    stationReference.energyProductionIsOn = false;
                        //    stationReference.commontPruductTimeLocal = stationReference.Destr1ParProductTime;
                        //    stationReference.currentPruductionFillLocal = 0;
                        //    //currenProductionFill.fillAmount = 0;
                        //    stationReference.productionPlan.Add(D1P); //populating production plan with current type of ship
                        //    stationReference.CurrentProducedShipLocal = D1P;
                        //}
                        ////so if current production plan is set already this function will only add a next ship to produce to the plan
                        //else
                        //{
                        //    stationReference.Destr1ProducPar++;
                        //    stationReference.productionPlan.Add(D1P); //populating production plan with current type of ship
                        //}
                        ////updating the Text UI params
                        //destr1ParProductStation.text = stationReference.Destr1ProducPar.ToString();

                        stationReference.Destr1Par++;
                        //consuming the energy of player to produce this type of ship
                        Lists.energyOfPlayer -= Constants.Instance.D1PProdEnergy;
                        energyCount.text = Lists.energyOfPlayer.ToString("0");
                        //disabling the upgrade possibility of station if the level of energy is lower than necessary to upgrade current station
                        if (stationReference.stationCurrentLevel != stationReference.upgradeCounts)
                        {
                            if (Lists.energyOfPlayer < stationReference.nextUpgradeEnergyCount)
                            {
                                //if (stationReference.stationCurrentLevel < stationReference.upgradeCounts)
                                //{
                                //upgradeText.gameObject.SetActive(false);
                                if (stationReference.stationCurrentLevel == 0) upgradeText.text = Constants.Instance.enrgy0to1Upgrd.ToString();
                                else if (stationReference.stationCurrentLevel == 1) upgradeText.text = Constants.Instance.enrgy1to2Upgrd.ToString();
                                else if (stationReference.stationCurrentLevel == 2) upgradeText.text = Constants.Instance.enrgy2to3Upgrd.ToString();
                                upgradeText.transform.parent.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                                upgradeText.transform.parent.GetComponent<Button>().enabled = false;
                                //}
                            }
                        }
                        updatingGreedsOnProductionButton();
                    }
                    //if limits will be surpassed with planning to produce ship there will appear the warning
                    else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
                }
                else
                {
                    limitReachedWarning(Constants.Instance.getNoEnergy());
                }
            }
            else if (shipType == D4)
            {
                if (Lists.energyOfPlayer > Constants.Instance.D4ProdEnergy)
                {
                    //this is a ships limit trigger for preventing overproduction. So if player wants to set next production ship that is not class 1 or 2 cruiser 4
                    //the limit is 45 
                    if (stationReference.stationFleetCount() < Constants.Instance.shipsLimit)
                    {
                        //so if there is at least one ship on plan this function will not work
                        //cause this snippet of code is for starting production from zero plan
                        //if (stationReference.productionPlan.Count < 1)
                        //{
                        //    stationReference.Destr4Produc++; //anyway it is necessary to add an production count to show player what is produced and refused in case of dismission
                        //    currenProductionBckgr.sprite = Destr4Spr;
                        //    currenProductionFill.sprite = Destr4Spr;
                        //    stationReference.shipProductionIsOn = true;
                        //    stationReference.energyProductionIsOn = false;
                        //    stationReference.commontPruductTimeLocal = stationReference.Destr4ProductTime;
                        //    stationReference.currentPruductionFillLocal = 0;
                        //    //currenProductionFill.fillAmount = 0;
                        //    stationReference.productionPlan.Add(D4); //populating production plan with current type of ship
                        //    stationReference.CurrentProducedShipLocal = D4;
                        //}
                        ////so if current production plan is set already this function will only add a next ship to produce to the plan
                        //else
                        //{
                        //    stationReference.Destr4Produc++;
                        //    stationReference.productionPlan.Add(D4); //populating production plan with current type of ship
                        //}

                        ////updating the Text UI params
                        //destr4ProductStation.text = stationReference.Destr4Produc.ToString();

                        stationReference.Destr4++;

                        //consuming the energy of player to produce this type of ship
                        Lists.energyOfPlayer -= Constants.Instance.D4ProdEnergy;
                        energyCount.text = Lists.energyOfPlayer.ToString("0");
                        //disabling the upgrade possibility of station if the level of energy is lower than necessary to upgrade current station
                        if (stationReference.stationCurrentLevel != stationReference.upgradeCounts)
                        {
                            if (Lists.energyOfPlayer < stationReference.nextUpgradeEnergyCount)
                            {
                                //if (stationReference.stationCurrentLevel < stationReference.upgradeCounts)
                                //{
                                //upgradeText.gameObject.SetActive(false);
                                if (stationReference.stationCurrentLevel == 0) upgradeText.text = Constants.Instance.enrgy0to1Upgrd.ToString();
                                else if (stationReference.stationCurrentLevel == 1) upgradeText.text = Constants.Instance.enrgy1to2Upgrd.ToString();
                                else if (stationReference.stationCurrentLevel == 2) upgradeText.text = Constants.Instance.enrgy2to3Upgrd.ToString();
                                upgradeText.transform.parent.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                                upgradeText.transform.parent.GetComponent<Button>().enabled = false;
                                //}
                            }
                        }
                        updatingGreedsOnProductionButton();
                    }
                    //if limits will be surpassed with planning to produce ship there will appear the warning
                    else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
                }
                else {
                    limitReachedWarning(Constants.Instance.getNoEnergy());
                }
            }
            else if (shipType == G1)
            {
                if (Lists.energyOfPlayer > Constants.Instance.G1ProdEnergy)
                {
                    //this is a ships limit trigger for preventing overproduction. So if player wants to set next production ship that is not class 1 or 2 cruiser 4
                    //the limit is 45 
                    if (stationReference.stationFleetCount() < Constants.Instance.shipsLimit && (stationReference.Gun1 + stationReference.Gun1Produc + stationReference.Gun2 +
                    stationReference.Gun2Produc + stationReference.Gun3 + stationReference.Gun3Produc) < 2)
                    {
                        //so if there is at least one ship on plan this function will not work
                        //cause this snippet of code is for starting production from zero plan
                        //if (stationReference.productionPlan.Count < 1)
                        //{
                        //    stationReference.Gun1Produc++; //anyway it is necessary to add an production count to show player what is produced and refused in case of dismission
                        //    currenProductionBckgr.sprite = Gun1Spr;
                        //    currenProductionFill.sprite = Gun1Spr;
                        //    stationReference.shipProductionIsOn = true;
                        //    stationReference.energyProductionIsOn = false;
                        //    stationReference.commontPruductTimeLocal = stationReference.Gun1ProductTime;
                        //    stationReference.currentPruductionFillLocal = 0;
                        //    //currenProductionFill.fillAmount = 0;
                        //    stationReference.productionPlan.Add(G1); //populating production plan with current type of ship
                        //    stationReference.CurrentProducedShipLocal = G1;
                        //}
                        ////so if current production plan is set already this function will only add a next ship to produce to the plan
                        //else
                        //{
                        //    stationReference.Gun1Produc++;
                        //    stationReference.productionPlan.Add(G1); //populating production plan with current type of ship
                        //}
                        ////updating the Text UI params
                        //gun1ProductStation.text = stationReference.Gun1Produc.ToString();

                        stationReference.Gun1++;

                        //consuming the energy of player to produce this type of ship
                        Lists.energyOfPlayer -= Constants.Instance.G1ProdEnergy;
                        energyCount.text = Lists.energyOfPlayer.ToString("0");
                        //disabling the upgrade possibility of station if the level of energy is lower than necessary to upgrade current station
                        if (stationReference.stationCurrentLevel != stationReference.upgradeCounts)
                        {
                            if (Lists.energyOfPlayer < stationReference.nextUpgradeEnergyCount)
                            {
                                //if (stationReference.stationCurrentLevel < stationReference.upgradeCounts)
                                //{
                                //upgradeText.gameObject.SetActive(false);
                                if (stationReference.stationCurrentLevel == 0) upgradeText.text = Constants.Instance.enrgy0to1Upgrd.ToString();
                                else if (stationReference.stationCurrentLevel == 1) upgradeText.text = Constants.Instance.enrgy1to2Upgrd.ToString();
                                else if (stationReference.stationCurrentLevel == 2) upgradeText.text = Constants.Instance.enrgy2to3Upgrd.ToString();
                                upgradeText.transform.parent.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                                upgradeText.transform.parent.GetComponent<Button>().enabled = false;
                                //}
                            }
                        }
                        updatingGreedsOnProductionButton();
                    }
                    //if limits will be surpassed with planning to produce ship there will appear the warning
                    else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
                }
                else
                {
                    limitReachedWarning(Constants.Instance.getNoEnergy());
                }
            }
            else if (shipType == G2)
            {
                if (Lists.energyOfPlayer > Constants.Instance.G2ProdEnergy)
                {
                    //this is a ships limit trigger for preventing overproduction. So if player wants to set next production ship that is not class 1 or 2 cruiser 4
                    //the limit is 45 
                    if (stationReference.stationFleetCount() < Constants.Instance.shipsLimit && (stationReference.Gun1 + stationReference.Gun1Produc + stationReference.Gun2 +
                    stationReference.Gun2Produc + stationReference.Gun3 + stationReference.Gun3Produc) < 2)
                    {
                        //so if there is at least one ship on plan this function will not work
                        //cause this snippet of code is for starting production from zero plan
                        //if (stationReference.productionPlan.Count < 1)
                        //{
                        //    stationReference.Gun2Produc++; //anyway it is necessary to add an production count to show player what is produced and refused in case of dismission
                        //    currenProductionBckgr.sprite = Gun2Spr;
                        //    currenProductionFill.sprite = Gun2Spr;
                        //    stationReference.shipProductionIsOn = true;
                        //    stationReference.energyProductionIsOn = false;
                        //    stationReference.commontPruductTimeLocal = stationReference.Gun2ProductTime;
                        //    stationReference.currentPruductionFillLocal = 0;
                        //    //currenProductionFill.fillAmount = 0;
                        //    stationReference.productionPlan.Add(G2); //populating production plan with current type of ship
                        //    stationReference.CurrentProducedShipLocal = G2;
                        //}
                        ////so if current production plan is set already this function will only add a next ship to produce to the plan
                        //else
                        //{
                        //    stationReference.Gun2Produc++;
                        //    stationReference.productionPlan.Add(G2); //populating production plan with current type of ship
                        //}
                        ////updating the Text UI params
                        //gun2ProductStation.text = stationReference.Gun2Produc.ToString();

                        stationReference.Gun2++;
                        //consuming the energy of player to produce this type of ship
                        Lists.energyOfPlayer -= Constants.Instance.G2ProdEnergy;
                        energyCount.text = Lists.energyOfPlayer.ToString("0");
                        //disabling the upgrade possibility of station if the level of energy is lower than necessary to upgrade current station
                        if (stationReference.stationCurrentLevel != stationReference.upgradeCounts)
                        {
                            if (Lists.energyOfPlayer < stationReference.nextUpgradeEnergyCount)
                            {
                                //if (stationReference.stationCurrentLevel < stationReference.upgradeCounts)
                                //{
                                //upgradeText.gameObject.SetActive(false);
                                if (stationReference.stationCurrentLevel == 0) upgradeText.text = Constants.Instance.enrgy0to1Upgrd.ToString();
                                else if (stationReference.stationCurrentLevel == 1) upgradeText.text = Constants.Instance.enrgy1to2Upgrd.ToString();
                                else if (stationReference.stationCurrentLevel == 2) upgradeText.text = Constants.Instance.enrgy2to3Upgrd.ToString();
                                upgradeText.transform.parent.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                                upgradeText.transform.parent.GetComponent<Button>().enabled = false;
                                //}
                            }
                        }
                        updatingGreedsOnProductionButton();
                    }
                    //if limits will be surpassed with planning to produce ship there will appear the warning
                    else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
                }
                else
                {
                    limitReachedWarning(Constants.Instance.getNoEnergy());
                }
            }
            else if (shipType == G3)
            {
                if (Lists.energyOfPlayer > Constants.Instance.G3ProdEnergy)
                {
                    //this is a ships limit trigger for preventing overproduction. So if player wants to set next production ship that is not class 1 or 2 cruiser 4
                    //the limit is 45 
                    if (stationReference.stationFleetCount() < Constants.Instance.shipsLimit && (stationReference.Gun1 + stationReference.Gun1Produc + stationReference.Gun2 +
                    stationReference.Gun2Produc + stationReference.Gun3 + stationReference.Gun3Produc) < 2)
                    {
                        //so if there is at least one ship on plan this function will not work
                        //cause this snippet of code is for starting production from zero plan
                        //if (stationReference.productionPlan.Count < 1)
                        //{
                        //    stationReference.Gun3Produc++; //anyway it is necessary to add an production count to show player what is produced and refused in case of dismission
                        //    currenProductionBckgr.sprite = Gun3Spr;
                        //    currenProductionFill.sprite = Gun3Spr;
                        //    stationReference.shipProductionIsOn = true;
                        //    stationReference.energyProductionIsOn = false;
                        //    stationReference.commontPruductTimeLocal = stationReference.Gun3ProductTime;
                        //    stationReference.currentPruductionFillLocal = 0;
                        //    //currenProductionFill.fillAmount = 0;
                        //    stationReference.productionPlan.Add(G3); //populating production plan with current type of ship
                        //    stationReference.CurrentProducedShipLocal = G3;
                        //}
                        ////so if current production plan is set already this function will only add a next ship to produce to the plan
                        //else
                        //{
                        //    stationReference.Gun3Produc++;
                        //    stationReference.productionPlan.Add(G3); //populating production plan with current type of ship
                        //}
                        ////updating the Text UI params
                        //gun3ProductStation.text = stationReference.Gun3Produc.ToString();

                        stationReference.Gun3++;
                        //consuming the energy of player to produce this type of ship
                        Lists.energyOfPlayer -= Constants.Instance.G3ProdEnergy;
                        energyCount.text = Lists.energyOfPlayer.ToString("0");
                        //disabling the upgrade possibility of station if the level of energy is lower than necessary to upgrade current station
                        if (stationReference.stationCurrentLevel != stationReference.upgradeCounts)
                        {
                            if (Lists.energyOfPlayer < stationReference.nextUpgradeEnergyCount)
                            {
                                //if (stationReference.stationCurrentLevel < stationReference.upgradeCounts)
                                //{
                                //upgradeText.gameObject.SetActive(false);
                                if (stationReference.stationCurrentLevel == 0) upgradeText.text = Constants.Instance.enrgy0to1Upgrd.ToString();
                                else if (stationReference.stationCurrentLevel == 1) upgradeText.text = Constants.Instance.enrgy1to2Upgrd.ToString();
                                else if (stationReference.stationCurrentLevel == 2) upgradeText.text = Constants.Instance.enrgy2to3Upgrd.ToString();
                                upgradeText.transform.parent.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                                upgradeText.transform.parent.GetComponent<Button>().enabled = false;
                                //}
                            }
                        }
                        updatingGreedsOnProductionButton();
                    }
                    //if limits will be surpassed with planning to produce ship there will appear the warning
                    else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
                }
                else
                {
                    limitReachedWarning(Constants.Instance.getNoEnergy());
                }
            }
            else if (shipType == GM)
            {
                if (Lists.energyOfPlayer > Constants.Instance.GMProdEnergy)
                {
                    //this is a ships limit trigger for preventing overproduction. So if player wants to set next production ship that is not class 1 or 2 cruiser 4
                    //the limit is 45 
                    if (stationReference.stationFleetCount() < Constants.Instance.shipsLimit && (stationReference.MiniGun + stationReference.MiniGunProduc) < 8)
                    {
                        //so if there is at least one ship on plan this function will not work
                        //cause this snippet of code is for starting production from zero plan
                        //if (stationReference.productionPlan.Count < 1)
                        //{
                        //    stationReference.MiniGunProduc++; //anyway it is necessary to add an production count to show player what is produced and refused in case of dismission
                        //    currenProductionBckgr.sprite = MiniGunSpr;
                        //    currenProductionFill.sprite = MiniGunSpr;
                        //    stationReference.shipProductionIsOn = true;
                        //    stationReference.energyProductionIsOn = false;
                        //    stationReference.commontPruductTimeLocal = stationReference.MiniGunProductTime;
                        //    stationReference.currentPruductionFillLocal = 0;
                        //    //currenProductionFill.fillAmount = 0;
                        //    stationReference.productionPlan.Add(GM); //populating production plan with current type of ship
                        //    stationReference.CurrentProducedShipLocal = GM;
                        //}
                        ////so if current production plan is set already this function will only add a next ship to produce to the plan
                        //else
                        //{
                        //    stationReference.MiniGunProduc++;
                        //    stationReference.productionPlan.Add(GM); //populating production plan with current type of ship
                        //}
                        ////updating the Text UI params
                        //MiniGunProductStation.text = stationReference.MiniGunProduc.ToString();

                        stationReference.MiniGun++;
                        //consuming the energy of player to produce this type of ship
                        Lists.energyOfPlayer -= Constants.Instance.GMProdEnergy;
                        energyCount.text = Lists.energyOfPlayer.ToString("0");
                        //disabling the upgrade possibility of station if the level of energy is lower than necessary to upgrade current station
                        if (stationReference.stationCurrentLevel != stationReference.upgradeCounts)
                        {
                            if (Lists.energyOfPlayer < stationReference.nextUpgradeEnergyCount)
                            {
                                //if (stationReference.stationCurrentLevel < stationReference.upgradeCounts)
                                //{
                                //upgradeText.gameObject.SetActive(false);
                                if (stationReference.stationCurrentLevel == 0) upgradeText.text = Constants.Instance.enrgy0to1Upgrd.ToString();
                                else if (stationReference.stationCurrentLevel == 1) upgradeText.text = Constants.Instance.enrgy1to2Upgrd.ToString();
                                else if (stationReference.stationCurrentLevel == 2) upgradeText.text = Constants.Instance.enrgy2to3Upgrd.ToString();
                                upgradeText.transform.parent.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                                upgradeText.transform.parent.GetComponent<Button>().enabled = false;
                                //}
                            }
                        }
                        updatingGreedsOnProductionButton();
                    }
                    //if limits will be surpassed with planning to produce ship there will appear the warning
                    else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
                }
                else
                {
                    limitReachedWarning(Constants.Instance.getNoEnergy());
                }
            }
            CommonButtonAudio.Play();
        }
    }

    private void updatingGreedsOnProductionButton() {
        if (!cruis1ValueStation.IsActive() && stationReference.Cruis1 > 0)
        {
            fleetGreedSizeStation++;
            //increase or decreace the width of greed dynamically depending on count of ship types
            StationFleetGreedRect.sizeDelta = new Vector2(fleetGreedSizeStation * 180, 100f);
            cruis1ValueStation.transform.parent.gameObject.SetActive(true);
        }
        if (!cruis2ValueStation.IsActive() && stationReference.Cruis2 > 0)
        {
            fleetGreedSizeStation++;
            //increase or decreace the width of greed dynamically depending on count of ship types
            StationFleetGreedRect.sizeDelta = new Vector2(fleetGreedSizeStation * 180, 100f);
            cruis2ValueStation.transform.parent.gameObject.SetActive(true);
        }
        if (!cruis3ValueStation.IsActive() && stationReference.Cruis3 > 0)
        {
            fleetGreedSizeStation++;
            //increase or decreace the width of greed dynamically depending on count of ship types
            StationFleetGreedRect.sizeDelta = new Vector2(fleetGreedSizeStation * 180, 100f);
            cruis3ValueStation.transform.parent.gameObject.SetActive(true);
        }
        if (!cruis4ValueStation.IsActive() && stationReference.Cruis4 > 0)
        {
            fleetGreedSizeStation++;
            //increase or decreace the width of greed dynamically depending on count of ship types
            StationFleetGreedRect.sizeDelta = new Vector2(fleetGreedSizeStation * 180, 100f);
            cruis4ValueStation.transform.parent.gameObject.SetActive(true);
        }
        if (!destr1ValueStation.IsActive() && stationReference.Destr1 > 0)
        {
            fleetGreedSizeStation++;
            //increase or decreace the width of greed dynamically depending on count of ship types
            StationFleetGreedRect.sizeDelta = new Vector2(fleetGreedSizeStation * 180, 100f);
            destr1ValueStation.transform.parent.gameObject.SetActive(true);
        }
        if (!destr1ParValueStation.IsActive() && stationReference.Destr1Par > 0)
        {
            fleetGreedSizeStation++;
            //increase or decreace the width of greed dynamically depending on count of ship types
            StationFleetGreedRect.sizeDelta = new Vector2(fleetGreedSizeStation * 180, 100f);
            destr1ParValueStation.transform.parent.gameObject.SetActive(true);
        }
        if (!destr2ValueStation.IsActive() && stationReference.Destr2 > 0)
        {
            fleetGreedSizeStation++;
            //increase or decreace the width of greed dynamically depending on count of ship types
            StationFleetGreedRect.sizeDelta = new Vector2(fleetGreedSizeStation * 180, 100f);
            destr2ValueStation.transform.parent.gameObject.SetActive(true);
        }
        if (!destr2ParValueStation.IsActive() && stationReference.Destr2Par > 0)
        {
            fleetGreedSizeStation++;
            //increase or decreace the width of greed dynamically depending on count of ship types
            StationFleetGreedRect.sizeDelta = new Vector2(fleetGreedSizeStation * 180, 100f);
            destr2ParValueStation.transform.parent.gameObject.SetActive(true);
        }
        if (!destr3ValueStation.IsActive() && stationReference.Destr3 > 0)
        {
            fleetGreedSizeStation++;
            //increase or decreace the width of greed dynamically depending on count of ship types
            StationFleetGreedRect.sizeDelta = new Vector2(fleetGreedSizeStation * 180, 100f);
            destr3ValueStation.transform.parent.gameObject.SetActive(true);
        }
        if (!destr4ValueStation.IsActive() && stationReference.Destr4 > 0)
        {
            fleetGreedSizeStation++;
            //increase or decreace the width of greed dynamically depending on count of ship types
            StationFleetGreedRect.sizeDelta = new Vector2(fleetGreedSizeStation * 180, 100f);
            destr4ValueStation.transform.parent.gameObject.SetActive(true);
        }
        if (!gun1ValueStation.IsActive() && stationReference.Gun1 > 0)
        {
            fleetGreedSizeStation++;
            //increase or decreace the width of greed dynamically depending on count of ship types
            StationFleetGreedRect.sizeDelta = new Vector2(fleetGreedSizeStation * 180, 100f);
            gun1ValueStation.transform.parent.gameObject.SetActive(true);
        }
        if (!gun2ValueStation.IsActive() && stationReference.Gun2 > 0)
        {
            fleetGreedSizeStation++;
            //increase or decreace the width of greed dynamically depending on count of ship types
            StationFleetGreedRect.sizeDelta = new Vector2(fleetGreedSizeStation * 180, 100f);
            gun2ValueStation.transform.parent.gameObject.SetActive(true);
        }
        if (!gun3ValueStation.IsActive() && stationReference.Gun3 > 0)
        {
            fleetGreedSizeStation++;
            //increase or decreace the width of greed dynamically depending on count of ship types
            StationFleetGreedRect.sizeDelta = new Vector2(fleetGreedSizeStation * 180, 100f);
            gun3ValueStation.transform.parent.gameObject.SetActive(true);
        }
        if (!MiniGunValueStation.IsActive() && stationReference.MiniGun > 0)
        {
            fleetGreedSizeStation++;
            //increase or decreace the width of greed dynamically depending on count of ship types
            StationFleetGreedRect.sizeDelta = new Vector2(fleetGreedSizeStation * 180, 100f);
            MiniGunValueStation.transform.parent.gameObject.SetActive(true);
        }

        cruis1ProductStation.text = stationReference.Cruis1Produc.ToString();
        cruis1ValueStation.text = stationReference.Cruis1.ToString();

        cruis2ProductStation.text = stationReference.Cruis2Produc.ToString();
        cruis2ValueStation.text = stationReference.Cruis2.ToString();

        cruis3ProductStation.text = stationReference.Cruis3Produc.ToString();
        cruis3ValueStation.text = stationReference.Cruis3.ToString();

        cruis4ProductStation.text = stationReference.Cruis4Produc.ToString();
        cruis4ValueStation.text = stationReference.Cruis4.ToString();

        destr1ProductStation.text = stationReference.Destr1Produc.ToString();
        destr1ValueStation.text = stationReference.Destr1.ToString();

        destr1ParProductStation.text = stationReference.Destr1ProducPar.ToString();
        destr1ParValueStation.text = stationReference.Destr1Par.ToString();

        destr2ProductStation.text = stationReference.Destr2Produc.ToString();
        destr2ValueStation.text = stationReference.Destr2.ToString();

        destr2ParProductStation.text = stationReference.Destr2ProducPar.ToString();
        destr2ParValueStation.text = stationReference.Destr2Par.ToString();

        destr3ProductStation.text = stationReference.Destr3Produc.ToString();
        destr3ValueStation.text = stationReference.Destr3.ToString();

        destr4ProductStation.text = stationReference.Destr4Produc.ToString();
        destr4ValueStation.text = stationReference.Destr4.ToString();

        gun1ProductStation.text = stationReference.Gun1Produc.ToString();
        gun1ValueStation.text = stationReference.Gun1.ToString();

        gun2ProductStation.text = stationReference.Gun2Produc.ToString();
        gun2ValueStation.text = stationReference.Gun2.ToString();

        gun3ProductStation.text = stationReference.Gun3Produc.ToString();
        gun3ValueStation.text = stationReference.Gun3.ToString();

        MiniGunProductStation.text = stationReference.MiniGunProduc.ToString();
        MiniGunValueStation.text = stationReference.MiniGun.ToString();

        //if (stationReference.productionPlan.Count > 0)
        //{
        //    launchProductionAuto(stationReference.productionPlan[0]);
        //}
        //else energyProductionLaunche();

        //setting upgrade button and text active by checking current energy level and if current station has a step to upgrade
        //if (Lists.energyOfPlayer >= stationReference.nextUpgradeEnergyCount && stationReference.stationCurrentLevel < stationReference.upgradeCounts)
        //{
        //    //upgradeText.gameObject.SetActive(true);
        //    upgradeText.text = Constants.Instance.getUpGrade();
        //    upgradeText.transform.parent.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        //    upgradeText.transform.parent.GetComponent<Button>().enabled = true;
        //}
    }

    //private void launchProductionAuto(string shipType)
    //{
    //    if (shipType == C1)
    //    {
    //        currenProductionBckgr.sprite = Cruis1Spr;
    //        currenProductionFill.sprite = Cruis1Spr;
    //        cruis1ProductStation.text = stationReference.Cruis1Produc.ToString();
    //    }
    //    else if (shipType == C2)
    //    {
    //        currenProductionBckgr.sprite = Cruis2Spr;
    //        currenProductionFill.sprite = Cruis2Spr;
    //        cruis2ProductStation.text = stationReference.Cruis2Produc.ToString();
    //    }
    //    else if (shipType == C3)
    //    {
    //        currenProductionBckgr.sprite = Cruis3Spr;
    //        currenProductionFill.sprite = Cruis3Spr;
    //        cruis3ProductStation.text = stationReference.Cruis3Produc.ToString();
    //    }
    //    else if (shipType == C4)
    //    {
    //        currenProductionBckgr.sprite = Cruis4Spr;
    //        currenProductionFill.sprite = Cruis4Spr;
    //        cruis4ProductStation.text = stationReference.Cruis4Produc.ToString();
    //    }
    //    else if (shipType == D1)
    //    {
    //        currenProductionBckgr.sprite = Destr1Spr;
    //        currenProductionFill.sprite = Destr1Spr;
    //        destr1ProductStation.text = stationReference.Destr1Produc.ToString();
    //    }
    //    else if (shipType == D1P)
    //    {
    //        currenProductionBckgr.sprite = Destr1ParSpr;
    //        currenProductionFill.sprite = Destr1ParSpr;
    //        destr1ParProductStation.text = stationReference.Destr1ProducPar.ToString();
    //    }
    //    else if (shipType == D2)
    //    {
    //        currenProductionBckgr.sprite = Destr2Spr;
    //        currenProductionFill.sprite = Destr2Spr;
    //        destr2ProductStation.text = stationReference.Destr2Produc.ToString();
    //    }
    //    else if (shipType == D2P)
    //    {
    //        currenProductionBckgr.sprite = Destr2ParSpr;
    //        currenProductionFill.sprite = Destr2ParSpr;
    //        destr2ParProductStation.text = stationReference.Destr2ProducPar.ToString();
    //    }
    //    else if (shipType == D3)
    //    {
    //        currenProductionBckgr.sprite = Destr3Spr;
    //        currenProductionFill.sprite = Destr3Spr;
    //        destr3ProductStation.text = stationReference.Destr3Produc.ToString();
    //    }
    //    else if (shipType == D4)
    //    {
    //        currenProductionBckgr.sprite = Destr4Spr;
    //        currenProductionFill.sprite = Destr4Spr;
    //        destr4ProductStation.text = stationReference.Destr4Produc.ToString();
    //    }
    //    else if (shipType == G1)
    //    {
    //        currenProductionBckgr.sprite = Gun1Spr;
    //        currenProductionFill.sprite = Gun1Spr;
    //        gun1ProductStation.text = stationReference.Gun1Produc.ToString();
    //    }
    //    else if (shipType == G2)
    //    {
    //        currenProductionBckgr.sprite = Gun2Spr;
    //        currenProductionFill.sprite = Gun2Spr;
    //        gun2ProductStation.text = stationReference.Gun2Produc.ToString();
    //    }
    //    else if (shipType == G3)
    //    {
    //        currenProductionBckgr.sprite = Gun3Spr;
    //        currenProductionFill.sprite = Gun3Spr;
    //        gun3ProductStation.text = stationReference.Gun3Produc.ToString();
    //    }
    //    else if (shipType == GM)
    //    {
    //        currenProductionBckgr.sprite = MiniGunSpr;
    //        currenProductionFill.sprite = MiniGunSpr;
    //        MiniGunProductStation.text = stationReference.MiniGunProduc.ToString();
    //    }
    //}

    //private void energyProductionLaunche()
    //{
    //    currenProductionBckgr.sprite = EnergySpr;
    //    currenProductionFill.sprite = EnergySpr;
    //    //stationReference.energyProductionIsOn = true;
    //    //CommonProductTime = energyProductTimeLocal;
    //    //currenProductionFill.fillAmount = 0;
    //}

    //public void cancelProduction(String shipType) {

    //    //so player can cancel the production of ship if only it is not currently producing or there are more than on cruisers of that type under production
    //    if (shipType == C1 && stationReference.Cruis1Produc > 0 && (stationReference.CurrentProducedShipLocal != shipType || stationReference.Cruis1Produc > 1))
    //    {
    //        stationReference.Cruis1Produc--;
    //        //removing this type of ship from production plan that stays in the end of range of production sequence of this ship types. This is to prvent the bug
    //        stationReference.productionPlan.RemoveAt(stationReference.productionPlan.LastIndexOf(C1)); //removing current ship type from producction plan
    //        //pay back the energy of rejected to produce ship and updating the UI text properites
    //        Lists.energyOfPlayer += Constants.Instance.C1ProdEnergy;
    //        energyCount.text = Lists.energyOfPlayer.ToString("0");
    //    }
    //    //so player can cancel the production of ship if only it is not currently producing or there are more than on cruisers of that type under production
    //    if (shipType == C2 && stationReference.Cruis2Produc > 0 && (stationReference.CurrentProducedShipLocal != shipType || stationReference.Cruis2Produc > 1))
    //    {
    //        stationReference.Cruis2Produc--;
    //        //removing this type of ship from production plan that stays in the end of range of production sequence of this ship types. This is to prvent the bug
    //        stationReference.productionPlan.RemoveAt(stationReference.productionPlan.LastIndexOf(C2)); //removing current ship type from producction plan
    //        //pay back the energy of rejected to produce ship and updating the UI text properites
    //        Lists.energyOfPlayer += Constants.Instance.C2ProdEnergy;
    //        energyCount.text = Lists.energyOfPlayer.ToString("0");
    //    }
    //    //so player can cancel the production of ship if only it is not currently producing or there are more than on cruisers of that type under production
    //    if (shipType == C3 && stationReference.Cruis3Produc > 0 && (stationReference.CurrentProducedShipLocal != shipType || stationReference.Cruis3Produc > 1))
    //    {
    //        stationReference.Cruis3Produc--;
    //        //removing this type of ship from production plan that stays in the end of range of production sequence of this ship types. This is to prvent the bug
    //        stationReference.productionPlan.RemoveAt(stationReference.productionPlan.LastIndexOf(C3)); //removing current ship type from producction plan
    //        //pay back the energy of rejected to produce ship and updating the UI text properites
    //        Lists.energyOfPlayer += Constants.Instance.C3ProdEnergy;
    //        energyCount.text = Lists.energyOfPlayer.ToString("0");
    //    }
    //    //so player can cancel the production of ship if only it is not currently producing or there are more than on cruisers of that type under production
    //    if (shipType == C4 && stationReference.Cruis4Produc > 0 && (stationReference.CurrentProducedShipLocal != shipType || stationReference.Cruis4Produc > 1))
    //    {
    //        stationReference.Cruis4Produc--;
    //        //removing this type of ship from production plan that stays in the end of range of production sequence of this ship types. This is to prvent the bug
    //        stationReference.productionPlan.RemoveAt(stationReference.productionPlan.LastIndexOf(C4)); //removing current ship type from producction plan
    //        //pay back the energy of rejected to produce ship and updating the UI text properites
    //        Lists.energyOfPlayer += Constants.Instance.C4ProdEnergy;
    //        energyCount.text = Lists.energyOfPlayer.ToString("0");
    //    }
    //    //so player can cancel the production of ship if only it is not currently producing or there are more than on cruisers of that type under production
    //    if (shipType == D1 && stationReference.Destr1Produc > 0 && (stationReference.CurrentProducedShipLocal != shipType || stationReference.Destr1Produc > 1))
    //    {
    //        stationReference.Destr1Produc--;
    //        //removing this type of ship from production plan that stays in the end of range of production sequence of this ship types. This is to prvent the bug
    //        stationReference.productionPlan.RemoveAt(stationReference.productionPlan.LastIndexOf(D1)); //removing current ship type from producction plan
    //        //pay back the energy of rejected to produce ship and updating the UI text properites
    //        Lists.energyOfPlayer += Constants.Instance.D1ProdEnergy;
    //        energyCount.text = Lists.energyOfPlayer.ToString("0");
    //    }
    //    //so player can cancel the production of ship if only it is not currently producing or there are more than on cruisers of that type under production
    //    if (shipType == D1P && stationReference.Destr1ProducPar > 0 && (stationReference.CurrentProducedShipLocal != shipType || stationReference.Destr1ProducPar > 1))
    //    {
    //        stationReference.Destr1ProducPar--;
    //        //removing this type of ship from production plan that stays in the end of range of production sequence of this ship types. This is to prvent the bug
    //        stationReference.productionPlan.RemoveAt(stationReference.productionPlan.LastIndexOf(D1P)); //removing current ship type from producction plan

    //        //pay back the energy of rejected to produce ship and updating the UI text properites
    //        Lists.energyOfPlayer += Constants.Instance.D1PProdEnergy;
    //        energyCount.text = Lists.energyOfPlayer.ToString("0");
    //    }
    //    //so player can cancel the production of ship if only it is not currently producing or there are more than on cruisers of that type under production
    //    if (shipType == D2 && stationReference.Destr2Produc > 0 && (stationReference.CurrentProducedShipLocal != shipType || stationReference.Destr2Produc > 1))
    //    {
    //        stationReference.Destr2Produc--;
    //        //removing this type of ship from production plan that stays in the end of range of production sequence of this ship types. This is to prvent the bug
    //        stationReference.productionPlan.RemoveAt(stationReference.productionPlan.LastIndexOf(D2)); //removing current ship type from producction plan
    //        //pay back the energy of rejected to produce ship and updating the UI text properites
    //        Lists.energyOfPlayer += Constants.Instance.D2ProdEnergy;
    //        energyCount.text = Lists.energyOfPlayer.ToString("0");
    //    }
    //    //so player can cancel the production of ship if only it is not currently producing or there are more than on cruisers of that type under production
    //    if (shipType == D2P && stationReference.Destr2ProducPar > 0 && (stationReference.CurrentProducedShipLocal != shipType || stationReference.Destr2ProducPar > 1))
    //    {
    //        stationReference.Destr2ProducPar--;
    //        //removing this type of ship from production plan that stays in the end of range of production sequence of this ship types. This is to prvent the bug
    //        stationReference.productionPlan.RemoveAt(stationReference.productionPlan.LastIndexOf(D2P)); //removing current ship type from producction plan
    //        //pay back the energy of rejected to produce ship and updating the UI text properites
    //        Lists.energyOfPlayer += Constants.Instance.D2PProdEnergy;
    //        energyCount.text = Lists.energyOfPlayer.ToString("0");
    //    }
    //    //so player can cancel the production of ship if only it is not currently producing or there are more than on cruisers of that type under production
    //    if (shipType == D3 && stationReference.Destr3Produc > 0 && (stationReference.CurrentProducedShipLocal != shipType || stationReference.Destr3Produc > 1))
    //    {
    //        stationReference.Destr3Produc--;
    //        //removing this type of ship from production plan that stays in the end of range of production sequence of this ship types. This is to prvent the bug
    //        stationReference.productionPlan.RemoveAt(stationReference.productionPlan.LastIndexOf(D3)); //removing current ship type from producction plan
    //        //pay back the energy of rejected to produce ship and updating the UI text properites
    //        Lists.energyOfPlayer += Constants.Instance.D3ProdEnergy;
    //        energyCount.text = Lists.energyOfPlayer.ToString("0");
    //    }
    //    //so player can cancel the production of ship if only it is not currently producing or there are more than on cruisers of that type under production
    //    if (shipType == D4 && stationReference.Destr4Produc > 0 && (stationReference.CurrentProducedShipLocal != shipType || stationReference.Destr4Produc > 1))
    //    {
    //        stationReference.Destr4Produc--;
    //        //removing this type of ship from production plan that stays in the end of range of production sequence of this ship types. This is to prvent the bug
    //        stationReference.productionPlan.RemoveAt(stationReference.productionPlan.LastIndexOf(D4)); //removing current ship type from producction plan
    //        //pay back the energy of rejected to produce ship and updating the UI text properites
    //        Lists.energyOfPlayer += Constants.Instance.D4ProdEnergy;
    //        energyCount.text = Lists.energyOfPlayer.ToString("0");
    //    }
    //    //so player can cancel the production of ship if only it is not currently producing or there are more than on cruisers of that type under production
    //    if (shipType == G1 && stationReference.Gun1Produc > 0 && (stationReference.CurrentProducedShipLocal != shipType || stationReference.Gun1Produc > 1))
    //    {
    //        stationReference.Gun1Produc--;
    //        //removing this type of ship from production plan that stays in the end of range of production sequence of this ship types. This is to prvent the bug
    //        stationReference.productionPlan.RemoveAt(stationReference.productionPlan.LastIndexOf(G1)); //removing current ship type from producction plan
    //        //pay back the energy of rejected to produce ship and updating the UI text properites
    //        Lists.energyOfPlayer += Constants.Instance.G1ProdEnergy;
    //        energyCount.text = Lists.energyOfPlayer.ToString("0");
    //    }
    //    if (shipType == G2 && stationReference.Gun2Produc > 0 && (stationReference.CurrentProducedShipLocal != shipType || stationReference.Gun2Produc > 1))
    //    {
    //        stationReference.Gun2Produc--;
    //        //removing this type of ship from production plan that stays in the end of range of production sequence of this ship types. This is to prvent the bug
    //        stationReference.productionPlan.RemoveAt(stationReference.productionPlan.LastIndexOf(G2)); //removing current ship type from producction plan
    //        //pay back the energy of rejected to produce ship and updating the UI text properites
    //        Lists.energyOfPlayer += Constants.Instance.G2ProdEnergy;
    //        energyCount.text = Lists.energyOfPlayer.ToString("0");
    //    }
    //    if (shipType == G3 && stationReference.Gun3Produc > 0 && (stationReference.CurrentProducedShipLocal != shipType || stationReference.Gun3Produc > 1))
    //    {
    //        stationReference.Gun3Produc--;
    //        //removing this type of ship from production plan that stays in the end of range of production sequence of this ship types. This is to prvent the bug
    //        stationReference.productionPlan.RemoveAt(stationReference.productionPlan.LastIndexOf(G3)); //removing current ship type from producction plan
    //        //pay back the energy of rejected to produce ship and updating the UI text properites
    //        Lists.energyOfPlayer += Constants.Instance.G3ProdEnergy;
    //        energyCount.text = Lists.energyOfPlayer.ToString("0");
    //    }
    //    if (shipType == GM && stationReference.MiniGunProduc > 0 && (stationReference.CurrentProducedShipLocal != shipType || stationReference.MiniGunProduc > 1))
    //    {
    //        stationReference.MiniGunProduc--;
    //        //removing this type of ship from production plan that stays in the end of range of production sequence of this ship types. This is to prvent the bug
    //        stationReference.productionPlan.RemoveAt(stationReference.productionPlan.LastIndexOf(GM)); //removing current ship type from producction plan
    //        //pay back the energy of rejected to produce ship and updating the UI text properites
    //        Lists.energyOfPlayer += Constants.Instance.GMProdEnergy;
    //        energyCount.text = Lists.energyOfPlayer.ToString("0");
    //    }

    //    CommonCancelAudio.Play();

    //    updateStationUIValues(stationReference);

    //    if (!stationReference.isUpgrading)
    //    {
    //        //setting back the upgrade possibility to station if the energy is enough to upgrade the station after canceling the production of ship of gun
    //        if (stationReference.stationCurrentLevel != stationReference.upgradeCounts)
    //        {
    //            if (Lists.energyOfPlayer >= stationReference.nextUpgradeEnergyCount)
    //            {
    //                upgradeText.text = Constants.Instance.getUpGrade();
    //                upgradeText.transform.parent.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    //                upgradeText.transform.parent.GetComponent<Button>().enabled = true;
    //            }
    //        }
    //    }
    //}

    //following two methods are designed to use with ships icons buttons to exchange with ships between station and ported cruiser
    //ships are determined by string values that assigned on button components of each icon
    public void putShipFromStationToCruiser(String shipType)
    {
        if (portedCruiserObject == null)
        {
            LaunchingObjcts newCruiser;
            cruiserListToActivatePlayer = ObjectPullerJourney.current.GetCruis4JourneyPlayerPullList();
            cruiserToActivatePlayer = ObjectPullerJourney.current.GetUniversalBullet(cruiserListToActivatePlayer);


            cruiserToActivatePlayer.transform.position = new Vector3(stationReference.transform.position.x + 5, -8, stationReference.transform.position.z + 5);
            cruiserToActivatePlayer.transform.rotation = Constants.Instance.playerCruiserStartRotation;

            newCruiser = cruiserToActivatePlayer.GetComponent<LaunchingObjcts>();
            stationReference.playerCruiserObject.Add(newCruiser);
            newCruiser.isPortedToPlayerStation = true;
            portedCruiserObject = newCruiser;
            cruiserToActivatePlayer.GetComponent<Collider>().enabled = true;
            //this one is necessary to eneable the collider of energy gather child component of player scene cruiser
            foreach (Collider col in cruiserToActivatePlayer.GetComponentsInChildren<Collider>()) col.enabled = true;
            cruiserToActivatePlayer.SetActive(true);
            Lists.shipsOnScene.Add(cruiserToActivatePlayer);

            //stationReference.playerCruiserNear = true;
            setPortedCruiserSprite();
            StationPortObject.SetActive(true);
            PortedCruiserObj.SetActive(true);
            updatePortedShipUIValues();

            if (shipType == C1)
            {
                if (playerCruiserShipsCount() <= Constants.Instance.shipsPreLimit)
                {
                    stationReference.Cruis1--;
                    portedCruiserObject.Cruis1++;
                    changePortedCruiserSprite(); //this method changes the cruiser of player if it now has higher level cruiser
                }
                else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
            }
            else if (shipType == C2)
            {
                if (playerCruiserShipsCount() <= Constants.Instance.shipsPreLimit)
                {
                    stationReference.Cruis2--;
                    portedCruiserObject.Cruis2++;
                    changePortedCruiserSprite(); //this method changes the cruiser of player if it now has higher level cruiser
                }
                else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
            }
            else if (shipType == C3)
            {
                if (playerCruiserShipsCount() < Constants.Instance.shipsLimit)
                {
                    stationReference.Cruis3--;
                    portedCruiserObject.Cruis3++;
                    changePortedCruiserSprite(); //this method changes the cruiser of player if it now has higher level cruiser
                }
                else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
            }
            else if (shipType == C4)
            {
                if (playerCruiserShipsCount() < Constants.Instance.shipsLimit)
                {
                    stationReference.Cruis4--;
                    portedCruiserObject.Cruis4++;
                    changePortedCruiserSprite(); //this method changes the cruiser of player if it now has higher level cruiser
                }
                else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
            }
            else if (shipType == D1)
            {
                if (playerCruiserShipsCount() < Constants.Instance.shipsLimit)
                {
                    stationReference.Destr1--;
                    portedCruiserObject.Destr1++;
                }
                else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
            }
            else if (shipType == D1P)
            {
                if (playerCruiserShipsCount() < Constants.Instance.shipsLimit)
                {
                    stationReference.Destr1Par--;
                    portedCruiserObject.Destr1Par++;
                }
                else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
            }
            else if (shipType == D2)
            {
                if (playerCruiserShipsCount() < Constants.Instance.shipsLimit)
                {
                    stationReference.Destr2--;
                    portedCruiserObject.Destr2++;
                }
                else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
            }
            else if (shipType == D2P)
            {
                if (playerCruiserShipsCount() < Constants.Instance.shipsLimit)
                {
                    stationReference.Destr2Par--;
                    portedCruiserObject.Destr2Par++;
                }
                else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
            }
            else if (shipType == D3)
            {
                if (playerCruiserShipsCount() < Constants.Instance.shipsLimit)
                {
                    stationReference.Destr3--;
                    portedCruiserObject.Destr3++;
                }
                else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
            }
            else if (shipType == D4)
            {
                if (playerCruiserShipsCount() < Constants.Instance.shipsLimit)
                {
                    stationReference.Destr4--;
                    portedCruiserObject.Destr4++;
                }
                else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
            }
            else if (shipType == G1)
            {
                if (playerCruiserShipsCount() < Constants.Instance.shipsLimit && (Lists.Gun1OfPlayerCruis + Lists.Gun2OfPlayerCruis + Lists.Gun3OfPlayerCruis) < 2)
                {
                    stationReference.Gun1--;
                    portedCruiserObject.Gun1++;
                }
                else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
            }
            else if (shipType == G2)
            {
                if (playerCruiserShipsCount() < Constants.Instance.shipsLimit && (Lists.Gun1OfPlayerCruis + Lists.Gun2OfPlayerCruis + Lists.Gun3OfPlayerCruis) < 2)
                {
                    stationReference.Gun2--;
                    portedCruiserObject.Gun2++;
                }
                else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
            }
            else if (shipType == G3)
            {
                if (playerCruiserShipsCount() < Constants.Instance.shipsLimit && (Lists.Gun1OfPlayerCruis + Lists.Gun2OfPlayerCruis + Lists.Gun3OfPlayerCruis) < 2)
                {
                    stationReference.Gun3--;
                    portedCruiserObject.Gun3++;
                }
                else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
            }
            else if (shipType == GM)
            {
                //so 8 is the limit if carying the mini guns (one batch fo each big gun use)
                if (Lists.MiniGunOfPlayerCruis < 8)
                {
                    stationReference.MiniGun--;
                    portedCruiserObject.MiniGun++;
                }
                else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
            }
            updateStationGreeds();
            updateStationUIValues(stationReference);
            updatePortedShipUIValues();

            if (portedCruiserObject.Cruis1 > 0 && portedCruiserObject.currentCruiserLevel != 1)
            {
                currentCruiser.sprite = Cruis1Spr;
            }
            else if (portedCruiserObject.Cruis2 > 0 && portedCruiserObject.Cruis1 == 0 && portedCruiserObject.currentCruiserLevel != 2  /*&& Lists.JourneyCruisLevel > 2*/)
            {
                currentCruiser.sprite = Cruis2Spr;
            }
            else if (portedCruiserObject.Cruis3 > 0 && portedCruiserObject.Cruis2 == 0 && portedCruiserObject.Cruis1 == 0 && portedCruiserObject.currentCruiserLevel != 3)
            {
                currentCruiser.sprite = Cruis3Spr;
            }
            //so the lowes level cruiser will be set only if there are no any higher level cruisers on board and if there are no any cruisers on board at all
            else if (((portedCruiserObject.Cruis3 + portedCruiserObject.Cruis2 + portedCruiserObject.Cruis1) < 1 && portedCruiserObject.currentCruiserLevel != 4)
                || ((portedCruiserObject.Cruis3 + portedCruiserObject.Cruis2 + portedCruiserObject.Cruis1 + portedCruiserObject.Cruis4) < 1 && portedCruiserObject.currentCruiserLevel != 4))
            {
                currentCruiser.sprite = Cruis4Spr;
            }
            //stationReference.playerCruiserNear = true;

            CommonButtonAudio.Play();
            if (stationReference.ifStationHasCruisers() < 1)
            {
                limitReachedWarning(Constants.Instance.getVulnerableTxt());
            }
        }
        else
        {
            if (stationReference.playerCruiserObject.Count > 0)
            {
                if (shipType == C1)
                {
                    if (playerCruiserShipsCount() <= Constants.Instance.shipsPreLimit)
                    {
                        stationReference.Cruis1--;
                        portedCruiserObject.Cruis1++;
                        changePortedCruiserSprite(); //this method changes the cruiser of player if it now has higher level cruiser
                    }
                    else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
                }
                else if (shipType == C2)
                {
                    if (playerCruiserShipsCount() <= Constants.Instance.shipsPreLimit)
                    {
                        stationReference.Cruis2--;
                        portedCruiserObject.Cruis2++;
                        changePortedCruiserSprite(); //this method changes the cruiser of player if it now has higher level cruiser
                    }
                    else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
                }
                else if (shipType == C3)
                {
                    if (playerCruiserShipsCount() < Constants.Instance.shipsLimit)
                    {
                        stationReference.Cruis3--;
                        portedCruiserObject.Cruis3++;
                        changePortedCruiserSprite(); //this method changes the cruiser of player if it now has higher level cruiser
                    }
                    else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
                }
                else if (shipType == C4)
                {
                    if (playerCruiserShipsCount() < Constants.Instance.shipsLimit)
                    {
                        stationReference.Cruis4--;
                        portedCruiserObject.Cruis4++;
                        changePortedCruiserSprite(); //this method changes the cruiser of player if it now has higher level cruiser
                    }
                    else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
                }
                else if (shipType == D1)
                {
                    if (playerCruiserShipsCount() < Constants.Instance.shipsLimit)
                    {
                        stationReference.Destr1--;
                        portedCruiserObject.Destr1++;
                    }
                    else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
                }
                else if (shipType == D1P)
                {
                    if (playerCruiserShipsCount() < Constants.Instance.shipsLimit)
                    {
                        stationReference.Destr1Par--;
                        portedCruiserObject.Destr1Par++;
                    }
                    else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
                }
                else if (shipType == D2)
                {
                    if (playerCruiserShipsCount() < Constants.Instance.shipsLimit)
                    {
                        stationReference.Destr2--;
                        portedCruiserObject.Destr2++;
                    }
                    else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
                }
                else if (shipType == D2P)
                {
                    if (playerCruiserShipsCount() < Constants.Instance.shipsLimit)
                    {
                        stationReference.Destr2Par--;
                        portedCruiserObject.Destr2Par++;
                    }
                    else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
                }
                else if (shipType == D3)
                {
                    if (playerCruiserShipsCount() < Constants.Instance.shipsLimit)
                    {
                        stationReference.Destr3--;
                        portedCruiserObject.Destr3++;
                    }
                    else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
                }
                else if (shipType == D4)
                {
                    if (playerCruiserShipsCount() < Constants.Instance.shipsLimit)
                    {
                        stationReference.Destr4--;
                        portedCruiserObject.Destr4++;
                    }
                    else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
                }
                else if (shipType == G1)
                {
                    if (playerCruiserShipsCount() < Constants.Instance.shipsLimit && (Lists.Gun1OfPlayerCruis + Lists.Gun2OfPlayerCruis + Lists.Gun3OfPlayerCruis) < 2)
                    {
                        stationReference.Gun1--;
                        portedCruiserObject.Gun1++;
                    }
                    else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
                }
                else if (shipType == G2)
                {
                    if (playerCruiserShipsCount() < Constants.Instance.shipsLimit && (Lists.Gun1OfPlayerCruis + Lists.Gun2OfPlayerCruis + Lists.Gun3OfPlayerCruis) < 2)
                    {
                        stationReference.Gun2--;
                        portedCruiserObject.Gun2++;
                    }
                    else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
                }
                else if (shipType == G3)
                {
                    if (playerCruiserShipsCount() < Constants.Instance.shipsLimit && (Lists.Gun1OfPlayerCruis + Lists.Gun2OfPlayerCruis + Lists.Gun3OfPlayerCruis) < 2)
                    {
                        stationReference.Gun3--;
                        portedCruiserObject.Gun3++;
                    }
                    else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
                }
                else if (shipType == GM)
                {
                    //so 8 is the limit if carying the mini guns (one batch fo each big gun use)
                    if (Lists.MiniGunOfPlayerCruis < 8)
                    {
                        stationReference.MiniGun--;
                        portedCruiserObject.MiniGun++;
                    }
                    else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
                }
                updateStationGreeds();
                updateStationUIValues(stationReference);
                updatePortedShipUIValues();
                CommonButtonAudio.Play();
                if (stationReference.ifStationHasCruisers() < 1)
                {
                    limitReachedWarning(Constants.Instance.getVulnerableTxt());
                }
                //stationReference.updateFleetCountToDisplay();
            }
            else CommonNotEnoughAudio.Play();
        }
    }
    public void putShipFromCruiserToStation(String shipType)
    {
        //player can't put ships to station from ported cruiser if it's capture line is less that 100%
        //if (stationReference.stationFillAmount >= 0)
        //{
        if (shipType == C1)
        {
            if (stationReference.stationFleetCount() <= Constants.Instance.shipsPreLimit)
            {
                stationReference.Cruis1++;
                portedCruiserObject.Cruis1--;
                changePortedCruiserSprite(); //this method changes the cruiser of player if it now has higher level cruiser
            }
            else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
        }
        else if (shipType == C2)
        {
            if (stationReference.stationFleetCount() <= Constants.Instance.shipsPreLimit)
            {
                stationReference.Cruis2++;
                portedCruiserObject.Cruis2--;
                changePortedCruiserSprite(); //this method changes the cruiser of player if it now has higher level cruiser
            }
            else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
        }
        else if (shipType == C3)
        {
            //this is a ships limit trigger for preventing overpassing the station fleet limit. So if player wants to put more than limit (45 places (1 and 2 level cruisers take 3 places))
            //this code will put on the warning message
            if (stationReference.stationFleetCount() < Constants.Instance.shipsLimit)
            {
                stationReference.Cruis3++;
                portedCruiserObject.Cruis3--;
                changePortedCruiserSprite(); //this method changes the cruiser of player if it now has higher level cruiser
            }
            else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
        }
        else if (shipType == C4)
        {
            if (stationReference.stationFleetCount() < Constants.Instance.shipsLimit)
            {
                stationReference.Cruis4++;
                portedCruiserObject.Cruis4--;
                changePortedCruiserSprite(); //this method changes the cruiser of player if it now has higher level cruiser
            }
            else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
        }
        else if (shipType == D1)
        {
            if (stationReference.stationFleetCount() < Constants.Instance.shipsLimit)
            {
                stationReference.Destr1++;
                portedCruiserObject.Destr1--;
            }
            else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
        }
        else if (shipType == D1P)
        {
            if (stationReference.stationFleetCount() < Constants.Instance.shipsLimit)
            {
                stationReference.Destr1Par++;
                portedCruiserObject.Destr1Par--;
            }
            else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
        }
        else if (shipType == D2)
        {
            if (stationReference.stationFleetCount() < Constants.Instance.shipsLimit)
            {
                stationReference.Destr2++;
                portedCruiserObject.Destr2--;
            }
            else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
        }
        else if (shipType == D2P)
        {
            if (stationReference.stationFleetCount() < Constants.Instance.shipsLimit)
            {
                stationReference.Destr2Par++;
                portedCruiserObject.Destr2Par--;
            }
            else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
        }
        else if (shipType == D3)
        {
            if (stationReference.stationFleetCount() < Constants.Instance.shipsLimit)
            {
                stationReference.Destr3++;
                portedCruiserObject.Destr3--;
            }
            else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
        }
        else if (shipType == D4)
        {
            if (stationReference.stationFleetCount() < Constants.Instance.shipsLimit)
            {
                stationReference.Destr4++;
                portedCruiserObject.Destr4--;
            }
            else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
        }
        else if (shipType == G1)
        {
            if (stationReference.stationFleetCount() < Constants.Instance.shipsLimit && (stationReference.Gun1 + stationReference.Gun1Produc + stationReference.Gun2 +
                    stationReference.Gun2Produc + stationReference.Gun3 + stationReference.Gun3Produc) < 2)
            {
                stationReference.Gun1++;
                portedCruiserObject.Gun1--;
            }
            else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
        }
        else if (shipType == G2)
        {
            if (stationReference.stationFleetCount() < Constants.Instance.shipsLimit && (stationReference.Gun1 + stationReference.Gun1Produc + stationReference.Gun2 +
                       stationReference.Gun2Produc + stationReference.Gun3 + stationReference.Gun3Produc) < 2)
            {
                stationReference.Gun2++;
                portedCruiserObject.Gun2--;
            }
            else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
        }
        else if (shipType == G3)
        {
            if (stationReference.stationFleetCount() < Constants.Instance.shipsLimit && (stationReference.Gun1 + stationReference.Gun1Produc + stationReference.Gun2 +
                          stationReference.Gun2Produc + stationReference.Gun3 + stationReference.Gun3Produc) < 2)
            {
                stationReference.Gun3++;
                portedCruiserObject.Gun3--;
            }
            else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
        }
        else if (shipType == GM)
        {
            //so there is a limit to have mini guns
            if ((stationReference.MiniGun + stationReference.MiniGunProduc) < 8)
            {
                stationReference.MiniGun++;
                portedCruiserObject.MiniGun--;
            }
            else limitReachedWarning(Constants.Instance.getOutOfLimitsWarning());
        }
        updateStationGreeds();
        updateStationUIValues(stationReference);
        if (portedCruiserObject != null) updatePortedShipUIValues();
        CommonButtonAudio.Play();

        //disactivating the cruiser if it gives all its cruisers to station and closing the panel if there is no anu cruisers ported to station
        if (portedCruiserObject != null)
        {
            if ((portedCruiserObject.Cruis3 + portedCruiserObject.Cruis2 + portedCruiserObject.Cruis1 + portedCruiserObject.Cruis4 + portedCruiserObject.Destr1 + portedCruiserObject.Destr1Par + 
                portedCruiserObject.Destr2 + portedCruiserObject.Destr2Par + portedCruiserObject.Destr3 + portedCruiserObject.Destr4 +portedCruiserObject.Gun1 + portedCruiserObject.Gun2 + 
                portedCruiserObject.Gun3 + portedCruiserObject.MiniGun) < 1)
            {
                StationPortObject.SetActive(false);
                PortedCruiserObj.SetActive(false);
                stationReference.playerCruiserObject.Remove(portedCruiserObject);
                portedCruiserObject.makePlayerCruiserDefault();
                portedCruiserObject.disactivatingCurrentShipNoBurst();
                Lists.shipsOnScene.Remove(portedCruiserObject.gameObject);
                portedCruiserObject = null;
                if (stationReference.playerCruiserObject.Count >0) closeAnyPanel();
            }
        }
    }

    //this method is used to show a warning that player ships are out of limits, no matter what fleet, station or cruiser of while exchange between them
    private void limitReachedWarning(String message)
    {
        WarningMsgPanel.SetActive(true);
        WarningMsgTxt.text = message;
        WarningSound.Play();
        WarningMsgPanel.GetComponent<Animator>().SetBool("dissolve", true);
    }

    public void closeAnyPanel()
    {
        TimeTextField.text = "x0";
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
        //if (PlayerShipPanel.activeInHierarchy) PlayerShipPanel.GetComponent<Animator>().SetBool("open", false);
        //if (CPUShipPanel.activeInHierarchy)
        //{
        //    CPUShipPanel.GetComponent<Animator>().SetBool("open", false);
        //    //CPUShipParams(); //sets the parameters of CPU ship for next ship to meet
        //}
        if ( stationPanelIsEnabled && !PlayerCruisPanelIsEnabled)
        {
            //stationPanel.GetComponent<Animator>().SetBool("open", false);

            //stationPanelMaskImage.GetComponent<Mask>().enabled = true;
            StationPanelCloseButton.SetActive(false);
        }
        if (FleetComparePanelPanelIsEnabled)
        {
            //fleetComparePanel.transform.GetChild(0).GetComponent<Mask>().enabled = true;
            fleetComparePanelCloseButton.SetActive(false);
            FightButtonComparePanel.SetActive(false);
        }
        if (PlayerCruisPanelIsEnabled) {
            foreach (GameObject go in stationButtons)
            {
                go.transform.GetChild(0).gameObject.SetActive(false);//disactivating ported token
                go.SetActive(false);
            }
            MiniImgAndStation.Clear();
            TextAndStation.Clear();
            ButtonAndStation.Clear();
            //playerCruiserPanel.GetComponent<Animator>().SetBool("openPanel", false);

            //fade = 1;
            //playerCruiserPanelMaskImg.GetComponent<Mask>().enabled = true;
            //fadeOut = true;
            playerCruiserPanelCloseButton.SetActive(false);
        }
        //if (GainLosePanel.activeInHierarchy) GainLosePanel.GetComponent<Animator>().SetBool("open", false);
        //fadeOut = true;
        //fade = 1;
        localLaunchingObjects = null;
        chosenStation = null;
        CruisJourneyReal = null;
        //setting inactive the warning msg while closing the panels on journey scene
        if (WarningMsgPanel.activeInHierarchy) WarningMsgPanel.SetActive(false);
        disactivatePanels();
        //Invoke("disactivatePanels", 0.6f);
    }

    //it was necessary to create stand alone method fro that sound cause closeAnyPanel method is invoced from switching to station panel method and it also used 
    //after upgrading the stations (it closes all active panel). So the UI sounds are not mixed and that method is called only if close panel button is invoced
    public void closeSound() {
        CommonButtonAudio.Play();
    }

    //disactivates all panels after one second after closing button is pushed
    private void disactivatePanels()
    {
        if (stationPanelIsEnabled && !PlayerCruisPanelIsEnabled)
        {
            stationPanel.SetActive(false);
            stationPanelIsEnabled = false;
        }

        //closing the compare fleets panel
        if (FleetComparePanelPanelIsEnabled)
        {
            fleetComparePanel.SetActive(false);
            FleetComparePanelPanelIsEnabled = false;
        }
        if (PlayerCruisPanelIsEnabled)
        {
            playerCruiserPanel.SetActive(false);
            PlayerCruisPanelIsEnabled = false;
        }

        anyPanelIsEnabled = false;
    }

    //this method is used to determine which dummy of defending object to set on defence scene so order is following 
    //1 - cruis1 dummy, 2 - cruis2 dummy, 3 - cruis3 dummy, 4 - cruis4 dummy, 11 - StationFed dummy, 22 - station C dummy, 33 - StationD dummy, 44 - station A dummy
    public void setTheMaketNumberForDefenceScene(bool myStationIsUnderAttack, int stationCurrentLevel, LaunchingObjcts playerCruiserObject)
    {   if (myStationIsUnderAttack)
        {
            if (stationCurrentLevel == 0) Lists.dummyOnDefenceScene = 44;
            else if (stationCurrentLevel == 1) Lists.dummyOnDefenceScene = 33;
            else if (stationCurrentLevel == 2) Lists.dummyOnDefenceScene = 22;
            else Lists.dummyOnDefenceScene = 11;
        }
        else
        {
            if (playerCruiserObject.Cruis1 > 0) Lists.dummyOnDefenceScene = 1;
            else if (playerCruiserObject.Cruis2 > 0) Lists.dummyOnDefenceScene = 2;
            else if (playerCruiserObject.Cruis3 > 0) Lists.dummyOnDefenceScene = 3;
            else Lists.dummyOnDefenceScene = 4;
        }
    }

    //method to use while attacking any CPU station and switching the scene
    public void fightVSStationOrShip() {

        SaveFile(); //saving data of scene while changing the scene cause date is not saved on quit from other scenes

        //Lists.setShipsForPlayer(Lists.Cruis1OfPlayerCruis, Lists.Cruis2OfPlayerCruis, Lists.Cruis3OfPlayerCruis, Lists.Cruis4OfPlayerCruis, Lists.Gun1OfPlayerCruis,
        //    Lists.Gun2OfPlayerCruis, Lists.Gun3OfPlayerCruis, Lists.Destr1OfPlayerCruis, Lists.Destr1OfPlayerParCruis, Lists.Destr2OfPlayerCruis,
        //    Lists.Destr2OfPlayerParCruis, Lists.Destr3OfPlayerCruis, Lists.Destr4OfPlayerCruis, Lists.MiniGunOfPlayerCruis);

        Lists.setShipsForPlayer(localLaunchingObjects.Cruis1, localLaunchingObjects.Cruis2, localLaunchingObjects.Cruis3, localLaunchingObjects.Cruis4, localLaunchingObjects.Gun1,
            localLaunchingObjects.Gun2, localLaunchingObjects.Gun3, localLaunchingObjects.Destr1, localLaunchingObjects.Destr1Par, localLaunchingObjects.Destr2,
            localLaunchingObjects.Destr2Par, localLaunchingObjects.Destr3, localLaunchingObjects.Destr4, localLaunchingObjects.MiniGun);

        //TO DO NOW IT IS JUST SIMPLE ATTACK WITHOUT CHECKING IF PLAYER STATION HAS ANY SHIP
        Lists.setShipsForCPU(stationReferenceCompare.Cruis1, stationReferenceCompare.Cruis2, stationReferenceCompare.Cruis3, stationReferenceCompare.Cruis4,
            stationReferenceCompare.CruisG, stationReferenceCompare.Gun1, stationReferenceCompare.Gun2, stationReferenceCompare.Gun3, stationReferenceCompare.Destr1,
            stationReferenceCompare.Destr1Par, stationReferenceCompare.Destr2, stationReferenceCompare.Destr2Par,
            stationReferenceCompare.Destr3, stationReferenceCompare.Destr4, stationReferenceCompare.DestrG);

        // sets station type to 0 so no station will be activated on battle scene (FOR NOW IT IS ONLY FRO TWO TYPES OF STATIONS)
        //TODO, the key for further settings is lower

        if (!stationReferenceCompare.isGuardCoreStation)
        {
            if (stationReferenceCompare.stationCurrentLevel == 0) Lists.stationTypeLists = 1;
            else if (stationReferenceCompare.stationCurrentLevel == 1) Lists.stationTypeLists = 4;
            else if (stationReferenceCompare.stationCurrentLevel == 2) Lists.stationTypeLists = 3;
            else if (stationReferenceCompare.stationCurrentLevel == 3) Lists.stationTypeLists = 6;
        }
        else Lists.stationTypeLists = 7; //7 is not exactly guards station number but in battle scene it checks if the number is not equal to any of above numbers

        /*
        else if (otherStation.transform.parent.gameObject.name.Contains("StationB")) stationType = 2;
        else if (otherStation.transform.parent.gameObject.name.Contains("StationC")) stationType = 3;
        else if (otherStation.transform.parent.gameObject.name.Contains("StationE")) stationType = 5;
        else if (otherStation.transform.parent.gameObject.name.Contains("StationFed")) stationType = 6; */

        // saving the instance of station that is under attack to update it's fleet count later after the battle on SpaceCtrl class 
        Lists.stationOnAttack = stationReferenceCompare;
        Lists.shipOnAttack = stationReferenceCompare.playerCruiserPortedToCPUStation;

        Lists.isPlayerStationOnDefence = false; //set false the static bool var to make enemy station be able to shot to player ships

        Lists.shipAttacksStation();
        Lists.PlayerAttacksCPUStation = true; //this 
        //assignin a proper color to UI token of opposite CPU player that is under attack. So for common CPU's the first condition and second is for guard station attack
        if (!stationReferenceCompare.isGuardStation) Lists.colorOfOpposite = getProperMatColorByIndex(colorsOfPlayers[stationReferenceCompare.CPUNumber]);// getProperStatioUIColorByIndex(colorsOfPlayers[stationReferenceCompare.CPUNumber]);
        else Lists.colorOfOpposite = new Color(2.4f, 2.4f, 2.4f, 1); ;

        if (stationReferenceCompare.Fighter == 0)
        {
            SceneSwitchMngr.LoadBattleScene();
        }
        else
        {
            setTheMaketNumberForDefenceScene(false,0, localLaunchingObjects); //setting proper dummy to defende on defence scene (0 is default value and does not effect any thing)
            //setting proper dummy for enemy station that is demostrated in front of turrel
            if (!stationReferenceCompare.isGuardCoreStation)
            {
                if (stationReferenceCompare.stationCurrentLevel == 0) Lists.dummyOnDefenceSceneEnemy = 44;
                else if (stationReferenceCompare.stationCurrentLevel == 1) Lists.dummyOnDefenceSceneEnemy = 33;
                else if (stationReferenceCompare.stationCurrentLevel == 2) Lists.dummyOnDefenceSceneEnemy = 22;
                else Lists.dummyOnDefenceSceneEnemy = 11;
            }
            else Lists.dummyOnDefenceSceneEnemy =55;

            Lists.setFightersForCPU(stationReferenceCompare.Fighter);
            SceneSwitchMngr.LoadDefenceScene();
        }
    }

    //this function is called from any CPU controller (station and cruiser) class after the Lists.enemyCruisersOnScene var will get 0. And set it's properties to correspont to win status
    public void youWinTheGameFunction()
    {
        TimeTextField.text = "x0";
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
        //so you win button will appear only if current level is not the one in border of dimensions (which are 3 and 7)
        if (Lists.currentLevel != 3 && Lists.currentLevel != 7 && Lists.currentLevel != 10)
        {
            SwitchSceneSound.Play();
            foreach (GameObject go in sceneUIPorperties) go.SetActive(false); //disactivateing all playeble UI of player after win or lose
            YouWinLoseButtonTxt.text = Constants.Instance.getYouWinTxt();
            YouWinLoseButtonImg.color = new Color(0, 0.85f, 0, 1); //the green color, bit dark
            YouWinLoseButtonTxt.color = Color.green;
            YouWinLoseButtonImg.sprite = winSprite;
            YouWinLoseButton.SetActive(true);
            SaveDaraga();
            Destroy(CruisJourneyReal);
        }
        else {
            SwitchSceneSound.Play();
            foreach (GameObject go in sceneUIPorperties) go.SetActive(false); //disactivateing all playeble UI of player after win or lose
            YouWinLoseButtonTxt.text = Constants.Instance.getTeleportationTxt();
            YouWinLoseButtonImg.color = new Color(0, 0.85f, 0, 1); //the green color, bit dark
            YouWinLoseButtonTxt.color = Color.green;
            YouWinLoseButtonImg.sprite = winSprite;
            foreach (StationController sc in Lists.AllStations) sc.gameObject.SetActive(false);
            foreach (CaptureLine cl in Lists.emptyStations) cl.gameObject.transform.parent.gameObject.SetActive(false);
            YouWinLoseButton.SetActive(true);
            Destroy(CruisJourneyReal);
            //this one is trigger if after pushing the you win lose button (which is with teleportation text) player teleports or not 
            //so if true the plyaer will teleport, otherwise it will just get back to menu
            //the button of youWinLose is assigned to switchToMenuAfterEnd method
            isTeleported = true; 
        }
        GoToMenuButton.SetActive(false);
    }

    //this function activates winLoseButton and set it's properties to correspont to lose status
    private void youLoseTheGameFunction()
    {
        TimeTextField.text = "x0";
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;

        SwitchSceneSound.Play();
        foreach (GameObject go in sceneUIPorperties) go.SetActive(false); //disactivateing all playeble UI of player after win or lose
        YouWinLoseButtonTxt.text = Constants.Instance.getYouLostTxt();
        YouWinLoseButtonImg.color = new Color(0.85f, 0, 0, 1); //the red color, bit dark
        YouWinLoseButtonTxt.color = Color.red;
        YouWinLoseButtonImg.sprite = loseSprite;
        YouWinLoseButton.SetActive(true);
        GoToMenuButton.SetActive(false);
    }

    //this script is called by pushed to button of lose or win on journey scene (so it is callsed only if Player win or lose battle)
    public void switchToMenuAfterEnd()
    {
        if (!isTeleported)
        {
            SwitchSceneSound.Play();
            StartCoroutine(switchSceneToMenuWithLag());
        }
        else {
            if (mainCam.orthographic)
            {
                mainCam.orthographic = false;
                mainCam.transform.rotation = Quaternion.Euler(60, mainCam.transform.rotation.eulerAngles.y, mainCam.transform.rotation.eulerAngles.z);
                //cameraManager.isPerspective = true;
            }
            YouWinLoseButton.SetActive(false);
            isTeleported = false;
            teleportationSound.Play();
            teleportationEffect.Play();
            //setting the skybox to a higher dimension skybox since there will be a teleportation
            if (Lists.isBlackDimension) RenderSettings.skybox = skyboxes[1];
            if (Lists.isBlueDimension) RenderSettings.skybox = skyboxes[2];
            if (Lists.isRedDimension) RenderSettings.skybox = skyboxes[3];
            //setting according dimension music to a higher level
            if (Lists.isBlackDimension)
            {
                DimensionMusic.clip = BlueDimensionMusic;
                DimensionMusic.Play();
            }
            else if (Lists.isBlueDimension)
            {
                DimensionMusic.clip = RedDimensionMusic;
                DimensionMusic.Play();
            }
            else if (Lists.isRedDimension)
            {
                DimensionMusic.clip = WinDimensionMusic;
                DimensionMusic.Play();
            }
            StartCoroutine(afterTeleportation());
        }
    }

    //this coroutine puts back the YouWin button again on scene after teleportation, but this time it is exactly you win button
    IEnumerator afterTeleportation()
    {
        yield return new WaitForSeconds(4f);
        SwitchSceneSound.Play();
        YouWinLoseButtonTxt.text = Constants.Instance.getYouWinTxt();
        YouWinLoseButtonImg.color = new Color(0, 0.85f, 0, 1); //the green color, bit dark
        YouWinLoseButtonTxt.color = Color.green;
        YouWinLoseButtonImg.sprite = winSprite;
        YouWinLoseButton.SetActive(true);
        SaveDaraga();
    }

    //this one is to play change scene sound before change scene
    IEnumerator switchSceneToMenuWithLag() {
        yield return new WaitForSeconds(0.9f);
        //clearing all the level properties to be ready to switch to new level
        foreach (CaptureLine cl in Lists.emptyStations) Destroy(cl.transform.parent.gameObject);
        Lists.emptyStations.Clear();

        foreach (StationController sc in Lists.AllStations) Destroy(sc.gameObject);
        Lists.AllStations.Clear();

        foreach (StationController sc in Lists.playerStations) Destroy(sc.gameObject);
        Lists.playerStations.Clear();

        foreach (StationController sc in Lists.CPU1Stations) Destroy(sc.gameObject);
        Lists.CPU1Stations.Clear();

        foreach (StationController sc in Lists.CPU2Stations) Destroy(sc.gameObject);
        Lists.CPU2Stations.Clear();

        foreach (StationController sc in Lists.CPU3Stations) Destroy(sc.gameObject);
        Lists.CPU3Stations.Clear();

        foreach (StationController sc in Lists.CPU4Stations) Destroy(sc.gameObject);
        Lists.CPU4Stations.Clear();

        foreach (StationController sc in Lists.CPUGuardStations) Destroy(sc.gameObject);
        Lists.CPUGuardStations.Clear();

        foreach (GameObject go in Lists.shipsOnScene) Destroy(go);
        Lists.shipsOnScene.Clear();

        foreach (GameObject go in Lists.shipsOnScene) Destroy(go);
        Lists.AllSceneObjects.Clear();

        foreach (GameObject go in Lists.energonsOnScene) Destroy(go);
        Lists.energonsOnScene.Clear();

        foreach (GameObject go in Lists.energonsControllablesOnScene) Destroy(go);
        Lists.energonsControllablesOnScene.Clear();

        foreach (GameObject go in Lists.energyBalls) Destroy(go);
        Lists.energyBalls.Clear();

        Lists.ClearFleetOfPlayerCruis();

        Lists.guardCoreStation = null;
        GuardAttackTimerTitle.gameObject.SetActive(false);

        //Lists.CPU1SceneCruis = null;
        //Lists.CPU2SceneCruis = null;
        //Lists.CPU3SceneCruis = null;
        //Lists.CPU4SceneCruis = null;
        //Lists.GuardSceneCruis = null;

        Lists.CPU1CruisersOnScene = 0;
        Lists.CPU2CruisersOnScene = 0;
        Lists.CPU3CruisersOnScene = 0;
        Lists.CPU4CruisersOnScene = 0;
        Lists.CPUGCruisersOnScene = 0;
        Lists.enemyStationsOnScene = 0;

        Lists.isContinued = false;

        if (File.Exists(Application.persistentDataPath + "/" + fileName + ".art"))
        {
            //deleting the file while player switches to menu scene after win or lose 
            File.Delete(Application.persistentDataPath + "/" + fileName + ".art");
        }

        if (Time.timeScale != 1)
        {
            //setting the time scale to regular if it was not so
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
        }
        SceneSwitchMngr.LoadMenuScene();
    }

    //go to menu frm journey scene button callback
    public void GoToMenuFromJourney()
    {
        //so saving the game works only if player is not under win or lose condition (means the button is already active)
        if (!YouWinLoseButton.activeInHierarchy) SaveFile();

        foreach (CaptureLine cl in Lists.emptyStations) Destroy(cl.transform.parent.gameObject);
        foreach (StationController sc in Lists.AllStations) Destroy(sc.gameObject);
        foreach (StationController sc in Lists.playerStations) Destroy(sc.gameObject);
        foreach (StationController sc in Lists.CPU1Stations) Destroy(sc.gameObject);
        foreach (StationController sc in Lists.CPU2Stations) Destroy(sc.gameObject);
        foreach (StationController sc in Lists.CPU3Stations) Destroy(sc.gameObject);
        foreach (StationController sc in Lists.CPU4Stations) Destroy(sc.gameObject);
        foreach (StationController sc in Lists.CPUGuardStations) Destroy(sc.gameObject);
        foreach (GameObject go in Lists.shipsOnScene) Destroy(go);
        foreach (GameObject go in Lists.energonsOnScene) Destroy(go);
        foreach (GameObject go in Lists.energonsControllablesOnScene) Destroy(go);
        foreach (GameObject go in Lists.energyBalls) Destroy(go);
        Lists.energyBalls.Clear();

        if (Time.timeScale != 1)
        {
            //setting the time scale to regular if it was not so
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
        }

        SceneSwitchMngr.LoadMenuScene();
    }

    //method to open ship demo panel with proper settings
    public void openShipInfoDemoPanel(String shipTypeDemo)
    { //so player can cancel the production of ship if only it is not currently producing or there are more than on cruisers of that type under production
        
        
        if (shipTypeDemo == C1)
        {
            shipTitles[1].gameObject.SetActive(true); //setting true the mega shot feature on cruisers

            //setting the texts of features according to the level and type of ship
            shipFeatures[0].text = Constants.Instance.getCruiserWord() + " " + 1;
            shipFeatures[1].text = Constants.Instance.getC1Mega();
            shipFeatures[2].text = Constants.Instance.CRUIS_1_HP.ToString();
            shipFeatures[3].text = (Constants.Instance.CRUIS_1_BULLET_HARM*3).ToString();
            shipFeatures[4].text = Constants.Instance.getForceShield()+", "+ Constants.Instance.getTripleShot()+ ", "+ Constants.Instance.getManeuvers();
            shipFeatures[5].text = Constants.Instance.C1ProdEnergy.ToString();

            ShipInfoDemoPanelImg.color = new Color (0.86f,0,0,1);
            Cruis1Demo.SetActive(true);
            shipNameDemo.text = C1Demo;
            DemoScreenFrame.color = new Color(0.86f, 0, 0, 1);
        }
        if (shipTypeDemo == C2)
        {
            shipTitles[1].gameObject.SetActive(true); //setting true the mega shot feature on cruisers

            //setting the texts of features according to the level and type of ship
            shipFeatures[0].text = Constants.Instance.getCruiserWord() + " " + 2;
            shipFeatures[1].text = Constants.Instance.getC2Mega();
            shipFeatures[2].text = Constants.Instance.CRUIS_2_HP.ToString();
            shipFeatures[3].text = (Constants.Instance.CRUIS_2_BULLET_HARM * 3).ToString();
            shipFeatures[4].text = Constants.Instance.getForceShield() + ", " + Constants.Instance.getDoubleShot() + ", " + Constants.Instance.getManeuvers();
            shipFeatures[5].text = Constants.Instance.C2ProdEnergy.ToString();

            ShipInfoDemoPanelImg.color = new Color(0.8f, 0, 0.87f, 1);
            Cruis2Demo.SetActive(true);
            shipNameDemo.text = C2Demo;
            DemoScreenFrame.color = new Color(0.8f, 0, 0.87f, 1);
        }
        if (shipTypeDemo == C3)
        {
            shipTitles[1].gameObject.SetActive(true); //setting true the mega shot feature on cruisers

            //setting the texts of features according to the level and type of ship
            shipFeatures[0].text = Constants.Instance.getCruiserWord() + " " + 3;
            shipFeatures[1].text = Constants.Instance.getC3Mega();
            shipFeatures[2].text = Constants.Instance.CRUIS_3_HP.ToString();
            shipFeatures[3].text = (Constants.Instance.CRUIS_3_BULLET_HARM * 3).ToString();
            shipFeatures[4].text = Constants.Instance.getForceShield();
            shipFeatures[5].text = Constants.Instance.C3ProdEnergy.ToString();

            ShipInfoDemoPanelImg.color = new Color(0, 1, 0.87f, 1);
            Cruis3Demo.SetActive(true);
            shipNameDemo.text = C3Demo;
            DemoScreenFrame.color = new Color(0, 1, 0.87f, 1);
        }
        if (shipTypeDemo == C4)
        {
            shipTitles[1].gameObject.SetActive(true); //setting true the mega shot feature on cruisers

            //setting the texts of features according to the level and type of ship
            shipFeatures[0].text = Constants.Instance.getCruiserWord() + " " + 4;
            shipFeatures[1].text = Constants.Instance.getC4Mega();
            shipFeatures[2].text = Constants.Instance.CRUIS_4_HP.ToString();
            shipFeatures[3].text = (Constants.Instance.CRUIS_4_BULLET_HARM * 3).ToString();
            shipFeatures[4].text = Constants.Instance.getForceShield();
            shipFeatures[5].text = Constants.Instance.C4ProdEnergy.ToString();

            ShipInfoDemoPanelImg.color = new Color(0, 0.87f, 0, 1);
            Cruis4Demo.SetActive(true);
            shipNameDemo.text = C4Demo;
            DemoScreenFrame.color = new Color(0, 0.87f, 0, 1);
        }
        if (shipTypeDemo == CF)
        {
            shipTitles[1].gameObject.SetActive(true); //setting true the mega shot feature on cruisers

            //setting the texts of features according to the level and type of ship
            shipFeatures[0].text = Constants.Instance.getCruiserWord() + " " + 2;
            shipFeatures[1].text = Constants.Instance.getC4Mega();
            shipFeatures[2].text = Constants.Instance.CRUIS_2_HP.ToString();
            shipFeatures[3].text = (Constants.Instance.CRUIS_2_BULLET_HARM * 3).ToString();
            shipFeatures[4].text = Constants.Instance.getForceShield() + ", " + Constants.Instance.getDoubleShot();
            shipFeatures[5].text = Constants.Instance.C2ProdEnergy.ToString();

            ShipInfoDemoPanelImg.color = Color.grey;
            CruisFedDemo.SetActive(true);
            shipNameDemo.text = CFDemo;
            DemoScreenFrame.color = Color.grey;
        }
        if (shipTypeDemo == D1)
        {
            shipTitles[1].gameObject.SetActive(false); //setting false the mega shot feature on destroyers

            //setting the texts of features according to the level and type of ship
            shipFeatures[0].text = Constants.Instance.getDestrWord() + " " + 1;
            shipFeatures[2].text = Constants.Instance.DESTR_1_HP.ToString();
            shipFeatures[3].text = (Constants.Instance.DESTR_1_BULLET_HARM * 3).ToString();
            shipFeatures[4].text = Constants.Instance.getForceShield() + ", " + Constants.Instance.getDoubleShot();
            shipFeatures[5].text = Constants.Instance.D1ProdEnergy.ToString();

            ShipInfoDemoPanelImg.color = new Color(0.86f, 0, 0, 1);
            Destr1Demo.SetActive(true);
            shipNameDemo.text = D1Demo;
            DemoScreenFrame.color = new Color(0.86f, 0, 0, 1);
        }
        if (shipTypeDemo == D1P)
        {
            shipTitles[1].gameObject.SetActive(false); //setting false the mega shot feature on destroyers

            //setting the texts of features according to the level and type of ship
            shipFeatures[0].text = Constants.Instance.getDestrWord() + " " + 1;
            shipFeatures[2].text = Constants.Instance.DESTR_1_HP.ToString();
            shipFeatures[3].text = (Constants.Instance.DESTR_1_BULLET_HARM * 3).ToString();
            shipFeatures[4].text = Constants.Instance.getForceShield() + ", " + Constants.Instance.getDoubleShot() + ", " + Constants.Instance.getParalizerFeature();
            shipFeatures[5].text = Constants.Instance.D1PProdEnergy.ToString();

            ShipInfoDemoPanelImg.color = new Color(0.86f, 0, 0, 1); 
            Destr1ParDemo.SetActive(true);
            shipNameDemo.text = D1PDemo;
            DemoScreenFrame.color = new Color(0.86f, 0, 0, 1);
        }
        if (shipTypeDemo == D2)
        {
            shipTitles[1].gameObject.SetActive(false); //setting false the mega shot feature on destroyers

            //setting the texts of features according to the level and type of ship
            shipFeatures[0].text = Constants.Instance.getDestrWord() + " " + 2;
            shipFeatures[2].text = Constants.Instance.DESTR_2_HP.ToString();
            shipFeatures[3].text = (Constants.Instance.DESTR_2_BULLET_HARM * 3).ToString();
            shipFeatures[4].text = Constants.Instance.getForceShield();
            shipFeatures[5].text = Constants.Instance.D2ProdEnergy.ToString();

            ShipInfoDemoPanelImg.color = new Color(0.8f, 0, 0.87f, 1);
            Destr2Demo.SetActive(true);
            shipNameDemo.text = D2Demo;
            DemoScreenFrame.color = new Color(0.8f, 0, 0.87f, 1);
        }
        if (shipTypeDemo == D2P)
        {
            shipTitles[1].gameObject.SetActive(false); //setting false the mega shot feature on destroyers

            //setting the texts of features according to the level and type of ship
            shipFeatures[0].text = Constants.Instance.getDestrWord() + " " + 2;
            shipFeatures[2].text = Constants.Instance.DESTR_2_HP.ToString();
            shipFeatures[3].text = (Constants.Instance.DESTR_2_BULLET_HARM * 3).ToString();
            shipFeatures[4].text = Constants.Instance.getForceShield() + Constants.Instance.getParalizerFeature();
            shipFeatures[5].text = Constants.Instance.D2PProdEnergy.ToString();

            ShipInfoDemoPanelImg.color = new Color(0.8f, 0, 0.87f, 1);
            Destr2ParDemo.SetActive(true);
            shipNameDemo.text = D2PDemo;
            DemoScreenFrame.color = new Color(0.8f, 0, 0.87f, 1);
        }
        if (shipTypeDemo == D3)
        {
            shipTitles[1].gameObject.SetActive(false); //setting false the mega shot feature on destroyers

            //setting the texts of features according to the level and type of ship
            shipFeatures[0].text = Constants.Instance.getDestrWord() + " " + 3;
            shipFeatures[2].text = Constants.Instance.DESTR_3_HP.ToString();
            shipFeatures[3].text = (Constants.Instance.DESTR_3_BULLET_HARM * 3).ToString();
            shipFeatures[4].text = Constants.Instance.getForceShield();
            shipFeatures[5].text = Constants.Instance.D3ProdEnergy.ToString();

            ShipInfoDemoPanelImg.color = new Color(0, 1, 0.87f, 1);
            Destr3Demo.SetActive(true);
            shipNameDemo.text = D3Demo;
            DemoScreenFrame.color = new Color(0, 1, 0.87f, 1);
        }
        if (shipTypeDemo == D4)
        {
            shipTitles[1].gameObject.SetActive(false); //setting false the mega shot feature on destroyers

            //setting the texts of features according to the level and type of ship
            shipFeatures[0].text = Constants.Instance.getDestrWord() + " " + 4;
            shipFeatures[2].text = Constants.Instance.DESTR_4_HP.ToString();
            shipFeatures[3].text = (Constants.Instance.DESTR_4_BULLET_HARM * 3).ToString();
            shipFeatures[4].text = "-";
            shipFeatures[5].text = Constants.Instance.D4ProdEnergy.ToString();

            ShipInfoDemoPanelImg.color = new Color(0, 0.87f, 0, 1);
            Destr4Demo.SetActive(true);
            shipNameDemo.text = D4Demo;
            DemoScreenFrame.color = new Color(0, 0.87f, 0, 1);
        }
        if (shipTypeDemo == DF)
        {
            shipTitles[1].gameObject.SetActive(false); //setting false the mega shot feature on destroyers

            //setting the texts of features according to the level and type of ship
            shipFeatures[0].text = Constants.Instance.getDestrWord() + " " + 2;
            shipFeatures[2].text = Constants.Instance.DESTR_2_HP.ToString();
            shipFeatures[3].text = (Constants.Instance.DESTR_2_BULLET_HARM * 3).ToString();
            shipFeatures[4].text = Constants.Instance.getForceShield();
            shipFeatures[5].text = Constants.Instance.D2ProdEnergy.ToString();

            ShipInfoDemoPanelImg.color = Color.grey;
            DestrFedDemo.SetActive(true);
            shipNameDemo.text = DFDemo;
            DemoScreenFrame.color = Color.grey;
        }
        if (shipTypeDemo == G1)
        {
            shipTitles[1].gameObject.SetActive(false); //setting false the mega shot feature on destroyers

            //setting the texts of features according to the level and type of ship
            shipFeatures[0].text = Constants.Instance.getTurretWord() + " " + 3;
            shipFeatures[2].text = Constants.Instance.GUN_LIFE_SINGLE.ToString();
            shipFeatures[3].text = (Constants.Instance.GUN_HARM_SINGLE * 3).ToString();
            shipFeatures[4].text = Constants.Instance.getDefence();
            shipFeatures[5].text = Constants.Instance.G1ProdEnergy.ToString();

            ShipInfoDemoPanelImg.color = new Color(0, 1, 0.87f, 1);
            Gun1Demo.SetActive(true);
            shipNameDemo.text = G1Demo;
            DemoScreenFrame.color = new Color(0, 1, 0.87f, 1);
        }
        if (shipTypeDemo == G2)
        {
            shipTitles[1].gameObject.SetActive(false); //setting false the mega shot feature on destroyers

            //setting the texts of features according to the level and type of ship
            shipFeatures[0].text = Constants.Instance.getTurretWord() + " " + 2;
            shipFeatures[2].text = Constants.Instance.GUN_LIFE_DUAL.ToString();
            shipFeatures[3].text = (Constants.Instance.GUN_HARM_DUAL * 3).ToString();
            shipFeatures[4].text = Constants.Instance.getDefence();
            shipFeatures[5].text = Constants.Instance.G2ProdEnergy.ToString();

            ShipInfoDemoPanelImg.color = new Color(0.86f, 0, 0, 1);
            Gun2Demo.SetActive(true);
            shipNameDemo.text = G2Demo;
            DemoScreenFrame.color = new Color(0.86f, 0, 0, 1);
        }
        if (shipTypeDemo == G3)
        {
            shipTitles[1].gameObject.SetActive(false); //setting false the mega shot feature on destroyers

            //setting the texts of features according to the level and type of ship
            shipFeatures[0].text = Constants.Instance.getTurretWord() + " " + 1;
            shipFeatures[2].text = Constants.Instance.GUN_LIFE_TRIPLE.ToString();
            shipFeatures[3].text = (Constants.Instance.GUN_HARM_TRIPLE * 3).ToString();
            shipFeatures[4].text = Constants.Instance.getDefence();
            shipFeatures[5].text = Constants.Instance.G3ProdEnergy.ToString();

            ShipInfoDemoPanelImg.color = new Color(0, 0.87f, 0, 1);
            Gun3Demo.SetActive(true);
            shipNameDemo.text = G3Demo;
            DemoScreenFrame.color = new Color(0, 0.87f, 0, 1);
        }
        if (shipTypeDemo == GM)
        {
            shipTitles[1].gameObject.SetActive(false); //setting false the mega shot feature on destroyers

            //setting the texts of features according to the level and type of ship
            shipFeatures[0].text = Constants.Instance.getTurretWord() + " mini";
            shipFeatures[2].text = "1";
            shipFeatures[3].text = "1";
            shipFeatures[4].text = Constants.Instance.getDefence();
            shipFeatures[5].text = Constants.Instance.GMProdEnergy.ToString();

            ShipInfoDemoPanelImg.color = new Color(0, 1, 0.87f, 1);
            MiniGunDemo.SetActive(true);
            shipNameDemo.text = GMDemo;
            DemoScreenFrame.color = new Color(0, 1, 0.87f, 1);
        }
        CommonButtonAudio.Play();
        //ShipInfoDemoPanelMask.transform.localPosition = infoPanelin;
        
        ShipInfoDemoPanelMask.SetActive(true);
        //ShipInfoDemoPanelMask.GetComponent<Animator>().SetBool("open", true);

        DemoPanelClose.SetActive(true);
    }

    //method for closing the ship info demo panel assigned to demo panel close button
    public void closeInfoPanel()
    {
        //ShipInfoDemoPanelMask.GetComponent<Animator>().SetBool("open", false);
        DemoPanelClose.SetActive(false);

        Cruis1Demo.SetActive(false);
        Cruis2Demo.SetActive(false);
        Cruis3Demo.SetActive(false);
        Cruis4Demo.SetActive(false);
        CruisFedDemo.SetActive(false);
        Destr1Demo.SetActive(false);
        Destr1ParDemo.SetActive(false);
        Destr2Demo.SetActive(false);
        Destr2ParDemo.SetActive(false);
        Destr3Demo.SetActive(false);
        Destr4Demo.SetActive(false);
        DestrFedDemo.SetActive(false);
        Gun1Demo.SetActive(false);
        Gun2Demo.SetActive(false);
        Gun3Demo.SetActive(false);
        MiniGunDemo.SetActive(false);

        //Invoke("disactivateShipsDemoPanels", 0.6f);
        disactivateShipsDemoPanels();
    }

    //method for closing the ship info demo panel called from closeInfoPanel function
    private void disactivateShipsDemoPanels()
    {
        //ShipInfoDemoPanelMask.transform.localPosition = infoPanelOut;
        ShipInfoDemoPanelMask.SetActive(false);
    }

    //saving the data of game while quitting the game
    private void OnApplicationPause(bool pause)
    {
        //so saving the game works only if player is not under win or lose condition (means the button is already active)
        if (!YouWinLoseButton.activeInHierarchy) SaveFile();
    }

    //this one for unity editor
    private void OnApplicationQuit()
    {
        //so saving the game works only if player is not under win or lose condition (means the button is already active)
        if (!YouWinLoseButton.activeInHierarchy) SaveFile();
    }

    //this method is used to read save data from special file with key value approach
    private string getSavedValue(string[] line, string pattern)
    {

        string result = "";
        foreach (string key in line)
        {
            if (key.Trim() != string.Empty)
            {
                string value = key;
                value = Crypt(key);

                if (pattern == value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0])
                {
                    result = value.Remove(0, value.IndexOf(' ') + 1);
                }
            }
        }
        return result;
    }

    //crypting method for saved data
    string Crypt(string text)
    {
        string result = string.Empty;
        foreach (char j in text)
        {
            // ((int) j ^ 29) - применение XOR к номеру символа
            // (char)((int) j ^ 29) - получаем символ из измененного номера
            // Число, которым мы XORим можете поставить любое. Эксперементируйте.
            result += (char)(j ^ 29);
        }
        return result;
    }

    //this method is for saving data, and is for using in differend methods for example on quit or on scene switch
    public void SaveFile()
    {
        //saving data

        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/" + fileName + ".art");
        string sp = " "; //space 

        sw.WriteLine(Crypt("currentLevel" + sp + Lists.currentLevel));
        sw.WriteLine(Crypt("currentLevelDifficulty" + sp + Lists.currentLevelDifficulty));

        //setting energy and booster points of player and the color of player
        sw.WriteLine(Crypt("energyOfPlayer" + sp + Lists.energyOfPlayer));
        //sw.WriteLine(Crypt("boosterOfPlayer" + sp + Lists.boosterOfPlayer));
        sw.WriteLine(Crypt("playerColor" + sp + Lists.playerColor));

        //saving the count of station on scene to instantiate them on scene while start on continuing the game by indexes that depend saved station counts
        //for example if this value is 0 no functioning station will be launched on scene while getting the saved file
        sw.WriteLine(Crypt("AllStationsCount" + sp + Lists.AllStations.Count));
        for (int i = 0; i < Lists.AllStations.Count; i++)
        {
            sw.WriteLine(Crypt("StationXPos" + i + sp + Lists.AllStations[i].stationPosition.x)); //y pos is always -8
            sw.WriteLine(Crypt("StationZPos" + i + sp + Lists.AllStations[i].stationPosition.z)); //y pos is always -8

            sw.WriteLine(Crypt("StationIsPlayer" + i + sp + Lists.AllStations[i].isPlayerStation));
            sw.WriteLine(Crypt("StationIsCPU" + i + sp + Lists.AllStations[i].isCPUStation));
            sw.WriteLine(Crypt("StationIsGuard" + i + sp + Lists.AllStations[i].isGuardStation));
            sw.WriteLine(Crypt("StationIsGuardCore" + i + sp + Lists.AllStations[i].isGuardCoreStation));

            sw.WriteLine(Crypt("StationCurrentLevel" + i + sp + Lists.AllStations[i].stationCurrentLevel));
            sw.WriteLine(Crypt("StationUpgradeCounts" + i + sp + Lists.AllStations[i].upgradeCounts));
            sw.WriteLine(Crypt("StationNextUpgradeEnergyCount" + i + sp + Lists.AllStations[i].nextUpgradeEnergyCount));
            sw.WriteLine(Crypt("StationCurrentUpgradeTime" + i + sp + Lists.AllStations[i].currentUpgradeTime));
            //sw.WriteLine(Crypt("StationCPUStationUpgradeTimer" + i + sp + Lists.AllStations[i].CPUStationUpgradeTimer));
            //sw.WriteLine(Crypt("StationCPUStationIsUpgradIng" + i + sp + Lists.AllStations[i].CPUStationIsUpgradIng));
            //sw.WriteLine(Crypt("CPUStationFleetIncreaserTimer" + i + sp + Lists.AllStations[i].CPUStationFleetIncreaserTimer));
            //sw.WriteLine(Crypt("destrProductionCount" + i + sp + Lists.AllStations[i].destrProductionCount));

            sw.WriteLine(Crypt("StationCPUSceneCruiserLaunchTimer" + i + sp + Lists.AllStations[i].CPUSceneCruiserLaunchTimer));
            sw.WriteLine(Crypt("StationCPUSceneCruiserLaunchCoroutineIsOn" + i + sp + Lists.AllStations[i].CPUSceneCruiserLaunchCoroutineIsOn));

            sw.WriteLine(Crypt("StationIsUpgrading" + i + sp + Lists.AllStations[i].isUpgrading));
            sw.WriteLine(Crypt("StationUpgradeFill" + i + sp + Lists.AllStations[i].upgradeFill));

            sw.WriteLine(Crypt("StationCPUNumber" + i + sp + Lists.AllStations[i].CPUNumber));

            //saving data about fleet of station
            sw.WriteLine(Crypt("energyOfStationToUPGradeFoRCPU" + i + sp + Lists.AllStations[i].energyOfStationToUPGradeFoRCPU));
            sw.WriteLine(Crypt("StationEnergy" + i + sp + Lists.AllStations[i].energyOfStation));
            sw.WriteLine(Crypt("StationCruis4" + i + sp + Lists.AllStations[i].Cruis4));
            sw.WriteLine(Crypt("StationCruis3" + i + sp + Lists.AllStations[i].Cruis3));
            sw.WriteLine(Crypt("StationCruis2" + i + sp + Lists.AllStations[i].Cruis2));
            sw.WriteLine(Crypt("StationCruis1" + i + sp + Lists.AllStations[i].Cruis1));
            sw.WriteLine(Crypt("StationCruisG" + i + sp + Lists.AllStations[i].CruisG));
            sw.WriteLine(Crypt("StationDestr4" + i + sp + Lists.AllStations[i].Destr4));
            sw.WriteLine(Crypt("StationDestr3" + i + sp + Lists.AllStations[i].Destr3));
            sw.WriteLine(Crypt("StationDestr2" + i + sp + Lists.AllStations[i].Destr2));
            sw.WriteLine(Crypt("StationDestr2Par" + i + sp + Lists.AllStations[i].Destr2Par));
            sw.WriteLine(Crypt("StationDestr1" + i + sp + Lists.AllStations[i].Destr1));
            sw.WriteLine(Crypt("StationDestr1Par" + i + sp + Lists.AllStations[i].Destr1Par));
            sw.WriteLine(Crypt("StationDestrG" + i + sp + Lists.AllStations[i].DestrG));
            sw.WriteLine(Crypt("StationGun1" + i + sp + Lists.AllStations[i].Gun1));
            sw.WriteLine(Crypt("StationGun2" + i + sp + Lists.AllStations[i].Gun2));
            sw.WriteLine(Crypt("StationGun3" + i + sp + Lists.AllStations[i].Gun3));
            sw.WriteLine(Crypt("StationMiniGun" + i + sp + Lists.AllStations[i].MiniGun));
            sw.WriteLine(Crypt("StationFighter" + i + sp + Lists.AllStations[i].Fighter));

        }

        //saving the count of emty stations and the parameters of that emty stations
        sw.WriteLine(Crypt("emptyStations" + sp + Lists.emptyStations.Count));
        for (int i = 0; i < Lists.emptyStations.Count; i++)
        {
            sw.WriteLine(Crypt("EmptyTag" + i + sp + Lists.emptyStations[i].tag));
            sw.WriteLine(Crypt("EmptyXPos" + i + sp + Lists.emptyStations[i].transform.parent.position.x)); //y pos is always -8
            sw.WriteLine(Crypt("EmptyZPos" + i + sp + Lists.emptyStations[i].transform.parent.position.z)); //y pos is always -8
        }

        //saving the count of CPU cruisers on scene if any and lower are the parameters of cruisers
        int cruisersOnScene = Lists.shipsOnScene.Count;
        sw.WriteLine(Crypt("cruisersOnScene" + sp + cruisersOnScene));

        //this block saves the date about player and CPU cruisers active on scene at the moment 
        int CPUCruisersOnScene =0;
        int PlayerCruisersOnScene = 0;
        for (int i = 0; i < Lists.shipsOnScene.Count; i++)
        {
            //getting only CPU cruisers from the list and saving only their parameters
            if (Lists.shipsOnScene[i].CompareTag(Cruis1CPUTag) || Lists.shipsOnScene[i].CompareTag(Cruis2CPUTag) || Lists.shipsOnScene[i].CompareTag(Cruis3CPUTag) || Lists.shipsOnScene[i].CompareTag(Cruis4CPUTag))
            {
                CPUShipCtrlJourney CPUCtrlr = Lists.shipsOnScene[i].GetComponent<CPUShipCtrlJourney>();
                sw.WriteLine(Crypt("CruiserName" + CPUCruisersOnScene + sp + Lists.shipsOnScene[i].name));
                sw.WriteLine(Crypt("CruiserXPos" + CPUCruisersOnScene + sp + Lists.shipsOnScene[i].transform.position.x)); //y pos is always -8
                sw.WriteLine(Crypt("CruiserZPos" + CPUCruisersOnScene + sp + Lists.shipsOnScene[i].transform.position.z)); //y pos is always -8

                sw.WriteLine(Crypt("CruiserCPUNumber" + CPUCruisersOnScene + sp + CPUCtrlr.CPUNumber));
                sw.WriteLine(Crypt("CruiserMovePointX" + CPUCruisersOnScene + sp + CPUCtrlr.moveToPoint.x));
                sw.WriteLine(Crypt("CruiserMovePointZ" + CPUCruisersOnScene + sp + CPUCtrlr.moveToPoint.z));
                sw.WriteLine(Crypt("CruiserYRotation" + CPUCruisersOnScene + sp + CPUCtrlr.yRotation));
                sw.WriteLine(Crypt("CruiserIsMoving" + CPUCruisersOnScene + sp + CPUCtrlr.isMoving));

                sw.WriteLine(Crypt("CruiserEnergy" + CPUCruisersOnScene + sp + CPUCtrlr.energy));
                sw.WriteLine(Crypt("CruiserCruis4" + CPUCruisersOnScene + sp + CPUCtrlr.Cruis4));
                sw.WriteLine(Crypt("CruiserCruis3" + CPUCruisersOnScene + sp + CPUCtrlr.Cruis3));
                sw.WriteLine(Crypt("CruiserCruis2" + CPUCruisersOnScene + sp + CPUCtrlr.Cruis2));
                sw.WriteLine(Crypt("CruiserCruis1" + CPUCruisersOnScene + sp + CPUCtrlr.Cruis1));
                sw.WriteLine(Crypt("CruiserCruisG" + CPUCruisersOnScene + sp + CPUCtrlr.CruisG));
                sw.WriteLine(Crypt("CruiserDestr4" + CPUCruisersOnScene + sp + CPUCtrlr.Destr4));
                sw.WriteLine(Crypt("CruiserDestr3" + CPUCruisersOnScene + sp + CPUCtrlr.Destr3));
                sw.WriteLine(Crypt("CruiserDestr2" + CPUCruisersOnScene + sp + CPUCtrlr.Destr2));
                sw.WriteLine(Crypt("CruiserDestr2Par" + CPUCruisersOnScene + sp + CPUCtrlr.Destr2Par));
                sw.WriteLine(Crypt("CruiserDestr1" + CPUCruisersOnScene + sp + CPUCtrlr.Destr1));
                sw.WriteLine(Crypt("CruiserDestr1Par" + CPUCruisersOnScene + sp + CPUCtrlr.Destr1Par));
                sw.WriteLine(Crypt("CruiserDestrG" + CPUCruisersOnScene + sp + CPUCtrlr.DestrG));
                sw.WriteLine(Crypt("CruiserGun1" + CPUCruisersOnScene + sp + CPUCtrlr.Gun1));
                sw.WriteLine(Crypt("CruiserGun2" + CPUCruisersOnScene + sp + CPUCtrlr.Gun2));
                sw.WriteLine(Crypt("CruiserGun3" + CPUCruisersOnScene + sp + CPUCtrlr.Gun3));
                sw.WriteLine(Crypt("CruiserMiniGun" + CPUCruisersOnScene + sp + CPUCtrlr.MiniGun));
                sw.WriteLine(Crypt("CruiserFighter" + CPUCruisersOnScene + sp + CPUCtrlr.Fighter));
                CPUCruisersOnScene++;
            }
            else {
                LaunchingObjcts PlayerCruiser = Lists.shipsOnScene[i].GetComponent<LaunchingObjcts>();
                sw.WriteLine(Crypt("PlayerCruiserName" + PlayerCruisersOnScene + sp + Lists.shipsOnScene[i].name));
                sw.WriteLine(Crypt("PlayerCruiserXPos" + PlayerCruisersOnScene + sp + Lists.shipsOnScene[i].transform.position.x)); //y pos is always -8
                sw.WriteLine(Crypt("PlayerCruiserZPos" + PlayerCruisersOnScene + sp + Lists.shipsOnScene[i].transform.position.z)); //y pos is always -8

                //sw.WriteLine(Crypt("PlayerCruiserMovePointX" + PlayerCruisersOnScene + sp + PlayerCruiser.moveToPoint.x));
                //sw.WriteLine(Crypt("PlayerCruiserMovePointZ" + PlayerCruisersOnScene + sp + PlayerCruiser.moveToPoint.z));
                sw.WriteLine(Crypt("PlayerCruiserYRotation" + PlayerCruisersOnScene + sp + Lists.shipsOnScene[i].transform.rotation.eulerAngles.y));
                //sw.WriteLine(Crypt("PlayerCruiserIsMoving" + PlayerCruisersOnScene + sp + PlayerCruiser.isMoving));

                sw.WriteLine(Crypt("PlayerCruiserCruis4" + PlayerCruisersOnScene + sp + PlayerCruiser.Cruis4));
                sw.WriteLine(Crypt("PlayerCruiserCruis3" + PlayerCruisersOnScene + sp + PlayerCruiser.Cruis3));
                sw.WriteLine(Crypt("PlayerCruiserCruis2" + PlayerCruisersOnScene + sp + PlayerCruiser.Cruis2));
                sw.WriteLine(Crypt("PlayerCruiserCruis1" + PlayerCruisersOnScene + sp + PlayerCruiser.Cruis1));
                sw.WriteLine(Crypt("PlayerCruiserCruisG" + PlayerCruisersOnScene + sp + PlayerCruiser.CruisG));
                sw.WriteLine(Crypt("PlayerCruiserDestr4" + PlayerCruisersOnScene + sp + PlayerCruiser.Destr4));
                sw.WriteLine(Crypt("PlayerCruiserDestr3" + PlayerCruisersOnScene + sp + PlayerCruiser.Destr3));
                sw.WriteLine(Crypt("PlayerCruiserDestr2" + PlayerCruisersOnScene + sp + PlayerCruiser.Destr2));
                sw.WriteLine(Crypt("PlayerCruiserDestr2Par" + PlayerCruisersOnScene + sp + PlayerCruiser.Destr2Par));
                sw.WriteLine(Crypt("PlayerCruiserDestr1" + PlayerCruisersOnScene + sp + PlayerCruiser.Destr1));
                sw.WriteLine(Crypt("PlayerCruiserDestr1Par" + PlayerCruisersOnScene + sp + PlayerCruiser.Destr1Par));
                sw.WriteLine(Crypt("PlayerCruiserDestrG" + PlayerCruisersOnScene + sp + PlayerCruiser.DestrG));
                sw.WriteLine(Crypt("PlayerCruiserGun1" + PlayerCruisersOnScene + sp + PlayerCruiser.Gun1));
                sw.WriteLine(Crypt("PlayerCruiserGun2" + PlayerCruisersOnScene + sp + PlayerCruiser.Gun2));
                sw.WriteLine(Crypt("PlayerCruiserGun3" + PlayerCruisersOnScene + sp + PlayerCruiser.Gun3));
                sw.WriteLine(Crypt("PlayerCruiserMiniGun" + PlayerCruisersOnScene + sp + PlayerCruiser.MiniGun));
                sw.WriteLine(Crypt("PlayerCruiserFighter" + PlayerCruisersOnScene + sp + PlayerCruiser.Fighter));
                PlayerCruisersOnScene++;
            }
        }

        //saving the count of CPU cruisers and PlayerCruiser on scene if any 
        sw.WriteLine(Crypt("CPUCruisersOnScene" + sp + CPUCruisersOnScene));
        sw.WriteLine(Crypt("PlayerCruisersOnScene" + sp + PlayerCruisersOnScene));

        //int EnergonsOnScene = 0;
        //int GuardsOnScene = 0;
        //this block saves the date about energon and guard cruisers active on scene at the moment 
        for (int i = 0; i < Lists.energonsOnScene.Count; i++)
        {
            EnergonMngr EnergonCtrlr = Lists.energonsOnScene[i].GetComponent<EnergonMngr>();
            ////getting only CPU cruisers from the list and saving only their parameters
            //if (Lists.energonsOnScene[i].CompareTag("Energon"))
            //{
            //    sw.WriteLine(Crypt("EnergonXPos" + EnergonsOnScene + sp + Lists.energonsOnScene[i].transform.position.x)); //y pos is always -8
            //    sw.WriteLine(Crypt("EnergonZPos" + EnergonsOnScene + sp + Lists.energonsOnScene[i].transform.position.z)); //y pos is always -8

            //    sw.WriteLine(Crypt("energonLvl" + EnergonsOnScene + sp + EnergonCtrlr.energonAndGuardLvl));
            //    sw.WriteLine(Crypt("nextRotatioLerpEnergon" + EnergonsOnScene + sp + EnergonCtrlr.nextRotatioLerp));
            //    sw.WriteLine(Crypt("nextMovingSpeedEnergon" + EnergonsOnScene + sp + EnergonCtrlr.nextMovingSpeed));
                
            //    sw.WriteLine(Crypt("energyOfEnergon" + EnergonsOnScene + sp + EnergonCtrlr.energyOfEnergonAndGuard));
            //    sw.WriteLine(Crypt("EnergonCruis4" + EnergonsOnScene + sp + EnergonCtrlr.Cruis4));
            //    sw.WriteLine(Crypt("EnergonCruis3" + EnergonsOnScene + sp + EnergonCtrlr.Cruis3));
            //    sw.WriteLine(Crypt("EnergonCruis2" + EnergonsOnScene + sp + EnergonCtrlr.Cruis2));
            //    sw.WriteLine(Crypt("EnergonCruis1" + EnergonsOnScene + sp + EnergonCtrlr.Cruis1));
            //    sw.WriteLine(Crypt("EnergonCruisG" + EnergonsOnScene + sp + EnergonCtrlr.CruisG));
            //    sw.WriteLine(Crypt("EnergonDestr4" + EnergonsOnScene + sp + EnergonCtrlr.Destr4));
            //    sw.WriteLine(Crypt("EnergonDestr3" + EnergonsOnScene + sp + EnergonCtrlr.Destr3));
            //    sw.WriteLine(Crypt("EnergonDestr2" + EnergonsOnScene + sp + EnergonCtrlr.Destr2));
            //    sw.WriteLine(Crypt("EnergonDestr2Par" + EnergonsOnScene + sp + EnergonCtrlr.Destr2Par));
            //    sw.WriteLine(Crypt("EnergonDestr1" + EnergonsOnScene + sp + EnergonCtrlr.Destr1));
            //    sw.WriteLine(Crypt("EnergonDestr1Par" + EnergonsOnScene + sp + EnergonCtrlr.Destr1Par));
            //    sw.WriteLine(Crypt("EnergonDestrG" + EnergonsOnScene + sp + EnergonCtrlr.DestrG));
            //    sw.WriteLine(Crypt("EnergonGun1" + EnergonsOnScene + sp + EnergonCtrlr.Gun1));
            //    sw.WriteLine(Crypt("EnergonGun2" + EnergonsOnScene + sp + EnergonCtrlr.Gun2));
            //    sw.WriteLine(Crypt("EnergonGun3" + EnergonsOnScene + sp + EnergonCtrlr.Gun3));
            //    sw.WriteLine(Crypt("EnergonMiniGun" + EnergonsOnScene + sp + EnergonCtrlr.MiniGun));
            //    sw.WriteLine(Crypt("EnergonFighter" + EnergonsOnScene + sp + EnergonCtrlr.Fighter));
            //    EnergonsOnScene++;
            //}
            //else
            //{
                sw.WriteLine(Crypt("GuardXPos" + i + sp + Lists.energonsOnScene[i].transform.position.x)); //y pos is always -8
                sw.WriteLine(Crypt("GuardZPos" + i + sp + Lists.energonsOnScene[i].transform.position.z)); //y pos is always -8

                sw.WriteLine(Crypt("GuardLvl" + i + sp + EnergonCtrlr.energonAndGuardLvl));
                sw.WriteLine(Crypt("nextRotatioLerpGuard" + i + sp + EnergonCtrlr.nextRotatioLerp));
                sw.WriteLine(Crypt("nextMovingSpeedGuard" + i + sp + EnergonCtrlr.nextMovingSpeed));
                sw.WriteLine(Crypt("guardChaseTimeMiddle" + i + sp + EnergonCtrlr.guardChaseTimeMiddle));
                sw.WriteLine(Crypt("guardTranslateModif" + i + sp + EnergonCtrlr.guardTranslateModif));

                sw.WriteLine(Crypt("energyOfGuard" + i + sp + EnergonCtrlr.energyOfEnergonAndGuard));
                sw.WriteLine(Crypt("GuardCruis4" + i + sp + EnergonCtrlr.Cruis4));
                sw.WriteLine(Crypt("GuardCruis3" + i + sp + EnergonCtrlr.Cruis3));
                sw.WriteLine(Crypt("GuardCruis2" + i + sp + EnergonCtrlr.Cruis2));
                sw.WriteLine(Crypt("GuardCruis1" + i + sp + EnergonCtrlr.Cruis1));
                sw.WriteLine(Crypt("GuardCruisG" + i + sp + EnergonCtrlr.CruisG));
                sw.WriteLine(Crypt("GuardDestr4" + i + sp + EnergonCtrlr.Destr4));
                sw.WriteLine(Crypt("GuardDestr3" + i + sp + EnergonCtrlr.Destr3));
                sw.WriteLine(Crypt("GuardDestr2" + i + sp + EnergonCtrlr.Destr2));
                sw.WriteLine(Crypt("GuardDestr2Par" + i + sp + EnergonCtrlr.Destr2Par));
                sw.WriteLine(Crypt("GuardDestr1" + i + sp + EnergonCtrlr.Destr1));
                sw.WriteLine(Crypt("GuardDestr1Par" + i + sp + EnergonCtrlr.Destr1Par));
                sw.WriteLine(Crypt("GuardDestrG" + i + sp + EnergonCtrlr.DestrG));
                sw.WriteLine(Crypt("GuardGun1" + i + sp + EnergonCtrlr.Gun1));
                sw.WriteLine(Crypt("GuardGun2" + i + sp + EnergonCtrlr.Gun2));
                sw.WriteLine(Crypt("GuardGun3" + i + sp + EnergonCtrlr.Gun3));
                sw.WriteLine(Crypt("GuardMiniGun" + i + sp + EnergonCtrlr.MiniGun));
                sw.WriteLine(Crypt("GuardFighter" + i + sp + EnergonCtrlr.Fighter));
                //GuardsOnScene++;
            //}
        }
        //saving the count of Energon cruisers and Guard cruisers on scene if any 
        //sw.WriteLine(Crypt("EnergonCruisersOnScene" + sp + EnergonsOnScene));
        sw.WriteLine(Crypt("GuardCruisersOnScene" + sp + Lists.energonsOnScene.Count));

        for (int i = 0; i < Lists.energonsControllablesOnScene.Count; i++)
        {
            EnergonController EnergonCtrlr = Lists.energonsControllablesOnScene[i].GetComponent<EnergonController>();
            sw.WriteLine(Crypt("EnergonXPos" + i + sp + Lists.energonsControllablesOnScene[i].transform.position.x)); //y pos is always -8
            sw.WriteLine(Crypt("EnergonZPos" + i + sp + Lists.energonsControllablesOnScene[i].transform.position.z)); //y pos is always -8

            sw.WriteLine(Crypt("EnergonsStationXPos" + i + sp + EnergonCtrlr.energonsStation.transform.position.x)); //y pos is always -8
            sw.WriteLine(Crypt("EnergonsStationZPos" + i + sp + EnergonCtrlr.energonsStation.transform.position.z)); //y pos is always -8
            
            sw.WriteLine(Crypt("EnergonMovePointX" + i + sp + EnergonCtrlr.moveToPoint.x));
            sw.WriteLine(Crypt("EnergonMovePointZ" + i + sp + EnergonCtrlr.moveToPoint.z));
            sw.WriteLine(Crypt("EnergonYRotation" + i + sp + EnergonCtrlr.yRotation));
            sw.WriteLine(Crypt("EnergonIsMoving" + i + sp + EnergonCtrlr.isMoving));

            sw.WriteLine(Crypt("energonLevel" + i + sp + EnergonCtrlr.energonLevel));
            sw.WriteLine(Crypt("isPlayerEnergon" + i + sp + EnergonCtrlr.isPlayerEnergon));
            sw.WriteLine(Crypt("energyCapacity" + i + sp + EnergonCtrlr.energyCapacity));
            sw.WriteLine(Crypt("energyOfEnergon" + i + sp + EnergonCtrlr.energyOfEnergon));
            sw.WriteLine(Crypt("energonMovingSpeed" + i + sp + EnergonCtrlr.energonMovingSpeed));
            sw.WriteLine(Crypt("CPUNumber" + i + sp + EnergonCtrlr.CPUNumber));
        }
        //saving the count of Energon cruisers and Guard cruisers on scene if any 
        //sw.WriteLine(Crypt("EnergonCruisersOnScene" + sp + EnergonsOnScene));
        sw.WriteLine(Crypt("EnergonsOnScene" + sp + Lists.energonsControllablesOnScene.Count));
        sw.Close();
    }

    //юри шулай куп информация саклый торган файл, ничэнче этапны уткэн мэглуматны хэм дэ сатып алганмы уеннын юкмы мэглумэтен качырып языр очен ясалган, бары тик шушы мэглумэт
    //кенэ мохим
    private void SaveDaraga() {
        //saving data

        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/" + fileNameDaraga + ".art");
        string sp = " "; //space 
        if (Lists.alinganUroven == Lists.currentLevel)
        {
            //saving data for battle
            sw.WriteLine(Crypt("currentLevelDifficulty" + sp + Lists.currentLevelDifficulty));
            sw.WriteLine(Crypt("energyOfPlayer" + sp + Lists.energyOfPlayer));
            //sw.WriteLine(Crypt("boosterOfPlayer" + sp + Lists.boosterOfPlayer));
            sw.WriteLine(Crypt("playerColor" + sp + Lists.playerColor));
            sw.WriteLine(Crypt("elegeIdenBieclege" + sp + (Lists.currentLevel + 1)));
            sw.WriteLine(Crypt("Destr4Index" + sp + Constants.Instance.Destr4Index));
            sw.WriteLine(Crypt("Destr1Index" + sp + Constants.Instance.Destr1Index));
            sw.WriteLine(Crypt("shipsPreLimit" + sp + Constants.Instance.shipsPreLimit));
            sw.WriteLine(Crypt("shipsLimit" + sp + Constants.Instance.shipsLimit));
        }
        else {
            //saving data for battle
            sw.WriteLine(Crypt("currentLevelDifficulty" + sp + Lists.currentLevelDifficulty));
            sw.WriteLine(Crypt("energyOfPlayer" + sp + Lists.energyOfPlayer));
            //sw.WriteLine(Crypt("boosterOfPlayer" + sp + Lists.boosterOfPlayer));
            sw.WriteLine(Crypt("playerColor" + sp + Lists.playerColor));
            sw.WriteLine(Crypt("elegeIdenBieclege" + sp + Lists.alinganUroven));
            sw.WriteLine(Crypt("Destr4Index" + sp + Constants.Instance.Destr4Index));
            sw.WriteLine(Crypt("Destr1Index" + sp + Constants.Instance.Destr1Index));
            sw.WriteLine(Crypt("shipsPreLimit" + sp + Constants.Instance.shipsPreLimit));
            sw.WriteLine(Crypt("shipsLimit" + sp + Constants.Instance.shipsLimit));
        }

        //saving the information about player's cruiser fleet
        sw.WriteLine(Crypt("Cruis1OfPlayerCruis" + sp + Lists.Cruis1OfPlayerCruis));
        sw.WriteLine(Crypt("Cruis2OfPlayerCruis" + sp + Lists.Cruis2OfPlayerCruis));
        sw.WriteLine(Crypt("Cruis3OfPlayerCruis" + sp + Lists.Cruis3OfPlayerCruis));
        sw.WriteLine(Crypt("Cruis4OfPlayerCruis" + sp + Lists.Cruis4OfPlayerCruis));
        sw.WriteLine(Crypt("Destr1OfPlayerCruis" + sp + Lists.Destr1OfPlayerCruis));
        sw.WriteLine(Crypt("Destr1OfPlayerParCruis" + sp + Lists.Destr1OfPlayerParCruis));
        sw.WriteLine(Crypt("Destr2OfPlayerCruis" + sp + Lists.Destr2OfPlayerCruis));
        sw.WriteLine(Crypt("Destr2OfPlayerParCruis" + sp + Lists.Destr2OfPlayerParCruis));
        sw.WriteLine(Crypt("Destr3OfPlayerCruis" + sp + Lists.Destr3OfPlayerCruis));
        sw.WriteLine(Crypt("Destr4OfPlayerCruis" + sp + Lists.Destr4OfPlayerCruis));
        sw.WriteLine(Crypt("Gun1OfPlayerCruis" + sp + Lists.Gun1OfPlayerCruis));
        sw.WriteLine(Crypt("Gun2OfPlayerCruis" + sp + Lists.Gun2OfPlayerCruis));
        sw.WriteLine(Crypt("Gun3OfPlayerCruis" + sp + Lists.Gun3OfPlayerCruis));
        sw.WriteLine(Crypt("MiniGunOfPlayerCruis" + sp + Lists.MiniGunOfPlayerCruis)); 
        //sw.WriteLine(Crypt("boostrGainDark" + sp + Constants.Instance.boostrGainDark));

        sw.Close();
    }

    //tutor panel open
    public void openCloseTutorPanel(int index) {
        if (index == 1)
        {
            leftOrRight = 0; //setting the count of sprite to first one;
            TutorialPanelImage.sprite = TutorSprites[leftOrRight]; //setting the first tutor sprite as default
            TutorialPanel.transform.GetChild(0).gameObject.SetActive(true);
            TutorialPanel.transform.GetChild(1).gameObject.SetActive(false);
            TutorialPanel.SetActive(true);

            //making a pause in game while looking tutor
            Time.timeScale = 0;
            Time.fixedDeltaTime = 0;
        }
        else
        {
            TutorialPanel.SetActive(false);

            //setting the time scale to regular
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
        }
        CommonButtonAudio.Play();
    }
    //tutor panel open
    public void scrollTheTutorSprites (int index)
    {
        //1 is forth scrolling the sprites, 0 is back
        if (index == 1)
        {
            if (leftOrRight < TutorSprites.Count - 1)
            {
                leftOrRight++; //adding the count of sprite to first one;
                TutorialPanelImage.sprite = TutorSprites[leftOrRight]; //setting the first tutor sprite as default
            }
            else if (leftOrRight == TutorSprites.Count - 1 && !TutorialPanel.transform.GetChild(2).gameObject.activeInHierarchy&&!TutorialPanel.transform.GetChild(1).gameObject.activeInHierarchy)
            {
                TutorialPanel.transform.GetChild(0).gameObject.SetActive(false);
                TutorialPanel.transform.GetChild(2).gameObject.SetActive(true);
            }
            else if (leftOrRight == TutorSprites.Count-1 && !TutorialPanel.transform.GetChild(1).gameObject.activeInHierarchy)
            {
                TutorialPanel.transform.GetChild(0).gameObject.SetActive(false);
                TutorialPanel.transform.GetChild(2).gameObject.SetActive(false);
                TutorialPanel.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        else
        {
            if (leftOrRight > 0)
            {
                if (leftOrRight == TutorSprites.Count-1 && TutorialPanel.transform.GetChild(1).gameObject.activeInHierarchy)
                {
                    TutorialPanel.transform.GetChild(0).gameObject.SetActive(false);
                    TutorialPanel.transform.GetChild(1).gameObject.SetActive(false);
                    TutorialPanel.transform.GetChild(2).gameObject.SetActive(true);
                    leftOrRight++;
                }
                else if (leftOrRight == TutorSprites.Count - 1 && TutorialPanel.transform.GetChild(2).gameObject.activeInHierarchy)
                {
                    TutorialPanel.transform.GetChild(0).gameObject.SetActive(true);
                    TutorialPanel.transform.GetChild(1).gameObject.SetActive(false);
                    TutorialPanel.transform.GetChild(2).gameObject.SetActive(false);
                    leftOrRight++;
                }
                leftOrRight--; //deducing the count of sprite;
                TutorialPanelImage.sprite = TutorSprites[leftOrRight]; //setting the tutor sprite as 
            }
        }
        CommonButtonAudio.Play();
    }

    public void watchTutorialVideo()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=9rR3NBFWhbE");
    }

    // Update is called once per frame
    void Update()
    {
        //shows the station current production process
        if (stationPanelIsEnabled)
        {
            //currenProductionFill.fillAmount = stationReference.currentPruductionFillLocal;

            if (stationReference.isUpgrading) stationIcon.fillAmount = stationReference.upgradeFill;
        }
        //condition to check if player lost the game 
        if (!YouWinLoseButton.activeInHierarchy && Lists.playerStations.Count<1 && countOfPlayerCruisers() < 1 /*&& (Lists.emptyStations.Count < 1 || (Lists.emptyStations.Count > 0 && Lists.energyOfPlayer < 120))*/) 
        {
            youLoseTheGameFunction();
        }
        //setting the lowest timer to disply on CPU nxt turn time
        //if (CruisLaunchingCPUStationToDisplay.Count>0) CPUTurnSlider.value = CruisLaunchingCPUStationToDisplay[0].CPUSceneCruiserLaunchTimer*-1;

        if (GuardAttackTimerTitle.gameObject.activeInHierarchy) GuardTurnSlider.value = Lists.guardCoreStation.CPUSceneCruiserLaunchTimer*-1;
}

    
}
