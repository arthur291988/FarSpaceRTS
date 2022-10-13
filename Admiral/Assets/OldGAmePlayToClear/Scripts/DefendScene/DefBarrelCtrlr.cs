
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class DefBarrelCtrlr : MonoBehaviour
{

    public GameObject aimSprite;

    //point of spawn of gun bullets
    public Transform spawnPoint3;
    public Transform spawnPoint2;
    public Transform spawnPoint;

    private bool isDestroying;

    //two parts taking part in gun reload rpocess
    public GameObject reloadPart;
    public GameObject batch;

    public GameObject burningEffect;
    public ParticleSystem gunExploision;
    private AudioSource exploisionSound;
    public GameObject[] gunObjects;
    public GameObject gunBarrelParent; //is used to destroy hole gun in case of destruction of barrel

    private int GUN_HP;

    public GameObject aimingPos;  //empty game object that stays 5 units far from gun on z axis and helps to direct bullets twoard aiming direction (transferred to aiming script)
    //public GameObject bullet;

    //public GameObject shotEffect;

    //public GameObject aimingFarPoint; //empty game object that stays 50 units far from gun on z axis and helps to rotate gun toward it (transferred to aiming script)

    //public GameObject bullBurst;

    //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
    private ParticleSystem.MainModule PSmain; // is used to change the color of gun shot effect according to its type
    //private List<ParticleSystem.MainModule>  PSmain;

    private int GUN_HARM;

    private static Slider HPslider;
    private static Image HPbkgr;
    private static Image HPfill;
    private static Image HPhandle;

    private bool isSetYellow = false; //turns off painting yellow function of after it is yellow
    private bool isSetRed = false; //turns off painting red function of after it is red
    private int bonusHPValue;
    private const int bonusHPValue1 = 10;
    private const int bonusHPValue2 = 15;
    private const int bonusHPValue3 = 19;
    public static bool bonusHP = false;

    public static bool bonusHPAfterRewarded = false;

    private Animator anim; // shot animation of gun

    //harm levels of bullet hits
    private int DESTR_4_HARM;
    private int DESTR_3_HARM;
    private int DESTR_2_HARM;
    private int DESTR_1_HARM;

    //this lists are necessary to assign proper object and pull that object from ObjectPullerDefence class
    private List<GameObject> GunBulletListToActivate;
    private List<GameObject> GunBulletBurstListToActivate;

    private AudioSource bonusGatherSound;

    private void Start()
    {
        //PSmain = new List<ParticleSystem.MainModule>();

        exploisionSound = gunExploision.GetComponent<AudioSource>();

        DESTR_4_HARM = Constants.Instance.DESTR_4_BULLET_HARM+1; //decided to increase the harm of destrs on defence scene since the task is really easy
        DESTR_3_HARM = Constants.Instance.DESTR_3_BULLET_HARM+1;
        DESTR_2_HARM = Constants.Instance.DESTR_2_BULLET_HARM+1;
        DESTR_1_HARM = Constants.Instance.DESTR_1_BULLET_HARM+1;

        Lists.DefendGun.Add(gameObject);

        GunBulletBurstListToActivate = ObjectPullerDefence.current.GetGunBullBurstPullList();
        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
        //PSmain = bullBurst.GetComponent<ParticleSystem>().main;
        PSmain = GunBulletBurstListToActivate[0].GetComponent<ParticleSystem>().main;

        if (name.Contains("Dual"))
        {
            PSmain.startColor = new Color(1, 0.314429f, 0, 1);
            GUN_HARM = Constants.Instance.GUN_HARM_DUAL;
            GUN_HP = Constants.Instance.GUN_HP_DUAL;
            bonusHPValue = bonusHPValue2;
            GunBulletListToActivate = ObjectPullerDefence.current.GetG2BullList();
        }
        else if (name.Contains("Triple"))
        {
            GUN_HARM = Constants.Instance.GUN_HARM_TRIPLE;
            PSmain.startColor = Color.green;
            GUN_HP = Constants.Instance.GUN_HP_TRIPLE;
            bonusHPValue = bonusHPValue3;
            GunBulletListToActivate = ObjectPullerDefence.current.GetG3BullList();
        }
        else
        {
            PSmain.startColor = Color.cyan;
            GUN_HARM = Constants.Instance.GUN_HARM_SINGLE;
            GUN_HP = Constants.Instance.GUN_HP_SINGLE;
            bonusHPValue = bonusHPValue1;
            GunBulletListToActivate = ObjectPullerDefence.current.GetG1BullList();
        }

        bonusGatherSound = GetComponent<AudioSource>();

        Invoke("setMaxHP", 0.3f);

        //if ((double)Screen.width / Screen.height < 1.5) Camera.main.fieldOfView = 75; //for very narrow screen tablets

        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
        ShipsCtrlr.setGun(gameObject.transform.parent, GUN_HARM, GunBulletBurstListToActivate/*bullBurst*/);
        anim = this.GetComponent<Animator>();

        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
        GunShotButt.setGunProperties(gameObject, aimingPos, /*bullet*/GunBulletListToActivate, /*shotEffect,*/ batch, reloadPart, spawnPoint, spawnPoint2, spawnPoint3, anim);
        DefAimingCtrlr.setBarrelForAiming(gameObject/*, aimingFarPoint*/);

    }

    //gets the value of hp controlling slider from GunShotButt on its Start function
    public static void setSlider(Slider sldr, Image bkgr, Image fill, Image handle) {
        HPslider = sldr;
        HPbkgr = bkgr;
        HPfill = fill;
        HPhandle = handle;
    }

    //on this class start function that method is invoced after 0.3 secs to excape methods crossings in time
    private void setMaxHP() {
        HPslider.maxValue = GUN_HP;
        HPslider.value = GUN_HP;
        HPbkgr.color = new Color(0, 0.65f, 0, 0.3f);
        HPfill.color = new Color(0, 0.65f, 0, 1);
        HPhandle.color = new Color(0, 0.65f, 0, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDestroying)
        { if (!other.CompareTag("BullDstrPlay4")&& !other.CompareTag("Respawn")&&!other.CompareTag("PowerShield"))
            {
                other.GetComponent<DefShipBullet>().getBurst();

                //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                //GameObject burst = Instantiate(other.GetComponent<DefShipBullet>().getBurst(), other.transform.position, Quaternion.identity);
                other.gameObject.GetComponent<TrailRenderer>().Clear();
                other.gameObject.SetActive(false);
                //Destroy(other.gameObject);
            }

            //DESTROYERS BULLETS HARM
            if (other.gameObject.CompareTag("BullDstr4")) GUN_HP -= DESTR_4_HARM; //reduse the life for one HP
            else if (other.gameObject.CompareTag("BullDstr3")) GUN_HP -= DESTR_3_HARM; //reduse the life for two HP
            else if (other.gameObject.CompareTag("BullDstr2")) GUN_HP -= DESTR_2_HARM;
            else if (other.gameObject.CompareTag("BullDstr1")) GUN_HP -= DESTR_1_HARM;
            HPslider.value = GUN_HP;

            //paints the HP slider yellow and red depending on HP level
            if (GUN_HP <= 20&& !isSetYellow)
            {
                isSetYellow = true;
                HPbkgr.color = new Color(0.65f, 0.65f, 0.016f, 0.3f);
                HPfill.color = new Color(0.65f, 0.65f, 0.016f, 1f);
                HPhandle.color = new Color(0.65f, 0.65f, 0.016f, 1f);
            }
            if (GUN_HP < 8 &&!isSetRed)
            {
                isSetRed = true;
                HPbkgr.color = new Color(0.65f, 0, 0, 0.3f);
                HPfill.color = new Color(0.65f, 0, 0, 1);
                HPhandle.color = new Color(0.65f, 0, 0, 1);
            }
            //starting the UI effect thet gives a sygnal to player
            StartCoroutine(HPSygnalsToPLyaer());
        }
    }

    //this one is necessary to prevent the bug with painting the camera on wierd color and that the next gun option appears only after this one destroyed fully
    private void OnDestroy()
    {
        LaunchManager.gunIsdestroyed = true;
    }

    //this coroutine is used to give a sygnal to player of HP changes
    private IEnumerator HPSygnalsToPLyaer() {
        if (GUN_HP > 20)
        {
            HPbkgr.color = new Color(0, 1, 0, 0.3f);
            HPfill.color = new Color(0, 1, 0, 1);
            HPhandle.color = new Color(0, 1, 0, 1);
        }
        if (GUN_HP > 8 && GUN_HP <= 20)
        {
            HPbkgr.color = new Color(1, 0.65f, 0.016f, 0.3f);
            HPfill.color = new Color(1, 0.65f, 0.016f, 1f);
            HPhandle.color = new Color(1, 0.65f, 0.016f, 1f);
        }
        if (GUN_HP <= 8) {
            HPbkgr.color = new Color(1, 0, 0, 0.3f);
            HPfill.color = new Color(1, 0, 0, 1);
            HPhandle.color = new Color(1, 0, 0, 1);
        }
        yield return new WaitForSeconds(0.3f);
        if (GUN_HP > 20)
        {
            HPbkgr.color = new Color(0, 0.65f, 0, 0.3f);
            HPfill.color = new Color(0, 0.65f, 0, 1);
            HPhandle.color = new Color(0, 0.65f, 0, 1);
        }
        if (GUN_HP > 8 && GUN_HP <= 20)
        {
            HPbkgr.color = new Color(0.65f, 0.65f, 0.016f, 0.3f);
            HPfill.color = new Color(0.65f, 0.65f, 0.016f, 1f);
            HPhandle.color = new Color(0.65f, 0.65f, 0.016f, 1f);
        }
        if (GUN_HP <= 8)
        {
            HPbkgr.color = new Color(0.65f, 0, 0, 0.3f);
            HPfill.color = new Color(0.65f, 0, 0, 1);
            HPhandle.color = new Color(0.65f, 0, 0, 1);
        }
    }

    private void Update()
    {
        //trigger for getting a sygnal from bonus objects (bonusGather class) and adding bonus HP
        if (bonusHP) {
            GUN_HP += bonusHPValue;
            if (GUN_HP > HPslider.maxValue) GUN_HP = (int)HPslider.maxValue;
            HPslider.value = GUN_HP;
            //paints the HP slider yellow and red depending on HP level
            if (GUN_HP > 20)
            {
                isSetYellow = false;
                HPbkgr.color = new Color(0, 0.65f, 0, 0.3f);
                HPfill.color = new Color(0, 0.65f, 0, 1);
                HPhandle.color = new Color(0, 0.65f, 0, 1);
            }
            if (GUN_HP > 8 && GUN_HP <= 20)
            {
                isSetRed = false;
                HPbkgr.color = new Color(0.65f, 0.92f, 0.016f, 0.3f);
                HPfill.color = new Color(0.65f, 0.92f, 0.016f, 1f);
                HPhandle.color = new Color(0.65f, 0.92f, 0.016f, 1f);
            }
            bonusGatherSound.Play();
            bonusHP = false;
            //starting the UI effect thet gives a sygnal to player
            StartCoroutine(HPSygnalsToPLyaer());
        }

        //trigger for getting a sygnal from bonus achieved from whatching the rewarded video (LaunchManager class) and adding bonus HP 
        if (bonusHPAfterRewarded)
        {
            GUN_HP = (int)HPslider.maxValue;
            HPslider.value = GUN_HP;
            HPbkgr.color = new Color(0, 0.65f, 0, 0.3f);
            HPfill.color = new Color(0, 0.65f, 0, 1);
            HPhandle.color = new Color(0, 0.65f, 0, 1);

            bonusHPAfterRewarded = false;
            //starting the UI effect thet gives a sygnal to player
            StartCoroutine(HPSygnalsToPLyaer());
        }

        if (GUN_HP > 6 && !isDestroying && burningEffect.activeInHierarchy) burningEffect.SetActive(false); //starts burning effect of gun if its HP is less than 6
        if (GUN_HP <= 6 && !isDestroying && !burningEffect.activeInHierarchy) burningEffect.SetActive(true); //starts burning effect of gun if its HP is less than 6

        if (GUN_HP <= 0 && !isDestroying) //starts destroying process of gun with 2 secs
        {

            Lists.DefendGun.Remove(gameObject);
            isDestroying = true; //turns on destroying condition of ship to stop all ship activities in 2 secs while it is destroying


            //Lists.PlayerShip.Remove(gameObject);

            //if (shipParaliser) Destroy(shipParaliser);
            //if (powerShieldLocal) Destroy(powerShieldLocal);
            burningEffect.SetActive(false);
            //LaunchManager.gunIsdestroyed = true; //this one now is on destroy method
            GetComponent<Collider>().enabled = false; // turns off the collider of ship to prevent errors with on trigger enter functions of curren script

            gunExploision.Play();
            exploisionSound.Play();

            Destroy(gameObject, 2); //destroys the barrel if life is less than or equlas to zero
            Destroy(gunBarrelParent, 2); //destroys the whole gun if life is less than or equlas to zero

            foreach (GameObject go in gunObjects)
            {
                go.GetComponent<TrailRenderer>().enabled = true;
                go.GetComponent<Rigidbody>().isKinematic = false;
                go.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-2f, 2f), Random.Range(-2, 2), Random.Range(-2, 2)) * 2, ForceMode.Impulse);
                go.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-1f, 1f), Random.Range(-1, 1), Random.Range(-1, 1)), ForceMode.Impulse);
            }

        }
    }
}
