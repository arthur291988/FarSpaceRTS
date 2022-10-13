using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergonMngr : MonoBehaviour
{
    public Transform movingToEmptyObj;
    private Quaternion nextMovingRotation;
    private float nextRandomeRotation;
    //this variables are set on SpaceCtrlr class while instantiating on scene the ships on start method
    public int energonAndGuardLvl;
    public float nextRotatioLerp;
    public float nextMovingSpeed;
    public float energonLife;
    public float energonLifeStart;

    //this one is used to reduce speed of energon or guard efter they hit the asteroid
    private float reduceSpeedVar = 1;

    private string guardTag = "GCruisOut";

    private float changeDicrectionTime;

    //guard chase settings
    //private bool isGuard = false; //is used to assigne CPU ship as pirate
    public bool isChasing = false; //is used to start chasing a player ship to Guard ship
    public float guardTranslateModif = 5.2f; //is used to increase the speed of guard ship if it is close to player ship (otherwise it will never overcome it)
    public float guardChaseTimeMiddle = 11f;
    private float guardChaseTime = 10;
    private float guardStartTime = 0f;

    public bool isChasingEnergyBall = false; //is used to start chasing energy ball by all guard and energon ships
    //public float invokeChaceTime = 10;
    //public GameObject playerShip; //to hold a reference to player ship to chase it 
    private Vector3 directionToLook; //direction to look into player ship while chase mode
    private float yRotation; //this var is used to set a guard ship rotation while chasing the player ship

    //public bool isParalized;
    //private float paralizedTime = 9;
    //private float paralizerTimer;

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

    public int Cruis1;
    public int Cruis2;
    public int Cruis3;
    public int Cruis4;
    public int CruisG;
    public int Destr1;
    public int Destr1Par;
    public int Destr2;
    public int Destr2Par;
    public int Destr3;
    public int Destr4;
    public int DestrG;
    public int Gun1;
    public int Gun2;
    public int Gun3;
    public int MiniGun;
    public int Fighter;

    ////this one is to pull the paralizer from the puller whent the player bullet hits the energon ship
    //private List<GameObject> ParalizerList;
    //GameObject ParalizerListReal;

    ////this one is to pull the booster from the puller whent the player bullet hits the guard ship
    //private List<GameObject> boosterList;
    //GameObject boosterReal;
    ////this one is to pull the energy from the puller whent the player bullet hits the energon ship
    //private List<GameObject> energyList;
    //GameObject energyReal;

    //private List<GameObject> burstList;
    //GameObject burstReal;

    [HideInInspector]
    public float energyOfEnergonAndGuard;
    //[HideInInspector]
    //public float boosterOfEnergonAndGuard;

    //private AudioSource paralizedSound;

    private GameObject infoPanelLocal;
    private List<GameObject> infoPanelLocalListToActivate;
    private MiniInfoPanel miniInfoPanelObject;
    private bool isSelected;

    //is used to populate with all ships of station and take only the % of fleet
    private List<string> allCruisersAndDestrs;
    private List<string> cruisersAndDestrAfterReduce;

    private Transform chasingObjectForGuard;


    private List<GameObject> BurstList;
    private GameObject BurstReal;

    // Start is called before the first frame update
    void Start()
    {
        allCruisersAndDestrs = new List<string>();
        cruisersAndDestrAfterReduce = new List<string>();
    }

    private void OnEnable()
    {
        setRotationCenter(); //starting the move of energon ship after it is spawned or respawned
        StartCoroutine(reducingTheSpeed());
        StartCoroutine(addThisToSelectable());
    }

    private void OnDisable()
    {
        disactivateInfoAboutShip();
        StopAllCoroutines();
        isChasing = false;
        chasingObjectForGuard = null;
    }
    private IEnumerator addThisToSelectable()
    {
        yield return new WaitForSeconds(0.3f);
        SelectingBox.Instance.selectableEnergonAndGuards.Add(this);
    }


    //now it is called from guardEnergyGathe class to statr chasing the player if it gets to guards sonar area
    public void startAChaseProcess(Transform chasingObject) {
        CancelInvoke("startAChaseProcess");
        guardStartTime = 0;
        guardChaseTime = Random.Range(guardChaseTimeMiddle - 2, guardChaseTimeMiddle + 3);
        chasingObjectForGuard = chasingObject;
        isChasing = true;
        //Invoke("startAChaseProcess", Random.Range (invokeChaceTime, invokeChaceTime+5));
    }

    public int assessFleetPower()
    {
        int x;
        x = Cruis4 * Constants.Instance.Cruis4Index + Cruis3 * Constants.Instance.Cruis3Index + Cruis2 * Constants.Instance.Cruis2Index + Cruis1 * Constants.Instance.Cruis1Index + CruisG * Constants.Instance.Cruis2Index
                + Destr4 * Constants.Instance.Destr4Index + Destr3 * Constants.Instance.Destr3Index + Destr2 * Constants.Instance.Destr2Index + Destr2Par * Constants.Instance.Destr2Index + DestrG * Constants.Instance.Destr2Index
                + Destr1 * Constants.Instance.Destr1Index + Destr1Par * Constants.Instance.Destr1Index + Gun1 * Constants.Instance.Gun1Index + Gun2 * Constants.Instance.Gun2Index
                + Gun3 * Constants.Instance.Gun3Index + Fighter;
        
        return x;
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

    //this method is called from any trigger with active game object to determine reducing of fleet of player ship won the battle
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

        if (infoPanelLocal)
        {
            miniInfoPanelObject.fleetCount.text = assessFleetPower().ToString();
            miniInfoPanelObject.energyCount.text = energyOfEnergonAndGuard.ToString("0");
        }
    }

    public void showInfoAboutShip() {
        infoPanelLocalListToActivate = ObjectPullerJourney.current.GetMiniInfoPanelPullList(); 
        if (infoPanelLocal != null)
        {
            if (infoPanelLocal.activeInHierarchy) infoPanelLocal.SetActive(false);
            infoPanelLocal = null;
        }
        infoPanelLocal = ObjectPullerJourney.current.GetUniversalBullet(infoPanelLocalListToActivate);
        miniInfoPanelObject = infoPanelLocal.GetComponent<MiniInfoPanel>();
        //infoPanelLocal.transform.SetParent(SpaceCtrlr.Instance.parentCanvas, false);
        infoPanelLocal.transform.position = SpaceCtrlr.Instance.mainCam.WorldToScreenPoint(transform.position);
        miniInfoPanelObject.energyCount.text = energyOfEnergonAndGuard.ToString("0");
        //miniInfoPanelObject.boosterCount.text = boosterOfEnergonAndGuard.ToString("0");
        miniInfoPanelObject.fleetCount.text = assessFleetPower().ToString();
        isSelected=true;
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

    public void setTheFleetAndResourcesOfEnergon()
    {
        if (energonAndGuardLvl == 0)
        {
            Cruis4 = Random.Range(3, 6);
            Destr4 = Random.Range(5, 12);

            energyOfEnergonAndGuard = Random.Range(100, 150);
            //boosterOfEnergonAndGuard = Random.Range(0, 15);
        }
        else if (energonAndGuardLvl == 1)
        {
            Cruis4 = Random.Range(3, 6);
            Cruis3 = Random.Range(1, 5);
            Destr4 = Random.Range(5, 15);
            Destr3 = Random.Range(2, 12);

            Gun1 = Random.Range(0, 3);
            Fighter = Random.Range(5, 8);

            energyOfEnergonAndGuard = Random.Range(150, 230);
            //boosterOfEnergonAndGuard = Random.Range(10, 20);
        }
        else if (energonAndGuardLvl == 2)
        {
            Cruis4 = Random.Range(2, 4);
            Cruis3 = Random.Range(3, 7);
            Cruis2 = Random.Range(1, 4);
            CruisG = Random.Range(0, 2);
            Destr4 = Random.Range(1, 3);
            Destr3 = Random.Range(5, 8);
            Destr2 = Random.Range(2, 7);
            Destr2Par = Random.Range(2, 7);
            DestrG = Random.Range(1, 3);

            Gun1 = 1;
            Gun2 = Random.Range(0, 2);
            Fighter = Random.Range(5, 8);

            energyOfEnergonAndGuard = Random.Range(230, 300);
            //boosterOfEnergonAndGuard = Random.Range(15, 25);
        }
        else if (energonAndGuardLvl == 3)
        {
            Cruis4 = Random.Range(0, 2);
            Cruis3 = Random.Range(1, 3);
            Cruis2 = Random.Range(1, 3);
            CruisG = Random.Range(1, 3);
            Cruis1 = Random.Range(1, 4);
            Destr4 = 1;
            Destr3 = 1;
            Destr2 = Random.Range(1, 3);
            Destr1 = Random.Range(2, 8);
            Destr2Par = Random.Range(1,4);
            Destr1Par = Random.Range(2, 8);
            DestrG = Random.Range(1, 3);

            Gun2 = 1;
            Gun3 = Random.Range(0, 2);
            Fighter = Random.Range(5, 8);

            energyOfEnergonAndGuard = Random.Range(320, 500);
            //boosterOfEnergonAndGuard = Random.Range(25, 35);
        }

        //energyCount.text = energyOfEnergon.ToString("0");
        //boosterCount.text = boosterOfEnergon.ToString("0");
        //fleetPower.text = assessFleetPower().ToString();
    }

    public void setTheFleetAndResourcesOfGuard()
    {
        if (energonAndGuardLvl == 0)
        {
            CruisG = 1;
            DestrG = Random.Range(3, 9);

            energyOfEnergonAndGuard = Random.Range(150, 250);
            //boosterOfEnergonAndGuard = Random.Range(15, 35);
        }
        else if (energonAndGuardLvl == 1)
        {
            CruisG = Random.Range(1, 4);
            DestrG = Random.Range(7, 15);
            Gun1 = Random.Range(1, 3);
            Fighter = Random.Range(5, 8);

            energyOfEnergonAndGuard = Random.Range(250, 400);
            //boosterOfEnergonAndGuard = Random.Range(35, 45);
        }
        else if (energonAndGuardLvl == 2)
        {
            CruisG = Random.Range(4, 9);
            DestrG = Random.Range(15, 25);
            Gun2 = Random.Range(1, 3);
            Fighter = Random.Range(8, 14);

            energyOfEnergonAndGuard = Random.Range(400, 600);
            //boosterOfEnergonAndGuard = Random.Range(40, 60);
        }
        else if (energonAndGuardLvl == 3)
        {
            CruisG = Random.Range(8, 15);
            DestrG = Random.Range(19, 29);
            Gun2 = 1;
            Gun3 = 1;
            Fighter = Random.Range(13, 19);
            energyOfEnergonAndGuard = Random.Range(800, 1500);
            //boosterOfEnergonAndGuard = Random.Range(80, 150);
        }

        //energyCount.text = energyOfEnergon.ToString("0");
        //boosterCount.text = boosterOfEnergon.ToString("0");
        //fleetPower.text = assessFleetPower().ToString();
    }


    //reducing the speed of ship after it hits the asteroid
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Asteroid") /*&& !CompareTag(guardTag)*/) reduceSpeedVar = 0.1f;
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Asteroid") && CompareTag(guardTag)) reduceSpeedVar = 0.1f;
    //}

    private IEnumerator reducingTheSpeed() {
        yield return new WaitForSeconds(Random.Range(7f, 10f));
        reduceSpeedVar = Random.Range(0.2f, 0.6f);
        StartCoroutine(reducingTheSpeed());
    }

    //function to appoint a new rotation to lerp and to change the properties of energon ship maneuvering
    private void setRotationCenter() {

        nextRandomeRotation = transform.rotation.eulerAngles.y + transform.rotation.eulerAngles.y > 0 ? Random.Range(-500f, -800f) : Random.Range(200f, 300f);
        nextMovingRotation = Quaternion.Euler(0, nextRandomeRotation, 0);
        changeDicrectionTime = Random.Range(Constants.Instance.energonDirChangeStartTime, Constants.Instance.energonDirChangeEndTime);
        Invoke("setRotationCenter", changeDicrectionTime);
    }

    public void disactivatingCurrentShip()
    {
        BurstList = ObjectPullerJourney.current.GetenergonBurstPullList();
        BurstReal = ObjectPullerJourney.current.GetUniversalBullet(BurstList);
        BurstReal.transform.position = transform.position;
        BurstReal.SetActive(true);
        SelectingBox.Instance.selectableEnergonAndGuards.Remove(this);
        gameObject.SetActive(false);
    }

    //this method works both for paralizer of guard 
    //public void paralizeShip()
    //{

    //    //reducing the life of ship if it has life and is not paralized
    //    if (energonLife > 0 && !isParalized) energonLife--;
    //    //paralizing the guard ship after player hits it with bullet and the life of guard is zero
    //    if (CompareTag(guardTag) )
    //    {
    //        if (energonLife == 0)
    //        {
    //            if (ParalizerListReal == null)
    //            {
    //                paralizedSound.Play();
    //                ParalizerList = ObjectPullerJourney.current.GetParalizerJourList();
    //                ParalizerListReal = ObjectPullerJourney.current.GetUniversalBullet(ParalizerList);
    //                ParalizerListReal.transform.position = transform.position;
    //                ParalizerListReal.SetActive(true);
    //                isParalized = true;
    //                energonLife = energonLifeStart;
    //            }
    //        }
    //        //if guard ship is not paralized it spawns the boosters
    //        else if (boosterReal == null && Random.Range(0, 3) > 0)
    //        {
    //            boosterList = ObjectPullerJourney.current.GetBoosterSpherePullList();
    //            boosterReal = ObjectPullerJourney.current.GetUniversalBullet(boosterList);
    //            boosterReal.transform.position = transform.position;
    //            boosterReal.SetActive(true);
    //            startAChaseProcess();
    //        }
    //    }
    //    //activating the booster on scene after player hits the guard ship with bullet and setting the level according booster features
    //    //and starting the chase process of player ship
    //    else
    //    {
    //        if (energonLife == 0)
    //        {
    //            burstList = ObjectPullerJourney.current.GetenergonBurstPullList();
    //            burstReal = ObjectPullerJourney.current.GetUniversalBullet(burstList);
    //            burstReal.transform.position = transform.position;
    //            burstReal.SetActive(true);

    //            energyList = ObjectPullerJourney.current.GetEnergySphereBigPullList();
    //            energyReal = ObjectPullerJourney.current.GetUniversalBullet(energyList);
    //            energyReal.transform.position = transform.position;
    //            energyReal.SetActive(true);
    //            SpaceCtrlr.Instance.energyBallsBigObjects.Add(energyReal.transform); //aim to chase for other enegons and guards
    //            sendASygnalAboutNewEnergyBall();
    //            //teleportation of energon ship out of the scene
    //            transform.position = new Vector3(Random.Range(100f, -100f), -8, Random.Range(100f, -100f));
    //            foreach (TrailRenderer tr in GetComponentsInChildren<TrailRenderer>()) tr.Clear();
    //            energonLife = energonLifeStart;
    //        }


    //        ////so here the second condition is probability of getting the booster from guard
    //        //if (boosterReal == null && Random.Range(0, 3) > 0)
    //        //{
    //        //    boosterList = ObjectPullerJourney.current.GetBoosterSpherePullList();
    //        //    boosterReal = ObjectPullerJourney.current.GetUniversalBullet(boosterList);
    //        //    boosterReal.transform.position = transform.position;
    //        //    boosterReal.SetActive(true);
    //        //    startAChaseProcess();
    //        //}
    //    }
    //}

    //this method send a sygnal to all energons and guards that there appeared a new big energy ball so they all start to chase it
    //private void sendASygnalAboutNewEnergyBall()
    //{
    //    for (int i = 0; i < SpaceCtrlr.Instance.aimingObjects.Count; i++) {
    //        EnergonMngr energonMngr = SpaceCtrlr.Instance.aimingObjects[i].GetComponent < EnergonMngr > ();
    //        if (energonMngr != this && !energonMngr.isParalized) {
    //            energonMngr.startChaseOfEnergyBall();
    //        }
    //    }
    //}

    //this method is called by a sygnal from another energon that spawns the new enery ball to scene
    //public void startChaseOfEnergyBall()
    //{
    //    isChasing = false;
    //    isChasingEnergyBall = true;
    //}

    //private void FixedUpdate()
    //{
    //    //so the booster bonus moves to player cruiser after it appears from guard
    //    //if (boosterReal != null)
    //    //{
    //    //    boosterReal.transform.Translate((SpaceCtrlr.Instance.CruisJourneyReal.transform.position - boosterReal.transform.position).normalized*0.03f);
    //    //}
    //}

    // Update is called once per frame
    void Update()
    {
        //reduceSpeedVar is modifier of speed that used to reduce the speed of ship after it hits the asteroid which is triggered in onTriggerEnter method
        //if (!isParalized) {
            if (isChasing)
            {
                transform.Translate(directionToLook * guardTranslateModif * reduceSpeedVar * Time.deltaTime, Space.World);//if CPU is gurad and is starte to chase a player ship
                if (!chasingObjectForGuard.gameObject.activeInHierarchy) isChasing = false;
            }
            else if (isChasingEnergyBall) transform.Translate(directionToLook * guardTranslateModif * reduceSpeedVar * Time.deltaTime, Space.World);//if CPU is gurad and is starte to chase a player ship
            else transform.Translate((movingToEmptyObj.position - transform.position) * nextMovingSpeed * reduceSpeedVar * Time.deltaTime, Space.World);
        //}

        //count down of time while the energon ship is paralized
        //if (isParalized) {
        //    paralizerTimer += Time.deltaTime;
        //    if (paralizerTimer >= paralizedTime) {
        //        isParalized = false;
        //        if (CompareTag(guardTag)) ParalizerListReal.SetActive(false);
        //        ParalizerListReal = null;
        //        paralizerTimer = 0;
        //    }
        //}

        if (isSelected && infoPanelLocal != null)
        {
            infoPanelLocal.transform.position = SpaceCtrlr.Instance.mainCam.WorldToScreenPoint(transform.position);
        }

        //starting to count doun the time till booster will be destroyed on scene
        //if (boosterReal != null)
        //{
        //    paralizerTimer += Time.deltaTime;
        //    if (Lists.isBlackDimension)
        //    {
        //        if (paralizerTimer >= 17)
        //        {
        //            boosterReal.SetActive(false);
        //            boosterReal = null;
        //            paralizerTimer = 0;
        //        }
        //    }
        //    if (Lists.isBlueDimension)
        //    {
        //        if (paralizerTimer >= 15)
        //        {
        //            boosterReal.SetActive(false);
        //            boosterReal = null;
        //            paralizerTimer = 0;
        //        }
        //    }
        //    if (Lists.isRedDimension)
        //    {
        //        if (paralizerTimer >= 12)
        //        {
        //            boosterReal.SetActive(false);
        //            boosterReal = null;
        //            paralizerTimer = 0;
        //        }
        //    }
        //}



        //gradually increasing the speed of guard or energon after they lost the speed (deliberately)
        if (reduceSpeedVar < 1) reduceSpeedVar += 0.006f;
        if (reduceSpeedVar > 1) reduceSpeedVar = 1; //set the speed modifier 1 if it passed above 1 in update method

        if (!isChasing /*&& !isParalized*/) transform.rotation = Quaternion.Lerp(transform.rotation, nextMovingRotation, nextRotatioLerp);

        //this function puts back the energon ship if it got out of bounds 
        if (transform.position.x > 210 || transform.position.x < -210 || transform.position.z < -210 || transform.position.z < -210)
        {
            foreach (TrailRenderer tr in GetComponentsInChildren<TrailRenderer>()) tr.Clear();
            transform.position = new Vector3(Random.Range(100f, -100f), -8, Random.Range(100f, -100f));
        }

        //that code regulates pirate ship movement on scene it chases player only 11 secs and it starts to increase it speed if it close to player ship
        //on scrMagnitude less than 200
        if (/*!isParalized && */isChasing /*&& SpaceCtrlr.Instance.CruisJourneyReal != null*//*&& !isChasingEnergyBall*/)
        {
            guardStartTime += Time.deltaTime;

            directionToLook = (chasingObjectForGuard.position - transform.position).normalized;
            //if (directionToLook.sqrMagnitude < 200) guardTranslateModif += 0.0005f;
            //else guardTranslateModif = 0.005f;
            yRotation = Quaternion.LookRotation(directionToLook, Vector3.up).eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, yRotation + 90, 0);
            if (guardStartTime >= guardChaseTime)
            {
                isChasing = false;
                chasingObjectForGuard = null;
            }
        }

        //if (isChasingEnergyBall && SpaceCtrlr.Instance.energyBallsBigObjects.Count>0)
        //{
        //    directionToLook = (SpaceCtrlr.Instance.energyBallsBigObjects[0].position - transform.position).normalized;
        //    yRotation = Quaternion.LookRotation(directionToLook, Vector3.up).eulerAngles.y;
        //    if (CompareTag(guardTag)) transform.rotation = Quaternion.Euler(0, yRotation + 90, 0);
        //    else transform.rotation = Quaternion.Euler(0, yRotation, 0);
        //}
    }

}
