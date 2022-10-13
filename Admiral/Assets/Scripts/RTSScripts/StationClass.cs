using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationClass : MonoBehaviour
{
    //[HideInInspector]
    //public EnergonMoving energonOnScene;
    [HideInInspector]
    public Transform stationTransform;
    [HideInInspector]
    public Vector3 stationPosition;
    [HideInInspector]
    public float energonCatchDistance; //TO ASSIGN WHILE depending on the lvl of station
    [HideInInspector]
    public float speedOfBullet; //TO ASSIGN WHILE PULLLING depending on the lvl of station
    [HideInInspector]
    public float colorToEnergy; //TO ASSIGN WHILE PULLLING depending on the lvl of station (this is constant to transfer the color intensity to energy amount)
    //[HideInInspector]
    //public bool shotIsMade; //is managed by energon on scene 
    //public int CPUNumber; //TO ASSIGN WHILE PULLLING depending on the lvl of station
    [HideInInspector]
    public GameObject ObjectPulled;
    [HideInInspector]
    public List<GameObject> ObjectPulledList;

    [HideInInspector]
    public int energyOfStation;
    [HideInInspector]
    public int energyLimitOfStation;
    [HideInInspector]
    public float energyInscreaseTime; 
    [HideInInspector]
    public float energyInscreaseTimer;
    [HideInInspector]
    public float energyPullFromEnergonRate;
    [HideInInspector]
    public float HPInscreaseTimer;
    //[HideInInspector]
    //public float stationShotTimer;
    //[HideInInspector]
    //public float stationShotTime;
    [HideInInspector]
    public int ShipsLimit; 
    [HideInInspector]
    public int ShipsAssigned; 
    //[HideInInspector]
    //public int energyRequiredToShot;
    [HideInInspector]
    public int energyToGetFromLine;
    [HideInInspector]
    public int stationCurrentLevel; 
    [HideInInspector]
    public int upgradeCounts; 
    [HideInInspector]
    public int energyToNextUpgradeOfStation; 
    [HideInInspector]
    public int stationGunLevel; 
    [HideInInspector]
    public int GunUpgradeCounts; 
    [HideInInspector]
    public int energyToNextUpgradeOfGun;
    [HideInInspector]
    public int energyToConnection;
    [HideInInspector]
    public int energyLoseIfDestroyed;

    [HideInInspector]
    public int energyOfStationToUPGradeGun;
    [HideInInspector]
    public int energyOfStationToUPGradeStation;
    [HideInInspector]
    public int energyOfStationToSetConnection;

    [HideInInspector]
    public int Cruis4;
    [HideInInspector]
    public int Cruis3;
    [HideInInspector]
    public int Cruis2;
    [HideInInspector]
    public int Cruis1; 
    [HideInInspector]
    public int Destr4;
    [HideInInspector]
    public int Destr3;
    [HideInInspector]
    public int Destr2;
    [HideInInspector]
    public int Destr2Par;
    [HideInInspector]
    public int Destr1;
    [HideInInspector]
    public int Destr1Par;

    [HideInInspector]
    public const float radiusOfShipsRingAroundStation = 6;
    //[HideInInspector]
    //public List<Vector3> squardPositions;
    [HideInInspector]
    public const float fleetGatherRadius = 14f;
    [HideInInspector]
    public float fillingSpeed; //TO ASSIGN WHILE PULLLING

    [HideInInspector]
    public int CPUNumber; //CPUNumber of player is always 0
    
    public Transform fillingLine;
    [HideInInspector]
    public float lifeLineAmount;

    //[HideInInspector]
    //public GameObject stationBullet;

    [HideInInspector]
    public float shotTimerTransformIndex;

    [HideInInspector]
    public EnergonMoving closestEnegon;

    [HideInInspector]
    public float attackDistance;
    [HideInInspector]
    public bool attackMode;
    [HideInInspector]
    public float attackTimeMax; //TO ASSIGN WHILE PULLLING
    [HideInInspector]
    public float attackTimeMin; //TO ASSIGN WHILE PULLLING
    [HideInInspector]
    public float harm; //TO ASSIGN WHILE PULLLING
    [HideInInspector]
    public LineRenderer attackLaserLine;
    [HideInInspector]
    public LineRenderer energyPullLine;
    [HideInInspector]
    public float attackTimerTime;
    [HideInInspector]
    public float shieldTimerTime;

    //Used by CPU statuin only
    [HideInInspector]
    public List<CPUBattleShip> shipsToGiveOrderCommandOfStation;

    public GameObject gunSphereVisible; //to disactivate or activate hole gun complex
    public GameObject gunSphereParent; //to rotate the gun
    [HideInInspector]
    public Transform gunSphereParentTransform;
    //public GameObject gun;

    [HideInInspector]
    public ParticleSystem energyGainEffect;
    [HideInInspector]
    public ParticleSystem.MainModule energyGainEffectMain;

    [HideInInspector]
    public SpriteRenderer territoryLine;

    //[HideInInspector]
    //public Dictionary<Vector3, StationClass> connectionsToStations;
    //[HideInInspector]
    //public byte connectionsCount;
    public List<StationClass> groupWhereTheStationIs;
    public List<StationClass> ConnectedStations;

    [HideInInspector]
    public Color colorOfStationMat;

    public StationClass stationToConnect;

    public const float oneStepCloseStationsMaxDistance = 140f;
    private void Awake()
    {
        //groupWhereTheStationIs = new List<StationClass>();
        //ConnectedStations = new List<StationClass>();
           //shotIsMade = false;
        stationTransform = transform;
        stationPosition = stationTransform.position;
        //connectionsToStations = new Dictionary<Vector3, StationClass>();
        territoryLine = stationTransform.GetChild(1).GetComponent<SpriteRenderer>();
        energyGainEffect = GetComponent<ParticleSystem>();
        energyGainEffectMain = energyGainEffect.main;
        //squardPositions = new List<Vector3>();
        //if (stationCurrentLevel>0) gunSphereParentTransform = gunSphereParent.transform;
        //fleetGatherRadius = 9f;//TO DELETE CAUSE IS ASSIGNED WHILE INSTANTIATING
        //radiusOfShipsRingAroundStation = 6;//TO DELETE CAUSE IS ASSIGNED WHILE INSTANTIATING
        //stationCurrentLevel = 0;//TO DELETE CAUSE IS ASSIGNED WHILE INSTANTIATING
        //upgradeCounts = 0;//TO DELETE CAUSE IS ASSIGNED WHILE INSTANTIATING
        //energyToNextUpgradeOfStation = 0;//TO DELETE CAUSE IS ASSIGNED WHILE INSTANTIATING
        //stationGunLevel = 0;//TO DELETE CAUSE IS ASSIGNED WHILE INSTANTIATING
        //energyToNextUpgradeOfGun = 0;//TO DELETE CAUSE IS ASSIGNED WHILE INSTANTIATING
        //GunUpgradeCounts = 0;//TO DELETE CAUSE IS ASSIGNED WHILE INSTANTIATING
        //Cruis4 = 0;//TO DELETE CAUSE IS ASSIGNED WHILE INSTANTIATING
        //Destr4 = 0;//TO DELETE CAUSE IS ASSIGNED WHILE INSTANTIATING
        //energyOfStation = 11;//TO DELETE CAUSE IS ASSIGNED WHILE INSTANTIATING
        //ShipsAssigned = 0;//TO DELETE CAUSE IS ASSIGNED WHILE INSTANTIATING
        //energyRequiredToShot = 10;//TO DELETE CAUSE IS ASSIGNED WHILE INSTANTIATING
        //energyInscreaseTime = 5f;//TO DELETE CAUSE IS ASSIGNED WHILE INSTANTIATING
        //energyInscreaseTimer = energyInscreaseTime;
        //energonCatchDistance = 30f; //TO DELETE CAUSE IS ASSIGNED WHILE INSTANTIATING
        //speedOfBullet = 10f;//TO DELETE CAUSE IS ASSIGNED WHILE INSTANTIATING
        //colorToEnergy = 25f;//TO DELETE CAUSE IS ASSIGNED WHILE INSTANTIATING
        //ShipsLimit = 16; //TO DELETE CAUSE IS ASSIGNED WHILE INSTANTIATING
        //fillingSpeed = 1;//TO DELETE CAUSE IS ASSIGNED WHILE INSTANTIATING
    }

    public virtual void pullTheEnergyFromEnergons() { }

    public virtual void  utilaizeTheEnergy(bool isRecursionCall)
    {
    }
    public virtual void utilaizeTheEnergyOfCPUGroup(int useOfEnergy)
    {
    }
    public virtual void checkIfStationCanConnect() { 
    
    }

    public float distanceToEnergon()
    {
        return (closestEnegon.energonTransform.position - stationPosition).magnitude;
    }


    public void disactivateThisStation(StationClass newStation)
    {
        //player station. It clears the maternal station on ships or reassign the upgraded stations to them
        if (CPUNumber == 0)
        {
            for (int i = 0; i < CommonProperties.playerBattleShips.Count; i++)
                if (CommonProperties.playerBattleShips[i].maternalStation == this) CommonProperties.playerBattleShips[i].maternalStation = newStation;
            if (CommonProperties.stationPanelScript.station == this) CommonProperties.stationPanelScript.closeThePanel(true); //also closes the panel if it is open for destroyed of upgraded station
        }
        //CPU stations. does the same
        else
        {
            for (int i = 0; i < CommonProperties.CPUBattleShipsDictionary[CPUNumber - 1].Count; i++)
                if (CommonProperties.CPUBattleShipsDictionary[CPUNumber - 1][i].maternalStation == this) CommonProperties.CPUBattleShipsDictionary[CPUNumber - 1][i].maternalStation = newStation;
        }

        //bull means destroying the station 
        if (newStation == null)
        {
            ObjectPulledList = ObjectPullerRTS.current.GetStationBurstPull();
            ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
            ObjectPulled.transform.position = stationPosition;
            ObjectPulled.SetActive(true);

            ObjectPulledList = ObjectPullerRTS.current.GetStarPull(upgradeCounts);
            ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
            CommonProperties.stars.Add(ObjectPulled.GetComponent<StarController>());
            ObjectPulled.transform.position = stationPosition;
            ObjectPulled.SetActive(true);
            for (int i = 0; i < CommonProperties.CPUStations.Count; i++) CommonProperties.CPUStations[i].giveAnOrderToFleet();

            //regrouping the stations in the group of this station
            if (groupWhereTheStationIs != null /*&& groupWhereTheStationIs.Count > 0*/)
            {
                int energyOfGroupBuffer = 0;
                //closing the panel if station of player is destroyed and if it was connected to some
                if (CPUNumber == 0 && CommonProperties.stationPanelIsActive && groupWhereTheStationIs !=null) CommonProperties.stationPanelScript.closeThePanel(false);

                //sending the signal of cutting the connection with old station to other stations and set null to their group references cause the group each time is regrouped
                foreach (StationClass stationsConnectedToOldStation in ConnectedStations)
                {
                    stationsConnectedToOldStation.ConnectedStations.Remove(this);
                }

                //reduce the energy of group
                CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= energyLoseIfDestroyed;
                if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] < 0) CommonProperties.energyOfStationGroups[groupWhereTheStationIs] = 0;
                else energyOfGroupBuffer = CommonProperties.energyOfStationGroups[groupWhereTheStationIs];

                
                //removing the station group from master group and from energy groups because new set of groups should be established
                CommonProperties.StationGroups[CPUNumber].Remove(groupWhereTheStationIs);
                CommonProperties.energyOfStationGroups.Remove(groupWhereTheStationIs);
                CommonProperties.energyLimitOfStationGroups.Remove(groupWhereTheStationIs);

                //erasing this group reference from the ather stations in group
                foreach (StationClass stationsInGroupOfDestroyingStation in groupWhereTheStationIs)
                    if (stationsInGroupOfDestroyingStation != this) stationsInGroupOfDestroyingStation.groupWhereTheStationIs = null;


                //removing the lines that were connected with destroying station
                for (int i = 0; i < CommonProperties.connectionLines[CPUNumber].Count; i++)
                {
                    if (CommonProperties.connectionLines[CPUNumber][i].stations.Contains(this))
                    {
                        CommonProperties.connectionLines[CPUNumber][i].disactivateThisLine(CPUNumber/*, this*/);
                        i--;
                    }
                }
                regroupTheStationsAfterOneDestroyed(energyOfGroupBuffer);

                //    if (groupWhereTheStationIs.Count > 2)
                //{
                //    CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.energyOfStationGroups[groupWhereTheStationIs] / groupWhereTheStationIs.Count;
                //    //updating the panel info if it is opened by player
                //    if (CPUNumber == 0 && CommonProperties.stationPanelIsActive) CommonProperties.stationPanelScript.updateVariablesAfterEnergyChanges();
                //}

            }

        }
        //else
        //{
        //    if (groupWhereTheStationIs != null /*&& groupWhereTheStationIs.Count > 0*/)
        //    {
                
        //    }
        //}
        if (gunSphereVisible != null) gunSphereVisible.SetActive(false);
        //if (groupWhereTheStationIs != null /*&& groupWhereTheStationIs.Count > 0*/)
        //{
        //    //groupWhereTheStationIs.Remove(this);
        //    //deleting the group of stations if there left no any stations in group or there left one station only
        //    if (groupWhereTheStationIs.Count < 2)
        //    {
        //        CommonProperties.StationGroups[CPUNumber].Remove(groupWhereTheStationIs);
        //        CommonProperties.energyOfStationGroups.Remove(groupWhereTheStationIs);
        //        if (groupWhereTheStationIs.Count==1) groupWhereTheStationIs[0].groupWhereTheStationIs = null;
        //    }
        //    groupWhereTheStationIs = null;
        //}
        groupWhereTheStationIs = null;
        ConnectedStations.Clear();
        gameObject.SetActive(false);
        lifeLineAmount = 0;

        if (newStation == null) GameController.current.checkIfPlayerWinOrLost(); //win/lost conditions checked only if station is destroyed
    }

    public void regroupTheStationsAfterOneDestroyed(int energyOfDiedGroup)
    { 
        //regrouping all the station of this player (CPUNumber)
      
        //check if there left only one connection or more
        if (CommonProperties.connectionLines[CPUNumber].Count == 1)
        {
            if (CommonProperties.connectionLines[CPUNumber][0].stations[0].groupWhereTheStationIs == null && CommonProperties.connectionLines[CPUNumber][0].stations[1].groupWhereTheStationIs == null)
            {
                List<List<StationClass>> newGroup = new List<List<StationClass>>();
                List<StationClass> newConnection = new List<StationClass>();
                if (CommonProperties.StationGroups.ContainsKey(CPUNumber)) CommonProperties.StationGroups[CPUNumber].Add(newConnection);
                //so if there no station groups of this CPU it is first created
                else
                {
                    CommonProperties.StationGroups.Add(CPUNumber, newGroup);
                    newGroup.Add(newConnection);
                }
                newConnection.Add(CommonProperties.connectionLines[CPUNumber][0].stations[0]);
                CommonProperties.connectionLines[CPUNumber][0].stations[0].groupWhereTheStationIs = newConnection;
                newConnection.Add(CommonProperties.connectionLines[CPUNumber][0].stations[1]);
                CommonProperties.connectionLines[CPUNumber][0].stations[1].groupWhereTheStationIs = newConnection;
                CommonProperties.energyOfStationGroups.Add(newConnection, 0);

                //no need to add other energyes cause they are zero at this point since were zero while connection
                CommonProperties.energyOfStationGroups[newConnection] = energyOfDiedGroup;
                //setting the limit of energy for station group
                CommonProperties.energyLimitOfStationGroups.Add(newConnection, CommonProperties.getTheEnergyLimitOfStationsGroup(newConnection));
            }
            //!!! I THINK THAT THIS LINE IS USELESS CAUSE THERE CAN'T BE SINGLE LINE LEFT WITH DIFFERENT GROUP STATIONS
            else if (CommonProperties.connectionLines[CPUNumber][0].stations[0].groupWhereTheStationIs != null && CommonProperties.connectionLines[CPUNumber][0].stations[1].groupWhereTheStationIs != null)
            {
                //case if one station of line is in another group than second one. Adding one group to another and leaving only one of the groups
                if (CommonProperties.connectionLines[CPUNumber][0].stations[0].groupWhereTheStationIs != CommonProperties.connectionLines[CPUNumber][0].stations[1].groupWhereTheStationIs)
                {
                    List<StationClass> tempClass = CommonProperties.connectionLines[CPUNumber][0].stations[0].groupWhereTheStationIs;
                    foreach (StationClass station in tempClass)
                    {
                        CommonProperties.connectionLines[CPUNumber][0].stations[1].groupWhereTheStationIs.Add(station);
                        station.groupWhereTheStationIs = CommonProperties.connectionLines[CPUNumber][0].stations[1].groupWhereTheStationIs;
                        CommonProperties.energyOfStationGroups[station.groupWhereTheStationIs] += station.energyOfStation; //no need to add other energyes cause they are zero at this point since were zero while connection
                    }
                    CommonProperties.StationGroups[stationToConnect.CPUNumber].Remove(tempClass);
                    CommonProperties.energyOfStationGroups.Remove(tempClass);
                }
            }
        }
        else if (CommonProperties.connectionLines[CPUNumber].Count != 0)
        {
            //regrouping the stations that were in the group and that are still have connections
            for (int i = 0; i < CommonProperties.connectionLines[CPUNumber].Count; i++)
            {
                for (int y = 0; y < CommonProperties.connectionLines[CPUNumber].Count; y++)
                {
                    if (CommonProperties.connectionLines[CPUNumber][i] != CommonProperties.connectionLines[CPUNumber][y])
                    {
                        if (CommonProperties.connectionLines[CPUNumber][i].stations[0].groupWhereTheStationIs == null && CommonProperties.connectionLines[CPUNumber][i].stations[1].groupWhereTheStationIs == null)
                        {
                            List<List<StationClass>> newGroup = new List<List<StationClass>>();
                            List<StationClass> newConnection = new List<StationClass>();
                            if (CommonProperties.StationGroups.ContainsKey(CPUNumber)) CommonProperties.StationGroups[CPUNumber].Add(newConnection);
                            //so if there no station groups of this CPU it is first created
                            else
                            {
                                CommonProperties.StationGroups.Add(CPUNumber, newGroup);
                                newGroup.Add(newConnection);
                            }
                            newConnection.Add(CommonProperties.connectionLines[CPUNumber][i].stations[0]);
                            CommonProperties.connectionLines[CPUNumber][i].stations[0].groupWhereTheStationIs = newConnection;
                            newConnection.Add(CommonProperties.connectionLines[CPUNumber][i].stations[1]);
                            CommonProperties.connectionLines[CPUNumber][i].stations[1].groupWhereTheStationIs = newConnection;
                            CommonProperties.energyOfStationGroups.Add(newConnection, 0);
                            //no need to add other energyes cause they are zero at this point since were not enough while connection
                            CommonProperties.energyOfStationGroups[newConnection] = newConnection[0].energyOfStation + newConnection[1].energyOfStation; 
                            //setting the limit of energy for station group
                            CommonProperties.energyLimitOfStationGroups.Add(newConnection, CommonProperties.getTheEnergyLimitOfStationsGroup(newConnection));

                            //start to check adjacent lines if there is some
                            if (CommonProperties.connectionLines[CPUNumber][i].stations.Contains(CommonProperties.connectionLines[CPUNumber][y].stations[0]) ||
                                CommonProperties.connectionLines[CPUNumber][i].stations.Contains(CommonProperties.connectionLines[CPUNumber][y].stations[1]))
                            {
                                foreach (StationClass station in CommonProperties.connectionLines[CPUNumber][y].stations)
                                {
                                    if (!CommonProperties.connectionLines[CPUNumber][i].stations[0].groupWhereTheStationIs.Contains(station) && station.groupWhereTheStationIs == null)
                                    {
                                        station.groupWhereTheStationIs = CommonProperties.connectionLines[CPUNumber][i].stations[0].groupWhereTheStationIs;
                                        CommonProperties.connectionLines[CPUNumber][i].stations[0].groupWhereTheStationIs.Add(station);
                                    }
                                }
                            }


                        }
                        else if (CommonProperties.connectionLines[CPUNumber][i].stations[0].groupWhereTheStationIs == null)
                        {
                            //adding the null-group station to group of stations of second (not null-group) station
                            CommonProperties.connectionLines[CPUNumber][i].stations[0].groupWhereTheStationIs = CommonProperties.connectionLines[CPUNumber][i].stations[1].groupWhereTheStationIs;

                            //start to check adjacent lines if there is some
                            if (CommonProperties.connectionLines[CPUNumber][i].stations.Contains(CommonProperties.connectionLines[CPUNumber][y].stations[0]) ||
                                CommonProperties.connectionLines[CPUNumber][i].stations.Contains(CommonProperties.connectionLines[CPUNumber][y].stations[1]))
                            {
                                foreach (StationClass station in CommonProperties.connectionLines[CPUNumber][y].stations)
                                {
                                    if (!CommonProperties.connectionLines[CPUNumber][i].stations[0].groupWhereTheStationIs.Contains(station) && station.groupWhereTheStationIs == null)
                                    {
                                        station.groupWhereTheStationIs = CommonProperties.connectionLines[CPUNumber][i].stations[0].groupWhereTheStationIs;
                                        CommonProperties.connectionLines[CPUNumber][i].stations[0].groupWhereTheStationIs.Add(station);
                                    }
                                }
                            }
                        }
                        else if (CommonProperties.connectionLines[CPUNumber][i].stations[1].groupWhereTheStationIs == null)
                        {
                            //adding the null-group station to group of stations of second (not null-group) station
                            CommonProperties.connectionLines[CPUNumber][i].stations[1].groupWhereTheStationIs = CommonProperties.connectionLines[CPUNumber][i].stations[0].groupWhereTheStationIs;
                            CommonProperties.connectionLines[CPUNumber][i].stations[0].groupWhereTheStationIs.Add(CommonProperties.connectionLines[CPUNumber][i].stations[1]);

                            //start to check adjacent lines if there is some
                            if (CommonProperties.connectionLines[CPUNumber][i].stations.Contains(CommonProperties.connectionLines[CPUNumber][y].stations[0]) ||
                                CommonProperties.connectionLines[CPUNumber][i].stations.Contains(CommonProperties.connectionLines[CPUNumber][y].stations[1]))
                            {
                                foreach (StationClass station in CommonProperties.connectionLines[CPUNumber][y].stations)
                                {
                                    if (!CommonProperties.connectionLines[CPUNumber][i].stations[0].groupWhereTheStationIs.Contains(station) && station.groupWhereTheStationIs == null)
                                    {
                                        station.groupWhereTheStationIs = CommonProperties.connectionLines[CPUNumber][i].stations[0].groupWhereTheStationIs;
                                        CommonProperties.connectionLines[CPUNumber][i].stations[0].groupWhereTheStationIs.Add(station);
                                    }
                                }
                            }
                        }
                        else if (CommonProperties.connectionLines[CPUNumber][i].stations[0].groupWhereTheStationIs != null && CommonProperties.connectionLines[CPUNumber][i].stations[1].groupWhereTheStationIs != null)
                        {
                            //case if one station of line is in another group than second one. Adding one group to another and leaving only one of the groups
                            if (CommonProperties.connectionLines[CPUNumber][i].stations[0].groupWhereTheStationIs != CommonProperties.connectionLines[CPUNumber][i].stations[1].groupWhereTheStationIs)
                            {
                                List<StationClass> tempClass = CommonProperties.connectionLines[CPUNumber][i].stations[0].groupWhereTheStationIs;
                                foreach (StationClass station in tempClass)
                                {
                                    CommonProperties.connectionLines[CPUNumber][i].stations[1].groupWhereTheStationIs.Add(station);
                                    station.groupWhereTheStationIs = CommonProperties.connectionLines[CPUNumber][i].stations[1].groupWhereTheStationIs;
                                    CommonProperties.energyOfStationGroups[station.groupWhereTheStationIs] += station.energyOfStation; //no need to add other energyes cause they are zero at this point since were zero while connection
                                }
                                CommonProperties.StationGroups[stationToConnect.CPUNumber].Remove(tempClass);
                                CommonProperties.energyOfStationGroups.Remove(tempClass);
                            }

                            //start to check adjacent lines if there is some
                            if (CommonProperties.connectionLines[CPUNumber][i].stations.Contains(CommonProperties.connectionLines[CPUNumber][y].stations[0]) ||
                                CommonProperties.connectionLines[CPUNumber][i].stations.Contains(CommonProperties.connectionLines[CPUNumber][y].stations[1]))
                            {
                                foreach (StationClass station in CommonProperties.connectionLines[CPUNumber][y].stations)
                                {
                                    if (!CommonProperties.connectionLines[CPUNumber][i].stations[0].groupWhereTheStationIs.Contains(station) && station.groupWhereTheStationIs == null)
                                    {
                                        station.groupWhereTheStationIs = CommonProperties.connectionLines[CPUNumber][i].stations[0].groupWhereTheStationIs;
                                        CommonProperties.connectionLines[CPUNumber][i].stations[0].groupWhereTheStationIs.Add(station);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void increaseTheHPOfStation(float energyAmount)
    {
        lifeLineAmount += (energyAmount / 500)* (2-fillingSpeed);
        if (lifeLineAmount > 0)
        {
            lifeLineAmount = 0;
        }
        fillingLine.localPosition = new Vector3(lifeLineAmount, 0, 0);
    }
    public void resetTheClosestEnergon()
    {
        if (CommonProperties.energonsOnScene.Count > 1)
        {
            for (int i = 0; i < CommonProperties.energonsOnScene.Count; i++)
            {
                if (i == 0) closestEnegon = CommonProperties.energonsOnScene[i];
                else
                {
                    if (distanceToEnergon() > (CommonProperties.energonsOnScene[i].energonTransform.position - stationPosition).magnitude) closestEnegon = CommonProperties.energonsOnScene[i];
                }
            }
        }
        else if (CommonProperties.energonsOnScene.Count == 1) closestEnegon = CommonProperties.energonsOnScene[0];
    }


    public virtual int stationDefenceFleetPower()
    {
        return 0;
    }

    public virtual int stationDefenceFleetLeftByUnits()
    {
        return 0;
    }

    //used only by CPU Station
    public virtual void callForAHelp() { 
    
    }

    ////Used by CPU statuin only
    //public virtual void gatherTheReferencesToShipsOfStation() { 
    //}

    //Used by CPU statuin only
    public virtual void sendTheFleetToThePoint(Vector3 startPoint, Vector3 destinationPoint) { 
    }

    public void reduceTheHPOfStation(float fillAmount)
    {
        lifeLineAmount -= fillAmount * fillingSpeed;
        if (lifeLineAmount <= -6)
        {
            lifeLineAmount = -6;
            disactivateThisStation(null);
        }
        fillingLine.localPosition = new Vector3(lifeLineAmount, 0, 0);
    }
}
