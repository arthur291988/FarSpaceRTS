
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.Linq;


public class LaunchManager : /*MonoBehaviour*/ Singleton<LaunchManager>
{
    //this var is used to hold a reference to index of dummy to set on defence scene as object to defend. So the order is following
    //1 - cruis1 dummy, 2 - cruis2 dummy, 3 - cruis3 dummy, 4 - cruis4 dummy, 11 - StationFed dummy, 22 - station C dummy, 33 - StationD dummy, 44 - station A dummy
    //and the order inside collection is next
    //0 - cruis1 dummy, 1 - cruis2 dummy, 2 - cruis3 dummy, 3 - cruis4 dummy, 4 - StationFed dummy, 5 - station C dummy, 6 - StationD dummy, 7 - station A dummy
    [SerializeField]
    private List<GameObject> dummiesCollection;

    //this var is used to hold a reference to index of dummy to set on defence scene as object to defend. So the order is following
    //1 - cruis1 dummy, 2 - cruis2 dummy, 3 - cruis3 dummy, 4 - cruis4 dummy, 5 - cruisG dummy, 11 - StationFed dummy, 22 - station C dummy, 33 - StationD dummy, 
    //44 - station A dummy, 55 - station G dummy
    //and the order inside collection is next
    //0 - cruis1 dummy, 1 - cruis2 dummy, 2 - cruis3 dummy, 3 - cruis4 dummy, 4 - cruisG dummy, 5 - StationFed dummy, 6 - station C dummy, 7 - StationD dummy, 
    //8 - station A dummy, 9 - station G dummy
    [SerializeField]
    private List<GameObject> enemyDummiesCollection;

    //this one stores the references to holographic materials to make them fade on on start method cause they are all in state of fade out after the journey scene
    public List<Material> holoMats;

    //RawImage that used to represent opposite player color
    //[SerializeField]
    //private RawImage OppositeCPUImage;

    //asteroids pulling properties to launch them on scene
    private List<GameObject> AsteroidList;
    private GameObject AsteroidReal;

    //virtual camera pbject to follow player and using cinemachine features
    public GameObject virtualCamera;

    public GameObject Destr1CPU;
    public GameObject Destr2CPU;
    public GameObject Destr3CPU;
    public GameObject Destr4CPU;

    //this materials are to assign as skybox depending on the dimension
    public Material redSkyboxMat;
    public Material blueSkyboxMat;
    public Material blackSkyboxMat;

    //this one is used to on and off GunShotButt script from that UI element, it it is on while start it cases null reference errors
    public GameObject shotButton;
    //this one is used to on and off DefAimingController script from that UI element, it it is on while start it cases null reference errors
    //public GameObject aimingButton;

    //guns to Instantiate on defence scene after player chose it
    public GameObject CPU1Gun;
    public GameObject CPU2Gun;
    public GameObject CPU3Gun;
    public GameObject MiniGuns;

    //this gameobjects lists are galaxyes to place randomly on scene
    public GameObject GalaxyObjects;
    private ParticleSystem.MainModule main; //is used to change galaxy colors randomly on start
    private GameObject galaxy1; //are used to store locally game objects for being able to change their particles color randomely
    private GameObject galaxy2;

    //to display count of player guns and set them active or disactivate in guns greed
    public Text gun1ValuePlayer;
    public Text gun2ValuePlayer;
    public Text gun3ValuePlayer;

    public Text yourFleetText;

    public GameObject Gun1Indicator;
    public GameObject Gun2Indicator;
    public GameObject Gun3Indicator;

    private GameObject chousenShipGO;
    public GameObject PlayerShipGunGreed;
    public GameObject setButton;
    public GameObject noMinigGunPanel;
    

    public static int y;
    public static bool launcheAFighter = false;

    public static bool noMoreFighters = false;

    public Text allFightersCount;
    public RawImage fightersImg;

    public Text EnergyCount;
    public RawImage EnergyImg;

    public AudioSource ChoseShipSound;
    public AudioSource NotChousenSound;
    public AudioSource gunSetSound;

    public static bool gunIsdestroyed = false;

    private Vector3 spawnPoint;

    public Camera backCamera;
    //colors to set for dimention in cameras
    //private Color blackDimensionCol = new Color(0, 0.005f, 0.094f, 0);
    //private Color blueDimensionCol = new Color(0, 0.208f, 0.5f, 0);
    //private Color redDimensionCol = new Color(0.5568628f, 0.1294118f, 0.1404912f, 0);

    public AudioSource gunShotSound;

    //the count of lost cruisers after loosing the battle with fighters
    public Text lostCruiser1ValueTxt;
    public Text lostCruiser2ValueTxt;
    public Text lostCruiser3ValueTxt;
    public Text lostCruiser4ValueTxt;

    public GameObject lostShipsPanel;

    public GameObject toBattleButton;
    public GameObject noCruisersButton;
    public GameObject youWinWithoutCruisersBut;

    //UI texts to use with chosen language
    public Text youWinLostTxt;
    public Text noCruisersTxt;
    public Text watchVideoTxt;
    //public Text buyCruiserTxt;
    public Text noMiniGunTxt;
    public Text toBattleTxt;
    public Text destroyedCruisersTxt;

    //this bool is used to trak if palyer lost all its energy and bullets
    public bool outOfBulletsAndEnergy = false;

    private List<GameObject> D1PullToActivate;
    private List<GameObject> D2PullToActivate;
    private List<GameObject> D3PullToActivate;
    private List<GameObject> D4PullToActivate;


    //reward ads properties
    [SerializeField]
    private GameObject rewardAdsEnergyOut;
    [SerializeField]
    private Text whatchVideo;
    [SerializeField]
    private Text noEnergy;
    [SerializeField]
    private Text energyGain;
    private int videoTimesUsed;

