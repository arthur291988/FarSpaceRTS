using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static System.Collections.Specialized.BitVector32;

public class ConnectionCPUStations : MonoBehaviour
{
    private const int BASE_STATION_DEFENCE_SHIPS_COUNT = 12;
    //private const int MINIMUM_ENENRGY_TO_START_STATION_UPGRADE_LOOP = 250;
    //private const int MINIMUM_ENENRGY_TO_START_GUN_UPGRADE_LOOP = 200;
    //private const int MINIMUM_ENENRGY_TO_START_SHIP_PRODUCE_LOOP = 100;

    [HideInInspector]
    public static GameObject ObjectPulled;
    [HideInInspector]
    public static List<GameObject> ObjectPulledList;
    private static LineRenderer lineToDragFromStation;
    private static StationClass stationConnectionStartFrom;
    private static StationClass stationToConnect;
    //// Start is called before the first frame update
    //void Start()
    //{
    //}
    public static void instantiateConnectionLine()
    {
        ObjectPulledList = ObjectPullerRTS.current.GetConnectionLinePull();
        ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
        lineToDragFromStation = ObjectPulled.GetComponent<LineRenderer>();
        lineToDragFromStation.SetPosition(0, stationConnectionStartFrom.stationPosition);
        ObjectPulled.SetActive(true);
    }

