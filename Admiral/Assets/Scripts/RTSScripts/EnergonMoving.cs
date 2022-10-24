using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergonMoving : MonoBehaviour
{
    [SerializeField]
    private Transform movingToEmptyObj;
    [HideInInspector]
    public Transform energonTransform;
    private Quaternion nextMovingRotation;
    private Vector3 startPoint;
    private Vector3 endPoint;
    private Vector3 nextStartPoint;
    private float nextPointOnVectorStep;
    private List<Vector3> movingWayPoints;
    private int pointsCount;
    private int nextPointToMoveIndex;
    //LineRenderer wayVisualizeLine;
    private float nextRotatioLerp;
    private float ZAxisMax;
    private float ZAxisMin;
    private float XAxisMax;
    private float XAxisMin;
    private int axis; //energon moving axis

    [HideInInspector]
    public float colorRGB;

    [HideInInspector]
    public float speedOfEnergon;
    //private float colorRGBNext;
    //private float colorLerpSpeed;
    //private float colorRGBNextTime;
    private Color colorOfGlowingElements;
    [SerializeField]
    private List<MeshRenderer> energonGlowingMeshes;

    [HideInInspector]
    public GameController gameController;


    private List<GameObject> BurstList;
    private GameObject BurstReal;


    // Start is called before the first frame update
    //void Start()
    //{
    //    energonTransform = transform;
    //    colorRGB = 0.5f;
    //    colorRGBNextTime = 3f;
    //    colorRGBNext = Random.Range(1f,10f);
    //    colorLerpSpeed = Random.Range(0.001f, 0.005f);
    //    setTheColorToEnergon();
    //    wayVisualizeLine = GetComponent<LineRenderer>();
    //    nextPointToMove = 0;
    //    movingWayPoints = new List<Vector3>();
    //    pointsCount = 10;
    //    nextRotatioLerp = 0.05f;
    //    movingWayPoints = new List<Vector3>();
    //    determiningHorizontalEdgesOfGameField();
    //    populateMovingPoints();
    //    transform.position = startPoint;
    //    nextMovingRotation = Quaternion.LookRotation(movingWayPoints[0] - transform.position, Vector3.up);
    //}


    private void OnEnable()
    {
        energonTransform = transform;
        //colorRGB = Random.Range(0,2)>0 ? Random.Range(1f, 3f): Random.Range(0, 2) > 0 ? Random.Range(4f, 6f) : Random.Range(0, 2) > 0 ? Random.Range(7f, 10f) :  Random.Range(3f, 8f);
        //speedOfEnergon = colorRGB < 3f ? 1 : colorRGB < 5f ? 1.5f : colorRGB < 7f ? 2 : colorRGB < 9f ? 2.5f : 3;

        colorRGB = 4f;
        speedOfEnergon = 3f;

        nextRotatioLerp = speedOfEnergon*0.03f; //0.05f
        //colorRGBNextTime = 3f;
        //colorRGBNext = Random.Range(1f, 10f);
        //colorLerpSpeed = Random.Range(0.001f, 0.005f);
        setTheColorToEnergon();
        //wayVisualizeLine = GetComponent<LineRenderer>();
        nextPointToMoveIndex = 0;
        //movingWayPoints = new List<Vector3>();
        pointsCount = 10;
        movingWayPoints = new List<Vector3>();
        determiningHorizontalEdgesOfGameField();
        populateMovingPoints();
        energonTransform.position = startPoint;
        nextMovingRotation = Quaternion.LookRotation(movingWayPoints[0] - energonTransform.position, Vector3.up);
    }
    public void takeTheEnergyOfEnergon() {
        if (colorRGB > 3) {
            colorRGB -= 3;
            speedOfEnergon = colorRGB < 3f ? 1 : colorRGB < 5f ? 1.5f : colorRGB < 7f ? 2 : colorRGB < 9f ? 2.5f : 3;
            setTheColorToEnergon();
        }
        else  {
            disactivateEnergon(true);
        } 
    }

    private void setTheColorToEnergon()
    {
        colorOfGlowingElements = new Color(colorRGB, colorRGB, colorRGB, 1);
        for (int i = 0; i < energonGlowingMeshes.Count;i++)
        {
            energonGlowingMeshes[i].material.SetColor("_Color", colorOfGlowingElements);
        }
    }

    //private void setNextColorChangeTime()
    //{
    //    if (colorRGB > 5)
    //    {
    //        colorRGBNextTime = Random.Range(3f,7f);
    //        colorLerpSpeed = 0.005f;
    //        colorRGBNext = 0.1f;
    //    }
    //    else
    //    {
    //        colorRGBNextTime = Random.Range(1f,3f);
    //           colorRGBNext = Random.Range(2f, 10f);
    //        colorLerpSpeed = Random.Range(0.001f, 0.005f);
    //    }
    //}
    
    //function to appoint a new rotation to lerp and to change the properties of energon ship maneuvering
    private void setNextLookRotation(Vector3 looToPoint)
    {
        nextMovingRotation = Quaternion.LookRotation(looToPoint- energonTransform.position,Vector3.up);
    }

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
        //ZAxisMax += 50;
        //ZAxisMin -= 50;

        axis = Random.Range(0, 4); //0 - horizontal/left-right | 1 - horizontal/right-left | 2 -  vertical/down-up | 3 - vertical/up-down
        
        startPoint = axis == 0 ? new Vector3(XAxisMax + 50, 0, Random.Range(ZAxisMin, ZAxisMax)) : axis == 1 ? new Vector3(XAxisMin - 50, 0, Random.Range(ZAxisMin, ZAxisMax))
            : axis == 2 ? new Vector3(Random.Range(XAxisMin, XAxisMax), 0, ZAxisMax + 50) : new Vector3(Random.Range(XAxisMin, XAxisMax), 0, ZAxisMin - 50);

        endPoint = axis == 0 ? new Vector3(XAxisMin - 50, 0, Random.Range(ZAxisMin, ZAxisMax)) : axis == 1 ? new Vector3(XAxisMax + 50, 0, Random.Range(ZAxisMin, ZAxisMax))
             : axis == 2 ? new Vector3(Random.Range(XAxisMin, XAxisMax), 0, ZAxisMin - 50) : new Vector3(Random.Range(XAxisMin, XAxisMax), 0, ZAxisMax + 50);
    }

    //cut the length of game field in horizontal to the equal slices by length of turn points 
    private void populateMovingPoints() {
        for (int i = 0; i < pointsCount; i++) {
            if (i == 0) nextStartPoint = startPoint + (endPoint - startPoint)/ pointsCount;
            else nextStartPoint = nextStartPoint + (endPoint - nextStartPoint) / (pointsCount-i);
            if (axis<2) movingWayPoints.Add(new Vector3 (nextStartPoint.x, 0, Random.Range(ZAxisMin-50, ZAxisMax+50)));
            else movingWayPoints.Add(new Vector3(Random.Range(XAxisMin-50, XAxisMax+50), 0, nextStartPoint.z));
        }
        //wayVisualizeLine.positionCount = pointsCount;
        //wayVisualizeLine.SetPositions(movingWayPoints.ToArray());
    }


    private void disactivateEnergon(bool isDestroy)
    {
        if (isDestroy)
        {
            BurstList = ObjectPullerRTS.current.GetCruisBurstPull();
            BurstReal = ObjectPullerRTS.current.GetGameObjectFromPull(BurstList);
            BurstReal.transform.position = energonTransform.position;
            BurstReal.SetActive(true);
            gameController.LaunchTheEnergon();
            CommonProperties.energonsOnScene.Remove(this);
            StopCoroutine(disactivateEnergonCor());
            gameObject.SetActive(false);
        }
        else {
            StartCoroutine(disactivateEnergonCor());
        }
    }
    IEnumerator disactivateEnergonCor () {
        yield return new WaitForSeconds(20);
        //foreach (StationClass station in CommonProperties.allStations) station.shotIsMade = false;
        gameController.LaunchTheEnergon();
        CommonProperties.energonsOnScene.Remove(this);
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        energonTransform.Translate((movingToEmptyObj.position - energonTransform.position) * speedOfEnergon * Time.fixedDeltaTime, Space.World);
        energonTransform.rotation = Quaternion.Lerp(energonTransform.rotation, nextMovingRotation, nextRotatioLerp);
    }

    // Update is called once per frame
    void Update()
    {
        //energonTransform.Translate((movingToEmptyObj.position - energonTransform.position) * 1f * Time.deltaTime, Space.World);
        //energonTransform.rotation = Quaternion.Lerp(transform.rotation, nextMovingRotation, nextRotatioLerp);

        //energon moves only to point which is the child of energon itself, so it is like virtual point that is always ahead of energon, and it turns once it gets close to the destination point
        if ((movingWayPoints[nextPointToMoveIndex] - energonTransform.position).magnitude < 10 && nextPointToMoveIndex < (pointsCount - 1))
        {
            nextPointToMoveIndex++;
            setNextLookRotation(movingWayPoints[nextPointToMoveIndex]);
            if (nextPointToMoveIndex == (pointsCount - 1)) disactivateEnergon(false); 
        }

        //if (colorRGB != colorRGBNext) {
        //    colorRGB = Mathf.Lerp(colorRGB, colorRGBNext, colorLerpSpeed);
        //    setTheColorToEnergon();
        //}
        //if (colorRGBNextTime > 0) colorRGBNextTime -= Time.deltaTime;
        //else setNextColorChangeTime();
    }
}
