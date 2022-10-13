using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System.IO;
using System;
using UnityEngine.Audio;


#if UNITY_IOS
using UnityEngine.iOS;
#endif

public class MenuMngr : Singleton<MenuMngr>
{
    private string fileName = "SaveData"; //file for save game data
    private string fileNameDaraga = "DaragaData";
    private string fileNamePrefs = "PlayerPrefs";
    public Dropdown dropdown;
    //private string chosenLanguage;
    public AudioSource schangeSceneSound;
    public AudioSource commonPushedSound;
    //public AudioSource cancelOrCloseSound;

    //settings panel properties
    [SerializeField]
    private GameObject settingsPanel;
    [SerializeField]
    private GameObject settingsPanelBack;
    private GameObject closeSettingsPanelButton;
    private RawImage settingsPanelRawImg;
    private Material holoMatsToFade; //material of RawImg component of settings panel GO
    private bool fadeIn;
    private int ColorIntencity;
    private bool fadeOut;
    private float fade;

    //shop panel properties
    [SerializeField]
    private GameObject shopPanel;
    private RawImage shopPanelRawImg;
    private GameObject closeShopPanelButton;
    private float RenderingLvl;
    private Material holoMatsToFadeShop; //material of RawImg component of settings panel GO
    private bool isShopPanelOpening;

    //this collectsion holds the references to all levels UI on MenuMngr
    [SerializeField]
    private List<GameObject> levelButtons;
    [SerializeField]
    private List<Sprite> levelSprites;

    //the serialized properties to change the graphics settings of the game
    [SerializeField]
    private Slider graphicsSettingsSlider;
    [SerializeField]
    private Text graphicsTxt;
    [SerializeField]
    private RenderPipelineAsset lowQuality;
    [SerializeField]
    private RenderPipelineAsset midQuality;
    [SerializeField]
    private RenderPipelineAsset highQuality;
    private bool MidQuality;
    private int lvlToContinue;


    //the serialized properties to change the graphics settings of the game
    [SerializeField]
    private Slider musicSettingsSlider;
    [SerializeField]
    private Text musicTxt;
    [SerializeField]
    private Slider soundSettingsSlider;
    [SerializeField]
    private Text soundsTxt;
    [SerializeField]
    private AudioMixer audioMixer;

    //this toggles list is used to assign a color to player

    [SerializeField]
    private List<Toggle> playerColor;
    [SerializeField]
    private Text playerColorTxt;
    private int colorTaggleOnIndex = 0;

    [SerializeField]
    private Text unlockText;
    [SerializeField]
    private List<Text> noAdsTxt;
    [SerializeField]
    private List<Text> StartKitTxt;

    [SerializeField]
    private GameObject deleteSavedGamePanel;
    private Text deleteSavedGameTxt;
    private float OcclusionDistance;
    private bool thereIsASavedGame;
    private int intLvl;

    //these buttons are in IAP suggestion buttons
    //0-buy all lvls; 1-buy Kit1; 2- buy Kit2; 3-BuyKit3; 4-buy Kit2 after Kit1; 5-buy Kit3 after Kit1; 6-buy Kit3 after Kit2
    [SerializeField]
    private Button [] buyButtons;

    //start kit of player
    //private bool soundExtraPlayerHas;
    private float soundExtraVolume;
    private float soundMixerExtra;
    private int destr4ExtraKit;
    [SerializeField]
    private Toggle useAKitToggle;
    [SerializeField]
    private Text useAKitText;


    //rate us count features properties
    private int switchToGameCounts;
    private bool rateOff;
    [SerializeField]
    private GameObject rateUsPanel;
    [SerializeField]
    private SwipeMenu swipeMngr;


