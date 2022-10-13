
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
//using UnityEditor.PackageManager;
//using UnityEngine.SceneManagement;
//using System.ComponentModel;
//using System.Collections;
//using System.Runtime.CompilerServices;
//using UnityEditor.Profiling.Memory.Experimental;

public class BattleLaunch : MonoBehaviour
{
    //this materials are to assign as skybox depending on the dimension. The order is 0-black, 1-blue, 2-red
    [SerializeField]
    private List<Material> skyboxes;


    //this one stores the references to holographic materials tomake them fade on on start method cause they are all in state of fade out after the journey scene
    public List<Material> holoMats;

    public GameObject Player4Dstr;
    public GameObject Player3Dstr;
    public GameObject Player2Dstr;
    public GameObject Player2DstrParal;
    public GameObject Player1Dstr;
    public GameObject Player1DstrParal;

    public GameObject Player4Cruis;
    public GameObject Player3Cruis;
    public GameObject Player2Cruis;
    public GameObject Player1Cruis;

    public GameObject Player1Gun;
    public GameObject Player2Gun;
    public GameObject Player3Gun;

    //CPU ships GO
    public GameObject CPU4Dstr;
    public GameObject CPU3Dstr;
    public GameObject CPU2Dstr;
    public GameObject CPU2DstrParal;
    public GameObject CPU1Dstr;
    public GameObject CPU1DstrParal;
    public GameObject CPUGuardDestr;

    public GameObject CPU4Cruis;
    public GameObject CPU3Cruis;
    public GameObject CPU2Cruis;
    public GameObject CPU1Cruis;
    public GameObject CPUGuardCruis;

    public GameObject CPU1Gun;
    public GameObject CPU2Gun;
    public GameObject CPU3Gun;

    //station types to activate on scene in case if station is attacked
    public GameObject stationA;
    //public GameObject stationB;
    public GameObject stationC;
    public GameObject stationD;
    //public GameObject stationBig;
    public GameObject stationFed;
    public GameObject stationGuard;

    //this variable is used to instantiate all player fleet on battlefield and to put player cruisers instances to AllPlayerShipsWithoutGuns static list
    //to prevent a bug of exploision of player guns right after start of battle
    private GameObject playerShipReal;

    //RawImage that used to represent opposite player color
    //[SerializeField]
    //private RawImage OppositeCPUImage;

    //these tags are used to populate AllPlayerShipsWithoutGuns list rigt here on start method. So we will put cruisers to the lists before guns will check if this list
    //count is not 0. Cause if it 0 guns will burst right away
    //private string cruiser4Tag = "Cruis4Play";
    //private string cruiser3Tag = "Cruis3Play";
    //private string cruiser2Tag = "Cruis2Play";
    //private string cruiser1Tag = "Cruis1Play";

    private int Cruis1CPU;
    private int Cruis2CPU;
    private int Cruis3CPU;
    private int Cruis4CPU;
    private int CruisGCPU;
    private int Destr1CPU;
    private int Destr2CPU;
    private int Destr1CPUParal;
    private int Destr2CPUParal;
    private int Destr3CPU;
    private int Destr4CPU;
    private int DestrGCPU;
    private int Gun1CPU;
    private int Gun2CPU;
    private int Gun3CPU;

    private float line1ZPos = 18f;
    private float line2ZPos = 15f;
    private float line3ZPos = 12f;
    private float constYPos = -1.3f;

    private float startXPos = -21;

    private float line1ZPosPlayer = -2f;
    private float line2ZPosPlayer = -5f;
    private float line3ZPosPlayer = -8f;

    //this variables are used to set the CPU ships properly on scene
    int b = 7;
    int x = 3;
    int m = 1; // that one is used only with none maneouvre ships. To place them close to each other

    //images to assign to display player battle field schema
    public Sprite Cruis1Spr;
    public Sprite Cruis2Spr;
    public Sprite Cruis3Spr;
    public Sprite Cruis4Spr;
    public Sprite Destr1Spr;
    public Sprite Destr1ParSpr;
    public Sprite Destr2Spr;
    public Sprite Destr2ParSpr;
    public Sprite Destr3Spr;
    public Sprite Destr4Spr;
    public Sprite Gun1Spr;
    public Sprite Gun2Spr;
    public Sprite Gun3Spr;

    //ship icon buttons
    public GameObject Cruis1Indicator;
    public GameObject Cruis2Indicator;
    public GameObject Cruis3Indicator;
    public GameObject Cruis4Indicator;
    public GameObject Destr1Indicator;
    public GameObject Destr1ParIndicator;
    public GameObject Destr2Indicator;
    public GameObject Destr2ParIndicator;
    public GameObject Destr3Indicator;
    public GameObject Destr4Indicator;
    public GameObject Gun1Indicator;
    public GameObject Gun2Indicator;
    public GameObject Gun3Indicator;

    public Text yourFleetText;
    public Text BattleFieldSchemaText;

    private GameObject chousenShipGO; //variable to store current chosen ship GO and tranfering it to Lists Dictionary

    private List<Vector3> CPUShipsPositions = new List<Vector3>();
    private List<Vector3> PlayerShipsPositions = new List<Vector3>();

    public Sprite notSetButtImg;  //sample image of slot (which is not take yet)
    private Sprite chousenShipImage; //variable to store current chosen ship img
    public GameObject playerFleetGreed;
    public GameObject battleFiealdSchemaGreed;
    public GameObject loadingBoard;
    public Text loadingTxt;

    //this one is used to check the quantity of guns on scene (the limit is no more than 2 guns on scene)
    private int gunsOnField = 0;
    //this ones to control the if at leat one cruiser on battlefield. Used as trigger while switching the scene to battle, incrimented and decrimented
    //while pushing battlefield slots with placing on them cruisers
    private int cruisersOnField = 0;

    private bool setPlayerCruis = false;

    //Player fleet variables
    public Text cruis1ValuePlayer;
    public Text cruis2ValuePlayer;
    public Text cruis3ValuePlayer;
    public Text cruis4ValuePlayer;
    //public Text cruisGValuePlayer;
    public Text destr1ValuePlayer;
    public Text destr2ValuePlayer;
    public Text destr1ParValuePlayer;
    public Text destr2ParValuePlayer;
    public Text destr3ValuePlayer;
    public Text destr4ValuePlayer;
    //public Text destrGValuePlayer;
    public Text gun1ValuePlayer;
    public Text gun2ValuePlayer;
    public Text gun3ValuePlayer;

    //is used to dynamically change the size of Player fleet greed
    private int fleetGreedSizePlayer = 0;
    public RawImage PlayerShipFleetGreed;
    private AudioSource notEnoughSound;
    public AudioSource PutBackShipSound;
    public AudioSource SetShipSound;
    public AudioSource ChoseShipSound;

    //wirning massage panel to warn player tha he/she trys to put more than two guns on battlefield which is restricted
    public GameObject noMoreThanTwoGunsMsg;
    public Text noMoreThanTwoGunsTxt;
    public AudioSource noMoreThanTwoGunsSound;
    public AudioSource schangeSceneSound;

    //these two static bools are used to trigger the end of battle and get the sygnals from the CPU/Player ship/gun controllers throug checking the static Lists class property
    //called all player or CPU ships that currently on scene
    public static bool CPUWin;
    public static bool PlayerWin;

    //setting true the botton of switch the scenes after somone win
    public Text endOfBattleTxt;

    //asteroids pulling properties to launch them on scene
    private List<GameObject> AsteroidList;
    private GameObject AsteroidReal;

    //setting the ships count by pulling the info from static list of ships which on its turn gets the ships from JourneyController 
    //scrips while switching to battle scene i.e. here
    private void setTheShipsCount() {
        this.Cruis1CPU = Lists.Cruis1CPU;
        this.Cruis2CPU = Lists.Cruis2CPU;
        this.Cruis3CPU = Lists.Cruis3CPU;
        this.Cruis4CPU = Lists.Cruis4CPU;
        this.CruisGCPU = Lists.CruisGCPU;
        this.Destr1CPU = Lists.Destr1CPU;
        this.Destr2CPU = Lists.Destr2CPU;
        this.Destr1CPUParal = Lists.Destr1CPUParal;
        this.Destr2CPUParal = Lists.Destr2CPUParal;
        this.Destr3CPU = Lists.Destr3CPU;
        this.Destr4CPU = Lists.Destr4CPU;
        this.DestrGCPU = Lists.DestrGCPU;
        this.Gun1CPU = Lists.Gun1CPU;
        this.Gun2CPU = Lists.Gun2CPU;
        this.Gun3CPU = Lists.Gun3CPU;
    }

