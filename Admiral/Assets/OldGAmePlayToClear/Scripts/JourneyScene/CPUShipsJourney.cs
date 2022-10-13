using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

public class CPUShipsJourney : MonoBehaviour
{
    private string StationTag = "Gun"; //this tag is used to identify stations by tag
    private string GalaxyTag = "MegaShot"; //this tag is used to identify galaxyies by tag
    private string FogTag = "GunBullCPU"; //this tag is used to identify Fog by tag
    private string TelepTag = "PowerShieldCPU"; //this tag is used to identify teleport by tag
    //private string StationTagForDestroy = "BullCruis1"; TO DELETE, was created to destroy automatically the station if it is intersect with station

    private string cruisGuardTag = "GCruisOut";

    //icons of CPU ships randomely assignet to ships
    public GameObject pirateIcon;
    public GameObject aggreIcon;
    public GameObject peaceIcon;
    public GameObject guardIcon;

    //those are used to trigger fir player ship if this ship is aggressive or peace type (pirate setting are lower)
    public bool isAggres = false;
    public bool isPeace = false;

    private GameObject playerShip;

    //pirate settings
    private bool isPirate = false; //is used to assigne CPU ship as pirate
    private float pirateTranslateModif = 0.007f; //is used to increase the speed of pirate ship if it is close to player ship (otherwise it will never overcome it)
    private float pirateChaseTime = 11f;
    private float pirateStartTime = 0f;

    private float maxSpeedOfShip;
    private float minSpeedOfShip;

    private bool hasMovingFeature = false;
    private float xMove;
    private float zMove;
    private float yRotation;
    private Vector3 directionToLook;


    //to use this values with different dimenstion teleportations (according to tag and current deimention features)
    private string upTeleportTag = "Finish";
    private string downTeleportTag = "Respawn";
    public Material blackTeleport;
    public Material blueTeleport;
    public Material redTeleport;
    public GameObject teleportEffectSphere;

    private ParticleSystem.MainModule main;


