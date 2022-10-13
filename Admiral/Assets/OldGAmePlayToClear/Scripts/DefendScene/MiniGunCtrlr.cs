using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGunCtrlr : MonoBehaviour
{
    private Vector3 dircetionToBullet; //is used to rotate miniGun barrel toward bullets
    private Vector3 shotDirection;
    private float xRotation;
    private float yRotation;

    public Transform spawnPoint;

    //public GameObject barrelOfBigGun;
    //public GameObject bullet;
    private GameObject shotBulletReal;

    private GameObject attackObject;

    public ParticleSystem gunShot;

    private Vector3 gunStartPos;
    private Vector3 gunReadyPos;
    private Quaternion startRotation;

    private bool isDetecting = true; 
    private bool isDestroying = false;
    private bool isRotating = false;
    private bool isPuttingUp = false;
    private bool isPuttingDown = false;

    public GameObject aimingPoint;
    public AudioSource servoSound;

    //private Vector3 aimingVector;
    //private Vector3 bigGunPosition;
    //private Quaternion from;
    //private Quaternion to;

    //private bool fire = false;

    private AudioSource shotSound;
    private AudioSource moveSound;

    private int attackCounts;

    //this lists are necessary to assign proper object and pull that object from ObjectPullerDefence class
    private List<GameObject> miniGunBulletListToActivate;

    // Start is called before the first frame update
    void Start()
    {
        miniGunBulletListToActivate = ObjectPullerDefence.current.GetGMiniBullList();
        Lists.MiniGunsOnScene.Add(transform.parent.gameObject);
        startRotation = gameObject.transform.localRotation;
        gunStartPos = gameObject.transform.localPosition;
        gunReadyPos = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y+0.13f, gameObject.transform.localPosition.z);
        shotSound = GetComponent<AudioSource>();
    }


    IEnumerator reloadMiniGun() {
        yield return new WaitForSeconds(2f);
        isDetecting = true;
    }

    private void attackPointing()
    {
        //it works only if there is at least one opposite side ship on battlefield
        if (!isDetecting && !isDestroying)
        {
            if (!gunShot.isPlaying) gunShot.Play(); //shot effect particle sysetem
            if (!shotSound.isPlaying) shotSound.Play(); //shot effect sound
            float accuracy = Random.Range(0.01f, -0.01f); //this randome is for 3 class destroyers only, it determines the accuracy of the shot (higher - is less accurate)

            //so shot direction is subtraction of ships point vector from enemys point vector (with accuracy correction)
            shotDirection = aimingPoint.transform.position + new Vector3(accuracy, 0, 0) - transform.position;

            //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
            //spawning the bullet and adding it an impulse of shot ot fly
            //shotBulletReal = Instantiate(bullet, spawnPoint.position, Quaternion.Euler(0, 0, 0));
            shotBulletReal = ObjectPullerDefence.current.GetUniversalBullet(miniGunBulletListToActivate);
            shotBulletReal.transform.position = spawnPoint.position;
            shotBulletReal.transform.rotation = Quaternion.Euler(0, 0, 0);
            shotBulletReal.SetActive(true);

            shotBulletReal.GetComponent<Rigidbody>().velocity = Vector3.zero; //setting imoulse of bullet zero to prevent dobling it's impulse
            shotBulletReal.GetComponent<Rigidbody>().AddForce(shotDirection * 200, ForceMode.Impulse);

            attackCounts--; //that one is necessary to limit one attack batch of bullets with 20. So mini gun makes pause after each 20 bullets 

            //this one is invocing this method again after randome time which is set for Dstr3 class, so it is recursion of this method
            if (!IsInvoking("attackPointing") && !isDestroying && attackCounts>0) Invoke("attackPointing", 0.05f);
        }
    }


    private void FixedUpdate()
    {
        if (isRotating && attackObject != null)
        {
            xRotation = Quaternion.LookRotation(dircetionToBullet, Vector3.up).eulerAngles.x;
            yRotation = Quaternion.LookRotation(dircetionToBullet, Vector3.up).eulerAngles.y;

            gameObject.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDetecting && Lists.ShipBullets.Count>0) {

            attackObject = Lists.ShipBullets[Random.Range(0, Lists.ShipBullets.Count)];
            isPuttingUp = true;
            isRotating = true;
            isDetecting = false;
            dircetionToBullet = attackObject.transform.position - transform.position;
            servoSound.Play();
            attackCounts = 20;
            Invoke("attackPointing", Random.Range(0.5f,1.5f));
        }
        if (attackCounts < 1 && isRotating)
        {
            StartCoroutine(reloadMiniGun());
            isPuttingDown = true;
            isRotating = false;
            gunShot.Stop();
            shotSound.Stop();
            transform.rotation = startRotation;
        }

        if (isPuttingUp) {
            transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, gunReadyPos, 0.3f);
            if (transform.localPosition == gunReadyPos)
            {
                isPuttingUp = false;
            }
        }

        if (isPuttingDown) {
            transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, gunStartPos, 0.3f);
            if (transform.localPosition == gunStartPos)
            {
                isPuttingDown = false;
            }
        }
        
    }
}
