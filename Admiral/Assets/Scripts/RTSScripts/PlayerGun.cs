using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : GunClass
{
    ////Camera camera;
    //[HideInInspector]
    //public Transform gunTransform;
    //private Transform gunTurrelTransform;
    //[HideInInspector]
    //public float attackDistance; //TO ASSIGN WHILE PULLLING
    //private List<CPUBattleShip> closeCPUBattleShips;
    //private CPUBattleShip shipToAttak;
    //private bool attackMode;
    //private float attackTimerTime;
    //[HideInInspector]
    //public float attackTimeMax; //TO ASSIGN WHILE PULLLING
    //[HideInInspector]
    //public float attackTimeMin; //TO ASSIGN WHILE PULLLING
    //[HideInInspector]
    //public float shotTime; //TO ASSIGN WHILE PULLLING
    //[HideInInspector]
    //public float HP; //TO ASSIGN WHILE PULLLING

    //private Vector3 shotDirection;
    //private GameObject gunBulletPulled;
    //private List<GameObject> gunBulletPulledList;

    //[SerializeField]
    //private Animator shotAnimator;
    //[SerializeField]
    //private ParticleSystem shotEffect;
    //[SerializeField]
    //private Transform spawnPoint;
    //private List<GameObject> BurstList;
    //private GameObject BurstReal;
    //private GameObject gunTurrel; //the tower of the gun
    //private float bullSpeed; //TO ASSIGN LATER 

    //public GameObject gunSphereVisible;
    //public GameObject gunSphereParent;

    //[SerializeField]
    //private StationClass stationOfThisGun;

    //[HideInInspector]
    //public int CPUNumber;


    //public List<GameObject> gunColorElements; //parts of gun demostrate the color of CPU
    //[HideInInspector]
    //public Color colorOfGunMat;

    // Start is called before the first frame update
    void Start()
    {
        if (name.Contains("1"))
        {
            attackTimeMin = CommonProperties.gun1MinAttackTime;
            attackTimeMax = CommonProperties.gun1MaxAttackTime;
            gunLevel = 1;
        }
        else if (name.Contains("2"))
        {
            attackTimeMin = CommonProperties.gun2MinAttackTime;
            attackTimeMax = CommonProperties.gun2MaxAttackTime;
            gunLevel = 2;
        }
        else if (name.Contains("3"))
        {
            attackTimeMin = CommonProperties.gun3MinAttackTime;
            attackTimeMax = CommonProperties.gun3MaxAttackTime;
            gunLevel = 3;
        }
        //gunTurrel = transform.GetChild(0).gameObject;
        gunTurrelTransform = gunTurrel.transform;
        attackDistance = CommonProperties.attackDistanceForGuns;
        bullSpeed = 20f;
        gunTransform = transform;
        closeBattleShips = new List<BattleShipClass>();
        shotTime = 0.5f;

    }

    private void OnDisable()
    {
        closeBattleShips.Clear();
    }

    //setting a proper color material to station color sphere
    //0-Red, 1-green, 2-blue, 3-yellow, 4-purple
    public void setProperGunColor()
    {
        //stationColorSphere.GetComponent<MeshRenderer>().material = colorOfStationMat;
        for (int i = 0; i < gunColorElements.Count; i++)
        {
            gunColorElements[i].GetComponent<MeshRenderer>().material.SetColor("_Color", colorOfGunMat);
        }
    }


    private void shotAnimationOn() => shotAnimator.SetBool("shot", true);

    private void collectTheCloseEnemyShips()
    {
        for (int i = 0; i < CommonProperties.CPUBattleShips.Count; i++)
        {
            if ((CommonProperties.CPUBattleShips[i].shipTransform.position - gunTransform.position).magnitude <= attackDistance /*&& !closeCPUBattleShips.Contains(CommonProperties.CPUBattleShips[i]) &&
                CommonProperties.CPUBattleShips[i].isActiveAndEnabled*/)
            {
                closeBattleShips.Add(CommonProperties.CPUBattleShips[i]);
            }

            //if (((CommonProperties.CPUBattleShips[i].shipTransform.position - gunTransform.position).magnitude > attackDistancePlay || !CommonProperties.CPUBattleShips[i].isActiveAndEnabled)
            //    && closeCPUBattleShips.Contains(CommonProperties.CPUBattleShips[i]))
            //{
            //    if (shipToAttak == CommonProperties.CPUBattleShips[i]) shipToAttak = null;
            //    closeCPUBattleShips.Remove(CommonProperties.CPUBattleShips[i]);

            //}
        }

        if (closeBattleShips.Count > 0)
        {
            attackMode = true;
        }
        else
        {
            attackMode = false;
            shipToAttak = null;
            if (attackTimerTime < Random.Range(attackTimeMin, attackTimeMax)) attackTimerTime = Random.Range(attackTimeMin, attackTimeMax);
        }
    }

    private IEnumerator attackTheEnemy() {
        makeAShot();
        yield return new WaitForSeconds(shotTime);
        makeAShot();
    }

    private void shotEffects()
    {

        //shotSound.Play();
        shotDirection = (spawnPoint.position - spawnPointBase.position).normalized;
        //does not matter if it is the player bullet or CPU, determination is rulled by layer by CPUNumber
        objectPulledList = ObjectPullerRTS.current.GetPlayerGun1BulletPull();
        objectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(objectPulledList);
        objectPulled.transform.position = spawnPoint.position;
        objectPulled.GetComponent<BulletPlayerGun>().CPUNumber = CPUNumber;
        objectPulled.SetActive(true);
        shotEffect.Play();
        shotAnimationOn();
        objectPulled.GetComponent<Rigidbody>().velocity = Vector3.zero; //setting impulse of bullet zero to prevent dobling it's impulse
        objectPulled.GetComponent<Rigidbody>().AddForce(shotDirection * bullSpeed, ForceMode.Impulse);

        if (gunLevel > 1) StartCoroutine(moreShots());
    }

    private IEnumerator moreShots()
    {
        //shotSound.Play();
        yield return new WaitForSeconds(0.2f);//does not matter if it is the player bullet or CPU, determination is rulled by layer by CPUNumber

        shotDirection = (spawnPoint2.position - spawnPointBase2.position).normalized;
        objectPulledList = ObjectPullerRTS.current.GetPlayerGun1BulletPull();
        objectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(objectPulledList);
        objectPulled.transform.position = spawnPoint2.position;
        objectPulled.GetComponent<BulletPlayerGun>().CPUNumber = CPUNumber;
        objectPulled.SetActive(true);
        shotEffect2.Play();
        objectPulled.GetComponent<Rigidbody>().velocity = Vector3.zero; //setting impulse of bullet zero to prevent dobling it's impulse
        objectPulled.GetComponent<Rigidbody>().AddForce(shotDirection * bullSpeed, ForceMode.Impulse);

        if (gunLevel > 2) {
            //shotSound.Play();
            yield return new WaitForSeconds(0.2f);//does not matter if it is the player bullet or CPU, determination is rulled by layer by CPUNumber
            shotDirection = (spawnPoint3.position - spawnPointBase3.position).normalized;
            objectPulledList = ObjectPullerRTS.current.GetPlayerGun1BulletPull();
            objectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(objectPulledList);
            objectPulled.transform.position = spawnPoint3.position;
            objectPulled.GetComponent<BulletPlayerGun>().CPUNumber = CPUNumber;
            objectPulled.SetActive(true);
            shotEffect3.Play();
            objectPulled.GetComponent<Rigidbody>().velocity = Vector3.zero; //setting impulse of bullet zero to prevent dobling it's impulse
            objectPulled.GetComponent<Rigidbody>().AddForce(shotDirection * bullSpeed, ForceMode.Impulse);
        }
    } 

    private void rotateTheTurrel()
    {
        gunTurrelTransform.rotation = Quaternion.Lerp(gunTurrelTransform.rotation, Quaternion.LookRotation(shipToAttak.transform.position- gunTurrelTransform.position, Vector3.up), 0.1f); 
    }

    private void makeAShot()
    {
        if (shipToAttak != null)
        {
            if (!shipToAttak.isActiveAndEnabled || (shipToAttak.shipTransform.position - gunTransform.position).magnitude > attackDistance)
            {
                if (closeBattleShips.Contains(shipToAttak)) closeBattleShips.Remove(shipToAttak);
                shipToAttak = null;
            }
            else shotEffects();
        }
        else if (closeBattleShips.Count > 0)
        {
            shipToAttak = closeBattleShips.Count < 2 ? closeBattleShips[0] : closeBattleShips[Random.Range(0, closeBattleShips.Count)];
            if (shipToAttak.isActiveAndEnabled) shotEffects();
            else shipToAttak = null;
        }

        closeBattleShips.Clear();
    }
    
    //private void disactivateThisGun()
    //{
    //    objectPulledList = ObjectPullerRTS.current.GetDestrBurstPull();
    //    objectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(objectPulledList);
    //    objectPulled.transform.position = gunTransform.position;
    //    objectPulled.SetActive(true);
    //    stationOfThisGun.stationGunLevel--;
    //    closeBattleShips.Clear();
    //    gunSphereVisible.SetActive(false);
    //    //gameObject.SetActive(false);
    //}
    //private void preBurstEffect()
    //{
    //    objectPulledList = ObjectPullerRTS.current.GetDestrPreBurstPull();
    //    objectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(objectPulledList);
    //    objectPulled.transform.position = gunTransform.position;
    //    objectPulled.SetActive(true);
    //}
    //public void reduceTheHPOfGun(float harmAmount)
    //{
    //    HP -= harmAmount;
    //    if (HP <= 4) preBurstEffect();
    //    if (HP <= 0) disactivateThisGun();
    //}

    // Update is called once per frame
    void Update()
    {
        collectTheCloseEnemyShips();
        if (attackMode && attackTimerTime > 0)
        {
            attackTimerTime -= Time.deltaTime;
        }
        if (attackMode && attackTimerTime <= 0)
        {
            attackTimerTime = Random.Range(attackTimeMin, attackTimeMax);
            StartCoroutine(attackTheEnemy());
        }

    }
    private void FixedUpdate()
    {
        if (shipToAttak != null && attackMode)
        {
            if (gunTurrelTransform.rotation != Quaternion.LookRotation(shipToAttak.transform.position - gunTurrelTransform.position, Vector3.up)) rotateTheTurrel();
        }
    }
}
