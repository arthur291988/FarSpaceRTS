
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//using UnityEngine.SceneManagement; ->->->->->->->->->->

public class PlayersJoystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public Slider slider;
    private float aimXPose = 0;

    //following properties are used with camera shake in case of mega attacks
    Vector3 camStartPosition; // used with camera shake on Update and assignet premirely on start method as default main camera position
    private float shakeDuration;
    private float shakeAmount = 0.3f;
    private float decreaseFactor = 0.2f;
    private float cruis1Duration = 5f;
    private float cruis2Duration = 3f;
    private float cruis4Duration = 5f;
    private bool isShaking = false; // this one is necessary to preven intersection of update methods of CPUShipCtrl class and curren class, without it one of classes will prevent shaking

    //power shield properties
    public Button shieldAsButton;
    public GameObject shieldButton;
    private static float shieldTimerStart; //timer which is used with shield reload
    private static float shieldRealodFilling; //filling amount of shield icon
    //private static GameObject powerSieldReal; // this one is locally instantiated power shiled
    //private static GameObject powerShield;
    private static float shieldOnTime;
    private static float shieldReloadTime;
    private static bool withShieldCurrent = false;

    private Vector3 touchDraggedPoint;
    private Vector3 shotDirection;
    private static Vector3 ShipStartPosition;
    private GameObject bulletReal;
    private GameObject bulletReal2; //for use with double bullet featured ships like Cruis 1 and 2 and destr 1
    private GameObject bulletReal3; //for use with triple bullet featured ships only cruis 1

    //colors to set for dimention in cameras
    //private Color blackDimensionCol = new Color(0, 0.005f, 0.094f, 0);
    //private Color blueDimensionCol = new Color(0, 0.208f, 0.5f, 0);
    //private Color redDimensionCol = new Color(0.5568628f, 0.1294118f, 0.1404912f, 0);

    //these propertie are used with ships reloading indicator 
    public GameObject ReloadIndicator;
    //public GameObject RightCtrlr; TO DELETE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    //public GameObject LeftCtrlr; TO DELETE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    private float timerStart; //timer which is used with gun reload
    private static float realodFilling; //filling amount of aiming icon

    //float to modify the force of CPU attack to be applicable for joystick shot
    private const float SHOT_FORCE_MODIFIER = 1.1f;

    //palaizer bullets for player
    private static GameObject paralBullet;

    //shot effect of ship
    private static ParticleSystem shotEffect;


    //is current ship has double bullet shot features
    private static bool doubleBulletLocal;
    //is current ship has triple bullet shot features
    private static bool tripleBulletLocal;

    //shot bullets
    //are used to instantiate the bullet to shot
    private static GameObject shotBulletReal;

    //this float and bool are used to restrict player's attack sequence
    private static float ATTACK_TIME_FROM;
    private static bool canShot; 

    //ships bullet force (speed of flight)
    private static float SHIP_ATTACK_FORCE;

    //properties to use with paralizer ships
    private static bool isParalizer = false;
    private bool paralizerBullet = false;

    private static bool isParalized = false;

    //this gameobjects lists are galaxyes to place randomly on scene
    public GameObject GalaxyObjects; 
    private ParticleSystem.MainModule main; //is used to change galaxy colors randomly on start
    private GameObject galaxy1; //are used to store locally game objects for being able to change their particles color randomely
    private GameObject galaxy2;

    //mega attack features
    public Button MegaAttackButt;
    private static GameObject megaShotLocal; //Cruiser4 mega attack effect, mega wave
    public GameObject Cat3MegaDefence; //Cruiser3 mega attack effect, mega powershield
    public GameObject Cat2MegaParal; //Cruiser2 mega attack effect, mega paralizer
    public GameObject Cat1MegaLaser; //Cruiser1 mega attack effect, mega lazer
    private GameObject MegaLaser; //stores local mega laser for Cruiser1 to delete it after few seconds
    private bool right=true; //makes Cruiser1 mega laser effect rotate firs to the right 

    private static GameObject PlayerShip;
    private LineRenderer aimingLine;
    float distance = 30f; //distance is used with slider, so this property is used to set constant z distance from ship to the end off aiming line
    private static AudioSource laserShot; // shot sound

    //This one is necessary for setting active maneuvering controllersTO DELETE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    //private static bool isManeuvering = false;TO DELETE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    private bool isDragged = false;
    private bool fire = false; //is used to give a signal to fixed update method to make a shot

    //current ship time slowing property
    private static float TIME_DILATION;

    //the lists to set active the bullet from the pull of bullets on ObjectPuller class
    private static List <GameObject> shotBulletPullJoystick;
    private static List<GameObject> shotBulletParalPullJoystick;

    //following setters get the properties of ship the player choose commonly from PlayerShipCPU method
    public static void setShip(GameObject ship, bool Manuvering, float timeDilation, ParticleSystem shotEff, GameObject megaShot, bool doubleBullet, bool tripleBullet, AudioSource shotSound) 
    {
        PlayerShip = ship;
        ShipStartPosition = PlayerShip.transform.position; 
        realodFilling = 1;
        shieldRealodFilling = 1;
        canShot = true;
        megaShotLocal = megaShot;
        TIME_DILATION = timeDilation;
        shotEffect = shotEff;
        doubleBulletLocal = doubleBullet;
        tripleBulletLocal = tripleBullet;
        laserShot.clip = shotSound.clip;
    }


    public static void setShield(/*GameObject shield, */float onTime, float reloadTime, bool shieldIs) {
        //powerShield = shield;
        shieldOnTime = onTime;
        shieldReloadTime = reloadTime;
        shieldTimerStart = reloadTime+ onTime;
        withShieldCurrent = shieldIs;
        //if (powerSieldReal) Destroy(powerSieldReal);
    }

    //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((
    public static void setShotBullet(/*GameObject bullet, */bool isParal, List <GameObject> shotBulletPull) {
        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((

        /*shotBulletReal = bullet;*/
        isParalizer = isParal;
        shotBulletPullJoystick = shotBulletPull;
    }

    //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((

    public static void setParalBullet(/*GameObject bulletPar*/List<GameObject> shotBulletParalPull)
    {
        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((
        //paralBullet = bulletPar;
        shotBulletParalPullJoystick = shotBulletParalPull;
    }

    public static void setAttackTime(float time)
    {
        ATTACK_TIME_FROM = time;
    }

    public static void setAttackForce(float force)
    {
        SHIP_ATTACK_FORCE = force* SHOT_FORCE_MODIFIER;
    }

    public static void setParalized(bool paral)
    {
        isParalized = paral;
    }

    // Start is called before the first frame update
    void Start()
    {
        //instantiate two galaxys on battle sce from star
        galaxy1 = Instantiate(GalaxyObjects, new Vector3(UnityEngine.Random.Range(-25, -165),
                   -250, UnityEngine.Random.Range(60, 300)), Quaternion.Euler(25, 0, UnityEngine.Random.Range(-25, 25)));
        
        galaxy1.GetComponent<MeshRenderer>().material.SetColor("_Color1", new Color(UnityEngine.Random.Range(0.15f, 0.3f),
                    UnityEngine.Random.Range(0.7f, 0.85f), 0, 0));
        galaxy1.GetComponent<MeshRenderer>().material.SetColor("_Color2", new Color(UnityEngine.Random.Range(0.6f, 0.8f),
            UnityEngine.Random.Range(0.5f, 0.7f), 0, 0));

        

        galaxy2 = Instantiate(GalaxyObjects, new Vector3(UnityEngine.Random.Range(25, 165),
                   -250, UnityEngine.Random.Range(60, 300)), Quaternion.Euler(3, 0, UnityEngine.Random.Range(-25, 25)));
        
        galaxy2.GetComponent<MeshRenderer>().material.SetColor("_Color1", new Color(UnityEngine.Random.Range(0.1f, 0.2f),
                    0, UnityEngine.Random.Range(0.45f, 0.85f), 0));
        galaxy2.GetComponent<MeshRenderer>().material.SetColor("_Color2", new Color(UnityEngine.Random.Range(0.55f, 0.85f),
            0, UnityEngine.Random.Range(0.45f, 0.65f), 0));



        ////instantiate two galaxys on battle sce from star
        //galaxy1 = Instantiate(GalaxyObjects[UnityEngine.Random.Range(0, GalaxyObjects.Count)], new Vector3(UnityEngine.Random.Range(-25, -165),
        //           -150, UnityEngine.Random.Range(60, 300)), UnityEngine.Random.rotation);
        //galaxy2 = Instantiate(GalaxyObjects[UnityEngine.Random.Range(0, GalaxyObjects.Count)], new Vector3(UnityEngine.Random.Range(25, 165),
        //           -150, UnityEngine.Random.Range(60, 300)), UnityEngine.Random.rotation);
        //main = galaxy1.GetComponent<ParticleSystem>().main;
        //main.startColor = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f, 1f, 1f); //generates randome color for galaxy
        //main = galaxy2.GetComponent<ParticleSystem>().main;
        //main.startColor = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f, 1f, 1f); //generates randome color for galaxy


        //sets the colour of camera background according to current dimension
        //if (Lists.isBlackDimension) Camera.main.backgroundColor = blackDimensionCol;
        //if (Lists.isBlueDimension) Camera.main.backgroundColor = blueDimensionCol;
        //if (Lists.isRedDimension) Camera.main.backgroundColor = redDimensionCol;

        camStartPosition = Camera.main.transform.position;
        aimingLine = GetComponent<LineRenderer>();
        aimingLine.positionCount = 2;
        laserShot = GetComponent<AudioSource>();
        canShot = true;
    }

    //following methods instantiate players ship mega attack according to name of cruiser name, each level of cruisers has its own type of mega attack.
    //this method with class is assigned onClick method of mega attack button
    public void megaShotInst() {
        if (Lists.PlayerShip.Count > 0)
        {
            if (PlayerShip.name.Contains("Cruis4Play"))
            {
                shakeDuration = cruis4Duration;
                isShaking = true;
                MegaAttackButt.enabled = false;
                MegaAttackButt.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.2f);
                megaShotLocal.GetComponent<ParticleSystem>().Play();
                megaShotLocal.GetComponent<AudioSource>().Play();
            }
            else if (PlayerShip.name.Contains("Cruis3Play"))
            {
                shakeDuration = cruis2Duration;
                isShaking = true;
                MegaAttackButt.enabled = false;
                MegaAttackButt.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.2f);
                GameObject defenceField = Instantiate(Cat3MegaDefence, new Vector3(0, -1.3f, -3), Quaternion.Euler(90, 0, 0));
                Destroy(defenceField, 7);
            }
            else if (PlayerShip.name.Contains("Cruis2Play"))
            {
                shakeDuration = cruis2Duration;
                isShaking = true;
                MegaAttackButt.enabled = false;
                MegaAttackButt.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.2f);
                GameObject paral = Instantiate(Cat2MegaParal, PlayerShip.transform.position, Quaternion.Euler(0, 0, 0));
                Destroy(paral, 1);
                foreach (GameObject gos in Lists.AllCPUShipsWithoutGuns)
                {
                    if (!gos.GetComponent<CPUShipsCtrl>().shieldIsActive)gos.GetComponent<CPUShipsCtrl>().callForMegaParalFromOutside();
                }
                foreach (GameObject gog in Lists.AllCPUGuns)
                {
                    gog.GetComponent<CPUGunCtrlr>().callForMegaParalFromOutside();
                }
            }
            else if (PlayerShip.name.Contains("Cruis1Play"))
            {
                shakeDuration = cruis1Duration;
                isShaking = true;
                MegaAttackButt.enabled = false;
                MegaAttackButt.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.2f);
                MegaLaser = Instantiate(Cat1MegaLaser, PlayerShip.transform.position, Quaternion.Euler(0, 0, 0));
            }
        }
    }
        

    //%%%%%%%%%%%%%%%%%%%%%%%%%%!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! TO DELETE EXPERIMAENTAL SCENE SWITCHER ONLY
    public void chsngeScene() {
        Lists.PlayerShip.Clear();
        Lists.AllPlayerShips.Clear();
        Lists.AllPlayerShipsWithoutGuns.Clear();
        Lists.AllCPUShipsWithoutGuns.Clear();
        Lists.AllCPUShips.Clear();

        Lists.PlayerShips1Class.Clear();
        Lists.CPUShips1Class.Clear();
        Lists.PlayerShips2Class.Clear();
        Lists.CPUShips2Class.Clear();

        Lists.AllPlayerGuns.Clear();
        Lists.AllCPUGuns.Clear();

        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;


        SceneSwitchMngr.LoadJourneyScene();
        //SceneManager.LoadScene(1);
    }

    //%%%%%%%%%%%%%%%%%%%%%%%%%%!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! TO DELETE EXPERIMAENTAL SCENE SWITCHER ONLY
    public void chsngeSceneDefence()
    {
        Lists.PlayerShip.Clear();
        Lists.AllPlayerShips.Clear();
        Lists.AllPlayerShipsWithoutGuns.Clear();
        Lists.AllCPUShipsWithoutGuns.Clear();
        Lists.AllCPUShips.Clear();

        Lists.PlayerShips1Class.Clear();
        Lists.CPUShips1Class.Clear();
        Lists.PlayerShips2Class.Clear();
        Lists.CPUShips2Class.Clear();

        Lists.AllPlayerGuns.Clear();
        Lists.AllCPUGuns.Clear();

        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;


        SceneSwitchMngr.LoadDefenceScene();
        //SceneManager.LoadScene(2);
    }

    //power shiled launcher
    public void powerShieldButton()
    {
        if (!isParalized)
        {
            //invokes the power shiled method from the player ship script it is necessary for escaping to be paralized in case of mega paralizer effect
            //cause mega parlizer checks if power shield is turned on from from player ship CPU script
            PlayerShip.GetComponent<PlayerShipCPU>().putShieldOnMethodForJoystick(); 
            //powerSieldReal = Instantiate(powerShield, PlayerShip.transform.position, Quaternion.identity); TO DELETE -------------------------
            shieldAsButton.enabled = false;
            shieldButton.GetComponent<Image>().color = new Color(0,1,0,0.2f);
            shieldButton.GetComponent<Image>().fillAmount = 0; //shield circle sets zero after each shield activation
            shieldTimerStart = 0; // zeros the timer to start the time count before reloading the ships shield
            //if (powerSieldReal) Destroy(powerSieldReal, shieldOnTime); // shield must be destroyed after invocation TO DELETE -------------------------
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    { if (Lists.PlayerShip.Count > 0)
        {
            isDragged = true;
            aimingLine.enabled = true;

            //this one checks if reloading circle (slider handler on UI canvas) if filled and also it allows the player to reduce the time of game, 
            //the tiem reduce feature is different for different ship typers
            if (realodFilling >= 1)
            {
                Time.timeScale = TIME_DILATION;
                Time.fixedDeltaTime = 0.01f * Time.timeScale;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //so if ship is not parasiled it instantiates the bullet according the ship type
        if (Lists.PlayerShip.Count > 0&& canShot && !isParalized)
        {

            //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((
            //if ship is not paralizer it instantiates only regular bullets
            //if (!isParalizer) bulletReal = Instantiate(shotBulletReal, ShipStartPosition, Quaternion.Euler(-90, 0, 0));


            if (!isParalizer)
            {
                //if the ship has such feature it will shot with double bullets so additional bullet is created and impulse added to it later in fixed update
                if (doubleBulletLocal) {
                    bulletReal2 = ObjectPuller.current.GetUniversalBullet(shotBulletPullJoystick);
                    bulletReal2.transform.position = ShipStartPosition * 1.1f; //correction of spawn position of second bullet to make it noticable for player
                    bulletReal2.transform.rotation = Quaternion.Euler(0, 0, 0);
                    bulletReal2.SetActive(true);
                }
                //if the ship has such feature it will shot with triple bullets so additional bullets is created and impulse added to it later in fixed update
                //only for crui 1 level
                if (tripleBulletLocal)
                {
                    bulletReal2 = ObjectPuller.current.GetUniversalBullet(shotBulletPullJoystick);
                    bulletReal2.transform.position = ShipStartPosition * 1.1f; //correction of spawn position of second bullet to make it noticable for player
                    bulletReal2.transform.rotation = Quaternion.Euler(0, 0, 0);
                    bulletReal2.SetActive(true);

                    bulletReal3 = ObjectPuller.current.GetUniversalBullet(shotBulletPullJoystick);
                    bulletReal3.transform.position = ShipStartPosition * 1.15f; //correction of spawn position of second bullet to make it noticable for player
                    bulletReal3.transform.rotation = Quaternion.Euler(0, 0, 0);
                    bulletReal3.SetActive(true);
                }
                bulletReal = ObjectPuller.current.GetUniversalBullet(shotBulletPullJoystick);
                bulletReal.transform.position = ShipStartPosition;
                bulletReal.transform.rotation = Quaternion.Euler(0, 0, 0);
                bulletReal.SetActive(true);
            }

            //if it is paralizer it instantiates two different types of bullets per one shot, paralizing bullent and regular bullet 
            else
            {
                //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((
                if (!paralizerBullet)
                {
                    //bulletReal = Instantiate(shotBulletReal, ShipStartPosition, Quaternion.Euler(0, 0, 0));

                    bulletReal = ObjectPuller.current.GetUniversalBullet(shotBulletPullJoystick);
                    bulletReal.transform.position = ShipStartPosition; 
                    bulletReal.transform.rotation = Quaternion.Euler(0, 0, 0);
                    bulletReal.SetActive(true);

                    //if the ship has such feature it will shot with double bullets so additional bullet is created and impulse added to it later in fixed update
                    if (doubleBulletLocal)
                    {
                        bulletReal2 = ObjectPuller.current.GetUniversalBullet(shotBulletPullJoystick);
                        bulletReal2.transform.position = ShipStartPosition * 1.1f; //correction of spawn position of second bullet to make it noticable for player
                        bulletReal2.transform.rotation = Quaternion.Euler(0, 0, 0);
                        bulletReal2.SetActive(true);
                    }

                    paralizerBullet = true;
                }
                //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((
                else
                {
                    //bulletReal = Instantiate(paralBullet, ShipStartPosition, Quaternion.Euler(0, 0, 0));
                    bulletReal = ObjectPuller.current.GetUniversalBullet(shotBulletParalPullJoystick);
                    bulletReal.transform.position = ShipStartPosition;
                    bulletReal.transform.rotation = Quaternion.Euler(0, 0, 0);
                    bulletReal.SetActive(true);
                    paralizerBullet = false;
                }
            }
            //the ship shots only if there was a catch if player touch on aiming circle before
            if (isDragged)
            {
                fire = true;
            }
            isDragged = false;
            canShot = false;
            aimXPose = 0;
            slider.value = 0;
            touchDraggedPoint = ShipStartPosition + new Vector3(aimXPose, -1.3f, distance);
            ReloadIndicator.GetComponent<Image>().fillAmount = 0; //reloading circle sets zero after each shot
            timerStart = 0; // zeros the timer to start the time count before reloading the ships gun

        }
        if (shotEffect) shotEffect.Play();
        //after shot time scale is set to regular state, cause it was reduced before while player aiming
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }

    

    private void FixedUpdate()
    {
        if (isDragged)
        {
            //calculating the shot direction
            //touchDraggedPoint = ShipStartPosition + new Vector3(slider.value, -1.3f, distance);
            
            touchDraggedPoint = ShipStartPosition + new Vector3(aimXPose, -1.3f, distance);

            //touchDraggedPoint = ShipStartPosition + new Vector3(slider.value, -1.3f, distance); &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
            shotDirection = touchDraggedPoint - ShipStartPosition;
        }
        if (fire)
        {
            if (!isParalizer)
            {
                bulletReal.GetComponent<Rigidbody>().velocity = Vector3.zero;
                bulletReal.GetComponent<Rigidbody>().AddForce(shotDirection * SHIP_ATTACK_FORCE, ForceMode.Impulse);
                if (doubleBulletLocal)
                {
                    bulletReal2.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    bulletReal2.GetComponent<Rigidbody>().AddForce(shotDirection * SHIP_ATTACK_FORCE, ForceMode.Impulse);
                }

                if (tripleBulletLocal)
                {
                    bulletReal2.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    bulletReal2.GetComponent<Rigidbody>().AddForce(shotDirection * SHIP_ATTACK_FORCE, ForceMode.Impulse);

                    bulletReal3.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    bulletReal3.GetComponent<Rigidbody>().AddForce(shotDirection * SHIP_ATTACK_FORCE, ForceMode.Impulse);
                }
            }
            //this devision is necessary to prevent a bug of missing reference while switching to 1 class paralizer from 2 class paralizer after the firs one made 
            //a fireball shot (after very start of battle scene). Cause in this case this class holds true the paralizer type of shot but the bulletReal2 is not instantiated 
            //by the moment. The scond reson is to prevent an unnecessary function of shouting the disacitvated bullet, cause that is what did that script after it instantiated
            //the second bullet from the start shot for double shot type of ship
            else
            {
                bulletReal.GetComponent<Rigidbody>().velocity = Vector3.zero;
                bulletReal.GetComponent<Rigidbody>().AddForce(shotDirection * SHIP_ATTACK_FORCE, ForceMode.Impulse);
                if (doubleBulletLocal&& paralizerBullet)
                {
                    bulletReal2.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    bulletReal2.GetComponent<Rigidbody>().AddForce(shotDirection * SHIP_ATTACK_FORCE, ForceMode.Impulse);
                }
            }
            laserShot.Play();
            aimingLine.enabled = false;
            fire = false;
        }
    }

    private void Update()
    {
        //this method shakes the camera whet mega attack is started;
        if (shakeDuration > 0 && isShaking)
        {
            Camera.main.transform.position = camStartPosition + UnityEngine.Random.insideUnitSphere * shakeAmount;
            shakeDuration -= decreaseFactor;
        }
        else if (isShaking)
        {
            shakeDuration = 0f;
            Camera.main.transform.position = camStartPosition;
            isShaking = false;
        }

        // if mega laser of 1 leve cruiser is on (and ship is on) that condition moves it from right to left in one loop
        if (MegaLaser&& PlayerShip) {
            MegaLaser.transform.position = PlayerShip.transform.position;
            if (right)
            {
                MegaLaser.transform.Rotate(0, 40 * Time.deltaTime, 0);
                if (MegaLaser.transform.rotation.y*100 > 40) right = false;
            }
            else MegaLaser.transform.Rotate(0, -40 * Time.deltaTime, 0);
            if (MegaLaser.transform.rotation.y * 100 < -40)
            {
                right = true;
                Destroy(MegaLaser);
            }
        }

        //moving aiming line 
        if (isDragged && aimXPose<72 && aimXPose >-72) aimXPose += slider.value;

        //this check is necessery to avoid error of MissingReferenceException, so ship should be chousen by player
        if (Lists.PlayerShip.Count > 0)
        {
            //sets color of mega attack button to transparent and grey if current ship is not cruiser and back to red if current ship is cruiser
            //but it also takes into account if mega attack button is enabled (means not pushed once already)
            if (!PlayerShip.name.Contains("Cruis") && MegaAttackButt.enabled) MegaAttackButt.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.2f);
            else if (MegaAttackButt.enabled) MegaAttackButt.GetComponent<Image>().color = Color.red;

            if (!ReloadIndicator.activeInHierarchy)ReloadIndicator.SetActive(true);
            ShipStartPosition = PlayerShip.transform.position;
            aimingLine.SetPosition(1, touchDraggedPoint);
            aimingLine.SetPosition(0, ShipStartPosition);
        }
        else {
            Destroy(MegaLaser);
            ReloadIndicator.SetActive(false);
            shieldButton.SetActive(false);
            aimingLine.enabled = false;
            //sets color of mega attack button to transparent and grey if ship is not chousen by player
            MegaAttackButt.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.2f); 
        }

        //if the ship chosen by player has powerShield this snippet puts it on and off 
        if (shieldButton.activeInHierarchy&&!withShieldCurrent) shieldButton.SetActive(false);
        if (!shieldButton.activeInHierarchy&&withShieldCurrent&& Lists.PlayerShip.Count > 0) shieldButton.SetActive(true);

        //checks if timer reached the gun reloading time
        if (timerStart < ATTACK_TIME_FROM && !canShot)
        {
            timerStart += Time.deltaTime;
            realodFilling = timerStart / ATTACK_TIME_FROM;
            ReloadIndicator.GetComponent<Image>().fillAmount = realodFilling;
            ReloadIndicator.GetComponent<Image>().color = new Color(1,0, 0, 0.3f); 
        }
        else
        {
            canShot = true;
            ReloadIndicator.GetComponent<Image>().fillAmount = realodFilling;
            ReloadIndicator.GetComponent<Image>().color = Color.red;
        }

        //checks if timer reached the shield reloading time
        if (shieldTimerStart < (shieldReloadTime+ shieldOnTime)&&withShieldCurrent)
        {
            shieldTimerStart += Time.deltaTime;
            shieldRealodFilling = shieldTimerStart / (shieldReloadTime + shieldOnTime);
            shieldButton.GetComponent<Image>().fillAmount = shieldRealodFilling;
        }
        else if (withShieldCurrent)
        {
            shieldAsButton.enabled = true;
            shieldButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            shieldButton.GetComponent<Image>().fillAmount = shieldRealodFilling;
        }

        
        //moves power shield with players ship position
        //if (powerSieldReal&& PlayerShip&& withShieldCurrent) powerSieldReal.transform.position = PlayerShip.transform.position;
    }

}
