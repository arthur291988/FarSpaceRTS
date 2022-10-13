using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RandomRotator : MonoBehaviour
{
    [SerializeField]
    private float tumble;
    private float randomeDirectionX;
    private float randomeDirectionY;
    private Vector3 thisAsteroidPoint;
    private Vector3 moveDir;
    private Vector3 rotationDir;
    //private Rigidbody rb;
    private float randomeVelosityIncreaser;

    //private Transform pointA;
    //private Transform pointB;
    //private Transform pointC;
    //private Transform pointD;

    private bool xCross;
    private float easyMinSpeed = 2;
    private float easyMaxSpeed = 4;

    //private float interpolateAmount;

    //these triggers are used to identify player ship
    private string Cruis1PlayerTag = "BullCruis1";
    private string Cruis2PlayerTag = "BullCruis2";
    private string Cruis3PlayerTag = "BullCruis3";
    private string Cruis4PlayerTag = "BullCruis4";

    private string playerBulletTag = "playerBullJourney";
    private string energonBullTag = "energonBull";

    private int thisAsteroidEnergyReduce;
    private int thisAsteroidHP;
    private int thisAsteroidHPStart;

    //to pull asteroid burst effect from puller
    private List<GameObject> asteroidBurstList;
    GameObject asteroidBurstReal;

    //properties to gather energy as bonus from asteroids 
    private GameObject energyBallReal; 
    private List<GameObject> energyBallList;

    void Start()
    {
        tumble = Random.Range(35f, 55f);
        rotationDir = Random.insideUnitSphere * tumble;
        thisAsteroidPoint = transform.position;
        if (name.Contains("1"))
        {
            thisAsteroidEnergyReduce = Constants.Instance.asteroid1Reduce;
            thisAsteroidHP = Constants.Instance.asteroid1HP;
            thisAsteroidHPStart = Constants.Instance.asteroid1HP;
        }
        else if (name.Contains("2"))
        {
            thisAsteroidEnergyReduce = Constants.Instance.asteroid2Reduce;
            thisAsteroidHP = Constants.Instance.asteroid2HP;
            thisAsteroidHPStart = Constants.Instance.asteroid2HP;
        }
        else if (name.Contains("3"))
        {
            thisAsteroidEnergyReduce = Constants.Instance.asteroid3Reduce;
            thisAsteroidHP = Constants.Instance.asteroid3HP;
            thisAsteroidHPStart = Constants.Instance.asteroid3HP;
        }
        else
        {
            thisAsteroidEnergyReduce = Constants.Instance.asteroid4Reduce;
            thisAsteroidHP = Constants.Instance.asteroid4HP;
            thisAsteroidHPStart = Constants.Instance.asteroid4HP;
        }

        if (Lists.currentLevelDifficulty == 0)
        {
            if (thisAsteroidPoint.z > 0)
            {
                moveDir = new Vector3(0, -8, -15) - new Vector3(0, -8, 0);
            }
            else moveDir = new Vector3(0, -8, 15) - new Vector3(0, -8, 0);

            randomeVelosityIncreaser = Random.Range(easyMinSpeed, easyMaxSpeed);
        }
        else
        {
            if (Lists.currentLevelDifficulty == 1)
            {
                xCross = Random.Range(0, 2) > 0 ? true : false;
                if (xCross)
                {
                    if (thisAsteroidPoint.z > 0)
                    {
                        moveDir = new Vector3(0, -8, -15) - new Vector3(0, -8, 0);
                    }
                    else moveDir = new Vector3(0, -8, 15) - new Vector3(0, -8, 0);
                }
                else {
                    transform.position = new Vector3(Random.Range(0, 2) > 0 ? 199f : -199f, -8, Random.Range(-45f, 45f));
                    thisAsteroidPoint = transform.position;
                    if (thisAsteroidPoint.x > 0)
                    {
                        moveDir = new Vector3(-15, -8, 0) - new Vector3(0, -8, 0);
                    }
                    else moveDir = new Vector3(15, -8, 0) - new Vector3(0, -8, 0);
                }

                randomeVelosityIncreaser = Random.Range(easyMinSpeed+1, easyMaxSpeed+1);
            }
            else
            {
                xCross = Random.Range(0, 2) > 0 ? true : false;
                if (xCross)
                {
                    if (thisAsteroidPoint.z > 0)
                    {
                        moveDir = new Vector3(0, -8, -15) - new Vector3(0, -8, 0);
                    }
                    else moveDir = new Vector3(0, -8, 15) - new Vector3(0, -8, 0);
                }
                else
                {
                    transform.position = new Vector3(Random.Range(0, 2) > 0 ? 199f : -199f, -8, Random.Range(-65f, 65f));
                    thisAsteroidPoint = transform.position;
                    if (thisAsteroidPoint.x > 0)
                    {
                        moveDir = new Vector3(-15, -8, 0) - new Vector3(0, -8, 0);
                    }
                    else moveDir = new Vector3(15, -8, 0) - new Vector3(0, -8, 0);
                }
                randomeVelosityIncreaser = Random.Range(easyMinSpeed+3, easyMaxSpeed+3);
            }
        }

        //randomeDirectionX = Random.Range(0f, 15f);
        //randomeDirectionY = Random.Range(0f, 15f);
        //moveDir = new Vector3(thisAsteroidPoint.x + randomeDirectionX * (Random.Range(0, 2) > 0 ? 1 : -1), -8, thisAsteroidPoint.y + randomeDirectionY * (Random.Range(0, 2) > 0 ? 1 : -1)) - thisAsteroidPoint;
        //rb.angularVelocity = Random.insideUnitSphere * tumble;


        //randomeVelosityIncreaser = Random.Range(12f, 17f);

        //pointA = SpaceCtrlr.Instance.pointA;
        //pointB = SpaceCtrlr.Instance.pointB;
        //pointC = SpaceCtrlr.Instance.pointC;
        //pointD = SpaceCtrlr.Instance.pointD;

        //randomeVelosityIncreaser = Random.Range(0.7f, 3f);
        //randomeVelosityIncreaser = Random.Range(0.01f, 0.03f);
        //rb = GetComponent<Rigidbody>();
        //tumble = Random.Range(40f, 55f);
        //tumble = Random.Range(1f, 2.5f);

        //rb.AddForce(moveDir.normalized*0.001f, ForceMode.Impulse);
    }
    //private void FixedUpdate()
    //{

    //    transform.Translate(moveDir.normalized * randomeVelosityIncreaser, Space.World);
    //    transform.Rotate(rotationDir, Space.World);
    //    //rb.velocity = moveDir.normalized*randomeVelosityIncreaser;
    //}

    //reducing the speed of Player ship after it hits the asteroid and reduces it's energy to rate
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Cruis1PlayerTag) || other.CompareTag(Cruis2PlayerTag) || other.CompareTag(Cruis3PlayerTag) || other.CompareTag(Cruis4PlayerTag))
        {
            //this second level if condition is necessary to prevent a guard ship to attack a player cruiser while it is ported to its station
            if (!other.gameObject.GetComponent<LaunchingObjcts>().isPortedToPlayerStation)
            {
                ShipController.reduceSpeedVar = 0.1f;
                Lists.energyOfPlayer -= thisAsteroidEnergyReduce;
                if (Lists.energyOfPlayer <= 0) Lists.energyOfPlayer = 0;
                SpaceCtrlr.Instance.energyCount.text = Lists.energyOfPlayer.ToString("0");

                SpaceCtrlr.Instance.energyCount.color = Color.red;
                SpaceCtrlr.Instance.energyCount.transform.parent.gameObject.GetComponent<Animator>().SetBool("tokenActive", true);
                SpaceCtrlr.Instance.Invoke("disactAnimAndColorsOfTokens", 2.5f);
            }

            //SpaceCtrlr.disactAnimAndColorsOfTokens();
        }
        else if (other.CompareTag("GCruisOut"))
        {
            thisAsteroidHP--;
            if (thisAsteroidHP == 0)
            {

                asteroidBurstList = ObjectPullerJourney.current.GetAsteroidBurstPullList();
                asteroidBurstReal = ObjectPullerJourney.current.GetUniversalBullet(asteroidBurstList);
                asteroidBurstReal.transform.position = transform.position;
                asteroidBurstReal.SetActive(true);

                //so here the second condition is probability of getting the booster from guard
                if (/*energyBallReal == null && */Random.Range(0, 3) > 0)
                {
                    energyBallList = ObjectPullerJourney.current.GetEnergySpherePullList();
                    energyBallReal = ObjectPullerJourney.current.GetUniversalBullet(energyBallList);
                    energyBallReal.transform.position = transform.position;
                    energyBallReal.SetActive(true);
                    Lists.energyBalls.Add(energyBallReal);
                    attractTheClosestEnergonToGetEnergyBall();

                }

                thisAsteroidHP = thisAsteroidHPStart;
                transform.position = new Vector3(1000, -8, 1000);
            }
        }
        else if (other.CompareTag("Energon"))
        {
            EnergonController ec = other.GetComponent<EnergonController>();
            ec.energyOfEnergon -= thisAsteroidEnergyReduce;
            if (ec.energyOfEnergon < 0) ec.energyOfEnergon = 0;
            ec.updateInfoPanelToDisplay();
        }
    }

    private void attractTheClosestEnergonToGetEnergyBall() {
        List<EnergonController> allCPUEnergons = new List<EnergonController>();
        for (int i=0; i < Lists.energonsControllablesOnScene.Count; i++) {
            EnergonController ec = Lists.energonsControllablesOnScene[i].GetComponent<EnergonController>();
            if (!ec.isPlayerEnergon) {
                allCPUEnergons.Add(ec);
            }
        }
        List <float> toEmptyStationsWayLength = new List<float>();
        for (int i = 0; i < allCPUEnergons.Count; i++)
        {
            toEmptyStationsWayLength.Add((allCPUEnergons[i].gameObject.transform.position - energyBallReal.transform.position).sqrMagnitude);
        }

        if (toEmptyStationsWayLength.Count > 0)
        {
            allCPUEnergons[toEmptyStationsWayLength.IndexOf(toEmptyStationsWayLength.Min())].goToGetEnergyBall(energyBallReal.transform.position);
        }
    }

    private void Update()
    {

        transform.Translate(moveDir.normalized * randomeVelosityIncreaser * Time.deltaTime, Space.World);
        transform.Rotate(rotationDir * Time.deltaTime, Space.World);

        //this function puts back asteroid if it got out of bounds 
        if (Lists.currentLevelDifficulty == 0)
        {
            if (transform.position.z < -201)
            {
                transform.position = new Vector3(Random.Range(35f, -35f), -8, 200);
                //rotationDir = Random.insideUnitSphere * tumble;
                randomeVelosityIncreaser = Random.Range(easyMinSpeed, easyMaxSpeed);
            }
            else if (transform.position.z > 201)
            {
                transform.position = new Vector3(Random.Range(35f, -35f), -8, -200);
                //tumble = Random.Range(85f, 105f);
                //rotationDir = Random.insideUnitSphere * tumble;
                randomeVelosityIncreaser = Random.Range(easyMinSpeed, easyMaxSpeed);
            }
        }

        else if (Lists.currentLevelDifficulty == 1) {
            if (xCross)
            {
                if (transform.position.z < -201)
                {
                    transform.position = new Vector3(Random.Range(45f, -45f), -8, 200);
                    //tumble = Random.Range(85f, 105f);
                    //rotationDir = Random.insideUnitSphere * tumble;
                    randomeVelosityIncreaser = Random.Range(easyMinSpeed, easyMaxSpeed);
                }
                else if (transform.position.z > 201)
                {
                    transform.position = new Vector3(Random.Range(45f, -45f), -8, -200);
                    //tumble = Random.Range(85f, 105f);
                    //rotationDir = Random.insideUnitSphere * tumble;
                    randomeVelosityIncreaser = Random.Range(easyMinSpeed, easyMaxSpeed);
                }
            }
            else
            {
                if (transform.position.x < -201)
                {
                    transform.position = new Vector3(200, -8, Random.Range(45f, -45f));
                    //tumble = Random.Range(85f, 105f);
                    //rotationDir = Random.insideUnitSphere * tumble;
                    randomeVelosityIncreaser = Random.Range(easyMinSpeed+1, easyMaxSpeed+1);
                }
                else if (transform.position.x > 201)
                {
                    transform.position = new Vector3(-200, -8, Random.Range(45f, -45f));
                    //tumble = Random.Range(85f, 105f);
                    //rotationDir = Random.insideUnitSphere * tumble;
                    randomeVelosityIncreaser = Random.Range(easyMinSpeed+1, easyMaxSpeed+1);
                }
            }
        }
        else if (Lists.currentLevelDifficulty == 2)
        {
            if (xCross)
            {
                if (transform.position.z < -201)
                {
                    transform.position = new Vector3(Random.Range(45f, -45f), -8, 200);
                    //tumble = Random.Range(85f, 105f);
                    //rotationDir = Random.insideUnitSphere * tumble;
                    randomeVelosityIncreaser = Random.Range(easyMinSpeed, easyMaxSpeed);
                }
                else if (transform.position.z > 201)
                {
                    transform.position = new Vector3(Random.Range(45f, -45f), -8, -200);
                    //tumble = Random.Range(85f, 105f);
                    //rotationDir = Random.insideUnitSphere * tumble;
                    randomeVelosityIncreaser = Random.Range(easyMinSpeed, easyMaxSpeed);
                }
            }
            else
            {
                if (transform.position.x < -201)
                {
                    transform.position = new Vector3(200, -8, Random.Range(65f, -65f));
                    //tumble = Random.Range(85f, 105f);
                    //rotationDir = Random.insideUnitSphere * tumble;
                    randomeVelosityIncreaser = Random.Range(easyMinSpeed + 3, easyMaxSpeed + 3);
                }
                else if (transform.position.x > 201)
                {
                    transform.position = new Vector3(-200, -8, Random.Range(65f, -65f));
                    //tumble = Random.Range(85f, 105f);
                    //rotationDir = Random.insideUnitSphere * tumble;
                    randomeVelosityIncreaser = Random.Range(easyMinSpeed + 3, easyMaxSpeed + 3);
                }
            }
        }
        //    transform.position = new Vector3(Random.Range(0, 2)>0 ? 155f : -155f, -8, Random.Range(0, 2) > 0 ? 155f : -155f);
        //    randomeDirectionX = Random.Range(0f, 15f);
        //    randomeDirectionY = Random.Range(0f, 15f);
        //    thisAsteroidPoint = transform.position;
        //    //rb = GetComponent<Rigidbody>();
        //    
        //    //tumble = Random.Range(40f, 55f);
        //    //tumble = Random.Range(1f, 2.5f);
        //    
        //    moveDir = new Vector3(thisAsteroidPoint.x + randomeDirectionX * (Random.Range(0, 2) > 0 ? 1 : -1), -8, thisAsteroidPoint.y + randomeDirectionY * (Random.Range(0, 2) > 0 ? 1 : -1)) - thisAsteroidPoint;
        //    //rb.angularVelocity = Random.insideUnitSphere * tumble;


        //    randomeVelosityIncreaser = Random.Range(8f, 17f);
        //    //transform.position = new Vector3(Random.Range(50f, -50f), -8, Random.Range(50f, -50f));
        //}
    }
}