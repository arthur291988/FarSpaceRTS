using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelectingBox : Singleton<UISelectingBox>
{
    public RectTransform selectingBox;
    private Camera selectCamera;
    [HideInInspector]
    public List<PlayerBattleShip> selectablePlayerBattleShipsObject;
    private Vector2 startPos;
    [HideInInspector]
    public bool ifAnyShipChousen;
    [HideInInspector]
    public List<PlayerBattleShip> chosenPlayerBattleShipsObject;
    private List<PlayerBattleShip> chosenCruisers;
    private List<Vector3> squardPositions;
    private float radiusGroup;
    private int innnerCircleMax;
    private MegaAttackController megaAttackController;
    private float yMax;
    private float xMax;
    [SerializeField]
    private Image edgeButtonImg;
    private bool toolsPanelIsTouched;


    //private GameObject lowEnegryImg;


    [SerializeField]
    private Image deselectToken;


    private void Awake()
    {
        toolsPanelIsTouched = false;
        selectCamera = Camera.main;
        megaAttackController = FindObjectOfType<MegaAttackController>();
        selectablePlayerBattleShipsObject = new List<PlayerBattleShip>();
        chosenPlayerBattleShipsObject = new List<PlayerBattleShip>();
        chosenCruisers = new List<PlayerBattleShip>();
        squardPositions = new List<Vector3>();
        innnerCircleMax = 8; //excep the ship in center
        radiusGroup = 3;
    }

    private void Start()
    {
        //lowEnegryImg = /*ConnectionPLayerStations.Instance.lowEnegryImg;*/
           yMax = edgeButtonImg.rectTransform.anchoredPosition.y + edgeButtonImg.rectTransform.sizeDelta.y;
        xMax = (edgeButtonImg.rectTransform.anchoredPosition.x - edgeButtonImg.rectTransform.sizeDelta.x*2)+Screen.width;

        //Debug.Log(Screen.width);

        //Debug.Log(edgeButtonImg.rectTransform.anchoredPosition.y + "   edgeButtonImg.rectTransform.anchoredPosition.y");
        //Debug.Log(edgeButtonImg.rectTransform.anchoredPosition.x + "  edgeButtonImg.rectTransform.anchoredPosition.x");
        //Debug.Log(edgeButtonImg.rectTransform.sizeDelta.x + "  edgeButtonImg.rectTransform.sizeDelta.x");

        //Debug.Log(yMax + "   YYY Rect");
        //Debug.Log(xMax + "   XXX Rect");
    }

   

    public bool checkIfTouchNotInsideUIPanel(Vector2 pos) {
        if (pos.x > xMax && pos.y < yMax) return false;
        else return true;
    } 

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch _touch = Input.GetTouch(0);


            if (_touch.phase == TouchPhase.Began)
            {
                //Debug.Log(_touch.position.y + "   YYY");
                //Debug.Log(_touch.position.x + "   XXX");


                startPos = _touch.position;
                if (checkIfTouchNotInsideUIPanel(_touch.position))
                {
                    chosenCruisers.Clear();
                    megaAttackController.megaAttackButton.interactable = false;
                }
                else
                {
                    toolsPanelIsTouched = true;
                }
                activateOrDisactivateReleaseSelectionToken(false);
            }

            if (!ifAnyShipChousen)
            {
                //if tools panel is touched (the area in bottom of screen) no selecting box is updated
                if (_touch.phase == TouchPhase.Moved)
                {
                    if (!toolsPanelIsTouched && !ConnectionPLayerStations.Instance.lineIsDruggedFromPlayerStation) updateSelectionBox(_touch.position);
                }
                else if (_touch.phase == TouchPhase.Ended)
                {
                    ReleaseSelectionBox();
                }

            }

            else
            {

                if (_touch.phase == TouchPhase.Began)
                {
                    if (checkIfTouchNotInsideUIPanel(_touch.position))
                    {
                        //Vector3 moveToPoint = Input.GetTouch(0).position;
                        //moveToPoint.z = selectCamera.transform.position.y+32; //+41 cause the y position of scene objects is 0
                        //Vector3 moveToPointFromTouch = selectCamera.ScreenToWorldPoint(moveToPoint);
                        //moveToPointFromTouch.y = 0;

                        Vector3 moveToPointFromTouch = Vector3.zero;
                        Plane plane = new Plane(Vector3.up, 0);
                        float distance;
                        Ray ray = Camera.main.ScreenPointToRay(_touch.position);
                        if (plane.Raycast(ray, out distance))
                        {
                            moveToPointFromTouch = ray.GetPoint(distance);
                        }




                        float stepForOuterRadius = 1;

                        if (chosenPlayerBattleShipsObject.Count > 1)
                        {
                            for (int i = 0; i < chosenPlayerBattleShipsObject.Count; i++)
                            {
                                if (i == 0)
                                {
                                    squardPositions.Add(moveToPointFromTouch);
                                }
                                else if (i <= innnerCircleMax)
                                {
                                    if (radiusGroup != 3) radiusGroup = 3;
                                    Vector3 newPos;
                                    float step = (Mathf.PI * 2) / innnerCircleMax; // отступ
                                    newPos.x = moveToPointFromTouch.x + Mathf.Sin(step * i) * radiusGroup; // по оси X
                                    newPos.z = moveToPointFromTouch.z + Mathf.Cos(step * i) * radiusGroup; // по оси Z
                                    newPos.y = 0; // по оси Y всегда 0
                                    squardPositions.Add(newPos);
                                    if (i == innnerCircleMax)
                                    {
                                        if ((chosenPlayerBattleShipsObject.Count - squardPositions.Count) > innnerCircleMax * 2) stepForOuterRadius = innnerCircleMax * 2;
                                        else stepForOuterRadius = chosenPlayerBattleShipsObject.Count - squardPositions.Count;
                                    }
                                }
                                else if (i <= (innnerCircleMax * 3))
                                {
                                    if (radiusGroup != 6) radiusGroup = 6;
                                    Vector3 newPos;
                                    float step = (Mathf.PI * 2) / stepForOuterRadius; // отступ
                                    newPos.x = moveToPointFromTouch.x + Mathf.Sin(step * i) * radiusGroup; // по оси X
                                    newPos.z = moveToPointFromTouch.z + Mathf.Cos(step * i) * radiusGroup; // по оси Z
                                    newPos.y = 0; // по оси Y всегда 0
                                    squardPositions.Add(newPos);
                                    if (i == (innnerCircleMax * 3))
                                    {
                                        if ((chosenPlayerBattleShipsObject.Count - squardPositions.Count) > innnerCircleMax * 3) stepForOuterRadius = innnerCircleMax * 3;
                                        else stepForOuterRadius = chosenPlayerBattleShipsObject.Count - squardPositions.Count;
                                    }
                                }
                                else if (i <= (innnerCircleMax * 7))
                                {
                                    if (radiusGroup != 9) radiusGroup = 9;
                                    Vector3 newPos;
                                    float step = (Mathf.PI * 2) / stepForOuterRadius; // отступ
                                    newPos.x = moveToPointFromTouch.x + Mathf.Sin(step * i) * radiusGroup; // по оси X
                                    newPos.z = moveToPointFromTouch.z + Mathf.Cos(step * i) * radiusGroup; // по оси Z
                                    newPos.y = 0; // по оси Y всегда 0
                                    squardPositions.Add(newPos);
                                    if (i == (innnerCircleMax * 7))
                                    {
                                        if ((chosenPlayerBattleShipsObject.Count - squardPositions.Count) > innnerCircleMax * 4) stepForOuterRadius = innnerCircleMax * 4;
                                        else stepForOuterRadius = chosenPlayerBattleShipsObject.Count - squardPositions.Count;
                                    }
                                }
                                else if (i <= (innnerCircleMax * 15))
                                {
                                    if (radiusGroup != 12) radiusGroup = 12;
                                    Vector3 newPos;
                                    float step = (Mathf.PI * 2) / stepForOuterRadius; // отступ
                                    newPos.x = moveToPointFromTouch.x + Mathf.Sin(step * i) * radiusGroup; // по оси X
                                    newPos.z = moveToPointFromTouch.z + Mathf.Cos(step * i) * radiusGroup; // по оси Z
                                    newPos.y = 0; // по оси Y всегда 0
                                    squardPositions.Add(newPos);
                                }
                            }
                            for (int i = 0; i < chosenPlayerBattleShipsObject.Count; i++)
                            {
                                chosenPlayerBattleShipsObject[i].giveAShipMoveOrder(squardPositions[i]);
                            }
                        }
                        else
                        {
                            chosenPlayerBattleShipsObject[0].giveAShipMoveOrder(moveToPointFromTouch);
                        }
                    }
                    else
                    {
                        toolsPanelIsTouched = true;
                        for (int i = 0; i < chosenPlayerBattleShipsObject.Count; i++)
                        {
                            chosenPlayerBattleShipsObject[i].deselectWithoutMoveOrders();
                        }
                        chosenPlayerBattleShipsObject.Clear();
                        squardPositions.Clear();
                        ifAnyShipChousen = false;
                        activateOrDisactivateReleaseSelectionToken(false);
                    }
                }
                else if (_touch.phase == TouchPhase.Ended)
                {
                    //if (lowEnegryImg.activeInHierarchy) lowEnegryImg.SetActive(false);
                    chosenPlayerBattleShipsObject.Clear();
                    squardPositions.Clear();
                    ifAnyShipChousen = false;
                    toolsPanelIsTouched = false;
                    activateOrDisactivateReleaseSelectionToken(false);
                }
            }

        }

        
    }

    private void activateOrDisactivateReleaseSelectionToken(bool isActive) {
        if (isActive) deselectToken.color = new Color(0.7f, 0, 0.04f, 1);
        else deselectToken.color = new Color(0.7f, 0, 0.04f, 0.1f);
    }

    private void updateSelectionBox(Vector2 currentMousPos)
    {
        if (!selectingBox.gameObject.activeInHierarchy) selectingBox.gameObject.SetActive(true);
        float width = currentMousPos.x - startPos.x;
        float height = currentMousPos.y - startPos.y;

        selectingBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectingBox.anchoredPosition = startPos + new Vector2(width / 2, height / 2);
    }

    private void assignTheCruiserToMegaAttack() {
        if (chosenCruisers.Count > 0)
        {
            if (chosenCruisers.Count < 2) megaAttackController.chosenCruiserToMegaAttack = chosenCruisers[0];
            else megaAttackController.chosenCruiserToMegaAttack = chosenCruisers[Random.Range(0, chosenCruisers.Count)];
            megaAttackController.megaAttackButton.interactable = true;
        }
        else
        {
            megaAttackController.megaAttackButton.interactable = false;
        }
        chosenCruisers.Clear();
    }

    private void ReleaseSelectionBox()
    {
        selectingBox.gameObject.SetActive(false);

        Vector2 min = selectingBox.anchoredPosition - (selectingBox.sizeDelta / 2);
        Vector2 max = selectingBox.anchoredPosition + (selectingBox.sizeDelta / 2);

        for (int i = 0; i < selectablePlayerBattleShipsObject.Count; i++)
        {
            Vector3 screenPos = selectCamera.WorldToScreenPoint(selectablePlayerBattleShipsObject[i].transform.position);

            if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
            {
                selectablePlayerBattleShipsObject[i].SelectedAndReady();
                if (selectablePlayerBattleShipsObject[i].isCruiser) chosenCruisers.Add(selectablePlayerBattleShipsObject[i]);
                if (deselectToken.color.a<1) activateOrDisactivateReleaseSelectionToken(true);
            }
        }
        if (megaAttackController.megaAttackTimer<=0) assignTheCruiserToMegaAttack();
        selectingBox.sizeDelta = Vector2.zero;
        toolsPanelIsTouched = false;
    }


}
