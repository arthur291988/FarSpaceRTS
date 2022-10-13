
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ShipsCtrlr : MonoBehaviour
{
    private static Transform playerGun;
    //public GameObject bullet;
    private GameObject shotBulletReal;

    //following properties are used with instantiations of bonuses
    //public GameObject HPPref;
    //public GameObject BulletPref;
    //public GameObject ShieldPref;
    private bool hasBonus = false;
    private bool BonusIsHP = false;
    private bool BonusIsBull = false;
    private bool BonusIsShield = false;


    //public GameObject burstEffect;
    private Vector3 dircetionToGun;
    private float zRotation;
    private float yRotation;

    private int SHIP_LIFE;
    //this var holds the ship life property of this ship that is assigned to it on start method and reassigns it each time the ship respawned from the onEnable method 
    //to prevent a bug on chain reaction af destroying the fighters right after thay are respawned from the pull
    private int STATIC_SHIP_LIFE; 
    private static int BULLET_HARM;
    private bool isDestroying = false;

    private float shipMoveSpeed;
    private float rotateManeuverSpeed;


    //Vector3 startPoint;
    Vector3 destinationPoint;
    Vector3 rotationManeuverCenter;
    private bool movingLeft;
    private bool rotationManeuverMovingLeft;
    private bool rotationManeuverMovingRight;
    private bool rotationManeuverUp;
    private bool corkscrewEffect; //special effect to make ship move in more effect way
    //private bool changeDstTrigger = false;

    //private ParticleSystem.MainModule PSmain; // is used to change the color of bullet shot effect according to its type

    //private bool pointingEffectIsOn = false; // is used to stop the update method of turning on the pointer/halo effect ufter first switching on

    private float ACCURE_RANGE;
    private float ATTACK_TIME_FROM;
    private float SHIP_ATTACK_FORCE;
    private Vector3 shotDirection;//are used to direct the bullet shot 

    //private bool fire;
    //shot sound
    private AudioSource shotShound;
    public ParticleSystem shotEffect;
    //public GameObject aimingPointer;

    public GameObject burningEffect;
    //public ParticleSystem currentShipExploision;
    //private AudioSource ShipExploisionSound;
    //public GameObject[] shipParts;
    //public List<Vector3> shipPartsStartPos = new List<Vector3>(); //is necessary to gather up all parts after ship blows and to make it ready to next launche from pull

    private static GameObject bullBurst;

    private string destroyer4Tag = "Destr4";
    private string destroyer3Tag = "Destr3";
    private string destroyer2Tag = "Destr2";
    private string destroyer1Tag = "Destr1";
    private string destroyerFedTag = "DestrFed";

    private static List<GameObject> localBulletBurstPull;
    private List<GameObject> shipBulletPullList;
    private List<GameObject> HPBonusPullList;
    private List<GameObject> ShieldBonusPullList;
    private List<GameObject> BulletsBonusPullList;
    private List<GameObject> ShipBurstPullList;


    //to paint the elements of ship to decent color
    [SerializeField]
    private List<GameObject> IDColorElements;

    //Vector3 busrtPos; //this point holds burst effect in same position after ship exploited and prevent it from moving with ship 

    //this method gets transform of players Gun (parent) on DefendScene (it is used on DefBarrelCtrlr)
    public static void setGun(Transform gun, int harm, List<GameObject> localBulletBurstPullTrans /*GameObject burstOfBull*/) {
        playerGun = gun;
        BULLET_HARM = harm;
        //bullBurst = burstOfBull;
        localBulletBurstPull = localBulletBurstPullTrans;
    }


    //Start is called before the first frame update
    void Start()
    {
        isDestroying = false;
        HPBonusPullList = ObjectPullerDefence.current.GetHPBonusPullList();
        ShieldBonusPullList = ObjectPullerDefence.current.GetShieldBonusPullList();
        BulletsBonusPullList = ObjectPullerDefence.current.GetBulletsBonusPullList();
        ShipBurstPullList = ObjectPullerDefence.current.GetShipBurstPullList();
        rotationManeuverMovingLeft = false;
        rotationManeuverMovingRight = false;
        corkscrewEffect = false;

        //probability of bonuses to appear, more than 4 is really high probability
        if (Random.Range(0, 15) > 8)
        {
            hasBonus = true;
            int i = Random.Range(0, 15);
            if (i <= 5) BonusIsShield = true;
            if (i > 5 && i < 9) BonusIsBull = true;
            if (i >= 9) BonusIsHP = true;
        }

        //ShipExploisionSound = currentShipExploision.GetComponent<AudioSource>();

        shotShound = GetComponent<AudioSource>();

        ////moves ships from left to right sides of scene and vice versa after they got the point in cycles
        //if (transform.position.x < 0)
        //{
        //    movingLeft = false; //movingLeft = true;
        //    destinationPoint = new Vector3(Random.Range(2000f, 2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f));
        //    setToGunDircetion(50);
        //}
        //else
        //{
        //    movingLeft = true; //movingLeft = false;
        //    destinationPoint = new Vector3(Random.Range(-2000f, -2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f));
        //    setToGunDircetion(-50);
        //}

        //fire = false;
        //isDestroying = false;
        //Invoke("setToGunDircetion",0.3f);

        //setting ship params according to its type taken from its name
        if (name.Contains(destroyer4Tag))
        {
            shipBulletPullList = ObjectPullerDefence.current.GetD4ShipBulletPullList();
            SHIP_LIFE = Constants.Instance.SHIP_HP_4_DEF;
            STATIC_SHIP_LIFE = SHIP_LIFE;
            ACCURE_RANGE = Constants.Instance.DESTR_4_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.DEF_DESTR_4_ATTACK_TIME;
            SHIP_ATTACK_FORCE = Constants.Instance.DESTR_4_DEF_ATTACK_FORCE;
            shipMoveSpeed = Constants.Instance.DESTR_4_DEF_MOVE_SPEED;
            rotateManeuverSpeed = shipMoveSpeed + 1.1f;
        }
        else if (name.Contains(destroyer3Tag))
        {
            shipBulletPullList = ObjectPullerDefence.current.GetD3ShipBulletPullList();
            SHIP_LIFE = Constants.Instance.SHIP_HP_3_DEF;
            STATIC_SHIP_LIFE = SHIP_LIFE;
            ACCURE_RANGE = Constants.Instance.DESTR_3_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.DEF_DESTR_3_ATTACK_TIME;
            SHIP_ATTACK_FORCE = Constants.Instance.DESTR_3_DEF_ATTACK_FORCE;
            shipMoveSpeed = Constants.Instance.DESTR_3_DEF_MOVE_SPEED;
            rotateManeuverSpeed = shipMoveSpeed + 1.1f;
        }
        else if (name.Contains(destroyer2Tag))
        {
            shipBulletPullList = ObjectPullerDefence.current.GetD2ShipBulletPullList();
            SHIP_LIFE = Constants.Instance.SHIP_HP_2_DEF;
            STATIC_SHIP_LIFE = SHIP_LIFE;
            ACCURE_RANGE = Constants.Instance.DESTR_2_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.DEF_DESTR_2_ATTACK_TIME;
            SHIP_ATTACK_FORCE = Constants.Instance.DESTR_2_DEF_ATTACK_FORCE;
            shipMoveSpeed = Constants.Instance.DESTR_2_DEF_MOVE_SPEED;
            rotateManeuverSpeed = shipMoveSpeed + 1.1f;
        }
        else if (name.Contains(destroyer1Tag))
        {

            shipBulletPullList = ObjectPullerDefence.current.GetD1ShipBulletPullList();
            SHIP_LIFE = Constants.Instance.SHIP_HP_1_DEF;
            STATIC_SHIP_LIFE = SHIP_LIFE;
            ACCURE_RANGE = Constants.Instance.DESTR_1_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.DEF_DESTR_1_ATTACK_TIME;
            SHIP_ATTACK_FORCE = Constants.Instance.DESTR_1_DEF_ATTACK_FORCE;
            shipMoveSpeed = Constants.Instance.DESTR_1_DEF_MOVE_SPEED;
            rotateManeuverSpeed = shipMoveSpeed + 1.1f;
        }
        else SHIP_LIFE = Constants.Instance.SHIP_HP_FED_DEF; 
        
        for (int i = 0; i < IDColorElements.Count; i++)
        {
            IDColorElements[i].GetComponent<MeshRenderer>().material.SetColor("_Color", Lists.colorOfOpposite);
        }
        //StartCoroutine(attackPointing(Random.Range(3, 10)));
        //Invoke("attackPointing", Random.Range(3, 10));
    }

    private void OnEnable()
    {
        //setting start state of fighter to ba able to process after enabling again after the previous destruction and putting to the pull
        isDestroying = false; 
        rotationManeuverMovingLeft = false;
        rotationManeuverMovingRight = false;
        corkscrewEffect = false;

        SHIP_LIFE=STATIC_SHIP_LIFE;

        //moves ships from left to right sides of scene and vice versa after they got the point in cycles
        if (transform.position.x < 0)
        {
            movingLeft = false; //movingLeft = true;
            destinationPoint = new Vector3(Random.Range(2000f, 2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f));
            setToGunDircetion(50);
        }
        else
        {
            movingLeft = true; //movingLeft = false;
            destinationPoint = new Vector3(Random.Range(-2000f, -2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f));
            setToGunDircetion(-50);
        }

        //probability of bonuses to appear, more than 4 is really high probability
        if (Random.Range(0, 15) > 8)
        {
            hasBonus = true;
            int i = Random.Range(0, 15);
            if (i <= 5) BonusIsShield = true;
            if (i > 5 && i < 9) BonusIsBull = true;
            if (i >= 9) BonusIsHP = true;
        }

        StartCoroutine(attackPointing(Random.Range(3, 10)));

        //    isDestroying = false;
        //    HPBonusPullList = ObjectPullerDefence.current.GetHPBonusPullList();
        //    ShieldBonusPullList = ObjectPullerDefence.current.GetShieldBonusPullList();
        //    BulletsBonusPullList = ObjectPullerDefence.current.GetBulletsBonusPullList();
        //    ShipBurstPullList = ObjectPullerDefence.current.GetShipBurstPullList();
        //    rotationManeuverMovingLeft = false;
        //    rotationManeuverMovingRight = false;
        //    corkscrewEffect = false;

        //    //probability of bonuses to appear, more than 4 is really high probability
        //    if (Random.Range(0, 15) > 4)
        //    {
        //        hasBonus = true;
        //        int i = Random.Range(0, 14);
        //        if (i <= 5) BonusIsShield = true;
        //        if (i > 5 && i < 9) BonusIsBull = true;
        //        if (i >= 9) BonusIsHP = true;
        //    }

        //    //ShipExploisionSound = currentShipExploision.GetComponent<AudioSource>();

        //    shotShound = GetComponent<AudioSource>();

        //    //moves ships from left to right sides of scene and vice versa after they got the point in cycles
        //    if (transform.position.x < 0)
        //    {
        //        movingLeft = false; //movingLeft = true;
        //        destinationPoint = new Vector3(Random.Range(2000f, 2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f));
        //        setToGunDircetion(50);
        //    }
        //    else
        //    {
        //        movingLeft = true; //movingLeft = false;
        //        destinationPoint = new Vector3(Random.Range(-2000f, -2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f));
        //        setToGunDircetion(-50);
        //    }

        //    //fire = false;
        //    isDestroying = false;
        //    //Invoke("setToGunDircetion",0.3f);

        //    //setting ship params according to its type taken from its name
        //    if (name.Contains(destroyer4Tag))
        //    {
        //        shipBulletPullList = ObjectPullerDefence.current.GetD4ShipBulletPullList();
        //        SHIP_LIFE = Lists.SHIP_HP_4_DEF;
        //        ACCURE_RANGE = Lists.DESTR_4_ACCUR_RANGE;
        //        ATTACK_TIME_FROM = Lists.DEF_DESTR_4_ATTACK_TIME;
        //        SHIP_ATTACK_FORCE = Lists.DESTR_4_DEF_ATTACK_FORCE;
        //        shipMoveSpeed = Lists.DESTR_4_DEF_MOVE_SPEED;
        //        rotateManeuverSpeed = shipMoveSpeed + 1.1f;
        //    }
        //    else if (name.Contains(destroyer3Tag))
        //    {
        //        shipBulletPullList = ObjectPullerDefence.current.GetD3ShipBulletPullList();
        //        SHIP_LIFE = Lists.SHIP_HP_3_DEF;
        //        ACCURE_RANGE = Lists.DESTR_3_ACCUR_RANGE;
        //        ATTACK_TIME_FROM = Lists.DEF_DESTR_3_ATTACK_TIME;
        //        SHIP_ATTACK_FORCE = Lists.DESTR_3_DEF_ATTACK_FORCE;
        //        shipMoveSpeed = Lists.DESTR_3_DEF_MOVE_SPEED;
        //        rotateManeuverSpeed = shipMoveSpeed + 1.1f;
        //    }
        //    else if (name.Contains(destroyer2Tag))
        //    {
        //        shipBulletPullList = ObjectPullerDefence.current.GetD2ShipBulletPullList();
        //        SHIP_LIFE = Lists.SHIP_HP_2_DEF;
        //        ACCURE_RANGE = Lists.DESTR_2_ACCUR_RANGE;
        //        ATTACK_TIME_FROM = Lists.DEF_DESTR_2_ATTACK_TIME;
        //        SHIP_ATTACK_FORCE = Lists.DESTR_2_DEF_ATTACK_FORCE;
        //        shipMoveSpeed = Lists.DESTR_2_DEF_MOVE_SPEED;
        //        rotateManeuverSpeed = shipMoveSpeed + 1.1f;
        //    }
        //    else if (name.Contains(destroyer1Tag))
        //    {

        //        shipBulletPullList = ObjectPullerDefence.current.GetD1ShipBulletPullList();
        //        SHIP_LIFE = Lists.SHIP_HP_1_DEF;
        //        ACCURE_RANGE = Lists.DESTR_1_ACCUR_RANGE;
        //        ATTACK_TIME_FROM = Lists.DEF_DESTR_1_ATTACK_TIME;
        //        SHIP_ATTACK_FORCE = Lists.DESTR_1_DEF_ATTACK_FORCE;
        //        shipMoveSpeed = Lists.DESTR_1_DEF_MOVE_SPEED;
        //        rotateManeuverSpeed = shipMoveSpeed + 1.1f;
        //    }
        //    else SHIP_LIFE = Lists.SHIP_HP_FED_DEF;

        //    StartCoroutine(attackPointing(Random.Range(3, 10)));
        //    //Invoke("attackPointing", Random.Range(3, 10));
    }

    //sets rotation of ship into look direction to player gun
    private void setToGunDircetion(float xRotation) {
        dircetionToGun = destinationPoint - transform.position;
        //dircetionToGun = playerGun.position - transform.position;
        zRotation = Quaternion.LookRotation(dircetionToGun, Vector3.up).eulerAngles.x;
        yRotation = Quaternion.LookRotation(dircetionToGun, Vector3.up).eulerAngles.y;
        gameObject.transform.rotation = Quaternion.Euler(xRotation, yRotation+90, zRotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDestroying)
        {
            //comparing is necessary to avoid destroying ships own bullet
            if (other.gameObject.CompareTag("GunBullPlay"))
            {

                //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                //GameObject burst = Instantiate(bullBurst, other.transform.position, Quaternion.identity);
                //Destroy(other.gameObject);

                GameObject burst = ObjectPullerDefence.current.GetUniversalBullet(localBulletBurstPull);
                burst.transform.position = other.transform.position;
                burst.transform.rotation = Quaternion.identity;
                burst.SetActive(true);


                other.GetComponent<TrailRenderer>().Clear();
                other.gameObject.SetActive(false);

                SHIP_LIFE -= BULLET_HARM; //reduse the life

            }
        }
    }

    private IEnumerator attackPointing(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        //it works only if there is at least one opposite side gun on battlefield
        if (!isDestroying )
        {
            //---------- to make CPU to be able to recognize which shipa are on opposite site
            float accuracy = Random.Range(ACCURE_RANGE, -ACCURE_RANGE); //this randome is for 3 class destroyers only, it determines the accuracy of the shot (higher - is less accurate)

            //this snippets of that method works only in case it there is a gunon scene, it is necessary to preven fighters from stopping to shot while player changes its 
            //guns
            if (Lists.DefendGun.Count > 0)
            {
                //so shot direction is subtraction of ships point vector from enemys point vector (with accuracy correction)
                shotDirection = playerGun.position + new Vector3(accuracy, 0, 0) - transform.position;

                //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                // initialising CPU destroyer bullet
                //shotBulletReal = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 0));

                shotBulletReal = ObjectPullerDefence.current.GetUniversalBullet(shipBulletPullList);
                shotBulletReal.transform.position = transform.position;
                shotBulletReal.transform.rotation = Quaternion.Euler(0, 0, 0);
                shotBulletReal.SetActive(true);

                shotBulletReal.GetComponent<Rigidbody>().velocity = Vector3.zero; //setting imoulse of bullet zero to prevent dobling it's impulse
                shotBulletReal.GetComponent<Rigidbody>().AddForce(shotDirection * SHIP_ATTACK_FORCE, ForceMode.Impulse);
                shotShound.Play();
                shotEffect.Play();
            }

            //this one is invocing this method again after randome time which is set for Dstr3 class, so it is recursion of this method
            StopCoroutine(attackPointing(0));
            StartCoroutine(attackPointing(Random.Range(ATTACK_TIME_FROM, ATTACK_TIME_FROM + 2)));

            //if (!IsInvoking("attackPointing") && !isDestroying) Invoke("attackPointing", Random.Range(ATTACK_TIME_FROM, ATTACK_TIME_FROM +2));
        }
    }


    private void FixedUpdate()
    {

        if (rotationManeuverMovingLeft)
        {
            //different movements (upward or downward) depending on where is the center of rotating around point 
            if (rotationManeuverUp) transform.RotateAround(rotationManeuverCenter, Vector3.back, rotateManeuverSpeed);
            else transform.RotateAround(rotationManeuverCenter, Vector3.forward, rotateManeuverSpeed);
            transform.Rotate(10, 0, 0); // this one gives a ship corkscrewEffect while making this Maneuver
        }
        else if (rotationManeuverMovingRight) {
            //different movements (upward or downward) depending on where is the center of rotating around point 
            if (rotationManeuverUp) transform.RotateAround(rotationManeuverCenter, Vector3.forward, rotateManeuverSpeed);
            else transform.RotateAround(rotationManeuverCenter, Vector3.back, rotateManeuverSpeed);
            transform.Rotate(10, 0, 0); // this one gives a ship corkscrewEffect while making this Maneuver
        }
        else
        {
            //multiplying to 1.8 is necessery to make ship get its destination point faster cause Lerp reducing the speed of ship while it getting close to destination point
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(destinationPoint.x * 2/*1.8f*/, destinationPoint.y, destinationPoint.z), shipMoveSpeed * Time.deltaTime);

            //if (corkscrewEffect) transform.Rotate(5, 0, 0);
        }

    }

    //this coroutine is used to give a sygnal to player of another fighter destroy
    private IEnumerator FightersSygnalsToPLyaer()
    {
        LaunchManager.Instance.fightersImg.color = new Color(1, 0, 0, 1);
        
        yield return new WaitForSeconds(0.5f);

        LaunchManager.Instance.fightersImg.color = new Color(0.5f, 0, 0, 0.7f);
    }

    //private void DisactivateCurrentShip() {
    //    for (int x = 0; x < shipParts.Length; x++)
    //    {
    //        shipParts[x].GetComponent<TrailRenderer>().enabled = false;
    //        shipParts[x].GetComponent<Rigidbody>().isKinematic = true;
    //        shipParts[x].GetComponent<Rigidbody>().velocity = Vector3.zero;
    //        shipParts[x].transform.localPosition = shipPartsStartPos[x];
    //    }
    //    gameObject.SetActive(false);
    //}

    // Update is called once per frame
    void Update()
    {
        //so if ship reached the point of one of the sides edges it will change it's moving direction
        if (movingLeft && gameObject.transform.position.x < destinationPoint.x && !isDestroying)
        {
            movingLeft = false;

            rotationManeuverMovingLeft = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if (destinationPoint.y < 0)
            {
                rotationManeuverCenter = new Vector3(transform.position.x, destinationPoint.y + 400, transform.position.z);
                rotationManeuverUp = true;
            }
            else
            {
                rotationManeuverCenter = new Vector3(transform.position.x, destinationPoint.y - 400, transform.position.z);
                rotationManeuverUp = false;
            }
            destinationPoint = new Vector3(Random.Range(2000f, 2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f));


            //TO SET BACK IS WILL NOT WORK
            /*
            movingLeft = false;
            destinationPoint = new Vector3(Random.Range(1500, 1800/*2000f, 2500f*//*), Random.Range(-500f, 500f), Random.Range(800f, 1300f));
            setToGunDircetion(50);*/
        }
        if (!movingLeft && gameObject.transform.position.x >  destinationPoint.x && !isDestroying)
        {
            movingLeft = true;
            rotationManeuverMovingRight = true;
            transform.rotation = Quaternion.Euler(0, 180, 0);

            //setting the point of rotation arount and make a sygnal where will move the ship up or down to set a correct vector of rotating around
            if (destinationPoint.y < 0)
            {
                rotationManeuverCenter = new Vector3(transform.position.x, destinationPoint.y + 400, transform.position.z);
                rotationManeuverUp = true;
            }
            //setting the point of rotation arount and make a sygnal where will move the ship up or down to set a correct vector of rotating around
            else
            {
                rotationManeuverCenter = new Vector3(transform.position.x, destinationPoint.y - 400, transform.position.z);
                rotationManeuverUp = false;
            }

            //movingLeft = true;
            destinationPoint = new Vector3(Random.Range(-2000f, -2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f));


            //TO SET BACK IS WILL NOT WORK
            /*
            movingLeft = true;
            destinationPoint = new Vector3(Random.Range(-1500, -1800/*-2000f, -2500f*//*), Random.Range(-500f, 500f), Random.Range(800f, 1300f));
            setToGunDircetion(-50);*/
        }

        //stop the rotation maneuver to change the direction of ship to moving towards destination point after the maneuver
        if (rotationManeuverMovingLeft && gameObject.transform.position.x > rotationManeuverCenter.x && !isDestroying)
        {
            rotationManeuverMovingLeft = false;
            setToGunDircetion(50);
        }
        if (rotationManeuverMovingRight && gameObject.transform.position.x < rotationManeuverCenter.x && !isDestroying)
        {
            rotationManeuverMovingRight = false;
            setToGunDircetion(-50);
        }

        if (SHIP_LIFE <= 7 && !isDestroying && !burningEffect.activeInHierarchy) burningEffect.SetActive(true);

        if (SHIP_LIFE <= 0 && !isDestroying)
        {
            isDestroying = true;

            //********************************************************************************************
            //Lists.DefCPUShips.Remove(gameObject);

            if (hasBonus)
            {
                if (BonusIsHP)
                {
                    //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((
                    //GameObject go = Instantiate(HPPref, transform.position, Quaternion.identity);
                    GameObject go = ObjectPullerDefence.current.GetUniversalBullet(HPBonusPullList);
                    go.transform.position = transform.position;
                    go.transform.rotation = Quaternion.identity;
                    go.SetActive(true);
                }
                else if (BonusIsBull)
                {
                    //GameObject go = Instantiate(BulletPref, transform.position, Quaternion.identity);
                    GameObject go = ObjectPullerDefence.current.GetUniversalBullet(BulletsBonusPullList);
                    go.transform.position = transform.position;
                    go.transform.rotation = Quaternion.identity;
                    go.SetActive(true);
                }
                else if (BonusIsShield)
                {
                    //GameObject go = Instantiate(ShieldPref, transform.position, Quaternion.identity);
                    GameObject go = ObjectPullerDefence.current.GetUniversalBullet(ShieldBonusPullList);
                    go.transform.position = transform.position;
                    go.transform.rotation = Quaternion.identity;
                    go.SetActive(true);
                }
            }
            burningEffect.SetActive(false);
            //GetComponent<Collider>().enabled = false; // turns off the collider of ship to prevent errors with on trigger enter functions of curren script
            //aimingPointer.SetActive(false);
            //ShipExploisionSound.Play();
            //busrtPos = currentShipExploision.transform.position; //this point holds burst effect in same position after ship exploited and prevent it from moving with ship 
            //currentShipExploision.Play();

            //launching objects right before the decreasing cause other way the last fighter will be deacreased without launching
            //condition checks if CPU still has count of fighters
            if ((Lists.FightersCPU - LaunchManager.y) > 0) LaunchManager.launcheAFighter = true;

            //decrease the count of fighters each time when curren fighter us destroyed
            if (Lists.FightersCPU > 0)
            {
                Lists.FightersCPU--;
                StartCoroutine(FightersSygnalsToPLyaer());
            }

            //clearing trails of fighters to prevent that wierd effect of teleportation
            foreach (TrailRenderer tr in GetComponentsInChildren<TrailRenderer>())
            {
                tr.Clear();
            }
            

            //triggers the bool on launcheManager script to start a process of nex stage if there are no more fighters on scene
            if (Lists.FightersCPU == 0) LaunchManager.noMoreFighters = true;

            //Invoke("DisactivateCurrentShip", 2f);
            //Destroy(gameObject, 2); //destroys the ship if life is less than or equlas to zero
            gameObject.SetActive(false);

            GameObject bgo = ObjectPullerDefence.current.GetUniversalBullet(ShipBurstPullList);
            bgo.transform.position = transform.position;
            bgo.transform.rotation = Quaternion.Euler(Random.Range (35f,80f),0,0);
            bgo.SetActive(true);

            //foreach (GameObject go in shipParts)
            //{
            //    go.GetComponent<TrailRenderer>().enabled = true;
            //    go.GetComponent<Rigidbody>().isKinematic = false;
            //    go.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-2f, 2), Random.Range(-2, 2), Random.Range(-2, 2)) * 20, ForceMode.Impulse);
            //    go.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-2f, 2f), Random.Range(-2, 2), Random.Range(-2, 2)), ForceMode.Impulse);
            //}

        }

        //if (isDestroying) currentShipExploision.transform.position = busrtPos; //holds burst effect in same position after ship exploited and prevent it from moving with ship 

    }
}