    // Start is called before the first frame update
    void Start()
    {
        deleteSavedGameTxt = deleteSavedGamePanel.transform.GetChild(0).GetComponent<Text>();
        deleteSavedGameTxt.text = Constants.Instance.getDeledSavedGame();
        thereIsASavedGame = false;

        clearingTheGameParameters();

        //so if the saved file exist it LoadLvlDataFromFile func returns the saved level and here the continue button is activated of level
        //- 1 is because levels start from 1 and the collection count starts from 0
        if (File.Exists(Application.persistentDataPath + "/" + fileName + ".art"))
        {
            lvlToContinue = LoadLvlDataFromFile();
            levelButtons[lvlToContinue - 1].transform.GetChild(1).gameObject.SetActive(true);
            swipeMngr.setSavedLevel(lvlToContinue - 1);
            thereIsASavedGame = true;
        }

        //loading player pref settings if they exist, otherwise setting the defauld settings
        if (File.Exists(Application.persistentDataPath + "/" + fileNamePrefs + ".art"))
        {
            LoadPrefsFromFile();
            playerColor[colorTaggleOnIndex].isOn = true;
            Lists.playerColor = colorTaggleOnIndex;
            //this one here as an attempt to prevent a bug of game crash (in weak device like galaxy tab 10 it crashes before I change the quality manually)
            GraphicsSettings.renderPipelineAsset = midQuality;
            QualitySettings.renderPipeline = midQuality;
            graphicsChange(); //updating the graphics after loading the data from file
        }
        else
        {
            graphicsSettingsSlider.value = 1;

            GraphicsSettings.renderPipelineAsset = midQuality;
            QualitySettings.renderPipeline = midQuality;
            musicSettingsSlider.value = -20;
            soundSettingsSlider.value = 0;

            colorTaggleOnIndex = 0;
            playerColor[colorTaggleOnIndex].isOn = true;
            Lists.playerColor = colorTaggleOnIndex;
        }


        if (OcclusionDistance == 7.77f && ColorIntencity == 19 && MidQuality == true && RenderingLvl == 29.04f)
        {
            buyButtons[0].interactable = false;
        }
        if (soundExtraVolume == 50)
        {
            buyButtons[1].interactable = false;

            buyButtons[2].gameObject.SetActive(false); //so if 1 kit bought the 2 kit will be cheaper and the buttons will be changed one will be true and second false
            buyButtons[4].gameObject.SetActive(true); //so if 1 kit bought the 2 kit will be cheaper and the buttons will be changed

            buyButtons[3].gameObject.SetActive(false); //so if 1 kit bought the 3 kit will be cheaper and the buttons will be changed one will be true and second false
            buyButtons[5].gameObject.SetActive(true); //so if 1 kit bought the 3 kit will be cheaper and the buttons will be changed

            useAKitToggle.gameObject.SetActive(true); //to be able to use bought kit while starting the new game
        }
        else if (soundExtraVolume == 100) {
            buyButtons[1].interactable = false;
            buyButtons[2].interactable = false;
            buyButtons[4].interactable = false;

            buyButtons[3].gameObject.SetActive(false); //so if 1 kit bought the 2 kit will be cheaper and the buttons will be changed one will be true and second false
            buyButtons[5].gameObject.SetActive(false); //so if 1 kit bought the 2 kit will be cheaper and the buttons will be changed one will be true and second false
            buyButtons[6].gameObject.SetActive(true); //so if 1 kit bought the 2 kit will be cheaper and the buttons will be changed

            useAKitToggle.gameObject.SetActive(true);
        }
        else if (soundExtraVolume == 200)
        {
            buyButtons[1].interactable = false;
            buyButtons[2].interactable = false;
            buyButtons[3].interactable = false;
            buyButtons[4].interactable = false;
            buyButtons[5].interactable = false;
            buyButtons[6].interactable = false;
            useAKitToggle.gameObject.SetActive(true);
        }


        if (soundExtraVolume>0 || (OcclusionDistance == 7.77f && ColorIntencity == 19 && MidQuality == true && RenderingLvl == 29.04f)) Lists.adsBought = true;

        //activating new game buttons and setting decent level sprite
        int y = 1;
        for (int i = 0; i < ElegeIdenSani(); i++)
        {
            if (OcclusionDistance == 7.77f && ColorIntencity == 19 && MidQuality == true && RenderingLvl == 29.04f)
            {
                levelButtons[ElegeIdenSani() - y].transform.GetChild(0).gameObject.SetActive(true);
                levelButtons[ElegeIdenSani() - y].GetComponent<Image>().sprite = levelSprites[ElegeIdenSani() - y];
                y++;
            }
            else
            {
                if ((ElegeIdenSani() - y) == 3)
                {
                    //сатып алу кнопкасын кушу сатып алынмаган булса
                    levelButtons[ElegeIdenSani() - y].transform.GetChild(3).gameObject.SetActive(true);
                    levelButtons[ElegeIdenSani() - y].GetComponent<Image>().sprite = levelSprites[ElegeIdenSani() - y];
                }
                else if ((ElegeIdenSani() - y) < 3)
                {
                    levelButtons[ElegeIdenSani() - y].transform.GetChild(0).gameObject.SetActive(true);
                    levelButtons[ElegeIdenSani() - y].GetComponent<Image>().sprite = levelSprites[ElegeIdenSani() - y];
                }
                y++;
            }

        }
        Lists.alinganUroven = ElegeIdenSani();

        //so the rate us panel is activated only if it was not turned off earlier and the turns to game is 5 or more



#if UNITY_IOS
        Device.RequestStoreReview();
#else
        if (switchToGameCounts > 5 && !rateOff) rateUsPanel.SetActive(true);
#endif


        //setting the market prices fro IAP products and after update the texts of buttons (inside the coroutine)
        StartCoroutine(setTheMarketPrices());

        settingsPanelRawImg = settingsPanel.GetComponent<RawImage>();
        holoMatsToFade = settingsPanelRawImg.material;
        closeSettingsPanelButton = settingsPanel.transform.GetChild(0).gameObject;

        shopPanelRawImg = shopPanel.GetComponent<RawImage>();
        holoMatsToFadeShop = settingsPanelRawImg.material;
        closeShopPanelButton = shopPanel.transform.GetChild(0).gameObject;

        holoMatsToFade.SetColor("_MainColor", new Color(0, 1, 1, 0)); //setting to holo mat cyan color cause it is changed while game

        //setting the chosen value of langage chosen prior by player
        if (Lists.isEnglish) dropdown.SetValueWithoutNotify(0);
        else if (Lists.isRussian) dropdown.SetValueWithoutNotify(1);
        else if (Lists.isGerman) dropdown.SetValueWithoutNotify(2);
        else if (Lists.isSpanish) dropdown.SetValueWithoutNotify(3);
        else dropdown.SetValueWithoutNotify(4);
        changeTheWordLang(); //updating the text on UI depending on choosen lang
    }

    //closing rate us panel buttons methods 0-closeWithNoRate 1-LaterButton 2-rateButton
    public void CloseRateUsPanelOn(int buttonIndex)
    {
        if (buttonIndex == 0) {
            rateOff = true;
            rateUsPanel.SetActive(false);
        }
        else if (buttonIndex == 1)
        {
            switchToGameCounts = 0; //starting the count process again
            rateUsPanel.SetActive(false);
        }
        else if (buttonIndex == 2)
        {
            Application.OpenURL("market://details?id=com.Art.U.R.Admiral");
            rateOff = true;
            rateUsPanel.SetActive(false);
        }
        savePlayerPrefs();
    }

    //this method is used to set zero all game parameters to load them from saved file to continue the game or start a new game from zero point
    //this is neessary if player quits the game from journey scene to menu scne
    private void clearingTheGameParameters() {
        Lists.emptyStations.Clear();
        Lists.AllStations.Clear();
        Lists.playerStations.Clear();
        Lists.CPU1Stations.Clear();
        Lists.CPU2Stations.Clear();
        Lists.CPU3Stations.Clear();
        Lists.CPU4Stations.Clear();
        Lists.CPUGuardStations.Clear();
        Lists.shipsOnScene.Clear();
        Lists.AllSceneObjects.Clear();
        Lists.AllSceneObjects.Clear();
        Lists.ClearFleetOfPlayerCruis();
        Lists.energonsOnScene.Clear();
        Lists.energonsControllablesOnScene.Clear();
        //Lists.CPU1SceneCruis = null;
        //Lists.CPU2SceneCruis = null;
        //Lists.CPU3SceneCruis = null;
        //Lists.CPU4SceneCruis = null;

        Lists.CPU1CruisersOnScene = 0;
        Lists.CPU2CruisersOnScene = 0;
        Lists.CPU3CruisersOnScene = 0;
        Lists.CPU4CruisersOnScene = 0;
        Lists.CPUGCruisersOnScene = 0;
        Lists.enemyStationsOnScene = 0;
    }
     private int ElegeIdenSani() {
        if (File.Exists(Application.persistentDataPath + "/" + fileNameDaraga + ".art"))
        {
            string[] rows = File.ReadAllLines(Application.persistentDataPath + "/" + fileNameDaraga + ".art");
            int elegeIdenSani;
            if (int.TryParse(getSavedValue(rows, "elegeIdenBieclege"), out elegeIdenSani))
            {
                if (elegeIdenSani > 10) elegeIdenSani--; //to prevent argument out of range exception on start for loop while checking the achieved level 
                return elegeIdenSani;
            }
            else return 1;
        }
        else return 1;
    }

