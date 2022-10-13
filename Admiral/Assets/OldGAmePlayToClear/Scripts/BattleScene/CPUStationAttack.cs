using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUStationAttack : MonoBehaviour
{
    //are used to instantiate the bullet to shot
    private GameObject shotBulletReal;
    private GameObject shotBulletReal2; //for double bullet shot ships

    private bool isDestroying = false; // used to stop all ship processes when it is on process of destroying (cause it takes 2 secs to destroy ship GO)

    public ParticleSystem shotEffect; //this effect playes when current ship makes a shot to emitate energetic light of shot 

    private string station4Tag = "StationA";
    private string station3Tag = "StationD";
    private string station2Tag = "StationC";
    private string station1Tag = "StationFed";
    private string stationGTag = "StationGuard";

    //those controle the features of exact ship
    private float ACCURE_RANGE;
    private float ATTACK_TIME_FROM;

    //are used to direct the bullet shot
    private Vector3 shotDirection;

    //is used to give a push tu bullet and turn off the fixed update once bullet pushed
    private bool fire = false;

    //ships bullet force (speed of flight)
    private float SHIP_ATTACK_FORCE;

    //shot sound
    private AudioSource shotShound;


    //to paint the elements of ship to decent color
    public List<GameObject> IDColorElements;

    //this lists are necessary to assign proper bullet types from ships and pull the bullets from ObjectPuller class
    private List<GameObject> bulletsListToActivate;

    private bool doubleBullet = false; //to use with stations that have double bullet shot features like Station C, Fed and Guard

    void Start()
    {
        //Stations as CRUISERS
        if (name.Contains(station4Tag))
        {
            ACCURE_RANGE = Constants.Instance.CRUIS_4_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.CRUIS_4_ATTACK_TIME;
            SHIP_ATTACK_FORCE = Constants.Instance.CRUIS_4_ATTACK_FORCE;

            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            if (Lists.isPlayerStationOnDefence) bulletsListToActivate = ObjectPuller.current.GetS4BulletPlayerList();
            else bulletsListToActivate = ObjectPuller.current.GetS4BulletCPUList();
        }

        else if (name.Contains(station3Tag))
        {
            ACCURE_RANGE = Constants.Instance.CRUIS_3_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.CRUIS_3_ATTACK_TIME;
            SHIP_ATTACK_FORCE = Constants.Instance.CRUIS_3_ATTACK_FORCE;

            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            if (Lists.isPlayerStationOnDefence) bulletsListToActivate = ObjectPuller.current.GetS3BulletPlayerList();
            else bulletsListToActivate = ObjectPuller.current.GetS3BulletCPUList();
        }
        else if (name.Contains(station2Tag))
        {
            ACCURE_RANGE = Constants.Instance.CRUIS_2_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.CRUIS_2_ATTACK_TIME;
            SHIP_ATTACK_FORCE = Constants.Instance.CRUIS_2_ATTACK_FORCE;

            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class in case if this is player's ship
            if (Lists.isPlayerStationOnDefence) bulletsListToActivate = ObjectPuller.current.GetS2BulletPlayerList();
            else bulletsListToActivate = ObjectPuller.current.GetS2BulletCPUList();

            doubleBullet = true; //makes curren cruiser to shot with double bullets per one shot
        }
        else if (name.Contains(station1Tag))
        {
            ACCURE_RANGE = Constants.Instance.CRUIS_1_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.CRUIS_1_ATTACK_TIME;
            SHIP_ATTACK_FORCE = Constants.Instance.CRUIS_1_ATTACK_FORCE;

            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            if (Lists.isPlayerStationOnDefence) bulletsListToActivate = ObjectPuller.current.GetS1BulletPlayerList();
            else bulletsListToActivate = ObjectPuller.current.GetS1BulletCPUList();

            doubleBullet = true; //makes curren cruiser to shot with double bullets per one shot
        }

        else if (name.Contains(stationGTag))
        {
            ACCURE_RANGE = Constants.Instance.CRUIS_2_ACCUR_RANGE;
            ATTACK_TIME_FROM = Constants.Instance.CRUIS_2_ATTACK_TIME;
            SHIP_ATTACK_FORCE = Constants.Instance.CRUIS_2_ATTACK_FORCE;

            //this one gets the List of pulled objects from ObjectPuller class to use here and pass to PlayerJoystick class
            bulletsListToActivate = ObjectPuller.current.GetSFedBulletCPUList();

            doubleBullet = true; //makes curren cruiser to shot with double bullets per one shot
        }

        shotShound = GetComponent<AudioSource>(); //so the audio source is assigned right to the station

        if (!name.Contains(stationGTag))
        {
            if (Lists.isPlayerStationOnDefence)
            {
                for (int i = 0; i < IDColorElements.Count; i++)
                {
                    IDColorElements[i].GetComponent<MeshRenderer>().material.SetColor("_Color", Lists.colorOfPlayer);
                }
            }
            else {
                for (int i = 0; i < IDColorElements.Count; i++)
                {
                    IDColorElements[i].GetComponent<MeshRenderer>().material.SetColor("_Color", Lists.colorOfOpposite);
                }
            }
        }

        StartCoroutine(waitForFirstAttack());
    }

    //initiate the first attack of CPU ship (2 secs are necessary for waiting to other ships are added to collections)
    IEnumerator waitForFirstAttack()
    {
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        attackPointing();
    }

    //CPU attack management function
    private void attackPointing()
    {
        //this condition determines if this station will attack player fleet or CPU fleet, so first condition makes station attack player's fleets
        if (Lists.isPlayerStationOnDefence)
        {
            //it works only if there is at least one opposite side ship on battlefield
            if (Lists.AllPlayerShips.Count > 0 && Lists.AllCPUShips.Count > 0) //all conditions to stop attack processes with explanatory names
            {
                //---------- to make CPU to be able to recognize which ships are on opposite site
                int i = UnityEngine.Random.Range(0, Lists.AllCPUShips.Count); //this randome chouses any ship from list of white ships
                float accuracy = UnityEngine.Random.Range(ACCURE_RANGE, -ACCURE_RANGE); //this randome is for 3 class destroyers only, it determines the accuracy of the shot (higher - is less accurate)

                //so shot direction is subtraction of ships point vector from enemys point vector (with accuracy correction)
                shotDirection = Lists.AllCPUShips[i].transform.position + new Vector3(accuracy, 0, 0) - transform.position;

                //if the ship has such feature it will shot with double bullets so additional bullet is created and impulse added to it later in fixed update
                //here it is not pu into paralizer sequence cause only cruiser 2 class will have double bullets and it is not paralizer
                if (doubleBullet)
                {
                    shotBulletReal2 = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                    shotBulletReal2.transform.position = transform.position * 1.07f;
                    shotBulletReal2.transform.rotation = Quaternion.Euler(0, 0, 0);
                    shotBulletReal2.SetActive(true);
                }
                shotBulletReal = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                shotBulletReal.transform.position = transform.position;
                shotBulletReal.transform.rotation = Quaternion.Euler(0, 0, 0);
                shotBulletReal.SetActive(true);
            }

            fire = true;
            shotShound.Play();
            shotEffect.Play(); //creates energy shot effect for current ship
                               //this one is invoking this method again after randome time which is set for Dstr3 class, so it is recursion of this method
            if (!IsInvoking("attackPointing") && !isDestroying) Invoke("attackPointing", UnityEngine.Random.Range(ATTACK_TIME_FROM, ATTACK_TIME_FROM + 2));
        }
        else
        {
            //it works only if there is at least one opposite side ship on battlefield
            if (Lists.AllPlayerShips.Count > 0 && Lists.AllCPUShips.Count > 0)
            {
                //---------- to make CPU to be able to recognize which shipa are on opposite site
                int i = Random.Range(0, Lists.AllPlayerShips.Count); //this randome chouses any ship from list of white ships
                float accuracy = Random.Range(ACCURE_RANGE, -ACCURE_RANGE); //this randome is for 3 class destroyers only, it determines the accuracy of the shot (higher - is less accurate)

                //so shot direction is subtraction of ships point vector from enemys point vector (with accuracy correction)
                shotDirection = Lists.AllPlayerShips[i].transform.position + new Vector3(accuracy, 0, 0) - transform.position;

                //if the ship has such feature it will shot with double bullets so additional bullet is created and impulse added to it later in fixed update
                //here it is not pu into paralizer sequence cause only station 2 and 1 class (and guard) will have double bullets and it is not paralizer
                if (doubleBullet)
                {
                    shotBulletReal2 = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                    shotBulletReal2.transform.position = transform.position * 1.1f;
                    shotBulletReal2.transform.rotation = Quaternion.Euler(0, 0, 0);
                    shotBulletReal2.SetActive(true);
                }
                shotBulletReal = ObjectPuller.current.GetUniversalBullet(bulletsListToActivate);
                shotBulletReal.transform.position = transform.position;
                shotBulletReal.transform.rotation = Quaternion.Euler(0, 0, 0);
                shotBulletReal.SetActive(true);

                fire = true;
                shotShound.Play();
                shotEffect.Play();

                //this one is invocing this method again after randome time which is set for Dstr3 class, so it is recursion of this method
                if (!IsInvoking("attackPointing") && !isDestroying) Invoke("attackPointing", Random.Range(ATTACK_TIME_FROM, ATTACK_TIME_FROM + 2));
            }
        }
    }

    private void FixedUpdate()
    {
        if (fire)
        {
            //this is necessaty to set velocity of bullet zero to avoid doubling the bullet speed each time it is activating from pull
            shotBulletReal.GetComponent<Rigidbody>().velocity = Vector3.zero;
            shotBulletReal.GetComponent<Rigidbody>().AddForce(shotDirection * SHIP_ATTACK_FORCE, ForceMode.Impulse);

            if (doubleBullet)
            {
                shotBulletReal2.GetComponent<Rigidbody>().velocity = Vector3.zero;
                shotBulletReal2.GetComponent<Rigidbody>().AddForce(shotDirection * SHIP_ATTACK_FORCE, ForceMode.Impulse);
            }

            fire = false;
        }
    }
}
