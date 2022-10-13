
using System.Collections.Generic;
using UnityEngine;

public class ObjectPullerJourney : MonoBehaviour
{
    public static ObjectPullerJourney current;


    public GameObject Cruis1JourneyCPU;
    public GameObject Cruis2JourneyCPU;
    public GameObject Cruis3JourneyCPU;
    public GameObject Cruis4JourneyCPU;


    public GameObject Cruis1JourneyPlayer;
    public GameObject Cruis2JourneyPlayer;
    public GameObject Cruis3JourneyPlayer;
    public GameObject Cruis4JourneyPlayer;


    public GameObject paralizerJourney;

    public GameObject boosterSphere;
    public GameObject energySphere;
    public GameObject energySphereBig;

    public GameObject StationA;
    public GameObject StationC;
    public GameObject StationD;
    public GameObject StationE;
    public GameObject StationFed;

    public GameObject EmptyStation0;
    public GameObject EmptyStation1;
    public GameObject EmptyStation2;
    public GameObject EmptyStation3;

    public GameObject energonBullet;
    public GameObject energonBulletBurst;

    public GameObject playerBullet;


    public GameObject asteroidBurst;


    public GameObject captureEffect;

    public GameObject energonBurst;

    public GameObject miniInfoPanel;

    public GameObject miniInfoPanelNoEnergy;
    //public GameObject miniInfoPanelNoFleet;
    public GameObject miniInfoPanelOnlyEnergy;

    public GameObject connectionSygnalUI;


    private int pulledAmountLess = 10;
    private int pulledAmountMiddle = 8;

    private bool willGrow = true;


    public List<GameObject> Cruis1JourneyCPUPull;
    public List<GameObject> Cruis2JourneyCPUPull;
    public List<GameObject> Cruis3JourneyCPUPull;
    public List<GameObject> Cruis4JourneyCPUPull;

    public List<GameObject> Cruis1JourneyPlayerPull;
    public List<GameObject> Cruis2JourneyPlayerPull;
    public List<GameObject> Cruis3JourneyPlayerPull;
    public List<GameObject> Cruis4JourneyPlayerPull;


    private List<GameObject> StationAPull;
    private List<GameObject> StationCPull;
    private List<GameObject> StationDPull;
    private List<GameObject> StationEPull;
    private List<GameObject> StationFedPull;

    private List<GameObject> EmptyStation0Pull;
    private List<GameObject> EmptyStation1Pull;
    private List<GameObject> EmptyStation2Pull;
    private List<GameObject> EmptyStation3Pull;

    private List<GameObject> boosterSpherePull;
    private List<GameObject> energySpherePull;
    private List<GameObject> energySphereBigPull;
    private List<GameObject> paralizerJourneyPull;

    private List<GameObject> energonBulletPull;
    private List<GameObject> energonBulletBurstPull;

    private List<GameObject> playerBulletPull;

    private List<GameObject> asteroidBurstPull;

    private List<GameObject> energonBurstPull;

    private List<GameObject> TelepPull;

    private List<GameObject> captureEffectPull;


    private List<GameObject> miniInfoPanelPull;

    private List<GameObject> miniInfoPanelNoEnergyPull;
    //private List<GameObject> miniInfoPanelNoFleetPull;
    private List<GameObject> miniInfoPanelOnlyEnergyPull;

    private List<GameObject> connectionSygnalUIPull;

    private void Awake()
    {
        current = this;
    }

