//using System.Collections;
using UnityEngine;

public class PlayerGunBullet : MonoBehaviour
{
    private void OnEnable()
    {
        if (GetComponent<TrailRenderer>()) GetComponent<TrailRenderer>().Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        //---------- to add the other tags of allay bullets now it contains only 3 class destoyer bullet tag
        if (other.gameObject.CompareTag("BullCruis1") || other.gameObject.CompareTag("BullDstr1") || other.gameObject.CompareTag("BullParal1") ||
            other.gameObject.CompareTag("BullCruis2") || other.gameObject.CompareTag("BullDstr2") || other.gameObject.CompareTag("BullParal2") ||
            other.gameObject.CompareTag("BullCruis3") || other.gameObject.CompareTag("BullDstr3") ||
            other.gameObject.CompareTag("BullCruis4") || other.gameObject.CompareTag("BullDstr4"))
        {

            //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((
            //Destroy(gameObject);

            GetComponent<TrailRenderer>().Clear(); //to prevent painting tha wierd lines on scene when the bullet respawned from pull in othe position
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        //destrys the bullet if it is out of screen view
        if (transform.position.z > 20 || transform.position.z < -20 || transform.position.x < -55 || transform.position.x > 55 /*|| destroy*/)
        {
            //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((
            //Destroy(gameObject);
            GetComponent<TrailRenderer>().Clear(); //to prevent painting tha wierd lines on scene when the bullet respawned from pull in othe position
            gameObject.SetActive(false);
        }
    }
}
