using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBallRTS : MonoBehaviour
{
    private Vector3 stationToMovePos;
    private bool isPlayerStation;
    private int energyAmount;
    private int CPUNumber;
    private Transform energyBallTransform;
    private StationClass station;

    // Start is called before the first frame update
    void Start()
    {
        energyBallTransform = transform;
    }

    public void setProperties(Vector3 stationPos, StationClass stationThatLaunchedBullet, int energy, float scale, bool isPlayer/*, int CPUNumber*/) {
        stationToMovePos = stationPos;
        energyAmount = energy;
        transform.localScale = new Vector3(scale, scale, scale);
        isPlayerStation = isPlayer;
        //this.CPUNumber = CPUNumber;
        station = stationThatLaunchedBullet;
    }
    private void FixedUpdate()
    {
        energyBallTransform.Translate((stationToMovePos - energyBallTransform.position).normalized *Time.fixedDeltaTime* 2f, Space.World);
    }
    private void disactivateAndPassTheEnergy()
    {
        if (station.CPUNumber == 0)
        {
            if (station.groupWhereTheStationIs != null /*&& station.groupWhereTheStationIs.Count > 0*/)
            {
                CommonProperties.energyOfStationGroups[station.groupWhereTheStationIs] += energyAmount; //adding the energy to group of station
            }
            else station.energyOfStation += energyAmount;//adding the energy to station only
        }
        else {
            if (station.groupWhereTheStationIs != null /*&& station.groupWhereTheStationIs.Count > 0*/)
            {
                CommonProperties.energyOfStationGroups[station.groupWhereTheStationIs] += (int)(energyAmount*1.8f); //adding the energy to group of station
                ConnectionCPUStations.distributeGroupEnergy(station.groupWhereTheStationIs);
            }
            else station.energyOfStation += (int)(energyAmount * 1.8f);//adding the energy to station only
        }
        station.energyGainEffectMain.startSize = energyAmount / 10;
        station.energyGainEffect.Play();
        if (station.lifeLineAmount<0) station.increaseTheHPOfStation(energyAmount); //increasing the HP of station if it is damaged
        //only CPU station utilizes the energy automatically and for player station wich is 0 index one utilization means updating the UI (if panel is on) and check is station can connect
        if (station.CPUNumber == 0)
        {
            station.utilaizeTheEnergy(false);
        }
        else if (station.groupWhereTheStationIs == null /*|| station.groupWhereTheStationIs.Count == 0*/)
        {
            station.utilaizeTheEnergy(false);
        }
        //else {
        //    station.utilaizeTheEnergy(false);
        //}
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((stationToMovePos - energyBallTransform.position).magnitude < 5) disactivateAndPassTheEnergy();
        if(!station.isActiveAndEnabled) gameObject.SetActive(false); //if station is destroyed
    }
}
