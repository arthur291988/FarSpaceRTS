using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class GunShotButt : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    //slider is used to show bullets quantity
    public Slider bulletsIndicator;
    private int singleBulletsMax = 100;
    private int dualBulletsMax = 200;
    private int tripleBulletsMax = 300;
    private int bonusBulletValue;
    private const int bonusBulletValue1 = 30;
    private const int bonusBulletValue2 = 50;
    private const int bonusBulletValue3 = 90;
    public static bool bonusBullet = false;

    //public Camera backCamera;

    //slider is used to show gun health (is transferred to DefBarrelCtrlr class)
    public Slider GunHealth;
    public Image HPbkgr;
    public Image HPFill;
    public Image HPHandle;

    //this one to simulate a signal to player on chenges of bullets batch after gathering bonus and reloading
    public Image BulletsBatchHandle;

    ////colors to set for dimention in cameras
    //private Color blackDimensionCol = new Color(0, 0.005f, 0.094f, 0);
    //private Color blueDimensionCol = new Color(0, 0.208f, 0.5f, 0);
    //private Color redDimensionCol = new Color(0.5568628f, 0.1294118f, 0.1404912f, 0);

    public Button shieldButton; //button that puts shield on
    public GameObject powerShield;
    public AudioSource powerShieldSound;
    private float shieldTime = 5f;
    public static bool bonusShield = false;

    //this gameobjects lists are galaxyes to place randomly on scene
    //public List<GameObject> GalaxyObjects = new List<GameObject>();
    //private ParticleSystem.MainModule main; //is used to change galaxy colors randomly on start
    //private GameObject galaxy1; //are used to store locally game objects for being able to change their particles color randomely
    //private GameObject galaxy2;

    private bool shootingStopped = false; //this one is necessery to stop shooting effect one time after gun is destroyed

    //material is used to give a sygnals for reload the gun 
    public Material sygnallingMat; //material is used to give a sygnal for reloading actions
    public Material batchStartMat; //start mat of bullets batch to assign it after reload is over
    public Material TripleReloadPartMat;
    public Material DualReloadPartMat;
    public Material SingleReloadPartMat;
    private float alfaOfMat; //float is used to give a flashing effect to batch and reload part material
    private bool alfaIsReducing = true; //is used on update method to reduce and increase alfa of sygnalling mat
    private static GameObject batchOfGun; // local batch of gun GO to store the transferred batch
    private static GameObject batchOfGunChildObject; // this child object of batch is used as reloaded batch of gun
    //private static Material batchOfGunMaterial; // local batch of gun GO material to store the transferred batch
    private static GameObject reloadDetail; // local reload part of gun GO to store the transferred reload part
    //private static Material reloadDetailMaterial; // local reload part of gun GO to store the transferred reload part
    private bool onReloadMode; //gives a sygnal to start a flashing mode 
    public static bool onTriggerMode; //turns on reload part flashing
    public static bool wholeReloadIsOver; //finishes all reload process by getting a sygnal from reload part script
    private bool startReloadTraking; //tha one is necessery to prevent start of reloading mode on start while gun batch is empty
    public Button reloadButt;
    private bool isDual = false;
    private bool isSingle = false;
    private bool isTriple = false;

    //set of static object to get from gun object on scene
    private static GameObject gunBarrel;
    private static Transform spawnPoint3;
    private static Transform spawnPoint2;
    private static Transform spawnPoint;
    private static GameObject aimingPos;
    //private static GameObject bullet;
    //private static GameObject shotEffect;
    private static Animator anim;

    private GameObject bulletReal;
    private GameObject bulletReal2;
    private GameObject bulletReal3;

    //are used to give a randome values to shot direction of barrels
    private const float RANDOME_MINUS = -0.03f;
    private const float RANDOME_PLUS = 0.03f;

    private Vector3 lookDirection;
    private static Vector3 gunPosition;

    Color thisButtonStartColor;

    private float repeatTime;

    AudioSource gunShotSound;
    public AudioSource gunReloadButtonSound;
    public AudioSource gunReloadOverSound;
    public AudioSource lackOfbullets;
    [SerializeField]
    private AudioClip gun1ShotSound;
    [SerializeField]
    private AudioClip gun2ShotSound;
    [SerializeField]
    private AudioClip gun3ShotSound;

    private static List<GameObject> localBulletPull;

    [SerializeField]
    private AudioSource bonusGatherSound;

    public static void setGunProperties(GameObject gun, GameObject aimPoint, /*GameObject bulletReal*/ List<GameObject> localBulletPullTrans /*GameObject shotEffectReal*/, GameObject batch, GameObject reloadPart, Transform spwnPoint1Loc, Transform spwnPoint2Loc, 
        Transform spwnPoint3Loc, Animator animator) 
    {
        gunBarrel = gun;
        aimingPos = aimPoint;

        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
        //bullet = bulletReal;
        localBulletPull = localBulletPullTrans;
        spawnPoint = spwnPoint1Loc;
        spawnPoint2 = spwnPoint2Loc;
        spawnPoint3 = spwnPoint3Loc;
        anim = animator;
        //shotEffect = shotEffectReal;
        batchOfGun = batch;
        //batchOfGunMaterial = batchOfGun.GetComponent<MeshRenderer>().material;
        reloadDetail = reloadPart;
        //reloadDetailMaterial = reloadDetail.GetComponent<MeshRenderer>().material;
        batchOfGunChildObject = batchOfGun.transform.GetChild(0).gameObject;
        //gunPosition = gunBarrel.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        ////instantiate two galaxys on battle sce from star
        //galaxy1 = Instantiate(GalaxyObjects[Random.Range(0, GalaxyObjects.Count)], new Vector3(Random.Range(-90, -1200),
        //           Random.Range(350, -530), 1400), Random.rotation);
        //galaxy1.transform.localScale = new Vector3(3,3,3); //makes galaxy bigger by changing its scale
        //galaxy2 = Instantiate(GalaxyObjects[Random.Range(0, GalaxyObjects.Count)], new Vector3(Random.Range(90, 1200),
        //           Random.Range(350, -530), 1400), Random.rotation);
        //galaxy2.transform.localScale = new Vector3(3, 3, 3);

        //main = galaxy1.GetComponent<ParticleSystem>().main;
        //main.startColor = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f, 1f, 1f); //generates randome color for galaxy
        //main = galaxy2.GetComponent<ParticleSystem>().main;
        //main.startColor = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f, 1f, 1f); //generates randome color for galaxy

        ////sets the colour of camera background according to current dimension, as well the background camera
        //if (Lists.isBlackDimension)
        //{
        //    Camera.main.backgroundColor = blackDimensionCol;
        //    backCamera.backgroundColor = blackDimensionCol;
        //}
        //if (Lists.isBlueDimension)
        //{
        //    Camera.main.backgroundColor = blueDimensionCol;
        //    backCamera.backgroundColor = blueDimensionCol;
        //}
        //if (Lists.isRedDimension)
        //{
        //    Camera.main.backgroundColor = redDimensionCol;
        //    backCamera.backgroundColor = redDimensionCol;
        //}

        gunShotSound = GetComponent<AudioSource>();

        DefBarrelCtrlr.setSlider(GunHealth, HPbkgr, HPFill, HPHandle); //transfers slider on scene to particular gun scrip (DefBarrelCtrlr class)

        startReloadTraking = false;
        onReloadMode = false;
        onTriggerMode = false;
        wholeReloadIsOver = false;

        Invoke("setPropertiesOfReceivedObj", 0.6f);
        Invoke("setBullets", 0.5f);

        thisButtonStartColor = GetComponent<RawImage>().color;
        //shooting = false;
        repeatTime = 0.1f; //////////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! here was the list
    }

    private void OnEnable()
    {
        DefBarrelCtrlr.setSlider(GunHealth, HPbkgr, HPFill, HPHandle); //transfers slider on scene to particular gun scrip (DefBarrelCtrlr class)

        startReloadTraking = false;
        onReloadMode = false;
        onTriggerMode = false;
        wholeReloadIsOver = false;

        BulletsBatchHandle.color = new Color(0.65f,0.65f,0,1);

        Invoke("setPropertiesOfReceivedObj", 0.6f);
        Invoke("setBullets", 0.5f);
    }

    //method that puts on power shield and starts coroutine to switch it off
    public void powerShieldOn() {
        shieldButton.interactable = false;
        powerShield.SetActive(true);
        powerShieldSound.Play();
        StartCoroutine(powerShieldOff(shieldTime));
    }

    IEnumerator powerShieldOff(float timer) {
        yield return new WaitForSeconds(timer); 
        powerShield.SetActive(false);
    }

    //put that method outside of start method to prevent of bug with missing reference, cause gun has no time to tranfer it GO for checking its name 
    //this method charges guns ith bullets accorting to their type
    //
    private void setBullets() {
        if (gunBarrel.name.Contains("Dual"))
        {
            bulletsIndicator.maxValue = dualBulletsMax;
            bonusBulletValue = bonusBulletValue2;
        }
        else if (gunBarrel.name.Contains("Triple"))
        {
            bulletsIndicator.maxValue = tripleBulletsMax; 
            bonusBulletValue = bonusBulletValue3;
        }
        else
        {
            bulletsIndicator.maxValue = singleBulletsMax;
            bonusBulletValue = bonusBulletValue1;
        }
        bulletsIndicator.value = bulletsIndicator.maxValue;
    }

    private void setPropertiesOfReceivedObj()
    {
        if (gunBarrel.name.Contains("Dual"))
        {
            isSingle = false;
            isTriple = false;
            isDual = true;
            shieldTime = 7f;
            gunShotSound.clip = gun2ShotSound;
        }
        else if (gunBarrel.name.Contains("Triple"))
        {
            isSingle = false;
            isDual = false;
            isTriple = true;
            shieldTime = 9f;
            gunShotSound.clip = gun3ShotSound;
        }
        else
        {
            isTriple = false;
            isDual = false;
            isSingle = true;
            gunShotSound.clip = gun1ShotSound;
        }
        startReloadTraking = true;
    }

    IEnumerator musicStopTime() {
        yield return new WaitForSeconds(0.2f);
        gunShotSound.Stop();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (bulletsIndicator.value > 0 && Lists.DefendGun.Count > 0)
        {
            StopCoroutine(musicStopTime());
            gunShotSound.Play();
            GetComponent<RawImage>().color = new Color(1, 0, 0, 0.3f);
            //shooting = true;
            //shotEffect.SetActive(true);
            anim.enabled = true;
            if (isTriple) AttackBulletsTriple();
            else if (isDual) AttackBulletsDual();
            else if (isSingle) AttackBulletsSingle();
        }
        else if (Lists.DefendGun.Count > 0) lackOfbullets.Play();
    }

    public void OnPointerUp(PointerEventData eventData)
    { if (Lists.DefendGun.Count > 0) {
            GetComponent<RawImage>().color = thisButtonStartColor;
            //shooting = false;
            //shotEffect.SetActive(false);
            anim.enabled = false;
            CancelInvoke();
            StartCoroutine(musicStopTime());
        }
    }

    //following three methods initiates shot effect 
    private void AttackBulletsTriple()
    {
        if (Lists.DefendGun.Count > 0)
        {
            lookDirection = aimingPos.transform.position - gunBarrel.transform.position;

            //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((

            //bulletReal = Instantiate(bullet, spawnPoint.position, Quaternion.identity);

            bulletReal = ObjectPullerDefence.current.GetUniversalBullet(localBulletPull);
            bulletReal.transform.position = spawnPoint.position;
            bulletReal.transform.rotation = Quaternion.identity;
            bulletReal.SetActive(true);

            bulletReal.GetComponent<Rigidbody>().velocity = Vector3.zero; //setting imoulse of bullet zero to prevent dobling it's impulse
            bulletReal.GetComponent<Rigidbody>().AddForce(new Vector3(lookDirection.x + Random.Range(RANDOME_MINUS, RANDOME_PLUS), lookDirection.y + Random.Range(RANDOME_MINUS, RANDOME_PLUS), lookDirection.z + Random.Range(RANDOME_MINUS, RANDOME_PLUS)) * /*100*/ 415, ForceMode.Impulse);
            bulletsIndicator.value--;

            //bulletReal2 = Instantiate(bullet, spawnPoint2.position, Quaternion.identity);
            bulletReal2 = ObjectPullerDefence.current.GetUniversalBullet(localBulletPull);
            bulletReal2.transform.position = spawnPoint2.position;
            bulletReal2.transform.rotation = Quaternion.identity;
            bulletReal2.SetActive(true);

            bulletReal2.GetComponent<Rigidbody>().velocity = Vector3.zero; //setting imoulse of bullet zero to prevent dobling it's impulse
            bulletReal2.GetComponent<Rigidbody>().AddForce(new Vector3(lookDirection.x + Random.Range(RANDOME_MINUS, RANDOME_PLUS), lookDirection.y + Random.Range(RANDOME_MINUS, RANDOME_PLUS), lookDirection.z + Random.Range(RANDOME_MINUS, RANDOME_PLUS)) * 420, ForceMode.Impulse);
            bulletsIndicator.value--;

            //bulletReal3 = Instantiate(bullet, spawnPoint3.position, Quaternion.identity);

            bulletReal3 = ObjectPullerDefence.current.GetUniversalBullet(localBulletPull);
            bulletReal3.transform.position = spawnPoint3.position;
            bulletReal3.transform.rotation = Quaternion.identity;
            bulletReal3.SetActive(true);

            bulletReal3.GetComponent<Rigidbody>().velocity = Vector3.zero; //setting imoulse of bullet zero to prevent dobling it's impulse
            bulletReal3.GetComponent<Rigidbody>().AddForce(new Vector3(lookDirection.x + Random.Range(RANDOME_MINUS, RANDOME_PLUS), lookDirection.y + Random.Range(RANDOME_MINUS, RANDOME_PLUS), lookDirection.z + Random.Range(RANDOME_MINUS, RANDOME_PLUS)) * 425, ForceMode.Impulse);
            bulletsIndicator.value--;

            if (bulletsIndicator.value > 0) Invoke("AttackBulletsTriple", repeatTime);
        }
    }

    private void AttackBulletsDual()
    {
        if (Lists.DefendGun.Count > 0)
        {
            lookDirection = aimingPos.transform.position - gunBarrel.transform.position;

            //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
            //bulletReal = Instantiate(bullet, spawnPoint.position, Quaternion.identity);
            //bulletReal2 = Instantiate(bullet, spawnPoint2.position, Quaternion.identity);

            bulletReal = ObjectPullerDefence.current.GetUniversalBullet(localBulletPull);
            bulletReal.transform.position = spawnPoint.position;
            bulletReal.transform.rotation = Quaternion.identity;
            bulletReal.SetActive(true);

            bulletReal.GetComponent<Rigidbody>().velocity = Vector3.zero; //setting imoulse of bullet zero to prevent dobling it's impulse
            bulletReal.GetComponent<Rigidbody>().AddForce(new Vector3(lookDirection.x + Random.Range(RANDOME_MINUS, RANDOME_PLUS), lookDirection.y + Random.Range(RANDOME_MINUS, RANDOME_PLUS), lookDirection.z + Random.Range(RANDOME_MINUS, RANDOME_PLUS)) * 415, ForceMode.Impulse);
            bulletsIndicator.value--;

            bulletReal2 = ObjectPullerDefence.current.GetUniversalBullet(localBulletPull);
            bulletReal2.transform.position = spawnPoint2.position;
            bulletReal2.transform.rotation = Quaternion.identity;
            bulletReal2.SetActive(true);

            bulletReal2.GetComponent<Rigidbody>().velocity = Vector3.zero; //setting imoulse of bullet zero to prevent dobling it's impulse
            bulletReal2.GetComponent<Rigidbody>().AddForce(new Vector3(lookDirection.x + Random.Range(RANDOME_MINUS, RANDOME_PLUS), lookDirection.y + Random.Range(RANDOME_MINUS, RANDOME_PLUS), lookDirection.z + Random.Range(RANDOME_MINUS, RANDOME_PLUS)) * 420, ForceMode.Impulse);
            bulletsIndicator.value--;
            if (bulletsIndicator.value > 0) Invoke("AttackBulletsDual", repeatTime);
        }
    }

    private void AttackBulletsSingle()
    {
        if (Lists.DefendGun.Count > 0)
        {
            lookDirection = aimingPos.transform.position - gunBarrel.transform.position;
            //bulletReal = Instantiate(bullet, spawnPoint.position, Quaternion.identity);

            bulletReal = ObjectPullerDefence.current.GetUniversalBullet(localBulletPull);
            bulletReal.transform.position = spawnPoint.position;
            bulletReal.transform.rotation = Quaternion.identity;
            bulletReal.SetActive(true);

            bulletReal.GetComponent<Rigidbody>().velocity = Vector3.zero; //setting imoulse of bullet zero to prevent dobling it's impulse

            bulletReal.GetComponent<Rigidbody>().AddForce(new Vector3(lookDirection.x + Random.Range(RANDOME_MINUS, RANDOME_PLUS), lookDirection.y + Random.Range(RANDOME_MINUS, RANDOME_PLUS), lookDirection.z + Random.Range(RANDOME_MINUS, RANDOME_PLUS)) * 425, ForceMode.Impulse);
            bulletsIndicator.value--;
            if (bulletsIndicator.value > 0) Invoke("AttackBulletsSingle", repeatTime);
        }
    }

    //this coroutine is used to give a sygnal to player of Bullets butch changes
    private IEnumerator BulletsBatchSygnalsToPLyaer()
    {
        BulletsBatchHandle.color = new Color(1, 0.65f, 0, 1);
        yield return new WaitForSeconds(0.6f);
        BulletsBatchHandle.color = new Color(0.65f, 0.65f, 0, 1);
    }

    private IEnumerator EnergyReduceSygnalsToPLyaer()
    {
        LaunchManager.Instance.EnergyImg.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.6f);
        LaunchManager.Instance.EnergyImg.color = new Color(0.65f, 0.65f,0.65f, 1);
    }

    //reload button processing function
    public void reloadButton() {
        if (isSingle)
        {
            if (Lists.energyOfPlayer >= 5)
            {
                gunReloadButtonSound.Play();
                reloadButt.enabled = false;
                reloadButt.image.color = new Color(0.65f, 0.5615385f, 0f, 0.13f);
                onTriggerMode = true;
                batchOfGun.GetComponent<MeshRenderer>().material = batchStartMat;
                reloadDetail.GetComponent<MeshRenderer>().material = sygnallingMat;
                //reducing the energy of player after each reload of gun
                Lists.energyOfPlayer -= 5;
                LaunchManager.Instance.EnergyCount.text = Lists.energyOfPlayer.ToString("0");
                StartCoroutine(EnergyReduceSygnalsToPLyaer());
            }
        }
        else if (isDual)
        {
            if (Lists.energyOfPlayer >= 7)
            {
                gunReloadButtonSound.Play();
                reloadButt.enabled = false;
                reloadButt.image.color = new Color(0.65f, 0.5615385f, 0f, 0.13f);
                onTriggerMode = true;
                batchOfGun.GetComponent<MeshRenderer>().material = batchStartMat;
                reloadDetail.GetComponent<MeshRenderer>().material = sygnallingMat;
                //reducing the energy of player after each reload of gun
                Lists.energyOfPlayer -= 7;
                LaunchManager.Instance.EnergyCount.text = Lists.energyOfPlayer.ToString("0");
                StartCoroutine(EnergyReduceSygnalsToPLyaer());
            }
        }
        else if (isTriple)
        {
            if (Lists.energyOfPlayer >= 9)
            {
                gunReloadButtonSound.Play();
                reloadButt.enabled = false;
                reloadButt.image.color = new Color(0.65f, 0.5615385f, 0f, 0.13f);
                onTriggerMode = true;
                batchOfGun.GetComponent<MeshRenderer>().material = batchStartMat;
                reloadDetail.GetComponent<MeshRenderer>().material = sygnallingMat;
                //reducing the energy of player after each reload of gun
                Lists.energyOfPlayer -= 9;
                LaunchManager.Instance.EnergyCount.text = Lists.energyOfPlayer.ToString("0");
                StartCoroutine(EnergyReduceSygnalsToPLyaer());
            }
        }
        reloadButt.image.material = null;
    }


    // Update is called once per frame
    void Update()
    {
        //*****************************************************************************************************************************************
        //shipsCount.text = Lists.DefCPUShips.Count.ToString();

        //trigger for getting a sygnal from bonus objects (bonusGather class) and adding bonus bullets
        if (bonusBullet)
        {
            bulletsIndicator.value += bonusBulletValue;
            bonusBullet = false;
            //giving a signal to player of changes on bullets batch
            StartCoroutine(BulletsBatchSygnalsToPLyaer());
            bonusGatherSound.Play();
        }

        //trigger for getting a sygnal from bonus objects (bonusGather class) and adding bonus shield
        if (bonusShield)
        {
            shieldButton.interactable = true;
            bonusShield = false;
            bonusGatherSound.Play();
        }


        //all following if conditions are used to controll the sequence of reloading process of gun 
        if (startReloadTraking && bulletsIndicator.value < 1 && !onReloadMode) {
            onReloadMode = true;
            //shotEffect.SetActive(false);
            anim.enabled = false;
            gunShotSound.Stop();
            batchOfGunChildObject.SetActive(false); //disactivating the effect of energy on batch 
            batchOfGun.GetComponent<MeshRenderer>().material = sygnallingMat;
            if (isDual) reloadDetail.transform.localPosition = new Vector3(reloadDetail.transform.localPosition.x, reloadDetail.transform.localPosition.y, -1.03f);
            else if (isTriple) reloadDetail.transform.localPosition = new Vector3(reloadDetail.transform.localPosition.x, reloadDetail.transform.localPosition.y, -0.72f);
            else if (isSingle) reloadDetail.transform.localPosition = new Vector3(reloadDetail.transform.localPosition.x, reloadDetail.transform.localPosition.y, -0.62f);
            reloadButt.enabled = true;

            LaunchManager.Instance.changeCameraWhileReloading(); //making virtual camera closer to gun to make it easier for player to reload

            //sending a sygnal of loose the battle if conditions are so
            if ((isSingle && Lists.energyOfPlayer < 5) || (isDual && Lists.energyOfPlayer < 7) || (isTriple && Lists.energyOfPlayer < 9))
            {
                LaunchManager.Instance.outOfBulletsAndEnergy = true;
            }
        }
        if (onReloadMode)
        {
            if (alfaIsReducing)
            {
                alfaOfMat -= 1 * Time.deltaTime;
                if (alfaOfMat < 0.1f) alfaIsReducing = false;
            }
            else
            {
                alfaOfMat += 1 * Time.deltaTime;
                if (alfaOfMat > 0.9f) alfaIsReducing = true;
            }
            if (!onTriggerMode) reloadButt.image.material = sygnallingMat; //reloadButt.image.color = new Color(1, 0.5615385f, 0f, alfaOfMat);
            //sygnallingMat.color = new Color(1, alfaOfMat, 0.016f);
        }
        if (wholeReloadIsOver) {
            if (isDual)
            {
                reloadDetail.GetComponent<MeshRenderer>().material = DualReloadPartMat; 
                bulletsIndicator.value = dualBulletsMax;
            }
            else if (isSingle)
            {
                reloadDetail.GetComponent<MeshRenderer>().material = SingleReloadPartMat;
                bulletsIndicator.value = singleBulletsMax;
            }
            else if (isTriple)
            {
                reloadDetail.GetComponent<MeshRenderer>().material = TripleReloadPartMat;
                bulletsIndicator.value = tripleBulletsMax;
            }
            gunReloadOverSound.Play();
            batchOfGunChildObject.SetActive(true); //activating the effect of energy on batch 
            wholeReloadIsOver = false;
            onReloadMode = false;
            reloadButt.image.material = null;
            //giving a signal to player of changes on bullets batch
            StartCoroutine(BulletsBatchSygnalsToPLyaer());
        }
        
        if (Lists.DefendGun.Count <1 && !shootingStopped /*&& Lists.barrelIsSet*/)
        {
            GetComponent<RawImage>().color = thisButtonStartColor;
            //shotEffect.SetActive(false);
            anim.enabled = false;
            CancelInvoke();
            //StartCoroutine(musicStopTime());
            shootingStopped = true;
        }


        //TO DELETE ONLY FOR TESTING ON COMPUTER
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (bulletsIndicator.value > 0 && Lists.DefendGun.Count > 0)
            {
                StopCoroutine(musicStopTime());
                gunShotSound.Play();
                GetComponent<RawImage>().color = new Color(1, 0, 0, 0.3f);
                //shooting = true;
                //shotEffect.SetActive(true);
                anim.enabled = true;
                if (isTriple) AttackBulletsTriple();
                else if (isDual) AttackBulletsDual();
                else if (isSingle) AttackBulletsSingle();
            }
            else if (Lists.DefendGun.Count > 0) lackOfbullets.Play();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (Lists.DefendGun.Count > 0)
            {
                GetComponent<RawImage>().color = thisButtonStartColor;
                //shooting = false;
                //shotEffect.SetActive(false);
                anim.enabled = false;
                CancelInvoke();
                StartCoroutine(musicStopTime());
            }
        }

    }

}
