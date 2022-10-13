using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunCtrlr : MonoBehaviour
{
    private GameObject gunParaliser; //gun paralizer local GO
    //public GameObject gunParaliserReal; //gun paralizer public GO

    //public GameObject gunExploisionReal;

    //public GameObject[] gunParts;

    public GameObject shotEffect;

    public GameObject burningEffect;

    private Animator anim;

    private GunPlayer gunPlayer;

    private bool isParalized = false;
    private bool isDestroying = false;

    public GameObject shottingGunBarrel;

    private int GUN_LIFE; //HP of Gun

    private float PARALIZED_TIME1;
    private float PARALIZED_TIME2;

    //harm levels of bullet hits
    private int DESTR_4_HARM;
    private int CRUIS_4_HARM;
    private int DESTR_3_HARM;
    private int CRUIS_3_HARM;
    private int DESTR_2_HARM;
    private int CRUIS_2_HARM;
    private int DESTR_1_HARM;
    private int CRUIS_1_HARM;

    //this lists are necessary to assign proper paralized effect types for ships and pull the paralized effect from ObjectPuller class
    private List<GameObject> paralEffectListToActivate;

    //this lists are necessary to assign proper burst effect types for ships and pull the burst effect from ObjectPuller class
    private List<GameObject> ShipBurstEffectListToActivate;

    private void OnDestroy()
    {
        Lists.AllPlayerShips.Remove(gameObject);
        Lists.AllPlayerGuns.Remove(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (name.Contains("Single"))
        {
            GUN_LIFE = Constants.Instance.GUN_LIFE_SINGLE;
            if (!Lists.AllPlayerShips.Contains(gameObject)) Lists.AllPlayerShips.Add(gameObject);
            if (!Lists.AllPlayerGuns.Contains(gameObject)) Lists.AllPlayerGuns.Add(gameObject);
            //if (!Lists.PlayerGunsSingle.Contains(gameObject)) Lists.PlayerGunsSingle.Add(gameObject);
        }
        if (name.Contains("Dual"))
        {
            GUN_LIFE = Constants.Instance.GUN_LIFE_DUAL;
            if (!Lists.AllPlayerShips.Contains(gameObject)) Lists.AllPlayerShips.Add(gameObject);
            if (!Lists.AllPlayerGuns.Contains(gameObject)) Lists.AllPlayerGuns.Add(gameObject);
            //if (!Lists.PlayerGunsSingle.Contains(gameObject)) Lists.PlayerGunsSingle.Add(gameObject);
        }
        if (name.Contains("Triple"))
        {
            GUN_LIFE = Constants.Instance.GUN_LIFE_TRIPLE;
            if (!Lists.AllPlayerShips.Contains(gameObject)) Lists.AllPlayerShips.Add(gameObject);
            if (!Lists.AllPlayerGuns.Contains(gameObject)) Lists.AllPlayerGuns.Add(gameObject);
            //if (!Lists.PlayerGunsTriple.Contains(gameObject)) Lists.PlayerGunsTriple.Add(gameObject);
        }

        ShipBurstEffectListToActivate = ObjectPuller.current.GetDestrBurstPull();
        paralEffectListToActivate = ObjectPuller.current.GetDestrParalEffectPull();

        anim = shottingGunBarrel.GetComponent<Animator>();
        gunPlayer = shottingGunBarrel.GetComponent<GunPlayer>();

        DESTR_4_HARM = Constants.Instance.DESTR_4_BULLET_HARM;
        CRUIS_4_HARM = Constants.Instance.CRUIS_4_BULLET_HARM;
        DESTR_3_HARM = Constants.Instance.DESTR_3_BULLET_HARM;
        CRUIS_3_HARM = Constants.Instance.CRUIS_3_BULLET_HARM;
        DESTR_2_HARM = Constants.Instance.DESTR_2_BULLET_HARM;
        CRUIS_2_HARM = Constants.Instance.CRUIS_2_BULLET_HARM;
        DESTR_1_HARM = Constants.Instance.DESTR_1_BULLET_HARM;
        CRUIS_1_HARM = Constants.Instance.CRUIS_1_BULLET_HARM;

        PARALIZED_TIME1 = Constants.Instance.PARALIZE_TIME1;
        PARALIZED_TIME2 = Constants.Instance.PARALIZE_TIME2;
    }

    //is invoket from cruis4mega class to reduce gun HP from mega attack
    public void setGunLife(int reduce)
    {
        GUN_LIFE -= reduce;
    }

    //this method is for call from the megaParal2Method of CPU cruiser2, that method call for local routine, it is constructed so cause in case of call of routine from outside in case of destroying the cruiser 2
    //this routine never stops it's effect
    public void callForMegaParalFromOutside()
    {
        StartCoroutine(megaParal());
    }

    // for external invocation with mega paralizing effect of criuser 2 class, that method is invoked from palyer joystik class, from method which is initiates cruiser 2 attack
    public IEnumerator megaParal()
    {
        StopCoroutine(paralizedTime(0));
        if (isParalized) gunParaliser.SetActive(false);
        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
        //gunParaliser = Instantiate(gunParaliserReal, transform.position, Quaternion.identity);
        gunParaliser = ObjectPuller.current.GetUniversalBullet(paralEffectListToActivate);
        gunParaliser.transform.position = transform.position;
        gunParaliser.transform.rotation = Quaternion.identity;
        gunParaliser.SetActive(true);

        isParalized = true;
        shotEffect.SetActive(false);
        gunPlayer.CancelInvoke();
        gunPlayer.enabled = false;
        anim.enabled = false;
        yield return new WaitForSeconds(7);
        isParalized = false;

        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
        //Destroy(gunParaliser);
        gunParaliser.SetActive(false);
        if (!isDestroying)
        {
            shotEffect.SetActive(true);
            gunPlayer.enabled = true;
            gunPlayer.startInvocation();
            anim.enabled = true;
        }
    }

    IEnumerator paralizedTime(float time)
    {
        yield return new WaitForSeconds(time);
        isParalized = false;

        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
        //Destroy(gunParaliser);
        gunParaliser.SetActive(false);

        if (!isDestroying)
        {
            shotEffect.SetActive(true); 
            gunPlayer.enabled = true;
            gunPlayer.startInvocation();
            anim.enabled = true;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDestroying)
        {
            //comparing is necessary to avoid destroying ships own bullet
            if (!other.gameObject.CompareTag("BullCruisPlay1") && !other.gameObject.CompareTag("BullDstrPlay1") && !other.gameObject.CompareTag("BullParalPlay1")
            && !other.gameObject.CompareTag("BullCruisPlay2") && !other.gameObject.CompareTag("BullDstrPlay2") && !other.gameObject.CompareTag("BullParalPlay2")
            && !other.gameObject.CompareTag("BullCruisPlay3") && !other.gameObject.CompareTag("BullDstrPlay3") && !other.gameObject.CompareTag("BullCruisPlay4")
            && !other.gameObject.CompareTag("BullDstrPlay4") && !other.gameObject.CompareTag("PowerShield") && !other.gameObject.CompareTag("GunBullPlay")
            && !other.gameObject.CompareTag("GunBullCPU"))
            {

                //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((
                //GameObject burst = Instantiate(other.GetComponent<CPUBullet>().getBurst(), other.transform.position, Quaternion.identity);
                other.GetComponent<CPUBullet>().getBurst();

                //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((
                //Destroy(other.gameObject);
                other.gameObject.SetActive(false);

                //DESTROYERS BULLETS HARM
                if (other.gameObject.CompareTag("BullDstr4"))
                    GUN_LIFE -= DESTR_4_HARM; //reduse the life for one HP
                else if (other.gameObject.CompareTag("BullDstr3"))
                    GUN_LIFE -= DESTR_3_HARM; //reduse the life for two HP
                else if (other.gameObject.CompareTag("BullDstr2"))
                    GUN_LIFE -= DESTR_2_HARM; //reduse the life for two HP
                else if (other.gameObject.CompareTag("BullDstr1"))
                    GUN_LIFE -= DESTR_1_HARM; //reduse the life for two HP

                //CRUISERS BULLETS HARM
                else if (other.gameObject.CompareTag("BullCruis4"))
                    GUN_LIFE -= CRUIS_4_HARM; //reduse the life for two HP
                else if (other.gameObject.CompareTag("BullCruis3"))
                    GUN_LIFE -= CRUIS_3_HARM; //reduse the life for two HP
                else if (other.gameObject.CompareTag("BullCruis2"))
                    GUN_LIFE -= CRUIS_2_HARM; //reduse the life for two HP
                else if (other.gameObject.CompareTag("BullCruis1"))
                    GUN_LIFE -= CRUIS_1_HARM; //reduse the life for two HP

                //PARALIZER BULLETS PROCESSING, fro two types of paralizer bullets
                else if (other.gameObject.CompareTag("BullParal2"))
                {
                    if (!isParalized)
                    {
                        //instantiates the paralizer effect around current ship, stops maneuvring features and sets isParalized bool on to turn off all action by ship
                        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                        //gunParaliser = Instantiate(gunParaliserReal, transform.position, Quaternion.identity);
                        gunParaliser = ObjectPuller.current.GetUniversalBullet(paralEffectListToActivate);
                        gunParaliser.transform.position = transform.position;
                        gunParaliser.transform.rotation = Quaternion.identity;
                        gunParaliser.SetActive(true);

                        isParalized = true;
                        shotEffect.SetActive(false);
                        gunPlayer.CancelInvoke();
                        gunPlayer.enabled = false;
                        anim.enabled = false;
                        StartCoroutine(paralizedTime(PARALIZED_TIME2));
                    }
                }
                else if (other.gameObject.CompareTag("BullParal1"))
                {
                    if (!isParalized)
                    {
                        //instantiates the paralizer effect around current ship, stops maneuvring features and sets isParalized bool on to turn off all action by ship
                        //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
                        //gunParaliser = Instantiate(gunParaliserReal, transform.position, Quaternion.identity);
                        gunParaliser = ObjectPuller.current.GetUniversalBullet(paralEffectListToActivate);
                        gunParaliser.transform.position = transform.position;
                        gunParaliser.transform.rotation = Quaternion.identity;
                        gunParaliser.SetActive(true);

                        isParalized = true;
                        shotEffect.SetActive(false);
                        gunPlayer.CancelInvoke();
                        gunPlayer.enabled = false;
                        anim.enabled = false;
                        StartCoroutine(paralizedTime(PARALIZED_TIME1));
                    }
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (GUN_LIFE <= 4 && !isDestroying && !burningEffect.activeInHierarchy) burningEffect.SetActive(true);

        //last condition checks if player ships are set on battlefield, otherwise there is a bug of detroying player guns right after they are set in battlefields
        //cause Lists.playerFleetIsSet puts false after iterator finishes putting player ships on battlefield on Start method f Battle launche scripts
        //second or condition is to check if there is still at leat one player ship on battlefield, means that player guns will not fight by themselves without ships
        if ((GUN_LIFE <= 0  || Lists.AllPlayerShipsWithoutGuns.Count < 1) && !isDestroying && !Lists.isAfterBattleWin /*!Lists.playerFleetIsSet && !Lists.isAfterBattleWin*/)
        {
            isDestroying = true;

            GUN_LIFE = Constants.Instance.GUN_LIFE_SINGLE;

            gunPlayer.CancelInvoke();
            gunPlayer.enabled = false;
            anim.enabled = false;

            Lists.AllPlayerShips.Remove(gameObject);
            Lists.AllPlayerGuns.Remove(gameObject);
            //Lists.PlayerGunsSingle.Remove(gameObject);
            //Lists.PlayerGunsDual.Remove(gameObject);
            //Lists.PlayerGunsTriple.Remove(gameObject);

            //triggers that CPU wins the battle depending on static lists count of ships on battle scene
            if (Lists.AllCPUShips.Count > 1 && Lists.AllPlayerShips.Count < 1) BattleLaunch.CPUWin = true;

            //counts of loses to process when get back to journey scene on panel of win lose (on JourneyController class)
            if (name.Contains("Single")) Lists.G1Lose++;
            else if (name.Contains("Dual")) Lists.G2Lose++;
            else if (name.Contains("Triple")) Lists.G3Lose++;


            if (isParalized) gunParaliser.SetActive(false);
            //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
            //if (gunParaliser) Destroy(gunParaliser);

            burningEffect.SetActive(false);
            shotEffect.SetActive(false);
            
            GameObject exploision = ObjectPuller.current.GetUniversalBullet(ShipBurstEffectListToActivate);
            exploision.transform.position = transform.position;
            exploision.transform.rotation = Quaternion.identity;
            exploision.SetActive(true);
            //TO SET BACK IF PULLING WILL NOT WORK WELL(((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((((
            //GameObject exploision = Instantiate(gunExploisionReal, transform.position, Quaternion.identity);

            GetComponent<Collider>().enabled = false; // turns off the collider of gun to prevent errors with on trigger enter functions of curren script
            Destroy(gameObject/*, 2*/); 

            //foreach (GameObject go in gunParts)
            //{
            //    go.GetComponent<TrailRenderer>().enabled = true;
            //    go.GetComponent<Rigidbody>().isKinematic = false;
            //    go.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-2f, 2f), Random.Range(-2, 2), Random.Range(-2, 2)) * 2, ForceMode.Impulse);
            //    go.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-1f, 1f), Random.Range(-1, 1), Random.Range(-1, 1)), ForceMode.Impulse);
            //}
        }

    }
}