    public static void setConnections(StationClass stationConnectionStartFromParam, StationClass stationToConnectParam) {

        ObjectPulled = null;
        ObjectPulledList = null;
        stationConnectionStartFrom = stationConnectionStartFromParam;
        stationToConnect = stationToConnectParam;
        instantiateConnectionLine();
        lineToDragFromStation.SetPosition(1, stationToConnect.stationPosition);
        lineToDragFromStation.endColor = new Color(stationConnectionStartFrom.colorOfStationMat.r, stationToConnect.colorOfStationMat.g, stationToConnect.colorOfStationMat.b, 0.3f);
        lineToDragFromStation.startColor = new Color(stationConnectionStartFrom.colorOfStationMat.r, stationToConnect.colorOfStationMat.g, stationToConnect.colorOfStationMat.b, 0.3f);
        GameObject energyTransporter = lineToDragFromStation.gameObject.transform.GetChild(0).gameObject;
        ConnectionLine lineScript = lineToDragFromStation.gameObject.GetComponent<ConnectionLine>();
        lineScript.stations.Add(stationToConnect);
        lineScript.stations.Add(stationConnectionStartFrom);
        lineScript.setTheSpeedOfTransporter();
        //Debug.Log(lineScript.stations.Count);
        //if there is no connection line collection yet in dictionary we create it first
        if (!CommonProperties.connectionLines.ContainsKey(stationConnectionStartFrom.CPUNumber)) CommonProperties.connectionLines.Add(stationConnectionStartFrom.CPUNumber, new List<ConnectionLine>());
        CommonProperties.connectionLines[stationConnectionStartFrom.CPUNumber].Add(lineScript);
        energyTransporter.transform.position = stationConnectionStartFrom.stationPosition;
        energyTransporter.transform.rotation = Quaternion.LookRotation(stationToConnect.stationPosition - stationConnectionStartFrom.stationPosition, Vector3.up);
        energyTransporter.GetComponent<MeshRenderer>().material.SetColor("_Color", stationConnectionStartFrom.colorOfStationMat);
        energyTransporter.SetActive(true);
        lineScript.lineIsSet = true;
        setStationGroupForCPU();
        //Debug.Log(lineScript.stations.Count + "  this one is second call");
        if (stationConnectionStartFrom.groupWhereTheStationIs != null && stationConnectionStartFrom.groupWhereTheStationIs.Count > 0)
        {
            CommonProperties.energyOfStationGroups[stationConnectionStartFrom.groupWhereTheStationIs] -= stationConnectionStartFrom.energyToConnection;
        }
        else stationConnectionStartFrom.energyOfStation -= stationConnectionStartFrom.energyToConnection;
        foreach (StationPlayerRTS stationPlayer in CommonProperties.playerStations) stationPlayer.checkIfStationCanConnect();
    }
    private static void setStationGroupForCPU()
    {
        //creating new connection group
        if (stationConnectionStartFrom.ConnectedStations.Count < 1 && stationToConnect.ConnectedStations.Count < 1)
        {
            List<List<StationClass>> newGroup = new List<List<StationClass>>();
            List<StationClass> newConnection = new List<StationClass>();
            if (CommonProperties.StationGroups.ContainsKey(stationConnectionStartFrom.CPUNumber)) 
                CommonProperties.StationGroups[stationConnectionStartFrom.CPUNumber].Add(newConnection);
            //so if there no station groups of this CPU it is first created
            else
            {
                CommonProperties.StationGroups.Add(stationConnectionStartFrom.CPUNumber, newGroup);
                newGroup.Add(newConnection);
            }
            newConnection.Add(stationConnectionStartFrom);
            stationConnectionStartFrom.ConnectedStations.Add(stationToConnect);
            //stationConnectionStartFrom.connectionsCount++;
            stationConnectionStartFrom.groupWhereTheStationIs = newConnection;
            newConnection.Add(stationToConnect);
            stationToConnect.ConnectedStations.Add(stationConnectionStartFrom);
            stationToConnect.groupWhereTheStationIs = newConnection;
            CommonProperties.energyOfStationGroups.Add(newConnection, 0);
            CommonProperties.energyOfStationGroups[newConnection] = stationToConnect.energyOfStation + stationToConnect.energyOfStationToUPGradeStation + stationToConnect.energyOfStationToUPGradeGun + stationToConnect.energyOfStationToSetConnection +
                stationConnectionStartFrom.energyOfStation + stationConnectionStartFrom.energyOfStationToUPGradeStation + stationConnectionStartFrom.energyOfStationToUPGradeGun + stationConnectionStartFrom.energyOfStationToSetConnection;

            //setting the limit of energy for station group
            CommonProperties.energyLimitOfStationGroups.Add(newConnection, CommonProperties.getTheEnergyLimitOfStationsGroup(newConnection));

            //leaving some energy to station for a case if it will in future be out of any group
            stationConnectionStartFrom.energyOfStation = 30;
            stationConnectionStartFrom.energyOfStationToUPGradeStation = 0;
            stationConnectionStartFrom.energyOfStationToUPGradeGun = 0;
            stationConnectionStartFrom.energyOfStationToSetConnection = 0;
            stationToConnect.energyOfStation = 30;
            stationToConnect.energyOfStationToUPGradeStation = 0;
            stationToConnect.energyOfStationToUPGradeGun = 0;
            stationToConnect.energyOfStationToSetConnection = 0;

        }
        //connecting one station to the connection group
        else if (stationConnectionStartFrom.ConnectedStations.Count < 1)
        {
            stationConnectionStartFrom.groupWhereTheStationIs = stationToConnect.groupWhereTheStationIs;
            stationToConnect.groupWhereTheStationIs.Add(stationConnectionStartFrom);
            stationToConnect.ConnectedStations.Add(stationConnectionStartFrom);
            stationConnectionStartFrom.ConnectedStations.Add(stationToConnect);
            CommonProperties.energyOfStationGroups[stationToConnect.groupWhereTheStationIs] += stationConnectionStartFrom.energyOfStation;
            CommonProperties.energyOfStationGroups[stationToConnect.groupWhereTheStationIs] += stationConnectionStartFrom.energyOfStationToUPGradeStation;
            CommonProperties.energyOfStationGroups[stationToConnect.groupWhereTheStationIs] += stationConnectionStartFrom.energyOfStationToUPGradeGun;
            CommonProperties.energyOfStationGroups[stationToConnect.groupWhereTheStationIs] += stationConnectionStartFrom.energyOfStationToSetConnection;

            //setting the limit of energy for station group
            CommonProperties.energyLimitOfStationGroups[stationToConnect.groupWhereTheStationIs] = CommonProperties.getTheEnergyLimitOfStationsGroup(stationToConnect.groupWhereTheStationIs);

            //leaving some energy to station for a case if it will in future be out of any group
            stationConnectionStartFrom.energyOfStation = 30;
            stationConnectionStartFrom.energyOfStationToUPGradeStation = 0;
            stationConnectionStartFrom.energyOfStationToUPGradeGun = 0;
            stationConnectionStartFrom.energyOfStationToSetConnection = 0; 
        }
        //connecting one station to the connection group
        else if (stationToConnect.ConnectedStations.Count < 1)
        {
            stationToConnect.groupWhereTheStationIs = stationConnectionStartFrom.groupWhereTheStationIs;
            stationConnectionStartFrom.groupWhereTheStationIs.Add(stationToConnect);
            stationToConnect.ConnectedStations.Add(stationConnectionStartFrom);
            stationConnectionStartFrom.ConnectedStations.Add(stationToConnect);
            CommonProperties.energyOfStationGroups[stationConnectionStartFrom.groupWhereTheStationIs] += stationToConnect.energyOfStation;
            CommonProperties.energyOfStationGroups[stationConnectionStartFrom.groupWhereTheStationIs] += stationToConnect.energyOfStationToUPGradeStation;
            CommonProperties.energyOfStationGroups[stationConnectionStartFrom.groupWhereTheStationIs] += stationToConnect.energyOfStationToUPGradeGun;
            CommonProperties.energyOfStationGroups[stationConnectionStartFrom.groupWhereTheStationIs] += stationToConnect.energyOfStationToSetConnection;

            //setting the limit of energy for station group
            CommonProperties.energyLimitOfStationGroups[stationConnectionStartFrom.groupWhereTheStationIs] = CommonProperties.getTheEnergyLimitOfStationsGroup(stationConnectionStartFrom.groupWhereTheStationIs);

            //leaving some energy to station for a case if it will in future be out of any group
            stationToConnect.energyOfStation = 30;
            stationToConnect.energyOfStationToUPGradeStation = 0;
            stationToConnect.energyOfStationToUPGradeGun = 0;
            stationToConnect.energyOfStationToSetConnection = 0;
        }
        //connecting one connection group to other connection group
        else if (stationConnectionStartFrom.ConnectedStations.Count > 0 && stationToConnect.ConnectedStations.Count > 0)
        {

            if (stationConnectionStartFrom.groupWhereTheStationIs != stationToConnect.groupWhereTheStationIs)
            {
                List<StationClass> tempClass = stationToConnect.groupWhereTheStationIs;
                foreach (StationClass station in tempClass)
                {
                    stationConnectionStartFrom.groupWhereTheStationIs.Add(station);
                    station.groupWhereTheStationIs = stationConnectionStartFrom.groupWhereTheStationIs;
                    CommonProperties.energyOfStationGroups[stationConnectionStartFrom.groupWhereTheStationIs] += station.energyOfStation;
                    CommonProperties.energyOfStationGroups[stationConnectionStartFrom.groupWhereTheStationIs] += station.energyOfStationToUPGradeStation;
                    CommonProperties.energyOfStationGroups[stationConnectionStartFrom.groupWhereTheStationIs] += station.energyOfStationToUPGradeGun;
                    CommonProperties.energyOfStationGroups[stationConnectionStartFrom.groupWhereTheStationIs] += station.energyOfStationToSetConnection;

                    //leaving some energy to station for a case if it will in future be out of any group
                    station.energyOfStation = 30;
                    station.energyOfStationToUPGradeStation = 0;
                    station.energyOfStationToUPGradeGun = 0;
                    station.energyOfStationToSetConnection = 0;
                }
                stationToConnect.ConnectedStations.Add(stationConnectionStartFrom);
                stationConnectionStartFrom.ConnectedStations.Add(stationToConnect);
                //stationConnectionStartFrom.connectionsCount++;
                //stationToConnect.connectionsCount++;
                CommonProperties.StationGroups[stationToConnect.CPUNumber].Remove(tempClass);
                CommonProperties.energyOfStationGroups.Remove(tempClass);
                CommonProperties.energyLimitOfStationGroups.Remove(tempClass);
                tempClass = null;

                //setting the limit of energy for station group
                CommonProperties.energyLimitOfStationGroups.Add(stationConnectionStartFrom.groupWhereTheStationIs, CommonProperties.getTheEnergyLimitOfStationsGroup(stationConnectionStartFrom.groupWhereTheStationIs));

            }
            //else
            //{
            //    stationToConnect.ConnectedStations.Add(stationConnectionStartFrom);
            //    stationConnectionStartFrom.ConnectedStations.Add(stationToConnect);
            //    //stationConnectionStartFrom.connectionsCount++;
            //    //stationToConnect.connectionsCount++;
            //}
        }
    }

