using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationPlayerRTS : StationClass
{
    //private EnergonMoving energonOnScene;
    //[HideInInspector]
    //public Transform stationTransform;
    //[HideInInspector]
    //public Vector3 stationPosition;

    //[HideInInspector]
    //public float energonCatchDistance; //TO ASSIGN WHILE depending on the lvl of station
    //[HideInInspector]
    //public float speedOfBullet; //TO ASSIGN WHILE PULLLING depending on the lvl of station
    //[HideInInspector]
    //public float colorToEnergy; //TO ASSIGN WHILE PULLLING depending on the lvl of station (this is constant to transfer the color intensity to energy amount)
    //[HideInInspector]
    //public bool shotIsMade; //TO ASSIGN WITH RESPAWN OF ENERGON ON SCENE

    //private GameObject BulletPulled;
    //private List<GameObject> BulletPulledList;

    //[HideInInspector]
    //public float energyOfStation;
    //[HideInInspector]
    //public float energyInscreaseTime; //TO ASSIGN WITH RESPAWN OF ENERGON ON SCENE

    private CommonProperties commonProperties;

    private Vector3 positionOfCameraToActivateStationPanel;
    private CameraManager cameraManager;
    [HideInInspector]
    public bool stationPanelIsActiveForThis;

    private StationAttackButton attackButton;

    public Transform fillingLineRecharge;
    //private List<PlayerBattleShip> shipsUnderCommandOfStation;

    public List<GameObject> stationColorSphere; //parts of station demostrate the color of station

    private List<CPUBattleShip> closeCPUBattleShips;
    private CPUBattleShip shipToAttak;

    [SerializeField]
    private PlayerGun gunClass;

    //private GameObject aimingRectOfThis;

    private Camera camera;

    [SerializeField]
    private GameObject connectionToken;

    // Start is called before the first frame update
    void Start()
    {
        //energyOfStation = 0;
        //ShipsAssigned = 0;
        //shotIsMade = false;
        //energonOnScene = FindObjectOfType<EnergonMoving>();
        //stationTransform = transform;
        //stationPosition = stationTransform.position;

        camera = CommonProperties.MainCameraOfRTS;
        attackDistance = CommonProperties.attackDistanceForStations;
        closeCPUBattleShips = new List<CPUBattleShip>();
        attackLaserLine = GetComponent<LineRenderer>();
        attackLaserLine.positionCount = 2;

        energyPullLine = stationTransform.GetChild(0).GetComponent<LineRenderer>();
       

        //TO FINISH WITH ALL TYPES OF STATIONS
        if (name.Contains("0"))
        {
            attackTimeMin = CommonProperties.cruiser4MinAttackTime;
            attackTimeMax = CommonProperties.cruiser4MaxAttackTime;
            harm = CommonProperties.Cruiser4Harm + 3;
        }
        else if (name.Contains("1"))
        {
            attackTimeMin = CommonProperties.cruiser3MinAttackTime;
            attackTimeMax = CommonProperties.cruiser3MaxAttackTime;
            harm = CommonProperties.Cruiser3Harm + 3;
        }
        else if (name.Contains("2"))
        {
            attackTimeMin = CommonProperties.cruiser2MinAttackTime;
            attackTimeMax = CommonProperties.cruiser2MaxAttackTime;
            harm = CommonProperties.Cruiser2Harm + 3;
        }
        else if (name.Contains("3"))
        {
            attackTimeMin = CommonProperties.cruiser1MinAttackTime;
            attackTimeMax = CommonProperties.cruiser1MaxAttackTime;
            harm = CommonProperties.Cruiser1Harm + 3;
        }

        fillingLineRecharge.localPosition = new Vector3(0, 0, 0);
        commonProperties = FindObjectOfType<CommonProperties>();
        stationPanelIsActiveForThis = false;
        attackButton = FindObjectOfType<StationAttackButton>();
        cameraManager = CommonProperties.MainCameraOfRTSTransform.gameObject.GetComponent<CameraManager>();

        //shipsUnderCommandOfStation = new List<PlayerBattleShip>();
        //energonCatchDistance = 30f; //TO DELETE CAUSE IS ASSIGNED WHILE INSTANTIATING
        //speedOfBullet = 10f; //TO DELETE CAUSE IS ASSIGNED WHILE INSTANTIATING
        //colorToEnergy = 25f; //TO DELETE CAUSE IS ASSIGNED WHILE INSTANTIATING
        //energyInscreaseTime = 0.1f; //TO DELETE CAUSE IS ASSIGNED WHILE INSTANTIATING
        //ShipsLimit = 16; //TO DELETE CAUSE IS ASSIGNED WHILE INSTANTIATING
    }

    //TO DELETE
    private void OnEnable()
    {
        if (stationCurrentLevel > 0) gunSphereParentTransform = gunSphereParent.transform;
        positionOfCameraToActivateStationPanel = new Vector3(stationPosition.x, CommonProperties.MainCameraOfRTSTransform.position.y, stationPosition.z);

        //to prevent a bug of spawning additional pulled object (automatic will grow station that copy of pulled object with 0 index which can be already active and with active gun) with active gun 
        if (gunSphereVisible != null && gunSphereVisible.activeInHierarchy) gunSphereVisible.SetActive(false);
    }
    private void OnDisable()
    {
        //if (stationBullet != null) stationBullet.SetActive(false);
        CommonProperties.playerStations.Remove(this);
        CommonProperties.allStations.Remove(this);
    }

    //setting a proper color material to station color sphere
    //0-Red, 1-green, 2-blue, 3-yellow, 4-purple
    public void setProperStationColor()
    {
        //stationColorSphere.GetComponent<MeshRenderer>().material = colorOfStationMat;
        for (int i = 0; i < stationColorSphere.Count; i++)
        {
            stationColorSphere[i].GetComponent<MeshRenderer>().material.SetColor("_Color", colorOfStationMat);
        }
        //Color territoryColor = new Color(colorOfStationMat.r * 0.15f, colorOfStationMat.g * 0.15f, colorOfStationMat.b * 0.15f, 1);
        territoryLine.color = colorOfStationMat;
        energyGainEffectMain.startColor = colorOfStationMat;

    }

    private void OnMouseUpAsButton()
    {
        if (!CommonProperties.stationPanelIsActive && UISelectingBox.Instance.chosenPlayerBattleShipsObject.Count == 0 && UISelectingBox.Instance.checkIfTouchNotInsideUIPanel(Input.mousePosition))
        {
            CommonProperties.stationPanelScript.activateThePanel(this);
            commonProperties.stationPanel.SetActive(true);
            CommonProperties.stationPanelIsActive = true;
            stationPanelIsActiveForThis = true;
        }
    }


    private void OnMouseDrag()
    {
        if (!ConnectionPLayerStations.Instance.lineIsDruggedFromPlayerStation && connectionToken.activeInHierarchy)
        {
            ConnectionPLayerStations.Instance.instantiateConnectionLineToDragFromStation(stationPosition, this);
            
        }


        //if (!ConnectionPLayerStations.Instance.lowEnegryImg.activeInHierarchy) ConnectionPLayerStations.Instance.lowEnegryImg.SetActive(true);
    }

    private void collectTheCloseEnemyShips()
    {
        for (int i = 0; i < CommonProperties.CPUBattleShips.Count; i++)
        {
            if ((CommonProperties.CPUBattleShips[i].shipTransform.position - stationPosition).magnitude <= attackDistance)
            {
                closeCPUBattleShips.Add(CommonProperties.CPUBattleShips[i]);
            }
        }
        

        if (closeCPUBattleShips.Count > 0)
        {
            attackMode = true;
        }
        else
        {
            attackMode = false;
            shipToAttak = null;
            if (attackTimerTime < Random.Range(attackTimeMin, attackTimeMax)) attackTimerTime = Random.Range(attackTimeMin, attackTimeMax);
        }

    }

    IEnumerator attackTheEnemy()
    {
        if (shipToAttak != null)
        {
            if (!shipToAttak.isActiveAndEnabled || (shipToAttak.shipTransform.position - stationPosition).magnitude > attackDistance)
            {
                if (closeCPUBattleShips.Contains(shipToAttak)) closeCPUBattleShips.Remove(shipToAttak);
                shipToAttak = null;
            }
            else
            {
                attackLaserLine.SetPosition(0, stationPosition);
                attackLaserLine.SetPosition(1, shipToAttak.shipTransform.position);
                attackLaserLine.enabled = true;
            }
        }
        
        else
        {
            if (closeCPUBattleShips.Count > 0)
            {
                shipToAttak = closeCPUBattleShips.Count < 2 ? closeCPUBattleShips[0] : closeCPUBattleShips[Random.Range(0, closeCPUBattleShips.Count)];
                if (shipToAttak.isActiveAndEnabled)
                {
                    attackLaserLine.SetPosition(0, stationPosition);
                    attackLaserLine.SetPosition(1, shipToAttak.shipTransform.position);
                    attackLaserLine.enabled = true;
                }
                else shipToAttak = null;
            }
            
        }

        if (shipToAttak != null)
        {
            if (!shipToAttak.shieldIsOn) shipToAttak.reduceTheHPOfShip(harm);
        }
        

        closeCPUBattleShips.Clear();

        yield return new WaitForSeconds(0.8f);
        attackLaserLine.enabled = false;
    }

    //to catch the energon
    //public void makeAShotFromStation(bool groupOfStation)
    //{
    //    if (groupOfStation) CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= energyRequiredToShot;
    //    else energyOfStation -= energyRequiredToShot;
    //    foreach (StationPlayerRTS stationPlayer in CommonProperties.playerStations) stationPlayer.checkIfStationCanConnect();
    //    if (CommonProperties.stationPanelIsActive) CommonProperties.stationPanelScript.updateVariablesAfterEnergyChanges();
    //    ObjectPulledList = ObjectPullerRTS.current.GetStationBulletPull();
    //    ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
    //    stationBullet = ObjectPulled;
    //    ObjectPulled.transform.position = stationPosition;
    //    //last parameter is default fro player station cause it is the number of CPU
    //    ObjectPulled.GetComponent<StationBullet>().setTheAimAndStation(closestEnegon.energonTransform, stationTransform.position, this, colorToEnergy, speedOfBullet, energonCatchDistance, true/*,0*/);
    //    ObjectPulled.SetActive(true);
    //    if (aimingLine.enabled)
    //    {
    //        aimingLine.enabled = false;
    //        aimingRectOfThis.SetActive(false);
    //    }
    //}

    public override void pullTheEnergyFromEnergons()
    {
        if (closestEnegon != null)
        {
            if (distanceToEnergon() < energonCatchDistance && closestEnegon.isActiveAndEnabled)
            {
                energyPullLine.SetPosition(0, stationTransform.position);
                energyPullLine.SetPosition(1, closestEnegon.energonTransform.position);
                if (!energyPullLine.enabled)
                {
                    energyPullLine.enabled = true;
                }
                if (groupWhereTheStationIs != null)
                {
                    if (CommonProperties.energyLimitOfStationGroups[groupWhereTheStationIs] > CommonProperties.energyOfStationGroups[groupWhereTheStationIs])
                    {
                        CommonProperties.energyOfStationGroups[groupWhereTheStationIs] += (int)(energyPullFromEnergonRate + closestEnegon.speedOfEnergon);

                        if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.energyLimitOfStationGroups[groupWhereTheStationIs])
                            CommonProperties.energyOfStationGroups[groupWhereTheStationIs] = CommonProperties.energyLimitOfStationGroups[groupWhereTheStationIs];
                    }
                }
                else
                {
                    if (energyLimitOfStation > energyOfStation)
                    {
                        energyOfStation += (int)(energyPullFromEnergonRate + closestEnegon.speedOfEnergon);
                        if (energyOfStation > energyLimitOfStation) energyOfStation = energyLimitOfStation;
                    }
                }
                foreach (StationPlayerRTS stationPlayer in CommonProperties.playerStations) stationPlayer.checkIfStationCanConnect();
            }
            else
            {
                if (energyPullLine.enabled)
                {
                    //aimingRectOfThis.SetActive(false);
                    energyPullLine.enabled = false;
                }
            }
        }
    }

    private void energyIncrementAndCheckTheStationPanel()
    {
        if (groupWhereTheStationIs != null/*&& groupWhereTheStationIs.Count > 0*/) CommonProperties.energyOfStationGroups[groupWhereTheStationIs]++;
        else energyOfStation++;
        if (stationPanelIsActiveForThis)
        {
            CommonProperties.stationPanelScript.updateVariablesAfterEnergyChanges();
        }
        foreach (StationPlayerRTS stationPlayer in CommonProperties.playerStations) stationPlayer.checkIfStationCanConnect();
    }

    //private void checkIfStationCanShot()
    //{
    //    if (closestEnegon != null)
    //    {
    //        //if (groupWhereTheStationIs != null /*&& groupWhereTheStationIs.Count > 0*/)
    //        //{
    //        //    if (distanceToEnergon() < energonCatchDistance /*&& !shotIsMade && CommonProperties.energyOfStationGroups[groupWhereTheStationIs] >= energyRequiredToShot*/ && closestEnegon.isActiveAndEnabled)
    //        //    {
    //        //        if (!StationAttackButton.playerStations.Contains(this)) attackButton.addStationToButton(this, closestEnegon);
    //        //        aimingLine.SetPosition(0, stationTransform.position);
    //        //        aimingLine.SetPosition(1, closestEnegon.energonTransform.position);

    //        //        if (!aimingLine.enabled)
    //        //        {
    //        //            aimingLine.enabled = true;
    //        //            ObjectPulledList = ObjectPullerRTS.current.GetAimingRectPullList();
    //        //            aimingRectOfThis = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
    //        //            aimingRectOfThis.transform.position = aimingLine.GetPosition(1);
    //        //            aimingRectOfThis.SetActive(true);
    //        //        }
    //        //        if (aimingRectOfThis!=null) aimingRectOfThis.transform.position = new Vector3(camera.WorldToScreenPoint(aimingLine.GetPosition(1)).x, camera.WorldToScreenPoint(aimingLine.GetPosition(1)).y, 0);
    //        //    }
    //        //    else if (StationAttackButton.playerStations.Contains(this))
    //        //    {
    //        //        attackButton.removeStationFromButton(this);
    //        //        if (aimingLine.enabled)
    //        //        {
    //        //            aimingRectOfThis.SetActive(false);
    //        //            aimingLine.enabled = false;
    //        //        }
    //        //    }
    //        //}
    //        //else {
    //        //    if (distanceToEnergon() < energonCatchDistance && !shotIsMade && energyOfStation >= energyRequiredToShot && closestEnegon.isActiveAndEnabled)
    //        //    {
    //        //        if (!StationAttackButton.playerStations.Contains(this)) attackButton.addStationToButton(this, closestEnegon);
    //        //        aimingLine.SetPosition(0, stationTransform.position);
    //        //        aimingLine.SetPosition(1, closestEnegon.energonTransform.position);

    //        //        if (!aimingLine.enabled)
    //        //        {
    //        //            aimingLine.enabled = true;
    //        //            ObjectPulledList = ObjectPullerRTS.current.GetAimingRectPullList();
    //        //            aimingRectOfThis = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
    //        //            aimingRectOfThis.transform.position = aimingLine.GetPosition(1);
    //        //            aimingRectOfThis.SetActive(true);
    //        //        }
    //        //        if (aimingRectOfThis != null) aimingRectOfThis.transform.position = new Vector3(camera.WorldToScreenPoint(aimingLine.GetPosition(1)).x, camera.WorldToScreenPoint(aimingLine.GetPosition(1)).y, 0);
    //        //    }
    //        //    else if (StationAttackButton.playerStations.Contains(this))
    //        //    {
    //        //        attackButton.removeStationFromButton(this);
    //        //        if (aimingLine.enabled)
    //        //        {
    //        //            aimingRectOfThis.SetActive(false);
    //        //            aimingLine.enabled = false;
    //        //        }
    //        //    }
    //        //}

    //    }
    //}

    public override void checkIfStationCanConnect()
    {
        if (groupWhereTheStationIs != null /*&& groupWhereTheStationIs.Count > 0*/)
        {
            if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] >= energyToConnection)
            {
                for (int i = 0; i < CommonProperties.playerStations.Count; i++)
                {
                    if (CommonProperties.playerStations[i] != this && (CommonProperties.playerStations[i].stationPosition - stationPosition).magnitude < oneStepCloseStationsMaxDistance
                        && !groupWhereTheStationIs.Contains(CommonProperties.playerStations[i]))
                    {
                        connectionToken.SetActive(true);
                        break;
                    }
                    else if (connectionToken.activeInHierarchy) connectionToken.SetActive(false);
                }
            }
            else if (connectionToken.activeInHierarchy) connectionToken.SetActive(false);
        }
        else {
            if (energyOfStation >= energyToConnection)
            {
                for (int i = 0; i < CommonProperties.playerStations.Count; i++)
                {
                    if (CommonProperties.playerStations[i] != this && (CommonProperties.playerStations[i].stationPosition - stationPosition).magnitude < oneStepCloseStationsMaxDistance)
                    {
                        connectionToken.SetActive(true);
                        break;
                    }
                    else if (connectionToken.activeInHierarchy) connectionToken.SetActive(false);
                }
            }
            else if (connectionToken.activeInHierarchy) connectionToken.SetActive(false);
        }
    }

    public override int stationDefenceFleetPower()
    {
        int shipsPower = 0;
        foreach (PlayerBattleShip ship in CommonProperties.playerBattleShips)
        {
            if ((ship.shipTransform.position - stationPosition).magnitude < fleetGatherRadius)
            {
                shipsPower += ship.shipPower;
            }
        }
        return shipsPower;
    }

    

    //here it is used only to update the energy variable on panel if it is open while player station gets the energy, it is called from ebergyBallRTS class
    public override void utilaizeTheEnergy(bool isRecursionCall)
    {
        if (stationPanelIsActiveForThis)
        {
            CommonProperties.stationPanelScript.updateVariablesAfterEnergyChanges();
        }
        foreach (StationPlayerRTS stationPlayer in CommonProperties.playerStations) stationPlayer.checkIfStationCanConnect();
    }

    //public override void disactivateThisStation(StationClass newStation)
    //{
    //    for (int i = 0; i < CommonProperties.playerBattleShips.Count; i++)
    //        if (CommonProperties.playerBattleShips[i].maternalStation == this) CommonProperties.playerBattleShips[i].maternalStation = newStation;
    //    base.disactivateThisStation(newStation);
    //}


    public void upgradeStation(int nextStationLevel) {
        //energyOfStation -= energyToNextUpgradeOfStation;
        StationPlayerRTS station;
        int nextStationLevelTemp = nextStationLevel;
        if (nextStationLevel == 1)
        {
            ObjectPulledList = ObjectPullerRTS.current.GetStationPull(nextStationLevelTemp, 0);
            ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
            station = ObjectPulled.GetComponent<StationPlayerRTS>();
            station.energyToNextUpgradeOfStation = CommonProperties.enrgy1to2Upgrd;
            station.energyToNextUpgradeOfGun = CommonProperties.gun0to1Upgrd;
            station.fillingSpeed = CommonProperties.star1FillingReducer;
            station.energyToConnection = CommonProperties.Station1EnergyToConnection;
            station.energyLoseIfDestroyed = CommonProperties.Station1GroupEnergyLoseIfDestroyed;
            station.energyToGetFromLine = CommonProperties.Station1EnergyFromLine;
            station.energyInscreaseTime = CommonProperties.Station1EnergyProduceTime;
            station.energyPullFromEnergonRate = CommonProperties.Station1EnergyPullRate;
            //station.stationShotTime = CommonProperties.Station1ShotTime;
            station.energonCatchDistance = CommonProperties.Station1EnergonCatchDistance;
            station.energyLimitOfStation = CommonProperties.Station1EnergyLimit;
            station.colorToEnergy = CommonProperties.Station1ColorToEnergyMultiplyer;
            station.ShipsLimit = CommonProperties.Station1ShipsLimit;
            station.speedOfBullet = CommonProperties.Station1BulletSpeed;
        }
        else if (nextStationLevel == 2)
        {
            ObjectPulledList = ObjectPullerRTS.current.GetStationPull(nextStationLevelTemp, 0);
            ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
            station = ObjectPulled.GetComponent<StationPlayerRTS>();
            station.energyToNextUpgradeOfStation = CommonProperties.enrgy2to3Upgrd;
            station.energyToNextUpgradeOfGun = CommonProperties.gun1to2Upgrd;
            station.fillingSpeed = CommonProperties.star2FillingReducer;
            station.energyToConnection = CommonProperties.Station2EnergyToConnection;
            station.energyLoseIfDestroyed = CommonProperties.Station2GroupEnergyLoseIfDestroyed;
            station.energyToGetFromLine = CommonProperties.Station2EnergyFromLine;
            station.energyInscreaseTime = CommonProperties.Station2EnergyProduceTime;
            station.energyPullFromEnergonRate = CommonProperties.Station2EnergyPullRate;
            //station.stationShotTime = CommonProperties.Station2ShotTime;
            station.energonCatchDistance = CommonProperties.Station2EnergonCatchDistance;
            station.energyLimitOfStation = CommonProperties.Station2EnergyLimit;
            station.colorToEnergy = CommonProperties.Station2ColorToEnergyMultiplyer;
            station.ShipsLimit = CommonProperties.Station2ShipsLimit;
            station.speedOfBullet = CommonProperties.Station2BulletSpeed;
        }
        else 
        {
            ObjectPulledList = ObjectPullerRTS.current.GetStationPull(nextStationLevelTemp, 0);
            ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
            station = ObjectPulled.GetComponent<StationPlayerRTS>();
            station.energyToNextUpgradeOfStation = CommonProperties.enrgy2to3Upgrd;
            station.energyToNextUpgradeOfGun = CommonProperties.gun2to3Upgrd;
            station.fillingSpeed = CommonProperties.star3FillingReducer;
            station.energyToConnection = CommonProperties.Station3EnergyToConnection;
            station.energyLoseIfDestroyed = CommonProperties.Station3GroupEnergyLoseIfDestroyed;
            station.energyToGetFromLine = CommonProperties.Station3EnergyFromLine;
            station.energyInscreaseTime = CommonProperties.Station3EnergyProduceTime;
            station.energyPullFromEnergonRate = CommonProperties.Station3EnergyPullRate;
            //station.stationShotTime = CommonProperties.Station3ShotTime;
            station.energonCatchDistance = CommonProperties.Station3EnergonCatchDistance;
            station.energyLimitOfStation = CommonProperties.Station3EnergyLimit;
            station.colorToEnergy = CommonProperties.Station3ColorToEnergyMultiplyer;
            station.ShipsLimit = CommonProperties.Station3ShipsLimit;
            station.speedOfBullet = CommonProperties.Station3BulletSpeed;
        }

        station.CPUNumber = 0;
        station.colorOfStationMat = colorOfStationMat; 
        station.setProperStationColor();
        station.stationCurrentLevel = nextStationLevelTemp;
        station.upgradeCounts = upgradeCounts;
        station.stationGunLevel = nextStationLevelTemp-1; //new station will appear without gun even if it was 
        station.GunUpgradeCounts = nextStationLevelTemp;
        station.Cruis4 = Cruis4;
        station.Cruis3 = Cruis3;
        station.Cruis2 = Cruis2;
        station.Cruis1 = Cruis1;
        station.Destr4 = Destr4;
        station.Destr3 = Destr3;
        station.Destr2 = Destr2;
        station.Destr2Par = Destr2Par;
        station.Destr1 = Destr1;
        station.Destr1Par = Destr1Par;
        station.energyOfStation = energyOfStation;
        station.energyInscreaseTimer = station.energyInscreaseTime;
        station.HPInscreaseTimer = station.energyInscreaseTime;
        //station.stationShotTimer = station.stationShotTime;
        //station.shotTimerTransformIndex = -6f / station.stationShotTime;
        station.ShipsAssigned = ShipsAssigned;
        station.fillingLine.localPosition = new Vector3(0, 0, 0); //make full life to new station
        station.ConnectedStations.Clear(); //clear the connections if there left any from previous life of pulled object
        if (groupWhereTheStationIs != null/*&& groupWhereTheStationIs.Count>0*/) connectUpgradedStationToGroup(station);
        else station.groupWhereTheStationIs = null;
        ObjectPulled.transform.position = stationPosition;
        station.stationTransform = ObjectPulled.transform;
        station.stationPosition = stationPosition;
        CommonProperties.allStations.Add(station);
        CommonProperties.playerStations.Add(station);
        ObjectPulled.SetActive(true);
        //removeStationFromAttackButton();
        disactivateThisStation(station);

        foreach (StationPlayerRTS stationPlayer in CommonProperties.playerStations) if (stationPlayer!=this) stationPlayer.checkIfStationCanConnect();
    }

    private void connectUpgradedStationToGroup(StationPlayerRTS station) {
        //determmine the group processes for new and old station
        station.groupWhereTheStationIs = groupWhereTheStationIs;
        groupWhereTheStationIs.Remove(this);
        station.groupWhereTheStationIs.Add(station);
        for (int i = 0; i < CommonProperties.connectionLines[CPUNumber].Count; i++)
        {
            if (CommonProperties.connectionLines[CPUNumber][i].stations.Contains(this))
            {
                CommonProperties.connectionLines[CPUNumber][i].reassignStationAfterUpgrade(this, station);
            }
        }

        //deal with connections
        //connect upgraded station with old station connections
        foreach (StationClass stationsConnectedToOldStation in ConnectedStations) station.ConnectedStations.Add(stationsConnectedToOldStation);
        //connect old station connections with upgraded station
        foreach (StationClass stationsConnectedToOldStation in ConnectedStations) stationsConnectedToOldStation.ConnectedStations.Add(station);
        //sending the signal of cutting the connection with old station to other stations
        foreach (StationClass stationsConnectedToOldStation in ConnectedStations) stationsConnectedToOldStation.ConnectedStations.Remove(this);
    }

    public void upgradeTheGun(int gunLevel) {
        //energyOfStation -= energyToNextUpgradeOfGun;
        stationGunLevel = gunLevel;
        if (gunClass.colorOfGunMat != colorOfStationMat)
        {
            gunClass.CPUNumber = CPUNumber;
            gunClass.colorOfGunMat = colorOfStationMat;
            gunClass.setProperGunColor();
        }
        //gunSphereParent.SetActive(true);
        gunSphereVisible.SetActive(true);
    }

    //public void removeStationFromAttackButton() {
    //    if (StationAttackButton.playerStations.Contains(this))
    //    {
    //        attackButton.removeStationFromButton(this);
    //        if (aimingLine.enabled)
    //        {
    //            aimingLine.enabled = false;
    //            aimingRectOfThis.SetActive(false);
    //        }
    //    }
    //}

    private void FixedUpdate()
    {
        //smoothly but fast lerp camera to station to open the panel
        if (CommonProperties.stationPanelIsActive && stationPanelIsActiveForThis)
        {
            //if (!cameraManager.isPerspective)
            //{
            //    if (positionOfCameraToActivateStationPanel.x != CommonProperties.MainCameraOfRTSTransform.position.x || positionOfCameraToActivateStationPanel.z != CommonProperties.MainCameraOfRTSTransform.position.z)
            //        CommonProperties.MainCameraOfRTSTransform.position = Vector3.MoveTowards(CommonProperties.MainCameraOfRTSTransform.position, positionOfCameraToActivateStationPanel, 2f);
            //    if (CommonProperties.MainCameraOfRTS.orthographicSize != 25f) CommonProperties.MainCameraOfRTS.orthographicSize = Mathf.Lerp(CommonProperties.MainCameraOfRTS.orthographicSize, 25f, 0.1f);
            //}
            //else
            //{
                if (positionOfCameraToActivateStationPanel.x != CommonProperties.MainCameraOfRTSTransform.position.x || (positionOfCameraToActivateStationPanel.z + 100) != CommonProperties.MainCameraOfRTSTransform.position.z)
                    CommonProperties.MainCameraOfRTSTransform.position = Vector3.MoveTowards(CommonProperties.MainCameraOfRTSTransform.position, 
                        new Vector3 (positionOfCameraToActivateStationPanel.x, positionOfCameraToActivateStationPanel.y, positionOfCameraToActivateStationPanel.z + 100), 2f);

                if (CommonProperties.MainCameraOfRTSTransform.rotation != Quaternion.Euler(60, 180, 0)) 
                    CommonProperties.MainCameraOfRTSTransform.rotation = Quaternion.Lerp(CommonProperties.MainCameraOfRTSTransform.rotation, Quaternion.Euler(60, 180, 0), 2f);

                if (CommonProperties.MainCameraOfRTS.fieldOfView != 10f) CommonProperties.MainCameraOfRTS.fieldOfView = Mathf.Lerp(CommonProperties.MainCameraOfRTS.fieldOfView, 10f, 0.1f);
            //}
        }
        if (attackLaserLine.enabled && shipToAttak != null)
        {
            attackLaserLine.SetPosition(1, shipToAttak.shipTransform.position);
        }
        if (attackMode && shipToAttak != null && stationCurrentLevel>0 && stationGunLevel==GunUpgradeCounts) {
            if (gunSphereParentTransform.rotation!=Quaternion.LookRotation(shipToAttak.shipTransform.position- gunSphereParentTransform.position, Vector3.up)) 
                gunSphereParentTransform.rotation=Quaternion.Lerp(gunSphereParentTransform.rotation, Quaternion.LookRotation(shipToAttak.shipTransform.position - gunSphereParentTransform.position, Vector3.up), 0.1f);
        }
    }

   
    // Update is called once per frame
    void Update()
    {
        if (shipToAttak == null) collectTheCloseEnemyShips();
        if (attackMode && attackTimerTime > 0)
        {
            attackTimerTime -= Time.deltaTime;
        }
        if (attackMode && attackTimerTime <= 0)
        {
            attackTimerTime = Random.Range(attackTimeMin, attackTimeMax);
            StartCoroutine(attackTheEnemy());
        }

        if (CommonProperties.energonsOnScene.Count > 0) resetTheClosestEnergon();

        pullTheEnergyFromEnergons();
        if (stationPanelIsActiveForThis)
        {
            CommonProperties.stationPanelScript.updateVariablesAfterEnergyChanges();
        }
        //checkIfStationCanShot();

        //timer to make next shot to energon
        //if (stationShotTimer > 0 && shotIsMade)
        //{
        //    stationShotTimer -= Time.deltaTime;
        //    fillingLineRecharge.localPosition = new Vector3(stationShotTimer * shotTimerTransformIndex, 0, 0);
        //    //0.4 is constant to transfer
        //    if (stationShotTimer < 0)
        //    {
        //        shotIsMade = false;
        //        fillingLineRecharge.localPosition = new Vector3(0, 0, 0);
        //        stationShotTimer = stationShotTime;
        //    }
        //}

        //timer to increment the energy naturally
        if (energyInscreaseTimer > 0 && groupWhereTheStationIs==null)
        {
            energyInscreaseTimer -= Time.deltaTime;
            if (energyInscreaseTimer < 0)
            {
                energyIncrementAndCheckTheStationPanel();
                energyInscreaseTimer = energyInscreaseTime;
            }
        }
        //timer to increment the HP naturally
        if (lifeLineAmount < 0)
        {
            HPInscreaseTimer -= Time.deltaTime;
            if (HPInscreaseTimer < 0)
            {
                increaseTheHPOfStation(150);
                HPInscreaseTimer = energyInscreaseTime;
            }
        }
    }
}
 