    private void OnEnable()
    {
        //playerShip = LaunchingObjcts.Instance.getPlayerShip();
        
        //if (CompareTag(StationTag))
        //{
        //    //Lists.StationsOnScene.Add(gameObject);
        //}
        /*else*/ if (CompareTag(GalaxyTag))
        {
            //Lists.SpaceObjOnScene.Add(gameObject);
            main = GetComponent<ParticleSystem>().main;
            main.startColor = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f, 1f, 1f); //generates randome color for galaxy
        }
        else if (CompareTag(FogTag))
        {
            //    Lists.FogOnScene.Add(gameObject);
            main = GetComponent<ParticleSystem>().main;
            main.startColor = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f, 1f, 1f); //generates randome color for galaxy
        }
        //teleportaion objec also processes the dimension switches 
        else if (CompareTag(TelepTag))
        {
            //Lists.TeleportOnScene.Add(gameObject);
            if (Lists.isBlueDimension)
            {
                teleportEffectSphere.tag = Random.Range(0f, 1f) > 0.6f ? upTeleportTag : downTeleportTag;
                if (teleportEffectSphere.CompareTag(upTeleportTag))
                {
                    gameObject.GetComponent<Renderer>().material.color = Color.red;
                    teleportEffectSphere.GetComponent<Renderer>().material = redTeleport;
                }
                else
                {
                    gameObject.GetComponent<Renderer>().material.color = Color.grey;
                    teleportEffectSphere.GetComponent<Renderer>().material = blackTeleport;
                }
            }
            else if (Lists.isBlackDimension)
            {
                teleportEffectSphere.tag = upTeleportTag;
                gameObject.GetComponent<Renderer>().material.color = Color.blue;
                teleportEffectSphere.GetComponent<Renderer>().material = blueTeleport;
            }
            else if (Lists.isRedDimension)
            {
                teleportEffectSphere.tag = downTeleportTag;
                gameObject.GetComponent<Renderer>().material.color = Color.blue;
                teleportEffectSphere.GetComponent<Renderer>().material = blueTeleport;
            }
        }
        //next condition is for ships including guard ship
        else if (CompareTag(cruisGuardTag))
        {
            guardIcon.SetActive(false);

            //Lists.ShipsOnScene.Add(gameObject);

            //following methods rotate space ship toward its move direction and used on transform translate method
            if (Lists.isBlackDimension)
            {
                maxSpeedOfShip = 0.2f;
                minSpeedOfShip = -0.2f;
            }
            else if (Lists.isBlueDimension)
            {
                maxSpeedOfShip = 0.35f;
                minSpeedOfShip = -0.35f;
            }
            else if (Lists.isRedDimension)
            {
                maxSpeedOfShip = 0.45f;
                minSpeedOfShip = -0.45f;
            }
            xMove = Random.Range(minSpeedOfShip, maxSpeedOfShip);
            zMove = Random.Range(minSpeedOfShip, maxSpeedOfShip);

            directionToLook = (transform.right * xMove + transform.up * 0 + transform.forward * zMove);
            yRotation = Quaternion.LookRotation(directionToLook, Vector3.up).eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, yRotation + 90, 0);

            guardIcon.SetActive(true);
            //Lists.GuardsOnScene.Add(gameObject);

            hasMovingFeature = true;
        }
        else if (!CompareTag(StationTag))
        {
            isAggres = false;
            isPeace = false;
            isPirate = false;
            aggreIcon.SetActive(false);
            peaceIcon.SetActive(false);
            pirateIcon.SetActive(false);

            //Lists.ShipsOnScene.Add(gameObject);

            //following methods rotate space ship toward its move direction and used on transform translate method
            if (Lists.isBlackDimension)
            {
                maxSpeedOfShip = 0.2f;
                minSpeedOfShip = -0.2f;
            }
            else if (Lists.isBlueDimension)
            {
                maxSpeedOfShip = 0.35f;
                minSpeedOfShip = -0.35f;
            }
            else if (Lists.isRedDimension)
            {
                maxSpeedOfShip = 0.45f;
                minSpeedOfShip = -0.45f;
            }
            xMove = Random.Range(minSpeedOfShip, maxSpeedOfShip);
            zMove = Random.Range(minSpeedOfShip, maxSpeedOfShip);

            //randomly determine if current CPU ship is pirate and only if there is no any more pirates on scene
            //if (Random.Range(0f, 1f) < 0.2f && Lists.PiratesOnScene.Count == 0) isPirate = true;

            if (!isPirate)
            {
                if (Random.Range(0f, 1f) < 0.3f)
                {
                    peaceIcon.SetActive(true);
                    isPeace = true;
                }
                else
                {
                    aggreIcon.SetActive(true);
                    isAggres = true;
                }
                directionToLook = (transform.right * xMove + transform.up * 0 + transform.forward * zMove);
                yRotation = Quaternion.LookRotation(directionToLook, Vector3.up).eulerAngles.y;
                transform.rotation = Quaternion.Euler(0, yRotation + 90, 0);
            }
            else
            {
                pirateIcon.SetActive(true);
                //Lists.PiratesOnScene.Add(gameObject);
            }
            hasMovingFeature = true;
        }
    }


    void FixedUpdate() {
        if (hasMovingFeature) {
            if (!isPirate) transform.Translate(xMove * 0.5f, 0, zMove * 0.5f, Space.World); //if CPU is not pirate it moves on its own direction
            else transform.Translate(directionToLook* pirateTranslateModif, Space.World);//if CPU is pirate it chases player ship 11 secs
        }

    }

    private void OnDisable()
    {
        if (!CompareTag(StationTag) && !CompareTag(GalaxyTag) && !CompareTag(FogTag) && !CompareTag(TelepTag)) {

            foreach (TrailRenderer trail in GetComponentsInChildren<TrailRenderer>()) trail.Clear();
        }
    }


    // Update is called once per frame
    void Update()
    {
        //that code regulates pirate ship movement on scene it chases player only 11 secs and it starts to increase it speed if it close to player ship
        //on scrMagnitude less than 200
        if (isPirate && pirateStartTime<pirateChaseTime)
        {
            pirateStartTime += Time.deltaTime;
            //Debug.Log(pirateStartTime);

            directionToLook = playerShip.transform.position - transform.position;
            if (directionToLook.sqrMagnitude < 200) pirateTranslateModif += 0.0005f;
            else pirateTranslateModif = 0.005f;
            yRotation = Quaternion.LookRotation(directionToLook, Vector3.up).eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, yRotation + 90, 0);
        }
    }
}
