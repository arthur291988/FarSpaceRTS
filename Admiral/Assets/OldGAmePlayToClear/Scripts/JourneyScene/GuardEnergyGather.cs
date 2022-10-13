using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardEnergyGather : MonoBehaviour
{
    private LineRenderer energyGatherLaser;
    private Transform energeEndPoint;
    private Transform energeStartPoint;

    //this variable is set from SpaceCtrlr class while instantiating this guard 
    public float energyChageRate;

    private string Cruis1PlayerTag = "BullCruis1";
    private string Cruis2PlayerTag = "BullCruis2";
    private string Cruis3PlayerTag = "BullCruis3";
    private string Cruis4PlayerTag = "BullCruis4";
    //references to CPU journey scene cruisers
    private string Cruis1CPUTag = "BullDstrPlay1";
    private string Cruis2CPUTag = "BullDstrPlay2";
    private string Cruis3CPUTag = "BullDstrPlay3";
    private string Cruis4CPUTag = "BullDstrPlay4";

    private EnergonMngr guardMngr;

    //this one is necessary to prevent the bug of missing reference exception
    //private GameObject EnergonCruiser;
    private EnergonController EnergonCruiser;


    private AudioSource laserSound;

    private void OnEnable()
    {
        energyGatherLaser = GetComponent<LineRenderer>();
        energeEndPoint = transform.parent;
        laserSound = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        guardMngr = GetComponentInParent<EnergonMngr>();
    }


    private void OnTriggerEnter(Collider other)
    {
        //starting laser intake from player in case if it got into the guard trigger field. And if the player cruiser is not ported to station
        if (((other.CompareTag(Cruis1PlayerTag) || other.CompareTag(Cruis2PlayerTag) || other.CompareTag(Cruis3PlayerTag) || other.CompareTag(Cruis4PlayerTag)) &&
             !SpaceCtrlr.Instance.adsPanelIsOn && !other.GetComponent<LaunchingObjcts>().isPortedToPlayerStation) ||
             (other.CompareTag(Cruis1CPUTag) || other.CompareTag(Cruis2CPUTag) || other.CompareTag(Cruis3CPUTag) || other.CompareTag(Cruis4CPUTag)))
        {
            guardMngr.startAChaseProcess(other.transform); //starting to chase the player cause it got to sonar area of guard
            //energeStartPoint = other.transform;
            //playerCruiser = other.gameObject;
            //energyGatherLaser.positionCount = 2; //setting position counts to laser line of capturing the energy
            //energyGatherLaser.SetPosition(0, energeStartPoint.position);
            //energyGatherLaser.SetPosition(1, energeEndPoint.position);
            //energyGatherLaser.enabled = true; //turning on the laser to capture the energy
            //laserSound.Play();
        }
        else if (other.CompareTag("Energon"))
        {
            EnergonCruiser = other.GetComponent<EnergonController>();
            if (EnergonCruiser.CPUNumber!=5) {
                energeStartPoint = other.transform;
                energyGatherLaser.positionCount = 2; //setting position counts to laser line of capturing the energy
                energyGatherLaser.SetPosition(0, energeStartPoint.position);
                energyGatherLaser.SetPosition(1, energeEndPoint.position);
                energyGatherLaser.enabled = true; //turning on the laser to capture the energy
                laserSound.Play();
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //starting laser intake from player in case if it got into the guard trigger field . And if the player cruiser is not ported to station
        if (other.CompareTag("Energon"))
        {
            if (!energyGatherLaser.enabled)
            {
                EnergonCruiser = other.GetComponent<EnergonController>();
                energyGatherLaser.enabled = true;
            }
            energeStartPoint = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Energon"))
        {
            EnergonCruiser = null;
            energyGatherLaser.enabled = false; //turning of the laser to capture the energy
        }
    }

    private void FixedUpdate()
    {
        if (energyGatherLaser.enabled && EnergonCruiser != null)
        {
            if (EnergonCruiser.energyOfEnergon > 0)
            {
                EnergonCruiser.energyOfEnergon -= energyChageRate;
                EnergonCruiser.updateInfoPanelToDisplay();
            }
            if (EnergonCruiser.energyOfEnergon < 0)
            {
                EnergonCruiser.energyOfEnergon = 0;
                EnergonCruiser.updateInfoPanelToDisplay();
            }
            energyGatherLaser.SetPosition(0, energeStartPoint.position);
            energyGatherLaser.SetPosition(1, energeEndPoint.position);
        }
    }
}