    // Start is called before the first frame update
    void Start()
    {
        gunIsdestroyed = false;
        launcheAFighter = false; 
        noMoreFighters = false;

        //setting on the fade value of hologram material to make them visible on scene render
        foreach (Material mat in holoMats)
        {
            mat.SetFloat("_Fade", 1f);
        }

        //setting proper language to rewwarded video buttons 
        whatchVideo.text = Constants.Instance.getWatchVideoTxt();
        noEnergy.text = Constants.Instance.getNoEnergy();
        energyGain.text = "+"+Constants.Instance.rewardedEnergyDefence.ToString("0");

        //assignin a proper color to UI token of opposite CPU player that is under attack 
        //OppositeCPUImage.color = Lists.colorOfOpposite;

        //activating a proper dummie as object to defend
        //1 - cruis1 dummy, 2 - cruis2 dummy, 3 - cruis3 dummy, 4 - cruis4 dummy, 11 - StationFed dummy, 22 - station C dummy, 33 - StationD dummy, 44 - station A dummy
        //and the order inside collection is next
        //0 - cruis1 dummy, 1 - cruis2 dummy, 2 - cruis3 dummy, 3 - cruis4 dummy, 4 - StationFed dummy, 5 - station C dummy, 6 - StationD dummy, 7 - station A dummy
        if (Lists.dummyOnDefenceScene == 1) dummiesCollection[0].SetActive(true);
        else if (Lists.dummyOnDefenceScene == 2) dummiesCollection[1].SetActive(true);
        else if(Lists.dummyOnDefenceScene == 3) dummiesCollection[2].SetActive(true);
        else if(Lists.dummyOnDefenceScene == 4) dummiesCollection[3].SetActive(true);
        else if(Lists.dummyOnDefenceScene == 11) dummiesCollection[4].SetActive(true);
        else if(Lists.dummyOnDefenceScene == 22) dummiesCollection[5].SetActive(true);
        else if(Lists.dummyOnDefenceScene == 33) dummiesCollection[6].SetActive(true);
        else dummiesCollection[7].SetActive(true);

        //activating a proper dummie as object to defend
        //1 - cruis1 dummy, 2 - cruis2 dummy, 3 - cruis3 dummy, 4 - cruis4 dummy, 5 - cruisG dummy, 
        //11 - StationFed dummy, 22 - station C dummy, 33 - StationD dummy, 44 - station A dummy, 55 - station G dummy
        //and the order inside collection is next
        //0 - cruis1 dummy, 1 - cruis2 dummy, 2 - cruis3 dummy, 3 - cruis4 dummy, 4 - cruisG dummy, 5 - StationFed dummy, 6 - station C dummy, 7 - StationD dummy, 
        //8 - station A dummy, 9 - station G dummy
        if (Lists.dummyOnDefenceSceneEnemy == 1) enemyDummiesCollection[0].SetActive(true);
        else if (Lists.dummyOnDefenceSceneEnemy == 2) enemyDummiesCollection[1].SetActive(true);
        else if (Lists.dummyOnDefenceSceneEnemy == 3) enemyDummiesCollection[2].SetActive(true);
        else if (Lists.dummyOnDefenceSceneEnemy == 4) enemyDummiesCollection[3].SetActive(true);
        else if (Lists.dummyOnDefenceSceneEnemy == 5) enemyDummiesCollection[4].SetActive(true);
        else if (Lists.dummyOnDefenceSceneEnemy == 11) enemyDummiesCollection[5].SetActive(true);
        else if (Lists.dummyOnDefenceSceneEnemy == 22) enemyDummiesCollection[6].SetActive(true);
        else if (Lists.dummyOnDefenceSceneEnemy == 33) enemyDummiesCollection[7].SetActive(true);
        else if (Lists.dummyOnDefenceSceneEnemy == 44) enemyDummiesCollection[8].SetActive(true);
        else enemyDummiesCollection[9].SetActive(true);

        //setting the energy of player count
        EnergyCount.text = Lists.energyOfPlayer.ToString("0");

        //launching the randome quantity of asteroids amidst btw gun and fighters 
        launchingTheAsteroids(Random.Range(30, 50));

        Lists.MiniGunsOnScene.Clear();
        D1PullToActivate = ObjectPullerDefence.current.GetD1PullList();
        D2PullToActivate = ObjectPullerDefence.current.GetD2PullList();
        D3PullToActivate = ObjectPullerDefence.current.GetD3PullList();
        D4PullToActivate = ObjectPullerDefence.current.GetD4PullList();

        noCruisersTxt.text = Constants.Instance.getNoCruisersTxt();
        watchVideoTxt.text = Constants.Instance.getWatchVideoTxt();
        //buyCruiserTxt.text = Constants.Instance.getBuyCruiserTxt();
        noMiniGunTxt.text = Constants.Instance.getNoMiniGunTxt();
        toBattleTxt.text = Constants.Instance.getToBattleTxt();
        destroyedCruisersTxt.text = Constants.Instance.getDestroyedCruisersTxt();

        //DontDestroyOnLoad(gunSetSound);

        //sets the colour of camera background according to current dimension, as well the background camera
        if (Lists.isBlackDimension)
        {
            //Camera.main.backgroundColor = blackDimensionCol;
            //backCamera.backgroundColor = blackDimensionCol;
            RenderSettings.skybox = blackSkyboxMat;
        }
        if (Lists.isBlueDimension)
        {
            //Camera.main.backgroundColor = blueDimensionCol;
            //backCamera.backgroundColor = blueDimensionCol;
            RenderSettings.skybox = blueSkyboxMat;
        }
        if (Lists.isRedDimension)
        {
            //Camera.main.backgroundColor = redDimensionCol;
            //backCamera.backgroundColor = redDimensionCol;
            RenderSettings.skybox = redSkyboxMat;
        }

        //instantiate two galaxys on battle sce from star
        galaxy1 = Instantiate(GalaxyObjects, new Vector3(Random.Range(-90, -1200),
                   Random.Range(350, -530), 2400), Quaternion.Euler(Random.Range(-30, -60), 0, Random.Range(-40, 40)));
        galaxy1.transform.localScale = new Vector3(20, 1, 20); //makes galaxy bigger by changing its scale


        galaxy1.GetComponent<MeshRenderer>().material.SetColor("_Color1", new Color(UnityEngine.Random.Range(0.15f, 0.3f),
                    UnityEngine.Random.Range(0.7f, 0.85f), 0, 0));
        galaxy1.GetComponent<MeshRenderer>().material.SetColor("_Color2", new Color(UnityEngine.Random.Range(0.6f, 0.8f),
            UnityEngine.Random.Range(0.5f, 0.7f), 0, 0));

        //galaxy1.GetComponent<MeshRenderer>().material.SetColor("_Color1", new Color(0,
        //            UnityEngine.Random.Range(0.55f, 0.85f), UnityEngine.Random.Range(0.45f, 0.8f), 0));
        //galaxy1.GetComponent<MeshRenderer>().material.SetColor("_Color2", new Color(0,
        //    0.01f, UnityEngine.Random.Range(0.45f, 0.85f), 0));

        galaxy2 = Instantiate(GalaxyObjects, new Vector3(Random.Range(90, 1200),
                   Random.Range(350, -530), 2400), Quaternion.Euler(Random.Range(-30, -60), 0, Random.Range(-40, 40)));
        galaxy2.transform.localScale = new Vector3(20, 1, 20);
        galaxy2.GetComponent<MeshRenderer>().material.SetColor("_Color1", new Color(UnityEngine.Random.Range(0.1f, 0.2f),
                    0, UnityEngine.Random.Range(0.45f, 0.85f), 0));
        galaxy2.GetComponent<MeshRenderer>().material.SetColor("_Color2", new Color(UnityEngine.Random.Range(0.55f, 0.85f),
            0, UnityEngine.Random.Range(0.45f, 0.65f), 0));

        //main = galaxy1.GetComponent<ParticleSystem>().main;
        //main.startColor = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f, 1f, 1f); //generates randome color for galaxy
        //main = galaxy2.GetComponent<ParticleSystem>().main;
        //main.startColor = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f, 1f, 1f); //generates randome color for galaxy

        spawnPoint = new Vector3(0, 0, 5.5f);

        y = Lists.FightersCPU > 4 ? 5 : Lists.FightersCPU;
        allFightersCount.text = Lists.FightersCPU.ToString();
        if (Lists.isBlackDimension)
        {
            for (int i = 0; i < y; i++)
            {
                //Instantiate(Random.Range(0, 2) == 0 ? Destr4CPU : Destr3CPU, Random.Range(0, 2) == 0 ? new Vector3(Random.Range(2000f, 2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f))
                //: new Vector3(Random.Range(-2000f, -2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f)), Quaternion.identity);

                GameObject ship = ObjectPullerDefence.current.GetUniversalBullet(Random.Range(0, 2) == 0 ? D4PullToActivate : D3PullToActivate);
                ship.transform.position = Random.Range(0, 2) == 0 ? new Vector3(Random.Range(2000f, 2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f))
                : new Vector3(Random.Range(-2000f, -2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f));
                ship.transform.rotation = Quaternion.identity;
                ship.SetActive(true);
            }
        }
        else if (Lists.isBlueDimension)
        {
            for (int i = 0; i < y; i++)
            {
                //Instantiate(Random.Range(0, 3) > 0 ? Destr2CPU : Random.Range(0, 2) == 0 ? Destr3CPU : Destr4CPU, Random.Range(0, 2) == 0 ? new Vector3(Random.Range(2000f, 2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f))
                //: new Vector3(Random.Range(-2000f, -2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f)), Quaternion.identity);

                GameObject ship = ObjectPullerDefence.current.GetUniversalBullet(Random.Range(0, 3) > 0 ? D2PullToActivate : Random.Range(0, 2) == 0 ? D3PullToActivate : D4PullToActivate);
                ship.transform.position = Random.Range(0, 2) == 0 ? new Vector3(Random.Range(2000f, 2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f))
                : new Vector3(Random.Range(-2000f, -2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f));
                ship.transform.rotation = Quaternion.identity;
                ship.SetActive(true);

            }
        }
        else if (Lists.isRedDimension)
        {
            for (int i = 0; i < y; i++)
            {
                //Instantiate(Random.Range(0, 3) > 0 ? Destr1CPU : Random.Range(0, 3) > 0 ? Destr2CPU : Random.Range(0, 2) == 0 ? Destr4CPU : Destr3CPU, Random.Range(0, 2) == 0 ? new Vector3(Random.Range(2000f, 2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f))
                //: new Vector3(Random.Range(-2000f, -2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f)), Quaternion.identity);

                GameObject ship = ObjectPullerDefence.current.GetUniversalBullet(Random.Range(0, 3) > 0 ? D1PullToActivate : Random.Range(0, 3) > 0 ? D2PullToActivate : Random.Range(0, 2) == 0 ? D4PullToActivate : D3PullToActivate);
                ship.transform.position = Random.Range(0, 2) == 0 ? new Vector3(Random.Range(2000f, 2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f))
                : new Vector3(Random.Range(-2000f, -2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f));
                ship.transform.rotation = Quaternion.identity;
                ship.SetActive(true);
            }
        }
        yourFleetText.text = Constants.Instance.getYouGunsTxt();
        settingGunGreedUParamsI();

        //so if player has no guns from the very beginning there will appear lose cruisers panel from the beginning
        if ((Lists.Gun1OfPlayer + Lists.Gun2OfPlayer + Lists.Gun3OfPlayer) == 0)
        {
            setButton.SetActive(false); //disactivating the button of setting guns cause by default it is active on canvas
            PlayerShipGunGreed.SetActive(false); //disactivating the mini panel of guns cause by default it is active on canvas
            List<Text> toReduceCruisersTxtObj = new List<Text>();
            if (Lists.isBlackDimension)
            {
                //adding all cruisers to special list to chose one randomely after player lost the battle with fighters
                if (Lists.Cruis1OfPlayer > 0)
                {
                    toReduceCruisersTxtObj.Add(lostCruiser1ValueTxt);
                }
                if (Lists.Cruis2OfPlayer > 0)
                {
                    toReduceCruisersTxtObj.Add(lostCruiser2ValueTxt);
                }
                if (Lists.Cruis3OfPlayer > 0)
                {
                    toReduceCruisersTxtObj.Add(lostCruiser3ValueTxt);
                }
                if (Lists.Cruis4OfPlayer > 0)
                {
                    toReduceCruisersTxtObj.Add(lostCruiser4ValueTxt);
                }

                //reduces only one cruiser according to deimension (so for black dimension reduce rate is ones) . And adding it to lost ships lists accordingly
                int x = Random.Range(0, toReduceCruisersTxtObj.Count());
                toReduceCruisersTxtObj[x].text = "1";
                toReduceCruisersTxtObj[x].transform.parent.gameObject.SetActive(true);
                if (toReduceCruisersTxtObj[x] == lostCruiser1ValueTxt)
                {
                    Lists.Cruis1OfPlayer--;
                    Lists.C1Lose++;
                }
                else if (toReduceCruisersTxtObj[x] == lostCruiser2ValueTxt)
                {
                    Lists.Cruis2OfPlayer--;
                    Lists.C2Lose++;
                }
                else if (toReduceCruisersTxtObj[x] == lostCruiser3ValueTxt)
                {
                    Lists.Cruis3OfPlayer--;
                    Lists.C3Lose++;
                }
                else if (toReduceCruisersTxtObj[x] == lostCruiser4ValueTxt)
                {
                    Lists.Cruis4OfPlayer--;
                    Lists.C4Lose++;
                }
            }
            if (Lists.isBlueDimension)
            {
                if (Lists.Cruis1OfPlayer > 0)
                {
                    toReduceCruisersTxtObj.Add(lostCruiser1ValueTxt);
                }
                if (Lists.Cruis2OfPlayer > 0)
                {
                    toReduceCruisersTxtObj.Add(lostCruiser2ValueTxt);
                }
                if (Lists.Cruis3OfPlayer > 0)
                {
                    toReduceCruisersTxtObj.Add(lostCruiser3ValueTxt);
                }
                if (Lists.Cruis4OfPlayer > 0)
                {
                    toReduceCruisersTxtObj.Add(lostCruiser4ValueTxt);
                }
                int x = Random.Range(0, toReduceCruisersTxtObj.Count());
                toReduceCruisersTxtObj[x].text = "1";
                toReduceCruisersTxtObj[x].transform.parent.gameObject.SetActive(true);
                if (toReduceCruisersTxtObj[x] == lostCruiser1ValueTxt)
                {
                    Lists.Cruis1OfPlayer--;
                    Lists.C1Lose++;
                }
                else if (toReduceCruisersTxtObj[x] == lostCruiser2ValueTxt)
                {
                    Lists.Cruis2OfPlayer--;
                    Lists.C2Lose++;
                }
                else if (toReduceCruisersTxtObj[x] == lostCruiser3ValueTxt)
                {
                    Lists.Cruis3OfPlayer--;
                    Lists.C3Lose++;
                }
                else if (toReduceCruisersTxtObj[x] == lostCruiser4ValueTxt)
                {
                    Lists.Cruis4OfPlayer--;
                    Lists.C4Lose++;
                }
                toReduceCruisersTxtObj.Remove(toReduceCruisersTxtObj[x]);

                //reducing a cruiser amount for second time if player has one. And adding it to lost ships lists accordingly
                if (toReduceCruisersTxtObj.Count > 0)
                {
                    x = Random.Range(0, toReduceCruisersTxtObj.Count());
                    toReduceCruisersTxtObj[x].text = "1";
                    toReduceCruisersTxtObj[x].transform.parent.gameObject.SetActive(true);
                    if (toReduceCruisersTxtObj[x] == lostCruiser1ValueTxt)
                    {
                        Lists.Cruis1OfPlayer--;
                        Lists.C1Lose++;
                    }
                    else if (toReduceCruisersTxtObj[x] == lostCruiser2ValueTxt)
                    {
                        Lists.Cruis2OfPlayer--;
                        Lists.C2Lose++;
                    }
                    else if (toReduceCruisersTxtObj[x] == lostCruiser3ValueTxt)
                    {
                        Lists.Cruis3OfPlayer--;
                        Lists.C3Lose++;
                    }
                    else if (toReduceCruisersTxtObj[x] == lostCruiser4ValueTxt)
                    {
                        Lists.Cruis4OfPlayer--;
                        Lists.C4Lose++;
                    }
                }
            }
            if (Lists.isRedDimension)
            {
                if (Lists.Cruis1OfPlayer > 0)
                {
                    toReduceCruisersTxtObj.Add(lostCruiser1ValueTxt);
                }
                if (Lists.Cruis2OfPlayer > 0)
                {
                    toReduceCruisersTxtObj.Add(lostCruiser2ValueTxt);
                }
                if (Lists.Cruis3OfPlayer > 0)
                {
                    toReduceCruisersTxtObj.Add(lostCruiser3ValueTxt);
                }
                if (Lists.Cruis4OfPlayer > 0)
                {
                    toReduceCruisersTxtObj.Add(lostCruiser4ValueTxt);
                }
                int x = Random.Range(0, toReduceCruisersTxtObj.Count());
                toReduceCruisersTxtObj[x].text = "1";
                toReduceCruisersTxtObj[x].transform.parent.gameObject.SetActive(true);
                if (toReduceCruisersTxtObj[x] == lostCruiser1ValueTxt)
                {
                    Lists.Cruis1OfPlayer--;
                    Lists.C1Lose++;
                }
                else if (toReduceCruisersTxtObj[x] == lostCruiser2ValueTxt)
                {
                    Lists.Cruis2OfPlayer--;
                    Lists.C2Lose++;
                }
                else if (toReduceCruisersTxtObj[x] == lostCruiser3ValueTxt)
                {
                    Lists.Cruis3OfPlayer--;
                    Lists.C3Lose++;
                }
                else if (toReduceCruisersTxtObj[x] == lostCruiser4ValueTxt)
                {
                    Lists.Cruis4OfPlayer--;
                    Lists.C4Lose++;
                }
                toReduceCruisersTxtObj.Remove(toReduceCruisersTxtObj[x]);

                //reducing a cruiser amount for second time if player has one. And adding it to lost ships lists accordingly
                if (toReduceCruisersTxtObj.Count > 0)
                {
                    x = Random.Range(0, toReduceCruisersTxtObj.Count());
                    toReduceCruisersTxtObj[x].text = "1";
                    toReduceCruisersTxtObj[x].transform.parent.gameObject.SetActive(true);
                    if (toReduceCruisersTxtObj[x] == lostCruiser1ValueTxt)
                    {
                        Lists.Cruis1OfPlayer--;
                        Lists.C1Lose++;
                    }
                    else if (toReduceCruisersTxtObj[x] == lostCruiser2ValueTxt)
                    {
                        Lists.Cruis2OfPlayer--;
                        Lists.C2Lose++;
                    }
                    else if (toReduceCruisersTxtObj[x] == lostCruiser3ValueTxt)
                    {
                        Lists.Cruis3OfPlayer--;
                        Lists.C3Lose++;
                    }
                    else if (toReduceCruisersTxtObj[x] == lostCruiser4ValueTxt)
                    {
                        Lists.Cruis4OfPlayer--;
                        Lists.C4Lose++;
                    }
                }

                //reducing a cruiser amount for third time if player has one. And adding it to lost ships lists accordingly
                if (toReduceCruisersTxtObj.Count > 0)
                {
                    x = Random.Range(0, toReduceCruisersTxtObj.Count());
                    toReduceCruisersTxtObj[x].text = "1";
                    toReduceCruisersTxtObj[x].transform.parent.gameObject.SetActive(true);
                    if (toReduceCruisersTxtObj[x] == lostCruiser1ValueTxt)
                    {
                        Lists.Cruis1OfPlayer--;
                        Lists.C1Lose++;
                    }
                    else if (toReduceCruisersTxtObj[x] == lostCruiser2ValueTxt)
                    {
                        Lists.Cruis2OfPlayer--;
                        Lists.C2Lose++;
                    }
                    else if (toReduceCruisersTxtObj[x] == lostCruiser3ValueTxt)
                    {
                        Lists.Cruis3OfPlayer--;
                        Lists.C3Lose++;
                    }
                    else if (toReduceCruisersTxtObj[x] == lostCruiser4ValueTxt)
                    {
                        Lists.Cruis4OfPlayer--;
                        Lists.C4Lose++;
                    }
                }
            }
            lostShipsPanel.SetActive(true);
        }
    }

