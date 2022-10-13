using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationBullet : MonoBehaviour
{
    private Transform bulletTransform;
    private Transform energonTRansform;
    private Vector3 stationPosition;

    private float speedOfBullet; //TO SET FROM STATION ACCORDING TO ITS LEVEL
    private float colorToEnergyConstantMultiplyer; //TO SET FROM STATION ACCORDING TO ITS LEVEL

    private float maxDistace;
    private bool isPlayer;
    //private int CPUNumber;
    private TrailRenderer trail;
    private GameObject bulletBurst;
    private List<GameObject> bulletBurstPullList;
    private GameObject energyBall;
    private List<GameObject> energyBallPullList;
    private int energyReceived;
    private float scaleOfEnergyBall;
    private StationClass stationThatMadeAShot;


    // Start is called before the first frame update
    //void Start()
    //{
    //}

    private void OnEnable()
    {
        bulletTransform = transform;
        if (trail == null) trail = GetComponent<TrailRenderer>();
        trail.Clear();
    }

    private void disactivateBullet(bool hit)
    {
        if (hit)
        {
            energyBallPullList = ObjectPullerRTS.current.GetenergyBallPulls();
            energyBall = ObjectPullerRTS.current.GetGameObjectFromPull(energyBallPullList);
            energyBall.transform.position = bulletTransform.position;
            energyBall.GetComponent<EnergyBallRTS>().setProperties(stationPosition, stationThatMadeAShot, energyReceived, scaleOfEnergyBall, isPlayer/*, CPUNumber*/);
            energyBall.SetActive(true);
        }

        gameObject.SetActive(false);
        //trail.Clear();
    }

    public void setTheAimAndStation(Transform energonShip, Vector3 stationPos, StationClass station, float colorToEnergy, float speed, float distance, bool isPlayer/*, int CPUNumber*/) {
        energonTRansform = energonShip;
        stationPosition = stationPos;
        speedOfBullet = speed;
        colorToEnergyConstantMultiplyer = colorToEnergy;
        maxDistace = distance;
        this.isPlayer = isPlayer;
        //this.CPUNumber = CPUNumber;
        stationThatMadeAShot = station;
    }

    private void FixedUpdate()
    {
        bulletTransform.Translate((energonTRansform.position - bulletTransform.position).normalized*Time.fixedDeltaTime*speedOfBullet, Space.World);
    }

    // Update is called once per frame
    void Update()
    {
        if ((energonTRansform.position - bulletTransform.position).magnitude < 1.5f) {
            bulletBurstPullList = ObjectPullerRTS.current.GetGun1BulletBurstPull();
            bulletBurst = ObjectPullerRTS.current.GetGameObjectFromPull(bulletBurstPullList);
            bulletBurst.transform.position = bulletTransform.position;
            bulletBurst.SetActive(true);
            EnergonMoving energon = energonTRansform.gameObject.GetComponent<EnergonMoving>();
            scaleOfEnergyBall = energon.colorRGB*2;
            energon.takeTheEnergyOfEnergon();
            energyReceived = (int)(scaleOfEnergyBall * colorToEnergyConstantMultiplyer);
            
            disactivateBullet(true);
        }
        if ((stationPosition-bulletTransform.position).magnitude>maxDistace) {
            bulletBurstPullList = ObjectPullerRTS.current.GetGun1BulletBurstPull();
            bulletBurst = ObjectPullerRTS.current.GetGameObjectFromPull(bulletBurstPullList);
            bulletBurst.transform.position = bulletTransform.position;
            bulletBurst.SetActive(true);
            disactivateBullet(false);
        }
    }
}