    public static void distributeGroupEnergy(List<StationClass> groupsWhereTheStationIs)
    {
        //first is to provide all stations with defence minimum ships
        for (int i = 0; i < groupsWhereTheStationIs.Count; i++)
        {
            //defence minimum increases if the station level is higer
            if (groupsWhereTheStationIs[i].ShipsAssigned < (BASE_STATION_DEFENCE_SHIPS_COUNT+groupsWhereTheStationIs[i].stationCurrentLevel)) 
                groupsWhereTheStationIs[i].utilaizeTheEnergyOfCPUGroup(1);
        }

        int possibleConnections = 0;
        for (int i = 0; i < groupsWhereTheStationIs.Count; i++)
        {
            groupsWhereTheStationIs[i].checkIfStationCanConnect();
            if (groupsWhereTheStationIs[i].stationToConnect != null)
            {
                if (groupsWhereTheStationIs[i].energyToConnection <= CommonProperties.energyOfStationGroups[groupsWhereTheStationIs])
                {
                    CommonProperties.energyOfStationGroups[groupsWhereTheStationIs] -= groupsWhereTheStationIs[i].energyToConnection;
                    setConnections(groupsWhereTheStationIs[i], groupsWhereTheStationIs[i].stationToConnect);
                }
                else possibleConnections++;
            }
        }

        if (possibleConnections == 0)
        {
            upgradeOrProduceShips(groupsWhereTheStationIs);
        }

        #region old code
        ////second is to capture as more as possible stars
        //if (CommonProperties.stars.Count > 0)
        //{
        //    if (groupsWhereTheStationIs.Count > 2)
        //    {
        //        if (Random.Range(0, 10) == 0)
        //        {
        //            for (int i = 0; i < groupsWhereTheStationIs.Count; i++)
        //            {
        //                if (groupsWhereTheStationIs[i].ShipsAssigned < groupsWhereTheStationIs[i].ShipsLimit) groupsWhereTheStationIs[i].utilaizeTheEnergyOfCPUGroup(2);
        //            }
        //        }
        //        else
        //        {
        //            int possibleConnections = 0;
        //            for (int i = 0; i < groupsWhereTheStationIs.Count; i++)
        //            {
        //                groupsWhereTheStationIs[i].checkIfStationCanConnect();
        //                if (groupsWhereTheStationIs[i].stationToConnect != null)
        //                {
        //                    if (groupsWhereTheStationIs[i].energyToConnection <= CommonProperties.energyOfStationGroups[groupsWhereTheStationIs])
        //                    {
        //                        CommonProperties.energyOfStationGroups[groupsWhereTheStationIs] -= groupsWhereTheStationIs[i].energyToConnection;
        //                        setConnections(groupsWhereTheStationIs[i], groupsWhereTheStationIs[i].stationToConnect);
        //                    }
        //                    else possibleConnections++;
        //                }
        //            }

        //            if (possibleConnections == 0)
        //            {
        //                upgradeOrProduceShips(groupsWhereTheStationIs);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (Random.Range(0, 5) > 0)
        //        {
        //            for (int i = 0; i < groupsWhereTheStationIs.Count; i++)
        //            {
        //                if (groupsWhereTheStationIs[i].ShipsAssigned < groupsWhereTheStationIs[i].ShipsLimit) groupsWhereTheStationIs[i].utilaizeTheEnergyOfCPUGroup(2);
        //            }
        //        }
        //        else
        //        {
        //            int possibleConnections = 0;
        //            for (int i = 0; i < groupsWhereTheStationIs.Count; i++)
        //            {
        //                groupsWhereTheStationIs[i].checkIfStationCanConnect();
        //                if (groupsWhereTheStationIs[i].stationToConnect != null)
        //                {
        //                    if (groupsWhereTheStationIs[i].energyToConnection <= CommonProperties.energyOfStationGroups[groupsWhereTheStationIs])
        //                    {
        //                        CommonProperties.energyOfStationGroups[groupsWhereTheStationIs] -= groupsWhereTheStationIs[i].energyToConnection;
        //                        setConnections(groupsWhereTheStationIs[i], groupsWhereTheStationIs[i].stationToConnect);
        //                    }
        //                    else possibleConnections++;
        //                }
        //            }

        //            if (possibleConnections == 0)
        //            {
        //                upgradeOrProduceShips(groupsWhereTheStationIs);
        //            }
        //        }
        //    }
        //}

        ////third is connections, upgrades, and guns
        //else {
        //    //if there is any possiable connections and even if there is no enough energy yet, the goup accumulate the energy
        //    int possibleConnections = 0;
        //    for (int i = 0; i < groupsWhereTheStationIs.Count; i++)
        //    {
        //        groupsWhereTheStationIs[i].checkIfStationCanConnect();
        //        if (groupsWhereTheStationIs[i].stationToConnect != null)
        //        {
        //            if (groupsWhereTheStationIs[i].energyToConnection <= CommonProperties.energyOfStationGroups[groupsWhereTheStationIs])
        //            {
        //                CommonProperties.energyOfStationGroups[groupsWhereTheStationIs] -= groupsWhereTheStationIs[i].energyToConnection;
        //                setConnections(groupsWhereTheStationIs[i], groupsWhereTheStationIs[i].stationToConnect);
        //            }
        //            else possibleConnections++;
        //        }
        //    }

        //    if (possibleConnections == 0)
        //    {
        //        upgradeOrProduceShips(groupsWhereTheStationIs);
        //    }
        //}

        //Debug.Log("After " + CommonProperties.energyOfStationGroups[groupsWhereTheStationIs]);
        #endregion 
    }