    //this method sets the CPU ships with maneouvre features on battle scene, so this ships are set first on battle scene. 
    //on preset Vector3 list there are all positions available to set the ships
    //it jumps to left and right from center to point with gap 2 position points from center and puts ship on landed point and after. And changes x variable
    //to make another jump with even bigger gap
    //13 th and 22 nd point are exclusive, they are so called tranfer point to upper line of ships
    //There is excel file with schema of all ship point coordinates 
    // "b" variable is taket from prevoius cicles of setting CPU ships with maneouvre features
    private void settingManouvreShips() {

        //this if statement is necessary to prevent ships to attack cuse the tiem will be on.
        //switched on time while player is setting its ship on UI is necessary for making animations work and preventing a bug of destroying Guns cause no ships on field
        //and it is repeated with all types of ships and guns (on guns it turns off the CPU Gun controller)
        if (!Lists.playerFleetIsSet) CPU1Cruis.GetComponent<CPUShipsCtrl>().enabled = false;
        else CPU1Cruis.GetComponent<CPUShipsCtrl>().enabled = true;

        if (!Lists.playerFleetIsSet) CPU2Cruis.GetComponent<CPUShipsCtrl>().enabled = false;
        else CPU2Cruis.GetComponent<CPUShipsCtrl>().enabled = true;

        if (!Lists.playerFleetIsSet) CPU1Dstr.GetComponent<CPUShipsCtrl>().enabled = false;
        else CPU1Dstr.GetComponent<CPUShipsCtrl>().enabled = true;

        if (!Lists.playerFleetIsSet) CPU1DstrParal.GetComponent<CPUShipsCtrl>().enabled = false;
        else CPU1DstrParal.GetComponent<CPUShipsCtrl>().enabled = true;

        if (!Lists.playerFleetIsSet) CPU3Cruis.GetComponent<CPUShipsCtrl>().enabled = false;
        else CPU3Cruis.GetComponent<CPUShipsCtrl>().enabled = true;

        if (!Lists.playerFleetIsSet) CPU4Cruis.GetComponent<CPUShipsCtrl>().enabled = false;
        else CPU4Cruis.GetComponent<CPUShipsCtrl>().enabled = true;

        if (!Lists.playerFleetIsSet) CPUGuardCruis.GetComponent<CPUShipsCtrl>().enabled = false;
        else CPUGuardCruis.GetComponent<CPUShipsCtrl>().enabled = true;

        if (!Lists.playerFleetIsSet) CPU2Dstr.GetComponent<CPUShipsCtrl>().enabled = false;
        else CPU2Dstr.GetComponent<CPUShipsCtrl>().enabled = true;

        if (!Lists.playerFleetIsSet) CPU2DstrParal.GetComponent<CPUShipsCtrl>().enabled = false;
        else CPU2DstrParal.GetComponent<CPUShipsCtrl>().enabled = true;

        if(!Lists.playerFleetIsSet) CPUGuardDestr.GetComponent<CPUShipsCtrl>().enabled = false;
        else CPUGuardDestr.GetComponent<CPUShipsCtrl>().enabled = true;

        if (!Lists.playerFleetIsSet) CPU3Dstr.GetComponent<CPUShipsCtrl>().enabled = false;
        else CPU3Dstr.GetComponent<CPUShipsCtrl>().enabled = true;

        if (!Lists.playerFleetIsSet) CPU4Dstr.GetComponent<CPUShipsCtrl>().enabled = false;
        else CPU4Dstr.GetComponent<CPUShipsCtrl>().enabled = true;

        if (!Lists.playerFleetIsSet) CPU2Gun.GetComponent<CPUGunCtrlr>().enabled = false;
        else
        {
            CPU2Gun.GetComponent<CPUGunCtrlr>().enabled = true;
            //enabling the gun shot animation and function only if player fleet is set, to prevent a bug of miss reference cause bullets pulled only if the player fleet is set
            CPU2Gun.transform.GetChild(0).GetComponent<GunPlayer>().enabled = true;
        }

        if (!Lists.playerFleetIsSet) CPU3Gun.GetComponent<CPUGunCtrlr>().enabled = false;
        else
        {
            CPU3Gun.GetComponent<CPUGunCtrlr>().enabled = true;
            //enabling the gun shot animation and function only if player fleet is set, to prevent a bug of miss reference cause bullets pulled only if the player fleet is set
            CPU3Gun.transform.GetChild(0).GetComponent<GunPlayer>().enabled = true;
        }

        if (!Lists.playerFleetIsSet) CPU1Gun.GetComponent<CPUGunCtrlr>().enabled = false;
        else
        {
            CPU1Gun.GetComponent<CPUGunCtrlr>().enabled = true;
            //enabling the gun shot animation and function only if player fleet is set, to prevent a bug of miss reference cause bullets pulled only if the player fleet is set
            CPU1Gun.transform.GetChild(0).GetComponent<GunPlayer>().enabled = true; 
        }


        for (int i = 0; i < Cruis1CPU; i++)
        {

            Instantiate(CPU1Cruis, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
            if (b == 13)
            {
                b = 22;
                x = 3;
                //Instantiate(CPU1Cruis, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
            }
            else if (b == 28)
            {
                b = 37;
                x = 3;
                //Instantiate(CPU1Cruis, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
            }
            else
            {
                m = m == -1 ? 1 : -1;
                //m = (i % 2 != 1 || i == 0) ? -1 : 1; //so 1%2 == 1 and it means the result will be 1 (and it is false for !=1)
                b = b + x * m;
                x += 3;
            }
        }
        for (int i = 0; i < Cruis2CPU; i++)
        {
            Instantiate(CPU2Cruis, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
            if (b == 13)
            {
                b = 22;
                x = 3;
                //Instantiate(CPU1Cruis, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
            }
            else if (b == 28)
            {
                b = 37;
                x = 3;
                //Instantiate(CPU1Cruis, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
            }
            else
            {
                m = m == -1 ? 1 : -1;
                //m = (i % 2 != 1 || i == 0) ? -1 : 1; //so 1%2 == 1 and it means the result will be 1
                b = b + x * m;
                x += 3;
            }
        }

        settingStaticShipsAndGuns();
    }

    //this method sets the non maneouvre CPU ships on battle scene 
    //on preset Vector3 list there are all positions available to set the ships and this code properly sets CPU ships near eachother without gaps
    //it jumps to left and right from center to point with gap 2 position points from center and puts ship first on lsnded point and after to two
    //points near (from left and right). There is excel file with schema of all ship point coordinates 
    // "b" variable is taket from prevoius cicles of setting CPU ships with maneouvre features
    private void settingStaticShipsAndGuns()
    {
        int instTimes = 0; //is used to turn for loot for left and right instantiations in same b position before jumpint to other side of middle point of 
        //ships line
        //TO REPLACE THE GUARD CRUISER to upper method IN CASE IF I WILL DESIDE IT TO BE WITH MANEOUVRE FEATURES !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        for (int i = 0; i < Destr1CPU; i++)
        {
            if (instTimes == 0)
            {
                Instantiate(CPU1Dstr, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (instTimes == 1)
            {
                Instantiate(CPU1Dstr, CPUShipsPositions[b + 1], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (instTimes == 2)
            {
                Instantiate(CPU1Dstr, CPUShipsPositions[b - 1], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (b == 13)
            {
                b = 22;
                x = 3;
                Instantiate(CPU1Dstr, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
            else if (b == 28)
            {
                b = 37;
                x = 3;
                Instantiate(CPU1Dstr, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
            else
            {
                m = m == -1 ? 1 : -1; //switcher of m
                b = b + x * m;
                x += 3;
                Instantiate(CPU1Dstr, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
        }
        for (int i = 0; i < Destr1CPUParal; i++)
        {
            if (instTimes == 0)
            {
                Instantiate(CPU1DstrParal, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (instTimes == 1)
            {
                Instantiate(CPU1DstrParal, CPUShipsPositions[b + 1], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (instTimes == 2)
            {
                Instantiate(CPU1DstrParal, CPUShipsPositions[b - 1], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (b == 13)
            {
                b = 22;
                x = 3;
                Instantiate(CPU1DstrParal, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
            else if (b == 28)
            {
                b = 37;
                x = 3;
                Instantiate(CPU1DstrParal, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
            else
            {
                m = m == -1 ? 1 : -1; //switcher of m
                b = b + x * m;
                x += 3;
                Instantiate(CPU1DstrParal, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
        }

        //it is necessary to add turning off and on CPU Ship Controller script on this loop
        for (int i = 0; i < CruisGCPU; i++)
        {
            if (instTimes == 0)
            {
                Instantiate(CPUGuardCruis, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (instTimes == 1)
            {
                Instantiate(CPUGuardCruis, CPUShipsPositions[b + 1], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (instTimes == 2)
            {
                Instantiate(CPUGuardCruis, CPUShipsPositions[b - 1], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (b == 13)
            {
                b = 22;
                x = 3;
                Instantiate(CPUGuardCruis, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
            else if (b == 28)
            {
                b = 37;
                x = 3;
                Instantiate(CPUGuardCruis, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
            else
            {
                m = m == -1 ? 1 : -1; //switcher of m
                b = b + x * m;
                x += 3;
                Instantiate(CPUGuardCruis, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
        }
        for (int i = 0; i < Cruis3CPU; i++)
        {
            if (instTimes == 0)
            {
                Instantiate(CPU3Cruis, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (instTimes == 1)
            {
                Instantiate(CPU3Cruis, CPUShipsPositions[b + 1], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (instTimes == 2)
            {
                Instantiate(CPU3Cruis, CPUShipsPositions[b - 1], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (b == 13)
            {
                b = 22;
                x = 3;
                Instantiate(CPU3Cruis, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
            else if (b == 28)
            {
                b = 37;
                x = 3;
                Instantiate(CPU3Cruis, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
            else
            {
                m = m == -1 ? 1 : -1; //switcher of m
                b = b + x * m;
                x += 3;
                Instantiate(CPU3Cruis, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
        }
        for (int i = 0; i < Cruis4CPU; i++)
        {
            if (instTimes == 0)
            {
                Instantiate(CPU4Cruis, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (instTimes == 1)
            {
                Instantiate(CPU4Cruis, CPUShipsPositions[b + 1], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (instTimes == 2)
            {
                Instantiate(CPU4Cruis, CPUShipsPositions[b - 1], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (b == 13)
            {
                b = 22;
                x = 3;
                Instantiate(CPU4Cruis, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
            else if (b == 28)
            {
                b = 37;
                x = 3;
                Instantiate(CPU4Cruis, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
            else
            {
                m = m == -1 ? 1 : -1; //switcher of m
                b = b + x * m;
                x += 3;
                Instantiate(CPU4Cruis, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
        }
        for (int i = 0; i < Gun1CPU; i++)
        {
            if (instTimes == 0)
            {
                Instantiate(CPU1Gun, CPUShipsPositions[b], Quaternion.Euler(0, 180, 0));
                instTimes++;
            }
            else if (instTimes == 1)
            {
                Instantiate(CPU1Gun, CPUShipsPositions[b + 1], Quaternion.Euler(0, 180, 0));
                instTimes++;
            }
            else if (instTimes == 2)
            {
                Instantiate(CPU1Gun, CPUShipsPositions[b - 1], Quaternion.Euler(0, 180, 0));
                instTimes++;
            }
            else if (b == 13)
            {
                b = 22;
                x = 3;
                Instantiate(CPU1Gun, CPUShipsPositions[b], Quaternion.Euler(0, 180, 0));
                instTimes = 1;
            }
            else if (b == 28)
            {
                b = 37;
                x = 3;
                Instantiate(CPU1Gun, CPUShipsPositions[b], Quaternion.Euler(0, 180, 0));
                instTimes = 1;
            }
            else
            {
                m = m == -1 ? 1 : -1; //switcher of m
                b = b + x * m;
                x += 3;
                Instantiate(CPU1Gun, CPUShipsPositions[b], Quaternion.Euler(0, 180, 0));
                instTimes = 1;
            }
        }
        for (int i = 0; i < Gun2CPU; i++)
        {
            if (instTimes == 0)
            {
                Instantiate(CPU2Gun, CPUShipsPositions[b], Quaternion.Euler(0, 180, 0));
                instTimes++;
            }
            else if (instTimes == 1)
            {
                Instantiate(CPU2Gun, CPUShipsPositions[b + 1], Quaternion.Euler(0, 180, 0));
                instTimes++;
            }
            else if (instTimes == 2)
            {
                Instantiate(CPU2Gun, CPUShipsPositions[b - 1], Quaternion.Euler(0, 180, 0));
                instTimes++;
            }
            else if (b == 13)
            {
                b = 22;
                x = 3;
                Instantiate(CPU2Gun, CPUShipsPositions[b], Quaternion.Euler(0, 180, 0));
                instTimes = 1;
            }
            else if (b == 28)
            {
                b = 37;
                x = 3;
                Instantiate(CPU2Gun, CPUShipsPositions[b], Quaternion.Euler(0, 180, 0));
                instTimes = 1;
            }
            else
            {
                m = m == -1 ? 1 : -1; //switcher of m
                b = b + x * m;
                x += 3;
                Instantiate(CPU2Gun, CPUShipsPositions[b], Quaternion.Euler(0, 180, 0));
                instTimes = 1;
            }
        }
        for (int i = 0; i < Gun3CPU; i++)
        {
            if (instTimes == 0)
            {
                Instantiate(CPU3Gun, CPUShipsPositions[b], Quaternion.Euler(0, 180, 0));
                instTimes++;
            }
            else if (instTimes == 1)
            {
                Instantiate(CPU3Gun, CPUShipsPositions[b + 1], Quaternion.Euler(0, 180, 0));
                instTimes++;
            }
            else if (instTimes == 2)
            {
                Instantiate(CPU3Gun, CPUShipsPositions[b - 1], Quaternion.Euler(0, 180, 0));
                instTimes++;
            }
            else if (b == 13)
            {
                b = 22;
                x = 3;
                Instantiate(CPU3Gun, CPUShipsPositions[b], Quaternion.Euler(0, 180, 0));
                instTimes = 1;
            }
            else if (b == 28)
            {
                b = 37;
                x = 3;
                Instantiate(CPU3Gun, CPUShipsPositions[b], Quaternion.Euler(0, 180, 0));
                instTimes = 1;
            }
            else
            {
                m = m == -1 ? 1 : -1; //switcher of m
                b = b + x * m;
                x += 3;
                Instantiate(CPU3Gun, CPUShipsPositions[b], Quaternion.Euler(0, 180, 0));
                instTimes = 1;
            }
        }
        for (int i = 0; i < Destr2CPU; i++)
        {
            if (instTimes == 0)
            {
                Instantiate(CPU2Dstr, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (instTimes == 1)
            {
                Instantiate(CPU2Dstr, CPUShipsPositions[b + 1], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (instTimes == 2)
            {
                Instantiate(CPU2Dstr, CPUShipsPositions[b - 1], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (b == 13)
            {
                b = 22;
                x = 3;
                Instantiate(CPU2Dstr, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
            else if (b == 28)
            {
                b = 37;
                x = 3;
                Instantiate(CPU2Dstr, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
            else
            {
                m = m == -1 ? 1 : -1; //switcher of m
                b = b + x * m;
                x += 3;
                Instantiate(CPU2Dstr, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
        }
        for (int i = 0; i < Destr2CPUParal; i++)
        {
            if (instTimes == 0)
            {
                Instantiate(CPU2DstrParal, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (instTimes == 1)
            {
                Instantiate(CPU2DstrParal, CPUShipsPositions[b + 1], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (instTimes == 2)
            {
                Instantiate(CPU2DstrParal, CPUShipsPositions[b - 1], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (b == 13)
            {
                b = 22;
                x = 3;
                Instantiate(CPU2DstrParal, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
            else if (b == 28)
            {
                b = 37;
                x = 3;
                Instantiate(CPU2DstrParal, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
            else
            {
                m = m == -1 ? 1 : -1; //switcher of m
                b = b + x * m;
                x += 3;
                Instantiate(CPU2DstrParal, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
        }
        for (int i = 0; i < Destr3CPU; i++)
        {
            if (instTimes == 0)
            {
                Instantiate(CPU3Dstr, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (instTimes == 1)
            {
                Instantiate(CPU3Dstr, CPUShipsPositions[b + 1], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (instTimes == 2)
            {
                Instantiate(CPU3Dstr, CPUShipsPositions[b - 1], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (b == 13)
            {
                b = 22;
                x = 3;
                Instantiate(CPU3Dstr, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
            else if (b == 28)
            {
                b = 37;
                x = 3;
                Instantiate(CPU3Dstr, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
            else
            {
                m = m == -1 ? 1 : -1; //switcher of m
                b = b + x * m;
                x += 3;
                Instantiate(CPU3Dstr, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
        }
        for (int i = 0; i < Destr4CPU; i++)
        {
            if (instTimes == 0)
            {
                Instantiate(CPU4Dstr, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (instTimes == 1)
            {
                Instantiate(CPU4Dstr, CPUShipsPositions[b + 1], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (instTimes == 2)
            {
                Instantiate(CPU4Dstr, CPUShipsPositions[b - 1], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (b == 13)
            {
                b = 22;
                x = 3;
                Instantiate(CPU4Dstr, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
            else if (b == 28)
            {
                b = 37;
                x = 3;
                Instantiate(CPU4Dstr, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
            else
            {
                m = m == -1 ? 1 : -1; //switcher of m
                b = b + x * m;
                x += 3;
                Instantiate(CPU4Dstr, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
        }

        //it is necessary to add turning off and on CPU Ship Controller script on this loop
        for (int i = 0; i < DestrGCPU; i++)
        {
            if (instTimes == 0)
            {
                Instantiate(CPUGuardDestr, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (instTimes == 1)
            {
                Instantiate(CPUGuardDestr, CPUShipsPositions[b + 1], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (instTimes == 2)
            {
                Instantiate(CPUGuardDestr, CPUShipsPositions[b - 1], Quaternion.Euler(0, -90, 0));
                instTimes++;
            }
            else if (b == 13)
            {
                b = 22;
                x = 3;
                Instantiate(CPUGuardDestr, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
            else if (b == 28)
            {
                b = 37;
                x = 3;
                Instantiate(CPUGuardDestr, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
            else
            {
                m = m == -1 ? 1 : -1; //switcher of m
                b = b + x * m;
                x += 3;
                Instantiate(CPUGuardDestr, CPUShipsPositions[b], Quaternion.Euler(0, -90, 0));
                instTimes = 1;
            }
        }
    }

    //storing star values of player fleet to local private cariable to assign them back after battle is launched to avoid the bug of double reduing the player ship count
    //called on start
    //private void storePlayerFleetStartValues()
    //{
    //    Cruis1Player = Lists.Cruis1OfPlayer;
    //    Cruis2Player = Lists.Cruis2OfPlayer;
    //    Cruis3Player = Lists.Cruis3OfPlayer;
    //    Cruis4Player = Lists.Cruis4OfPlayer;
    //    Destr1Player = Lists.Destr1OfPlayer;
    //    Destr2Player = Lists.Destr2OfPlayer;
    //    Destr1PlayerParal = Lists.Destr1OfPlayerPar;
    //    Destr2PlayerParal = Lists.Destr2OfPlayerPar;
    //    Destr3Player = Lists.Destr3OfPlayer;
    //    Destr4Player = Lists.Destr4OfPlayer;
    //    Gun1Player = Lists.Gun1OfPlayer;
    //    Gun2Player = Lists.Gun2OfPlayer;
    //    Gun3Player = Lists.Gun3OfPlayer;
    //}


    // Start is called before the first frame update
    void Start()
    {
        //sets the colour of camera background according to current dimension, as well the background camera
        if (Lists.isBlackDimension)
        {
            RenderSettings.skybox = skyboxes[0];
        }
        if (Lists.isBlueDimension)
        {
            RenderSettings.skybox = skyboxes[1];
        }
        if (Lists.isRedDimension)
        {
            RenderSettings.skybox = skyboxes[2];
        }

        //assignin a proper color to UI token of opposite CPU player that is under attack 
        //OppositeCPUImage.color = Lists.colorOfOpposite;

        //if (!Lists.playerFleetIsSet) DontDestroyOnLoad(schangeSceneSound);
        CPUWin = false;
        PlayerWin = false;
        Lists.isAfterBattleWin = false;
        Lists.isAfterBattleLost = false;

        //setting UI constants according chosen language
        yourFleetText.text = Constants.Instance.getYourFleet();
        BattleFieldSchemaText.text = Constants.Instance.getBattleFieldScheme();

        notEnoughSound = GetComponent<AudioSource>();

        //populating the list of vetor3 points to place CPU ships
        for (int i = 0; i < 15; i++) {
            CPUShipsPositions.Add(new Vector3(startXPos, constYPos, line1ZPos));
            startXPos += 3; //3 cause the step is 3 
        }

        startXPos = -21;
        for (int i = 0; i < 15; i++)
        {
            CPUShipsPositions.Add(new Vector3(startXPos, constYPos, line2ZPos));
            startXPos += 3;
        }

        startXPos = -21;
        for (int i = 0; i < 15; i++)
        {
            CPUShipsPositions.Add(new Vector3(startXPos, constYPos, line3ZPos));
            startXPos += 3;
        }

        //populating the list of vetor3 points to place Player ships

        startXPos = -21;
        for (int i = 0; i < 15; i++)
        {
            PlayerShipsPositions.Add(new Vector3(startXPos, constYPos, line1ZPosPlayer));
            startXPos += 3;
        }

        startXPos = -21;
        for (int i = 0; i < 15; i++)
        {
            PlayerShipsPositions.Add(new Vector3(startXPos, constYPos, line2ZPosPlayer));
            startXPos += 3;
        }

        startXPos = -21;
        for (int i = 0; i < 15; i++)
        {
            PlayerShipsPositions.Add(new Vector3(startXPos, constYPos, line3ZPosPlayer));
            startXPos += 3;
        }

        //setting the camera field of view to tablets by using references of screen width and height
        if ((double)Screen.width / Screen.height < 1.5) Camera.main.fieldOfView = 85; //for very narrow screen tablets
        else if ((double)Screen.width / Screen.height > 1.5f && (double)Screen.width / Screen.height < 1.6f) Camera.main.fieldOfView = 80; //for wider screen tablets tab A10
        else if ((double)Screen.width / Screen.height == 1.6f) Camera.main.fieldOfView = 75;

        //setting the count of ships from journey scene CPU Ship panel information, through Lists
        setTheShipsCount();

        //setting CPU Ships on battle scene after getting the count of ships from journey scene through the Lists
        settingManouvreShips();

        if (Lists.playerFleetIsSet)
        {
            //launching the randome quantity of asteroids amidst two armies 
            launchingTheAsteroids(Random.Range(10, 20));

            Time.timeScale = 0; // *************************************************************
            foreach (string key in Lists.playerShipsSchema.Keys)
            {
                playerShipReal = Instantiate(Lists.playerShipsSchema[key], PlayerShipsPositions[int.Parse(key)], Quaternion.Euler(0, Lists.playerShipsSchema[key].name.Contains("Gun") ? 0 : 90, 0));

                //adding the cruisers of player to  AllPlayerShipsWithoutGuns static list right here to prevent a bug of destroying the guns cause they check if this 
                //list count is not 0
                if (playerShipReal.layer == 9 && playerShipReal.name.Contains("Cruis"))
                {
                    if (!Lists.AllPlayerShipsWithoutGuns.Contains(playerShipReal)) Lists.AllPlayerShipsWithoutGuns.Add(playerShipReal);
                }
            }

            //so if stationTypeLists == 0 there will not be instantiated any station on scene
            if (Lists.stationTypeLists == 1) stationA.SetActive(true);
            //else if (Lists.stationTypeLists == 2) stationB.SetActive(true);
            else if (Lists.stationTypeLists == 3) stationC.SetActive(true);
            else if (Lists.stationTypeLists == 4) stationD.SetActive(true);
            //else if(Lists.stationTypeLists == 5) stationBig.SetActive(true);
            else if (Lists.stationTypeLists == 6) stationFed.SetActive(true);
            else if (Lists.stationTypeLists != 0) stationGuard.SetActive(true);

            Lists.playerFleetIsSet = false;
        }
        else
        {
            //copy the data about player start fleet to use it later on ObjectPuller class to instantiating and pulling only the necessary objects to scene from puller
            //if not that line all the data about player start fleet will be deleted from static list while putting them on battlefield scheme in method lower
            //this information is cleared to use in next battle while switching to other scene from here
            Lists.CopyFleetOfPlayerForPull();

            //setting on the fade value of hologram material to make them visible on scene render

            foreach (Material mat in holoMats)
            {
                mat.SetFloat("_Fade", 1f);
            }

            //storing happens here cause this cone works on start
            //storePlayerFleetStartValues();

            Lists.playerShipsSchema.Clear();

            //clear static ints that count the player cruisers on battle scene when start, and set static bool false which controls that only one cruiser is set as player's
            Lists.cruiser1PlayerPutOnScene = 0;
            Lists.cruiser2PlayerPutOnScene = 0;
            Lists.cruiser3PlayerPutOnScene = 0;
            Lists.cruiser4PlayerPutOnScene = 0;
            Lists.playerCruiserSet = false;

            playerFleetGreed.SetActive(true);

            Time.timeScale = 1; //***********************************************************************
            //following conditions check if ship count is not zero and tunrs its icon on and off  on fleet scroll 
            //cruisers set
            if (Lists.Cruis1OfPlayer > 0)
            {
                cruis1ValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);
                cruis1ValuePlayer.text = Lists.Cruis1OfPlayer.ToString();
                fleetGreedSizePlayer++; //increases the size of Player fleet greed to be proportional to count of ship type counts
            }
            else cruis1ValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);

            if (Lists.Cruis2OfPlayer > 0)
            {
                cruis2ValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);
                cruis2ValuePlayer.text = Lists.Cruis2OfPlayer.ToString();
                fleetGreedSizePlayer++; //increases the size of Player fleet greed to be proportional to count of ship type counts
            }
            else cruis2ValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);

            if (Lists.Cruis3OfPlayer > 0)
            {
                cruis3ValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);
                cruis3ValuePlayer.text = Lists.Cruis3OfPlayer.ToString();
                fleetGreedSizePlayer++; //increases the size of Player fleet greed to be proportional to count of ship type counts
            }
            else cruis3ValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);

            if (Lists.Cruis4OfPlayer > 0)
            {
                cruis4ValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);
                cruis4ValuePlayer.text = Lists.Cruis4OfPlayer.ToString();
                fleetGreedSizePlayer++; //increases the size of Player fleet greed to be proportional to count of ship type counts
            }
            else cruis4ValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);

            //Destroyers set
            if (Lists.Destr1OfPlayer > 0)
            {
                destr1ValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);
                destr1ValuePlayer.text = Lists.Destr1OfPlayer.ToString();
                fleetGreedSizePlayer++; //increases the size of Player fleet greed to be proportional to count of ship type counts
            }
            else destr1ValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
            if (Lists.Destr1OfPlayerPar > 0)
            {
                destr1ParValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);
                destr1ParValuePlayer.text = Lists.Destr1OfPlayerPar.ToString();
                fleetGreedSizePlayer++; //increases the size of Player fleet greed to be proportional to count of ship type counts
            }
            else destr1ParValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
            if (Lists.Destr2OfPlayer > 0)
            {
                destr2ValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);
                destr2ValuePlayer.text = Lists.Destr2OfPlayer.ToString();
                fleetGreedSizePlayer++; //increases the size of Player fleet greed to be proportional to count of ship type counts
            }
            else destr2ValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
            if (Lists.Destr2OfPlayerPar > 0)
            {
                destr2ParValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);
                destr2ParValuePlayer.text = Lists.Destr2OfPlayerPar.ToString();
                fleetGreedSizePlayer++; //increases the size of Player fleet greed to be proportional to count of ship type counts
            }
            else destr2ParValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
            if (Lists.Destr3OfPlayer > 0)
            {
                destr3ValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);
                destr3ValuePlayer.text = Lists.Destr3OfPlayer.ToString();
                fleetGreedSizePlayer++; //increases the size of Player fleet greed to be proportional to count of ship type counts
            }
            else destr3ValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
            if (Lists.Destr4OfPlayer > 0)
            {
                destr4ValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);
                destr4ValuePlayer.text = Lists.Destr4OfPlayer.ToString();
                fleetGreedSizePlayer++; //increases the size of Player fleet greed to be proportional to count of ship type counts
            }
            else destr4ValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);

            //guns set
            if (Lists.Gun1OfPlayer > 0)
            {
                gun1ValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);
                gun1ValuePlayer.text = Lists.Gun1OfPlayer.ToString();
                fleetGreedSizePlayer++; //increases the size of Player fleet greed to be proportional to count of ship type counts
            }
            else gun1ValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
            if (Lists.Gun2OfPlayer > 0)
            {
                gun2ValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);
                gun2ValuePlayer.text = Lists.Gun2OfPlayer.ToString();
                fleetGreedSizePlayer++; //increases the size of Player fleet greed to be proportional to count of ship type counts
            }
            else gun2ValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
            if (Lists.Gun3OfPlayer > 0)
            {
                gun3ValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);
                gun3ValuePlayer.text = Lists.Gun3OfPlayer.ToString();
                fleetGreedSizePlayer++; //increases the size of Player fleet greed to be proportional to count of ship type counts
            }
            else gun3ValuePlayer.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);

            //increase or decreace the width of greed dynamically depending on count of ship types
            PlayerShipFleetGreed.GetComponent<RectTransform>().sizeDelta = new Vector2(fleetGreedSizePlayer * 180, 100f);

            battleFiealdSchemaGreed.SetActive(true);



        }
    }

    // the method to launching the asteroids on scene only after the pleyer set it's fleet (as paremeter it takes the count of asteroids to launche on scene)
    private void launchingTheAsteroids(int asteroidsCount)
    {
        for (int i = 0; i < asteroidsCount; i++)
        {
            int index = Random.Range(1, 5);
            if (index == 1) AsteroidList = ObjectPuller.current.GetAsteroids1Pull();
            else if (index == 2) AsteroidList = ObjectPuller.current.GetAsteroids2Pull();
            else if (index == 3) AsteroidList = ObjectPuller.current.GetAsteroids3Pull();
            else AsteroidList = ObjectPuller.current.GetAsteroids4Pull();
            AsteroidReal = ObjectPuller.current.GetUniversalBullet(AsteroidList);
            //random area amidst two armies
            AsteroidReal.transform.position = new Vector3 (Random.Range (-40f, 40f), -1.3f, Random.Range (2f,8f));
            AsteroidReal.transform.rotation = Random.rotation;
            AsteroidReal.SetActive(true);
        }
    }

    private void noMoreTwoGuns()
    {
        noMoreThanTwoGunsMsg.SetActive(true);
        noMoreThanTwoGunsTxt.text = Constants.Instance.getTwoGunWarning();
        noMoreThanTwoGunsSound.Play();
        noMoreThanTwoGunsMsg.GetComponent<Animator>().SetBool("dissolve", true);
    }

    public void battleLauncheButton()
    {
        schangeSceneSound.Play();
        StartCoroutine(battelLaunchWithLag());
    }

    IEnumerator battelLaunchWithLag() {
        yield return new WaitForSeconds(0.9f);
        if (Lists.playerShipsSchema.Count > 0 && cruisersOnField > 0)
        {
            //loadingTxt.text = Lists.getLoadingText();
            //loadingBoard.SetActive(true);
            Lists.playerFleetIsSet = true;
            playerFleetGreed.SetActive(false);
            battleFiealdSchemaGreed.SetActive(false);

            //clearing all interstitial support Lists and Dictionaries which are used to set players fleet with special UI for that (the code is lower)
            Lists.takenButtons.Clear();
            Lists.takenButtonsDic.Clear();
            Lists.notTakenButtons.Clear();
            Lists.notTakenButtonsDic.Clear();
            Lists.takenButtonsCruisNearCruisStay.Clear();
            Lists.takenButtonsCruisChousen.Clear();
            Lists.takenButtonsSecondCruis.Clear();

            //setting the player's fleet count to an start amount 
            //    Lists.setShipsForPlayer(Cruis1Player, Cruis2Player, Cruis3Player, Cruis4Player, Gun1Player, Gun2Player, Gun3Player, Destr1Player, Destr1PlayerParal, Destr2Player,
            //Destr2PlayerParal, Destr3Player, Destr4Player);

            SceneSwitchMngr.LoadBattleScene();
            //SceneManager.LoadScene(0);
        }
        else if ((Lists.playerShipsSchema.Count > 0 && cruisersOnField < 1) || Lists.playerShipsSchema.Count < 1)
        {
            noMoreThanTwoGunsTxt.text = Constants.Instance.getNoCruiserWarning();
            noMoreThanTwoGunsMsg.SetActive(true);
            noMoreThanTwoGunsSound.Play();
            noMoreThanTwoGunsMsg.GetComponent<Animator>().SetBool("dissolve", true);
        }
    }

    public void BattleFieldButtonSet(Button btn) {
        if (btn.transform.gameObject.GetComponent<Image>().sprite == notSetButtImg)
        {
            if (chousenShipImage)
            {
                //so first condition is for checking if any ship is chousen by player to place on  battlefield
                //second one is for checking if current ship quantity is not zero and player can put it on battlefield
                if (Lists.currentChousenShipIndicator == Cruis1Indicator && Lists.Cruis1OfPlayer > 0)
                {
                    //this four lines are used to disactivate left and right button slots near moving cruisers (cause on that points cruiser will move left and right)
                    //and this near slots are added to special static List to use when pushing Ship buttons other than moving ones/
                    //moving ships are Cruis1 and Cruis2, and this four lines are for them only
                    //this code ignores that moving cruisers may stay at borders of battlefields cause border slots are disactivated on pushing buttons of ships of this type
                    Lists.notTakenButtonsDic[(int.Parse(btn.name) - 1).ToString()].interactable = false;
                    Lists.takenButtonsCruisNearCruisStay.Add(Lists.notTakenButtonsDic[(int.Parse(btn.name) - 1).ToString()]);
                    Lists.notTakenButtonsDic[(int.Parse(btn.name) + 1).ToString()].interactable = false;
                    Lists.takenButtonsCruisNearCruisStay.Add(Lists.notTakenButtonsDic[(int.Parse(btn.name) + 1).ToString()]);

                    //following if statements are used to disactivate slots that stay one slot far from moving type cruisers and are used to prevent from putting 
                    //two moving type cruisers on slots that may intersect while they are moving toward each other. So there should be at least two epty slots btw two 
                    //moving type cruisers
                    if (int.Parse(btn.name) != 1 && !btn.name.Contains("16") && !btn.name.Contains("31")
                            && !btn.name.Contains("13") && !btn.name.Contains("28") && !btn.name.Contains("43"))
                    {
                        if (Lists.notTakenButtonsDic.ContainsKey((int.Parse(btn.name) + 2).ToString()))
                        {
                            Lists.notTakenButtonsDic[(int.Parse(btn.name) + 2).ToString()].interactable = false;
                            Lists.takenButtonsSecondCruis.Add(Lists.notTakenButtonsDic[(int.Parse(btn.name) + 2).ToString()]);
                        }
                        if (Lists.notTakenButtonsDic.ContainsKey((int.Parse(btn.name) - 2).ToString()))
                        {
                            Lists.notTakenButtonsDic[(int.Parse(btn.name) - 2).ToString()].interactable = false;
                            Lists.takenButtonsSecondCruis.Add(Lists.notTakenButtonsDic[(int.Parse(btn.name) - 2).ToString()]);
                        }
                    }
                    //these to if statements takes into account that moving type cruisers may stay to left and right borders of battlefields and takes into account
                    //only left and right slots that stay one slote far from moving type cruiser
                    else if (int.Parse(btn.name) == 1 || btn.name.Contains("16") || btn.name.Contains("31"))
                    {
                        if (Lists.notTakenButtonsDic.ContainsKey((int.Parse(btn.name) + 2).ToString()))
                        {
                            Lists.notTakenButtonsDic[(int.Parse(btn.name) + 2).ToString()].interactable = false;
                            Lists.takenButtonsSecondCruis.Add(Lists.notTakenButtonsDic[(int.Parse(btn.name) + 2).ToString()]);
                        }
                    }
                    else if (btn.name.Contains("13") || btn.name.Contains("28") || btn.name.Contains("43"))
                    {
                        if (Lists.notTakenButtonsDic.ContainsKey((int.Parse(btn.name) - 2).ToString()))
                        {
                            Lists.notTakenButtonsDic[(int.Parse(btn.name) - 2).ToString()].interactable = false;
                            Lists.takenButtonsSecondCruis.Add(Lists.notTakenButtonsDic[(int.Parse(btn.name) - 2).ToString()]);
                        }
                    }
                    //moving current pushed button from one list to another (from not taken ones to taken ones)
                    Lists.takenButtons.Add(btn);
                    Lists.notTakenButtons.Remove(btn);
                    Lists.takenButtonsDic.Add(btn.name,btn);
                    Lists.notTakenButtonsDic.Remove(btn.name);
                    btn.transform.gameObject.GetComponent<Image>().sprite = chousenShipImage; //change the sprite of current button to chousen ship picture

                    //addding ship name and GO accirding to it to special dictionary that will used later while setting this GO on scene, name is used to get proper ship
                    //with its key
                    Lists.playerShipsSchema.Add(btn.name, chousenShipGO); 
                    Lists.Cruis1OfPlayer--; //reducing the quantity of ship that has player
                    cruisersOnField++;
                    //this one is necessary for assigning randome cruiser ander player control arfter start the battle scene
                    //so if this value more than one it will be taken into account on PlayerShipCPU while starting the class
                    Lists.cruiser1PlayerPutOnScene++;
                    cruis1ValuePlayer.text = Lists.Cruis1OfPlayer.ToString(); //updating UI that display current ship quantity on players fleet greed 
                    SetShipSound.Play();
                }

                //if chosen ship quantity is zero system will give according sound to give a sygnal of that 
                else if (Lists.currentChousenShipIndicator == Cruis1Indicator && Lists.Cruis1OfPlayer == 0) notEnoughSound.Play();

                if (Lists.currentChousenShipIndicator == Cruis2Indicator && Lists.Cruis2OfPlayer > 0)
                {
                    Lists.notTakenButtonsDic[(int.Parse(btn.name) - 1).ToString()].interactable = false;
                    Lists.takenButtonsCruisNearCruisStay.Add(Lists.notTakenButtonsDic[(int.Parse(btn.name) - 1).ToString()]);
                    Lists.notTakenButtonsDic[(int.Parse(btn.name) + 1).ToString()].interactable = false;
                    Lists.takenButtonsCruisNearCruisStay.Add(Lists.notTakenButtonsDic[(int.Parse(btn.name) + 1).ToString()]);

                    if (int.Parse(btn.name) != 1 && !btn.name.Contains("16") && !btn.name.Contains("31")
                            && !btn.name.Contains("13") && !btn.name.Contains("28") && !btn.name.Contains("43"))
                    {
                        if (Lists.notTakenButtonsDic.ContainsKey((int.Parse(btn.name) + 2).ToString()))
                        {
                            Lists.notTakenButtonsDic[(int.Parse(btn.name) + 2).ToString()].interactable = false;
                            Lists.takenButtonsSecondCruis.Add(Lists.notTakenButtonsDic[(int.Parse(btn.name) + 2).ToString()]);
                        }
                        if (Lists.notTakenButtonsDic.ContainsKey((int.Parse(btn.name) - 2).ToString()))
                        {
                            Lists.notTakenButtonsDic[(int.Parse(btn.name) - 2).ToString()].interactable = false;
                            Lists.takenButtonsSecondCruis.Add(Lists.notTakenButtonsDic[(int.Parse(btn.name) - 2).ToString()]);
                        }
                    }
                    else if (int.Parse(btn.name) == 1 || btn.name.Contains("16") || btn.name.Contains("31"))
                    {
                        if (Lists.notTakenButtonsDic.ContainsKey((int.Parse(btn.name) + 2).ToString()))
                        {
                            Lists.notTakenButtonsDic[(int.Parse(btn.name) + 2).ToString()].interactable = false;
                            Lists.takenButtonsSecondCruis.Add(Lists.notTakenButtonsDic[(int.Parse(btn.name) + 2).ToString()]);
                        }
                    }
                    else if (btn.name.Contains("13") || btn.name.Contains("28") || btn.name.Contains("43"))
                    {
                        if (Lists.notTakenButtonsDic.ContainsKey((int.Parse(btn.name) - 2).ToString()))
                        {
                            Lists.notTakenButtonsDic[(int.Parse(btn.name) - 2).ToString()].interactable = false;
                            Lists.takenButtonsSecondCruis.Add(Lists.notTakenButtonsDic[(int.Parse(btn.name) - 2).ToString()]);
                        }
                    }

                    Lists.takenButtons.Add(btn);
                    Lists.notTakenButtons.Remove(btn);
                    Lists.takenButtonsDic.Add(btn.name, btn);
                    Lists.notTakenButtonsDic.Remove(btn.name);
                    btn.transform.gameObject.GetComponent<Image>().sprite = chousenShipImage;
                    Lists.playerShipsSchema.Add(btn.name, chousenShipGO);
                    Lists.Cruis2OfPlayer--;
                    cruisersOnField++;
                    //this one is necessary for assigning randome cruiser ander player control arfter start the battle scene
                    //so if this value more than one it will be taken into account on PlayerShipCPU while starting the class
                    Lists.cruiser2PlayerPutOnScene++;
                    cruis2ValuePlayer.text = Lists.Cruis2OfPlayer.ToString();
                    SetShipSound.Play();
                }
                else if (Lists.currentChousenShipIndicator == Cruis2Indicator && Lists.Cruis2OfPlayer == 0) notEnoughSound.Play();

                if (Lists.currentChousenShipIndicator == Cruis3Indicator && Lists.Cruis3OfPlayer > 0)
                {
                    Lists.takenButtons.Add(btn);
                    Lists.notTakenButtons.Remove(btn);
                    Lists.takenButtonsDic.Add(btn.name, btn);
                    Lists.notTakenButtonsDic.Remove(btn.name);
                    btn.transform.gameObject.GetComponent<Image>().sprite = chousenShipImage;
                    Lists.playerShipsSchema.Add(btn.name, chousenShipGO);
                    Lists.Cruis3OfPlayer--;
                    cruisersOnField++;
                    //this one is necessary for assigning randome cruiser ander player control arfter start the battle scene
                    //so if this value more than one it will be taken into account on PlayerShipCPU while starting the class
                    Lists.cruiser3PlayerPutOnScene++;
                    cruis3ValuePlayer.text = Lists.Cruis3OfPlayer.ToString();
                    SetShipSound.Play();
                }
                else if (Lists.currentChousenShipIndicator == Cruis3Indicator && Lists.Cruis3OfPlayer == 0) notEnoughSound.Play();

                if (Lists.currentChousenShipIndicator == Cruis4Indicator && Lists.Cruis4OfPlayer > 0)
                {
                    Lists.takenButtons.Add(btn);
                    Lists.notTakenButtons.Remove(btn);
                    Lists.takenButtonsDic.Add(btn.name, btn);
                    Lists.notTakenButtonsDic.Remove(btn.name);
                    btn.transform.gameObject.GetComponent<Image>().sprite = chousenShipImage;
                    Lists.playerShipsSchema.Add(btn.name, chousenShipGO);
                    Lists.Cruis4OfPlayer--;
                    cruisersOnField++;
                    //this one is necessary for assigning randome cruiser ander player control arfter start the battle scene
                    //so if this value more than one it will be taken into account on PlayerShipCPU while starting the class
                    Lists.cruiser4PlayerPutOnScene++;
                    cruis4ValuePlayer.text = Lists.Cruis4OfPlayer.ToString();
                    SetShipSound.Play();
                }
                else if (Lists.currentChousenShipIndicator == Cruis4Indicator && Lists.Cruis4OfPlayer == 0) notEnoughSound.Play();

                if (Lists.currentChousenShipIndicator == Destr4Indicator && Lists.Destr4OfPlayer > 0)
                {
                    Lists.takenButtons.Add(btn);
                    Lists.notTakenButtons.Remove(btn);
                    Lists.takenButtonsDic.Add(btn.name, btn);
                    Lists.notTakenButtonsDic.Remove(btn.name);
                    btn.transform.gameObject.GetComponent<Image>().sprite = chousenShipImage;
                    Lists.playerShipsSchema.Add(btn.name, chousenShipGO);
                    Lists.Destr4OfPlayer--;
                    destr4ValuePlayer.text = Lists.Destr4OfPlayer.ToString();
                    SetShipSound.Play();
                }
                else if (Lists.currentChousenShipIndicator == Destr4Indicator && Lists.Destr4OfPlayer == 0) notEnoughSound.Play();

                if (Lists.currentChousenShipIndicator == Destr3Indicator && Lists.Destr3OfPlayer > 0)
                {
                    Lists.takenButtons.Add(btn);
                    Lists.notTakenButtons.Remove(btn);
                    Lists.takenButtonsDic.Add(btn.name, btn);
                    Lists.notTakenButtonsDic.Remove(btn.name);
                    btn.transform.gameObject.GetComponent<Image>().sprite = chousenShipImage;
                    Lists.playerShipsSchema.Add(btn.name, chousenShipGO);
                    Lists.Destr3OfPlayer--;
                    destr3ValuePlayer.text = Lists.Destr3OfPlayer.ToString();
                    SetShipSound.Play();
                }
                else if (Lists.currentChousenShipIndicator == Destr3Indicator && Lists.Destr3OfPlayer == 0) notEnoughSound.Play();

                if (Lists.currentChousenShipIndicator == Destr2Indicator && Lists.Destr2OfPlayer > 0)
                {
                    Lists.takenButtons.Add(btn);
                    Lists.notTakenButtons.Remove(btn);
                    Lists.takenButtonsDic.Add(btn.name, btn);
                    Lists.notTakenButtonsDic.Remove(btn.name);
                    btn.transform.gameObject.GetComponent<Image>().sprite = chousenShipImage;
                    Lists.playerShipsSchema.Add(btn.name, chousenShipGO);
                    Lists.Destr2OfPlayer--;
                    destr2ValuePlayer.text = Lists.Destr2OfPlayer.ToString();
                    SetShipSound.Play();
                }
                else if (Lists.currentChousenShipIndicator == Destr2Indicator && Lists.Destr2OfPlayer == 0) notEnoughSound.Play();

                if (Lists.currentChousenShipIndicator == Destr1Indicator && Lists.Destr1OfPlayer > 0)
                {
                    Lists.takenButtons.Add(btn);
                    Lists.notTakenButtons.Remove(btn);
                    Lists.takenButtonsDic.Add(btn.name, btn);
                    Lists.notTakenButtonsDic.Remove(btn.name);
                    btn.transform.gameObject.GetComponent<Image>().sprite = chousenShipImage;
                    Lists.playerShipsSchema.Add(btn.name, chousenShipGO);
                    Lists.Destr1OfPlayer--;
                    destr1ValuePlayer.text = Lists.Destr1OfPlayer.ToString();
                    SetShipSound.Play();
                }
                else if (Lists.currentChousenShipIndicator == Destr1Indicator && Lists.Destr1OfPlayer == 0) notEnoughSound.Play();

                if (Lists.currentChousenShipIndicator == Destr1ParIndicator && Lists.Destr1OfPlayerPar > 0)
                {
                    Lists.takenButtons.Add(btn);
                    Lists.notTakenButtons.Remove(btn);
                    Lists.takenButtonsDic.Add(btn.name, btn);
                    Lists.notTakenButtonsDic.Remove(btn.name);
                    btn.transform.gameObject.GetComponent<Image>().sprite = chousenShipImage;
                    Lists.playerShipsSchema.Add(btn.name, chousenShipGO);
                    Lists.Destr1OfPlayerPar--;
                    destr1ParValuePlayer.text = Lists.Destr1OfPlayerPar.ToString();
                    SetShipSound.Play();
                }
                else if (Lists.currentChousenShipIndicator == Destr1ParIndicator && Lists.Destr1OfPlayerPar == 0) notEnoughSound.Play();

                if (Lists.currentChousenShipIndicator == Destr2ParIndicator && Lists.Destr2OfPlayerPar > 0)
                {
                    Lists.takenButtons.Add(btn);
                    Lists.notTakenButtons.Remove(btn);
                    Lists.takenButtonsDic.Add(btn.name, btn);
                    Lists.notTakenButtonsDic.Remove(btn.name);
                    btn.transform.gameObject.GetComponent<Image>().sprite = chousenShipImage;
                    Lists.playerShipsSchema.Add(btn.name, chousenShipGO);
                    Lists.Destr2OfPlayerPar--;
                    destr2ParValuePlayer.text = Lists.Destr2OfPlayerPar.ToString();
                    SetShipSound.Play();
                }
                else if (Lists.currentChousenShipIndicator == Destr2ParIndicator && Lists.Destr2OfPlayerPar == 0) notEnoughSound.Play();

                if (Lists.currentChousenShipIndicator == Gun1Indicator && Lists.Gun1OfPlayer > 0 && gunsOnField < 2)
                {
                    Lists.takenButtons.Add(btn);
                    Lists.notTakenButtons.Remove(btn);
                    Lists.takenButtonsDic.Add(btn.name, btn);
                    Lists.notTakenButtonsDic.Remove(btn.name);
                    btn.transform.gameObject.GetComponent<Image>().sprite = chousenShipImage;
                    Lists.playerShipsSchema.Add(btn.name, chousenShipGO);
                    Lists.Gun1OfPlayer--;
                    gunsOnField++;
                    gun1ValuePlayer.text = Lists.Gun1OfPlayer.ToString();
                    SetShipSound.Play();
                }
                //cheks if there are no more than two guns on scene and starts coroutine with warning sound and panel
                else if (Lists.currentChousenShipIndicator == Gun1Indicator && Lists.Gun1OfPlayer > 0 && gunsOnField == 2) 
                {
                    noMoreTwoGuns();
                }
                else if (Lists.currentChousenShipIndicator == Gun1Indicator && Lists.Gun1OfPlayer == 0) notEnoughSound.Play();

                if (Lists.currentChousenShipIndicator == Gun2Indicator && Lists.Gun2OfPlayer > 0 && gunsOnField < 2)
                {
                    Lists.takenButtons.Add(btn);
                    Lists.notTakenButtons.Remove(btn);
                    Lists.takenButtonsDic.Add(btn.name, btn);
                    Lists.notTakenButtonsDic.Remove(btn.name);
                    btn.transform.gameObject.GetComponent<Image>().sprite = chousenShipImage;
                    Lists.playerShipsSchema.Add(btn.name, chousenShipGO);
                    Lists.Gun2OfPlayer--;
                    gunsOnField++;
                    gun2ValuePlayer.text = Lists.Gun2OfPlayer.ToString();
                    SetShipSound.Play();
                }
                //cheks if there are no more than two guns on scene and starts method with warning sound and panel
                else if (Lists.currentChousenShipIndicator == Gun2Indicator && Lists.Gun2OfPlayer > 0 && gunsOnField == 2)
                {
                    noMoreTwoGuns();
                }
                else if (Lists.currentChousenShipIndicator == Gun2Indicator && Lists.Gun2OfPlayer == 0) notEnoughSound.Play();
                

                if (Lists.currentChousenShipIndicator == Gun3Indicator && Lists.Gun3OfPlayer > 0 && gunsOnField < 2)
                {
                    Lists.takenButtons.Add(btn);
                    Lists.notTakenButtons.Remove(btn);
                    Lists.takenButtonsDic.Add(btn.name, btn);
                    Lists.notTakenButtonsDic.Remove(btn.name);
                    btn.transform.gameObject.GetComponent<Image>().sprite = chousenShipImage;
                    Lists.playerShipsSchema.Add(btn.name, chousenShipGO);
                    Lists.Gun3OfPlayer--;
                    gunsOnField++;
                    gun3ValuePlayer.text = Lists.Gun3OfPlayer.ToString();
                    SetShipSound.Play();
                }
                //cheks if there are no more than two guns on scene and starts method with warning sound and panel
                else if (Lists.currentChousenShipIndicator == Gun3Indicator && Lists.Gun3OfPlayer > 0&&gunsOnField == 2)
                {
                    noMoreTwoGuns();
                }
                else if (Lists.currentChousenShipIndicator == Gun3Indicator && Lists.Gun3OfPlayer == 0) notEnoughSound.Play();
            }
            else notEnoughSound.Play(); //if no ship is chosen after launching the scene then system will just give according sound
        }
        else
        {
            if (btn.transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr)
            {
                //this four lines are used to activate left and right button slots near moving cruisers, cause it is taken off from battlefield by pushing that button
                //moving ships are Cruis1 and Cruis2, and this four lines are for them only
                Lists.notTakenButtonsDic[(int.Parse(btn.name) - 1).ToString()].interactable = true;
                Lists.takenButtonsCruisNearCruisStay.Remove(Lists.notTakenButtonsDic[(int.Parse(btn.name) - 1).ToString()]);
                Lists.notTakenButtonsDic[(int.Parse(btn.name) + 1).ToString()].interactable = true;
                Lists.takenButtonsCruisNearCruisStay.Remove(Lists.notTakenButtonsDic[(int.Parse(btn.name) + 1).ToString()]);


                //moving current pushed button from one list to another (from taken ones to not taken ones)
                Lists.notTakenButtons.Add(btn);
                Lists.takenButtons.Remove(btn);
                Lists.takenButtonsDic.Remove(btn.name);
                Lists.notTakenButtonsDic.Add(btn.name, btn);

                //first clearing the list of buttons that were disactivated by cruiser to reset tham lower to preven adding them to this list twise
                foreach (Button but in Lists.takenButtonsCruisChousen)
                {
                    but.interactable = true;
                }
                Lists.takenButtonsCruisChousen.Clear();

                //first clearing the list of buttons that were disactivated by cruiser to reset tham lower to preven adding them to this list twise
                foreach (Button but in Lists.takenButtonsSecondCruis)
                {
                    but.interactable = true;
                }
                Lists.takenButtonsSecondCruis.Clear();

                if (Lists.currentChousenShipIndicator == Cruis2Indicator || Lists.currentChousenShipIndicator == Cruis1Indicator)
                {
                    foreach (Button but in Lists.notTakenButtons)
                    {
                        if (but.name.StartsWith("0") || but.name.Contains("15") || but.name.Contains("30")
                            || but.name.Contains("14") || but.name.Contains("29") || but.name.Contains("44"))
                        {
                            but.interactable = false;
                            Lists.takenButtonsCruisChousen.Add(but);
                        }

                        if (!but.name.StartsWith("0") && !but.name.Contains("15") && !but.name.Contains("30")
                            && !but.name.Contains("14") && !but.name.Contains("29") && !but.name.Contains("44") 
                            && Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 1).ToString())
                            || Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 1).ToString()))
                        {
                            but.interactable = false;
                            Lists.takenButtonsCruisChousen.Add(but);
                        }

                        if (int.Parse(but.name) != 1 && !but.name.Contains("16") && !but.name.Contains("31")
                            && !but.name.Contains("13") && !but.name.Contains("28") && !but.name.Contains("43"))
                        {
                            if ((Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr)) ||
                                (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                                )
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                        else if (int.Parse(but.name) == 1 || but.name.Contains("16") || but.name.Contains("31"))
                        {
                            if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                        else if (but.name.Contains("13") || but.name.Contains("28") || but.name.Contains("43"))
                        {
                            if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                    }


                }

                btn.transform.gameObject.GetComponent<Image>().sprite = notSetButtImg;
                Lists.playerShipsSchema.Remove(btn.name);
                Lists.Cruis1OfPlayer++;
                cruisersOnField--;
                //this one is necessary for assigning randome cruiser ander player control arfter start the battle scene
                //so if this value more than one it will be taken into account on PlayerShipCPU while starting the class
                Lists.cruiser1PlayerPutOnScene--;
                cruis1ValuePlayer.text = Lists.Cruis1OfPlayer.ToString();
                PutBackShipSound.Play();
            }
            else if (btn.transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr)
            {
                Lists.notTakenButtonsDic[(int.Parse(btn.name) - 1).ToString()].interactable = true;
                Lists.takenButtonsCruisNearCruisStay.Remove(Lists.notTakenButtonsDic[(int.Parse(btn.name) - 1).ToString()]);
                Lists.notTakenButtonsDic[(int.Parse(btn.name) + 1).ToString()].interactable = true;
                Lists.takenButtonsCruisNearCruisStay.Remove(Lists.notTakenButtonsDic[(int.Parse(btn.name) + 1).ToString()]);


                Lists.notTakenButtons.Add(btn);
                Lists.takenButtons.Remove(btn);
                Lists.takenButtonsDic.Remove(btn.name);
                Lists.notTakenButtonsDic.Add(btn.name, btn);

                //first clearing the list of buttons that were disactivated by cruiser to reset tham lower to preven adding them to this list twise
                foreach (Button but in Lists.takenButtonsCruisChousen)
                {
                    but.interactable = true;
                }
                Lists.takenButtonsCruisChousen.Clear();

                //first clearing the list of buttons that were disactivated by cruiser to reset tham lower to preven adding them to this list twise
                foreach (Button but in Lists.takenButtonsSecondCruis)
                {
                    but.interactable = true;
                }
                Lists.takenButtonsSecondCruis.Clear();

                if (Lists.currentChousenShipIndicator == Cruis2Indicator || Lists.currentChousenShipIndicator == Cruis1Indicator)
                {
                    //this foreach line works only if current chosen ship button (if it is pushed and indicator stays on it) is moving type (Cruis1 of Cruis2)
                    //first if is for disactivating buttons that are on left and right borders of battlefield
                    foreach (Button but in Lists.notTakenButtons)
                    {
                        if (but.name.StartsWith("0") || but.name.Contains("15") || but.name.Contains("30")
                            || but.name.Contains("14") || but.name.Contains("29") || but.name.Contains("44"))
                        {
                            but.interactable = false;
                            Lists.takenButtonsCruisChousen.Add(but);
                        }

                        //this one checks if pushed button is not on left or right borders of battlefield and if there are ships set already on near leaft and right slots
                        //of current button, looks like extra but it is necessary to clear these near buttons on other placing buttons codes
                        if (!but.name.StartsWith("0") && !but.name.Contains("15") && !but.name.Contains("30")
                            && !but.name.Contains("14") && !but.name.Contains("29") && !but.name.Contains("44") 
                            && Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 1).ToString())
                            || Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 1).ToString()))
                        {
                            but.interactable = false;
                            Lists.takenButtonsCruisChousen.Add(but);
                        }
                        //this lower three if conditions are for rechekcking the 2 slots far slots from moving type of cruisers after puting off the cruiser and if
                        //chousen ship is still moving type cruiser
                        if (int.Parse(but.name) != 1 && !but.name.Contains("16") && !but.name.Contains("31")
                            && !but.name.Contains("13") && !but.name.Contains("28") && !but.name.Contains("43"))
                        {
                            if ((Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr)) ||
                                (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                                )
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                        else if (int.Parse(but.name) == 1 || but.name.Contains("16") || but.name.Contains("31"))
                        {
                            if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                        else if (but.name.Contains("13") || but.name.Contains("28") || but.name.Contains("43"))
                        {
                            if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }

                    }
                }
                //else
                //{
                //    foreach (Button but in Lists.takenButtonsCruisChousen)
                //    {
                //        but.interactable = true;
                //    }
                //    Lists.takenButtonsCruisChousen.Clear();
                //}

                btn.transform.gameObject.GetComponent<Image>().sprite = notSetButtImg;
                Lists.playerShipsSchema.Remove(btn.name);
                Lists.Cruis2OfPlayer++;
                cruisersOnField--;
                //this one is necessary for assigning randome cruiser ander player control arfter start the battle scene
                //so if this value more than one it will be taken into account on PlayerShipCPU while starting the class
                Lists.cruiser2PlayerPutOnScene--;
                cruis2ValuePlayer.text = Lists.Cruis2OfPlayer.ToString();
                PutBackShipSound.Play();
            }
            else if (btn.transform.gameObject.GetComponent<Image>().sprite == Cruis3Spr)
            {
                Lists.notTakenButtons.Add(btn);
                Lists.takenButtons.Remove(btn);
                Lists.takenButtonsDic.Remove(btn.name);
                Lists.notTakenButtonsDic.Add(btn.name, btn);

                if (Lists.currentChousenShipIndicator == Cruis2Indicator || Lists.currentChousenShipIndicator == Cruis1Indicator)
                {
                    //first clearing the list of buttons that were disactivated by cruiser to reset them lower to preven adding them to this list twise
                    foreach (Button but in Lists.takenButtonsCruisChousen)
                    {
                        but.interactable = true;
                    }
                    Lists.takenButtonsCruisChousen.Clear();

                    //first clearing the list of buttons that were disactivated by moving cruiser to moving cuisers to reset them lower 
                    //to preven adding them to this list twise
                    foreach (Button but in Lists.takenButtonsSecondCruis)
                    {
                        but.interactable = true;
                    }
                    Lists.takenButtonsSecondCruis.Clear();

                    foreach (Button but in Lists.notTakenButtons)
                    {
                        if (but.name.StartsWith("0") || but.name.Contains("15") || but.name.Contains("30")
                            || but.name.Contains("14") || but.name.Contains("29") || but.name.Contains("44"))
                        {
                            but.interactable = false;
                            Lists.takenButtonsCruisChousen.Add(but);
                        }

                        if (!but.name.StartsWith("0") && !but.name.Contains("15") && !but.name.Contains("30")
                            && !but.name.Contains("14") && !but.name.Contains("29") && !but.name.Contains("44") 
                            && Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 1).ToString())
                            || Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 1).ToString()))
                        {
                            but.interactable = false;
                            Lists.takenButtonsCruisChousen.Add(but);
                        }

                        if (int.Parse(but.name) != 1 && !but.name.Contains("16") && !but.name.Contains("31")
                            && !but.name.Contains("13") && !but.name.Contains("28") && !but.name.Contains("43"))
                        {
                            if ((Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr)) ||
                                (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                                )
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                        else if (int.Parse(but.name) == 1 || but.name.Contains("16") || but.name.Contains("31"))
                        {
                            if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                        else if (but.name.Contains("13") || but.name.Contains("28") || but.name.Contains("43"))
                        {
                            if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                    }
                }

                btn.transform.gameObject.GetComponent<Image>().sprite = notSetButtImg;
                Lists.playerShipsSchema.Remove(btn.name);
                Lists.Cruis3OfPlayer++;
                cruisersOnField--;
                //this one is necessary for assigning randome cruiser ander player control arfter start the battle scene
                //so if this value more than one it will be taken into account on PlayerShipCPU while starting the class
                Lists.cruiser3PlayerPutOnScene--;
                cruis3ValuePlayer.text = Lists.Cruis3OfPlayer.ToString();
                PutBackShipSound.Play();
            }
            else if (btn.transform.gameObject.GetComponent<Image>().sprite == Cruis4Spr)
            {
                Lists.notTakenButtons.Add(btn);
                Lists.takenButtons.Remove(btn);
                Lists.takenButtonsDic.Remove(btn.name);
                Lists.notTakenButtonsDic.Add(btn.name, btn);

                if (Lists.currentChousenShipIndicator == Cruis2Indicator || Lists.currentChousenShipIndicator == Cruis1Indicator)
                {
                    //first clearing the list of buttons that were disactivated by cruiser to reset them lower to preven adding them to this list twise
                    foreach (Button but in Lists.takenButtonsCruisChousen)
                    {
                        but.interactable = true;
                    }
                    Lists.takenButtonsCruisChousen.Clear();

                    //first clearing the list of buttons that were disactivated by moving cruiser to moving cuisers to reset them lower 
                    //to preven adding them to this list twise
                    foreach (Button but in Lists.takenButtonsSecondCruis)
                    {
                        but.interactable = true;
                    }
                    Lists.takenButtonsSecondCruis.Clear();

                    foreach (Button but in Lists.notTakenButtons)
                    {
                        if (but.name.StartsWith("0") || but.name.Contains("15") || but.name.Contains("30")
                            || but.name.Contains("14") || but.name.Contains("29") || but.name.Contains("44"))
                        {
                            but.interactable = false;
                            Lists.takenButtonsCruisChousen.Add(but);
                        }

                        if (!but.name.StartsWith("0") && !but.name.Contains("15") && !but.name.Contains("30")
                            && !but.name.Contains("14") && !but.name.Contains("29") && !but.name.Contains("44")
                            && Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 1).ToString())
                            || Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 1).ToString()))
                        {
                            but.interactable = false;
                            Lists.takenButtonsCruisChousen.Add(but);
                        }

                        if (int.Parse(but.name) != 1 && !but.name.Contains("16") && !but.name.Contains("31")
                            && !but.name.Contains("13") && !but.name.Contains("28") && !but.name.Contains("43"))
                        {
                            if ((Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr)) ||
                                (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                                )
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                        else if (int.Parse(but.name) == 1 || but.name.Contains("16") || but.name.Contains("31"))
                        {
                            if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                        else if (but.name.Contains("13") || but.name.Contains("28") || but.name.Contains("43"))
                        {
                            if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                    }
                }

                btn.transform.gameObject.GetComponent<Image>().sprite = notSetButtImg;
                Lists.playerShipsSchema.Remove(btn.name);
                Lists.Cruis4OfPlayer++;
                cruisersOnField--;
                //this one is necessary for assigning randome cruiser ander player control arfter start the battle scene
                //so if this value more than one it will be taken into account on PlayerShipCPU while starting the class
                Lists.cruiser4PlayerPutOnScene--;
                cruis4ValuePlayer.text = Lists.Cruis4OfPlayer.ToString();
                PutBackShipSound.Play();
            }
            else if (btn.transform.gameObject.GetComponent<Image>().sprite == Destr1Spr)
            {
                Lists.notTakenButtons.Add(btn);
                Lists.takenButtons.Remove(btn);
                Lists.takenButtonsDic.Remove(btn.name);
                Lists.notTakenButtonsDic.Add(btn.name, btn);

                if (Lists.currentChousenShipIndicator == Cruis2Indicator || Lists.currentChousenShipIndicator == Cruis1Indicator)
                {
                    //first clearing the list of buttons that were disactivated by cruiser to reset them lower to preven adding them to this list twise
                    foreach (Button but in Lists.takenButtonsCruisChousen)
                    {
                        but.interactable = true;
                    }
                    Lists.takenButtonsCruisChousen.Clear();

                    //first clearing the list of buttons that were disactivated by moving cruiser to moving cuisers to reset them lower 
                    //to preven adding them to this list twise
                    foreach (Button but in Lists.takenButtonsSecondCruis)
                    {
                        but.interactable = true;
                    }
                    Lists.takenButtonsSecondCruis.Clear();

                    foreach (Button but in Lists.notTakenButtons)
                    {
                        if (but.name.StartsWith("0") || but.name.Contains("15") || but.name.Contains("30")
                            || but.name.Contains("14") || but.name.Contains("29") || but.name.Contains("44"))
                        {
                            but.interactable = false;
                            Lists.takenButtonsCruisChousen.Add(but);
                        }

                        if (!but.name.StartsWith("0") && !but.name.Contains("15") && !but.name.Contains("30")
                            && !but.name.Contains("14") && !but.name.Contains("29") && !but.name.Contains("44")
                            && Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 1).ToString())
                            || Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 1).ToString()))
                        {
                            but.interactable = false;
                            Lists.takenButtonsCruisChousen.Add(but);
                        }

                        if (int.Parse(but.name) != 1 && !but.name.Contains("16") && !but.name.Contains("31")
                            && !but.name.Contains("13") && !but.name.Contains("28") && !but.name.Contains("43"))
                        {
                            if ((Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr)) ||
                                (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                                )
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                        else if (int.Parse(but.name) == 1 || but.name.Contains("16") || but.name.Contains("31"))
                        {
                            if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                        else if (but.name.Contains("13") || but.name.Contains("28") || but.name.Contains("43"))
                        {
                            if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                    }
                }

                btn.transform.gameObject.GetComponent<Image>().sprite = notSetButtImg;
                Lists.playerShipsSchema.Remove(btn.name);
                Lists.Destr1OfPlayer++;
                destr1ValuePlayer.text = Lists.Destr1OfPlayer.ToString();
                PutBackShipSound.Play();
            }
            else if (btn.transform.gameObject.GetComponent<Image>().sprite == Destr2Spr)
            {
                Lists.notTakenButtons.Add(btn);
                Lists.takenButtons.Remove(btn);
                Lists.takenButtonsDic.Remove(btn.name);
                Lists.notTakenButtonsDic.Add(btn.name, btn);

                if (Lists.currentChousenShipIndicator == Cruis2Indicator || Lists.currentChousenShipIndicator == Cruis1Indicator)
                {
                    //first clearing the list of buttons that were disactivated by cruiser to reset them lower to preven adding them to this list twise
                    foreach (Button but in Lists.takenButtonsCruisChousen)
                    {
                        but.interactable = true;
                    }
                    Lists.takenButtonsCruisChousen.Clear();

                    //first clearing the list of buttons that were disactivated by moving cruiser to moving cuisers to reset them lower 
                    //to preven adding them to this list twise
                    foreach (Button but in Lists.takenButtonsSecondCruis)
                    {
                        but.interactable = true;
                    }
                    Lists.takenButtonsSecondCruis.Clear();

                    foreach (Button but in Lists.notTakenButtons)
                    {
                        if (but.name.StartsWith("0") || but.name.Contains("15") || but.name.Contains("30")
                            || but.name.Contains("14") || but.name.Contains("29") || but.name.Contains("44"))
                        {
                            but.interactable = false;
                            Lists.takenButtonsCruisChousen.Add(but);
                        }

                        if (!but.name.StartsWith("0") && !but.name.Contains("15") && !but.name.Contains("30")
                            && !but.name.Contains("14") && !but.name.Contains("29") && !but.name.Contains("44")
                            && Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 1).ToString())
                            || Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 1).ToString()))
                        {
                            but.interactable = false;
                            Lists.takenButtonsCruisChousen.Add(but);
                        }

                        if (int.Parse(but.name) != 1 && !but.name.Contains("16") && !but.name.Contains("31")
                            && !but.name.Contains("13") && !but.name.Contains("28") && !but.name.Contains("43"))
                        {
                            if ((Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr)) ||
                                (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                                )
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                        else if (int.Parse(but.name) == 1 || but.name.Contains("16") || but.name.Contains("31"))
                        {
                            if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                        else if (but.name.Contains("13") || but.name.Contains("28") || but.name.Contains("43"))
                        {
                            if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                    }
                }

                btn.transform.gameObject.GetComponent<Image>().sprite = notSetButtImg;
                Lists.playerShipsSchema.Remove(btn.name);
                Lists.Destr2OfPlayer++;
                destr2ValuePlayer.text = Lists.Destr2OfPlayer.ToString();
                PutBackShipSound.Play();
            }
            else if (btn.transform.gameObject.GetComponent<Image>().sprite == Destr3Spr)
            {
                Lists.notTakenButtons.Add(btn);
                Lists.takenButtons.Remove(btn);
                Lists.takenButtonsDic.Remove(btn.name);
                Lists.notTakenButtonsDic.Add(btn.name, btn);

                if (Lists.currentChousenShipIndicator == Cruis2Indicator || Lists.currentChousenShipIndicator == Cruis1Indicator)
                {
                    //first clearing the list of buttons that were disactivated by cruiser to reset them lower to preven adding them to this list twise
                    foreach (Button but in Lists.takenButtonsCruisChousen)
                    {
                        but.interactable = true;
                    }
                    Lists.takenButtonsCruisChousen.Clear();

                    //first clearing the list of buttons that were disactivated by moving cruiser to moving cuisers to reset them lower 
                    //to preven adding them to this list twise
                    foreach (Button but in Lists.takenButtonsSecondCruis)
                    {
                        but.interactable = true;
                    }
                    Lists.takenButtonsSecondCruis.Clear();

                    foreach (Button but in Lists.notTakenButtons)
                    {
                        if (but.name.StartsWith("0") || but.name.Contains("15") || but.name.Contains("30")
                            || but.name.Contains("14") || but.name.Contains("29") || but.name.Contains("44"))
                        {
                            but.interactable = false;
                            Lists.takenButtonsCruisChousen.Add(but);
                        }

                        if (!but.name.StartsWith("0") && !but.name.Contains("15") && !but.name.Contains("30")
                            && !but.name.Contains("14") && !but.name.Contains("29") && !but.name.Contains("44")
                            && Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 1).ToString())
                            || Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 1).ToString()))
                        {
                            but.interactable = false;
                            Lists.takenButtonsCruisChousen.Add(but);
                        }

                        if (int.Parse(but.name) != 1 && !but.name.Contains("16") && !but.name.Contains("31")
                            && !but.name.Contains("13") && !but.name.Contains("28") && !but.name.Contains("43"))
                        {
                            if ((Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr)) ||
                                (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                                )
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                        else if (int.Parse(but.name) == 1 || but.name.Contains("16") || but.name.Contains("31"))
                        {
                            if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                        else if (but.name.Contains("13") || but.name.Contains("28") || but.name.Contains("43"))
                        {
                            if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                    }
                }

                btn.transform.gameObject.GetComponent<Image>().sprite = notSetButtImg;
                Lists.playerShipsSchema.Remove(btn.name);
                Lists.Destr3OfPlayer++;
                destr3ValuePlayer.text = Lists.Destr3OfPlayer.ToString();
                PutBackShipSound.Play();
            }
            else if (btn.transform.gameObject.GetComponent<Image>().sprite == Destr4Spr)
            {
                Lists.notTakenButtons.Add(btn);
                Lists.takenButtons.Remove(btn);
                Lists.takenButtonsDic.Remove(btn.name);
                Lists.notTakenButtonsDic.Add(btn.name, btn);

                if (Lists.currentChousenShipIndicator == Cruis2Indicator || Lists.currentChousenShipIndicator == Cruis1Indicator)
                {
                    //first clearing the list of buttons that were disactivated by cruiser to reset them lower to preven adding them to this list twise
                    foreach (Button but in Lists.takenButtonsCruisChousen)
                    {
                        but.interactable = true;
                    }
                    Lists.takenButtonsCruisChousen.Clear();

                    //first clearing the list of buttons that were disactivated by moving cruiser to moving cuisers to reset them lower 
                    //to preven adding them to this list twise
                    foreach (Button but in Lists.takenButtonsSecondCruis)
                    {
                        but.interactable = true;
                    }
                    Lists.takenButtonsSecondCruis.Clear();

                    foreach (Button but in Lists.notTakenButtons)
                    {
                        if (but.name.StartsWith("0") || but.name.Contains("15") || but.name.Contains("30")
                            || but.name.Contains("14") || but.name.Contains("29") || but.name.Contains("44"))
                        {
                            but.interactable = false;
                            Lists.takenButtonsCruisChousen.Add(but);
                        }

                        if (!but.name.StartsWith("0") && !but.name.Contains("15") && !but.name.Contains("30")
                            && !but.name.Contains("14") && !but.name.Contains("29") && !but.name.Contains("44")
                            && Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 1).ToString())
                            || Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 1).ToString()))
                        {
                            but.interactable = false;
                            Lists.takenButtonsCruisChousen.Add(but);
                        }

                        if (int.Parse(but.name) != 1 && !but.name.Contains("16") && !but.name.Contains("31")
                            && !but.name.Contains("13") && !but.name.Contains("28") && !but.name.Contains("43"))
                        {
                            if ((Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr)) ||
                                (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                                )
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                        else if (int.Parse(but.name) == 1 || but.name.Contains("16") || but.name.Contains("31"))
                        {
                            if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                        else if (but.name.Contains("13") || but.name.Contains("28") || but.name.Contains("43"))
                        {
                            if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                    }
                }

                btn.transform.gameObject.GetComponent<Image>().sprite = notSetButtImg;
                Lists.playerShipsSchema.Remove(btn.name);
                Lists.Destr4OfPlayer++;
                destr4ValuePlayer.text = Lists.Destr4OfPlayer.ToString();
                PutBackShipSound.Play();
            }
            else if (btn.transform.gameObject.GetComponent<Image>().sprite == Destr1ParSpr)
            {
                Lists.notTakenButtons.Add(btn);
                Lists.takenButtons.Remove(btn);
                Lists.takenButtonsDic.Remove(btn.name);
                Lists.notTakenButtonsDic.Add(btn.name, btn);

                if (Lists.currentChousenShipIndicator == Cruis2Indicator || Lists.currentChousenShipIndicator == Cruis1Indicator)
                {
                    //first clearing the list of buttons that were disactivated by cruiser to reset them lower to preven adding them to this list twise
                    foreach (Button but in Lists.takenButtonsCruisChousen)
                    {
                        but.interactable = true;
                    }
                    Lists.takenButtonsCruisChousen.Clear();

                    //first clearing the list of buttons that were disactivated by moving cruiser to moving cuisers to reset them lower 
                    //to preven adding them to this list twise
                    foreach (Button but in Lists.takenButtonsSecondCruis)
                    {
                        but.interactable = true;
                    }
                    Lists.takenButtonsSecondCruis.Clear();

                    foreach (Button but in Lists.notTakenButtons)
                    {
                        if (but.name.StartsWith("0") || but.name.Contains("15") || but.name.Contains("30")
                            || but.name.Contains("14") || but.name.Contains("29") || but.name.Contains("44"))
                        {
                            but.interactable = false;
                            Lists.takenButtonsCruisChousen.Add(but);
                        }

                        if (!but.name.StartsWith("0") && !but.name.Contains("15") && !but.name.Contains("30")
                            && !but.name.Contains("14") && !but.name.Contains("29") && !but.name.Contains("44")
                            && Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 1).ToString())
                            || Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 1).ToString()))
                        {
                            but.interactable = false;
                            Lists.takenButtonsCruisChousen.Add(but);
                        }

                        if (int.Parse(but.name) != 1 && !but.name.Contains("16") && !but.name.Contains("31")
                            && !but.name.Contains("13") && !but.name.Contains("28") && !but.name.Contains("43"))
                        {
                            if ((Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr)) ||
                                (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                                )
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                        else if (int.Parse(but.name) == 1 || but.name.Contains("16") || but.name.Contains("31"))
                        {
                            if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                        else if (but.name.Contains("13") || but.name.Contains("28") || but.name.Contains("43"))
                        {
                            if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                    }
                }

                btn.transform.gameObject.GetComponent<Image>().sprite = notSetButtImg;
                Lists.playerShipsSchema.Remove(btn.name);
                Lists.Destr1OfPlayerPar++;
                destr1ParValuePlayer.text = Lists.Destr1OfPlayerPar.ToString();
                PutBackShipSound.Play();
            }
            else if (btn.transform.gameObject.GetComponent<Image>().sprite == Destr2ParSpr)
            {
                Lists.notTakenButtons.Add(btn);
                Lists.takenButtons.Remove(btn);
                Lists.takenButtonsDic.Remove(btn.name);
                Lists.notTakenButtonsDic.Add(btn.name, btn);

                if (Lists.currentChousenShipIndicator == Cruis2Indicator || Lists.currentChousenShipIndicator == Cruis1Indicator)
                {
                    //first clearing the list of buttons that were disactivated by cruiser to reset them lower to preven adding them to this list twise
                    foreach (Button but in Lists.takenButtonsCruisChousen)
                    {
                        but.interactable = true;
                    }
                    Lists.takenButtonsCruisChousen.Clear();

                    //first clearing the list of buttons that were disactivated by moving cruiser to moving cuisers to reset them lower 
                    //to preven adding them to this list twise
                    foreach (Button but in Lists.takenButtonsSecondCruis)
                    {
                        but.interactable = true;
                    }
                    Lists.takenButtonsSecondCruis.Clear();

                    foreach (Button but in Lists.notTakenButtons)
                    {
                        if (but.name.StartsWith("0") || but.name.Contains("15") || but.name.Contains("30")
                            || but.name.Contains("14") || but.name.Contains("29") || but.name.Contains("44"))
                        {
                            but.interactable = false;
                            Lists.takenButtonsCruisChousen.Add(but);
                        }

                        if (!but.name.StartsWith("0") && !but.name.Contains("15") && !but.name.Contains("30")
                            && !but.name.Contains("14") && !but.name.Contains("29") && !but.name.Contains("44")
                            && Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 1).ToString())
                            || Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 1).ToString()))
                        {
                            but.interactable = false;
                            Lists.takenButtonsCruisChousen.Add(but);
                        }

                        if (int.Parse(but.name) != 1 && !but.name.Contains("16") && !but.name.Contains("31")
                            && !but.name.Contains("13") && !but.name.Contains("28") && !but.name.Contains("43"))
                        {
                            if ((Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr)) ||
                                (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                                )
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                        else if (int.Parse(but.name) == 1 || but.name.Contains("16") || but.name.Contains("31"))
                        {
                            if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                        else if (but.name.Contains("13") || but.name.Contains("28") || but.name.Contains("43"))
                        {
                            if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                    }
                }

                btn.transform.gameObject.GetComponent<Image>().sprite = notSetButtImg;
                Lists.playerShipsSchema.Remove(btn.name);
                Lists.Destr2OfPlayerPar++;
                destr2ParValuePlayer.text = Lists.Destr2OfPlayerPar.ToString();
                PutBackShipSound.Play();
            }
            else if (btn.transform.gameObject.GetComponent<Image>().sprite == Gun1Spr)
            {
                Lists.notTakenButtons.Add(btn);
                Lists.takenButtons.Remove(btn);
                Lists.takenButtonsDic.Remove(btn.name);
                Lists.notTakenButtonsDic.Add(btn.name, btn);

                if (Lists.currentChousenShipIndicator == Cruis2Indicator || Lists.currentChousenShipIndicator == Cruis1Indicator)
                {
                    //first clearing the list of buttons that were disactivated by cruiser to reset them lower to preven adding them to this list twise
                    foreach (Button but in Lists.takenButtonsCruisChousen)
                    {
                        but.interactable = true;
                    }
                    Lists.takenButtonsCruisChousen.Clear();

                    //first clearing the list of buttons that were disactivated by moving cruiser to moving cuisers to reset them lower 
                    //to preven adding them to this list twise
                    foreach (Button but in Lists.takenButtonsSecondCruis)
                    {
                        but.interactable = true;
                    }
                    Lists.takenButtonsSecondCruis.Clear();

                    foreach (Button but in Lists.notTakenButtons)
                    {
                        if (but.name.StartsWith("0") || but.name.Contains("15") || but.name.Contains("30")
                            || but.name.Contains("14") || but.name.Contains("29") || but.name.Contains("44"))
                        {
                            but.interactable = false;
                            Lists.takenButtonsCruisChousen.Add(but);
                        }

                        if (!but.name.StartsWith("0") && !but.name.Contains("15") && !but.name.Contains("30")
                            && !but.name.Contains("14") && !but.name.Contains("29") && !but.name.Contains("44")
                            && Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 1).ToString())
                            || Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 1).ToString()))
                        {
                            but.interactable = false;
                            Lists.takenButtonsCruisChousen.Add(but);
                        }

                        if (int.Parse(but.name) != 1 && !but.name.Contains("16") && !but.name.Contains("31")
                            && !but.name.Contains("13") && !but.name.Contains("28") && !but.name.Contains("43"))
                        {
                            if ((Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr)) ||
                                (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                                )
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                        else if (int.Parse(but.name) == 1 || but.name.Contains("16") || but.name.Contains("31"))
                        {
                            if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                        else if (but.name.Contains("13") || but.name.Contains("28") || but.name.Contains("43"))
                        {
                            if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                    }
                }

                btn.transform.gameObject.GetComponent<Image>().sprite = notSetButtImg;
                Lists.playerShipsSchema.Remove(btn.name);
                Lists.Gun1OfPlayer++;
                gunsOnField--;
                gun1ValuePlayer.text = Lists.Gun1OfPlayer.ToString();
                PutBackShipSound.Play();
            }
            else if (btn.transform.gameObject.GetComponent<Image>().sprite == Gun2Spr)
            {
                Lists.notTakenButtons.Add(btn);
                Lists.takenButtons.Remove(btn);
                Lists.takenButtonsDic.Remove(btn.name);
                Lists.notTakenButtonsDic.Add(btn.name, btn);

                if (Lists.currentChousenShipIndicator == Cruis2Indicator || Lists.currentChousenShipIndicator == Cruis1Indicator)
                {
                    //first clearing the list of buttons that were disactivated by cruiser to reset them lower to preven adding them to this list twise
                    foreach (Button but in Lists.takenButtonsCruisChousen)
                    {
                        but.interactable = true;
                    }
                    Lists.takenButtonsCruisChousen.Clear();

                    //first clearing the list of buttons that were disactivated by moving cruiser to moving cuisers to reset them lower 
                    //to preven adding them to this list twise
                    foreach (Button but in Lists.takenButtonsSecondCruis)
                    {
                        but.interactable = true;
                    }
                    Lists.takenButtonsSecondCruis.Clear();

                    foreach (Button but in Lists.notTakenButtons)
                    {
                        if (but.name.StartsWith("0") || but.name.Contains("15") || but.name.Contains("30")
                            || but.name.Contains("14") || but.name.Contains("29") || but.name.Contains("44"))
                        {
                            but.interactable = false;
                            Lists.takenButtonsCruisChousen.Add(but);
                        }

                        if (!but.name.StartsWith("0") && !but.name.Contains("15") && !but.name.Contains("30")
                            && !but.name.Contains("14") && !but.name.Contains("29") && !but.name.Contains("44")
                            && Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 1).ToString())
                            || Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 1).ToString()))
                        {
                            but.interactable = false;
                            Lists.takenButtonsCruisChousen.Add(but);
                        }

                        if (int.Parse(but.name) != 1 && !but.name.Contains("16") && !but.name.Contains("31")
                            && !but.name.Contains("13") && !but.name.Contains("28") && !but.name.Contains("43"))
                        {
                            if ((Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr)) ||
                                (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                                )
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                        else if (int.Parse(but.name) == 1 || but.name.Contains("16") || but.name.Contains("31"))
                        {
                            if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                        else if (but.name.Contains("13") || but.name.Contains("28") || but.name.Contains("43"))
                        {
                            if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                    }
                }

                btn.transform.gameObject.GetComponent<Image>().sprite = notSetButtImg;
                Lists.playerShipsSchema.Remove(btn.name);
                Lists.Gun2OfPlayer++;
                gunsOnField--;
                gun2ValuePlayer.text = Lists.Gun2OfPlayer.ToString();
                PutBackShipSound.Play();
            }
            else if (btn.transform.gameObject.GetComponent<Image>().sprite == Gun3Spr)
            {
                Lists.notTakenButtons.Add(btn);
                Lists.takenButtons.Remove(btn);
                Lists.takenButtonsDic.Remove(btn.name);
                Lists.notTakenButtonsDic.Add(btn.name, btn);

                if (Lists.currentChousenShipIndicator == Cruis2Indicator || Lists.currentChousenShipIndicator == Cruis1Indicator)
                {
                    //first clearing the list of buttons that were disactivated by cruiser to reset them lower to preven adding them to this list twise
                    foreach (Button but in Lists.takenButtonsCruisChousen)
                    {
                        but.interactable = true;
                    }
                    Lists.takenButtonsCruisChousen.Clear();

                    //first clearing the list of buttons that were disactivated by moving cruiser to moving cuisers to reset them lower 
                    //to preven adding them to this list twise
                    foreach (Button but in Lists.takenButtonsSecondCruis)
                    {
                        but.interactable = true;
                    }
                    Lists.takenButtonsSecondCruis.Clear();

                    foreach (Button but in Lists.notTakenButtons)
                    {
                        if (but.name.StartsWith("0") || but.name.Contains("15") || but.name.Contains("30")
                            || but.name.Contains("14") || but.name.Contains("29") || but.name.Contains("44"))
                        {
                            but.interactable = false;
                            Lists.takenButtonsCruisChousen.Add(but);
                        }

                        if (!but.name.StartsWith("0") && !but.name.Contains("15") && !but.name.Contains("30")
                            && !but.name.Contains("14") && !but.name.Contains("29") && !but.name.Contains("44")
                            && Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 1).ToString())
                            || Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 1).ToString()))
                        {
                            but.interactable = false;
                            Lists.takenButtonsCruisChousen.Add(but);
                        }

                        if (int.Parse(but.name) != 1 && !but.name.Contains("16") && !but.name.Contains("31")
                            && !but.name.Contains("13") && !but.name.Contains("28") && !but.name.Contains("43"))
                        {
                            if ((Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr)) ||
                                (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                                )
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                        else if (int.Parse(but.name) == 1 || but.name.Contains("16") || but.name.Contains("31"))
                        {
                            if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                        else if (but.name.Contains("13") || but.name.Contains("28") || but.name.Contains("43"))
                        {
                            if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                                && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                            {
                                but.interactable = false;
                                Lists.takenButtonsSecondCruis.Add(but);
                            }
                        }
                    }
                }

                btn.transform.gameObject.GetComponent<Image>().sprite = notSetButtImg;
                Lists.playerShipsSchema.Remove(btn.name);
                Lists.Gun3OfPlayer++;
                gunsOnField--;
                gun3ValuePlayer.text = Lists.Gun3OfPlayer.ToString();
                PutBackShipSound.Play();
            }
        }
    }

    public void C1Button()
    {
        if (Lists.currentChousenShipIndicator) Lists.currentChousenShipIndicator.SetActive(false);
        Lists.currentChousenShipIndicator = Cruis1Indicator;
        Lists.currentChousenShipIndicator.SetActive (true);
        chousenShipImage = Cruis1Spr;
        chousenShipGO = Player1Cruis;
        ChoseShipSound.Play();

        //this is the same checking code which is explained on battleField ship remove button
        foreach (Button but in Lists.notTakenButtons) {
            if (but.name.StartsWith("0") || but.name.Contains("15") || but.name.Contains("30")
                || but.name.Contains("14") || but.name.Contains("29") || but.name.Contains("44"))
            {
                but.interactable = false;
                Lists.takenButtonsCruisChousen.Add(but);
            }

            if (!but.name.StartsWith("0") && !but.name.Contains("15") && !but.name.Contains("30")
                && !but.name.Contains("14") && !but.name.Contains("29") && !but.name.Contains("44") 
                && Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 1).ToString())
                || Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 1).ToString()))
            {
                but.interactable = false;
                Lists.takenButtonsCruisChousen.Add(but);
            }

            if (int.Parse(but.name) != 1 && !but.name.Contains("16") && !but.name.Contains("31")
                            && !but.name.Contains("13") && !but.name.Contains("28") && !but.name.Contains("43"))
            {
                if ((Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString()) 
                    && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr)) ||
                    (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                    && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                    )
                {
                    but.interactable = false;
                    Lists.takenButtonsSecondCruis.Add(but);
                }
            }
            else if (int.Parse(but.name) == 1 || but.name.Contains("16") || but.name.Contains("31"))
            {
                if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                    && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                {
                    but.interactable = false;
                    Lists.takenButtonsSecondCruis.Add(but);
                }
            }
            else if (but.name.Contains("13") || but.name.Contains("28") || but.name.Contains("43"))
            {
                if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                    && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                {
                    but.interactable = false;
                    Lists.takenButtonsSecondCruis.Add(but);
                }
            }
        }


        foreach (Button but in Lists.takenButtonsSecondCruis)
        {
            but.interactable = false;
        }

    }
    public void C2Button()
    {
        if (Lists.currentChousenShipIndicator)  Lists.currentChousenShipIndicator.SetActive(false);
        Lists.currentChousenShipIndicator = Cruis2Indicator;
        Lists.currentChousenShipIndicator.SetActive(true);
        chousenShipImage = Cruis2Spr;
        chousenShipGO = Player2Cruis;
        ChoseShipSound.Play();

        //this is the same checking code which is explained on battleField ship remove button
        foreach (Button but in Lists.notTakenButtons)
        {
            if (but.name.StartsWith("0") || but.name.Contains("15") || but.name.Contains("30")
                || but.name.Contains("14") || but.name.Contains("29") || but.name.Contains("44"))
            {
                but.interactable = false;
                Lists.takenButtonsCruisChousen.Add(but);
            }

            if (!but.name.StartsWith("0") && !but.name.Contains("15") && !but.name.Contains("30")
                && !but.name.Contains("14") && !but.name.Contains("29") && !but.name.Contains("44") && 
                Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 1).ToString())
                || Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 1).ToString()))
            {
                but.interactable = false;
                Lists.takenButtonsCruisChousen.Add(but);
            }

            if (int.Parse(but.name) != 1 && !but.name.Contains("16") && !but.name.Contains("31")
                            && !but.name.Contains("13") && !but.name.Contains("28") && !but.name.Contains("43"))
            {
                if ((Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                    && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr)) ||
                    (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                    && (Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                    )
                {
                    but.interactable = false;
                    Lists.takenButtonsSecondCruis.Add(but);
                }
            }
            else if (int.Parse(but.name) == 1 || but.name.Contains("16") || but.name.Contains("31"))
            {
                if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) + 2).ToString())
                    && (Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) + 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                {
                    but.interactable = false;
                    Lists.takenButtonsSecondCruis.Add(but);
                }
            }
            else if (but.name.Contains("13") || but.name.Contains("28") || but.name.Contains("43"))
            {
                if (Lists.takenButtonsDic.ContainsKey((int.Parse(but.name) - 2).ToString())
                    && (Lists.takenButtonsDic[(int.Parse(but.name)- 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis2Spr ||
                                Lists.takenButtonsDic[(int.Parse(but.name) - 2).ToString()].transform.gameObject.GetComponent<Image>().sprite == Cruis1Spr))
                {
                    but.interactable = false;
                    Lists.takenButtonsSecondCruis.Add(but);
                }
            }



        }
        foreach (Button but in Lists.takenButtonsSecondCruis)
        {
            but.interactable = false;
        }
    }
    public void C3Button()
    {
        if (Lists.currentChousenShipIndicator) Lists.currentChousenShipIndicator.SetActive(false);
        Lists.currentChousenShipIndicator = Cruis3Indicator;
        Lists.currentChousenShipIndicator.SetActive(true);
        chousenShipImage = Cruis3Spr;
        chousenShipGO = Player3Cruis;
        ChoseShipSound.Play();

        //sets active buttons that were disabled by only chosing maneouvering cruisers (but not setting them on scene)
        foreach (Button but in Lists.takenButtonsCruisChousen)
        {
                but.interactable = true;
        }
        Lists.takenButtonsCruisChousen.Clear();

        //puts back active state for buttons that were disabled on code for preventing to put two maneauvring cruise closer than 2 units far from each other
        foreach (Button but in Lists.takenButtonsSecondCruis)
        {
            but.interactable = true;
        }
        Lists.takenButtonsSecondCruis.Clear();

        //checks the buttons that should be inactive cause thay are near maneuvring Cruisers that are staing on battlefield already, and it should be
        //last in row here
        foreach (Button btn in Lists.takenButtonsCruisNearCruisStay)
        {
            btn.interactable = false;
        }
    }
    public void C4Button()
    {
        if (Lists.currentChousenShipIndicator) Lists.currentChousenShipIndicator.SetActive(false);
        Lists.currentChousenShipIndicator = Cruis4Indicator;
        Lists.currentChousenShipIndicator.SetActive(true);
        chousenShipImage = Cruis4Spr;
        chousenShipGO = Player4Cruis;
        ChoseShipSound.Play();

        //sets active buttons that were disabled by only chosing maneouvering cruisers (but not setting them on scene)
        foreach (Button but in Lists.takenButtonsCruisChousen)
        {
            but.interactable = true;
        }
        Lists.takenButtonsCruisChousen.Clear();

        //puts back active state for buttons that were disabled on code for preventing to put two maneauvring cruise closer than 2 units far from each other
        foreach (Button but in Lists.takenButtonsSecondCruis)
        {
            but.interactable = true;
        }
        Lists.takenButtonsSecondCruis.Clear();

        //checks the buttons that should be inactive cause thay are near maneuvring Cruisers that are staing on battlefield already, and it should be
        //last in row here
        foreach (Button btn in Lists.takenButtonsCruisNearCruisStay)
        {
            btn.interactable = false;
        }
    }
    public void D4Button()
    {
        if (Lists.currentChousenShipIndicator) Lists.currentChousenShipIndicator.SetActive(false);
        Lists.currentChousenShipIndicator = Destr4Indicator;
        Lists.currentChousenShipIndicator.SetActive(true);
        chousenShipImage = Destr4Spr;
        chousenShipGO = Player4Dstr;
        ChoseShipSound.Play();

        //sets active buttons that were disabled by only chosing maneouvering cruisers (but not setting them on scene)
        foreach (Button but in Lists.takenButtonsCruisChousen)
        {
            but.interactable = true;
        }
        Lists.takenButtonsCruisChousen.Clear();

        //puts back active state for buttons that were disabled on code for preventing to put two maneauvring cruise closer than 2 units far from each other
        foreach (Button but in Lists.takenButtonsSecondCruis)
        {
            but.interactable = true;
        }
        Lists.takenButtonsSecondCruis.Clear();

        //checks the buttons that should be inactive cause thay are near maneuvring Cruisers that are staing on battlefield already, and it should be
        //last in row here
        foreach (Button btn in Lists.takenButtonsCruisNearCruisStay)
        {
            btn.interactable = false;
        }
    }
    public void D3Button()
    {
        if (Lists.currentChousenShipIndicator) Lists.currentChousenShipIndicator.SetActive(false);
        Lists.currentChousenShipIndicator = Destr3Indicator;
        Lists.currentChousenShipIndicator.SetActive(true);
        chousenShipImage = Destr3Spr;
        chousenShipGO = Player3Dstr;
        ChoseShipSound.Play();

        //sets active buttons that were disabled by only chosing maneouvering cruisers (but not setting them on scene)
        foreach (Button but in Lists.takenButtonsCruisChousen)
        {
            but.interactable = true;
        }
        Lists.takenButtonsCruisChousen.Clear();

        //puts back active state for buttons that were disabled on code for preventing to put two maneauvring cruise closer than 2 units far from each other
        foreach (Button but in Lists.takenButtonsSecondCruis)
        {
            but.interactable = true;
        }
        Lists.takenButtonsSecondCruis.Clear();

        //checks the buttons that should be inactive cause thay are near maneuvring Cruisers that are staing on battlefield already, and it should be
        //last in row here
        foreach (Button btn in Lists.takenButtonsCruisNearCruisStay)
        {
            btn.interactable = false;
        }
    }
    public void D2Button()
    {
        if (Lists.currentChousenShipIndicator) Lists.currentChousenShipIndicator.SetActive(false);
        Lists.currentChousenShipIndicator = Destr2Indicator;
        Lists.currentChousenShipIndicator.SetActive(true);
        chousenShipImage = Destr2Spr;
        chousenShipGO = Player2Dstr;
        ChoseShipSound.Play();

        //sets active buttons that were disabled by only chosing maneouvering cruisers (but not setting them on scene)
        foreach (Button but in Lists.takenButtonsCruisChousen)
        {
            but.interactable = true;
        }
        Lists.takenButtonsCruisChousen.Clear();

        //puts back active state for buttons that were disabled on code for preventing to put two maneauvring cruise closer than 2 units far from each other
        foreach (Button but in Lists.takenButtonsSecondCruis)
        {
            but.interactable = true;
        }
        Lists.takenButtonsSecondCruis.Clear();

        //checks the buttons that should be inactive cause thay are near maneuvring Cruisers that are staing on battlefield already, and it should be
        //last in row here
        foreach (Button btn in Lists.takenButtonsCruisNearCruisStay)
        {
            btn.interactable = false;
        }
    }
    public void D2PButton()
    {
        if (Lists.currentChousenShipIndicator) Lists.currentChousenShipIndicator.SetActive(false);
        Lists.currentChousenShipIndicator = Destr2ParIndicator;
        Lists.currentChousenShipIndicator.SetActive(true);
        chousenShipImage = Destr2ParSpr;
        chousenShipGO = Player2DstrParal;
        ChoseShipSound.Play();

        //sets active buttons that were disabled by only chosing maneouvering cruisers (but not setting them on scene)
        foreach (Button but in Lists.takenButtonsCruisChousen)
        {
            but.interactable = true;
        }
        Lists.takenButtonsCruisChousen.Clear();

        //puts back active state for buttons that were disabled on code for preventing to put two maneauvring cruise closer than 2 units far from each other
        foreach (Button but in Lists.takenButtonsSecondCruis)
        {
            but.interactable = true;
        }
        Lists.takenButtonsSecondCruis.Clear();

        //checks the buttons that should be inactive cause thay are near maneuvring Cruisers that are staing on battlefield already, and it should be
        //last in row here
        foreach (Button btn in Lists.takenButtonsCruisNearCruisStay)
        {
            btn.interactable = false;
        }
    }
    public void D1Button()
    {
        if (Lists.currentChousenShipIndicator) Lists.currentChousenShipIndicator.SetActive(false);
        Lists.currentChousenShipIndicator = Destr1Indicator;
        Lists.currentChousenShipIndicator.SetActive(true);
        chousenShipImage = Destr1Spr;
        chousenShipGO = Player1Dstr;
        ChoseShipSound.Play();

        //sets active buttons that were disabled by only chosing maneouvering cruisers (but not setting them on scene)
        foreach (Button but in Lists.takenButtonsCruisChousen)
        {
            but.interactable = true;
        }
        Lists.takenButtonsCruisChousen.Clear();

        //puts back active state for buttons that were disabled on code for preventing to put two maneauvring cruise closer than 2 units far from each other
        foreach (Button but in Lists.takenButtonsSecondCruis)
        {
            but.interactable = true;
        }
        Lists.takenButtonsSecondCruis.Clear();

        //checks the buttons that should be inactive cause thay are near maneuvring Cruisers that are staing on battlefield already, and it should be
        //last in row here
        foreach (Button btn in Lists.takenButtonsCruisNearCruisStay)
        {
            btn.interactable = false;
        }
    }
    public void D1PButton()
    {
        if (Lists.currentChousenShipIndicator) Lists.currentChousenShipIndicator.SetActive(false);
        Lists.currentChousenShipIndicator = Destr1ParIndicator;
        Lists.currentChousenShipIndicator.SetActive(true);
        chousenShipImage = Destr1ParSpr;
        chousenShipGO = Player1DstrParal;
        ChoseShipSound.Play();

        //sets active buttons that were disabled by only chosing maneouvering cruisers (but not setting them on scene)
        foreach (Button but in Lists.takenButtonsCruisChousen)
        {
            but.interactable = true;
        }
        Lists.takenButtonsCruisChousen.Clear();

        //puts back active state for buttons that were disabled on code for preventing to put two maneauvring cruise closer than 2 units far from each other
        foreach (Button but in Lists.takenButtonsSecondCruis)
        {
            but.interactable = true;
        }
        Lists.takenButtonsSecondCruis.Clear();

        //checks the buttons that should be inactive cause thay are near maneuvring Cruisers that are staing on battlefield already, and it should be
        //last in row here
        foreach (Button btn in Lists.takenButtonsCruisNearCruisStay)
        {
            btn.interactable = false;
        }
    }
    public void G1Button()
    {
        if (Lists.currentChousenShipIndicator) Lists.currentChousenShipIndicator.SetActive(false);
        Lists.currentChousenShipIndicator = Gun1Indicator;
        Lists.currentChousenShipIndicator.SetActive(true);
        chousenShipImage = Gun1Spr;
        chousenShipGO = Player1Gun;
        ChoseShipSound.Play();

        //sets active buttons that were disabled by only chosing maneouvering cruisers (but not setting them on scene)
        foreach (Button but in Lists.takenButtonsCruisChousen)
        {
            but.interactable = true;
        }
        Lists.takenButtonsCruisChousen.Clear();

        //puts back active state for buttons that were disabled on code for preventing to put two maneauvring cruise closer than 2 units far from each other
        foreach (Button but in Lists.takenButtonsSecondCruis)
        {
            but.interactable = true;
        }
        Lists.takenButtonsSecondCruis.Clear();

        //checks the buttons that should be inactive cause thay are near maneuvring Cruisers that are staing on battlefield already, and it should be
        //last in row here
        foreach (Button btn in Lists.takenButtonsCruisNearCruisStay)
        {
            btn.interactable = false;
        }
    }
    public void G2Button()
    {
        if (Lists.currentChousenShipIndicator) Lists.currentChousenShipIndicator.SetActive(false);
        Lists.currentChousenShipIndicator = Gun2Indicator;
        Lists.currentChousenShipIndicator.SetActive(true);
        chousenShipImage = Gun2Spr;
        chousenShipGO = Player2Gun;
        ChoseShipSound.Play();

        //sets active buttons that were disabled by only chosing maneouvering cruisers (but not setting them on scene)
        foreach (Button but in Lists.takenButtonsCruisChousen)
        {
            but.interactable = true;
        }
        Lists.takenButtonsCruisChousen.Clear();

        //puts back active state for buttons that were disabled on code for preventing to put two maneauvring cruise closer than 2 units far from each other
        foreach (Button but in Lists.takenButtonsSecondCruis)
        {
            but.interactable = true;
        }
        Lists.takenButtonsSecondCruis.Clear();

        //checks the buttons that should be inactive cause thay are near maneuvring Cruisers that are staing on battlefield already, and it should be
        //last in row here
        foreach (Button btn in Lists.takenButtonsCruisNearCruisStay)
        {
            btn.interactable = false;
        }
    }
    public void G3Button()
    {
        if (Lists.currentChousenShipIndicator) Lists.currentChousenShipIndicator.SetActive(false);
        Lists.currentChousenShipIndicator = Gun3Indicator;
        Lists.currentChousenShipIndicator.SetActive(true);
        chousenShipImage = Gun3Spr;
        chousenShipGO = Player3Gun;
        ChoseShipSound.Play();

        //sets active buttons that were disabled by only chosing maneouvering cruisers (but not setting them on scene)
        foreach (Button but in Lists.takenButtonsCruisChousen)
        {
            but.interactable = true;
        }
        Lists.takenButtonsCruisChousen.Clear();

        //puts back active state for buttons that were disabled on code for preventing to put two maneauvring cruise closer than 2 units far from each other
        foreach (Button but in Lists.takenButtonsSecondCruis)
        {
            but.interactable = true;
        }
        Lists.takenButtonsSecondCruis.Clear();

        //checks the buttons that should be inactive cause thay are near maneuvring Cruisers that are staing on battlefield already, and it should be
        //last in row here
        foreach (Button btn in Lists.takenButtonsCruisNearCruisStay)
        {
            btn.interactable = false;
        }
    }

    public void switchSceneAfterBattle() {
        schangeSceneSound.Play();
        StartCoroutine(switchSceneAfterBattleWithLag());
    }

    IEnumerator switchSceneAfterBattleWithLag() {
        yield return new WaitForSeconds(0.9f);
        Lists.PlayerShip.Clear();
        Lists.AllPlayerShips.Clear();
        Lists.AllPlayerShipsWithoutGuns.Clear();
        Lists.AllCPUShipsWithoutGuns.Clear();
        Lists.AllCPUShips.Clear();

        Lists.PlayerShips1Class.Clear();
        Lists.CPUShips1Class.Clear();
        Lists.PlayerShips2Class.Clear();
        Lists.CPUShips2Class.Clear();

        Lists.AllPlayerGuns.Clear();
        Lists.AllCPUGuns.Clear();

        //this one is necessary to clear the data about player start fleet that is used on ObjectPuller while instantiating and pulling only necessary objects to scene
        //depending on player fleet. Thi data is populated on Start method in condition block while the fleet of player is not yet set on scheme (1 iteration)
        Lists.ClearFleetOfPlayerForPull();

        //prepare the bool for next battle. This condition is necessary to prevent a burst of guns after the start of battle
        Lists.playerFleetIsSet = false;

        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;

        SceneSwitchMngr.LoadJourneyScene();
    }

    private void Update()
    {

        if (CPUWin) {
            CPUWin = false;
            Lists.isAfterBattleLost = true;
            Lists.isAfterBattleWin = false;
            endOfBattleTxt.transform.parent.gameObject.SetActive(true);
            endOfBattleTxt.color = new Color(0.81f, 0.26f, 0, 1);
            endOfBattleTxt.transform.parent.gameObject.GetComponent<Image>().color = new Color (0.81f, 0.26f,0,1);
            endOfBattleTxt.text = Constants.Instance.getYouLostTxt();
        }
        if (PlayerWin)
        {
            PlayerWin = false;
            Lists.isAfterBattleWin = true;
            Lists.isAfterBattleLost = false;
            endOfBattleTxt.transform.parent.gameObject.SetActive(true);
            endOfBattleTxt.color = new Color(0, 0.81f, 0, 1); 
            endOfBattleTxt.transform.parent.gameObject.GetComponent<Image>().color = new Color(0, 0.81f, 0, 1);
            endOfBattleTxt.text = Constants.Instance.getYouWinTxt();
        }

        //Debug.Log(Lists.AllPlayerShipsWithoutGuns.Count);

        //if (setPlayerCruis)
        //{
        //    setPlayerCruis = false;
        //    setPlayerShip();
        //}
    }

}
