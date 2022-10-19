using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUBattleShip : BattleShipClass
{
    //public GameObject powerShiled;
    //[HideInInspector]
    //public bool shieldIsOn;

    //[HideInInspector]
    //public Transform shipTransform;
    //[HideInInspector]
    //public float speedOfThisShip;
    //[HideInInspector]
    //public float attackDistance; //TO ASSIGN WHILE PULLLING
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
    //[HideInInspector]
    //public bool isMoving;
    //[HideInInspector]
    //public int CPUNumber;

    //[HideInInspector]
    //public Vector3 moveToPoint;

    //private float attackTimerTime;
    //private float shieldTimerTime;
    //private List<PlayerBattleShip> closePlayerBattleShips;
    //private List<StationClass> closePlayerStations;


    public ParticleSystem megaAttackOfCruis4;
    public GameObject megaAttackOfCruis3;
    public GameObject megaAttackOfCruis2;
    public GameObject megaAttackOfCruis1;

    public List<GameObject> shipColoredElements;
    List<Vector3> wayPoints;
    private int nextPointToMoveIndex;
    private Vector3 moveToPointInWay;

    private void OnEnable()
    {
        nextPointToMoveIndex = 0;
        wayPoints = new List<Vector3>();
        attackDistance = CommonProperties.attackDistanceForAll;
        shipTransform = transform;
        //closePlayerGuns = new List<PlayerGun>();
        //closePlayerStations = new List<StationClass>();
        closeBattleShips = new List<BattleShipClass>();
        closeStations = new List<StationClass>();
        closeStars = new List<StarController>();
        if (attackLaserLine == null) attackLaserLine = GetComponent<LineRenderer>();
        attackLaserLine.positionCount = 2;
        powerShiled.SetActive(false);
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
            HP = isCruiser ? CommonProperties.cruiser4HP : CommonProperties.destr4HP;
            if (isCruiser) megaShotSound = megaAttackOfCruis4.gameObject.GetComponent<AudioSource>();
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
            HP = isCruiser ? CommonProperties.cruiser3HP : CommonProperties.destr3HP;
            if (isCruiser) megaShotSound = megaAttackOfCruis3.GetComponent<AudioSource>();
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
            speedOfThisShip = CommonProperties.moveSpeedFor3;

            //if (isCruiser) megaShotSound = megaAttackOfCruis3.GetComponent<AudioSource>();

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

        foreach (MeshRenderer MR in GetComponentsInChildren<MeshRenderer>()) MR.enabled = true;

        shieldIsOn = false;
        attackMode = false;
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

    //used to determine to wich ship assign the mega attack
    public int countOfEnemyShipsNear() {
        int count = 0;
        for (int i = 0; i < CommonProperties.playerBattleShips.Count; i++)
        {
            if ((CommonProperties.playerBattleShips[i].shipTransform.position - shipTransform.position).magnitude <= attackDistance)
            {
                count++;
            }
        }
        for (int i = 0; i < CommonProperties.CPUBattleShipsDictionary.Count; i++)
        {
            if (i != (CPUNumber - 1))
            {
                for (int y = 0; y < CommonProperties.CPUBattleShipsDictionary[i].Count; y++)
                {
                    if ((CommonProperties.CPUBattleShipsDictionary[i][y].shipTransform.position - shipTransform.position).magnitude <= attackDistance)
                    {
                        count++;
                    }
                }
            }
        }
        return count;
    }

    private void collectTheCloseEnemyShips()
    {
        for (int i = 0; i < CommonProperties.playerBattleShips.Count; i++)
        {
            if (CommonProperties.playerBattleShips[i].isActiveAndEnabled&&(CommonProperties.playerBattleShips[i].shipTransform.position - shipTransform.position).magnitude <= attackDistance && !CommonProperties.playerBattleShips[i].isUnderMegaDefence)
            {
                closeBattleShips.Add(CommonProperties.playerBattleShips[i]);
            }
        }
        for (int i = 0; i < CommonProperties.CPUBattleShipsDictionary.Count; i++)
        {
            if (i != (CPUNumber - 1))
            {
                for (int y = 0; y < CommonProperties.CPUBattleShipsDictionary[i].Count; y++)
                {
                    if (CommonProperties.CPUBattleShipsDictionary[i][y].isActiveAndEnabled && (CommonProperties.CPUBattleShipsDictionary[i][y].shipTransform.position - shipTransform.position).magnitude <= attackDistance && !CommonProperties.CPUBattleShipsDictionary[i][y].isUnderMegaDefence)
                    {
                        closeBattleShips.Add(CommonProperties.CPUBattleShipsDictionary[i][y]);
                    }
                }
            }
        }
        //for (int i = 0; i < CommonProperties.playerGuns.Count; i++)
        //{
        //    if ((CommonProperties.playerGuns[i].gunTransform.position - shipTransform.position).magnitude <= attackDistance)
        //    {
        //        closePlayerGuns.Add(CommonProperties.playerGuns[i]);
        //    }
        //}

        //attack the star and stations only it there is no battle ships around
        if (closeBattleShips.Count < 1)
        {
            for (int i = 0; i < CommonProperties.playerStations.Count; i++)
            {
                if (CommonProperties.playerStations[i].isActiveAndEnabled && (CommonProperties.playerStations[i].stationPosition - shipTransform.position).magnitude <= attackDistance)
                {
                    closeStations.Add(CommonProperties.playerStations[i]);
                }
            }
            for (int i = 0; i < CommonProperties.CPUStationsDictionary.Count; i++)
            {
                if (i != (CPUNumber - 1))
                {
                    for (int y = 0; y < CommonProperties.CPUStationsDictionary[i].Count; y++)
                    {
                        if (CommonProperties.CPUStationsDictionary[i][y].isActiveAndEnabled && (CommonProperties.CPUStationsDictionary[i][y].stationPosition - shipTransform.position).magnitude <= attackDistance)
                        {
                            closeStations.Add(CommonProperties.CPUStationsDictionary[i][y]);
                        }
                    }
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


        if (closeBattleShips.Count > 0 /*|| closePlayerGuns.Count>0*/ || closeStations.Count > 0 || closeStars.Count > 0)
        {
            attackMode = true;
            if (isCruiser) {
                if (countOfEnemyShipsNear() > 1)
                {
                    if (!CommonProperties.CPUMegaAttackBattleShipsDictionary[CPUNumber].Contains(this)) CommonProperties.CPUMegaAttackBattleShipsDictionary[CPUNumber].Add(this);
                    if (!CommonProperties.MegaAttackCoroutineIsOn[CPUNumber] && CommonProperties.MegaAttackTimer[CPUNumber] <= 0) StartCoroutine(CommonProperties.MegaAttack(CPUNumber));
                }
                else CommonProperties.CPUMegaAttackBattleShipsDictionary[CPUNumber].Remove(this);
            }
            
        }
        else
        {
            attackMode = false;
            //gunToAttak = null;
            shipToAttak = null;
            stationToAttak = null;
            starToAttak = null;
            if (attackTimerTime < Random.Range(attackTimeMin, attackTimeMax)) attackTimerTime = Random.Range(attackTimeMin, attackTimeMax);
            if (shieldTimerTime < Random.Range(shieldTimeMin, shieldTimeMax)) shieldTimerTime = Random.Range(shieldTimeMin, shieldTimeMax);
        }
    }

    IEnumerator attackTheEnemy()
    {
        //if (gunToAttak != null)
        //{
        //    if (!gunToAttak.isActiveAndEnabled || (gunToAttak.gunTransform.position - shipTransform.position).magnitude > attackDistance)
        //    {
        //        if (closePlayerGuns.Contains(gunToAttak)) closePlayerGuns.Remove(gunToAttak);
        //        gunToAttak = null;
        //    }
        //    else
        //    {
        //        attackLaserLine.SetPosition(0, shipTransform.position);
        //        attackLaserLine.SetPosition(1, gunToAttak.gunTransform.position);
        //        attackLaserLine.enabled = true;
        //    }
        //}
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
        else if (closeBattleShips.Count > 0) {
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
                shipToAttak = closeBattleShips.Count < 2 ? closeBattleShips[0] : closeBattleShips[Random.Range(0, closeBattleShips.Count)];
                if (shipToAttak.isActiveAndEnabled)
                {
                    attackLaserLine.SetPosition(0, shipTransform.position);
                    attackLaserLine.SetPosition(1, shipToAttak.shipTransform.position);
                    attackLaserLine.enabled = true;
                }
                else shipToAttak = null;
            }
            //else if (closePlayerGuns.Count > 0)
            //{
            //    gunToAttak = closePlayerGuns[0];
            //    if (gunToAttak.isActiveAndEnabled)
            //    {
            //        attackLaserLine.SetPosition(0, shipTransform.position);
            //        attackLaserLine.SetPosition(1, gunToAttak.gunTransform.position);
            //        attackLaserLine.enabled = true;
            //    }
            //    else gunToAttak = null;
            //}
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
            if (!shipToAttak.shieldIsOn)
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
        }
        //else if (gunToAttak != null) {
        //    gunToAttak.reduceTheHPOfGun(harm);
        //}
        else if (stationToAttak != null)
        {
            stationToAttak.reduceTheHPOfStation(harmStar);
        }
        else if (starToAttak != null)
        {
            starToAttak.reduceTheHPOfStar(harmStar, CPUNumber);
        }

        closeBattleShips.Clear();
        //closePlayerGuns.Clear();
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
            if (shipToAttak.isActiveAndEnabled && (shipToAttak.shipTransform.position - shipTransform.position).magnitude < attackDistance && !shipToAttak.isUnderMegaDefence && !shipToAttak.isParalyzed) {
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

            for (int i = 0; i < CommonProperties.playerBattleShips.Count; i++)
            {
                if ((CommonProperties.playerBattleShips[i].shipTransform.position - shipTransform.position).magnitude <= attackDistance - 4 && !CommonProperties.playerBattleShips[i].shieldIsOn)
                {
                    CommonProperties.playerBattleShips[i].StartCoroutine(CommonProperties.playerBattleShips[i].megaAttackEffectForThisShip(levelOfShip));
                }
            }
            for (int i = 0; i < CommonProperties.CPUBattleShipsDictionary.Count; i++)
            {
                if (i != (CPUNumber - 1))
                {
                    for (int y = 0; y < CommonProperties.CPUBattleShipsDictionary[i].Count; y++)
                    {
                        if ((CommonProperties.CPUBattleShipsDictionary[i][y].shipTransform.position - shipTransform.position).magnitude <= attackDistance - 4 && !CommonProperties.CPUBattleShipsDictionary[i][y].shieldIsOn)
                        {
                            CommonProperties.CPUBattleShipsDictionary[i][y].StartCoroutine(CommonProperties.CPUBattleShipsDictionary[i][y].megaAttackEffectForThisShip(levelOfShip));
                        }
                    }
                }
            }
        }
        else if (levelOfShip == 1) {
            megaAttackOfCruis1.SetActive(true);
        }
    }

    //CPUNumber - 1 only for point to 0 index in CPUBattle shipt dictionary. Because the dictionary contains only the CPU battle ships collections
    //in this exact case it gets this CPU battle ships collections
    IEnumerator disactivateMegaAttack() {
        yield return new WaitForSeconds(2.5f);
        if (megaAttackOfCruis3 != null) megaAttackOfCruis3.SetActive(false);
        if (megaAttackOfCruis2 != null) megaAttackOfCruis2.SetActive(false);

        //for level3 cruiser the effect of mega attack (disappearing) works only after the visual effect of mega attack ends. 
        if (levelOfShip == 3)
        {
            for (int i = 0; i < CommonProperties.CPUBattleShipsDictionary.Count; i++)
            {
                if (i == (CPUNumber - 1))
                {
                    for (int y = 0; y < CommonProperties.CPUBattleShipsDictionary[i].Count; y++)
                    {
                        if (CommonProperties.CPUBattleShipsDictionary[i][y] != this && (CommonProperties.CPUBattleShipsDictionary[i][y].shipTransform.position - shipTransform.position).magnitude <= attackDistance)
                        {
                            closeBattleShips.Add(CommonProperties.CPUBattleShipsDictionary[i][y]);
                            CommonProperties.CPUBattleShipsDictionary[i][y].StartCoroutine(CommonProperties.CPUBattleShipsDictionary[i][y].megaAttackEffectForThisShip(levelOfShip));
                        }
                    }
                }
            }
            StartCoroutine(megaAttackEffectForThisShip(levelOfShip));
        }
    }
    public IEnumerator megaAttackEffectForThisShip(int cruiserLevel)
    {
        int secondsToWait = cruiserLevel == 3 ? 7 : 9;

        if (cruiserLevel == 3)
        {
            foreach (MeshRenderer MR in GetComponentsInChildren<MeshRenderer>()) MR.enabled = false;
            isUnderMegaDefence = true;
        }
        else
        {
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

    IEnumerator defenceShieldTurn()
    {
        shieldIsOn = true;
        powerShiled.SetActive(true);
        yield return new WaitForSeconds(shieldOnTime);
        powerShiled.SetActive(false);
        shieldIsOn = false;
    }

    public override void reduceTheHPOfShip(float harmAmount)
    {
        HP -= harmAmount;
        if (HP <= 4) preBurstEffect();
        if (HP <= 0)
        {
            disactivateThisShip();
        }
    }
    private void disactivateThisShip()
    {
        if (isCruiser) BurstList = ObjectPullerRTS.current.GetCruisBurstPull();
        else BurstList = ObjectPullerRTS.current.GetDestrBurstPull();
        BurstReal = ObjectPullerRTS.current.GetGameObjectFromPull(BurstList);
        BurstReal.transform.position = shipTransform.position;
        BurstReal.SetActive(true);
        if (maternalStation != null)
        {
            maternalStation.ShipsAssigned--;
            if (maternalStation.groupWhereTheStationIs != null /*&& maternalStation.groupWhereTheStationIs.Count > 0*/)
            {   
                //sending the sygnal to maternal station to produce more ships but only to defence minimum
                if (maternalStation.stationCurrentLevel == 0 && CommonProperties.energyOfStationGroups[maternalStation.groupWhereTheStationIs] > CommonProperties.D4ProdEnergy) maternalStation.utilaizeTheEnergyOfCPUGroup(1); 
                else if (maternalStation.stationCurrentLevel == 1 && CommonProperties.energyOfStationGroups[maternalStation.groupWhereTheStationIs] > CommonProperties.D3ProdEnergy) maternalStation.utilaizeTheEnergyOfCPUGroup(1); 
                else if (maternalStation.stationCurrentLevel == 1 && CommonProperties.energyOfStationGroups[maternalStation.groupWhereTheStationIs] > CommonProperties.D2ProdEnergy) maternalStation.utilaizeTheEnergyOfCPUGroup(1); 
                else if (maternalStation.stationCurrentLevel == 1 && CommonProperties.energyOfStationGroups[maternalStation.groupWhereTheStationIs] > CommonProperties.D1ProdEnergy) maternalStation.utilaizeTheEnergyOfCPUGroup(1);
            }
            else
            {
                if (maternalStation.stationCurrentLevel == 0 && maternalStation.energyOfStation > CommonProperties.D4ProdEnergy) maternalStation.utilaizeTheEnergy(true); //sending the sygnal to maternal station to produce more ships
                else if (maternalStation.stationCurrentLevel == 1 && maternalStation.energyOfStation > CommonProperties.D3ProdEnergy) maternalStation.utilaizeTheEnergy(true); //sending the sygnal to maternal station to produce more ships
                else if (maternalStation.stationCurrentLevel == 1 && maternalStation.energyOfStation > CommonProperties.D2ProdEnergy) maternalStation.utilaizeTheEnergy(true); //sending the sygnal to maternal station to produce more ships
                else if (maternalStation.stationCurrentLevel == 1 && maternalStation.energyOfStation > CommonProperties.D1ProdEnergy) maternalStation.utilaizeTheEnergy(true); //sending the sygnal to maternal station to produce more ships
            }
        }

        if (megaAttackOfCruis3 != null) megaAttackOfCruis3.SetActive(false);
        if (megaAttackOfCruis2 != null) megaAttackOfCruis2.SetActive(false);
        isUnderMegaDefence = false;
        if (isCruiser) CommonProperties.CPUMegaAttackBattleShipsDictionary[CPUNumber].Remove(this);
        maternalStation = null;
        attackLaserLine.enabled = false;
        StopCoroutine(attackTheEnemy());
        if (isParalyzer) paralizerLaserLine.enabled = false;
        powerShiled.SetActive(false);
        closeBattleShips.Clear();
        //closePlayerGuns.Clear();
        closeStations.Clear();
        closeStars.Clear();
        attackMode = false;
        //gunToAttak = null;
        shipToAttak = null;
        stationToAttak = null;
        starToAttak = null;
        isParalyzer = false;
        isParalyzed = false;
        paralizedEffect.SetActive(false);
        CommonProperties.CPUBattleShips.Remove(this);
        CommonProperties.CPUBattleShipsDictionary[CPUNumber - 1].Remove(this);

        //StationClass closeStation = getClosestStation();
        //if (closeStation != null)
        //{
        //    //if station is defentless it calls all its alive ships back to defend it
        //    if (closeStation.stationDefenceFleetLeftByUnits() < 3)
        //    {
        //        if (CommonProperties.CPUStationsDictionary[CPUNumber - 1].Count > 1)
        //        {
        //            closeStation.callForAHelp();
        //        }
        //    }
        //}
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

    private StationClass getClosestStation()
    {
        //"CPUNumber - 1)" means that in dictionary the first element is the first CPU stations collection.So stations with 0 index are 1 CPU stations. That does not mean that it gets player stations.
        if (CommonProperties.CPUStationsDictionary[CPUNumber - 1].Count > 1)
        {
            StationClass closetsStation = null;
            for (int i = 0; i < CommonProperties.CPUStationsDictionary[CPUNumber - 1].Count; i++)
            {
                if (i == 0) closetsStation = CommonProperties.CPUStationsDictionary[CPUNumber - 1][i];
                else
                {
                    if ((closetsStation.stationPosition - shipTransform.position).sqrMagnitude > (CommonProperties.CPUStationsDictionary[CPUNumber - 1][i].stationPosition - shipTransform.position).sqrMagnitude) closetsStation = CommonProperties.CPUStationsDictionary[CPUNumber - 1][i]; ;
                }
            }
            return closetsStation;
        }
        else if (CommonProperties.CPUStationsDictionary[CPUNumber - 1].Count == 1) return CommonProperties.CPUStationsDictionary[CPUNumber - 1][0];
        else return null;
    }

    public void giveAShipMoveOrder(Vector3 moveTowards, List <Vector3> wayPoints)
    {
        moveToPoint = moveTowards;
        if (wayPoints==null)
        {
            //rotating the ship to look to move direction
            if (moveToPoint != shipTransform.position)
            {
                float yRotation = Quaternion.LookRotation(moveToPoint - shipTransform.position, Vector3.up).eulerAngles.y;
                transform.rotation = Quaternion.Euler(0, yRotation + 90, 0);
                if (!isParalyzed) engineSound.Play();
                moveToPointInWay = moveToPoint;
                isMoving = true;
            }
        }
        else
        {
            this.wayPoints = wayPoints;
            //if (nextPointToMoveIndex < this.wayPoints.Count - 1) nextPointToMoveIndex++;
            float yRotation = Quaternion.LookRotation(this.wayPoints[0] - shipTransform.position, Vector3.up).eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, yRotation + 90, 0);
            moveToPointInWay = this.wayPoints[0];
            if (!isParalyzed) engineSound.Play();
            isMoving = true;
        }
    }

    private void moveToNextPointInAWay()
    {
        if (nextPointToMoveIndex < wayPoints.Count - 1)
        {
            nextPointToMoveIndex++;
            float yRotation = Quaternion.LookRotation(wayPoints[nextPointToMoveIndex] - shipTransform.position, Vector3.up).eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, yRotation + 90, 0);
            moveToPointInWay = wayPoints[nextPointToMoveIndex];
            isMoving = true;
        }
        else {
            nextPointToMoveIndex=0;
            wayPoints = null;
            moveToPointInWay = moveToPoint;
            float yRotation = Quaternion.LookRotation(moveToPoint - shipTransform.position, Vector3.up).eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, yRotation + 90, 0);
            isMoving = true;
        }
    }


    private void Update()
    {
        if (moveToPoint != moveToPointInWay && (moveToPointInWay - shipTransform.position).magnitude < 10)
        {
            moveToNextPointInAWay();
        }

        if (shipToAttak == null /*&& stationToAttak == null /*&& gunToAttak == null && starToAttak == null*/ && !isParalyzed) collectTheCloseEnemyShips();
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
            if (!isParalyzed) StartCoroutine(attackTheEnemy());
            paralizerShotCounter++;
            if (isParalyzer && paralizerShotCounter >= (levelOfShip + 1))
            {
                paralizerShotCounter = 0;
                StartCoroutine(attackTheEnemyWithParalyzer());
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
            attackLaserLine.SetPosition(0, shipTransform.position);
            if (isParalyzer && paralizerLaserLine.enabled) paralizerLaserLine.SetPosition(0, shipTransform.position);
            gameObject.transform.position = Vector3.MoveTowards(transform.position, moveToPointInWay, speedOfThisShip);
            if (transform.position == moveToPoint)
            {
                isMoving = false;
            }
        }
        //if (attackLaserLine.enabled && shipToAttak != null && shipToAttak.HP > 0)
        //{
        //    attackLaserLine.SetPosition(1, shipToAttak.shipTransform.position);
        //    if (shipToAttak.HP <= 0) attackLaserLine.enabled = false;
        //}
        //if (isParalyzer && paralizerLaserLine.enabled && shipToAttak != null && shipToAttak.HP > 0)
        //{
        //    paralizerLaserLine.SetPosition(1, shipToAttak.shipTransform.position);
        //    if (shipToAttak.HP <= 0) paralizerLaserLine.enabled = false;
        //}
    }
}