    // the method to launching the asteroids on scene (as paremeter it takes the count of asteroids to launche on scene)
    private void launchingTheAsteroids(int asteroidsCount)
    {
        for (int i = 0; i < asteroidsCount; i++)
        {
            int index = Random.Range(1, 5);
            if (index == 1) AsteroidList = ObjectPullerDefence.current.GetAsteroids1Pull();
            else if (index == 2) AsteroidList = ObjectPullerDefence.current.GetAsteroids2Pull();
            else if (index == 3) AsteroidList = ObjectPullerDefence.current.GetAsteroids3Pull();
            else AsteroidList = ObjectPullerDefence.current.GetAsteroids4Pull();
            AsteroidReal = ObjectPullerDefence.current.GetUniversalBullet(AsteroidList);
            //random area amidst two armies
            AsteroidReal.transform.position = new Vector3(Random.Range(2000, -2000), Random.Range(-350f, 400f), Random.Range(780f, 300f));
            AsteroidReal.transform.rotation = Random.rotation;
            AsteroidReal.SetActive(true);
        }
    }

    private void settingGunGreedUParamsI() {
        gun1ValuePlayer.text = Lists.Gun1OfPlayer.ToString();
        gun2ValuePlayer.text = Lists.Gun2OfPlayer.ToString();
        gun3ValuePlayer.text = Lists.Gun3OfPlayer.ToString();

        //guns set
        if (Lists.Gun1OfPlayer > 0)
        {
            gun1ValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);
            gun1ValuePlayer.text = Lists.Gun1OfPlayer.ToString();
        }
        else gun1ValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
        if (Lists.Gun2OfPlayer > 0)
        {
            gun2ValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);
            gun2ValuePlayer.text = Lists.Gun2OfPlayer.ToString();
        }
        else gun2ValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
        if (Lists.Gun3OfPlayer > 0)
        {
            gun3ValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);
            gun3ValuePlayer.text = Lists.Gun3OfPlayer.ToString();
        }
        else gun3ValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
    }
 
    public void setGunButton() {
        if (chousenShipGO != null)
        {
            Time.timeScale = 1;
            gunSetSound.Play();
            PlayerShipGunGreed.GetComponent<Animator>().SetBool("Disactivate", true);
            setButton.SetActive(false);
            shotButton.GetComponent<GunShotButt>().enabled = true;
            //aimingButton.GetComponent<DefAimingCtrlr>().enabled = true;
            Instantiate(chousenShipGO, spawnPoint, Quaternion.Euler(0, 0, 0));

            if (Lists.MiniGunOfPlayer >= 2)
            {
                //Lists.MiniGunOfPlayer -= 4;
                Instantiate(MiniGuns, new Vector3(-1.5f, 0, 6.5f), Quaternion.Euler(0, 0, 0));
                Instantiate(MiniGuns, new Vector3(1.5f, 0, 6.5f), Quaternion.Euler(0, 0, 0));
                //Instantiate(MiniGuns, new Vector3(-2.5f, 0, 6.5f), Quaternion.Euler(0, 0, 0));
                //Instantiate(MiniGuns, new Vector3(2.5f, 0, 6.5f), Quaternion.Euler(0, 0, 0));
            }
            //else if (Lists.MiniGunOfPlayer == 3)
            //{
            //    //Lists.MiniGunOfPlayer -= 3;
            //    Instantiate(MiniGuns, new Vector3(-1.5f, 0, 6.5f), Quaternion.Euler(0, 0, 0));
            //    Instantiate(MiniGuns, new Vector3(1.5f, 0, 6.5f), Quaternion.Euler(0, 0, 0));
            //    Instantiate(MiniGuns, new Vector3(-2.5f, 0, 6.5f), Quaternion.Euler(0, 0, 0));
            //}
            //else if (Lists.MiniGunOfPlayer == 2)
            //{
            //    //Lists.MiniGunOfPlayer -= 2;
            //    Instantiate(MiniGuns, new Vector3(-1.5f, 0, 6.5f), Quaternion.Euler(0, 0, 0));
            //    Instantiate(MiniGuns, new Vector3(1.5f, 0, 6.5f), Quaternion.Euler(0, 0, 0));
            //}
            else if (Lists.MiniGunOfPlayer == 1)
            {
                //Lists.MiniGunOfPlayer -= 1;
                Instantiate(MiniGuns, new Vector3(-1.5f, 0, 6.5f), Quaternion.Euler(0, 0, 0));
            }
            else if (Lists.MiniGunOfPlayer == 0)
            {
                noMinigGunPanel.SetActive(true);
                noMinigGunPanel.GetComponent<Animator>().SetBool("dissolve", true);
            }



            //sets the colour of camera background according to current dimension, as well the background camera
            //if (Lists.isBlackDimension)
            //{
            //    Camera.main.backgroundColor = blackDimensionCol;
            //}
            //if (Lists.isBlueDimension)
            //{
            //    Camera.main.backgroundColor = blueDimensionCol;
            //}
            //if (Lists.isRedDimension)
            //{
            //    Camera.main.backgroundColor = redDimensionCol;
            //}


            //time is necessary to let barrel to be st to 
            //to DefAimingCtrlr class from DefBarrelCtrlr class
            Invoke("settingTheVirtualCameraToBarrelAfterTime", 0.5f);
        }
        else NotChousenSound.Play();
    }

    //this method is called from setGunButton method of this class after 0.5 second after the new gun was set on scene, time is necessary to let barrel to be st to 
    //to DefAimingCtrlr class from DefBarrelCtrlr class
    private void settingTheVirtualCameraToBarrelAfterTime()
    {
        virtualCamera.GetComponent<VirtualCamCtrlr>().getTheGunBarrelInstaceToFollow(DefAimingCtrlr.barrelOfGun);
    }

    //this method is assigned to button of camera on defence scene which switches the camera from close to fam from turrel
    public void changeCamera() {
        virtualCamera.GetComponent<VirtualCamCtrlr>().changeTheCameraViewOnDefenceScene();
    }

    //this method makes player camera closer to gun while it is time to reload the gun
    public void changeCameraWhileReloading()
    {
        virtualCamera.GetComponent<VirtualCamCtrlr>().changeTheCameraViewOnDefenceSceneWhileReloadingGun();
    }

    public void G1Button()
    {
        chousenShipGO = CPU1Gun;
        Gun1Indicator.SetActive(true);
        Gun2Indicator.SetActive(false);
        Gun3Indicator.SetActive(false);
        ChoseShipSound.Play();
    }
    public void G2Button()
    {
        chousenShipGO = CPU2Gun;
        Gun2Indicator.SetActive(true);
        Gun1Indicator.SetActive(false);
        Gun3Indicator.SetActive(false);
        ChoseShipSound.Play();
    }
    public void G3Button()
    {
        chousenShipGO = CPU3Gun;
        Gun3Indicator.SetActive(true);
        Gun1Indicator.SetActive(false);
        Gun2Indicator.SetActive(false);
        ChoseShipSound.Play();
    }

    

    //switching to nex stage of battle if there left at least one cruiser
    public void goToBattle()
    {
        gunSetSound.Play();
        Lists.DefendGun.Clear(); //this is really important thing to put on scene switcher. It clears out the list of guns. If it does not it will hold the gun on list
        //when player win the battle with fighters and add to it the second GO to list when next defend scene is loaded and thet that all will cause a crash of game
        Lists.FightersCPU = 0;
        SceneSwitchMngr.LoadBattleScene();
    }

    //closes lost ships panel and sets active noCruiserButton if player does not have any cruisers left
    //alternatively turns on toBattleButton if there is at least one cruiser left
    public void closeLostShipsPanel()
    {
        gunSetSound.Play();
        lostShipsPanel.SetActive(false);
        if (Lists.battleWithCruiser || Lists.battleWithStation || Lists.battleWithGuard)
        {
            if ((Lists.Cruis1OfPlayer + Lists.Cruis2OfPlayer + Lists.Cruis3OfPlayer + Lists.Cruis4OfPlayer) > 0)
            {
                toBattleButton.SetActive(true);
            }
            else
            {
                //TODO WITH SUGGESTING TO PLAYER TO BUY A CRUISER
                noCruisersButton.SetActive(true);
                //noCruisersButton.transform.localPosition = new Vector2(0, 0);
            }
        }
        //else if (Lists.battleWithDestroyer) {
        //    youWinLostTxt.text = Lists.getYouLostTxt();
        //    youWinLostTxt.color = Color.red;
        //    youWinWithoutCruisersBut.GetComponent<Image>().color = Color.red;
        //    youWinWithoutCruisersBut.SetActive(true);
        //    Lists.isAfterDestrLost = true;
        //}


        //Debug.Log("C1 = " + Lists.Cruis1OfPlayer + " C2 = " + Lists.Cruis2OfPlayer + " C3 = " + Lists.Cruis3OfPlayer + " C4 = " + Lists.Cruis4OfPlayer);
    }

    //this method publicly called by close rewarded ads (energy out) panel and activates the defeat panel
    public void closeRewrdedOutOfEnergy() {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
        ChoseShipSound.Play();
        rewardAdsEnergyOut.SetActive(false);
        //rewardAdsEnergyOut.transform.localPosition = new Vector2(-30000, 0); //putting out the panel of rewarded video
        shotButton.GetComponent<GunShotButt>().enabled = false;
        if (Lists.FightersCPU > 0)
        {
            List<Text> toReduceCruisersTxtObj = new List<Text>();
            if (Lists.isBlackDimension)
            {
                //adding all cruisers to special list to chose one randomely after player lost the battle with fighters
                if (Lists.Cruis1OfPlayer > 0)
                {
                    toReduceCruisersTxtObj.Add(lostCruiser1ValueTxt);
                }
                if (Lists.Cruis2OfPlayer > 0)
                {
                    toReduceCruisersTxtObj.Add(lostCruiser2ValueTxt);
                }
                if (Lists.Cruis3OfPlayer > 0)
                {
                    toReduceCruisersTxtObj.Add(lostCruiser3ValueTxt);
                }
                if (Lists.Cruis4OfPlayer > 0)
                {
                    toReduceCruisersTxtObj.Add(lostCruiser4ValueTxt);
                }

                //reduces only one cruiser according to deimension (so for black dimension reduce rate is ones) . And adding it to lost ships lists accordingly
                int x = Random.Range(0, toReduceCruisersTxtObj.Count());
                toReduceCruisersTxtObj[x].text = "1";
                toReduceCruisersTxtObj[x].transform.parent.gameObject.SetActive(true);
                if (toReduceCruisersTxtObj[x] == lostCruiser1ValueTxt)
                {
                    Lists.Cruis1OfPlayer--;
                    Lists.C1Lose++;
                }
                else if (toReduceCruisersTxtObj[x] == lostCruiser2ValueTxt)
                {
                    Lists.Cruis2OfPlayer--;
                    Lists.C2Lose++;
                }
                else if (toReduceCruisersTxtObj[x] == lostCruiser3ValueTxt)
                {
                    Lists.Cruis3OfPlayer--;
                    Lists.C3Lose++;
                }
                else if (toReduceCruisersTxtObj[x] == lostCruiser4ValueTxt)
                {
                    Lists.Cruis4OfPlayer--;
                    Lists.C4Lose++;
                }
            }
            if (Lists.isBlueDimension)
            {
                if (Lists.Cruis1OfPlayer > 0)
                {
                    toReduceCruisersTxtObj.Add(lostCruiser1ValueTxt);
                }
                if (Lists.Cruis2OfPlayer > 0)
                {
                    toReduceCruisersTxtObj.Add(lostCruiser2ValueTxt);
                }
                if (Lists.Cruis3OfPlayer > 0)
                {
                    toReduceCruisersTxtObj.Add(lostCruiser3ValueTxt);
                }
                if (Lists.Cruis4OfPlayer > 0)
                {
                    toReduceCruisersTxtObj.Add(lostCruiser4ValueTxt);
                }
                int x = Random.Range(0, toReduceCruisersTxtObj.Count());
                toReduceCruisersTxtObj[x].text = "1";
                toReduceCruisersTxtObj[x].transform.parent.gameObject.SetActive(true);
                if (toReduceCruisersTxtObj[x] == lostCruiser1ValueTxt)
                {
                    Lists.Cruis1OfPlayer--;
                    Lists.C1Lose++;
                }
                else if (toReduceCruisersTxtObj[x] == lostCruiser2ValueTxt)
                {
                    Lists.Cruis2OfPlayer--;
                    Lists.C2Lose++;
                }
                else if (toReduceCruisersTxtObj[x] == lostCruiser3ValueTxt)
                {
                    Lists.Cruis3OfPlayer--;
                    Lists.C3Lose++;
                }
                else if (toReduceCruisersTxtObj[x] == lostCruiser4ValueTxt)
                {
                    Lists.Cruis4OfPlayer--;
                    Lists.C4Lose++;
                }
                toReduceCruisersTxtObj.Remove(toReduceCruisersTxtObj[x]);

                //reducing a cruiser amount for second time if player has one. And adding it to lost ships lists accordingly
                if (toReduceCruisersTxtObj.Count > 0)
                {
                    x = Random.Range(0, toReduceCruisersTxtObj.Count());
                    toReduceCruisersTxtObj[x].text = "1";
                    toReduceCruisersTxtObj[x].transform.parent.gameObject.SetActive(true);
                    if (toReduceCruisersTxtObj[x] == lostCruiser1ValueTxt)
                    {
                        Lists.Cruis1OfPlayer--;
                        Lists.C1Lose++;
                    }
                    else if (toReduceCruisersTxtObj[x] == lostCruiser2ValueTxt)
                    {
                        Lists.Cruis2OfPlayer--;
                        Lists.C2Lose++;
                    }
                    else if (toReduceCruisersTxtObj[x] == lostCruiser3ValueTxt)
                    {
                        Lists.Cruis3OfPlayer--;
                        Lists.C3Lose++;
                    }
                    else if (toReduceCruisersTxtObj[x] == lostCruiser4ValueTxt)
                    {
                        Lists.Cruis4OfPlayer--;
                        Lists.C4Lose++;
                    }
                }
            }
            if (Lists.isRedDimension)
            {
                if (Lists.Cruis1OfPlayer > 0)
                {
                    toReduceCruisersTxtObj.Add(lostCruiser1ValueTxt);
                }
                if (Lists.Cruis2OfPlayer > 0)
                {
                    toReduceCruisersTxtObj.Add(lostCruiser2ValueTxt);
                }
                if (Lists.Cruis3OfPlayer > 0)
                {
                    toReduceCruisersTxtObj.Add(lostCruiser3ValueTxt);
                }
                if (Lists.Cruis4OfPlayer > 0)
                {
                    toReduceCruisersTxtObj.Add(lostCruiser4ValueTxt);
                }
                int x = Random.Range(0, toReduceCruisersTxtObj.Count());
                toReduceCruisersTxtObj[x].text = "1";
                toReduceCruisersTxtObj[x].transform.parent.gameObject.SetActive(true);
                if (toReduceCruisersTxtObj[x] == lostCruiser1ValueTxt)
                {
                    Lists.Cruis1OfPlayer--;
                    Lists.C1Lose++;
                }
                else if (toReduceCruisersTxtObj[x] == lostCruiser2ValueTxt)
                {
                    Lists.Cruis2OfPlayer--;
                    Lists.C2Lose++;
                }
                else if (toReduceCruisersTxtObj[x] == lostCruiser3ValueTxt)
                {
                    Lists.Cruis3OfPlayer--;
                    Lists.C3Lose++;
                }
                else if (toReduceCruisersTxtObj[x] == lostCruiser4ValueTxt)
                {
                    Lists.Cruis4OfPlayer--;
                    Lists.C4Lose++;
                }
                toReduceCruisersTxtObj.Remove(toReduceCruisersTxtObj[x]);

                //reducing a cruiser amount for second time if player has one. And adding it to lost ships lists accordingly
                if (toReduceCruisersTxtObj.Count > 0)
                {
                    x = Random.Range(0, toReduceCruisersTxtObj.Count());
                    toReduceCruisersTxtObj[x].text = "1";
                    toReduceCruisersTxtObj[x].transform.parent.gameObject.SetActive(true);
                    if (toReduceCruisersTxtObj[x] == lostCruiser1ValueTxt)
                    {
                        Lists.Cruis1OfPlayer--;
                        Lists.C1Lose++;
                    }
                    else if (toReduceCruisersTxtObj[x] == lostCruiser2ValueTxt)
                    {
                        Lists.Cruis2OfPlayer--;
                        Lists.C2Lose++;
                    }
                    else if (toReduceCruisersTxtObj[x] == lostCruiser3ValueTxt)
                    {
                        Lists.Cruis3OfPlayer--;
                        Lists.C3Lose++;
                    }
                    else if (toReduceCruisersTxtObj[x] == lostCruiser4ValueTxt)
                    {
                        Lists.Cruis4OfPlayer--;
                        Lists.C4Lose++;
                    }
                }

                //reducing a cruiser amount for third time if player has one. And adding it to lost ships lists accordingly
                if (toReduceCruisersTxtObj.Count > 0)
                {
                    x = Random.Range(0, toReduceCruisersTxtObj.Count());
                    toReduceCruisersTxtObj[x].text = "1";
                    toReduceCruisersTxtObj[x].transform.parent.gameObject.SetActive(true);
                    if (toReduceCruisersTxtObj[x] == lostCruiser1ValueTxt)
                    {
                        Lists.Cruis1OfPlayer--;
                        Lists.C1Lose++;
                    }
                    else if (toReduceCruisersTxtObj[x] == lostCruiser2ValueTxt)
                    {
                        Lists.Cruis2OfPlayer--;
                        Lists.C2Lose++;
                    }
                    else if (toReduceCruisersTxtObj[x] == lostCruiser3ValueTxt)
                    {
                        Lists.Cruis3OfPlayer--;
                        Lists.C3Lose++;
                    }
                    else if (toReduceCruisersTxtObj[x] == lostCruiser4ValueTxt)
                    {
                        Lists.Cruis4OfPlayer--;
                        Lists.C4Lose++;
                    }
                }
            }
            lostShipsPanel.SetActive(true);
        }
    }

    //rewarding the player after he/she watched the video
    public void watchRewardedOutOfEnergy () {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;

        rewardAdsEnergyOut.SetActive(false);
        //rewardAdsEnergyOut.transform.localPosition = new Vector2(-30000, 0); //putting out the panel of rewarded video
        ChoseShipSound.Play();

        DefBarrelCtrlr.bonusHPAfterRewarded = true;
        Lists.energyOfPlayer = Constants.Instance.rewardedEnergyDefence;
        EnergyCount.text = Lists.energyOfPlayer.ToString("0");
        StartCoroutine(EnergyReduceSygnalsToPLyaer());
    }

    //switching to space scene from defend scene after player lost all cruisers
    public void goToJourneyAfterLost()
    {
        gunSetSound.Play();
        Lists.DefendGun.Clear(); //this is really important thing to put on scene switcher. It clears out the list of guns. If it does not it will hold the gun on list
        //when player win the battle with fighters and add to it the second GO to list when next defend scene is loaded and thet that all will cause a crash of game
        Lists.FightersCPU = 0;
        Lists.isAfterNoCruis = true;
        Lists.isAfterBattleLost = true; //maybe this is an extra
        Lists.isAfterBattleWin = false; //maybe this is an extra
        SceneSwitchMngr.LoadJourneyScene();
    }

    //rewarding the player after he/she watched the video
    public void watchRewardedNoCruiser()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;

        noCruisersButton.SetActive(false);
        //noCruisersButton.transform.localPosition = new Vector2(-30000, 0); //putting out the panel of rewarded video
        ChoseShipSound.Play();

        //rewrding the player with 3 cruisers of 4 th class. We can surely add only this cruiser to player fleet
        Lists.Cruis4OfPlayer += 3;
        Lists.C4Lose -= 3;

        goToBattle(); //switching to battle scene after player watched the rewarded video
    }

    private IEnumerator EnergyReduceSygnalsToPLyaer()
    {
        EnergyImg.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.6f);
        EnergyImg.color = new Color(0.65f, 0.65f, 0.65f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        allFightersCount.text = Lists.FightersCPU.ToString();

        //getting a trigger from ShipsCtrlr each time when ship is destroyed and launching a new firghter to scene with specific dimension set
        if (launcheAFighter)
        {
            launcheAFighter = false;
            if (Lists.isBlackDimension && Lists.FightersCPU > 0)
            {
                //Instantiate(Random.Range(0, 2) == 0 ? Destr4CPU : Destr3CPU, Random.Range(0, 2) == 0 ? new Vector3(Random.Range(2000f, 2500f),
                //Random.Range(-500f, 500f), Random.Range(800f, 1300f))
                //: new Vector3(Random.Range(-2000f, -2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f)), Quaternion.identity);

                GameObject ship = ObjectPullerDefence.current.GetUniversalBullet(Random.Range(0, 2) == 0 ? D4PullToActivate : D3PullToActivate);
                ship.transform.position = Random.Range(0, 2) == 0 ? new Vector3(Random.Range(2000f, 2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f))
                : new Vector3(Random.Range(-2000f, -2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f));
                ship.transform.rotation = Quaternion.identity;
                ship.SetActive(true);
            }
            else if (Lists.isBlueDimension && Lists.FightersCPU > 0)
            {
                //Instantiate(Random.Range(0, 3) > 0 ? Destr2CPU : Random.Range(0, 2) == 0 ? Destr3CPU : Destr4CPU, Random.Range(0, 2) == 0 ?
                //    new Vector3(Random.Range(2000f, 2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f))
                //    : new Vector3(Random.Range(-2000f, -2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f)), Quaternion.identity);

                GameObject ship = ObjectPullerDefence.current.GetUniversalBullet(Random.Range(0, 3) > 0 ? D2PullToActivate : Random.Range(0, 2) == 0 ? D3PullToActivate : D4PullToActivate);
                ship.transform.position = Random.Range(0, 2) == 0 ? new Vector3(Random.Range(2000f, 2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f))
                : new Vector3(Random.Range(-2000f, -2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f));
                ship.transform.rotation = Quaternion.identity;
                ship.SetActive(true);
            }
            else if (Lists.isRedDimension && Lists.FightersCPU > 0)
            {
                //Instantiate(Random.Range(0, 3) > 0 ? Destr1CPU : Random.Range(0, 3) > 0 ? Destr2CPU : Random.Range(0, 2) == 0 ? Destr4CPU :
                // Destr3CPU, Random.Range(0, 2) == 0 ? new Vector3(Random.Range(2000f, 2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f))
                // : new Vector3(Random.Range(-2000f, -2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f)), Quaternion.identity);

                GameObject ship = ObjectPullerDefence.current.GetUniversalBullet(Random.Range(0, 3) > 0 ? D1PullToActivate : Random.Range(0, 3) > 0 ? D2PullToActivate : Random.Range(0, 2) == 0 ? D4PullToActivate : D3PullToActivate);
                ship.transform.position = Random.Range(0, 2) == 0 ? new Vector3(Random.Range(2000f, 2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f))
                : new Vector3(Random.Range(-2000f, -2500f), Random.Range(-500f, 500f), Random.Range(800f, 1300f));
                ship.transform.rotation = Quaternion.identity;
                ship.SetActive(true);
            }
        }

        if (gunIsdestroyed)
        {
            //following three lines increases the static count of player mini gun loses to quantity that were on scene when gun was destroyed, destroys all mini guns on scene 
            //and clears the List of mini guns on scene
            Lists.MiniGunOfPlayer -= Lists.MiniGunsOnScene.Count();
            Lists.MiniGunLose += Lists.MiniGunsOnScene.Count();
            foreach (GameObject go in Lists.MiniGunsOnScene) Destroy(go);
            Lists.MiniGunsOnScene.Clear();

            //reducing the quatity of players guns according to its type (only after it is destroyerd, so it will be save in case if gun will not be destroyed and player wins)
            if (chousenShipGO == CPU1Gun) Lists.Gun1OfPlayer--;
            else if (chousenShipGO == CPU2Gun) Lists.Gun2OfPlayer--;
            else if (chousenShipGO == CPU3Gun) Lists.Gun3OfPlayer--;

            //adding lost gun to loses lists of player after battle (to use on Journey scene controller cript)
            if (chousenShipGO == CPU1Gun) Lists.G1Lose++;
            else if (chousenShipGO == CPU2Gun) Lists.G2Lose++;
            else if (chousenShipGO == CPU3Gun) Lists.G3Lose++;

            chousenShipGO = null; //setting the gun null to prevent the bug of setting prevoius gun
            Gun2Indicator.SetActive(false); //setting chosen (i.e. all) gun button indicator false to make player chose one
            Gun1Indicator.SetActive(false);
            Gun3Indicator.SetActive(false);

            if ((Lists.Gun1OfPlayer + Lists.Gun2OfPlayer + Lists.Gun3OfPlayer) > 0)
            {
                //------------------------------------------------------------------------------------------------------------------ TO ADD A TXT FEATURES
                gunShotSound.Stop();
                gunIsdestroyed = false;
                settingGunGreedUParamsI();
                PlayerShipGunGreed.GetComponent<Animator>().SetBool("Disactivate", false);
                setButton.SetActive(true);
                shotButton.GetComponent<GunShotButt>().enabled = false;
            }
            //setting the switch to next stage of fight in case if player has lost all of its guns. 
            //first the code reduces randomly the numbers of cruisers of player and after
            //In case if player will loose all of its cruisers system gives the only option to get back to space with losts. 
            //In case if player still has the cruisers system gives him the only option to switch to next stage
            else
            {
                gunShotSound.Stop();
                gunIsdestroyed = false;
                shotButton.GetComponent<GunShotButt>().enabled = false;
                if (Lists.FightersCPU>0)
                {
                    List<Text> toReduceCruisersTxtObj = new List<Text>();
                    if (Lists.isBlackDimension)
                    {
                        //adding all cruisers to special list to chose one randomely after player lost the battle with fighters
                        if (Lists.Cruis1OfPlayer > 0)
                        {
                            toReduceCruisersTxtObj.Add(lostCruiser1ValueTxt);
                        }
                        if (Lists.Cruis2OfPlayer > 0)
                        {
                            toReduceCruisersTxtObj.Add(lostCruiser2ValueTxt);
                        }
                        if (Lists.Cruis3OfPlayer > 0)
                        {
                            toReduceCruisersTxtObj.Add(lostCruiser3ValueTxt);
                        }
                        if (Lists.Cruis4OfPlayer > 0)
                        {
                            toReduceCruisersTxtObj.Add(lostCruiser4ValueTxt);
                        }

                        //reduces only one cruiser according to deimension (so for black dimension reduce rate is ones) . And adding it to lost ships lists accordingly
                        int x = Random.Range(0, toReduceCruisersTxtObj.Count());
                        toReduceCruisersTxtObj[x].text = "1";
                        toReduceCruisersTxtObj[x].transform.parent.gameObject.SetActive(true);
                        if (toReduceCruisersTxtObj[x] == lostCruiser1ValueTxt)
                        {
                            Lists.Cruis1OfPlayer--;
                            Lists.C1Lose++;
                        }
                        else if (toReduceCruisersTxtObj[x] == lostCruiser2ValueTxt)
                        {
                            Lists.Cruis2OfPlayer--;
                            Lists.C2Lose++;
                        }
                        else if (toReduceCruisersTxtObj[x] == lostCruiser3ValueTxt)
                        {
                            Lists.Cruis3OfPlayer--;
                            Lists.C3Lose++;
                        }
                        else if (toReduceCruisersTxtObj[x] == lostCruiser4ValueTxt)
                        {
                            Lists.Cruis4OfPlayer--;
                            Lists.C4Lose++;
                        }
                    }
                    if (Lists.isBlueDimension)
                    {
                        if (Lists.Cruis1OfPlayer > 0)
                        {
                            toReduceCruisersTxtObj.Add(lostCruiser1ValueTxt);
                        }
                        if (Lists.Cruis2OfPlayer > 0)
                        {
                            toReduceCruisersTxtObj.Add(lostCruiser2ValueTxt);
                        }
                        if (Lists.Cruis3OfPlayer > 0)
                        {
                            toReduceCruisersTxtObj.Add(lostCruiser3ValueTxt);
                        }
                        if (Lists.Cruis4OfPlayer > 0)
                        {
                            toReduceCruisersTxtObj.Add(lostCruiser4ValueTxt);
                        }
                        int x = Random.Range(0, toReduceCruisersTxtObj.Count()); 
                        toReduceCruisersTxtObj[x].text = "1";
                        toReduceCruisersTxtObj[x].transform.parent.gameObject.SetActive(true);
                        if (toReduceCruisersTxtObj[x] == lostCruiser1ValueTxt)
                        {
                            Lists.Cruis1OfPlayer--;
                            Lists.C1Lose++;
                        }
                        else if (toReduceCruisersTxtObj[x] == lostCruiser2ValueTxt)
                        {
                            Lists.Cruis2OfPlayer--;
                            Lists.C2Lose++;
                        }
                        else if (toReduceCruisersTxtObj[x] == lostCruiser3ValueTxt)
                        {
                            Lists.Cruis3OfPlayer--;
                            Lists.C3Lose++;
                        }
                        else if (toReduceCruisersTxtObj[x] == lostCruiser4ValueTxt)
                        {
                            Lists.Cruis4OfPlayer--;
                            Lists.C4Lose++;
                        }
                        toReduceCruisersTxtObj.Remove(toReduceCruisersTxtObj[x]);

                        //reducing a cruiser amount for second time if player has one. And adding it to lost ships lists accordingly
                        if (toReduceCruisersTxtObj.Count > 0)
                        {
                            x = Random.Range(0, toReduceCruisersTxtObj.Count());
                            toReduceCruisersTxtObj[x].text = "1";
                            toReduceCruisersTxtObj[x].transform.parent.gameObject.SetActive(true);
                            if (toReduceCruisersTxtObj[x] == lostCruiser1ValueTxt)
                            {
                                Lists.Cruis1OfPlayer--;
                                Lists.C1Lose++;
                            }
                            else if (toReduceCruisersTxtObj[x] == lostCruiser2ValueTxt)
                            {
                                Lists.Cruis2OfPlayer--;
                                Lists.C2Lose++;
                            }
                            else if (toReduceCruisersTxtObj[x] == lostCruiser3ValueTxt)
                            {
                                Lists.Cruis3OfPlayer--;
                                Lists.C3Lose++;
                            }
                            else if (toReduceCruisersTxtObj[x] == lostCruiser4ValueTxt)
                            {
                                Lists.Cruis4OfPlayer--;
                                Lists.C4Lose++;
                            }
                        }
                    }
                    if (Lists.isRedDimension)
                    {
                        if (Lists.Cruis1OfPlayer > 0)
                        {
                            toReduceCruisersTxtObj.Add(lostCruiser1ValueTxt);
                        }
                        if (Lists.Cruis2OfPlayer > 0)
                        {
                            toReduceCruisersTxtObj.Add(lostCruiser2ValueTxt);
                        }
                        if (Lists.Cruis3OfPlayer > 0)
                        {
                            toReduceCruisersTxtObj.Add(lostCruiser3ValueTxt);
                        }
                        if (Lists.Cruis4OfPlayer > 0)
                        {
                            toReduceCruisersTxtObj.Add(lostCruiser4ValueTxt);
                        }
                        int x = Random.Range(0, toReduceCruisersTxtObj.Count());
                        toReduceCruisersTxtObj[x].text = "1";
                        toReduceCruisersTxtObj[x].transform.parent.gameObject.SetActive(true);
                        if (toReduceCruisersTxtObj[x] == lostCruiser1ValueTxt)
                        {
                            Lists.Cruis1OfPlayer--;
                            Lists.C1Lose++;
                        }
                        else if (toReduceCruisersTxtObj[x] == lostCruiser2ValueTxt)
                        {
                            Lists.Cruis2OfPlayer--;
                            Lists.C2Lose++;
                        }
                        else if (toReduceCruisersTxtObj[x] == lostCruiser3ValueTxt)
                        {
                            Lists.Cruis3OfPlayer--;
                            Lists.C3Lose++;
                        }
                        else if (toReduceCruisersTxtObj[x] == lostCruiser4ValueTxt)
                        {
                            Lists.Cruis4OfPlayer--;
                            Lists.C4Lose++;
                        }
                        toReduceCruisersTxtObj.Remove(toReduceCruisersTxtObj[x]);

                        //reducing a cruiser amount for second time if player has one. And adding it to lost ships lists accordingly
                        if (toReduceCruisersTxtObj.Count > 0)
                        {
                            x = Random.Range(0, toReduceCruisersTxtObj.Count()); 
                            toReduceCruisersTxtObj[x].text = "1";
                            toReduceCruisersTxtObj[x].transform.parent.gameObject.SetActive(true);
                            if (toReduceCruisersTxtObj[x] == lostCruiser1ValueTxt)
                            {
                                Lists.Cruis1OfPlayer--;
                                Lists.C1Lose++;
                            }
                            else if (toReduceCruisersTxtObj[x] == lostCruiser2ValueTxt)
                            {
                                Lists.Cruis2OfPlayer--;
                                Lists.C2Lose++;
                            }
                            else if (toReduceCruisersTxtObj[x] == lostCruiser3ValueTxt)
                            {
                                Lists.Cruis3OfPlayer--;
                                Lists.C3Lose++;
                            }
                            else if (toReduceCruisersTxtObj[x] == lostCruiser4ValueTxt)
                            {
                                Lists.Cruis4OfPlayer--;
                                Lists.C4Lose++;
                            }
                        }

                        //reducing a cruiser amount for third time if player has one. And adding it to lost ships lists accordingly
                        if (toReduceCruisersTxtObj.Count > 0)
                        {
                            x = Random.Range(0, toReduceCruisersTxtObj.Count());
                            toReduceCruisersTxtObj[x].text = "1";
                            toReduceCruisersTxtObj[x].transform.parent.gameObject.SetActive(true);
                            if (toReduceCruisersTxtObj[x] == lostCruiser1ValueTxt)
                            {
                                Lists.Cruis1OfPlayer--;
                                Lists.C1Lose++;
                            }
                            else if (toReduceCruisersTxtObj[x] == lostCruiser2ValueTxt)
                            {
                                Lists.Cruis2OfPlayer--;
                                Lists.C2Lose++;
                            }
                            else if (toReduceCruisersTxtObj[x] == lostCruiser3ValueTxt)
                            {
                                Lists.Cruis3OfPlayer--;
                                Lists.C3Lose++;
                            }
                            else if (toReduceCruisersTxtObj[x] == lostCruiser4ValueTxt)
                            {
                                Lists.Cruis4OfPlayer--;
                                Lists.C4Lose++;
                            }
                        }
                    }
                    lostShipsPanel.SetActive(true);
                }
            }
        }
        //another condition for loose the game if player lost all it's bullets and energy
        if (outOfBulletsAndEnergy) {
            gunShotSound.Stop();
            outOfBulletsAndEnergy = false; //turning off the trigger
            //stopping the time and showing the player the panel of rewarded ads if he has not whatched it twice already
            if (videoTimesUsed < 2)
            {
                rewardAdsEnergyOut.SetActive(true);
                //rewardAdsEnergyOut.transform.localPosition = new Vector2(0, 0);
                gunSetSound.Play();
                //stopping the time to propose to player to whatch video
                Time.timeScale = 0;
                Time.fixedDeltaTime = 0;
            }
            else
            {
                shotButton.GetComponent<GunShotButt>().enabled = false;
                if (Lists.FightersCPU > 0)
                {
                    List<Text> toReduceCruisersTxtObj = new List<Text>();
                    if (Lists.isBlackDimension)
                    {
                        //adding all cruisers to special list to chose one randomely after player lost the battle with fighters
                        if (Lists.Cruis1OfPlayer > 0)
                        {
                            toReduceCruisersTxtObj.Add(lostCruiser1ValueTxt);
                        }
                        if (Lists.Cruis2OfPlayer > 0)
                        {
                            toReduceCruisersTxtObj.Add(lostCruiser2ValueTxt);
                        }
                        if (Lists.Cruis3OfPlayer > 0)
                        {
                            toReduceCruisersTxtObj.Add(lostCruiser3ValueTxt);
                        }
                        if (Lists.Cruis4OfPlayer > 0)
                        {
                            toReduceCruisersTxtObj.Add(lostCruiser4ValueTxt);
                        }

                        //reduces only one cruiser according to deimension (so for black dimension reduce rate is ones) . And adding it to lost ships lists accordingly
                        int x = Random.Range(0, toReduceCruisersTxtObj.Count());
                        toReduceCruisersTxtObj[x].text = "1";
                        toReduceCruisersTxtObj[x].transform.parent.gameObject.SetActive(true);
                        if (toReduceCruisersTxtObj[x] == lostCruiser1ValueTxt)
                        {
                            Lists.Cruis1OfPlayer--;
                            Lists.C1Lose++;
                        }
                        else if (toReduceCruisersTxtObj[x] == lostCruiser2ValueTxt)
                        {
                            Lists.Cruis2OfPlayer--;
                            Lists.C2Lose++;
                        }
                        else if (toReduceCruisersTxtObj[x] == lostCruiser3ValueTxt)
                        {
                            Lists.Cruis3OfPlayer--;
                            Lists.C3Lose++;
                        }
                        else if (toReduceCruisersTxtObj[x] == lostCruiser4ValueTxt)
                        {
                            Lists.Cruis4OfPlayer--;
                            Lists.C4Lose++;
                        }
                    }
                    if (Lists.isBlueDimension)
                    {
                        if (Lists.Cruis1OfPlayer > 0)
                        {
                            toReduceCruisersTxtObj.Add(lostCruiser1ValueTxt);
                        }
                        if (Lists.Cruis2OfPlayer > 0)
                        {
                            toReduceCruisersTxtObj.Add(lostCruiser2ValueTxt);
                        }
                        if (Lists.Cruis3OfPlayer > 0)
                        {
                            toReduceCruisersTxtObj.Add(lostCruiser3ValueTxt);
                        }
                        if (Lists.Cruis4OfPlayer > 0)
                        {
                            toReduceCruisersTxtObj.Add(lostCruiser4ValueTxt);
                        }
                        int x = Random.Range(0, toReduceCruisersTxtObj.Count());
                        toReduceCruisersTxtObj[x].text = "1";
                        toReduceCruisersTxtObj[x].transform.parent.gameObject.SetActive(true);
                        if (toReduceCruisersTxtObj[x] == lostCruiser1ValueTxt)
                        {
                            Lists.Cruis1OfPlayer--;
                            Lists.C1Lose++;
                        }
                        else if (toReduceCruisersTxtObj[x] == lostCruiser2ValueTxt)
                        {
                            Lists.Cruis2OfPlayer--;
                            Lists.C2Lose++;
                        }
                        else if (toReduceCruisersTxtObj[x] == lostCruiser3ValueTxt)
                        {
                            Lists.Cruis3OfPlayer--;
                            Lists.C3Lose++;
                        }
                        else if (toReduceCruisersTxtObj[x] == lostCruiser4ValueTxt)
                        {
                            Lists.Cruis4OfPlayer--;
                            Lists.C4Lose++;
                        }
                        toReduceCruisersTxtObj.Remove(toReduceCruisersTxtObj[x]);

                        //reducing a cruiser amount for second time if player has one. And adding it to lost ships lists accordingly
                        if (toReduceCruisersTxtObj.Count > 0)
                        {
                            x = Random.Range(0, toReduceCruisersTxtObj.Count());
                            toReduceCruisersTxtObj[x].text = "1";
                            toReduceCruisersTxtObj[x].transform.parent.gameObject.SetActive(true);
                            if (toReduceCruisersTxtObj[x] == lostCruiser1ValueTxt)
                            {
                                Lists.Cruis1OfPlayer--;
                                Lists.C1Lose++;
                            }
                            else if (toReduceCruisersTxtObj[x] == lostCruiser2ValueTxt)
                            {
                                Lists.Cruis2OfPlayer--;
                                Lists.C2Lose++;
                            }
                            else if (toReduceCruisersTxtObj[x] == lostCruiser3ValueTxt)
                            {
                                Lists.Cruis3OfPlayer--;
                                Lists.C3Lose++;
                            }
                            else if (toReduceCruisersTxtObj[x] == lostCruiser4ValueTxt)
                            {
                                Lists.Cruis4OfPlayer--;
                                Lists.C4Lose++;
                            }
                        }
                    }
                    if (Lists.isRedDimension)
                    {
                        if (Lists.Cruis1OfPlayer > 0)
                        {
                            toReduceCruisersTxtObj.Add(lostCruiser1ValueTxt);
                        }
                        if (Lists.Cruis2OfPlayer > 0)
                        {
                            toReduceCruisersTxtObj.Add(lostCruiser2ValueTxt);
                        }
                        if (Lists.Cruis3OfPlayer > 0)
                        {
                            toReduceCruisersTxtObj.Add(lostCruiser3ValueTxt);
                        }
                        if (Lists.Cruis4OfPlayer > 0)
                        {
                            toReduceCruisersTxtObj.Add(lostCruiser4ValueTxt);
                        }
                        int x = Random.Range(0, toReduceCruisersTxtObj.Count());
                        toReduceCruisersTxtObj[x].text = "1";
                        toReduceCruisersTxtObj[x].transform.parent.gameObject.SetActive(true);
                        if (toReduceCruisersTxtObj[x] == lostCruiser1ValueTxt)
                        {
                            Lists.Cruis1OfPlayer--;
                            Lists.C1Lose++;
                        }
                        else if (toReduceCruisersTxtObj[x] == lostCruiser2ValueTxt)
                        {
                            Lists.Cruis2OfPlayer--;
                            Lists.C2Lose++;
                        }
                        else if (toReduceCruisersTxtObj[x] == lostCruiser3ValueTxt)
                        {
                            Lists.Cruis3OfPlayer--;
                            Lists.C3Lose++;
                        }
                        else if (toReduceCruisersTxtObj[x] == lostCruiser4ValueTxt)
                        {
                            Lists.Cruis4OfPlayer--;
                            Lists.C4Lose++;
                        }
                        toReduceCruisersTxtObj.Remove(toReduceCruisersTxtObj[x]);

                        //reducing a cruiser amount for second time if player has one. And adding it to lost ships lists accordingly
                        if (toReduceCruisersTxtObj.Count > 0)
                        {
                            x = Random.Range(0, toReduceCruisersTxtObj.Count());
                            toReduceCruisersTxtObj[x].text = "1";
                            toReduceCruisersTxtObj[x].transform.parent.gameObject.SetActive(true);
                            if (toReduceCruisersTxtObj[x] == lostCruiser1ValueTxt)
                            {
                                Lists.Cruis1OfPlayer--;
                                Lists.C1Lose++;
                            }
                            else if (toReduceCruisersTxtObj[x] == lostCruiser2ValueTxt)
                            {
                                Lists.Cruis2OfPlayer--;
                                Lists.C2Lose++;
                            }
                            else if (toReduceCruisersTxtObj[x] == lostCruiser3ValueTxt)
                            {
                                Lists.Cruis3OfPlayer--;
                                Lists.C3Lose++;
                            }
                            else if (toReduceCruisersTxtObj[x] == lostCruiser4ValueTxt)
                            {
                                Lists.Cruis4OfPlayer--;
                                Lists.C4Lose++;
                            }
                        }

                        //reducing a cruiser amount for third time if player has one. And adding it to lost ships lists accordingly
                        if (toReduceCruisersTxtObj.Count > 0)
                        {
                            x = Random.Range(0, toReduceCruisersTxtObj.Count());
                            toReduceCruisersTxtObj[x].text = "1";
                            toReduceCruisersTxtObj[x].transform.parent.gameObject.SetActive(true);
                            if (toReduceCruisersTxtObj[x] == lostCruiser1ValueTxt)
                            {
                                Lists.Cruis1OfPlayer--;
                                Lists.C1Lose++;
                            }
                            else if (toReduceCruisersTxtObj[x] == lostCruiser2ValueTxt)
                            {
                                Lists.Cruis2OfPlayer--;
                                Lists.C2Lose++;
                            }
                            else if (toReduceCruisersTxtObj[x] == lostCruiser3ValueTxt)
                            {
                                Lists.Cruis3OfPlayer--;
                                Lists.C3Lose++;
                            }
                            else if (toReduceCruisersTxtObj[x] == lostCruiser4ValueTxt)
                            {
                                Lists.Cruis4OfPlayer--;
                                Lists.C4Lose++;
                            }
                        }
                    }
                    lostShipsPanel.SetActive(true);
                    gunSetSound.Play();
                }
            }
        }

        if (noMoreFighters) {
            noMoreFighters = false;
            if (Lists.battleWithCruiser || Lists.battleWithStation || Lists.battleWithGuard) toBattleButton.SetActive(true);

            ////activating switch to journey scene with you win theme
            //else if (Lists.battleWithDestroyer)
            //{
            //    youWinLostTxt.text = Lists.getYouWinTxt();
            //    youWinWithoutCruisersBut.SetActive(true);
            //    Lists.isAfterDestrWin = true;
            //}
            gunSetSound.Play();
        }
    }
}