    private void OnEnable()
    {
        Cruis1JourneyPlayerPull = new List<GameObject>();
        Cruis2JourneyPlayerPull = new List<GameObject>();
        Cruis3JourneyPlayerPull = new List<GameObject>();
        Cruis4JourneyPlayerPull = new List<GameObject>();

        for (int i = 0; i < pulledAmountMiddle; i++)
        {
            if (Lists.currentLevel > 7)
            {
                //creating pull of CPU ships
                GameObject obj55 = (GameObject)Instantiate(Cruis1JourneyPlayer);
                obj55.SetActive(false);
                Cruis1JourneyPlayerPull.Add(obj55);
            }

            if (Lists.currentLevel > 3)
            {
                GameObject obj56 = (GameObject)Instantiate(Cruis2JourneyPlayer);
                obj56.SetActive(false);
                Cruis2JourneyPlayerPull.Add(obj56);
            }

            if (Lists.currentLevel > 1)
            {
                GameObject obj57 = (GameObject)Instantiate(Cruis3JourneyPlayer);
                obj57.SetActive(false);
                Cruis3JourneyPlayerPull.Add(obj57);
            }


            GameObject obj58 = (GameObject)Instantiate(Cruis4JourneyPlayer);
            obj58.SetActive(false);
            Cruis4JourneyPlayerPull.Add(obj58);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Cruis1JourneyCPUPull = new List<GameObject>();
        Cruis2JourneyCPUPull = new List<GameObject>();
        Cruis3JourneyCPUPull = new List<GameObject>();
        Cruis4JourneyCPUPull = new List<GameObject>();

        
        //CruisGJourneyCPUPull = new List<GameObject>();

        paralizerJourneyPull = new List<GameObject>();
        boosterSpherePull = new List<GameObject>();
        energySpherePull = new List<GameObject>();
        energySphereBigPull = new List<GameObject>();

        StationAPull = new List<GameObject>();
        //StationBPull = new List<GameObject>();
        StationCPull = new List<GameObject>();
        StationDPull = new List<GameObject>();
        StationEPull = new List<GameObject>();
        StationFedPull = new List<GameObject>();

        EmptyStation0Pull = new List<GameObject>();
        EmptyStation1Pull = new List<GameObject>();
        EmptyStation2Pull = new List<GameObject>();
        EmptyStation3Pull = new List<GameObject>();

        energonBulletPull = new List<GameObject>();
        energonBulletBurstPull = new List<GameObject>();

        playerBulletPull = new List<GameObject>();

        TelepPull = new List<GameObject>();

        asteroidBurstPull = new List<GameObject>();
        energonBurstPull = new List<GameObject>();

        captureEffectPull = new List<GameObject>();

        miniInfoPanelPull = new List<GameObject>();

        miniInfoPanelNoEnergyPull = new List<GameObject>();
        miniInfoPanelOnlyEnergyPull = new List<GameObject>();
        connectionSygnalUIPull = new List<GameObject>();
        //miniInfoPanelNoFleetPull = new List<GameObject>();

        for (int i = 0; i < 2; i++)
        {
            GameObject obj = (GameObject)Instantiate(paralizerJourney);
            obj.SetActive(false);
            paralizerJourneyPull.Add(obj);

            GameObject obj1 = (GameObject)Instantiate(boosterSphere);
            obj1.SetActive(false);
            boosterSpherePull.Add(obj1);

            GameObject obj3 = (GameObject)Instantiate(energySphere);
            obj3.SetActive(false);
            energySpherePull.Add(obj3);


            GameObject obj2 = (GameObject)Instantiate(asteroidBurst);
            obj2.SetActive(false);
            asteroidBurstPull.Add(obj2);

            GameObject obj4 = (GameObject)Instantiate(energySphereBig);
            obj4.SetActive(false);
            energySphereBigPull.Add(obj4);


            GameObject obj5 = (GameObject)Instantiate(connectionSygnalUI);
            obj5.transform.SetParent(SpaceCtrlr.Instance.parentCanvas, false);
            obj5.SetActive(false);
            connectionSygnalUIPull.Add(obj5);

            //GameObject obj6 = (GameObject)Instantiate(miniInfoPanelNoFleet);
            //obj6.transform.SetParent(SpaceCtrlr.Instance.parentCanvas, false);
            //obj6.SetActive(false);
            //miniInfoPanelNoFleetPull.Add(obj6);
        }

        //create a pull of cruisers bullets with less start pulled amount, cause there most likely will be less cruiser bullets
        for (int i = 0; i < pulledAmountLess; i++)
        {
            //creating a pull of stations
            GameObject obj = (GameObject)Instantiate(StationA);
            obj.SetActive(false);
            StationAPull.Add(obj);

            if (Lists.currentLevel > 3)
            {
                GameObject obj2 = (GameObject)Instantiate(StationC);
                obj2.SetActive(false);
                StationCPull.Add(obj2);
            }

            if (Lists.currentLevel > 1)
            {
                GameObject obj3 = (GameObject)Instantiate(StationD);
                obj3.SetActive(false);
                StationDPull.Add(obj3);
            }

            if (Lists.currentLevel > 2)
            {
                GameObject obj10 = (GameObject)Instantiate(StationE);
                obj10.SetActive(false);
                StationEPull.Add(obj10);
            }

            if (Lists.currentLevel > 7)
            {
                GameObject obj11 = (GameObject)Instantiate(StationFed);
                obj11.SetActive(false);
                StationFedPull.Add(obj11);
            }

            GameObject obj13 = (GameObject)Instantiate(energonBullet);
            obj13.SetActive(false);
            energonBulletPull.Add(obj13);

            GameObject obj14 = (GameObject)Instantiate(energonBulletBurst);
            obj14.SetActive(false);
            energonBulletBurstPull.Add(obj14);



            GameObject obj15 = (GameObject)Instantiate(miniInfoPanel);
            obj15.transform.SetParent(SpaceCtrlr.Instance.parentCanvas, false);
            obj15.SetActive(false);
            miniInfoPanelPull.Add(obj15);


            GameObject obj19 = (GameObject)Instantiate(captureEffect);
            obj19.SetActive(false);
            captureEffectPull.Add(obj19);

            GameObject obj20 = (GameObject)Instantiate(miniInfoPanelNoEnergy);
            obj20.transform.SetParent(SpaceCtrlr.Instance.parentCanvas, false);
            obj20.SetActive(false);
            miniInfoPanelNoEnergyPull.Add(obj20);

            GameObject obj21 = (GameObject)Instantiate(miniInfoPanelOnlyEnergy);
            obj21.transform.SetParent(SpaceCtrlr.Instance.parentCanvas, false);
            obj21.SetActive(false);
            miniInfoPanelOnlyEnergyPull.Add(obj21);
        }

        //create a pull of cruisers 
        for (int i = 0; i < pulledAmountMiddle; i++)
        {

            //creating a pull of emptystations
            GameObject obj14 = (GameObject)Instantiate(EmptyStation0);
            obj14.SetActive(false);
            EmptyStation0Pull.Add(obj14);

            GameObject obj99 = (GameObject)Instantiate(energonBurst);
            obj99.SetActive(false);
            energonBurstPull.Add(obj99);

            if (Lists.currentLevel > 1)
            {
                //creating a pull of emptystations
                GameObject obj15 = (GameObject)Instantiate(EmptyStation1);
                obj15.SetActive(false);
                EmptyStation1Pull.Add(obj15);
            }

            if (Lists.currentLevel > 3)
            {
                //creating a pull of stations
                GameObject obj16 = (GameObject)Instantiate(EmptyStation2);
                obj16.SetActive(false);
                EmptyStation2Pull.Add(obj16);
            }

            if (Lists.currentLevel > 7)
            {
                //creating a pull of stations
                GameObject obj17 = (GameObject)Instantiate(EmptyStation3);
                obj17.SetActive(false);
                EmptyStation3Pull.Add(obj17);
            }

            if (Lists.currentLevel > 7)
            {
                //creating pull of CPU ships
                GameObject obj1 = (GameObject)Instantiate(Cruis1JourneyCPU);
                obj1.SetActive(false);
                Cruis1JourneyCPUPull.Add(obj1);
            }

            if (Lists.currentLevel > 3)
            {
                GameObject obj2 = (GameObject)Instantiate(Cruis2JourneyCPU);
                obj2.SetActive(false);
                Cruis2JourneyCPUPull.Add(obj2);
            }

            if (Lists.currentLevel > 1)
            {
                GameObject obj3 = (GameObject)Instantiate(Cruis3JourneyCPU);
                obj3.SetActive(false);
                Cruis3JourneyCPUPull.Add(obj3);
            }

            if (Lists.currentLevel > 7)
            {
                //creating pull of CPU ships
                GameObject obj55 = (GameObject)Instantiate(Cruis1JourneyPlayer);
                obj55.SetActive(false);
                Cruis1JourneyPlayerPull.Add(obj55);
            }

            if (Lists.currentLevel > 3)
            {
                GameObject obj56 = (GameObject)Instantiate(Cruis2JourneyPlayer);
                obj56.SetActive(false);
                Cruis2JourneyPlayerPull.Add(obj56);
            }

            if (Lists.currentLevel > 1)
            {
                GameObject obj57 = (GameObject)Instantiate(Cruis3JourneyPlayer);
                obj57.SetActive(false);
                Cruis3JourneyPlayerPull.Add(obj57);
            }

            GameObject obj4 = (GameObject)Instantiate(Cruis4JourneyCPU);
            obj4.SetActive(false);
            Cruis4JourneyCPUPull.Add(obj4);

            GameObject obj58 = (GameObject)Instantiate(Cruis4JourneyPlayer);
            obj58.SetActive(false);
            Cruis4JourneyPlayerPull.Add(obj58);


            GameObject obj5 = (GameObject)Instantiate(playerBullet);
            obj5.SetActive(false);
            playerBulletPull.Add(obj5);

        }
    }

    
    public List<GameObject> GetStationAPullList()
    {
        return StationAPull;
    }
    public List<GameObject> GetStationCPullList()
    {
        return StationCPull;
    }
    public List<GameObject> GetStationDPullList()
    {
        return StationDPull;
    }
    public List<GameObject> GetStationEPullList()
    {
        return StationEPull;
    }
    public List<GameObject> GetStationFedPullList()
    {
        return StationFedPull;
    }
    public List<GameObject> GetTelepPullList()
    {
        return TelepPull;
    }
    
    public List<GameObject> GetCaptureEffectPullList()
    {
        return captureEffectPull;
    }
    public List<GameObject> GetEmptyStation0PullList()
    {
        return EmptyStation0Pull;
    }
    public List<GameObject> GetEmptyStation1PullList()
    {
        return EmptyStation1Pull;
    }
    public List<GameObject> GetEmptyStation2PullList()
    {
        return EmptyStation2Pull;
    }
    public List<GameObject> GetEmptyStation3PullList()
    {
        return EmptyStation3Pull;
    }

    public List<GameObject> GetAsteroidBurstPullList()
    {
        return asteroidBurstPull;
    }

    
    public List<GameObject> GetEnergonBulletPullPullList()
    {
        return energonBulletPull;
    }
    public List<GameObject> GetEnergonBulletBurstPullPullList()
    {
        return energonBulletBurstPull;
    }
    public List<GameObject> GetPlayerBulletPullList()
    {
        return playerBulletPull;
    }

    public List<GameObject> GetParalizerJourList()
    {
        return paralizerJourneyPull;
    }
    public List<GameObject> GetBoosterSpherePullList()
    {
        return boosterSpherePull;
    }

    public List<GameObject> GetEnergySpherePullList()
    {
        return energySpherePull;
    }
    public List<GameObject> GetCruis4JourneyCPUPullList()
    {
        return Cruis4JourneyCPUPull;
    }
    public List<GameObject> GetCruis3JourneyCPUPullList()
    {
        return Cruis3JourneyCPUPull;
    }
    public List<GameObject> GetCruis2JourneyCPUPullList()
    {
        return Cruis2JourneyCPUPull;
    }
    public List<GameObject> GetCruis1JourneyCPUPullList()
    {
        return Cruis1JourneyCPUPull;
    }


    public List<GameObject> GetCruis4JourneyPlayerPullList()
    {
        return Cruis4JourneyPlayerPull;
    }
    public List<GameObject> GetCruis3JourneyPlayerPullList()
    {
        return Cruis3JourneyPlayerPull;
    }
    public List<GameObject> GetCruis2JourneyPlayerPullList()
    {
        return Cruis2JourneyPlayerPull;
    }
    public List<GameObject> GetCruis1JourneyPlayerPullList()
    {
        return Cruis1JourneyPlayerPull;
    }

    public List<GameObject> GetenergonBurstPullList()
    {
        return energonBurstPull;
    }

    public List<GameObject> GetEnergySphereBigPullList()
    {
        return energySphereBigPull;
    }

    public List<GameObject> GetMiniInfoPanelPullList()
    {
        return miniInfoPanelPull;
    }


    public List<GameObject> GetminiInfoPanelNoEnergyPullList()
    {
        return miniInfoPanelNoEnergyPull;
    }

    public List<GameObject> GetminiInfoPanelOnlyEnergyPullList()
    {
        return miniInfoPanelOnlyEnergyPull;
    }

    public List<GameObject> GetConnectionSygnalUIPullList()
    {
        return connectionSygnalUIPull;
    }
    //public List<GameObject> GetMiniInfoPanelNoFleetPullList()
    //{
    //    return miniInfoPanelNoFleetPull;
    //}

    


    //universal method to set active proper game object from the list of GOs, it just needs to get correct List of game objects
    public GameObject GetUniversalBullet(List<GameObject> GOLists)
    {
        for (int i = 0; i < GOLists.Count; i++)
        {
            if (!GOLists[i].activeInHierarchy) return (GameObject) GOLists[i];
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
