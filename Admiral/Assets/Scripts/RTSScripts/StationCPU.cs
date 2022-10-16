using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;

public class StationCPU : StationClass
{
    //private EnergonMoving energonOnScene;
    //[HideInInspector]
    //public Transform stationTransform;
    //private Vector3 stationPosition;
    //[HideInInspector]
    //public float energonCatchDistance; //TO ASSIGN WHILE depending on the lvl of station
    //[HideInInspector]
    //public float speedOfBullet; //TO ASSIGN WHILE PULLLING depending on the lvl of station
    //[HideInInspector]
    //public float colorToEnergy; //TO ASSIGN WHILE PULLLING depending on the lvl of station (this is constant to transfer the color intensity to energy amount)
    //[HideInInspector]
    //public bool shotIsMade; //TO ASSIGN WITH RESPAWN OF ENERGON ON SCENE
    //[HideInInspector]
    //public int CPUNumber; //TO ASSIGN WHILE PULLLING depending on the lvl of station

    //private GameObject BulletPulled;
    //private List<GameObject> BulletPulledList;

    //[HideInInspector]
    //public float energyOfStation;
    //[HideInInspector]
    //public float energyInscreaseTime; //TO ASSIGN WITH RESPAWN OF ENERGON ON SCENE

    private int energyOfStationToUPGrade;

    private float radiusGroup;
    private int innnerCircleMax;

    public const int BASE_STATION_DEFENCE_SHIPS_COUNT = 2; //6;
    private const int SHIPS_COUNT_MINIMUM_TO_ATTACK = 10;

    public List<GameObject> stationColorSphere; //parts of station demostrate the color of station

    private List<BattleShipClass> closeBattleShips;
    private BattleShipClass shipToAttak;

    private List<Vector3> squardPositions;

    [SerializeField]
    private CPUGun gunClass;

    private StationClass stationToAttack;

    private const float nearHexMaxDistance = 65f;
    List<Vector3> wayDots;

    //public bool canSetConnection;

    //private List<CPUBattleShip> shipsOfStationThatNear;

