
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectingBox : Singleton<SelectingBox>
{
    public RectTransform selectingBox;
    private Camera selectCam;
    public List<LaunchingObjcts> selectableShips;
    public List<EnergonMngr> selectableEnergonAndGuards;
    //public List<EnergonMngr> selectedEnergonAndGuards;
    public List<StationController> selectableStations;

    public List<EnergonController> selectableEnergonPlayer;
    public List<EnergonController> selectableEnergonCPU;

    public List<StationController> selectableStationsPlayer;

    private Vector2 startPos;
    public bool ifAnyShipChousen;
    //public List<LineRenderer> chosenShipLineRenderer;
    public List<LaunchingObjcts> chosenShipObj;
    public List<EnergonController> chosenEnergonShipObj;


    public List<CPUShipCtrlJourney> selectableShipsCPU;

    [SerializeField]
    private RawImage connectionButtonTransform;
    float xMax;
    //float xMin;
    //float yMin;
    //float yMax;

    private void Awake()
    {
        selectCam = Camera.main;
        selectableShips = new List<LaunchingObjcts>();
        selectableShipsCPU = new List<CPUShipCtrlJourney>();
        selectableEnergonAndGuards = new List<EnergonMngr>();
        //chosenShipLineRenderer = new List<LineRenderer>();
        chosenShipObj = new List<LaunchingObjcts>();
        chosenEnergonShipObj = new List<EnergonController>();
        selectableStations = new List<StationController>();
        selectableStationsPlayer = new List<StationController>();
        selectableEnergonPlayer = new List<EnergonController>();
        selectableEnergonCPU = new List<EnergonController>();
    }

    private void Start()
    {
        xMax = connectionButtonTransform.rectTransform.position.x + connectionButtonTransform.rectTransform.sizeDelta.x;
        //xMin = connectionButtonTransform.rectTransform.position.x - connectionButtonTransform.rectTransform.sizeDelta.x;
        //yMax = connectionButtonTransform.rectTransform.position.y + connectionButtonTransform.rectTransform.sizeDelta.y;
        //yMin = connectionButtonTransform.rectTransform.position.y - connectionButtonTransform.rectTransform.sizeDelta.y;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1 && !SpaceCtrlr.Instance.anyPanelIsEnabled)
        {
            Touch _touch = Input.GetTouch(0);


            if (_touch.phase == TouchPhase.Began)
            {
                for (int i = 0; i < selectableEnergonAndGuards.Count; i++)
                {
                    selectableEnergonAndGuards[i].disactivateInfoAboutShip();

                }
                for (int i = 0; i < selectableStations.Count; i++)
                {
                    selectableStations[i].disactivateInfoAboutShip();

                }
                for (int i = 0; i < selectableStationsPlayer.Count; i++)
                {
                    selectableStationsPlayer[i].disactivateInfoAboutShip();

                }
                for (int i = 0; i < selectableShipsCPU.Count; i++)
                {
                    selectableShipsCPU[i].disactivateInfoAboutShip();
                }
                for (int i = 0; i < selectableEnergonCPU.Count; i++)
                {
                    selectableEnergonCPU[i].disactivateInfoAboutShip();
                }

                startPos = _touch.position;
            }

            if (!ifAnyShipChousen )
            {
                //if (_touch.phase == TouchPhase.Began)
                //{
                //    startPos = _touch.position;
                //}
                if (_touch.phase == TouchPhase.Moved)
                {
                    updateSelectionBox(_touch.position);
                }
                else if (_touch.phase == TouchPhase.Ended)
                {
                    ReleaseSelectionBox();
                }
            }
            else {
                if (_touch.position.x > xMax /*|| _touch.position.x < xMin || _touch.position.y > yMax || _touch.position.y < yMin*/)
                {
                    if (_touch.phase == TouchPhase.Began)
                    {
                        Vector3 moveToPoint = Input.GetTouch(0).position;
                        moveToPoint.z = selectCam.transform.position.y+41; //+41 cause the y position of scene objects is -8
                        Vector3 moveToPointFromTouch = selectCam.ScreenToWorldPoint(moveToPoint);
                        moveToPointFromTouch.y = -8f;
                        //for (int i = 0; i < chosenShipLineRenderer.Count; i++)
                        //{
                        //    chosenShipLineRenderer[i].SetPosition(1, moveToPointFromTouch);
                        //}
                        //chosenShipLineRenderer.Clear();
                        for (int i = 0; i < chosenShipObj.Count; i++)
                        {
                            chosenShipObj[i].giveAShipMoveOrder(moveToPointFromTouch);
                        }
                        for (int i = 0; i < chosenEnergonShipObj.Count; i++)
                        {
                            chosenEnergonShipObj[i].giveAShipMoveOrder(moveToPointFromTouch);
                        }
                    }
                    else if (_touch.phase == TouchPhase.Ended)
                    {
                        chosenShipObj.Clear();
                        chosenEnergonShipObj.Clear();
                        ifAnyShipChousen = false;
                        SpaceCtrlr.Instance.localLaunchingObjects = null;
                        SpaceCtrlr.Instance.chosenStation = null;
                    }
                }
                else
                {
                    for (int i = 0; i < chosenShipObj.Count; i++)
                    {
                        chosenShipObj[i].turnOffSelectedRing();
                    }
                    for (int i = 0; i < chosenEnergonShipObj.Count; i++)
                    {
                        chosenEnergonShipObj[i].turnOffSelectedRing();
                    }
                    chosenShipObj.Clear();
                    chosenEnergonShipObj.Clear();
                    ifAnyShipChousen = false;
                }
            }
            
            
        }
    }

    private void updateSelectionBox(Vector2 currentMousPos) {
        if (!selectingBox.gameObject.activeInHierarchy) selectingBox.gameObject.SetActive(true);
        float width = currentMousPos.x  - startPos.x;
        float height = currentMousPos.y - startPos.y;

        selectingBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectingBox.anchoredPosition = startPos + new Vector2(width / 2, height / 2);
    }

    private void ReleaseSelectionBox()
    {
        selectingBox.gameObject.SetActive(false);

        Vector2 min = selectingBox.anchoredPosition - (selectingBox.sizeDelta/2);
        Vector2 max = selectingBox.anchoredPosition + (selectingBox.sizeDelta/2);

        for (int i = 0; i < selectableEnergonAndGuards.Count; i++)
        {
            Vector3 screenPos = selectCam.WorldToScreenPoint(selectableEnergonAndGuards[i].transform.position);
            if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
            {
                selectableEnergonAndGuards[i].showInfoAboutShip();
            }
        }
        for (int i = 0; i < selectableStations.Count; i++)
        {
            Vector3 screenPos = selectCam.WorldToScreenPoint(selectableStations[i].transform.position);

            if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
            {
                selectableStations[i].showInfoAboutThisObjectOnCanvas();
            }
        }
        for (int i = 0; i < selectableStationsPlayer.Count; i++)
        {
            Vector3 screenPos = selectCam.WorldToScreenPoint(selectableStationsPlayer[i].transform.position);

            if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
            {
                selectableStationsPlayer[i].showInfoAboutThisObjectOnCanvas();
            }
        }
        for (int i = 0; i < selectableShipsCPU.Count; i++)
        {
            Vector3 screenPos = selectCam.WorldToScreenPoint(selectableShipsCPU[i].transform.position);

            if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
            {
                selectableShipsCPU[i].showInfoAboutShip();
            }
        }

        for (int i = 0; i < selectableShips.Count; i++)
        {
            Vector3 screenPos = selectCam.WorldToScreenPoint(selectableShips[i].transform.position);
            //Debug.Log(screenPos.x + " " + screenPos.y + " " + screenPos.z);

            if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
            {
                selectableShips[i].SelectedAndReady();
            }
        }
        for (int i = 0; i < selectableEnergonPlayer.Count; i++)
        {
            Vector3 screenPos = selectCam.WorldToScreenPoint(selectableEnergonPlayer[i].transform.position);
            //Debug.Log(screenPos.x + " " + screenPos.y + " " + screenPos.z);

            if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
            {
                selectableEnergonPlayer[i].SelectedAndReady();
            }
        }
        for (int i = 0; i < selectableEnergonCPU.Count; i++)
        {
            Vector3 screenPos = selectCam.WorldToScreenPoint(selectableEnergonCPU[i].transform.position);

            if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
            {
                selectableEnergonCPU[i].showInfoAboutShip();
            }
        }

        selectingBox.sizeDelta = Vector2.zero;
    }
}
