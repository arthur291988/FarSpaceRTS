using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationAttackButton : MonoBehaviour
{

    public static List<StationPlayerRTS> playerStations = new List<StationPlayerRTS>();
    [SerializeField]
    private Button attackButton;
    [SerializeField]
    private Material attackButtonActiveMat;
    private Image buttonImage;
    //[SerializeField]
    //private GameObject aimingToEnergon;
    //private aimToEnergon aimingToEnergonClass;
    private StationPlayerRTS shotStation;

    //public void addStationToButton(StationPlayerRTS station, EnergonMoving energon)
    //{
    //    playerStations.Add(station);
    //    if (!attackButton.interactable)
    //    {
    //        attackButton.interactable = true;
    //        //aimingToEnergonClass.energon = energon.gameObject;
    //        //aimingToEnergonClass.restTheAiming();
    //        //aimingToEnergon.SetActive(true);
    //    }
    //    buttonImage.material = attackButtonActiveMat;
    //}
    //public void removeStationFromButton(StationPlayerRTS station)
    //{
    //    playerStations.Remove(station);
    //    if (playerStations.Count < 1)
    //    {
    //        attackButton.interactable = false;
    //        //aimingToEnergon.SetActive(false);
    //        buttonImage.material = null;
    //    }
    //}

    //public void makeAShot()
    //{
    //    for (int i = 0; i < playerStations.Count; i++)
    //    {
    //        if (i == 0) shotStation = playerStations[i];
    //        else if (playerStations[i].distanceToEnergon() < shotStation.distanceToEnergon()) shotStation = playerStations[i];
    //    }
    //    if (playerStations.Count > 0)
    //    {
    //        shotStation.makeAShotFromStation(shotStation.groupWhereTheStationIs != null /*&& shotStation.groupWhereTheStationIs.Count > 0*/);
    //        shotStation.shotIsMade = true;
    //        shotStation.fillingLineRecharge.localPosition = new Vector3(-6f, 0, 0);
    //        playerStations.Remove(shotStation);
    //        if (playerStations.Count < 1)
    //        {
    //            attackButton.interactable = false;
    //            //aimingToEnergon.SetActive(false);
    //            buttonImage.material = null;
    //        }
    //    }

    //}

    // Start is called before the first frame update
    void Start()
    {
        //aimingToEnergonClass = aimingToEnergon.GetComponent<aimToEnergon>();
        buttonImage = attackButton.image;
    }
}