    //crypting method for saved data
    string Crypt(string text)
    {
        string result = string.Empty;
        foreach (char j in text)
        {
            // ((int) j ^ 29) - применение XOR к номеру символа
            // (char)((int) j ^ 29) - получаем символ из измененного номера
            // Число, которым мы XORим можете поставить любое. Эксперементируйте.
            result += (char)(j ^ 29);
        }
        return result;
    }

    //this method is used to read save data from special file with key value approach
    private string getSavedValue(string[] line, string pattern)
    {
        string result = "";
        foreach (string key in line)
        {
            if (key.Trim() != string.Empty)
            {
                string value = key;
                value = Crypt(key);

                if (pattern == value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0])
                {
                    result = value.Remove(0, value.IndexOf(' ') + 1);
                }
            }
        }
        return result;
    }

    //saving preferences of player
    private void savePlayerPrefs()
    {
        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/" + fileNamePrefs + ".art");
        string sp = " "; //space 

        sw.WriteLine(Crypt("isEnglish" + sp + Lists.isEnglish));
        sw.WriteLine(Crypt("isGerman" + sp + Lists.isGerman));
        sw.WriteLine(Crypt("isSpanish" + sp + Lists.isSpanish));
        sw.WriteLine(Crypt("isRussian" + sp + Lists.isRussian));
        sw.WriteLine(Crypt("isChinees" + sp + Lists.isChinees));

        //saving data for battle
        sw.WriteLine(Crypt("currentLevelDifficulty" + sp + Lists.currentLevelDifficulty));
        sw.WriteLine(Crypt("energyOfPlayer" + sp + Lists.energyOfPlayer));
        //sw.WriteLine(Crypt("boosterOfPlayer" + sp + Lists.boosterOfPlayer));
        sw.WriteLine(Crypt("playerColor" + sp + Lists.playerColor));
        sw.WriteLine(Crypt("MidQuality" + sp + MidQuality));
        sw.WriteLine(Crypt("elegeIdenBieclege" + sp + (Lists.currentLevel + 1)));
        sw.WriteLine(Crypt("Destr4Index" + sp + Constants.Instance.Destr4Index));
        sw.WriteLine(Crypt("Destr1Index" + sp + Constants.Instance.Destr1Index));
        sw.WriteLine(Crypt("shipsPreLimit" + sp + Constants.Instance.shipsPreLimit));
        sw.WriteLine(Crypt("shipsLimit" + sp + Constants.Instance.shipsLimit));
        sw.WriteLine(Crypt("soundExtraVolume" + sp + soundExtraVolume));
        sw.WriteLine(Crypt("soundMixerExtra" + sp + soundMixerExtra));

        //saving the information about player's cruiser fleet
        sw.WriteLine(Crypt("Cruis1OfPlayerCruis" + sp + Lists.Cruis1OfPlayerCruis));
        sw.WriteLine(Crypt("Cruis2OfPlayerCruis" + sp + Lists.Cruis2OfPlayerCruis));
        sw.WriteLine(Crypt("Cruis3OfPlayerCruis" + sp + Lists.Cruis3OfPlayerCruis));
        sw.WriteLine(Crypt("Cruis4OfPlayerCruis" + sp + Lists.Cruis4OfPlayerCruis));
        sw.WriteLine(Crypt("Destr1OfPlayerCruis" + sp + Lists.Destr1OfPlayerCruis));
        sw.WriteLine(Crypt("Destr1OfPlayerParCruis" + sp + Lists.Destr1OfPlayerParCruis));
        sw.WriteLine(Crypt("Destr2OfPlayerCruis" + sp + Lists.Destr2OfPlayerCruis));
        sw.WriteLine(Crypt("Destr2OfPlayerParCruis" + sp + Lists.Destr2OfPlayerParCruis));
        sw.WriteLine(Crypt("Destr3OfPlayerCruis" + sp + Lists.Destr3OfPlayerCruis));
        sw.WriteLine(Crypt("Destr4OfPlayerCruis" + sp + Lists.Destr4OfPlayerCruis));
        sw.WriteLine(Crypt("Gun1OfPlayerCruis" + sp + Lists.Gun1OfPlayerCruis));
        sw.WriteLine(Crypt("Gun2OfPlayerCruis" + sp + Lists.Gun2OfPlayerCruis));
        sw.WriteLine(Crypt("Gun3OfPlayerCruis" + sp + Lists.Gun3OfPlayerCruis));
        sw.WriteLine(Crypt("MiniGunOfPlayerCruis" + sp + Lists.MiniGunOfPlayerCruis));
        //sw.WriteLine(Crypt("boostrGainDark" + sp + Constants.Instance.boostrGainDark));

        //saving the fleet of scene cruisers of CPU
        //sw.WriteLine(Crypt("energyOfCPU1" + sp + Lists.energyOfCPU1));
        //sw.WriteLine(Crypt("Cruis1OfCPU1" + sp + Lists.Cruis1OfCPU1));
        //sw.WriteLine(Crypt("Cruis2OfCPU1" + sp + Lists.Cruis2OfCPU1));
        //sw.WriteLine(Crypt("Cruis3OfCPU1" + sp + Lists.Cruis3OfCPU1));
        //sw.WriteLine(Crypt("Cruis4OfCPU1" + sp + Lists.Cruis4OfCPU1));
        //sw.WriteLine(Crypt("Destr1OfCPU1" + sp + Lists.Destr1OfCPU1));
        //sw.WriteLine(Crypt("Destr1OfCPU1Par" + sp + Lists.Destr1OfCPU1Par));
        //sw.WriteLine(Crypt("Destr2OfCPU1" + sp + Lists.Destr2OfCPU1));
        //sw.WriteLine(Crypt("Destr2OfCPU1Par" + sp + Lists.Destr2OfCPU1Par));
        sw.WriteLine(Crypt("OcclusionDistance" + sp + OcclusionDistance));
        //sw.WriteLine(Crypt("Destr3OfCPU1" + sp + Lists.Destr3OfCPU1));
        //sw.WriteLine(Crypt("Destr4OfCPU1" + sp + Lists.Destr4OfCPU1));
        //sw.WriteLine(Crypt("Gun1OfCPU1" + sp + Lists.Gun1OfCPU1));
        //sw.WriteLine(Crypt("Gun2OfCPU1" + sp + Lists.Gun2OfCPU1));
        //sw.WriteLine(Crypt("Gun3OfCPU1" + sp + Lists.Gun3OfCPU1));
        sw.WriteLine(Crypt("RenderingLvl" + sp + RenderingLvl));
        //sw.WriteLine(Crypt("FighterOfCPU1" + sp + Lists.FighterOfCPU1));

        //sw.WriteLine(Crypt("energyOfCPU2" + sp + Lists.energyOfCPU2));
        //sw.WriteLine(Crypt("Cruis1OfCPU2" + sp + Lists.Cruis1OfCPU2));
        //sw.WriteLine(Crypt("Cruis2OfCPU2" + sp + Lists.Cruis2OfCPU2));
        //sw.WriteLine(Crypt("Cruis3OfCPU2" + sp + Lists.Cruis3OfCPU2));
        //sw.WriteLine(Crypt("Cruis4OfCPU2" + sp + Lists.Cruis4OfCPU2));
        //sw.WriteLine(Crypt("Destr1OfCPU2" + sp + Lists.Destr1OfCPU2));
        //sw.WriteLine(Crypt("Destr1OfCPU2Par" + sp + Lists.Destr1OfCPU2Par));
        //sw.WriteLine(Crypt("Destr2OfCPU2" + sp + Lists.Destr2OfCPU2));
        //sw.WriteLine(Crypt("Destr2OfCPU2Par" + sp + Lists.Destr2OfCPU2Par));
        //sw.WriteLine(Crypt("Destr3OfCPU2" + sp + Lists.Destr3OfCPU2));
        //sw.WriteLine(Crypt("Destr4OfCPU2" + sp + Lists.Destr4OfCPU2));
        //sw.WriteLine(Crypt("Gun1OfCPU2" + sp + Lists.Gun1OfCPU2));
        //sw.WriteLine(Crypt("Gun2OfCPU2" + sp + Lists.Gun2OfCPU2));
        //sw.WriteLine(Crypt("Gun3OfCPU2" + sp + Lists.Gun3OfCPU2));
        //sw.WriteLine(Crypt("FighterOfCPU2" + sp + Lists.FighterOfCPU2));
        sw.WriteLine(Crypt("destr4ExtraKit" + sp + destr4ExtraKit));
        sw.WriteLine(Crypt("switchToGameCounts" + sp + switchToGameCounts));
        sw.WriteLine(Crypt("rateOff" + sp + rateOff));

        sw.WriteLine(Crypt("GraphicsValue" + sp + graphicsSettingsSlider.value));
        sw.WriteLine(Crypt("MusicValue" + sp + musicSettingsSlider.value));
        sw.WriteLine(Crypt("ColorIntencity" + sp + ColorIntencity));
        sw.WriteLine(Crypt("SoundValue" + sp + soundSettingsSlider.value));
        sw.WriteLine(Crypt("PlayerColorValue" + sp + colorTaggleOnIndex));

        sw.Close();
    }

    //load player prefs on start from file
    private void LoadPrefsFromFile()
    {
        string[] rows = File.ReadAllLines(Application.persistentDataPath + "/" + fileNamePrefs + ".art");

        bool isEnglish;
        if (bool.TryParse(getSavedValue(rows, "isEnglish"), out isEnglish)) Lists.isEnglish = isEnglish;
        bool isGerman;
        if (bool.TryParse(getSavedValue(rows, "isGerman"), out isGerman)) Lists.isGerman = isGerman;
        bool isSpanish;
        if (bool.TryParse(getSavedValue(rows, "isSpanish"), out isSpanish)) Lists.isSpanish = isSpanish;
        bool isRussian;
        if (bool.TryParse(getSavedValue(rows, "isRussian"), out isRussian)) Lists.isRussian = isRussian;
        bool isChinees;
        if (bool.TryParse(getSavedValue(rows, "isChinees"), out isChinees)) Lists.isChinees = isChinees;
        float RenderingLvlLocal;
        if (float.TryParse(getSavedValue(rows, "RenderingLvl"), out RenderingLvlLocal)) RenderingLvl = RenderingLvlLocal;
        float GraphicsValue;
        if (float.TryParse(getSavedValue(rows, "GraphicsValue"), out GraphicsValue)) graphicsSettingsSlider.value = GraphicsValue;
        float MusicValue;
        if (float.TryParse(getSavedValue(rows, "MusicValue"), out MusicValue)) musicSettingsSlider.value = MusicValue;
        float SoundValue;
        if (float.TryParse(getSavedValue(rows, "SoundValue"), out SoundValue)) soundSettingsSlider.value = SoundValue;
        int PlayerColorValue;
        if (int.TryParse(getSavedValue(rows, "PlayerColorValue"), out PlayerColorValue)) colorTaggleOnIndex = PlayerColorValue;
        float OcclusionDistanceLocal;
        if (float.TryParse(getSavedValue(rows, "OcclusionDistance"), out OcclusionDistanceLocal)) OcclusionDistance = OcclusionDistanceLocal;
        float soundExtraVolumeLocal;
        if (float.TryParse(getSavedValue(rows, "soundExtraVolume"), out soundExtraVolumeLocal)) soundExtraVolume = soundExtraVolumeLocal;
        float soundMixerExtraLocal;
        if (float.TryParse(getSavedValue(rows, "soundMixerExtra"), out soundMixerExtraLocal)) soundMixerExtra = soundMixerExtraLocal; 
        int ColorIntencityLocal;
        if (int.TryParse(getSavedValue(rows, "ColorIntencity"), out ColorIntencityLocal)) ColorIntencity = ColorIntencityLocal;
        int destr4ExtraKitLocal;
        if (int.TryParse(getSavedValue(rows, "destr4ExtraKit"), out destr4ExtraKitLocal)) destr4ExtraKit = destr4ExtraKitLocal;
        bool MidQualityLocal;
        if (bool.TryParse(getSavedValue(rows, "MidQuality"), out MidQualityLocal)) MidQuality = MidQualityLocal;
        bool rateOffLocal;
        if (bool.TryParse(getSavedValue(rows, "rateOff"), out rateOffLocal)) rateOff = rateOffLocal;
        int switchToGameCountsLocal;
        if (int.TryParse(getSavedValue(rows, "switchToGameCounts"), out switchToGameCountsLocal)) switchToGameCounts = switchToGameCountsLocal;
    }

    //method is use the name of key (from saving method in each scene (method are the same)) and return back the value wich is specially sepraeted from key word by method getSavedValue();
    private int LoadLvlDataFromFile()
    {
        string[] rows = File.ReadAllLines(Application.persistentDataPath + "/" + fileName + ".art");

        int currentLevel;
        if (int.TryParse(getSavedValue(rows, "currentLevel"), out currentLevel)) return currentLevel;
        else return 1; //so if there is no level to return by default this func will return 1 level to loead it;
    }

    //this one is used to get saved data from file which is already can be used from this point
    private void LoadSavedDataFromFile()
    {
        string[] rows = File.ReadAllLines(Application.persistentDataPath + "/" + fileName + ".art");

        int currentLevel;
        if (int.TryParse(getSavedValue(rows, "currentLevel"), out currentLevel)) Lists.currentLevel = currentLevel;
        int currentLevelDifficulty;
        if (int.TryParse(getSavedValue(rows, "currentLevelDifficulty"), out currentLevelDifficulty)) Lists.currentLevelDifficulty = currentLevelDifficulty;

        float energyOfPlayer;
        if (float.TryParse(getSavedValue(rows, "energyOfPlayer"), out energyOfPlayer)) Lists.energyOfPlayer = energyOfPlayer;
        //float boosterOfPlayer;
        //if (float.TryParse(getSavedValue(rows, "boosterOfPlayer"), out boosterOfPlayer)) Lists.boosterOfPlayer = boosterOfPlayer;
        int playerColor;
        if (int.TryParse(getSavedValue(rows, "playerColor"), out playerColor)) Lists.playerColor = playerColor;
    }

    //this one is for calling the graphics settings panel (satyp aly metody)
    public void SetDefaultGraphics()
    {
        commonPushedSound.Play();
        OcclusionDistance = 7.77f;
        ColorIntencity = 19;
        MidQuality = true;
        RenderingLvl = 29.04f;
        if (ElegeIdenSani()==4) levelButtons[3].transform.GetChild(0).gameObject.SetActive(true);
        levelButtons[3].transform.GetChild(3).gameObject.SetActive(false);
        Lists.adsBought = true;
        buyButtons[0].interactable = false;
        buyButtons[0].transform.GetChild(0).GetComponent<Text>().text = Constants.Instance.getBought();
        savePlayerPrefs(); //saving the prefs of which the language is part and the graphic settings
    }

    //this one is for calling the graphics settings panel (satyp aly metody)
    public void SetMaximumGraphics(int index)
    {
        commonPushedSound.Play();
        if (index == 1) {
            soundExtraVolume = 50;
            soundMixerExtra = 15;
            destr4ExtraKit = 3;

            buyButtons[2].gameObject.SetActive(false); //so if 1 kit bought the 2 kit will be cheaper and the buttons will be changed one will be true and second false
            buyButtons[4].gameObject.SetActive(true); //so if 1 kit bought the 2 kit will be cheaper and the buttons will be changed

            buyButtons[3].gameObject.SetActive(false); //so if 1 kit bought the 3 kit will be cheaper and the buttons will be changed one will be true and second false
            buyButtons[5].gameObject.SetActive(true); //so if 1 kit bought the 3 kit will be cheaper and the buttons will be changed
        }
        if (index == 2)
        {
            soundExtraVolume = 100;
            soundMixerExtra = 30;
            destr4ExtraKit = 6;
            //uenchi ostegerek naborny styp algan iken astagyrak naborny styp alyngan bulyp kursetele
            buyButtons[index - 1].transform.GetChild(0).GetComponent<Text>().text = Constants.Instance.getBought();
            buyButtons[index - 1].interactable = false;

            buyButtons[4].transform.GetChild(0).GetComponent<Text>().text = Constants.Instance.getBought();
            buyButtons[4].interactable = false;

            buyButtons[3].gameObject.SetActive(false); //so if 1 kit bought the 2 kit will be cheaper and the buttons will be changed one will be true and second false
            buyButtons[5].gameObject.SetActive(false); //so if 1 kit bought the 2 kit will be cheaper and the buttons will be changed one will be true and second false
            buyButtons[6].gameObject.SetActive(true); //so if 1 kit bought the 2 kit will be cheaper and the buttons will be changed
        }
        if (index == 3)
        {
            soundExtraVolume = 200;
            soundMixerExtra = 50;
            destr4ExtraKit = 12;
            //uenchi ostegerek naborny styp algan iken astagyrak naborny styp alyngan bulyp kursetele
            buyButtons[index - 1].transform.GetChild(0).GetComponent<Text>().text = Constants.Instance.getBought();
            buyButtons[index - 1].interactable = false;
            //uenchi ostegerek naborny styp algan iken astagyrak naborny styp alyngan bulyp kursetele
            buyButtons[index-2].transform.GetChild(0).GetComponent<Text>().text = Constants.Instance.getBought();
            buyButtons[index-2].interactable = false;

            buyButtons[4].transform.GetChild(0).GetComponent<Text>().text = Constants.Instance.getBought();
            buyButtons[4].interactable = false;
            buyButtons[5].transform.GetChild(0).GetComponent<Text>().text = Constants.Instance.getBought();
            buyButtons[5].interactable = false;
            buyButtons[6].transform.GetChild(0).GetComponent<Text>().text = Constants.Instance.getBought();
            buyButtons[6].interactable = false;
        }
        buyButtons[index].transform.GetChild(0).GetComponent<Text>().text = Constants.Instance.getBought();
        buyButtons[index].interactable = false;
        Lists.adsBought = true;
        useAKitToggle.gameObject.SetActive(true);
        savePlayerPrefs(); 
    }

    //set the prices of goods
    private IEnumerator setTheMarketPrices() {
        while (!IAPMgr.Instance.IsInitialized()) yield return null;

        buyButtons[0].transform.GetChild(0).GetComponent<Text>().text = IAPMgr.Instance.getProductPriceFromStore(IAPMgr.Instance.all_levels);
        buyButtons[1].transform.GetChild(0).GetComponent<Text>().text = IAPMgr.Instance.getProductPriceFromStore(IAPMgr.Instance.kit_1);
        buyButtons[2].transform.GetChild(0).GetComponent<Text>().text = IAPMgr.Instance.getProductPriceFromStore(IAPMgr.Instance.kit_2);
        buyButtons[3].transform.GetChild(0).GetComponent<Text>().text = IAPMgr.Instance.getProductPriceFromStore(IAPMgr.Instance.kit_3);
        buyButtons[4].transform.GetChild(0).GetComponent<Text>().text = IAPMgr.Instance.getProductPriceFromStore(IAPMgr.Instance.kit_22);
        buyButtons[5].transform.GetChild(0).GetComponent<Text>().text = IAPMgr.Instance.getProductPriceFromStore(IAPMgr.Instance.kit_33);
        buyButtons[6].transform.GetChild(0).GetComponent<Text>().text = IAPMgr.Instance.getProductPriceFromStore(IAPMgr.Instance.kit_333);

        changeTheWordLang();
    }

    //changing the words while the languege is changed by player
    private void changeTheWordLang()
    {
        for (int i = 0; i < levelButtons.Count; i++)
        {
            levelButtons[i].transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = Constants.Instance.getNewGame();
            levelButtons[i].transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = Constants.Instance.getContinue();
            if (levelButtons[i].name.Contains("Level1") || levelButtons[i].name.Contains("Level2"))
                levelButtons[i].transform.GetChild(2).GetComponent<Text>().text = Constants.Instance.getEasy();
            else if (levelButtons[i].name.Contains("Level3") || levelButtons[i].name.Contains("Level4") || levelButtons[i].name.Contains("Level6") || levelButtons[i].name.Contains("Level8"))
                levelButtons[i].transform.GetChild(2).GetComponent<Text>().text = Constants.Instance.getNormal();
            else if (levelButtons[i].name.Contains("Level5") || levelButtons[i].name.Contains("Level7") || levelButtons[i].name.Contains("Level9") || levelButtons[i].name.Contains("Level01"))
                levelButtons[i].transform.GetChild(2).GetComponent<Text>().text = Constants.Instance.getHard();
        }
        levelButtons[3].transform.GetChild(3).transform.GetChild(0).GetComponent<Text>().text = Constants.Instance.getPurchase();
        playerColorTxt.text = Constants.Instance.getPlayerColor();
        soundsTxt.text = Constants.Instance.getSounds();
        musicTxt.text = Constants.Instance.getMusic();
        graphicsTxt.text = Constants.Instance.getGraphics();
        unlockText.text = Constants.Instance.getUnlockAll();
        deleteSavedGameTxt.text = Constants.Instance.getDeledSavedGame();
        foreach (Text tx in noAdsTxt) tx.text = Constants.Instance.getNoAds();
        foreach (Text tx in StartKitTxt) tx.text = Constants.Instance.getStartkit();
        if (OcclusionDistance == 7.77f && ColorIntencity == 19 && MidQuality == true && RenderingLvl == 29.04f)
        {
            buyButtons[0].transform.GetChild(0).GetComponent<Text>().text = Constants.Instance.getBought();
        }
        if (soundExtraVolume == 50)
        {
            buyButtons[1].transform.GetChild(0).GetComponent<Text>().text = Constants.Instance.getBought();
            useAKitText.text = Constants.Instance.getStartkit();
        }
        else if (soundExtraVolume == 100)
        {
            buyButtons[1].transform.GetChild(0).GetComponent<Text>().text = Constants.Instance.getBought();
            buyButtons[2].transform.GetChild(0).GetComponent<Text>().text = Constants.Instance.getBought();
            buyButtons[4].transform.GetChild(0).GetComponent<Text>().text = Constants.Instance.getBought();
            useAKitText.text = Constants.Instance.getStartkit();
        }
        else if (soundExtraVolume == 200)
        {
            buyButtons[1].transform.GetChild(0).GetComponent<Text>().text = Constants.Instance.getBought();
            buyButtons[2].transform.GetChild(0).GetComponent<Text>().text = Constants.Instance.getBought();
            buyButtons[3].transform.GetChild(0).GetComponent<Text>().text = Constants.Instance.getBought();
            buyButtons[4].transform.GetChild(0).GetComponent<Text>().text = Constants.Instance.getBought();
            buyButtons[5].transform.GetChild(0).GetComponent<Text>().text = Constants.Instance.getBought();
            buyButtons[6].transform.GetChild(0).GetComponent<Text>().text = Constants.Instance.getBought();
            useAKitText.text = Constants.Instance.getStartkit();
        }
    }

    //this one called publicly from the dropdown UI and the index order is next 
    //0 - english, 1 - russian, 2 - German, 3- Spanish, 4-Chinese
    public void languageChange(int indexOfLang) {
        if (indexOfLang == 0)
        {
            Lists.isEnglish = true;
            Lists.isRussian = false;
            Lists.isGerman = false;
            Lists.isSpanish = false;
            Lists.isChinees = false;
        }
        else if (indexOfLang == 1)
        {
            Lists.isEnglish = false;
            Lists.isRussian = true;
            Lists.isGerman = false;
            Lists.isSpanish = false;
            Lists.isChinees = false;
        }
        else if (indexOfLang == 2)
        {
            Lists.isEnglish = false;
            Lists.isRussian = false;
            Lists.isGerman = true;
            Lists.isSpanish = false;
            Lists.isChinees = false;
        }
        else if (indexOfLang == 3)
        {
            Lists.isEnglish = false;
            Lists.isRussian = false;
            Lists.isGerman = false;
            Lists.isSpanish = true;
            Lists.isChinees = false;
        }
        else if (indexOfLang == 4)
        {
            Lists.isEnglish = false;
            Lists.isRussian = false;
            Lists.isGerman = false;
            Lists.isSpanish = false;
            Lists.isChinees = true;
        }
        changeTheWordLang(); //updating the text on UI depending on choosen lang
        commonPushedSound.Play();
        savePlayerPrefs(); //saving the prefs of which the language is part
    }

    //the method to choose the color of player
    public void changeThePlayerColor(int indexOfLang)
    {
        if (colorTaggleOnIndex != indexOfLang)
        {
            commonPushedSound.Play();
            playerColor[colorTaggleOnIndex].enabled = true;
            playerColor[colorTaggleOnIndex].isOn = false;
            colorTaggleOnIndex = indexOfLang;
            playerColor[indexOfLang].enabled = false;
        }
        Lists.playerColor = indexOfLang;
    }

    //this func is called publicly by pushing the buttons of levels from menu scene
    public void startTheLevel(int level/*string level*/) {
        if (thereIsASavedGame) {
            deleteSavedGamePanel.SetActive(true);
            commonPushedSound.Play();
            intLvl = level;
        }
        else {
            int startKitModifier;
            if (useAKitToggle.IsActive() && useAKitToggle.isOn) startKitModifier = 1;
            else startKitModifier = 0;
            intLvl = level /*int.Parse(level)*/;
            Lists.currentLevel = intLvl;
            if (intLvl < 4)
            {
                Lists.isBlackDimension = true;
                Lists.isBlueDimension = false;
                Lists.isRedDimension = false;
                Lists.energyOfPlayer = 250 + soundExtraVolume * startKitModifier;//250
                //Lists.boosterOfPlayer = 20 + soundMixerExtra * startKitModifier;

                Lists.Cruis1OfPlayerCruis = 0;
                Lists.Cruis2OfPlayerCruis = 0;
                Lists.Cruis3OfPlayerCruis = 0;
                Lists.Cruis4OfPlayerCruis = 1;
                Lists.Destr1OfPlayerCruis = 0;
                Lists.Destr1OfPlayerParCruis = 0;
                Lists.Destr2OfPlayerCruis = 0;
                Lists.Destr2OfPlayerParCruis = 0;
                Lists.Destr3OfPlayerCruis = 0;
                Lists.Destr4OfPlayerCruis = 3+ destr4ExtraKit * startKitModifier;
                Lists.Gun1OfPlayerCruis = 0;
                Lists.Gun2OfPlayerCruis = 0;
                Lists.Gun3OfPlayerCruis = 0;
                Lists.MiniGunOfPlayerCruis = 0;
            }
            else if (intLvl < 8)
            {
                Lists.isBlackDimension = false;
                Lists.isBlueDimension = true;
                Lists.isRedDimension = false;
                Lists.energyOfPlayer = 300 + soundExtraVolume * startKitModifier;
                //Lists.boosterOfPlayer = 30 + soundMixerExtra * startKitModifier;

                Lists.Cruis1OfPlayerCruis = 0;
                Lists.Cruis2OfPlayerCruis = 0;
                Lists.Cruis3OfPlayerCruis = 0;
                Lists.Cruis4OfPlayerCruis = 1;
                Lists.Destr1OfPlayerCruis = 0;
                Lists.Destr1OfPlayerParCruis = 0;
                Lists.Destr2OfPlayerCruis = 0;
                Lists.Destr2OfPlayerParCruis = 0;
                Lists.Destr3OfPlayerCruis = 0;
                Lists.Destr4OfPlayerCruis = 3+ destr4ExtraKit * startKitModifier;
                Lists.Gun1OfPlayerCruis = 0;
                Lists.Gun2OfPlayerCruis = 0;
                Lists.Gun3OfPlayerCruis = 0;
                Lists.MiniGunOfPlayerCruis = 0;
            }
            else
            {
                Lists.isBlackDimension = false;
                Lists.isBlueDimension = false;
                Lists.isRedDimension = true;
                Lists.energyOfPlayer = 400 + soundExtraVolume * startKitModifier;
                //Lists.boosterOfPlayer = 35 + soundMixerExtra * startKitModifier;

                Lists.Cruis1OfPlayerCruis = 0;
                Lists.Cruis2OfPlayerCruis = 0;
                Lists.Cruis3OfPlayerCruis = 0;
                Lists.Cruis4OfPlayerCruis = 1;
                Lists.Destr1OfPlayerCruis = 0;
                Lists.Destr1OfPlayerParCruis = 0;
                Lists.Destr2OfPlayerCruis = 0;
                Lists.Destr2OfPlayerParCruis = 0;
                Lists.Destr3OfPlayerCruis = 0;
                Lists.Destr4OfPlayerCruis = 3 + destr4ExtraKit * startKitModifier;
                Lists.Gun1OfPlayerCruis = 0;
                Lists.Gun2OfPlayerCruis = 0;
                Lists.Gun3OfPlayerCruis = 0;
                Lists.MiniGunOfPlayerCruis = 0;
            }
            schangeSceneSound.Play();
            StartCoroutine(switchSceneWithPause());
        }
    }

    //this func is called publicly by pushing the buttons of continue levels from menu scene
    public void continueTheLevel()
    {
        Lists.currentLevel = lvlToContinue;
        Lists.isContinued = true;
        //loading some data for continued level
        LoadSavedDataFromFile();
        if (lvlToContinue < 4)
        {
            Lists.isBlackDimension = true;
            Lists.isBlueDimension = false;
            Lists.isRedDimension = false;
        }
        else if (lvlToContinue < 8)
        {
            Lists.isBlackDimension = false;
            Lists.isBlueDimension = true;
            Lists.isRedDimension = false;
        }
        else
        {
            Lists.isBlackDimension = false;
            Lists.isBlueDimension = false;
            Lists.isRedDimension = true;
        }
        schangeSceneSound.Play();
        StartCoroutine(switchSceneWithPause());
    }

    //this method is to close or start a new game depending on what button player pushed on delete a saved game panel. 
    //So if index 0 player just closed delete a saved game panel. If index 1 player pushed ok button on delete a saved game panel
    //the level is saved on startTheLevel ,ehod above and used here if player confirms the new game start
    public void deleteASavedGameRequest(int indexOfParameter) {
        if (indexOfParameter == 0)
        {
            deleteSavedGamePanel.SetActive(false);
            commonPushedSound.Play();
        }
        else {
            int startKitModifier;
            if (useAKitToggle.IsActive() && useAKitToggle.isOn) startKitModifier = 1;
            else startKitModifier = 0;
            Lists.currentLevel = intLvl;
            if (intLvl < 4)
            {
                Lists.isBlackDimension = true;
                Lists.isBlueDimension = false;
                Lists.isRedDimension = false;
                Lists.energyOfPlayer = 250 + soundExtraVolume * startKitModifier; //250
                //Lists.boosterOfPlayer = 20 + soundMixerExtra * startKitModifier;

                Lists.Cruis1OfPlayerCruis =0;
                Lists.Cruis2OfPlayerCruis = 0;
                Lists.Cruis3OfPlayerCruis = 0;
                Lists.Cruis4OfPlayerCruis = 1; //1
                Lists.Destr1OfPlayerCruis = 0;
                Lists.Destr1OfPlayerParCruis = 0;
                Lists.Destr2OfPlayerCruis = 0;
                Lists.Destr2OfPlayerParCruis = 0;
                Lists.Destr3OfPlayerCruis = 0;
                Lists.Destr4OfPlayerCruis = 3 + destr4ExtraKit * startKitModifier; //3
                Lists.Gun1OfPlayerCruis = 0;
                Lists.Gun2OfPlayerCruis = 0;
                Lists.Gun3OfPlayerCruis = 0;
                Lists.MiniGunOfPlayerCruis = 0;
            }
            else if (intLvl < 8)
            {
                Lists.isBlackDimension = false;
                Lists.isBlueDimension = true;
                Lists.isRedDimension = false;
                Lists.energyOfPlayer = 300 + soundExtraVolume * startKitModifier;
                //Lists.boosterOfPlayer = 30 + soundMixerExtra * startKitModifier;

                Lists.Cruis1OfPlayerCruis = 0;
                Lists.Cruis2OfPlayerCruis = 0;
                Lists.Cruis3OfPlayerCruis = 0;
                Lists.Cruis4OfPlayerCruis = 1;
                Lists.Destr1OfPlayerCruis = 0;
                Lists.Destr1OfPlayerParCruis = 0;
                Lists.Destr2OfPlayerCruis = 0;
                Lists.Destr2OfPlayerParCruis = 0;
                Lists.Destr3OfPlayerCruis = 0;
                Lists.Destr4OfPlayerCruis = 3 + destr4ExtraKit * startKitModifier;
                Lists.Gun1OfPlayerCruis = 0;
                Lists.Gun2OfPlayerCruis = 0;
                Lists.Gun3OfPlayerCruis = 0;
                Lists.MiniGunOfPlayerCruis = 0;
            }
            else
            {
                Lists.isBlackDimension = false;
                Lists.isBlueDimension = false;
                Lists.isRedDimension = true;
                Lists.energyOfPlayer = 400 + soundExtraVolume * startKitModifier;
                //Lists.boosterOfPlayer = 35 + soundMixerExtra * startKitModifier;

                Lists.Cruis1OfPlayerCruis = 0;
                Lists.Cruis2OfPlayerCruis = 0;
                Lists.Cruis3OfPlayerCruis = 0;
                Lists.Cruis4OfPlayerCruis = 1;
                Lists.Destr1OfPlayerCruis = 0;
                Lists.Destr1OfPlayerParCruis = 0;
                Lists.Destr2OfPlayerCruis = 0;
                Lists.Destr2OfPlayerParCruis = 0;
                Lists.Destr3OfPlayerCruis = 0;
                Lists.Destr4OfPlayerCruis = 3 + destr4ExtraKit * startKitModifier;
                Lists.Gun1OfPlayerCruis = 0;
                Lists.Gun2OfPlayerCruis = 0;
                Lists.Gun3OfPlayerCruis = 0;
                Lists.MiniGunOfPlayerCruis = 0;
            }
            schangeSceneSound.Play();
            StartCoroutine(switchSceneWithPause());
        }
    }

    //this method is called publicly from menu scene while player changes the value of graphics level settings slider
    public void graphicsChange() {
        if (graphicsSettingsSlider.value == 0) {
            GraphicsSettings.renderPipelineAsset = lowQuality;
            QualitySettings.renderPipeline = lowQuality;
        }
        else if (graphicsSettingsSlider.value == 1)
        {
            GraphicsSettings.renderPipelineAsset = midQuality;
            QualitySettings.renderPipeline = midQuality;
        }
        else
        {
            GraphicsSettings.renderPipelineAsset = highQuality;
            QualitySettings.renderPipeline = highQuality;
        }
    }
    //this method is called publicly from menu scene while player changes the value of music level settings slider
    public void musicChange()
    {
        audioMixer.SetFloat("BckgrMusic", musicSettingsSlider.value);
    }
    //this method is called publicly from menu scene while player changes the value of music level settings slider
    public void soundshange()
    {
        audioMixer.SetFloat("SoundEffect", soundSettingsSlider.value);
        audioMixer.SetFloat("UISound", soundSettingsSlider.value);
    }
    public void openSettingsPanel() {
        commonPushedSound.Play();
        //so any of two panels (shop and settings) opens only if all of them are closed already
        if (fade != 1 && settingsPanel.transform.localPosition.x==-30000 && shopPanel.transform.localPosition.x == -30000)
        {
            isShopPanelOpening = false; //this one is used while closing the panels on update method to determine which is open
            settingsPanelRawImg.GetComponent<Mask>().enabled = true;
            //settingsPanel.SetActive(true);
            closeSettingsPanelButton.SetActive(true);
            fadeIn = true;
            fade = 0;
            settingsPanel.transform.localPosition = new Vector3(0, -45, 0);
        }
    }

    public void openShopPanel()
    {
        commonPushedSound.Play();
        //so any of two panels (shop and settings) opens only if all of them are closed already
        if (fade != 1 && settingsPanel.transform.localPosition.x == -30000 && shopPanel.transform.localPosition.x == -30000)
        {
            isShopPanelOpening = true;
            shopPanelRawImg.GetComponent<Mask>().enabled = true; //this one is used while closing the panels on update method to determine which is open
            //settingsPanel.SetActive(true);
            closeShopPanelButton.SetActive(true);
            fadeIn = true;
            fade = 0;
            shopPanel.transform.localPosition = new Vector3(0, -45, 0);
        }
    }

    public void closeSettingsPanel()
    {
        commonPushedSound.Play();
        settingsPanelBack.transform.localPosition = new Vector3(-30000, -45, 0);
        savePlayerPrefs(); //saving the prefs data to load it later on loading menu scene
        if (fade != 0)
        {
            //closeSettingsPanelButton.SetActive(false);
            settingsPanelRawImg.GetComponent<Mask>().enabled = true;
            fadeOut = true;
            fade = 1;
            Invoke("disactivatePanels", 0.6f);
        }
    }

    public void closeShopPanel()
    {
        commonPushedSound.Play();
        settingsPanelBack.transform.localPosition = new Vector3(-30000, -45, 0);
        if (fade != 0)
        {
            //closeSettingsPanelButton.SetActive(false);
            shopPanelRawImg.GetComponent<Mask>().enabled = true;
            fadeOut = true;
            fade = 1;
            Invoke("disactivatePanels", 0.6f);
        }
    }

    private void disactivatePanels()
    {
        if (!isShopPanelOpening) settingsPanel.transform.localPosition = new Vector3(-30000, -45, 0);
        else shopPanel.transform.localPosition = new Vector3(-30000, -45, 0);
        //settingsPanel.SetActive(false);
    }

    private void FixedUpdate()
    {
        //panels open and close effects are following two conditions by using the all holo materials fade effect from the special list
        //fade in will work only the fade out is finished, so new panel will open only if previous one is closed
        if (fadeIn && !fadeOut)
        {

            fade += Time.deltaTime * 1.5f;
            if (fade >= 1)
            {
                fadeIn = false;
                if (!isShopPanelOpening)
                {
                    settingsPanelRawImg.GetComponent<Mask>().enabled = false;
                }
                else
                {
                    shopPanelRawImg.GetComponent<Mask>().enabled = false;
                }
                fade = 1;
                settingsPanelBack.transform.localPosition = new Vector3(0, -45, 0);
            }
            if (!isShopPanelOpening)
            {
                holoMatsToFade.SetFloat("_Fade", fade);
            }
            else holoMatsToFadeShop.SetFloat("_Fade", fade);
        }
        if (fadeOut)
        {
            fade -= Time.deltaTime * 3f;
            if (fade <= 0)
            {
                fadeOut = false;
                fade = 0;
            }
            if (!isShopPanelOpening)
            {
                holoMatsToFade.SetFloat("_Fade", fade);
            }
            else holoMatsToFadeShop.SetFloat("_Fade", fade);
        }

    }
    IEnumerator switchSceneWithPause() {

        holoMatsToFade.SetFloat("_Fade", 0); //setting the holo mat fade property to 0 (to prepare it to use on next scene)
        yield return new WaitForSeconds(0.9f);
        //increasing the count of switches to game and saving that data (this count is used with rate us features). So if count is 5 the request appears.
        //This code will work only till player either refuse the request or make a rate
        if (!rateOff)
        {
            switchToGameCounts++;
            savePlayerPrefs();
        }
        SceneSwitchMngr.LoadJourneyScene();
    }
}
