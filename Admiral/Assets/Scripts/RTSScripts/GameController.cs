using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController current;

    [SerializeField]
    private GameObject energon;

    [HideInInspector]
    private GameObject ObjectPulled;
    [HideInInspector]
    private List<GameObject> ObjectPulledList;


    //those materials are used to set a proper color to stations
    private static Color redColorOfStation;
    private static Color greenColorOfStation;
    private static Color blueColorOfStation;
    private static Color yellowColorOfStation;
    private static Color purpleColorOfStation;
    public static List<int> colorsOfPlayers;
    // Start is called before the first frame update


    public GameObject YouWinLoseButton;
    private Text YouWinLoseButtonTxt;
    [SerializeField]
    private Sprite winSprite;
    [SerializeField]
    private Sprite loseSprite;
    private Image YouWinLoseButtonImg;
    [SerializeField]
    private AudioSource commonUISoundDeep;

    private float VERTICAL_START_TOP_TO_SET_STARS_AND_STATIONS = -150;
    private float VERTICAL_STEP_TO_SET_STARS_AND_STATIONS = 50;
    private float HORIZONTAL_STEP_TO_SET_STARS_AND_STATIONS = 43;


    private void Awake()
    {
        current = this;
    }

    void Start()
    {
        redColorOfStation = new Color(2f, 0.3f, 0.3f, 1f);
        greenColorOfStation = new Color(0.5f, 2f, 0.5f, 1f);
        yellowColorOfStation = new Color(3, 2f, 0.5f, 1f);
        purpleColorOfStation = new Color(2f, 0f, 2, 1f);
        blueColorOfStation = new Color(0f, 1.8f, 3f, 1f);


        //0-Red, 1-green, 2-blue, 3-yellow, 4-purple
        colorsOfPlayers = new List<int>();
        //populating the list of rest colors of game players
        //0-Red, 1-green, 2-blue, 3-yellow, 4-purple
        for (int i = 0; i < 5; i++)
        {
            if (CommonProperties.colorOfPlayer != i) colorsOfPlayers.Add(i);
        }

        setTheLevel();

        for (byte i = 0; i < 1; i++)
        {
            StartCoroutine(launchTheEnergon());
        }


        YouWinLoseButtonImg = YouWinLoseButton.GetComponent<Image>();
        YouWinLoseButtonTxt = YouWinLoseButton.transform.GetChild(0).GetComponent<Text>();
    }


    public void LaunchTheEnergon() {
        StartCoroutine(launchTheEnergon());
    }
    public static Color getProperMatColorByIndex(int colorIndex)
    {
        if (colorIndex == 0) return redColorOfStation;
        else if (colorIndex == 1) return greenColorOfStation;
        else if (colorIndex == 2) return blueColorOfStation;
        else if (colorIndex == 3) return yellowColorOfStation;
        else return purpleColorOfStation;
    }

   
    private void setTheLevel()
    {
        int rowsCount = 7;
        int columsCount = 11;
        float YForNextColumn = VERTICAL_START_TOP_TO_SET_STARS_AND_STATIONS;
        float Y = YForNextColumn;
        float XForNextColumn=0;
        float X = XForNextColumn;
        int CPUNumberForStation = 0;

        for (int i = 0; i < columsCount; i++) {
            for (int j = 0; j < rowsCount; j++)
            {
                if (LevelParameters.stationPoints.Contains(new Vector3(X, 0, Y)))
                {
                    launchStation(CPUNumberForStation, new Vector3(X, 0, Y));
                    CPUNumberForStation++;
                }
                else launchStar(new Vector3(X, 0, Y));
                Y += VERTICAL_STEP_TO_SET_STARS_AND_STATIONS;
            }
            if (i == 0 || i % 2 == 0)
            {
                YForNextColumn += VERTICAL_STEP_TO_SET_STARS_AND_STATIONS/2;
                if (i == 0) XForNextColumn += HORIZONTAL_STEP_TO_SET_STARS_AND_STATIONS;
                else XForNextColumn += (XForNextColumn*-2)+HORIZONTAL_STEP_TO_SET_STARS_AND_STATIONS;
                rowsCount--;
            }
            else
            {
                XForNextColumn -= XForNextColumn * 2;
            }
            Y = YForNextColumn;
            X = XForNextColumn;
        }





        //ObjectPulledList = ObjectPullerRTS.current.GetStarPull(3);
        //ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
        //CommonProperties.stars.Add(ObjectPulled.GetComponent<StarController>());
        //ObjectPulled.transform.position = new Vector3(86, 0, -100);
        //ObjectPulled.SetActive(true);

        //ObjectPulledList = ObjectPullerRTS.current.GetStarPull(3);
        //ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
        //CommonProperties.stars.Add(ObjectPulled.GetComponent<StarController>());
        //ObjectPulled.transform.position = new Vector3(86, 0, 100);
        //ObjectPulled.SetActive(true);

        //ObjectPulledList = ObjectPullerRTS.current.GetStarPull(3);
        //ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
        //CommonProperties.stars.Add(ObjectPulled.GetComponent<StarController>());
        //ObjectPulled.transform.position = new Vector3(-86, 0, -100);
        //ObjectPulled.SetActive(true);

        //ObjectPulledList = ObjectPullerRTS.current.GetStarPull(3);
        //ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
        //CommonProperties.stars.Add(ObjectPulled.GetComponent<StarController>());
        //ObjectPulled.transform.position = new Vector3(-172, 0, 50);
        //ObjectPulled.SetActive(true);

        //ObjectPulledList = ObjectPullerRTS.current.GetStarPull(3);
        //ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
        //CommonProperties.stars.Add(ObjectPulled.GetComponent<StarController>());
        //ObjectPulled.transform.position = new Vector3(172, 0, 50);
        //ObjectPulled.SetActive(true); 

        //ObjectPulledList = ObjectPullerRTS.current.GetStarPull(3);
        //ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
        //CommonProperties.stars.Add(ObjectPulled.GetComponent<StarController>());
        //ObjectPulled.transform.position = new Vector3(-172, 0, -50);
        //ObjectPulled.SetActive(true);

        //ObjectPulledList = ObjectPullerRTS.current.GetStarPull(3);
        //ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
        //CommonProperties.stars.Add(ObjectPulled.GetComponent<StarController>());
        //ObjectPulled.transform.position = new Vector3(172, 0, -50);
        //ObjectPulled.SetActive(true);

        //ObjectPulledList = ObjectPullerRTS.current.GetStarPull(3);
        //ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
        //CommonProperties.stars.Add(ObjectPulled.GetComponent<StarController>());
        //ObjectPulled.transform.position = new Vector3(0, 0, -150);
        //ObjectPulled.SetActive(true);

        //ObjectPulledList = ObjectPullerRTS.current.GetStarPull(3);
        //ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
        //CommonProperties.stars.Add(ObjectPulled.GetComponent<StarController>());
        //ObjectPulled.transform.position = new Vector3(0, 0, 150);
        //ObjectPulled.SetActive(true);

        #region stations instatiation
        //TO AUTOMATE WITH FUNCTION
        //StationPlayerRTS station;

        //ObjectPulledList = ObjectPullerRTS.current.GetStationPull(0, 0);
        //ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
        //station = ObjectPulled.GetComponent<StationPlayerRTS>();
        //station.CPUNumber = 0;
        //station.colorOfStationMat = getProperMatColorByIndex(CommonProperties.colorOfPlayer); //setting the next color available on colours list to this CPU station
        //station.setProperStationColor();
        ////station.fleetGatherRadius = 14f;
        ////station.radiusOfShipsRingAroundStation = 6;
        //station.stationCurrentLevel = 0;
        //station.upgradeCounts = 3;
        //station.energyToNextUpgradeOfStation = CommonProperties.enrgy0to1Upgrd;
        //station.stationGunLevel = 0;
        //station.energyToNextUpgradeOfGun = CommonProperties.gun0to1Upgrd;
        //station.GunUpgradeCounts = 0;
        //station.Cruis4 = 0;
        //station.Cruis3 = 0;
        //station.Cruis2 = 0;
        //station.Cruis1 = 0;
        //station.Destr4 = 0;
        //station.Destr3 = 0;
        //station.Destr2 = 0;
        //station.Destr2Par = 0;
        //station.Destr1 = 0;
        //station.Destr1Par = 0;
        //station.energyOfStation = 150; 
        //station.fillingSpeed = CommonProperties.star0FillingReducer; ;
        //station.ShipsAssigned = 0;
        //station.energyToConnection = CommonProperties.Station0EnergyToConnection;
        ////station.energyLoseIfDestroyed = CommonProperties.Station0GroupEnergyLoseIfDestroyed;
        //station.energyToGetFromLine = CommonProperties.Station0EnergyFromLine;
        ////station.energyRequiredToShot = CommonProperties.Station0EnergytoShot;
        //station.energyInscreaseTime = CommonProperties.Station0EnergyProduceTime;
        //station.energyInscreaseTimer = station.energyInscreaseTime;
        //station.energyProdeucePerTime = CommonProperties.Station0EnergyProduceAmount;
        //station.energyPullFromEnergonRate = CommonProperties.Station0EnergyPullRate;
        //station.HPInscreaseTimer = station.energyInscreaseTime;
        ////station.stationShotTime = CommonProperties.Station0ShotTime;
        ////station.stationShotTimer = station.stationShotTime;
        ////station.shotTimerTransformIndex = -6f/station.stationShotTime;
        //station.energonCatchDistance = CommonProperties.Station0EnergonCatchDistance;
        //station.energyLimitOfStation = CommonProperties.Station0EnergyLimit;
        //station.speedOfBullet = CommonProperties.Station0BulletSpeed;
        //station.colorToEnergy = CommonProperties.Station0ColorToEnergyMultiplyer;
        //station.ShipsLimit = CommonProperties.Station0ShipsLimit;
        //station.fillingLine.localPosition = new Vector3(0, 0, 0); //make full life to new station
        //station.ConnectedStations.Clear();
        //station.groupWhereTheStationIs=null;
        //ObjectPulled.transform.position = new Vector3 (86,0,0);
        //station.stationTransform = ObjectPulled.transform;
        //station.stationPosition = station.stationTransform.position;
        //CommonProperties.allStations.Add(station);
        //CommonProperties.playerStations.Add(station);
        //ObjectPulled.SetActive(true);


        //StationCPU stationCPU;
        //ObjectPulledList = ObjectPullerRTS.current.GetStationPull(0, 1);
        //ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
        //stationCPU = ObjectPulled.GetComponent<StationCPU>();
        //stationCPU.CPUNumber = 1;
        ////station.fleetGatherRadius = 14f;
        ////station.radiusOfShipsRingAroundStation = 6;

        //stationCPU.colorOfStationMat = getProperMatColorByIndex(colorsOfPlayers[stationCPU.CPUNumber-1]); //setting the next color available on colours list to this CPU station
        //stationCPU.setProperStationColor();

        //stationCPU.stationCurrentLevel = 0;
        //stationCPU.upgradeCounts = 3;
        //stationCPU.energyToNextUpgradeOfStation = CommonProperties.enrgy0to1Upgrd;
        //stationCPU.stationGunLevel = 0;
        //stationCPU.energyToNextUpgradeOfGun = CommonProperties.gun0to1Upgrd;
        //stationCPU.GunUpgradeCounts = 0;
        //stationCPU.Cruis4 = 0;
        //stationCPU.Cruis3 = 0;
        //stationCPU.Cruis2 = 0;
        //stationCPU.Cruis1 = 0;
        //stationCPU.Destr4 = 0;
        //stationCPU.Destr3 = 0;
        //stationCPU.Destr2 = 0;
        //stationCPU.Destr2Par = 0;
        //stationCPU.Destr1 = 0;
        //stationCPU.Destr1Par = 0;
        //stationCPU.energyOfStation = 200; //40
        //stationCPU.fillingSpeed = CommonProperties.star0FillingReducer; ;
        //stationCPU.ShipsAssigned = 0;
        //stationCPU.energyToConnection = CommonProperties.Station0EnergyToConnection;
        ////stationCPU.energyLoseIfDestroyed = CommonProperties.Station0GroupEnergyLoseIfDestroyed;
        //stationCPU.energyToGetFromLine = CommonProperties.Station0EnergyFromLine;
        ////stationCPU.energyRequiredToShot = CommonProperties.Station0EnergytoShot;
        //stationCPU.energyInscreaseTime = CommonProperties.Station0EnergyProduceTime;
        //stationCPU.energyInscreaseTimer = stationCPU.energyInscreaseTime;
        //stationCPU.HPInscreaseTimer = stationCPU.energyInscreaseTime;
        ////stationCPU.stationShotTime = CommonProperties.Station0ShotTime;
        //stationCPU.energyPullFromEnergonRate = CommonProperties.Station0EnergyPullRate;
        ////stationCPU.stationShotTimer = stationCPU.stationShotTime;
        //stationCPU.energonCatchDistance = CommonProperties.Station0EnergonCatchDistance;
        //stationCPU.energyLimitOfStation = CommonProperties.Station0EnergyLimit;
        //stationCPU.energyProdeucePerTime = CommonProperties.Station0EnergyProduceAmountCPU;
        //stationCPU.speedOfBullet = CommonProperties.Station0BulletSpeed;
        //stationCPU.colorToEnergy = CommonProperties.Station0ColorToEnergyMultiplyer;
        //stationCPU.ShipsLimit = CommonProperties.Station0ShipsLimit;
        //stationCPU.fillingLine.localPosition = new Vector3(0, 0, 0); //make full life to new station
        //stationCPU.ConnectedStations.Clear();
        //stationCPU.groupWhereTheStationIs = null;
        //ObjectPulled.transform.position = new Vector3(0, 0, 50);
        //stationCPU.stationTransform = ObjectPulled.transform;
        //stationCPU.stationPosition = stationCPU.stationTransform.position;
        //CommonProperties.allStations.Add(stationCPU);
        //CommonProperties.CPUStations.Add(stationCPU);
        //CommonProperties.CPUStationsDictionary[stationCPU.CPUNumber - 1].Add(stationCPU);
        //ObjectPulled.SetActive(true);

        //stationCPU = null;

        //ObjectPulledList = ObjectPullerRTS.current.GetStationPull(0, 1);
        //ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
        //stationCPU = ObjectPulled.GetComponent<StationCPU>();
        //stationCPU.CPUNumber = 2;
        ////station.fleetGatherRadius = 14f;
        ////station.radiusOfShipsRingAroundStation = 6;
        //stationCPU.colorOfStationMat = getProperMatColorByIndex(colorsOfPlayers[stationCPU.CPUNumber - 1]); //setting the next color available on colours list to this CPU station
        //stationCPU.setProperStationColor();

        //stationCPU.stationCurrentLevel = 0;
        //stationCPU.upgradeCounts = 3;
        //stationCPU.energyToNextUpgradeOfStation = CommonProperties.enrgy0to1Upgrd;
        //stationCPU.stationGunLevel = 0;
        //stationCPU.energyToNextUpgradeOfGun = CommonProperties.gun0to1Upgrd;
        //stationCPU.GunUpgradeCounts = 0;
        //stationCPU.Cruis4 = 0;
        //stationCPU.Cruis3 = 0;
        //stationCPU.Cruis2 = 0;
        //stationCPU.Cruis1 = 0;
        //stationCPU.Destr4 = 0;
        //stationCPU.Destr3 = 0;
        //stationCPU.Destr2 = 0;
        //stationCPU.Destr2Par = 0;
        //stationCPU.Destr1 = 0;
        //stationCPU.Destr1Par = 0;
        //stationCPU.energyOfStation = 200; //40
        //stationCPU.fillingSpeed = CommonProperties.star0FillingReducer; ;
        //stationCPU.ShipsAssigned = 0;
        //stationCPU.energyToConnection = CommonProperties.Station0EnergyToConnection;
        ////stationCPU.energyLoseIfDestroyed = CommonProperties.Station0GroupEnergyLoseIfDestroyed;
        //stationCPU.energyToGetFromLine = CommonProperties.Station0EnergyFromLine;
        ////stationCPU.energyRequiredToShot = CommonProperties.Station0EnergytoShot;
        //stationCPU.energyInscreaseTime = CommonProperties.Station0EnergyProduceTime;
        //stationCPU.energyInscreaseTimer = stationCPU.energyInscreaseTime;
        //stationCPU.energyProdeucePerTime = CommonProperties.Station0EnergyProduceAmountCPU;
        //stationCPU.HPInscreaseTimer = stationCPU.energyInscreaseTime;
        ////stationCPU.stationShotTime = CommonProperties.Station0ShotTime;
        ////stationCPU.stationShotTimer = stationCPU.stationShotTime;
        //stationCPU.energyPullFromEnergonRate = CommonProperties.Station0EnergyPullRate;
        //stationCPU.energonCatchDistance = CommonProperties.Station0EnergonCatchDistance;
        //stationCPU.energyLimitOfStation = CommonProperties.Station0EnergyLimit;
        //stationCPU.speedOfBullet = CommonProperties.Station0BulletSpeed;
        //stationCPU.colorToEnergy = CommonProperties.Station0ColorToEnergyMultiplyer;
        //stationCPU.ShipsLimit = CommonProperties.Station0ShipsLimit;
        //stationCPU.fillingLine.localPosition = new Vector3(0, 0, 0); //make full life to new station

        //stationCPU.ConnectedStations.Clear();
        //stationCPU.groupWhereTheStationIs = null;
        //ObjectPulled.transform.position = new Vector3(0, 0, -50);

        //stationCPU.stationTransform = ObjectPulled.transform;
        //stationCPU.stationPosition = stationCPU.stationTransform.position;
        //CommonProperties.allStations.Add(stationCPU);
        //CommonProperties.CPUStations.Add(stationCPU);
        //CommonProperties.CPUStationsDictionary[stationCPU.CPUNumber - 1].Add(stationCPU);
        //ObjectPulled.SetActive(true);


        //stationCPU = null;

        //ObjectPulledList = ObjectPullerRTS.current.GetStationPull(0, 1);
        //ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
        //stationCPU = ObjectPulled.GetComponent<StationCPU>();
        //stationCPU.CPUNumber = 3;
        ////station.fleetGatherRadius = 14f;
        ////station.radiusOfShipsRingAroundStation = 6;
        //stationCPU.colorOfStationMat = getProperMatColorByIndex(colorsOfPlayers[stationCPU.CPUNumber - 1]); //setting the next color available on colours list to this CPU station
        //stationCPU.setProperStationColor();

        //stationCPU.stationCurrentLevel = 0;
        //stationCPU.upgradeCounts = 3;
        //stationCPU.energyToNextUpgradeOfStation = CommonProperties.enrgy0to1Upgrd;
        //stationCPU.stationGunLevel = 0;
        //stationCPU.energyToNextUpgradeOfGun = CommonProperties.gun0to1Upgrd;
        //stationCPU.GunUpgradeCounts = 0;
        //stationCPU.Cruis4 = 0;
        //stationCPU.Cruis3 = 0;
        //stationCPU.Cruis2 = 0;
        //stationCPU.Cruis1 = 0;
        //stationCPU.Destr4 = 0;
        //stationCPU.Destr3 = 0;
        //stationCPU.Destr2 = 0;
        //stationCPU.Destr2Par = 0;
        //stationCPU.Destr1 = 0;
        //stationCPU.Destr1Par = 0;
        //stationCPU.energyOfStation = 200; //40
        //stationCPU.fillingSpeed = CommonProperties.star0FillingReducer; ;
        //stationCPU.ShipsAssigned = 0;
        //stationCPU.energyToConnection = CommonProperties.Station0EnergyToConnection;
        ////stationCPU.energyLoseIfDestroyed = CommonProperties.Station0GroupEnergyLoseIfDestroyed;
        //stationCPU.energyToGetFromLine = CommonProperties.Station0EnergyFromLine;
        ////stationCPU.energyRequiredToShot = CommonProperties.Station0EnergytoShot;
        //stationCPU.energyInscreaseTime = CommonProperties.Station0EnergyProduceTime;
        //stationCPU.energyInscreaseTimer = stationCPU.energyInscreaseTime;
        //stationCPU.energyProdeucePerTime = CommonProperties.Station0EnergyProduceAmountCPU;
        //stationCPU.HPInscreaseTimer = stationCPU.energyInscreaseTime;
        ////stationCPU.stationShotTime = CommonProperties.Station0ShotTime;
        ////stationCPU.stationShotTimer = stationCPU.stationShotTime;
        //stationCPU.energyPullFromEnergonRate = CommonProperties.Station0EnergyPullRate;
        //stationCPU.energonCatchDistance = CommonProperties.Station0EnergonCatchDistance;
        //stationCPU.energyLimitOfStation = CommonProperties.Station0EnergyLimit;
        //stationCPU.speedOfBullet = CommonProperties.Station0BulletSpeed;
        //stationCPU.colorToEnergy = CommonProperties.Station0ColorToEnergyMultiplyer;
        //stationCPU.ShipsLimit = CommonProperties.Station0ShipsLimit;
        //stationCPU.fillingLine.localPosition = new Vector3(0, 0, 0); //make full life to new station

        //stationCPU.ConnectedStations.Clear();
        //stationCPU.groupWhereTheStationIs = null;
        //ObjectPulled.transform.position = new Vector3(-86, 0, 0);

        //stationCPU.stationTransform = ObjectPulled.transform;
        //stationCPU.stationPosition = stationCPU.stationTransform.position;
        //CommonProperties.allStations.Add(stationCPU);
        //CommonProperties.CPUStations.Add(stationCPU);
        //CommonProperties.CPUStationsDictionary[stationCPU.CPUNumber - 1].Add(stationCPU);
        //ObjectPulled.SetActive(true);


        //stationCPU = null;

        //ObjectPulledList = ObjectPullerRTS.current.GetStationPull(0, 1);
        //ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
        //stationCPU = ObjectPulled.GetComponent<StationCPU>();
        //stationCPU.CPUNumber = 4;
        ////station.fleetGatherRadius = 14f;
        ////station.radiusOfShipsRingAroundStation = 6;
        //stationCPU.colorOfStationMat = getProperMatColorByIndex(colorsOfPlayers[stationCPU.CPUNumber - 1]); //setting the next color available on colours list to this CPU station
        //stationCPU.setProperStationColor();

        //stationCPU.stationCurrentLevel = 0;
        //stationCPU.upgradeCounts = 3;
        //stationCPU.energyToNextUpgradeOfStation = CommonProperties.enrgy0to1Upgrd;
        //stationCPU.stationGunLevel = 0;
        //stationCPU.energyToNextUpgradeOfGun = CommonProperties.gun0to1Upgrd;
        //stationCPU.GunUpgradeCounts = 0;
        //stationCPU.Cruis4 = 0;
        //stationCPU.Cruis3 = 0;
        //stationCPU.Cruis2 = 0;
        //stationCPU.Cruis1 = 0;
        //stationCPU.Destr4 = 0;
        //stationCPU.Destr3 = 0;
        //stationCPU.Destr2 = 0;
        //stationCPU.Destr2Par = 0;
        //stationCPU.Destr1 = 0;
        //stationCPU.Destr1Par = 0;
        //stationCPU.energyOfStation = 200; //40
        //stationCPU.fillingSpeed = CommonProperties.star0FillingReducer; ;
        //stationCPU.ShipsAssigned = 0;
        //stationCPU.energyToConnection = CommonProperties.Station0EnergyToConnection;
        ////stationCPU.energyLoseIfDestroyed = CommonProperties.Station0GroupEnergyLoseIfDestroyed;
        //stationCPU.energyToGetFromLine = CommonProperties.Station0EnergyFromLine;
        ////stationCPU.energyRequiredToShot = CommonProperties.Station0EnergytoShot;
        //stationCPU.energyInscreaseTime = CommonProperties.Station0EnergyProduceTime;
        //stationCPU.energyInscreaseTimer = stationCPU.energyInscreaseTime;
        //stationCPU.energyProdeucePerTime = CommonProperties.Station0EnergyProduceAmountCPU;
        //stationCPU.HPInscreaseTimer = stationCPU.energyInscreaseTime;
        ////stationCPU.stationShotTime = CommonProperties.Station0ShotTime;
        ////stationCPU.stationShotTimer = stationCPU.stationShotTime;
        //stationCPU.energyPullFromEnergonRate = CommonProperties.Station0EnergyPullRate;
        //stationCPU.energonCatchDistance = CommonProperties.Station0EnergonCatchDistance;
        //stationCPU.energyLimitOfStation = CommonProperties.Station0EnergyLimit;
        //stationCPU.speedOfBullet = CommonProperties.Station0BulletSpeed;
        //stationCPU.colorToEnergy = CommonProperties.Station0ColorToEnergyMultiplyer;
        //stationCPU.ShipsLimit = CommonProperties.Station0ShipsLimit;
        //stationCPU.fillingLine.localPosition = new Vector3(0, 0, 0); //make full life to new station
        //stationCPU.ConnectedStations.Clear();
        //stationCPU.groupWhereTheStationIs = null;

        //ObjectPulled.transform.position = new Vector3(-86, 0, 100);

        //stationCPU.stationTransform = ObjectPulled.transform;
        //stationCPU.stationPosition = stationCPU.stationTransform.position;
        //CommonProperties.allStations.Add(stationCPU);
        //CommonProperties.CPUStations.Add(stationCPU);
        //CommonProperties.CPUStationsDictionary[stationCPU.CPUNumber - 1].Add(stationCPU);
        //ObjectPulled.SetActive(true);

        #endregion stations instatiation

    }

    private void launchStation(int CPUNumber, Vector3 position) {

        if (CPUNumber == 0)
        {
            StationPlayerRTS station;

            ObjectPulledList = ObjectPullerRTS.current.GetStationPull(0, 0);
            ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
            station = ObjectPulled.GetComponent<StationPlayerRTS>();
            station.CPUNumber = CPUNumber;
            station.colorOfStationMat = getProperMatColorByIndex(CommonProperties.colorOfPlayer); //setting the next color available on colours list to this CPU station
            station.setProperStationColor();
            //station.fleetGatherRadius = 14f;
            //station.radiusOfShipsRingAroundStation = 6;
            station.stationCurrentLevel = 0;
            station.upgradeCounts = 3;
            station.energyToNextUpgradeOfStation = CommonProperties.enrgy0to1Upgrd;
            station.stationGunLevel = 0;
            station.energyToNextUpgradeOfGun = CommonProperties.gun0to1Upgrd;
            station.GunUpgradeCounts = 0;
            station.Cruis4 = 0;
            station.Cruis3 = 0;
            station.Cruis2 = 0;
            station.Cruis1 = 0;
            station.Destr4 = 0;
            station.Destr3 = 0;
            station.Destr2 = 0;
            station.Destr2Par = 0;
            station.Destr1 = 0;
            station.Destr1Par = 0;
            station.energyOfStation = 150;
            station.fillingSpeed = CommonProperties.star0FillingReducer; ;
            station.ShipsAssigned = 0;
            station.energyToConnection = CommonProperties.Station0EnergyToConnection;
            //station.energyLoseIfDestroyed = CommonProperties.Station0GroupEnergyLoseIfDestroyed;
            station.energyToGetFromLine = CommonProperties.Station0EnergyFromLine;
            //station.energyRequiredToShot = CommonProperties.Station0EnergytoShot;
            station.energyInscreaseTime = CommonProperties.Station0EnergyProduceTime;
            station.energyInscreaseTimer = station.energyInscreaseTime;
            station.energyProdeucePerTime = CommonProperties.Station0EnergyProduceAmount;
            station.energyPullFromEnergonRate = CommonProperties.Station0EnergyPullRate;
            station.HPInscreaseTimer = station.energyInscreaseTime;
            //station.stationShotTime = CommonProperties.Station0ShotTime;
            //station.stationShotTimer = station.stationShotTime;
            //station.shotTimerTransformIndex = -6f/station.stationShotTime;
            station.energonCatchDistance = CommonProperties.Station0EnergonCatchDistance;
            station.energyLimitOfStation = CommonProperties.Station0EnergyLimit;
            //station.speedOfBullet = CommonProperties.Station0BulletSpeed;
            station.colorToEnergy = CommonProperties.Station0ColorToEnergyMultiplyer;
            station.ShipsLimit = CommonProperties.Station0ShipsLimit;
            station.fillingLine.localPosition = new Vector3(0, 0, 0); //make full life to new station
            station.ConnectedStations.Clear();
            station.groupWhereTheStationIs = null;
            ObjectPulled.transform.position = position;
            station.stationTransform = ObjectPulled.transform;
            station.stationPosition = station.stationTransform.position;
            CommonProperties.allStations.Add(station);
            CommonProperties.playerStations.Add(station);
            ObjectPulled.SetActive(true);
        }
        else {
            StationCPU stationCPU;
            ObjectPulledList = ObjectPullerRTS.current.GetStationPull(0, 1);
            ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
            stationCPU = ObjectPulled.GetComponent<StationCPU>();
            stationCPU.CPUNumber = CPUNumber;
            //station.fleetGatherRadius = 14f;
            //station.radiusOfShipsRingAroundStation = 6;

            stationCPU.colorOfStationMat = getProperMatColorByIndex(colorsOfPlayers[stationCPU.CPUNumber - 1]); //setting the next color available on colours list to this CPU station
            stationCPU.setProperStationColor();

            stationCPU.stationCurrentLevel = 0;
            stationCPU.upgradeCounts = 3;
            stationCPU.energyToNextUpgradeOfStation = CommonProperties.enrgy0to1Upgrd;
            stationCPU.stationGunLevel = 0;
            stationCPU.energyToNextUpgradeOfGun = CommonProperties.gun0to1Upgrd;
            stationCPU.GunUpgradeCounts = 0;
            stationCPU.Cruis4 = 0;
            stationCPU.Cruis3 = 0;
            stationCPU.Cruis2 = 0;
            stationCPU.Cruis1 = 0;
            stationCPU.Destr4 = 0;
            stationCPU.Destr3 = 0;
            stationCPU.Destr2 = 0;
            stationCPU.Destr2Par = 0;
            stationCPU.Destr1 = 0;
            stationCPU.Destr1Par = 0;
            stationCPU.energyOfStation = 200; //40
            stationCPU.fillingSpeed = CommonProperties.star0FillingReducer; ;
            stationCPU.ShipsAssigned = 0;
            stationCPU.energyToConnection = CommonProperties.Station0EnergyToConnection;
            //stationCPU.energyLoseIfDestroyed = CommonProperties.Station0GroupEnergyLoseIfDestroyed;
            stationCPU.energyToGetFromLine = CommonProperties.Station0EnergyFromLine;
            //stationCPU.energyRequiredToShot = CommonProperties.Station0EnergytoShot;
            stationCPU.energyInscreaseTime = CommonProperties.Station0EnergyProduceTime;
            stationCPU.energyInscreaseTimer = stationCPU.energyInscreaseTime;
            stationCPU.HPInscreaseTimer = stationCPU.energyInscreaseTime;
            //stationCPU.stationShotTime = CommonProperties.Station0ShotTime;
            stationCPU.energyPullFromEnergonRate = CommonProperties.Station0EnergyPullRate;
            //stationCPU.stationShotTimer = stationCPU.stationShotTime;
            stationCPU.energonCatchDistance = CommonProperties.Station0EnergonCatchDistance;
            stationCPU.energyLimitOfStation = CommonProperties.Station0EnergyLimit;
            stationCPU.energyProdeucePerTime = CommonProperties.Station0EnergyProduceAmountCPU;
            //stationCPU.speedOfBullet = CommonProperties.Station0BulletSpeed;
            stationCPU.colorToEnergy = CommonProperties.Station0ColorToEnergyMultiplyer;
            stationCPU.ShipsLimit = CommonProperties.Station0ShipsLimit;
            stationCPU.fillingLine.localPosition = new Vector3(0, 0, 0); //make full life to new station
            stationCPU.ConnectedStations.Clear();
            stationCPU.groupWhereTheStationIs = null;
            ObjectPulled.transform.position = position;
            stationCPU.stationTransform = ObjectPulled.transform;
            stationCPU.stationPosition = stationCPU.stationTransform.position;
            CommonProperties.allStations.Add(stationCPU);
            CommonProperties.CPUStations.Add(stationCPU);
            CommonProperties.CPUStationsDictionary[stationCPU.CPUNumber - 1].Add(stationCPU);
            ObjectPulled.SetActive(true);
        }
    }

    private void launchStar(Vector3 position)
    {
        ObjectPulledList = ObjectPullerRTS.current.GetStarPull(3);
        ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
        CommonProperties.stars.Add(ObjectPulled.GetComponent<StarController>());
        ObjectPulled.transform.position = position;
        ObjectPulled.SetActive(true);
    }


    private IEnumerator launchTheEnergon() {
        yield return new WaitForSeconds(Random.Range(1f,3f));
        ObjectPulledList = ObjectPullerRTS.current.GetEnergonPull();
        ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
        EnergonMoving energon = ObjectPulled.GetComponent<EnergonMoving>();
        energon.gameController = this;
        CommonProperties.energonsOnScene.Add(energon);
        ObjectPulled.SetActive(true);
    }

    //0 is nothing, 1 player win, 2 player lost
    public void checkIfPlayerWinOrLost()
    {
        if (CommonProperties.playerStations.Count == 0 && CommonProperties.playerBattleShips.Count == 0)
        {
            if (!YouWinLoseButton.activeInHierarchy)
            {
                commonUISoundDeep.Play();
                YouWinLoseButtonTxt.text = Constants.Instance.getYouLostTxt();
                YouWinLoseButtonImg.color = new Color(0.85f, 0, 0, 1); //the red color, bit dark
                YouWinLoseButtonTxt.color = Color.red;
                YouWinLoseButtonImg.sprite = loseSprite;
                YouWinLoseButton.SetActive(true);
            }
        }
        else if (CommonProperties.CPUStations.Count == 0 && CommonProperties.CPUBattleShips.Count == 0)
        {
            if (!YouWinLoseButton.activeInHierarchy)
            {
                commonUISoundDeep.Play();
                YouWinLoseButtonTxt.text = Constants.Instance.getYouWinTxt();
                YouWinLoseButtonImg.color = new Color(0, 0.85f, 0, 1); //the green color, bit dark
                YouWinLoseButtonTxt.color = Color.green;
                YouWinLoseButtonImg.sprite = winSprite;
                YouWinLoseButton.SetActive(true);
            }
        }
    }

}
