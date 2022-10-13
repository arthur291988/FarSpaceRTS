using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnergyBall : MonoBehaviour
{
    //private string EnergonTag = "Energon";
    //private string GCruisTag = "GCruisOut";

    //private void OnTriggerEnter(Collider other)
    //{if (other.CompareTag(EnergonTag) || other.CompareTag(GCruisTag))
    //    {

    //        for (int i = 0; i < SpaceCtrlr.Instance.aimingObjects.Count; i++)
    //        {
    //            SpaceCtrlr.Instance.aimingObjects[i].GetComponent<EnergonMngr>().isChasingEnergyBall = false;
    //        }

    //        gameObject.SetActive(false);

    //        SpaceCtrlr.Instance.energyBallsBigObjects.Remove(gameObject.transform);
    //        //if there is another energy ball on scene, player ship that captured previou energy ball sends a sygnal to all guards and energons that there is another energy ball to chase it
    //        if (SpaceCtrlr.Instance.energyBallsBigObjects.Count > 0)
    //        {
    //            for (int i = 0; i < SpaceCtrlr.Instance.aimingObjects.Count; i++)
    //            {
    //                EnergonMngr energonMngr = SpaceCtrlr.Instance.aimingObjects[i].GetComponent<EnergonMngr>();
    //                if (!energonMngr.isParalized)
    //                {
    //                    energonMngr.startChaseOfEnergyBall();
    //                }
    //            }
    //        }
    //    }
    //}
}
