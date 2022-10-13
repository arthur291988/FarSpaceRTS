
using UnityEngine.EventSystems;
using UnityEngine;
//using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEditor;

public class DefAimingCtrlr : MonoBehaviour, IDragHandler, IPointerUpHandler
{
    private bool isDragged = false; // is used on pointer handler and on fixed update functions to unload the processor if aiming button is not pushed

    public static GameObject barrelOfGun; //public barrel of gun which is controlled by this class
                                          //private static GameObject aimingPos; //empty GO which is 50 units far from barrel and serves as look rotation point for rotating gun to shot direction
                                          //private static Vector3 aimingStartPOsition; // stores the very start position of aiming point to set it back if player does not push the aiming button


    [SerializeField]
    private Joystick joystickOfShip;

    //private float lengthOfClampMagnitude; // regulates the magnitude of sharpnes of barrel movements according to players finger movements sharpness

    //private static Vector3 gunPosition; //stores start gun position to calculate aiming vector together with aimin point

    //these two are used to calculate players touch movement and transfer it to aiming point to move it in same direction in dimension
    Vector3 touchDrag;
    Vector3 touchStart;

    //stores shot direction to rotate the barrel along it
    //Vector3 lookDirection;

    //stores calculated aiming point move direction which is touchDrag - touchStart with lengthOfClampMagnitude
    //Vector3 AimPointMoveDir;

    //Color thisButtonStartColor;

    //this one is for setting special camera setting for very narrow screens (most likely tablets)
    public Camera bcgrndCamera;
    

    //AudioSource ServoSound;

    //private float xRotation;
    //private float yRotation;

    //these ones are the multiplyers to slow down the rotation speed of berrels in accordance with screeb width. Wider the screen slower the rotation speed
    //private float rotationMultiplyer;

    //this one is used to adapt the rotation multiplyer to be according to screen size (so for 1280*720 it is 0.002f)
    //private float barrelRotationMultiplyer;


    //private static Rigidbody rb;
    //Vector3 camStartPosition; // used with camera shake on Update and assignet premirely on start method as default main camera position
    //private bool isShaking = false;
    //private float shakeDuration;
    //private float shakeAmount = 0.05f;
    //private float decreaseFactor = 0.2f;
    //public Camera cameraTrial;
    //private RawImage aimingImg;

    // Start is called before the first frame update
    void Start()
    {
        //camStartPosition = cameraTrial.transform.position; //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        //if ((double)Screen.width / Screen.height < 1.5) Camera.main.fieldOfView = 85; //for very narrow screen tablets

        if ((double)Screen.width / Screen.height < 1.5) bcgrndCamera.fieldOfView = 85; //for very narrow screen tablets setting bkgrd camera wider

        //if (Screen.width < 1280) barrelRotationMultiplyer = 0.0025f; //for old tablets mostly width of which is lower thab 1280
        //else if (Screen.width < 1440) barrelRotationMultiplyer = 0.002f;
        //else if (Screen.width < 1560) barrelRotationMultiplyer = 0.0018f;
        //else if (Screen.width < 1729) barrelRotationMultiplyer = 0.0016f;
        //else if (Screen.width < 1920) barrelRotationMultiplyer = 0.0015f;
        //else if (Screen.width < 2160) barrelRotationMultiplyer = 0.0013f;
        //else if (Screen.width < 2388) barrelRotationMultiplyer = 0.0012f;
        //else if (Screen.width < 2560) barrelRotationMultiplyer = 0.0011f;
        //else if (Screen.width < 2960) barrelRotationMultiplyer = 0.0008f;
        //else if (Screen.width < 3040) barrelRotationMultiplyer = 0.0007f;
        //else if (Screen.width >= 3040) barrelRotationMultiplyer = 0.0005f;

        //aimingImg = GetComponent<RawImage>();

        //ServoSound = GetComponent<AudioSource>();





        //TO SET BACK IF JOYSTICK WILL NOT WORKOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
        //thisButtonStartColor = GetComponent<RawImage>().color;








        //aimingStartPOsition = aimingPos.transform.position;
    }


    //%%%%%%%%%%%%%%%%%%%%%%%%%%!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! TO DELETE EXPERIMAENTAL SCENE SWITCHER ONLY
    public void chsngeScene()
    {
        Lists.DefendGun.Clear(); //this is really important thing to put on scene switcher. It clears out the list of guns. If it does not it will hold the gun on list
        //when player win the battle with fighters and add to it the second GO to list when next defend scene is loaded and thet that all will cause a crash of game
        Lists.FightersCPU = 0;
        SceneSwitchMngr.LoadJourneyScene();
        //SceneManager.LoadScene(0); //loading journey scene
    }

