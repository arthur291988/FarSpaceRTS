using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefShipBullet : MonoBehaviour
{
    // Start is called before the first frame update

    //public GameObject burst;


    private GameObject shipBulletBurst;

    private List<GameObject> shipBulletBurstListToActivate;
    //private GameObject localBulletBurst;
    //private const string SHIP_1_NAME = "1";
    //private const string SHIP_2_NAME = "2";
    //private const string SHIP_3_NAME = "3";
    //private const string SHIP_4_NAME = "4";



    //private ParticleSystem.MainModule PSmain; // is used to change the color of bullet burst effect according to its type

    //void Start() {

    //    Lists.ShipBullets.Add(gameObject);

    //    if (name.Contains("1")) shipBulletBurstListToActivate = ObjectPullerDefence.current.GetD1ShipBulletBurstPullList();
    //    else if (name.Contains("2")) shipBulletBurstListToActivate = ObjectPullerDefence.current.GetD2ShipBulletBurstPullList();
    //    else if (name.Contains("3")) shipBulletBurstListToActivate = ObjectPullerDefence.current.GetD3ShipBulletBurstPullList();
    //    else if (name.Contains("4")) shipBulletBurstListToActivate = ObjectPullerDefence.current.GetD4ShipBulletBurstPullList();
    //    //{
    //    //    localBulletBurst = burst;
    //    //    PSmain = localBulletBurst.GetComponent<ParticleSystem>().main;
    //    //    if (name.Contains(SHIP_1_NAME)) PSmain.startColor = new Color(1, 0.2884141f, 0, 1); 
    //    //    else if (name.Contains(SHIP_2_NAME)) PSmain.startColor = new Color(0.6038628f, 0, 1, 1);
    //    //    else if (name.Contains(SHIP_3_NAME)) PSmain.startColor = new Color(0, 1, 0.9071302f, 1);
    //    //    else if (name.Contains(SHIP_4_NAME)) PSmain.startColor = new Color(0, 1, 0.02998185f, 1);
    //}
    private void OnEnable()
    {
        Lists.ShipBullets.Add(gameObject);

        if (name.Contains("1")) shipBulletBurstListToActivate = ObjectPullerDefence.current.GetD1ShipBulletBurstPullList();
        else if (name.Contains("2")) shipBulletBurstListToActivate = ObjectPullerDefence.current.GetD2ShipBulletBurstPullList();
        else if (name.Contains("3")) shipBulletBurstListToActivate = ObjectPullerDefence.current.GetD3ShipBulletBurstPullList();
        else if (name.Contains("4")) shipBulletBurstListToActivate = ObjectPullerDefence.current.GetD4ShipBulletBurstPullList();
    }

    public void getBurst()
    {
        shipBulletBurst = ObjectPullerDefence.current.GetUniversalBullet(shipBulletBurstListToActivate);
        shipBulletBurst.transform.position = transform.position;
        shipBulletBurst.transform.rotation = Quaternion.identity;
        shipBulletBurst.SetActive(true);
    }

    //public GameObject getBurst()
    //{
    //    return burst;
    //}

    private void OnTriggerEnter(Collider other)
    {
        //---------- to add the other tags of allay bullets now it contains only 3 class destoyer bullet tag
        if (other.gameObject.CompareTag("GunBullPlay") || other.gameObject.CompareTag("PowerShield") || other.gameObject.CompareTag("BullDstrPlay4") || other.gameObject.CompareTag("Asteroid"))
        {
            //GameObject burstw = Instantiate(burst, transform.position, Quaternion.identity);
            shipBulletBurst = ObjectPullerDefence.current.GetUniversalBullet(shipBulletBurstListToActivate);
            shipBulletBurst.transform.position = transform.position;
            shipBulletBurst.transform.rotation = Quaternion.identity;
            shipBulletBurst.SetActive(true);

            GetComponent<TrailRenderer>().Clear();
            gameObject.SetActive(false);
            //Destroy(gameObject);

        }
    }

    private void OnDisable()
    {
        Lists.ShipBullets.Remove(gameObject);
    }

    //private void OnDestroy()
    //{
    //    Lists.ShipBullets.Remove(gameObject);
    //}


    void Update()
    {
        if (transform.position.z < -20f)
        {
            GetComponent<TrailRenderer>().Clear();
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }

    }
}
