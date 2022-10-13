using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionPLayerStations : Singleton<ConnectionPLayerStations>
{
    [HideInInspector]
    public GameObject ObjectPulled;
    [HideInInspector]
    public List<GameObject> ObjectPulledList;
    public GameObject lowEnegryImg;
    private Transform lowEnenrgyImgLocalTRansform;
    private LineRenderer lineToDragFromPlayerStation;
    [HideInInspector]
    public bool lineIsDruggedFromPlayerStation;
    private Camera selectCamera;
    //private float screenToCameraDistance;

    //private float cameraDeviataion;
    //private Vector3 cameraDeviataionVector;

    public GameObject connectionSensorUI;
    private Transform connectionSensorUITransform;
    public GameObject connectionSignalUI;
    private Transform connectionSignalUITransform;
    public RectTransform connectionSensorRect;
    private StationClass stationConnectionStartFrom;
    private StationClass stationToConnect;
    private Vector3 screenPos;
    private bool sensorOnStation;

    // Start is called before the first frame update
    void Start()
    {
        sensorOnStation = false;
        connectionSignalUITransform = connectionSignalUI.transform;
        connectionSensorUITransform = connectionSensorUI.transform;

        selectCamera = Camera.main;
        //cameraDeviataion = (selectCamera.transform.position - Vector3.zero).magnitude;
        //cameraDeviataionVector = selectCamera.transform.position - Vector3.zero;
        //screenToCameraDistance = selectCamera.farClipPlane;
        lineIsDruggedFromPlayerStation = false;
        lowEnenrgyImgLocalTRansform = lowEnegryImg.transform;
    }
    public void instantiateConnectionLineToDragFromStation(Vector3 stationPosition, StationClass station)
    {
        ObjectPulledList = ObjectPullerRTS.current.GetConnectionLinePull();
        ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
        lineToDragFromPlayerStation = ObjectPulled.GetComponent<LineRenderer>();
        lineToDragFromPlayerStation.SetPosition(0, stationPosition);
        ObjectPulled.SetActive(true);
        stationConnectionStartFrom = station;
        lineIsDruggedFromPlayerStation = true;
        connectionSensorUI.SetActive(true);
    }

    public void disactivateConnectionLine()
    {
        lineIsDruggedFromPlayerStation = false;
        sensorOnStation = false;
        if (connectionSignalUI.activeInHierarchy) connectionSignalUI.SetActive(false);
        connectionSensorUI.SetActive(false);
        if (ObjectPulled != null) ObjectPulled.SetActive(false);
        ObjectPulled = null;
    }
    private void setConnection()
    {
        sensorOnStation = false;
        lineIsDruggedFromPlayerStation = false;
        if (connectionSignalUI.activeInHierarchy) connectionSignalUI.SetActive(false);
        connectionSensorUI.SetActive(false);
        ObjectPulled = null;
        lineToDragFromPlayerStation.SetPosition(1, stationToConnect.stationPosition);
        lineToDragFromPlayerStation.endColor = new Color(stationToConnect.colorOfStationMat.r, stationToConnect.colorOfStationMat.g, stationToConnect.colorOfStationMat.b, 0.3f); 
        lineToDragFromPlayerStation.startColor = new Color(stationToConnect.colorOfStationMat.r, stationToConnect.colorOfStationMat.g, stationToConnect.colorOfStationMat.b, 0.3f);
        GameObject energyTransporter = lineToDragFromPlayerStation.gameObject.transform.GetChild(0).gameObject;
        ConnectionLine lineScript = lineToDragFromPlayerStation.gameObject.GetComponent<ConnectionLine>();
        lineScript.stations.Add(stationToConnect);
        lineScript.stations.Add(stationConnectionStartFrom);
        lineScript.setTheSpeedOfTransporter();
        //if there is no connection line collection yet in dictionary we create it first
        if (!CommonProperties.connectionLines.ContainsKey(0)) CommonProperties.connectionLines.Add(0, new List<ConnectionLine>());
        CommonProperties.connectionLines[0].Add(lineScript);
        energyTransporter.transform.position = stationConnectionStartFrom.stationPosition;
        energyTransporter.transform.rotation = Quaternion.LookRotation(stationToConnect.stationPosition- stationConnectionStartFrom.stationPosition, Vector3.up);
        energyTransporter.GetComponent<MeshRenderer>().material.SetColor("_Color", stationConnectionStartFrom.colorOfStationMat);
        energyTransporter.SetActive(true);
        lineScript.lineIsSet = true;
        setStationGroupForPlayer();
        if (stationConnectionStartFrom.groupWhereTheStationIs != null && stationConnectionStartFrom.groupWhereTheStationIs.Count > 0)
        {
            CommonProperties.energyOfStationGroups[stationConnectionStartFrom.groupWhereTheStationIs] -= stationConnectionStartFrom.energyToConnection;
        }
        else stationConnectionStartFrom.energyOfStation -= stationConnectionStartFrom.energyToConnection;
        foreach (StationPlayerRTS stationPlayer in CommonProperties.playerStations) stationPlayer.checkIfStationCanConnect();
    }

    private void setStationGroupForPlayer() {
        //creating new connection group
        if (stationConnectionStartFrom.ConnectedStations.Count < 1 && stationToConnect.ConnectedStations.Count < 1) {
            List<List <StationClass>> newGroup = new List<List<StationClass>>();
            List<StationClass> newConnection = new List<StationClass>(); 
            if (CommonProperties.StationGroups.ContainsKey(stationConnectionStartFrom.CPUNumber)) CommonProperties.StationGroups[stationConnectionStartFrom.CPUNumber].Add(newConnection);
            //so if there no station groups of this CPU it is first created
            else
            {
                CommonProperties.StationGroups.Add(stationConnectionStartFrom.CPUNumber, newGroup);
                newGroup.Add(newConnection);
            }
            newGroup.Add(newConnection);
            newConnection.Add(stationConnectionStartFrom);
            stationConnectionStartFrom.ConnectedStations.Add(stationToConnect);
            //stationConnectionStartFrom.connectionsCount++;
            stationConnectionStartFrom.groupWhereTheStationIs = newConnection;
            newConnection.Add(stationToConnect);
            stationToConnect.ConnectedStations.Add(stationConnectionStartFrom);
            stationToConnect.groupWhereTheStationIs= newConnection;
            CommonProperties.energyOfStationGroups.Add(newConnection,0);
            CommonProperties.energyOfStationGroups[newConnection] = stationToConnect.energyOfStation + stationConnectionStartFrom.energyOfStation;

            //setting the limit of energy for station group
            CommonProperties.energyLimitOfStationGroups.Add(newConnection, CommonProperties.getTheEnergyLimitOfStationsGroup(newConnection));

            //leaving some energy to station for a case if it will in future be out of any group
            stationConnectionStartFrom.energyOfStation = 30; 
            stationToConnect.energyOfStation = 30;
        }
        //connecting one station to the connection group
        else if (stationConnectionStartFrom.ConnectedStations.Count < 1) {
            stationConnectionStartFrom.groupWhereTheStationIs = stationToConnect.groupWhereTheStationIs;
            stationToConnect.groupWhereTheStationIs.Add(stationConnectionStartFrom);
            stationToConnect.ConnectedStations.Add(stationConnectionStartFrom);
            stationConnectionStartFrom.ConnectedStations.Add(stationToConnect);
            CommonProperties.energyOfStationGroups[stationToConnect.groupWhereTheStationIs] += stationConnectionStartFrom.energyOfStation;

            //setting the limit of energy for station group
            CommonProperties.energyLimitOfStationGroups[stationToConnect.groupWhereTheStationIs]=CommonProperties.getTheEnergyLimitOfStationsGroup(stationToConnect.groupWhereTheStationIs);

            stationConnectionStartFrom.energyOfStation = 30; //leaving some energy to station for a case if it will in future be out of any group
        }
        //connecting one station to the connection group
        else if (stationToConnect.ConnectedStations.Count < 1) {
            stationToConnect.groupWhereTheStationIs = stationConnectionStartFrom.groupWhereTheStationIs;
            stationConnectionStartFrom.groupWhereTheStationIs.Add(stationToConnect);
            stationToConnect.ConnectedStations.Add(stationConnectionStartFrom);
            stationConnectionStartFrom.ConnectedStations.Add(stationToConnect);
            CommonProperties.energyOfStationGroups[stationConnectionStartFrom.groupWhereTheStationIs] += stationToConnect.energyOfStation;

            //setting the limit of energy for station group
            CommonProperties.energyLimitOfStationGroups[stationConnectionStartFrom.groupWhereTheStationIs] = CommonProperties.getTheEnergyLimitOfStationsGroup(stationConnectionStartFrom.groupWhereTheStationIs);

            stationToConnect.energyOfStation = 30;//leaving some energy to station for a case if it will in future be out of any group
        }
        //connecting one connection group to other connection group
        else if (stationConnectionStartFrom.ConnectedStations.Count> 0 && stationToConnect.ConnectedStations.Count > 0) {

            if (stationConnectionStartFrom.groupWhereTheStationIs != stationToConnect.groupWhereTheStationIs)
            {
                List<StationClass> tempClass = stationToConnect.groupWhereTheStationIs;
                foreach (StationClass station in tempClass)
                {
                    stationConnectionStartFrom.groupWhereTheStationIs.Add(station);
                    station.groupWhereTheStationIs = stationConnectionStartFrom.groupWhereTheStationIs;
                    CommonProperties.energyOfStationGroups[stationConnectionStartFrom.groupWhereTheStationIs] += station.energyOfStation;
                    station.energyOfStation = 30; //leaving some energy to station for a case if it will in future be out of any group
                }

                //setting the limit of energy for station group
                CommonProperties.energyLimitOfStationGroups[stationConnectionStartFrom.groupWhereTheStationIs] = CommonProperties.getTheEnergyLimitOfStationsGroup(stationConnectionStartFrom.groupWhereTheStationIs);

                stationToConnect.ConnectedStations.Add(stationConnectionStartFrom);
                stationConnectionStartFrom.ConnectedStations.Add(stationToConnect);
                //stationConnectionStartFrom.connectionsCount++;
                //stationToConnect.connectionsCount++;
                CommonProperties.StationGroups[stationToConnect.CPUNumber].Remove(tempClass);
                CommonProperties.energyOfStationGroups.Remove(tempClass);
                CommonProperties.energyLimitOfStationGroups.Remove(tempClass);
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

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch _touch = Input.GetTouch(0);
            if (_touch.phase == TouchPhase.Ended)
            {
                if (lowEnegryImg.activeInHierarchy) lowEnegryImg.SetActive(false);
                if (lineIsDruggedFromPlayerStation)
                {
                    if (sensorOnStation) setConnection();
                    else disactivateConnectionLine();
                }
            }
            if (lowEnegryImg.activeInHierarchy)
            {
                lowEnenrgyImgLocalTRansform.position = _touch.position;
            }
            if (lineIsDruggedFromPlayerStation)
            {
                Vector3 moveToPointFromTouch = Vector3.zero;
                Plane plane = new Plane(Vector3.up, 0);
                float distance;
                Ray ray = Camera.main.ScreenPointToRay(_touch.position);
                if (plane.Raycast(ray, out distance))
                {
                    moveToPointFromTouch = ray.GetPoint(distance);
                }

                lineToDragFromPlayerStation.SetPosition(1, moveToPointFromTouch);

                connectionSensorUITransform.position = _touch.position;
            }

        }
        //two finger touch disactivates the connection function
        if (Input.touchCount > 1)
        {
            if (lowEnegryImg.activeInHierarchy) lowEnegryImg.SetActive(false);
            if (lineIsDruggedFromPlayerStation)
            {
                disactivateConnectionLine();
                //lineIsDruggedFromPlayerStation = false;
                //if (connectionSignalUI.activeInHierarchy) connectionSignalUI.SetActive(false);
                //connectionSensorUI.SetActive(false);
                //ObjectPulled.SetActive(false);
                //ObjectPulled = null;
            }
        }

        if (lineIsDruggedFromPlayerStation)
        {
            Vector2 min = connectionSensorRect.anchoredPosition - (connectionSensorRect.sizeDelta / 2);
            Vector2 max = connectionSensorRect.anchoredPosition + (connectionSensorRect.sizeDelta / 2);

            if (!connectionSignalUI.activeInHierarchy)
            {
                for (int i = 0; i < CommonProperties.playerStations.Count; i++)
                {
                    //140 is one step colse station distance
                    if (CommonProperties.playerStations[i] != stationConnectionStartFrom && (CommonProperties.playerStations[i].stationPosition- stationConnectionStartFrom.stationPosition).magnitude<140 
                        && (stationConnectionStartFrom.groupWhereTheStationIs == null || (stationConnectionStartFrom.groupWhereTheStationIs!=null && !stationConnectionStartFrom.groupWhereTheStationIs.Contains(CommonProperties.playerStations[i]))))
                    {
                        screenPos = selectCamera.WorldToScreenPoint(CommonProperties.playerStations[i].stationPosition);

                        if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
                        {
                            connectionSignalUITransform.position = connectionSensorUITransform.position;
                            connectionSignalUI.SetActive(true);
                            sensorOnStation = true;
                            stationToConnect = CommonProperties.playerStations[i];
                            break;
                        }
                    }
                }
            }
            else
            {
                connectionSignalUITransform.position = connectionSensorUITransform.position;
                if (screenPos.x < min.x || screenPos.x > max.x || screenPos.y < min.y || screenPos.y > max.y)
                {
                    if (connectionSignalUI.activeInHierarchy)
                    {
                        connectionSignalUI.SetActive(false);
                        sensorOnStation = false;
                    }
                    stationToConnect = null;
                }
            }

        }
    }
}
