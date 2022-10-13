using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ShipController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    private Joystick joystickOfShip;

    private bool isJoystickPushed;

    public AudioSource maneuvreSound;

    private static float moveSpeed;

    //public Slider slider;
    private static GameObject ship;
    private static Transform lookPoint;

    public static GameObject leftEngine;
    public static GameObject rightEngine;
    public static GameObject left;
    public static GameObject right;

    private Vector3 moveFromPoint;
    private static Vector3 moveToPoint;
    private static Vector3 moveToDircetion;
    private static Vector3 shipStartPosition;
    private static Rigidbody shipRB;
    //private bool isSliderIsPushed;
    public static bool isMovePushed = false;
    //this one is used to reduce speed of energon or guard efter they hit the asteroid
    public static float reduceSpeedVar = 1;
    //private float distance = 1.5f;
    //private float manuvreSpeed = 5f;


    // Start is called before the first frame update
    //void Start()
    //{
    //    //if (name.Contains("Cruis4Play"))
    //    //{
    //    //    moveSpeed = 5;
    //    //    //SpaceCtrlr.Instance.EngineSound.clip = SpaceCtrlr.Instance.class4CruisEngine;
    //    //}
    //    //else if (name.Contains("Cruis3Play"))
    //    //{
    //    //    moveSpeed = 7;
    //    //    //SpaceCtrlr.Instance.EngineSound.clip = SpaceCtrlr.Instance.class3CruisEngine;
    //    //}
    //    //else if (name.Contains("Cruis2Play"))
    //    //{
    //    //    moveSpeed = 9;
    //    //    //SpaceCtrlr.Instance.EngineSound.clip = SpaceCtrlr.Instance.class2CruisEngine;
    //    //}
    //    //else if (name.Contains("Cruis1Play"))
    //    //{
    //    //    moveSpeed = 12;
    //    //    //SpaceCtrlr.Instance.EngineSound.clip = SpaceCtrlr.Instance.class1CruisEngine;
    //    //}

    //    //TO DELETE ------------------------------------
    //    //panel.SetActive(true);
    //}



    public static void setShip(GameObject shipInst, Transform lookTo, GameObject engineRight, GameObject engineLeft, GameObject leftTurn, GameObject rightTurn) {
        ship = shipInst;
        leftEngine = engineLeft;
        rightEngine = engineRight;
        left = leftTurn;
        right = rightTurn;
        shipRB = ship.GetComponent<Rigidbody>();
        lookPoint = lookTo;
        moveToPoint = lookPoint.transform.position;
        shipStartPosition = ship.transform.position;
        moveToDircetion = moveToPoint - shipStartPosition;
        if (ship.name.Contains("Cruis4Play")) moveSpeed = 5;
        else if (ship.name.Contains("Cruis3Play")) moveSpeed = 7;
        else if (ship.name.Contains("Cruis2Play")) moveSpeed = 9;
        else if (ship.name.Contains("Cruis1Play")) moveSpeed = 12;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        isJoystickPushed = true;
        //isSliderIsPushed = true;
        maneuvreSound.Play();
    }


    //private void FixedUpdate()
    //{
    //    ////so the movements of player ship will work only if it is not paralized, the second condition in if block
    //    //if (isSliderIsPushed && !SpaceCtrlr.Instance.localLaunchingObjects.isParalized) {
    //    //    ship.transform.Rotate(0, slider.value,0);
    //    //    //if (transform.rotation.eulerAngles.x>-30 && transform.rotation.eulerAngles.x < 30) ship.transform.Rotate(slider.value,0 , 0);

    //    //    //moveFromPoint = new Vector3(shipRB.position.x, -8f, distance);
    //    //    //moveToPoint = new Vector3(slider.value, -8f, distance);
    //    //    moveToDircetion = moveToPoint - shipStartPosition;
    //    //    //shipRB.rotation = Quaternion.FromToRotation(moveFromPoint- siptStartPosition, moveToDircetion);
    //    //}

    //    if (isJoystickPushed && !SpaceCtrlr.Instance.localLaunchingObjects.isParalized)
    //    {
    //        ship.transform.Rotate(0, joystickOfShip.Horizontal, 0);
    //        //if (transform.rotation.eulerAngles.x>-30 && transform.rotation.eulerAngles.x < 30) ship.transform.Rotate(slider.value,0 , 0);

    //        //moveFromPoint = new Vector3(shipRB.position.x, -8f, distance);
    //        //moveToPoint = new Vector3(slider.value, -8f, distance);
    //        moveToDircetion = moveToPoint - shipStartPosition;
    //        //shipRB.rotation = Quaternion.FromToRotation(moveFromPoint- siptStartPosition, moveToDircetion);
    //    }

    //    if (isJoystickPushed && !SpaceCtrlr.Instance.localLaunchingObjects.isParalized && joystickOfShip.Vertical >= 0.1f && !isMovePushed) {
    //        isMovePushed = true;
    //        SpaceCtrlr.Instance.EngineSound.Play();
    //    }
    //    if (isJoystickPushed && joystickOfShip.Vertical < 0.1f && isMovePushed) {
    //        isMovePushed = false;
    //        SpaceCtrlr.Instance.EngineSound.Stop();
    //    }

    //    //so the movements of player ship will work only if it is not paralized, the second condition in if block
    //    if (isMovePushed && !SpaceCtrlr.Instance.localLaunchingObjects.isParalized) shipRB.velocity = moveToDircetion* moveSpeed* reduceSpeedVar;//shipRB.AddForce(moveToDircetion, ForceMode.Force);
    //    else if (shipRB!=null) shipRB.velocity = Vector3.Lerp(shipRB.velocity, new Vector3(0, 0, 0),0.008f);

    //    //gradually increasing the speed of guard or energon after they it the asteroid and lost the speed, sygnal about hit comes from onTriggerEnter func of RandomRotator
    //    //class that is attached to each asteroid
    //    if (reduceSpeedVar < 1) reduceSpeedVar += 0.008f;
    //    if (reduceSpeedVar > 1) reduceSpeedVar = 1; //set the speed modifier 1 if it passed above 1 in update method

    //    if (lookPoint) moveToPoint = lookPoint.transform.position;
    //    if (lookPoint) shipStartPosition = ship.transform.position;
    //}

    // Update is called once per frame
    void Update()
    {
        
        //if (isMovePushed && ship!=null) {
        //    leftEngine.SetActive(true);
        //    rightEngine.SetActive(true);
        //}
        //else if (ship != null)
        //{
        //    leftEngine.SetActive(false);
        //    rightEngine.SetActive(false);
        //}






        //if (slider.value > 0 && ship != null) {
        //    right.SetActive(true);
        //    left.SetActive(false);
        //}
        //else if (slider.value < 0 && ship != null)
        //{
        //    right.SetActive(false);
        //    left.SetActive(true);
        //}
        //else if (slider.value == 0 && ship != null)
        //{
        //    right.SetActive(false);
        //    left.SetActive(false);
        //}

        if (joystickOfShip.Horizontal > 0 && ship != null)
        {
            right.SetActive(true);
            left.SetActive(false);
        }
        else if (joystickOfShip.Horizontal < 0 && ship != null)
        {
            right.SetActive(false);
            left.SetActive(true);
        }
        else if (joystickOfShip.Horizontal == 0 && ship != null)
        {
            right.SetActive(false);
            left.SetActive(false);
        }

        

        //Debug.Log(slider.value + " "+ isSliderIsPushed);
    }

    //public void chsngeScene()
    //{
    //    SceneManager.LoadScene(0);
    //}

    public void OnPointerUp(PointerEventData eventData)
    {
        //isSliderIsPushed = false;
        //slider.value = 0;
        maneuvreSound.Stop();
        isJoystickPushed = false;
        isMovePushed = false;
        //SpaceCtrlr.Instance.EngineSound.Stop();
    }


}
