
using UnityEngine;

public class GuardAttack : MonoBehaviour
{

    //references to player journey scene cruisers
    private string Cruis1PlayerTag = "BullCruis1";
    private string Cruis2PlayerTag = "BullCruis2";
    private string Cruis3PlayerTag = "BullCruis3";
    private string Cruis4PlayerTag = "BullCruis4";
    private EnergonMngr energonMngr;


    void Start()
    {
        energonMngr = GetComponentInParent<EnergonMngr>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Cruis1PlayerTag) || other.CompareTag(Cruis2PlayerTag) || other.CompareTag(Cruis3PlayerTag) || other.CompareTag(Cruis4PlayerTag))
        {
            LaunchingObjcts playerCruisObject = other.GetComponent<LaunchingObjcts>();
            energonMngr.isChasing = false; //stop to chase the player cruiser after guard has catched it

            //so scene switch with turning to battle or guard scene will be only in case if player cruiser has at least one cruiser
            if ((playerCruisObject.Cruis1 + playerCruisObject.Cruis2 + playerCruisObject.Cruis3 + playerCruisObject.Cruis4) > 0)
            {
                //this second level if condition is necessary to prevent a guard ship to attack a player cruiser while it is ported to its station
                if (!playerCruisObject.isPortedToPlayerStation && !SpaceCtrlr.Instance.adsPanelIsOn)
                {
                    if ((playerCruisObject.assessFleetPower() > energonMngr.assessFleetPower() && ((float)energonMngr.assessFleetPower() / playerCruisObject.assessFleetPower()) > 0.7f)
                        || (playerCruisObject.assessFleetPower() < energonMngr.assessFleetPower() && ((float)playerCruisObject.assessFleetPower() / energonMngr.assessFleetPower()) > 0.7f))
                    {
                        SpaceCtrlr.Instance.SaveFile(); //saving data of scene while changing the scene cause date is not saved on quit from other scenes

                        Lists.setShipsForPlayer(playerCruisObject.Cruis1, playerCruisObject.Cruis2, playerCruisObject.Cruis3, playerCruisObject.Cruis4, playerCruisObject.Gun1,
                        playerCruisObject.Gun2, playerCruisObject.Gun3, playerCruisObject.Destr1, playerCruisObject.Destr1Par, playerCruisObject.Destr2,
                        playerCruisObject.Destr2Par, playerCruisObject.Destr3, playerCruisObject.Destr4, playerCruisObject.MiniGun);

                        //setting the fleet of guards
                        Lists.setShipsForCPU(0, 0, 0, 0, energonMngr.CruisG, energonMngr.Gun1, energonMngr.Gun2, energonMngr.Gun3, 0, 0, 0, 0, 0, 0, energonMngr.DestrG);

                        //to not set any stations on scene cause here fights a ships only
                        Lists.stationTypeLists = 0;

                        // saving the instance of player ship that is under attack to update it's fleet count later after the battle on SpaceCtrl class here is no need to save 
                        //the reference to guard ship cause it will just be respawned
                        Lists.shipOnAttack = other.gameObject;
                        Lists.CPUShipOnAttack = energonMngr.gameObject;

                        Lists.guardAttacksShip();

                        //assignin a proper color to UI token of opposite CPU player that is under attack. In this case this is exception cause guard attacks.
                        //it sets tje UI color to white
                        Lists.colorOfOpposite = new Color(1.5f, 1.5f, 1.5f, 1);

                        //setting the time on jpurney scene to regular scale before switching the scene to attack
                        Time.timeScale = 1;
                        Time.fixedDeltaTime = 0.02f;

                        if (energonMngr.Fighter == 0)
                        {
                            SceneSwitchMngr.LoadBattleScene();
                        }
                        else
                        {
                            SpaceCtrlr.Instance.setTheMaketNumberForDefenceScene(false, 0, playerCruisObject); //setting proper dummy to defende on defence scene (0 is default value and does not effect)
                            Lists.dummyOnDefenceSceneEnemy = 5; //5 is for guard cruiser dummy on defece scene as enemy cruiser
                            Lists.setFightersForCPU(energonMngr.Fighter);
                            SceneSwitchMngr.LoadDefenceScene();
                        }
                    }
                    else
                    {
                        if (playerCruisObject.assessFleetPower() < energonMngr.assessFleetPower())
                        {
                            energonMngr.reducingTheFleetOFCruiserOnBattle(1 - ((float)playerCruisObject.assessFleetPower() / energonMngr.assessFleetPower() * 0.75f));
                            playerCruisObject.makePlayerCruiserDefault();
                            Lists.shipsOnScene.Remove(playerCruisObject.gameObject);
                            float reduceAmount;
                            if (Lists.isBlackDimension) reduceAmount = Constants.Instance.enrgyGainLoseDark * 0.3f;
                            else if (Lists.isBlueDimension) reduceAmount = Constants.Instance.enrgyGainLoseBlue * 0.3f;
                            else reduceAmount = Constants.Instance.enrgyGainLoseRed * 0.3f;
                            SpaceCtrlr.Instance.changingTheEnergyOfPlayer(true, reduceAmount);
                            playerCruisObject.disactivatingCurrentShip();
                        }
                        else {
                            playerCruisObject.reducingTheFleetOFCruiserOnBattle(1 - ((float)energonMngr.assessFleetPower() / playerCruisObject.assessFleetPower() * 0.75f));
                            Lists.energonsOnScene.Remove(energonMngr.gameObject);
                            SpaceCtrlr.Instance.gainSound.Play(); 
                            SpaceCtrlr.Instance.changingTheEnergyOfPlayer(true, energonMngr.energyOfEnergonAndGuard);
                            energonMngr.disactivatingCurrentShip();
                        }
                    }
                }
            }
            //so if player scene cruiser will not have any cruisers in fleet it will lose energy and all fleet  
            else
            {
                energonMngr.reducingTheFleetOFCruiserOnBattle(0.95f);
                playerCruisObject.makePlayerCruiserDefault();
                Lists.shipsOnScene.Remove(playerCruisObject.gameObject);
                float reduceAmount;
                if (Lists.isBlackDimension) reduceAmount = Constants.Instance.enrgyGainLoseDark * 0.3f;
                else if (Lists.isBlueDimension) reduceAmount = Constants.Instance.enrgyGainLoseBlue * 0.3f;
                else reduceAmount = Constants.Instance.enrgyGainLoseRed * 0.3f;
                SpaceCtrlr.Instance.changingTheEnergyOfPlayer(true, reduceAmount);
                playerCruisObject.disactivatingCurrentShip();
            }
        }
    }
}