    public static void setBarrelForAiming(GameObject barrel /* GameObject aimingPointer*/) {
        barrelOfGun = barrel;

        //rb = barrelOfGun.GetComponent<Rigidbody>();
        //aimingPos = aimingPointer;
        //gunPosition = barrelOfGun.transform.position;
        //aimingStartPOsition = aimingPos.transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //if (!ServoSound.isPlaying) ServoSound.Play();

        //Debug.Log(Mathf.Abs (yRotation*1000));
        //if (Mathf.Abs(yRotation * 1000) > 50 || Mathf.Abs(xRotation * 1000) > 50 && !ServoSound.isPlaying) ServoSound.Play();
        //else ServoSound.Stop();
        //transform.position = eventData.pressEventCamera.ScreenToViewportPoint(eventData.position);






        //touchDrag = eventData.position;
        isDragged = true;

    }

    //public void OnPointerDown(PointerEventData eventData)
    //{
        //isShaking = true;


        //TO SET BACK IF JOYSTICK WILL NOT WORKOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
        //GetComponent<RawImage>().color = new Color(0, 1, 0, 0.3f);
        //aimingImg.color = new Color (0, 0 ,0,0);
        //touchStart = eventData.position;
    //}

    public void OnPointerUp(PointerEventData eventData)
    {
        //isShaking = false;








        //TO SET BACK IF JOYSTICK WILL NOT WORKOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
        //GetComponent<RawImage>().color = thisButtonStartColor;
        isDragged = false;
        //aimingImg.color = new Color(0, 0.82f, 0.34f, 0.85f);













        //transform.position = touchStart;
        //lengthOfClampMagnitude = 0;
        //aimingPos.transform.position = aimingStartPOsition;
        //ServoSound.Stop();
    }

    

    

    private void FixedUpdate()
    {
        if (isDragged && Lists.DefendGun.Count > 0)
        {









            //TO SET BACK IF JOYSTICK WILL NOT WORKOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
            //rb.rotation = Quaternion.Euler(xRotation, yRotation,0);
            //barrelOfGun.transform.Rotate(xRotation, yRotation, 0);



            barrelOfGun.transform.Rotate(joystickOfShip.Vertical*-1*0.4f, joystickOfShip.Horizontal* 0.7f, 0);




















            //TO PUT BACK IN CASE OF NECESSITY

            //AimPointMoveDir = Vector3.Normalize(touchDrag - touchStart) * lengthOfClampMagnitude; //Vector3.Normalize (touchDrag - touchStart); //Vector3.ClampMagnitude((touchDrag - touchStart), 0.01f);
            //aimingPos.transform.position = aimingPos.transform.position + AimPointMoveDir;
            //lookDirection = aimingPos.transform.position - gunPosition;
            //barrelOfGun.transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        }

        //so if player does not aiming this snippet of code puts barrel back to its start position slowly
        else if (Lists.DefendGun.Count > 0)
        {
            //repeating of Lists.DefendGun.Count > 0 is necessary to prevent bug with setting another gun, fixed update method is still working and checking this is condition
            //even if gun was removed from list and that causes a bug. So checking the emptyness of
            if (barrelOfGun.transform.rotation != Quaternion.Euler(0, 0, 0))
                barrelOfGun.transform.rotation = Quaternion.Lerp(barrelOfGun.transform.rotation, Quaternion.Euler(0, 0, 0), 0.1f);
        }
    }
    //private void Update()
    //{

        //Debug.Log((touchDrag - touchStart).sqrMagnitude + " square " + (touchDrag - touchStart).normalized + " normal "+ (touchDrag - touchStart).magnitude+" magni") ;

        //if (isDragged) Debug.Log(Mathf.Abs((touchDrag - touchStart).x + (touchDrag - touchStart).y));
        //this code regulates sharpness of barrel movements according to players fingers movements
        //if (isDragged) lengthOfClampMagnitude = Mathf.Abs((touchDrag - touchStart).x + (touchDrag - touchStart).y) * 0.0006f;


        //if player dragget aiming button this function starts to calculate y and x rotation values to transfer them to FixedUpdate method to realize rotation physics
        //y and x are reversed cause barrel of gun gismos are so. And because of it also y axis multiplyed to -0.001 value









        //TO SET BACK IF JOYSTICK WILL NOT WORKOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
        //if (isDragged)
        //{
        //    yRotation = (touchDrag - touchStart).x* barrelRotationMultiplyer;  //*0.002f;
        //    xRotation = (touchDrag - touchStart).y * -barrelRotationMultiplyer;   //*-0.002f;
        //}




















        //if (isDragged) lengthOfClampMagnitude = (touchDrag - touchStart).sqrMagnitude / 100000;



        //if (isDragged && (touchDrag - touchStart).sqrMagnitude < 4000) lengthOfClampMagnitude = 0.02f;
        //else if (isDragged && (touchDrag - touchStart).sqrMagnitude > 4000 && (touchDrag - touchStart).sqrMagnitude < 8000) lengthOfClampMagnitude = 0.09f;
        //else lengthOfClampMagnitude = 0.5f;

        //this method shakes the camera whet mega attack is started;





        //else if ((touchDrag - touchStart).sqrMagnitude > 2500&& (touchDrag - touchStart).sqrMagnitude < 4000) lengthOfClampMagnitude = 0.2f;
        //else if ((touchDrag - touchStart).sqrMagnitude > 4000) lengthOfClampMagnitude = 0.5f;
    //}

}
