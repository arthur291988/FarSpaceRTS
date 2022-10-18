using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayerGun : MonoBehaviour
{
    [HideInInspector]
    public float harm;
    [HideInInspector]
    public int CPUNumber;

    private GameObject bulletBurst;
    private List<GameObject> bulletBurstPullList;
    private TrailRenderer trailRenderer;

    private void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        harm = CommonProperties.gunBulletHarm;
    }
    private void OnEnable()
    {
        CancelInvoke(); //stop invoking any methods after the bullet is pulled from the puller
        Invoke("disactivateBullet", 2f);
    }

    private void disactivateBullet()
    {
        CancelInvoke();
        gameObject.SetActive(false);
        trailRenderer.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        int layerOfOther = other.gameObject.layer;
        //CPUNumber + 10 is the lyer of player/or CPU that instantiated thiы bullet. >10 is necessary to make sure that bullet does not interact with other layers (Default for example)
        if (layerOfOther != (CPUNumber+10)&& layerOfOther>=10) {
            bulletBurstPullList = ObjectPullerRTS.current.GetGun1BulletBurstPull();
            bulletBurst = ObjectPullerRTS.current.GetGameObjectFromPull(bulletBurstPullList);
            bulletBurst.transform.position = transform.position;
            bulletBurst.SetActive(true);
            disactivateBullet();
            if (other.CompareTag("BattleShip"))
            {
               if (layerOfOther > 10) other.GetComponent<CPUBattleShip>().reduceTheHPOfShip(harm,null,null);
               else other.GetComponent<PlayerBattleShip>().reduceTheHPOfShip(harm,null,null);
            }
            //else if (other.CompareTag("PowerShield")) { 
            
            //}
        }
    }
}
