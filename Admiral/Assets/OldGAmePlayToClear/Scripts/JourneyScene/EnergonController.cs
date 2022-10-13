
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnergonController : MonoBehaviour
{
    [HideInInspector]
    public bool isPlayerEnergon;

    [HideInInspector]
    public int energonLevel;

    [HideInInspector]
    public float energyCapacity;

    [HideInInspector]
    public float energyOfEnergon;

    [HideInInspector]
    public float energonMovingSpeed;

    private GameObject infoPanelLocal;
    private List<GameObject> infoPanelLocalListToActivate;
    private MiniInfoPanel miniInfoPanelObject;
    private bool isSelected;

    [HideInInspector]
    public int CPUNumber;

    [HideInInspector]
    public bool isMoving;
    public Vector3 moveToPoint;
    //[SerializeField]
    //private GameObject selectedRing;
    private Outline selectedOutline;
    [HideInInspector]
    public AudioSource engineSound;

    //this game sphere is used as identificator of player journey scene cruisre by color of belonging to. It is assigned onenable of this class
    public List<GameObject> IDColorElements;
    [HideInInspector]
    public Color colorOfEnergonMat;

    [HideInInspector]
    public LineRenderer shipMovingLine;

    [HideInInspector]
    public StationController energonsStation;
    [HideInInspector]
    public Vector3 energonsStationPosition;


    private List<float> toEmptyStationsWayLength;
    private StationController CPUStationMovingTo;
    private StationController OtherCPUStationNearWhereTheEnergonNow;
    //private Transform moveToObjectTransform;
    public float yRotation; //this var is ised to rotate CPU ship towards move rotation and safe its rotation to renew the game

    private string cpturedStationTagAway = "Gun";


    //references to player journey scene cruisers
    private string Cruis1PlayerTag = "BullCruis1";
    private string Cruis2PlayerTag = "BullCruis2";
    private string Cruis3PlayerTag = "BullCruis3";
    private string Cruis4PlayerTag = "BullCruis4";


    //references to CPU journey scene cruisers
    private string Cruis1CPUTag = "BullDstrPlay1";
    private string Cruis2CPUTag = "BullDstrPlay2";
    private string Cruis3CPUTag = "BullDstrPlay3";
    private string Cruis4CPUTag = "BullDstrPlay4";

    private StationController stationVisited; //to lock the possibility of gathering the energy from same station allways for player

    private void OnEnable()
    {
        toEmptyStationsWayLength = new List<float>();
        selectedOutline = GetComponent<Outline>();
        selectedOutline.enabled = false;
    }

    private void OnDisable()
    {
        disactivateInfoAboutShip();
        StopAllCoroutines();
        //selectedRing.SetActive(false);
        selectedOutline.enabled = false;
    }

    public void makeEnergonPlayers()
    {
        isPlayerEnergon = true;
        shipMovingLine = GetComponent<LineRenderer>();
        shipMovingLine.positionCount = 2;
        shipMovingLine.SetPosition(0, transform.position);
        shipMovingLine.SetPosition(1, transform.position);
        shipMovingLine.enabled = true;
        colorOfEnergonMat = SpaceCtrlr.Instance.getProperMatColorByIndex(Lists.playerColor);

        engineSound = GetComponent<AudioSource>();
        //setting a proper color to colored elements of Player cruiser to make it has the same color that station CPU's has
        for (int i = 0; i < IDColorElements.Count; i++)
        {
            IDColorElements[i].GetComponent<MeshRenderer>().material.SetColor("_Color", colorOfEnergonMat);
        }
        if (infoPanelLocal != null)
        {
            isSelected = false;
            if (infoPanelLocal.activeInHierarchy) infoPanelLocal.SetActive(false);
            infoPanelLocal = null;
        }
        //selectedRing.SetActive(false);
        selectedOutline.enabled = false;
        isMoving = false;
        OtherCPUStationNearWhereTheEnergonNow = null;
        addThisToSelectable();
    }
    public void makeEnergonCPUs(int CPUnumberInstance)
    {
        isPlayerEnergon = false;
        if (shipMovingLine!=null) shipMovingLine.enabled = false;
        CPUNumber = CPUnumberInstance;

        if (CPUNumber != 5)
        {
            colorOfEnergonMat = SpaceCtrlr.Instance.getProperMatColorByIndex(SpaceCtrlr.Instance.colorsOfPlayers[CPUNumber]);
            for (int i = 0; i < IDColorElements.Count; i++)
            {
                IDColorElements[i].GetComponent<MeshRenderer>().material.SetColor("_Color", colorOfEnergonMat);
            }
        }
        else {
            colorOfEnergonMat = new Color(2.4f, 2.4f, 2.4f, 1); 
            for (int i = 0; i < IDColorElements.Count; i++)
            {
                IDColorElements[i].GetComponent<MeshRenderer>().material.SetColor("_Color", colorOfEnergonMat);
            }
        }
        if (infoPanelLocal != null)
        {
            isSelected = false;
            if (infoPanelLocal.activeInHierarchy) infoPanelLocal.SetActive(false);
            infoPanelLocal = null;
        }
        //selectedRing.SetActive(false);
        selectedOutline.enabled = false;
        isMoving = false;
        addThisToSelectable();
        OtherCPUStationNearWhereTheEnergonNow = null;
        detectingStationToMove();
        //StartCoroutine (callDetectingFromStartOfScene());
    }

    //private IEnumerator callDetectingFromStartOfScene() {

    //    yield return new WaitForSeconds(0.3f);
    //    detectingStationToMove();
    //}

    private void detectingStationToMove()
    {

        //populating the special list of floats (distances to aimed point to move) by getting the lists of instances of empty station classes (CaptureLine classes)
        //and transforming theyr positions and calculating the sqrMagnitudes of vector btwn the ship and station
        List<StationController> otherStations = new List<StationController>();

        for (int i = 0; i < Lists.AllStations.Count; i++)
        {
            if ((Lists.AllStations[i].CPUNumber != CPUNumber || Lists.AllStations[i].isPlayerStation) && !Lists.AllStations[i].isGuardCoreStation && OtherCPUStationNearWhereTheEnergonNow != Lists.AllStations[i])
                otherStations.Add(Lists.AllStations[i]);
        }
        for (int i = 0; i < otherStations.Count; i++)
        {
            toEmptyStationsWayLength.Add((otherStations[i].gameObject.transform.position - transform.position).sqrMagnitude);
        }

        if (toEmptyStationsWayLength.Count > 0)
        {
            //chosing the closest point (empty station) to move to. Cause the index of emty stations is the same as index of distances to them we can take the index of minimal 
            //float (distance) from the distance lists and with it determine the closest empty station to ship and transform it's position (parent pos cause child GO is line and 
            // it stays lower than ship)
            CPUStationMovingTo = Random.Range(0, 4) > 1 ? otherStations[toEmptyStationsWayLength.IndexOf(toEmptyStationsWayLength.Min())] : otherStations[Random.Range(0, otherStations.Count)];
            //moveToObjectTransform = CPUStationMovingTo.gameObject.transform;
            moveToPoint = CPUStationMovingTo.gameObject.transform.position;
            //rotating CPU ship towar moving direction before start of move
            yRotation = Quaternion.LookRotation(moveToPoint - transform.position, Vector3.up).eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
            //isMovingToEnemyStation = true;
            isMoving = true;
            toEmptyStationsWayLength.Clear();
        }
        else if (OtherCPUStationNearWhereTheEnergonNow!=null) getBackToEnergonsStation();
    }

    private void getBackToEnergonsStation()
    {
        //moveToObjectTransform = CPUStationMovingTo.gameObject.transform;
        moveToPoint = energonsStationPosition;
        //rotating CPU ship towar moving direction before start of move
        yRotation = Quaternion.LookRotation(moveToPoint - transform.position, Vector3.up).eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        //isMovingToEnemyStation = true;
        isMoving = true;
    }

    public void goToGetEnergyBall(Vector3 energyBallPoint)
    { 
        moveToPoint = energyBallPoint;
        //rotating CPU ship towar moving direction before start of move
        yRotation = Quaternion.LookRotation(moveToPoint - transform.position, Vector3.up).eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        OtherCPUStationNearWhereTheEnergonNow = null;
        isMoving = true;
    }

    public void updateInfoPanelToDisplay()
    {

        if (infoPanelLocal!=null)
        {
            if (infoPanelLocal.activeInHierarchy)
            {
                miniInfoPanelObject.energyCount.text = energyOfEnergon.ToString("0");
                miniInfoPanelObject.energyCpacity.text = energyCapacity.ToString("0");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayerEnergon)
        {
            if (other.CompareTag(cpturedStationTagAway))
            {
                StationController sc = other.GetComponent<StationController>();

                //if station (not empty) met is belong to other CPU or player and it has no fleet, this CPU will try to capture it, but if CPU was moving to capture empty station 
                //it will ignore this station
                if (!sc.isPlayerStation && !sc.isGuardCoreStation && sc != stationVisited)
                {
                    stationVisited = sc;
                    if (Lists.isBlackDimension) energyOfEnergon += Random.Range(Constants.Instance.energyOfStationDark - 30, Constants.Instance.energyOfStationDark + 30);
                    else if (Lists.isBlueDimension) energyOfEnergon += Random.Range(Constants.Instance.energyOfStationBlue - 30, Constants.Instance.energyOfStationBlue + 30);
                    else if (Lists.isRedDimension) energyOfEnergon += Random.Range(Constants.Instance.energyOfStationRed - 30, Constants.Instance.energyOfStationRed + 30);
                    if (energyOfEnergon > energyCapacity) energyOfEnergon = energyCapacity;
                    SpaceCtrlr.Instance.gainSound.Play();
                    updateInfoPanelToDisplay();
                }
                else if (sc.isPlayerStation)
                {
                    stationVisited = sc;
                    SpaceCtrlr.Instance.changingTheEnergyOfPlayer(true, energyOfEnergon);
                    energyOfEnergon = 0;
                    updateInfoPanelToDisplay();
                }
            }

            else if (other.CompareTag(Cruis1PlayerTag) || other.CompareTag(Cruis2PlayerTag) || other.CompareTag(Cruis3PlayerTag) || other.CompareTag(Cruis4PlayerTag))
            {
                LaunchingObjcts playerCruiser = other.GetComponent<LaunchingObjcts>();
                if (playerCruiser.Cruis1 > 0) SpaceCtrlr.Instance.changingTheEnergyOfPlayer(true, energyOfEnergon * Constants.Instance.energyGetWithCruiser1);
                else if (playerCruiser.Cruis2 > 0) SpaceCtrlr.Instance.changingTheEnergyOfPlayer(true, energyOfEnergon * Constants.Instance.energyGetWithCruiser2);
                else if (playerCruiser.Cruis3 > 0) SpaceCtrlr.Instance.changingTheEnergyOfPlayer(true, energyOfEnergon * Constants.Instance.energyGetWithCruiser3);
                else SpaceCtrlr.Instance.changingTheEnergyOfPlayer(true, energyOfEnergon * Constants.Instance.energyGetWithCruiser4);
                energyOfEnergon = 0;
                stationVisited = null;
                updateInfoPanelToDisplay();
            }

            else if (other.CompareTag(Cruis1CPUTag) || other.CompareTag(Cruis2CPUTag) || other.CompareTag(Cruis3CPUTag) || other.CompareTag(Cruis4CPUTag))
            {
                CPUShipCtrlJourney CPUCruiserObj = other.GetComponent<CPUShipCtrlJourney>();

                if (CPUCruiserObj.Cruis1 > 0)
                {
                    if (CPUCruiserObj.CPUNumber == 0 && Lists.CPU1Stations.Count > 0)
                        Lists.CPU1Stations[Random.Range(0, Lists.CPU1Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser1, false);
                    else if (CPUCruiserObj.CPUNumber == 1 && Lists.CPU2Stations.Count > 0)
                        Lists.CPU2Stations[Random.Range(0, Lists.CPU2Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser1, false);
                    else if (CPUCruiserObj.CPUNumber == 2 && Lists.CPU3Stations.Count > 0)
                        Lists.CPU3Stations[Random.Range(0, Lists.CPU3Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser1, false);
                    else if (CPUCruiserObj.CPUNumber == 3 && Lists.CPU4Stations.Count > 0)
                        Lists.CPU4Stations[Random.Range(0, Lists.CPU4Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser1, false);
                }
                else if (CPUCruiserObj.Cruis2 > 0)
                {
                    if (CPUCruiserObj.CPUNumber == 0 && Lists.CPU1Stations.Count > 0)
                        Lists.CPU1Stations[Random.Range(0, Lists.CPU1Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser2, false);
                    else if (CPUCruiserObj.CPUNumber == 1 && Lists.CPU2Stations.Count > 0)
                        Lists.CPU2Stations[Random.Range(0, Lists.CPU2Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser2, false);
                    else if (CPUCruiserObj.CPUNumber == 2 && Lists.CPU3Stations.Count > 0)
                        Lists.CPU3Stations[Random.Range(0, Lists.CPU3Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser2, false);
                    else if (CPUCruiserObj.CPUNumber == 3 && Lists.CPU4Stations.Count > 0)
                        Lists.CPU4Stations[Random.Range(0, Lists.CPU4Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser2, false);
                }
                else if (CPUCruiserObj.Cruis3 > 0)
                {
                    if (CPUCruiserObj.CPUNumber == 0 && Lists.CPU1Stations.Count > 0)
                        Lists.CPU1Stations[Random.Range(0, Lists.CPU1Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser3, false);
                    else if (CPUCruiserObj.CPUNumber == 1 && Lists.CPU2Stations.Count > 0)
                        Lists.CPU2Stations[Random.Range(0, Lists.CPU2Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser3, false);
                    else if (CPUCruiserObj.CPUNumber == 2 && Lists.CPU3Stations.Count > 0)
                        Lists.CPU3Stations[Random.Range(0, Lists.CPU3Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser3, false);
                    else if (CPUCruiserObj.CPUNumber == 3 && Lists.CPU4Stations.Count > 0)
                        Lists.CPU4Stations[Random.Range(0, Lists.CPU4Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser3, false);
                }
                else
                {
                    if (CPUCruiserObj.CPUNumber == 0 && Lists.CPU1Stations.Count > 0)
                        Lists.CPU1Stations[Random.Range(0, Lists.CPU1Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser4, false);
                    else if (CPUCruiserObj.CPUNumber == 1 && Lists.CPU2Stations.Count > 0)
                        Lists.CPU2Stations[Random.Range(0, Lists.CPU2Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser4, false);
                    else if (CPUCruiserObj.CPUNumber == 2 && Lists.CPU3Stations.Count > 0)
                        Lists.CPU3Stations[Random.Range(0, Lists.CPU3Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser4, false);
                    else if (CPUCruiserObj.CPUNumber == 3 && Lists.CPU4Stations.Count > 0)
                        Lists.CPU4Stations[Random.Range(0, Lists.CPU4Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser4, false);
                }
                energyOfEnergon = 0;
                stationVisited = null;
                updateInfoPanelToDisplay();
            }

            else if (other.CompareTag("EnergyBall")) {
                energyBallMngr Eball = other.GetComponent<energyBallMngr>();
                energyOfEnergon += Eball.energyOfThisEnergyBall;
                if (energyOfEnergon > energyCapacity) energyOfEnergon = energyCapacity;
                SpaceCtrlr.Instance.gainSound.Play();
                updateInfoPanelToDisplay();
                Eball.gameObject.SetActive(false);
                Lists.energyBalls.Remove(Eball.gameObject);
            }
        }
        else
        {
            if (other.CompareTag(cpturedStationTagAway))
            {
                StationController sc = other.GetComponent<StationController>();

                //isMoving = false;
                //if station (not empty) met is belong to other CPU or player and it has no fleet, this CPU will try to capture it, but if CPU was moving to capture empty station 
                //it will ignore this station
                if (!sc.isGuardCoreStation)
                {
                    isMoving = false;
                    if (sc.CPUNumber != CPUNumber || sc.isPlayerStation)
                    {
                        OtherCPUStationNearWhereTheEnergonNow = sc;
                        if (Lists.isBlackDimension) energyOfEnergon += Random.Range(Constants.Instance.energyOfStationDark - 30, Constants.Instance.energyOfStationDark + 30);
                        else if (Lists.isBlueDimension) energyOfEnergon += Random.Range(Constants.Instance.energyOfStationBlue - 30, Constants.Instance.energyOfStationBlue + 30);
                        else if (Lists.isRedDimension) energyOfEnergon += Random.Range(Constants.Instance.energyOfStationRed - 30, Constants.Instance.energyOfStationRed + 30);
                        if (energyOfEnergon > energyCapacity) energyOfEnergon = energyCapacity;
                        if (energyOfEnergon == energyCapacity) getBackToEnergonsStation();
                        else {
                            if (Random.Range(0, 2) > 0 && Lists.AllStations.Count > 2) detectingStationToMove();
                            else getBackToEnergonsStation();
                        }
                    }
                    else if (sc.CPUNumber == CPUNumber)
                    {
                        OtherCPUStationNearWhereTheEnergonNow = null;
                        detectingStationToMove();
                        sc.setCPUStatioFleetAfterGettingEnergy(energyOfEnergon, false);
                        energyOfEnergon = 0;
                    }
                    updateInfoPanelToDisplay();
                }
            }

            else if (other.CompareTag(Cruis1PlayerTag) || other.CompareTag(Cruis2PlayerTag) || other.CompareTag(Cruis3PlayerTag) || other.CompareTag(Cruis4PlayerTag))
            {
                LaunchingObjcts playerCruiser = other.GetComponent<LaunchingObjcts>();
                if (playerCruiser.Cruis1>0) SpaceCtrlr.Instance.changingTheEnergyOfPlayer(true, energyOfEnergon* Constants.Instance.energyGetWithCruiser1);
                else if (playerCruiser.Cruis2 > 0) SpaceCtrlr.Instance.changingTheEnergyOfPlayer(true, energyOfEnergon * Constants.Instance.energyGetWithCruiser2);
                else if (playerCruiser.Cruis3 > 0) SpaceCtrlr.Instance.changingTheEnergyOfPlayer(true, energyOfEnergon * Constants.Instance.energyGetWithCruiser3);
                else SpaceCtrlr.Instance.changingTheEnergyOfPlayer(true, energyOfEnergon * Constants.Instance.energyGetWithCruiser4);
                energyOfEnergon = 0;
                updateInfoPanelToDisplay();
                //111 is default and does not make any change to method call, it will attack only player stations. So if CPUNumber !=5 means that this is not guards energon 
                if (CPUNumber!=5) energonsStation.launchingAProperCruiserToAttackAStationAsRevenge(111, true, true); 
            }

            else if (other.CompareTag(Cruis1CPUTag) || other.CompareTag(Cruis2CPUTag) || other.CompareTag(Cruis3CPUTag) || other.CompareTag(Cruis4CPUTag))
            {
                CPUShipCtrlJourney CPUCruiserObj = other.GetComponent<CPUShipCtrlJourney>();
                if (CPUCruiserObj.CPUNumber != CPUNumber)
                {
                    if (CPUCruiserObj.Cruis1 > 0)
                    {
                        if (CPUCruiserObj.CPUNumber == 0 && Lists.CPU1Stations.Count > 0) 
                            Lists.CPU1Stations[Random.Range(0, Lists.CPU1Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser1, false); 
                        else if (CPUCruiserObj.CPUNumber == 1 && Lists.CPU2Stations.Count > 0)
                            Lists.CPU2Stations[Random.Range(0, Lists.CPU2Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser1, false);
                        else if (CPUCruiserObj.CPUNumber == 2 && Lists.CPU3Stations.Count > 0)
                            Lists.CPU3Stations[Random.Range(0, Lists.CPU3Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser1, false);
                        else if (CPUCruiserObj.CPUNumber == 3 && Lists.CPU4Stations.Count > 0)
                            Lists.CPU4Stations[Random.Range(0, Lists.CPU4Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser1, false);
                    }
                    else if (CPUCruiserObj.Cruis2 > 0)
                    {
                        if (CPUCruiserObj.CPUNumber == 0 && Lists.CPU1Stations.Count > 0)
                            Lists.CPU1Stations[Random.Range(0, Lists.CPU1Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser2, false);
                        else if (CPUCruiserObj.CPUNumber == 1 && Lists.CPU2Stations.Count > 0)
                            Lists.CPU2Stations[Random.Range(0, Lists.CPU2Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser2, false);
                        else if (CPUCruiserObj.CPUNumber == 2 && Lists.CPU3Stations.Count > 0)
                            Lists.CPU3Stations[Random.Range(0, Lists.CPU3Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser2, false);
                        else if (CPUCruiserObj.CPUNumber == 3 && Lists.CPU4Stations.Count > 0)
                            Lists.CPU4Stations[Random.Range(0, Lists.CPU4Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser2, false);
                    }
                    else if (CPUCruiserObj.Cruis3 > 0)
                    {
                        if (CPUCruiserObj.CPUNumber == 0 && Lists.CPU1Stations.Count > 0)
                            Lists.CPU1Stations[Random.Range(0, Lists.CPU1Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser3, false);
                        else if (CPUCruiserObj.CPUNumber == 1 && Lists.CPU2Stations.Count > 0)
                            Lists.CPU2Stations[Random.Range(0, Lists.CPU2Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser3, false);
                        else if (CPUCruiserObj.CPUNumber == 2 && Lists.CPU3Stations.Count > 0)
                            Lists.CPU3Stations[Random.Range(0, Lists.CPU3Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser3, false);
                        else if (CPUCruiserObj.CPUNumber == 3 && Lists.CPU4Stations.Count > 0)
                            Lists.CPU4Stations[Random.Range(0, Lists.CPU4Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser3, false);
                    }
                    else
                    {
                        if (CPUCruiserObj.CPUNumber == 0 && Lists.CPU1Stations.Count > 0)
                            Lists.CPU1Stations[Random.Range(0, Lists.CPU1Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser4, false);
                        else if (CPUCruiserObj.CPUNumber == 1 && Lists.CPU2Stations.Count > 0)
                            Lists.CPU2Stations[Random.Range(0, Lists.CPU2Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser4, false);
                        else if (CPUCruiserObj.CPUNumber == 2 && Lists.CPU3Stations.Count > 0)
                            Lists.CPU3Stations[Random.Range(0, Lists.CPU3Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser4, false);
                        else if (CPUCruiserObj.CPUNumber == 3 && Lists.CPU4Stations.Count > 0)
                            Lists.CPU4Stations[Random.Range(0, Lists.CPU4Stations.Count)].setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser4, false);
                    }
                    energyOfEnergon = 0;
                    updateInfoPanelToDisplay(); 
                    //So if CPUNumber != 5 means that this is not guards energon
                    if (CPUNumber!=5) energonsStation.launchingAProperCruiserToAttackAStationAsRevenge(CPUCruiserObj.CPUNumber, false,true);
                }
                else {
                    if (CPUCruiserObj.Cruis1 > 0)
                    {
                        energonsStation.setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser1, false);
                    }
                    else if (CPUCruiserObj.Cruis2 > 0)
                    {
                        energonsStation.setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser2, false);
                    }
                    else if (CPUCruiserObj.Cruis3 > 0)
                    {
                        energonsStation.setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser3, false);
                    }
                    else
                    {
                        energonsStation.setCPUStatioFleetAfterGettingEnergy(energyOfEnergon * Constants.Instance.energyGetWithCruiser4, false);
                    }
                    energyOfEnergon = 0;
                    updateInfoPanelToDisplay();
                }
            }

            else if (other.CompareTag("EnergyBall"))
            {
                energyBallMngr Eball = other.GetComponent<energyBallMngr>();
                energyOfEnergon += Eball.energyOfThisEnergyBall;
                if (energyOfEnergon > energyCapacity) energyOfEnergon = energyCapacity;
                updateInfoPanelToDisplay(); 
                Lists.energyBalls.Remove(Eball.gameObject);
                Eball.gameObject.SetActive(false);
                if (Lists.energyBalls.Count > 0) goToGetEnergyBall(Lists.energyBalls[0].transform.position);
                else getBackToEnergonsStation();
            }
        }
    }

    //only used for CPU energons
    public void showInfoAboutShip()
    {
        infoPanelLocalListToActivate = ObjectPullerJourney.current.GetminiInfoPanelOnlyEnergyPullList();
        if (infoPanelLocal != null)
        {
            if (infoPanelLocal.activeInHierarchy) infoPanelLocal.SetActive(false);
            infoPanelLocal = null;
        }
        infoPanelLocal = ObjectPullerJourney.current.GetUniversalBullet(infoPanelLocalListToActivate);
        miniInfoPanelObject = infoPanelLocal.GetComponent<MiniInfoPanel>();
        infoPanelLocal.transform.position = SpaceCtrlr.Instance.mainCam.WorldToScreenPoint(transform.position);
        miniInfoPanelObject.energyCount.text = energyOfEnergon.ToString("0");
        miniInfoPanelObject.energyCpacity.text = energyCapacity.ToString("0");
        isSelected = true;
        infoPanelLocal.SetActive(true);
    }

    //only used for CPU energons
    public void disactivateInfoAboutShip()
    {
        isSelected = false;
        if (infoPanelLocal != null)
        {
            if (infoPanelLocal.activeInHierarchy) infoPanelLocal.SetActive(false);
            infoPanelLocal = null;
        }
        StopAllCoroutines();
    }

    //only used for player energons
    public void SelectedAndReady()
    {
        SelectingBox.Instance.ifAnyShipChousen = true;
        //SelectingBox.Instance.chosenShipLineRenderer.Add(shipMovingLine);
        SelectingBox.Instance.chosenEnergonShipObj.Add(this);
        //selectedRing.SetActive(true);
        selectedOutline.enabled = true;
        infoPanelLocalListToActivate = ObjectPullerJourney.current.GetminiInfoPanelOnlyEnergyPullList();
        if (infoPanelLocal != null)
        {
            isSelected = false;
            if (infoPanelLocal.activeInHierarchy) infoPanelLocal.SetActive(false);
            infoPanelLocal = null;
        }
        infoPanelLocal = ObjectPullerJourney.current.GetUniversalBullet(infoPanelLocalListToActivate);
        miniInfoPanelObject = infoPanelLocal.GetComponent<MiniInfoPanel>();
        infoPanelLocal.transform.position = SpaceCtrlr.Instance.mainCam.WorldToScreenPoint(transform.position);
        miniInfoPanelObject.energyCount.text = energyOfEnergon.ToString("0");
        miniInfoPanelObject.energyCpacity.text = energyCapacity.ToString("0");
        infoPanelLocal.SetActive(true);
        isSelected = true;
    }


    //only used for player energons
    public void giveAShipMoveOrder(Vector3 moveTowards)
    {
        moveToPoint = moveTowards;
        float yRotation = Quaternion.LookRotation(moveToPoint - transform.position, Vector3.up).eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        //selectedRing.SetActive(false);
        selectedOutline.enabled = false;
        engineSound.Play();
        isMoving = true;
        isSelected = false;
        if (infoPanelLocal != null)
        {
            if (infoPanelLocal.activeInHierarchy) infoPanelLocal.SetActive(false);
        }
        infoPanelLocal = null;
        shipMovingLine.SetPosition(1, moveTowards);
    }

    //only used for player energons
    public void turnOffSelectedRing()
    {
        //selectedRing.SetActive(false);
        selectedOutline.enabled = false;
        if (infoPanelLocal != null)
        {
            if (infoPanelLocal.activeInHierarchy) infoPanelLocal.SetActive(false);
            infoPanelLocal = null;
        }
    }

    public void addThisToSelectable()
    {
        if (isPlayerEnergon)
        {
            if (SelectingBox.Instance.selectableEnergonCPU.Contains(this))
            {
                SelectingBox.Instance.selectableEnergonPlayer.Add(this);
                SelectingBox.Instance.selectableEnergonCPU.Remove(this);
            }
            else SelectingBox.Instance.selectableEnergonPlayer.Add(this);
        }
        else
        {
            if (SelectingBox.Instance.selectableEnergonPlayer.Contains(this))
            {
                SelectingBox.Instance.selectableEnergonPlayer.Remove(this);
                SelectingBox.Instance.selectableEnergonCPU.Add(this);
            }
            else SelectingBox.Instance.selectableEnergonCPU.Add(this);
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            if (isPlayerEnergon) shipMovingLine.SetPosition(0, transform.position);
            transform.position = Vector3.MoveTowards(transform.position, moveToPoint, energonMovingSpeed);
            if (transform.position == moveToPoint)
            {
                isMoving = false;
                if (isPlayerEnergon && engineSound.isPlaying) engineSound.Stop();
                else if (!isPlayerEnergon) {
                    OtherCPUStationNearWhereTheEnergonNow = null;
                    detectingStationToMove();
                } 
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected && infoPanelLocal != null)
        {
            infoPanelLocal.transform.position = SpaceCtrlr.Instance.mainCam.WorldToScreenPoint(transform.position);
        }
    }
}
