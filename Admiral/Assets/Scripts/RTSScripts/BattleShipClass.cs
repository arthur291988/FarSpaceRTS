using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleShipClass : MonoBehaviour
{
    [HideInInspector]
    public int CPUNumber;
    [HideInInspector]
    public bool isCruiser;
    [HideInInspector]
    public bool isParalyzer;
    [HideInInspector]
    public int levelOfShip;
    [HideInInspector]
    public int shipPower;

    public GameObject powerShiled;
    public Material powerShieldMaterial;
    [HideInInspector]
    public bool shieldIsOn;

    [HideInInspector]
    public Transform shipTransform;
    [HideInInspector]
    public float speedOfThisShip;
    [HideInInspector]
    public float attackDistance; //TO ASSIGN WHILE PULLLING
    [HideInInspector]
    public float attackTimeMax; //TO ASSIGN WHILE PULLLING
    [HideInInspector]
    public float attackTimeMin; //TO ASSIGN WHILE PULLLING
    [HideInInspector]
    public float shieldTimeMax; //TO ASSIGN WHILE PULLLING
    [HideInInspector]
    public float shieldTimeMin; //TO ASSIGN WHILE PULLLING
    [HideInInspector]
    public float shieldOnTime; //TO ASSIGN WHILE PULLLING
    [HideInInspector]
    public float attackTimerTime;
    [HideInInspector]
    public float shieldTimerTime;

    [HideInInspector]
    public float HP; //TO ASSIGN WHILE PULLLING
    [HideInInspector]
    public float harm; //TO ASSIGN WHILE PULLLING
    [HideInInspector]
    public float harmStar; //TO ASSIGN WHILE PULLLING

    [HideInInspector]
    public bool isMoving;
    [HideInInspector]
    public Vector3 moveToPoint;

    [HideInInspector]
    public LineRenderer attackLaserLine;
    [HideInInspector]
    public LineRenderer paralizerLaserLine;

    [HideInInspector]
    public BattleShipClass shipToAttak;
    //[HideInInspector]
    //public PlayerGun gunToAttak;
    [HideInInspector]
    public StationClass stationToAttak;
    [HideInInspector]
    public StarController starToAttak;
    [HideInInspector]
    public bool attackMode;

    [HideInInspector]
    public StationClass maternalStation;

    [HideInInspector]
    public List<StarController> closeStars;
    [HideInInspector]
    public List<PlayerGun> closeGuns;
    [HideInInspector]
    public List<BattleShipClass> closeBattleShips;
    [HideInInspector]
    public List<StationClass> closeStations;


    [HideInInspector]
    public List<GameObject> BurstList;
    [HideInInspector]
    public GameObject BurstReal;

    [HideInInspector]
    public AudioSource shotSound;
    [HideInInspector]
    public AudioSource engineSound;
    [HideInInspector]
    public AudioSource megaShotSound;

    [HideInInspector]
    public bool isUnderMegaDefence;

    [HideInInspector]
    public bool isParalyzed;
    public GameObject paralizedEffect;
    [HideInInspector]
    public float paralizeTimeForParaizer;

    [HideInInspector]
    public int paralizerShotCounter;

    public virtual void reduceTheHPOfShip(float harmAmount)
    {
    }

    private void Awake()
    {
        shotSound = GetComponent<AudioSource>();
        engineSound = transform.GetChild(0).GetComponent<AudioSource>();
        powerShieldMaterial = powerShiled.GetComponent<MeshRenderer>().material;
    }

    public void preBurstEffect()
    {
        if (isCruiser) BurstList = ObjectPullerRTS.current.GetCruisPreBurstPull();
        else BurstList = ObjectPullerRTS.current.GetDestrPreBurstPull();
        BurstReal = ObjectPullerRTS.current.GetGameObjectFromPull(BurstList);
        BurstReal.transform.position = shipTransform.position;
        BurstReal.SetActive(true);
    }

    public IEnumerator paralizeThisShip(float secondsToWait)
    {
        isParalyzed = true;
        paralizedEffect.SetActive(true);

        yield return new WaitForSeconds(secondsToWait);

        isParalyzed = false;
        paralizedEffect.SetActive(false);

    }

}
