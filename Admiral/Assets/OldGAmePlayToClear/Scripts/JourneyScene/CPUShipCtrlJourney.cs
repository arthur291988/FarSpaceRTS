using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CPUShipCtrlJourney : MonoBehaviour /*Singleton<CPUShipCtrlJourney>*/
{
    private List<float> toEmptyStationsWayLength;
    public Vector3 moveToPoint; //point to move next for ship
    LineRenderer captureLaser; //laser effect of the ship
    //public bool isWaiting; //the state of ship when it is waiting/ratating near it's station
    public bool isMoving; //the state of ship when it is waiting/ratating near it's station

    private Transform moveToObjectTransform;

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

    private StationController CPUStationMovingTo;
    //public StationController CPUStationMovingAroundNow;
    private CaptureLine EmptyStationMovingTo;
    private LaunchingObjcts playerShipToAttack;
    public CPUShipCtrlJourney CPUCruiserObject;

    //this game sphere is used as identificator of CPU journey scene cruisre by color of belonging to. It is assigned from station controller script while launching a cruier
    public List<GameObject> IDColorElements;

    public float energy;
    public int Cruis4;
    public int Cruis3;
    public int Cruis2;
    public int Cruis1;
    public int CruisG;
    public int Destr4;
    public int Destr3;
    public int Destr2;
    public int Destr2Par;
    public int Destr1;
    public int Destr1Par;
    public int DestrG;
    public int Gun3;
    public int Gun2;
    public int Gun1;
    public int MiniGun;
    public int Fighter;

    private string C1 = "C1";
    private string C2 = "C2";
    private string C3 = "C3";
    private string C4 = "C4";
    private string CG = "CG";
    private string D1 = "D1";
    private string D1P = "D1P";
    private string D2 = "D2";
    private string D2P = "D2P";
    private string D3 = "D3";
    private string D4 = "D4";
    private string DG = "DG";
    private string G1 = "G1";
    private string G2 = "G2";
    private string G3 = "G3";
    private string GM = "GM";
    private string EN = "EN";
    private string FI = "FI";

    private float CPUCruisCommonMoveSpeed;

    public float yRotation; //this var is ised to rotate CPU ship towards move rotation and safe its rotation to renew the game

    //tags of station launch places 
    private string noUpStationPlaceTag = "BullCruisPlay1";
    private string Up1StationPlaceTag = "BullCruisPlay2";
    private string Up2StationPlaceTag = "BullCruisPlay3";
    private string Up3StationPlaceTag = "BullCruisPlay4";

    //private string cpturedStationTagCloser = "Gun";
    private string cpturedStationTagAway = "Gun";

    //trigger to starts capture effect
    //private bool captureIsOn;

    //this one is to pull the capture effect from the puller
    private List<GameObject> captureLaserListToActivate;

    //capture effect that appears inside of station while it is capturing
    private GameObject captureEffect;


    //this var determine the number of CPUCruiser (each instance of this class is managed as some exact CPU cruiser controller)
    //the order is 0, 1, 2, 3
    public int CPUNumber;

    public GameObject StationThisCPU; //to hold a reference to empty station
    public CaptureLine captureLine; //to hold a reference to empty station object
    private Vector3 rotateAroundStation;

    //is used to populate with all ships of station and take only the % of fleet
    private List<string> allCruisersAndDestrs;
    private List<string> cruisersAndDestrAfterReduce;

    private List<GameObject> BurstList;
    private GameObject BurstReal;


    private GameObject infoPanelLocal;
    private List<GameObject> infoPanelLocalListToActivate;
    private MiniInfoPanel miniInfoPanelObject;
    private bool isSelected;

    //private bool isAfterEnergon; //to change the rotation of ship while chasing the energon otherwise the rotation will not change and be cached

    private LineRenderer shipMovingLine;

    private void OnEnable()
    {
        allCruisersAndDestrs = new List<string>();
        cruisersAndDestrAfterReduce = new List<string>();

        toEmptyStationsWayLength = new List<float>();
        if (name.Contains("4")) CPUCruisCommonMoveSpeed = Constants.Instance.CPUCruis4Speed;
        else if (name.Contains("3")) CPUCruisCommonMoveSpeed = Constants.Instance.CPUCruis3Speed;
        else if (name.Contains("2")) CPUCruisCommonMoveSpeed = Constants.Instance.CPUCruis2Speed;
        else if (name.Contains("1")) CPUCruisCommonMoveSpeed = Constants.Instance.CPUCruis1Speed;

        captureLaser = GetComponent<LineRenderer>();

        StartCoroutine(addThisToSelectable());
        //this line is necessary to prevent a bug of miss capturing the empty station, it is when the CPU cruiser (that was disabled while capturing the empty station)
        //is respawned on scene after some battle and it restarts the effect of capture but it does not captures the station in fact cause CaptureLine class turns false
        //all the facts of capturing the station on it's enable method. So the true shoud be restored for this exact cruiser cause it's capture line will be enabled after respawn
        if (captureLine != null) activateCaprtureEffect(CPUNumber);

        //this one is used while determining if player win the level, so if there left no enemy cruisers on level player wins
        //Lists.enemyCruisersOnScene++;

        //adding this ship GO to the static lists of ships on scene to hold them while switching the scene and activating them after come back to journey scene
        //to continue thir taks
        //Lists.shipsOnScene.Add(gameObject);

        shipMovingLine = transform.GetChild(0).GetComponent<LineRenderer>();
        shipMovingLine.positionCount = 2;
        shipMovingLine.SetPosition(0, transform.position);
        shipMovingLine.SetPosition(1, transform.position);
        if (isMoving) shipMovingLine.SetPosition(1, moveToPoint);
        else shipMovingLine.SetPosition(1, transform.position);
    }
    private void OnDisable()
    {
        if (captureLaser.enabled)
        {
            if (CPUNumber == 0) captureLine.IsCPU1Filling = false;
            else if (CPUNumber == 1) captureLine.IsCPU2Filling = false;
            else if (CPUNumber == 2) captureLine.IsCPU3Filling = false;
            else if (CPUNumber == 3) captureLine.IsCPU4Filling = false;
        }
        if (captureEffect) captureEffect.SetActive(false);
        captureLaser.enabled = false;
        disactivateInfoAboutShip();
        StopAllCoroutines();
    }
    private IEnumerator addThisToSelectable()
    {
        yield return new WaitForSeconds(0.3f);
        SelectingBox.Instance.selectableShipsCPU.Add(this);
    }

    //this method is called as revenge reaction (for attacking this CPUs energon) 
    public void detectingWeakestStationToAttack(int CPUPlayerNumber, bool isPlayerStation) {
        if (isPlayerStation)
        { //this is player stations attack
            if (Lists.playerStations.Count > 0)
            {
                for (int i = 0; i < Lists.playerStations.Count; i++)
                {
                    if (i == 0) CPUStationMovingTo = Lists.playerStations[i];
                    //chosing the weakest station to attack
                    else if (Lists.playerStations[i].assessFleetPower() < CPUStationMovingTo.assessFleetPower()) CPUStationMovingTo = Lists.playerStations[i];
                }

                //chosing the closest point (empty station) to move to. Cause the index of emty stations is the same as index of distances to them we can take the index of minimal 
                //float (distance) from the distance lists and with it determine the closest empty station to ship and transform it's position (parent pos cause child GO is line and 
                // it stays lower than ship)
                moveToPoint = CPUStationMovingTo.transform.position;
                shipMovingLine.SetPosition(1, moveToPoint);
                //rotating CPU ship towar moving direction before start of move
                yRotation = Quaternion.LookRotation(moveToPoint - transform.position, Vector3.up).eulerAngles.y;
                transform.rotation = Quaternion.Euler(0, yRotation + 90, 0);
                //isMovingToEnemyStation = true;
                //isAfterEnergon = false;
                isMoving = true;
            }

        }
        else
        {
            //isAfterEnergon = false;
            if (CPUPlayerNumber == 0 && Lists.CPU1Stations.Count > 0)
            {
                for (int i = 0; i < Lists.CPU1Stations.Count; i++)
                {
                    if (i == 0) CPUStationMovingTo = Lists.CPU1Stations[i];
                    //chosing the weakest station to attack
                    else if (Lists.CPU1Stations[i].assessFleetPower() < CPUStationMovingTo.assessFleetPower()) CPUStationMovingTo = Lists.CPU1Stations[i];
                }

                //chosing the closest point (empty station) to move to. Cause the index of emty stations is the same as index of distances to them we can take the index of minimal 
                //float (distance) from the distance lists and with it determine the closest empty station to ship and transform it's position (parent pos cause child GO is line and 
                // it stays lower than ship)
                moveToPoint = CPUStationMovingTo.transform.position;
                shipMovingLine.SetPosition(1, moveToPoint);
                //rotating CPU ship towar moving direction before start of move
                yRotation = Quaternion.LookRotation(moveToPoint - transform.position, Vector3.up).eulerAngles.y;
                transform.rotation = Quaternion.Euler(0, yRotation + 90, 0);
                //isMovingToEnemyStation = true;
                isMoving = true;
            }
            else if (CPUPlayerNumber == 1 && Lists.CPU2Stations.Count > 0)
            {
                for (int i = 0; i < Lists.CPU2Stations.Count; i++)
                {
                    if (i == 0) CPUStationMovingTo = Lists.CPU2Stations[i];
                    //chosing the weakest station to attack
                    else if (Lists.CPU2Stations[i].assessFleetPower() < CPUStationMovingTo.assessFleetPower()) CPUStationMovingTo = Lists.CPU2Stations[i];
                }

                //chosing the closest point (empty station) to move to. Cause the index of emty stations is the same as index of distances to them we can take the index of minimal 
                //float (distance) from the distance lists and with it determine the closest empty station to ship and transform it's position (parent pos cause child GO is line and 
                // it stays lower than ship)
                moveToPoint = CPUStationMovingTo.transform.position;
                shipMovingLine.SetPosition(1, moveToPoint);
                //rotating CPU ship towar moving direction before start of move
                yRotation = Quaternion.LookRotation(moveToPoint - transform.position, Vector3.up).eulerAngles.y;
                transform.rotation = Quaternion.Euler(0, yRotation + 90, 0);
                //isMovingToEnemyStation = true;
                isMoving = true;
            }
            else if (CPUPlayerNumber == 2 && Lists.CPU3Stations.Count > 0)
            {
                for (int i = 0; i < Lists.CPU3Stations.Count; i++)
                {
                    if (i == 0) CPUStationMovingTo = Lists.CPU3Stations[i];
                    //chosing the weakest station to attack
                    else if (Lists.CPU3Stations[i].assessFleetPower() < CPUStationMovingTo.assessFleetPower()) CPUStationMovingTo = Lists.CPU3Stations[i];
                }

                //chosing the closest point (empty station) to move to. Cause the index of emty stations is the same as index of distances to them we can take the index of minimal 
                //float (distance) from the distance lists and with it determine the closest empty station to ship and transform it's position (parent pos cause child GO is line and 
                // it stays lower than ship)
                moveToPoint = CPUStationMovingTo.transform.position;
                shipMovingLine.SetPosition(1, moveToPoint);
                //rotating CPU ship towar moving direction before start of move
                yRotation = Quaternion.LookRotation(moveToPoint - transform.position, Vector3.up).eulerAngles.y;
                transform.rotation = Quaternion.Euler(0, yRotation + 90, 0);
                //isMovingToEnemyStation = true;
                isMoving = true;
            }
            else if (CPUPlayerNumber == 3 && Lists.CPU4Stations.Count > 0)
            {
                for (int i = 0; i < Lists.CPU4Stations.Count; i++)
                {
                    if (i == 0) CPUStationMovingTo = Lists.CPU4Stations[i];
                    //chosing the weakest station to attack
                    else if (Lists.CPU4Stations[i].assessFleetPower() < CPUStationMovingTo.assessFleetPower()) CPUStationMovingTo = Lists.CPU4Stations[i];
                }

                //chosing the closest point (empty station) to move to. Cause the index of emty stations is the same as index of distances to them we can take the index of minimal 
                //float (distance) from the distance lists and with it determine the closest empty station to ship and transform it's position (parent pos cause child GO is line and 
                // it stays lower than ship)
                moveToPoint = CPUStationMovingTo.transform.position;
                shipMovingLine.SetPosition(1, moveToPoint);
                //rotating CPU ship towar moving direction before start of move
                yRotation = Quaternion.LookRotation(moveToPoint - transform.position, Vector3.up).eulerAngles.y;
                transform.rotation = Quaternion.Euler(0, yRotation + 90, 0);
                //isMovingToEnemyStation = true;
                isMoving = true;

            }

        }
    }
    public void detectingWeakestStationToAttackFromAll()
    {
        //populating the special list of floats (distances to aimed point to move) by getting the lists of instances of empty station classes (CaptureLine classes)
        //and transforming theyr positions and calculating the sqrMagnitudes of vector btwn the ship and station
        List<StationController> otherStations = new List<StationController>();

        for (int i = 0; i < Lists.AllStations.Count; i++)
        {
            if ((Lists.AllStations[i].CPUNumber != CPUNumber || Lists.AllStations[i].isPlayerStation) && !Lists.AllStations[i].isGuardCoreStation) otherStations.Add(Lists.AllStations[i]);
        }
        for (int i = 0; i < otherStations.Count; i++)
        {
            if (i == 0) CPUStationMovingTo = otherStations[i];
            //chosing the weakest station to attack
            else if (otherStations[i].assessFleetPower() < CPUStationMovingTo.assessFleetPower()) CPUStationMovingTo = otherStations[i];
        }
        if (otherStations.Count > 0)
        {
            //chosing the weakest station to attack
            moveToPoint = CPUStationMovingTo.transform.position;
            shipMovingLine.SetPosition(1, moveToPoint);
            //rotating CPU ship towar moving direction before start of move
            yRotation = Quaternion.LookRotation(moveToPoint - transform.position, Vector3.up).eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, yRotation + 90, 0);
            //isMovingToEnemyStation = true;
            //isAfterEnergon = false;
            isMoving = true;
            captureLine = null;
        }
    }

    //public void detectingTransformOfObjectToMove()
    //{
    //    if (Lists.emptyStations.Count > 0 &&energy>0) detectingAnEmptyStationToMove();
    //    //else
    //    //{
    //    //    if (Lists.energonsControllablesOnScene.Count > 0)
    //    //    {
    //    //        if (Random.Range(0, 11) > 3)
    //    //        {
    //    //            //populating the special list of floats (distances to aimed point to move) by getting the lists of instances of empty station classes (CaptureLine classes)
    //    //            //and transforming theyr positions and calculating the sqrMagnitudes of vector btwn the ship and station
    //    //            //int energonFleetPower = 0;
    //    //            List<EnergonController> otherEnergons = new List<EnergonController>();
    //    //            EnergonController energonObject;
    //    //            for (int i = 0; i < Lists.energonsControllablesOnScene.Count; i++)
    //    //            {
    //    //                energonObject = Lists.energonsControllablesOnScene[i].GetComponent<EnergonController>();
    //    //                if (energonObject.CPUNumber != CPUNumber || energonObject.isPlayerEnergon) {
    //    //                    otherEnergons.Add(energonObject);
    //    //                }
    //    //            }
    //    //            if (otherEnergons.Count>0) moveToObjectTransform = otherEnergons[Random.Range(0, otherEnergons.Count)].transform;
    //    //            moveToPoint = moveToObjectTransform.position;
    //    //            shipMovingLine.SetPosition(1, moveToPoint);

    //    //            //rotating CPU ship towar moving direction before start of move
    //    //            yRotation = Quaternion.LookRotation(moveToPoint - transform.position, Vector3.up).eulerAngles.y;
    //    //            transform.rotation = Quaternion.Euler(0, yRotation + 90, 0);
    //    //            isAfterEnergon = true;
    //    //            isMoving = true;
    //    //        }
    //    //        else
    //    //        {
    //    //            detectingWeakestStationToAttackFromAll();
    //    //        }
    //    //    }
    //    //}
    //}

    //detecting closest empty station to move twards to capture it.
    public void detectingAnEmptyStationToMove()
    {
        //populating the special list of floats (distances to aimed point to move) by getting the lists of instances of empty station classes (CaptureLine classes)
        //and transforming theyr positions and calculating the sqrMagnitudes of vector btwn the ship and station
        //if (Lists.emptyStations.Count > 0) //double checking to try to prevent a bug
        //{
        foreach (CaptureLine cl in Lists.emptyStations)
        {
            //if (/*!cl.isChosenToCaptureByOther*/energy>0)
            //{
            toEmptyStationsWayLength.Add((cl.gameObject.transform.position - transform.position).sqrMagnitude);
            //}
        }

        //so if there left no empty stations on scene this CPU cruiser will look for attack a station
        //if (toEmptyStationsWayLength.Count < 1) detectingTransformOfObjectToMove();
        if (toEmptyStationsWayLength.Count < 1) detectingWeakestStationToAttackFromAll();

        else
        {
                //chosing the closest point (empty station) to move to. Cause the index of emty stations is the same as index of distances to them we can take the index of minimal 
                //float (distance) from the distance lists and with it determine the closest empty station to ship and transform it's position (parent pos cause child GO is line and 
                // it stays lower than ship)
                EmptyStationMovingTo = Lists.emptyStations[toEmptyStationsWayLength.IndexOf(toEmptyStationsWayLength.Min())];

            if (checkIfEmptyStationIsNotUnderCaptureOfThisCruiser(EmptyStationMovingTo))
            {
                //EmptyStationMovingTo.isChosenToCaptureByOther = true;
                moveToObjectTransform = EmptyStationMovingTo.gameObject.gameObject.transform.parent;
                moveToPoint = moveToObjectTransform.position;
                shipMovingLine.SetPosition(1, moveToPoint);

                //rotating CPU ship towar moving direction before start of move
                yRotation = Quaternion.LookRotation(moveToPoint - transform.position, Vector3.up).eulerAngles.y;
                transform.rotation = Quaternion.Euler(0, yRotation + 90, 0);
                //isAfterEnergon = false;
                isMoving = true;
                toEmptyStationsWayLength.Clear();
            }
            else detectingWeakestStationToAttackFromAll();
        }
        //}
        //else detectingEnemyStationToAttack();
    }

    private bool checkIfEmptyStationIsNotUnderCaptureOfThisCruiser(CaptureLine cl)
    {
        if ((CPUNumber == 0 && !cl.IsCPU1Filling) || (CPUNumber == 1 && !cl.IsCPU2Filling) || (CPUNumber == 2 && !cl.IsCPU3Filling) || (CPUNumber == 3 && !cl.IsCPU4Filling)) return true;
        else return false;
    }

    public void showInfoAboutShip()
    {
        infoPanelLocalListToActivate = ObjectPullerJourney.current.GetminiInfoPanelNoEnergyPullList(); 
        if (infoPanelLocal != null)
        {
            if (infoPanelLocal.activeInHierarchy) infoPanelLocal.SetActive(false);
            infoPanelLocal = null;
        }
        infoPanelLocal = ObjectPullerJourney.current.GetUniversalBullet(infoPanelLocalListToActivate);
        miniInfoPanelObject = infoPanelLocal.GetComponent<MiniInfoPanel>();
        infoPanelLocal.transform.position = SpaceCtrlr.Instance.mainCam.WorldToScreenPoint(transform.position);
        miniInfoPanelObject.fleetCount.text = assessFleetPower().ToString();
        isSelected = true;
        infoPanelLocal.SetActive(true);
    }
    public void disactivateInfoAboutShip()
    {
        isSelected = false;
        if (infoPanelLocal != null)
        {
            if (infoPanelLocal.activeInHierarchy) infoPanelLocal.SetActive(false);
            infoPanelLocal = null;
        }
    }


    //giving a sygnal to CPUs station to start another scene cruiser launch (by taking into account the number of CPU and highest level of CPUstations available)
    public void launchANewCruiser()
    {
        //removing this ship from the scene objects
        Lists.shipsOnScene.Remove(gameObject);
        if (CPUNumber == 0)
        {
            Lists.CPU1CruisersOnScene--;
            SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(CPUNumber + 1, Lists.CPU1Stations.Count, Lists.CPU1CruisersOnScene);
        }
        else if (CPUNumber == 1)
        {
            Lists.CPU2CruisersOnScene--;
            SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(CPUNumber + 1, Lists.CPU2Stations.Count, Lists.CPU2CruisersOnScene);
        }
        else if (CPUNumber == 2)
        {
            Lists.CPU3CruisersOnScene--;
            SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(CPUNumber + 1, Lists.CPU3Stations.Count, Lists.CPU3CruisersOnScene);
        }
        else if (CPUNumber == 3)
        {
            Lists.CPU4CruisersOnScene--;
            SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(CPUNumber + 1, Lists.CPU4Stations.Count, Lists.CPU4CruisersOnScene);
        }
        //so if there left no enemy stations and cruisers on scene player wins
        if (/*Lists.enemyCruisersOnScene == 0*/(Lists.CPU1CruisersOnScene + Lists.CPU2CruisersOnScene + Lists.CPU3CruisersOnScene + Lists.CPU4CruisersOnScene) < 1 &&
            (Lists.CPUGuardStations.Count + Lists.CPU1Stations.Count + Lists.CPU2Stations.Count + Lists.CPU3Stations.Count +
            Lists.CPU4Stations.Count) < 1) SpaceCtrlr.Instance.youWinTheGameFunction();


    }
        
    //method to start rotation around station process. It is the state of ship that is waiting for new orders
    public void moveAroundStation(Vector3 rotationPoint) {
        rotateAroundStation = rotationPoint;
    }

    private void clearTheFleetButFighters()
    {
        Cruis4 = 0;
        Cruis3 = 0;
        Cruis2 = 0;
        Cruis1 = 0;
        CruisG = 0;
        Destr4 = 0;
        Destr3 = 0;
        Destr2 = 0;
        Destr2Par = 0;
        Destr1 = 0;
        Destr1Par = 0;
        DestrG = 0;
        Gun3 = 0;
        Gun2 = 0;
        Gun1 = 0;
        MiniGun = 0;
    }
    private void clearTheFleetAll()
    {
        Cruis4 = 0;
        Cruis3 = 0;
        Cruis2 = 0;
        Cruis1 = 0;
        CruisG = 0;
        Destr4 = 0;
        Destr3 = 0;
        Destr2 = 0;
        Destr2Par = 0;
        Destr1 = 0;
        Destr1Par = 0;
        DestrG = 0;
        Gun3 = 0;
        Gun2 = 0;
        Gun1 = 0;
        MiniGun = 0;
        Fighter = 0;
    }

    public void reducingTheFleetOFCruiserOnBattle(float coeffOfReduce)
    {
        allCruisersAndDestrs.Clear();
        cruisersAndDestrAfterReduce.Clear();

        for (int i = 0; i < Cruis1; i++)
        {
            allCruisersAndDestrs.Add(C1);
        }
        for (int i = 0; i < Cruis2; i++)
        {
            allCruisersAndDestrs.Add(C2);
        }
        for (int i = 0; i < Gun3; i++)
        {
            allCruisersAndDestrs.Add(G3);
        }
        for (int i = 0; i < Destr1; i++)
        {
            allCruisersAndDestrs.Add(D1);
        }
        for (int i = 0; i < Destr1Par; i++)
        {
            allCruisersAndDestrs.Add(D1P);
        }
        for (int i = 0; i < CruisG; i++)
        {
            allCruisersAndDestrs.Add(CG);
        }
        for (int i = 0; i < Gun2; i++)
        {
            allCruisersAndDestrs.Add(G2);
        }
        for (int i = 0; i < Gun1; i++)
        {
            allCruisersAndDestrs.Add(G1);
        }
        for (int i = 0; i < Cruis3; i++)
        {
            allCruisersAndDestrs.Add(C3);
        }
        for (int i = 0; i < Destr2; i++)
        {
            allCruisersAndDestrs.Add(D2);
        }
        for (int i = 0; i < Destr2Par; i++)
        {
            allCruisersAndDestrs.Add(D2P);
        }
        for (int i = 0; i < DestrG; i++)
        {
            allCruisersAndDestrs.Add(DG);
        }
        for (int i = 0; i < Cruis4; i++)
        {
            allCruisersAndDestrs.Add(C4);
        }
        for (int i = 0; i < Destr3; i++)
        {
            allCruisersAndDestrs.Add(D3);
        }
        for (int i = 0; i < Destr4; i++)
        {
            allCruisersAndDestrs.Add(D4);
        }

        float x = allCruisersAndDestrs.Count * coeffOfReduce > 1 ? Mathf.Floor(allCruisersAndDestrs.Count * coeffOfReduce) : Mathf.Ceil(allCruisersAndDestrs.Count * coeffOfReduce);
        for (int i = 0; i < x; i++)
        {
            cruisersAndDestrAfterReduce.Add(allCruisersAndDestrs[i]);
        }
        clearTheFleetButFighters();
        for (int i = 0; i < cruisersAndDestrAfterReduce.Count; i++)
        {
            if (cruisersAndDestrAfterReduce[i] == C1)
            {
                Cruis1++;
            }
            else if (cruisersAndDestrAfterReduce[i] == C2)
            {
                Cruis2++;
            }
            else if (cruisersAndDestrAfterReduce[i] == C3)
            {
                Cruis3++;
            }
            else if (cruisersAndDestrAfterReduce[i] == C4)
            {
                Cruis4++;
            }
            else if (cruisersAndDestrAfterReduce[i] == CG)
            {
                CruisG++;
            }
            if (cruisersAndDestrAfterReduce[i] == D1)
            {
                Destr1++;
            }
            else if (cruisersAndDestrAfterReduce[i] == D1P)
            {
                Destr1Par++;
            }
            else if (cruisersAndDestrAfterReduce[i] == D2)
            {
                Destr2++;
            }
            else if (cruisersAndDestrAfterReduce[i] == D2P)
            {
                Destr2Par++;
            }
            else if (cruisersAndDestrAfterReduce[i] == D3)
            {
                Destr3++;
            }
            else if (cruisersAndDestrAfterReduce[i] == D4)
            {
                Destr4++;
            }
            else if (cruisersAndDestrAfterReduce[i] == DG)
            {
                DestrG++;
            }
            else if (cruisersAndDestrAfterReduce[i] == G1)
            {
                DestrG++;
            }
            else if (cruisersAndDestrAfterReduce[i] == G2)
            {
                DestrG++;
            }
            else if (cruisersAndDestrAfterReduce[i] == G3)
            {
                DestrG++;
            }
        }

        if (Fighter > 0) Fighter = (int)Mathf.Ceil(Fighter * 0.4f);

        if (infoPanelLocal) miniInfoPanelObject.fleetCount.text = assessFleetPower().ToString();
    }


    private void OnTriggerEnter(Collider other)
    {
        //detecting empty station creating place, stopping near it and starting capture process
        if (other.CompareTag(noUpStationPlaceTag) || other.CompareTag(Up1StationPlaceTag) || other.CompareTag(Up2StationPlaceTag) || other.CompareTag(Up3StationPlaceTag))
        {
            StationThisCPU = other.gameObject;
            captureLine = StationThisCPU.GetComponent<CaptureLine>(); 
            moveAroundStation(other.transform.parent.position);
            //StationThisCPU = other.gameObject;
            isMoving = false;
            activateCaprtureEffect(CPUNumber);
            //if (checkIfEmptyStationIsNotUnderCaptureOfThisCruiser(captureLine))
            //{
            //    moveAroundStation(other.transform.parent.position);
            //    //StationThisCPU = other.gameObject;
            //    isMoving = false;
            //    activateCaprtureEffect(CPUNumber);
            //}
            //else {
            //    captureLine = null; 
            //    isMoving = false;
            //    detectingWeakestStationToAttackFromAll();
            //    captureLine = null;
            //    StationThisCPU = null;
            //}
            
        }

        else if (other.CompareTag(cpturedStationTagAway))
        {
            StationController sc = other.GetComponent<StationController>();

            //if station (not empty) met is belong to other CPU or player and it has no fleet, this CPU will try to capture it, but if CPU was moving to capture empty station 
            //it will ignore this station
            if ((sc.CPUNumber != CPUNumber || sc.isPlayerStation) && !sc.isGuardCoreStation)
            {
                if (sc.ifStationHasCruisers() == 0)
                {
                    //so if met station is not thi CPU's it will be removed from other players stations list 
                    if (sc.isPlayerStation)
                    {
                        Lists.playerStations.Remove(sc);
                        //updating the UI of stations information of player that lost the station 
                        SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(0, Lists.playerStations.Count, 1);

                        float reduceAmount;
                        if (Lists.isBlackDimension) reduceAmount = Constants.Instance.enrgyGainLoseDark * 0.5f;
                        else if (Lists.isBlueDimension) reduceAmount = Constants.Instance.enrgyGainLoseBlue * 0.5f;
                        else reduceAmount = Constants.Instance.enrgyGainLoseRed * 0.5f;
                        SpaceCtrlr.Instance.changingTheEnergyOfPlayer(false, reduceAmount);
                        SelectingBox.Instance.selectableStations.Add(sc);
                        SelectingBox.Instance.selectableStationsPlayer.Remove(sc);
                    }
                    else
                    {
                        if (sc.CPUNumber == 0)
                        {
                            Lists.CPU1Stations.Remove(sc);
                            //updating the UI of stations information of player that lost the station 
                            SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(1, Lists.CPU1Stations.Count,
                                Lists.CPU1CruisersOnScene);
                        }
                        else if (sc.CPUNumber == 1)
                        {
                            Lists.CPU2Stations.Remove(sc);
                            //updating the UI of stations information of player that lost the station 
                            SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(2, Lists.CPU2Stations.Count,
                                Lists.CPU2CruisersOnScene);
                        }
                        else if (sc.CPUNumber == 2)
                        {
                            Lists.CPU3Stations.Remove(sc);
                            //updating the UI of stations information of player that lost the station 
                            SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(3, Lists.CPU3Stations.Count,
                                Lists.CPU3CruisersOnScene);
                        }
                        else if (sc.CPUNumber == 3)
                        {
                            Lists.CPU4Stations.Remove(sc);
                            //updating the UI of stations information of player that lost the station 
                            SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(4, Lists.CPU4Stations.Count,
                                Lists.CPU4CruisersOnScene);
                        }
                        else if (sc.isGuardStation)
                        {
                            Lists.CPUGuardStations.Remove(sc);
                        }
                    }

                    reducingTheFleetOFCruiserOnBattle(0.95f);

                    //adding this station to proper CPU's stations lists and making a according settings to station
                    if (CPUNumber == 0) Lists.CPU1Stations.Add(sc);
                    else if (CPUNumber == 1) Lists.CPU2Stations.Add(sc);
                    else if (CPUNumber == 2) Lists.CPU3Stations.Add(sc);
                    else if (CPUNumber == 3) Lists.CPU4Stations.Add(sc);

                    float energyGatheredToIncreaseFleet=0;
                    if (sc.isPlayerStation)
                    {
                        if (Lists.isBlackDimension) energyGatheredToIncreaseFleet = Random.Range(Constants.Instance.energyOfStationDark - 30, Constants.Instance.energyOfStationDark + 30);
                        else if (Lists.isBlueDimension) energyGatheredToIncreaseFleet = Random.Range(Constants.Instance.energyOfStationBlue - 30, Constants.Instance.energyOfStationBlue + 30);
                        else if (Lists.isRedDimension) energyGatheredToIncreaseFleet = Random.Range(Constants.Instance.energyOfStationRed - 30, Constants.Instance.energyOfStationRed + 30);
                    }
                    else energyGatheredToIncreaseFleet = sc.energyOfStation;

                    sc.clearAllPruductionBeforeUpgradeCPU(); //clearing the station before making it current CPU station
                    sc.isCPUStation = true; //cause the CPU got the station on that if block
                    sc.isPlayerStation = false; //cause the player got the station on that if block
                    sc.isGuardStation = false; //cause the player got the station on that if block
                    //sc.stationProductionSwitchTrigger = false;
                    sc.isUpgrading = false;
                    sc.upgradeFill = 0;

                    //setting the winner color to conquered station
                    sc.colorOfStationMat = SpaceCtrlr.Instance.getProperMatColorByIndex(SpaceCtrlr.Instance.colorsOfPlayers[CPUNumber]);

                    sc.Cruis1 = Cruis1;
                    sc.Cruis2 = Cruis2;
                    sc.Cruis3 = Cruis3;
                    sc.Cruis4 = Cruis4;
                    sc.CruisG = CruisG;
                    sc.Destr1 = Destr1;
                    sc.Destr2 = Destr2;
                    sc.Destr1Par = Destr1Par;
                    sc.Destr2Par = Destr2Par;
                    sc.Destr3 = Destr3;
                    sc.Destr4 = Destr4;
                    sc.DestrG = DestrG;
                    sc.Gun1 = Gun1;
                    sc.Gun2 = Gun2;
                    sc.Gun3 = Gun3;
                    sc.Fighter = Fighter;
                    sc.MiniGun = MiniGun;

                    sc.CPUNumber = CPUNumber;
                    sc.stationsEnergon.makeEnergonCPUs(CPUNumber);
                    sc.startProcessesForCPUFromEmptyStation();
                    //sc.launchingACPUCruiserOnScene(); //starting a process of launching a new cruiser on scene
                    sc.disactivateInfoAboutShip();
                    //starting the process of creating the fleet of station and creating the cruiser of station to give it an orders. The param of func gives the number to CPU
                    //(each instance of StationController class is managed as some exact CPU station controller or player station)
                    //sc.startProcessesForCPU(CPUNumber);
                    //starting a process of launching a proper scene cruiser on chosen station (from method)//that method does not make anymore the launch of cruiser
                    //the cruiser laucnhe is started once new cruiser is launched on scene in Station Controller script
                    //that method just resets the counts of stations on scene
                    launchANewCruiser();
                    SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(CPUNumber + 1, getProperCPUStationsCount(CPUNumber), getProperCPUCruisersOnSceneCount(CPUNumber));


                    if (Lists.isBlackDimension) sc.energyOfStation  = Random.Range(Constants.Instance.energyOfStationDark - 30, Constants.Instance.energyOfStationDark + 30);
                    else if (Lists.isBlueDimension) sc.energyOfStation = Random.Range(Constants.Instance.energyOfStationBlue - 30, Constants.Instance.energyOfStationBlue + 30);
                    else if (Lists.isRedDimension) sc.energyOfStation = Random.Range(Constants.Instance.energyOfStationRed - 30, Constants.Instance.energyOfStationRed + 30);
                     
                    //SpaceCtrlr.Instance.resetTheTimer();
                    disactivatingCurrentShipNoBurst();
                    sc.setCPUStatioFleetAfterGettingEnergy(energyGatheredToIncreaseFleet, false);
                }
                else
                {
                    //attack with switching the scene will happen only in case if CPU met player station
                    if (sc.isPlayerStation)
                    {
                        //SpaceCtrlr.Instance.SaveFile(); //saving data of scene while changing the scene cause date is not saved on quit from other scenes

                        //so real attack to player cruiser will be only if this cruiser of this CPU has at least one cruiser, otherwise there will be only simple battle
                        if ((Cruis1 + Cruis2 + Cruis3 + Cruis4) > 0)
                        {
                            if ((sc.assessFleetPower() > assessFleetPower() && ((float)assessFleetPower() / sc.assessFleetPower()) > 0.7f)
                                || (sc.assessFleetPower() < assessFleetPower() && ((float)sc.assessFleetPower() / assessFleetPower()) > 0.7f))
                            {
                                SpaceCtrlr.Instance.SaveFile(); //saving data of scene while changing the scene cause date is not saved on quit from other scenes
                                Lists.setShipsForPlayer(sc.Cruis1, sc.Cruis2, sc.Cruis3, sc.Cruis4, sc.Gun1, sc.Gun2, sc.Gun3, sc.Destr1, sc.Destr1Par, sc.Destr2, sc.Destr2Par, sc.Destr3, sc.Destr4, sc.MiniGun);

                                Lists.setShipsForCPU(Cruis1, Cruis2, Cruis3, Cruis4, CruisG, Gun1, Gun2, Gun3, Destr1, Destr1Par, Destr2, Destr2Par, Destr3, Destr4, DestrG);

                                if (Cruis1 > 0) Lists.dummyOnDefenceSceneEnemy = 1;
                                else if (Cruis2 > 0) Lists.dummyOnDefenceSceneEnemy = 2;
                                else if (Cruis3 > 0) Lists.dummyOnDefenceSceneEnemy = 3;
                                else Lists.dummyOnDefenceSceneEnemy = 4;


                                if (SpaceCtrlr.Instance.adsPanelIsOn) SpaceCtrlr.Instance.skipRewardedProposal(); //closing the rewarded ads panel proposal, if it is open while CPU attacks player 

                                //to not set any stations on scene cause here fights a ships only
                                Lists.stationTypeLists = 0;

                                if (Cruis1 > 0) Lists.dummyOnDefenceSceneEnemy = 1;
                                else if (Cruis2 > 0) Lists.dummyOnDefenceSceneEnemy = 2;
                                else if (Cruis3 > 0) Lists.dummyOnDefenceSceneEnemy = 3;
                                else Lists.dummyOnDefenceSceneEnemy = 4;

                                // sets station type to 0 so no station will be activated on battle scene 
                                if (sc.stationCurrentLevel == 0) Lists.stationTypeLists = 1;
                                else if (sc.stationCurrentLevel == 1) Lists.stationTypeLists = 4;
                                else if (sc.stationCurrentLevel == 2) Lists.stationTypeLists = 3;
                                else if (sc.stationCurrentLevel == 3) Lists.stationTypeLists = 6;

                                //saving the instance of station that is under attack to update it's fleet count later after the battle on SpaceCtrl class 
                                Lists.stationOnAttack = sc;
                                Lists.CPUShipOnAttack = gameObject;
                                //Lists.shipOnAttack = gameObject;

                                Lists.isPlayerStationOnDefence = true; //to make able player's station dummy shot the CPU ships

                                //this method performs common stuff to prepare switch the scene 
                                Lists.shipAttacksStation();

                                //assignin a proper color to UI token of opposite CPU player that is under attack 
                                Lists.colorOfOpposite = SpaceCtrlr.Instance.getProperMatColorByIndex(SpaceCtrlr.Instance.colorsOfPlayers[CPUNumber]);

                                //setting proper dummy to defende on defence scene 
                                SpaceCtrlr.Instance.setTheMaketNumberForDefenceScene(true, sc.stationCurrentLevel, null);

                                attackPlayerStation();
                            }
                            else
                            {
                                if (sc.assessFleetPower() < assessFleetPower())
                                {
                                    Lists.playerStations.Remove(sc);
                                    //updating the UI of stations information of player that lost the station 
                                    SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(0, Lists.playerStations.Count, 1);

                                    float reduceAmount;
                                    if (Lists.isBlackDimension) reduceAmount = Constants.Instance.enrgyGainLoseDark * 0.5f;
                                    else if (Lists.isBlueDimension) reduceAmount = Constants.Instance.enrgyGainLoseBlue * 0.5f;
                                    else reduceAmount = Constants.Instance.enrgyGainLoseRed * 0.5f;
                                    SpaceCtrlr.Instance.changingTheEnergyOfPlayer(false, reduceAmount);

                                    reducingTheFleetOFCruiserOnBattle(1 - ((float)sc.assessFleetPower() / assessFleetPower() * 0.75f));

                                    //adding this station to proper CPU's stations lists and making a according settings to station
                                    if (CPUNumber == 0) Lists.CPU1Stations.Add(sc);
                                    else if (CPUNumber == 1) Lists.CPU2Stations.Add(sc);
                                    else if (CPUNumber == 2) Lists.CPU3Stations.Add(sc);
                                    else if (CPUNumber == 3) Lists.CPU4Stations.Add(sc);

                                    float energyGatheredToIncreaseFleet = 0;
                                    if (Lists.isBlackDimension) energyGatheredToIncreaseFleet = Random.Range(Constants.Instance.energyOfStationDark - 30, Constants.Instance.energyOfStationDark + 30);
                                    else if (Lists.isBlueDimension) energyGatheredToIncreaseFleet = Random.Range(Constants.Instance.energyOfStationBlue - 30, Constants.Instance.energyOfStationBlue + 30);
                                    else if (Lists.isRedDimension) energyGatheredToIncreaseFleet = Random.Range(Constants.Instance.energyOfStationRed - 30, Constants.Instance.energyOfStationRed + 30);

                                    sc.clearAllPruductionBeforeUpgradeCPU(); //clearing the station before making it current CPU station
                                    sc.isCPUStation = true; //cause the CPU got the station on that if block
                                    sc.isPlayerStation = false; //cause the player got the station on that if block
                                    sc.isGuardStation = false; //cause the player got the station on that if block
                                    //sc.stationProductionSwitchTrigger = false;
                                    sc.isUpgrading = false;
                                    sc.upgradeFill = 0;

                                    //setting the winner color to conquered station
                                    sc.colorOfStationMat = SpaceCtrlr.Instance.getProperMatColorByIndex(SpaceCtrlr.Instance.colorsOfPlayers[CPUNumber]);

                                    sc.Cruis1 = Cruis1;
                                    sc.Cruis2 = Cruis2;
                                    sc.Cruis3 = Cruis3;
                                    sc.Cruis4 = Cruis4;
                                    sc.Destr1 = Destr1;
                                    sc.Destr2 = Destr2;
                                    sc.Destr1Par = Destr1Par;
                                    sc.Destr2Par = Destr2Par;
                                    sc.Destr3 = Destr3;
                                    sc.Destr4 = Destr4;
                                    sc.Gun1 = Gun1;
                                    sc.Gun2 = Gun2;
                                    sc.Gun3 = Gun3;
                                    sc.Fighter = Fighter;
                                    sc.MiniGun = MiniGun;

                                    sc.CPUNumber = CPUNumber;

                                    sc.disactivateInfoAboutShip();
                                    SelectingBox.Instance.selectableStations.Add(sc);
                                    SelectingBox.Instance.selectableStationsPlayer.Remove(sc);
                                    sc.stationsEnergon.makeEnergonCPUs(CPUNumber);
                                    sc.startProcessesForCPUFromEmptyStation();
                                    //sc.launchingACPUCruiserOnScene(); //starting a process of launching a new cruiser on scene

                                    //starting a process of launching a proper scene cruiser on chosen station (from method)//that method does not make anymore the launch of cruiser
                                    //the cruiser laucnhe is started once new cruiser is launched on scene in Station Controller script
                                    //that method just resets the counts of stations on scene
                                    launchANewCruiser();
                                    SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(CPUNumber + 1, getProperCPUStationsCount(CPUNumber), getProperCPUCruisersOnSceneCount(CPUNumber));
                                    //SpaceCtrlr.Instance.resetTheTimer();
                                    disactivatingCurrentShipNoBurst();
                                    sc.setCPUStatioFleetAfterGettingEnergy(energyGatheredToIncreaseFleet, false);
                                }
                                else
                                {
                                    launchANewCruiser(); //starting a process of launching a proper scene cruiser on chosen station (from method)
                                    sc.reducingTheFleetOFCruiserOnBattle(1 - ((float)assessFleetPower() / sc.assessFleetPower() * 0.75f));
                                    float reduceAmount;
                                    if (Lists.isBlackDimension) reduceAmount = Constants.Instance.enrgyGainLoseDark * 0.3f;
                                    else if (Lists.isBlueDimension) reduceAmount = Constants.Instance.enrgyGainLoseBlue * 0.3f;
                                    else reduceAmount = Constants.Instance.enrgyGainLoseRed * 0.3f;
                                    SpaceCtrlr.Instance.changingTheEnergyOfPlayer(true, reduceAmount);
                                    disactivatingCurrentShip(); //this disactivation with burst of CPU ship cause it was defeated
                                }
                            }
                        }
                        else
                        {
                            launchANewCruiser(); //starting a process of launching a proper scene cruiser on chosen station (from method)
                            sc.reducingTheFleetOFCruiserOnBattle(0.95f);
                            float reduceAmount;
                            if (Lists.isBlackDimension) reduceAmount = Constants.Instance.enrgyGainLoseDark * 0.3f;
                            else if (Lists.isBlueDimension) reduceAmount = Constants.Instance.enrgyGainLoseBlue * 0.3f;
                            else reduceAmount = Constants.Instance.enrgyGainLoseRed * 0.3f;
                            SpaceCtrlr.Instance.changingTheEnergyOfPlayer(true, reduceAmount);
                            disactivatingCurrentShip(); //this disactivation with burst of CPU ship cause it was defeated
                        }

                    }
                    //if met station is not player's one CPU will just turn other CPU's station to it's one if it has stronger fleet, otherwise it will disappear and reduce 
                    //the attacked station fleet to one step lower
                    else
                    {
                        if ((Cruis1 + Cruis2 + Cruis3 + Cruis4) > 0)
                        {
                            //so CPU captures the station only if it's fleet stronger than attacked station fleet
                            if (assessFleetPower() >= sc.assessFleetPower())
                            {
                                if (sc.CPUNumber == 0)
                                {
                                    Lists.CPU1Stations.Remove(sc);
                                    //updating the UI of stations information of player that lost the station 
                                    SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(1, Lists.CPU1Stations.Count,
                                        Lists.CPU1CruisersOnScene);
                                }
                                else if (sc.CPUNumber == 1)
                                {
                                    Lists.CPU2Stations.Remove(sc);
                                    //updating the UI of stations information of player that lost the station 
                                    SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(2, Lists.CPU2Stations.Count,
                                        Lists.CPU2CruisersOnScene);
                                }
                                else if (sc.CPUNumber == 2)
                                {
                                    Lists.CPU3Stations.Remove(sc);
                                    //updating the UI of stations information of player that lost the station 
                                    SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(3, Lists.CPU3Stations.Count,
                                        Lists.CPU3CruisersOnScene);
                                }
                                else if (sc.CPUNumber == 3)
                                {
                                    Lists.CPU4Stations.Remove(sc);
                                    //updating the UI of stations information of player that lost the station 
                                    SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(4, Lists.CPU4Stations.Count,
                                        Lists.CPU4CruisersOnScene);
                                }
                                else if (sc.isGuardStation)
                                {
                                    Lists.CPUGuardStations.Remove(sc);
                                }

                                reducingTheFleetOFCruiserOnBattle(1 - ((float)sc.assessFleetPower() / assessFleetPower() * 0.75f));

                                //adding this station to proper CPU's stations lists and making a according settings to station
                                if (CPUNumber == 0) Lists.CPU1Stations.Add(sc);
                                else if (CPUNumber == 1) Lists.CPU2Stations.Add(sc);
                                else if (CPUNumber == 2) Lists.CPU3Stations.Add(sc);
                                else if (CPUNumber == 3) Lists.CPU4Stations.Add(sc);

                                float energyGatheredToIncreaseFleet = 0;
                                if (sc.isPlayerStation)
                                {
                                    if (Lists.isBlackDimension) energyGatheredToIncreaseFleet = Random.Range(Constants.Instance.energyOfStationDark - 30, Constants.Instance.energyOfStationDark + 30);
                                    else if (Lists.isBlueDimension) energyGatheredToIncreaseFleet = Random.Range(Constants.Instance.energyOfStationBlue - 30, Constants.Instance.energyOfStationBlue + 30);
                                    else if (Lists.isRedDimension) energyGatheredToIncreaseFleet = Random.Range(Constants.Instance.energyOfStationRed - 30, Constants.Instance.energyOfStationRed + 30);
                                }
                                else energyGatheredToIncreaseFleet = sc.energyOfStation;
                                
                                sc.clearAllPruductionBeforeUpgradeCPU(); //clearing the station before making it current CPU station
                                sc.isCPUStation = true; //cause the CPU got the station on that if block
                                sc.isPlayerStation = false; //cause the player got the station on that if block
                                sc.isGuardStation = false; //cause the player got the station on that if block
                                //sc.stationProductionSwitchTrigger = false;
                                sc.isUpgrading = false;
                                sc.upgradeFill = 0;

                                //setting the winner color to conquered station
                                sc.colorOfStationMat = SpaceCtrlr.Instance.getProperMatColorByIndex(SpaceCtrlr.Instance.colorsOfPlayers[CPUNumber]);

                                sc.Cruis1 = Cruis1;
                                sc.Cruis2 = Cruis2;
                                sc.Cruis3 = Cruis3;
                                sc.Cruis4 = Cruis4;
                                sc.CruisG = CruisG;
                                sc.Destr1 = Destr1;
                                sc.Destr2 = Destr2;
                                sc.Destr1Par = Destr1Par;
                                sc.Destr2Par = Destr2Par;
                                sc.Destr3 = Destr3;
                                sc.Destr4 = Destr4;
                                sc.DestrG = DestrG;
                                sc.Gun1 = Gun1;
                                sc.Gun2 = Gun2;
                                sc.Gun3 = Gun3;
                                sc.Fighter = Fighter;
                                sc.MiniGun = MiniGun;

                                sc.CPUNumber = CPUNumber;
                                sc.stationsEnergon.makeEnergonCPUs(CPUNumber);
                                sc.startProcessesForCPUFromEmptyStation();
                                //sc.launchingACPUCruiserOnScene(); //starting a process of launching a new cruiser on scene

                                sc.disactivateInfoAboutShip();
                                //starting a process of launching a proper scene cruiser on chosen station (from method)//that method does not make anymore the launch of cruiser
                                //the cruiser laucnhe is started once new cruiser is launched on scene in Station Controller script
                                //that method just resets the counts of stations on scene
                                launchANewCruiser();
                                SpaceCtrlr.Instance.resetTheCountsOfStationIconsWhileOnScene(CPUNumber + 1, getProperCPUStationsCount(CPUNumber), getProperCPUCruisersOnSceneCount(CPUNumber)); 
                                

                                if (Lists.isBlackDimension) sc.energyOfStation = Random.Range(Constants.Instance.energyOfStationDark - 30, Constants.Instance.energyOfStationDark + 30);
                                else if (Lists.isBlueDimension) sc.energyOfStation = Random.Range(Constants.Instance.energyOfStationBlue - 30, Constants.Instance.energyOfStationBlue + 30);
                                else if (Lists.isRedDimension) sc.energyOfStation = Random.Range(Constants.Instance.energyOfStationRed - 30, Constants.Instance.energyOfStationRed + 30);
                                //SpaceCtrlr.Instance.resetTheTimer();
                                disactivatingCurrentShipNoBurst();
                                sc.setCPUStatioFleetAfterGettingEnergy(energyGatheredToIncreaseFleet, false);
                            }
                            else
                            {
                                launchANewCruiser(); //starting a process of launching a proper scene cruiser on chosen station (from method)

                                sc.reducingTheFleetOFCruiserOnBattle(1 - ((float)assessFleetPower() / sc.assessFleetPower() * 0.75f));
                                disactivatingCurrentShip(); //this disactivation with burst of CPU ship cause it was defeated
                            }
                        }
                        else
                        {
                            launchANewCruiser(); //starting a process of launching a proper scene cruiser on chosen station (from method)
                            sc.reducingTheFleetOFCruiserOnBattle(0.95f);
                            disactivatingCurrentShip(); //this disactivation with burst of CPU ship cause it was defeated
                        }
                    }
                }
            }
        }

        else if (other.CompareTag(Cruis1PlayerTag) || other.CompareTag(Cruis2PlayerTag) || other.CompareTag(Cruis3PlayerTag) || other.CompareTag(Cruis4PlayerTag))
        {
            playerShipToAttack = other.GetComponent<LaunchingObjcts>();
            //so scene switch with turning to battle or guard scene will be only in case if player cruiser has at least one cruiser
            if ((playerShipToAttack.Cruis1 + playerShipToAttack.Cruis2 + playerShipToAttack.Cruis3 + playerShipToAttack.Cruis4) > 0)
            {
                //so real attack to player cruiser will be only if this cruiser of this CPU has at least one cruiser, otherwise there will be only simple battle
                if ((Cruis1 + Cruis2 + Cruis3 + Cruis4) > 0)
                {
                    if ((playerShipToAttack.assessFleetPower() > assessFleetPower() && ((float)assessFleetPower() / playerShipToAttack.assessFleetPower()) > 0.7f)
                        || (playerShipToAttack.assessFleetPower() < assessFleetPower() && ((float)playerShipToAttack.assessFleetPower() / assessFleetPower()) > 0.7f))
                    {

                        SpaceCtrlr.Instance.SaveFile(); //saving data of scene while changing the scene cause date is not saved on quit from other scenes

                        Lists.setShipsForPlayer(playerShipToAttack.Cruis1, playerShipToAttack.Cruis2, playerShipToAttack.Cruis3, playerShipToAttack.Cruis4, playerShipToAttack.Gun1,
                      playerShipToAttack.Gun2, playerShipToAttack.Gun3, playerShipToAttack.Destr1, playerShipToAttack.Destr1Par, playerShipToAttack.Destr2,
                      playerShipToAttack.Destr2Par, playerShipToAttack.Destr3, playerShipToAttack.Destr4, playerShipToAttack.MiniGun);

                        Lists.setShipsForCPU(Cruis1, Cruis2, Cruis3, Cruis4, CruisG, Gun1, Gun2, Gun3, Destr1, Destr1Par, Destr2, Destr2Par, Destr3, Destr4, DestrG);

                        if (Cruis1 > 0) Lists.dummyOnDefenceSceneEnemy = 1;
                        else if (Cruis2 > 0) Lists.dummyOnDefenceSceneEnemy = 2;
                        else if (Cruis3 > 0) Lists.dummyOnDefenceSceneEnemy = 3;
                        else Lists.dummyOnDefenceSceneEnemy = 4;


                        if (SpaceCtrlr.Instance.adsPanelIsOn) SpaceCtrlr.Instance.skipRewardedProposal(); //closing the rewarded ads panel proposal, if it is open while CPU attacks player 

                        //to not set any stations on scene cause here fights a ships only
                        Lists.stationTypeLists = 0;

                        // saving the instance of player ship and CPU ship that is under attack to update their fleet counts later after the battle on SpaceCtrl class  
                        Lists.shipOnAttack = other.gameObject;
                        Lists.CPUShipOnAttack = gameObject;

                        //assignin a proper color to UI token of opposite CPU player that is under attack 
                        Lists.colorOfOpposite = SpaceCtrlr.Instance.getProperMatColorByIndex(SpaceCtrlr.Instance.colorsOfPlayers[CPUNumber]);

                        Lists.CPUCruisAttacksShip();

                        //setting proper dummy to defende on defence scene (0 is default value for cruisers battle and means nothing here)
                        SpaceCtrlr.Instance.setTheMaketNumberForDefenceScene(false, 0, playerShipToAttack);

                        attackPlayerStation();
                    }
                    else
                    {
                        if (playerShipToAttack.assessFleetPower() < assessFleetPower())
                        {
                            reducingTheFleetOFCruiserOnBattle(1 - ((float)playerShipToAttack.assessFleetPower() / assessFleetPower() * 0.75f));
                            Lists.shipsOnScene.Remove(playerShipToAttack.gameObject);
                            playerShipToAttack.makePlayerCruiserDefault();
                            float reduceAmount;
                            if (Lists.isBlackDimension) reduceAmount = Constants.Instance.enrgyGainLoseDark * 0.3f;
                            else if (Lists.isBlueDimension) reduceAmount = Constants.Instance.enrgyGainLoseBlue * 0.3f;
                            else reduceAmount = Constants.Instance.enrgyGainLoseRed * 0.3f;
                            SpaceCtrlr.Instance.changingTheEnergyOfPlayer(false, reduceAmount);
                            playerShipToAttack.disactivatingCurrentShip();
                        }
                        else
                        {
                            launchANewCruiser(); //starting a process of launching a proper scene cruiser on chosen station (from method)
                            playerShipToAttack.reducingTheFleetOFCruiserOnBattle(1 - ((float)assessFleetPower() / playerShipToAttack.assessFleetPower() * 0.75f));
                            float reduceAmount;
                            if (Lists.isBlackDimension) reduceAmount = Constants.Instance.enrgyGainLoseDark * 0.3f;
                            else if (Lists.isBlueDimension) reduceAmount = Constants.Instance.enrgyGainLoseBlue * 0.3f;
                            else reduceAmount = Constants.Instance.enrgyGainLoseRed * 0.3f;
                            SpaceCtrlr.Instance.changingTheEnergyOfPlayer(true, reduceAmount);
                            disactivatingCurrentShip(); //this disactivation with burst of CPU ship cause it was defeated
                        }
                    }
                }
                else
                {
                    launchANewCruiser(); //starting a process of launching a proper scene cruiser on chosen station (from method)
                    playerShipToAttack.reducingTheFleetOFCruiserOnBattle(0.95f);
                    float reduceAmount;
                    if (Lists.isBlackDimension) reduceAmount = Constants.Instance.enrgyGainLoseDark * 0.3f;
                    else if (Lists.isBlueDimension) reduceAmount = Constants.Instance.enrgyGainLoseBlue * 0.3f;
                    else reduceAmount = Constants.Instance.enrgyGainLoseRed * 0.3f;
                    SpaceCtrlr.Instance.changingTheEnergyOfPlayer(true, reduceAmount);
                    disactivatingCurrentShip(); //this disactivation with burst of CPU ship cause it was defeated
                }

            }
            //so if player scene cruiser will not have any cruisers in fleet it will lose energy and all fleet  
            else
            {
                //make the UI lose effect work after getting back from battle since player cruiser lost

                reducingTheFleetOFCruiserOnBattle(0.95f);

                Lists.shipsOnScene.Remove(playerShipToAttack.gameObject);
                playerShipToAttack.makePlayerCruiserDefault();
                float reduceAmount;
                if (Lists.isBlackDimension) reduceAmount = Constants.Instance.enrgyGainLoseDark * 0.3f;
                else if (Lists.isBlueDimension) reduceAmount = Constants.Instance.enrgyGainLoseBlue * 0.3f;
                else reduceAmount = Constants.Instance.enrgyGainLoseRed * 0.3f;
                SpaceCtrlr.Instance.changingTheEnergyOfPlayer(false, reduceAmount);

                playerShipToAttack.disactivatingCurrentShip();
                //clearing the fleet of player cruis since it was defeated cause CPU station won
                //Lists.ClearFleetOfPlayerCruis();
            }
        }

        else if (/*other.CompareTag("Energon") || */other.CompareTag("GCruisOut"))
        {
            EnergonMngr energonObj;
            //if (other.CompareTag("Energon"))  energonObj = other.GetComponent<EnergonMngr>();
            //else 
            energonObj = other.transform.parent.GetComponent<EnergonMngr>();
            if ((Cruis1 + Cruis2 + Cruis3 + Cruis4) > 0)
            {
                //so CPU captures the station only if it's fleet stronger than attacked station fleet
                if (assessFleetPower() >= energonObj.assessFleetPower())
                {
                    reducingTheFleetOFCruiserOnBattle(1 - ((float)energonObj.assessFleetPower() / assessFleetPower() * 0.75f));
                    Lists.energonsOnScene.Remove(other.gameObject);
                    energonObj.disactivatingCurrentShip();
                }
                else
                {
                    launchANewCruiser(); //starting a process of launching a proper scene cruiser on chosen station (from method)
                    energonObj.reducingTheFleetOFCruiserOnBattle(1 - ((float)assessFleetPower() / energonObj.assessFleetPower() * 0.75f));
                    disactivatingCurrentShip();
                }
            }
            else
            {
                launchANewCruiser(); //starting a process of launching a proper scene cruiser on chosen station (from method)
                energonObj.reducingTheFleetOFCruiserOnBattle(0.95f);
                disactivatingCurrentShip();
            }
        }
        //else if (other.CompareTag("Energon")) {
        //    if (isAfterEnergon)
        //    {
        //        isMoving = false;
        //        detectingTransformOfObjectToMove();
        //    }
        //}
        else if (other.CompareTag(Cruis1CPUTag) || other.CompareTag(Cruis2CPUTag) || other.CompareTag(Cruis3CPUTag) || other.CompareTag(Cruis4CPUTag)) {
            if (other.GetComponent<CPUShipCtrlJourney>().CPUCruiserObject != this && CPUCruiserObject == other.GetComponent<CPUShipCtrlJourney>()) {
                CPUShipCtrlJourney CPUCruiserObj = other.GetComponent<CPUShipCtrlJourney>();
                if ((Cruis1 + Cruis2 + Cruis3 + Cruis4) > 0)
                {
                    //so CPU captures the station only if it's fleet stronger than attacked station fleet
                    if (assessFleetPower() >= CPUCruiserObj.assessFleetPower())
                    {
                        reducingTheFleetOFCruiserOnBattle(1 - ((float)CPUCruiserObj.assessFleetPower() / assessFleetPower() * 0.75f));
                        CPUCruiserObj.launchANewCruiser();
                        CPUCruiserObj.disactivatingCurrentShip();
                    }
                    else
                    {
                        launchANewCruiser(); //starting a process of launching a proper scene cruiser on chosen station (from method)
                        CPUCruiserObj.reducingTheFleetOFCruiserOnBattle(1 - ((float)assessFleetPower() / CPUCruiserObj.assessFleetPower() * 0.75f));
                        disactivatingCurrentShip();
                    }
                }
                else
                {
                    launchANewCruiser(); //starting a process of launching a proper scene cruiser on chosen station (from method)
                    CPUCruiserObj.reducingTheFleetOFCruiserOnBattle(0.95f);
                    disactivatingCurrentShip();
                }
            }
        }
    }

    //this method is used to activate capture effect if cruiser is currently moving around it's station and this station was attacked
    private void activateCaprtureEffect(int CPUNumb)
    {
        captureLaser.positionCount = 2; //setting position counts to laser line
        captureLaser.SetPosition(0, transform.position);
        //if (Lists.StationThisCPU1.CompareTag(cpturedStationTagAway)) captureLaser.SetPosition(1, Lists.StationThisCPU1.transform.position);
        //else 
        captureLaser.SetPosition(1, StationThisCPU.transform.parent.position);
        captureLaser.enabled = true; //turning on the laser to capture the station

        //turnin on the capture effect from pull
        captureLaserListToActivate = ObjectPullerJourney.current.GetCaptureEffectPullList();
        captureEffect = ObjectPullerJourney.current.GetUniversalBullet(captureLaserListToActivate);
        //if (Lists.StationThisCPU1.CompareTag(cpturedStationTagAway)) captureEffect.transform.position = Lists.StationThisCPU1.transform.position;
        //else 
        captureEffect.transform.position = StationThisCPU.transform.parent.position;
        captureEffect.transform.rotation = Quaternion.identity;
        captureEffect.SetActive(true);

        //sending a sygnal to CaptureLine class to start a UI processing with filling the UI line (that show the process of cpturing the station)

        //if (Lists.StationThisCPU1.CompareTag(cpturedStationTagAway)) {
        //    Lists.StationThisCPU1.GetComponent<StationController>().IsCPU1Filling = true;
        //}
        //else 
        if (CPUNumb==0) captureLine.IsCPU1Filling = true;
        else if (CPUNumb == 1) captureLine.IsCPU2Filling = true;
        else if (CPUNumb == 2) captureLine.IsCPU3Filling = true;
        else if (CPUNumb == 3) captureLine.IsCPU4Filling = true;
    }

    //this method is for calling from CaptureLine class after end of capture process and making the current empty station CPU's
    public void disactivateCaptureEffect(int CPUNumb)
    {
        if (captureEffect) captureEffect.SetActive(false);
        captureLaser.enabled = false;

        if (CPUNumb == 0) captureLine.IsCPU1Filling = false;
        else if (CPUNumb == 1) captureLine.IsCPU2Filling = false;
        else if (CPUNumb == 2) captureLine.IsCPU3Filling = false;
        else if (CPUNumb == 3) captureLine.IsCPU4Filling = false;
    }

    //this method is for assessing the power of cruis fleet is called from differend classes to asses CPU Cruiser power and take a decision of attack
    public int assessFleetPower()
    {
        int x = Cruis4 * Constants.Instance.Cruis4Index + Cruis3 * Constants.Instance.Cruis3Index + Cruis2 * Constants.Instance.Cruis2Index + Cruis1 * Constants.Instance.Cruis1Index + CruisG * Constants.Instance.Cruis2Index
               + Destr4 * Constants.Instance.Destr4Index + Destr3 * Constants.Instance.Destr3Index + Destr2 * Constants.Instance.Destr2Index + Destr2Par * Constants.Instance.Destr2Index
               + Destr1 * Constants.Instance.Destr1Index + Destr1Par * Constants.Instance.Destr1Index + DestrG * Constants.Instance.Destr2Index + Gun1 * Constants.Instance.Gun1Index + Gun2 * Constants.Instance.Gun2Index
               + Gun3 * Constants.Instance.Gun3Index + Fighter;
        return x;
        
    }

    private void FixedUpdate()
    {
        //making CPU ship move toward chosen point
        if (isMoving)
        {
            if (!shipMovingLine.enabled) shipMovingLine.enabled = true;
            shipMovingLine.SetPosition(0, transform.position);
            //if (isAfterEnergon)
            //{
            //    transform.position = Vector3.MoveTowards(transform.position, moveToObjectTransform.position, CPUCruisCommonMoveSpeed);
            //    yRotation = Quaternion.LookRotation(moveToPoint - transform.position, Vector3.up).eulerAngles.y;
            //    transform.rotation = Quaternion.Euler(0, yRotation + 90, 0);
            //    shipMovingLine.SetPosition(1, moveToObjectTransform.position);
            //}
            //else
            //{
            transform.position = Vector3.MoveTowards(transform.position, moveToPoint, CPUCruisCommonMoveSpeed);
            //}

            if (transform.position == moveToPoint)
            {
                detectingWeakestStationToAttackFromAll();
            }
            //    detectingTransformOfObjectToMove();
            //    //if (moveToObjectTransform != null && moveToObjectTransform.gameObject.activeInHierarchy)
            //    //{
            //    //    moveToPoint = moveToObjectTransform.position; 
            //    //    yRotation = Quaternion.LookRotation(moveToPoint - transform.position, Vector3.up).eulerAngles.y;
            //    //    transform.rotation = Quaternion.Euler(0, yRotation + 90, 0);
            //    //}
            //    //else
            //    //{
            //    //    isMoving = false;
            //    //    detectingTransformOfObjectToMove();
            //    //}
            //}
        }
        else
        {
            if (captureLaser.enabled)
            {
                if (shipMovingLine.enabled) shipMovingLine.enabled = false;
                transform.RotateAround(rotateAroundStation, Vector3.up, CPUCruisCommonMoveSpeed + 0.5f);
                yRotation = Quaternion.LookRotation(rotateAroundStation - transform.position, Vector3.up).eulerAngles.y;
                transform.rotation = Quaternion.Euler(0, yRotation, 0);

                captureLaser.SetPosition(0, transform.position);
                energy -= Constants.Instance.energyReduceNoTo0;
                if (energy <= 0)
                {
                    //if (Lists.StationThisCPU1 != null)
                    //{
                    disactivateCaptureEffect(CPUNumber); //first of all the capture effect is disactivated

                    detectingWeakestStationToAttackFromAll();
                    //detectingTransformOfObjectToMove();
                    //launchANewCruiser(); //starting a process of launching a proper scene cruiser on chosen station (from method)
                    //gameObject.SetActive(false); //disactivating the CPU cruiser
                }
                if (((CPUNumber == 0 && captureLine.CPU1FillAmount >= 0) || (CPUNumber == 1 && captureLine.CPU2FillAmount >= 0)
                || (CPUNumber == 2 && captureLine.CPU3FillAmount >= 0) || (CPUNumber == 3 && captureLine.CPU4FillAmount >= 0)) && captureLine!=null)
                {
                    disactivateCaptureEffect(CPUNumber); //first of all the capture effect is disactivated
                    captureLine.turnEmptyToCPUStation(CPUNumber, this);
                    launchANewCruiser(); //starting a process of launching a proper scene cruiser on chosen station (from method)
                    disactivatingCurrentShipNoBurst();
                }
            }
        }
    }

    private void Update()
    {
        if (isSelected && infoPanelLocal!=null)
        {
            infoPanelLocal.transform.position = SpaceCtrlr.Instance.mainCam.WorldToScreenPoint(transform.position);
        }
    }

    public void disactivatingCurrentShip()
    {
        BurstList = ObjectPullerJourney.current.GetenergonBurstPullList();
        BurstReal = ObjectPullerJourney.current.GetUniversalBullet(BurstList);
        BurstReal.transform.position = transform.position;
        SelectingBox.Instance.selectableShipsCPU.Remove(this);
        BurstReal.SetActive(true);
        gameObject.SetActive(false);
        clearTheFleetAll();
        captureLine = null;
    }

    public void disactivatingCurrentShipNoBurst()
    {
        SelectingBox.Instance.selectableShipsCPU.Remove(this);
        gameObject.SetActive(false);
        clearTheFleetAll();
        captureLine = null;
    }

    public void attackPlayerStation()
    {
        //setting the time on jpurney scene to regular scale before switching the scene
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
        if (Fighter == 0)
        {
            SceneSwitchMngr.LoadBattleScene();
        }
        else
        {
            Lists.setFightersForCPU(Fighter);
            SceneSwitchMngr.LoadDefenceScene();
        }
    }

    //getting a count of current player station by index, used on update while updating the UI station icons count on scene
    private int getProperCPUStationsCount(int indexOfCPU) {
        if (indexOfCPU == 0) return Lists.CPU1Stations.Count;
        else if (indexOfCPU == 1) return Lists.CPU2Stations.Count;
        else if (indexOfCPU == 2) return Lists.CPU3Stations.Count;
        else return Lists.CPU4Stations.Count;
    }

    private int getProperCPUCruisersOnSceneCount(int indexOfCPU)
    {
        if (indexOfCPU == 0) return Lists.CPU1CruisersOnScene;
        else if (indexOfCPU == 1) return Lists.CPU2CruisersOnScene;
        else if (indexOfCPU == 2) return Lists.CPU3CruisersOnScene;
        else return Lists.CPU4CruisersOnScene;
    }
}
