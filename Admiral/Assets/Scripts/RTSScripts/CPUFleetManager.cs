using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;

public class CPUFleetManager : MonoBehaviour
{
    //first key is CPUNumber and second is this CPU number fleet state
    //States are next (also by priority):
    /// <summary>
    /// 0-fleet does nothing (default), 
    /// 1-prepare for attack
    /// 2-stationAttack, 
    /// 3-starAttack
    /// 4-stationDefence
    /// </summary>
    /// 
    private Dictionary<int, byte> CPUFleetStates;
    private Dictionary<int, float> CPUFleetPrepareTimer;
    private Dictionary<int, float> CPUFleetAttackTimer;
    private Dictionary<int, bool> CPUIsPrepearingToAttack;
    private Dictionary<int, Vector3> CPUStartToAttackPoint;
    private Dictionary<int, StationClass> CPUStationUderDefence;

    private const float radiusOfShipsRingAroundStation = 6;
    private const int SHIPS_COUNT_MINIMUM_TO_ATTACK = 13;
    private const int FLEET_ATTACK_TIME = 90;
    private const int FLEET_ATTACK_PREPARE_TIME = 10;

    private int innnerCircleMax;
    private float radiusGroup;

    private const byte freeState = 0;
    private const byte prepareState = 1;//this one passes comands to next attack station or star
    private const byte staionAttackState = 2;
    private const byte starAttackState = 3;
    private const byte defenceState = 4; //this one beats any other

    private List<CPUBattleShip> CPUBattleShipsPrepearedToAttack;

    public const float oneStepCloseStationsMaxDistance = 140f;

    private List<Vector3> squardPositions;

    private void Awake()
    {
        CPUFleetStates = new Dictionary<int, byte>
        {
            { 1, 0 },
            { 2, 0 },
            { 3, 0 },
            { 4, 0 },
        };
        CPUFleetPrepareTimer = new Dictionary<int, float>
        {
            { 1, FLEET_ATTACK_PREPARE_TIME },
            { 2, FLEET_ATTACK_PREPARE_TIME },
            { 3, FLEET_ATTACK_PREPARE_TIME },
            { 4, FLEET_ATTACK_PREPARE_TIME },
        }; 
        CPUFleetAttackTimer = new Dictionary<int, float>
        {
            { 1, FLEET_ATTACK_TIME },
            { 2, FLEET_ATTACK_TIME },
            { 3, FLEET_ATTACK_TIME },
            { 4, FLEET_ATTACK_TIME },
        };
        CPUIsPrepearingToAttack = new Dictionary<int, bool>
        {
            { 1, false },
            { 2, false },
            { 3, false },
            { 4, false },
        };
        CPUStartToAttackPoint = new Dictionary<int, Vector3>     
        {
            { 1, Vector3.zero},
            { 2, Vector3.zero},
            { 3, Vector3.zero},
            { 4, Vector3.zero},
        };
        CPUStationUderDefence = new Dictionary<int, StationClass>
        {
            { 1, null},
            { 2, null},
            { 3, null},
            { 4, null},
        };


        innnerCircleMax = 8;
        radiusGroup = 3;
        CPUBattleShipsPrepearedToAttack = new List<CPUBattleShip>();

        squardPositions = new List<Vector3>();
    }

    public void cancelCallForHelp(int CPUNumber) {
            CPUFleetStates[CPUNumber] = freeState;
            CPUStationUderDefence[CPUNumber] = null;
    }
    