    // Start is called before the first frame update
    void Start()
    {
              //energyOfStation = 0;
              //shotIsMade = false;
              //ShipsAssigned = 0;
              //energonOnScene = FindObjectOfType<EnergonMoving>();
              //stationTransform = transform;
              //stationPosition = stationTransform.position;

              //CPUNumber = 1;
              attackDistance = CommonProperties.attackDistanceForStations;
        closeBattleShips = new List<BattleShipClass>();
        attackLaserLine = GetComponent<LineRenderer>();
        attackLaserLine.positionCount = 2;

        energyPullLine = stationTransform.GetChild(0).GetComponent<LineRenderer>();

        //TO FINISH WITH ALL TYPES OF STATIONS
        if (name.Contains("0"))
        {
            attackTimeMin = CommonProperties.cruiser4MinAttackTime;
            attackTimeMax = CommonProperties.cruiser4MaxAttackTime;
            harm = CommonProperties.Cruiser4Harm+3;
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
        energyOfStationToUPGrade = 0;
        energyOfStationToUPGradeGun = 0;
        energyOfStationToUPGradeStation = 0; //TO ST 0 FOR TEST ONLY

        //energyGainEffect = GetComponent<ParticleSystem>();
        //energyGainEffectMain = energyGainEffect.main;
        //CPUNumber = 1;//TO DELETE CAUSE IS ASSIGNED WHILE INSTANTIATING
    }
    private void OnEnable()
    {
        stationToConnect = null;
        wayDots = new List<Vector3>();
        if (stationCurrentLevel > 0) gunSphereParentTransform = gunSphereParent.transform;
        squardPositions = new List<Vector3>();
        shipsToGiveOrderCommandOfStation = new List<CPUBattleShip>();
        //shipsOfStationThatNear = new List<CPUBattleShip>();
        innnerCircleMax = 8; //the first circle around the ship that is on center of squard
        radiusGroup = 3;
        //to prevent a bug of spawning additional pulled object (automatic will grow station that copy of pulled object with 0 index which can be already active and with active gun) with active gun 
        if (gunSphereVisible != null && gunSphereVisible.activeInHierarchy) gunSphereVisible.SetActive(false);
    }
    private void OnDisable()
    {
        //if (stationBullet != null) stationBullet.SetActive(false);
        //if CPUNumber !=0 checks if this instance of class is only pulled object of not (so CPUNumber == 0 is default value and that means it is only pulled object)
        if (CPUNumber != 0)
        {
            CommonProperties.allStations.Remove(this); 
            CommonProperties.CPUStations.Remove(this); 
            CommonProperties.CPUStationsDictionary[CPUNumber - 1].Remove(this);
        }
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
        territoryLine.color = colorOfStationMat;
        energyGainEffectMain.startColor = colorOfStationMat;
    }

    //IEnumerator makeAShotFromStation()
    //{
    //    yield return new WaitForSeconds(Random.Range(0, 1f));
    //    if (distanceToEnergon() < energonCatchDistance) {
    //        if (groupWhereTheStationIs != null /*&& groupWhereTheStationIs.Count > 0*/)
    //        {
    //            if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] >= energyRequiredToShot)
    //            {
    //                CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= energyRequiredToShot;
    //                ObjectPulledList = ObjectPullerRTS.current.GetStationBulletPull();
    //                ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
    //                stationBullet = ObjectPulled;
    //                ObjectPulled.transform.position = stationPosition;
    //                ObjectPulled.GetComponent<StationBullet>().setTheAimAndStation(closestEnegon.energonTransform, stationTransform.position, this, colorToEnergy, speedOfBullet, energonCatchDistance, false/*, CPUNumber*/);
    //                ObjectPulled.SetActive(true);
    //            }
    //            else shotIsMade = false;
    //        }
    //        else if (energyOfStation >= energyRequiredToShot)
    //        {
    //            energyOfStation -= energyRequiredToShot;
    //            ObjectPulledList = ObjectPullerRTS.current.GetStationBulletPull();
    //            ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
    //            stationBullet = ObjectPulled;
    //            ObjectPulled.transform.position = stationPosition;
    //            ObjectPulled.GetComponent<StationBullet>().setTheAimAndStation(closestEnegon.energonTransform, stationTransform.position, this, colorToEnergy, speedOfBullet, energonCatchDistance, false/*, CPUNumber*/);
    //            ObjectPulled.SetActive(true);
    //        }
    //        else shotIsMade = false;
    //    }
    //    else shotIsMade = false;
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

            }
            else
            {
                //this is the trigger for finishing the energy gather and start for utilize it
                if (energyPullLine.enabled)
                {
                    energyPullLine.enabled = false;
                    if (groupWhereTheStationIs != null) ConnectionCPUStations.distributeGroupEnergy(groupWhereTheStationIs);
                    else utilaizeTheEnergy(false);
                }
            }
        }
    }

    public override void utilaizeTheEnergy(bool isRecursionCall)
    { 
        //int energyLeft=0;

        //holding the energy to upgrade the station itself and building or upgrading the gun of station
        //if (!isRecursionCall)
        //{
        //    checkIfStationCanConnect();
        //    if (stationCurrentLevel < upgradeCounts || stationGunLevel < GunUpgradeCounts || stationToConnect!=null)
        //    {
        //        //20/80 math means 80% of energy goes to upgrade or set connections
        //        if (stationToConnect!=null) energyLeft = (int)(energyOfStation * 0.2f); //so if there is some station to connect around it will hold more to set connection
        //        else energyLeft = (int)(energyOfStation * 0.8f); //if no station to connect station act more aggressive
        //        energyOfStationToUPGrade += energyOfStation - energyLeft;
        //        energyOfStation = energyLeft;

        //    }
        //    //utilizing the upgrade energy to upgrade the station itself or upgrade the gun or to set connections (which has highest priority)
        //    if (stationToConnect != null)
        //    {
        //        energyOfStationToSetConnection += energyOfStationToUPGrade;
        //        energyOfStationToUPGrade = 0;
        //        //if (stationCurrentLevel < upgradeCounts)
        //        //{
        //        //    if (stationGunLevel < GunUpgradeCounts)
        //        //    {
        //        //        energyOfStationToSetConnection += (int)(energyOfStationToUPGrade *0.7f);
        //        //        energyOfStationToUPGrade -= energyOfStationToSetConnection; //decrease the temp energy reserved to upgrade matters by energy to connect to use the left for upgrade station or gun
        //        //        energyOfStationToUPGradeStation += (int)(energyOfStationToUPGrade * 0.6f);
        //        //        energyOfStationToUPGradeGun += (int)(energyOfStationToUPGrade * 0.4f);

        //        //        energyOfStationToUPGrade = 0;
        //        //    }
        //        //    else
        //        //    {
        //        //        energyOfStationToSetConnection += (int)(energyOfStationToUPGrade * 0.7f);
        //        //        energyOfStationToUPGradeStation += (int)(energyOfStationToUPGrade * 0.3f);
        //        //        energyOfStationToUPGrade = 0;
        //        //    }
        //        //}
        //        //else if (stationGunLevel < GunUpgradeCounts)
        //        //{
        //        //    energyOfStationToSetConnection += (int)(energyOfStationToUPGrade * 0.8f);
        //        //    energyOfStationToUPGradeGun += (int)(energyOfStationToUPGrade * 0.2f);
        //        //    energyOfStationToUPGrade = 0;
        //        //}
        //    }
        //    else {
        //        if (stationCurrentLevel < upgradeCounts)
        //        {
        //            if (stationGunLevel < GunUpgradeCounts)
        //            {
        //                energyOfStationToUPGradeStation += (int)(energyOfStationToUPGrade * 0.8f);
        //                energyOfStationToUPGradeGun += (int)(energyOfStationToUPGrade * 0.2f);

        //                energyOfStationToUPGrade = 0;
        //            }
        //            else
        //            {
        //                energyOfStationToUPGradeStation += energyOfStationToUPGrade;
        //                energyOfStationToUPGrade = 0;
        //            }
        //        }
        //        else if (stationGunLevel < GunUpgradeCounts)
        //        {
        //            energyOfStationToUPGradeGun += energyOfStationToUPGrade;
        //            energyOfStationToUPGrade = 0;
        //        }
        //    }

        //    if (energyOfStationToSetConnection >= energyToConnection && stationToConnect != null)
        //    {
        //        energyOfStationToSetConnection -= energyToConnection;
        //        ConnectionCPUStations.setConnections(this, stationToConnect);
        //        stationToConnect = null;
        //    }
        //    //utilizing the energy to upgrade
        //    if (energyOfStationToUPGradeStation >= energyToNextUpgradeOfStation && stationCurrentLevel < upgradeCounts)
        //    {
        //        energyOfStationToUPGradeStation -= energyToNextUpgradeOfStation;
        //        //transfering the energy left after upgrade of station to gun upgrade energy balance, or further to energy of station
        //        if (energyOfStationToUPGradeStation > 0 && (stationCurrentLevel+1) == upgradeCounts)
        //        {
        //            if (stationGunLevel < GunUpgradeCounts)
        //            {
        //                energyOfStationToUPGradeGun += energyOfStationToUPGradeStation;
        //                energyOfStationToUPGradeStation = 0;
        //            }
        //            else
        //            {
        //                energyOfStation += (int)energyOfStationToUPGradeStation;
        //                energyOfStationToUPGradeStation = 0;
        //            }
        //        }
        //        upgradeStation(stationCurrentLevel + 1);
        //        return; //breaking this method
        //    }
        //    //gun upgrade goes second
        //    if (energyOfStationToUPGradeGun >= energyToNextUpgradeOfGun && stationGunLevel < GunUpgradeCounts)
        //    {
        //        energyOfStationToUPGradeGun -= energyToNextUpgradeOfGun;
        //        if (energyOfStationToUPGradeGun > 0)
        //        {
        //            energyOfStation += (int)energyOfStationToUPGradeGun; //transfering the energy left ufter upgrade of gun to energy balance
        //            energyOfStationToUPGradeGun = 0;
        //        }
        //        upgradeTheGun(stationGunLevel + 1);
        //    }

        //}

        if (!isRecursionCall)
        {
            checkIfStationCanConnect();

            //all energy to produce the ships first
            if ((stationCurrentLevel < upgradeCounts || stationGunLevel < GunUpgradeCounts || stationToConnect != null) && ShipsLimit == ShipsAssigned)
            {
                //20/80 math means 80% of energy goes to upgrade or set connections
                //if (stationToConnect != null) energyLeft = (int)(energyOfStation * 0.2f); //so if there is some station to connect around it will hold more to set connection
                //else energyLeft = (int)(energyOfStation * 0.8f); //if no station to connect station act more aggressive
                //energyOfStationToUPGrade += energyOfStation - energyLeft;
                //energyOfStation = energyLeft;

                //send all energy to upgrade matters
                energyOfStationToUPGrade = energyOfStation;
                energyOfStation = 0;
            }
            //utilizing the upgrade energy to upgrade the station itself or upgrade the gun or to set connections (which has highest priority)
            if (stationToConnect != null)
            {
                energyOfStationToSetConnection += energyOfStationToUPGrade;
                energyOfStationToUPGrade = 0;
                
            }
            else
            {
                if (stationCurrentLevel < upgradeCounts)
                {
                    if (stationGunLevel < GunUpgradeCounts)
                    {
                        energyOfStationToUPGradeStation += (int)(energyOfStationToUPGrade * 0.8f);
                        energyOfStationToUPGradeGun += (int)(energyOfStationToUPGrade * 0.2f);

                        energyOfStationToUPGrade = 0;
                    }
                    else
                    {
                        energyOfStationToUPGradeStation += energyOfStationToUPGrade;
                        energyOfStationToUPGrade = 0;
                    }
                }
                else if (stationGunLevel < GunUpgradeCounts)
                {
                    energyOfStationToUPGradeGun += energyOfStationToUPGrade;
                    energyOfStationToUPGrade = 0;
                }
            }

            if (energyOfStationToSetConnection >= energyToConnection && stationToConnect != null)
            {
                energyOfStationToSetConnection -= energyToConnection;
                ConnectionCPUStations.setConnections(this, stationToConnect);
                stationToConnect = null;
            }
            //utilizing the energy to upgrade
            if (energyOfStationToUPGradeStation >= energyToNextUpgradeOfStation && stationCurrentLevel < upgradeCounts)
            {
                energyOfStationToUPGradeStation -= energyToNextUpgradeOfStation;
                //transfering the energy left after upgrade of station to gun upgrade energy balance, or further to energy of station
                if (energyOfStationToUPGradeStation > 0 && (stationCurrentLevel + 1) == upgradeCounts)
                {
                    if (stationGunLevel < GunUpgradeCounts)
                    {
                        energyOfStationToUPGradeGun += energyOfStationToUPGradeStation;
                        energyOfStationToUPGradeStation = 0;
                    }
                    else
                    {
                        energyOfStation += (int)energyOfStationToUPGradeStation;
                        energyOfStationToUPGradeStation = 0;
                    }
                }
                upgradeStation(stationCurrentLevel + 1);
                return; //breaking this method
            }
            //gun upgrade goes second
            if (energyOfStationToUPGradeGun >= energyToNextUpgradeOfGun && stationGunLevel < GunUpgradeCounts)
            {
                energyOfStationToUPGradeGun -= energyToNextUpgradeOfGun;
                if (energyOfStationToUPGradeGun > 0)
                {
                    energyOfStation += (int)energyOfStationToUPGradeGun; //transfering the energy left ufter upgrade of gun to energy balance
                    energyOfStationToUPGradeGun = 0;
                }
                upgradeTheGun(stationGunLevel + 1);
            }

        }
        produceTheShips();

    }

    private void produceTheShips()
    {
        if (energyOfStation > 0)
        {
            if (stationCurrentLevel == 0)
            {
                if (ShipsLimit > ShipsAssigned)
                {
                    if (Random.Range(0, 2) > 0)
                    {
                        if (energyOfStation > CommonProperties.C4ProdEnergy)
                        {
                            energyOfStation -= CommonProperties.C4ProdEnergy;
                            launcheNewShip(true, 4);
                            //Cruis4++;
                            ShipsAssigned++;

                        }
                        else if (energyOfStation > CommonProperties.D4ProdEnergy)
                        {
                            energyOfStation -= CommonProperties.D4ProdEnergy;
                            launcheNewShip(false, 4);
                            //Destr4++;
                            ShipsAssigned++;

                        }
                    }

                    else
                    {
                        if (energyOfStation > CommonProperties.D4ProdEnergy)
                        {
                            energyOfStation -= CommonProperties.D4ProdEnergy;
                            launcheNewShip(false, 4);
                            //Destr4++;
                            ShipsAssigned++;

                        }
                    }
                }
                if (energyOfStation > CommonProperties.D4ProdEnergy && ShipsLimit > ShipsAssigned) utilaizeTheEnergy(true);
                else
                {
                    allignTheShipsAroundStation(); //put ships around station after the last energy unit is utilized
                    giveAnOrderToFleet();
                }
            }
            else if (stationCurrentLevel == 1)
            {
                if (ShipsLimit > ShipsAssigned)
                {
                    if (Random.Range(0, 2) > 0)
                    {
                        if (energyOfStation > CommonProperties.C3ProdEnergy)
                        {
                            energyOfStation -= CommonProperties.C3ProdEnergy;
                            launcheNewShip(true, 3);
                            //Cruis3++;
                            ShipsAssigned++;
                        }
                        else if (energyOfStation > CommonProperties.D3ProdEnergy)
                        {
                            energyOfStation -= CommonProperties.D3ProdEnergy;
                            launcheNewShip(false, 3);
                            //Destr3++;
                            ShipsAssigned++;
                        }
                    }
                    else
                    {
                        if (energyOfStation > CommonProperties.D3ProdEnergy)
                        {
                            energyOfStation -= CommonProperties.D3ProdEnergy;
                            launcheNewShip(false, 3);
                            //Destr3++;
                            ShipsAssigned++;
                        }
                    }
                }
                if (energyOfStation > CommonProperties.D3ProdEnergy && ShipsLimit > ShipsAssigned) utilaizeTheEnergy(true);
                else
                {
                    allignTheShipsAroundStation(); //put ships around station after the last energy unit is utilized
                    giveAnOrderToFleet();
                }
            }
            else if (stationCurrentLevel == 2)
            {
                if (ShipsLimit > ShipsAssigned)
                {
                    if (Random.Range(0, 2) > 0)
                    {
                        if (energyOfStation > CommonProperties.C2ProdEnergy)
                        {
                            energyOfStation -= CommonProperties.C2ProdEnergy;
                            launcheNewShip(true, 2);
                            //Cruis2++;
                            ShipsAssigned++;
                        }
                        else
                        {
                            if (UnityEngine.Random.Range(0, 2) > 0)
                            {
                                energyOfStation -= CommonProperties.D2ProdEnergy;
                                launcheNewShip(false, 2);
                                //Destr2++;
                                ShipsAssigned++;
                            }
                            else
                            {
                                energyOfStation -= CommonProperties.D2PProdEnergy;
                                launcheNewShip(false, 22);
                                //Destr2Par++;
                                ShipsAssigned++;
                            }
                        }
                    }
                    else if (energyOfStation > CommonProperties.D2ProdEnergy)
                    {
                        if (UnityEngine.Random.Range(0, 2) > 0)
                        {
                            energyOfStation -= CommonProperties.D2ProdEnergy;
                            launcheNewShip(false, 2);
                            //Destr2++;
                            ShipsAssigned++;
                        }
                        else
                        {
                            energyOfStation -= CommonProperties.D2PProdEnergy;
                            launcheNewShip(false, 22);
                            //Destr2Par++;
                            ShipsAssigned++;
                        }

                    }
                }

                if (energyOfStation > CommonProperties.D2ProdEnergy && ShipsLimit > ShipsAssigned) utilaizeTheEnergy(true);
                else
                {
                    allignTheShipsAroundStation(); //put ships around station after the last energy unit is utilized
                    giveAnOrderToFleet();
                }
            }
            else if (stationCurrentLevel == 3)
            {
                if (ShipsLimit > ShipsAssigned)
                {
                    if (UnityEngine.Random.Range(0, 2) > 0)
                    {
                        if (energyOfStation > CommonProperties.C1ProdEnergy)
                        {
                            energyOfStation -= CommonProperties.C1ProdEnergy;
                            launcheNewShip(true, 1);
                            //Cruis1++;
                            ShipsAssigned++;
                        }
                        else
                        {
                            if (UnityEngine.Random.Range(0, 2) > 0)
                            {
                                energyOfStation -= CommonProperties.D1ProdEnergy;
                                launcheNewShip(false, 1);
                                //Destr1++;
                                ShipsAssigned++;
                            }
                            else
                            {
                                energyOfStation -= CommonProperties.D1PProdEnergy;
                                launcheNewShip(false, 11);
                                //Destr1Par++;
                                ShipsAssigned++;
                            }
                        }
                    }

                    else if (energyOfStation > CommonProperties.D1ProdEnergy)
                    {
                        if (UnityEngine.Random.Range(0, 2) > 0)
                        {
                            energyOfStation -= CommonProperties.D1ProdEnergy;
                            launcheNewShip(false, 1);
                            //Destr1++;
                            ShipsAssigned++;
                        }
                        else
                        {
                            energyOfStation -= CommonProperties.D1PProdEnergy;
                            launcheNewShip(false, 11);
                            //Destr1Par++;
                            ShipsAssigned++;
                        }
                    }
                }
                if (energyOfStation > CommonProperties.D1ProdEnergy && ShipsLimit > ShipsAssigned) utilaizeTheEnergy(true);
                else
                {
                    allignTheShipsAroundStation(); //put ships around station after the last energy unit is utilized
                    giveAnOrderToFleet();
                }
            }
        }
    }

    //this function is called from energy ball energy pass, from ConnectionCPU class, from the CPUBattleShip class while it is destroyed, and from connection line class when energy transporter passes the energy
    // 1 - produce ship to defence minimum, 2 - produce ships to all energy to attack, 3 - upgrade station, 4 - upgrade gun
    public override void utilaizeTheEnergyOfCPUGroup(int useOfEnergy)
    {
        if (useOfEnergy == 3)
        {
            CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= energyToNextUpgradeOfStation;
            upgradeStation(stationCurrentLevel + 1);
        }
        if (useOfEnergy == 4)
        {
            CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= energyToNextUpgradeOfGun;
            upgradeTheGun(stationGunLevel + 1);
        }

        if (useOfEnergy == 1)
        {
            if (ShipsAssigned < BASE_STATION_DEFENCE_SHIPS_COUNT)
            {
                if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > 0)
                {
                    if (stationCurrentLevel == 0)
                    {
                        if (Random.Range(0, 2) > 0)
                        {
                            if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.C4ProdEnergy)
                            {
                                CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.C4ProdEnergy;
                                launcheNewShip(true, 4);
                                //Cruis4++;
                                ShipsAssigned++;

                            }
                            else if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.D4ProdEnergy)
                            {
                                CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.D4ProdEnergy;
                                launcheNewShip(false, 4);
                                //Destr4++;
                                ShipsAssigned++;

                            }
                        }

                        else
                        {
                            if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.D4ProdEnergy)
                            {
                                CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.D4ProdEnergy;
                                launcheNewShip(false, 4);
                                //Destr4++;
                                ShipsAssigned++;

                            }
                        }

                        if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.D4ProdEnergy && ShipsAssigned < BASE_STATION_DEFENCE_SHIPS_COUNT) utilaizeTheEnergyOfCPUGroup(1);
                        else
                        {
                            allignTheShipsAroundStation(); //put ships around station after the last energy unit is utilized
                        }
                    }
                    else if (stationCurrentLevel == 1)
                    {
                        if (Random.Range(0, 2) > 0)
                        {
                            if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.C3ProdEnergy)
                            {
                                CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.C3ProdEnergy;
                                launcheNewShip(true, 3);
                                //Cruis3++;
                                ShipsAssigned++;
                            }
                            else if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.D3ProdEnergy)
                            {
                                CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.D3ProdEnergy;
                                launcheNewShip(false, 3);
                                //Destr3++;
                                ShipsAssigned++;
                            }
                        }
                        else
                        {
                            if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.D3ProdEnergy)
                            {
                                CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.D3ProdEnergy;
                                launcheNewShip(false, 3);
                                //Destr3++;
                                ShipsAssigned++;
                            }
                        }

                        if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.D3ProdEnergy && ShipsAssigned < BASE_STATION_DEFENCE_SHIPS_COUNT) utilaizeTheEnergyOfCPUGroup(1);
                        else
                        {
                            allignTheShipsAroundStation(); //put ships around station after the last energy unit is utilized
                        }
                    }
                    else if (stationCurrentLevel == 2)
                    {
                        if (Random.Range(0, 2) > 0)
                        {
                            if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.C2ProdEnergy)
                            {
                                CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.C2ProdEnergy;
                                launcheNewShip(true, 2);
                                //Cruis2++;
                                ShipsAssigned++;
                            }
                            else
                            {
                                if (UnityEngine.Random.Range(0, 2) > 0)
                                {
                                    CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.D2ProdEnergy;
                                    launcheNewShip(false, 2);
                                    //Destr2++;
                                    ShipsAssigned++;
                                }
                                else
                                {
                                    CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.D2PProdEnergy;
                                    launcheNewShip(false, 22);
                                    //Destr2Par++;
                                    ShipsAssigned++;
                                }
                            }
                        }
                        else if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.D2ProdEnergy)
                        {
                            if (UnityEngine.Random.Range(0, 2) > 0)
                            {
                                CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.D2ProdEnergy;
                                launcheNewShip(false, 2);
                                //Destr2++;
                                ShipsAssigned++;
                            }
                            else
                            {
                                CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.D2PProdEnergy;
                                launcheNewShip(false, 22);
                                //Destr2Par++;
                                ShipsAssigned++;
                            }

                        }

                        if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.D2ProdEnergy && ShipsAssigned < BASE_STATION_DEFENCE_SHIPS_COUNT) utilaizeTheEnergyOfCPUGroup(1);
                        else
                        {
                            allignTheShipsAroundStation(); //put ships around station after the last energy unit is utilized
                        }
                    }

                    else if (stationCurrentLevel == 3)
                    {
                        if (UnityEngine.Random.Range(0, 2) > 0)
                        {
                            if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.C1ProdEnergy)
                            {
                                CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.C1ProdEnergy;
                                launcheNewShip(true, 1);
                                //Cruis1++;
                                ShipsAssigned++;
                            }
                            else
                            {
                                if (UnityEngine.Random.Range(0, 2) > 0)
                                {
                                    CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.D1ProdEnergy;
                                    launcheNewShip(false, 1);
                                    //Destr1++;
                                    ShipsAssigned++;
                                }
                                else
                                {
                                    CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.D1PProdEnergy;
                                    launcheNewShip(false, 11);
                                    //Destr1Par++;
                                    ShipsAssigned++;
                                }
                            }
                        }

                        else if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.D1ProdEnergy)
                        {
                            if (UnityEngine.Random.Range(0, 2) > 0)
                            {
                                CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.D1ProdEnergy;
                                launcheNewShip(false, 1);
                                //Destr1++;
                                ShipsAssigned++;
                            }
                            else
                            {
                                CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.D1PProdEnergy;
                                launcheNewShip(false, 11);
                                //Destr1Par++;
                                ShipsAssigned++;
                            }
                        }

                        if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.D1ProdEnergy && ShipsAssigned < BASE_STATION_DEFENCE_SHIPS_COUNT) utilaizeTheEnergyOfCPUGroup(1);
                        else
                        {
                            allignTheShipsAroundStation(); //put ships around station after the last energy unit is utilized
                        }
                    }
                }
            }
        }
        if (useOfEnergy == 2)
        {
            if (ShipsAssigned < ShipsLimit)
            {
                if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > 0)
                {
                    if (stationCurrentLevel == 0)
                    {
                        if (Random.Range(0, 2) > 0)
                        {
                            if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.C4ProdEnergy)
                            {
                                CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.C4ProdEnergy;
                                launcheNewShip(true, 4);
                                //Cruis4++;
                                ShipsAssigned++;

                            }
                            else if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.D4ProdEnergy)
                            {
                                CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.D4ProdEnergy;
                                launcheNewShip(false, 4);
                                //Destr4++;
                                ShipsAssigned++;

                            }
                        }

                        else
                        {
                            if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.D4ProdEnergy)
                            {
                                CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.D4ProdEnergy;
                                launcheNewShip(false, 4);
                                //Destr4++;
                                ShipsAssigned++;

                            }
                        }

                        if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.D4ProdEnergy && ShipsLimit > ShipsAssigned) utilaizeTheEnergyOfCPUGroup(2);
                        else
                        {
                            allignTheShipsAroundStation(); //put ships around station after the last energy unit is utilized
                            giveAnOrderToFleet();
                        }
                    }
                    else if (stationCurrentLevel == 1)
                    {
                        if (Random.Range(0, 2) > 0)
                        {
                            if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.C3ProdEnergy)
                            {
                                CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.C3ProdEnergy;
                                launcheNewShip(true, 3);
                                //Cruis3++;
                                ShipsAssigned++;
                            }
                            else if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.D3ProdEnergy)
                            {
                                CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.D3ProdEnergy;
                                launcheNewShip(false, 3);
                                //Destr3++;
                                ShipsAssigned++;
                            }
                        }
                        else
                        {
                            if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.D3ProdEnergy)
                            {
                                CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.D3ProdEnergy;
                                launcheNewShip(false, 3);
                                //Destr3++;
                                ShipsAssigned++;
                            }
                        }

                        if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.D3ProdEnergy && ShipsLimit > ShipsAssigned) utilaizeTheEnergyOfCPUGroup(2);
                        else
                        {
                            allignTheShipsAroundStation(); //put ships around station after the last energy unit is utilized
                            giveAnOrderToFleet();
                        }
                    }
                    else if (stationCurrentLevel == 2)
                    {
                        if (Random.Range(0, 2) > 0)
                        {
                            if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.C2ProdEnergy)
                            {
                                CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.C2ProdEnergy;
                                launcheNewShip(true, 2);
                                //Cruis2++;
                                ShipsAssigned++;
                            }
                            else
                            {
                                if (UnityEngine.Random.Range(0, 2) > 0)
                                {
                                    CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.D2ProdEnergy;
                                    launcheNewShip(false, 2);
                                    //Destr2++;
                                    ShipsAssigned++;
                                }
                                else
                                {
                                    CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.D2PProdEnergy;
                                    launcheNewShip(false, 22);
                                    //Destr2Par++;
                                    ShipsAssigned++;
                                }
                            }
                        }
                        else if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.D2ProdEnergy)
                        {
                            if (UnityEngine.Random.Range(0, 2) > 0)
                            {
                                CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.D2ProdEnergy;
                                launcheNewShip(false, 2);
                                //Destr2++;
                                ShipsAssigned++;
                            }
                            else
                            {
                                CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.D2PProdEnergy;
                                launcheNewShip(false, 22);
                                //Destr2Par++;
                                ShipsAssigned++;
                            }

                        }

                        if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.D2ProdEnergy && ShipsLimit > ShipsAssigned) utilaizeTheEnergyOfCPUGroup(2);
                        else
                        {
                            allignTheShipsAroundStation(); //put ships around station after the last energy unit is utilized
                            giveAnOrderToFleet();
                        }
                    }

                    else if (stationCurrentLevel == 3)
                    {
                        if (UnityEngine.Random.Range(0, 2) > 0)
                        {
                            if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.C1ProdEnergy)
                            {
                                CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.C1ProdEnergy;
                                launcheNewShip(true, 1);
                                //Cruis1++;
                                ShipsAssigned++;
                            }
                            else
                            {
                                if (UnityEngine.Random.Range(0, 2) > 0)
                                {
                                    CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.D1ProdEnergy;
                                    launcheNewShip(false, 1);
                                    //Destr1++;
                                    ShipsAssigned++;
                                }
                                else
                                {
                                    CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.D1PProdEnergy;
                                    launcheNewShip(false, 11);
                                    //Destr1Par++;
                                    ShipsAssigned++;
                                }
                            }
                        }

                        else if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.D1ProdEnergy)
                        {
                            if (UnityEngine.Random.Range(0, 2) > 0)
                            {
                                CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.D1ProdEnergy;
                                launcheNewShip(false, 1);
                                //Destr1++;
                                ShipsAssigned++;
                            }
                            else
                            {
                                CommonProperties.energyOfStationGroups[groupWhereTheStationIs] -= CommonProperties.D1PProdEnergy;
                                launcheNewShip(false, 11);
                                //Destr1Par++;
                                ShipsAssigned++;
                            }
                        }

                        if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] > CommonProperties.D1ProdEnergy && ShipsLimit > ShipsAssigned) utilaizeTheEnergyOfCPUGroup(2);
                        else
                        {
                            allignTheShipsAroundStation(); //put ships around station after the last energy unit is utilized
                            giveAnOrderToFleet();
                        }
                    }
                }
            }
        }
    }
        
    public void upgradeStation(int nextStationLevel)
    {
        //energyOfStationToUPGradeStation -= energyToNextUpgradeOfStation;
        StationCPU station;
        int nextStationLevelTemp = nextStationLevel;
        
        
        if (nextStationLevel == 1)
        {
            ObjectPulledList = ObjectPullerRTS.current.GetStationPull(nextStationLevelTemp, CPUNumber);
            ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
            station = ObjectPulled.GetComponent<StationCPU>();
            station.energyToNextUpgradeOfStation = CommonProperties.enrgy1to2Upgrd;
            station.energyToNextUpgradeOfGun = CommonProperties.gun0to1Upgrd;
            station.fillingSpeed = CommonProperties.star1FillingReducer;
            station.energyToConnection = CommonProperties.Station1EnergyToConnection;
            station.energyLoseIfDestroyed = CommonProperties.Station1GroupEnergyLoseIfDestroyed;
            station.energyToGetFromLine = CommonProperties.Station1EnergyFromLine;
            station.energyInscreaseTime = CommonProperties.Station1EnergyProduceTime;
            //station.stationShotTime = CommonProperties.Station1ShotTime;
            station.energonCatchDistance = CommonProperties.Station1EnergonCatchDistance;
            station.energyLimitOfStation = CommonProperties.Station1EnergyLimit;
            station.colorToEnergy = CommonProperties.Station1ColorToEnergyMultiplyer;
            station.ShipsLimit = CommonProperties.Station1ShipsLimit;
            station.speedOfBullet = CommonProperties.Station1BulletSpeed;
        }
        else if (nextStationLevel == 2)
        {
            ObjectPulledList = ObjectPullerRTS.current.GetStationPull(nextStationLevelTemp, CPUNumber);
            ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
            station = ObjectPulled.GetComponent<StationCPU>();
            station.energyToNextUpgradeOfStation = CommonProperties.enrgy2to3Upgrd;
            station.energyToNextUpgradeOfGun = CommonProperties.gun1to2Upgrd;
            station.fillingSpeed = CommonProperties.star2FillingReducer;
            station.energyToConnection = CommonProperties.Station2EnergyToConnection;
            station.energyLoseIfDestroyed = CommonProperties.Station2GroupEnergyLoseIfDestroyed;
            station.energyToGetFromLine = CommonProperties.Station2EnergyFromLine;
            station.energyInscreaseTime = CommonProperties.Station2EnergyProduceTime;
            //station.stationShotTime = CommonProperties.Station2ShotTime;
            station.energonCatchDistance = CommonProperties.Station2EnergonCatchDistance;
            station.energyLimitOfStation = CommonProperties.Station2EnergyLimit;
            station.colorToEnergy = CommonProperties.Station2ColorToEnergyMultiplyer;
            station.ShipsLimit = CommonProperties.Station2ShipsLimit;
            station.speedOfBullet = CommonProperties.Station2BulletSpeed;
        }
        else
        {
            ObjectPulledList = ObjectPullerRTS.current.GetStationPull(nextStationLevelTemp, CPUNumber);
            ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
            station = ObjectPulled.GetComponent<StationCPU>();
            station.energyToNextUpgradeOfStation = CommonProperties.enrgy2to3Upgrd;
            station.energyToNextUpgradeOfGun = CommonProperties.gun2to3Upgrd;
            station.fillingSpeed = CommonProperties.star3FillingReducer;
            station.energyToConnection = CommonProperties.Station3EnergyToConnection;
            station.energyLoseIfDestroyed = CommonProperties.Station3GroupEnergyLoseIfDestroyed;
            station.energyToGetFromLine = CommonProperties.Station3EnergyFromLine;
            station.energyInscreaseTime = CommonProperties.Station3EnergyProduceTime;
            //station.stationShotTime = CommonProperties.Station3ShotTime;
            station.energonCatchDistance = CommonProperties.Station3EnergonCatchDistance;
            station.energyLimitOfStation = CommonProperties.Station3EnergyLimit;
            station.colorToEnergy = CommonProperties.Station3ColorToEnergyMultiplyer;
            station.ShipsLimit = CommonProperties.Station3ShipsLimit;
            station.speedOfBullet = CommonProperties.Station3BulletSpeed;
        }

        station.CPUNumber = CPUNumber;
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
        station.energyOfStationToUPGrade = energyOfStationToUPGrade;
        station.energyOfStationToUPGradeGun = energyOfStationToUPGradeGun;
        station.energyOfStationToUPGradeStation = energyOfStationToUPGradeStation;
        station.energyOfStationToSetConnection = energyOfStationToSetConnection;
        station.energyInscreaseTimer = station.energyInscreaseTime;
        station.HPInscreaseTimer = station.energyInscreaseTime;
        //station.stationShotTimer = station.stationShotTime;
        //station.shotTimerTransformIndex = -6f / station.stationShotTime;
        station.ShipsAssigned = ShipsAssigned;
        station.ConnectedStations.Clear();//clear the connections if there left any from previous life of pulled object
        station.fillingLine.localPosition = new Vector3(0, 0, 0); //make full life to new station
        if (groupWhereTheStationIs != null /*&& groupWhereTheStationIs.Count > 0*/) connectUpgradedStationToGroup(station);
        else station.groupWhereTheStationIs = null;
        ObjectPulled.transform.position = stationPosition;
        station.stationTransform = ObjectPulled.transform;
        station.stationPosition = stationPosition;
        CommonProperties.allStations.Add(station);
        CommonProperties.CPUStations.Add(station);
        CommonProperties.CPUStationsDictionary[CPUNumber - 1].Add(station);
        ObjectPulled.SetActive(true);
        energyOfStationToUPGradeGun = 0;
        energyOfStationToUPGradeStation = 0;
        energyOfStationToSetConnection = 0;
        disactivateThisStation(station);
        if (station.groupWhereTheStationIs == null /*|| station.groupWhereTheStationIs.Count == 0*/) station.utilaizeTheEnergy(true); //using the energy of station to produce new ships. Used only by stand alone station
    }
    
    //public override void disactivateThisStation(StationClass newStation)
    //{
    //    for (int i = 0; i < CommonProperties.CPUBattleShipsDictionary[CPUNumber - 1].Count; i++)
    //        if (CommonProperties.CPUBattleShipsDictionary[CPUNumber - 1][i].maternalStation == this) CommonProperties.CPUBattleShipsDictionary[CPUNumber - 1][i].maternalStation = newStation;
    //    base.disactivateThisStation(newStation);
    //}
    
    private void connectUpgradedStationToGroup(StationCPU station)
    {
        //determmine the group processes for new and old station
        station.groupWhereTheStationIs = groupWhereTheStationIs;
        groupWhereTheStationIs.Remove(this);
        station.groupWhereTheStationIs.Add(station);
        //setting the limit of energy for station group
        CommonProperties.energyLimitOfStationGroups[groupWhereTheStationIs] = CommonProperties.getTheEnergyLimitOfStationsGroup(groupWhereTheStationIs);

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

    public override void checkIfStationCanConnect()
    {
        if (groupWhereTheStationIs != null /*&& groupWhereTheStationIs.Count > 0*/)
        {
            //if (CommonProperties.energyOfStationGroups[groupWhereTheStationIs] >= energyToConnection)
            //{
                for (int i = 0; i < CommonProperties.CPUStationsDictionary[CPUNumber - 1].Count; i++)
                {
                    if (CommonProperties.CPUStationsDictionary[CPUNumber - 1][i] != this && (CommonProperties.CPUStationsDictionary[CPUNumber - 1][i].stationPosition - stationPosition).magnitude < oneStepCloseStationsMaxDistance
                        && !groupWhereTheStationIs.Contains(CommonProperties.CPUStationsDictionary[CPUNumber - 1][i]))
                    {
                        stationToConnect = CommonProperties.CPUStationsDictionary[CPUNumber - 1][i];
                        return;
                    }
                }
                stationToConnect = null;
            //}
            //else stationToConnect = null;
        }
        else
        {
            //if (energyOfStation >= energyToConnection)
            //{
                for (int i = 0; i < CommonProperties.CPUStationsDictionary[CPUNumber - 1].Count; i++)
                {
                    if (CommonProperties.CPUStationsDictionary[CPUNumber - 1][i] != this && (CommonProperties.CPUStationsDictionary[CPUNumber - 1][i].stationPosition - stationPosition).magnitude < oneStepCloseStationsMaxDistance)
                    {
                        stationToConnect = CommonProperties.CPUStationsDictionary[CPUNumber - 1][i];
                        return;
                    }
                }
                stationToConnect = null;
            //}
            //else stationToConnect = null;
        }
    }

    public void upgradeTheGun(int gunLevel)
    {
        
        stationGunLevel = gunLevel;
        gunClass.CPUNumber = CPUNumber;
        if (gunClass.colorOfGunMat != colorOfStationMat)
        {
            gunClass.colorOfGunMat = colorOfStationMat;
            gunClass.setProperGunColor();
        }
        //gunSphereParent.SetActive(true);
        gunSphereVisible.SetActive(true);
    }

    private void launcheNewShip(bool isCruiser, int lvl)
    {
        //indexes are 0-Cruis4, 1-Destr4, 2-Cruis3, 3-Destr3, 4-Cruis2, 5-Destr2, 6-Destr2Par, 7-Cruis1, 8-Destr1, 9-Destr1Par
        if (isCruiser)
        {
            if (lvl == 4) ObjectPulledList = ObjectPullerRTS.current.GetShipPull(0,CPUNumber);
            else if (lvl == 3) ObjectPulledList = ObjectPullerRTS.current.GetShipPull(2, CPUNumber);
            else if (lvl == 2) ObjectPulledList = ObjectPullerRTS.current.GetShipPull(4, CPUNumber);
            else if (lvl == 1) ObjectPulledList = ObjectPullerRTS.current.GetShipPull(7, CPUNumber);
        }
        else
        {
            if (lvl == 4) ObjectPulledList = ObjectPullerRTS.current.GetShipPull(1, CPUNumber);
            else if (lvl == 3) ObjectPulledList = ObjectPullerRTS.current.GetShipPull(3, CPUNumber);
            else if (lvl == 2) ObjectPulledList = ObjectPullerRTS.current.GetShipPull(5, CPUNumber);
            else if (lvl == 22) ObjectPulledList = ObjectPullerRTS.current.GetShipPull(6, CPUNumber);
            else if (lvl == 1) ObjectPulledList = ObjectPullerRTS.current.GetShipPull(8, CPUNumber);
            else if (lvl == 11) ObjectPulledList = ObjectPullerRTS.current.GetShipPull(9, CPUNumber);
        }
        ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
        CPUBattleShip cpuShipObject;
        cpuShipObject = ObjectPulled.GetComponent<CPUBattleShip>();
        cpuShipObject.CPUNumber = CPUNumber;
        cpuShipObject.maternalStation = this;
        cpuShipObject.setShipsColor(colorOfStationMat);
        CommonProperties.CPUBattleShipsDictionary[CPUNumber - 1].Add(cpuShipObject);
        CommonProperties.CPUBattleShips.Add(cpuShipObject);
        ObjectPulled.transform.position = stationPosition;
        ObjectPulled.SetActive(true);

    }

    private StarController getTheStarClosestToStation() {
        if (CommonProperties.stars.Count > 1)
        {
            StarController closetsStar = null;
            for (int i = 0; i < CommonProperties.stars.Count; i++) {
                if ((CommonProperties.stars[i].starPosition - stationPosition).magnitude<=oneStepCloseStationsMaxDistance) closetsStar = CommonProperties.stars[i];
                //if (i == 0) closetsStar = CommonProperties.stars[i];
                //else { 
                //    if((closetsStar.starPosition-stationPosition).sqrMagnitude> (CommonProperties.stars[i].starPosition - stationPosition).sqrMagnitude) closetsStar = CommonProperties.stars[i];
                //}
            }
            return closetsStar;
        }
        else if (CommonProperties.stars.Count == 1 && (CommonProperties.stars[0].starPosition - stationPosition).magnitude <= oneStepCloseStationsMaxDistance) return CommonProperties.stars[0];
        else return null;
    }

    //this one is used to count the power of station and call for a help
    public override int stationDefenceFleetPower()
    {
        int shipsPower = 0;
        foreach (CPUBattleShip ship in CommonProperties.CPUBattleShipsDictionary[CPUNumber - 1])
        {
            if ((ship.shipTransform.position - stationPosition).magnitude < fleetGatherRadius)
            {
                shipsPower += ship.shipPower;
            }
        }
        if (gunSphereVisible!=null&&gunSphereVisible.activeInHierarchy) {
            if (stationGunLevel == 1) shipsPower += CommonProperties.Gun1Index;
            else if (stationGunLevel ==2) shipsPower += CommonProperties.Gun2Index;
            else if (stationGunLevel ==3) shipsPower += CommonProperties.Gun3Index;
        }
        return shipsPower;
    }

    //this one is used to count the ships (and call for a help)
    public override int stationDefenceFleetLeftByUnits()
    {
        int shipsCount = 0;
        foreach (CPUBattleShip ship in CommonProperties.CPUBattleShipsDictionary[CPUNumber - 1])
        {
            if ((ship.shipTransform.position - stationPosition).magnitude < fleetGatherRadius)
            {
                shipsCount += 1;
            }
        }
        return shipsCount;
    }

    private void gatherTheReferencesToNearShips()
    {
        shipsToGiveOrderCommandOfStation.Clear();
        foreach (CPUBattleShip ship in CommonProperties.CPUBattleShipsDictionary[CPUNumber - 1])
        {
            if ((ship.shipTransform.position - stationPosition).magnitude < fleetGatherRadius)
            {
                shipsToGiveOrderCommandOfStation.Add(ship);
            }
        }
    }
    //public override void gatherTheReferencesToShipsOfStation()
    //{
    //    shipsToGiveOrderCommandOfStation.Clear();
    //    foreach (CPUBattleShip ship in CommonProperties.CPUBattleShipsDictionary[CPUNumber - 1])
    //    {
    //        if (ship.maternalStation == this)
    //        {
    //            shipsToGiveOrderCommandOfStation.Add(ship);
    //        }
    //    }
    //}
    //private void gatherTheReferencesToShipsOfStationThatNear()
    //{
    //    shipsOfStationThatNear.Clear();
    //    foreach (CPUBattleShip ship in CommonProperties.CPUBattleShipsDictionary[CPUNumber - 1])
    //    {
    //        if (ship.maternalStation == this && (ship.shipTransform.position - stationPosition).magnitude < fleetGatherRadius)
    //        {
    //            shipsOfStationThatNear.Add(ship);
    //        }
    //    }
    //}
    public void getherTheReferenceToFleetOfStationExceptDefenceMinimum()
    {
        gatherTheReferencesToNearShips();
        if (shipsToGiveOrderCommandOfStation.Count > BASE_STATION_DEFENCE_SHIPS_COUNT)
        {
            //the limit is minimum count of defence ships of this station
            for (int i = 0; i < BASE_STATION_DEFENCE_SHIPS_COUNT; i++)
            {
                shipsToGiveOrderCommandOfStation.RemoveAt(0);
            }
        }
        else shipsToGiveOrderCommandOfStation.Clear();

        //gatherTheReferencesToShipsOfStationThatNear();
        //gatherTheReferencesToNearShips();
        ////attack is possible only if this station ships that near is more or equal to defence minimum 
        //if (shipsOfStationThatNear.Count >= BASE_STATION_DEFENCE_SHIPS_COUNT)
        //{
        //    //the limit is minimum count of defence ships of this station
        //    for (int i = 0; i < BASE_STATION_DEFENCE_SHIPS_COUNT; i++)
        //    {
        //        shipsToGiveOrderCommandOfStation.Remove(shipsOfStationThatNear[i]);
        //    }
        //}
        //else
        //{
        //    shipsToGiveOrderCommandOfStation.Clear();
        //    shipsOfStationThatNear.Clear();
        //}
    }

    //private void getherTheReferenceToExtraFleetOfStation(int fleetCountThatWillStayNearStation)
    //{
    //    gatherTheReferencesToShipsOfStation();
    //    for (int i = 0; i < fleetCountThatWillStayNearStation; i++) {
    //        shipsToGiveOrderCommandOfStation.RemoveAt(0);
    //    }
    //}

    public void giveAnOrderToFleet()
    {
        //if (CommonProperties.stars.Count > 0 )
        //{
        //    getherTheReferenceToFleetOfStationExceptDefenceMinimum();
        //    if (shipsToGiveOrderCommandOfStation.Count > 0) sendTheFleetToThePoint(stationPosition, getTheStarClosestToStation().starPosition);
        //}

        //attack star only if it is one step close to station 
        StarController closestStar = getTheStarClosestToStation();
        if (closestStar != null)
        {
            getherTheReferenceToFleetOfStationExceptDefenceMinimum();
            if (shipsToGiveOrderCommandOfStation.Count > 0) sendTheFleetToThePoint(stationPosition, closestStar.starPosition);
        }
        else
        {
            getherTheReferenceToFleetOfStationExceptDefenceMinimum();
            //if there left any ships after defence minimum
            if (shipsToGiveOrderCommandOfStation.Count > 0)
            {
                //group attack is possible only if there are more than 1 CPU stations of this CPU
                if (CommonProperties.CPUStationsDictionary[CPUNumber - 1].Count > 1)
                {
                    int groupFleet = 0;
                    foreach (StationCPU station in CommonProperties.CPUStationsDictionary[CPUNumber - 1])
                    {
                        if (station!=this) groupFleet += station.fleetOfStationMoreThanDefenceMinimum();
                    }
                    groupFleet += shipsToGiveOrderCommandOfStation.Count;
                    if (groupFleet >= SHIPS_COUNT_MINIMUM_TO_ATTACK)
                    {
                        attackWeakestStation();
                        foreach (StationCPU station in CommonProperties.CPUStationsDictionary[CPUNumber - 1]) {
                            if (station != this && station.fleetOfStationMoreThanDefenceMinimum() > 0) {
                                station.externalCallForExtraFleetAttackOrDefence(stationToAttack);
                            }
                        }
                    }
                }
                else if (shipsToGiveOrderCommandOfStation.Count >= SHIPS_COUNT_MINIMUM_TO_ATTACK)
                {
                    attackWeakestStation();
                }
            }
        }

        #region old fleet order system

        //int ownShipsNear = 0;
        //for (int i = 0; i < CommonProperties.CPUBattleShipsDictionary[CPUNumber - 1].Count; i++)
        //{
        //    if ((CommonProperties.CPUBattleShipsDictionary[CPUNumber - 1][i].shipTransform.position - stationPosition).magnitude < fleetGatherRadius && CommonProperties.CPUBattleShipsDictionary[CPUNumber - 1][i].maternalStation == this) ownShipsNear++;
        //}
        ////chieck if all this station ships are near and act
        //if (ownShipsNear == ShipsAssigned)
        //{
        //    //maximum ships limit is reached
        //    if (ownShipsNear == ShipsLimit)
        //    {
        //        if (CommonProperties.stars.Count > 0)
        //        {
        //            getherTheReferenceToExtraFleetOfStation(ShipsAssigned - 5);
        //            sendTheFleetToThePoint(stationPosition, getTheStarClosestToStation().starPosition);
        //        }
        //        else //attack the weakest station
        //        {
        //            byte index = 0;
        //            StationClass stattionToAttack = null;
        //            for (int i = 0; i < CommonProperties.allStations.Count; i++)
        //            {
        //                if (CommonProperties.allStations[i].CPUNumber != CPUNumber)
        //                {
        //                    if (index == 0) stattionToAttack = CommonProperties.allStations[i];
        //                    else if (stattionToAttack.stationDefenceFleetCount() > CommonProperties.allStations[i].stationDefenceFleetCount())
        //                    {
        //                        stattionToAttack = CommonProperties.allStations[i];
        //                    }
        //                    index++;
        //                }
        //            }
        //            getherTheReferenceToExtraFleetOfStation(BASE_STATION_DEFENCE_SHIPS_COUNT / 2);
        //            sendTheFleetToThePoint(stationPosition, stattionToAttack.stationPosition);
        //        }
        //    }
        //    else
        //    {
        //        if (ShipsAssigned > BASE_STATION_DEFENCE_SHIPS_COUNT)
        //        {
        //            if (CommonProperties.stars.Count > 0)
        //            {
        //                getherTheReferenceToExtraFleetOfStation(BASE_STATION_DEFENCE_SHIPS_COUNT);
        //                sendTheFleetToThePoint(stationPosition, getTheStarClosestToStation().starPosition);
        //            }
        //            else
        //            {
        //                for (int i = 0; i < CommonProperties.allStations.Count; i++)
        //                {
        //                    if (CommonProperties.allStations[i].CPUNumber != CPUNumber && CommonProperties.allStations[i].stationDefenceFleetCount() == 0)
        //                    {
        //                        getherTheReferenceToExtraFleetOfStation(BASE_STATION_DEFENCE_SHIPS_COUNT);
        //                        sendTheFleetToThePoint(stationPosition, CommonProperties.allStations[i].stationPosition);
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        ////send the other stations ships that are near this station back to their maternal stations 
        //if (ownShipsNear >= BASE_STATION_DEFENCE_SHIPS_COUNT)
        //{

        //    gatherTheReferencesToNearShips();
        //    List<CPUBattleShip> staionShips = new List<CPUBattleShip>();
        //    for (int i = 0; i < shipsToGiveOrderCommandOfStation.Count; i++)
        //    {
        //        if (shipsToGiveOrderCommandOfStation[i].maternalStation != this)
        //        {
        //            staionShips.Add(shipsToGiveOrderCommandOfStation[i]);
        //        }
        //    }
        //    shipsToGiveOrderCommandOfStation.Clear();
        //    for (int i = 0; i < CommonProperties.CPUStationsDictionary[CPUNumber - 1].Count; i++)
        //    {
        //        if (CommonProperties.CPUStationsDictionary[CPUNumber - 1][i] != this)
        //        {
        //            for (int y = 0; y < staionShips.Count; y++)
        //            {
        //                if (staionShips[y].maternalStation == CommonProperties.CPUStationsDictionary[CPUNumber - 1][i]) shipsToGiveOrderCommandOfStation.Add(staionShips[y]);
        //            }
        //            if (shipsToGiveOrderCommandOfStation.Count > 0)
        //            {
        //                sendTheFleetToThePoint(stationPosition, shipsToGiveOrderCommandOfStation[0].maternalStation.stationPosition);
        //            }
        //        }
        //    }
        //    shipsToGiveOrderCommandOfStation.Clear();

        //    //send the ships around that are not this station ships and does not have maternal stations, to attack weakest station
        //    for (int i = 0; i < staionShips.Count; i++)
        //    {
        //        if (staionShips[i].maternalStation == null) shipsToGiveOrderCommandOfStation.Add(staionShips[i]);
        //    }
        //    if (shipsToGiveOrderCommandOfStation.Count > 0)
        //    {
        //        byte index = 0;
        //        StationClass stattionToAttack = null;
        //        for (int i = 0; i < CommonProperties.allStations.Count; i++)
        //        {
        //            if (CommonProperties.allStations[i].CPUNumber != CPUNumber)
        //            {
        //                if (index == 0) stattionToAttack = CommonProperties.allStations[i];
        //                else if (stattionToAttack.stationDefenceFleetCount() > CommonProperties.allStations[i].stationDefenceFleetCount())
        //                {
        //                    stattionToAttack = CommonProperties.allStations[i];
        //                }
        //                index++;
        //            }
        //        }
        //        sendTheFleetToThePoint(stationPosition, stattionToAttack.stationPosition);
        //    }
        //    shipsToGiveOrderCommandOfStation.Clear();
        //}

        #endregion
    }

    public int fleetOfStationMoreThanDefenceMinimum() {
        int readyToAttackFleet =0;
        //gatherTheReferencesToShipsOfStationThatNear();
        gatherTheReferencesToNearShips();
        if (shipsToGiveOrderCommandOfStation.Count > BASE_STATION_DEFENCE_SHIPS_COUNT)
        {
            readyToAttackFleet = shipsToGiveOrderCommandOfStation.Count - BASE_STATION_DEFENCE_SHIPS_COUNT;
        }
        shipsToGiveOrderCommandOfStation.Clear();
        //shipsOfStationThatNear.Clear();
        return readyToAttackFleet;
    }

    public void externalCallForExtraFleetAttackOrDefence(StationClass stattionToAttackParam) {
        getherTheReferenceToFleetOfStationExceptDefenceMinimum();
        if (shipsToGiveOrderCommandOfStation.Count>0) sendTheFleetToThePoint(stationPosition, stattionToAttackParam.stationPosition);
    }

    private void attackWeakestStation() {
        byte index = 0;
        stationToAttack = null;

        //List<StationClass> enemyStations = new List<StationClass>();
        for (int i = 0; i < CommonProperties.allStations.Count; i++)
        {
            if (CommonProperties.allStations[i].CPUNumber != CPUNumber)
            {
                if (index == 0) stationToAttack = CommonProperties.allStations[i];
                else
                {
                    if ((stationToAttack.stationPosition - stationPosition).sqrMagnitude > (CommonProperties.allStations[i].stationPosition - stationPosition).sqrMagnitude) stationToAttack = CommonProperties.allStations[i];
                }
                index++;
            }
        }

        //for (int i = 0; i < CommonProperties.allStations.Count; i++)
        //{
        //    if (CommonProperties.allStations[i].CPUNumber != CPUNumber)
        //    {
        //        if (index == 0) stationToAttack = CommonProperties.allStations[i];
        //        else if (stationToAttack.stationDefenceFleetPower() > CommonProperties.allStations[i].stationDefenceFleetPower())
        //        {
        //            stationToAttack = CommonProperties.allStations[i];
        //        }
        //        index++;
        //    }
        //}
        sendTheFleetToThePoint(stationPosition, stationToAttack.stationPosition);
    }

    private void allignTheShipsAroundStation()
    {
        gatherTheReferencesToNearShips();
        if (shipsToGiveOrderCommandOfStation.Count > 1)
        {
            for (int i = 0; i < shipsToGiveOrderCommandOfStation.Count; i++)
            {
                Vector3 newPos;
                float step = (Mathf.PI * 2) / shipsToGiveOrderCommandOfStation.Count; // отступ
                newPos.x = stationPosition.x + Mathf.Sin(step * i) * radiusOfShipsRingAroundStation; // по оси X
                newPos.z = stationPosition.z + Mathf.Cos(step * i) * radiusOfShipsRingAroundStation; // по оси Z
                newPos.y = 0; // по оси Y всегда 0
                squardPositions.Add(newPos);
            }
            for (int i = 0; i < shipsToGiveOrderCommandOfStation.Count; i++)
            {
                shipsToGiveOrderCommandOfStation[i].giveAShipMoveOrder(squardPositions[i], null);
            }
        }
        else if (shipsToGiveOrderCommandOfStation.Count == 1)
        {
            shipsToGiveOrderCommandOfStation[0].giveAShipMoveOrder(new Vector3 (stationPosition.x+6, 0 , stationPosition.z + 6), null);
        }
        shipsToGiveOrderCommandOfStation.Clear();
        squardPositions.Clear();
    }

    public override void sendTheFleetToThePoint(Vector3 startPoint, Vector3 destinationPoint) {
        Vector3 moveToPoint = destinationPoint + (startPoint - destinationPoint).normalized * 7;
        float stepForOuterRadius = 1;

        if (shipsToGiveOrderCommandOfStation.Count > 1)
        {
            for (int i = 0; i < shipsToGiveOrderCommandOfStation.Count; i++)
            {
                if (i == 0)
                {
                    squardPositions.Add(moveToPoint);
                }
                else if (i <= innnerCircleMax)
                {
                    if (radiusGroup != 3) radiusGroup = 3;
                    Vector3 newPos;
                    float step = (Mathf.PI * 2) / innnerCircleMax; // отступ
                    newPos.x = moveToPoint.x + Mathf.Sin(step * i) * radiusGroup; // по оси X
                    newPos.z = moveToPoint.z + Mathf.Cos(step * i) * radiusGroup; // по оси Z
                    newPos.y = 0; // по оси Y всегда 0
                    squardPositions.Add(newPos);
                    if (i == innnerCircleMax)
                    {
                        if ((shipsToGiveOrderCommandOfStation.Count - squardPositions.Count) > innnerCircleMax * 2) stepForOuterRadius = innnerCircleMax * 2;
                        else stepForOuterRadius = shipsToGiveOrderCommandOfStation.Count - squardPositions.Count;
                    }
                }
                else if (i <= (innnerCircleMax * 3))
                {
                    if (radiusGroup != 6) radiusGroup = 6;
                    Vector3 newPos;
                    float step = (Mathf.PI * 2) / stepForOuterRadius; // отступ
                    newPos.x = moveToPoint.x + Mathf.Sin(step * i) * radiusGroup; // по оси X
                    newPos.z = moveToPoint.z + Mathf.Cos(step * i) * radiusGroup; // по оси Z
                    newPos.y = 0; // по оси Y всегда 0
                    squardPositions.Add(newPos);
                    if (i == (innnerCircleMax * 3))
                    {
                        if ((shipsToGiveOrderCommandOfStation.Count - squardPositions.Count) > innnerCircleMax * 3) stepForOuterRadius = innnerCircleMax * 3;
                        else stepForOuterRadius = shipsToGiveOrderCommandOfStation.Count - squardPositions.Count;
                    }
                }
                else if (i <= (innnerCircleMax * 7))
                {
                    if (radiusGroup != 9) radiusGroup = 9;
                    Vector3 newPos;
                    float step = (Mathf.PI * 2) / stepForOuterRadius; // отступ
                    newPos.x = moveToPoint.x + Mathf.Sin(step * i) * radiusGroup; // по оси X
                    newPos.z = moveToPoint.z + Mathf.Cos(step * i) * radiusGroup; // по оси Z
                    newPos.y = 0; // по оси Y всегда 0
                    squardPositions.Add(newPos);
                    if (i == (innnerCircleMax * 7))
                    {
                        if ((shipsToGiveOrderCommandOfStation.Count - squardPositions.Count) > innnerCircleMax * 4) stepForOuterRadius = innnerCircleMax * 4;
                        else stepForOuterRadius = shipsToGiveOrderCommandOfStation.Count - squardPositions.Count;
                    }
                }
                else if (i <= (innnerCircleMax * 15))
                {
                    if (radiusGroup != 12) radiusGroup = 12;
                    Vector3 newPos;
                    float step = (Mathf.PI * 2) / stepForOuterRadius; // отступ
                    newPos.x = moveToPoint.x + Mathf.Sin(step * i) * radiusGroup; // по оси X
                    newPos.z = moveToPoint.z + Mathf.Cos(step * i) * radiusGroup; // по оси Z
                    newPos.y = 0; // по оси Y всегда 0
                    squardPositions.Add(newPos);
                }
            }

            if ((startPoint - destinationPoint).magnitude < oneStepCloseStationsMaxDistance)
            {
                for (int i = 0; i < shipsToGiveOrderCommandOfStation.Count; i++)
                {
                    shipsToGiveOrderCommandOfStation[i].giveAShipMoveOrder(squardPositions[i], null);
                }
            }
            else {
                wayDots.Clear();
                determineBestWayToMovePoint(startPoint, destinationPoint);
                for (int i = 0; i < shipsToGiveOrderCommandOfStation.Count; i++)
                {
                    shipsToGiveOrderCommandOfStation[i].giveAShipMoveOrder(squardPositions[i], wayDots);
                }
            }
        }
        shipsToGiveOrderCommandOfStation.Clear();
        squardPositions.Clear();
    }
    
    private void determineBestWayToMovePoint(Vector3 startPoint, Vector3 destinationPoint)
    {
        List<Vector3> nearDots = new List<Vector3>();
        Vector3 dotClosestToMovePoint = Vector3.zero;
        foreach (Vector3 nearHexDot in CommonProperties.HexBorderDots)
        {
            if ((nearHexDot - startPoint).magnitude < nearHexMaxDistance) nearDots.Add(nearHexDot);
        }

        for (int i = 0; i < nearDots.Count; i++) {
            if (i == 0) dotClosestToMovePoint = nearDots[i];
            else if ((dotClosestToMovePoint- destinationPoint).magnitude> (nearDots[i] - destinationPoint).magnitude) dotClosestToMovePoint = nearDots[i];
        }
        wayDots.Add(dotClosestToMovePoint);
        while ((dotClosestToMovePoint - destinationPoint).magnitude > nearHexMaxDistance) {
            nearDots.Clear();
            foreach (Vector3 nearHexDot in CommonProperties.HexBorderDots)
            {
                if ((nearHexDot - dotClosestToMovePoint).magnitude < nearHexMaxDistance) nearDots.Add(nearHexDot);
            }

            for (int i = 0; i < nearDots.Count; i++)
            {
                if (i == 0) dotClosestToMovePoint = nearDots[i];
                else if ((dotClosestToMovePoint - destinationPoint).magnitude > (nearDots[i] - destinationPoint).magnitude) dotClosestToMovePoint = nearDots[i];
            }
            wayDots.Add(dotClosestToMovePoint);
        }
        //if ((dotClosestToMovePoint - destinationPoint).magnitude > nearHexMaxDistance) determineBestWayToMovePoint(dotClosestToMovePoint, destinationPoint);
    }

    public override void callForAHelp()
    {   //group help is possible only if there are more than 1 CPU stations of this CPU
        
            int groupFleet = 0;
            foreach (StationCPU station in CommonProperties.CPUStationsDictionary[CPUNumber - 1] )
            {
                if(station != this) groupFleet += station.fleetOfStationMoreThanDefenceMinimum();
            }
            if (groupFleet >0)
            {
                foreach (StationCPU station in CommonProperties.CPUStationsDictionary[CPUNumber - 1])
                {
                    if (station != this && station.fleetOfStationMoreThanDefenceMinimum() > 0)
                    {
                        station.externalCallForExtraFleetAttackOrDefence(this);
                    }
                }
            }
        
    }

    private void collectTheCloseEnemyShips()
    {
        for (int i = 0; i < CommonProperties.playerBattleShips.Count; i++)
        {
            if ((CommonProperties.playerBattleShips[i].shipTransform.position - stationPosition).magnitude <= attackDistance)
            {
                closeBattleShips.Add(CommonProperties.playerBattleShips[i]);
            }
        }
        for (int i = 0; i < CommonProperties.CPUBattleShipsDictionary.Count; i++)
        {
            if (i != (CPUNumber - 1))
            {
                for (int y = 0; y < CommonProperties.CPUBattleShipsDictionary[i].Count; y++)
                {
                    if ((CommonProperties.CPUBattleShipsDictionary[i][y].shipTransform.position - stationPosition).magnitude <= attackDistance)
                    {
                        closeBattleShips.Add(CommonProperties.CPUBattleShipsDictionary[i][y]);
                    }
                }
            }
        }


        if (closeBattleShips.Count > 0)
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
                if (closeBattleShips.Contains(shipToAttak)) closeBattleShips.Remove(shipToAttak);
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
            if (closeBattleShips.Count > 0)
            {
                shipToAttak = closeBattleShips.Count < 2 ? closeBattleShips[0] : closeBattleShips[Random.Range(0, closeBattleShips.Count)];
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

        closeBattleShips.Clear();

        yield return new WaitForSeconds(0.8f);
        attackLaserLine.enabled = false;
    }

    private void FixedUpdate()
    {
        if (attackLaserLine.enabled && shipToAttak != null)
        {
            attackLaserLine.SetPosition(1, shipToAttak.shipTransform.position);
        }
        if (attackMode && shipToAttak != null && stationCurrentLevel > 0 && stationGunLevel == GunUpgradeCounts)
        {
            if (gunSphereParentTransform.rotation != Quaternion.LookRotation(shipToAttak.shipTransform.position - gunSphereParentTransform.position, Vector3.up))
                gunSphereParentTransform.rotation = Quaternion.Lerp(gunSphereParentTransform.rotation, Quaternion.LookRotation(shipToAttak.shipTransform.position - gunSphereParentTransform.position, Vector3.up), 0.1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //attack timers
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

        if (CommonProperties.energonsOnScene.Count>0) resetTheClosestEnergon();

        pullTheEnergyFromEnergons();

        //if (closestEnegon != null)
        //{
        //    if (distanceToEnergon() < energonCatchDistance && !shotIsMade)
        //    {
        //        shotIsMade = true;
        //        StartCoroutine(makeAShotFromStation());
        //    }
        //}
        ////timer to make next shot to energon
        //if (stationShotTimer > 0 && shotIsMade)
        //{
        //    stationShotTimer -= Time.deltaTime;
        //    if (stationShotTimer < 0)
        //    {
        //        shotIsMade = false;
        //        stationShotTimer = stationShotTime;
        //    }
        //}

        //timer to increment the energy naturally
        if (energyInscreaseTimer > 0 && groupWhereTheStationIs==null)
        {
            energyInscreaseTimer -= Time.deltaTime;
            if (energyInscreaseTimer < 0)
            {
                energyOfStation++;
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
