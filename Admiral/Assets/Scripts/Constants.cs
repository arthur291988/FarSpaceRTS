using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : Singleton<Constants>
{
    //lesvel 1 params (Difficulty 0 - Easy, Difficulty 1 - Middle, Difficulty 2 - Hard, )
    public Dictionary<string, int> level1Params = new Dictionary<string, int>
    {
        ["Station0"] = 2, //to set 1 here
        ["Station1"] = 0, //to set 0 here
        ["Station2"] = 0,
        ["Station3"] = 0,

        ["Empty0"] = 2,
        ["Empty1"] = 0,
        ["Empty2"] = 0,
        ["Empty3"] = 0,

        ["Guard0"] = 3, //to fix to 0
        ["Guard1"] = 0, //to set 0 here
        ["Guard2"] = 0,
        ["Guard3"] = 0,

        ["GuardStation"] = 0,

        ["Energon0"] = 6, //to set 6 here
        ["Energon1"] = 0, //to set 0 here
        ["Energon2"] = 0, //to set 0 here
        ["Energon3"] = 0,
    };
    public Dictionary<string, int> level2Params = new Dictionary<string, int>
    {
        ["Station0"] = 2,
        ["Station1"] = 1,
        ["Station2"] = 0,
        ["Station3"] = 0,

        ["Empty0"] = 2,
        ["Empty1"] = 1,
        ["Empty2"] = 0,
        ["Empty3"] = 0,

        ["Guard0"] = 3,
        ["Guard1"] = 1,
        ["Guard2"] = 0,
        ["Guard3"] = 0,

        ["GuardStation"] = 0,

        ["Energon0"] = 5,
        ["Energon1"] = 1,
        ["Energon2"] = 0,
        ["Energon3"] = 0,
    };
    public Dictionary<string, int> level3Params = new Dictionary<string, int>
    {
        ["Station0"] = 1,
        ["Station1"] = 2,
        ["Station2"] = 0,
        ["Station3"] = 0,

        ["Empty0"] = 1,
        ["Empty1"] = 3,
        ["Empty2"] = 0,
        ["Empty3"] = 0,

        ["Guard0"] = 2,
        ["Guard1"] = 2,
        ["Guard2"] = 1,
        ["Guard3"] = 0,

        ["GuardStation"] = 1,

        ["Energon0"] = 3,
        ["Energon1"] = 3,
        ["Energon2"] = 0,
        ["Energon3"] = 0,
    };
    public Dictionary<string, int> level4Params = new Dictionary<string, int>
    {
        ["Station0"] = 1,
        ["Station1"] = 1,
        ["Station2"] = 1,
        ["Station3"] = 0,

        ["Empty0"] = 1,
        ["Empty1"] = 2,
        ["Empty2"] = 1,
        ["Empty3"] = 0,

        ["Guard0"] = 1,
        ["Guard1"] = 3,
        ["Guard2"] = 1,
        ["Guard3"] = 0,

        ["GuardStation"] = 0,

        ["Energon0"] = 2,
        ["Energon1"] = 3,
        ["Energon2"] = 2,
        ["Energon3"] = 0,
    };
    public Dictionary<string, int> level5Params = new Dictionary<string, int>
    {
        ["Station0"] = 0,
        ["Station1"] = 1,
        ["Station2"] = 1,
        ["Station3"] = 0,

        ["Empty0"] = 0,
        ["Empty1"] = 2,
        ["Empty2"] = 1,
        ["Empty3"] = 0,

        ["Guard0"] = 0,
        ["Guard1"] = 1,
        ["Guard2"] = 4,
        ["Guard3"] = 0,

        ["GuardStation"] = 1,

        ["Energon0"] = 2,
        ["Energon1"] = 2,
        ["Energon2"] = 2,
        ["Energon3"] = 0,
    };
    public Dictionary<string, int> level6Params = new Dictionary<string, int>
    {
        ["Station0"] = 0,
        ["Station1"] = 0,
        ["Station2"] = 3,
        ["Station3"] = 0,

        ["Empty0"] = 0,
        ["Empty1"] = 1,
        ["Empty2"] = 3,
        ["Empty3"] = 0,

        ["Guard0"] = 0,
        ["Guard1"] = 2,
        ["Guard2"] = 3,
        ["Guard3"] = 0,

        ["GuardStation"] = 1,

        ["Energon0"] = 2,
        ["Energon1"] = 2,
        ["Energon2"] = 2,
        ["Energon3"] = 0,
    };
    public Dictionary<string, int> level7Params = new Dictionary<string, int>
    {
        ["Station0"] = 0,
        ["Station1"] = 1,
        ["Station2"] = 3,
        ["Station3"] = 0,

        ["Empty0"] = 1,
        ["Empty1"] = 2,
        ["Empty2"] = 3,
        ["Empty3"] = 0,

        ["Guard0"] = 0,
        ["Guard1"] = 3,
        ["Guard2"] = 2,
        ["Guard3"] = 1,

        ["GuardStation"] = 1,

        ["Energon0"] = 1,
        ["Energon1"] = 1,
        ["Energon2"] = 3,
        ["Energon3"] = 0,
    };
    public Dictionary<string, int> level8Params = new Dictionary<string, int>
    {
        ["Station0"] = 0,
        ["Station1"] = 1,
        ["Station2"] = 2,
        ["Station3"] = 1,

        ["Empty0"] = 0,
        ["Empty1"] = 2,
        ["Empty2"] = 2,
        ["Empty3"] = 1,

        ["Guard0"] = 0,
        ["Guard1"] = 1,
        ["Guard2"] = 4,
        ["Guard3"] = 1,

        ["GuardStation"] =0,

        ["Energon0"] = 1,
        ["Energon1"] = 1,
        ["Energon2"] = 3,
        ["Energon3"] = 0,
    };
    public Dictionary<string, int> level9Params = new Dictionary<string, int>
    {
        ["Station0"] = 0,
        ["Station1"] = 0,
        ["Station2"] = 2,
        ["Station3"] = 2,

        ["Empty0"] = 0,
        ["Empty1"] = 1,
        ["Empty2"] = 3,
        ["Empty3"] = 3,

        ["Guard0"] = 0,
        ["Guard1"] = 0,
        ["Guard2"] = 3,
        ["Guard3"] = 2,

        ["GuardStation"] = 1,

        ["Energon0"] = 1,
        ["Energon1"] = 0,
        ["Energon2"] = 3,
        ["Energon3"] = 2,
    };
    public Dictionary<string, int> level10Params = new Dictionary<string, int>
    {
        ["Station0"] = 0,
        ["Station1"] = 0,
        ["Station2"] = 0,
        ["Station3"] =4,

        ["Empty0"] = 0,
        ["Empty1"] = 0,
        ["Empty2"] = 0,
        ["Empty3"] = 8,

        ["Guard0"] = 0,
        ["Guard1"] = 0,
        ["Guard2"] = 0,
        ["Guard3"] = 6,

        ["GuardStation"] = 1,

        ["Energon0"] = 1,
        ["Energon1"] = 0,
        ["Energon2"] = 1,
        ["Energon3"] = 3,
    };

    public Vector3 playerCruiserStartPos = new Vector3(138, -8, 70);
    public Quaternion playerCruiserStartRotation = Quaternion.Euler(0, -36, 0);

    //this constant station positions does not include the position of guard station that is always Vector3 (0,-8,0)
    public List<Vector3> stationPoints = new List<Vector3> { new Vector3(50, -8, 50), new Vector3(50, -8,-50) , new Vector3(-50, -8, 0) , new Vector3(100, -8, 0) ,
        new Vector3(-50, -8, -100),new Vector3(-50, -8, 100), new Vector3(0, -8, 150), new Vector3(130, -8, -70), new Vector3(0, -8, -150), new Vector3(-130, -8, 70),
        new Vector3(150, -8, 70), new Vector3(-150, -8, -70)};

    public float DESTR_4_ACCUR_RANGE = 3; //less is better
    public float CRUIS_4_ACCUR_RANGE = 3;
    public float DESTR_3_ACCUR_RANGE = 2; //less is better
    public float CRUIS_3_ACCUR_RANGE = 2;
    public float DESTR_2_ACCUR_RANGE = 1;
    public float CRUIS_2_ACCUR_RANGE = 1;
    public float DESTR_1_ACCUR_RANGE = 0.7f;
    public float CRUIS_1_ACCUR_RANGE = 0.5f;

    public float DESTR_4_ATTACK_TIME = 4; //less is better
    public float CRUIS_4_ATTACK_TIME = 4;
    public float DESTR_3_ATTACK_TIME = 3;
    public float CRUIS_3_ATTACK_TIME = 2.5f;
    public float DESTR_2_ATTACK_TIME = 2f;
    public float CRUIS_2_ATTACK_TIME = 1.5f;
    public float DESTR_1_ATTACK_TIME = 1.5f;
    public float CRUIS_1_ATTACK_TIME = 1f;

    public float DESTR_4_ATTACK_FORCE = 0.5f; //more is better
    public float CRUIS_4_ATTACK_FORCE = 0.6f;
    public float DESTR_3_ATTACK_FORCE = 0.6f; //more is better
    public float CRUIS_3_ATTACK_FORCE = 0.8f;
    public float DESTR_2_ATTACK_FORCE = 1.1f;
    public float CRUIS_2_ATTACK_FORCE = 1.2f;
    public float DESTR_1_ATTACK_FORCE = 1.4f;
    public float CRUIS_1_ATTACK_FORCE = 1.6f;

    public int DESTR_4_HP = 5; //more is better
    public int CRUIS_4_HP = 8;
    public int DESTR_3_HP = 8;
    public int CRUIS_3_HP = 11;
    public int DESTR_2_HP = 11;
    public int CRUIS_2_HP = 15;
    public int DESTR_1_HP = 14;
    public int CRUIS_1_HP = 18;

    public int DESTR_4_BULLET_HARM = 1; //more is better
    public int CRUIS_4_BULLET_HARM = 2;
    public int DESTR_3_BULLET_HARM = 2;
    public int CRUIS_3_BULLET_HARM = 3;
    public int DESTR_2_BULLET_HARM = 3;
    public int CRUIS_2_BULLET_HARM = 4;
    public int DESTR_1_BULLET_HARM = 4;
    public int CRUIS_1_BULLET_HARM = 5;

    //FOR PARALIZERS
    public float DESTR_2_MANUVRE_SPEED = 2;//more is better
    public float DESTR_1_MANUVRE_SPEED = 3;

    public float PARALIZE_TIME1 = 9;
    public float PARALIZE_TIME2 = 6;

    //unity ads Ids
    public string googleId = "4009839";
    public string appStoreId = "4009838";

    //shield work time and reload time
    public float CRUIS_4_SHIELD_TIME = 3f; //more is better
    public float DESTR_3_SHIELD_TIME = 3f;
    public float CRUIS_3_SHIELD_TIME = 3.5f;
    public float DESTR_2_SHIELD_TIME = 3.5f;
    public float CRUIS_2_SHIELD_TIME = 4f;
    public float DESTR_1_SHIELD_TIME = 4.5f;
    public float CRUIS_1_SHIELD_TIME = 5;

    public float CRUIS_4_SHIELD_RELOAD_TIME = 9; // less is better
    public float DESTR_3_SHIELD_RELOAD_TIME = 8f;
    public float CRUIS_3_SHIELD_RELOAD_TIME = 7f;
    public float DESTR_2_SHIELD_RELOAD_TIME = 6f;
    public float CRUIS_2_SHIELD_RELOAD_TIME = 5f;
    public float DESTR_1_SHIELD_RELOAD_TIME = 4f;
    public float CRUIS_1_SHIELD_RELOAD_TIME = 3f;

    //time dilation range (time freeze)
    public float DESTR_4_TIME_DILATION = 0.65f;
    public float CRUIS_4_TIME_DILATION = 0.6f; //less is better
    public float DESTR_3_TIME_DILATION = 0.45f;
    public float CRUIS_3_TIME_DILATION = 0.45f;
    public float DESTR_2_TIME_DILATION = 0.3f;
    public float CRUIS_2_TIME_DILATION = 0.3f;
    public float DESTR_1_TIME_DILATION = 0.2f;
    public float CRUIS_1_TIME_DILATION = 0.1f;

    //gun properties
    public float SINGLE_ROTATION_SPEED = 20f;
    public float DUAL_ROTATION_SPEED = 25f;
    public float TRIPLE_ROTATION_SPEED = 35f;

    public float SINGLE_ROTATION_ANGLE = 11f;
    public float DUAL_ROTATION_ANGLE = 13f;
    public float TRIPLE_ROTATION_ANGLE = 15f;

    public float SINGLE_REPEAT_TIME = 0.07f;
    public float DUAL_REPEAT_TIME = 0.06f;
    public float TRIPLE_REPEAT_TIME = 0.06f;

    public int GUN_LIFE_SINGLE = 11;
    public int GUN_LIFE_DUAL = 13;
    public int GUN_LIFE_TRIPLE = 15;

    //DEFENCE SCENE CONSTANTS
    public int GUN_HARM_SINGLE = 2;
    public int GUN_HARM_DUAL = 3;
    public int GUN_HARM_TRIPLE = 4;

    public int GUN_HP_SINGLE = 30;
    public int GUN_HP_DUAL = 40;
    public int GUN_HP_TRIPLE = 50;

    public int SHIP_HP_4_DEF = 15;
    public int SHIP_HP_3_DEF = 20;
    public int SHIP_HP_2_DEF = 30;
    public int SHIP_HP_1_DEF = 45;
    public int SHIP_HP_FED_DEF = 35;

    public float DESTR_4_DEF_ATTACK_FORCE = 0.06f; //0.027f;
    public float DESTR_3_DEF_ATTACK_FORCE = 0.07f;
    public float DESTR_2_DEF_ATTACK_FORCE = 0.1f;
    public float DESTR_1_DEF_ATTACK_FORCE = 0.15f;

    public float DESTR_4_DEF_MOVE_SPEED = 0.04f; //0.04f;
    public float DESTR_3_DEF_MOVE_SPEED = 0.05f;//0.05f;
    public float DESTR_2_DEF_MOVE_SPEED = 0.06f;//0.06f;
    public float DESTR_1_DEF_MOVE_SPEED = 0.07f;//0.07f;

    public float DEF_DESTR_4_ATTACK_TIME = 9; //less is better
    public float DEF_DESTR_3_ATTACK_TIME = 8;
    public float DEF_DESTR_2_ATTACK_TIME = 6f;
    public float DEF_DESTR_1_ATTACK_TIME = 4f;



    //JOURNEY SCENE 
    //these are values to asses the power of fleet of any object in game
    public int Destr4Index = 1;
    public int Destr3Index = 2;
    public int Destr2Index = 3;
    public int Destr1Index = 4;
    public int Cruis4Index = 2;
    public int Cruis3Index = 3;
    public int Cruis2Index = 5;
    public int Cruis1Index = 7;
    public int Gun3Index = 7;
    public int Gun2Index = 5;
    public int Gun1Index = 3;

    //this constant is necessary to limit the production amount of stations and to limit the boarding amount to cruiser 
    //this amount equals to the spaces count on battle field which is 45. Cruisr 1 and 2 class takes 3 places (because of maneuver features)
    //figthers are not taken into account and has their own limits
    public int shipsPreLimit = 42; //this one is used to launche the last batch of ships production
    public int shipsLimit = 45;
    public int fightersLimit = 20;

    //booster basis count to gain depending on dimension
    //public int boostrGainDark = 20;
    //public int boostrGainBlue = 35;
    //public int boostrGainRed = 55;

    //energy basis count to gain depending on dimension (from destroyed asteroids)
    public int energyGainDark = 7;
    public int energyGainBlue = 13;
    public int energyGainRed = 18;

    public int energyBallEnergyDark = 250;
    public int energyBallEnergyBlue = 350;
    public int energyBallEnergyRed = 550;

    //booster reduce rate depending on station type 
    //public float boostReduce0 = 0.02f;
    //public float boostReduce1 = 0.03f;
    //public float boostReduce2 = 0.04f;
    //public float boostReduce3 = 0.06f; 

    //booster multiplyer rate depending on station type. The one that speeds up the production 
    //public float boostProduction0 = 4f;
    //public float boostProduction1 = 5f;
    //public float boostProduction2 = 6f;
    //public float boostProduction3 = 7f;

    //energy reduce or increase after the battle in dependence who win or lose
    public int enrgyGainLoseDark = 100;
    public int enrgyGainLoseBlue = 150;
    public int enrgyGainLoseRed = 200;
    public int enrgyGainLoseDarkWithAds = 135;
    public int enrgyGainLoseBlueWithAds = 185;
    public int enrgyGainLoseRedWithAds = 240;
    //public int boosterGainLoseDark = 15;
    //public int boosterGainLoseBlue = 25;
    //public int boosterGainLoseRed = 40;
    //public int boosterGainLoseDarkWithAds = 22;
    //public int boosterGainLoseBlueWithAds = 32;
    //public int boosterGainLoseRedWithAds = 50;


    public int energyOfStationDark = 65; //270;
    public int energyOfStationBlue = 105; //380;
    public int energyOfStationRed = 155;//450;


    //public int enrgyGainLoseSimpleBattleWithFleet = 35;

    //energy level that is necessary to upgrade station
    public int enrgy0to1Upgrd = 80;
    public int enrgy1to2Upgrd = 150;
    public int enrgy2to3Upgrd = 210;

    //rate of reduce the energy when hit by energon bullet
    //public int energonBullReduce = 10;

    public int asteroid1Reduce = 35;
    public int asteroid2Reduce = 25;
    public int asteroid3Reduce = 25;
    public int asteroid4Reduce = 15;
    public int asteroid1HP = 2;
    public int asteroid2HP = 1;
    public int asteroid3HP = 2;
    public int asteroid4HP = 1;
    //this properties are used to change the maneuvre changes of energon ship after someone started to gather energy from it (is changed from PlayerEnrgyGather class
    // and used by EnergonMngr class). Here are the default values
    public float energonDirChangeStartTime = 8;
    public float energonDirChangeEndTime = 10;

    //following values set the energon moving speed and energon rotation lerp according to dimension (or their type)
    public float energonMovingSpeed4 = 0.05f;//1f;
    public float energonMovingSpeed3 = 0.07f;//1.4f;
    public float energonMovingSpeed2 = 0.09f;//1.8f;
    public float energonMovingSpeed1 = 0.11f;//2.2f;

    public float guardMovingSpeed4 = 0.5f;//1f;
    public float guardMovingSpeed3 = 0.6f;//1.4f;
    public float guardMovingSpeed2 = 0.7f;//1.8f;
    public float guardMovingSpeed1 = 0.9f;//2.2f;

    public float energonEnergyCapacityd4 = 500f;//1f;
    public float energonEnergyCapacityd3 = 800f;//1.4f;
    public float energonEnergyCapacityd2 = 1300f;//1.8f;
    public float energonEnergyCapacityd1 = 1800f;//2.2f;


    //public float guardChaseInvokeTime4 = 25f;
    //public float guardChaseInvokeTime3 = 22f;
    //public float guardChaseInvokeTime2 = 18f;
    //public float guardChaseInvokeTime1 = 15f;

    public float guardChaseTimeMiddle4 = 5f;
    public float guardChaseTimeMiddle3 = 7f;
    public float guardChaseTimeMiddle2 = 9f;
    public float guardChaseTimeMiddle1 = 11f;

    public float guardChaseSpeed4 = 5.2f;
    public float guardChaseSpeed3 = 6f;
    public float guardChaseSpeed2 = 7f;
    public float guardChaseSpeed1 = 8f;

    public float energonRotationLerp4 = 0.008f;
    public float energonRotationLerp3 = 0.01f;
    public float energonRotationLerp2 = 0.012f;
    public float energonRotationLerp1 = 0.015f;

    //public float energonShotSpeed4 = 4.5f;
    //public float energonShotSpeed3 = 3.8f;
    //public float energonShotSpeed2 = 3.3f;
    //public float energonShotSpeed1 = 2.8f;

    //public float energonShotBullSpeed4 = 20f;
    //public float energonShotBullSpeed3 = 25f;
    //public float energonShotBullSpeed2 = 30f;
    //public float energonShotBullSpeed1 = 37f;

    //public float playerJourShotBullSpeed4 = 33f;
    //public float playerJourShotBullSpeed3 = 36f;
    //public float playerJourShotBullSpeed2 = 40f;
    //public float playerJourShotBullSpeed1 = 45f;

    //following variables determine the necessary energy to produce a ships by their types
    public float C1ProdEnergy = 80f;
    public float C2ProdEnergy = 60f;
    public float C3ProdEnergy = 40f;
    public float C4ProdEnergy = 25f;
    public float D1ProdEnergy = 58f;
    public float D1PProdEnergy = 55f;
    public float D2ProdEnergy = 45f;
    public float D2PProdEnergy = 40f;
    public float D3ProdEnergy = 20f;
    public float D4ProdEnergy = 10f;
    public float G1ProdEnergy = 40f;
    public float G2ProdEnergy = 50f;
    public float G3ProdEnergy = 60f;
    public float GMProdEnergy = 25f;

    //higher the next upgrade level less the step of time to upgrade so more time consume to upgrade the station. Used only for Player stations
    public float time0to1Upgrd = 0.0006f; //0.0003f;
    public float time1to2Upgrd = 0.0004f;//0.0001f;
    public float time2to3Upgrd = 0.0002f;//0.00005f;

    //higher the next upgrade level more the step of time to upgrade so more time consume to upgrade the station Used only for CPU stations
    public float time0to1UpgrdCPU = 40f;
    public float time1to2UpgrdCPU = 70f;
    public float time2to3UpgrdCPU = 110f;
    public float standartTimeBeforeUpgrdProcessStartCPU = 150f; //this one is used for waiting a standart amount of time before start of any upgrade

    //public float timeCreateCruiser = 90f;//65f;
    //public float timeCreateDestr = 75f;//35f;

    //time step to fill the station creating bar
    public float timeToCreateStation = 0.01f;//0.005f;

    //this one is used to count a step btw the CPU station next fleet increases
    //public float timeOfCPUStationStepEasy = 165f; //to make 165
    //public float timeOfCPUStationStepMiddle = 155f; //to make 165
    //public float timeOfCPUStationStepHard = 145f; //to make 165

    //these determines randome range time to CPU station to launche a new cruiser
    public float timeOfCPUCruisLauncheMinEasy = 315;//215f;
    public float timeOfCPUCruisLauncheMaxEasy = 355;//255f;

    public float timeOfCPUCruisLauncheMinMiddle = 295f;//215;
    public float timeOfCPUCruisLauncheMaxMiddle = 325f;//275f;

    public float timeOfCPUCruisLauncheMinHard = 275f;//215;
    public float timeOfCPUCruisLauncheMaxHard = 295f;//275f;

    //these determines randome range time to Guard station to launche a new cruiser
    public float timeOfGuardCruisLauncheEasy = 1050f;//650;

    public float timeOfGuardCruisLauncheMiddle = 950f;//500f;

    public float timeOfGuardCruisLauncheHard = 850f;//750f; //increased the time to make the level mor hursh, so CPU players are able to evolve in full power

    //the rate to reduce the energy while creating a station from zero or to capturing the enemy station
    public float energyReduceNoTo0 = 0.2f; //0.1f;
    public float energyReduce1ToNo = 0.3f;//0.12f;
    public float energyReduce2ToNo = 0.4f;//0.14f;
    public float energyReduce3ToNo = 0.5f;//0.16f;

    //these variables are used to reduce or increase the energy of player while intake it from enerfon or lose it to Guard
    public float energyReduce0 = 0.03f; //0.1f;
    public float energyReduce1 = 0.05f;//0.12f;
    public float energyReduce2 = 0.07f;//0.14f;
    public float energyReduce3 = 0.11f;//0.16f;


    public float energyGetWithCruiser4 = 0.3f; //0.1f;
    public float energyGetWithCruiser3 = 0.4f;//0.12f;
    public float energyGetWithCruiser2 = 0.5f;//0.14f;
    public float energyGetWithCruiser1 = 0.6f;//0.16f;


    //public int hitsBeforePlayerCruiserIsParalized = 3;//0.16f;

    //public float energyGain0 = 0.3f; //0.1f;
    //public float energyGain1 = 0.5f;//0.12f;
    //public float energyGain2 = 0.7f;//0.14f;
    //public float energyGain3 = 0.9f;//0.16f;

    //those are the ship types moving speeds, they are adjusted to be relevant to player's ships moving speeds (though lower than player's ships moving speeds)
    public float CPUCruis4Speed = 0.09f;
    public float CPUCruis3Speed = 0.14f;
    public float CPUCruis2Speed = 0.16f;
    public float CPUCruis1Speed = 0.19f;

    //this value is counted in experemental way, it is the amount of energy that takes creating a station in empty place
    public float energyEmptyToStation = 120;
    public float energyToAttckStations = 30;

    //public float Cruis4ProductTimeStation0 = 0.0008f;//0.0001f;
    //public float Destr4ProductTimeStation0 = 0.001f;//0.01f;//0.0003f;
    public float EnergyProductTimeStation0 = 0.001f;//0.001f;

    //public float Cruis4ProductTimeStation1 = 0.001f;//0.01f;//0.0003f;
    //public float Cruis3ProductTimeStation1 = 0.0008f;//0.01f;;//0.0001f;
    //public float Destr4ProductTimeStation1 = 0.002f;//0.01f;//0.0006f;
    //public float Destr3ProductTimeStation1 = 0.001f;//0.01f;//0.0003f;
    //public float Gun1ProductTimeStation1 = 0.0006f;//0.01f;//0.00005f;
    //public float MiniGunProductTimeStation1 = 0.002f;
    public float EnergyProductTimeStation1 = 0.003f;//0.0025f;

    //public float Cruis4ProductTimeStation2 = 0.0015f;//0.0005f;
    //public float Cruis3ProductTimeStation2 = 0.001f;//0.0003f;
    //public float Cruis2ProductTimeStation2 = 0.0008f;//0.0001f;
    //public float Destr4ProductTimeStation2 = 0.003f;//0.0009f;
    //public float Destr3ProductTimeStation2 = 0.002f;//0.0006f;
    //public float Destr2ProductTimeStation2 = 0.001f;//0.00025f;
    //public float Destr2ParProductTimeStation2 = 0.001f;//0.0002f;
    //public float Gun1ProductTimeStation2 = 0.0008f;//0.0001f;
    //public float Gun2ProductTimeStation2 = 0.0006f;//0.00005f;
    //public float MiniGunProductTimeStation2 = 0.003f;//0.0001f;
    public float EnergyProductTimeStation2 = 0.004f;//0.004f;

    //public float Cruis4ProductTimeStation3 = 0.0025f;//0.0007f;
    //public float Cruis3ProductTimeStation3 = 0.0015f;//0.0005f;
    //public float Cruis2ProductTimeStation3 = 0.001f;//0.0002f;
    //public float Cruis1ProductTimeStation3 = 0.0008f;//0.0001f;
    //public float Destr4ProductTimeStation3 = 0.007f;//0.0012f;
    //public float Destr3ProductTimeStation3 = 0.003f;//0.0009f;
    //public float Destr2ProductTimeStation3 = 0.002f;//0.00055f;
    //public float Destr2ParProductTimeStation3 = 0.002f;//0.0005f;
    //public float Destr1ProductTimeStation3 = 0.001f;//0.0002f;
    //public float Destr1ParProductTimeStation3 = 0.001f;//0.00025f;
    //public float Gun1ProductTimeStation3 = 0.001f;//0.0005f;
    //public float Gun2ProductTimeStation3 = 0.0008f;//0.0001f;
    //public float Gun3ProductTimeStation3 = 0.0006f;//0.00005f;
    //public float MiniGunProductTimeStation3 = 0.005f;// 0.0002f;
    public float EnergyProductTimeStation3 = 0.007f;//0.007f;

    public float rewardedEnergyDefence = 35;

    public float shareOfCPUStationFleetToLaunchCruiser = 0.3f;

    public float shareOfCPUStationFleetToLaunchCruiserToAttackStation = 0.8f;

    //TEXT PROPERTIES
    //energy
    public string getNoEnergy()
    {
        if (Lists.isEnglish) return "no Energy";
        else if (Lists.isRussian) return "нет Энергии";
        else if (Lists.isChinees) return "没有能量";
        else if (Lists.isGerman) return "keine Energie";
        else return "sin energía"; //isSpanish
    }

    //energy
    public string getEnergy()
    {
        if (Lists.isEnglish) return "Energy";
        else if (Lists.isRussian) return "Энергия";
        else if (Lists.isChinees) return "能源";
        else if (Lists.isGerman) return "Energie";
        else return "Energía"; //isSpanish
    }

    public string getNewGame()
    {
        if (Lists.isEnglish) return "New Game";
        else if (Lists.isRussian) return "Новая игра";
        else if (Lists.isChinees) return "新游戏";
        else if (Lists.isGerman) return "Neues Spiel";
        else return "Nuevo Juego"; //isSpanish
    }

    public string getContinue()
    {
        if (Lists.isEnglish) return "Continue";
        else if (Lists.isRussian) return "Продолжить";
        else if (Lists.isChinees) return "继续";
        else if (Lists.isGerman) return "Fortsetzen";
        else return "Continuar"; //isSpanish
    }
    public string getPurchase()
    {
        if (Lists.isEnglish) return "Buy now";
        else if (Lists.isRussian) return "Купить";
        else if (Lists.isChinees) return "买";
        else if (Lists.isGerman) return "Kaufen";
        else return "comprar"; //isSpanish
    }

    public string getBought()
    {
        if (Lists.isEnglish) return "Purchased";
        else if (Lists.isRussian) return "Куплено";
        else if (Lists.isChinees) return "已购买";
        else if (Lists.isGerman) return "Gekauft";
        else return "Comprado"; //isSpanish
    }

    public string getEasy()
    {
        if (Lists.isEnglish) return "Easy";
        else if (Lists.isRussian) return "Легкий";
        else if (Lists.isChinees) return "简单";
        else if (Lists.isGerman) return "einfach";
        else return "fácil"; //isSpanish
    }

    public string getNormal()
    {
        if (Lists.isEnglish) return "Normal";
        else if (Lists.isRussian) return "Средний";
        else if (Lists.isChinees) return "正常";
        else if (Lists.isGerman) return "normal";
        else return "normal"; //isSpanish
    }

    public string getHard()
    {
        if (Lists.isEnglish) return "Hard";
        else if (Lists.isRussian) return "Сложный";
        else if (Lists.isChinees) return "硬";
        else if (Lists.isGerman) return "Schwer";
        else return "dificil"; //isSpanish
    }

    public string getGraphics()
    {
        if (Lists.isEnglish) return "Graphics";
        else if (Lists.isRussian) return "Графика";
        else if (Lists.isChinees) return "图形";
        else if (Lists.isGerman) return "Grafik";
        else return "Gráficos"; //isSpanish
    }

    public string getMusic()
    {
        if (Lists.isEnglish) return "Music";
        else if (Lists.isRussian) return "Музыка";
        else if (Lists.isChinees) return "音乐";
        else if (Lists.isGerman) return "Musik";
        else return "Música"; //isSpanish
    }

    public string getSounds()
    {
        if (Lists.isEnglish) return "sounds";
        else if (Lists.isRussian) return "Звуки";
        else if (Lists.isChinees) return "声音";
        else if (Lists.isGerman) return "Geräusche";
        else return "Sonidos"; //isSpanish
    }

    //public string getNextAttackOf()
    //{
    //    if (Lists.isEnglish) return "next move of:";
    //    else if (Lists.isRussian) return "Следующий ход:";
    //    else if (Lists.isChinees) return "下一步行动:";
    //    else if (Lists.isGerman) return "nächster Zug:";
    //    else return "siguiente movimiento:"; //isSpanish
    //}

    public string getGuardAttack()
    {
        if (Lists.isEnglish) return "Next attack of:";
        else if (Lists.isRussian) return "Следующая атака:";
        else if (Lists.isChinees) return "下一次攻击:";
        else if (Lists.isGerman) return "nächster Angriff:";
        else return "próximo ataque:"; //isSpanish
    }

    public string getPlayerColor()
    {
        if (Lists.isEnglish) return "Player Color";
        else if (Lists.isRussian) return "Цвет Игрока";
        else if (Lists.isChinees) return "播放器颜色";
        else if (Lists.isGerman) return "Spielerfarbe";
        else return "Color del jugador"; //isSpanish
    }

    //station fleet
    public string getStatFleet()
    {
        if (Lists.isEnglish) return "Station fleet";
        else if (Lists.isRussian) return "Флот станции";
        else if (Lists.isChinees) return "车站车队";
        else if (Lists.isGerman) return "Stationsflotte";
        else return "Flota de la estación"; //isSpanish
    }

    public string getProduction()
    {
        if (Lists.isEnglish) return "Production";
        else if (Lists.isRussian) return "Производство";
        else if (Lists.isChinees) return "生产";
        else if (Lists.isGerman) return "Produktion";
        else return "producción"; //isSpanish
    }

    public string getSpacePort()
    {
        if (Lists.isEnglish) return "Cruiser fleet";
        else if (Lists.isRussian) return "Флот крейсера";
        else if (Lists.isChinees) return "巡洋舰队";
        else if (Lists.isGerman) return "Kreuzerflotte";
        else return "Flota de cruceros"; //isSpanish
    }
    //public string getCurrentProduction()
    //{
    //    if (Lists.isEnglish) return "current";
    //    else if (Lists.isRussian) return "текущее";
    //    else if (Lists.isChinees) return "当前";
    //    else if (Lists.isGerman) return "aktuelle";
    //    else return "actual"; //isSpanish
    //}

    //buy
    public string getBuy()
    {
        if (Lists.isEnglish) return "Buy";
        else if (Lists.isRussian) return "Купить";
        else if (Lists.isChinees) return "购买";
        else if (Lists.isGerman) return "kaufen";
        else return "Сompra"; //isSpanish
    }

    ////defence
    public string getDefence()
    {
        if (Lists.isEnglish) return "Defence";
        else if (Lists.isRussian) return "Защита";
        else if (Lists.isChinees) return "防御";
        else if (Lists.isGerman) return "verteidigen";
        else return "defensa"; //isSpanish
    }

    //fight
    public string getFight()
    {
        if (Lists.isEnglish) return "Fight";
        else if (Lists.isRussian) return "Атака";
        else if (Lists.isChinees) return "斗争";
        else if (Lists.isGerman) return "Kampf";
        else return "lucha"; //isSpanish
    }

    //give up
    public string getGiveUp()
    {
        if (Lists.isEnglish) return "Give up";
        else if (Lists.isRussian) return "Сдаться";
        else if (Lists.isChinees) return "放弃";
        else if (Lists.isGerman) return "Gib auf";
        else return "rendirse"; //isSpanish
    }

    //your fleet
    public string getYourFleet()
    {
        if (Lists.isEnglish) return "your fleet";
        else if (Lists.isRussian) return "ваш флот";
        else if (Lists.isChinees) return "你的舰队";
        else if (Lists.isGerman) return "Ihre Flotte";
        else return "tu flota"; //isSpanish
    }

    //your fleet
    public string getYourStations()
    {
        if (Lists.isEnglish) return "your stations";
        else if (Lists.isRussian) return "ваши станции";
        else if (Lists.isChinees) return "您的车站";
        else if (Lists.isGerman) return "Ihre Stationen";
        else return "tus estaciones"; //isSpanish
    }

    //their fleet text
    public string getTheirFleet()
    {
        if (Lists.isEnglish) return "their fleet";
        else if (Lists.isRussian) return "их флот";
        else if (Lists.isChinees) return "他们的舰队";
        else if (Lists.isGerman) return "Schiffsflotte";
        else return "su flota"; //isSpanish
    }

    //win button text title
    public string getYouWinTxt()
    {
        if (Lists.isEnglish) return "You win";
        else if (Lists.isRussian) return "Вы победили";
        else if (Lists.isChinees) return "你赢了";
        else if (Lists.isGerman) return "Du gewinnst";
        else return "Tú ganas"; //isSpanish
    }
    //lost button text title
    public string getYouLostTxt()
    {
        if (Lists.isEnglish) return "defeat";
        else if (Lists.isRussian) return "Поражение";
        else if (Lists.isChinees) return "打败";
        else if (Lists.isGerman) return "Niederlage";
        else return "derrota"; //isSpanish
    }
    //your guns title
    public string getYouGunsTxt()
    {
        if (Lists.isEnglish) return "your guns";
        else if (Lists.isRussian) return "Ваши орудия";
        else if (Lists.isChinees) return "你的枪";
        else if (Lists.isGerman) return "deine Waffen";
        else return "tus armas"; //isSpanish
    }
    //to battle title
    public string getToBattleTxt()
    {
        if (Lists.isEnglish) return "to battle";
        else if (Lists.isRussian) return "к битве";
        else if (Lists.isChinees) return "去打仗";
        else if (Lists.isGerman) return "bekämpfen";
        else return "a la batalla"; //isSpanish
    }
    //no cruisers to continue title
    public string getNoCruisersTxt()
    {
        if (Lists.isEnglish) return "no cruisers to continue";
        else if (Lists.isRussian) return "нет крейсеров для продолжения";
        else if (Lists.isChinees) return "没有巡洋舰继续";
        else if (Lists.isGerman) return "Keine Kreuzer weiter";
        else return "no hay cruceros para continuar"; //isSpanish
    }

    //watch video title
    public string getWatchVideoTxt()
    {
        if (Lists.isEnglish) return "watch video";
        else if (Lists.isRussian) return "смотреть видео";
        else if (Lists.isChinees) return "看视频";
        else if (Lists.isGerman) return "Schau Video";
        else return "ver video"; //isSpanish
    }

    //buy cruiser title
    public string getBuyCruiserTxt()
    {
        if (Lists.isEnglish) return "Buy Cruiser now";
        else if (Lists.isRussian) return "Купить крейсер";
        else if (Lists.isChinees) return "购买巡洋舰";
        else if (Lists.isGerman) return "Cruiser kaufen";
        else return "Comprar crucero"; //isSpanish
    }
    //destroyed cruisers title
    public string getDestroyedCruisersTxt()
    {
        if (Lists.isEnglish) return "destroyed cruisers";
        else if (Lists.isRussian) return "уничтоженные крейсеры";
        else if (Lists.isChinees) return "被摧毁的巡洋舰";
        else if (Lists.isGerman) return "zerstörte Kreuzer";
        else return "cruceros destruidos"; //isSpanish
    }
    //no mini gun title
    public string getNoMiniGunTxt()
    {
        if (Lists.isEnglish) return "no mini guns";
        else if (Lists.isRussian) return "нет мини пулеметов";
        else if (Lists.isChinees) return "没有迷你枪";
        else if (Lists.isGerman) return "keine Minigewehre";
        else return "sin mini pistolas"; //isSpanish
    }

    //teleport
    public string getTeleportationTxt()
    {
        if (Lists.isEnglish) return "Teleportation";
        else if (Lists.isRussian) return "телепортация";
        else if (Lists.isChinees) return "隐形传送";
        else if (Lists.isGerman) return "Teleportation";
        else return "teletransportación"; //isSpanish
    }


    public string getVulnerableTxt()
    {
        if (Lists.isEnglish) return "station is vulnerable";
        else if (Lists.isRussian) return "станция уязвима";
        else if (Lists.isChinees) return "车站脆弱";
        else if (Lists.isGerman) return "Station ist anfällig";
        else return "la estación es vulnerable"; //isSpanish
    }

    public string getTwoGunWarning()
    {
        if (Lists.isEnglish) return "No more than 2 guns";
        else if (Lists.isRussian) return "не более 2-х пулеметов";
        else if (Lists.isChinees) return "不超过2支枪";
        else if (Lists.isGerman) return "nicht mehr als 2 Kanonen";
        else return "no más de 2 pistolas"; //isSpanish
    }
    public string getNoCruiserWarning()
    {
        if (Lists.isEnglish) return "Place at least 1 cruiser on the battlefield";
        else if (Lists.isRussian) return "разместите хотя бы 1 крейсер на поле битвы";
        else if (Lists.isChinees) return "在战场上放置至少1艘巡洋舰";
        else if (Lists.isGerman) return "Platziere mindestens 1 Kreuzer auf dem Schlachtfeld";
        else return "coloca al menos 1 crucero en el campo de batalla"; //isSpanish
    }

    public string getBattleFieldScheme()
    {
        if (Lists.isEnglish) return "Battlefield Scheme";
        else if (Lists.isRussian) return "Схема поля битвы";
        else if (Lists.isChinees) return "战场模式";
        else if (Lists.isGerman) return "Schlachtfeldschema";
        else return "Esquema de campo de batalla"; //isSpanish
    }

    public string getLoadingText()
    {
        if (Lists.isEnglish) return "loading";
        else if (Lists.isRussian) return "загрузка";
        else if (Lists.isChinees) return "装货";
        else if (Lists.isGerman) return "Laden";
        else return "cargando"; //isSpanish
    }

    public string getC1Mega()
    {
        if (Lists.isEnglish) return "mega laser";
        else if (Lists.isRussian) return "мега лазер";
        else if (Lists.isChinees) return "巨型激光";
        else if (Lists.isGerman) return "Mega-Laser";
        else return "mega láser"; //isSpanish
    }
    public string getC2Mega()
    {
        if (Lists.isEnglish) return "mega paralyzer";
        else if (Lists.isRussian) return "мега парализатор";
        else if (Lists.isChinees) return "大型瘫痪者";
        else if (Lists.isGerman) return "Mega-Lähmer";
        else return "Mega paralizador"; //isSpanish
    }
    public string getC3Mega()
    {
        if (Lists.isEnglish) return "fleet shield";
        else if (Lists.isRussian) return "щит для флота";
        else if (Lists.isChinees) return "舰队盾";
        else if (Lists.isGerman) return "Flottenschild";
        else return "escudo de flota"; //isSpanish
    }
    public string getC4Mega()
    {
        if (Lists.isEnglish) return "shock wave";
        else if (Lists.isRussian) return "ударная волна";
        else if (Lists.isChinees) return "激波";
        else if (Lists.isGerman) return "Schockwelle";
        else return "onda de choque"; //isSpanish
    }
    public string getDoubleShot()
    {
        if (Lists.isEnglish) return "double shot";
        else if (Lists.isRussian) return "двойной выстрел";
        else if (Lists.isChinees) return "双重射击";
        else if (Lists.isGerman) return "doppelschuss";
        else return "doble tiro"; //isSpanish
    }
    public string getTripleShot()
    {
        if (Lists.isEnglish) return "triple shot";
        else if (Lists.isRussian) return "тройной выстрел";
        else if (Lists.isChinees) return "三连拍";
        else if (Lists.isGerman) return "Dreifachschuss";
        else return "tiro triple"; //isSpanish
    }

    public string getManeuvers()
    {
        if (Lists.isEnglish) return "maneuvers";
        else if (Lists.isRussian) return "маневры";
        else if (Lists.isChinees) return "演习";
        else if (Lists.isGerman) return "Manöver";
        else return "maniobras"; //isSpanish
    }
    public string getParalizerFeature()
    {
        if (Lists.isEnglish) return "paralyzer";
        else if (Lists.isRussian) return "парализатор";
        else if (Lists.isChinees) return "瘫痪者";
        else if (Lists.isGerman) return "Lähmer";
        else return "paralizador"; //isSpanish
    }
    public string getForceShield()
    {
        if (Lists.isEnglish) return "force shield";
        else if (Lists.isRussian) return "силовой щит";
        else if (Lists.isChinees) return "力盾";
        else if (Lists.isGerman) return "Kraftschild";
        else return "escudo de fuerza"; //isSpanish
    }
    public string getCruiserWord()
    {
        if (Lists.isEnglish) return "cruiser";
        else if (Lists.isRussian) return "крейсер";
        else if (Lists.isChinees) return "类";
        else if (Lists.isGerman) return "Kreuzer";
        else return "crucero"; //isSpanish
    }
    public string getDestrWord()
    {
        if (Lists.isEnglish) return "destroyer";
        else if (Lists.isRussian) return "эсминец";
        else if (Lists.isChinees) return "驱逐舰";
        else if (Lists.isGerman) return "Zerstörer";
        else return "destructor"; //isSpanish
    }

    public string getTurretWord()
    {
        if (Lists.isEnglish) return "turret";
        else if (Lists.isRussian) return "турель";
        else if (Lists.isChinees) return "炮塔";
        else if (Lists.isGerman) return "Turm";
        else return "torreta"; //isSpanish
    }
    public string getClassWord()
    {
        if (Lists.isEnglish) return "class";
        else if (Lists.isRussian) return "класс";
        else if (Lists.isChinees) return "类";
        else if (Lists.isGerman) return "Klasse";
        else return "clase"; //isSpanish
    }
    public string getMegaShotWord()
    {
        if (Lists.isEnglish) return "mega attack";
        else if (Lists.isRussian) return "мега атака";
        else if (Lists.isChinees) return "大规模攻击";
        else if (Lists.isGerman) return "Mega-Angriff";
        else return "mega ataque"; //isSpanish
    }
    public string getFirePowerWord()
    {
        if (Lists.isEnglish) return "firepower";
        else if (Lists.isRussian) return "огневая мощь";
        else if (Lists.isChinees) return "火力";
        else if (Lists.isGerman) return "Feuerkraft";
        else return "potencia de fuego"; //isSpanish
    }
    public string getFeaturesWord()
    {
        if (Lists.isEnglish) return "features";
        else if (Lists.isRussian) return "специфика";
        else if (Lists.isChinees) return "特征";
        else if (Lists.isGerman) return "Eigenschaften";
        else return "caracteristicas"; //isSpanish
    }
    public string getCostWord()
    {
        if (Lists.isEnglish) return "cost";
        else if (Lists.isRussian) return "стоимость";
        else if (Lists.isChinees) return "成本";
        else if (Lists.isGerman) return "Kosten";
        else return "costo"; //isSpanish
    }

    #region station panel UI properties

    public string getOutOfLimitsWarning()
    {
        if (Lists.isEnglish) return "limit reached";
        else if (Lists.isRussian) return "достигнут предел";
        else if (Lists.isChinees) return "达到限制";
        else if (Lists.isGerman) return "Limit erreicht";
        else return "límite alcanzado"; //isSpanish
    }

    public string getYouHaveNoCruiserWarning()
    {
        if (Lists.isEnglish) return "You have no cruiser to attack";
        else if (Lists.isRussian) return "У Вас нет крейсеров для атаки";
        else if (Lists.isChinees) return "你没有巡洋舰可以攻击";
        else if (Lists.isGerman) return "Sie haben keinen Kreuzer zum Angriff";
        else return "No tienes cruceros para atacar"; //isSpanish
    }
    public string getTheyHaveNoCruiserWarning()
    {
        if (Lists.isEnglish) return "They have no cruiser to attack";
        else if (Lists.isRussian) return "У них нет крейсеров для атаки";
        else if (Lists.isChinees) return "他们没有巡洋舰可以攻击";
        else if (Lists.isGerman) return "Sie haben keine Kreuzer zum Angriff";
        else return "No tienen cruceros para atacar"; //isSpanish
    }
    public string getUpGrade()
    {
        if (Lists.isEnglish) return "Upgrade";
        else if (Lists.isRussian) return "Апгрейд";
        else if (Lists.isChinees) return "升级";
        else if (Lists.isGerman) return "nachrüsten";
        else return "mejorar"; //isSpanish
    }

    public string getUnlockAll()
    {
        if (Lists.isEnglish) return "unlock all levels";
        else if (Lists.isRussian) return "разблокировать все уровни";
        else if (Lists.isChinees) return "解锁所有级别";
        else if (Lists.isGerman) return "schalte alle Level frei";
        else return "desbloquear todos los niveles"; //isSpanish
    }

    public string getStartkit()
    {
        if (Lists.isEnglish) return "Start kit";
        else if (Lists.isRussian) return "Стартовый набор";
        else if (Lists.isChinees) return "入门套件";
        else if (Lists.isGerman) return "Starter-Kit";
        else return "Kit de inicio"; //isSpanish
    }

    public string getNoAds()
    {
        if (Lists.isEnglish) return "+no ads";
        else if (Lists.isRussian) return "+без рекламы";
        else if (Lists.isChinees) return "+没有广告";
        else if (Lists.isGerman) return "+Ohne Werbung";
        else return "+Sin publicidad"; //isSpanish
    }

    public string getDeledSavedGame()
    {
        if (Lists.isEnglish) return "delete a saved game";
        else if (Lists.isRussian) return "удалить сохраненную игру";
        else if (Lists.isChinees) return "删除已保存的游戏";
        else if (Lists.isGerman) return "lösche ein gespeichertes Spiel";
        else return "eliminar un juego guardado"; //isSpanish
    }

    public string getGameAim()
    {
        if (Lists.isEnglish) return "Capture all stations!";
        else if (Lists.isRussian) return "Захватите все станции!";
        else if (Lists.isChinees) return "捕获所有电台!";
        else if (Lists.isGerman) return "Erfassen Sie alle Stationen!";
        else return "Captura todas las estaciones!"; //isSpanish
    }


    public string getWhatchHowToPlay()
    {
        if (Lists.isEnglish) return "watch how to play";
        else if (Lists.isRussian) return "посмотреть как играть";
        else if (Lists.isChinees) return "看怎么玩";
        else if (Lists.isGerman) return "schau dir an, wie man spielt";
        else return "mira como jugar"; //isSpanish
    }

    #endregion station panel UI properties
}