    public void callForHelp(int CPUNumber, Vector3 stationPosition, StationClass station)
    {
        if (CPUFleetStates[CPUNumber] != defenceState)
        {
            CPUFleetStates[CPUNumber] = defenceState;
            CPUStationUderDefence[CPUNumber] = station;
            for (int i = 0; i < CommonProperties.CPUBattleShipsDictionary[CPUNumber - 1].Count; i++)
            {
                Vector3 newPos;
                float step = (Mathf.PI * 2) / CommonProperties.CPUBattleShipsDictionary[CPUNumber - 1].Count; // отступ
                newPos.x = stationPosition.x + Mathf.Sin(step * i) * radiusOfShipsRingAroundStation; // по оси X
                newPos.z = stationPosition.z + Mathf.Cos(step * i) * radiusOfShipsRingAroundStation; // по оси Z
                newPos.y = 0; // по оси Y всегда 0
                squardPositions.Add(newPos);
            }
            for (int i = 0; i < CommonProperties.CPUBattleShipsDictionary[CPUNumber - 1].Count; i++)
            {
                CommonProperties.CPUBattleShipsDictionary[CPUNumber - 1][i].giveAShipMoveOrder(squardPositions[i], null);
            }
            squardPositions.Clear();
        }
    }

    public bool checkIfStationUnderDefence(int CPUNumber, StationClass station) {
        if (CPUFleetStates[CPUNumber] == defenceState && CPUStationUderDefence[CPUNumber] == station) return true;
        else return false;
    }

    public StationClass getTheStationUNderDefence(int CPUNumber)
    {
        if (CPUFleetStates[CPUNumber] == defenceState) return CPUStationUderDefence[CPUNumber];
        else return null;
    }

    public void preapareForAttack(int CPUNumber, Vector3 stationPosition)
    {
        if (CPUFleetStates[CPUNumber] == freeState)
        {
            if (CommonProperties.CPUBattleShipsDictionary[CPUNumber - 1].Count > CommonProperties.CPUStationsDictionary[CPUNumber - 1].Count * SHIPS_COUNT_MINIMUM_TO_ATTACK)
            {
                //reducing the battle ships count to attack is for leave some ships to defence
                for (int i = 0; i < (CommonProperties.CPUBattleShipsDictionary[CPUNumber - 1].Count - CommonProperties.CPUStationsDictionary[CPUNumber - 1].Count * 3); i++)
                {
                    CPUBattleShipsPrepearedToAttack.Add(CommonProperties.CPUBattleShipsDictionary[CPUNumber - 1][i]);
                }
                determineThePointsForFleetToMove(stationPosition, Vector3.zero);

                for (int i = 0; i < CPUBattleShipsPrepearedToAttack.Count; i++)
                {
                    CPUBattleShipsPrepearedToAttack[i].giveAShipMoveOrder(squardPositions[i], null);
                }
                CPUFleetPrepareTimer[CPUNumber] = FLEET_ATTACK_PREPARE_TIME;
                CPUIsPrepearingToAttack[CPUNumber] = true;
                CPUStartToAttackPoint[CPUNumber] = stationPosition;
                CPUFleetStates[CPUNumber] = prepareState;
                squardPositions.Clear();
            }
        }
    }

    public void attackTheStation(int CPUNumber, Vector3 gatheredPoint)
    {
        byte index = 0;
        List<StationClass> stationsToAttack = null;
        StationClass stationToAttack = null;

        for (int i = 0; i < CommonProperties.allStations.Count; i++)
        {
            if (CommonProperties.allStations[i].CPUNumber != CPUNumber)
            {
                if ((CommonProperties.allStations[i].stationPosition - gatheredPoint).magnitude <= oneStepCloseStationsMaxDistance)
                    stationsToAttack.Add(CommonProperties.allStations[i]);
            }
        }
        if (stationsToAttack.Count == 1)
        {
            determineThePointsForFleetToMove(stationsToAttack[0].stationPosition, gatheredPoint);
            for (int i = 0; i < CPUBattleShipsPrepearedToAttack.Count; i++)
            {
                if (CPUBattleShipsPrepearedToAttack[i] != null && CPUBattleShipsPrepearedToAttack[i].CPUNumber == CPUNumber && CPUBattleShipsPrepearedToAttack[i].isActiveAndEnabled)
                    CPUBattleShipsPrepearedToAttack[i].giveAShipMoveOrder(squardPositions[i], null);
            }
        }
        else if (stationsToAttack.Count != 0)
        {
            for (int i = 0; i < stationsToAttack.Count; i++)
            {
                if (index == 0) stationToAttack = stationsToAttack[i];
                else
                {
                    if (stationsToAttack[i].stationDefenceFleetPower()< stationToAttack.stationDefenceFleetPower()) stationToAttack = CommonProperties.allStations[i];
                }
                index++;
            }

            determineThePointsForFleetToMove(stationToAttack.stationPosition, CPUStartToAttackPoint[CPUNumber]);
            for (int i = 0; i < CPUBattleShipsPrepearedToAttack.Count; i++)
            {
                if (CPUBattleShipsPrepearedToAttack[i] != null && CPUBattleShipsPrepearedToAttack[i].CPUNumber == CPUNumber && CPUBattleShipsPrepearedToAttack[i].isActiveAndEnabled)
                    CPUBattleShipsPrepearedToAttack[i].giveAShipMoveOrder(squardPositions[i], null);
            }
        }
        CPUBattleShipsPrepearedToAttack.Clear();
        squardPositions.Clear();
        CPUFleetStates[CPUNumber] = staionAttackState;
        CPUFleetAttackTimer[CPUNumber] = FLEET_ATTACK_TIME;
    }


