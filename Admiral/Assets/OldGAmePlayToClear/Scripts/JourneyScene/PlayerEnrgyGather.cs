using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnrgyGather : MonoBehaviour
{
    private LineRenderer energyGatherLaser;
    private Transform energeEndPoint;
    private Transform energeStartPoint;

    //different level energons gives different rate of energy gather
    private float energyChangeRate;
    private float lvlOfEnergon; //this one is used to set energy chage rate depending on met energon

    private float reduceStartTime;
    private float slowModifierTime = 1;


    private AudioSource laserSound;

    private void OnEnable()
    {
        energyGatherLaser = GetComponent<LineRenderer>();
        energeEndPoint = transform.parent;
        energyChangeRate = Constants.Instance.energyReduce1ToNo; //default energy gather rate is 1 level
        laserSound = GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        //this one is to prevent a bug with gathering energy from nowhere after getting back to scene after the battle
        energyGatherLaser.enabled = false; //turning of the laser to capture the energy
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    //this if condition takes into account that player cruiser has on it's fleet at leas one cruiser, otherwise it will not start the gather of energy
    //    if (other.CompareTag("Energon") /*&& (Lists.Cruis1OfPlayerCruis + Lists.Cruis2OfPlayerCruis+ Lists.Cruis3OfPlayerCruis+ Lists.Cruis4OfPlayerCruis)>0*/)
    //    {
    //        if (other.GetComponent<EnergonMngr>().isParalized)
    //        {
    //            energyGatherLaser.positionCount = 2; //setting position counts to laser line of capturing the energy
    //            energeStartPoint = other.transform;
    //            energyGatherLaser.SetPosition(0, energeStartPoint.position);
    //            energyGatherLaser.SetPosition(1, energeEndPoint.position);
    //            energyGatherLaser.enabled = true; //turning on the laser to capture the energy

    //            laserSound.Play();

    //            //by following variables values changes player ship gives a siglan to energon ship to start maneuvering more frequently to avoid losing much energy (used on energonMngr class)
    //            Constants.Instance.energonDirChangeStartTime = 2f;
    //            Constants.Instance.energonDirChangeEndTime = 4f;

    //            reduceStartTime = 6;
    //            slowModifierTime = 1;
    //        }
    //    }
    //}
    //private void OnTriggerStay(Collider other)
    //{
    //    //this if condition takes into account that player cruiser has on it's fleet at leas one cruiser, otherwise it will not start the gather of energy
    //    if (other.CompareTag("Energon") && (Lists.Cruis1OfPlayerCruis + Lists.Cruis2OfPlayerCruis + Lists.Cruis3OfPlayerCruis + Lists.Cruis4OfPlayerCruis) > 0)
    //    {
    //        if (!energyGatherLaser.enabled && other.GetComponent<EnergonMngr>().isParalized)
    //        {
    //            //following block sets decent energy gather rate acording to catched energon lvl, and it is on triggerStay block cause it should gather energy with higher
    //            //rate only from according energon
    //            //lvlOfEnergon = other.GetComponent<EnergonMngr>().energonLvl;
    //            //if (lvlOfEnergon == 1) energyChangeRate = Constants.Instance.energyGain0;
    //            //else if (lvlOfEnergon == 2) energyChangeRate = Constants.Instance.energyGain1;
    //            //else if (lvlOfEnergon == 3) energyChangeRate = Constants.Instance.energyGain2;
    //            //else if (lvlOfEnergon == 4) energyChangeRate = Constants.Instance.energyGain3;

    //            //energyGatherLaser.enabled = true;

    //            //by following variables values changes player ship gives a siglan to energon ship to start maneuvering more frequently to avoid losing much energy (used on energonMngr class)
    //            Constants.Instance.energonDirChangeStartTime = 2f;
    //            Constants.Instance.energonDirChangeEndTime = 4f;


    //        }
    //        else if (energyGatherLaser.enabled && !other.GetComponent<EnergonMngr>().isParalized)
    //        {
    //            energyGatherLaser.enabled = false; //turning of the laser to capture the energy

    //            //setting a default values to maneuvering change speed for energon ship (used on energonMngr class)
    //            Constants.Instance.energonDirChangeStartTime = 8f;
    //            Constants.Instance.energonDirChangeEndTime = 10f;

    //            SpaceCtrlr.Instance.energyCount.color = new Color(0.9f, 0.9f, 0.9f, 1f); //making a energy count white when player stops gather the energy
    //        }
    //        energeStartPoint = other.transform;

    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Energon"))
    //    {
    //        energyGatherLaser.enabled = false; //turning of the laser to capture the energy

    //        //setting a default values to maneuvering change speed for energon ship (used on energonMngr class)
    //        Constants.Instance.energonDirChangeStartTime = 8f;
    //        Constants.Instance.energonDirChangeEndTime = 10f;

    //        laserSound.Stop();

    //        SpaceCtrlr.Instance.energyCount.color = new Color(0.9f, 0.9f, 0.9f, 1f); //making a energy count white when player stops gather the energy
    //    }
    //}


    //Update is called once per frame
    void Update()
    {
        if (energyGatherLaser.enabled)
        {
            Lists.energyOfPlayer += energyChangeRate;
            SpaceCtrlr.Instance.energyCount.text = Lists.energyOfPlayer.ToString("0");
            SpaceCtrlr.Instance.energyCount.color = Color.green; //making a energy count green while player gathers the energy
        }
    }

    private void FixedUpdate()
    {
        if (energyGatherLaser.enabled)
        {
            if (reduceStartTime > 0) reduceStartTime -= 0.04f; //starting the timer to reduce the energy pumping speed from energon
            else if (slowModifierTime > 0.2f) slowModifierTime -= 0.01f; //reducing the rate that player ship pumps the energy from energon

            Lists.energyOfPlayer += energyChangeRate * slowModifierTime;
            SpaceCtrlr.Instance.energyCount.text = Lists.energyOfPlayer.ToString("0");
            SpaceCtrlr.Instance.energyCount.color = Color.green; //making a energy count green while player gathers the energy
            energyGatherLaser.SetPosition(0, energeStartPoint.position);
            energyGatherLaser.SetPosition(1, energeEndPoint.position);
        }

    }
}
