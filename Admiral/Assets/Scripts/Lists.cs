
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lists : MonoBehaviour
{
    #region gameStartFeatures

    //trigger if the level is continued or new, in case if it is continued then in space ctrlr there will be loading processes from saved file
    public static bool isContinued;

    //DIMENSION SWITCHES

    public static bool isBlackDimension = true;
    public static bool isRedDimension = false;
    public static bool isBlueDimension = false;

    //UI SETTINGS
    public static bool isEnglish = true;
    public static bool isRussian = false;
    public static bool isChinees = false;
    public static bool isGerman = false;
    public static bool isSpanish = false;

    //current difficulty of game from 0-easy, 1-normal, 2-hard
    public static int currentLevelDifficulty = 0;

    //current level of game
    public static int currentLevel;

    //player color is used to set a player color while launching a level and used on space ctrlr class to assignt the rest colors to CPU
    //0-Red, 1-green, 2-blue, 3-yellow, 4-purple
    public static int playerColor=2;

    ////lesvel 1 params (Difficulty 0 - Easy, Difficulty 1 - Middle, Difficulty 2 - Hard, )
    //public static Dictionary<string, int> level1Params = new Dictionary<string, int>
    //{
    //    ["Station0"] = 2, //to set 1 here
    //    ["Station1"] = 0, //to set 0 here
    //    ["Station2"] = 0,
    //    ["Station3"] = 0,

    //    ["Empty0"] = 2,
    //    ["Empty1"] = 0,
    //    ["Empty2"] = 0,
    //    ["Empty3"] = 0,

    //    ["Guard0"] = 2, //to fix to 0
    //    ["Guard1"] = 1, //to set 0 here
    //    ["Guard2"] = 0,
    //    ["Guard3"] = 0,

    //    ["GuardStation"] = 0,

    //    ["Energon0"] = 6, //to set 6 here
    //    ["Energon1"] = 0, //to set 0 here
    //    ["Energon2"] = 0, //to set 0 here
    //    ["Energon3"] = 0,
    //}; 
    //public static Dictionary<string, int> level2Params = new Dictionary<string, int>
    //{
    //    ["Station0"] = 2, 
    //    ["Station1"] = 1, 
    //    ["Station2"] = 0,
    //    ["Station3"] = 0,

    //    ["Empty0"] = 2,
    //    ["Empty1"] = 1,
    //    ["Empty2"] = 0,
    //    ["Empty3"] = 0,

    //    ["Guard0"] = 3, 
    //    ["Guard1"] = 1, 
    //    ["Guard2"] = 0,
    //    ["Guard3"] = 0,

    //    ["GuardStation"] = 0,

    //    ["Energon0"] = 5, 
    //    ["Energon1"] = 1, 
    //    ["Energon2"] = 0, 
    //    ["Energon3"] = 0,
    //};
    //public static Dictionary<string, int> level3Params = new Dictionary<string, int>
    //{
    //    ["Station0"] = 1,
    //    ["Station1"] = 2,
    //    ["Station2"] = 0,
    //    ["Station3"] = 0,

    //    ["Empty0"] = 1,
    //    ["Empty1"] = 3,
    //    ["Empty2"] = 0,
    //    ["Empty3"] = 0,

    //    ["Guard0"] = 2, 
    //    ["Guard1"] = 2, 
    //    ["Guard2"] = 1,
    //    ["Guard3"] = 0,

    //    ["GuardStation"] = 1,

    //    ["Energon0"] = 3, 
    //    ["Energon1"] = 3, 
    //    ["Energon2"] = 0, 
    //    ["Energon3"] = 0,
    //};
    //public static Dictionary<string, int> level4Params = new Dictionary<string, int>
    //{
    //    ["Station0"] = 1,
    //    ["Station1"] = 1,
    //    ["Station2"] = 1,
    //    ["Station3"] = 0,

    //    ["Empty0"] = 1,
    //    ["Empty1"] = 2,
    //    ["Empty2"] = 1,
    //    ["Empty3"] = 0,

    //    ["Guard0"] = 1,
    //    ["Guard1"] = 3,
    //    ["Guard2"] = 1,
    //    ["Guard3"] = 0,

    //    ["GuardStation"] = 0,

    //    ["Energon0"] = 2,
    //    ["Energon1"] = 3,
    //    ["Energon2"] = 2,
    //    ["Energon3"] = 0,
    //}; 
    //public static Dictionary<string, int> level5Params = new Dictionary<string, int>
    //{
    //    ["Station0"] = 0,
    //    ["Station1"] = 1,
    //    ["Station2"] = 1,
    //    ["Station3"] = 0,

    //    ["Empty0"] = 0,
    //    ["Empty1"] = 2,
    //    ["Empty2"] = 1,
    //    ["Empty3"] = 0,

    //    ["Guard0"] = 0,
    //    ["Guard1"] = 1,
    //    ["Guard2"] = 4,
    //    ["Guard3"] = 0,

    //    ["GuardStation"] = 1,

    //    ["Energon0"] = 2,
    //    ["Energon1"] = 2,
    //    ["Energon2"] = 2,
    //    ["Energon3"] = 0,
    //};
    //public static Dictionary<string, int> level6Params = new Dictionary<string, int>
    //{
    //    ["Station0"] = 0,
    //    ["Station1"] = 0,
    //    ["Station2"] = 3,
    //    ["Station3"] = 0,

    //    ["Empty0"] = 0,
    //    ["Empty1"] = 1,
    //    ["Empty2"] = 3,
    //    ["Empty3"] = 0,

    //    ["Guard0"] = 0,
    //    ["Guard1"] = 2,
    //    ["Guard2"] = 3,
    //    ["Guard3"] = 0,

    //    ["GuardStation"] = 1,

    //    ["Energon0"] = 2,
    //    ["Energon1"] = 2,
    //    ["Energon2"] = 2,
    //    ["Energon3"] = 0,
    //};



    //public static List<Vector3> stationPoints = new List<Vector3> { new Vector3(50, -8, 100), new Vector3(100, -8, 0) , new Vector3(50, -8, -100) , new Vector3(-50, -8, -100) , 
    //    new Vector3(-100, -8, 0),new Vector3(-50, -8, 100), new Vector3(0, -8, 150), new Vector3(130, -8, 70), new Vector3(130, -8, -70), new Vector3(0, -8, -150), 
    //    new Vector3(-130, -8, -70), new Vector3(-130, -8, 70)};

    //public static List<Vector3> stationPoints = new List<Vector3> { new Vector3(50, -8, 50), new Vector3(50, -8,-50) , new Vector3(-50, -8, 0) , new Vector3(100, -8, 0) ,
    //    new Vector3(-50, -8, -100),new Vector3(-50, -8, 100), new Vector3(0, -8, 150), new Vector3(130, -8, -70), new Vector3(0, -8, -150), new Vector3(-130, -8, 70),
    //    new Vector3(150, -8, 70), new Vector3(-150, -8, -70)};

    #endregion gameStartFeatures

    //this var holds the reference to guard core station instance to display it's attack time on timer
    public static StationController guardCoreStation;

    //BATTLE SCENE!!!!
    public static List<GameObject> AllShips = new List<GameObject>();

    public static List<GameObject> AllPlayerShips = new List<GameObject>();
    public static List<GameObject> AllCPUShips = new List<GameObject>();

    public static List<GameObject> AllPlayerShipsWithoutGuns = new List<GameObject>();
    public static List<GameObject> AllCPUShipsWithoutGuns = new List<GameObject>();

    public static List<GameObject> PlayerShips1Class = new List<GameObject>();
    public static List<GameObject> CPUShips1Class = new List<GameObject>();

    public static List<GameObject> PlayerShips2Class = new List<GameObject>();
    public static List<GameObject> CPUShips2Class = new List<GameObject>();

    public static List<GameObject> PlayerShip = new List<GameObject>();

    //public static List<GameObject> PlayerDestroyers = new List<GameObject>();
    //public static List<GameObject> PlayerDestroyers1 = new List<GameObject>();
    //public static List<GameObject> PlayerDestroyers2 = new List<GameObject>();
    //public static List<GameObject> PlayerDestroyers3 = new List<GameObject>();
    //public static List<GameObject> PlayerDestroyers4 = new List<GameObject>();

    //public static List<GameObject> PlayerCruisers = new List<GameObject>();

    //this one is necessary to set one of cruisers on scene under player control
    public static List<GameObject> PlayerCruisers1 = new List<GameObject>();
    public static List<GameObject> PlayerCruisers2 = new List<GameObject>();
    public static List<GameObject> PlayerCruisers3 = new List<GameObject>();
    public static List<GameObject> PlayerCruisers4 = new List<GameObject>();

    //public static List<GameObject> CPUDestroyers = new List<GameObject>();
    //public static List<GameObject> CPUDestroyers1 = new List<GameObject>();
    //public static List<GameObject> CPUDestroyers2 = new List<GameObject>();
    //public static List<GameObject> CPUDestroyers3 = new List<GameObject>();
    //public static List<GameObject> CPUDestroyers4 = new List<GameObject>();
    //public static List<GameObject> CPUCruisers = new List<GameObject>();
    //public static List<GameObject> CPUCruisers1 = new List<GameObject>();
    //public static List<GameObject> CPUCruisers2 = new List<GameObject>();
    //public static List<GameObject> CPUCruisers3 = new List<GameObject>();
    //public static List<GameObject> CPUCruisers4 = new List<GameObject>();

    public static List<GameObject> AllPlayerGuns = new List<GameObject>();
    public static List<GameObject> AllCPUGuns = new List<GameObject>();

    public static List<GameObject> MiniGunsOnScene = new List<GameObject>();

    public static int cruiser1PlayerPutOnScene=0;
    public static int cruiser2PlayerPutOnScene = 0;
    public static int cruiser3PlayerPutOnScene = 0;
    public static int cruiser4PlayerPutOnScene = 0;
    public static bool playerCruiserSet = false;

    // this condition is used to check that first cruiser in loop of instattiation has been already assigned as the one that will make mega attack. 
    //Among the same types of cruisers
    //it is assigned true on CPUShipController when setting cruiser ship properties and set false on JourneyController while switching to attack 
    public static bool CPUMegaAttackIsAssigned = false;
    public static int alinganUroven=1;

    //public static List<GameObject> PlayerGunsSingle = new List<GameObject>();
    //public static List<GameObject> PlayerGunsDual = new List<GameObject>();
    //public static List<GameObject> PlayerGunsTriple = new List<GameObject>();

    //public static List<GameObject> CPUGunsSingle = new List<GameObject>();
    //public static List<GameObject> CPUGunsDual = new List<GameObject>();
    //public static List<GameObject> CPUGunsTriple = new List<GameObject>();

    //ships features to use in ships scripts
    //public static float DESTR_4_ACCUR_RANGE = 3; //less is better
    //public static float CRUIS_4_ACCUR_RANGE = 3;
    //public static float DESTR_3_ACCUR_RANGE = 2; //less is better
    //public static float CRUIS_3_ACCUR_RANGE = 2;
    //public static float DESTR_2_ACCUR_RANGE = 1;
    //public static float CRUIS_2_ACCUR_RANGE = 1;
    //public static float DESTR_1_ACCUR_RANGE = 0.7f;
    //public static float CRUIS_1_ACCUR_RANGE = 0.5f;

    //public static float DESTR_4_ATTACK_TIME = 4; //less is better
    //public static float CRUIS_4_ATTACK_TIME = 4;
    //public static float DESTR_3_ATTACK_TIME = 3;
    //public static float CRUIS_3_ATTACK_TIME = 2.5f;
    //public static float DESTR_2_ATTACK_TIME = 2f;
    //public static float CRUIS_2_ATTACK_TIME = 1.5f;
    //public static float DESTR_1_ATTACK_TIME = 1.5f;
    //public static float CRUIS_1_ATTACK_TIME = 1f;

    //public static float DESTR_4_ATTACK_FORCE = 0.5f; //more is better
    //public static float CRUIS_4_ATTACK_FORCE = 0.6f;
    //public static float DESTR_3_ATTACK_FORCE = 0.6f; //more is better
    //public static float CRUIS_3_ATTACK_FORCE = 0.8f;
    //public static float DESTR_2_ATTACK_FORCE = 1.1f;
    //public static float CRUIS_2_ATTACK_FORCE = 1.2f;
    //public static float DESTR_1_ATTACK_FORCE = 1.4f;
    //public static float CRUIS_1_ATTACK_FORCE = 1.6f;

    //public static int DESTR_4_HP = 5; //more is better
    //public static int CRUIS_4_HP = 8;
    //public static int DESTR_3_HP = 8;
    //public static int CRUIS_3_HP = 11;
    //public static int DESTR_2_HP = 11;
    //public static int CRUIS_2_HP = 15;
    //public static int DESTR_1_HP = 14;
    //public static int CRUIS_1_HP = 18;

    //public static int DESTR_4_BULLET_HARM = 1; //more is better
    //public static int CRUIS_4_BULLET_HARM = 2;
    //public static int DESTR_3_BULLET_HARM = 2;
    //public static int CRUIS_3_BULLET_HARM = 3;
    //public static int DESTR_2_BULLET_HARM = 3;
    //public static int CRUIS_2_BULLET_HARM = 4;
    //public static int DESTR_1_BULLET_HARM = 4;
    //public static int CRUIS_1_BULLET_HARM = 5;

    ////FOR PARALIZERS
    //public static float DESTR_2_MANUVRE_SPEED = 2;//more is better
    //public static float DESTR_1_MANUVRE_SPEED = 3;

    //public static float PARALIZE_TIME1 = 9;
    //public static float PARALIZE_TIME2 = 6;

    ////shield work time and reload time
    //public static float CRUIS_4_SHIELD_TIME = 3f; //more is better
    //public static float DESTR_3_SHIELD_TIME = 3f;
    //public static float CRUIS_3_SHIELD_TIME = 3.5f;
    //public static float DESTR_2_SHIELD_TIME = 3.5f;
    //public static float CRUIS_2_SHIELD_TIME = 4f;
    //public static float DESTR_1_SHIELD_TIME = 4.5f;
    //public static float CRUIS_1_SHIELD_TIME = 5;

    //public static float CRUIS_4_SHIELD_RELOAD_TIME = 9; // less is better
    //public static float DESTR_3_SHIELD_RELOAD_TIME = 8f;
    //public static float CRUIS_3_SHIELD_RELOAD_TIME = 7f;
    //public static float DESTR_2_SHIELD_RELOAD_TIME = 6f;
    //public static float CRUIS_2_SHIELD_RELOAD_TIME = 5f;
    //public static float DESTR_1_SHIELD_RELOAD_TIME = 4f;
    //public static float CRUIS_1_SHIELD_RELOAD_TIME = 3f;

    ////time dilation range (time freeze)
    //public static float DESTR_4_TIME_DILATION = 0.65f;
    //public static float CRUIS_4_TIME_DILATION = 0.6f; //less is better
    //public static float DESTR_3_TIME_DILATION = 0.45f;
    //public static float CRUIS_3_TIME_DILATION = 0.45f;
    //public static float DESTR_2_TIME_DILATION = 0.3f;
    //public static float CRUIS_2_TIME_DILATION = 0.3f;
    //public static float DESTR_1_TIME_DILATION = 0.2f;
    //public static float CRUIS_1_TIME_DILATION = 0.1f;

    ////gun properties
    //public static float SINGLE_ROTATION_SPEED = 20f;
    //public static float DUAL_ROTATION_SPEED = 25f;
    //public static float TRIPLE_ROTATION_SPEED = 35f;

    //public static float SINGLE_ROTATION_ANGLE = 11f;
    //public static float DUAL_ROTATION_ANGLE = 13f;
    //public static float TRIPLE_ROTATION_ANGLE = 15f;

    //public static float SINGLE_REPEAT_TIME = 0.07f;
    //public static float DUAL_REPEAT_TIME = 0.06f;
    //public static float TRIPLE_REPEAT_TIME = 0.06f;

    //public static int GUN_LIFE_SINGLE = 11;
    //public static int GUN_LIFE_DUAL = 13;
    //public static int GUN_LIFE_TRIPLE = 15;

    //SCENE TRANSFER PROPERTIRES
    //public static bool isAfterPirate; // this one is used to remove the tasks of station and reduce the money of palyer for that as penalty 
    //public static bool isAfterDestrWin;
    //public static bool isAfterDestrLost;
    public static bool isAfterNoCruis;
    public static bool isAfterBattleWin;
    public static bool isAfterBattleLost;

    //loses after player was defeated in battle (gains are the properties of the enemy ship)
    //public static int moneyLose;
    //public static int o2Lose;
    //public static int energyLose;
    //public static int foodLose;
    //public static int equipLose;
    //public static int electrLose;
    public static int C1Lose;
    public static int C2Lose;
    public static int C3Lose;
    public static int C4Lose;
    public static int D1Lose;
    public static int D1PLose;
    public static int D2Lose;
    public static int D2PLose;
    public static int D3Lose;
    public static int D4Lose;
    public static int G1Lose;
    public static int G2Lose;
    public static int G3Lose;
    public static int MiniGunLose;

    public static int C1LoseCPU;
    public static int C2LoseCPU;
    public static int C3LoseCPU;
    public static int C4LoseCPU;
    public static int CGLoseCPU;
    public static int D1LoseCPU;
    public static int D1PLoseCPU;
    public static int D2LoseCPU;
    public static int D2PLoseCPU;
    public static int D3LoseCPU;
    public static int D4LoseCPU;
    public static int DGLoseCPU;
    public static int G1LoseCPU;
    public static int G2LoseCPU;
    public static int G3LoseCPU;
    public static int FighterLoseCPU;

    //DEFENCE SCENE!!!!

    //public static List<GameObject> DefCPUShips = new List<GameObject>();

    public static List<GameObject> DefendGun = new List<GameObject>();


    //this one is responsible for showing interstitials
    public static int sceneChangeCounts;
    public static bool adsBought;

    //public static int GUN_HARM_SINGLE = 2;
    //public static int GUN_HARM_DUAL = 3;
    //public static int GUN_HARM_TRIPLE = 4;

    //public static int GUN_HP_SINGLE = 30;
    //public static int GUN_HP_DUAL = 40;
    //public static int GUN_HP_TRIPLE = 50;

    //public static int SHIP_HP_4_DEF = 15;
    //public static int SHIP_HP_3_DEF = 20;
    //public static int SHIP_HP_2_DEF = 30;
    //public static int SHIP_HP_1_DEF = 45;
    //public static int SHIP_HP_FED_DEF = 35;

    //public static float DESTR_4_DEF_ATTACK_FORCE = 0.06f; //0.027f;
    //public static float DESTR_3_DEF_ATTACK_FORCE = 0.07f;
    //public static float DESTR_2_DEF_ATTACK_FORCE = 0.1f;
    //public static float DESTR_1_DEF_ATTACK_FORCE = 0.15f;

    //public static float DESTR_4_DEF_MOVE_SPEED = 0.04f; //0.04f;
    //public static float DESTR_3_DEF_MOVE_SPEED = 0.05f;//0.05f;
    //public static float DESTR_2_DEF_MOVE_SPEED = 0.06f;//0.06f;
    //public static float DESTR_1_DEF_MOVE_SPEED = 0.07f;//0.07f;

    //public static float DEF_DESTR_4_ATTACK_TIME = 9; //less is better
    //public static float DEF_DESTR_3_ATTACK_TIME = 8;
    //public static float DEF_DESTR_2_ATTACK_TIME = 6f;
    //public static float DEF_DESTR_1_ATTACK_TIME = 4f;

    public static List<GameObject> ShipBullets = new List<GameObject>();

    //public static bool barrelIsSet= false;


    //JOURNEY SCENE!!!!
    //public static List<GameObject> ShipsOnScene = new List<GameObject>();
   // public static List<GameObject> GuardsOnScene = new List<GameObject>();
    //public static List<GameObject> StationsOnScene = new List<GameObject>();
    //public static List<GameObject> SpaceObjOnScene = new List<GameObject>();
    //public static List<GameObject> FogOnScene = new List<GameObject>();
    //public static List<GameObject> TeleportOnScene = new List<GameObject>();
    //public static List<GameObject> PiratesOnScene = new List<GameObject>(); //is necessary to allow only one pirate on scene

    //this properties count down the guard cruisers on each dimension
    //less the guards count stronger each following guard, if there are no guards any more in dimension then player can swith to other
    //public static int darkDimGuards = 3;
    //public static int blueDimGuards = 4;
    //public static int redDimGuards = 5;

    //switch to battle attack properties to hold them here between scenes and pull after switch is over

    //public static int moneyOfCPU;
    public static int energyOfCPU;
    //public static int O2OfCPU;
    //public static int foodOfCPU;
    //public static int equipmentOfCPU;
    //public static int electronicsOfCPU;
    public static int Cruis1CPU;
    public static int Cruis2CPU;
    public static int Cruis3CPU; 
    public static int Cruis4CPU; 
    public static int CruisGCPU;
    public static int Destr1CPU;
    public static int Destr2CPU;
    public static int Destr1CPUParal;
    public static int Destr2CPUParal;
    public static int Destr3CPU;  
    public static int Destr4CPU; 
    public static int DestrGCPU;
    public static int Gun1CPU;
    public static int Gun2CPU;
    public static int Gun3CPU;
    public static int FightersCPU; 

    //this properties are used to store player fleet properties on start of any battle

    public static int C1Start;
    public static int C2Start;
    public static int C3Start;
    public static int C4Start;
    public static int D1Start;
    public static int D1PStart;
    public static int D2Start;
    public static int D2PStart;
    public static int D3Start;
    public static int D4Start;
    public static int G1Start;
    public static int G2Start;
    public static int G3Start;
    public static int MiniGunStart;

    //save start values of player fleet to hold it while player is on battle
    public static void SetStartPlayerFleet()
    {
        C1Start = Lists.Cruis1OfPlayer;
        C2Start = Lists.Cruis2OfPlayer;
        C3Start = Lists.Cruis3OfPlayer;
        C4Start = Lists.Cruis4OfPlayer;
        D1Start = Lists.Destr1OfPlayer;
        D1PStart = Lists.Destr1OfPlayerPar;
        D2Start = Lists.Destr2OfPlayer;
        D2PStart = Lists.Destr2OfPlayerPar;
        D3Start = Lists.Destr3OfPlayer;
        D4Start = Lists.Destr4OfPlayer;
        G1Start = Lists.Gun1OfPlayer;
        G2Start = Lists.Gun2OfPlayer;
        G3Start = Lists.Gun3OfPlayer;
        MiniGunStart = Lists.MiniGunOfPlayer;
    }
    //set start values of player ship back when getting back from any battle to get into account all loses
    public static void SetStartPlayerFleetBack()
    {
        Lists.Cruis1OfPlayer = C1Start;
        Lists.Cruis2OfPlayer = C2Start;
        Lists.Cruis3OfPlayer = C3Start;
        Lists.Cruis4OfPlayer=C4Start;
        Lists.Destr1OfPlayer = D1Start;
        Lists.Destr1OfPlayerPar= D1PStart;
        Lists.Destr2OfPlayer=D2Start;
        Lists.Destr2OfPlayerPar=D2PStart;
        Lists.Destr3OfPlayer=D3Start;
        Lists.Destr4OfPlayer=D4Start;
        Lists.Gun1OfPlayer=G1Start;
        Lists.Gun2OfPlayer=G2Start;
        Lists.Gun3OfPlayer=G3Start;
        Lists.MiniGunOfPlayer= MiniGunStart;
    }

    //sets storage variables zero to use them for next battle
    public static void SetStartStorageFleetZero()
    {
        C1Start = 0;
        C2Start = 0;
        C3Start = 0;
        C4Start = 0;
        D1Start = 0;
        D1PStart = 0;
        D2Start = 0;
        D2PStart = 0;
        D3Start = 0;
        D4Start = 0;
        G1Start = 0;
        G2Start = 0;
        G3Start = 0;
        MiniGunStart = 0;
    }

    
    //following conditions holds type of met ship or station on journey scene and determines following win conditions on LaunchManager of Defence Scene after battle with fighters
    //so if ship is Destroyer type there will not be any tactical battle 
    public static bool battleWithStation = false;
    public static bool battleWithGuard = false;
    public static bool battleWithCruiser = false;

    // holds station type property to activate proper station type on battle scene
    public static int stationTypeLists;

    //method to set CPU ships while switching to battle scene from journey scene. 
    public static void setShipsForCPU(int Cruis1CPU, int Cruis2CPU, int Cruis3CPU, int Cruis4CPU, int CruisGCPU, int Gun1CPU, int Gun2CPU, int Gun3CPU,
        int Destr1CPU, int Destr1CPUParal, int Destr2CPU, int Destr2CPUParal, int Destr3CPU, int Destr4CPU, int DestrGCPU)
    {
        //Lists.moneyOfCPU = moneyOfCPU;
        //Lists.energyOfCPU = energyOfCPU;
        //Lists.O2OfCPU = O2OfCPU;
        //Lists.foodOfCPU = foodOfCPU;
        //Lists.equipmentOfCPU = equipmentOfCPU;
        //Lists.electronicsOfCPU = electronicsOfCPU;
        Lists.Cruis1CPU = Cruis1CPU;
        Lists.Cruis2CPU = Cruis2CPU;
        Lists.Cruis3CPU = Cruis3CPU;
        Lists.Cruis4CPU = Cruis4CPU;
        Lists.CruisGCPU = CruisGCPU;
        Lists.Destr1CPU = Destr1CPU;
        Lists.Destr2CPU = Destr2CPU;
        Lists.Destr1CPUParal = Destr1CPUParal;
        Lists.Destr2CPUParal = Destr2CPUParal;
        Lists.Destr3CPU = Destr3CPU;
        Lists.Destr4CPU = Destr4CPU;
        Lists.DestrGCPU = DestrGCPU;
        Lists.Gun1CPU = Gun1CPU;
        Lists.Gun2CPU = Gun2CPU;
        Lists.Gun3CPU = Gun3CPU;
    }

    //PREVOIUS VARIANT OF METHOD ABOVE
    //public static void setShipsForCPU(int moneyOfCPU, int energyOfCPU, int O2OfCPU, int foodOfCPU, int equipmentOfCPU, int electronicsOfCPU, int Cruis1CPU, int Cruis2CPU, int Cruis3CPU, int Cruis4CPU, int CruisGCPU, int Gun1CPU, int Gun2CPU, int Gun3CPU,
    //    int Destr1CPU, int Destr1CPUParal, int Destr2CPU, int Destr2CPUParal, int Destr3CPU, int Destr4CPU, int DestrGCPU) 
    //{
    //    Lists.moneyOfCPU = moneyOfCPU;
    //    Lists.energyOfCPU = energyOfCPU;
    //    Lists.O2OfCPU= O2OfCPU;
    //    Lists.foodOfCPU= foodOfCPU;
    //    Lists.equipmentOfCPU= equipmentOfCPU;
    //    Lists.electronicsOfCPU= electronicsOfCPU;
    //    Lists.Cruis1CPU = Cruis1CPU;
    //    Lists.Cruis2CPU = Cruis2CPU;
    //    Lists.Cruis3CPU = Cruis3CPU;
    //    Lists.Cruis4CPU = Cruis4CPU;
    //    Lists.CruisGCPU = CruisGCPU;
    //    Lists.Destr1CPU = Destr1CPU;
    //    Lists.Destr2CPU = Destr2CPU;
    //    Lists.Destr1CPUParal = Destr1CPUParal;
    //    Lists.Destr2CPUParal = Destr2CPUParal;
    //    Lists.Destr3CPU = Destr3CPU;
    //    Lists.Destr4CPU = Destr4CPU;
    //    Lists.DestrGCPU = DestrGCPU;
    //    Lists.Gun1CPU = Gun1CPU;
    //    Lists.Gun2CPU = Gun2CPU;
    //    Lists.Gun3CPU = Gun3CPU;
    //}

    //method to set CPU fighters while switching to defence scene from journey scene. 
    public static void setFightersForCPU(int FightersCPU)
    {
        Lists.FightersCPU = FightersCPU;
    }
    //PREVOIUS VARIANT OF METHOD ABOVE
    //public static void setFightersForCPU(int moneyOfCPU, int energyOfCPU, int O2OfCPU, int foodOfCPU, int equipmentOfCPU, int electronicsOfCPU, int FightersCPU)
    //{
    //    Lists.moneyOfCPU = moneyOfCPU;
    //    Lists.energyOfCPU = energyOfCPU;
    //    Lists.O2OfCPU = O2OfCPU;
    //    Lists.foodOfCPU = foodOfCPU;
    //    Lists.equipmentOfCPU = equipmentOfCPU;
    //    Lists.electronicsOfCPU = electronicsOfCPU;
    //    Lists.FightersCPU = FightersCPU;
    //}

    //method to set Player ships while switching to battle scene from journey scene. Used in CPUShipCtrlJourney class to transfer player ships from station or cruiser
    //depending whit is under attack by CPU, as well in LaunchingObjcts class to transfer player ships from player's cruiser 
    public static void setShipsForPlayer(int Cruis1CPU, int Cruis2CPU, int Cruis3CPU, int Cruis4CPU, int Gun1CPU, int Gun2CPU, int Gun3CPU,
        int Destr1CPU, int Destr1CPUParal, int Destr2CPU, int Destr2CPUParal, int Destr3CPU, int Destr4CPU, int MiniGun)
    {
        Lists.Cruis1OfPlayer = Cruis1CPU;
        Lists.Cruis2OfPlayer = Cruis2CPU;
        Lists.Cruis3OfPlayer = Cruis3CPU;
        Lists.Cruis4OfPlayer = Cruis4CPU;
        Lists.Destr1OfPlayer = Destr1CPU;
        Lists.Destr2OfPlayer = Destr2CPU;
        Lists.Destr1OfPlayerPar = Destr1CPUParal;
        Lists.Destr2OfPlayerPar = Destr2CPUParal;
        Lists.Destr3OfPlayer = Destr3CPU;
        Lists.Destr4OfPlayer = Destr4CPU;
        Lists.Gun1OfPlayer = Gun1CPU;
        Lists.Gun2OfPlayer = Gun2CPU;
        Lists.Gun3OfPlayer = Gun3CPU;
        Lists.MiniGunOfPlayer = MiniGun;
}

    //public static int Fighters;
    ////those are used to store fighters types for switching to defence scene (without battle scene)
    //public static int Destr1CPUFighters;
    //public static int Destr2CPUFighters;
    //public static int Destr3CPUFighters;
    //public static int Destr4CPUFighters;

    #region UIElements

    ////energy
    //public static string getNoEnergy()
    //{
    //    if (isEnglish) return "no Energy";
    //    else if (isRussian) return "нет Энергии";
    //    else if (isChinees) return "没有能量";
    //    else if (isGerman) return "keine Energie";
    //    else return "sin energía"; //isSpanish
    //}

    ////energy
    //public static string getEnergy()
    //{
    //    if (isEnglish) return "Energy";
    //    else if (isRussian) return "Энергия";
    //    else if (isChinees) return "能源";
    //    else if (isGerman) return "Energie";
    //    else return "Energía"; //isSpanish
    //}

    ////station fleet
    //public static string getStatFleet()
    //{
    //    if (isEnglish) return "Station fleet";
    //    else if (isRussian) return "Флот станции";
    //    else if (isChinees) return "车站车队";
    //    else if (isGerman) return "Stationsflotte";
    //    else return "Flota de la estación"; //isSpanish
    //}

    //public static string getProduction()
    //{
    //    if (isEnglish) return "Production";
    //    else if (isRussian) return "Производство";
    //    else if (isChinees) return "生产";
    //    else if (isGerman) return "Produktion";
    //    else return "producción"; //isSpanish
    //}

    //public static string getSpacePort()
    //{
    //    if (isEnglish) return "Space Port";
    //    else if (isRussian) return "Космопорт";
    //    else if (isChinees) return "太空港";
    //    else if (isGerman) return "Raumhafen";
    //    else return "Puerto espacial"; //isSpanish
    //}
    //public static string getCurrentProduction()
    //{
    //    if (isEnglish) return "current";
    //    else if (isRussian) return "текущее";
    //    else if (isChinees) return "当前";
    //    else if (isGerman) return "aktuelle";
    //    else return "actual"; //isSpanish
    //}

    ////buy
    //public static string getBuy()
    //{
    //    if (isEnglish) return "Buy";
    //    else if (isRussian) return "Купить";
    //    else if (isChinees) return "购买";
    //    else if (isGerman) return "kaufen";
    //    else return "Сompra"; //isSpanish
    //}
    
    //////defence
    //public static string getDefence()
    //{
    //    if (isEnglish) return "Defence";
    //    else if (isRussian) return "Защита";
    //    else if (isChinees) return "防御";
    //    else if (isGerman) return "verteidigen";
    //    else return "defensa"; //isSpanish
    //}

    ////fight
    //public static string getFight()
    //{
    //    if (isEnglish) return "Fight";
    //    else if (isRussian) return "Атака";
    //    else if (isChinees) return "斗争";
    //    else if (isGerman) return "Kampf";
    //    else return "lucha"; //isSpanish
    //}

    ////give up
    //public static string getGiveUp()
    //{
    //    if (isEnglish) return "Give up";
    //    else if (isRussian) return "Сдаться";
    //    else if (isChinees) return "放弃";
    //    else if (isGerman) return "Gib auf";
    //    else return "rendirse"; //isSpanish
    //}
    
    ////your fleet
    //public static string getYourFleet()
    //{
    //    if (isEnglish) return "your fleet";
    //    else if (isRussian) return "ваш флот";
    //    else if (isChinees) return "你的舰队";
    //    else if (isGerman) return "Ihre Flotte";
    //    else return "tu flota"; //isSpanish
    //}

    ////your fleet
    //public static string getYourStations()
    //{
    //    if (isEnglish) return "your stations";
    //    else if (isRussian) return "ваши станции";
    //    else if (isChinees) return "您的车站";
    //    else if (isGerman) return "Ihre Stationen";
    //    else return "tus estaciones"; //isSpanish
    //}

    ////their fleet text
    //public static string getTheirFleet()
    //{
    //    if (isEnglish) return "their fleet";
    //    else if (isRussian) return "их флот";
    //    else if (isChinees) return "他们的舰队";
    //    else if (isGerman) return "Schiffsflotte";
    //    else return "su flota"; //isSpanish
    //}

    ////win button text title
    //public static string getYouWinTxt()
    //{
    //    if (isEnglish) return "You win";
    //    else if (isRussian) return "Вы победили";
    //    else if (isChinees) return "你赢了";
    //    else if (isGerman) return "Du gewinnst";
    //    else return "Tú ganas"; //isSpanish
    //}
    ////lost button text title
    //public static string getYouLostTxt()
    //{
    //    if (isEnglish) return "defeat";
    //    else if (isRussian) return "Поражение";
    //    else if (isChinees) return "打败";
    //    else if (isGerman) return "Niederlage";
    //    else return "derrota"; //isSpanish
    //}
    ////your guns title
    //public static string getYouGunsTxt()
    //{
    //    if (isEnglish) return "your guns";
    //    else if (isRussian) return "Ваши орудия";
    //    else if (isChinees) return "你的枪";
    //    else if (isGerman) return "deine Waffen";
    //    else return "tus armas"; //isSpanish
    //}
    ////to battle title
    //public static string getToBattleTxt()
    //{
    //    if (isEnglish) return "to battle";
    //    else if (isRussian) return "к битве";
    //    else if (isChinees) return "去打仗";
    //    else if (isGerman) return "bekämpfen";
    //    else return "a la batalla"; //isSpanish
    //}
    ////no cruisers to continue title
    //public static string getNoCruisersTxt()
    //{
    //    if (isEnglish) return "no cruisers to continue";
    //    else if (isRussian) return "нет крейсеров для продолжения";
    //    else if (isChinees) return "没有巡洋舰继续";
    //    else if (isGerman) return "Keine Kreuzer weiter";
    //    else return "no hay cruceros para continuar"; //isSpanish
    //}

    ////watch video title
    //public static string getWatchVideoTxt()
    //{
    //    if (isEnglish) return "watch video";
    //    else if (isRussian) return "смотреть видео";
    //    else if (isChinees) return "看视频";
    //    else if (isGerman) return "Schau Video";
    //    else return "ver video"; //isSpanish
    //}

    ////buy cruiser title
    //public static string getBuyCruiserTxt()
    //{
    //    if (isEnglish) return "Buy Cruiser now";
    //    else if (isRussian) return "Купить крейсер";
    //    else if (isChinees) return "购买巡洋舰";
    //    else if (isGerman) return "Cruiser kaufen";
    //    else return "Comprar crucero"; //isSpanish
    //}
    ////destroyed cruisers title
    //public static string getDestroyedCruisersTxt()
    //{
    //    if (isEnglish) return "destroyed cruisers";
    //    else if (isRussian) return "уничтоженные крейсеры";
    //    else if (isChinees) return "被摧毁的巡洋舰";
    //    else if (isGerman) return "zerstörte Kreuzer";
    //    else return "cruceros destruidos"; //isSpanish
    //}
    ////no mini gun title
    //public static string getNoMiniGunTxt()
    //{
    //    if (isEnglish) return "no mini guns";
    //    else if (isRussian) return "нет мини пулеметов";
    //    else if (isChinees) return "没有迷你枪";
    //    else if (isGerman) return "keine Minigewehre";
    //    else return "sin mini pistolas"; //isSpanish
    //}

    //public static string getVulnerableTxt()
    //{
    //    if (isEnglish) return "station is vulnerable";
    //    else if (isRussian) return "станция уязвима";
    //    else if (isChinees) return "车站脆弱";
    //    else if (isGerman) return "Station ist anfällig";
    //    else return "la estación es vulnerable"; //isSpanish
    //}

    //public static string getTwoGunWarning()
    //{
    //    if (isEnglish) return "No more than 2 guns";
    //    else if (isRussian) return "не более 2-х пулеметов";
    //    else if (isChinees) return "不超过2支枪";
    //    else if (isGerman) return "nicht mehr als 2 Kanonen";
    //    else return "no más de 2 pistolas"; //isSpanish
    //}
    //public static string getNoCruiserWarning()
    //{
    //    if (isEnglish) return "Place at least 1 cruiser on the battlefield";
    //    else if (isRussian) return "разместите хотя бы 1 крейсер на поле битвы";
    //    else if (isChinees) return "在战场上放置至少1艘巡洋舰";
    //    else if (isGerman) return "Platziere mindestens 1 Kreuzer auf dem Schlachtfeld";
    //    else return "coloca al menos 1 crucero en el campo de batalla"; //isSpanish
    //}

    //public static string getBattleFieldScheme()
    //{
    //    if (isEnglish) return "Battlefield Scheme";
    //    else if (isRussian) return "Схема поля битвы";
    //    else if (isChinees) return "战场模式";
    //    else if (isGerman) return "Schlachtfeldschema";
    //    else return "Esquema de campo de batalla"; //isSpanish
    //}

    //public static string getLoadingText()
    //{
    //    if (isEnglish) return "loading";
    //    else if (isRussian) return "загрузка";
    //    else if (isChinees) return "装货";
    //    else if (isGerman) return "Laden";
    //    else return "cargando"; //isSpanish
    //}

    //public static string getC1Mega()
    //{
    //    if (isEnglish) return "mega laser";
    //    else if (isRussian) return "мега лазер";
    //    else if (isChinees) return "巨型激光";
    //    else if (isGerman) return "Mega-Laser";
    //    else return "mega láser"; //isSpanish
    //}
    //public static string getC2Mega()
    //{
    //    if (isEnglish) return "mega paralyzer";
    //    else if (isRussian) return "мега парализатор";
    //    else if (isChinees) return "大型瘫痪者";
    //    else if (isGerman) return "Mega-Lähmer";
    //    else return "Mega paralizador"; //isSpanish
    //}
    //public static string getC3Mega()
    //{
    //    if (isEnglish) return "fleet shield";
    //    else if (isRussian) return "щит для флота";
    //    else if (isChinees) return "舰队盾";
    //    else if (isGerman) return "Flottenschild";
    //    else return "escudo de flota"; //isSpanish
    //}
    //public static string getC4Mega()
    //{
    //    if (isEnglish) return "shock wave";
    //    else if (isRussian) return "ударная волна";
    //    else if (isChinees) return "激波";
    //    else if (isGerman) return "Schockwelle";
    //    else return "onda de choque"; //isSpanish
    //}
    //public static string getDoubleShot()
    //{
    //    if (isEnglish) return "double shot";
    //    else if (isRussian) return "двойной выстрел";
    //    else if (isChinees) return "双重射击";
    //    else if (isGerman) return "doppelschuss";
    //    else return "doble tiro"; //isSpanish
    //}
    //public static string getTripleShot()
    //{
    //    if (isEnglish) return "triple shot";
    //    else if (isRussian) return "тройной выстрел";
    //    else if (isChinees) return "三连拍";
    //    else if (isGerman) return "Dreifachschuss";
    //    else return "tiro triple"; //isSpanish
    //}

    //public static string getManeuvers()
    //{
    //    if (isEnglish) return "maneuvers";
    //    else if (isRussian) return "маневры";
    //    else if (isChinees) return "演习";
    //    else if (isGerman) return "Manöver";
    //    else return "maniobras"; //isSpanish
    //}
    //public static string getParalizerFeature()
    //{
    //    if (isEnglish) return "paralyzer";
    //    else if (isRussian) return "парализатор";
    //    else if (isChinees) return "瘫痪者";
    //    else if (isGerman) return "Lähmer";
    //    else return "paralizador"; //isSpanish
    //}
    //public static string getForceShield()
    //{
    //    if (isEnglish) return "force shield";
    //    else if (isRussian) return "силовой щит";
    //    else if (isChinees) return "力盾";
    //    else if (isGerman) return "Kraftschild";
    //    else return "escudo de fuerza"; //isSpanish
    //}
    //public static string getCruiserWord()
    //{
    //    if (isEnglish) return "cruiser";
    //    else if (isRussian) return "крейсер";
    //    else if (isChinees) return "类";
    //    else if (isGerman) return "Kreuzer";
    //    else return "crucero"; //isSpanish
    //}
    //public static string getDestrWord()
    //{
    //    if (isEnglish) return "destroyer";
    //    else if (isRussian) return "эсминец";
    //    else if (isChinees) return "驱逐舰";
    //    else if (isGerman) return "Zerstörer";
    //    else return "destructor"; //isSpanish
    //}

    //public static string getTurretWord()
    //{
    //    if (isEnglish) return "turret";
    //    else if (isRussian) return "турель";
    //    else if (isChinees) return "炮塔";
    //    else if (isGerman) return "Turm";
    //    else return "torreta"; //isSpanish
    //}
    //public static string getClassWord()
    //{
    //    if (isEnglish) return "class";
    //    else if (isRussian) return "класс";
    //    else if (isChinees) return "类";
    //    else if (isGerman) return "Klasse";
    //    else return "clase"; //isSpanish
    //}
    //public static string getMegaShotWord()
    //{
    //    if (isEnglish) return "mega attack";
    //    else if (isRussian) return "мега атака";
    //    else if (isChinees) return "大规模攻击";
    //    else if (isGerman) return "Mega-Angriff";
    //    else return "mega ataque"; //isSpanish
    //}
    //public static string getFirePowerWord()
    //{
    //    if (isEnglish) return "firepower";
    //    else if (isRussian) return "огневая мощь";
    //    else if (isChinees) return "火力";
    //    else if (isGerman) return "Feuerkraft";
    //    else return "potencia de fuego"; //isSpanish
    //}
    //public static string getFeaturesWord()
    //{
    //    if (isEnglish) return "features";
    //    else if (isRussian) return "специфика";
    //    else if (isChinees) return "特征";
    //    else if (isGerman) return "Eigenschaften";
    //    else return "caracteristicas"; //isSpanish
    //}
    //public static string getCostWord()
    //{
    //    if (isEnglish) return "cost";
    //    else if (isRussian) return "стоимость";
    //    else if (isChinees) return "成本";
    //    else if (isGerman) return "Kosten";
    //    else return "costo"; //isSpanish
    //}

    //#region station panel UI properties

    //public static string getOutOfLimitsWarning()
    //{
    //    if (isEnglish) return "limit reached";
    //    else if (isRussian) return "достигнут предел";
    //    else if (isChinees) return "达到限制";
    //    else if (isGerman) return "Limit erreicht";
    //    else return "límite alcanzado"; //isSpanish
    //}

    //public static string getYouHaveNoCruiserWarning()
    //{
    //    if (isEnglish) return "You have no cruiser to attack";
    //    else if (isRussian) return "У Вас нет крейсеров для атаки";
    //    else if (isChinees) return "你没有巡洋舰可以攻击";
    //    else if (isGerman) return "Sie haben keinen Kreuzer zum Angriff";
    //    else return "No tienes cruceros para atacar"; //isSpanish
    //}
    //public static string getTheyHaveNoCruiserWarning()
    //{
    //    if (isEnglish) return "They have no cruiser to attack";
    //    else if (isRussian) return "У них нет крейсеров для атаки";
    //    else if (isChinees) return "他们没有巡洋舰可以攻击";
    //    else if (isGerman) return "Sie haben keine Kreuzer zum Angriff";
    //    else return "No tienen cruceros para atacar"; //isSpanish
    //}
    //public static string getUpGrade()
    //{
    //    if (isEnglish) return "Upgrade";
    //    else if (isRussian) return "Апгрейд";
    //    else if (isChinees) return "升级";
    //    else if (isGerman) return "nachrüsten";
    //    else return "mejorar"; //isSpanish
    //}

    //#endregion station panel UI properties


    ////rank
    //public static string getRank()
    //{
    //    if (isEnglish) return "Rank";
    //    else if (isRussian) return "Ранг";
    //    else if (isChinees) return "秩";
    //    else if (isGerman) return "Rang";
    //    else return "Rango"; //isSpanish
    //}

    ////money
    //public static string getMoney()
    //{
    //    if (isEnglish) return "Money";
    //    else if (isRussian) return "Деньги";
    //    else if (isChinees) return "金钱";
    //    else if (isGerman) return "Geld";
    //    else return "Dinero"; //isSpanish
    //}

    ////their inventory
    //public static string getTheirInv()
    //{
    //    if (isEnglish) return "their inventory";
    //    else if (isRussian) return "их груз";
    //    else if (isChinees) return "他们的库存";
    //    else if (isGerman) return "Schiffsinventar";
    //    else return "su inventario"; //isSpanish
    //}
    ////food
    //public static string getFood()
    //{
    //    if (isEnglish) return "Food";
    //    else if (isRussian) return "Еда";
    //    else if (isChinees) return "食物";
    //    else if (isGerman) return "Essen";
    //    else return "Comida"; //isSpanish
    //}

    ////oxygen
    //public static string getOxygen()
    //{
    //    if (isEnglish) return "Oxygen";
    //    else if (isRussian) return "Кислород";
    //    else if (isChinees) return "氧气";
    //    else if (isGerman) return "Sauerstoff";
    //    else return "Oxígeno"; //isSpanish
    //}

    ////equipment
    //public static string getEquip()
    //{
    //    if (isEnglish) return "Equipment";
    //    else if (isRussian) return "Оборудование";
    //    else if (isChinees) return "设备";
    //    else if (isGerman) return "Ausrüstung";
    //    else return "Equipo"; //isSpanish
    //}

    ////electronics
    //public static string getElectro()
    //{
    //    if (isEnglish) return "Electronics";
    //    else if (isRussian) return "Электроника";
    //    else if (isChinees) return "电子";
    //    else if (isGerman) return "Elektronik";
    //    else return "Electrónica"; //isSpanish
    //}

    ////current tasks
    //public static string getCurrTask()
    //{
    //    if (isEnglish) return "Current tasks";
    //    else if (isRussian) return "Текущие задания";
    //    else if (isChinees) return "当前任务";
    //    else if (isGerman) return "Aktuelle aufgaben";
    //    else return "Tareas actuales"; //isSpanish
    //}

    ////tasks of station
    //public static string getTasksOfStat()
    //{
    //    if (isEnglish) return "Station tasks";
    //    else if (isRussian) return "Задания станции";
    //    else if (isChinees) return "车站任务";
    //    else if (isGerman) return "Stationsaufgaben";
    //    else return "Tareas de la estación"; //isSpanish
    //}

    ////station market
    //public static string getStatMarket()
    //{
    //    if (isEnglish) return "Station market";
    //    else if (isRussian) return "Рынок станции";
    //    else if (isChinees) return "车站市场";
    //    else if (isGerman) return "Bahnhofsmarkt";
    //    else return "Mercado de la estación"; //isSpanish
    //}
    ////sell
    //public static string getSell()
    //{
    //    if (isEnglish) return "Sell";
    //    else if (isRussian) return "Продать";
    //    else if (isChinees) return "出售";
    //    else if (isGerman) return "verkaufen";
    //    else return "venta"; //isSpanish
    //}

    ////delivery
    //public static string getDelivery()
    //{
    //    if (isEnglish) return "Delivery";
    //    else if (isRussian) return "Перевозка";
    //    else if (isChinees) return "交付";
    //    else if (isGerman) return "liefern";
    //    else return "entrega"; //isSpanish
    //}

    ////destroy
    //public static string getDestroy()
    //{
    //    if (isEnglish) return "Destroy";
    //    else if (isRussian) return "Уничтожить";
    //    else if (isChinees) return "破坏";
    //    else if (isGerman) return "zerstören";
    //    else return "destrucción"; //isSpanish
    //}
    ////ask
    //public static string getAsk()
    //{
    //    if (isEnglish) return "Ask";
    //    else if (isRussian) return "Просить";
    //    else if (isChinees) return "询问";
    //    else if (isGerman) return "bitten";
    //    else return "pedir"; //isSpanish
    //}

    ////space law
    //public static string getLaw()
    //{
    //    if (isEnglish) return "Space law based support for you";
    //    else if (isRussian) return "Поддержка для вас на основе космического права";
    //    else if (isChinees) return "太空法为您提供的支持";
    //    else if (isGerman) return "weltraumrechtliche Unterstützung für Sie";
    //    else return "apoyo basado en la ley espacial para usted"; //isSpanish
    //}


    ////your inventory
    //public static string getYourInv()
    //{
    //    if (isEnglish) return "your inventory";
    //    else if (isRussian) return "ваш груз";
    //    else if (isChinees) return "您的库存";
    //    else if (isGerman) return "Ihr Inventar";
    //    else return "tu inventario"; //isSpanish
    //}

    //#region messages from ships and stations

    //#region agressive ships messages 

    //#region greeting messages

    //#region aggr CPU ship mess
    //private static List<string> aggressiveShipHelloEng = new List<string>()
    //{"Greetings, we are really lucky to meet you here...",
    //    "Look who is here, you had to get stronger fleet...",
    //    "Are you lost? Well, the wolf found you..",
    //    "Pay or die wimp ...",
    //    "We will destroy you and not even notice..."
    //};

    //private static List<string> aggressiveShipHelloRus = new List<string>()
    //{"Добро пожаловать, нам очень повезло встретить вас тут...",
    //    "Только посмотрите кто тут, вам следовало усилисть свой флот...",
    //    "Вы потерялись? Ну что же, волк нашел вас...",
    //    "Заплати или умри слабак...",
    //    "мы уничтожим вас и даже не заметим..."
    //};

    //private static List<string> aggressiveShipHelloCh = new List<string>()
    //{"问候，我们很幸运在这里见到您",
    //    "看看谁在这里，你必须拥有更强大的舰队",
    //    "你迷路了吗？好吧，狼找到了你",
    //    "付钱或死w夫",
    //    "我们将摧毁你，甚至不会注意到"
    //};


    //private static List<string> aggressiveShipHelloSpa= new List<string>()
    //{"Saludos, tenemos mucha suerte de conocerte aquí",
    //     "Mira quién está aquí, tenías que conseguir una flota más fuerte",
    //     "¿Estás perdido? Bueno, el lobo te encontró",
    //     "Pagar o morir debilucho",
    //     "Te destruiremos y ni siquiera lo notaremos"
    //};

    //private static List<string> aggressiveShipHelloGer = new List<string>()
    //{"Grüße, wir sind wirklich glücklich, Sie hier zu treffen",
    //     "Guck mal, wer hier ist, du musstest eine stärkere Flotte bekommen",
    //     "Bist du verloren? Nun, der Wolf hat dich gefunden",
    //     "Zahlen oder sterben Weichei",
    //     "Wir werden dich zerstören und es nicht einmal bemerken"
    //};

    //public static string getAggrShipMessage()
    //{
    //    if (isEnglish) return aggressiveShipHelloEng[Random.Range(0, aggressiveShipHelloEng.Count)];
    //    else if (isRussian) return aggressiveShipHelloRus[Random.Range(0, aggressiveShipHelloRus.Count)];
    //    else if (isChinees) return aggressiveShipHelloCh[Random.Range(0, aggressiveShipHelloCh.Count)];
    //    else if (isGerman) return aggressiveShipHelloGer[Random.Range(0, aggressiveShipHelloGer.Count)];
    //    else return aggressiveShipHelloSpa[Random.Range(0, aggressiveShipHelloSpa.Count)];
    //}
    //#endregion aggr CPU ship mess


    //#region peace CPU ship mess

    //private static List<string> peaceShipHelloEng = new List<string>()
    //{"Hello my fellow captain, I am here to help you",
    //    "I am really glad to meet you here my friend",
    //    "Hello! May the universe help you",
    //    "Greetings! How can we help you?",
    //    "Nice to meet you, ask anything you want"
    //};

    //private static List<string> peaceShipHelloRus = new List<string>()
    //{"Здравствуй мой друг, я здесь чтобы помочь тебе",
    //    "Я очень рад встретить Вас тут мой друг",
    //    "Привет! Пусть вселенная поможет Вам",
    //    "Приветствую! чем мы можем Вам помочь?",
    //    "Приятно познакомиться, просите все, что вы хотите"
    //};

    //private static List<string> peaceShipHelloCh = new List<string>()
    //{"您好我的朋友队长，我在这里为您服务",
    //    "我很高兴在这里认识你",
    //    "你好！ 愿宇宙帮助你",
    //    "问候！ 我们该怎样帮助你？",
    //    "很高兴认识你，问任何你想问的"
    //};


    //private static List<string> peaceShipHelloSpa = new List<string>()
    //{"Hola mi amigo capitán, estoy aquí para ayudarte",
    //     "Estoy muy contenta de conocerte aquí mi amigo/a",
    //     "¡Hola! Que el universo te ayude",
    //     "¡Saludos! como podemos ayudarte?",
    //     "Encantado/a de conocerte, pregunta lo que quieras"
    //};

    //private static List<string> peaceShipHelloGer = new List<string>()
    //{"Hallo mein Kapitänskollege, ich bin hier, um Ihnen zu helfen",
    //     "Ich freue mich sehr, Sie hier zu treffen, mein Freund",
    //     "Hallo! Möge das Universum dir helfen",
    //     "Schöne Grüße! Wie können wir Ihnen helfen?",
    //     "Schön Sie kennenzulernen, fragen Sie alles, was Sie wollen"
    //};

    //public static string getPeaceShipMessage()
    //{
    //    if (isEnglish) return peaceShipHelloEng[Random.Range(0, peaceShipHelloEng.Count)];
    //    else if (isRussian) return peaceShipHelloRus[Random.Range(0, peaceShipHelloRus.Count)];
    //    else if (isChinees) return peaceShipHelloCh[Random.Range(0, peaceShipHelloCh.Count)];
    //    else if (isGerman) return peaceShipHelloGer[Random.Range(0, peaceShipHelloGer.Count)];
    //    else return peaceShipHelloSpa[Random.Range(0, peaceShipHelloSpa.Count)];
    //}

    //#endregion peace CPU ship mess

    //#endregion greeting messages

    //#region accept gift messages

    //private static List<string> aggressiveShipHelloEng = new List<string>()
    //{"Greetings, we are really lucky to meet you here...",
    //    "Look who is here, you had to get stronger fleet...",
    //    "Are you lost? Well, the wolf found you..",
    //    "Pay or die wimp ...",
    //    "We will destroy you and not even notice..."
    //};

    //private static List<string> aggressiveShipHelloRus = new List<string>()
    //{"Добро пожаловать, нам очень повезло встретить вас тут...",
    //    "Только посмотрите кто тут, вам следовало усилисть свой флот...",
    //    "Вы потерялись? Ну что же, волк нашел вас...",
    //    "Заплати или умри слабак...",
    //    "мы уничтожим вас и даже не заметим..."
    //};

    //private static List<string> aggressiveShipHelloCh = new List<string>()
    //{"问候，我们很幸运在这里见到您",
    //    "看看谁在这里，你必须拥有更强大的舰队",
    //    "你迷路了吗？好吧，狼找到了你",
    //    "付钱或死w夫",
    //    "我们将摧毁你，甚至不会注意到"
    //};


    //private static List<string> aggressiveShipHelloSpa = new List<string>()
    //{"Saludos, tenemos mucha suerte de conocerte aquí",
    //     "Mira quién está aquí, tenías que conseguir una flota más fuerte",
    //     "¿Estás perdido? Bueno, el lobo te encontró",
    //     "Pagar o morir debilucho",
    //     "Te destruiremos y ni siquiera lo notaremos"
    //};

    //private static List<string> aggressiveShipHelloGer = new List<string>()
    //{"Grüße, wir sind wirklich glücklich, Sie hier zu treffen",
    //     "Guck mal, wer hier ist, du musstest eine stärkere Flotte bekommen",
    //     "Bist du verloren? Nun, der Wolf hat dich gefunden",
    //     "Pay or Die Wimp",
    //     "Wir werden dich zerstören und es nicht einmal bemerken"
    //};

    //public static string getAggrShipMessage()
    //{
    //    if (isEnglish) return aggressiveShipHelloEng[Random.Range(0, aggressiveShipHelloEng.Count)];
    //    else if (isRussian) return aggressiveShipHelloRus[Random.Range(0, aggressiveShipHelloRus.Count)];
    //    else if (isChinees) return aggressiveShipHelloCh[Random.Range(0, aggressiveShipHelloCh.Count)];
    //    else if (isGerman) return aggressiveShipHelloGer[Random.Range(0, aggressiveShipHelloGer.Count)];
    //    else return aggressiveShipHelloSpa[Random.Range(0, aggressiveShipHelloSpa.Count)];
    //}

    //#endregion accept gift messages

    //#endregion agressive ships messages

    //#endregion messages from ships and stations

    #endregion UIElements

    #region Player properties 


    //the cruiser on jurney scene that is currently managed by player
    //public static int JourneyCruisLevel;

    //the cruiser of CPU1 on jurney scene that is currently managed by player
    //public static int JourneyCruisLevelCPU1 = 4;

    public static float energyOfPlayer = 500;
    //public static float boosterOfPlayer = 20;

    //public static int rankOfPlayer = 0;
    //public static int moneyOfPlayer = 0;
    //public static int O2OfPlayer = 0;
    //public static int foodOfPlayer = 0;
    //public static int equipmentOfPlayer = 0;
    //public static int electronicsOfPlayer = 0;
    public static int Cruis1OfPlayer = 0;  
    public static int Cruis2OfPlayer = 0;  
    public static int Cruis3OfPlayer = 0; 
    public static int Cruis4OfPlayer = 0; 
    public static int Destr1OfPlayer = 0;
    public static int Destr1OfPlayerPar = 0;
    public static int Destr2OfPlayer = 0;
    public static int Destr2OfPlayerPar = 0;
    public static int Destr3OfPlayer = 0;  
    public static int Destr4OfPlayer =0;  
    public static int Gun1OfPlayer = 0; 
    public static int Gun2OfPlayer =0; 
    public static int Gun3OfPlayer =0;
    public static int MiniGunOfPlayer =0;

    public static int Cruis1OfPlayerCruis = 0;
    public static int Cruis2OfPlayerCruis = 0;
    public static int Cruis3OfPlayerCruis =0;
    public static int Cruis4OfPlayerCruis = 1;
    public static int Destr1OfPlayerCruis = 0;
    public static int Destr1OfPlayerParCruis = 0;
    public static int Destr2OfPlayerCruis = 0;
    public static int Destr2OfPlayerParCruis = 0;
    public static int Destr3OfPlayerCruis = 0;
    public static int Destr4OfPlayerCruis = 3;
    public static int Gun1OfPlayerCruis = 0;
    public static int Gun2OfPlayerCruis =0;
    public static int Gun3OfPlayerCruis = 0;
    public static int MiniGunOfPlayerCruis = 0;

    public static int Cruis1OfPlayerForPull = 0;
    public static int Cruis2OfPlayerForPull = 0;
    public static int Cruis3OfPlayerForPull = 0;
    public static int Cruis4OfPlayerForPull = 0;
    public static int Destr1OfPlayerForPull = 0;
    public static int Destr1OfPlayerParForPull = 0;
    public static int Destr2OfPlayerForPull = 0;
    public static int Destr2OfPlayerParForPull = 0;
    public static int Destr3OfPlayerForPull = 0;
    public static int Destr4OfPlayerForPull = 0;
    public static int Gun1OfPlayerForPull = 0;
    public static int Gun2OfPlayerForPull = 0;
    public static int Gun3OfPlayerForPull = 0;
    public static int MiniGunOfPlayerForPull = 0;

    //clearing the fleet of player's cruiser after it was defeated by any of CPU attack (used on SpaceCtrlr class start method)
    public static void ClearFleetOfPlayerCruis()
    {
        Cruis1OfPlayerCruis = 0;
        Cruis2OfPlayerCruis = 0;
        Cruis3OfPlayerCruis = 0;
        Cruis4OfPlayerCruis = 0;
        Destr1OfPlayerCruis = 0;
        Destr1OfPlayerParCruis = 0;
        Destr2OfPlayerCruis = 0;
        Destr2OfPlayerParCruis = 0;
        Destr3OfPlayerCruis = 0;
        Destr4OfPlayerCruis = 0;
        Gun1OfPlayerCruis = 0;
        Gun2OfPlayerCruis = 0;
        Gun3OfPlayerCruis = 0;
        MiniGunOfPlayerCruis = 0;
    }

    public static void ClearFleetOfPlayerForPull()
    {
        Cruis1OfPlayerForPull = 0;
        Cruis2OfPlayerForPull = 0;
        Cruis3OfPlayerForPull = 0;
        Cruis4OfPlayerForPull = 0;
        Destr1OfPlayerForPull = 0;
        Destr1OfPlayerParForPull = 0;
        Destr2OfPlayerForPull = 0;
        Destr2OfPlayerParForPull = 0;
        Destr3OfPlayerForPull = 0;
        Destr4OfPlayerForPull = 0;
        Gun1OfPlayerForPull = 0;
        Gun2OfPlayerForPull = 0;
        Gun3OfPlayerForPull = 0;
        MiniGunOfPlayerForPull = 0;
    }
    public static void CopyFleetOfPlayerForPull()
    {
        Cruis1OfPlayerForPull = Cruis1OfPlayer;
        Cruis2OfPlayerForPull = Cruis2OfPlayer;
        Cruis3OfPlayerForPull = Cruis3OfPlayer;
        Cruis4OfPlayerForPull = Cruis4OfPlayer;
        Destr1OfPlayerForPull = Destr1OfPlayer;
        Destr1OfPlayerParForPull = Destr1OfPlayerPar;
        Destr2OfPlayerForPull = Destr2OfPlayer;
        Destr2OfPlayerParForPull = Destr2OfPlayerPar;
        Destr3OfPlayerForPull = Destr3OfPlayer;
        Destr4OfPlayerForPull = Destr4OfPlayer;
        Gun1OfPlayerForPull = Gun1OfPlayer;
        Gun2OfPlayerForPull = Gun2OfPlayer;
        Gun3OfPlayerForPull = Gun3OfPlayer;
        MiniGunOfPlayerForPull = MiniGunOfPlayer;
    }


    public static void UpdateFleetOfPlayerCruis(LaunchingObjcts playerCruiserOnAttackReference)
    {
        playerCruiserOnAttackReference.Cruis1 -= C1Lose;
        playerCruiserOnAttackReference.Cruis2 -= C2Lose;
        playerCruiserOnAttackReference.Cruis3 -= C3Lose;
        playerCruiserOnAttackReference.Cruis4 -= C4Lose;
        playerCruiserOnAttackReference.Destr1 -= D1Lose;
        playerCruiserOnAttackReference.Destr1Par -= D1PLose;
        playerCruiserOnAttackReference.Destr2 -= D2Lose;
        playerCruiserOnAttackReference.Destr2Par -= D2PLose;
        playerCruiserOnAttackReference.Destr3 -= D3Lose;
        playerCruiserOnAttackReference.Destr4 -= D4Lose;
        playerCruiserOnAttackReference.Gun1 -= G1Lose;
        playerCruiserOnAttackReference.Gun2 -= G2Lose;
        playerCruiserOnAttackReference.Gun3 -= G3Lose;
        playerCruiserOnAttackReference.MiniGun -= MiniGunLose;
    }
    public static void updateTheFleetOfCPUCruiserAfterVictory(CPUShipCtrlJourney CPUCruiserReference)
    {
        CPUCruiserReference.Cruis1 -= C1LoseCPU;
        CPUCruiserReference.Cruis2 -= C2LoseCPU;
        CPUCruiserReference.Cruis3 -= C3LoseCPU;
        CPUCruiserReference.Cruis4 -= C4LoseCPU;
        CPUCruiserReference.CruisG -= CGLoseCPU;
        CPUCruiserReference.Destr1 -= D1LoseCPU;
        CPUCruiserReference.Destr1Par -= D1PLoseCPU;
        CPUCruiserReference.Destr2 -= D2LoseCPU;
        CPUCruiserReference.Destr2Par -= D2PLoseCPU;
        CPUCruiserReference.Destr3 -= D3LoseCPU;
        CPUCruiserReference.Destr4 -= D4LoseCPU;
        CPUCruiserReference.DestrG -= DGLoseCPU;
        CPUCruiserReference.Gun1 -= G1LoseCPU;
        CPUCruiserReference.Gun2 -= G2LoseCPU;
        CPUCruiserReference.Gun3 -= G3LoseCPU;
        CPUCruiserReference.Fighter -= FighterLoseCPU;
    }

    public static void updateTheFleetOfGuardAfterVictory(EnergonMngr GuardCruiserReference)
    {
        GuardCruiserReference.Cruis1 -= C1LoseCPU;
        GuardCruiserReference.Cruis2 -= C2LoseCPU;
        GuardCruiserReference.Cruis3 -= C3LoseCPU;
        GuardCruiserReference.Cruis4 -= C4LoseCPU;
        GuardCruiserReference.CruisG -= CGLoseCPU;
        GuardCruiserReference.Destr1 -= D1LoseCPU;
        GuardCruiserReference.Destr1Par -= D1PLoseCPU;
        GuardCruiserReference.Destr2 -= D2LoseCPU;
        GuardCruiserReference.Destr2Par -= D2PLoseCPU;
        GuardCruiserReference.Destr3 -= D3LoseCPU;
        GuardCruiserReference.Destr4 -= D4LoseCPU;
        GuardCruiserReference.DestrG -= DGLoseCPU;
        GuardCruiserReference.Gun1 -= G1LoseCPU;
        GuardCruiserReference.Gun2 -= G2LoseCPU;
        GuardCruiserReference.Gun3 -= G3LoseCPU;
        GuardCruiserReference.Fighter -= FighterLoseCPU;
    }

    public static void updateTheFleetOfCPUStationAfterVictory(StationController CPUStationReference)
    {
        CPUStationReference.Cruis1 -= C1LoseCPU;
        CPUStationReference.Cruis2 -= C2LoseCPU;
        CPUStationReference.Cruis3 -= C3LoseCPU;
        CPUStationReference.Cruis4 -= C4LoseCPU;
        CPUStationReference.CruisG -= CGLoseCPU;
        CPUStationReference.Destr1 -= D1LoseCPU;
        CPUStationReference.Destr1Par -= D1PLoseCPU;
        CPUStationReference.Destr2 -= D2LoseCPU;
        CPUStationReference.Destr2Par -= D2PLoseCPU;
        CPUStationReference.Destr3 -= D3LoseCPU;
        CPUStationReference.Destr4 -= D4LoseCPU;
        CPUStationReference.DestrG -= DGLoseCPU;
        CPUStationReference.Gun1 -= G1LoseCPU;
        CPUStationReference.Gun2 -= G2LoseCPU;
        CPUStationReference.Gun3 -= G3LoseCPU;
        CPUStationReference.Fighter -= FighterLoseCPU;
    }
    #endregion Player properties

    #region Setting fleet of player on Battle field with all UI elements

    public static GameObject currentChousenShipIndicator;

    //first argunment is the index on vector3 array of possible player ship points on battlefield schema 
    //(look for Excel file on folder Admiral), second argument is ship GameObject
    public static Dictionary<string, GameObject> playerShipsSchema = new Dictionary<string, GameObject>();

    //is used to trigger for instantiating player fleet ship game objects on loading the scene of Battle. Normally real ships are instantiated on second load of battle scene 
    //cause in first time player sets them on UI schema
    public static bool playerFleetIsSet = false;

    //this lists are used to store all battlefield buttons with aim of recognition neighbour buttons are taken or not
    public static Dictionary<string, Button> notTakenButtonsDic = new Dictionary<string, Button>();
    public static Dictionary<string, Button> takenButtonsDic = new Dictionary<string, Button>();

    public static List <Button> notTakenButtons = new List<Button>(); 
    public static List<Button> takenButtons = new List<Button>();
    public static List<Button> takenButtonsCruisNearCruisStay = new List<Button>();
    public static List<Button> takenButtonsCruisChousen = new List<Button>();
    public static List<Button> takenButtonsSecondCruis = new List<Button>();

    
    #endregion Setting fleet of player on Battle field with all UI elements  


    #region newGamePlay properties


    ////energy reduce or increase after the battle in dependence who win or lose
    //public static int enrgyGainLoseDark = 100;
    //public static int enrgyGainLoseBlue = 120;
    //public static int enrgyGainLoseRed = 170;

    ////energy level that is necessary to upgrade station
    //public static int enrgy0to1Upgrd = 80;
    //public static int enrgy1to2Upgrd = 150;
    //public static int enrgy2to3Upgrd = 210;

    ////rate of reduce the energy when hit by energon bullet
    //public static int energonBullReduce = 35;

    //public static int asteroid1Reduce = 65;
    //public static int asteroid2Reduce = 35;
    //public static int asteroid3Reduce = 25;
    //public static int asteroid4Reduce = 15;
    ////this properties are used to change the maneuvre changes of energon ship after someone started to gather energy from it (is changed from PlayerEnrgyGather class
    //// and used by EnergonMngr class). Here are the default values
    //public static float energonDirChangeStartTime = 8;
    //public static float energonDirChangeEndTime = 10;

    ////following values set the energon moving speed and energon rotation lerp according to dimension (or their type)
    //public static float energonMovingSpeed4 = 1f;//0.025f;
    //public static float energonMovingSpeed3 = 1.4f;
    //public static float energonMovingSpeed2 = 1.8f;
    //public static float energonMovingSpeed1 = 2.2f;

    //public static float guardChaseInvokeTime4 = 25f;
    //public static float guardChaseInvokeTime3 = 22f;
    //public static float guardChaseInvokeTime2 = 18f;
    //public static float guardChaseInvokeTime1 = 15f;

    //public static float guardChaseTimeMiddle4 = 5f;
    //public static float guardChaseTimeMiddle3 = 7f;
    //public static float guardChaseTimeMiddle2 = 9f;
    //public static float guardChaseTimeMiddle1 = 11f;

    //public static float guardChaseSpeed4 = 5.2f;
    //public static float guardChaseSpeed3 = 6f;
    //public static float guardChaseSpeed2 = 7f;
    //public static float guardChaseSpeed1 = 8f;

    //public static float energonRotationLerp4 = 0.008f;
    //public static float energonRotationLerp3 = 0.01f;
    //public static float energonRotationLerp2 = 0.012f;
    //public static float energonRotationLerp1 = 0.015f;

    //public static float energonShotSpeed4 = 3.5f;
    //public static float energonShotSpeed3 = 2.8f;
    //public static float energonShotSpeed2 = 2.3f;
    //public static float energonShotSpeed1 = 1.8f;

    //public static float energonShotBullSpeed4 = 20f;
    //public static float energonShotBullSpeed3 = 25f;
    //public static float energonShotBullSpeed2 = 29f;
    //public static float energonShotBullSpeed1 = 35f;

    //public static float playerJourShotBullSpeed4 = 33f;
    //public static float playerJourShotBullSpeed3 = 36f;
    //public static float playerJourShotBullSpeed2 = 40f;
    //public static float playerJourShotBullSpeed1 = 45f;

    ////following variables determine the necessary energy to produce a ships by their types
    //public static float C1ProdEnergy = 80f;
    //public static float C2ProdEnergy = 60f;
    //public static float C3ProdEnergy = 40f;
    //public static float C4ProdEnergy = 25f;
    //public static float D1ProdEnergy = 58f;
    //public static float D1PProdEnergy = 55f;
    //public static float D2ProdEnergy = 45f;
    //public static float D2PProdEnergy = 40f;
    //public static float D3ProdEnergy = 20f;
    //public static float D4ProdEnergy = 10f;
    //public static float G1ProdEnergy = 40f;
    //public static float G2ProdEnergy = 50f;
    //public static float G3ProdEnergy = 60f;
    //public static float GMProdEnergy = 25f;

    ////higher the next upgrade level less the step of time to upgrade so more time consume to upgrade the station. Used only for Player stations
    //public static float time0to1Upgrd = 0.0006f; //0.0003f;
    //public static float time1to2Upgrd = 0.0004f;//0.0001f;
    //public static float time2to3Upgrd = 0.0002f;//0.00005f;

    ////higher the next upgrade level more the step of time to upgrade so more time consume to upgrade the station Used only for CPU stations
    //public static float time0to1UpgrdCPU = 40f;
    //public static float time1to2UpgrdCPU = 70f;
    //public static float time2to3UpgrdCPU = 110f;
    //public static float standartTimeBeforeUpgrdProcessStartCPU = 150f; //this one is used for waiting a standart amount of time before start of any upgrade
    
    ////time step to fill the station creating bar
    //public static float timeToCreateStation = 0.01f;//0.005f;

    ////this one is used to count a step btw the CPU station next fleet increases
    //public static float timeOfCPUStationStepEasy = 165f; //to make 165
    //public static float timeOfCPUStationStepMiddle = 155f; //to make 165
    //public static float timeOfCPUStationStepHard = 145f; //to make 165

    ////these determines randome range time to CPU station to launche a new cruiser
    //public static float timeOfCPUCruisLauncheMinEasy = 215f;//215;
    //public static float timeOfCPUCruisLauncheMaxEasy = 275f;//275f;

    //public static float timeOfCPUCruisLauncheMinMiddle = 195f;//215;
    //public static float timeOfCPUCruisLauncheMaxMiddle = 225f;//275f;

    //public static float timeOfCPUCruisLauncheMinHard = 175f;//215;
    //public static float timeOfCPUCruisLauncheMaxHard = 195f;//275f;
    
    ////these determines randome range time to Guard station to launche a new cruiser
    //public static float timeOfGuardCruisLauncheEasy = 650f;//700;

    //public static float timeOfGuardCruisLauncheMiddle = 500f;//215;

    //public static float timeOfGuardCruisLauncheHard = 400f;//215;

    ////the rate to reduce the energy while creating a station from zero or to capturing the enemy station
    ////these variables are also used to reduce or increase the energy of player while intake it from enerfon or lose it to Guard
    //public static float energyReduceNoTo0 = 0.2f; //0.1f;
    //public static float energyReduce1ToNo = 0.3f;//0.12f;
    //public static float energyReduce2ToNo = 0.4f;//0.14f;
    //public static float energyReduce3ToNo = 0.5f;//0.16f;

    ////those are the ship types moving speeds, they are adjusted to be relevant to player's ships moving speeds (though lower than player's ships moving speeds)
    //public static float CPUCruis4Speed = 0.09f;
    //public static float CPUCruis3Speed = 0.14f;
    //public static float CPUCruis2Speed = 0.16f;
    //public static float CPUCruis1Speed = 0.19f;

    ////this value is counted in experemental way, it is the amount of energy that takes creating a station in empty place
    //public static float energyEmptyToStation = 120;
    //public static float energyToAttckStations = 30;

    //public static float Cruis4ProductTimeStation0 = 0.0008f;//0.0001f;
    //public static float Destr4ProductTimeStation0 = 0.001f;//0.01f;//0.0003f;
    //public static float EnergyProductTimeStation0 = 0.001f;//0.01f;//0.0015f;

    //public static float Cruis4ProductTimeStation1 = 0.001f;//0.01f;//0.0003f;
    //public static float Cruis3ProductTimeStation1 = 0.0008f;//0.01f;;//0.0001f;
    //public static float Destr4ProductTimeStation1 = 0.002f;//0.01f;//0.0006f;
    //public static float Destr3ProductTimeStation1 = 0.001f;//0.01f;//0.0003f;
    //public static float Gun1ProductTimeStation1 = 0.0006f;//0.01f;//0.00005f;
    //public static float MiniGunProductTimeStation1 = 0.002f;
    //public static float EnergyProductTimeStation1 = 0.0025f;//0.0018f;

    //public static float Cruis4ProductTimeStation2 = 0.0015f;//0.0005f;
    //public static float Cruis3ProductTimeStation2 = 0.001f;//0.0003f;
    //public static float Cruis2ProductTimeStation2 = 0.0008f;//0.0001f;
    //public static float Destr4ProductTimeStation2 = 0.003f;//0.0009f;
    //public static float Destr3ProductTimeStation2 = 0.002f;//0.0006f;
    //public static float Destr2ProductTimeStation2 = 0.001f;//0.00025f;
    //public static float Destr2ParProductTimeStation2 = 0.001f;//0.0002f;
    //public static float Gun1ProductTimeStation2 = 0.0008f;//0.0001f;
    //public static float Gun2ProductTimeStation2 = 0.0006f;//0.00005f;
    //public static float MiniGunProductTimeStation2 = 0.003f;//0.0001f;
    //public static float EnergyProductTimeStation2 = 0.004f;//0.0021f;

    //public static float Cruis4ProductTimeStation3 = 0.0025f;//0.0007f;
    //public static float Cruis3ProductTimeStation3 = 0.0015f;//0.0005f;
    //public static float Cruis2ProductTimeStation3 = 0.001f;//0.0002f;
    //public static float Cruis1ProductTimeStation3 = 0.0008f;//0.0001f;
    //public static float Destr4ProductTimeStation3 = 0.007f;//0.0012f;
    //public static float Destr3ProductTimeStation3 = 0.003f;//0.0009f;
    //public static float Destr2ProductTimeStation3 = 0.002f;//0.00055f;
    //public static float Destr2ParProductTimeStation3 = 0.002f;//0.0005f;
    //public static float Destr1ProductTimeStation3 = 0.001f;//0.0002f;
    //public static float Destr1ParProductTimeStation3 = 0.001f;//0.00025f;
    //public static float Gun1ProductTimeStation3 = 0.001f;//0.0005f;
    //public static float Gun2ProductTimeStation3 = 0.0008f;//0.0001f;
    //public static float Gun3ProductTimeStation3 = 0.0006f;//0.00005f;
    //public static float MiniGunProductTimeStation3 = 0.005f;// 0.0002f;
    //public static float EnergyProductTimeStation3 = 0.007f;//0.0025f;
    

    #region properties of CPUs tha operate on game
    //public static float energyOfCPU1 = 0;
    //public static int Cruis1OfCPU1 = 0;
    //public static int Cruis2OfCPU1 = 0;
    //public static int Cruis3OfCPU1 = 0;
    //public static int Cruis4OfCPU1 = 0;
    //public static int CruisGOfCPU1 = 0;
    //public static int Destr1OfCPU1 = 0;
    //public static int Destr1OfCPU1Par = 0;
    //public static int Destr2OfCPU1 = 0;
    //public static int Destr2OfCPU1Par = 0;
    //public static int Destr3OfCPU1 = 0;
    //public static int Destr4OfCPU1 = 0;
    //public static int DestrGOfCPU1 = 0;
    //public static int Gun1OfCPU1 = 0;
    //public static int Gun2OfCPU1 = 0;
    //public static int Gun3OfCPU1 = 0;
    //public static int FighterOfCPU1 = 0;

    //this method is used on SpaceCtrlr class to clear the fleet of CPU cruiser after it was defeated by someone including player
    //public static void clearTheFleetOfCPU1AfterDefeat()
    //{
    //    Cruis1OfCPU1 = 0;
    //    Cruis2OfCPU1 = 0;
    //    Cruis3OfCPU1 = 0;
    //    Cruis4OfCPU1 = 0;
    //    CruisGOfCPU1 = 0;
    //    Destr1OfCPU1 = 0;
    //    Destr1OfCPU1Par = 0;
    //    Destr2OfCPU1 = 0;
    //    Destr2OfCPU1Par = 0;
    //    Destr3OfCPU1 = 0;
    //    Destr4OfCPU1 = 0;
    //    DestrGOfCPU1 = 0;
    //    Gun1OfCPU1 = 0;
    //    Gun2OfCPU1 = 0;
    //    Gun3OfCPU1 = 0;
    //    FighterOfCPU1 = 0;
    //}
    //public static void updateTheFleetOfCPU1AfterVictory()
    //{
    //    Cruis1OfCPU1 -= C1LoseCPU;
    //    Cruis2OfCPU1 -= C2LoseCPU;
    //    Cruis3OfCPU1 -= C3LoseCPU;
    //    Cruis4OfCPU1 -= C4LoseCPU;
    //    CruisGOfCPU1 -= CGLoseCPU;
    //    Destr1OfCPU1 -= D1LoseCPU;
    //    Destr1OfCPU1Par -= D1PLoseCPU;
    //    Destr2OfCPU1 -= D2LoseCPU;
    //    Destr2OfCPU1Par -= D2PLoseCPU;
    //    Destr3OfCPU1 -= D3LoseCPU;
    //    Destr4OfCPU1 -= D4LoseCPU;
    //    DestrGOfCPU1 -= DGLoseCPU;
    //    Gun1OfCPU1 -= G1LoseCPU;
    //    Gun2OfCPU1 -= G2LoseCPU;
    //    Gun3OfCPU1 -= G3LoseCPU;
    //    FighterOfCPU1 -= FighterLoseCPU;
    //}

    //public static float energyOfCPU2 = 0;
    //public static int Cruis1OfCPU2 = 0;
    //public static int Cruis2OfCPU2 = 0;
    //public static int Cruis3OfCPU2 = 0;
    //public static int Cruis4OfCPU2 = 0;
    //public static int CruisGOfCPU2 = 0;
    //public static int Destr1OfCPU2 = 0;
    //public static int Destr1OfCPU2Par = 0;
    //public static int Destr2OfCPU2 = 0;
    //public static int Destr2OfCPU2Par = 0;
    //public static int Destr3OfCPU2 = 0;
    //public static int Destr4OfCPU2 = 0;
    //public static int DestrGOfCPU2 = 0;
    //public static int Gun1OfCPU2 = 0;
    //public static int Gun2OfCPU2 = 0;
    //public static int Gun3OfCPU2 = 0;
    //public static int FighterOfCPU2 = 0;

    //public static void updateTheFleetOfCPU2AfterVictory()
    //{
    //    Cruis1OfCPU2 -= C1LoseCPU;
    //    Cruis2OfCPU2 -= C2LoseCPU;
    //    Cruis3OfCPU2 -= C3LoseCPU;
    //    Cruis4OfCPU2 -= C4LoseCPU;
    //    CruisGOfCPU2 -= CGLoseCPU;
    //    Destr1OfCPU2 -= D1LoseCPU;
    //    Destr1OfCPU2Par -= D1PLoseCPU;
    //    Destr2OfCPU2 -= D2LoseCPU;
    //    Destr2OfCPU2Par -= D2PLoseCPU;
    //    Destr3OfCPU2 -= D3LoseCPU;
    //    Destr4OfCPU2 -= D4LoseCPU;
    //    DestrGOfCPU2 -= DGLoseCPU;
    //    Gun1OfCPU2 -= G1LoseCPU;
    //    Gun2OfCPU2 -= G2LoseCPU;
    //    Gun3OfCPU2 -= G3LoseCPU;
    //    FighterOfCPU2 -= FighterLoseCPU;
    //}

    

    //public static float energyOfCPU3 = 0;
    //public static int Cruis1OfCPU3 = 0;
    //public static int Cruis2OfCPU3 = 0;
    //public static int Cruis3OfCPU3 = 0;
    //public static int Cruis4OfCPU3 = 0;
    //public static int CruisGOfCPU3 = 0;
    //public static int Destr1OfCPU3 = 0;
    //public static int Destr1OfCPU3Par = 0;
    //public static int Destr2OfCPU3 = 0;
    //public static int Destr2OfCPU3Par = 0;
    //public static int Destr3OfCPU3 = 0;
    //public static int Destr4OfCPU3 = 0;
    //public static int DestrGOfCPU3 = 0;
    //public static int Gun1OfCPU3 = 0;
    //public static int Gun2OfCPU3 = 0;
    //public static int Gun3OfCPU3 = 0;
    //public static int FighterOfCPU3 = 0;

    //public static void updateTheFleetOfCPU3AfterVictory()
    //{
    //    Cruis1OfCPU3 -= C1LoseCPU;
    //    Cruis2OfCPU3 -= C2LoseCPU;
    //    Cruis3OfCPU3 -= C3LoseCPU;
    //    Cruis4OfCPU3 -= C4LoseCPU;
    //    CruisGOfCPU3 -= CGLoseCPU;
    //    Destr1OfCPU3 -= D1LoseCPU;
    //    Destr1OfCPU3Par -= D1PLoseCPU;
    //    Destr2OfCPU3 -= D2LoseCPU;
    //    Destr2OfCPU3Par -= D2PLoseCPU;
    //    Destr3OfCPU3 -= D3LoseCPU;
    //    Destr4OfCPU3 -= D4LoseCPU;
    //    DestrGOfCPU3 -= DGLoseCPU;
    //    Gun1OfCPU3 -= G1LoseCPU;
    //    Gun2OfCPU3 -= G2LoseCPU;
    //    Gun3OfCPU3 -= G3LoseCPU;
    //    FighterOfCPU3 -= FighterLoseCPU;
    //}

    //public static float energyOfCPU4 = 0;
    //public static int Cruis1OfCPU4 = 0;
    //public static int Cruis2OfCPU4 = 0;
    //public static int Cruis3OfCPU4 = 0;
    //public static int Cruis4OfCPU4 = 0;
    //public static int CruisGOfCPU4 = 0;
    //public static int Destr1OfCPU4 = 0;
    //public static int Destr1OfCPU4Par = 0;
    //public static int Destr2OfCPU4 = 0;
    //public static int Destr2OfCPU4Par = 0;
    //public static int Destr3OfCPU4 = 0;
    //public static int Destr4OfCPU4 = 0;
    //public static int DestrGOfCPU4 = 0;
    //public static int Gun1OfCPU4 = 0;
    //public static int Gun2OfCPU4 = 0;
    //public static int Gun3OfCPU4 = 0;
    //public static int FighterOfCPU4 = 0;

    //public static void updateTheFleetOfCPU4AfterVictory()
    //{
    //    Cruis1OfCPU4 -= C1LoseCPU;
    //    Cruis2OfCPU4 -= C2LoseCPU;
    //    Cruis3OfCPU4 -= C3LoseCPU;
    //    Cruis4OfCPU4 -= C4LoseCPU;
    //    CruisGOfCPU4 -= CGLoseCPU;
    //    Destr1OfCPU4 -= D1LoseCPU;
    //    Destr1OfCPU4Par -= D1PLoseCPU;
    //    Destr2OfCPU4 -= D2LoseCPU;
    //    Destr2OfCPU4Par -= D2PLoseCPU;
    //    Destr3OfCPU4 -= D3LoseCPU;
    //    Destr4OfCPU4 -= D4LoseCPU;
    //    DestrGOfCPU4 -= DGLoseCPU;
    //    Gun1OfCPU4 -= G1LoseCPU;
    //    Gun2OfCPU4 -= G2LoseCPU;
    //    Gun3OfCPU4 -= G3LoseCPU;
    //    FighterOfCPU4 -= FighterLoseCPU;
    //}

    //public static int CruisGOfGuard = 0;
    //public static int DestrGOfGuard = 0;
    //public static int Gun1OfGuard = 0;
    //public static int Gun2OfGuard = 0;
    //public static int Gun3OfGuard = 0;
    //public static int FighterOfGuard = 0;


    //this list serves as trigger to start upgrade one of the stations that is ready for upgrade 
    //public static List<StationController> readyToUpgradeStationsCPU1 = new List<StationController>();

    //this list serves as trigger to ship to move towards the station that is ready to give ships to attack a enemy station
    //public static List<StationController> readyToGiveShipsStationCPU1 = new List<StationController>();

    //this list serves as trigger to ship to move towards the station that is ready to give ships to attack a enemy station
    //public static List<StationController> stationUnderAttackCPU1 = new List<StationController>();

    //dictionary to hold the station properties that the CPU1 cruiser is moving around currently
    //public static GameObject StationThisCPU1;
    //public static Vector3 RotateAroundStationCPU1;

    //public static bool CPU1CruisIsOn;

    #endregion properties of CPUs tha operate on game

    //empty stations count available in scene
    public static List<CaptureLine> emptyStations = new List<CaptureLine>();

    //this list is necessary to hold all stations on current scene (mostly to start attack)
    public static List<StationController> AllStations = new List<StationController>();

    public static List<StationController> playerStations = new List<StationController>();
    public static List<StationController> CPU1Stations = new List<StationController>();
    public static List<StationController> CPU2Stations = new List<StationController>();
    public static List<StationController> CPU3Stations = new List<StationController>();
    public static List<StationController> CPU4Stations = new List<StationController>();
    public static List<StationController> CPUGuardStations = new List<StationController>();
    //energy balls are added here from RandomRotator script of steroid while it is destroyed by guard
    //and removed from here while energon (players or CPUs) entered it's trigger
    //this collection is renewable after getting back to scene but not after getting back to game
    public static List<GameObject> energyBalls = new List<GameObject>();

    public static List<GameObject> shipsOnScene = new List<GameObject>();
    public static List<GameObject> energonsOnScene = new List<GameObject>(); //in this one holds only the guards after update of GP
    public static List<GameObject> energonsControllablesOnScene = new List<GameObject>(); //this one holds the energons only


    public static List<GameObject> AllSceneObjects = new List<GameObject>();

    //TODO this varisable should be set zero on main menu scene while switching to the new game or to be set to saved property while loading the saved game
    //this variable is used to check if player win the game, while start of the game CPU controllers increase this int and after they decriese it while they are destroyed
    //and this variable is checked on update of space controller class. If it iz zero than player wins 
    //public static int enemyCruisersOnScene = 0;
    public static int CPU1CruisersOnScene = 0;
    public static int CPU2CruisersOnScene = 0;
    public static int CPU3CruisersOnScene = 0;
    public static int CPU4CruisersOnScene = 0;
    public static int CPUGCruisersOnScene = 0;
    public static int enemyStationsOnScene = 0;

    //holding player's ship position and rotation to restore it on the same position it was when switching the scene
    //public static Vector3 playerShipPosition;
    //public static Quaternion playerShipRotation;

    //this values are used to set an energy production count before starting ship production sequence. It is all after capturing (empty) and upgrading all possible stations
    //so it meand that the CPU is preparing for attack
    //public static int level0StationSequenceProductTrigger(float currentEnergy) {
    //    int sequienceTrigger;
    //    if (currentEnergy < 65)
    //    {
    //        sequienceTrigger = Random.Range(50, 80);
    //        return sequienceTrigger;
    //    }
    //    else {
    //        sequienceTrigger = Random.Range(5, 15);
    //        return sequienceTrigger;
    //    }
    //}
    //public static int level123StationSequenceProductTrigger(float currentEnergy)
    //{
    //    int sequienceTrigger;
    //    if (currentEnergy < 50)
    //    {
    //        sequienceTrigger = Random.Range(50, 80);
    //        return sequienceTrigger;
    //    }
    //    else
    //    {
    //        sequienceTrigger = Random.Range(5, 15);
    //        return sequienceTrigger;
    //    }
    //}

    //public static int level4OnlyAttackIndex = 16;
    //public static int level43AttackIndex = 39;
    //public static int level42AttackIndex = 74;
    //public static int level41AttackIndex = 122;

    ////these are values to asses the power of fleet of any object in game
    //public static int Destr4Index = 1;
    //public static int Destr3Index = 2;
    //public static int Destr2Index = 3;
    //public static int Destr1Index = 4; 
    //public static int Cruis4Index = 2;
    //public static int Cruis3Index = 3;
    //public static int Cruis2Index = 5;
    //public static int Cruis1Index = 7;
    //public static int Gun3Index = 7;
    //public static int Gun2Index = 5;
    //public static int Gun1Index = 3;

    ////this constant is necessary to limit the production amount of stations and to limit the boarding amount to cruiser 
    ////this amount equals to the spaces count on battle field which is 45. Cruisr 1 and 2 class takes 3 places (because of maneuver features)
    ////figthers are not taken into account and has their own limits
    //public static int shipsPreLimit = 42; //this one is used to launche the last batch of ships production
    //public static int shipsLimit = 45;
    //public static int fightersLimit = 20;

    //this variables holds the references to ships and stations that participate on attack
    public static StationController stationOnAttack;
    public static GameObject shipOnAttack;
    public static GameObject CPUShipOnAttack;

    //this var is used to prevent a bug of missing reference exception after getting back to journey while player attacks CPU station, cause in this case 
    //there is no CPU ship holding on CPUShipOnAttack.It is set false after each getting back to journey scene
    public static bool PlayerAttacksCPUStation = false;

    //this condiyion holds the condition that scene is switching to give a sugnal to stop everything
    //public static bool sceneIsSwitching;

    public static void shipAttacksStation() {
        //first of all reset the condition of checking with whom this CPU is about to fight
        battleWithCruiser = false;
        battleWithStation = false;
        battleWithGuard = false;
        //and setting the coorect variant of fighting object
        battleWithStation = true;

        //this method is necessary to hold the start values of player fleet to reduce it after player gets back to that class from any battle
        //that prevents the bug of reducing players fleet twiсe
        //Lists.SetStartPlayerFleet();

        //saving journey scene game objects to restore tham after getting back to scene from battle
        if (Lists.playerStations.Count > 0)
        {
            foreach (StationController sc in Lists.playerStations)
            {
                Lists.AllSceneObjects.Add(sc.transform.gameObject);
            }
        }
        if (Lists.CPU1Stations.Count > 0)
        {
            foreach (StationController sc in Lists.CPU1Stations)
            {
                Lists.AllSceneObjects.Add(sc.transform.gameObject);
            }
        }
        if (Lists.CPU2Stations.Count > 0)
        {
            foreach (StationController sc in Lists.CPU2Stations)
            {
                Lists.AllSceneObjects.Add(sc.transform.gameObject);
            }
        }
        if (Lists.CPU3Stations.Count > 0)
        {
            foreach (StationController sc in Lists.CPU3Stations)
            {
                Lists.AllSceneObjects.Add(sc.transform.gameObject);
            }
        }
        if (Lists.CPU4Stations.Count > 0)
        {
            foreach (StationController sc in Lists.CPU4Stations)
            {
                Lists.AllSceneObjects.Add(sc.transform.gameObject);
            }
        }
        if (Lists.CPUGuardStations.Count > 0)
        {
            foreach (StationController sc in Lists.CPUGuardStations)
            {
                Lists.AllSceneObjects.Add(sc.transform.gameObject);
            }
        }
        if (Lists.emptyStations.Count > 0)
        {
            foreach (CaptureLine cl in Lists.emptyStations)
            {
                Lists.AllSceneObjects.Add(cl.transform.parent.gameObject);
            }
        }
        if (energyBalls.Count > 0)
        {
            foreach (GameObject go in energyBalls)
            {
                AllSceneObjects.Add(go);
            }
        }

        //setting false all stations and empty station on scene before switching the scene
        if (Lists.AllSceneObjects.Count > 0)
        {
            foreach (GameObject go in Lists.AllSceneObjects)
            {
                //this is necessary to prevent a bug of calling destroyed game object, so all stations (of anyone) and empty stations
                //should nt be destroyed
                DontDestroyOnLoad(go);
                go.SetActive(false);
            }
        }

        //setting false all CPU ships on scene before switching the scene
        if (Lists.shipsOnScene.Count > 0)
        {
            //this is necessary to prevent a bug of calling destroyed game object, so all CPU ships should nt be destroyed
            foreach (GameObject go in Lists.shipsOnScene)
            {
                if (!go.CompareTag("BullDstrPlay1") && !go.CompareTag("BullDstrPlay2") && !go.CompareTag("BullDstrPlay3") && !go.CompareTag("BullDstrPlay4"))
                {
                    go.GetComponent<LaunchingObjcts>().disactivateCapture();
                }
                DontDestroyOnLoad(go);
                go.SetActive(false);
            }
        }

        if (Lists.energonsOnScene.Count > 0)
        {
            //this is necessary to prevent a bug of calling destroyed game object, so all CPU ships should nt be destroyed
            foreach (GameObject go in Lists.energonsOnScene)
            {
                DontDestroyOnLoad(go);
                go.SetActive(false);
            }
        }
        if (Lists.energonsControllablesOnScene.Count > 0)
        {
            //this is necessary to prevent a bug of calling destroyed game object, so all CPU ships should nt be destroyed
            foreach (GameObject go in energonsControllablesOnScene)
            {
                DontDestroyOnLoad(go);
                go.SetActive(false);
            }
        }

        

        //TODO WITH OTHER CPU THIS STATIONS
        //if (StationThisCPU1!=null) DontDestroyOnLoad(StationThisCPU1);

        //sceneIsSwitching = true;

        //disactivating capture effect of player cruiser to prevent a bug of missing player cruiser on CaptureButton class
        //if () SpaceCtrlr.Instance.localLaunchingObjects.disactivateCapture();
        //disactivating capture effect of CPU cruiser to prevent a bug of missing CPU cruiser on while capturing
        //CPUShipCtrlJourney.Instance.disactivateCaptureEffect();

        //saving player's ship position and rotation to restore it on the same position it was when switching the scene
        //playerShipPosition = SpaceCtrlr.Instance.CruisJourneyReal.transform.position;
        //playerShipRotation = SpaceCtrlr.Instance.CruisJourneyReal.transform.rotation;
        clearStaticWinLoseProps(); //prepare the win lose variables to get new data from new battle
        CPUMegaAttackIsAssigned = false; //this one is for letting CPU to use only 1 mega attack
    }

    public static void guardAttacksShip()
    {
        //first of all reset the condition of checking with whom this CPU is about to fight
        battleWithCruiser = false;
        battleWithStation = false;
        battleWithGuard = false;
        //and setting the coorect variant of fighting object
        battleWithGuard = true;

        //this method is necessary to hold the start values of player fleet to reduce it after player gets back to that class from any battle
        //that prevents the bug of reducing players fleet twiсe
        //Lists.SetStartPlayerFleet();

        //saving journey scene game objects to restore tham after getting back to scene from battle
        if (Lists.playerStations.Count > 0)
        {
            foreach (StationController sc in Lists.playerStations)
            {
                Lists.AllSceneObjects.Add(sc.transform.gameObject);
            }
        }
        if (Lists.CPU1Stations.Count > 0)
        {
            foreach (StationController sc in Lists.CPU1Stations)
            {
                Lists.AllSceneObjects.Add(sc.transform.gameObject);
            }
        }
        if (Lists.CPU2Stations.Count > 0)
        {
            foreach (StationController sc in Lists.CPU2Stations)
            {
                Lists.AllSceneObjects.Add(sc.transform.gameObject);
            }
        }
        if (Lists.CPU3Stations.Count > 0)
        {
            foreach (StationController sc in Lists.CPU3Stations)
            {
                Lists.AllSceneObjects.Add(sc.transform.gameObject);
            }
        }
        if (Lists.CPU4Stations.Count > 0)
        {
            foreach (StationController sc in Lists.CPU4Stations)
            {
                Lists.AllSceneObjects.Add(sc.transform.gameObject);
            }
        }
        if (Lists.CPUGuardStations.Count > 0)
        {
            foreach (StationController sc in Lists.CPUGuardStations)
            {
                Lists.AllSceneObjects.Add(sc.transform.gameObject);
            }
        }
        if (Lists.emptyStations.Count > 0)
        {
            foreach (CaptureLine cl in Lists.emptyStations)
            {
                Lists.AllSceneObjects.Add(cl.transform.parent.gameObject);
            }
        }
        if (energyBalls.Count > 0)
        {
            foreach (GameObject go in energyBalls)
            {
                AllSceneObjects.Add(go);
            }
        }

        //setting false all stations and empty station on scene before switching the scene
        if (Lists.AllSceneObjects.Count > 0)
        {
            foreach (GameObject go in Lists.AllSceneObjects)
            {
                //this is necessary to prevent a bug of calling destroyed game object, so all stations (of anyone) and empty stations
                //should nt be destroyed
                DontDestroyOnLoad(go);
                go.SetActive(false);
            }
        }

        //setting false all CPU ships on scene before switching the scene
        if (Lists.shipsOnScene.Count > 0)
        {
            //this is necessary to prevent a bug of calling destroyed game object, so all CPU ships should nt be destroyed
            foreach (GameObject go in Lists.shipsOnScene)
            {
                if (!go.CompareTag("BullDstrPlay1") && !go.CompareTag("BullDstrPlay2") && !go.CompareTag("BullDstrPlay3") && !go.CompareTag("BullDstrPlay4"))
                {
                    go.GetComponent<LaunchingObjcts>().disactivateCapture();
                }
                DontDestroyOnLoad(go);
                go.SetActive(false);
            }
        }
        if (Lists.energonsOnScene.Count > 0)
        {
            //this is necessary to prevent a bug of calling destroyed game object, so all CPU ships should nt be destroyed
            foreach (GameObject go in Lists.energonsOnScene)
            {
                DontDestroyOnLoad(go);
                go.SetActive(false);
            }
        }
        if (Lists.energonsControllablesOnScene.Count > 0)
        {
            //this is necessary to prevent a bug of calling destroyed game object, so all CPU ships should nt be destroyed
            foreach (GameObject go in energonsControllablesOnScene)
            {
                DontDestroyOnLoad(go);
                go.SetActive(false);
            }
        }

        //TODO WITH OTHER CPU THIS STATIONS
        //if (StationThisCPU1 != null) DontDestroyOnLoad(StationThisCPU1);

        //sceneIsSwitching = true;

        //disactivating capture effect of player cruiser to prevent a bug of missing player cruiser on CaptureButton class
        //SpaceCtrlr.Instance.localLaunchingObjects.disactivateCapture();
        //disactivating capture effect of CPU cruiser to prevent a bug of missing CPU cruiser on while capturing
        //CPUShipCtrlJourney.Instance.disactivateCaptureEffect();

        //saving player's ship position and rotation to restore it on the same position it was when switching the scene
        //playerShipPosition = SpaceCtrlr.Instance.CruisJourneyReal.transform.position;
        //playerShipRotation = SpaceCtrlr.Instance.CruisJourneyReal.transform.rotation;
        clearStaticWinLoseProps(); //prepare the win lose variables to get new data from new battle
        CPUMegaAttackIsAssigned = false; //this one is for letting CPU to use only 1 mega attack
    }

    public static void CPUCruisAttacksShip()
    {
        //first of all reset the condition of checking with whom this CPU is about to fight
        battleWithCruiser = false;
        battleWithStation = false;
        battleWithGuard = false;
        //and setting the coorect variant of fighting object
        battleWithCruiser = true;

        //this method is necessary to hold the start values of player fleet to reduce it after player gets back to that class from any battle
        //that prevents the bug of reducing players fleet twiсe
        //Lists.SetStartPlayerFleet();

        //saving journey scene game objects to restore tham after getting back to scene from battle
        if (Lists.playerStations.Count > 0)
        {
            foreach (StationController sc in Lists.playerStations)
            {
                Lists.AllSceneObjects.Add(sc.transform.gameObject);
            }
        }
        if (Lists.CPU1Stations.Count > 0)
        {
            foreach (StationController sc in Lists.CPU1Stations)
            {
                Lists.AllSceneObjects.Add(sc.transform.gameObject);
            }
        }
        if (Lists.CPU2Stations.Count > 0)
        {
            foreach (StationController sc in Lists.CPU2Stations)
            {
                Lists.AllSceneObjects.Add(sc.transform.gameObject);
            }
        }
        if (Lists.CPU3Stations.Count > 0)
        {
            foreach (StationController sc in Lists.CPU3Stations)
            {
                Lists.AllSceneObjects.Add(sc.transform.gameObject);
            }
        }
        if (Lists.CPU4Stations.Count > 0)
        {
            foreach (StationController sc in Lists.CPU4Stations)
            {
                Lists.AllSceneObjects.Add(sc.transform.gameObject);
            }
        }
        if (Lists.CPUGuardStations.Count > 0)
        {
            foreach (StationController sc in Lists.CPUGuardStations)
            {
                Lists.AllSceneObjects.Add(sc.transform.gameObject);
            }
        }
        if (Lists.emptyStations.Count > 0)
        {
            foreach (CaptureLine cl in Lists.emptyStations)
            {
                Lists.AllSceneObjects.Add(cl.transform.parent.gameObject);
            }
        }
        if (energyBalls.Count > 0)
        {
            foreach (GameObject go in energyBalls)
            {
                AllSceneObjects.Add(go);
            }
        }

        //setting false all stations and empty station on scene before switching the scene
        if (Lists.AllSceneObjects.Count > 0)
        {
            foreach (GameObject go in Lists.AllSceneObjects)
            {
                //this is necessary to prevent a bug of calling destroyed game object, so all stations (of anyone) and empty stations
                //should nt be destroyed
                DontDestroyOnLoad(go);
                go.SetActive(false);
            }
        }

        //setting false all CPU ships on scene before switching the scene
        if (Lists.shipsOnScene.Count > 0)
        {
            //this is necessary to prevent a bug of calling destroyed game object, so all CPU ships should nt be destroyed
            foreach (GameObject go in Lists.shipsOnScene)
            {
                if (!go.CompareTag("BullDstrPlay1") && !go.CompareTag("BullDstrPlay2") && !go.CompareTag("BullDstrPlay3") && !go.CompareTag("BullDstrPlay4"))
                {
                    go.GetComponent<LaunchingObjcts>().disactivateCapture();
                }
                DontDestroyOnLoad(go);
                go.SetActive(false);
            }
        }
        if (Lists.energonsOnScene.Count > 0)
        {
            //this is necessary to prevent a bug of calling destroyed game object, so all CPU ships should nt be destroyed
            foreach (GameObject go in Lists.energonsOnScene)
            {
                DontDestroyOnLoad(go);
                go.SetActive(false);
            }
        }
        if (Lists.energonsControllablesOnScene.Count > 0)
        {
            //this is necessary to prevent a bug of calling destroyed game object, so all CPU ships should nt be destroyed
            foreach (GameObject go in energonsControllablesOnScene)
            {
                DontDestroyOnLoad(go);
                go.SetActive(false);
            }
        }

        //TODO WITH OTHER CPU THIS STATIONS
        //if (StationThisCPU1 != null) DontDestroyOnLoad(StationThisCPU1);

        //sceneIsSwitching = true;

        //disactivating capture effect of player cruiser to prevent a bug of missing player cruiser on CaptureButton class
        //SpaceCtrlr.Instance.localLaunchingObjects.disactivateCapture();
        //disactivating capture effect of CPU cruiser to prevent a bug of missing CPU cruiser on while capturing
        //CPUShipCtrlJourney.Instance.disactivateCaptureEffect();

        //saving player's ship position and rotation to restore it on the same position it was when switching the scene
        //playerShipPosition = SpaceCtrlr.Instance.CruisJourneyReal.transform.position;
        //playerShipRotation = SpaceCtrlr.Instance.CruisJourneyReal.transform.rotation;
        clearStaticWinLoseProps(); //prepare the win lose variables to get new data from new battle
        CPUMegaAttackIsAssigned = false; //this one is for letting CPU to use only 1 mega attack
    }

    //clear all temporary lose properties after the battle
    private static void clearStaticWinLoseProps()
    {
        C1Lose = 0;
        C2Lose = 0;
        C3Lose = 0;
        C4Lose = 0;
        D1Lose = 0;
        D1PLose = 0;
        D2Lose = 0;
        D2PLose = 0;
        D3Lose = 0;
        D4Lose = 0;
        G1Lose = 0;
        G2Lose = 0;
        G3Lose = 0;
        MiniGunLose = 0;

        C1LoseCPU = 0;
        C2LoseCPU = 0;
        C3LoseCPU = 0;
        C4LoseCPU = 0;
        D1LoseCPU = 0;
        D1PLoseCPU = 0;
        D2LoseCPU = 0;
        D2PLoseCPU = 0;
        D3LoseCPU = 0;
        D4LoseCPU = 0;
        G1LoseCPU = 0;
        G2LoseCPU = 0;
        G3LoseCPU = 0;
        FighterLoseCPU = 0;
    }

    //this var is used to hold an opposite CPU color to represent it's color to player
    public static Color colorOfOpposite; //= new Color(0.88f, 0.88f, 0.88f, 1);
    public static Color colorOfPlayer;

    //this var is used to hold a reference to index of dummy to set on defence scene as object to defend. So the order is following
    //1 - cruis1 dummy, 2 - cruis2 dummy, 3 - cruis3 dummy, 4 - cruis4 dummy, 11 - StationFed dummy, 22 - station C dummy, 33 - StationD dummy, 44 - station A dummy
    public static int dummyOnDefenceScene;

    //this var is used to hold a reference to index of dummy to set on defence scene as object to defend. So the order is following
    //1 - cruis1 dummy, 2 - cruis2 dummy, 3 - cruis3 dummy, 4 - cruis4 dummy, 5 - cruisG dummy, 
    //11 - StationFed dummy, 22 - station C dummy, 33 - StationD dummy, 44 - station A dummy, 55 - station A dummy
    public static int dummyOnDefenceSceneEnemy;

    //this bool is used to assign the station on battle scene to be player's or enemye's, so the stations on battle scene also will attack the fleets
    public static bool isPlayerStationOnDefence;

    #endregion newGamePlay properties
}