    private static void upgradeOrProduceShips(List<StationClass> groupsWhereTheStationIs)
    {
        int possibleStationUpgrades = 0;

        StationClass stationToUpgrade = null;
        for (int i = 0; i < groupsWhereTheStationIs.Count; i++)
        {
            //upgrade the station only of ot has enough connections 
            if (groupsWhereTheStationIs[i].stationCurrentLevel<groupsWhereTheStationIs[i].upgradeCounts && 
                groupsWhereTheStationIs[i].stationCurrentLevel<groupsWhereTheStationIs[i].ConnectedStations.Count)
            {
                if (stationToUpgrade == null) stationToUpgrade = groupsWhereTheStationIs[i];
                else if (stationToUpgrade.stationCurrentLevel < groupsWhereTheStationIs[i].stationCurrentLevel) stationToUpgrade = groupsWhereTheStationIs[i];
            }
            //if (groupsWhereTheStationIs[i].stationCurrentLevel < groupsWhereTheStationIs[i].upgradeCounts &&
            //    groupsWhereTheStationIs[i].stationCurrentLevel < groupsWhereTheStationIs[i].ConnectedStations.Count) possibleStationUpgrades++;
        }
        if (stationToUpgrade != null && stationToUpgrade.energyToNextUpgradeOfStation <= CommonProperties.energyOfStationGroups[groupsWhereTheStationIs])
        {
            stationToUpgrade.utilaizeTheEnergyOfCPUGroup(3);
            stationToUpgrade = null;
        }
        else possibleStationUpgrades++;

        if (possibleStationUpgrades == 0)
        {
            for (int i = 0; i < groupsWhereTheStationIs.Count; i++)
            {
                if (groupsWhereTheStationIs[i].stationGunLevel < groupsWhereTheStationIs[i].GunUpgradeCounts)
                {
                    if (stationToUpgrade == null) stationToUpgrade = groupsWhereTheStationIs[i];
                    else if (stationToUpgrade.ConnectedStations.Count < groupsWhereTheStationIs[i].ConnectedStations.Count) stationToUpgrade = groupsWhereTheStationIs[i];
                }
            }
            if (stationToUpgrade != null && stationToUpgrade.ConnectedStations.Count > 1 &&
                stationToUpgrade.energyToNextUpgradeOfGun <= CommonProperties.energyOfStationGroups[groupsWhereTheStationIs])
            {
                stationToUpgrade.utilaizeTheEnergyOfCPUGroup(4);
                stationToUpgrade = null;
            }
        }

        //if (possibleStationUpgrades == 0)
        //{
        //    for (int i = 0; i < groupsWhereTheStationIs.Count; i++)
        //    {
        //        if (groupsWhereTheStationIs[i].energyToNextUpgradeOfGun <= CommonProperties.energyOfStationGroups[groupsWhereTheStationIs] && groupsWhereTheStationIs[i].stationGunLevel < groupsWhereTheStationIs[i].GunUpgradeCounts)
        //        {
        //            groupsWhereTheStationIs[i].utilaizeTheEnergyOfCPUGroup(4);
        //        }
        //    }
        //}

        for (int i = 0; i < groupsWhereTheStationIs.Count; i++)
        {
            if (stationToUpgrade == null) stationToUpgrade = groupsWhereTheStationIs[i];
            else if (stationToUpgrade.stationCurrentLevel < groupsWhereTheStationIs[i].stationCurrentLevel &&
                groupsWhereTheStationIs[i].ShipsAssigned< groupsWhereTheStationIs[i].ShipsLimit) {
                stationToUpgrade = groupsWhereTheStationIs[i];
            }
            else if (stationToUpgrade.stationCurrentLevel >= groupsWhereTheStationIs[i].stationCurrentLevel &&
                stationToUpgrade.ShipsAssigned == stationToUpgrade.ShipsLimit)
            {
                stationToUpgrade = groupsWhereTheStationIs[i];
            }
        }
        if (stationToUpgrade!=null && stationToUpgrade.ShipsAssigned < stationToUpgrade.ShipsLimit) stationToUpgrade.utilaizeTheEnergyOfCPUGroup(2);
        #region old code
        //if (Random.Range(0, 5) > 0)
        //{
        //    if (CommonProperties.energyOfStationGroups[groupsWhereTheStationIs] >= MINIMUM_ENENRGY_TO_START_STATION_UPGRADE_LOOP)
        //    {
        //        for (int i = 0; i < groupsWhereTheStationIs.Count; i++)
        //        {
        //            if (groupsWhereTheStationIs[i].energyToNextUpgradeOfStation <= CommonProperties.energyOfStationGroups[groupsWhereTheStationIs] && groupsWhereTheStationIs[i].stationCurrentLevel < groupsWhereTheStationIs[i].upgradeCounts)
        //            {
        //                groupsWhereTheStationIs[i].utilaizeTheEnergyOfCPUGroup(3);
        //            }
        //        }
        //    }
        //    if (Random.Range(0, 5) == 0 && CommonProperties.energyOfStationGroups[groupsWhereTheStationIs] >= MINIMUM_ENENRGY_TO_START_GUN_UPGRADE_LOOP)
        //    {
        //        for (int i = 0; i < groupsWhereTheStationIs.Count; i++)
        //        {
        //            if (groupsWhereTheStationIs[i].energyToNextUpgradeOfGun <= CommonProperties.energyOfStationGroups[groupsWhereTheStationIs] && groupsWhereTheStationIs[i].stationGunLevel < groupsWhereTheStationIs[i].GunUpgradeCounts)
        //            {
        //                groupsWhereTheStationIs[i].utilaizeTheEnergyOfCPUGroup(4);
        //            }
        //        }
        //    }
        //}
        //else
        //{
        //    if (CommonProperties.energyOfStationGroups[groupsWhereTheStationIs] >= MINIMUM_ENENRGY_TO_START_SHIP_PRODUCE_LOOP)
        //    {
        //        for (int i = 0; i < groupsWhereTheStationIs.Count; i++)
        //        {
        //            if (groupsWhereTheStationIs[i].ShipsAssigned < groupsWhereTheStationIs[i].ShipsLimit) groupsWhereTheStationIs[i].utilaizeTheEnergyOfCPUGroup(2);
        //        }
        //    }
        //}
        #endregion
    }

}