    private void determineThePointsForFleetToMove(Vector3 pointToMove, Vector3 gatherPoint) {
        float stepForOuterRadius = 1;
        Vector3 moveToPoint;
        //that means that fleet will not placed in center of enemy station or star or ally station, it will stay just 7 steps before
        //if there is no start point means that CPU gathers its fleet, and no need for before destination point gap calculations
        if (gatherPoint != Vector3.zero) moveToPoint = pointToMove + (gatherPoint - pointToMove).normalized * 6;
        else moveToPoint = pointToMove;

        if (CPUBattleShipsPrepearedToAttack.Count > 1)
        {
            for (int i = 0; i < CPUBattleShipsPrepearedToAttack.Count; i++)
            {
                if (i == 0)
                {
                    squardPositions.Add(moveToPoint);
                }
                else if (i <= innnerCircleMax)
                {
                    if (radiusGroup != 3) radiusGroup = 3;
                    Vector3 newPos;
                    float step = (Mathf.PI * 2) / innnerCircleMax; // отступ
                    newPos.x = moveToPoint.x + Mathf.Sin(step * i) * radiusGroup; // по оси X
                    newPos.z = moveToPoint.z + Mathf.Cos(step * i) * radiusGroup; // по оси Z
                    newPos.y = 0; // по оси Y всегда 0
                    squardPositions.Add(newPos);
                    if (i == innnerCircleMax)
                    {
                        if ((CPUBattleShipsPrepearedToAttack.Count - squardPositions.Count) > innnerCircleMax * 2) stepForOuterRadius = innnerCircleMax * 2;
                        else stepForOuterRadius = CPUBattleShipsPrepearedToAttack.Count - squardPositions.Count;
                    }
                }
                else if (i <= (innnerCircleMax * 3))
                {
                    if (radiusGroup != 6) radiusGroup = 6;
                    Vector3 newPos;
                    float step = (Mathf.PI * 2) / stepForOuterRadius; // отступ
                    newPos.x = moveToPoint.x + Mathf.Sin(step * i) * radiusGroup; // по оси X
                    newPos.z = moveToPoint.z + Mathf.Cos(step * i) * radiusGroup; // по оси Z
                    newPos.y = 0; // по оси Y всегда 0
                    squardPositions.Add(newPos);
                    if (i == (innnerCircleMax * 3))
                    {
                        if ((CPUBattleShipsPrepearedToAttack.Count - squardPositions.Count) > innnerCircleMax * 3) stepForOuterRadius = innnerCircleMax * 3;
                        else stepForOuterRadius = CPUBattleShipsPrepearedToAttack.Count - squardPositions.Count;
                    }
                }
                else if (i <= (innnerCircleMax * 7))
                {
                    if (radiusGroup != 9) radiusGroup = 9;
                    Vector3 newPos;
                    float step = (Mathf.PI * 2) / stepForOuterRadius; // отступ
                    newPos.x = moveToPoint.x + Mathf.Sin(step * i) * radiusGroup; // по оси X
                    newPos.z = moveToPoint.z + Mathf.Cos(step * i) * radiusGroup; // по оси Z
                    newPos.y = 0; // по оси Y всегда 0
                    squardPositions.Add(newPos);
                    if (i == (innnerCircleMax * 7))
                    {
                        if ((CPUBattleShipsPrepearedToAttack.Count - squardPositions.Count) > innnerCircleMax * 4) stepForOuterRadius = innnerCircleMax * 4;
                        else stepForOuterRadius = CPUBattleShipsPrepearedToAttack.Count - squardPositions.Count;
                    }
                }
                else if (i <= (innnerCircleMax * 15))
                {
                    if (radiusGroup != 12) radiusGroup = 12;
                    Vector3 newPos;
                    float step = (Mathf.PI * 2) / stepForOuterRadius; // отступ
                    newPos.x = moveToPoint.x + Mathf.Sin(step * i) * radiusGroup; // по оси X
                    newPos.z = moveToPoint.z + Mathf.Cos(step * i) * radiusGroup; // по оси Z
                    newPos.y = 0; // по оси Y всегда 0
                    squardPositions.Add(newPos);
                }
            }
        }
    }

