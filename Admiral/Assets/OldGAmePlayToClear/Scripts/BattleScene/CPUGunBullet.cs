//using System.Collections;
using UnityEngine;

public class CPUGunBullet : MonoBehaviour
{
    private void OnEnable()
    {
        if (GetComponent<TrailRenderer>()) GetComponent<TrailRenderer>().Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        //---------- to add the other tags of allay bullets now it contains only 3 class destoyer bullet tag
        if (other.gameObject.CompareTag("BullCruisPlay1") || other.gameObject.CompareTag("BullDstrPlay1") || other.gameObject.CompareTag("BullParalPlay1") ||
            other.gameObject.CompareTag("BullCruisPlay2") || other.gameObject.CompareTag("BullDstrPlay2") || other.gameObject.CompareTag("BullParalPlay2") ||
            other.gameObject.CompareTag("BullCruisPlay3") || other.gameObject.CompareTag("BullDstrPlay3") ||
            other.gameObject.CompareTag("BullCruisPlay4") || other.gameObject.CompareTag("BullDstrPlay4"))
        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((
        //Destroy(gameObject);
        {
            GetComponent<TrailRenderer>().Clear(); //to prevent painting tha wierd lines on scene when the bullet respawned from pull in othe position
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        //destrys the bullet if it is out of screen view
        if (transform.position.z > 50 || transform.position.z < -20 || transform.position.x < -75 || transform.position.x > 75 /*|| destroy*/)
        {
            //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((
            //Destroy(gameObject);

            GetComponent<TrailRenderer>().Clear(); //to prevent painting tha wierd lines on scene when the bullet respawned from pull in othe position
            gameObject.SetActive(false);
        }
    }
}
