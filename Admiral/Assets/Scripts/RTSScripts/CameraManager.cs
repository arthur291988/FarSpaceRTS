
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private new Camera camera;
    private Vector3 touchStartPosition;
    private Vector3 cameraStartPosition;

    private Vector3 touchMovedPosition;
    private Vector3 cameraMovedPosition;

    private float startDistance;

    //private float fingerMovementsSumm;

    //public bool isPerspective;

    private bool afterStartIsMoved;

    private Vector3 moveToPlayerPoint;

    private Transform cameraTransform;


    private float ZAxisMax;
    private float ZAxisMin;
    private float XAxisMax;
    private float XAxisMin;

    //[SerializeField]
    //private SpriteRenderer CloseStarsRend;
    //[SerializeField]
    //private SpriteRenderer MiddleStarsRend;
    //[SerializeField]
    //private SpriteRenderer FarStarsRend;

    //[SerializeField]
    //private Material CloseStarsMat;
    //[SerializeField]
    //private Material MiddletarsMat;
    //[SerializeField]
    //private Material FarStarsMat;

    public bool PanelIsClosed;

    void Start()
    {
        PanelIsClosed = false;
        //CloseStarsMat = CloseStarsRend.GetComponent<Material>();
        //MiddletarsMat = MiddleStarsRend.GetComponent<Material>();
        //FarStarsMat = FarStarsRend.GetComponent<Material>();

        if (Lists.isContinued || Lists.AllSceneObjects.Count > 0) afterStartIsMoved = true;
        camera = GetComponent<Camera>();
        cameraTransform = camera.transform;
        //transform.position = new Vector3(0, cameraTransform.position.y, 0);

        //camera.orthographic = false;
        cameraTransform.rotation = Quaternion.Euler(60, cameraTransform.rotation.eulerAngles.y, 0);
        cameraTransform.position = new Vector3(0, 170, 108);
        camera.fieldOfView = 40;
        //isPerspective = true;

        moveToPlayerPoint = new Vector3(Constants.Instance.playerCruiserStartPos.x, cameraTransform.position.y, Constants.Instance.playerCruiserStartPos.z);
        //cameraTransform.position = moveToPlayerPoint;
        Invoke ("determiningHorizontalEdgesOfGameField",0.3f);
    }

    ////this method is assigned to camera button on journey scene and changes the camera views on scene from action camera to high view camera
    //public void changeViewCamera()
    //{
    //    if (!CommonProperties.stationPanelIsActive)
    //    {
    //        //virtualCamera.GetComponent<VirtualCamCtrlr>().changeTheCameraViewOnJourneyCruiser();
    //        //if (camera.orthographic)
    //        //{
    //        //camera.orthographic = false;
    //        cameraTransform.rotation = Quaternion.Euler(60, cameraTransform.rotation.eulerAngles.y, 0);
    //        cameraTransform.position = new Vector3(90, 170, 164);
    //        camera.fieldOfView = 40;
    //        //isPerspective = true;
    //        //    }
    //        //    else
    //        //    {
    //        //        camera.orthographic = true;
    //        //        cameraTransform.rotation = Quaternion.Euler(90, cameraTransform.rotation.eulerAngles.y, 0);
    //        //        cameraTransform.position = new Vector3(95, 170, 60);
    //        //        camera.orthographicSize = 70;
    //        //        isPerspective = false;
    //        //    }
    //    }
    //}

    private void determiningHorizontalEdgesOfGameField()
    {
        for (int i = 0; i < CommonProperties.allStations.Count; i++)
        {
            if (i == 0)
            {
                XAxisMax = CommonProperties.allStations[i].stationPosition.x;
                XAxisMin = CommonProperties.allStations[i].stationPosition.x;
                ZAxisMax = CommonProperties.allStations[i].stationPosition.z;
                ZAxisMin = CommonProperties.allStations[i].stationPosition.z;
            }
            else
            {
                if (CommonProperties.allStations[i].stationPosition.x > XAxisMax) XAxisMax = CommonProperties.allStations[i].stationPosition.x;
                if (CommonProperties.allStations[i].stationPosition.x < XAxisMin) XAxisMin = CommonProperties.allStations[i].stationPosition.x;
                if (CommonProperties.allStations[i].stationPosition.z > ZAxisMax) ZAxisMax = CommonProperties.allStations[i].stationPosition.z;
                if (CommonProperties.allStations[i].stationPosition.z < ZAxisMin) ZAxisMin = CommonProperties.allStations[i].stationPosition.z;
            }
        }
        for (int i = 0; i < CommonProperties.stars.Count; i++)
        {
            if (CommonProperties.stars[i].starPosition.x > XAxisMax) XAxisMax = CommonProperties.stars[i].starPosition.x;
            if (CommonProperties.stars[i].starPosition.x < XAxisMin) XAxisMin = CommonProperties.stars[i].starPosition.x;
            if (CommonProperties.stars[i].starPosition.z > ZAxisMax) ZAxisMax = CommonProperties.stars[i].starPosition.z;
            if (CommonProperties.stars[i].starPosition.z < ZAxisMin) ZAxisMin = CommonProperties.stars[i].starPosition.z;

        }
        //borders of camera are far to 100 pxls from edges
        XAxisMin -= 100;
        XAxisMax += 100;
        //this is necessary cause default position of camera uses z axis 108 (because of prospective view)
        ZAxisMax += cameraTransform.position.z+100;
        ZAxisMin -= cameraTransform.position.z - 100;
    }

    private void FixedUpdate()
    {
        if (PanelIsClosed && camera.fieldOfView != 40f)
        {
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, 40f, 0.1f);
            if (camera.fieldOfView > 35f) PanelIsClosed = false;
        }

        //if (!afterStartIsMoved && !Lists.isContinued && Lists.AllSceneObjects.Count == 0)
        //{
        //    cameraTransform.position = Vector3.MoveTowards(transform.position, moveToPlayerPoint, 1);
        //    if (cameraTransform.position == moveToPlayerPoint) afterStartIsMoved = true;
        //}
        //if (Input.touchCount == 2 || CommonProperties.stationPanelIsActive)
        //{
        //    CloseStarsMat.SetVector("_Offset", new Vector2(cameraTransform.position.x * -0.1f, cameraTransform.position.z * -0.1f));
        //    CloseStarsMat.SetVector("_OffsetMid", new Vector2(cameraTransform.position.x * -0.03f, cameraTransform.position.z * -0.03f));

        //    //MiddletarsMat.SetVector("_Offset", new Vector2(cameraTransform.position.x * -0.03f, cameraTransform.position.z * -0.03f));
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (!CommonProperties.stationPanelIsActive)
        {
            if (Input.touchCount == 2)
            {
                // get current touch positions
                Touch tZero = Input.GetTouch(0);
                Touch tOne = Input.GetTouch(1);

                if (tZero.phase == TouchPhase.Began || tOne.phase == TouchPhase.Began)
                {
                    touchStartPosition = tZero.position;
                    touchStartPosition.z = cameraTransform.position.y;
                    cameraStartPosition = camera.ScreenToWorldPoint(touchStartPosition);
                    startDistance = Vector2.Distance(tZero.position, tOne.position);
                }
                else if (tZero.phase == TouchPhase.Moved && tOne.phase == TouchPhase.Moved)
                {
                    if (!afterStartIsMoved) afterStartIsMoved = true;
                    // get touch position from the previous frame
                    Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
                    Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;

                    float oldTouchDistance = Vector2.Distance(tZeroPrevious, tOnePrevious);
                    float currentTouchDistance = Vector2.Distance(tZero.position, tOne.position);

                    //if (!isPerspective)
                    //{
                    //    touchMovedPosition = tZero.position;
                    //    touchMovedPosition.z = cameraTransform.position.y;
                    //    cameraMovedPosition = camera.ScreenToWorldPoint(touchMovedPosition) - cameraStartPosition;
                    //    transform.position -= cameraMovedPosition;
                    //}
                    //else
                    //{
                    touchMovedPosition = tZero.position;
                    touchMovedPosition.z = cameraTransform.position.y;
                    cameraMovedPosition = camera.ScreenToWorldPoint(touchMovedPosition) - cameraStartPosition;
                    cameraTransform.position -= new Vector3(cameraMovedPosition.x, 0, cameraMovedPosition.z);
                    if (cameraTransform.position.x > XAxisMax) cameraTransform.position = new Vector3(XAxisMax, 170, cameraTransform.position.z);
                    if (cameraTransform.position.x < XAxisMin) cameraTransform.position = new Vector3(XAxisMin, 170, cameraTransform.position.z);
                    if (cameraTransform.position.z > ZAxisMax) cameraTransform.position = new Vector3(cameraTransform.position.x, 170, ZAxisMax);
                    if (cameraTransform.position.z < ZAxisMin) cameraTransform.position = new Vector3(cameraTransform.position.x, 170, ZAxisMin);
                    //}



                    if (startDistance + 50 < currentTouchDistance || startDistance - 50 > currentTouchDistance)
                    {
                        // get offset value
                        float deltaDistance = oldTouchDistance - currentTouchDistance;

                        //if (!isPerspective)
                        //{
                        //    camera.orthographicSize += deltaDistance * 0.1f;
                        //    camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, 5, 150);
                        //}
                        //else
                        //{
                        camera.fieldOfView += deltaDistance * 0.1f;
                        camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 10, 47);
                        //}
                    }
                    //else
                    //{
                    //}
                }
            }
            //if (Input.touchCount == 3)
            //{
            //    // get current touch positions
            //    Touch tZero = Input.GetTouch(0);
            //    Touch tOne = Input.GetTouch(1);
            //    Touch tTwo = Input.GetTouch(2);

            //    //if (tZero.phase == TouchPhase.Began || tOne.phase == TouchPhase.Began || tTwo.phase == TouchPhase.Began)
            //    //{
            //    //    //zeroStartPosX = tZero.position.x;
            //    //    //oneStartPosX = tOne.position.x;
            //    //    //twoStartPosX = tTwo.position.x;
            //    //    //touchStartPosition = tZero.position;
            //    //    //touchStartPosition.z = transform.position.y;
            //    //    //cameraStartPosition = camera.ScreenToWorldPoint(touchStartPosition);
            //    //    //startDistance = Vector2.Distance(tZero.position, tOne.position);
            //    //}
            //    //else 

            //    if (tZero.phase == TouchPhase.Moved || tOne.phase == TouchPhase.Moved || tTwo.phase == TouchPhase.Moved)
            //    {
            //        // get touch position from the previous frame
            //        float tZeroPrevious = tZero.deltaPosition.x;
            //        float tOnePrevious = tOne.deltaPosition.x;
            //        float tTwoPrevious = tTwo.deltaPosition.x;

            //        //Debug.Log(tZeroPrevious);
            //        //Debug.Log(tOnePrevious);
            //        //Debug.Log(tTwoPrevious);

            //        if (tZeroPrevious < 0 && tOnePrevious < 0 && tTwoPrevious > 0) fingerMovementsSumm = tZeroPrevious + tOnePrevious;
            //        else if (tZeroPrevious < 0 && tTwoPrevious < 0 && tOnePrevious > 0) fingerMovementsSumm = tZeroPrevious + tTwoPrevious;
            //        else if (tTwoPrevious < 0 && tOnePrevious < 0 && tZeroPrevious > 0) fingerMovementsSumm = tTwoPrevious + tOnePrevious;
            //        else if (tZeroPrevious > 0 && tOnePrevious > 0 && tTwoPrevious < 0) fingerMovementsSumm = tZeroPrevious + tOnePrevious;
            //        else if (tZeroPrevious > 0 && tTwoPrevious > 0 && tOnePrevious < 0) fingerMovementsSumm = tZeroPrevious + tTwoPrevious;
            //        else if (tTwoPrevious > 0 && tOnePrevious > 0 && tZeroPrevious < 0) fingerMovementsSumm = tTwoPrevious + tOnePrevious;
            //        //.Rotate(0, fingerMovementsSumm * 0.1f, 0);
            //        //transform.rotation.eulerAngles.y = transform.rotation.eulerAngles.y
            //        //transform.rotation.y += fingerMovementsSumm * 0.1f;

            //        if (!isPerspective)
            //        {
            //            cameraTransform.rotation = Quaternion.Euler(90, cameraTransform.rotation.eulerAngles.y + fingerMovementsSumm * 0.1f, 0);
            //        }
            //        else
            //        {
            //            cameraTransform.rotation = Quaternion.Euler(60, cameraTransform.rotation.eulerAngles.y + fingerMovementsSumm * 0.1f, 0);
            //        }
            //        //transform.Rotate(0, tOnePrevious * 0.1f, 0);
            //    }
            //}
        }
    }
}
