
using UnityEngine;
using System.Collections.Generic;

public class GunPlayer : MonoBehaviour
{
    public Transform spawnPoint3;
    public Transform spawnPoint2;
    public Transform spawnPoint;

    public GameObject aimingPos;
    //public GameObject bullet;

    private Vector3 lookDirection;
    private Vector3 gunPosition;

    private float rotationSpeed;
    private float rotationAngle;
    private float repeatTime;

    private float shotDirModifier = 1; // is used to increase shot power, CONSIDER TO ASSIGN PARTICULAR GUN PROPERTIES

    private GameObject buletReal;
    private GameObject buletReal2;
    private GameObject buletReal3;

    //this lists are necessary to assign proper bullet types for gun and pull the bullet from ObjectPuller class
    private List<GameObject> gunBulletsListToActivate;

    private bool moveRight;

    // Start is called before the first frame update
    void Start()
    {
        gunPosition = transform.localPosition;
        if (name.Contains("Single"))
        {
            if (name.Contains("CPU")) gunBulletsListToActivate = ObjectPuller.current.GetG1BulletsCPUPull();
            else gunBulletsListToActivate = ObjectPuller.current.GetG1BulletsPlayerPull();
            repeatTime = Constants.Instance.SINGLE_REPEAT_TIME;
            rotationAngle = Constants.Instance.SINGLE_ROTATION_ANGLE;
            rotationSpeed = Constants.Instance.SINGLE_ROTATION_SPEED;
            AttackBulletsSingle();
        }
        if (name.Contains("Dual"))
        {
            if (name.Contains("CPU")) gunBulletsListToActivate = ObjectPuller.current.GetG2BulletsCPUPull();
            else gunBulletsListToActivate = ObjectPuller.current.GetG2BulletsPlayerPull();
            repeatTime = Constants.Instance.DUAL_REPEAT_TIME;
            rotationAngle = Constants.Instance.DUAL_ROTATION_ANGLE;
            rotationSpeed = Constants.Instance.DUAL_ROTATION_SPEED;
            AttackBulletsDual();
        }
        if (name.Contains("Triple"))
        {
            if (name.Contains("CPU")) gunBulletsListToActivate = ObjectPuller.current.GetG3BulletsCPUPull();
            else gunBulletsListToActivate = ObjectPuller.current.GetG3BulletsPlayerPull();
            repeatTime = Constants.Instance.TRIPLE_REPEAT_TIME;
            rotationAngle = Constants.Instance.TRIPLE_ROTATION_ANGLE;
            rotationSpeed = Constants.Instance.TRIPLE_ROTATION_SPEED;
            AttackBulletsTriple();
        }

        if (Random.Range(0f, 1f) < 0.5f) moveRight = true;
        else moveRight = false;

        if (name.Contains("CPU")) shotDirModifier = -1f;
    }

    public void startInvocation() {
        if (name.Contains("Single")) AttackBulletsSingle();
        if (name.Contains("Dual")) AttackBulletsDual();
        if (name.Contains("Triple")) AttackBulletsTriple();
    }

    private void AttackBulletsSingle() {

        //shipParaliserReal = ParalShipDstr; TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
        //buletReal = Instantiate(bullet, spawnPoint.position, Quaternion.identity); 
        buletReal = ObjectPuller.current.GetUniversalBullet(gunBulletsListToActivate);
        //buletReal.GetComponent<TrailRenderer>().Clear();
        buletReal.transform.position = spawnPoint.position;
        buletReal.transform.rotation = Quaternion.identity;
        buletReal.SetActive(true);

        buletReal.GetComponent<Rigidbody>().velocity = Vector3.zero; // set velosity of bullet zero first to prvent doubling its impuls after first shot
        buletReal.GetComponent<Rigidbody>().AddForce(lookDirection * shotDirModifier*1.5f, ForceMode.Impulse);
        //buletReal.GetComponent<TrailRenderer>().enabled = true;
        Invoke("AttackBulletsSingle", repeatTime);
    }

