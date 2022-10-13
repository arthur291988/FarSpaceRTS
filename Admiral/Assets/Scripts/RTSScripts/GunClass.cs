using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunClass : MonoBehaviour
{
    [HideInInspector]
    public Transform gunTransform;
    [HideInInspector]
    public Transform gunTurrelTransform;
    [HideInInspector]
    public float attackDistance; //TO ASSIGN WHILE PULLLING
    [HideInInspector]
    public List<BattleShipClass> closeBattleShips;
    [HideInInspector]
    public BattleShipClass shipToAttak;
    [HideInInspector]
    public bool attackMode;
    [HideInInspector]
    public float attackTimerTime;
    [HideInInspector]
    public float attackTimeMax; //TO ASSIGN WHILE PULLLING
    [HideInInspector]
    public float attackTimeMin; //TO ASSIGN WHILE PULLLING
    [HideInInspector]
    public float shotTime; //TO ASSIGN WHILE PULLLING
    //[HideInInspector]
    //public float HP; //TO ASSIGN WHILE PULLLING

    [HideInInspector]
    public Vector3 shotDirection;
    [HideInInspector]
    public GameObject objectPulled;
    [HideInInspector]
    public List<GameObject> objectPulledList;

    public Animator shotAnimator;
    public ParticleSystem shotEffect;
    public ParticleSystem shotEffect2;
    public ParticleSystem shotEffect3;
    public Transform spawnPoint;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform spawnPointBase;
    public Transform spawnPointBase2;
    public Transform spawnPointBase3;

    public GameObject gunTurrel; //the tower of the gun
    [HideInInspector]
    public float bullSpeed; //TO ASSIGN LATER 

    //public GameObject gunSphereVisible;
    //public GameObject gunSphereParent;


    public StationClass stationOfThisGun;

    [HideInInspector]
    public int CPUNumber;

    public List<GameObject> gunColorElements; //parts of gun demostrate the color of CPU
    [HideInInspector]
    public Color colorOfGunMat;
    [HideInInspector]
    public int gunLevel;

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
