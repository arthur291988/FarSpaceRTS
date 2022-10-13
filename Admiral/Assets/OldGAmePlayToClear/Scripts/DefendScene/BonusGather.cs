
using UnityEngine;

public class BonusGather : MonoBehaviour
{
    private bool isBull = false;
    private bool isHp = false;
    private bool isShield = false;

    private void OnEnable()
    {
        //setting proper identification bool and material color for outer glow sphere 
        if (name.Contains("BulletPref"))
        {
            isBull = true;
            GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(1.06f, 0.02f, 0, 0)); //red
        }
        else if (name.Contains("ShieldPref"))
        {
            isShield = true;
            GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(0, 0.96f, 1.06f, 0));//cyan
        }
        else
        {
            isHp = true;
            GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(0.17f, 1.06f, 0, 0));//green
        }
        GetComponent<Rigidbody>().velocity = Vector3.zero;

    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GunBullPlay") || other.gameObject.CompareTag("Respawn"))
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
            if (isHp) DefBarrelCtrlr.bonusHP = true;
            if (isBull) GunShotButt.bonusBullet = true;
            if (isShield) GunShotButt.bonusShield = true;
        }
    }
    private void Update()
    {
        if (transform.position.y < -400) gameObject.SetActive(false); //Destroy(gameObject);
    }
}
