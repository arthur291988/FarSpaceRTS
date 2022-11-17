using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPullerRTS : MonoBehaviour
{
    public static ObjectPullerRTS current;

    private const int pulledPreBurst = 10;
    private const int pulledPlayerGun1Bullets = 13;
    private const int pulled4Objects = 4;
    private const int pulled3Objects = 3;
    private const int pulled15Objects = 15;
    private const int pulled50Objects = 50;
    //private int pulled50Objects = 50;
    private bool willGrow;

    //[SerializeField]
    ////private Transform parentCanvasUIElements;

    //public Transform parentCanvasOfAiming;
    //public GameObject aimingRect;

    public GameObject CruisPreBurst;
    public GameObject DestrPreBurst;
    public GameObject CruisBurst;
    public GameObject DestrBurst;
    public GameObject StationBurst;

    public GameObject PlayerGun1Bullet;

    public GameObject Gun1BulletBurst;

    //public GameObject PlayerGunToDrop;

    //public GameObject StationBullet;
    //public GameObject gunRollUpFillingImage;
    public GameObject energyBall;

    public GameObject Cruis4CPU;
    public GameObject Destr4CPU;
    public GameObject Cruis3CPU;
    public GameObject Destr3CPU;
    public GameObject Cruis2CPU;
    public GameObject Destr2CPU;
    public GameObject Destr2ParCPU;
    public GameObject Cruis1CPU;
    public GameObject Destr1CPU;
    public GameObject Destr1ParCPU;

    public GameObject Cruis4Player;
    public GameObject Destr4Player;
    public GameObject Cruis3Player;
    public GameObject Destr3Player;
    public GameObject Cruis2Player;
    public GameObject Destr2Player;
    public GameObject Destr2ParPlayer;
    public GameObject Cruis1Player;
    public GameObject Destr1Player;
    public GameObject Destr1ParPlayer;


    public GameObject playerStation0;
    public GameObject playerStation1;
    public GameObject playerStation2;
    public GameObject playerStation3;

    public GameObject CPUStation0;
    public GameObject CPUStation1;
    public GameObject CPUStation2;
    public GameObject CPUStation3;

    public GameObject Star0;
    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;


    public GameObject energon;

    public GameObject connectionLine;

    //public GameObject smallEnergy;
    //public GameObject midEnergy;
    //public GameObject bigEnergy;

    [HideInInspector]
    public List<GameObject> CruisPreBurstPull;
    [HideInInspector]
    public List<GameObject> DestrPreBurstPull;
    [HideInInspector]
    public List<GameObject> CruisBurstPull;
    [HideInInspector]
    public List<GameObject> DestrBurstPull;
    [HideInInspector]
    public List<GameObject> StationBurstPull;

    [HideInInspector]
    public List<GameObject> PlayerGun1BulletPull; //does not batter if it is the player bullet or CPU, determination is rulled by layer by CPUNumber

    [HideInInspector]
    public List<GameObject> Gun1BulletBurstPull;

    //[HideInInspector]
    //public List<GameObject> PlayerGunToDropPull;
    
    //[HideInInspector]
    //public List<GameObject> StationBulletPull;

    //[HideInInspector]
    //public List<GameObject> gunRollUpFillingImagePull;
    [HideInInspector]
    public List<GameObject> energyBallPull;

    [HideInInspector]
    public List<GameObject> Cruis4PlayerPull;
    [HideInInspector]
    public List<GameObject> Destr4PlayerPull;
    [HideInInspector]
    public List<GameObject> Cruis3PlayerPull;
    [HideInInspector]
    public List<GameObject> Destr3PlayerPull;
    [HideInInspector]
    public List<GameObject> Cruis2PlayerPull;
    [HideInInspector]
    public List<GameObject> Destr2PlayerPull;
    [HideInInspector]
    public List<GameObject> Destr2ParPlayerPull;
    [HideInInspector]
    public List<GameObject> Cruis1PlayerPull;
    [HideInInspector]
    public List<GameObject> Destr1PlayerPull;
    [HideInInspector]
    public List<GameObject> Destr1ParPlayerPull;
    [HideInInspector]
    public List<List<GameObject>> PlayerShipsPulls;

    [HideInInspector]
    public List<GameObject> Cruis4CPUPull;
    [HideInInspector]
    public List<GameObject> Destr4CPUPull;
    [HideInInspector]
    public List<GameObject> Cruis3CPUPull;
    [HideInInspector]
    public List<GameObject> Destr3CPUPull;
    [HideInInspector]
    public List<GameObject> Cruis2CPUPull;
    [HideInInspector]
    public List<GameObject> Destr2CPUPull;
    [HideInInspector]
    public List<GameObject> Destr2ParCPUPull;
    [HideInInspector]
    public List<GameObject> Cruis1CPUPull;
    [HideInInspector]
    public List<GameObject> Destr1CPUPull;
    [HideInInspector]
    public List<GameObject> Destr1ParCPUPull;
    [HideInInspector]
    public List<List<GameObject>> CPUShipsPulls;

    [HideInInspector]
    public List<GameObject> playerStation0Pull;
    [HideInInspector]
    public List<GameObject> playerStation1Pull;
    [HideInInspector]
    public List<GameObject> playerStation2Pull;
    [HideInInspector]
    public List<GameObject> playerStation3Pull;
    [HideInInspector]
    public List<List<GameObject>> PlayerStationsPulls;

    [HideInInspector]
    public List<GameObject> CPUStation0Pull;
    [HideInInspector]
    public List<GameObject> CPUStation1Pull;
    [HideInInspector]
    public List<GameObject> CPUStation2Pull;
    [HideInInspector]
    public List<GameObject> CPUStation3Pull;
    [HideInInspector]
    public List<List<GameObject>> CPUStationsPulls;


    [HideInInspector]
    public List<GameObject> energonPull; 

    [HideInInspector]
    public List<GameObject> Star0Pull;
    [HideInInspector]
    public List<GameObject> Star1Pull;
    [HideInInspector]
    public List<GameObject> Star2Pull;
    [HideInInspector]
    public List<GameObject> Star3Pull;
    [HideInInspector]
    public List<List<GameObject>> starPulls;

    //[HideInInspector]
    //public List<GameObject> aimingRectPull;
    [HideInInspector]
    public List<GameObject> connectionLinePull;
    
    //[HideInInspector]
    //public List<GameObject> smallEnergyPull;
    //[HideInInspector]
    //public List<GameObject> midEnergyPull;
    //[HideInInspector]
    //public List<GameObject> bigEnergyPull;
    //[HideInInspector]
    //public List<List<GameObject>> energyPulls;


    private void Awake()
    {
        willGrow = true;
        current = this;
    }

    private void OnEnable()
    {
        CruisPreBurstPull = new List<GameObject>();
        DestrPreBurstPull = new List<GameObject>();
        CruisBurstPull = new List<GameObject>();
        DestrBurstPull = new List<GameObject>();

        PlayerGun1BulletPull = new List<GameObject>();

        //PlayerGunToDropPull = new List<GameObject>();
        //StationBulletPull = new List<GameObject>();

        //gunRollUpFillingImagePull = new List<GameObject>();
        energyBallPull = new List<GameObject>();

        Cruis4PlayerPull = new List<GameObject>();
        Destr4PlayerPull = new List<GameObject>();
        Cruis3PlayerPull = new List<GameObject>();
        Destr3PlayerPull = new List<GameObject>();
        PlayerShipsPulls = new List<List<GameObject>>() { 
            Cruis4PlayerPull, Destr4PlayerPull, Cruis3PlayerPull, Destr3PlayerPull,Cruis2PlayerPull, Destr2PlayerPull, Destr2ParPlayerPull,Cruis1PlayerPull, Destr1PlayerPull, Destr1ParPlayerPull
        }; 

        Cruis4CPUPull = new List<GameObject>();
        Destr4CPUPull = new List<GameObject>();
        Cruis3CPUPull = new List<GameObject>();
        Destr3CPUPull = new List<GameObject>();
        CPUShipsPulls = new List<List<GameObject>>() {
            Cruis4CPUPull, Destr4CPUPull, Cruis3CPUPull, Destr3CPUPull, Cruis2CPUPull, Destr2CPUPull, Destr2ParCPUPull, Cruis1CPUPull, Destr1CPUPull, Destr1ParCPUPull
        };

        StationBurstPull = new List<GameObject>();

        energonPull = new List<GameObject>();

        playerStation0Pull = new List<GameObject>();
        playerStation1Pull = new List<GameObject>();
        playerStation2Pull = new List<GameObject>();
        playerStation3Pull = new List<GameObject>();
        //TO ADD OTHER STATIONS
        PlayerStationsPulls = new List<List<GameObject>>() {
            playerStation0Pull, playerStation1Pull,playerStation2Pull,playerStation3Pull
        };

        CPUStation0Pull = new List<GameObject>();
        CPUStation1Pull = new List<GameObject>();
        CPUStation2Pull = new List<GameObject>();
        CPUStation3Pull = new List<GameObject>();
        //TO ADD OTHER STATIONS
        CPUStationsPulls = new List<List<GameObject>>() {
            CPUStation0Pull, CPUStation1Pull,CPUStation2Pull,CPUStation3Pull
        };

        Star0Pull = new List<GameObject>();
        Star1Pull = new List<GameObject>();
        Star2Pull = new List<GameObject>();
        Star3Pull = new List<GameObject>();
        //TO ADD OTHER STARS
        starPulls = new List<List<GameObject>>() { 
            Star0Pull,Star1Pull,Star2Pull,Star3Pull
        };
        //aimingRectPull = new List<GameObject>();


        connectionLinePull = new List<GameObject>();

        //smallEnergyPull = new List<GameObject>();
        //midEnergyPull = new List<GameObject>();
        //bigEnergyPull = new List<GameObject>();

        //energyPulls = new List<List<GameObject>>() {
        //    smallEnergyPull,midEnergyPull,bigEnergyPull
        //};

        //for (int i = 0; i < pulled50Objects; i++)
        //{
        //    GameObject obj = (GameObject)Instantiate(smallEnergy);
        //    obj.SetActive(false);
        //    smallEnergyPull.Add(obj); 

        //    GameObject obj1 = (GameObject)Instantiate(midEnergy);
        //    obj1.SetActive(false);
        //    midEnergyPull.Add(obj1); 

        //    GameObject obj2 = (GameObject)Instantiate(bigEnergy);
        //    obj2.SetActive(false);
        //    bigEnergyPull.Add(obj2);
        //}

        for (int i = 0; i < pulledPreBurst; i++)
        {
            GameObject obj = (GameObject)Instantiate(CruisPreBurst);
            obj.SetActive(false);
            CruisPreBurstPull.Add(obj);

            GameObject obj1 = (GameObject)Instantiate(DestrPreBurst);
            obj1.SetActive(false);
            DestrPreBurstPull.Add(obj1);

            GameObject obj2 = (GameObject)Instantiate(CruisBurst);
            obj2.SetActive(false);
            CruisBurstPull.Add(obj2);

            GameObject obj3 = (GameObject)Instantiate(DestrBurst);
            obj3.SetActive(false);
            DestrBurstPull.Add(obj3);

            GameObject obj4 = (GameObject)Instantiate(Gun1BulletBurst);
            obj4.SetActive(false);
            Gun1BulletBurstPull.Add(obj4);
        }
        for (int i = 0; i < pulledPlayerGun1Bullets; i++)
        {
            GameObject obj = (GameObject)Instantiate(PlayerGun1Bullet);
            obj.SetActive(false);
            PlayerGun1BulletPull.Add(obj);

            //GameObject obj1 = (GameObject)Instantiate(StationBullet);
            //obj1.SetActive(false);
            //StationBulletPull.Add(obj1);

            GameObject obj2 = (GameObject)Instantiate(energyBall);
            obj2.SetActive(false);
            energyBallPull.Add(obj2);
        }
        for (int i = 0; i < pulled3Objects; i++)
        {
            //GameObject obj = (GameObject)Instantiate(gunRollUpFillingImage);
            //obj.transform.SetParent(parentCanvasUIElements, false);
            //obj.SetActive(false);
            //gunRollUpFillingImagePull.Add(obj); 
            
            GameObject obj1 = (GameObject)Instantiate(StationBurst);
            obj1.SetActive(false);
            StationBurstPull.Add(obj1);

            GameObject obj2 = (GameObject)Instantiate(playerStation0);
            obj2.SetActive(false);
            playerStation0Pull.Add(obj2); 
            
            GameObject obj4 = (GameObject)Instantiate(playerStation1);
            obj4.SetActive(false);
            playerStation1Pull.Add(obj4); 
            
            GameObject obj6 = (GameObject)Instantiate(playerStation2);
            obj6.SetActive(false);
            playerStation2Pull.Add(obj6); 
            
            GameObject obj7 = (GameObject)Instantiate(playerStation3);
            obj7.SetActive(false);
            playerStation3Pull.Add(obj7);

            GameObject obj3 = (GameObject)Instantiate(CPUStation0);
            obj3.SetActive(false);
            CPUStation0Pull.Add(obj3);

            GameObject obj5 = (GameObject)Instantiate(CPUStation1);
            obj5.SetActive(false);
            CPUStation1Pull.Add(obj5); 
            
            GameObject obj8 = (GameObject)Instantiate(CPUStation2);
            obj8.SetActive(false);
            CPUStation2Pull.Add(obj8); 
            
            GameObject obj9 = (GameObject)Instantiate(CPUStation3);
            obj9.SetActive(false);
            CPUStation3Pull.Add(obj9);
        }
        for (int i = 0; i < pulled15Objects; i++)
        {
            GameObject obj = (GameObject)Instantiate(Cruis4CPU);
            obj.SetActive(false);
            Cruis4CPUPull.Add(obj);

            GameObject obj1 = (GameObject)Instantiate(Destr4CPU);
            obj1.SetActive(false);
            Destr4CPUPull.Add(obj1); 
            
            GameObject obj6 = (GameObject)Instantiate(Cruis3CPU);
            obj6.SetActive(false);
            Cruis3CPUPull.Add(obj6);

            GameObject obj7 = (GameObject)Instantiate(Destr3CPU);
            obj7.SetActive(false);
            Destr3CPUPull.Add(obj7);

            GameObject obj8 = (GameObject)Instantiate(Cruis2CPU);
            obj8.SetActive(false);
            Cruis2CPUPull.Add(obj8);

            GameObject obj9 = (GameObject)Instantiate(Destr2CPU);
            obj9.SetActive(false);
            Destr2CPUPull.Add(obj9);

            GameObject obj10 = (GameObject)Instantiate(Destr2ParCPU);
            obj10.SetActive(false);
            Destr2ParCPUPull.Add(obj10);

            GameObject obj11 = (GameObject)Instantiate(Cruis1CPU);
            obj11.SetActive(false);
            Cruis1CPUPull.Add(obj11);

            GameObject obj12 = (GameObject)Instantiate(Destr1CPU);
            obj12.SetActive(false);
            Destr1CPUPull.Add(obj12);

            GameObject obj13 = (GameObject)Instantiate(Destr1ParCPU);
            obj13.SetActive(false);
            Destr1ParCPUPull.Add(obj13);


            GameObject obj2 = (GameObject)Instantiate(Cruis4Player);
            obj2.SetActive(false);
            Cruis4PlayerPull.Add(obj2);

            GameObject obj3 = (GameObject)Instantiate(Destr4Player);
            obj3.SetActive(false);
            Destr4PlayerPull.Add(obj3); 
            
            GameObject obj4 = (GameObject)Instantiate(Cruis3Player);
            obj4.SetActive(false);
            Cruis3PlayerPull.Add(obj4);

            GameObject obj5 = (GameObject)Instantiate(Destr3Player);
            obj5.SetActive(false);
            Destr3PlayerPull.Add(obj5); 
            
            GameObject obj14 = (GameObject)Instantiate(Cruis2Player);
            obj14.SetActive(false);
            Cruis2PlayerPull.Add(obj14);

            GameObject obj15 = (GameObject)Instantiate(Destr2Player);
            obj15.SetActive(false);
            Destr2PlayerPull.Add(obj15);

            GameObject obj16 = (GameObject)Instantiate(Destr2ParPlayer);
            obj16.SetActive(false);
            Destr2ParPlayerPull.Add(obj16); 
            
            GameObject obj17 = (GameObject)Instantiate(Cruis1Player);
            obj17.SetActive(false);
            Cruis1PlayerPull.Add(obj17);

            GameObject obj18 = (GameObject)Instantiate(Destr1Player);
            obj18.SetActive(false);
            Destr1PlayerPull.Add(obj18);

            GameObject obj19 = (GameObject)Instantiate(Destr1ParPlayer);
            obj19.SetActive(false);
            Destr1ParPlayerPull.Add(obj19);
        }
        for (int i = 0; i < pulled4Objects; i++)
        {
            GameObject obj = (GameObject)Instantiate(Star0);
            obj.SetActive(false);
            Star0Pull.Add(obj);

            GameObject obj2 = (GameObject)Instantiate(Star1);
            obj2.SetActive(false);
            Star1Pull.Add(obj2);

            GameObject obj3 = (GameObject)Instantiate(Star2);
            obj3.SetActive(false);
            Star2Pull.Add(obj3);

            GameObject obj4 = (GameObject)Instantiate(Star3);
            obj4.SetActive(false);
            Star3Pull.Add(obj4);

            GameObject obj1 = (GameObject)Instantiate(energon);
            obj1.SetActive(false);
            energonPull.Add(obj1);

            //GameObject obj5 = (GameObject)Instantiate(aimingRect);
            //obj5.transform.SetParent(parentCanvasOfAiming, false);
            //obj5.SetActive(false);
            //aimingRectPull.Add(obj5);
        }
        for (int i = 0; i < pulled50Objects; i++)
        {
            GameObject obj = (GameObject)Instantiate(connectionLine);
            obj.SetActive(false);
            connectionLinePull.Add(obj);
        }


        //GameObject objM = (GameObject)Instantiate(PlayerGunToDrop);
        //objM.SetActive(false);
        //PlayerGunToDropPull.Add(objM);
    }

    //public List<GameObject> GetAimingRectPullList()
    //{
    //    return aimingRectPull;
    //}

    public List<GameObject> GetCruisPreBurstPull()
    {
        return CruisPreBurstPull;
    }
    public List<GameObject> GetDestrPreBurstPull()
    {
        return DestrPreBurstPull;
    }
    public List<GameObject> GetCruisBurstPull()
    {
        return CruisBurstPull;
    }
    public List<GameObject> GetDestrBurstPull()
    {
        return DestrBurstPull;
    }

    //does not batter if it is the player bullet or CPU, determination is rulled by layer by CPUNumber
    public List<GameObject> GetPlayerGun1BulletPull()
    {
        return PlayerGun1BulletPull;
    }
    public List<GameObject> GetGun1BulletBurstPull()
    {
        return Gun1BulletBurstPull;
    }
    //public List<GameObject> GetPlayerGunToDropPulll()
    //{
    //    return PlayerGunToDropPull;
    //}

    //public List<GameObject> GetStationBulletPull()
    //{
    //    return StationBulletPull;
    //}
    //public List<GameObject> GetGunRollUpFillingImagePull()
    //{
    //    return gunRollUpFillingImagePull;
    //}
    public List<GameObject> GetenergyBallPulls()
    {
        return energyBallPull;
    }
    public List<GameObject> GetenergyBallPull()
    {
        return energyBallPull;
    }


    //public List<GameObject> GetDestr4CPUPullPull()
    //{
    //    return Destr4CPUPull;
    //}
    //public List<GameObject> GetCruis4CPUPullPull()
    //{
    //    return Cruis4CPUPull;
    //}
    //public List<GameObject> GetDestr4PlayerPullPull()
    //{
    //    return Destr4PlayerPull;
    //}
    //public List<GameObject> GetCruis4PlayerPullPull()
    //{
    //    return Cruis4PlayerPull;
    //}
    public List<GameObject> GetStationBurstPull()
    {
        return StationBurstPull;
    }

    public List<GameObject> GetStationPull(int index, int CPUNumber)
    {
        if (CPUNumber==0) return PlayerStationsPulls[index];
        else return CPUStationsPulls[index];
    }

    public List<GameObject> GetShipPull(int index, int CPUNumber)
    {
        if (CPUNumber == 0) return PlayerShipsPulls[index];
        else return CPUShipsPulls[index];
    }

    public List<GameObject> GetStarPull(int index)
    {
        return starPulls[index];
    }
    //public List<GameObject> GetEnergyPulls(int index)
    //{
    //    return energyPulls[index];
    //}


    public List<GameObject> GetEnergonPull()
    {
        return energonPull;
    }
    public List<GameObject> GetConnectionLinePull()
    {
        return connectionLinePull;
    }
    


    //universal method to set active proper game object from the list of GOs, it just needs to get correct List of game objects
    public GameObject GetGameObjectFromPull(List<GameObject> GOLists)
    {
        for (int i = 0; i < GOLists.Count; i++)
        {
            if (!GOLists[i].activeInHierarchy) return (GameObject)GOLists[i];
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(GOLists[0]);
            obj.SetActive(false);
            GOLists.Add(obj);
            return obj;
        }
        return null;
    }
}
