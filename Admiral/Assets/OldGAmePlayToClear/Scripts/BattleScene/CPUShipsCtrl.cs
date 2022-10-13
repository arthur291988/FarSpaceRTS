using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUShipsCtrl : MonoBehaviour
{
    //are used to instantiate the bullet to shot
    private GameObject shotBulletReal;
    private GameObject shotBulletReal2; //for double bullet shot ships
    private GameObject shotBulletReal3; //for triple bullet shot ship only for Cruis1
    //private GameObject currentShipBullet; //TO DELETE IF PULLER WILL WORK ))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
    //private GameObject currentShipExploision;

    //following bool conditions are used to set attack priority, used on attack pointing method
    private bool Class1 = false;
    private bool Class2 = false;

    //to paint the elements of ship to decent color
    public List<GameObject> IDColorElements;

    //following properties are used with camera shake in case of mega attacks
    Vector3 camStartPosition; //used with camera shake on Update and assignet premirely on start method as default main camera position
    private float shakeDuration;
    private float shakeAmount = 0.3f;
    private float decreaseFactor = 0.2f;
    private float cruis1Duration = 35f;
    private float cruis2Duration = 3f;
    private float cruis4Duration = 10f;
    private bool isShaking = false;

    public GameObject[] shipParts; //used to create burst effect by giving force impulse for each ship part when it has lost its all HP (burst is initiated on Update)
    //private List <Rigidbody> partsRB; //this list is to accumulate the rigitbodies of ship parts on start and to reduce the load of CPU on update method
    //private List<Rigidbody> preDestroyPartsRB; //this list is to accumulate the rigitbodies of ship parts on start and to reduce the load of CPU on update method
    //private List<TrailRenderer> partsTrails; //this list is to accumulate the trailRenderers of ship parts on start and to reduce the load of CPU on update method
    //private List<TrailRenderer> preDestroyPartsTrails; //this list is to accumulate the trailRenderers of ship parts on start and to reduce the load of CPU on update method
    private bool isDestroying = false; // used to stop all ship processes when it is on process of destroying (cause it takes 2 secs to destroy ship GO)

    public ParticleSystem shotEffect; //this effect playes when current ship makes a shot to emitate energetic light of shot 
    public GameObject burningEffect; //burning effect of ship which is about to burst (has less than HP). Used on Update

    //following properties are used with instantiating accorfing mega attack Go o effect, figures correspond to cruiser class
    public GameObject megaShot4; 
    public GameObject megaDefence3; 
    public GameObject megaParal2; 
    public GameObject megaLaser1; 
    private GameObject megaLaser1Local; 
    private bool right = true; // is used to determine start rotation direction of mega laser, used in update method

    //TO DELETE IF PULLER WILL WORK ))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
    //differend bullet types to use with different types of ship
    //public GameObject CruiserBullet4;
    //public GameObject CruiserBullet3;
    //public GameObject CruiserBullet2;
    //public GameObject CruiserBullet1;
    //public GameObject DestrBullet4;
    //public GameObject DestrBullet3;
    //public GameObject DestrBullet2;
    //public GameObject DestrBullet1;

    //public GameObject DstrExploision;
    //public GameObject CruiserExploision;

    //ships paraliazing effect
    public GameObject ParalShipCruis;
    public GameObject ParalShipDstr;

    //TO DELETE IF PULLER WILL WORK ))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
    //different paral bullets depending on range
    //public GameObject paralBullet1;
    //public GameObject paralBullet2;
    //private GameObject currentParalBullet; //to assign proper paral bullet  //TO DELETE IF PULLER WILL WORK ))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
    private float PARALIZED_TIME1;
    private float PARALIZED_TIME2;

    private bool isParalizer = false; //used to determine if current ship is with paralizer features
    private bool paralizerBullet = false; //used to switch the bullet types once per shot;
    private bool isParalized = false; //current paralized state of ship
    private GameObject shipParaliser; //these are used to assign proper paraliser effect to current ship
    //private GameObject shipParaliserReal;

    //private Rigidbody shipRB;

    private string destroyer4Tag = "Destr4";
    private string destroyer3Tag = "Destr3";
    private string destroyer2Tag = "Destr2";
    private string destroyer2TagPar = "Destr2Par";
    private string destroyer1Tag = "Destr1";
    private string destroyer1TagPar = "Destr1Par";
    private string destroyerFedTag = "FederalDestr";

    private string cruiser4Tag = "Cruis4";
    private string cruiser3Tag = "Cruis3";
    private string cruiser2Tag = "Cruis2";
    private string cruiser1Tag = "Cruis1";
    private string cruiserFedTag = "FederalCruis";

    //those controle the features of exact ship
    private float ACCURE_RANGE;
    private float ATTACK_TIME_FROM;

    //are used to direct the bullet shot
    private Vector3 shotDirection;

    //this constants determine the borders of Maneuvering along x axis
    private float Maneuvering_left_border;
    private float Maneuvering_right_border;
    private const float MANEUVERING_RATE = 3f;
    private bool moveRight;
    private float manuvreSpeed;

    //is used to give a push tu bullet and turn off the fixed update once bullet pushed
    private bool fire = false;

    //HP of the ship
    private int SHIP_LIFE;

    //turning on the maneuvering state *****************************************
    private bool destr_Maneuvering = false;
    private bool hasManeuverFeature = false;

    //ships bullet force (speed of flight)
    private float SHIP_ATTACK_FORCE;

    //harm levels of bullet hits
    private int DESTR_4_HARM;
    private int CRUIS_4_HARM;
    private int DESTR_3_HARM;
    private int CRUIS_3_HARM;
    private int DESTR_2_HARM;
    private int CRUIS_2_HARM;
    private int DESTR_1_HARM;
    private int CRUIS_1_HARM;

    //shot sound
    private AudioSource shotShound;

    //to use power shield ships
    private bool withShield; 
    //TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
    //public GameObject powerShield;

    public GameObject powerShieldLocal; //is public cause it is callsed from PlayerShipCPU on megaParalizer mega attack method
    //this mat var is used to hold a reference to cruiser 3 power shield mat, to set it green for cruis4 shield and set it back to cyan while turning it off to be ready
    //for use for next call from puller
    private Material cruis4ShieldMat; 

    //current ship defence field properties
    private float SHIP_SHIELD_TIME;
    private float SHIP_SHIELD_RELOAD_TIME;

    //this lists are necessary to assign proper bullet types from ships and pull the bullets from ObjectPuller class
    private List<GameObject> bulletsListToActivate;
    private List<GameObject> paralBulletsListToActivate;

    //this lists are necessary to assign proper sheild types from ships and pull the bullets from ObjectPuller class
    private List<GameObject> sheildsListToActivate;

    //this lists are necessary to assign proper paralized effect types for ships and pull the paralized effect from ObjectPuller class
    private List<GameObject> paralEffectListToActivate;

    //this lists are necessary to assign proper burst effect types for ships and pull the burst effect from ObjectPuller class
    private List<GameObject> ShipBurstEffectListToActivate;
    //this lists are necessary to assign proper PreBurst effect types for ships and pull the burst effect from ObjectPuller class
    private List<GameObject> ShipPreBurstEffectListToActivate;

    //is public cause it is callsed from PlayerShipCPU on megaParalizer mega attack method
    public bool shieldIsActive = false; //this one is necessary to switch power shield from one ship to another when it is disactivated. Used on put and put off shield methods

    private bool doubleBullet = false; //to use with ships that have double bullet shot features like Fed Cruiser, Cruiser 1 class and 2 class
    private bool tripleBullet = false; //to use with ships that have double bullet shot features only crui 1 class

    //is invoket from cruis4mega class to reduce ship HP from mega attack
    public void setShipLife(int reduce)
    {
        SHIP_LIFE -= reduce;
    }
    void Start()
    {
        //pre setting the rigitbodies and trail renderes for use this properties while destruing the ship
        //partsRB = new List<Rigidbody>();
        //preDestroyPartsRB = new List<Rigidbody>();
        //partsTrails = new List<TrailRenderer>();
        //preDestroyPartsTrails = new List<TrailRenderer>();

        //foreach (GameObject go in shipParts)
        //{
        //    partsRB.Add(go.GetComponent<Rigidbody>());
        //    partsTrails.Add(go.GetComponent<TrailRenderer>());
        //}

        //adding the ship to common batlefield ships lists depending on its tag and assigning to it the corresponding features
        if (name.Contains(destroyer4Tag))
        {
            if (!Lists.AllCPUShipsWithoutGuns.Contains(gameObject)) Lists.AllCPUShipsWithoutGuns.Add(gameObject);
            if (!Lists.AllCPUShips.Contains(gameObject)) Lists.AllCPUShips.Add(gameObject);
            //if (!Lists.CPUDestroyers.Contains(gameObject)) Lists.CPUDestroyers.Add(gameObject); ----------------------------
            SHIP_LIFE = Constants.Instance.DESTR_4_HP;
            ACCURE_RANGE = Constants.Instance.DESTR_4_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.DESTR_4_ATTACK_TIME;
            //currentShipBullet = DestrBullet4;  TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            SHIP_ATTACK_FORCE = Constants.Instance.DESTR_4_ATTACK_FORCE;
            //currentShipExploision = DstrExploision; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            ShipBurstEffectListToActivate = ObjectPuller.current.GetDestrBurstPull();
            ShipPreBurstEffectListToActivate = ObjectPuller.current.GetDestrPreBurstPull();
            //shipParaliserReal = ParalShipDstr; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            paralEffectListToActivate = ObjectPuller.current.GetDestrParalEffectPull();


            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            bulletsListToActivate = ObjectPuller.current.GetD4BulletCPUList();

            //this are the parts that flus out of ship if it is on pre destroy conditions (the HP is lower than 4) called on Update method
            //special kit exactly for this type of ship
            //preDestroyPartsRB.Add(shipParts[2].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[8].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[9].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[10].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[11].GetComponent<Rigidbody>());

            //preDestroyPartsTrails.Add(shipParts[2].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[8].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[9].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[10].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[11].GetComponent<TrailRenderer>());
        }

        else if (name.Contains(destroyer3Tag))
        {
            if (!Lists.AllCPUShipsWithoutGuns.Contains(gameObject)) Lists.AllCPUShipsWithoutGuns.Add(gameObject);
            if (!Lists.AllCPUShips.Contains(gameObject)) Lists.AllCPUShips.Add(gameObject);
            //if (!Lists.CPUDestroyers.Contains(gameObject)) Lists.CPUDestroyers.Add(gameObject); ----------------------------
            SHIP_LIFE = Constants.Instance.DESTR_3_HP;
            ACCURE_RANGE = Constants.Instance.DESTR_3_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.DESTR_3_ATTACK_TIME;
            //currentShipBullet = DestrBullet3; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            SHIP_ATTACK_FORCE = Constants.Instance.DESTR_3_ATTACK_FORCE;
            //currentShipExploision = DstrExploision; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            ShipBurstEffectListToActivate = ObjectPuller.current.GetDestrBurstPull();
            ShipPreBurstEffectListToActivate = ObjectPuller.current.GetDestrPreBurstPull();
            //shipParaliserReal = ParalShipDstr; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            paralEffectListToActivate = ObjectPuller.current.GetDestrParalEffectPull();

            //Power shiled properties
            withShield = true;
            StartCoroutine(waitForFirstShield());
            SHIP_SHIELD_TIME = Constants.Instance.DESTR_3_SHIELD_TIME;
            SHIP_SHIELD_RELOAD_TIME = Constants.Instance.DESTR_3_SHIELD_RELOAD_TIME;
            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            sheildsListToActivate = ObjectPuller.current.GetD3ShieldCPUPull();

            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            bulletsListToActivate = ObjectPuller.current.GetD3BulletCPUList();

            //this are the parts that flus out of ship if it is on pre destroy conditions (the HP is lower than 4) called on Update method
            //special kit exactly for this type of ship
            //preDestroyPartsRB.Add(shipParts[1].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[3].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[5].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[15].GetComponent<Rigidbody>());
            //preDestroyPartsTrails.Add(shipParts[1].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[3].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[5].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[15].GetComponent<TrailRenderer>());
        }

        //PARALIZER
        else if (name.Contains(destroyer2TagPar))
        {
            if (!Lists.AllCPUShipsWithoutGuns.Contains(gameObject)) Lists.AllCPUShipsWithoutGuns.Add(gameObject);
            if (!Lists.AllCPUShips.Contains(gameObject)) Lists.AllCPUShips.Add(gameObject);
            if (!Lists.CPUShips2Class.Contains(gameObject)) Lists.CPUShips2Class.Add(gameObject);

            //if (!Lists.CPUDestroyers.Contains(gameObject)) Lists.CPUDestroyers.Add(gameObject); ----------------------------
            //if (!Lists.CPUDestroyers2.Contains(gameObject)) Lists.CPUDestroyers2.Add(gameObject);   ----------------------------
            SHIP_LIFE = Constants.Instance.DESTR_2_HP;
            ACCURE_RANGE = Constants.Instance.DESTR_2_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.DESTR_2_ATTACK_TIME;
            //currentShipBullet = DestrBullet2; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            SHIP_ATTACK_FORCE = Constants.Instance.DESTR_2_ATTACK_FORCE;
            //currentShipExploision = DstrExploision; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            ShipBurstEffectListToActivate = ObjectPuller.current.GetDestrBurstPull();
            ShipPreBurstEffectListToActivate = ObjectPuller.current.GetDestrPreBurstPull();
            //shipParaliserReal = ParalShipDstr; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            paralEffectListToActivate = ObjectPuller.current.GetDestrParalEffectPull();

            //paralizer features
            isParalizer = true;
            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            paralBulletsListToActivate = ObjectPuller.current.GetD2PBulletCPUList();
            //currentParalBullet = paralBullet2; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))

            //Power shiled properties
            withShield = true;
            StartCoroutine(waitForFirstShield());
            SHIP_SHIELD_TIME = Constants.Instance.DESTR_2_SHIELD_TIME;
            SHIP_SHIELD_RELOAD_TIME = Constants.Instance.DESTR_2_SHIELD_RELOAD_TIME;
            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            sheildsListToActivate = ObjectPuller.current.GetD2ShieldCPUPull();

            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            bulletsListToActivate = ObjectPuller.current.GetD2BulletCPUList();

            //to set priority attack of 2 levels
            Class2 = true;

            //this are the parts that flus out of ship if it is on pre destroy conditions (the HP is lower than 4) called on Update method
            //special kit exactly for this type of ship
            //preDestroyPartsRB.Add(shipParts[3].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[5].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[6].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[2].GetComponent<Rigidbody>());

            //preDestroyPartsTrails.Add(shipParts[3].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[5].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[6].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[2].GetComponent<TrailRenderer>());

        }

        else if (name.Contains(destroyerFedTag))
        {
            if (!Lists.AllCPUShipsWithoutGuns.Contains(gameObject)) Lists.AllCPUShipsWithoutGuns.Add(gameObject);
            if (!Lists.AllCPUShips.Contains(gameObject)) Lists.AllCPUShips.Add(gameObject);
            //if (!Lists.CPUShips2Class.Contains(gameObject)) Lists.CPUShips2Class.Add(gameObject);

            //if (!Lists.CPUDestroyers.Contains(gameObject)) Lists.CPUDestroyers.Add(gameObject); ----------------------------
            //if (!Lists.CPUDestroyers2.Contains(gameObject)) Lists.CPUDestroyers2.Add(gameObject); ----------------------------

            SHIP_LIFE = Constants.Instance.DESTR_2_HP;
            ACCURE_RANGE = Constants.Instance.DESTR_2_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.DESTR_2_ATTACK_TIME;
            //currentShipBullet = DestrBullet2; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            SHIP_ATTACK_FORCE = Constants.Instance.DESTR_2_ATTACK_FORCE;
            //currentShipExploision = DstrExploision; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            ShipBurstEffectListToActivate = ObjectPuller.current.GetDestrBurstPull();
            ShipPreBurstEffectListToActivate = ObjectPuller.current.GetDestrPreBurstPull();
            //shipParaliserReal = ParalShipDstr; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            paralEffectListToActivate = ObjectPuller.current.GetDestrParalEffectPull();

            //Power shiled properties
            withShield = true;
            StartCoroutine(waitForFirstShield());
            SHIP_SHIELD_TIME = Constants.Instance.DESTR_2_SHIELD_TIME;
            SHIP_SHIELD_RELOAD_TIME = Constants.Instance.DESTR_2_SHIELD_RELOAD_TIME;
            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            sheildsListToActivate = ObjectPuller.current.GetDFedShieldCPUPull();

            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            bulletsListToActivate = ObjectPuller.current.GetDFedBulletCPUList();

            //to set priority attack of 2 levels
            Class1 = true;

            //this are the parts that flus out of ship if it is on pre destroy conditions (the HP is lower than 4) called on Update method
            //special kit exactly for this type of ship
            //preDestroyPartsRB.Add(shipParts[1].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[3].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[5].GetComponent<Rigidbody>());

            //preDestroyPartsTrails.Add(shipParts[1].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[3].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[5].GetComponent<TrailRenderer>());
        }

        else if (name.Contains(destroyer2Tag))
        {
            if (!Lists.AllCPUShipsWithoutGuns.Contains(gameObject)) Lists.AllCPUShipsWithoutGuns.Add(gameObject);
            if (!Lists.AllCPUShips.Contains(gameObject)) Lists.AllCPUShips.Add(gameObject);
            if (!Lists.CPUShips2Class.Contains(gameObject)) Lists.CPUShips2Class.Add(gameObject);

            //if (!Lists.CPUDestroyers.Contains(gameObject)) Lists.CPUDestroyers.Add(gameObject); ----------------------------
            //if (!Lists.CPUDestroyers2.Contains(gameObject)) Lists.CPUDestroyers2.Add(gameObject); ----------------------------

            SHIP_LIFE = Constants.Instance.DESTR_2_HP;
            ACCURE_RANGE = Constants.Instance.DESTR_2_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.DESTR_2_ATTACK_TIME;
            //currentShipBullet = DestrBullet2; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            SHIP_ATTACK_FORCE = Constants.Instance.DESTR_2_ATTACK_FORCE;
            //currentShipExploision = DstrExploision; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            ShipBurstEffectListToActivate = ObjectPuller.current.GetDestrBurstPull();
            ShipPreBurstEffectListToActivate = ObjectPuller.current.GetDestrPreBurstPull();
            //shipParaliserReal = ParalShipDstr; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            paralEffectListToActivate = ObjectPuller.current.GetDestrParalEffectPull();

            //Power shiled properties
            withShield = true;
            StartCoroutine(waitForFirstShield());
            SHIP_SHIELD_TIME = Constants.Instance.DESTR_2_SHIELD_TIME;
            SHIP_SHIELD_RELOAD_TIME = Constants.Instance.DESTR_2_SHIELD_RELOAD_TIME;
            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            sheildsListToActivate = ObjectPuller.current.GetD2ShieldCPUPull();

            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            bulletsListToActivate = ObjectPuller.current.GetD2BulletCPUList();

            //to set priority attack of 2 levels
            Class2 = true;
            
            //this are the parts that flus out of ship if it is on pre destroy conditions (the HP is lower than 4) called on Update method
            //special kit exactly for this type of ship
            //preDestroyPartsRB.Add(shipParts[3].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[5].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[6].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[2].GetComponent<Rigidbody>());

            //preDestroyPartsTrails.Add(shipParts[3].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[5].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[6].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[2].GetComponent<TrailRenderer>());
        }
        //PARALIZER
        else if (name.Contains(destroyer1TagPar))
        {
            if (!Lists.AllCPUShipsWithoutGuns.Contains(gameObject)) Lists.AllCPUShipsWithoutGuns.Add(gameObject);
            if (!Lists.AllCPUShips.Contains(gameObject)) Lists.AllCPUShips.Add(gameObject);
            if (!Lists.CPUShips1Class.Contains(gameObject)) Lists.CPUShips1Class.Add(gameObject);

            //if (!Lists.CPUDestroyers.Contains(gameObject)) Lists.CPUDestroyers.Add(gameObject); ----------------------------
            //if (!Lists.CPUDestroyers1.Contains(gameObject)) Lists.CPUDestroyers1.Add(gameObject); ----------------------------

            SHIP_LIFE = Constants.Instance.DESTR_1_HP;
            ACCURE_RANGE = Constants.Instance.DESTR_1_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.DESTR_1_ATTACK_TIME;
            //currentShipBullet = DestrBullet1; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            SHIP_ATTACK_FORCE = Constants.Instance.DESTR_1_ATTACK_FORCE;
            //currentShipExploision = DstrExploision; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            ShipBurstEffectListToActivate = ObjectPuller.current.GetDestrBurstPull();
            ShipPreBurstEffectListToActivate = ObjectPuller.current.GetDestrPreBurstPull();
            //shipParaliserReal = ParalShipDstr; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            paralEffectListToActivate = ObjectPuller.current.GetDestrParalEffectPull();

            //paralizer features
            isParalizer = true;
            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            paralBulletsListToActivate = ObjectPuller.current.GetD1PBulletCPUList();
            //currentParalBullet = paralBullet1; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))

            doubleBullet = true; //makes curren cruiser to shot with double bullets per one shot

            //Maneuvering features
            //manuvreSpeed = Lists.DESTR_1_MANUVRE_SPEED;
            //Maneuvering_left_border = transform.position.x - MANEUVERING_RATE;
            //Maneuvering_right_border = transform.position.x + MANEUVERING_RATE;
            //destr_Maneuvering = true;
            //hasManeuverFeature = true;
            //if (Random.Range(0f, 1f) < 0.5f) moveRight = true;
            //else moveRight = false;


            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            bulletsListToActivate = ObjectPuller.current.GetD1BulletCPUList();

            //Power shiled properties
            withShield = true;
            StartCoroutine(waitForFirstShield());
            SHIP_SHIELD_TIME = Constants.Instance.DESTR_1_SHIELD_TIME;
            SHIP_SHIELD_RELOAD_TIME = Constants.Instance.DESTR_1_SHIELD_RELOAD_TIME;
            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            sheildsListToActivate = ObjectPuller.current.GetD1ShieldCPUPull();


            //to set priority attack of 3 levels
            Class1 = true;

            //this are the parts that flus out of ship if it is on pre destroy conditions (the HP is lower than 4) called on Update method
            //special kit exactly for this type of ship
            //preDestroyPartsRB.Add(shipParts[1].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[2].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[3].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[6].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[10].GetComponent<Rigidbody>());

            //preDestroyPartsTrails.Add(shipParts[1].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[2].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[3].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[6].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[10].GetComponent<TrailRenderer>());
        }

        else if (name.Contains(destroyer1Tag))
        {
            if (!Lists.AllCPUShipsWithoutGuns.Contains(gameObject)) Lists.AllCPUShipsWithoutGuns.Add(gameObject);
            if (!Lists.AllCPUShips.Contains(gameObject)) Lists.AllCPUShips.Add(gameObject);
            if (!Lists.CPUShips1Class.Contains(gameObject)) Lists.CPUShips1Class.Add(gameObject);

            //if (!Lists.CPUDestroyers.Contains(gameObject)) Lists.CPUDestroyers.Add(gameObject); ----------------------------
            //if (!Lists.CPUDestroyers1.Contains(gameObject)) Lists.CPUDestroyers1.Add(gameObject); ----------------------------

            SHIP_LIFE = Constants.Instance.DESTR_1_HP;
            ACCURE_RANGE = Constants.Instance.DESTR_1_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.DESTR_1_ATTACK_TIME;
            //currentShipBullet = DestrBullet1; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            SHIP_ATTACK_FORCE = Constants.Instance.DESTR_1_ATTACK_FORCE;
            //currentShipExploision = DstrExploision; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            ShipBurstEffectListToActivate = ObjectPuller.current.GetDestrBurstPull();
            ShipPreBurstEffectListToActivate = ObjectPuller.current.GetDestrPreBurstPull();
            //shipParaliserReal = ParalShipDstr; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            paralEffectListToActivate = ObjectPuller.current.GetDestrParalEffectPull();

            //Maneuvering features
            //manuvreSpeed = Lists.DESTR_1_MANUVRE_SPEED;
            //Maneuvering_left_border = transform.position.x - MANEUVERING_RATE; 
            //Maneuvering_right_border = transform.position.x + MANEUVERING_RATE; 
            //destr_Maneuvering = true; 
            //hasManeuverFeature = true;
            //if (Random.Range(0f, 1f) < 0.5f) moveRight = true;
            //else moveRight = false;

            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            bulletsListToActivate = ObjectPuller.current.GetD1BulletCPUList();

            doubleBullet = true; //makes curren cruiser to shot with double bullets per one shot

            //Power shiled properties
            withShield = true;
            StartCoroutine(waitForFirstShield());
            SHIP_SHIELD_TIME = Constants.Instance.DESTR_1_SHIELD_TIME;
            SHIP_SHIELD_RELOAD_TIME = Constants.Instance.DESTR_1_SHIELD_RELOAD_TIME;
            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            sheildsListToActivate = ObjectPuller.current.GetD1ShieldCPUPull();

            //to set priority attack of 3 levels
            Class1 = true;

            //this are the parts that flus out of ship if it is on pre destroy conditions (the HP is lower than 4) called on Update method
            //special kit exactly for this type of ship
            //preDestroyPartsRB.Add(shipParts[1].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[2].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[3].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[6].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[10].GetComponent<Rigidbody>());

            //preDestroyPartsTrails.Add(shipParts[1].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[2].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[3].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[6].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[10].GetComponent<TrailRenderer>());
        }

        //CRUISERS
        else if (name.Contains(cruiser4Tag))
        {
            if (!Lists.AllCPUShipsWithoutGuns.Contains(gameObject)) Lists.AllCPUShipsWithoutGuns.Add(gameObject);
            if (!Lists.AllCPUShips.Contains(gameObject)) Lists.AllCPUShips.Add(gameObject);

            //if (!Lists.CPUCruisers.Contains(gameObject)) Lists.CPUCruisers.Add(gameObject); ----------------------------
            //if (!Lists.CPUCruisers4.Contains(gameObject)) Lists.CPUCruisers4.Add(gameObject); ----------------------------

            SHIP_LIFE = Constants.Instance.CRUIS_4_HP;
            ACCURE_RANGE = Constants.Instance.CRUIS_4_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.CRUIS_4_ATTACK_TIME;
            //currentShipBullet = CruiserBullet4; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            SHIP_ATTACK_FORCE = Constants.Instance.CRUIS_4_ATTACK_FORCE;
            //currentShipExploision = CruiserExploision;TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            ShipBurstEffectListToActivate = ObjectPuller.current.GetCruisBurstPull();
            ShipPreBurstEffectListToActivate = ObjectPuller.current.GetCruisPreBurstPull();
            //shipParaliserReal = ParalShipCruis; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            paralEffectListToActivate = ObjectPuller.current.GetCruisParalEffectPull();

            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            bulletsListToActivate = ObjectPuller.current.GetC4BulletCPUList();

            //Power shiled properties
            withShield = true;
            StartCoroutine(waitForFirstShield());
            SHIP_SHIELD_TIME = Constants.Instance.CRUIS_4_SHIELD_TIME;
            SHIP_SHIELD_RELOAD_TIME = Constants.Instance.CRUIS_4_SHIELD_RELOAD_TIME;
            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            sheildsListToActivate = ObjectPuller.current.GetC3ShieldCPUPull();

            //that condition makes sure that only one cruiser on scene will make mega attack. So only the highest class cruiser will make mega attack
            //and also it checks out that only one cruiser of the same type (for example Class1) will make a mega attack and only once
            if (!Lists.CPUMegaAttackIsAssigned && Lists.Cruis1CPU == 0 && Lists.Cruis2CPU == 0 && Lists.Cruis3CPU == 0)
            {
                Lists.CPUMegaAttackIsAssigned = true; //checking true that mega attack ship is already chosen to preven a mega attack from other cruiser of the same type
                //invocing mega attack method
                Invoke("megaShot4Method", Random.Range(2, 7));
            }

            //this are the parts that flus out of ship if it is on pre destroy conditions (the HP is lower than 4) called on Update method
            //special kit exactly for this type of ship
            //preDestroyPartsRB.Add(shipParts[0].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[1].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[2].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[3].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[4].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[6].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[13].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[14].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[15].GetComponent<Rigidbody>());

            //preDestroyPartsTrails.Add(shipParts[0].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[1].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[2].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[3].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[4].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[6].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[13].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[14].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[15].GetComponent<TrailRenderer>());
        }

        else if (name.Contains(cruiser3Tag))
        {
            if (!Lists.AllCPUShipsWithoutGuns.Contains(gameObject)) Lists.AllCPUShipsWithoutGuns.Add(gameObject);
            if (!Lists.AllCPUShips.Contains(gameObject)) Lists.AllCPUShips.Add(gameObject);

            //if (!Lists.CPUCruisers.Contains(gameObject)) Lists.CPUCruisers.Add(gameObject); ----------------------------
            //if (!Lists.CPUCruisers3.Contains(gameObject)) Lists.CPUCruisers3.Add(gameObject); ----------------------------

            SHIP_LIFE = Constants.Instance.CRUIS_3_HP;
            ACCURE_RANGE = Constants.Instance.CRUIS_3_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.CRUIS_3_ATTACK_TIME;
            //currentShipBullet = CruiserBullet3; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            SHIP_ATTACK_FORCE = Constants.Instance.CRUIS_3_ATTACK_FORCE;
            //currentShipExploision = CruiserExploision;TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            ShipBurstEffectListToActivate = ObjectPuller.current.GetCruisBurstPull();
            ShipPreBurstEffectListToActivate = ObjectPuller.current.GetCruisPreBurstPull();
            //shipParaliserReal = ParalShipCruis; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            paralEffectListToActivate = ObjectPuller.current.GetCruisParalEffectPull();

            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            bulletsListToActivate = ObjectPuller.current.GetC3BulletCPUList();

            //Power shiled properties
            withShield = true;
            StartCoroutine(waitForFirstShield());
            SHIP_SHIELD_TIME = Constants.Instance.CRUIS_3_SHIELD_TIME;
            SHIP_SHIELD_RELOAD_TIME = Constants.Instance.CRUIS_3_SHIELD_RELOAD_TIME;
            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            sheildsListToActivate = ObjectPuller.current.GetC3ShieldCPUPull();

            //that condition makes sure that only one cruiser on scene will make mega attack. So only the highest class cruiser will make mega attack
            //and also it checks out that only one cruiser of the same type (for example Class1) will make a mega attack and only once
            if (!Lists.CPUMegaAttackIsAssigned && Lists.Cruis1CPU == 0 && Lists.Cruis2CPU == 0)
            {
                Lists.CPUMegaAttackIsAssigned = true; //checking true that mega attack ship is already chosen to preven a mega attack from other cruiser of the same type
                //invocing mega attack method 
                Invoke("megaDefence3Method", Random.Range(2, 7));
            }

            //this are the parts that flus out of ship if it is on pre destroy conditions (the HP is lower than 4) called on Update method
            //special kit exactly for this type of ship
            //preDestroyPartsRB.Add(shipParts[2].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[4].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[5].GetComponent<Rigidbody>());

            //preDestroyPartsTrails.Add(shipParts[2].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[4].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[5].GetComponent<TrailRenderer>());
        }
        else if (name.Contains(cruiser2Tag))
        {
            if (!Lists.AllCPUShipsWithoutGuns.Contains(gameObject)) Lists.AllCPUShipsWithoutGuns.Add(gameObject);
            if (!Lists.AllCPUShips.Contains(gameObject)) Lists.AllCPUShips.Add(gameObject);
            if (!Lists.CPUShips2Class.Contains(gameObject)) Lists.CPUShips2Class.Add(gameObject);

            //if (!Lists.CPUCruisers.Contains(gameObject)) Lists.CPUCruisers.Add(gameObject); ----------------------------
            //if (!Lists.CPUCruisers2.Contains(gameObject)) Lists.CPUCruisers2.Add(gameObject); ----------------------------

            SHIP_LIFE = Constants.Instance.CRUIS_2_HP;
            ACCURE_RANGE = Constants.Instance.CRUIS_2_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.CRUIS_2_ATTACK_TIME;
            //currentShipBullet = CruiserBullet2; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            SHIP_ATTACK_FORCE = Constants.Instance.CRUIS_2_ATTACK_FORCE;
            //currentShipExploision = CruiserExploision;TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            ShipBurstEffectListToActivate = ObjectPuller.current.GetCruisBurstPull();
            ShipPreBurstEffectListToActivate = ObjectPuller.current.GetCruisPreBurstPull();
            //shipParaliserReal = ParalShipCruis; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            paralEffectListToActivate = ObjectPuller.current.GetCruisParalEffectPull();

            //Maneuvering features
            manuvreSpeed = Constants.Instance.DESTR_2_MANUVRE_SPEED;
            Maneuvering_left_border = transform.position.x - MANEUVERING_RATE;
            Maneuvering_right_border = transform.position.x + MANEUVERING_RATE;
            destr_Maneuvering = true;
            hasManeuverFeature = true;
            if (Random.Range(0f, 1f) < 0.5f) moveRight = true;
            else moveRight = false;

            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            bulletsListToActivate = ObjectPuller.current.GetC2BulletCPUList();

            doubleBullet = true; //makes curren cruiser to shot with double bullets per one shot

            //Power shiled properties
            withShield = true;
            StartCoroutine(waitForFirstShield());
            SHIP_SHIELD_TIME = Constants.Instance.CRUIS_2_SHIELD_TIME;
            SHIP_SHIELD_RELOAD_TIME = Constants.Instance.CRUIS_2_SHIELD_RELOAD_TIME;
            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            sheildsListToActivate = ObjectPuller.current.GetC2ShieldCPUPull();

            if (!Lists.CPUMegaAttackIsAssigned && Lists.Cruis1CPU == 0)
            {
                Lists.CPUMegaAttackIsAssigned = true; //checking true that mega attack ship is already chosen to preven a mega attack from other cruiser of the same type
                //invocing mega attack method 
                Invoke("megaParal2Method", Random.Range(2, 10));
            }

            //to set priority attack of 2 levels
            Class2 = true;

            //this are the parts that flus out of ship if it is on pre destroy conditions (the HP is lower than 4) called on Update method
            //special kit exactly for this type of ship
            //preDestroyPartsRB.Add(shipParts[3].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[5].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[7].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[9].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[11].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[12].GetComponent<Rigidbody>());

            //preDestroyPartsTrails.Add(shipParts[3].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[5].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[7].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[9].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[11].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[12].GetComponent<TrailRenderer>());
        }
        else if (name.Contains(cruiser1Tag))
        {
            if (!Lists.AllCPUShipsWithoutGuns.Contains(gameObject)) Lists.AllCPUShipsWithoutGuns.Add(gameObject);
            if (!Lists.AllCPUShips.Contains(gameObject)) Lists.AllCPUShips.Add(gameObject);
            if (!Lists.CPUShips1Class.Contains(gameObject)) Lists.CPUShips1Class.Add(gameObject);


            //if (!Lists.CPUCruisers.Contains(gameObject)) Lists.CPUCruisers.Add(gameObject); ----------------------------
            //if (!Lists.CPUCruisers1.Contains(gameObject)) Lists.CPUCruisers1.Add(gameObject); ----------------------------

            SHIP_LIFE = Constants.Instance.CRUIS_1_HP;
            ACCURE_RANGE = Constants.Instance.CRUIS_1_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.CRUIS_1_ATTACK_TIME;
            //currentShipBullet = CruiserBullet1; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            SHIP_ATTACK_FORCE = Constants.Instance.CRUIS_1_ATTACK_FORCE;
            //currentShipExploision = CruiserExploision;TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            ShipBurstEffectListToActivate = ObjectPuller.current.GetCruisBurstPull();
            ShipPreBurstEffectListToActivate = ObjectPuller.current.GetCruisPreBurstPull();
            //shipParaliserReal = ParalShipCruis; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            paralEffectListToActivate = ObjectPuller.current.GetCruisParalEffectPull();

            //Maneuvering features
            manuvreSpeed = Constants.Instance.DESTR_1_MANUVRE_SPEED;
            Maneuvering_left_border = transform.position.x - MANEUVERING_RATE;
            Maneuvering_right_border = transform.position.x + MANEUVERING_RATE;
            destr_Maneuvering = true;
            hasManeuverFeature = true;
            if (Random.Range(0f, 1f) < 0.5f) moveRight = true;
            else moveRight = false;

            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            bulletsListToActivate = ObjectPuller.current.GetC1BulletCPUList();

            //Power shiled properties
            withShield = true;
            StartCoroutine(waitForFirstShield());
            SHIP_SHIELD_TIME = Constants.Instance.CRUIS_1_SHIELD_TIME;
            SHIP_SHIELD_RELOAD_TIME = Constants.Instance.CRUIS_1_SHIELD_RELOAD_TIME;
            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            sheildsListToActivate = ObjectPuller.current.GetC1ShieldCPUPull();

            tripleBullet = true; //makes curren cruiser to shot with double bullets per one shot

            if (!Lists.CPUMegaAttackIsAssigned)
            {
                Lists.CPUMegaAttackIsAssigned = true; //checking true that mega attack ship is already chosen to preven a mega attack from other cruiser of the same type
                //invocing mega attack method 
                Invoke("megaLaser1Method", Random.Range(5, 10));
            }

            //to set priority attack of 3 levels
            Class1 = true;

            //this are the parts that flus out of ship if it is on pre destroy conditions (the HP is lower than 4) called on Update method
            //special kit exactly for this type of ship
            //preDestroyPartsRB.Add(shipParts[0].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[2].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[6].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[7].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[8].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[9].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[10].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[12].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[13].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[28].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[29].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[30].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[31].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[32].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[33].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[34].GetComponent<Rigidbody>());

            //preDestroyPartsTrails.Add(shipParts[0].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[2].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[6].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[7].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[8].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[9].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[10].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[12].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[13].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[28].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[29].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[30].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[31].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[32].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[33].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[34].GetComponent<TrailRenderer>());
        }

        else if (name.Contains(cruiserFedTag))
        {
            if (!Lists.AllCPUShipsWithoutGuns.Contains(gameObject)) Lists.AllCPUShipsWithoutGuns.Add(gameObject);
            if (!Lists.AllCPUShips.Contains(gameObject)) Lists.AllCPUShips.Add(gameObject);
            if (!Lists.CPUShips1Class.Contains(gameObject)) Lists.CPUShips1Class.Add(gameObject);

            //if (!Lists.CPUCruisers.Contains(gameObject)) Lists.CPUCruisers.Add(gameObject); ----------------------------
            //if (!Lists.CPUCruisers2.Contains(gameObject)) Lists.CPUCruisers2.Add(gameObject); ----------------------------

            SHIP_LIFE = Constants.Instance.CRUIS_2_HP;
            ACCURE_RANGE = Constants.Instance.CRUIS_2_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.CRUIS_2_ATTACK_TIME;
            //currentShipBullet = CruiserBullet2; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            SHIP_ATTACK_FORCE = Constants.Instance.CRUIS_2_ATTACK_FORCE;
            //currentShipExploision = CruiserExploision;TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            ShipBurstEffectListToActivate = ObjectPuller.current.GetCruisBurstPull();
            ShipPreBurstEffectListToActivate = ObjectPuller.current.GetCruisPreBurstPull();
            //shipParaliserReal = ParalShipCruis; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            paralEffectListToActivate = ObjectPuller.current.GetCruisParalEffectPull();

            //I have decided to make federal/guard cruisers nwithout maneuvering features
            ////Maneuvering features
            //manuvreSpeed = Lists.DESTR_2_MANUVRE_SPEED;
            //Maneuvering_left_border = transform.position.x - MANEUVERING_RATE;
            //Maneuvering_right_border = transform.position.x + MANEUVERING_RATE;
            //destr_Maneuvering = true;
            //hasManeuverFeature = true;
            //if (Random.Range(0f, 1f) < 0.5f) moveRight = true;
            //else moveRight = false;

            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            bulletsListToActivate = ObjectPuller.current.GetCFedBulletCPUList();

            doubleBullet = true; //makes curren cruiser to shot with double bullets per one shot

            //Power shiled properties
            withShield = true;
            StartCoroutine(waitForFirstShield());
            SHIP_SHIELD_TIME = Constants.Instance.CRUIS_2_SHIELD_TIME;
            SHIP_SHIELD_RELOAD_TIME = Constants.Instance.CRUIS_2_SHIELD_RELOAD_TIME;
            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            sheildsListToActivate = ObjectPuller.current.GetCFedShieldCPUPull();

            //invoking mega attack method for federal cruiser without checking if mega attack is already assigned to othe higher status cruiser
            //it checks only the fact of assigning the mega attack to other guard cruiser
            
            if (!Lists.CPUMegaAttackIsAssigned)
            {
                Lists.CPUMegaAttackIsAssigned = true; //checking true that mega attack ship is already chosen to preven a mega attack from other cruiser of the same type
                //invocing mega attack method 
                Invoke("megaShot4Method", Random.Range(2, 10));
            }

            //to set priority attack of 2 levels
            Class1 = true;

            //this are the parts that flus out of ship if it is on pre destroy conditions (the HP is lower than 4) called on Update method
            //special kit exactly for this type of ship
            //preDestroyPartsRB.Add(shipParts[1].GetComponent<Rigidbody>());
            //preDestroyPartsRB.Add(shipParts[3].GetComponent<Rigidbody>());

            //preDestroyPartsTrails.Add(shipParts[1].GetComponent<TrailRenderer>());
            //preDestroyPartsTrails.Add(shipParts[3].GetComponent<TrailRenderer>());
        }

        camStartPosition = Camera.main.transform.position;

        PARALIZED_TIME1 = Constants.Instance.PARALIZE_TIME1;
        PARALIZED_TIME2 = Constants.Instance.PARALIZE_TIME2;

        shotShound = GetComponent<AudioSource>();
        //shipRB = gameObject.GetComponent<Rigidbody>();

        DESTR_4_HARM = Constants.Instance.DESTR_4_BULLET_HARM;
        CRUIS_4_HARM = Constants.Instance.CRUIS_4_BULLET_HARM;
        DESTR_3_HARM = Constants.Instance.DESTR_3_BULLET_HARM;
        CRUIS_3_HARM = Constants.Instance.CRUIS_3_BULLET_HARM;
        DESTR_2_HARM = Constants.Instance.DESTR_2_BULLET_HARM;
        CRUIS_2_HARM = Constants.Instance.CRUIS_2_BULLET_HARM;
        DESTR_1_HARM = Constants.Instance.DESTR_1_BULLET_HARM;
        CRUIS_1_HARM = Constants.Instance.CRUIS_1_BULLET_HARM;

        for (int i = 0; i < IDColorElements.Count; i++)
        {
            IDColorElements[i].GetComponent<MeshRenderer>().material.SetColor("_Color", Lists.colorOfOpposite);
        }

        StartCoroutine(waitForFirstAttack());
    }




    //following four methods controls CPU ships mega attaks and are invoked from start method of each type of cruisers 

    private void megaShot4Method() {
        isShaking = true;
        shakeDuration = cruis4Duration;
        megaShot4.GetComponent<ParticleSystem>().Play();
        megaShot4.GetComponent<AudioSource>().Play();
    }

    private void megaDefence3Method()
    {
        isShaking = true;
        shakeDuration = cruis2Duration;
        GameObject defenceField = Instantiate(megaDefence3, new Vector3(0, -1.3f, 15), Quaternion.Euler(90, 0, 0));
        Destroy(defenceField, 7);
    }

    private void megaParal2Method()
    {
        isShaking = true;
        shakeDuration = cruis2Duration;
        GameObject paral = Instantiate(megaParal2, transform.position, Quaternion.Euler(0, 180, 0));
        Destroy(paral, 1);
        foreach (GameObject go in Lists.AllPlayerShipsWithoutGuns)
        {
            if (!go.GetComponent<PlayerShipCPU>().shieldIsActive /*&& !go.GetComponent<PlayerShipCPU>().powerShieldLocal*/) go.GetComponent<PlayerShipCPU>().callForMegaParalFromOutside(); //if attacked ship is not 
        }
        foreach (GameObject gog in Lists.AllPlayerGuns)
        {
            gog.GetComponent<PlayerGunCtrlr>().callForMegaParalFromOutside();
        }
    }
    
    public void megaLaser1Method()
    {
        isShaking = true;
        shakeDuration = cruis1Duration;
        megaLaser1Local = Instantiate(megaLaser1, transform.position, Quaternion.Euler(0, 180, 0));
    }


    //initiate the first attack of CPU ship (2 secs are necessary for waiting to other ships are added to collections)
    IEnumerator waitForFirstAttack()
    {
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        attackPointing();
    }

    //POWER SHIELD WORK FEATURES
    IEnumerator waitForFirstShield()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 3f));
        putShieldOnMethod();
    }

    private void putShieldOnMethod()
    {
        if (!isParalized && !isDestroying)
        {
            if (!shieldIsActive)
            {
                powerShieldLocal = ObjectPuller.current.GetUniversalBullet(sheildsListToActivate);
                powerShieldLocal.transform.position = transform.position;
                powerShieldLocal.transform.rotation = Quaternion.identity;
                //assigning a green color for cruiser 4 power shield since it uses 3 class cruiser shield which is cyan. This is used only for 4 class cruiser 
                //if (name.Contains(cruiser4Tag))
                //{
                //    cruis4ShieldMat = powerShieldLocal.GetComponent<MeshRenderer>().material;
                //    cruis4ShieldMat.SetColor("_Color", new Color(0.3f, 1.4f, 0, 1));
                //}
                if (name.Contains(cruiser4Tag))
                {
                    cruis4ShieldMat = powerShieldLocal.GetComponent<MeshRenderer>().material;
                    cruis4ShieldMat.SetColor("_Color", new Color(0.3f, 1.4f, 0, 1));
                }
                if (name.Contains(cruiser3Tag))
                {
                    cruis4ShieldMat = powerShieldLocal.GetComponent<MeshRenderer>().material;
                    cruis4ShieldMat.SetColor("_Color", new Color(0, 1.4f, 1.4f, 1));
                }
                powerShieldLocal.SetActive(true);
                shieldIsActive = true;
            }

            //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
            //if (!powerShieldLocal) powerShieldLocal = Instantiate(powerShield, transform.position, Quaternion.identity);
            //Destroy(powerShieldLocal, SHIP_SHIELD_TIME); 


            StopCoroutine(putOffTheShield(SHIP_SHIELD_TIME));
            StartCoroutine(putOffTheShield(SHIP_SHIELD_TIME));
            //if (!IsInvoking("putOffTheShield")) Invoke("putOffTheShield", SHIP_SHIELD_TIME);
            if (!IsInvoking("putShieldOnMethod")) Invoke("putShieldOnMethod", SHIP_SHIELD_RELOAD_TIME + SHIP_SHIELD_TIME);
        }
    }

    private IEnumerator putOffTheShield(float shieldTime)
    {
        yield return new WaitForSeconds(shieldTime);
        //setting back the material color to cyan to make it usable foor next call from the puller
        //if (name.Contains(cruiser4Tag)) cruis4ShieldMat.SetColor("_Color", new Color(0, 1.4f, 1.4f, 1));
        powerShieldLocal.SetActive(false);
        shieldIsActive = false;
    }

    //private void putOffTheShield()
    //{
    //    //setting back the material color to cyan to make it usable foor next call from the puller
    //    //if (name.Contains(cruiser4Tag)) cruis4ShieldMat.SetColor("_Color", new Color(0, 1.4f, 1.4f, 1));
    //    powerShieldLocal.SetActive(false);
    //    shieldIsActive = false;
    //}

    //IEnumerator putShieldOff()
    //{
    //    yield return new WaitForSeconds(SHIP_SHIELD_TIME);
    //    shieldIsOn = false;
    //    Destroy(powerShieldLocal);
    //    StartCoroutine(putShieldOn());
    //}

    //IEnumerator putShieldOn()
    //{
    //    yield return new WaitForSeconds(SHIP_SHIELD_RELOAD_TIME);
    //    //if the ship is not paralized that code puts power shield on
    //    if (!isParalized)
    //    {
    //        shieldIsOn = true;
    //        powerShieldLocal = Instantiate(powerShield, transform.position, Quaternion.identity);
    //    }
    //    StartCoroutine(putShieldOff());
    //}

    //CPU attack management function
    private void attackPointing()
    {
        //this is attack prioryty set for 1 class cruisers and destroyers, so thay have 3 level priority system, first they destroy 1 class ships next 2 class ships and next other calsses
        if (Class1)
        {
            //it works only if there is at least one opposite side ship on battlefield
            if (Lists.PlayerShips1Class.Count > 0 && !isParalized && !isDestroying) //all conditions to stop attack processes with explanatory names
            {
                //---------- to make CPU to be able to recognize which ships are on opposite site
                int i = Random.Range(0, Lists.PlayerShips1Class.Count); //this randome chouses any ship from list of white ships
                float accuracy = Random.Range(ACCURE_RANGE, -ACCURE_RANGE); //this randome is for 3 class destroyers only, it determines the accuracy of the shot (higher - is less accurate)

                //so shot direction is subtraction of ships point vector from enemys point vector (with accuracy correction)
                shotDirection = Lists.PlayerShips1Class[i].transform.position + new Vector3(accuracy, 0, 0) - transform.position;

                // PARALIZER BULLETS EFFECTS
                //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                //if (!isParalizer) shotBulletReal = Instantiate(currentShipBullet, transform.position, Quaternion.Euler(0, 0, 0));
                if (!isParalizer)
                {
                    //if the ship has such feature it will shot with double bullets so additional bullet is created and impulse added to it later in fixed update
                    if (doubleBullet) {
                        shotBulletReal2 = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                        shotBulletReal2.transform.position = transform.position*1.1f;
                        shotBulletReal2.transform.rotation = Quaternion.Euler(0, 0, 0);
                        shotBulletReal2.SetActive(true);
                    }
                    //if the ship has such feature it will shot with triple bullets so additional bullet is created and impulse added to it later in fixed update
                    if (tripleBullet)
                    {
                        shotBulletReal2 = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                        shotBulletReal2.transform.position = transform.position * 1.1f;
                        shotBulletReal2.transform.rotation = Quaternion.Euler(0, 0, 0);
                        shotBulletReal2.SetActive(true);

                        shotBulletReal3 = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                        shotBulletReal3.transform.position = transform.position * 1.15f;
                        shotBulletReal3.transform.rotation = Quaternion.Euler(0, 0, 0);
                        shotBulletReal3.SetActive(true);
                    }
                    shotBulletReal = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                    shotBulletReal.transform.position = transform.position;
                    shotBulletReal.transform.rotation = Quaternion.Euler(0, 0, 0);
                    shotBulletReal.SetActive(true);
                }

                else
                {
                    if (paralizerBullet)
                    {
                        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                        //shotBulletReal = Instantiate(currentShipBullet, transform.position, Quaternion.Euler(0, 0, 0));
                        shotBulletReal = ObjectPuller.current.GetUniversalBullet(paralBulletsListToActivate);
                        shotBulletReal.transform.position = transform.position;
                        shotBulletReal.transform.rotation = Quaternion.Euler(0, 0, 0);
                        shotBulletReal.SetActive(true);
                        paralizerBullet = false;
                    }
                    else
                    {
                        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                        //shotBulletReal = Instantiate(currentParalBullet, transform.position, Quaternion.Euler(0, 0, 0));
                        shotBulletReal = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                        shotBulletReal.transform.position = transform.position;
                        shotBulletReal.transform.rotation = Quaternion.Euler(0, 0, 0);
                        shotBulletReal.SetActive(true);
                        paralizerBullet = true;

                        //if the ship has such feature it will shot with double bullets so additional bullet is created and impulse added to it later in fixed update
                        if (doubleBullet)
                        {
                            shotBulletReal2 = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                            shotBulletReal2.transform.position = transform.position * 1.1f;
                            shotBulletReal2.transform.rotation = Quaternion.Euler(0, 0, 0);
                            shotBulletReal2.SetActive(true);
                        }
                    }
                }

                fire = true;
                shotShound.Play();
                shotEffect.Play(); //creates energy shot effect for current ship
                                   //this one is invoking this method again after randome time which is set for Dstr3 class, so it is recursion of this method
                if (!IsInvoking("attackPointing") && !isDestroying) Invoke("attackPointing", Random.Range(ATTACK_TIME_FROM, ATTACK_TIME_FROM + 2));
            }
            else if (Lists.PlayerShips2Class.Count > 0 && !isParalized && !isDestroying) //all conditions to stop attack processes with explanatory names
            {
                //---------- to make CPU to be able to recognize which ships are on opposite site
                int i = Random.Range(0, Lists.PlayerShips2Class.Count); //this randome chouses any ship from list of white ships
                float accuracy = Random.Range(ACCURE_RANGE, -ACCURE_RANGE); //this randome is for 3 class destroyers only, it determines the accuracy of the shot (higher - is less accurate)

                //so shot direction is subtraction of ships point vector from enemys point vector (with accuracy correction)
                shotDirection = Lists.PlayerShips2Class[i].transform.position + new Vector3(accuracy, 0, 0) - transform.position;

                // PARALIZER BULLETS EFFECTS
                //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                //if (!isParalizer) shotBulletReal = Instantiate(currentShipBullet, transform.position, Quaternion.Euler(0, 0, 0));
                if (!isParalizer)
                {
                    //if the ship has such feature it will shot with double bullets so additional bullet is created and impulse added to it later in fixed update
                    if (doubleBullet)
                    {
                        shotBulletReal2 = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                        shotBulletReal2.transform.position = transform.position * 1.1f;
                        shotBulletReal2.transform.rotation = Quaternion.Euler(0, 0, 0);
                        shotBulletReal2.SetActive(true);
                    }
                    if (tripleBullet)
                    {
                        shotBulletReal2 = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                        shotBulletReal2.transform.position = transform.position * 1.1f;
                        shotBulletReal2.transform.rotation = Quaternion.Euler(0, 0, 0);
                        shotBulletReal2.SetActive(true);

                        shotBulletReal3 = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                        shotBulletReal3.transform.position = transform.position * 1.15f;
                        shotBulletReal3.transform.rotation = Quaternion.Euler(0, 0, 0);
                        shotBulletReal3.SetActive(true);
                    }
                    shotBulletReal = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                    shotBulletReal.transform.position = transform.position;
                    shotBulletReal.transform.rotation = Quaternion.Euler(0, 0, 0);
                    shotBulletReal.SetActive(true);
                }

                else
                {
                    if (paralizerBullet)
                    {
                        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                        //shotBulletReal = Instantiate(currentShipBullet, transform.position, Quaternion.Euler(0, 0, 0));
                        shotBulletReal = ObjectPuller.current.GetUniversalBullet(paralBulletsListToActivate);
                        shotBulletReal.transform.position = transform.position;
                        shotBulletReal.transform.rotation = Quaternion.Euler(0, 0, 0);
                        shotBulletReal.SetActive(true);
                        paralizerBullet = false;
                    }
                    else
                    {
                        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                        //shotBulletReal = Instantiate(currentParalBullet, transform.position, Quaternion.Euler(0, 0, 0));
                        shotBulletReal = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                        shotBulletReal.transform.position = transform.position;
                        shotBulletReal.transform.rotation = Quaternion.Euler(0, 0, 0);
                        shotBulletReal.SetActive(true);
                        paralizerBullet = true;

                        //if the ship has such feature it will shot with double bullets so additional bullet is created and impulse added to it later in fixed update
                        if (doubleBullet)
                        {
                            shotBulletReal2 = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                            shotBulletReal2.transform.position = transform.position * 1.1f;
                            shotBulletReal2.transform.rotation = Quaternion.Euler(0, 0, 0);
                            shotBulletReal2.SetActive(true);
                        }
                    }
                }

                fire = true;
                shotShound.Play();
                shotEffect.Play(); //creates energy shot effect for current ship
                                   //this one is invoking this method again after randome time which is set for Dstr3 class, so it is recursion of this method
                if (!IsInvoking("attackPointing") && !isDestroying) Invoke("attackPointing", Random.Range(ATTACK_TIME_FROM, ATTACK_TIME_FROM + 2));
            }

            else if (Lists.AllPlayerShips.Count > 0 && !isParalized && !isDestroying) //all conditions to stop attack processes with explanatory names
            {
                //---------- to make CPU to be able to recognize which ships are on opposite site
                int i = Random.Range(0, Lists.AllPlayerShips.Count); //this randome chouses any ship from list of white ships
                float accuracy = Random.Range(ACCURE_RANGE, -ACCURE_RANGE); //this randome is for 3 class destroyers only, it determines the accuracy of the shot (higher - is less accurate)

                //so shot direction is subtraction of ships point vector from enemys point vector (with accuracy correction)
                shotDirection = Lists.AllPlayerShips[i].transform.position + new Vector3(accuracy, 0, 0) - transform.position;

                // PARALIZER BULLETS EFFECTS
                //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                //if (!isParalizer) shotBulletReal = Instantiate(currentShipBullet, transform.position, Quaternion.Euler(0, 0, 0));
                if (!isParalizer)
                {
                    //OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
                    if (doubleBullet)
                    {
                        shotBulletReal2 = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                        shotBulletReal2.transform.position = transform.position * 1.1f;
                        shotBulletReal2.transform.rotation = Quaternion.Euler(0, 0, 0);
                        shotBulletReal2.SetActive(true);
                    }
                    if (tripleBullet)
                    {
                        shotBulletReal2 = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                        shotBulletReal2.transform.position = transform.position * 1.1f;
                        shotBulletReal2.transform.rotation = Quaternion.Euler(0, 0, 0);
                        shotBulletReal2.SetActive(true);

                        shotBulletReal3 = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                        shotBulletReal3.transform.position = transform.position * 1.15f;
                        shotBulletReal3.transform.rotation = Quaternion.Euler(0, 0, 0);
                        shotBulletReal3.SetActive(true);
                    }
                    shotBulletReal = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                    shotBulletReal.transform.position = transform.position;
                    shotBulletReal.transform.rotation = Quaternion.Euler(0, 0, 0);
                    shotBulletReal.SetActive(true);
                }

                else
                {
                    if (paralizerBullet)
                    {
                        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                        //shotBulletReal = Instantiate(currentShipBullet, transform.position, Quaternion.Euler(0, 0, 0));
                        shotBulletReal = ObjectPuller.current.GetUniversalBullet(paralBulletsListToActivate);
                        shotBulletReal.transform.position = transform.position;
                        shotBulletReal.transform.rotation = Quaternion.Euler(0, 0, 0);
                        shotBulletReal.SetActive(true);
                        paralizerBullet = false;
                    }
                    else
                    {
                        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                        //shotBulletReal = Instantiate(currentParalBullet, transform.position, Quaternion.Euler(0, 0, 0));
                        shotBulletReal = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                        shotBulletReal.transform.position = transform.position;
                        shotBulletReal.transform.rotation = Quaternion.Euler(0, 0, 0);
                        shotBulletReal.SetActive(true);
                        paralizerBullet = true;

                        //if the ship has such feature it will shot with double bullets so additional bullet is created and impulse added to it later in fixed update
                        if (doubleBullet)
                        {
                            shotBulletReal2 = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                            shotBulletReal2.transform.position = transform.position * 1.1f;
                            shotBulletReal2.transform.rotation = Quaternion.Euler(0, 0, 0);
                            shotBulletReal2.SetActive(true);
                        }
                    }
                }

                fire = true;
                shotShound.Play();
                shotEffect.Play(); //creates energy shot effect for current ship
                                   //this one is invoking this method again after randome time which is set for Dstr3 class, so it is recursion of this method
                if (!IsInvoking("attackPointing") && !isDestroying) Invoke("attackPointing", Random.Range(ATTACK_TIME_FROM, ATTACK_TIME_FROM + 2));
            }

        }

        //this is attack prioryty set for 2 class cruisers and destroyers, so thay have 2 level priority system, first they destroy 1 class ships next other calsses
        else if (Class2)
        {
            if (Lists.PlayerShips1Class.Count > 0 && !isParalized && !isDestroying) //all conditions to stop attack processes with explanatory names
            {
                //---------- to make CPU to be able to recognize which ships are on opposite site
                int i = Random.Range(0, Lists.PlayerShips1Class.Count); //this randome chouses any ship from list of white ships
                float accuracy = Random.Range(ACCURE_RANGE, -ACCURE_RANGE); //this randome is for 3 class destroyers only, it determines the accuracy of the shot (higher - is less accurate)

                //so shot direction is subtraction of ships point vector from enemys point vector (with accuracy correction)
                shotDirection = Lists.PlayerShips1Class[i].transform.position + new Vector3(accuracy, 0, 0) - transform.position;

                // PARALIZER BULLETS EFFECTS
                //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                //if (!isParalizer) shotBulletReal = Instantiate(currentShipBullet, transform.position, Quaternion.Euler(0, 0, 0));
                if (!isParalizer)
                {
                    //if the ship has such feature it will shot with double bullets so additional bullet is created and impulse added to it later in fixed update
                    //here it is not pu into paralizer sequence cause only cruiser 2 class will have double bullets and it is not paralizer
                    if (doubleBullet)
                    {
                        shotBulletReal2 = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                        shotBulletReal2.transform.position = transform.position * 1.1f;
                        shotBulletReal2.transform.rotation = Quaternion.Euler(0, 0, 0);
                        shotBulletReal2.SetActive(true);
                    }
                    shotBulletReal = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                    shotBulletReal.transform.position = transform.position;
                    shotBulletReal.transform.rotation = Quaternion.Euler(0, 0, 0);
                    shotBulletReal.SetActive(true);
                }

                else
                {
                    if (paralizerBullet)
                    {
                        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                        //shotBulletReal = Instantiate(currentShipBullet, transform.position, Quaternion.Euler(0, 0, 0));
                        shotBulletReal = ObjectPuller.current.GetUniversalBullet(paralBulletsListToActivate);
                        shotBulletReal.transform.position = transform.position;
                        shotBulletReal.transform.rotation = Quaternion.Euler(0, 0, 0);
                        shotBulletReal.SetActive(true);
                        paralizerBullet = false;
                    }
                    else
                    {
                        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                        //shotBulletReal = Instantiate(currentParalBullet, transform.position, Quaternion.Euler(0, 0, 0));
                        shotBulletReal = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                        shotBulletReal.transform.position = transform.position;
                        shotBulletReal.transform.rotation = Quaternion.Euler(0, 0, 0);
                        shotBulletReal.SetActive(true);
                        paralizerBullet = true;
                    }
                }

                fire = true;
                shotShound.Play();
                shotEffect.Play(); //creates energy shot effect for current ship
                                   //this one is invoking this method again after randome time which is set for Dstr3 class, so it is recursion of this method
                if (!IsInvoking("attackPointing") && !isDestroying) Invoke("attackPointing", Random.Range(ATTACK_TIME_FROM, ATTACK_TIME_FROM + 2));
            }

            else if (Lists.AllPlayerShips.Count > 0 && !isParalized && !isDestroying) //all conditions to stop attack processes with explanatory names
            {
                //---------- to make CPU to be able to recognize which ships are on opposite site
                int i = Random.Range(0, Lists.AllPlayerShips.Count); //this randome chouses any ship from list of white ships
                float accuracy = Random.Range(ACCURE_RANGE, -ACCURE_RANGE); //this randome is for 3 class destroyers only, it determines the accuracy of the shot (higher - is less accurate)

                //so shot direction is subtraction of ships point vector from enemys point vector (with accuracy correction)
                shotDirection = Lists.AllPlayerShips[i].transform.position + new Vector3(accuracy, 0, 0) - transform.position;

                // PARALIZER BULLETS EFFECTS
                //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                //if (!isParalizer) shotBulletReal = Instantiate(currentShipBullet, transform.position, Quaternion.Euler(0, 0, 0));
                if (!isParalizer)
                {
                    //if the ship has such feature it will shot with double bullets so additional bullet is created and impulse added to it later in fixed update
                    //here it is not pu into paralizer sequence cause only cruiser 2 class will have double bullets and it is not paralizer
                    if (doubleBullet)
                    {
                        shotBulletReal2 = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                        shotBulletReal2.transform.position = transform.position * 1.1f;
                        shotBulletReal2.transform.rotation = Quaternion.Euler(0, 0, 0);
                        shotBulletReal2.SetActive(true);
                    }
                    shotBulletReal = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                    shotBulletReal.transform.position = transform.position;
                    shotBulletReal.transform.rotation = Quaternion.Euler(0, 0, 0);
                    shotBulletReal.SetActive(true);
                }

                else
                {
                    if (paralizerBullet)
                    {
                        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                        //shotBulletReal = Instantiate(currentShipBullet, transform.position, Quaternion.Euler(0, 0, 0));
                        shotBulletReal = ObjectPuller.current.GetUniversalBullet(paralBulletsListToActivate);
                        shotBulletReal.transform.position = transform.position;
                        shotBulletReal.transform.rotation = Quaternion.Euler(0, 0, 0);
                        shotBulletReal.SetActive(true);
                        paralizerBullet = false;
                    }
                    else
                    {
                        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                        //shotBulletReal = Instantiate(currentParalBullet, transform.position, Quaternion.Euler(0, 0, 0));
                        shotBulletReal = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                        shotBulletReal.transform.position = transform.position;
                        shotBulletReal.transform.rotation = Quaternion.Euler(0, 0, 0);
                        shotBulletReal.SetActive(true);
                        paralizerBullet = true;
                    }
                }

                fire = true;
                shotShound.Play();
                shotEffect.Play(); //creates energy shot effect for current ship
                                   //this one is invoking this method again after randome time which is set for Dstr3 class, so it is recursion of this method
                if (!IsInvoking("attackPointing") && !isDestroying) Invoke("attackPointing", Random.Range(ATTACK_TIME_FROM, ATTACK_TIME_FROM + 2));
            }
        }

        //this is attack prioryty set for lower class cruisers and destroyers, so thay have 1 level priority system, they destroy all calsses without sequences
        else
        {
            //it works only if there is at least one opposite side ship on battlefield
            if (Lists.AllPlayerShips.Count > 0 && !isParalized && !isDestroying)
            {
                //---------- to make CPU to be able to recognize which shipa are on opposite site
                int i = Random.Range(0, Lists.AllPlayerShips.Count); //this randome chouses any ship from list of white ships
                float accuracy = Random.Range(ACCURE_RANGE, -ACCURE_RANGE); //this randome is for 3 class destroyers only, it determines the accuracy of the shot (higher - is less accurate)

                //so shot direction is subtraction of ships point vector from enemys point vector (with accuracy correction)
                shotDirection = Lists.AllPlayerShips[i].transform.position + new Vector3(accuracy, 0, 0) - transform.position;

                // PARALIZER BULLETS EFFECTS
                //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                //if (!isParalizer) shotBulletReal = Instantiate(currentShipBullet, transform.position, Quaternion.Euler(0, 0, 0));
                if (!isParalizer)
                {
                    shotBulletReal = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                    shotBulletReal.transform.position = transform.position;
                    shotBulletReal.transform.rotation = Quaternion.Euler(0, 0, 0);
                    shotBulletReal.SetActive(true);
                }

                else
                {
                    if (paralizerBullet)
                    {
                        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                        //shotBulletReal = Instantiate(currentShipBullet, transform.position, Quaternion.Euler(0, 0, 0));
                        shotBulletReal = ObjectPuller.current.GetUniversalBullet(paralBulletsListToActivate);
                        shotBulletReal.transform.position = transform.position;
                        shotBulletReal.transform.rotation = Quaternion.Euler(0, 0, 0);
                        shotBulletReal.SetActive(true);
                        paralizerBullet = false;
                    }
                    else
                    {
                        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                        //shotBulletReal = Instantiate(currentParalBullet, transform.position, Quaternion.Euler(0, 0, 0));
                        shotBulletReal = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                        shotBulletReal.transform.position = transform.position;
                        shotBulletReal.transform.rotation = Quaternion.Euler(0, 0, 0);
                        shotBulletReal.SetActive(true);
                        paralizerBullet = true;
                    }
                }

                fire = true;
                shotShound.Play();
                shotEffect.Play();

                //this one is invocing this method again after randome time which is set for Dstr3 class, so it is recursion of this method
                if (!IsInvoking("attackPointing") && !isDestroying) Invoke("attackPointing", Random.Range(ATTACK_TIME_FROM, ATTACK_TIME_FROM + 2));
            }
        }
    }

    //on destroy the ship is removed from all lists DTTTTTTTTTTTTTTTTTTTTTTOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO DDDDDDDDDDDDDDDDEEEEEEEEEELLLLLLLLLLLLLLEEEEEEEEEEEEEETTTTTTTTT
    private void OnDestroy()
    {
        Lists.AllCPUShipsWithoutGuns.Remove(gameObject);
        Lists.AllCPUShips.Remove(gameObject);
        Lists.CPUShips1Class.Remove(gameObject);
        Lists.CPUShips2Class.Remove(gameObject);

        //destroying mega laser of cruiser 1 if cruiser is destroyed
        if (megaLaser1Local)
        {
            right = true;
            Destroy(megaLaser1Local);
        }


        //----------------------------------------------------------
        //Lists.CPUCruisers.Remove(gameObject);
        //Lists.CPUCruisers4.Remove(gameObject);
        //Lists.CPUCruisers3.Remove(gameObject);
        //Lists.CPUCruisers2.Remove(gameObject);
        //Lists.CPUCruisers1.Remove(gameObject);
        //Lists.CPUDestroyers.Remove(gameObject);
        //Lists.CPUDestroyers1.Remove(gameObject);
        //Lists.CPUDestroyers2.Remove(gameObject);
        //Lists.CPUDestroyers3.Remove(gameObject);
        //Lists.CPUDestroyers4.Remove(gameObject);
        //----------------------------------------------------------------

        //if (/*powerShieldLocal*/shieldIsActive)
        //{
        //    powerShieldLocal.SetActive(false);
        //    shieldIsActive = false;
        //    CancelInvoke("putOffTheShield");
        //}//Destroy(powerShieldLocal);
        //Destroy(shipParaliser);

    }
    //this method is for call from the megaParal2Method of player cruiser2, that method call for local routine, it is constructed so cause in case of call of routine from outside in case of destroying the cruiser 2
    //this routine never stops it's effect
    public void callForMegaParalFromOutside()
    {
        StartCoroutine(megaParal());
    }

    // for external invocation with mega paralizing effect of criuser 2 class, that method is invoked from palyer joystik class, from method which is initiates cruiser 2 attack
    public IEnumerator megaParal() {
        StopCoroutine(paralizedTime(0));
        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
        if (isParalized) shipParaliser.SetActive(false);
        //if (shipParaliser) Destroy(shipParaliser);
        if (/*powerShieldLocal*/shieldIsActive)
        {
            powerShieldLocal.SetActive(false);//Destroy(powerShieldLocal);
            shieldIsActive = false;
            CancelInvoke("putOffTheShield");
        }
        //if (powerShieldLocal) Destroy(powerShieldLocal);
        if (IsInvoking("putShieldOnMethod")) CancelInvoke("putShieldOnMethod");

        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
        //shipParaliser = Instantiate(shipParaliserReal, transform.position, Quaternion.identity);

        shipParaliser = ObjectPuller.current.GetUniversalBullet(paralEffectListToActivate);
        shipParaliser.transform.position = transform.position;
        shipParaliser.transform.rotation = Quaternion.identity;
        shipParaliser.SetActive(true);

        destr_Maneuvering = false;
        isParalized = true;
        yield return new WaitForSeconds(7);
        isParalized = false;
        if (hasManeuverFeature) destr_Maneuvering = true;
        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
        //Destroy(shipParaliser);
        shipParaliser.SetActive(false);
        attackPointing();
        if (withShield) putShieldOnMethod();
    }

    IEnumerator paralizedTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (hasManeuverFeature) destr_Maneuvering = true; //turns on ships maneuvering features only if has that feature
        isParalized = false;
        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
        //Destroy(shipParaliser);
        shipParaliser.SetActive(false);
        attackPointing();
        if (withShield) putShieldOnMethod();
    }

    //this one detects the trigger of bullet, destroys it, and reduse the HP of ship
    private void OnTriggerEnter(Collider other)
    {
        if (!isDestroying)
        {
            //comparing is necessary to avoid destroying ships own bullet
            if (!other.gameObject.CompareTag("BullCruis1") && !other.gameObject.CompareTag("BullDstr1") && !other.gameObject.CompareTag("BullParal1")
            && !other.gameObject.CompareTag("BullCruis2") && !other.gameObject.CompareTag("BullDstr2") && !other.gameObject.CompareTag("BullParal2")
            && !other.gameObject.CompareTag("BullCruis3") && !other.gameObject.CompareTag("BullDstr3") && !other.gameObject.CompareTag("BullCruis4")
            && !other.gameObject.CompareTag("BullDstr4") && !other.gameObject.CompareTag("PowerShieldCPU") && !other.gameObject.CompareTag("GunBullPlay")
            && !other.gameObject.CompareTag("GunBullCPU") && !other.gameObject.CompareTag("MegaShot"))
            {
                //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((
                //GameObject burst = Instantiate(other.GetComponent<PlayerBullet>().getBurst(), other.transform.position, Quaternion.identity);
                other.GetComponent<PlayerBullet>().getBurst();

                //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((
                //Destroy(other.gameObject);
                other.gameObject.SetActive(false);

                //DESTROYERS BULLETS HARM
                if (other.gameObject.CompareTag("BullDstrPlay4"))
                    SHIP_LIFE -= DESTR_4_HARM; //reduse the life for one HP
                else if (other.gameObject.CompareTag("BullDstrPlay3"))
                    SHIP_LIFE -= DESTR_3_HARM; //reduse the life for two HP
                else if (other.gameObject.CompareTag("BullDstrPlay2"))
                    SHIP_LIFE -= DESTR_2_HARM; //reduse the life for two HP
                else if (other.gameObject.CompareTag("BullDstrPlay1"))
                    SHIP_LIFE -= DESTR_1_HARM; //reduse the life for two HP

                //CRUISERS BULLETS HARM
                else if (other.gameObject.CompareTag("BullCruisPlay4"))
                    SHIP_LIFE -= CRUIS_4_HARM; //reduse the life for two HP
                else if (other.gameObject.CompareTag("BullCruisPlay3"))
                    SHIP_LIFE -= CRUIS_3_HARM; //reduse the life for two HP
                else if (other.gameObject.CompareTag("BullCruisPlay2"))
                    SHIP_LIFE -= CRUIS_2_HARM; //reduse the life for two HP
                else if (other.gameObject.CompareTag("BullCruisPlay1"))
                    SHIP_LIFE -= CRUIS_1_HARM; //reduse the life for two HP

                // PARALIZER BULLETS EFFECTS
                else if (other.gameObject.CompareTag("BullParalPlay2"))
                {
                    //instantiates the paralizer effect around current ship, stops maneuvring features and sets isParalized bool on to turn off all action by ship
                    if (!isParalized)
                    {
                        //instantiates the paralizer effect around current ship, stops maneuvring features and sets isParalized bool on to turn off all action by ship
                        shipParaliser = ObjectPuller.current.GetUniversalBullet(paralEffectListToActivate);
                        shipParaliser.transform.position = transform.position;
                        shipParaliser.transform.rotation = Quaternion.identity;
                        shipParaliser.SetActive(true);

                        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((
                        //shipParaliser = Instantiate(shipParaliserReal, transform.position, Quaternion.identity);
                        destr_Maneuvering = false;
                        isParalized = true;
                        StartCoroutine(paralizedTime(PARALIZED_TIME2));
                    }
                }
                else if (other.gameObject.CompareTag("BullParalPlay1"))
                {
                    //instantiates the paralizer effect around current ship, stops maneuvring features and sets isParalized bool on to turn off all action by ship
                    if (!isParalized)
                    {
                        //instantiates the paralizer effect around current ship, stops maneuvring features and sets isParalized bool on to turn off all action by ship
                        shipParaliser = ObjectPuller.current.GetUniversalBullet(paralEffectListToActivate);
                        shipParaliser.transform.position = transform.position;
                        shipParaliser.transform.rotation = Quaternion.identity;
                        shipParaliser.SetActive(true);

                        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((
                        //shipParaliser = Instantiate(shipParaliserReal, transform.position, Quaternion.identity);
                        destr_Maneuvering = false;
                        isParalized = true;
                        StartCoroutine(paralizedTime(PARALIZED_TIME1));
                    }
                }
            }
        }
    }

    private void Update()
    {
        //this method shakes the camera whet mega attack is started;
        if (shakeDuration > 0 && isShaking)
        {
            Camera.main.transform.position = camStartPosition + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= decreaseFactor;
        }
        else if (isShaking)
        {
            shakeDuration = 0f;
            Camera.main.transform.position = camStartPosition;
            isShaking = false;
        }

        //moves right and left the megaAttack laser of cruiser 1 when it is on scene
        if (megaLaser1Local)
        {
            megaLaser1Local.transform.position = transform.position;
            if (right)
            {
                megaLaser1Local.transform.Rotate(0, -40 * Time.deltaTime, 0);
                if (megaLaser1Local.transform.eulerAngles.y < 140) right = false;
            }
            else
            {
                megaLaser1Local.transform.Rotate(0, 40 * Time.deltaTime, 0);
            }
            if (megaLaser1Local.transform.eulerAngles.y > 220)
            {
                right = true;
                Destroy(megaLaser1Local);
            }
        }


        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
        //to move power shield with ship itself
        //if (powerShieldLocal && destr_Maneuvering)
        //{
        //    powerShieldLocal.transform.position = transform.position;
        //}

        if (/*powerShieldLocal*/shieldIsActive && destr_Maneuvering)
        {
            powerShieldLocal.transform.position = transform.position;
        }

        if (SHIP_LIFE <= 4 && !isDestroying && !burningEffect.activeInHierarchy)
        {
            burningEffect.SetActive(true);

            GameObject exploision = ObjectPuller.current.GetUniversalBullet(ShipPreBurstEffectListToActivate);
            exploision.transform.position = transform.position;
            exploision.transform.rotation = Quaternion.identity;
            exploision.SetActive(true);



            //set disactivated some parts of ship to show an effect that they are damaged
            foreach (GameObject go in shipParts)
            {
                go.SetActive(false);
            }

            //foreach (Rigidbody rb in preDestroyPartsRB)
            //{
            //    rb.isKinematic = false;
            //    rb.AddForce(new Vector3(Random.Range(-2f, 2f), Random.Range(-2, 2), Random.Range(-2, 2)) * 2, ForceMode.Impulse);
            //    rb.AddTorque(new Vector3(Random.Range(-1f, 1f), Random.Range(-1, 1), Random.Range(-1, 1)), ForceMode.Impulse);
            //}
        }

        if (SHIP_LIFE <= 0 && !isDestroying)
        {
            isDestroying = true;
            //stopping mega attack of CPU ship if it was destroyed
            if (IsInvoking("megaShot4Method")) CancelInvoke("megaShot4Method");
            if (IsInvoking("megaDefence3Method")) CancelInvoke("megaDefence3Method");
            if (IsInvoking("megaParal2Method")) CancelInvoke("megaParal2Method");
            if (IsInvoking("megaLaser1Method")) CancelInvoke("megaLaser1Method");

            GetComponent<Collider>().enabled = false;
            Lists.AllCPUShipsWithoutGuns.Remove(gameObject);
            Lists.AllCPUShips.Remove(gameObject);
            Lists.CPUShips1Class.Remove(gameObject);
            Lists.CPUShips2Class.Remove(gameObject);

            //-----------------------------------------------------------
            //Lists.CPUCruisers.Remove(gameObject);
            //Lists.CPUCruisers4.Remove(gameObject);
            //Lists.CPUCruisers3.Remove(gameObject);
            //Lists.CPUCruisers2.Remove(gameObject);
            //Lists.CPUCruisers1.Remove(gameObject);
            //Lists.CPUDestroyers.Remove(gameObject);
            //Lists.CPUDestroyers1.Remove(gameObject);
            //Lists.CPUDestroyers2.Remove(gameObject);
            //Lists.CPUDestroyers3.Remove(gameObject);
            //Lists.CPUDestroyers4.Remove(gameObject);
            //---------------------------------------------------------


            //triggers that Player wins the battle depending on static lists count of ships on battle scene
            if (Lists.AllCPUShips.Count < 1 && Lists.AllPlayerShips.Count > 0) BattleLaunch.PlayerWin = true;

            //counts of loses to process when get back to journey scene on panel of win lose (on JourneyController class)
            if (name.Contains(destroyer4Tag)) Lists.D4LoseCPU++;
            else if (name.Contains(destroyer3Tag)) Lists.D3LoseCPU++;
            else if (name.Contains(destroyer2Tag)) Lists.D2LoseCPU++;
            else if (name.Contains(destroyer2TagPar)) Lists.D2PLoseCPU++;
            else if (name.Contains(destroyer1Tag)) Lists.D1LoseCPU++;
            else if (name.Contains(destroyer1TagPar)) Lists.D1PLoseCPU++;
            else if (name.Contains(destroyerFedTag)) Lists.DGLoseCPU++;
            else if (name.Contains(cruiser4Tag)) Lists.C4LoseCPU++;
            else if (name.Contains(cruiser3Tag)) Lists.C3LoseCPU++;
            else if (name.Contains(cruiser2Tag)) Lists.C2LoseCPU++;
            else if (name.Contains(cruiser1Tag)) Lists.C1LoseCPU++;
            else if (name.Contains(cruiserFedTag)) Lists.CGLoseCPU++;

            if (isParalized) shipParaliser.SetActive(false);
            //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
            //if (shipParaliser) Destroy(shipParaliser);
            //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
            //if (powerShieldLocal) Destroy(powerShieldLocal); 

            if (/*powerShieldLocal*/shieldIsActive)
            {
                powerShieldLocal.SetActive(false);//Destroy(powerShieldLocal);
                shieldIsActive = false;
                CancelInvoke("putOffTheShield");
            }
            burningEffect.SetActive(false); 
            GetComponent<Collider>().enabled = false; // turns off the collider of ship to prevent errors with on trigger enter functions of curren script
            Destroy(gameObject/*, 2*/); //destroys the ship if life is less than or equlas to zero

            //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
            //GameObject exploision = Instantiate(currentShipExploision, transform.position, Quaternion.identity);
            GameObject exploision = ObjectPuller.current.GetUniversalBullet(ShipBurstEffectListToActivate);
            exploision.transform.position = transform.position;
            exploision.transform.rotation = Quaternion.identity;
            exploision.SetActive(true);

            //TO DELETE
            //foreach (GameObject go in shipParts)
            //{
            //    go.GetComponent<TrailRenderer>().enabled = true;
            //    go.GetComponent<Rigidbody>().isKinematic = false;
            //    go.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-2f, 2f), Random.Range(-2, 2), Random.Range(-2, 2)) * 2, ForceMode.Impulse);
            //    go.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-1f, 1f), Random.Range(-1, 1), Random.Range(-1, 1)), ForceMode.Impulse);
            //}

            //foreach (TrailRenderer tr in partsTrails)
            //{
            //    tr.enabled = true;
            //}

            //foreach (Rigidbody rb in partsRB) {
            //    rb.isKinematic = false;
            //    rb.AddForce(new Vector3(Random.Range(-2f, 2f), Random.Range(-2, 2), Random.Range(-2, 2)) * 2, ForceMode.Impulse);
            //    rb.AddTorque(new Vector3(Random.Range(-1f, 1f), Random.Range(-1, 1), Random.Range(-1, 1)), ForceMode.Impulse);
            //}

        }

        if (destr_Maneuvering && !isDestroying)
        {
            if (transform.position.x > Maneuvering_right_border)
            {
                moveRight = false;
            }
            if (transform.position.x < Maneuvering_left_border)
            {
                moveRight = true;
            }
        }

        if (hasManeuverFeature && !isDestroying) //if the ship is destoying it does not move
        {
            if (destr_Maneuvering)
            {
                if (moveRight)
                {
                    //cause CPU ships are flipped to 180 degrees on y axis rotation on them is checked on x axis
                    transform.Translate(new Vector3(manuvreSpeed, 0, 0) * Time.deltaTime, Space.World);
                    if (transform.rotation.x * 100 > -15f) transform.Rotate(-3f, 0, 0);

                }
                if (!moveRight)
                {
                    transform.Translate(new Vector3(-manuvreSpeed, 0, 0) * Time.deltaTime, Space.World);
                    if (transform.rotation.x * 100 < 15f) transform.Rotate(3f, 0, 0);

                }
            }
            else
            {
                //shipRB.velocity = new Vector3(0, 0, 0);
                transform.Rotate(0, 0, 0);
            }
        }
    }
    private void FixedUpdate()
    {
        if (fire)
        {
            //this is necessaty to set velocity of bullet zero to avoid doubling the bullet speed each time it is activating from pull
            shotBulletReal.GetComponent<Rigidbody>().velocity = Vector3.zero;
            shotBulletReal.GetComponent<Rigidbody>().AddForce(shotDirection * SHIP_ATTACK_FORCE, ForceMode.Impulse);

            if (doubleBullet) {
                shotBulletReal2.GetComponent<Rigidbody>().velocity = Vector3.zero;
                shotBulletReal2.GetComponent<Rigidbody>().AddForce(shotDirection * SHIP_ATTACK_FORCE, ForceMode.Impulse);
            }

            if (tripleBullet)
            {
                shotBulletReal2.GetComponent<Rigidbody>().velocity = Vector3.zero;
                shotBulletReal2.GetComponent<Rigidbody>().AddForce(shotDirection * SHIP_ATTACK_FORCE, ForceMode.Impulse);

                shotBulletReal3.GetComponent<Rigidbody>().velocity = Vector3.zero;
                shotBulletReal3.GetComponent<Rigidbody>().AddForce(shotDirection * SHIP_ATTACK_FORCE, ForceMode.Impulse);
            }

            fire = false;
        }

        //TO DELETE IN CASE IF WILL WORK--------------------, changed the place of that snippet of code to update, firs of all cause I changed the 
        //movement and rotation to be controlled from tansform of game object and not from rigidbody. And cause on fixed update time reduce method 
        //had no effect on ship movement, so on update I added multiplication to Time.DeltaTime on that reason. Also I deleted rigitbody from ships 
        //to avoid a mistacke with ship crazy movements


        //if (hasManeuverFeature && !isDestroying) //if the ship is destoying it does not move
        //{
        //    if (destr_Maneuvering)
        //    {
        //        if (moveRight)
        //        {
        //            //cause CPU ships are flipped to 180 degrees on y axis rotation on them is checked on x axis
        //            shipRB.velocity = new Vector3(manuvreSpeed, 0, 0);
        //            if (transform.rotation.x * 100 > -15f) transform.Rotate(-3f, 0, 0);

        //        }
        //        if (!moveRight)
        //        {
        //            shipRB.velocity = new Vector3(-manuvreSpeed, 0, 0);
        //            if (transform.rotation.x * 100 < 15f) transform.Rotate(3f, 0, 0);

        //        }
        //    }
        //    else
        //    {
        //        shipRB.velocity = new Vector3(0, 0, 0);
        //        transform.Rotate(0, 0, 0);
        //    }
        //}

    }
}
