using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelStation : MonoBehaviour
{
    [SerializeField]
    Sprite Station3Sprite;
    [SerializeField]
    Sprite Station2Sprite;
    [SerializeField]
    Sprite Station1Sprite;

    [SerializeField]
    Sprite Cruis1Sprite;
    [SerializeField]
    Sprite Cruis2Sprite;
    [SerializeField]
    Sprite Cruis3Sprite;
    [SerializeField]
    Sprite Cruis4Sprite;

    [SerializeField]
    Sprite Destr1Sprite;
    [SerializeField]
    Sprite Destr1ParSprite;
    [SerializeField]
    Sprite Destr2Sprite;
    [SerializeField]
    Sprite Destr2ParSprite;
    [SerializeField]
    Sprite Destr3Sprite;
    [SerializeField]
    Sprite Destr4Sprite;
    [SerializeField]
    Sprite DestrDefaultSprite;

    [SerializeField]
    Sprite Gun1Sprite;
    [SerializeField]
    Sprite Gun2Sprite;
    [SerializeField]
    Sprite Gun3Sprite;

    [SerializeField]
    GameObject ConnectionsToken;

    [SerializeField]
    private Image upgradeButtonImg;
    [SerializeField]
    private Image gunButtonImg;
    [SerializeField]
    private Image CruiserProductionButtonImg;
    [SerializeField]
    private Image DestrProductionButtonImg;
    [SerializeField]
    private Image DestrParProductionButtonImg;

    [SerializeField]
    private Button upgradeButton;
    [SerializeField]
    private Button gunButton;
    [SerializeField]
    private Button CruiserProductionButton;
    [SerializeField]
    private Button DestrProductionButton;
    [SerializeField]
    private Button DestrParProductionButton;

    [SerializeField]
    private Text energyOfStation;
    [SerializeField]
    private Text shipsAssigned;
    [SerializeField]
    private Text shipsLimit;

    [SerializeField]
    private Text energyForCruiser;
    [SerializeField]
    private Text energyForDestr;
    [SerializeField]
    private Text energyForDestrPar;
    [SerializeField]
    private Text energyForUpgrade;
    [SerializeField]
    private Text energyForGun;

    private int energyCruiserCost;
    private int energyDestrCost;
    private int energyDestrParCost;
    [HideInInspector]
    public StationPlayerRTS station;

    private List<GameObject> ObjectPulledList; 
    private GameObject ObjectPulled;


    private const float radiusOfShipsRingAroundStation = 6f;
    private Vector3 stationPosition;
    private int stepOfCircleAroundStationToLaunchShips;
    private CameraManager cameraManager;


    //int commonEnenrgy;


    private void Start()
    {
        //commonEnenrgy = 0;
        cameraManager = CommonProperties.MainCameraOfRTS.GetComponent<CameraManager>();
    }
    private void OnEnable()
    {
        CommonProperties.UIButtonSound.Play();
    }

    public void activateThePanel(StationPlayerRTS station)
    {
        this.station = station;
        updateVariablesOfStation();
        shipsLimit.text = station.ShipsLimit.ToString();
        checkIfStationCanUpgrade();
        checkIfGunCanUpgrade();
        setTheShipsToProduce();
        updateVariablesAfterEnergyChanges();
        stationPosition = this.station.stationPosition;
        //radiusOfShipsRingAroundStation = station.radiusOfShipsRingAroundStation;
        stepOfCircleAroundStationToLaunchShips = 0;
        if (station.groupWhereTheStationIs != null /*&& station.groupWhereTheStationIs.Count > 0*/) ConnectionsToken.SetActive(true);
    }

    private void checkIfStationCanUpgrade()
    {
        if (station.stationCurrentLevel < station.upgradeCounts) {
            upgradeButtonImg.gameObject.SetActive(true);
            if (station.stationCurrentLevel == 0) upgradeButtonImg.sprite = Station3Sprite;
            else if (station.stationCurrentLevel == 1) upgradeButtonImg.sprite = Station2Sprite;
            else if (station.stationCurrentLevel == 2) upgradeButtonImg.sprite = Station1Sprite;
            energyForUpgrade.text = station.energyToNextUpgradeOfStation.ToString();
            if (station.groupWhereTheStationIs != null /*&& station.groupWhereTheStationIs.Count > 0*/)
            {
                if (CommonProperties.energyOfStationGroups[station.groupWhereTheStationIs] >= station.energyToNextUpgradeOfStation)
                {
                    upgradeButton.interactable = true;
                }
                else if (upgradeButton.interactable)
                {
                    upgradeButton.interactable = false;
                }
            }
            else
            {
                if (station.energyOfStation >= station.energyToNextUpgradeOfStation)
                {
                    upgradeButton.interactable = true;
                }
                else if (upgradeButton.interactable)
                {
                    upgradeButton.interactable = false;
                }
            }
        }
        else if (upgradeButtonImg.enabled) upgradeButtonImg.gameObject.SetActive(false);

    }
    private void checkIfGunCanUpgrade()
    {
        if (station.stationGunLevel < station.GunUpgradeCounts)
        {
            gunButtonImg.gameObject.SetActive(true);
            if (station.stationCurrentLevel == 1) gunButtonImg.sprite = Gun1Sprite;
            else if (station.stationCurrentLevel == 2) gunButtonImg.sprite = Gun2Sprite;
            else if (station.stationCurrentLevel == 3) gunButtonImg.sprite = Gun3Sprite;
            energyForGun.text = station.energyToNextUpgradeOfGun.ToString();
            if (station.groupWhereTheStationIs != null /*&& station.groupWhereTheStationIs.Count > 0*/)
            {
                if (CommonProperties.energyOfStationGroups[station.groupWhereTheStationIs] >= station.energyToNextUpgradeOfGun)
                {
                    gunButton.interactable = true;
                }
                else if (gunButton.interactable) gunButton.interactable = false;

            }
            else
            {
                if (station.energyOfStation >= station.energyToNextUpgradeOfGun)
                {
                    gunButton.interactable = true;
                }
                else if (gunButton.interactable) gunButton.interactable = false;
            }
        }
        else if (gunButtonImg.enabled) gunButtonImg.gameObject.SetActive(false);

    }
    private void setTheShipsToProduce()
    {
        if (station.stationCurrentLevel == 0)
        {
            CruiserProductionButtonImg.sprite = Cruis4Sprite;
            DestrProductionButtonImg.sprite = Destr4Sprite;
            DestrParProductionButtonImg.sprite = DestrDefaultSprite;
            DestrParProductionButton.interactable = false;
            energyCruiserCost = CommonProperties.C4ProdEnergy;
            energyDestrCost = CommonProperties.D4ProdEnergy;
            energyDestrParCost = 0;
        }
        else if (station.stationCurrentLevel == 1)
        {
            CruiserProductionButtonImg.sprite = Cruis3Sprite;
            DestrProductionButtonImg.sprite = Destr3Sprite;
            DestrParProductionButtonImg.sprite = DestrDefaultSprite;
            DestrParProductionButton.interactable = false;
            energyCruiserCost = CommonProperties.C3ProdEnergy;
            energyDestrCost = CommonProperties.D3ProdEnergy;
            energyDestrParCost = 0;
        }
        else if (station.stationCurrentLevel == 2)
        {
            CruiserProductionButtonImg.sprite = Cruis2Sprite;
            DestrProductionButtonImg.sprite = Destr2Sprite;
            DestrParProductionButtonImg.sprite = Destr2ParSprite;
            DestrParProductionButton.interactable = true;
            energyCruiserCost = CommonProperties.C2ProdEnergy;
            energyDestrCost = CommonProperties.D2ProdEnergy;
            energyDestrParCost = CommonProperties.D2PProdEnergy;
        }
        else if (station.stationCurrentLevel == 3)
        {
            CruiserProductionButtonImg.sprite = Cruis1Sprite;
            DestrProductionButtonImg.sprite = Destr1Sprite;
            DestrParProductionButtonImg.sprite = Destr1ParSprite;
            DestrParProductionButton.interactable = true;
            energyCruiserCost = CommonProperties.C1ProdEnergy;
            energyDestrCost = CommonProperties.D1ProdEnergy;
            energyDestrParCost = CommonProperties.D1PProdEnergy;
        }

        energyForCruiser.text = energyCruiserCost.ToString();
        energyForDestr.text = energyDestrCost.ToString();
        energyForDestrPar.text = energyDestrParCost.ToString();
    }

    private void updateStationUpgradeVariables()
    {
        if (station.groupWhereTheStationIs != null /*&& station.groupWhereTheStationIs.Count > 0*/)
        {
            if (CommonProperties.energyOfStationGroups[station.groupWhereTheStationIs] >= station.energyToNextUpgradeOfStation)
            {
                if (!upgradeButton.interactable) upgradeButton.interactable = true;
            }
            else if (upgradeButton.interactable)
            {
                upgradeButton.interactable = false;
            }
        }
        else
        {
            if (station.energyOfStation >= station.energyToNextUpgradeOfStation)
            {
                if (!upgradeButton.interactable) upgradeButton.interactable = true;
            }
            else if (upgradeButton.interactable)
            {
                upgradeButton.interactable = false;
            }
        }
    }
    private void updateGunUpgradeVariables()
    {
        if (station.groupWhereTheStationIs != null /*&& station.groupWhereTheStationIs.Count > 0*/)
        {
            if (CommonProperties.energyOfStationGroups[station.groupWhereTheStationIs] >= station.energyToNextUpgradeOfGun)
            {
                if (!gunButton.interactable) gunButton.interactable = true;
            }
            else if (gunButton.interactable) gunButton.interactable = false;
        }
        else
        {
            if (station.energyOfStation >= station.energyToNextUpgradeOfGun)
            {
                if (!gunButton.interactable) gunButton.interactable = true;
            }
            else if (gunButton.interactable) gunButton.interactable = false;
        }
    }
    private void updateVariablesOfStation()
    {
        if (station!=null && station.groupWhereTheStationIs!=null && station.groupWhereTheStationIs.Count > 0)
        {
            energyOfStation.text = CommonProperties.energyOfStationGroups[station.groupWhereTheStationIs].ToString("0");
        }
        else
        {
            energyOfStation.text = station.energyOfStation.ToString("0");
        }
        shipsAssigned.text = station.ShipsAssigned.ToString();
    }
    private void checkIfShipsCanBeProduced()
    {
        if (station.groupWhereTheStationIs != null /*&& station.groupWhereTheStationIs.Count > 0*/) {
            if (station.ShipsAssigned < station.ShipsLimit)
            {
                if (energyCruiserCost <= CommonProperties.energyOfStationGroups[station.groupWhereTheStationIs])
                {
                    if (!CruiserProductionButton.interactable) CruiserProductionButton.interactable = true;
                }
                else if (CruiserProductionButton.interactable) CruiserProductionButton.interactable = false;

                if (energyDestrCost <= CommonProperties.energyOfStationGroups[station.groupWhereTheStationIs])
                {
                    if (!DestrProductionButton.interactable) DestrProductionButton.interactable = true;
                }
                else if (DestrProductionButton.interactable) DestrProductionButton.interactable = false;
                //only stations higher than 1 level can produce paralizing destroyers
                if (energyDestrParCost <= CommonProperties.energyOfStationGroups[station.groupWhereTheStationIs] && station.stationCurrentLevel > 1)
                {
                    if (!DestrParProductionButton.interactable) DestrParProductionButton.interactable = true;
                }
                else if (DestrParProductionButton.interactable) DestrParProductionButton.interactable = false;
            }
            else
            {
                if (CruiserProductionButton.interactable) CruiserProductionButton.interactable = false;
                if (DestrProductionButton.interactable) DestrProductionButton.interactable = false;
                if (DestrParProductionButton.interactable) DestrParProductionButton.interactable = false;
            }
        }
        else {
            if (station.ShipsAssigned < station.ShipsLimit)
            {
                if (energyCruiserCost <= station.energyOfStation)
                {
                    if (!CruiserProductionButton.interactable) CruiserProductionButton.interactable = true;
                }
                else if (CruiserProductionButton.interactable) CruiserProductionButton.interactable = false;

                if (energyDestrCost <= station.energyOfStation)
                {
                    if (!DestrProductionButton.interactable) DestrProductionButton.interactable = true;
                }
                else if (DestrProductionButton.interactable) DestrProductionButton.interactable = false;
                //only stations higher than 1 level can produce paralizing destroyers
                if (energyDestrParCost <= station.energyOfStation && station.stationCurrentLevel > 1)
                {
                    if (!DestrParProductionButton.interactable) DestrParProductionButton.interactable = true;
                }
                else if (DestrParProductionButton.interactable) DestrParProductionButton.interactable = false;
            }
            else
            {
                if (CruiserProductionButton.interactable) CruiserProductionButton.interactable = false;
                if (DestrProductionButton.interactable) DestrProductionButton.interactable = false;
                if (DestrParProductionButton.interactable) DestrParProductionButton.interactable = false;
            }
        }

    }
    public void updateVariablesAfterEnergyChanges()
    {
        updateVariablesOfStation();
        if (station.stationCurrentLevel < station.upgradeCounts) updateStationUpgradeVariables();
        if (station.stationGunLevel < station.GunUpgradeCounts) updateGunUpgradeVariables();
        checkIfShipsCanBeProduced();
    }

    private Vector3 assignToLaunchedShipMovePoint()
    {
        Vector3 newPos;
        float step = (Mathf.PI * 2) / station.ShipsLimit; // отступ
        newPos.x = stationPosition.x + Mathf.Sin(step * stepOfCircleAroundStationToLaunchShips) * radiusOfShipsRingAroundStation; // по оси X
        newPos.z = stationPosition.z + Mathf.Cos(step * stepOfCircleAroundStationToLaunchShips) * radiusOfShipsRingAroundStation; // по оси Z
        newPos.y = 0; // по оси Y всегда 0
        stepOfCircleAroundStationToLaunchShips++;
        return newPos;
    }

    public void launcheNewShip(int lvl)
    {
        //lvl is index assigned to button to determine which type of ship is ordered to produce 1 - cruiser, 2 - destr, 3 - destr par
        if (lvl == 1)
        {
            //indexes are 0-Cruis4, 1-Destr4, 2-Cruis3, 3-Destr3, 4-Cruis2, 5-Destr2, 6-Destr2Par, 7-Cruis1, 8-Destr1, 9-Destr1Par,
            if (station.stationCurrentLevel == 0) ObjectPulledList = ObjectPullerRTS.current.GetShipPull(0,0);
            else if(station.stationCurrentLevel == 1) ObjectPulledList = ObjectPullerRTS.current.GetShipPull(2,0);
            else if (station.stationCurrentLevel == 2) ObjectPulledList = ObjectPullerRTS.current.GetShipPull(4, 0);
            else if (station.stationCurrentLevel == 3) ObjectPulledList = ObjectPullerRTS.current.GetShipPull(7, 0);

            consumeTheEnergy(energyCruiserCost, false);
        }
        else if (lvl == 2)
        {
            if (station.stationCurrentLevel == 0) ObjectPulledList = ObjectPullerRTS.current.GetShipPull(1,0);
            else if(station.stationCurrentLevel == 1) ObjectPulledList = ObjectPullerRTS.current.GetShipPull(3,0);
            else if (station.stationCurrentLevel == 2) ObjectPulledList = ObjectPullerRTS.current.GetShipPull(5, 0);
            else if (station.stationCurrentLevel == 3) ObjectPulledList = ObjectPullerRTS.current.GetShipPull(8, 0);
            consumeTheEnergy(energyDestrCost, false);
        }
        else if (lvl == 3)
        {
            if (station.stationCurrentLevel == 2) ObjectPulledList = ObjectPullerRTS.current.GetShipPull(6, 0);
            else if (station.stationCurrentLevel == 3) ObjectPulledList = ObjectPullerRTS.current.GetShipPull(9, 0);
            consumeTheEnergy(energyDestrParCost, false);
        }
        station.ShipsAssigned++;
        updateVariablesAfterEnergyChanges();
        ObjectPulled = ObjectPullerRTS.current.GetGameObjectFromPull(ObjectPulledList);
        PlayerBattleShip cpuShipObject = ObjectPulled.GetComponent<PlayerBattleShip>();
        CommonProperties.playerBattleShips.Add(cpuShipObject);
        ObjectPulled.transform.position = station.stationPosition;
        cpuShipObject.maternalStation = station;
        cpuShipObject.setShipsColor(station.colorOfStationMat);
        ObjectPulled.SetActive(true);
        cpuShipObject.StartCoroutine(cpuShipObject.addThisToSelectable());
        cpuShipObject.giveAShipMoveOrder(assignToLaunchedShipMovePoint());

        CommonProperties.UIButtonSound.Play();
    }

    public void upgradeTheStation()
    {
        consumeTheEnergy(station.energyToNextUpgradeOfStation, true);
        station.stationPanelIsActiveForThis = false;
        station.upgradeStation(station.stationCurrentLevel + 1);
        closeThePanel(true);
        CommonProperties.UpgradeSound.Play();
    }
    public void upgradeTheGun()
    {
        consumeTheEnergy(station.energyToNextUpgradeOfGun, false);
        station.stationPanelIsActiveForThis = false;
        station.upgradeTheGun(station.stationGunLevel + 1);
        closeThePanel(true);
        CommonProperties.UpgradeSound.Play();
    }

    private void consumeTheEnergy(int consumeAmount, bool isStationUpgrade)
    {
        if (station.groupWhereTheStationIs != null /*&& station.groupWhereTheStationIs.Count > 0*/)
        {
            CommonProperties.energyOfStationGroups[station.groupWhereTheStationIs] -= consumeAmount;
        }
        else
        {
            station.energyOfStation -= consumeAmount;
        }
        if (!isStationUpgrade) foreach (StationPlayerRTS stationPlayer in CommonProperties.playerStations) stationPlayer.checkIfStationCanConnect();
    }

    public void closeThePanel(bool isUpgrade)
    {
        if (!isUpgrade)
        {
            CommonProperties.UIButtonSound.Play();
            station.stationPanelIsActiveForThis = false;
        }
        ConnectionsToken.SetActive(false);
        CommonProperties.stationPanelIsActive = false;
        station = null;
        cameraManager.PanelIsClosed = true;
        gameObject.SetActive(false);
    }

}