    private void AttackBulletsDual()
    {
        //TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
        //buletReal = Instantiate(bullet, spawnPoint.position, Quaternion.identity);

        //setting enabled bullets of guns from the pull of gun bullets
        buletReal = ObjectPuller.current.GetUniversalBullet(gunBulletsListToActivate);
        //buletReal.GetComponent<TrailRenderer>().Clear();
        buletReal.transform.position = spawnPoint.position;
        buletReal.transform.rotation = Quaternion.identity;
        buletReal.SetActive(true);

        buletReal.GetComponent<Rigidbody>().velocity = Vector3.zero; // set velosity of bullet zero first to prvent doubling its impuls after first shot
        buletReal.GetComponent<Rigidbody>().AddForce(lookDirection* shotDirModifier*1.4f, ForceMode.Impulse);
        //buletReal.GetComponent<TrailRenderer>().enabled = true;


        //TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
        //buletReal2 = Instantiate(bullet, spawnPoint2.position, Quaternion.identity);

        //setting enabled bullets of guns from the pull of gun bullets
        buletReal2 = ObjectPuller.current.GetUniversalBullet(gunBulletsListToActivate);
        //buletReal2.GetComponent<TrailRenderer>().Clear();
        buletReal2.transform.position = spawnPoint2.position;
        buletReal2.transform.rotation = Quaternion.identity;
        buletReal2.SetActive(true);

        buletReal2.GetComponent<Rigidbody>().velocity = Vector3.zero; // set velosity of bullet zero first to prvent doubling its impuls after first shot
        buletReal2.GetComponent<Rigidbody>().AddForce(lookDirection* shotDirModifier * 1.6f, ForceMode.Impulse);
        //buletReal2.GetComponent<TrailRenderer>().enabled = true;

        Invoke("AttackBulletsDual", repeatTime);
    }

    private void AttackBulletsTriple()
    {
        //TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
        //buletReal = Instantiate(bullet, spawnPoint.position, Quaternion.identity);

        //setting enabled bullets of guns from the pull of gun bullets
        buletReal = ObjectPuller.current.GetUniversalBullet(gunBulletsListToActivate);
        //buletReal.GetComponent<TrailRenderer>().Clear();
        buletReal.transform.position = spawnPoint.position;
        buletReal.transform.rotation = Quaternion.identity;
        buletReal.SetActive(true);

        buletReal.GetComponent<Rigidbody>().velocity = Vector3.zero; // set velosity of bullet zero first to prvent doubling its impuls after first shot
        buletReal.GetComponent<Rigidbody>().AddForce(lookDirection * shotDirModifier * 1.8f, ForceMode.Impulse);
        //buletReal.GetComponent<TrailRenderer>().enabled = true;

        //TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
        //buletReal2 = Instantiate(bullet, spawnPoint2.position, Quaternion.identity);
        //setting enabled bullets of guns from the pull of gun bullets
        buletReal2 = ObjectPuller.current.GetUniversalBullet(gunBulletsListToActivate);
        //buletReal2.GetComponent<TrailRenderer>().Clear();
        buletReal2.transform.position = spawnPoint2.position;
        buletReal2.transform.rotation = Quaternion.identity;
        buletReal2.SetActive(true);

        buletReal2.GetComponent<Rigidbody>().velocity = Vector3.zero; // set velosity of bullet zero first to prvent doubling its impuls after first shot
        buletReal2.GetComponent<Rigidbody>().AddForce(lookDirection * shotDirModifier * 1.6f, ForceMode.Impulse);
        //buletReal2.GetComponent<TrailRenderer>().enabled = true;

        //TO DELETE IF PULLER WILL WORK FINE )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))
        //buletReal3 = Instantiate(bullet, spawnPoint3.position, Quaternion.identity);
        //setting enabled bullets of guns from the pull of gun bullets
        buletReal3 = ObjectPuller.current.GetUniversalBullet(gunBulletsListToActivate);
        //buletReal3.GetComponent<TrailRenderer>().Clear();
        buletReal3.transform.position = spawnPoint3.position;
        buletReal3.transform.rotation = Quaternion.identity;
        buletReal3.SetActive(true);

        buletReal3.GetComponent<Rigidbody>().velocity = Vector3.zero; // set velosity of bullet zero first to prvent doubling its impuls after first shot
        buletReal3.GetComponent<Rigidbody>().AddForce(lookDirection * shotDirModifier * 1.4f, ForceMode.Impulse);
        //buletReal3.GetComponent<TrailRenderer>().enabled = true;

        Invoke("AttackBulletsTriple", repeatTime);
    }

    private void Update()
    {
        if (aimingPos.transform.localPosition.x > rotationAngle) moveRight = false;
        if (aimingPos.transform.localPosition.x < -rotationAngle) moveRight = true;

        //if (transform.localRotation.y>50) moveRight = false;
        //if (transform.localRotation.y<-50) moveRight = true;

        //if (moveRight) aimingPos.transform.tran;
        //else aimingPos.transform.Translate(aimingPos.transform.localPosition.x - 1, 0, 0);

        //if move right the empty point of ship which direction its shots moves in dimension (in local dimension) 
        if (moveRight) aimingPos.transform.Translate (rotationSpeed * Time.deltaTime, 0, 0, Space.Self);
        else aimingPos.transform.Translate(-rotationSpeed * Time.deltaTime, 0, 0, Space.Self);
        lookDirection = aimingPos.transform.localPosition - gunPosition;

        //shotDirection = aimingPos.transform.localPosition - gunWorldPosition;
        transform.localRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
    }

}
