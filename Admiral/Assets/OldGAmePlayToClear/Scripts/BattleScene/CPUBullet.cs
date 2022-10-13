using System.Collections.Generic;
using UnityEngine;

public class CPUBullet : MonoBehaviour
{
    //public GameObject burst;

    //private bool destroy = false;

    private string C1Bull = "BullCruis1";
    private string D1Bull = "BullDstr1";
    private string C1PBull = "BullParal1";
    private string C2Bull = "BullCruis2";
    private string D2Bull = "BullDstr2";
    private string C2PBull = "BullParal2";
    private string C3Bull = "BullCruis3";
    private string D3Bull = "BullDstr3";
    private string C4Bull = "BullCruis4";
    private string D4Bull = "BullDstr4";

    private List<GameObject> bulletBurstToActivate;
    private GameObject bullBurstLocal;

    private void OnEnable()
    {
        if (GetComponent<TrailRenderer>()) GetComponent<TrailRenderer>().Clear();
    }

    private void Start()
    {
        if (CompareTag(C1Bull)) bulletBurstToActivate = ObjectPuller.current.GetCruiser1BulletBurstPull();
        else if (CompareTag(C1PBull)) bulletBurstToActivate = ObjectPuller.current.GetCruiser1BulletBurstPull();
        else if (CompareTag(C2Bull)) bulletBurstToActivate = ObjectPuller.current.GetCruiser2BulletBurstPull();
        else if (CompareTag(C2PBull)) bulletBurstToActivate = ObjectPuller.current.GetCruiser2BulletBurstPull();
        else if (CompareTag(C3Bull)) bulletBurstToActivate = ObjectPuller.current.GetCruiser3BulletBurstPull();
        else if (CompareTag(C4Bull)) bulletBurstToActivate = ObjectPuller.current.GetCruiser4BulletBurstPull();
        else if (CompareTag(D1Bull)) bulletBurstToActivate = ObjectPuller.current.GetDestr1BulletBurstPull();
        else if (CompareTag(D2Bull)) bulletBurstToActivate = ObjectPuller.current.GetDestr2BulletBurstPull();
        else if (CompareTag(D3Bull)) bulletBurstToActivate = ObjectPuller.current.GetDestr3BulletBurstPull();
        else if (CompareTag(D4Bull)) bulletBurstToActivate = ObjectPuller.current.GetDestr4BulletBurstPull();

        //Invoke("destroyTime", 7f);
    }


    public void getBurst()
    {
        bullBurstLocal = ObjectPuller.current.GetUniversalBullet(bulletBurstToActivate);
        bullBurstLocal.transform.position = transform.position;
        bullBurstLocal.transform.rotation = Quaternion.identity;
        bullBurstLocal.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        //---------- to add the other tags of allay bullets now it contains only 3 class destoyer bullet tag
        if (other.gameObject.CompareTag("BullCruisPlay1") || other.gameObject.CompareTag("BullDstrPlay1") || other.gameObject.CompareTag("BullParalPlay1") ||
            other.gameObject.CompareTag("BullCruisPlay2") || other.gameObject.CompareTag("BullDstrPlay2") || other.gameObject.CompareTag("BullParalPlay2") ||
            other.gameObject.CompareTag("BullCruisPlay3") || other.gameObject.CompareTag("BullDstrPlay3") || other.gameObject.CompareTag("BullCruisPlay4") ||
             other.gameObject.CompareTag("BullDstrPlay4") || other.gameObject.CompareTag("GunBullPlay") || other.gameObject.CompareTag("PowerShield")
             || other.gameObject.CompareTag("Asteroid"))
        {

            //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((
            //GameObject burstw = Instantiate(burst, transform.position, Quaternion.identity);
            //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((
            //Destroy(gameObject);
            bullBurstLocal = ObjectPuller.current.GetUniversalBullet(bulletBurstToActivate);
            bullBurstLocal.transform.position = transform.position;
            bullBurstLocal.transform.rotation = Quaternion.identity;
            bullBurstLocal.SetActive(true);

            //to prevent painting tha wierd lines on scene when the bullet respawned from pull in othe position. And the check is to prevent bug with paral bullet 
            //(cause it does not have a trail)
            if (GetComponent<TrailRenderer>()) GetComponent<TrailRenderer>().Clear(); 
            gameObject.SetActive(false);
            CancelInvoke();
        }
    }

    //private void OnDestroy()
    //{
    //    GameObject burstw = Instantiate(burst, transform.position, Quaternion.identity);
    //    //Lists.CPUBullets.Remove(gameObject);
    //}
    private void Update()
    {
        //destrys the bullet if it is out of screen view
        if (transform.position.z > 70 || transform.position.z < -20 || transform.position.x < -75 || transform.position.x > 75 || transform.position.y > 50)
        {
            //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((
            //Destroy(gameObject);

            //to prevent painting tha wierd lines on scene when the bullet respawned from pull in othe position. And the check is to prevent bug with paral bullet 
            //(cause it does not have a trail)
            if (GetComponent<TrailRenderer>()) GetComponent<TrailRenderer>().Clear(); 
            gameObject.SetActive(false);
            //GameObject burstw = Instantiate(burst, transform.position, Quaternion.identity);
        }
    }
}
