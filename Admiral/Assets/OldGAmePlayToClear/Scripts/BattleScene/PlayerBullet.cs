//using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    //public GameObject burst;

    private string C1Bull = "BullCruisPlay1";
    private string D1Bull = "BullDstrPlay1";
    private string C1PBull = "BullParalPlay1";
    private string C2Bull = "BullCruisPlay2";
    private string D2Bull = "BullDstrPlay2";
    private string C2PBull = "BullParalPlay2";
    private string C3Bull = "BullCruisPlay3";
    private string D3Bull = "BullDstrPlay3";
    private string C4Bull = "BullCruisPlay4";
    private string D4Bull = "BullDstrPlay4";

    //private bool destroy = false;

    private List<GameObject> bulletBurstToActivate;
    private GameObject bullBurstLocal;

    private void OnEnable()
    {
        if (GetComponent<TrailRenderer>()) GetComponent<TrailRenderer>().Clear();
    }

    public /*GameObject*/ void getBurst()
    {
        bullBurstLocal = ObjectPuller.current.GetUniversalBullet(bulletBurstToActivate);
        bullBurstLocal.transform.position = transform.position;
        bullBurstLocal.transform.rotation = Quaternion.identity;
        bullBurstLocal.SetActive(true);
        /*return burst;*/
    }

    private void Start()
    {
        if (CompareTag(C1Bull)) bulletBurstToActivate = ObjectPuller.current.GetCruiser1BulletBurstPull();
        else if (CompareTag(C1PBull)) bulletBurstToActivate = ObjectPuller.current.GetDestr1BulletBurstPull();
        else if (CompareTag(C2Bull)) bulletBurstToActivate = ObjectPuller.current.GetCruiser2BulletBurstPull();
        else if (CompareTag(C2PBull)) bulletBurstToActivate = ObjectPuller.current.GetDestr2BulletBurstPull();
        else if (CompareTag(C3Bull)) bulletBurstToActivate = ObjectPuller.current.GetCruiser3BulletBurstPull();
        else if (CompareTag(C4Bull)) bulletBurstToActivate = ObjectPuller.current.GetCruiser4BulletBurstPull();
        else if (CompareTag(D1Bull)) bulletBurstToActivate = ObjectPuller.current.GetDestr1BulletBurstPull();
        else if (CompareTag(D2Bull)) bulletBurstToActivate = ObjectPuller.current.GetDestr2BulletBurstPull();
        else if (CompareTag(D3Bull)) bulletBurstToActivate = ObjectPuller.current.GetDestr3BulletBurstPull();
        else if (CompareTag(D4Bull)) bulletBurstToActivate = ObjectPuller.current.GetDestr4BulletBurstPull();

        //Invoke("destroyTime", 7f);
    }

    //private void Start()
    //{
    //    //StartCoroutine(destroyTime());
    //}
    private void OnTriggerEnter(Collider other)
    {
        //---------- to add the other tags of allay bullets now it contains only 3 class destoyer bullet tag
        if (other.gameObject.CompareTag("BullCruis1") || other.gameObject.CompareTag("BullDstr1") || other.gameObject.CompareTag("BullParal1") ||
            other.gameObject.CompareTag("BullCruis2") || other.gameObject.CompareTag("BullDstr2") || other.gameObject.CompareTag("BullParal2") ||
            other.gameObject.CompareTag("BullCruis3") || other.gameObject.CompareTag("BullDstr3") || other.gameObject.CompareTag("PowerShieldCPU") ||
            other.gameObject.CompareTag("BullCruis4") || other.gameObject.CompareTag("BullDstr4") || other.gameObject.CompareTag("GunBullCPU") || 
            other.gameObject.CompareTag("Asteroid"))
        {
            //GameObject burstw = Instantiate(burst, transform.position, Quaternion.identity);
            bullBurstLocal = ObjectPuller.current.GetUniversalBullet(bulletBurstToActivate);
            bullBurstLocal.transform.position = transform.position;
            bullBurstLocal.transform.rotation = Quaternion.identity;
            bullBurstLocal.SetActive(true);


            //Destroy(gameObject);

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
    //}
    private void Update()
    {
        //destrys the bullet if it is out of screen view
        if (transform.position.z > 40 || transform.position.z < -20 || transform.position.x < -75 || transform.position.x > 75 || transform.position.y > 50)
        {
            //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((
            //Destroy(gameObject);

            //to prevent painting tha wierd lines on scene when the bullet respawned from pull in othe position. And the check is to prevent bug with paral bullet 
            //(cause it does not have a trail)
            if (GetComponent<TrailRenderer>()) GetComponent<TrailRenderer>().Clear();
            gameObject.SetActive(false);
            
        }
    }
}
