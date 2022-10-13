//using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPuller : MonoBehaviour
{
    public static ObjectPuller current;

    public GameObject Asteroids1;
    public GameObject Asteroids2;
    public GameObject Asteroids3;
    public GameObject Asteroids4;

    //station bullets
    public GameObject S1BulletCPU;
    public GameObject S2BulletCPU;
    public GameObject S3BulletCPU;
    public GameObject S4BulletCPU;
    public GameObject SFedBulletCPU;
    public GameObject S1BulletPlay;
    public GameObject S2BulletPlay;
    public GameObject S3BulletPlay;
    public GameObject S4BulletPlay;

    //cruiser bullets
    public GameObject C1BulletCPU;
    public GameObject C2BulletCPU;
    public GameObject C3BulletCPU;
    public GameObject C4BulletCPU;
    public GameObject CFedBulletCPU;
    public GameObject D1BulletCPU;
    public GameObject D2BulletCPU;
    public GameObject D1PBulletCPU;
    public GameObject D2PBulletCPU;
    public GameObject D3BulletCPU;
    public GameObject D4BulletCPU;
    public GameObject DFedBulletCPU;

    public GameObject C1BulletPlay;
    public GameObject C2BulletPlay;
    public GameObject C3BulletPlay;
    public GameObject C4BulletPlay;
    public GameObject D1BulletPlay;
    public GameObject D2BulletPlay;
    public GameObject D1PBulletPlay;
    public GameObject D2PBulletPlay;
    public GameObject D3BulletPlay;
    public GameObject D4BulletPlay;

    public GameObject C1BulletBurst;
    public GameObject C2BulletBurst;
    public GameObject C3BulletBurst;
    public GameObject C4BulletBurst;
    public GameObject D1BulletBurst;
    public GameObject D2BulletBurst;
    public GameObject D3BulletBurst;
    public GameObject D4BulletBurst;

    public GameObject C1ShieldCPU;
    public GameObject C2ShieldCPU;
    public GameObject C3ShieldCPU;
    public GameObject CFedShieldCPU;
    public GameObject D1ShieldCPU;
    public GameObject D2ShieldCPU;
    public GameObject D3ShieldCPU;
    public GameObject DFedShieldCPU;

    public GameObject C1ShieldPlay;
    public GameObject C2ShieldPlay;
    public GameObject C3ShieldPlay;
    public GameObject D1ShieldPlay;
    public GameObject D2ShieldPlay;
    public GameObject D3ShieldPlay;

    public GameObject G1BulletCPU;
    public GameObject G2BulletCPU;
    public GameObject G3BulletCPU;

    public GameObject G1BulletPlay;
    public GameObject G2BulletPlay;
    public GameObject G3BulletPlay;

    public GameObject cruisParalEffect;
    public GameObject destrParalEffect;

    public GameObject cruisBurst;
    public GameObject destrBurst;

    public GameObject cruisPreBurst;
    public GameObject destrPreBurst;


    private int pulledAmount2 = 2;
    //private int pulledAmount = 5;
    private int pulledAmountLowerLess = 5;
    //private int pulledAmountLess = 20;
    //private int pulledAmountLowerMiddle = 35;
    //private int pulledAmountMiddle = 50;
    //private int pulledAmountMore = 90;

    private bool willGrow = true;

    //private List<List<GameObject>> listsOfGO;

    
    private List<GameObject> Asteroids1Pull;
    private List<GameObject> Asteroids2Pull;
    private List<GameObject> Asteroids3Pull;
    private List<GameObject> Asteroids4Pull;

    private List<GameObject> C1BulletsCPUPull;
    private List<GameObject> C2BulletsCPUPull;
    private List<GameObject> C3BulletsCPUPull;
    private List<GameObject> C4BulletsCPUPull;
    private List<GameObject> CFedBulletsCPUPull;
    private List<GameObject> D1BulletsCPUPull;
    private List<GameObject> D1PBulletsCPUPull;
    private List<GameObject> D2BulletsCPUPull;
    private List<GameObject> D2PBulletsCPUPull;
    private List<GameObject> D3BulletsCPUPull;
    private List<GameObject> D4BulletsCPUPull;
    private List<GameObject> DFedBulletsCPUPull;

    private List<GameObject> C1BulletsPlayerPull;
    private List<GameObject> C2BulletsPlayerPull;
    private List<GameObject> C3BulletsPlayerPull;
    private List<GameObject> C4BulletsPlayerPull;
    private List<GameObject> D1BulletsPlayerPull;
    private List<GameObject> D1PBulletsPlayerPull;
    private List<GameObject> D2BulletsPlayerPull;
    private List<GameObject> D2PBulletsPlayerPull;
    private List<GameObject> D3BulletsPlayerPull;
    private List<GameObject> D4BulletsPlayerPull;


    private List<GameObject> S1BulletsPlayerPull;
    private List<GameObject> S2BulletsPlayerPull;
    private List<GameObject> S3BulletsPlayerPull;
    private List<GameObject> S4BulletsPlayerPull; 
    private List<GameObject> S1BulletsCPUPull;
    private List<GameObject> S2BulletsCPUPull;
    private List<GameObject> S3BulletsCPUPull;
    private List<GameObject> S4BulletsCPUPull;
    private List<GameObject> SFedBulletsCPUPull;

    private List<GameObject> C1BulletsBurstPull;
    private List<GameObject> C2BulletsBurstPull;
    private List<GameObject> C3BulletsBurstPull;
    private List<GameObject> C4BulletsBurstPull;
    private List<GameObject> D1BulletsBurstPull;
    private List<GameObject> D2BulletsBurstPull;
    private List<GameObject> D3BulletsBurstPull;
    private List<GameObject> D4BulletsBurstPull;

    private List<GameObject> C1ShieldCPUPull;
    private List<GameObject> C2ShieldCPUPull;
    private List<GameObject> C3ShieldCPUPull;
    private List<GameObject> CFedShieldCPUPull;
    private List<GameObject> D1ShieldCPUPull;
    private List<GameObject> D2ShieldCPUPull;
    private List<GameObject> D3ShieldCPUPull;
    private List<GameObject> DFedShieldCPUPull;

    private List<GameObject> C1ShieldPlayPull;
    private List<GameObject> C2ShieldPlayPull;
    private List<GameObject> C3ShieldPlayPull;
    private List<GameObject> D1ShieldPlayPull;
    private List<GameObject> D2ShieldPlayPull;
    private List<GameObject> D3ShieldPlayPull;

    private List<GameObject> G1BulletsCPUPull;
    private List<GameObject> G2BulletsCPUPull;
    private List<GameObject> G3BulletsCPUPull;

    private List<GameObject> G1BulletsPlayerPull;
    private List<GameObject> G2BulletsPlayerPull;
    private List<GameObject> G3BulletsPlayerPull;

    private List<GameObject> cruisParalEffectPull;
    private List<GameObject> destrParalEffectPull;

    private List<GameObject> cruisBurstPull;
    private List<GameObject> destrBurstPull;

    private List<GameObject> cruisPreBurstPull;
    private List<GameObject> destrPreBurstPull;

    //those materials are used to set a proper color to ship power shields
    private Color redColorOfSheild;
    private Color whiteColorOfSheild;
    private Color cyanColorOfSheild;
    private Color purpleColorOfSheild;

    private void Awake()
    {
        current = this;
    }

    private void OnEnable()
    {
        //instantiating only the necessary bullets for stations tha will appear on scene
        //1-weaakest station green one, 4-blue station, 3 - purple station, 6-red station, 0-non station or guard station
        if (Lists.stationTypeLists == 1)
        {
            if (Lists.isPlayerStationOnDefence)
            {
                S4BulletsPlayerPull = new List<GameObject>();
                for (int i = 0; i < pulledAmount2; i++)
                {
                    //create a pull of C4BulletCPU
                    GameObject obj = (GameObject)Instantiate(S4BulletPlay);
                    obj.SetActive(false);
                    S4BulletsPlayerPull.Add(obj);
                }
            }
            else
            {
                S4BulletsCPUPull = new List<GameObject>();
                for (int i = 0; i < pulledAmount2; i++)
                {
                    //create a pull of C4BulletCPU
                    GameObject obj = (GameObject)Instantiate(S4BulletCPU);
                    obj.SetActive(false);
                    S4BulletsCPUPull.Add(obj);
                }
            }

        }
        else if (Lists.stationTypeLists == 4)
        {
            if (Lists.isPlayerStationOnDefence)
            {
                S3BulletsPlayerPull = new List<GameObject>();
                for (int i = 0; i < pulledAmount2; i++)
                {
                    //create a pull of C4BulletCPU
                    GameObject obj = (GameObject)Instantiate(S3BulletPlay);
                    obj.SetActive(false);
                    S3BulletsPlayerPull.Add(obj);
                }
            }
            else
            {
                S3BulletsCPUPull = new List<GameObject>();
                for (int i = 0; i < pulledAmount2; i++)
                {
                    //create a pull of C4BulletCPU
                    GameObject obj = (GameObject)Instantiate(S3BulletCPU);
                    obj.SetActive(false);
                    S3BulletsCPUPull.Add(obj);
                }
            }
        }
        else if (Lists.stationTypeLists == 3)
        {
            if (Lists.isPlayerStationOnDefence)
            {
                S2BulletsPlayerPull = new List<GameObject>();
                for (int i = 0; i < pulledAmount2; i++)
                {
                    //create a pull of C4BulletCPU
                    GameObject obj = (GameObject)Instantiate(S2BulletPlay);
                    obj.SetActive(false);
                    S2BulletsPlayerPull.Add(obj);
                }
            }
            else
            {
                S2BulletsCPUPull = new List<GameObject>();
                for (int i = 0; i < pulledAmount2; i++)
                {
                    //create a pull of C4BulletCPU
                    GameObject obj = (GameObject)Instantiate(S2BulletCPU);
                    obj.SetActive(false);
                    S2BulletsCPUPull.Add(obj);
                }
            }
        }
        else if (Lists.stationTypeLists == 6)
        {
            if (Lists.isPlayerStationOnDefence)
            {
                S1BulletsPlayerPull = new List<GameObject>();
                for (int i = 0; i < pulledAmount2; i++)
                {
                    //create a pull of C4BulletCPU
                    GameObject obj = (GameObject)Instantiate(S1BulletPlay);
                    obj.SetActive(false);
                    S1BulletsPlayerPull.Add(obj);
                }
            }
            else
            {
                S1BulletsCPUPull = new List<GameObject>();
                for (int i = 0; i < pulledAmount2; i++)
                {
                    //create a pull of C4BulletCPU
                    GameObject obj = (GameObject)Instantiate(S1BulletCPU);
                    obj.SetActive(false);
                    S1BulletsCPUPull.Add(obj);
                }
            }
        }
        else if (Lists.stationTypeLists != 0)
        {
            SFedBulletsCPUPull = new List<GameObject>();
            for (int i = 0; i < pulledAmount2; i++)
            {
                //create a pull of C4BulletCPU
                GameObject obj = (GameObject)Instantiate(SFedBulletCPU);
                obj.SetActive(false);
                SFedBulletsCPUPull.Add(obj);
            }
        }

        //S1BulletsCPUPull = new List<GameObject>();
        //S2BulletsCPUPull = new List<GameObject>();
        //S3BulletsCPUPull = new List<GameObject>();
        //S4BulletsCPUPull = new List<GameObject>();
        //SFedBulletsCPUPull = new List<GameObject>();
        //S1BulletsPlayerPull = new List<GameObject>();
        //S2BulletsPlayerPull = new List<GameObject>();
        //S3BulletsPlayerPull = new List<GameObject>();
        //S4BulletsPlayerPull = new List<GameObject>();

        Asteroids1Pull = new List<GameObject>();
        Asteroids2Pull = new List<GameObject>();
        Asteroids3Pull = new List<GameObject>();
        Asteroids4Pull = new List<GameObject>();
        //create a pull of destroyer bullets with middle start pulled amount, cause there most likely will be more destroyers bullets
        for (int i = 0; i < pulledAmountLowerLess; i++)
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

        if (Lists.playerFleetIsSet)
        {
            //instantiating the colors of players
            if (Lists.Cruis1OfPlayerForPull > 0 || Lists.Cruis1CPU > 0 || Lists.Destr1CPU > 0 || Lists.Destr1CPUParal > 0 || Lists.Destr1OfPlayerForPull > 0
                || Lists.Destr1OfPlayerParForPull > 0) redColorOfSheild = new Color(1.4f, 0, 0, 1);
            if (Lists.CruisGCPU > 0 || Lists.DestrGCPU > 0) whiteColorOfSheild = new Color(1.4f, 1.4f, 1.5f, 1);
            if (Lists.Cruis3OfPlayerForPull > 0 || Lists.Cruis3CPU > 0 || Lists.Cruis4OfPlayerForPull > 0 || Lists.Cruis4CPU > 0 || Lists.Destr3CPU > 0
                || Lists.Destr3OfPlayerForPull > 0) cyanColorOfSheild = new Color(0, 1.4f, 1.4f, 1);
            if (Lists.Cruis2OfPlayerForPull > 0 || Lists.Cruis2CPU > 0 || Lists.Destr2CPU > 0 || Lists.Destr2CPUParal > 0 || Lists.Destr2OfPlayerForPull > 0
                || Lists.Destr2OfPlayerParForPull > 0) purpleColorOfSheild = new Color(1.4f, 0, 1.4f, 1);


            //instantiating the bullets and shields of ships
            if (Lists.Cruis1CPU > 0)
            {
                C1BulletsCPUPull = new List<GameObject>();
                C1ShieldCPUPull = new List<GameObject>();

                //instantiating three bullets to pull cause cruis 1 makes 3 shots at once
                for (int i = 0; i < Lists.Cruis1CPU * 3; i++)
                {
                    GameObject obj = (GameObject)Instantiate(C1BulletCPU);
                    obj.SetActive(false);
                    C1BulletsCPUPull.Add(obj);
                }
                for (int i = 0; i < Lists.Cruis1CPU; i++)
                {
                    GameObject obj4 = (GameObject)Instantiate(C1ShieldCPU);
                    obj4.SetActive(false);
                    obj4.GetComponent<MeshRenderer>().material.SetColor("_Color", redColorOfSheild);
                    C1ShieldCPUPull.Add(obj4);
                }
            }
            if (Lists.Cruis2CPU > 0)
            {
                C2BulletsCPUPull = new List<GameObject>();
                C2ShieldCPUPull = new List<GameObject>();

                //instantiating three bullets to pull cause cruis 2 makes 2 shots at once
                for (int i = 0; i < Lists.Cruis2CPU * 2; i++)
                {
                    GameObject obj1 = (GameObject)Instantiate(C2BulletCPU);
                    obj1.SetActive(false);
                    C2BulletsCPUPull.Add(obj1);
                }
                for (int i = 0; i < Lists.Cruis2CPU; i++)
                {
                    GameObject obj4 = (GameObject)Instantiate(C2ShieldCPU);
                    obj4.SetActive(false);
                    obj4.GetComponent<MeshRenderer>().material.SetColor("_Color", purpleColorOfSheild);
                    C2ShieldCPUPull.Add(obj4);
                }
            }
            if (Lists.Cruis3CPU > 0)
            {
                C3BulletsCPUPull = new List<GameObject>();
                C3ShieldCPUPull = new List<GameObject>();

                for (int i = 0; i < Lists.Cruis3CPU; i++)
                {
                    GameObject obj1 = (GameObject)Instantiate(C3BulletCPU);
                    obj1.SetActive(false);
                    C3BulletsCPUPull.Add(obj1);

                    GameObject obj4 = (GameObject)Instantiate(C3ShieldCPU);
                    obj4.SetActive(false);
                    obj4.GetComponent<MeshRenderer>().material.SetColor("_Color", cyanColorOfSheild);
                    C3ShieldCPUPull.Add(obj4);
                }
            }
            if (Lists.Cruis4CPU > 0)
            {
                C4BulletsCPUPull = new List<GameObject>();
                C3ShieldCPUPull = new List<GameObject>();

                for (int i = 0; i < Lists.Cruis4CPU; i++)
                {
                    GameObject obj1 = (GameObject)Instantiate(C4BulletCPU);
                    obj1.SetActive(false);
                    C4BulletsCPUPull.Add(obj1);

                    GameObject obj4 = (GameObject)Instantiate(C3ShieldCPU);
                    obj4.SetActive(false);
                    obj4.GetComponent<MeshRenderer>().material.SetColor("_Color", cyanColorOfSheild);
                    C3ShieldCPUPull.Add(obj4);
                }
            }
            if (Lists.CruisGCPU > 0)
            {
                CFedBulletsCPUPull = new List<GameObject>();
                CFedShieldCPUPull = new List<GameObject>();

                //instantiating three bullets to pull cause cruis G makes 2 shots at once
                for (int i = 0; i < Lists.CruisGCPU * 2; i++)
                {
                    GameObject obj1 = (GameObject)Instantiate(CFedBulletCPU);
                    obj1.SetActive(false);
                    CFedBulletsCPUPull.Add(obj1);
                }
                for (int i = 0; i < Lists.CruisGCPU; i++)
                {
                    GameObject obj4 = (GameObject)Instantiate(CFedShieldCPU);
                    obj4.SetActive(false);
                    obj4.GetComponent<MeshRenderer>().material.SetColor("_Color", whiteColorOfSheild);
                    CFedShieldCPUPull.Add(obj4);
                }
            }
            if (Lists.Destr1CPU > 0 || Lists.Destr1CPUParal > 0)
            {
                D1BulletsCPUPull = new List<GameObject>();
                D1ShieldCPUPull = new List<GameObject>();
                if (Lists.Destr1CPUParal > 0) D1PBulletsCPUPull = new List<GameObject>();

                for (int i = 0; i < Lists.Destr1CPU * 2; i++)
                {
                    GameObject obj1 = (GameObject)Instantiate(D1BulletCPU);
                    obj1.SetActive(false);
                    D1BulletsCPUPull.Add(obj1);
                }
                for (int i = 0; i < Lists.Destr1CPUParal; i++)
                {
                    //destr 1 paral requires regular bullet instantiation as well (and since it makes two shots at once it will need two instantiations)
                    GameObject obj2 = (GameObject)Instantiate(D1BulletCPU);
                    obj2.SetActive(false);
                    D1BulletsCPUPull.Add(obj2);

                    GameObject obj3 = (GameObject)Instantiate(D1BulletCPU);
                    obj3.SetActive(false);
                    D1BulletsCPUPull.Add(obj3);

                    //intantiating in pull paralizer bullet
                    GameObject obj1 = (GameObject)Instantiate(D1PBulletCPU);
                    obj1.SetActive(false);
                    D1PBulletsCPUPull.Add(obj1);
                }
                int destrCount = Lists.Destr1CPUParal + Lists.Destr1CPU;
                for (int i = 0; i < destrCount; i++)
                {
                    GameObject obj4 = (GameObject)Instantiate(D1ShieldCPU);
                    obj4.SetActive(false);
                    obj4.GetComponent<MeshRenderer>().material.SetColor("_Color", redColorOfSheild);
                    D1ShieldCPUPull.Add(obj4);
                }
            }
            if (Lists.Destr2CPU > 0 || Lists.Destr2CPUParal > 0)
            {
                D2BulletsCPUPull = new List<GameObject>();
                D2ShieldCPUPull = new List<GameObject>();
                if (Lists.Destr2CPUParal > 0) D2PBulletsCPUPull = new List<GameObject>();
                for (int i = 0; i < Lists.Destr2CPU; i++)
                {
                    GameObject obj1 = (GameObject)Instantiate(D2BulletCPU);
                    obj1.SetActive(false);
                    D2BulletsCPUPull.Add(obj1);
                }
                for (int i = 0; i < Lists.Destr2CPUParal; i++)
                {
                    //destr 2 paral requires regular bullet instantiation as well 
                    GameObject obj2 = (GameObject)Instantiate(D2BulletCPU);
                    obj2.SetActive(false);
                    D2BulletsCPUPull.Add(obj2);

                    GameObject obj1 = (GameObject)Instantiate(D2PBulletCPU);
                    obj1.SetActive(false);
                    D2PBulletsCPUPull.Add(obj1);
                }
                int destrCount = Lists.Destr2CPUParal + Lists.Destr2CPU;
                for (int i = 0; i < destrCount; i++)
                {
                    GameObject obj4 = (GameObject)Instantiate(D2ShieldCPU);
                    obj4.SetActive(false);
                    obj4.GetComponent<MeshRenderer>().material.SetColor("_Color", purpleColorOfSheild);
                    D2ShieldCPUPull.Add(obj4);
                }
            }
            if (Lists.Destr3CPU > 0)
            {
                D3BulletsCPUPull = new List<GameObject>();
                D3ShieldCPUPull = new List<GameObject>();

                for (int i = 0; i < Lists.Destr3CPU; i++)
                {
                    GameObject obj1 = (GameObject)Instantiate(D3BulletCPU);
                    obj1.SetActive(false);
                    D3BulletsCPUPull.Add(obj1);

                    GameObject obj4 = (GameObject)Instantiate(D3ShieldCPU);
                    obj4.SetActive(false);
                    obj4.GetComponent<MeshRenderer>().material.SetColor("_Color", cyanColorOfSheild);
                    D3ShieldCPUPull.Add(obj4);
                }
            }
            if (Lists.Destr4CPU > 0)
            {
                D4BulletsCPUPull = new List<GameObject>();

                for (int i = 0; i < Lists.Destr4CPU; i++)
                {
                    GameObject obj1 = (GameObject)Instantiate(D4BulletCPU);
                    obj1.SetActive(false);
                    D4BulletsCPUPull.Add(obj1);
                }
            }
            if (Lists.DestrGCPU > 0)
            {
                DFedBulletsCPUPull = new List<GameObject>();
                DFedShieldCPUPull = new List<GameObject>();

                for (int i = 0; i < Lists.DestrGCPU; i++)
                {
                    GameObject obj1 = (GameObject)Instantiate(DFedBulletCPU);
                    obj1.SetActive(false);
                    DFedBulletsCPUPull.Add(obj1);

                    GameObject obj4 = (GameObject)Instantiate(DFedShieldCPU);
                    obj4.SetActive(false);
                    obj4.GetComponent<MeshRenderer>().material.SetColor("_Color", whiteColorOfSheild);
                    DFedShieldCPUPull.Add(obj4);
                }
            }

            if (Lists.Cruis1OfPlayerForPull > 0)
            {
                C1BulletsPlayerPull = new List<GameObject>();
                C1ShieldPlayPull = new List<GameObject>();

                //instantiating three bullets to pull cause cruis 1 makes 3 shots at once
                for (int i = 0; i < Lists.Cruis1OfPlayerForPull * 3; i++)
                {
                    GameObject obj10 = (GameObject)Instantiate(C1BulletPlay);
                    obj10.SetActive(false);
                    C1BulletsPlayerPull.Add(obj10);
                }

                for (int i = 0; i < Lists.Cruis1OfPlayerForPull; i++)
                {
                    GameObject obj4 = (GameObject)Instantiate(C1ShieldPlay);
                    obj4.SetActive(false);
                    obj4.GetComponent<MeshRenderer>().material.SetColor("_Color", redColorOfSheild);
                    C1ShieldPlayPull.Add(obj4);
                }
            }
            if (Lists.Cruis2OfPlayerForPull > 0)
            {
                C2BulletsPlayerPull = new List<GameObject>();
                C2ShieldPlayPull = new List<GameObject>();
                //instantiating three bullets to pull cause cruis 2 makes 2 shots at once
                for (int i = 0; i < Lists.Cruis2OfPlayerForPull * 2; i++)
                {
                    GameObject obj10 = (GameObject)Instantiate(C2BulletPlay);
                    obj10.SetActive(false);
                    C2BulletsPlayerPull.Add(obj10);
                }

                for (int i = 0; i < Lists.Cruis2OfPlayerForPull; i++)
                {
                    GameObject obj4 = (GameObject)Instantiate(C2ShieldPlay);
                    obj4.SetActive(false);
                    obj4.GetComponent<MeshRenderer>().material.SetColor("_Color", purpleColorOfSheild);
                    C2ShieldPlayPull.Add(obj4);
                }
            }
            if (Lists.Cruis3OfPlayerForPull > 0)
            {
                C3BulletsPlayerPull = new List<GameObject>();
                C3ShieldPlayPull = new List<GameObject>();

                for (int i = 0; i < Lists.Cruis3OfPlayerForPull; i++)
                {
                    GameObject obj10 = (GameObject)Instantiate(C3BulletPlay);
                    obj10.SetActive(false);
                    C3BulletsPlayerPull.Add(obj10);

                    GameObject obj4 = (GameObject)Instantiate(C3ShieldPlay);
                    obj4.SetActive(false);
                    obj4.GetComponent<MeshRenderer>().material.SetColor("_Color", cyanColorOfSheild);
                    C3ShieldPlayPull.Add(obj4);
                }
            }
            if (Lists.Cruis4OfPlayerForPull > 0)
            {
                C4BulletsPlayerPull = new List<GameObject>();
                C3ShieldPlayPull = new List<GameObject>();

                for (int i = 0; i < Lists.Cruis4OfPlayerForPull; i++)
                {
                    GameObject obj10 = (GameObject)Instantiate(C4BulletPlay);
                    obj10.SetActive(false);
                    C4BulletsPlayerPull.Add(obj10);

                    GameObject obj4 = (GameObject)Instantiate(C3ShieldPlay);
                    obj4.SetActive(false);
                    obj4.GetComponent<MeshRenderer>().material.SetColor("_Color", cyanColorOfSheild);
                    C3ShieldPlayPull.Add(obj4);
                }
            }
            if (Lists.Destr1OfPlayerForPull > 0 || Lists.Destr1OfPlayerParForPull > 0)
            {
                D1BulletsPlayerPull = new List<GameObject>();
                D1ShieldPlayPull = new List<GameObject>();
                if (Lists.Destr1OfPlayerParForPull > 0) D1PBulletsPlayerPull = new List<GameObject>();

                for (int i = 0; i < Lists.Destr1OfPlayerForPull * 2; i++)
                {
                    GameObject obj19 = (GameObject)Instantiate(D1BulletPlay);
                    obj19.SetActive(false);
                    D1BulletsPlayerPull.Add(obj19);
                }
                for (int i = 0; i < Lists.Destr1OfPlayerParForPull; i++)
                {

                    //destr 1 paral requires regular bullet instantiation as well (and it makes two shots at once and since so it needs two instantiations)
                    GameObject obj18 = (GameObject)Instantiate(D1BulletPlay);
                    obj18.SetActive(false);
                    D1BulletsPlayerPull.Add(obj18);
                    GameObject obj17 = (GameObject)Instantiate(D1BulletPlay);
                    obj17.SetActive(false);
                    D1BulletsPlayerPull.Add(obj17);

                    GameObject obj19 = (GameObject)Instantiate(D1PBulletPlay);
                    obj19.SetActive(false);
                    D1PBulletsPlayerPull.Add(obj19);
                }

                int destrCount = Lists.Destr1OfPlayerParForPull + Lists.Destr1OfPlayerForPull;
                for (int i = 0; i < destrCount; i++)
                {
                    GameObject obj4 = (GameObject)Instantiate(D1ShieldPlay);
                    obj4.SetActive(false);
                    obj4.GetComponent<MeshRenderer>().material.SetColor("_Color", redColorOfSheild);
                    D1ShieldPlayPull.Add(obj4);
                }
            }
            if (Lists.Destr2OfPlayerForPull > 0 || Lists.Destr2OfPlayerParForPull > 0)
            {
                D2BulletsPlayerPull = new List<GameObject>();
                D2ShieldPlayPull = new List<GameObject>();
                if (Lists.Destr2OfPlayerParForPull > 0) D2PBulletsPlayerPull = new List<GameObject>();

                for (int i = 0; i < Lists.Destr2OfPlayerForPull; i++)
                {
                    GameObject obj19 = (GameObject)Instantiate(D2BulletPlay);
                    obj19.SetActive(false);
                    D2BulletsPlayerPull.Add(obj19);
                }
                for (int i = 0; i < Lists.Destr2OfPlayerParForPull; i++)
                {
                    //destr 2 paral requires regular bullet instantiation as well 
                    GameObject obj18 = (GameObject)Instantiate(D2BulletPlay);
                    obj18.SetActive(false);
                    D2BulletsPlayerPull.Add(obj18);

                    GameObject obj19 = (GameObject)Instantiate(D2PBulletPlay);
                    obj19.SetActive(false);
                    D2PBulletsPlayerPull.Add(obj19);
                }
                int destrCount = Lists.Destr2OfPlayerParForPull + Lists.Destr2OfPlayerForPull;
                for (int i = 0; i < destrCount; i++)
                {
                    GameObject obj4 = (GameObject)Instantiate(D2ShieldPlay);
                    obj4.SetActive(false);
                    obj4.GetComponent<MeshRenderer>().material.SetColor("_Color", purpleColorOfSheild);
                    D2ShieldPlayPull.Add(obj4);
                }
            }
            if (Lists.Destr3OfPlayerForPull > 0)
            {
                D3BulletsPlayerPull = new List<GameObject>();
                D3ShieldPlayPull = new List<GameObject>();

                for (int i = 0; i < Lists.Destr3OfPlayerForPull; i++)
                {
                    GameObject obj19 = (GameObject)Instantiate(D3BulletPlay);
                    obj19.SetActive(false);
                    D3BulletsPlayerPull.Add(obj19);

                    GameObject obj4 = (GameObject)Instantiate(D3ShieldPlay);
                    obj4.SetActive(false);
                    obj4.GetComponent<MeshRenderer>().material.SetColor("_Color", cyanColorOfSheild);
                    D3ShieldPlayPull.Add(obj4);
                }
            }
            if (Lists.Destr4OfPlayerForPull > 0)
            {
                D4BulletsPlayerPull = new List<GameObject>();
                for (int i = 0; i < Lists.Destr4OfPlayerForPull; i++)
                {
                    GameObject obj19 = (GameObject)Instantiate(D4BulletPlay);
                    obj19.SetActive(false);
                    D4BulletsPlayerPull.Add(obj19);
                }
            }


            //instantiating the bulletbursts of ships
            if (Lists.Cruis1CPU > 0 || Lists.Cruis1OfPlayerForPull > 0)
            {
                C1BulletsBurstPull = new List<GameObject>();
                //cruis 1 makes 3 shots at once
                int CCount = (Lists.Cruis1CPU + Lists.Cruis1OfPlayerForPull) * 3;
                for (int i = 0; i < CCount; i++)
                {
                    GameObject obj = (GameObject)Instantiate(C1BulletBurst);
                    obj.SetActive(false);
                    C1BulletsBurstPull.Add(obj);
                }

            }
            if (Lists.Cruis2CPU > 0 || Lists.Cruis2OfPlayerForPull > 0 || Lists.CruisGCPU > 0)
            {
                C2BulletsBurstPull = new List<GameObject>();
                //cruis 2 makes 2 shots at once
                int CCount = (Lists.Cruis2CPU + Lists.Cruis2OfPlayerForPull + Lists.CruisGCPU) * 2;
                for (int i = 0; i < CCount; i++)
                {
                    GameObject obj = (GameObject)Instantiate(C2BulletBurst);
                    obj.SetActive(false);
                    C2BulletsBurstPull.Add(obj);
                }
            }
            if (Lists.Cruis3CPU > 0 || Lists.Cruis3OfPlayerForPull > 0)
            {
                C3BulletsBurstPull = new List<GameObject>();
                //cruis 2 makes 2 shots at once
                int CCount = Lists.Cruis3CPU + Lists.Cruis3OfPlayerForPull;
                for (int i = 0; i < CCount; i++)
                {
                    GameObject obj = (GameObject)Instantiate(C3BulletBurst);
                    obj.SetActive(false);
                    C3BulletsBurstPull.Add(obj);
                }
            }
            if (Lists.Cruis4CPU > 0 || Lists.Cruis4OfPlayerForPull > 0)
            {
                C4BulletsBurstPull = new List<GameObject>();

                int CCount = Lists.Cruis4CPU + Lists.Cruis4OfPlayerForPull;
                for (int i = 0; i < CCount; i++)
                {
                    GameObject obj = (GameObject)Instantiate(C4BulletBurst);
                    obj.SetActive(false);
                    C4BulletsBurstPull.Add(obj);
                }
            }
            if (Lists.Destr1CPU > 0 || Lists.Destr1CPUParal > 0 || Lists.Destr1OfPlayerForPull > 0 || Lists.Destr1OfPlayerParForPull > 0)
            {
                D1BulletsBurstPull = new List<GameObject>();
                //D1 makes 2 shots at once
                int CCount = Lists.Destr1CPU * 2 + Lists.Destr1CPUParal + Lists.Destr1OfPlayerForPull * 2 + Lists.Destr1OfPlayerParForPull;
                for (int i = 0; i < CCount; i++)
                {
                    GameObject obj = (GameObject)Instantiate(D1BulletBurst);
                    obj.SetActive(false);
                    D1BulletsBurstPull.Add(obj);
                }
            }
            if (Lists.Destr2CPU > 0 || Lists.Destr2CPUParal > 0 || Lists.Destr2OfPlayerForPull > 0 || Lists.Destr2OfPlayerParForPull > 0 || Lists.DestrGCPU > 0)
            {
                D2BulletsBurstPull = new List<GameObject>();
                int CCount = Lists.Destr2CPU + Lists.Destr2CPUParal + Lists.Destr2OfPlayerForPull + Lists.Destr2OfPlayerParForPull+ Lists.DestrGCPU;
                for (int i = 0; i < CCount; i++)
                {
                    GameObject obj = (GameObject)Instantiate(D2BulletBurst);
                    obj.SetActive(false);
                    D2BulletsBurstPull.Add(obj);
                }
            }
            if (Lists.Destr3CPU > 0 || Lists.Destr3OfPlayerForPull > 0)
            {
                D3BulletsBurstPull = new List<GameObject>();

                int CCount = Lists.Destr3CPU + Lists.Destr3OfPlayerForPull;
                for (int i = 0; i < CCount; i++)
                {
                    GameObject obj = (GameObject)Instantiate(D3BulletBurst);
                    obj.SetActive(false);
                    D3BulletsBurstPull.Add(obj);
                }
            }
            if (Lists.Destr4CPU > 0 || Lists.Destr4OfPlayerForPull > 0)
            {
                D4BulletsBurstPull = new List<GameObject>();
                int CCount = Lists.Destr4CPU + Lists.Destr4OfPlayerForPull;
                for (int i = 0; i < CCount; i++)
                {
                    GameObject obj = (GameObject)Instantiate(D4BulletBurst);
                    obj.SetActive(false);
                    D4BulletsBurstPull.Add(obj);
                }
            }

            //instantiating the bullet burts for station bullets,cause they may lost them if there will not be decent cruiser that correspond to station 
            //for example stationC with only cruiser 3. In this case there will not be pulled cruiser 2 bullet for station
            if (Lists.stationTypeLists == 1)
            {
                if (C4BulletsBurstPull == null)
                {
                    C4BulletsBurstPull = new List<GameObject>();
                    for (int i = 0; i < 1; i++)
                    {
                        GameObject obj = (GameObject)Instantiate(C4BulletBurst);
                        obj.SetActive(false);
                        C4BulletsBurstPull.Add(obj);
                    }
                }
            }
            else if (Lists.stationTypeLists == 4)
            {
                if (C3BulletsBurstPull == null)
                {
                    C3BulletsBurstPull = new List<GameObject>();
                    for (int i = 0; i < 1; i++)
                    {
                        GameObject obj = (GameObject)Instantiate(C3BulletBurst);
                        obj.SetActive(false);
                        C3BulletsBurstPull.Add(obj);
                    }
                }
            }
            else if (Lists.stationTypeLists == 3)
            {
                if (C2BulletsBurstPull == null)
                {
                    C2BulletsBurstPull = new List<GameObject>();
                    for (int i = 0; i < 1; i++)
                    {
                        GameObject obj = (GameObject)Instantiate(C2BulletBurst);
                        obj.SetActive(false);
                        C2BulletsBurstPull.Add(obj);
                    }
                }
            }
            else if (Lists.stationTypeLists == 6)
            {
                if (C1BulletsBurstPull == null)
                {
                    C1BulletsBurstPull = new List<GameObject>();
                    for (int i = 0; i < 1; i++)
                    {
                        GameObject obj = (GameObject)Instantiate(C1BulletBurst);
                        obj.SetActive(false);
                        C1BulletsBurstPull.Add(obj);
                    }
                }
            }
            else if (Lists.stationTypeLists != 0)
            {
                if (C2BulletsBurstPull == null)
                {
                    C2BulletsBurstPull = new List<GameObject>();
                    for (int i = 0; i < 1; i++)
                    {
                        GameObject obj = (GameObject)Instantiate(C2BulletBurst);
                        obj.SetActive(false);
                        C2BulletsBurstPull.Add(obj);
                    }
                }
            }

            //instantiating the gun bullets
            if (Lists.Gun1CPU > 0)
            {
                G1BulletsCPUPull = new List<GameObject>();

                for (int i = 0; i < 30 * Lists.Gun1CPU; i++)
                {
                    GameObject obj4 = (GameObject)Instantiate(G1BulletCPU);
                    obj4.SetActive(false);
                    G1BulletsCPUPull.Add(obj4);
                }

            }
            if (Lists.Gun2CPU > 0)
            {
                G2BulletsCPUPull = new List<GameObject>();
                for (int i = 0; i < 40 * Lists.Gun2CPU; i++)
                {
                    GameObject obj5 = (GameObject)Instantiate(G2BulletCPU);
                    obj5.SetActive(false);
                    G2BulletsCPUPull.Add(obj5);
                }

            }
            if (Lists.Gun3CPU > 0)
            {
                G3BulletsCPUPull = new List<GameObject>();
                for (int i = 0; i < 50 * Lists.Gun3CPU; i++)
                {
                    GameObject obj5 = (GameObject)Instantiate(G3BulletCPU);
                    obj5.SetActive(false);
                    G3BulletsCPUPull.Add(obj5);
                }
            }

            if (Lists.Gun1OfPlayerForPull > 0)
            {
                G1BulletsPlayerPull = new List<GameObject>();
                for (int i = 0; i < 30 * Lists.Gun1OfPlayerForPull; i++)
                {
                    GameObject obj1 = (GameObject)Instantiate(G1BulletPlay);
                    obj1.SetActive(false);
                    G1BulletsPlayerPull.Add(obj1);
                }

            }
            if (Lists.Gun2OfPlayerForPull > 0)
            {
                G2BulletsPlayerPull = new List<GameObject>();

                for (int i = 0; i < 40 * Lists.Gun2OfPlayerForPull; i++)
                {
                    GameObject obj1 = (GameObject)Instantiate(G2BulletPlay);
                    obj1.SetActive(false);
                    G2BulletsPlayerPull.Add(obj1);
                }
            }
            if (Lists.Gun3OfPlayerForPull > 0)
            {
                G3BulletsPlayerPull = new List<GameObject>();
                for (int i = 0; i < 50 * Lists.Gun3OfPlayerForPull; i++)
                {
                    GameObject obj1 = (GameObject)Instantiate(G3BulletPlay);
                    obj1.SetActive(false);
                    G3BulletsPlayerPull.Add(obj1);
                }
            }



            //instatiating the paral effects of ships
            if (Lists.Destr2CPUParal > 0 || Lists.Destr1CPUParal > 0 || Lists.Destr1OfPlayerParForPull > 0 || Lists.Destr2OfPlayerParForPull > 0
                || Lists.Cruis2OfPlayerForPull > 0 || Lists.Cruis2CPU > 0)
            {
                cruisParalEffectPull = new List<GameObject>();
                destrParalEffectPull = new List<GameObject>();
                int cruisersCount = Lists.Cruis1CPU + Lists.Cruis2CPU + Lists.Cruis3CPU + Lists.Cruis4CPU + Lists.CruisGCPU + Lists.Cruis1OfPlayerForPull +
                    Lists.Cruis2OfPlayerForPull + Lists.Cruis3OfPlayerForPull + Lists.Cruis4OfPlayerForPull;
                int destrCount = Lists.Destr1CPU + Lists.Destr1CPUParal + Lists.Destr2CPU + Lists.Destr2CPUParal + Lists.Destr3CPU + Lists.Destr4CPU + Lists.DestrGCPU +
                    Lists.Destr1OfPlayerForPull + Lists.Destr1OfPlayerParForPull + Lists.Destr2OfPlayerForPull + Lists.Destr2OfPlayerParForPull + Lists.Destr3OfPlayerForPull +
                    Lists.Destr4OfPlayerForPull;
                for (int i = 0; i < cruisersCount; i++)
                {
                    GameObject obj20 = (GameObject)Instantiate(cruisParalEffect);
                    obj20.SetActive(false);
                    cruisParalEffectPull.Add(obj20);
                }
                for (int i = 0; i < destrCount; i++)
                {
                    GameObject obj5 = (GameObject)Instantiate(destrParalEffect);
                    obj5.SetActive(false);
                    destrParalEffectPull.Add(obj5);
                }
            }

            cruisBurstPull = new List<GameObject>();
            destrBurstPull = new List<GameObject>();

            cruisPreBurstPull = new List<GameObject>();
            destrPreBurstPull = new List<GameObject>();

            //instatiating the burts of ships
            for (int i = 0; i < 8; i++)
            {
                //create a pull of destrBurst effect 
                GameObject obj9 = (GameObject)Instantiate(destrBurst);
                obj9.SetActive(false);
                destrBurstPull.Add(obj9);

                //create a pull of destrBurst effect 
                GameObject obj99 = (GameObject)Instantiate(destrPreBurst);
                obj99.SetActive(false);
                destrPreBurstPull.Add(obj99);
            }
            for (int i = 0; i < 4; i++)
            {
                //create a pull of destrBurst effect 
                GameObject obj9 = (GameObject)Instantiate(cruisBurst);
                obj9.SetActive(false);
                cruisBurstPull.Add(obj9);

                //create a pull of destrBurst effect 
                GameObject obj99 = (GameObject)Instantiate(cruisPreBurst);
                obj99.SetActive(false);
                cruisPreBurstPull.Add(obj99);
            }
        }


        //create a pull of station bullets with less start pulled amount, cause there most likely will be less cruiser bullets
        //for (int i = 0; i < pulledAmount2; i++)
        //{
        //    GameObject obj = (GameObject)Instantiate(S1BulletCPU);
        //    obj.SetActive(false);
        //    S1BulletsCPUPull.Add(obj);

        //    //create a pull of C2BulletCPU
        //    GameObject obj1 = (GameObject)Instantiate(S2BulletCPU);
        //    obj1.SetActive(false);
        //    S2BulletsCPUPull.Add(obj1);

        //    //create a pull of C3BulletCPU
        //    GameObject obj2 = (GameObject)Instantiate(S3BulletCPU);
        //    obj2.SetActive(false);
        //    S3BulletsCPUPull.Add(obj2);

        //    //create a pull of C4BulletCPU
        //    GameObject obj3 = (GameObject)Instantiate(S4BulletCPU);
        //    obj3.SetActive(false);
        //    S4BulletsCPUPull.Add(obj3);

        //    //create a pull of C4BulletCPU
        //    GameObject obj4 = (GameObject)Instantiate(SFedBulletCPU);
        //    obj4.SetActive(false);
        //    SFedBulletsCPUPull.Add(obj4);

        //    //create a pull of C1BulletCPU
        //    GameObject obj10 = (GameObject)Instantiate(S1BulletPlay);
        //    obj10.SetActive(false);
        //    S1BulletsPlayerPull.Add(obj10);

        //    //create a pull of C2BulletCPU
        //    GameObject obj11 = (GameObject)Instantiate(S2BulletPlay);
        //    obj11.SetActive(false);
        //    S2BulletsPlayerPull.Add(obj11);

        //    //create a pull of C3BulletCPU
        //    GameObject obj12 = (GameObject)Instantiate(S3BulletPlay);
        //    obj12.SetActive(false);
        //    S3BulletsPlayerPull.Add(obj12);

        //    //create a pull of C4BulletCPU
        //    GameObject obj13 = (GameObject)Instantiate(S4BulletPlay);
        //    obj13.SetActive(false);
        //    S4BulletsPlayerPull.Add(obj13);
        //}
    }

    #region OldStart
    // Start is called before the first frame update
    //void Start()
    //{
        

    //    #region old code

    //    //listsOfGO = new List<List<GameObject>>();



    //    //create a pull of cruisers bullets with less start pulled amount, cause there most likely will be less cruiser bullets
    //    //for (int i = 0; i < pulledAmount; i++)
    //    //{
    //    //    #region CPU cruiser bullets pull filling

    //    //    //GameObject obj = (GameObject)Instantiate(C1BulletCPU);
    //    //    //obj.SetActive(false);
    //    //    //C1BulletsCPUPull.Add(obj);

    //    //    //create a pull of C2BulletCPU
    //    //    //GameObject obj1 = (GameObject)Instantiate(C2BulletCPU);
    //    //    //obj1.SetActive(false);
    //    //    //C2BulletsCPUPull.Add(obj1);

    //    //    ////create a pull of C3BulletCPU
    //    //    //GameObject obj2 = (GameObject)Instantiate(C3BulletCPU);
    //    //    //obj2.SetActive(false);
    //    //    //C3BulletsCPUPull.Add(obj2);

    //    //    ////create a pull of C4BulletCPU
    //    //    //GameObject obj3 = (GameObject)Instantiate(C4BulletCPU);
    //    //    //obj3.SetActive(false);
    //    //    //C4BulletsCPUPull.Add(obj3);

    //    //    ////create a pull of C4BulletCPU
    //    //    //GameObject obj4 = (GameObject)Instantiate(CFedBulletCPU);
    //    //    //obj4.SetActive(false);
    //    //    //CFedBulletsCPUPull.Add(obj4);

    //    //    #endregion CPU cruiser bullets pull filling

    //    //    #region Player cruiser bullets pull filling

    //    //    //create a pull of C1BulletCPU
    //    //    //GameObject obj10 = (GameObject)Instantiate(C1BulletPlay);
    //    //    //obj10.SetActive(false);
    //    //    //C1BulletsPlayerPull.Add(obj10);

    //    //    ////create a pull of C2BulletCPU
    //    //    //GameObject obj11 = (GameObject)Instantiate(C2BulletPlay);
    //    //    //obj11.SetActive(false);
    //    //    //C2BulletsPlayerPull.Add(obj11);

    //    //    ////create a pull of C3BulletCPU
    //    //    //GameObject obj12 = (GameObject)Instantiate(C3BulletPlay);
    //    //    //obj12.SetActive(false);
    //    //    //C3BulletsPlayerPull.Add(obj12);

    //    //    ////create a pull of C4BulletCPU
    //    //    //GameObject obj13 = (GameObject)Instantiate(C4BulletPlay);
    //    //    //obj13.SetActive(false);
    //    //    //C4BulletsPlayerPull.Add(obj13);

    //    //    #endregion Player cruiser bullets pull filling


    //    //}

    //    //create a pull of cruisers bullets and cruisers bullets bursts cause they are very few, but destroyer bullets are filled in loop with higer amount capacity
    //    //for (int i = 0; i < pulledAmountLowerLess; i++)
    //    //{
    //    //    //#region cruiser bullets bursts effects

    //    //    ////create a pull of cruis1BullBurst effect 
    //    //    //GameObject obj = (GameObject)Instantiate(C1BulletBurst);
    //    //    //obj.SetActive(false);
    //    //    //C1BulletsBurstPull.Add(obj);

    //    //    ////create a pull of cruis2BullBurst effect 
    //    //    //GameObject obj1 = (GameObject)Instantiate(C2BulletBurst);
    //    //    //obj1.SetActive(false);
    //    //    //C2BulletsBurstPull.Add(obj1);

    //    //    ////create a pull of cruis3BullBurst effect 
    //    //    //GameObject obj2 = (GameObject)Instantiate(C3BulletBurst);
    //    //    //obj2.SetActive(false);
    //    //    //C3BulletsBurstPull.Add(obj2);

    //    //    ////create a pull of cruis4BullBurst effect 
    //    //    //GameObject obj3 = (GameObject)Instantiate(C4BulletBurst);
    //    //    //obj3.SetActive(false);
    //    //    //C4BulletsBurstPull.Add(obj3);
    //    //    //#endregion cruiser bullets bursts effects

    //    //    //create a pull of cruisBurst effect 
    //    //    GameObject obj4 = (GameObject)Instantiate(cruisBurst);
    //    //    obj4.SetActive(false);
    //    //    cruisBurstPull.Add(obj4);

    //    //    //create a pull of cruisPreBurst effect 
    //    //    GameObject obj44 = (GameObject)Instantiate(cruisPreBurst);
    //    //    obj44.SetActive(false);
    //    //    cruisPreBurstPull.Add(obj44);
    //    //}

    //    //create a pull of destroyer bullets with middle start pulled amount, cause there most likely will be more destroyers bullets
    //    //for (int i = 0; i < pulledAmountLess; i++)
    //    //{

    //    //#region CPU destr bullets pull filling

    //    ////create a pull of D4BulletCPU
    //    //GameObject obj4 = (GameObject)Instantiate(D4BulletCPU);
    //    //obj4.SetActive(false);
    //    //D4BulletsCPUPull.Add(obj4);

    //    ////create a pull of D3BulletCPU
    //    //GameObject obj5 = (GameObject)Instantiate(D3BulletCPU);
    //    //obj5.SetActive(false);
    //    //D3BulletsCPUPull.Add(obj5);

    //    ////create a pull of D2BulletCPU
    //    //GameObject obj6 = (GameObject)Instantiate(D2BulletCPU);
    //    //obj6.SetActive(false);
    //    //D2BulletsCPUPull.Add(obj6);

    //    ////create a pull of D2PBulletCPU
    //    //GameObject obj7 = (GameObject)Instantiate(D2PBulletCPU);
    //    //obj7.SetActive(false);
    //    //D2PBulletsCPUPull.Add(obj7);

    //    ////create a pull of D1PBulletCPU
    //    //GameObject obj8 = (GameObject)Instantiate(D1PBulletCPU);
    //    //obj8.SetActive(false);
    //    //D1PBulletsCPUPull.Add(obj8);

    //    ////create a pull of D1BulletCPU
    //    //GameObject obj9 = (GameObject)Instantiate(D1BulletCPU);
    //    //obj9.SetActive(false);
    //    //D1BulletsCPUPull.Add(obj9);

    //    ////create a pull of D1BulletCPU
    //    //GameObject obj122 = (GameObject)Instantiate(DFedBulletCPU);
    //    //obj122.SetActive(false);
    //    //DFedBulletsCPUPull.Add(obj122);

    //    //#endregion CPU destr bullets pull filling

    //    //#region Player destr bullets pull filling


    //    ////create a pull of D4BulletCPU
    //    //GameObject obj14 = (GameObject)Instantiate(D4BulletPlay);
    //    //obj14.SetActive(false);
    //    //D4BulletsPlayerPull.Add(obj14);

    //    ////create a pull of D3BulletCPU
    //    //GameObject obj15 = (GameObject)Instantiate(D3BulletPlay);
    //    //obj15.SetActive(false);
    //    //D3BulletsPlayerPull.Add(obj15);

    //    ////create a pull of D2BulletCPU
    //    //GameObject obj16 = (GameObject)Instantiate(D2BulletPlay);
    //    //obj16.SetActive(false);
    //    //D2BulletsPlayerPull.Add(obj16);

    //    ////create a pull of D2PBulletCPU
    //    //GameObject obj17 = (GameObject)Instantiate(D2PBulletPlay);
    //    //obj17.SetActive(false);
    //    //D2PBulletsPlayerPull.Add(obj17);

    //    ////create a pull of D1PBulletCPU
    //    //GameObject obj18 = (GameObject)Instantiate(D1PBulletPlay);
    //    //obj18.SetActive(false);
    //    //D1PBulletsPlayerPull.Add(obj18);

    //    ////create a pull of D1BulletCPU
    //    //GameObject obj19 = (GameObject)Instantiate(D1BulletPlay);
    //    //obj19.SetActive(false);
    //    //D1BulletsPlayerPull.Add(obj19);

    //    //#endregion Player destr bullets pull filling



    //    //create a pull of CruiserParalEffects
    //    //GameObject obj20 = (GameObject)Instantiate(cruisParalEffect);
    //    //obj20.SetActive(false);
    //    //cruisParalEffectPull.Add(obj20);
    //    //}

    //    //for (int i = 0; i < pulledAmountLowerMiddle; i++)
    //    //{
    //    //    //create a pull of destroyerParalEffects here cause there will be much more destr paral effects than cruiser ones, cruiser pull is created on loop upper with less pull amount
    //    //    //GameObject obj5 = (GameObject)Instantiate(destrParalEffect);
    //    //    //obj5.SetActive(false);
    //    //    //destrParalEffectPull.Add(obj5);

    //    //    //create a pull of destrBurst effect 
    //    //    GameObject obj9 = (GameObject)Instantiate(destrBurst);
    //    //    obj9.SetActive(false);
    //    //    destrBurstPull.Add(obj9);

    //    //    //create a pull of destrBurst effect 
    //    //    GameObject obj99 = (GameObject)Instantiate(destrPreBurst);
    //    //    obj99.SetActive(false);
    //    //    destrPreBurstPull.Add(obj99);

    //    //    //create destr bullets burst effect here cause there will be much more destr bullets in scene
    //    //    //#region destr bullets bursts effects

    //    //    ////create a pull of destr1BullBurst effect 
    //    //    //GameObject obj4 = (GameObject)Instantiate(D1BulletBurst);
    //    //    //obj4.SetActive(false);
    //    //    //D1BulletsBurstPull.Add(obj4);

    //    //    ////create a pull of destr2BullBurst effect 
    //    //    //GameObject obj6 = (GameObject)Instantiate(D2BulletBurst);
    //    //    //obj6.SetActive(false);
    //    //    //D2BulletsBurstPull.Add(obj6);

    //    //    ////create a pull of destr3BullBurst effect 
    //    //    //GameObject obj7 = (GameObject)Instantiate(D3BulletBurst);
    //    //    //obj7.SetActive(false);
    //    //    //D3BulletsBurstPull.Add(obj7);

    //    //    ////create a pull of destr4BullBurst effect 
    //    //    //GameObject obj8 = (GameObject)Instantiate(D4BulletBurst);
    //    //    //obj8.SetActive(false);
    //    //    //D4BulletsBurstPull.Add(obj8);

    //    //    //#endregion bullets bursts effects
    //    //}

    //    //create a pull of destroyer bullets with middle start pulled amount, cause there most likely will be more destroyers bullets
    //    //for (int i = 0; i < pulledAmountMiddle; i++)
    //    //{

    //    //    //#region CPU power shields

    //    //    ////create a pull of C1ShieldCPU
    //    //    //GameObject obj4 = (GameObject)Instantiate(C1ShieldCPU);
    //    //    //obj4.SetActive(false);
    //    //    //obj4.GetComponent<MeshRenderer>().material.SetColor("_Color", redColorOfSheild);
    //    //    //C1ShieldCPUPull.Add(obj4);

    //    //    ////create a pull of C2ShieldCPU
    //    //    //GameObject obj5 = (GameObject)Instantiate(C2ShieldCPU);
    //    //    //obj5.SetActive(false);
    //    //    //obj5.GetComponent<MeshRenderer>().material.SetColor("_Color", purpleColorOfSheild);
    //    //    //C2ShieldCPUPull.Add(obj5);

    //    //    ////create a pull of C3ShieldCPU
    //    //    //GameObject obj6 = (GameObject)Instantiate(C3ShieldCPU);
    //    //    //obj6.SetActive(false);
    //    //    //obj6.GetComponent<MeshRenderer>().material.SetColor("_Color", cyanColorOfSheild);
    //    //    //C3ShieldCPUPull.Add(obj6);

    //    //    ////create a pull of C3ShieldCPU
    //    //    //GameObject obj10 = (GameObject)Instantiate(CFedShieldCPU);
    //    //    //obj10.SetActive(false);
    //    //    //obj10.GetComponent<MeshRenderer>().material.SetColor("_Color", whiteColorOfSheild);
    //    //    //CFedShieldCPUPull.Add(obj10);

    //    //    ////create a pull of D1ShieldCPU
    //    //    //GameObject obj7 = (GameObject)Instantiate(D1ShieldCPU);
    //    //    //obj7.SetActive(false);
    //    //    //obj7.GetComponent<MeshRenderer>().material.SetColor("_Color", redColorOfSheild);
    //    //    //D1ShieldCPUPull.Add(obj7);

    //    //    ////create a pull of D2ShieldCPU
    //    //    //GameObject obj8 = (GameObject)Instantiate(D2ShieldCPU);
    //    //    //obj8.SetActive(false);
    //    //    //obj8.GetComponent<MeshRenderer>().material.SetColor("_Color", purpleColorOfSheild);
    //    //    //D2ShieldCPUPull.Add(obj8);

    //    //    ////create a pull of D3ShieldCPU
    //    //    //GameObject obj9 = (GameObject)Instantiate(D3ShieldCPU);
    //    //    //obj9.SetActive(false);
    //    //    //obj9.GetComponent<MeshRenderer>().material.SetColor("_Color", cyanColorOfSheild);
    //    //    //D3ShieldCPUPull.Add(obj9);

    //    //    ////create a pull of D3ShieldCPU
    //    //    //GameObject obj11 = (GameObject)Instantiate(DFedShieldCPU);
    //    //    //obj11.SetActive(false);
    //    //    //obj11.GetComponent<MeshRenderer>().material.SetColor("_Color", whiteColorOfSheild);
    //    //    //DFedShieldCPUPull.Add(obj11);

    //    //    //#endregion CPU power shields

    //    //    //#region Player power shields

    //    //    ////create a pull of C1ShieldPlay
    //    //    //GameObject obj14 = (GameObject)Instantiate(C1ShieldPlay);
    //    //    //obj14.SetActive(false);
    //    //    //obj14.GetComponent<MeshRenderer>().material.SetColor("_Color", redColorOfSheild);
    //    //    //C1ShieldPlayPull.Add(obj14);

    //    //    ////create a pull of C2ShieldPlay
    //    //    //GameObject obj15 = (GameObject)Instantiate(C2ShieldPlay);
    //    //    //obj15.SetActive(false);
    //    //    //obj15.GetComponent<MeshRenderer>().material.SetColor("_Color", purpleColorOfSheild);
    //    //    //C2ShieldPlayPull.Add(obj15);

    //    //    ////create a pull of C3ShieldPlay
    //    //    //GameObject obj16 = (GameObject)Instantiate(C3ShieldPlay);
    //    //    //obj16.SetActive(false);
    //    //    //obj16.GetComponent<MeshRenderer>().material.SetColor("_Color", cyanColorOfSheild);
    //    //    //C3ShieldPlayPull.Add(obj16);

    //    //    ////create a pull of D1ShieldPlay
    //    //    //GameObject obj17 = (GameObject)Instantiate(D1ShieldPlay);
    //    //    //obj17.SetActive(false);
    //    //    //obj17.GetComponent<MeshRenderer>().material.SetColor("_Color", redColorOfSheild);
    //    //    //D1ShieldPlayPull.Add(obj17);

    //    //    ////create a pull of D2ShieldPlay
    //    //    //GameObject obj18 = (GameObject)Instantiate(D2ShieldPlay);
    //    //    //obj18.SetActive(false);
    //    //    //obj18.GetComponent<MeshRenderer>().material.SetColor("_Color", purpleColorOfSheild);
    //    //    //D2ShieldPlayPull.Add(obj18);

    //    //    ////create a pull of D1BulletCPU
    //    //    //GameObject obj19 = (GameObject)Instantiate(D3ShieldPlay);
    //    //    //obj19.SetActive(false);
    //    //    //obj19.GetComponent<MeshRenderer>().material.SetColor("_Color", cyanColorOfSheild);
    //    //    //D3ShieldPlayPull.Add(obj19);

    //    //    //#endregion Player power shields
    //    //}

    //    //create a pull of guns bullets with more start pulled amount, cause there most likely will be more guns bullets on scene
    //    //for (int i = 0; i < pulledAmountMiddle; i++)
    //    //{

    //    //    #region CPU gun bullet 

    //    //    //create a pull of G1BulletCPU
    //    //    GameObject obj4 = (GameObject)Instantiate(G1BulletCPU);
    //    //    obj4.SetActive(false);
    //    //    G1BulletsCPUPull.Add(obj4);

    //    //    //create a pull of G2BulletCPU
    //    //    GameObject obj5 = (GameObject)Instantiate(G2BulletCPU);
    //    //    obj5.SetActive(false);
    //    //    G2BulletsCPUPull.Add(obj5);

    //    //    //create a pull of G3BulletCPU
    //    //    GameObject obj6 = (GameObject)Instantiate(G3BulletCPU);
    //    //    obj6.SetActive(false);
    //    //    G3BulletsCPUPull.Add(obj6);

    //    //    #endregion CPU gun bullet 

    //    //    #region Player gun bullet

    //    //    //create a pull of G1BulletPlay
    //    //    GameObject obj1 = (GameObject)Instantiate(G1BulletPlay);
    //    //    obj1.SetActive(false);
    //    //    G1BulletsPlayerPull.Add(obj1);

    //    //    //create a pull of G2BulletPlay
    //    //    GameObject obj2 = (GameObject)Instantiate(G2BulletPlay);
    //    //    obj2.SetActive(false);
    //    //    G2BulletsPlayerPull.Add(obj2);

    //    //    //create a pull of G3BulletPlay
    //    //    GameObject obj3 = (GameObject)Instantiate(G3BulletPlay);
    //    //    obj3.SetActive(false);
    //    //    G3BulletsPlayerPull.Add(obj3);

    //    //    #endregion Player gun bullet
    //    //}
    //    #endregion old code
    //}
    #endregion OldStart

    //these are invoked from various classes (PlayerShipCPU, CPUShipCtrl etc) get the pulls of GO and call them (GO) with GetUniversalBullet method below
    //so they send the list they get from here to the parameters of the method GetUniversalBullet

    //Players objects lists on battle scene
    public List<GameObject> GetC1BulletPlayerList()
    {
        return C1BulletsPlayerPull;
    }
    public List<GameObject> GetC2BulletPlayerList()
    {
        return C2BulletsPlayerPull;
    }
    public List<GameObject> GetC3BulletPlayerList() {
        return C3BulletsPlayerPull;
    }
    public List<GameObject> GetC4BulletPlayerList()
    {
        return C4BulletsPlayerPull;
    }
    public List<GameObject> GetD1PBulletPlayerList()
    {
        return D1PBulletsPlayerPull;
    }
    public List<GameObject> GetD1BulletPlayerList()
    {
        return D1BulletsPlayerPull;
    }
    public List<GameObject> GetD2PBulletPlayerList()
    {
        return D2PBulletsPlayerPull;
    }
    public List<GameObject> GetD2BulletPlayerList()
    {
        return D2BulletsPlayerPull;
    }
    public List<GameObject> GetD3BulletPlayerList()
    {
        return D3BulletsPlayerPull;
    }
    public List<GameObject> GetD4BulletPlayerList()
    {
        return D4BulletsPlayerPull;
    }

    public List<GameObject> GetC1ShieldPlayPull()
    {
        return C1ShieldPlayPull;
    }
    public List<GameObject> GetC2ShieldPlayPull()
    {
        return C2ShieldPlayPull;
    }
    public List<GameObject> GetC3ShieldPlayPull()
    {
        return C3ShieldPlayPull;
    }
    public List<GameObject> GetD1ShieldPlayPull()
    {
        return D1ShieldPlayPull;
    }
    public List<GameObject> GetD2ShieldPlayPull()
    {
        return D2ShieldPlayPull;
    }
    public List<GameObject> GetD3ShieldPlayPull()
    {
        return D3ShieldPlayPull;
    }

    public List<GameObject> GetG1BulletsPlayerPull()
    {
        return G1BulletsPlayerPull;
    }
    public List<GameObject> GetG2BulletsPlayerPull()
    {
        return G2BulletsPlayerPull;
    }
    public List<GameObject> GetG3BulletsPlayerPull()
    {
        return G3BulletsPlayerPull;
    }

    //CPU objects lists on battle scene
    public List<GameObject> GetC1BulletCPUList()
    {
        return C1BulletsCPUPull;
    }
    public List<GameObject> GetC2BulletCPUList()
    {
        return C2BulletsCPUPull;
    }
    public List<GameObject> GetC3BulletCPUList()
    {
        return C3BulletsCPUPull;
    }
    public List<GameObject> GetC4BulletCPUList()
    {
        return C4BulletsCPUPull;
    }

    public List<GameObject> GetCFedBulletCPUList()
    {
        return CFedBulletsCPUPull;
    }

    public List<GameObject> GetD1PBulletCPUList()
    {
        return D1PBulletsCPUPull;
    }
    public List<GameObject> GetD1BulletCPUList()
    {
        return D1BulletsCPUPull;
    }
    public List<GameObject> GetD2PBulletCPUList()
    {
        return D2PBulletsCPUPull;
    }
    public List<GameObject> GetD2BulletCPUList()
    {
        return D2BulletsCPUPull;
    }
    public List<GameObject> GetD3BulletCPUList()
    {
        return D3BulletsCPUPull;
    }
    public List<GameObject> GetD4BulletCPUList()
    {
        return D4BulletsCPUPull;
    }

    public List<GameObject> GetDFedBulletCPUList()
    {
        return DFedBulletsCPUPull;
    }


    public List<GameObject> GetS1BulletPlayerList()
    {
        return S1BulletsPlayerPull;
    }
    public List<GameObject> GetS2BulletPlayerList()
    {
        return S2BulletsPlayerPull;
    }
    public List<GameObject> GetS3BulletPlayerList()
    {
        return S3BulletsPlayerPull;
    }
    public List<GameObject> GetS4BulletPlayerList()
    {
        return S4BulletsPlayerPull;
    }

    public List<GameObject> GetS1BulletCPUList()
    {
        return S1BulletsCPUPull;
    }
    public List<GameObject> GetS2BulletCPUList()
    {
        return S2BulletsCPUPull;
    }
    public List<GameObject> GetS3BulletCPUList()
    {
        return S3BulletsCPUPull;
    }
    public List<GameObject> GetS4BulletCPUList()
    {
        return S4BulletsCPUPull;
    }

    public List<GameObject> GetSFedBulletCPUList()
    {
        return SFedBulletsCPUPull;
    }


    public List<GameObject> GetC1ShieldCPUPull()
    {
        return C1ShieldCPUPull;
    }
    public List<GameObject> GetC2ShieldCPUPull()
    {
        return C2ShieldCPUPull;
    }
    public List<GameObject> GetC3ShieldCPUPull()
    {
        return C3ShieldCPUPull;
    }
    public List<GameObject> GetCFedShieldCPUPull()
    {
        return CFedShieldCPUPull;
    }
    public List<GameObject> GetD1ShieldCPUPull()
    {
        return D1ShieldCPUPull;
    }
    public List<GameObject> GetD2ShieldCPUPull()
    {
        return D2ShieldCPUPull;
    }
    public List<GameObject> GetD3ShieldCPUPull()
    {
        return D3ShieldCPUPull;
    }
    public List<GameObject> GetDFedShieldCPUPull()
    {
        return DFedShieldCPUPull;
    }

    public List<GameObject> GetG1BulletsCPUPull()
    {
        return G1BulletsCPUPull;
    }
    public List<GameObject> GetG2BulletsCPUPull()
    {
        return G2BulletsCPUPull;
    }
    public List<GameObject> GetG3BulletsCPUPull()
    {
        return G3BulletsCPUPull;
    }

    //common GO pulls get
    public List<GameObject> GetCruisParalEffectPull()
    {
        return cruisParalEffectPull;
    }
    public List<GameObject> GetDestrParalEffectPull()
    {
        return destrParalEffectPull;
    }
    public List<GameObject> GetCruiser1BulletBurstPull()
    {
        return C1BulletsBurstPull;
    }
    public List<GameObject> GetCruiser2BulletBurstPull()
    {
        return C2BulletsBurstPull;
    }
    public List<GameObject> GetCruiser3BulletBurstPull()
    {
        return C3BulletsBurstPull;
    }
    public List<GameObject> GetCruiser4BulletBurstPull()
    {
        return C4BulletsBurstPull;
    }
    public List<GameObject> GetDestr1BulletBurstPull()
    {
        return D1BulletsBurstPull;
    }
    public List<GameObject> GetDestr2BulletBurstPull()
    {
        return D2BulletsBurstPull;
    }
    public List<GameObject> GetDestr3BulletBurstPull()
    {
        return D3BulletsBurstPull;
    }
    public List<GameObject> GetDestr4BulletBurstPull()
    {
        return D4BulletsBurstPull;
    }
    public List<GameObject> GetDestrBurstPull()
    {
        return destrBurstPull;
    }
    public List<GameObject> GetDestrPreBurstPull()
    {
        return destrPreBurstPull;
    }
    public List<GameObject> GetCruisBurstPull()
    {
        return cruisBurstPull;
    }

    public List<GameObject> GetCruisPreBurstPull()
    {
        return cruisPreBurstPull;
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
