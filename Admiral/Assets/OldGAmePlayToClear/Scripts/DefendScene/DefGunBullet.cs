
using UnityEngine;
using System.Collections.Generic;

public class DefGunBullet : MonoBehaviour
{
    private List<GameObject> GunBulletBurstListToActivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Asteroid")) {

            GetComponent<TrailRenderer>().Clear();
            gameObject.SetActive(false);

            GunBulletBurstListToActivate = ObjectPullerDefence.current.GetGunBullBurstPullList();
            GameObject burst = ObjectPullerDefence.current.GetUniversalBullet(GunBulletBurstListToActivate);
            burst.transform.position = transform.position;
            burst.transform.rotation = Quaternion.identity;
            burst.SetActive(true);
        }
    }

    void Update()
    {
        if (transform.position.z > 1350f || transform.position.z < -40f || transform.position.x > 2300f || transform.position.x < -2300f || transform.position.y > 600f || transform.position.y < -600f)
        //Destroy(gameObject);
        {
            GetComponent<TrailRenderer>().Clear();
            gameObject.SetActive(false);
        }
    }
}
