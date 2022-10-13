using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DstrEnergonBull : MonoBehaviour
{
    ////these triggers are used to identify player ship
    //private string Cruis1PlayerTag = "BullCruis1";
    //private string Cruis2PlayerTag = "BullCruis2";
    //private string Cruis3PlayerTag = "BullCruis3";
    //private string Cruis4PlayerTag = "BullCruis4";
    //private string GCruisTag = "GCruisOut";

    ////those are used to not destroy the energon bullet cause of colliding with energon ship parts right after it was created
    //private string EnergonTag = "Energon";
    //private string OtherEnergonTag = "BullCruisPlay1";

    //private string energonBulletTag = "energonBull";

    ////this one is to pull the bullet burst from the puller whent the energon bullet hits the ship
    //private List<GameObject> energonBulleBurstList;
    //GameObject energonBulleBurstReal;

    //private int energyGatherFromEnergon = 2;
    //private int energyGatherFromGuard = 3;

    //private void OnEnable()
    //{
    //    CancelInvoke(); //stop invoking any methods after the bullet is pulled from the puller
    //    Invoke("disactivateBullet",4f);
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (CompareTag(energonBulletTag))
    //    {
    //        //so if bullet hits the player ship which is determined by tag check on if block, it will inactivated and reduce the player energy and activates the burst
    //        //which is inactivated from it own function
    //        if (other.CompareTag(Cruis1PlayerTag) || other.CompareTag(Cruis2PlayerTag) || other.CompareTag(Cruis3PlayerTag) || other.CompareTag(Cruis4PlayerTag))
    //        {
    //            energonBulleBurstList = ObjectPullerJourney.current.GetEnergonBulletBurstPullPullList();
    //            energonBulleBurstReal = ObjectPullerJourney.current.GetUniversalBullet(energonBulleBurstList);
    //            energonBulleBurstReal.transform.position = transform.position;
    //            energonBulleBurstReal.SetActive(true);

    //            Lists.energyOfPlayer -= Constants.Instance.energonBullReduce;
    //            if (Lists.energyOfPlayer <= 0) Lists.energyOfPlayer = 0;
    //            SpaceCtrlr.Instance.energyCount.text = Lists.energyOfPlayer.ToString("0");
    //            SpaceCtrlr.Instance.localLaunchingObjects.hitsBeforeParalizer--;
    //            //paralizing player ship afer it gets the energon bullet and only if the player cruiser is not already paralized
    //            if (!SpaceCtrlr.Instance.localLaunchingObjects.isParalized && SpaceCtrlr.Instance.localLaunchingObjects.hitsBeforeParalizer<1) SpaceCtrlr.Instance.localLaunchingObjects.paralizingThePlayerShip(); 
    //            SpaceCtrlr.Instance.energyCount.color = Color.red;
    //            SpaceCtrlr.Instance.energyCount.transform.parent.gameObject.GetComponent<Animator>().SetBool("tokenActive", true);
    //            SpaceCtrlr.Instance.Invoke("disactAnimAndColorsOfTokens", 2.5f);

    //            gameObject.SetActive(false);
    //        }
    //        else if (!other.CompareTag(EnergonTag) && !other.CompareTag(OtherEnergonTag) && !other.CompareTag("Untagged"))
    //        {
    //            energonBulleBurstList = ObjectPullerJourney.current.GetEnergonBulletBurstPullPullList();
    //            energonBulleBurstReal = ObjectPullerJourney.current.GetUniversalBullet(energonBulleBurstList);
    //            energonBulleBurstReal.transform.position = transform.position;
    //            energonBulleBurstReal.SetActive(true);
    //            gameObject.SetActive(false);
    //        }
    //    }
    //    else {
    //        //so if bullet hits the player ship which is determined by tag check on if block, it will inactivated and reduce the player energy and activates the burst
    //        //which is inactivated from it own function
    //        if (other.CompareTag(EnergonTag) || other.CompareTag(GCruisTag))
    //        {
    //            energonBulleBurstList = ObjectPullerJourney.current.GetEnergonBulletBurstPullPullList();
    //            energonBulleBurstReal = ObjectPullerJourney.current.GetUniversalBullet(energonBulleBurstList);
    //            energonBulleBurstReal.transform.position = transform.position;
    //            energonBulleBurstReal.SetActive(true);
    //            if (other.CompareTag(EnergonTag))
    //            {
    //                //start paralizer effect on energon
    //                other.GetComponent<EnergonMngr>().paralizeShip();
    //                Lists.energyOfPlayer += energyGatherFromEnergon;
    //                SpaceCtrlr.Instance.energyCount.text = Lists.energyOfPlayer.ToString("0");

    //                SpaceCtrlr.Instance.energyCount.color = Color.green;
    //                SpaceCtrlr.Instance.energyCount.transform.parent.gameObject.GetComponent<Animator>().SetBool("tokenActive", true);
    //                SpaceCtrlr.Instance.Invoke("disactAnimAndColorsOfTokens", 2.5f);
    //            }

    //            if (other.CompareTag(GCruisTag))
    //            {
    //                other.transform.parent.gameObject.GetComponent<EnergonMngr>().paralizeShip();
    //                Lists.energyOfPlayer += energyGatherFromGuard;
    //                SpaceCtrlr.Instance.energyCount.text = Lists.energyOfPlayer.ToString("0");

    //                SpaceCtrlr.Instance.energyCount.color = Color.green;
    //                SpaceCtrlr.Instance.energyCount.transform.parent.gameObject.GetComponent<Animator>().SetBool("tokenActive", true);
    //                SpaceCtrlr.Instance.Invoke("disactAnimAndColorsOfTokens", 2.5f);
    //            }

    //            gameObject.SetActive(false);
    //        }
    //        //else if (other.CompareTag("Booster")) {
    //        //    //destroying the bullet
    //        //    energonBulleBurstList = ObjectPullerJourney.current.GetEnergonBulletBurstPullPullList();
    //        //    energonBulleBurstReal = ObjectPullerJourney.current.GetUniversalBullet(energonBulleBurstList);
    //        //    energonBulleBurstReal.transform.position = transform.position;
    //        //    energonBulleBurstReal.SetActive(true);

    //        //    gameObject.SetActive(false);

    //        //    //gaining the booster points for player
    //        //    other.gameObject.SetActive(false); //disactivating the boster to pull it from puller next time
    //        //    int x = Random.Range(0, 3);
    //        //    if (Lists.isBlackDimension)
    //        //    {
    //        //        //getting booster point depending on case so if 0 player gets the least booster points if 1 more and if 2 is most 
    //        //        Lists.boosterOfPlayer += x == 0 ? Constants.Instance.boostrGainDark : x == 1 ? Constants.Instance.boostrGainDark * 1.5f : Constants.Instance.boostrGainDark * 2;
    //        //        SpaceCtrlr.Instance.boosterCount.text = Lists.boosterOfPlayer.ToString("0");
    //        //        SpaceCtrlr.Instance.boosterCount.color = Color.green; //making a energy count green while player gathers the energy
    //        //    }
    //        //    else if (Lists.isBlueDimension)
    //        //    {
    //        //        //getting booster point depending on case so if 0 player gets the least booster points if 1 more and if 2 is most 
    //        //        Lists.boosterOfPlayer += x == 0 ? Constants.Instance.boostrGainBlue : x == 1 ? Constants.Instance.boostrGainBlue * 1.5f : Constants.Instance.boostrGainBlue * 2;
    //        //        SpaceCtrlr.Instance.boosterCount.text = Lists.boosterOfPlayer.ToString("0");
    //        //        SpaceCtrlr.Instance.boosterCount.color = Color.green; //making a energy count green while player gathers the energy
    //        //    }
    //        //    else if (Lists.isRedDimension)
    //        //    {
    //        //        //getting booster point depending on case so if 0 player gets the least booster points if 1 more and if 2 is most 
    //        //        Lists.boosterOfPlayer += x == 0 ? Constants.Instance.boostrGainRed : x == 1 ? Constants.Instance.boostrGainRed * 1.5f : Constants.Instance.boostrGainRed * 2;
    //        //        SpaceCtrlr.Instance.boosterCount.text = Lists.boosterOfPlayer.ToString("0");
    //        //        SpaceCtrlr.Instance.boosterCount.color = Color.green; //making a energy count green while player gathers the energy
    //        //    }

    //        //    SpaceCtrlr.Instance.gainSound.Play();
    //        //    SpaceCtrlr.Instance.boosterCount.transform.parent.gameObject.GetComponent<Animator>().SetBool("tokenActive", true);
    //        //    SpaceCtrlr.Instance.Invoke("disactAnimAndColorsOfTokens", 2.5f);
    //        //}
    //        //else if (other.CompareTag("EnergyBall"))
    //        //{
    //        //    //destroying the bullet
    //        //    energonBulleBurstList = ObjectPullerJourney.current.GetEnergonBulletBurstPullPullList();
    //        //    energonBulleBurstReal = ObjectPullerJourney.current.GetUniversalBullet(energonBulleBurstList);
    //        //    energonBulleBurstReal.transform.position = transform.position;
    //        //    energonBulleBurstReal.SetActive(true);

    //        //    gameObject.SetActive(false);

    //        //    other.gameObject.SetActive(false); //disactivating the boster to pull it from puller next time
    //        //    int x = Random.Range(0, 3);
    //        //    if (Lists.isBlackDimension)
    //        //    {
    //        //        //getting booster point depending on case so if 0 player gets the least booster points if 1 more and if 2 is most 
    //        //        Lists.energyOfPlayer += x == 0 ? Constants.Instance.energyGainDark : x == 1 ? Constants.Instance.energyGainDark * 1.5f : Constants.Instance.energyGainDark * 2;
    //        //        SpaceCtrlr.Instance.energyCount.text = Lists.energyOfPlayer.ToString("0");
    //        //        SpaceCtrlr.Instance.energyCount.color = Color.green; //making a energy count green while player gathers the energy
    //        //    }
    //        //    else if (Lists.isBlueDimension)
    //        //    {
    //        //        //getting booster point depending on case so if 0 player gets the least booster points if 1 more and if 2 is most 
    //        //        Lists.energyOfPlayer += x == 0 ? Constants.Instance.energyGainBlue : x == 1 ? Constants.Instance.energyGainBlue * 1.5f : Constants.Instance.energyGainBlue * 2;
    //        //        SpaceCtrlr.Instance.energyCount.text = Lists.energyOfPlayer.ToString("0");
    //        //        SpaceCtrlr.Instance.energyCount.color = Color.green; //making a energy count green while player gathers the energy
    //        //    }
    //        //    else if (Lists.isRedDimension)
    //        //    {
    //        //        //getting booster point depending on case so if 0 player gets the least booster points if 1 more and if 2 is most 
    //        //        Lists.energyOfPlayer += x == 0 ? Constants.Instance.energyGainRed : x == 1 ? Constants.Instance.energyGainRed * 1.5f : Constants.Instance.energyGainRed * 2;
    //        //        SpaceCtrlr.Instance.energyCount.text = Lists.energyOfPlayer.ToString("0");
    //        //        SpaceCtrlr.Instance.energyCount.color = Color.green; //making a energy count green while player gathers the energy
    //        //    }
    //        //    SpaceCtrlr.Instance.gainSound.Play();
    //        //    SpaceCtrlr.Instance.energyCount.transform.parent.gameObject.GetComponent<Animator>().SetBool("tokenActive", true);
    //        //    SpaceCtrlr.Instance.Invoke("disactAnimAndColorsOfTokens", 2.5f);
    //        //}
    //        else if (!other.CompareTag(Cruis1PlayerTag) && !other.CompareTag(Cruis2PlayerTag) && !other.CompareTag(Cruis3PlayerTag) && !other.CompareTag(Cruis4PlayerTag) && !other.CompareTag("Untagged"))
    //        {
    //            energonBulleBurstList = ObjectPullerJourney.current.GetEnergonBulletBurstPullPullList();
    //            energonBulleBurstReal = ObjectPullerJourney.current.GetUniversalBullet(energonBulleBurstList);
    //            energonBulleBurstReal.transform.position = transform.position;
    //            energonBulleBurstReal.SetActive(true);

    //            gameObject.SetActive(false);
    //        }
    //    }
    //}


    //private void disactivateBullet()
    //{
    //    gameObject.SetActive(false);
    //}
}
