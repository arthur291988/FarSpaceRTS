
using UnityEngine;
using System.Collections.Generic;

public class energyBallMngr : MonoBehaviour
{
    //private float countDown;
    [HideInInspector]
    public float energyOfThisEnergyBall;
    //private GameObject infoPanelLocal;
    //private List<GameObject> infoPanelLocalListToActivate;
    //private MiniInfoPanel miniInfoPanelObject;
    //private bool isSelected;
    private void OnEnable()
    {
        if (Lists.isBlackDimension)
        {
            energyOfThisEnergyBall = Random.Range(Constants.Instance.energyBallEnergyDark - 50, Constants.Instance.energyBallEnergyDark + 50);
        }
        else if (Lists.isBlueDimension)
        {
            energyOfThisEnergyBall = Random.Range(Constants.Instance.energyBallEnergyBlue - 50, Constants.Instance.energyBallEnergyBlue + 50);
        }
        else if (Lists.isRedDimension)
        {
            energyOfThisEnergyBall = Random.Range(Constants.Instance.energyBallEnergyRed - 50, Constants.Instance.energyBallEnergyRed + 50);
        }
    }
    //private void OnDisable()
    //{
    //    isSelected = false;
    //    if (infoPanelLocal != null)
    //    {
    //        if (infoPanelLocal.activeInHierarchy) infoPanelLocal.SetActive(false);
    //        infoPanelLocal = null;
    //    }
    //}

    //public void showInfoAboutShip()
    //{
    //    infoPanelLocalListToActivate = ObjectPullerJourney.current.GetMiniInfoPanelNoFleetPullList();
    //    if (infoPanelLocal != null)
    //    {
    //        if (infoPanelLocal.activeInHierarchy) infoPanelLocal.SetActive(false);
    //        infoPanelLocal = null;
    //    }
    //    infoPanelLocal = ObjectPullerJourney.current.GetUniversalBullet(infoPanelLocalListToActivate);
    //    miniInfoPanelObject = infoPanelLocal.GetComponent<MiniInfoPanel>();
    //    infoPanelLocal.transform.position = SpaceCtrlr.Instance.mainCam.WorldToScreenPoint(transform.position);
    //    miniInfoPanelObject.energyCount.text = energyOfThisEnergyBall.ToString("0");
    //    isSelected = true;
    //    infoPanelLocal.SetActive(true);
    //}

    //public void disactivateInfoAboutShip()
    //{
    //    isSelected = false;
    //    if (infoPanelLocal != null)
    //    {
    //        if (infoPanelLocal.activeInHierarchy) infoPanelLocal.SetActive(false);
    //        infoPanelLocal = null;
    //    }
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    countDown += Time.deltaTime;
    //    if (Lists.isBlackDimension)
    //    {
    //        if (countDown >= 17)
    //        {
    //            gameObject.SetActive(false);
    //            countDown = 0;
    //        }
    //    }
    //    if (Lists.isBlueDimension)
    //    {
    //        if (countDown >= 15)
    //        {
    //            gameObject.SetActive(false);
    //            countDown = 0;
    //        }
    //    }
    //    if (Lists.isRedDimension)
    //    {
    //        if (countDown >= 12)
    //        {
    //            gameObject.SetActive(false);
    //            countDown = 0;
    //        }
    //    }
    //}
}
