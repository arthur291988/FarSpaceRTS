using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergonShotMngr : MonoBehaviour
{
    public List<Transform> shotObj; //to trigger any ship that enters to shot area

    //this one is to pull the bullet from the puller
    //private List<GameObject> energonBulletList;

    //private GameObject energonBulletReal;

    private string Cruis1PlayerTag = "BullCruis1";
    private string Cruis2PlayerTag = "BullCruis2";
    private string Cruis3PlayerTag = "BullCruis3";
    private string Cruis4PlayerTag = "BullCruis4";

    //private Vector3 shotDirection;

    //private EnergonMngr energonMngr;

    //this vars is set in SpaceCtrlr class while instantiating the energon ship
    public float shotSpeed;
    public float shotCounts;
    public float shotBullSpeed;

    //private AudioSource shotSound;

    // Start is called before the first frame update
    void Start()
    {
       // energonMngr = GetComponentInParent<EnergonMngr>();
        shotObj = new List<Transform>();
        //this snipped adds the transform collections of current energonShotMngr class to make possible to clear them from SpaceCtrlr class while destroying player cruiser
        //SpaceCtrlr.Instance.energonShotAimTransforms.Add(shotObj);
        //if (SpaceCtrlr.Instance.CruisJourneyReal != null) Invoke("shootingTheBullet", shotSpeed);
        //shotSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Cruis1PlayerTag) || other.CompareTag(Cruis2PlayerTag) || other.CompareTag(Cruis3PlayerTag) || other.CompareTag(Cruis4PlayerTag)) {
            shotObj.Add(other.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Cruis1PlayerTag) || other.CompareTag(Cruis2PlayerTag) || other.CompareTag(Cruis3PlayerTag) || other.CompareTag(Cruis4PlayerTag))
        {
            shotObj.Remove(other.transform);
        }
    }


    //IEnumerator extraShot(float seconds) {
    //    yield return new WaitForSeconds(seconds);
    //    if (shotObj.Count > 0) //this one here to preven a bug of out of range of collection cause the player ship may be out of trigger of energon and collection will be empty
    //    {
    //        shotDirection = shotObj[0].transform.position * 1.1f - transform.position * 1.1f;

    //        shotSound.Play();
    //        energonBulletList = ObjectPullerJourney.current.GetEnergonBulletPullPullList();
    //        energonBulletReal = ObjectPullerJourney.current.GetUniversalBullet(energonBulletList);
    //        energonBulletReal.transform.position = transform.position;
    //        energonBulletReal.SetActive(true);

    //        energonBulletReal.GetComponent<Rigidbody>().velocity = Vector3.zero; //setting impulse of bullet zero to prevent dobling it's impulse
    //        energonBulletReal.GetComponent<Rigidbody>().AddForce(shotDirection.normalized * shotBullSpeed, ForceMode.Impulse);

    //    }
    //}

    //private void shootingTheBullet() {
    //    if (shotObj.Count > 0 && SpaceCtrlr.Instance.CruisJourneyReal != null && !energonMngr.isParalized && !SpaceCtrlr.Instance.adsPanelIsOn)
    //    {
    //        shotDirection = shotObj[0].transform.position - transform.position;

    //        energonBulletList = ObjectPullerJourney.current.GetEnergonBulletPullPullList();
    //        energonBulletReal = ObjectPullerJourney.current.GetUniversalBullet(energonBulletList);
    //        energonBulletReal.transform.position = transform.position;
    //        energonBulletReal.SetActive(true);

    //        energonBulletReal.GetComponent<Rigidbody>().velocity = Vector3.zero; //setting impulse of bullet zero to prevent dobling it's impulse
    //        energonBulletReal.GetComponent<Rigidbody>().AddForce(shotDirection.normalized * shotBullSpeed, ForceMode.Impulse);

    //        shotSound.Play();

    //        if (shotCounts > 1)
    //        {
    //            StartCoroutine(extraShot(0.5f));
    //        }
    //        if (shotCounts > 2)
    //        {
    //            StartCoroutine(extraShot(1f));
    //        }
    //    }
    //    if (SpaceCtrlr.Instance.CruisJourneyReal != null)Invoke("shootingTheBullet", shotSpeed);
    //}

}