    private void Update()
    {
        //time before fleet of CPU starts mass attack
        if (CPUFleetPrepareTimer[1] > 0 && CPUIsPrepearingToAttack[1])
        {
            CPUFleetPrepareTimer[1] -= Time.deltaTime;
            if (CPUFleetPrepareTimer[1] <= 0)
            {
                CPUIsPrepearingToAttack[1] = false;
                attackTheStation(1, CPUStartToAttackPoint[1]);
            }
        }
        if (CPUFleetPrepareTimer[2] > 0 && CPUIsPrepearingToAttack[2])
        {
            CPUFleetPrepareTimer[2] -= Time.deltaTime;
            if (CPUFleetPrepareTimer[2] <= 0)
            {
                CPUIsPrepearingToAttack[2] = false;
                attackTheStation(2, CPUStartToAttackPoint[2]);
            }
        }
        if (CPUFleetPrepareTimer[3] > 0 && CPUIsPrepearingToAttack[3])
        {
            CPUFleetPrepareTimer[3] -= Time.deltaTime;
            if (CPUFleetPrepareTimer[3] <= 0)
            {
                CPUIsPrepearingToAttack[3] = false;
                attackTheStation(3, CPUStartToAttackPoint[3]);
            }
        }
        if (CPUFleetPrepareTimer[4] > 0 && CPUIsPrepearingToAttack[4])
        {
            CPUFleetPrepareTimer[4] -= Time.deltaTime;
            if (CPUFleetPrepareTimer[4] <= 0)
            {
                CPUIsPrepearingToAttack[4] = false;
                attackTheStation(4, CPUStartToAttackPoint[4]);
            }
        }

        //time before station attack state will be canceled
        if (CPUFleetPrepareTimer[1] > 0 && CPUFleetStates[1]==staionAttackState)
        {
            CPUFleetPrepareTimer[1] -= Time.deltaTime;
            if (CPUFleetPrepareTimer[1] <= 0)
            {
                CPUFleetStates[1] = freeState;
            }
        }
        if (CPUFleetPrepareTimer[2] > 0 && CPUFleetStates[2] == staionAttackState)
        {
            CPUFleetPrepareTimer[2] -= Time.deltaTime;
            if (CPUFleetPrepareTimer[2] <= 0)
            {
                CPUFleetStates[2] = freeState;
            }
        }
        if (CPUFleetPrepareTimer[3] > 0 && CPUFleetStates[3] == staionAttackState)
        {
            CPUFleetPrepareTimer[3] -= Time.deltaTime;
            if (CPUFleetPrepareTimer[3] <= 0)
            {
                CPUFleetStates[3] = freeState;
            }
        }
        if (CPUFleetPrepareTimer[4] > 0 && CPUFleetStates[4] == staionAttackState)
        {
            CPUFleetPrepareTimer[4] -= Time.deltaTime;
            if (CPUFleetPrepareTimer[4] <= 0)
            {
                CPUFleetStates[4] = freeState;
            }
        }
    }
}
