using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjectPullerDefence : MonoBehaviour
{
    public static ObjectPullerDefence current;

    

    public GameObject Asteroids1;
    public GameObject Asteroids2;
    public GameObject Asteroids3;
    public GameObject Asteroids4;

    public GameObject G1Bull;
    public GameObject G2Bull;
    public GameObject G3Bull;

    public GameObject GunBullBurst;
    //public GameObject G2BullBurst;
    //public GameObject G3BullBurst;

    public GameObject GMiniBull;

    public GameObject HPBonus;
    public GameObject ShieldBonus;
    public GameObject BulletsBonus;

    public GameObject D1ShipBullet;
    public GameObject D2ShipBullet;
    public GameObject D3ShipBullet;
    public GameObject D4ShipBullet;

    public GameObject D1;
    public GameObject D2;
    public GameObject D3;
    public GameObject D4;

    public GameObject D1ShipBulletBurst;
    public GameObject D2ShipBulletBurst;
    public GameObject D3ShipBulletBurst;
    public GameObject D4ShipBulletBurst;

    public GameObject shipBurst;

    //public GameObject gunBurst;


    private int pulledAmount2 = 2;
    private int pulledAmount10 = 10;
    private int pulledAmount5 = 5;
    private int pulledAmount15 = 15;
    private int pulledAmount80 = 80;

    private bool willGrow = true;

    private List<GameObject> Asteroids1Pull;
    private List<GameObject> Asteroids2Pull;
    private List<GameObject> Asteroids3Pull;
    private List<GameObject> Asteroids4Pull;

    private List<GameObject> G1BullPull;
    private List<GameObject> G2BullPull;
    private List<GameObject> G3BullPull;

    private List<GameObject> GunBullBurstPull;
    //private List<GameObject> G2BullBurstPull;
    //private List<GameObject> G3BullBurstPull;

    private List<GameObject> GMiniBullPull;

    private List<GameObject> HPBonusPull;
    private List<GameObject> ShieldBonusPull;
    private List<GameObject> BulletsBonusPull;


    private List<GameObject> D1ShipBulletPull;
    private List<GameObject> D2ShipBulletPull;
    private List<GameObject> D3ShipBulletPull;
    private List<GameObject> D4ShipBulletPull;

    private List<GameObject> D1Pull;
    private List<GameObject> D2Pull;
    private List<GameObject> D3Pull;
    private List<GameObject> D4Pull;

    private List<GameObject> D1ShipBulletBurstPull;
    private List<GameObject> D2ShipBulletBurstPull;
    private List<GameObject> D3ShipBulletBurstPull;
    private List<GameObject> D4ShipBulletBurstPull;

    private List<GameObject> shipBurstPull;

    //private List<GameObject> gunBurstPull;

    private void Awake()
    {
        current = this;
    }

    private void OnEnable()
    {
        Asteroids1Pull = new List<GameObject>();
        Asteroids2Pull = new List<GameObject>();
        Asteroids3Pull = new List<GameObject>();
        Asteroids4Pull = new List<GameObject>();

        D1Pull = new List<GameObject>();
        D2Pull = new List<GameObject>();
        D3Pull = new List<GameObject>();
        D4Pull = new List<GameObject>();


        shipBurstPull = new List<GameObject>();

        //create a pull of destroyer bullets with middle start pulled amount, cause there most likely will be more destroyers bullets
        for (int i = 0; i < pulledAmount10; i++)
        {
            GameObject obj1515 = (GameObject)Instantiate(Asteroids1);
            obj1515.SetActive(false);
            Asteroids1Pull.Add(obj1515);

            GameObject obj1516 = (GameObject)Instantiate(Asteroids2);
            obj1516.SetActive(false);
            Asteroids2Pull.Add(obj1516);

            GameObject obj1517 = (GameObject)Instantiate(Asteroids3);
            obj1517.SetActive(false);
            Asteroids3Pull.Add(obj1517);

            GameObject obj1518 = (GameObject)Instantiate(Asteroids4);
            obj1518.SetActive(false);
            Asteroids4Pull.Add(obj1518);
        }

        

        //create a pull of cruisers bullets with less start pulled amount, cause there most likely will be less cruiser bullets
        for (int i = 0; i < pulledAmount5; i++)
        {
            if (Lists.currentLevel > 7)
            {
                //creating a pull of stations
                GameObject obj = (GameObject)Instantiate(D1);
                obj.SetActive(false);
                D1Pull.Add(obj);
            }
            if (Lists.currentLevel > 3)
            {
                GameObject obj1 = (GameObject)Instantiate(D2);
                obj1.SetActive(false);
                D2Pull.Add(obj1);
            }

            GameObject obj2 = (GameObject)Instantiate(D3);
            obj2.SetActive(false);
            D3Pull.Add(obj2);

            GameObject obj3 = (GameObject)Instantiate(D4);
            obj3.SetActive(false);
            D4Pull.Add(obj3);
        }

        //listsOfGO = new List<List<GameObject>>();
        for (int i = 0; i < pulledAmount2; i++)
        {
            GameObject obj3 = (GameObject)Instantiate(shipBurst);
            obj3.SetActive(false);
            shipBurstPull.Add(obj3);

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        G1BullPull = new List<GameObject>();
        G2BullPull = new List<GameObject>();
        G3BullPull = new List<GameObject>();

        GunBullBurstPull = new List<GameObject>();
        //G2BullBurstPull = new List<GameObject>();
        //G3BullBurstPull = new List<GameObject>();


        GMiniBullPull = new List<GameObject>();

        HPBonusPull = new List<GameObject>();
        ShieldBonusPull = new List<GameObject>();
        BulletsBonusPull = new List<GameObject>();

        D1ShipBulletPull = new List<GameObject>();
        D2ShipBulletPull = new List<GameObject>();
        D3ShipBulletPull = new List<GameObject>();
        D4ShipBulletPull = new List<GameObject>();

        

        D1ShipBulletBurstPull = new List<GameObject>();
        D2ShipBulletBurstPull = new List<GameObject>();
        D3ShipBulletBurstPull = new List<GameObject>();
        D4ShipBulletBurstPull = new List<GameObject>();

        
        //gunBurstPull = new List<GameObject>();


        //listsOfGO = new List<List<GameObject>>();
        for (int i = 0; i < pulledAmount2; i++)
        {
            //creating a pull of stations
            GameObject obj = (GameObject)Instantiate(HPBonus);
            obj.SetActive(false);
            HPBonusPull.Add(obj);

            GameObject obj1 = (GameObject)Instantiate(ShieldBonus);
            obj1.SetActive(false);
            ShieldBonusPull.Add(obj1);

            GameObject obj2 = (GameObject)Instantiate(BulletsBonus);
            obj2.SetActive(false);
            BulletsBonusPull.Add(obj2);
            if (Lists.currentLevel > 7)
            {
                GameObject obj3 = (GameObject)Instantiate(D1ShipBulletBurst);
                obj3.SetActive(false);
                D1ShipBulletBurstPull.Add(obj3);
            }
            if (Lists.currentLevel > 3) {
                GameObject obj4 = (GameObject)Instantiate(D2ShipBulletBurst);
                obj4.SetActive(false);
                D2ShipBulletBurstPull.Add(obj4);
            }

            GameObject obj5 = (GameObject)Instantiate(D3ShipBulletBurst);
            obj5.SetActive(false);
            D3ShipBulletBurstPull.Add(obj5);

            GameObject obj6 = (GameObject)Instantiate(D4ShipBulletBurst);
            obj6.SetActive(false);
            D4ShipBulletBurstPull.Add(obj6);

            //GameObject obj3 = (GameObject)Instantiate(shipBurst);
            //obj3.SetActive(false);
            //shipBurstPull.Add(obj3);

            //GameObject obj4 = (GameObject)Instantiate(gunBurst);
            //obj4.SetActive(false);
            //gunBurstPull.Add(obj3);
        }

        

        for (int i = 0; i < pulledAmount15; i++)
        {
            if (Lists.currentLevel > 7)
            {
                GameObject obj = (GameObject)Instantiate(D1ShipBullet);
                obj.SetActive(false);
                D1ShipBulletPull.Add(obj);
            }
            if (Lists.currentLevel > 3)
            {
                GameObject obj1 = (GameObject)Instantiate(D2ShipBullet);
                obj1.SetActive(false);
                D2ShipBulletPull.Add(obj1);
            }

            GameObject obj2 = (GameObject)Instantiate(D3ShipBullet);
            obj2.SetActive(false);
            D3ShipBulletPull.Add(obj2);

            GameObject obj3 = (GameObject)Instantiate(D4ShipBullet);
            obj3.SetActive(false);
            D4ShipBulletPull.Add(obj3);

            GameObject obj4 = (GameObject)Instantiate(GunBullBurst);
            obj4.SetActive(false);
            GunBullBurstPull.Add(obj4);

            //GameObject obj5 = (GameObject)Instantiate(G2BullBurst);
            //obj5.SetActive(false);
            //G2BullBurstPull.Add(obj5);

            //GameObject obj6 = (GameObject)Instantiate(G3BullBurst);
            //obj6.SetActive(false);
            //G3BullBurstPull.Add(obj6);
        }

        for (int i = 0; i < pulledAmount80; i++)
        {
            //there is no need to create a pull of G3 bullets unitl 7 th level, cause there is no option to produce and use such guns
            if (Lists.currentLevel > 7)
            {
                GameObject obj3 = (GameObject)Instantiate(G3Bull);
                obj3.SetActive(false);
                G3BullPull.Add(obj3);
            }

            //there is no need to create a pull of G2 and mini guns bullets unitl 3 th level, cause there is no option to produce and use such guns
            if (Lists.currentLevel > 3) {
                GameObject obj2 = (GameObject)Instantiate(G2Bull);
                obj2.SetActive(false);
                G2BullPull.Add(obj2);

                GameObject obj = (GameObject)Instantiate(GMiniBull);
                obj.SetActive(false);
                GMiniBullPull.Add(obj);
            }


            GameObject obj1 = (GameObject)Instantiate(G1Bull);
            obj1.SetActive(false);
            G1BullPull.Add(obj1);

            

        }


    }

    //these are invoked from various classes (PlayerShipCPU, CPUShipCtrl etc) get the pulls of GO and call them (GO) with GetUniversalBullet method below
    //so they send the list they get from here to the parameters of the method GetUniversalBullet

    //Players objects lists on battle scene
    public List<GameObject> GetG1BullList()
    {
        return G1BullPull;
    }
    public List<GameObject> GetG2BullList()
    {
        return G2BullPull;
    }
    public List<GameObject> GetG3BullList()
    {
        return G3BullPull;
    }
    public List<GameObject> GetGMiniBullList()
    {
        return GMiniBullPull;
    }

    public List<GameObject> GetGunBullBurstPullList()
    {
        return GunBullBurstPull;
    }
    //public List<GameObject> GetG2BullBurstPullList()
    //{
    //    return G2BullBurstPull;
    //}
    //public List<GameObject> GetG1BullBurstPullList()
    //{
    //    return G1BullBurstPull;
    //}

    public List<GameObject> GetD4ShipBulletPullList()
    {
        return D4ShipBulletPull;
    }
    public List<GameObject> GetD3ShipBulletPullList()
    {
        return D3ShipBulletPull;
    }
    public List<GameObject> GetD2ShipBulletPullList()
    {
        return D2ShipBulletPull;
    }
    public List<GameObject> GetD1ShipBulletPullList()
    {
        return D1ShipBulletPull;
    }
    public List<GameObject> GetD4PullList()
    {
        return D4Pull;
    }
    public List<GameObject> GetD3PullList()
    {
        return D3Pull;
    }
    public List<GameObject> GetD2PullList()
    {
        return D2Pull;
    }
    public List<GameObject> GetD1PullList()
    {
        return D1Pull;
    }

    public List<GameObject> GetD4ShipBulletBurstPullList()
    {
        return D4ShipBulletBurstPull;
    }
    public List<GameObject> GetD3ShipBulletBurstPullList()
    {
        return D3ShipBulletBurstPull;
    }
    public List<GameObject> GetD2ShipBulletBurstPullList()
    {
        return D2ShipBulletBurstPull;
    }
    public List<GameObject> GetD1ShipBulletBurstPullList()
    {
        return D1ShipBulletBurstPull;
    }

    //public List<GameObject> GetGunBurstPullList()
    //{
    //    return gunBurstPull;
    //}

    public List<GameObject> GetShipBurstPullList()
    {
        return shipBurstPull;
    }
    public List<GameObject> GetBulletsBonusPullList()
    {
        return BulletsBonusPull;
    }
    public List<GameObject> GetShieldBonusPullList()
    {
        return ShieldBonusPull;
    }
    public List<GameObject> GetHPBonusPullList()
    {
        return HPBonusPull;
    }

    public List<GameObject> GetAsteroids1Pull()
    {
        return Asteroids1Pull;
    }
    public List<GameObject> GetAsteroids2Pull()
    {
        return Asteroids2Pull;
    }
    public List<GameObject> GetAsteroids3Pull()
    {
        return Asteroids3Pull;
    }
    public List<GameObject> GetAsteroids4Pull()
    {
        return Asteroids4Pull;
    }

    //universal method to set active proper game object from the list of GOs, it just needs to get correct List of game objects   
    public GameObject GetUniversalBullet(List<GameObject> GOLists)
    {
        for (int i = 0; i < GOLists.Count; i++)
        {
            if (!GOLists[i].activeInHierarchy) return GOLists[i];
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(GOLists[0]);
            GOLists.Add(obj);
            return obj;
        }
        return null;
    }

}
