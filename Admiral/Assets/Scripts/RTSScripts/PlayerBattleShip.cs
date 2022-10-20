using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBattleShip : BattleShipClass
{
    private bool isSelected;
    //[HideInInspector]
    //public bool isMoving;
    //[HideInInspector]
    //public Vector3 moveToPoint;

    //public GameObject powerShiled;
    //[HideInInspector]
    //public bool shieldIsOn;

    private Outline selectedOutline;
    private LineRenderer shipMovingLine;
    //[HideInInspector]
    //public float speedOfThisShip;
    //[HideInInspector]
    //public float attackDistancePlay; //TO ASSIGN WHILE PULLLING
    //[HideInInspector]
    //public float attackTimeMax; //TO ASSIGN WHILE PULLLING
    //[HideInInspector]
    //public float attackTimeMin; //TO ASSIGN WHILE PULLLING
    //[HideInInspector]
    //public float shieldTimeMax; //TO ASSIGN WHILE PULLLING
    //[HideInInspector]
    //public float shieldTimeMin; //TO ASSIGN WHILE PULLLING
    //[HideInInspector]
    //public float shieldOnTime; //TO ASSIGN WHILE PULLLING

    //[HideInInspector]
    //public float HP; //TO ASSIGN WHILE PULLLING
    //[HideInInspector]
    //public float harm; //TO ASSIGN WHILE PULLLING
    //[HideInInspector]
    //public float harmStar; //TO ASSIGN WHILE PULLLING
    //private float attackTimerTime;
    //private float shieldTimerTime;
    //private List<CPUBattleShip> closeCPUBattleShips;
    //private List<StationClass> closeCPUStations;

    public ParticleSystem megaAttackOfCruis4;
    public GameObject megaAttackOfCruis3;
    public GameObject megaAttackOfCruis2;
    public GameObject megaAttackOfCruis1;
    private MegaAttackController megaAttackController;

    public List<GameObject> shipColoredElements; //parts of ship demostrate the color of ship
    //[HideInInspector]
    //public Color colorOfShipMat;

    private void OnEnable()
    {
        megaAttackController = FindObjectOfType<MegaAttackController>();
        shipTransform = transform;
        //closeCPUBattleShips = new List<CPUBattleShip>();
        //closeCPUStations = new List<StationClass>();
        closeBattleShips = new List<BattleShipClass>();
        closeStations = new List<StationClass>();
        closeStars = new List<StarController>();
        if (attackLaserLine==null) attackLaserLine = GetComponent<LineRenderer>();
        attackLaserLine.positionCount = 2;
        powerShiled.SetActive(false);
        attackLaserLine.enabled = false;
        attackDistance = CommonProperties.attackDistanceForAll;
        selectedOutline = GetComponent<Outline>();
        shipMovingLine = transform.GetChild(0).GetComponent<LineRenderer>();
        shipMovingLine.positionCount = 2;

        if (name.Contains("Cruis")) isCruiser = true;
        else isCruiser = false;

        if (name.Contains("4"))
        {
            levelOfShip = 4;
            attackTimeMin = isCruiser ? CommonProperties.cruiser4MinAttackTime : CommonProperties.destr4MinAttackTime;
            attackTimeMax = isCruiser ? CommonProperties.cruiser4MaxAttackTime : CommonProperties.destr4MaxAttackTime;
            shieldTimeMin = isCruiser ? CommonProperties.cruiser4MinShieldPause : CommonProperties.destr4MinShieldPause;
            shieldTimeMax = isCruiser ? CommonProperties.cruiser4MaxShieldPause : CommonProperties.destr4MaxShieldPause;
            shieldOnTime = isCruiser ? CommonProperties.cruiser4ShieldOnTime : CommonProperties.destr4ShieldOnTime;
            harm = isCruiser ? CommonProperties.Cruiser4Harm : CommonProperties.Destr4Harm;
            harmStar = isCruiser ? CommonProperties.Cruiser4StationHarm : CommonProperties.Destr4StationHarm;
            shipPower = isCruiser ? CommonProperties.Cruis4Index : CommonProperties.Destr4Index;
            if (isCruiser) megaShotSound = megaAttackOfCruis4.gameObject.GetComponent<AudioSource>();
            HP = isCruiser ? CommonProperties.cruiser4HP : CommonProperties.destr4HP;
            speedOfThisShip = CommonProperties.moveSpeedFor0;
        }
        else if (name.Contains("3"))
        {
            levelOfShip = 3;
            attackTimeMin = isCruiser ? CommonProperties.cruiser3MinAttackTime : CommonProperties.destr3MinAttackTime;
            attackTimeMax = isCruiser ? CommonProperties.cruiser3MaxAttackTime : CommonProperties.destr3MaxAttackTime;
            shieldTimeMin = isCruiser ? CommonProperties.cruiser3MinShieldPause : CommonProperties.destr3MinShieldPause;
            shieldTimeMax = isCruiser ? CommonProperties.cruiser3MaxShieldPause : CommonProperties.destr3MaxShieldPause;
            shieldOnTime = isCruiser ? CommonProperties.cruiser3ShieldOnTime : CommonProperties.destr3ShieldOnTime;
            harm = isCruiser ? CommonProperties.Cruiser3Harm : CommonProperties.Destr3Harm;
            harmStar = isCruiser ? CommonProperties.Cruiser3StationHarm : CommonProperties.Destr3StationHarm;
            shipPower = isCruiser ? CommonProperties.Cruis3Index : CommonProperties.Destr3Index;
            if (isCruiser) megaShotSound = megaAttackOfCruis3.GetComponent<AudioSource>();
            HP = isCruiser ? CommonProperties.cruiser3HP : CommonProperties.destr3HP;
            speedOfThisShip = CommonProperties.moveSpeedFor1;

        }
        else if (name.Contains("2"))
        {
            if (name.Contains("Par")) isParalyzer = true;
            levelOfShip = 2;
            shipPower = isCruiser ? CommonProperties.Cruis2Index : CommonProperties.Destr2Index;
            attackTimeMin = isCruiser ? CommonProperties.cruiser2MinAttackTime : CommonProperties.destr2MinAttackTime;
            attackTimeMax = isCruiser ? CommonProperties.cruiser2MaxAttackTime : CommonProperties.destr2MaxAttackTime;
            shieldTimeMin = isCruiser ? CommonProperties.cruiser2MinShieldPause : CommonProperties.destr2MinShieldPause;
            shieldTimeMax = isCruiser ? CommonProperties.cruiser2MaxShieldPause : CommonProperties.destr2MaxShieldPause;
            shieldOnTime = isCruiser ? CommonProperties.cruiser2ShieldOnTime : CommonProperties.destr2ShieldOnTime;
            harm = isCruiser ? CommonProperties.Cruiser2Harm : CommonProperties.Destr2Harm;
            harmStar = isCruiser ? CommonProperties.Cruiser2StationHarm : CommonProperties.Destr2StationHarm;
            HP = isCruiser ? CommonProperties.cruiser2HP : CommonProperties.destr2HP;
            //if (isCruiser) megaShotSound = megaAttackOfCruis3.GetComponent<AudioSource>();
            speedOfThisShip = CommonProperties.moveSpeedFor2;
            if (isParalyzer)
            {
                paralizeTimeForParaizer = 4;
                attackTimeMin += 0.5f;
                attackTimeMax += 0.5f;
                shieldTimeMin += 0.5f;
                shieldTimeMax += 0.5f;
                shieldOnTime += 0.5f;
                HP += 1;
            }
        }
        else if (name.Contains("1"))
        {
            if (name.Contains("Par")) isParalyzer = true;
            levelOfShip = 1; 
            shipPower = isCruiser ? CommonProperties.Cruis1Index : CommonProperties.Destr1Index;
            attackTimeMin = isCruiser ? CommonProperties.cruiser1MinAttackTime : CommonProperties.destr1MinAttackTime;
            attackTimeMax = isCruiser ? CommonProperties.cruiser1MaxAttackTime : CommonProperties.destr1MaxAttackTime;
            shieldTimeMin = isCruiser ? CommonProperties.cruiser1MinShieldPause : CommonProperties.destr1MinShieldPause;
            shieldTimeMax = isCruiser ? CommonProperties.cruiser1MaxShieldPause : CommonProperties.destr1MaxShieldPause;
            shieldOnTime = isCruiser ? CommonProperties.cruiser1ShieldOnTime : CommonProperties.destr1ShieldOnTime;
            harm = isCruiser ? CommonProperties.Cruiser1Harm : CommonProperties.Destr1Harm;
            harmStar = isCruiser ? CommonProperties.Cruiser1StationHarm : CommonProperties.Destr1StationHarm;
            HP = isCruiser ? CommonProperties.cruiser1HP : CommonProperties.destr1HP;

            //if (isCruiser) megaShotSound = megaAttackOfCruis3.GetComponent<AudioSource>();
            speedOfThisShip = CommonProperties.moveSpeedFor3;

            if (isParalyzer)
            {
                paralizeTimeForParaizer = 5;
                attackTimeMin += 0.5f;
                attackTimeMax += 0.5f;
                shieldTimeMin += 0.5f;
                shieldTimeMax += 0.5f;
                shieldOnTime += 0.5f;
                HP += 1;
            }
        }

        if (isParalyzer && paralizerLaserLine == null)
        {
            paralizerLaserLine = transform.GetChild(1).GetComponent<LineRenderer>();
            paralizerLaserLine.positionCount = 2;
            paralizerLaserLine.enabled = false;
        }

        shieldIsOn = false;
        attackMode = false;
        shipMovingLine.SetPosition(0, shipTransform.position);
        shipMovingLine.SetPosition(1, shipTransform.position);
        selectedOutline.enabled = false;

        foreach (MeshRenderer MR in GetComponentsInChildren<MeshRenderer>()) MR.enabled = true;
        gameObject.layer = CPUNumber + 10;
        powerShiled.layer = CPUNumber + 10; 

    }
    public void setShipsColor(Color color)
    {
        //stationColorSphere.GetComponent<MeshRenderer>().material = colorOfStationMat;
        for (int i = 0; i < shipColoredElements.Count; i++)
        {
            shipColoredElements[i].GetComponent<MeshRenderer>().material.SetColor("_Color", color);

            powerShieldMaterial.SetColor("_Color", color);
        }
    }

    #region selecting and moving functionality
    public void SelectedAndReady()
    {
        UISelectingBox.Instance.ifAnyShipChousen = true;
        UISelectingBox.Instance.chosenPlayerBattleShipsObject.Add(this);
        selectedOutline.enabled = true;
        isSelected = true;
    }

    public void giveAShipMoveOrder(Vector3 moveTowards)
    {
        if (!isParalyzed)
        {
            float yRotation = 0;
            moveToPoint = moveTowards;
            //rotating the ship to look to move direction
            if (moveToPoint != transform.position)
            {
                yRotation = Quaternion.LookRotation(moveToPoint - transform.position, Vector3.up).eulerAngles.y;
            }
            transform.rotation = Quaternion.Euler(0, yRotation + 90, 0);
            //engineSound.Play();
            isMoving = true;
            shipMovingLine.SetPosition(0, transform.position);
            shipMovingLine.SetPosition(1, moveTowards);

            engineSound.Play();
        }

        selectedOutline.enabled = false;
    }

    public void deselectWithoutMoveOrders()
    {
        selectedOutline.enabled = false;
        isSelected = false;
    }

    public IEnumerator addThisToSelectable()
    {
        yield return new WaitForSeconds(0.3f);
        UISelectingBox.Instance.selectablePlayerBattleShipsObject.Add(this);
    }
    #endregion selecting and moving functionality

    #region attack and defence function

    //used to determine to wich ship assign the mega attack
    public int countOfEnemyShipsNear()
    {
        int count = 0;
        for (int i = 0; i < CommonProperties.CPUBattleShips.Count; i++)
        {
            if ((CommonProperties.CPUBattleShips[i].shipTransform.position - shipTransform.position).magnitude <= attackDistance)
            {
                count++;
            }
        }
        return count;
    }



    private void collectTheCloseEnemyShips() {
        for (int i = 0; i < CommonProperties.CPUBattleShips.Count; i++)
        {
            if (CommonProperties.CPUBattleShips[i].isActiveAndEnabled && (CommonProperties.CPUBattleShips[i].shipTransform.position - shipTransform.position).magnitude <= attackDistance&& !CommonProperties.CPUBattleShips[i].isUnderMegaDefence)
            {
                closeBattleShips.Add(CommonProperties.CPUBattleShips[i]);
            }
        }
        //attack the star and stations only it there is no battle ships around
        if (closeBattleShips.Count < 1)
        {
            for (int i = 0; i < CommonProperties.CPUStations.Count; i++)
            {
                if (CommonProperties.CPUStations[i].isActiveAndEnabled && (CommonProperties.CPUStations[i].stationPosition - shipTransform.position).magnitude <= attackDistance)
                {
                    closeStations.Add(CommonProperties.CPUStations[i]);
                }
            }
            for (int i = 0; i < CommonProperties.stars.Count; i++)
            {
                if (CommonProperties.stars[i].isActiveAndEnabled && (CommonProperties.stars[i].starPosition - shipTransform.position).magnitude <= attackDistance)
                {
                    closeStars.Add(CommonProperties.stars[i]);
                }
            }
        }

        if (closeBattleShips.Count > 0 || closeStations.Count > 0 || closeStars.Count>0)
        {
            attackMode = true;
            if (isCruiser)
            {
                if (countOfEnemyShipsNear() > 1)
                {
                    if (!CommonProperties.PlayerMegaAttackBattleShips.Contains(this)) CommonProperties.PlayerMegaAttackBattleShips.Add(this);
                    if (!CommonProperties.MegaAttackCoroutineIsOn[CPUNumber] && CommonProperties.MegaAttackTimer[CPUNumber] <= 0) StartCoroutine(CommonProperties.MegaAttack(CPUNumber));
                }
                else CommonProperties.PlayerMegaAttackBattleShips.Remove(this);
            }
        }
        else
        {
            attackMode = false;
            shipToAttak = null;
            stationToAttak = null;
            starToAttak = null;
            if (attackTimerTime < Random.Range(attackTimeMin, attackTimeMax)) attackTimerTime = Random.Range(attackTimeMin, attackTimeMax);
            if (shieldTimerTime < Random.Range(shieldTimeMin, shieldTimeMax)) shieldTimerTime = Random.Range(shieldTimeMin, shieldTimeMax);
        }
    }

    IEnumerator attackTheEnemy()
    {

        if (shipToAttak != null)
        {
            if (!shipToAttak.isActiveAndEnabled || (shipToAttak.shipTransform.position - shipTransform.position).magnitude > attackDistance || shipToAttak.isUnderMegaDefence)
            {
                if (closeBattleShips.Contains(shipToAttak)) closeBattleShips.Remove(shipToAttak);
                shipToAttak = null;
            }
            else
            {
                attackLaserLine.SetPosition(0, shipTransform.position);
                attackLaserLine.SetPosition(1, shipToAttak.shipTransform.position);
                attackLaserLine.enabled = true;
            }
        }
        //priority attack is for battle ships so there is double check if there any battle ship around appeared
        else if (closeBattleShips.Count > 0)
        {
            shipToAttak = closeBattleShips.Count < 2 ? closeBattleShips[0] : closeBattleShips[Random.Range(0, closeBattleShips.Count)];
            if (shipToAttak.isActiveAndEnabled)
            {
                attackLaserLine.SetPosition(0, shipTransform.position);
                attackLaserLine.SetPosition(1, shipToAttak.shipTransform.position);
                attackLaserLine.enabled = true;
            }
            else shipToAttak = null;
        }
        else if (stationToAttak != null)
        {
            if (!stationToAttak.isActiveAndEnabled || (stationToAttak.stationPosition - shipTransform.position).magnitude > attackDistance)
            {
                if (closeStations.Contains(stationToAttak)) closeStations.Remove(stationToAttak);
                stationToAttak = null;
            }
            else
            {
                attackLaserLine.SetPosition(0, shipTransform.position);
                attackLaserLine.SetPosition(1, stationToAttak.stationPosition);
                attackLaserLine.enabled = true;
            }
        }
        else if (starToAttak != null)
        {
            if (!starToAttak.isActiveAndEnabled || (starToAttak.starPosition - shipTransform.position).magnitude > attackDistance)
            {
                if (closeStars.Contains(starToAttak)) closeStars.Remove(starToAttak);
                starToAttak = null;
            }
            else
            {
                attackLaserLine.SetPosition(0, shipTransform.position);
                attackLaserLine.SetPosition(1, starToAttak.starPosition);
                attackLaserLine.enabled = true;
            }
        }
        else
        {
           
            if (closeBattleShips.Count > 0)
            {
                for (int i = 0; i < closeBattleShips.Count; i++)
                {
                    if ((closeBattleShips[i].shipTransform.position - shipTransform.position).magnitude <= attackDistance)
                    {
                        shipToAttak = closeBattleShips[i];
                        break;
                    }
                }
                //shipToAttak = closeBattleShips.Count < 2 ? closeBattleShips[0] : closeBattleShips[Random.Range(0, closeBattleShips.Count)];
                if (shipToAttak!=null && shipToAttak.isActiveAndEnabled)
                {
                    attackLaserLine.SetPosition(0, shipTransform.position);
                    attackLaserLine.SetPosition(1, shipToAttak.shipTransform.position);
                    attackLaserLine.enabled = true;
                }
                else shipToAttak = null;
            }
            else if (closeStations.Count > 0)
            {
                stationToAttak = closeStations[0];
                if (stationToAttak.isActiveAndEnabled)
                {
                    attackLaserLine.SetPosition(0, shipTransform.position);
                    attackLaserLine.SetPosition(1, stationToAttak.stationPosition);
                    attackLaserLine.enabled = true;
                }
                else stationToAttak = null;
            }
            else if (closeStars.Count > 0)
            {
                starToAttak = closeStars[0];
                if (starToAttak.isActiveAndEnabled)
                {
                    attackLaserLine.SetPosition(0, shipTransform.position);
                    attackLaserLine.SetPosition(1, starToAttak.starPosition);
                    attackLaserLine.enabled = true;
                }
                else starToAttak = null;
            }
        }

        if (shipToAttak != null)
        {
            //this block is necessary to prevent the bug of following the laser line after destroyed battle ship to it's new spawn point
            //if (shipToAttak.HP <= harm && !shipToAttak.shieldIsOn)
            //{
            //    BattleShipClass tempShip = shipToAttak;
            //    shipToAttak = null; //stops the active laser follow the destroyed ship
            //    tempShip.reduceTheHPOfShip(harm);

            //}
            if (!shipToAttak.shieldIsOn) shipToAttak.reduceTheHPOfShip(harm);
        }
        else if (stationToAttak != null)
        {
            stationToAttak.reduceTheHPOfStation(harmStar);
        }
        else if (starToAttak != null)
        {
            starToAttak.reduceTheHPOfStar(harmStar, 0);
        }

        closeBattleShips.Clear();
        closeStations.Clear();
        closeStars.Clear();
        shotSound.Play();

        yield return new WaitForSeconds(0.2f);
        attackLaserLine.enabled = false;
    }

    IEnumerator attackTheEnemyWithParalyzer()
    {
        if (shipToAttak != null)
        {
            if (shipToAttak.isActiveAndEnabled && (shipToAttak.shipTransform.position - shipTransform.position).magnitude < attackDistance && !shipToAttak.isUnderMegaDefence && !shipToAttak.isParalyzed)
            {
                paralizerLaserLine.SetPosition(0, shipTransform.position);
                paralizerLaserLine.SetPosition(1, shipToAttak.shipTransform.position);
                paralizerLaserLine.enabled = true;
                if (!shipToAttak.shieldIsOn) shipToAttak.StartCoroutine(shipToAttak.paralizeThisShip(paralizeTimeForParaizer));
            }
        }

        shotSound.Play();
        yield return new WaitForSeconds(0.8f);
        paralizerLaserLine.enabled = false;
    }


    IEnumerator defenceShieldTurn()
    {
        shieldIsOn = true;
        powerShiled.SetActive(true);
        yield return new WaitForSeconds(shieldOnTime);
        powerShiled.SetActive(false);
        shieldIsOn = false;
    }

    public void MegaAttackOfShip()
    {
        if (levelOfShip == 4)
        {
            megaAttackOfCruis4.Play();
            megaShotSound.Play();
        }
        else if (levelOfShip == 3)
        {
            megaAttackOfCruis3.SetActive(true);
            megaShotSound.Play();
            StartCoroutine(disactivateMegaAttack());
        }
        else if (levelOfShip == 2)
        {
            megaAttackOfCruis2.SetActive(true);
            StartCoroutine(disactivateMegaAttack());
            for (int i = 0; i < CommonProperties.CPUBattleShips.Count; i++)
            {
                if ((CommonProperties.CPUBattleShips[i].shipTransform.position - shipTransform.position).magnitude <= attackDistance - 4 && !CommonProperties.CPUBattleShips[i].shieldIsOn)
                {
                    CommonProperties.CPUBattleShips[i].StartCoroutine(CommonProperties.CPUBattleShips[i].megaAttackEffectForThisShip(levelOfShip));
                }
            }
        }

        else if (levelOfShip == 1)
        {
            megaAttackOfCruis1.SetActive(true);
        }
    }
    IEnumerator disactivateMegaAttack()
    {
        yield return new WaitForSeconds(2.5f);
        if (megaAttackOfCruis3 != null) megaAttackOfCruis3.SetActive(false);
        if (megaAttackOfCruis2 != null) megaAttackOfCruis2.SetActive(false);
        if (levelOfShip == 3)
        {
            for (int i = 0; i < CommonProperties.playerBattleShips.Count; i++)
            {
                if (CommonProperties.playerBattleShips[i] != this && (CommonProperties.playerBattleShips[i].shipTransform.position - shipTransform.position).magnitude <= attackDistance-5)
                {
                    CommonProperties.playerBattleShips[i].StartCoroutine(CommonProperties.playerBattleShips[i].megaAttackEffectForThisShip(levelOfShip));
                }
            }
            StartCoroutine(megaAttackEffectForThisShip(levelOfShip));
        }
    }


    public IEnumerator megaAttackEffectForThisShip(int cruiserLevel)
    {
        int secondsToWait = cruiserLevel == 3 ? 7 :9;

        if (cruiserLevel == 3)
        {
            foreach (MeshRenderer MR in GetComponentsInChildren<MeshRenderer>()) MR.enabled = false;
            isUnderMegaDefence = true;
        }
        else {
            StopCoroutine(paralizeThisShip(0));
            isParalyzed = true;
            paralizedEffect.SetActive(true);
        }

        yield return new WaitForSeconds(secondsToWait);

        if (cruiserLevel == 3)
        {
            foreach (MeshRenderer MR in GetComponentsInChildren<MeshRenderer>()) MR.enabled = true;
            isUnderMegaDefence = false;
        }
        else
        {
            isParalyzed = false;
            paralizedEffect.SetActive(false);
        }
    }
   

    #endregion attack and defence function

    #region reducing the HP and disactivating
    private void disactivateThisShip()
    {
        if (isCruiser) BurstList = ObjectPullerRTS.current.GetCruisBurstPull();
        else BurstList = ObjectPullerRTS.current.GetDestrBurstPull();
        BurstReal = ObjectPullerRTS.current.GetGameObjectFromPull(BurstList);
        BurstReal.transform.position = shipTransform.position;
        BurstReal.SetActive(true);
        CommonProperties.playerBattleShips.Remove(this);
        UISelectingBox.Instance.selectablePlayerBattleShipsObject.Remove(this);
        UISelectingBox.Instance.chosenPlayerBattleShipsObject.Remove(this);
        if (UISelectingBox.Instance.chosenPlayerBattleShipsObject.Count==0) UISelectingBox.Instance.ifAnyShipChousen = false;
        //if (megaAttackController.chosenCruiserToMegaAttack == this) megaAttackController.disableMegaAttackButtonIfCruiserIsDestroyed();
        if (maternalStation!=null && maternalStation.isActiveAndEnabled) maternalStation.ShipsAssigned--;
        maternalStation = null;
        if (megaAttackOfCruis3 != null) megaAttackOfCruis3.SetActive(false);
        if (megaAttackOfCruis2 != null) megaAttackOfCruis2.SetActive(false);
        if (isCruiser) CommonProperties.PlayerMegaAttackBattleShips.Remove(this);
        isUnderMegaDefence = false;
        attackLaserLine.enabled = false;
        if (isParalyzer) paralizerLaserLine.enabled = false;
        powerShiled.SetActive(false);
        StopCoroutine(attackTheEnemy());
        closeBattleShips.Clear();
        //closeCPUGuns.Clear();
        closeBattleShips.Clear();
        closeStars.Clear();
        stationToAttak = null;
        shipToAttak = null;
        starToAttak = null;
        isParalyzed = false;
        isParalyzer = false;
        attackMode = false;
        paralizedEffect.SetActive(false);
        gameObject.SetActive(false);

        GameController.current.checkIfPlayerWinOrLost();
    }

    //private void preBurstEffect()
    //{
    //    if (isCruiser) BurstList = ObjectPullerRTS.current.GetCruisPreBurstPull();
    //    else BurstList = ObjectPullerRTS.current.GetDestrPreBurstPull();
    //    BurstReal = ObjectPullerRTS.current.GetGameObjectFromPull(BurstList);
    //    BurstReal.transform.position = shipTransform.position;
    //    BurstReal.SetActive(true);
    //}

    public override void reduceTheHPOfShip(float harmAmount) {
        HP -= harmAmount;
        if (HP <= 4) preBurstEffect();
        if (HP <= 0)
        {
            disactivateThisShip();
        }
    }
    #endregion reducing the HP and disactivating

    private void Update()
    {
        if (shipToAttak == null /*&& stationToAttak == null && starToAttak == null*/ && !isParalyzed) collectTheCloseEnemyShips();
        if (attackMode && attackTimerTime > 0)
        {
            attackTimerTime -= Time.deltaTime;
        }
        if (attackMode && shieldTimerTime > 0 && !shieldIsOn)
        {
            shieldTimerTime -= Time.deltaTime;
        }
        if (attackMode && attackTimerTime <= 0)
        {
            attackTimerTime = Random.Range(attackTimeMin, attackTimeMax);
            if (!isParalyzed)
            {
                StartCoroutine(attackTheEnemy());
                paralizerShotCounter++;
                if (isParalyzer && paralizerShotCounter >= (levelOfShip + 1))
                {
                    paralizerShotCounter = 0;
                    StartCoroutine(attackTheEnemyWithParalyzer());
                }
            }
        }
        if (attackMode && shieldTimerTime <= 0 && !shieldIsOn)
        {
            shieldTimerTime = Random.Range(shieldTimeMin, shieldTimeMax);
            if (!isParalyzed) StartCoroutine(defenceShieldTurn());
        }

        //to prevent the bug of attacking respawned ship
        //if (shipToAttak != null && shipToAttak.HP <= 0) shipToAttak = null;
        //if (stationToAttak != null && stationToAttak.lifeLineAmount <= -6) stationToAttak = null;
    }

    private void FixedUpdate()
    {
        if (isMoving&& !isParalyzed)
        {
            if (attackLaserLine.enabled) attackLaserLine.SetPosition(0, shipTransform.position);
            if (isParalyzer && paralizerLaserLine.enabled) paralizerLaserLine.SetPosition(0, shipTransform.position);
            shipMovingLine.SetPosition(0, transform.position);
            gameObject.transform.position = Vector3.MoveTowards(transform.position, moveToPoint, speedOfThisShip);
            if (transform.position == moveToPoint)
            {
                isMoving = false;
            }
        }
        //if (attackLaserLine.enabled && shipToAttak!=null)
        //{
        //    attackLaserLine.SetPosition(1, shipToAttak.shipTransform.position);
        //}
        //if (isParalyzer&& paralizerLaserLine.enabled && shipToAttak != null )
        //{
        //    paralizerLaserLine.SetPosition(1, shipToAttak.shipTransform.position);
        //}

    }
}
