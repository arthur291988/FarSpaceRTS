
using System.Collections;
using System.Collections.Generic;
//using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerShipCPU : MonoBehaviour, IPointerDownHandler
{
    //are used to instantiate the bullet to shot
    private GameObject shotBulletReal;
    private GameObject shotBulletReal2; //for double bullet shot ships
    private GameObject shotBulletReal3; //for triple bullet shot ship only for Cruis1
    //private GameObject currentShipBullet; //TO DELETE IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((
    //private GameObject currentShipExploision;
    //to paint the elements of ship to decent color
    public List<GameObject> IDColorElements;

    //following bool conditions are used to set attack priority, used on attack pointing method
    private bool Class1 = false;
    private bool Class2 = false;

    //player controll passing and detection on scene
    private bool underPlayerControl = false;
    public GameObject pointerDetection;

    public GameObject[] shipParts; //used to create burst effect by giving force impulse for each ship part when it has lost its all HP (burst is initiated on Update)
    //private List<Rigidbody> partsRB; //this list is to accumulate the rigitbodies of ship parts on start and to reduce the load of CPU on update method
    //private List<Rigidbody> preDestroyPartsRB; //this list is to accumulate the rigitbodies of ship parts on start and to reduce the load of CPU on update method
    //private List<TrailRenderer> partsTrails; //this list is to accumulate the trailRenderers of ship parts on start and to reduce the load of CPU on update method
    //private List<TrailRenderer> preDestroyPartsTrails; //this list is to accumulate the trailRenderers of ship parts on start and to reduce the load of CPU on update method
    private bool isDestroying = false; // used to stop all ship processes when it is on process of destroying (cause it takes 2 secs to destroy ship GO)

    public ParticleSystem shotEffect; //this effect playes when current ship makes a shot to emitate energetic light of shot 
    public GameObject burningEffect; //burning effect of ship which is about to burst (has less than HP). Used on Update

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

    public GameObject megaAttackPS; //this one is used to sore the mega attack game object of current ship and pass it to player joystick when player touches it

    //ships paraliazing effect
    //public GameObject ParalShipCruis; 
    //public GameObject ParalShipDstr; 

    //TO DELETE IF PULLER WILL WORK ))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
    //different paral bullets depending on range
    //public GameObject paralBullet1;
    //public GameObject paralBullet2;
    //private GameObject currentParalBullet; //to assign proper paral bullet  //TO DELETE IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((

    private float PARALIZED_TIME1;
    private float PARALIZED_TIME2;

    private bool isParalizer = false; //used to determine if current ship is with paralizer features
    private bool paralizerBullet = false; //used to switch the bullet types once per shot;
    private bool isParalized = false; //current paralized state of ship
    private GameObject shipParaliser; //these are used to assign proper paraliser effect to current ship
    //private GameObject shipParaliserReal;

    //private Rigidbody shipRB;

    private string destroyer4Tag = "Destr4Play";
    private string destroyer3Tag = "Destr3Play";
    private string destroyer2Tag = "Destr2Play";
    private string destroyer2TagPar = "Destr2ParPlay";
    private string destroyer1Tag = "Destr1Play";
    private string destroyer1TagPar = "Destr1ParPlay";

    private string cruiser4Tag = "Cruis4Play";
    private string cruiser3Tag = "Cruis3Play";
    private string cruiser2Tag = "Cruis2Play";
    private string cruiser1Tag = "Cruis1Play";

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

    //turning on the maneuvering state
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
    private bool withShield=false;
    //public GameObject powerShield; //TO DELETE IF PULLER WILL WORK ))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
    public GameObject powerShieldLocal; //is public cause it is callsed from CPUShipsCtrl on megaParalizer mega attack method
    //this mat var is used to hold a reference to cruiser 3 power shield mat, to set it green for cruis4 shield and set it back to cyan while turning it off to be ready
    //for use for next call from puller
    private Material cruis4ShieldMat;

    //current ship defence field properties
    private float SHIP_SHIELD_TIME;
    private float SHIP_SHIELD_RELOAD_TIME;

    //time dilation feature properties of current ship
    private float TIME_DILATION;

    //this lists are necessary to assign proper bullet types for ships and pull the bullets from ObjectPuller class
    private List<GameObject> bulletsListToActivate;
    private List<GameObject> paralBulletsListToActivate;

    //this lists are necessary to assign proper sheild types for ships and pull the sheilds from ObjectPuller class
    private List<GameObject> sheildsListToActivate;

    //this lists are necessary to assign proper paralized effect types for ships and pull the paralized effect from ObjectPuller class
    private List<GameObject> paralEffectListToActivate;

    //this lists are necessary to assign proper burst effect types for ships and pull the burst effect from ObjectPuller class
    private List<GameObject> ShipBurstEffectListToActivate;

    //this lists are necessary to assign proper PreBurst effect types for ships and pull the burst effect from ObjectPuller class
    private List<GameObject> ShipPreBurstEffectListToActivate;

    //is public cause it is callsed from CPUShipsCtrl on megaParalizer mega attack method
    public bool shieldIsActive = false; //this one is necessary to switch power shield from one ship to another when it is disactivated. Used on put and put off shield methods

    private bool doubleBullet = false; //to use with ships that have double bullet shot features like Fed Cruiser, class and 2 class

    private bool tripleBullet = false; //to use with ships that have triple bullet shot features Only cruis 1 class

    public void offPlayerControl()
    {
        underPlayerControl = false;
        pointerDetection.SetActive(false);
        //if (powerShieldLocal)
        //{

        if (powerShieldLocal!=null) powerShieldLocal.SetActive(false);
        shieldIsActive = false;
        //powerShieldLocal = null;
        StopCoroutine(putOffTheShield(SHIP_SHIELD_TIME));
        //CancelInvoke("putOffTheShield");
        //}//Destroy(powerShieldLocal);
        Lists.PlayerShip.Remove(gameObject);
        if (!IsInvoking("attackPointing")) Invoke("attackPointing", UnityEngine.Random.Range(ATTACK_TIME_FROM, ATTACK_TIME_FROM + 2));
        if (!IsInvoking("putShieldOnMethod") && withShield) Invoke("putShieldOnMethod", 1);
    }

    //this method passes the control to player. So if ship is under player control it turns it off and if not it process turnin on the player controll
    public void OnPointerDown(PointerEventData eventData)
    {
        if (underPlayerControl)
        {
            //if ship is paralized player can not left the ship
            if (!isParalized)
            {
                underPlayerControl = false;
                pointerDetection.SetActive(false);
                Lists.PlayerShip.Remove(gameObject);
                if (!IsInvoking("putShieldOnMethod") && withShield) Invoke("putShieldOnMethod", 1);
                if (!IsInvoking("attackPointing")) Invoke("attackPointing", UnityEngine.Random.Range(ATTACK_TIME_FROM, ATTACK_TIME_FROM + 2));
            }
        }
        else 
        {
            if (!isParalized)
            {
                //this one checks if player has already some chousen ship and it turns from ship to ship or from nowhere to ship 
                if (Lists.PlayerShip.Count == 0)
                {
                    //passes the propertyes of ship to players joystick 
                    PlayersJoystick.setShip(gameObject, hasManeuverFeature, TIME_DILATION, shotEffect, megaAttackPS, doubleBullet, tripleBullet, shotShound);
                    PlayersJoystick.setShotBullet(/*currentShipBullet, TO DELETE IF PULLER WILL WORK*/ isParalizer, bulletsListToActivate);
                    if (isParalizer) PlayersJoystick.setParalBullet(/*currentParalBullet TO DELETE IF PULLER WILL WORK*/ paralBulletsListToActivate);
                    PlayersJoystick.setShield(/*powerShield,*/ SHIP_SHIELD_TIME, SHIP_SHIELD_RELOAD_TIME, withShield); 
                    //if (hasManeuverFeature) LeftMove.setShipLeft(gameObject, Maneuvering_left_border); TO DELETE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    //if (hasManeuverFeature) RightMove.setShipRight(gameObject, Maneuvering_right_border); TO DELETE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    PlayersJoystick.setAttackForce(SHIP_ATTACK_FORCE);
                    PlayersJoystick.setAttackTime(ATTACK_TIME_FROM);
                    //LeftMove.setParalized(false);
                    //RightMove.setParalized(false);
                    PlayersJoystick.setParalized(false);
                    underPlayerControl = true;
                    CancelInvoke("putOffTheShield"); //stosp the method to putting off the shield cause the shield is off by time player chose the ship
                    pointerDetection.SetActive(true);
                    if (!Lists.PlayerShip.Contains(gameObject)) Lists.PlayerShip.Add(gameObject);
                    CancelInvoke(); // this one stops all invocations off attack shot (attackPointing()) to prevent the doublings of method.
                }
                //turning from one ship to other, first checks if current ship is not paralized
                else if (!Lists.PlayerShip[0].GetComponent<PlayerShipCPU>().isParalized) {
                    PlayersJoystick.setShip(gameObject, hasManeuverFeature, TIME_DILATION, shotEffect, megaAttackPS, doubleBullet, tripleBullet, shotShound);
                    PlayersJoystick.setShotBullet(/*currentShipBullet, TO DELETE IF PULLER WILL WORK*/ isParalizer, bulletsListToActivate);
                    if (isParalizer) PlayersJoystick.setParalBullet(/*currentParalBullet TO DELETE IF PULLER WILL WORK*/paralBulletsListToActivate);
                    PlayersJoystick.setShield(/*powerShield,*/ SHIP_SHIELD_TIME, SHIP_SHIELD_RELOAD_TIME, withShield);
                    //if (hasManeuverFeature) LeftMove.setShipLeft(gameObject, Maneuvering_left_border); TO DELETE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    //if (hasManeuverFeature) RightMove.setShipRight(gameObject, Maneuvering_right_border); TO DELETE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    PlayersJoystick.setAttackForce(SHIP_ATTACK_FORCE);
                    PlayersJoystick.setAttackTime(ATTACK_TIME_FROM);
                    //turning off the player controll on previous ship
                    underPlayerControl = true;
                    StopCoroutine(putOffTheShield(SHIP_SHIELD_TIME));
                    //CancelInvoke("putOffTheShield"); //stosp the method to putting off the shield cause the shield is off by time player chose the ship
                    if (Lists.PlayerShip.Count > 0)
                    {
                        Lists.PlayerShip[0].GetComponent<PlayerShipCPU>().offPlayerControl(); //turns off the player control on prevoius ship and starts automatic shield controller
                    }
                    pointerDetection.SetActive(true);
                    //LeftMove.setParalized(false); TO DELETE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    //RightMove.setParalized(false); TO DELETE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    PlayersJoystick.setParalized(false);
                    if (!Lists.PlayerShip.Contains(gameObject)) Lists.PlayerShip.Add(gameObject);
                    CancelInvoke(); // this one stops all invocations off attack shot (attackPointing()) to prevent the doublings of method.
                }
            }
        }
    }


    //is invoket from cruisCPUmega class to reduce ship HP from mega attack
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

        shotShound = GetComponent<AudioSource>();
        //adding the ship to common batlefield ships lists depending on its tag and assigning to it the corresponding features
        if (name.Contains(destroyer4Tag))
        {
            if (!Lists.AllPlayerShipsWithoutGuns.Contains(gameObject)) Lists.AllPlayerShipsWithoutGuns.Add(gameObject);
            if (!Lists.AllPlayerShips.Contains(gameObject)) Lists.AllPlayerShips.Add(gameObject);
            //if (!Lists.PlayerDestroyers.Contains(gameObject)) Lists.PlayerDestroyers.Add(gameObject); TO DELLLLLLETE IF THERE WILL TO BE NECESSITY FOR COLLECTING THAT PRCISELY -----------------------------------------
            SHIP_LIFE = Constants.Instance.DESTR_4_HP;
            ACCURE_RANGE = Constants.Instance.DESTR_4_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.DESTR_4_ATTACK_TIME;
            //currentShipBullet = DestrBullet4; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            SHIP_ATTACK_FORCE = Constants.Instance.DESTR_4_ATTACK_FORCE;
            //currentShipExploision = DstrExploision; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            ShipBurstEffectListToActivate = ObjectPuller.current.GetDestrBurstPull();
            ShipPreBurstEffectListToActivate = ObjectPuller.current.GetDestrPreBurstPull();
            //shipParaliserReal = ParalShipDstr; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            paralEffectListToActivate = ObjectPuller.current.GetDestrParalEffectPull();

            //time dilation
            TIME_DILATION = Constants.Instance.DESTR_4_TIME_DILATION;

            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            bulletsListToActivate = ObjectPuller.current.GetD4BulletPlayerList();


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
            if (!Lists.AllPlayerShipsWithoutGuns.Contains(gameObject)) Lists.AllPlayerShipsWithoutGuns.Add(gameObject);
            if (!Lists.AllPlayerShips.Contains(gameObject)) Lists.AllPlayerShips.Add(gameObject);
            //if (!Lists.PlayerDestroyers.Contains(gameObject)) Lists.PlayerDestroyers.Add(gameObject); ---------------------------------------------------------------------------------
            SHIP_LIFE = Constants.Instance.DESTR_3_HP;
            ACCURE_RANGE = Constants.Instance.DESTR_3_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.DESTR_3_ATTACK_TIME;
            //currentShipBullet = DestrBullet3; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
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
            sheildsListToActivate = ObjectPuller.current.GetD3ShieldPlayPull();

            //time dilation
            TIME_DILATION = Constants.Instance.DESTR_3_TIME_DILATION;

            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            bulletsListToActivate = ObjectPuller.current.GetD3BulletPlayerList();
            
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
            if (!Lists.AllPlayerShipsWithoutGuns.Contains(gameObject)) Lists.AllPlayerShipsWithoutGuns.Add(gameObject);
            if (!Lists.AllPlayerShips.Contains(gameObject)) Lists.AllPlayerShips.Add(gameObject);
            if (!Lists.PlayerShips2Class.Contains(gameObject)) Lists.PlayerShips2Class.Add(gameObject);
            //if (!Lists.PlayerDestroyers.Contains(gameObject)) Lists.PlayerDestroyers.Add(gameObject); ---------------------------------------------------------------------------------------
            //if (!Lists.PlayerDestroyers2.Contains(gameObject)) Lists.PlayerDestroyers2.Add(gameObject); ---------------------------------------------------------------------------------------
            SHIP_LIFE = Constants.Instance.DESTR_2_HP;
            ACCURE_RANGE = Constants.Instance.DESTR_2_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.DESTR_2_ATTACK_TIME;
            //currentShipBullet = DestrBullet2; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            SHIP_ATTACK_FORCE = Constants.Instance.DESTR_2_ATTACK_FORCE;
            //currentShipExploision = DstrExploision; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            ShipBurstEffectListToActivate = ObjectPuller.current.GetDestrBurstPull();
            ShipPreBurstEffectListToActivate = ObjectPuller.current.GetDestrPreBurstPull();
            //shipParaliserReal = ParalShipDstr; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            paralEffectListToActivate = ObjectPuller.current.GetDestrParalEffectPull();

            //paraliser features
            isParalizer = true;
            //currentParalBullet = paralBullet2; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            paralBulletsListToActivate = ObjectPuller.current.GetD2PBulletPlayerList();

            //Power shiled properties
            withShield = true;
            StartCoroutine(waitForFirstShield());
            SHIP_SHIELD_TIME = Constants.Instance.DESTR_2_SHIELD_TIME;
            SHIP_SHIELD_RELOAD_TIME = Constants.Instance.DESTR_2_SHIELD_RELOAD_TIME;
            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            sheildsListToActivate = ObjectPuller.current.GetD2ShieldPlayPull();

            //time dilation
            TIME_DILATION = Constants.Instance.DESTR_2_TIME_DILATION;

            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            bulletsListToActivate = ObjectPuller.current.GetD2BulletPlayerList();

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

        else if (name.Contains(destroyer2Tag))
        {
            if (!Lists.AllPlayerShipsWithoutGuns.Contains(gameObject)) Lists.AllPlayerShipsWithoutGuns.Add(gameObject);
            if (!Lists.AllPlayerShips.Contains(gameObject)) Lists.AllPlayerShips.Add(gameObject);
            if (!Lists.PlayerShips2Class.Contains(gameObject)) Lists.PlayerShips2Class.Add(gameObject);

            //if (!Lists.PlayerDestroyers.Contains(gameObject)) Lists.PlayerDestroyers.Add(gameObject); ---------------------------------------------------------------------------------------
            //if (!Lists.PlayerDestroyers2.Contains(gameObject)) Lists.PlayerDestroyers2.Add(gameObject); ---------------------------------------------------------------------------------------
            SHIP_LIFE = Constants.Instance.DESTR_2_HP;
            ACCURE_RANGE = Constants.Instance.DESTR_2_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.DESTR_2_ATTACK_TIME;
            //currentShipBullet = DestrBullet2; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
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
            sheildsListToActivate = ObjectPuller.current.GetD2ShieldPlayPull();

            //time dilation
            TIME_DILATION = Constants.Instance.DESTR_2_TIME_DILATION;

            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            bulletsListToActivate = ObjectPuller.current.GetD2BulletPlayerList();

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

        else if (name.Contains(destroyer1Tag))
        {
            if (!Lists.AllPlayerShipsWithoutGuns.Contains(gameObject)) Lists.AllPlayerShipsWithoutGuns.Add(gameObject);
            if (!Lists.AllPlayerShips.Contains(gameObject)) Lists.AllPlayerShips.Add(gameObject);
            if (!Lists.PlayerShips1Class.Contains(gameObject)) Lists.PlayerShips1Class.Add(gameObject);

            //if (!Lists.PlayerDestroyers.Contains(gameObject)) Lists.PlayerDestroyers.Add(gameObject); ---------------------------------------------------------------------------------------
            //if (!Lists.PlayerDestroyers1.Contains(gameObject)) Lists.PlayerDestroyers1.Add(gameObject); ---------------------------------------------------------------------------------------
            SHIP_LIFE = Constants.Instance.DESTR_1_HP;
            ACCURE_RANGE = Constants.Instance.DESTR_1_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.DESTR_1_ATTACK_TIME;
            //currentShipBullet = DestrBullet1; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            SHIP_ATTACK_FORCE = Constants.Instance.DESTR_1_ATTACK_FORCE;
            //currentShipExploision = DstrExploision; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            ShipBurstEffectListToActivate = ObjectPuller.current.GetDestrBurstPull();
            ShipPreBurstEffectListToActivate = ObjectPuller.current.GetDestrPreBurstPull();
            //shipParaliserReal = ParalShipDstr; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            paralEffectListToActivate = ObjectPuller.current.GetDestrParalEffectPull();


            doubleBullet = true; //makes curren cruiser to shot with double bullets per one shot

            //Maneuvering features
            //manuvreSpeed = Lists.DESTR_1_MANUVRE_SPEED;
            //Maneuvering_left_border = transform.position.x - MANEUVERING_RATE;
            //Maneuvering_right_border = transform.position.x + MANEUVERING_RATE;
            ////destr_Maneuvering = true; //TO SED BACK, ONLY FOR TESTINGS
            ////hasManeuverFeature = true;
            //if (Random.Range(0f, 1f) < 0.5f) moveRight = true;
            //else moveRight = false;

            //Power shiled properties
            withShield = true;
            StartCoroutine(waitForFirstShield());
            SHIP_SHIELD_TIME = Constants.Instance.DESTR_1_SHIELD_TIME;
            SHIP_SHIELD_RELOAD_TIME = Constants.Instance.DESTR_1_SHIELD_RELOAD_TIME;
            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            sheildsListToActivate = ObjectPuller.current.GetD1ShieldPlayPull();

            //time dilation
            TIME_DILATION = Constants.Instance.DESTR_1_TIME_DILATION;

            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            bulletsListToActivate = ObjectPuller.current.GetD1BulletPlayerList();

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

        //PARALIZER
        else if (name.Contains(destroyer1TagPar))
        {
            if (!Lists.AllPlayerShipsWithoutGuns.Contains(gameObject)) Lists.AllPlayerShipsWithoutGuns.Add(gameObject);
            if (!Lists.AllPlayerShips.Contains(gameObject)) Lists.AllPlayerShips.Add(gameObject);
            if (!Lists.PlayerShips1Class.Contains(gameObject)) Lists.PlayerShips1Class.Add(gameObject);

            //if (!Lists.PlayerDestroyers.Contains(gameObject)) Lists.PlayerDestroyers.Add(gameObject); ---------------------------------------------------------------------------------------
            //if (!Lists.PlayerDestroyers1.Contains(gameObject)) Lists.PlayerDestroyers1.Add(gameObject); ---------------------------------------------------------------------------------------
            SHIP_LIFE = Constants.Instance.DESTR_1_HP;
            ACCURE_RANGE = Constants.Instance.DESTR_1_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.DESTR_1_ATTACK_TIME;
            //currentShipBullet = DestrBullet1;  TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            SHIP_ATTACK_FORCE = Constants.Instance.DESTR_1_ATTACK_FORCE;
            //currentShipExploision = DstrExploision; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            ShipBurstEffectListToActivate = ObjectPuller.current.GetDestrBurstPull();
            ShipPreBurstEffectListToActivate = ObjectPuller.current.GetDestrPreBurstPull();
            //shipParaliserReal = ParalShipDstr; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            paralEffectListToActivate = ObjectPuller.current.GetDestrParalEffectPull();

            //paralizer features
            isParalizer = true;
            //currentParalBullet = paralBullet1; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            paralBulletsListToActivate = ObjectPuller.current.GetD1PBulletPlayerList();


            doubleBullet = true; //makes curren cruiser to shot with double bullets per one shot
            //Maneuvering features
            //manuvreSpeed = Lists.DESTR_1_MANUVRE_SPEED;
            //Maneuvering_left_border = transform.position.x - MANEUVERING_RATE;
            //Maneuvering_right_border = transform.position.x + MANEUVERING_RATE;
            //destr_Maneuvering = true;
            //hasManeuverFeature = true;
            //if (Random.Range(0f, 1f) < 0.5f) moveRight = true;
            //else moveRight = false;

            //Power shiled properties
            withShield = true;
            StartCoroutine(waitForFirstShield());
            SHIP_SHIELD_TIME = Constants.Instance.DESTR_1_SHIELD_TIME;
            SHIP_SHIELD_RELOAD_TIME = Constants.Instance.DESTR_1_SHIELD_RELOAD_TIME;
            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            sheildsListToActivate = ObjectPuller.current.GetD1ShieldPlayPull();

            //time dilation
            TIME_DILATION = Constants.Instance.DESTR_1_TIME_DILATION;

            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            bulletsListToActivate = ObjectPuller.current.GetD1BulletPlayerList();

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
            //if (!Lists.AllPlayerShipsWithoutGuns.Contains(gameObject)) Lists.AllPlayerShipsWithoutGuns.Add(gameObject);
            if (!Lists.AllPlayerShips.Contains(gameObject)) Lists.AllPlayerShips.Add(gameObject);
            //if (!Lists.PlayerCruisers.Contains(gameObject)) Lists.PlayerCruisers.Add(gameObject);
            if (!Lists.PlayerCruisers4.Contains(gameObject)) Lists.PlayerCruisers4.Add(gameObject); 
            SHIP_LIFE = Constants.Instance.CRUIS_4_HP;
            ACCURE_RANGE = Constants.Instance.CRUIS_4_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.CRUIS_4_ATTACK_TIME;
            //currentShipBullet = CruiserBullet4; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            SHIP_ATTACK_FORCE = Constants.Instance.CRUIS_4_ATTACK_FORCE;

            //currentShipExploision = CruiserExploision;TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            ShipBurstEffectListToActivate = ObjectPuller.current.GetCruisBurstPull();
            ShipPreBurstEffectListToActivate = ObjectPuller.current.GetCruisPreBurstPull();

            //shipParaliserReal = ParalShipCruis; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            paralEffectListToActivate = ObjectPuller.current.GetCruisParalEffectPull();

            //Power shiled properties
            withShield = true;
            StartCoroutine(waitForFirstShield());
            SHIP_SHIELD_TIME = Constants.Instance.CRUIS_4_SHIELD_TIME;
            SHIP_SHIELD_RELOAD_TIME = Constants.Instance.CRUIS_4_SHIELD_RELOAD_TIME;
            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            sheildsListToActivate = ObjectPuller.current.GetC3ShieldPlayPull();

            //time dilation
            TIME_DILATION = Constants.Instance.CRUIS_4_TIME_DILATION;

            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class in case if this is player's ship
            bulletsListToActivate = ObjectPuller.current.GetC4BulletPlayerList();

            //automatically setting the cruiser under player control from the beginning of game if there is no cruisers of higher level that player has chosen to put 
            //on battlefield. Is checked by ensuring that special int var that holds the count of higher level cruisers is zero.
            //that int var is assigned while putting ships with special UI on BattleLaunche class.
            //passes the propertyes of ship to players joystick 
            if (Lists.cruiser1PlayerPutOnScene == 0 && Lists.cruiser2PlayerPutOnScene == 0 && Lists.cruiser3PlayerPutOnScene == 0 && !Lists.playerCruiserSet)
            {
                Lists.playerCruiserSet = true;
                PlayersJoystick.setShip(gameObject, hasManeuverFeature, TIME_DILATION, shotEffect, megaAttackPS, doubleBullet, tripleBullet, shotShound);
                PlayersJoystick.setShotBullet(/*currentShipBullet, TO DELETE IF PULLER WILL WORK*/ isParalizer, bulletsListToActivate);
                PlayersJoystick.setShield(/*powerShield,*/ SHIP_SHIELD_TIME, SHIP_SHIELD_RELOAD_TIME, withShield);
                PlayersJoystick.setAttackForce(SHIP_ATTACK_FORCE);
                PlayersJoystick.setAttackTime(ATTACK_TIME_FROM);
                //LeftMove.setParalized(false);
                //RightMove.setParalized(false);
                PlayersJoystick.setParalized(false);
                underPlayerControl = true;
                pointerDetection.SetActive(true);
                if (!Lists.PlayerShip.Contains(gameObject)) Lists.PlayerShip.Add(gameObject);
                CancelInvoke(); // this one stops all invocations off attack shot (attackPointing()) to prevent the doublings of method.
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
            //if (!Lists.AllPlayerShipsWithoutGuns.Contains(gameObject)) Lists.AllPlayerShipsWithoutGuns.Add(gameObject);
            if (!Lists.AllPlayerShips.Contains(gameObject)) Lists.AllPlayerShips.Add(gameObject);
            //if (!Lists.PlayerCruisers.Contains(gameObject)) Lists.PlayerCruisers.Add(gameObject);
            if (!Lists.PlayerCruisers3.Contains(gameObject)) Lists.PlayerCruisers3.Add(gameObject);
            SHIP_LIFE = Constants.Instance.CRUIS_3_HP;
            ACCURE_RANGE = Constants.Instance.CRUIS_3_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.CRUIS_3_ATTACK_TIME;
            //currentShipBullet = CruiserBullet3;  TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            SHIP_ATTACK_FORCE = Constants.Instance.CRUIS_3_ATTACK_FORCE;

            //currentShipExploision = CruiserExploision;TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            ShipBurstEffectListToActivate = ObjectPuller.current.GetCruisBurstPull();
            ShipPreBurstEffectListToActivate = ObjectPuller.current.GetCruisPreBurstPull();

            //shipParaliserReal = ParalShipCruis; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
            paralEffectListToActivate = ObjectPuller.current.GetCruisParalEffectPull();

            //Power shiled properties
            withShield = true;
            StartCoroutine(waitForFirstShield());
            SHIP_SHIELD_TIME = Constants.Instance.CRUIS_3_SHIELD_TIME;
            SHIP_SHIELD_RELOAD_TIME = Constants.Instance.CRUIS_3_SHIELD_RELOAD_TIME;
            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            sheildsListToActivate = ObjectPuller.current.GetC3ShieldPlayPull();

            //time dilation
            TIME_DILATION = Constants.Instance.CRUIS_3_TIME_DILATION;

            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            bulletsListToActivate = ObjectPuller.current.GetC3BulletPlayerList();

            //automatically setting the cruiser under player control from the beginning of game if there is no cruisers of higher level that player has chosen to put 
            //on battlefield. Is checked by ensuring that special int var that holds the count of higher level cruisers is zero.
            //that int var is assigned while putting ships with special UI on BattleLaunche class.
            ////passes the propertyes of ship to players joystick 
            if (Lists.cruiser1PlayerPutOnScene == 0 && Lists.cruiser2PlayerPutOnScene == 0 && !Lists.playerCruiserSet)
            {
                Lists.playerCruiserSet = true;
                PlayersJoystick.setShip(gameObject, hasManeuverFeature, TIME_DILATION, shotEffect, megaAttackPS, doubleBullet, tripleBullet, shotShound);
                PlayersJoystick.setShotBullet(/*currentShipBullet, TO DELETE IF PULLER WILL WORK*/ isParalizer, bulletsListToActivate);
                PlayersJoystick.setShield(/*powerShield,*/ SHIP_SHIELD_TIME, SHIP_SHIELD_RELOAD_TIME, withShield);
                PlayersJoystick.setAttackForce(SHIP_ATTACK_FORCE);
                PlayersJoystick.setAttackTime(ATTACK_TIME_FROM);
                //LeftMove.setParalized(false);
                //RightMove.setParalized(false);
                PlayersJoystick.setParalized(false);
                underPlayerControl = true;
                pointerDetection.SetActive(true);
                if (!Lists.PlayerShip.Contains(gameObject)) Lists.PlayerShip.Add(gameObject);
                CancelInvoke(); // this one stops all invocations off attack shot (attackPointing()) to prevent the doublings of method.
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
            //if (!Lists.AllPlayerShipsWithoutGuns.Contains(gameObject)) Lists.AllPlayerShipsWithoutGuns.Add(gameObject);
            if (!Lists.AllPlayerShips.Contains(gameObject)) Lists.AllPlayerShips.Add(gameObject);
            if (!Lists.PlayerShips2Class.Contains(gameObject)) Lists.PlayerShips2Class.Add(gameObject);
            //if (!Lists.PlayerCruisers.Contains(gameObject)) Lists.PlayerCruisers.Add(gameObject);
            if (!Lists.PlayerCruisers2.Contains(gameObject)) Lists.PlayerCruisers2.Add(gameObject);

            SHIP_LIFE = Constants.Instance.CRUIS_2_HP;
            ACCURE_RANGE = Constants.Instance.CRUIS_2_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.CRUIS_2_ATTACK_TIME;
            //currentShipBullet = CruiserBullet2; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
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
            if (UnityEngine.Random.Range(0f, 1f) < 0.5f) moveRight = true;
            else moveRight = false;

            //Power shiled properties
            withShield = true;
            StartCoroutine(waitForFirstShield());
            SHIP_SHIELD_TIME = Constants.Instance.CRUIS_2_SHIELD_TIME;
            SHIP_SHIELD_RELOAD_TIME = Constants.Instance.CRUIS_2_SHIELD_RELOAD_TIME;
            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            sheildsListToActivate = ObjectPuller.current.GetC2ShieldPlayPull();

            doubleBullet = true; //makes curren cruiser to shot with double bullets per one shot

            //time dilation
            TIME_DILATION = Constants.Instance.CRUIS_2_TIME_DILATION;

            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            bulletsListToActivate = ObjectPuller.current.GetC2BulletPlayerList();

            //to set priority attack of 2 levels
            Class2 = true;

            //automatically setting the cruiser under player control from the beginning of game if there is no cruisers of higher level that player has chosen to put 
            //on battlefield. Is checked by ensuring that special int var that holds the count of higher level cruisers is zero.
            //that int var is assigned while putting ships with special UI on BattleLaunche class.
            ////passes the propertyes of ship to players joystick 
            if (Lists.cruiser1PlayerPutOnScene == 0 && !Lists.playerCruiserSet)
            {
                Lists.playerCruiserSet = true;
                PlayersJoystick.setShip(gameObject, hasManeuverFeature, TIME_DILATION, shotEffect, megaAttackPS, doubleBullet, tripleBullet, shotShound);
                PlayersJoystick.setShotBullet(/*currentShipBullet, TO DELETE IF PULLER WILL WORK*/ isParalizer, bulletsListToActivate);
                PlayersJoystick.setShield(/*powerShield,*/ SHIP_SHIELD_TIME, SHIP_SHIELD_RELOAD_TIME, withShield);
                PlayersJoystick.setAttackForce(SHIP_ATTACK_FORCE);
                PlayersJoystick.setAttackTime(ATTACK_TIME_FROM);
                //LeftMove.setParalized(false);
                //RightMove.setParalized(false);
                PlayersJoystick.setParalized(false);
                underPlayerControl = true;
                pointerDetection.SetActive(true);
                if (!Lists.PlayerShip.Contains(gameObject)) Lists.PlayerShip.Add(gameObject);
                CancelInvoke(); // this one stops all invocations off attack shot (attackPointing()) to prevent the doublings of method.
            }

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
            //if (!Lists.AllPlayerShipsWithoutGuns.Contains(gameObject)) Lists.AllPlayerShipsWithoutGuns.Add(gameObject);
            if (!Lists.AllPlayerShips.Contains(gameObject)) Lists.AllPlayerShips.Add(gameObject);
            if (!Lists.PlayerShips1Class.Contains(gameObject)) Lists.PlayerShips1Class.Add(gameObject);
            //if (!Lists.PlayerCruisers.Contains(gameObject)) Lists.PlayerCruisers.Add(gameObject);
            if (!Lists.PlayerCruisers1.Contains(gameObject)) Lists.PlayerCruisers1.Add(gameObject);
            SHIP_LIFE = Constants.Instance.CRUIS_1_HP;
            ACCURE_RANGE = Constants.Instance.CRUIS_1_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.CRUIS_1_ATTACK_TIME;
            //currentShipBullet = CruiserBullet1; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
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
            if (UnityEngine.Random.Range(0f, 1f) < 0.5f) moveRight = true;
            else moveRight = false;

            //Power shiled properties
            withShield = true;
            StartCoroutine(waitForFirstShield());
            SHIP_SHIELD_TIME = Constants.Instance.CRUIS_1_SHIELD_TIME;
            SHIP_SHIELD_RELOAD_TIME = Constants.Instance.CRUIS_1_SHIELD_RELOAD_TIME;
            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            sheildsListToActivate = ObjectPuller.current.GetC1ShieldPlayPull();

            //time dilation
            TIME_DILATION = Constants.Instance.CRUIS_1_TIME_DILATION;

            tripleBullet = true; //makes curren cruiser to shot with triple bullets per one shot

            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            bulletsListToActivate = ObjectPuller.current.GetC1BulletPlayerList();

            //to set priority attack of 3 levels
            Class1 = true;

            //automatically setting the cruiser under player control from the beginning of game if there is no cruisers of higher level (though cruiser 1 it is not checked cause it has higest level)
            //that player has chosen to put on battlefield. Is checked by ensuring that special int var that holds the count of higher level cruisers is zero.
            //that int var is assigned while putting ships with special UI on BattleLaunche class.
            ////passes the propertyes of ship to players joystick 
            if (!Lists.playerCruiserSet)
            {
                Lists.playerCruiserSet = true;
                PlayersJoystick.setShip(gameObject, hasManeuverFeature, TIME_DILATION, shotEffect, megaAttackPS, doubleBullet, tripleBullet, shotShound);
                PlayersJoystick.setShotBullet(/*currentShipBullet, TO DELETE IF PULLER WILL WORK*/ isParalizer, bulletsListToActivate);
                PlayersJoystick.setShield(/*powerShield,*/ SHIP_SHIELD_TIME, SHIP_SHIELD_RELOAD_TIME, withShield);
                PlayersJoystick.setAttackForce(SHIP_ATTACK_FORCE);
                PlayersJoystick.setAttackTime(ATTACK_TIME_FROM);
                //LeftMove.setParalized(false);
                //RightMove.setParalized(false);
                PlayersJoystick.setParalized(false);
                underPlayerControl = true;
                pointerDetection.SetActive(true);
                if (!Lists.PlayerShip.Contains(gameObject)) Lists.PlayerShip.Add(gameObject);
                CancelInvoke(); // this one stops all invocations off attack shot (attackPointing()) to prevent the doublings of method
            }

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

        PARALIZED_TIME1 = Constants.Instance.PARALIZE_TIME1;
        PARALIZED_TIME2 = Constants.Instance.PARALIZE_TIME2;

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
            IDColorElements[i].GetComponent<MeshRenderer>().material.SetColor("_Color", Lists.colorOfPlayer);
        }

        StartCoroutine(waitForFirstAttack());
    }

    //the public method to set one of the player ships under control of player, it is invoked from Battle launche method by chosing a randome ship from lists if 
    //ships on scene. Public cause it is invoked from othe class
    //public void setOneOfCruisersUnderPlayerControl()
    //{
    //    //automatically setting the cruiser under player control from the beginning of game
    //    //passes the propertyes of ship to players joystick 
    //    if (name.Contains(cruiser4Tag)) bulletsListToActivate = ObjectPuller.current.GetC4BulletPlayerList();
    //    else if (name.Contains(cruiser3Tag)) bulletsListToActivate = ObjectPuller.current.GetC3BulletPlayerList();
    //    else if (name.Contains(cruiser2Tag)) bulletsListToActivate = ObjectPuller.current.GetC2BulletPlayerList();
    //    else if (name.Contains(cruiser1Tag)) bulletsListToActivate = ObjectPuller.current.GetC1BulletPlayerList();


    //    if (name.Contains(cruiser4Tag)) sheildsListToActivate = ObjectPuller.current.GetC3ShieldPlayPull();
    //    else if (name.Contains(cruiser3Tag)) sheildsListToActivate = ObjectPuller.current.GetC3ShieldPlayPull();
    //    else if (name.Contains(cruiser2Tag)) sheildsListToActivate = ObjectPuller.current.GetC2ShieldPlayPull();
    //    else if (name.Contains(cruiser1Tag)) sheildsListToActivate = ObjectPuller.current.GetC1ShieldPlayPull();

    //    PlayersJoystick.setShip(gameObject, hasManeuverFeature, TIME_DILATION, shotEffect, megaAttackPS);
    //    PlayersJoystick.setShotBullet(/*currentShipBullet, TO DELETE IF PULLER WILL WORK*/ isParalizer, bulletsListToActivate);
    //    PlayersJoystick.setShield(/*powerShield,*/ SHIP_SHIELD_TIME, SHIP_SHIELD_RELOAD_TIME, withShield);
    //    PlayersJoystick.setAttackForce(SHIP_ATTACK_FORCE);
    //    PlayersJoystick.setAttackTime(ATTACK_TIME_FROM);
    //    LeftMove.setParalized(false);
    //    RightMove.setParalized(false);
    //    PlayersJoystick.setParalized(false);
    //    underPlayerControl = true;
    //    pointerDetection.SetActive(true);
    //    if (!Lists.PlayerShip.Contains(gameObject)) Lists.PlayerShip.Add(gameObject);
    //    CancelInvoke(); // this one stops all invocations off attack shot (attackPointing()) to prevent the doublings of method.
    //}

    //initiate the first attack of CPU ship (2 secs are necessary for waiting to other ships are added to collections)
    IEnumerator waitForFirstAttack()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(1f,3f));
        if (!underPlayerControl) attackPointing();
    }


    //POWER SHIELD WORK FEATURES

    IEnumerator waitForFirstShield()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 3f));
        if (!IsInvoking("putShieldOnMethod") && withShield) putShieldOnMethod();
    }

    //this method is invoked from PlayerJoystick method in case if player is taped the power shiled button, it is necessary for escaping to be paralized in 
    //case of mega paralizer effect cause mega parlizer checks if power shield is turned on from from current class script 

    public void putShieldOnMethodForJoystick()
    {
        if (!isParalized && !isDestroying) //so shield is put on only in case if current ship is not paralized or is not in destroying process
        {
            //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
            //powerShieldLocal = Instantiate(powerShield, transform.position, Quaternion.identity);
            //Destroy(powerShieldLocal, SHIP_SHIELD_TIME);
            
            //getting the proper shield from pull of class ObjectPuller
            powerShieldLocal = ObjectPuller.current.GetUniversalBullet(sheildsListToActivate);
            powerShieldLocal.transform.position = transform.position;
            powerShieldLocal.transform.rotation = Quaternion.identity;
            //assigning a green color for cruiser 4 power shield since it uses 3 class cruiser shield which is cyan. This is used only for 4 class cruiser 
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

            StopCoroutine(putOffTheShield(SHIP_SHIELD_TIME));
            StartCoroutine(putOffTheShield(SHIP_SHIELD_TIME));
            //if (!IsInvoking("putOffTheShield")) Invoke("putOffTheShield", SHIP_SHIELD_TIME);
        }
    }

    private void putShieldOnMethod() {
        //so shield is put on only in case if current ship is not paralized, of is not under player control or is not in destroying process

        if (!isParalized && !underPlayerControl && !isDestroying)
        {
            //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
            //if (!powerShieldLocal) powerShieldLocal = Instantiate(powerShield, transform.position, Quaternion.identity);

            //if (!powerShieldLocal)
            //{
            powerShieldLocal = ObjectPuller.current.GetUniversalBullet(sheildsListToActivate);
            powerShieldLocal.transform.position = transform.position;
            powerShieldLocal.transform.rotation = Quaternion.identity;
            //assigning a green color for cruiser 4 power shield since it uses 3 class cruiser shield which is cyan. This is used only for 4 class cruiser 
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
            //}
            //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
            //Destroy(powerShieldLocal, SHIP_SHIELD_TIME);
            StopCoroutine(putOffTheShield(SHIP_SHIELD_TIME));
            StartCoroutine(putOffTheShield(SHIP_SHIELD_TIME));
            //if (!IsInvoking("putOffTheShield")) Invoke("putOffTheShield", SHIP_SHIELD_TIME);
            if (!IsInvoking("putShieldOnMethod")) Invoke("putShieldOnMethod", SHIP_SHIELD_RELOAD_TIME + SHIP_SHIELD_TIME);
        }
    }



    //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((
    //sets the sheild inactive and pusts it back to pull

    private IEnumerator putOffTheShield(float shieldTime)
    {
        yield return new WaitForSeconds(shieldTime);

        powerShieldLocal.SetActive(false);
        shieldIsActive = false;
        //setting back the material color to cyan to make it usable foor next call from the puller
        //if (name.Contains(cruiser4Tag)) cruis4ShieldMat.SetColor("_Color", new Color(0, 1.4f, 1.4f, 1));
    }

    //private void putOffTheShield()
    //{ 
    //    powerShieldLocal.SetActive(false);
    //    shieldIsActive = false;
    //    //setting back the material color to cyan to make it usable foor next call from the puller
    //    //if (name.Contains(cruiser4Tag)) cruis4ShieldMat.SetColor("_Color", new Color(0, 1.4f, 1.4f, 1));
    //}

    //TOOOOOOOO DELETE
    //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&& to debug with player control features
    //IEnumerator putShieldOff() {
    //    if (!underPlayerControl) yield return new WaitForSeconds(SHIP_SHIELD_TIME);
    //    //shieldIsOn = false;
    //    Destroy(powerShieldLocal);
    //    if (!underPlayerControl) StartCoroutine(putShieldOn());
    //}

    //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&& to debug with player control features
    //IEnumerator putShieldOn()
    //{
    //    if (!underPlayerControl) yield return new WaitForSeconds(SHIP_SHIELD_RELOAD_TIME);
    //    //if the ship is not paralized that code puts power shield on
    //    if (!isParalized&& !underPlayerControl)
    //    {
    //        //shieldIsOn = true;
    //        powerShieldLocal = Instantiate(powerShield,transform.position,Quaternion.identity);
    //    }
    //    if (!underPlayerControl) StartCoroutine(putShieldOff());
    //}

    //CPU attack management function
    private void attackPointing()
    {
        //this is attack prioryty set for 1 class cruisers and destroyers, so thay have 3 level priority system, first they destroy 1 class ships next 2 class ships and next other calsses
        if (Class1)
        {
            //it works only if there is at least one opposite side ship on battlefield
            if (Lists.CPUShips1Class.Count > 0 && !isParalized && !underPlayerControl && !isDestroying) //all conditions to stop attack processes with explanatory names
            {
                //---------- to make CPU to be able to recognize which ships are on opposite site
                int i = UnityEngine.Random.Range(0, Lists.CPUShips1Class.Count); //this randome chouses any ship from list of white ships
                float accuracy = UnityEngine.Random.Range(ACCURE_RANGE, -ACCURE_RANGE); //this randome is for 3 class destroyers only, it determines the accuracy of the shot (higher - is less accurate)

                //so shot direction is subtraction of ships point vector from enemys point vector (with accuracy correction)
                shotDirection = Lists.CPUShips1Class[i].transform.position + new Vector3(accuracy, 0, 0) - transform.position;

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
                    if (tripleBullet) {

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
                            shotBulletReal2.transform.position = transform.position * 1.07f;
                            shotBulletReal2.transform.rotation = Quaternion.Euler(0, 0, 0);
                            shotBulletReal2.SetActive(true);
                        }
                    }
                }

                fire = true;
                shotShound.Play();
                shotEffect.Play(); //creates energy shot effect for current ship
                                   //this one is invoking this method again after randome time which is set for Dstr3 class, so it is recursion of this method
                if (!IsInvoking("attackPointing") && !isDestroying) Invoke("attackPointing", UnityEngine.Random.Range(ATTACK_TIME_FROM, ATTACK_TIME_FROM + 2));
            }
            else if (Lists.CPUShips2Class.Count > 0 && !isParalized && !underPlayerControl && !isDestroying) //all conditions to stop attack processes with explanatory names
            {
                //---------- to make CPU to be able to recognize which ships are on opposite site
                int i = UnityEngine.Random.Range(0, Lists.CPUShips2Class.Count); //this randome chouses any ship from list of white ships
                float accuracy = UnityEngine.Random.Range(ACCURE_RANGE, -ACCURE_RANGE); //this randome is for 3 class destroyers only, it determines the accuracy of the shot (higher - is less accurate)

                //so shot direction is subtraction of ships point vector from enemys point vector (with accuracy correction)
                shotDirection = Lists.CPUShips2Class[i].transform.position + new Vector3(accuracy, 0, 0) - transform.position;

                // PARALIZER BULLETS EFFECTS
                //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                //if (!isParalizer) shotBulletReal = Instantiate(currentShipBullet, transform.position, Quaternion.Euler(0, 0, 0));

                if (!isParalizer)
                {
                    //if the ship has such feature it will shot with double bullets so additional bullet is created and impulse added to it later in fixed update
                    if (doubleBullet)
                    {
                        shotBulletReal2 = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                        shotBulletReal2.transform.position = transform.position * 1.07f;
                        shotBulletReal2.transform.rotation = Quaternion.Euler(0, 0, 0);
                        shotBulletReal2.SetActive(true);
                    }
                    //that has only one type of ship cruis 1 class (so the feature is only on non paralizer and only on 1 class block)
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
                        paralizerBullet = false;
                        paralizerBullet = true;

                        //if the ship has such feature it will shot with double bullets so additional bullet is created and impulse added to it later in fixed update
                        if (doubleBullet)
                        {
                            shotBulletReal2 = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                            shotBulletReal2.transform.position = transform.position * 1.07f;
                            shotBulletReal2.transform.rotation = Quaternion.Euler(0, 0, 0);
                            shotBulletReal2.SetActive(true);
                        }
                    }
                }

                fire = true;
                shotShound.Play();
                shotEffect.Play(); //creates energy shot effect for current ship
                                   //this one is invoking this method again after randome time which is set for Dstr3 class, so it is recursion of this method
                if (!IsInvoking("attackPointing") && !isDestroying) Invoke("attackPointing", UnityEngine.Random.Range(ATTACK_TIME_FROM, ATTACK_TIME_FROM + 2));
            }

            else if (Lists.AllCPUShips.Count > 0 && !isParalized && !underPlayerControl && !isDestroying) //all conditions to stop attack processes with explanatory names
            {
                //---------- to make CPU to be able to recognize which ships are on opposite site
                int i = UnityEngine.Random.Range(0, Lists.AllCPUShips.Count); //this randome chouses any ship from list of white ships
                float accuracy = UnityEngine.Random.Range(ACCURE_RANGE, -ACCURE_RANGE); //this randome is for 3 class destroyers only, it determines the accuracy of the shot (higher - is less accurate)

                //so shot direction is subtraction of ships point vector from enemys point vector (with accuracy correction)
                shotDirection = Lists.AllCPUShips[i].transform.position + new Vector3(accuracy, 0, 0) - transform.position;

                // PARALIZER BULLETS EFFECTS
                //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                //if (!isParalizer) shotBulletReal = Instantiate(currentShipBullet, transform.position, Quaternion.Euler(0, 0, 0));

                if (!isParalizer)
                {
                    //if the ship has such feature it will shot with double bullets so additional bullet is created and impulse added to it later in fixed update
                    if (doubleBullet)
                    {
                        shotBulletReal2 = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                        shotBulletReal2.transform.position = transform.position * 1.07f;
                        shotBulletReal2.transform.rotation = Quaternion.Euler(0, 0, 0);
                        shotBulletReal2.SetActive(true);
                    }
                    //that has only one type of ship cruis 1 class (so the feature is only on non paralizer and only on 1 class block)
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
                        paralizerBullet = false;
                        paralizerBullet = true;
                        //if the ship has such feature it will shot with double bullets so additional bullet is created and impulse added to it later in fixed update
                        if (doubleBullet)
                        {
                            shotBulletReal2 = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                            shotBulletReal2.transform.position = transform.position * 1.07f;
                            shotBulletReal2.transform.rotation = Quaternion.Euler(0, 0, 0);
                            shotBulletReal2.SetActive(true);
                        }
                    }
                }

                fire = true;
                shotShound.Play();
                shotEffect.Play(); //creates energy shot effect for current ship
                                   //this one is invoking this method again after randome time which is set for Dstr3 class, so it is recursion of this method
                if (!IsInvoking("attackPointing") && !isDestroying) Invoke("attackPointing", UnityEngine.Random.Range(ATTACK_TIME_FROM, ATTACK_TIME_FROM + 2));
            }

        }

        //this is attack prioryty set for 2 class cruisers and destroyers, so thay have 2 level priority system, first they destroy 1 class ships next other calsses
        else if (Class2) {
            if (Lists.CPUShips1Class.Count > 0 && !isParalized && !underPlayerControl && !isDestroying) //all conditions to stop attack processes with explanatory names
            {
                //---------- to make CPU to be able to recognize which ships are on opposite site
                int i = UnityEngine.Random.Range(0, Lists.CPUShips1Class.Count); //this randome chouses any ship from list of white ships
                float accuracy = UnityEngine.Random.Range(ACCURE_RANGE, -ACCURE_RANGE); //this randome is for 3 class destroyers only, it determines the accuracy of the shot (higher - is less accurate)

                //so shot direction is subtraction of ships point vector from enemys point vector (with accuracy correction)
                shotDirection = Lists.CPUShips1Class[i].transform.position + new Vector3(accuracy, 0, 0) - transform.position;

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
                        shotBulletReal2.transform.position = transform.position * 1.07f;
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
                        paralizerBullet = false;
                        paralizerBullet = true;
                    }
                }

                fire = true;
                shotShound.Play();
                shotEffect.Play(); //creates energy shot effect for current ship
                                   //this one is invoking this method again after randome time which is set for Dstr3 class, so it is recursion of this method
                if (!IsInvoking("attackPointing") && !isDestroying) Invoke("attackPointing", UnityEngine.Random.Range(ATTACK_TIME_FROM, ATTACK_TIME_FROM + 2));
            }

            else if (Lists.AllCPUShips.Count > 0 && !isParalized && !underPlayerControl && !isDestroying) //all conditions to stop attack processes with explanatory names
            {
                //---------- to make CPU to be able to recognize which ships are on opposite site
                int i = UnityEngine.Random.Range(0, Lists.AllCPUShips.Count); //this randome chouses any ship from list of white ships
                float accuracy = UnityEngine.Random.Range(ACCURE_RANGE, -ACCURE_RANGE); //this randome is for 3 class destroyers only, it determines the accuracy of the shot (higher - is less accurate)

                //so shot direction is subtraction of ships point vector from enemys point vector (with accuracy correction)
                shotDirection = Lists.AllCPUShips[i].transform.position + new Vector3(accuracy, 0, 0) - transform.position;

                // PARALIZER BULLETS EFFECTS
                //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                //if (!isParalizer) shotBulletReal = Instantiate(currentShipBullet, transform.position, Quaternion.Euler(0, 0, 0));

                if (!isParalizer)
                {
                    shotBulletReal = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                    shotBulletReal.transform.position = transform.position;
                    shotBulletReal.transform.rotation = Quaternion.Euler(0, 0, 0);
                    shotBulletReal.SetActive(true);
                    //if the ship has such feature it will shot with double bullets so additional bullet is created and impulse added to it later in fixed update
                    //here it is not pu into paralizer sequence cause only cruiser 2 class will have double bullets and it is not paralizer
                    if (doubleBullet)
                    {
                        shotBulletReal2 = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                        shotBulletReal2.transform.position = transform.position * 1.07f;
                        shotBulletReal2.transform.rotation = Quaternion.Euler(0, 0, 0);
                        shotBulletReal2.SetActive(true);
                    }
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
                        paralizerBullet = false;
                        paralizerBullet = true;
                    }
                }

                fire = true;
                shotShound.Play();
                shotEffect.Play(); //creates energy shot effect for current ship
                                   //this one is invoking this method again after randome time which is set for Dstr3 class, so it is recursion of this method
                if (!IsInvoking("attackPointing") && !isDestroying) Invoke("attackPointing", UnityEngine.Random.Range(ATTACK_TIME_FROM, ATTACK_TIME_FROM + 2));
            }
        }

        //this is attack prioryty set for lower class cruisers and destroyers, so thay have 1 level priority system, they destroy all calsses without sequences
        else
        {
            //it works only if there is at least one opposite side ship on battlefield
            if (Lists.AllCPUShips.Count > 0 && !isParalized && !underPlayerControl && !isDestroying) //all conditions to stop attack processes with explanatory names
            {
                //---------- to make CPU to be able to recognize which ships are on opposite site
                int i = UnityEngine.Random.Range(0, Lists.AllCPUShips.Count); //this randome chouses any ship from list of white ships
                float accuracy = UnityEngine.Random.Range(ACCURE_RANGE, -ACCURE_RANGE); //this randome is for 3 class destroyers only, it determines the accuracy of the shot (higher - is less accurate)

                //so shot direction is subtraction of ships point vector from enemys point vector (with accuracy correction)
                shotDirection = Lists.AllCPUShips[i].transform.position + new Vector3(accuracy, 0, 0) - transform.position;

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
                        paralizerBullet = false;
                        paralizerBullet = true;
                    }
                }

                fire = true;
                shotShound.Play();
                shotEffect.Play(); //creates energy shot effect for current ship
                                   //this one is invoking this method again after randome time which is set for Dstr3 class, so it is recursion of this method
                if (!IsInvoking("attackPointing") && !isDestroying) Invoke("attackPointing", UnityEngine.Random.Range(ATTACK_TIME_FROM, ATTACK_TIME_FROM + 2));
            }
        }
    }

    //on destroy the ship is removed from all lists DTTTTTTTTTTTTTTTTTTTTTTOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO DDDDDDDDDDDDDDDDEEEEEEEEEELLLLLLLLLLLLLLEEEEEEEEEEEEEETTTTTTTTT
    private void OnDestroy()
    {
        Lists.AllPlayerShipsWithoutGuns.Remove(gameObject);
        Lists.AllPlayerShips.Remove(gameObject);
        Lists.PlayerShips2Class.Remove(gameObject);
        Lists.PlayerShips1Class.Remove(gameObject);

        //Lists.PlayerCruisers.Remove(gameObject); ----------------------------------------------------------------------------------------
        //Lists.PlayerCruisers1.Remove(gameObject); ----------------------------------------------------------------------------------------
        //Lists.PlayerCruisers2.Remove(gameObject); ----------------------------------------------------------------------------------------
        //Lists.PlayerCruisers3.Remove(gameObject); ----------------------------------------------------------------------------------------
        //Lists.PlayerCruisers4.Remove(gameObject); ----------------------------------------------------------------------------------------
        //Lists.PlayerDestroyers.Remove(gameObject); ----------------------------------------------------------------------------------------
        //Lists.PlayerDestroyers1.Remove(gameObject); ----------------------------------------------------------------------------------------
        //Lists.PlayerDestroyers2.Remove(gameObject); ----------------------------------------------------------------------------------------
        //Lists.PlayerDestroyers3.Remove(gameObject); ----------------------------------------------------------------------------------------
        //Lists.PlayerDestroyers4.Remove(gameObject); ----------------------------------------------------------------------------------------
        Lists.PlayerShip.Remove(gameObject);
        //Destroy(shipParaliser);
        //if (/*powerShieldLocal*/shieldIsActive)
        //{
        //    powerShieldLocal.SetActive(false);
        //    shieldIsActive = false;
        //    CancelInvoke("putOffTheShield");
        //}//Destroy(powerShieldLocal);
    }

    //this method is for call from the megaParal2Method of CPU cruiser2, that method call for local routine, it is constructed so cause in case of call of routine from outside in case of destroying the cruiser 2
    //this routine never stops it's effect
    public void callForMegaParalFromOutside() {
        StartCoroutine(megaParal());
    }

    // for external invocation with mega paralizing effect of criuser 2 class 
    public IEnumerator megaParal()
    {
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
        if (IsInvoking("putShieldOnMethod")) CancelInvoke("putShieldOnMethod");
        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
        //shipParaliser = Instantiate(shipParaliserReal, transform.position, Quaternion.identity);

        shipParaliser = ObjectPuller.current.GetUniversalBullet(paralEffectListToActivate);
        shipParaliser.transform.position = transform.position;
        shipParaliser.transform.rotation = Quaternion.identity;
        shipParaliser.SetActive(true);


        destr_Maneuvering = false;
        isParalized = true;
        //tha one turns on paraliser from player joystick
        if (underPlayerControl)
        {
            PlayersJoystick.setParalized(isParalized);
        }
        yield return new WaitForSeconds(7);
        isParalized = false;
        if (hasManeuverFeature) destr_Maneuvering = true;
        //tha one turns off paraliser from player joystick
        if (underPlayerControl)
        {
            PlayersJoystick.setParalized(isParalized);
        }
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
        if (underPlayerControl)
        {
            PlayersJoystick.setParalized(isParalized);
        }
        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
        //Destroy(shipParaliser);
        shipParaliser.SetActive(false);
        attackPointing();
        if (withShield) putShieldOnMethod();
    }

    //this one detects the trigger of bullet, destroys it, and reduse the HP of ship
    private void OnTriggerEnter(Collider other)
    {
        //if ship is not on destroying process this condition stops any processes of ship
        if (!isDestroying)
        {
            //comparing is necessary to avoid destroying ships own bullet
            if (!other.gameObject.CompareTag("BullCruisPlay1") && !other.gameObject.CompareTag("BullDstrPlay1") && !other.gameObject.CompareTag("BullParalPlay1")
            && !other.gameObject.CompareTag("BullCruisPlay2") && !other.gameObject.CompareTag("BullDstrPlay2") && !other.gameObject.CompareTag("BullParalPlay2")
            && !other.gameObject.CompareTag("BullCruisPlay3") && !other.gameObject.CompareTag("BullDstrPlay3") && !other.gameObject.CompareTag("BullCruisPlay4")
            && !other.gameObject.CompareTag("BullDstrPlay4") && !other.gameObject.CompareTag("PowerShield") && !other.gameObject.CompareTag("GunBullPlay")
            && !other.gameObject.CompareTag("GunBullCPU"))
            {
                //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((
                //GameObject burst = Instantiate(other.GetComponent<CPUBullet>().getBurst(),other.transform.position,Quaternion.identity);

                other.GetComponent<CPUBullet>().getBurst(); //turns on pulling process on CPUBullet class

                //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((
                //Destroy(other.gameObject);
                other.gameObject.SetActive(false);

                //DESTROYERS BULLETS HARM
                if (other.gameObject.CompareTag("BullDstr4"))
                    SHIP_LIFE -= DESTR_4_HARM; //reduse the life for one HP
                else if (other.gameObject.CompareTag("BullDstr3"))
                    SHIP_LIFE -= DESTR_3_HARM; //reduse the life for two HP
                else if (other.gameObject.CompareTag("BullDstr2"))
                    SHIP_LIFE -= DESTR_2_HARM; //reduse the life for two HP
                else if (other.gameObject.CompareTag("BullDstr1"))
                    SHIP_LIFE -= DESTR_1_HARM; //reduse the life for two HP

                //CRUISERS BULLETS HARM
                else if (other.gameObject.CompareTag("BullCruis4"))
                    SHIP_LIFE -= CRUIS_4_HARM; //reduse the life for two HP
                else if (other.gameObject.CompareTag("BullCruis3"))
                    SHIP_LIFE -= CRUIS_3_HARM; //reduse the life for two HP
                else if (other.gameObject.CompareTag("BullCruis2"))
                    SHIP_LIFE -= CRUIS_2_HARM; //reduse the life for two HP
                else if (other.gameObject.CompareTag("BullCruis1"))
                    SHIP_LIFE -= CRUIS_1_HARM; //reduse the life for two HP

                //PARALIZER BULLETS PROCESSING, fro two types of paralizer bullets
                else if (other.gameObject.CompareTag("BullParal2"))
                {
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
                        if (underPlayerControl)
                        {
                            //LeftMove.setParalized(isParalized); TO DELETE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                            //RightMove.setParalized(isParalized); TO DELETE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                            PlayersJoystick.setParalized(isParalized);
                        }
                        StartCoroutine(paralizedTime(PARALIZED_TIME2));
                    }
                }
                else if (other.gameObject.CompareTag("BullParal1"))
                {
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
                        if (underPlayerControl)
                        {
                            //LeftMove.setParalized(isParalized); TO DELETE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                            //RightMove.setParalized(isParalized); TO DELETE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                            PlayersJoystick.setParalized(isParalized);
                        }
                        StartCoroutine(paralizedTime(PARALIZED_TIME1));
                    }
                }
            }
        }
    }

    private void Update()
    {
        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
        //to move power shield with ship itself
        if (/*powerShieldLocal*/shieldIsActive && destr_Maneuvering)
        {
            powerShieldLocal.transform.position = transform.position;
        }

        if (SHIP_LIFE <= 4 && !isDestroying && !burningEffect.activeInHierarchy)
        {
            burningEffect.SetActive(true); //starts burning effect of ship if its HP is less than 4

            //makes pre burst effect 
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

        if (SHIP_LIFE <= 0 && !isDestroying) //starts destrouing process of ship with 2 secs
        {

            isDestroying = true; //turns on destroying condition of ship to stop all ship activities in 2 secs while it is destroying

            Lists.AllPlayerShipsWithoutGuns.Remove(gameObject);
            Lists.AllPlayerShips.Remove(gameObject);

            Lists.PlayerShips2Class.Remove(gameObject);
            Lists.PlayerShips1Class.Remove(gameObject);

            //-------------------------------------------------------------------------------------------------------------------------------------
            //Lists.PlayerCruisers.Remove(gameObject);
            //Lists.PlayerCruisers1.Remove(gameObject);
            //Lists.PlayerCruisers2.Remove(gameObject);
            //Lists.PlayerCruisers3.Remove(gameObject);
            //Lists.PlayerCruisers4.Remove(gameObject);
            //Lists.PlayerDestroyers.Remove(gameObject);
            //Lists.PlayerDestroyers1.Remove(gameObject);
            //Lists.PlayerDestroyers2.Remove(gameObject);
            //Lists.PlayerDestroyers3.Remove(gameObject);
            //Lists.PlayerDestroyers4.Remove(gameObject);
            //-------------------------------------------------------------------------------------------------------------------------------------
            Lists.PlayerShip.Remove(gameObject);


            //triggers that CPU wins the battle depending on static lists count of ships on battle scene
            if (Lists.AllCPUShips.Count > 1 && Lists.AllPlayerShips.Count < 1) BattleLaunch.CPUWin = true;

            //counts of loses to process when get back to journey scene on panel of win lose (on JourneyController class)
            if (name.Contains(destroyer4Tag)) Lists.D4Lose++;
            else if(name.Contains(destroyer3Tag)) Lists.D3Lose++;
            else if (name.Contains(destroyer2Tag)) Lists.D2Lose++;
            else if (name.Contains(destroyer2TagPar)) Lists.D2PLose++;
            else if (name.Contains(destroyer1Tag)) Lists.D1Lose++;
            else if (name.Contains(destroyer1TagPar)) Lists.D1PLose++;
            else if (name.Contains(cruiser4Tag)) Lists.C4Lose++;
            else if (name.Contains(cruiser3Tag)) Lists.C3Lose++;
            else if (name.Contains(cruiser2Tag)) Lists.C2Lose++;
            else if (name.Contains(cruiser1Tag)) Lists.C1Lose++;

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
            pointerDetection.SetActive(false);
            burningEffect.SetActive(false); 
            GetComponent<Collider>().enabled = false; // turns off the collider of ship to prevent errors with on trigger enter functions of curren script
            //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
            //GameObject exploision = Instantiate(currentShipExploision, transform.position, Quaternion.identity); 
            GameObject exploision = ObjectPuller.current.GetUniversalBullet(ShipBurstEffectListToActivate);
            exploision.transform.position = transform.position;
            exploision.transform.rotation = Quaternion.identity;
            exploision.SetActive(true);
            Destroy(gameObject/*,2*/); //destroys the ship if life is less than or equlas to zero

            
            //foreach (GameObject go in shipParts)
            //    {
            //        go.GetComponent<TrailRenderer>().enabled = true;
            //        go.GetComponent<Rigidbody>().isKinematic = false;
            //        go.GetComponent<Rigidbody>().AddForce(new Vector3(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(-2, 2), UnityEngine.Random.Range(-2, 2))*2, ForceMode.Impulse);
            //        go.GetComponent<Rigidbody>().AddTorque(new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1)), ForceMode.Impulse);
            //    }
            //foreach (TrailRenderer tr in partsTrails)
            //{
            //    tr.enabled = true;
            //}

            //foreach (Rigidbody rb in partsRB)
            //{
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
                    //multiplication to delta time is necessary to make time dilation effect from player joystick
                    transform.Translate(new Vector3(manuvreSpeed, 0, 0) * Time.deltaTime, Space.World); 
                    if (transform.rotation.z * 100 > -15f) transform.Rotate(3f, 0, 0);
                    //shipRB.velocity = new Vector3(manuvreSpeed, 0, 0);
                    //if (shipRB.rotation.z * 100 > -15f) transform.Rotate(3f, 0, 0);

                }
                if (!moveRight)
                {
                    //multiplication to delta time is necessary to make time dilation effect from player joystick
                    transform.Translate(new Vector3(-manuvreSpeed, 0, 0)*Time.deltaTime, Space.World);
                    if (transform.rotation.z * 100 < 15f) transform.Rotate(-3f, 0, 0);
                    //shipRB.velocity = new Vector3(-manuvreSpeed, 0, 0);
                    //if (shipRB.rotation.z * 100 < 15f) transform.Rotate(-3f, 0, 0);
                }
            }
            //if ship is not maneuvering (means it is paralised currently) this stops all movements and rotations of ship
            else
            {
                //shipRB.velocity = new Vector3(0, 0, 0);
                transform.Rotate(0, 0, 0);
            }

        }


    }
    private void FixedUpdate()
    {
        if (fire && !underPlayerControl)
        {
            //this is necessaty to set velocity of bullet zero to avoid doubling the bullet speed each time it is activating from pull
            shotBulletReal.GetComponent<Rigidbody>().velocity = Vector3.zero;
            shotBulletReal.GetComponent<Rigidbody>().AddForce(shotDirection * SHIP_ATTACK_FORCE, ForceMode.Impulse);
            if (doubleBullet)
            {
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
        //            transform.Translate(new Vector3(0.1f,0, 0) * Time.deltaTime, Space.World);
        //            if (transform.rotation.z * 100 > -15f) transform.Rotate(3f, 0, 0);
        //            //shipRB.velocity = new Vector3(manuvreSpeed, 0, 0);
        //            //if (shipRB.rotation.z * 100 > -15f) transform.Rotate(3f, 0, 0);

        //        }
        //        if (!moveRight)
        //        {
        //            transform.Translate(new Vector3(-0.1f, 0, 0) * Time.deltaTime, Space.World);
        //            if (transform.rotation.z * 100 < 15f) transform.Rotate(-3f, 0, 0);
        //            //shipRB.velocity = new Vector3(-manuvreSpeed, 0, 0);
        //            //if (shipRB.rotation.z * 100 < 15f) transform.Rotate(-3f, 0, 0);
        //        }
        //    }
        //    //if ship is not maneuvering (means it is paralised currently) this stops all movements and rotations of ship
        //    else 
        //    {
        //        //shipRB.velocity = new Vector3(0, 0, 0);
        //        transform.Rotate(0, 0, 0);
        //    }

        //}

    }
}
