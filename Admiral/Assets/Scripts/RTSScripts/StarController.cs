using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    public Transform fillingLine;
    [HideInInspector]
    public Vector3 starPosition;

    private float XPositionOfFillingLine;

    private float[] fillAmountOfAll= new float[5];

    private float fillingSpeed;
    //private float recoverySpeed;

    [HideInInspector]
    public GameObject ObjectPulled;
    [HideInInspector]
    public List<GameObject> ObjectPulledList;

    [HideInInspector]
    public byte upgradeCount;

    private bool starIsDead;

    //private float HPInscreaseTime;
    //private float HPInscreaseTimer;


    private void OnEnable()
    {
        starIsDead = false;
        for (int i = 0; i < fillAmountOfAll.Length; i++) fillAmountOfAll[i] = -6f;
        XPositionOfFillingLine = -6f;
        fillingLine.localPosition = new Vector3(XPositionOfFillingLine,0,0);
        starPosition = transform.position;

        if (name.Contains("1")) {
            upgradeCount = 1;
            fillingSpeed = CommonProperties.star1FillingReducer;
            //HPInscreaseTime = CommonProperties.Station1EnergyProduceTime;
        }
        else if (name.Contains("2"))
        {
            upgradeCount = 2;
            fillingSpeed = CommonProperties.star2FillingReducer;
            //HPInscreaseTime = CommonProperties.Station2EnergyProduceTime;
        }
        else if (name.Contains("3"))
        {
            upgradeCount = 3;
            fillingSpeed = CommonProperties.star3FillingReducer;
            //HPInscreaseTime = CommonProperties.Station3EnergyProduceTime;
        }
        else
        {
            upgradeCount = 0;
            fillingSpeed = CommonProperties.star0FillingReducer;
            //HPInscreaseTime = CommonProperties.Station3EnergyProduceTime;
        }
        //HPInscreaseTimer = HPInscreaseTime;
        //recoverySpeed = 2 - fillingSpeed;
    }


    public void reduceTheHPOfStar(float fillAmount, int CPUNumber)
    {
        if (!starIsDead)
        {
            float tempFloat = -6f;
            //reducing the fill amount of others 
            for (int i = 0; i < fillAmountOfAll.Length; i++)
            {
                if (i != CPUNumber)
                {
                    if (fillAmountOfAll[i] > -6)
                    {
                        if (fillAmountOfAll[i] > tempFloat)
                        {
                            tempFloat = fillAmountOfAll[i];
                            fillAmountOfAll[i] -= fillAmount * fillingSpeed;
                            if (fillAmountOfAll[i] < -6) fillAmountOfAll[i] = -6;

                            fillingLine.localPosition = new Vector3(fillAmountOfAll[i], 0, 0);
                        }
                        else fillAmountOfAll[i] = -6;

                        return; //stop the function cause someone has the shots on star
                    }
                }
            }
            //increasing the fill amount the one that makes a shot
            fillAmountOfAll[CPUNumber] += fillAmount * fillingSpeed;
            fillingLine.localPosition = new Vector3(fillAmountOfAll[CPUNumber], 0, 0);
            if (fillAmountOfAll[CPUNumber] > 0) fillAmountOfAll[CPUNumber] = 0;
            if (fillAmountOfAll[CPUNumber] >= 0)
            {
                starIsDead = true;
                disactivateThisStar(CPUNumber);
            }
        }
    }

    private void disactivateThisStar(int CPUNumber)
    {
        StationClass station = null;
        StationPlayerRTS stationPlayer = null;
        StationCPU stationCPU = null;

        //0 CPU number is player, others are CPU. index is The level of station
        ObjectPulledList = ObjectPullerRTS.current.GetStationPull(0, CPUNumber);
        ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
        if (CPUNumber == 0)
        {
            stationPlayer = ObjectPulled.GetComponent<StationPlayerRTS>();
            station = stationPlayer;
            CommonProperties.allStations.Add(station);
            ConnectionPLayerStations.Instance.disactivateConnectionLine(); //to prevent the bug of dynamically increasing the collection of player stations while using connection UI of station
            CommonProperties.playerStations.Add(station);
        }
        else
        {
            stationCPU  = ObjectPulled.GetComponent<StationCPU>();
            station = stationCPU;
            CommonProperties.allStations.Add(station);
            CommonProperties.CPUStations.Add(stationCPU);
        }

        station.CPUNumber = CPUNumber;
        if (station.CPUNumber != 0)
        {
            CommonProperties.CPUStationsDictionary[station.CPUNumber - 1].Add(station);
            stationCPU.colorOfStationMat = GameController.getProperMatColorByIndex(GameController.colorsOfPlayers[CPUNumber-1]); //setting the next color available on colours list to this CPU station
            stationCPU.setProperStationColor();
        }
        else
        {
            stationPlayer.colorOfStationMat = GameController.getProperMatColorByIndex(CommonProperties.colorOfPlayer); //setting the next color available on colours list to this CPU station
            stationPlayer.setProperStationColor();
        }
        //station.fleetGatherRadius = 14f;
        //station.radiusOfShipsRingAroundStation = 6;
        station.stationCurrentLevel = 0;
        station.upgradeCounts = upgradeCount;
        station.energyToNextUpgradeOfStation = CommonProperties.enrgy0to1Upgrd;
        station.stationGunLevel = 0;
        station.energyToNextUpgradeOfGun = CommonProperties.gun0to1Upgrd;
        station.GunUpgradeCounts = 0; //the star creates always 0 lvl station so it can't produce the gun. later on upgrade of station gun will upgrade counts will get it's proper value
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
        station.energyOfStation = 15;
        station.fillingSpeed = CommonProperties.star0FillingReducer;
        station.ShipsAssigned = 0;
        station.energyToConnection = CommonProperties.Station0EnergyToConnection;
        station.energyLoseIfDestroyed = CommonProperties.Station0GroupEnergyLoseIfDestroyed;
        station.energyToGetFromLine = CommonProperties.Station0EnergyFromLine;
        station.energyInscreaseTime = CommonProperties.Station0EnergyProduceTime;
        station.energyInscreaseTimer = station.energyInscreaseTime;
        station.HPInscreaseTimer = station.energyInscreaseTime;
        //station.stationShotTime = CommonProperties.Station0ShotTime;
        //station.stationShotTimer = station.stationShotTime;
        //station.shotTimerTransformIndex = -6f / station.stationShotTime;
        station.energonCatchDistance = CommonProperties.Station0EnergonCatchDistance;
        station.energyLimitOfStation = CommonProperties.Station0EnergyLimit;
        station.speedOfBullet = CommonProperties.Station0BulletSpeed; 
        station.colorToEnergy = CommonProperties.Station0ColorToEnergyMultiplyer; 
        station.ShipsLimit = CommonProperties.Station0ShipsLimit;
        station.fillingLine.localPosition = new Vector3(0, 0, 0); //make full life to new station

        station.ConnectedStations.Clear();
        station.groupWhereTheStationIs = null;
        ObjectPulled.transform.position = starPosition;

        station.stationTransform = ObjectPulled.transform;
        station.stationPosition = station.stationTransform.position;

        ObjectPulled.SetActive(true);

        CommonProperties.stars.Remove(this);
        gameObject.SetActive(false);
    }

    //public void increaseTheHPOfStar(float energyAmount)
    //{
    //    int CPUNumber = 0;
    //    for (int i = 0; i < fillAmountOfAll.Length; i++) {
    //        if (fillAmountOfAll[i] > -6)
    //        {
    //            CPUNumber = i;
    //            break;
    //        }
    //    }
    //    fillAmountOfAll[CPUNumber] -= (energyAmount / 500) * recoverySpeed;
    //    if (fillAmountOfAll[CPUNumber] < -6)
    //    {
    //        fillAmountOfAll[CPUNumber] = -6;
    //    }
    //    fillingLine.localPosition = new Vector3(fillAmountOfAll[CPUNumber], 0, 0);
    //}

    //private void Update()
    //{
    //    //timer to increment the HP of star naturally
    //    if (fillingLine.localPosition.x > -6)
    //    {
    //        HPInscreaseTimer -= Time.deltaTime;
    //        if (HPInscreaseTimer < 0)
    //        {
    //            increaseTheHPOfStar(20);
    //            HPInscreaseTimer = HPInscreaseTime;
    //        }
    //    }
    //}
}
