
using UnityEngine;

public class Cruiser4Mega : MonoBehaviour
{
    private int HPReduce;
    private int CPUNumber;
    private float lifeTime;
    private void Start()
    {
        if (name.Contains("MegaShot"))
        {
            HPReduce = 1;
        }
        
    }
    private void OnEnable()
    {
        if (name.Contains("MegaLaser"))
        {
            HPReduce = 2;
        }
        if (HPReduce > 1) lifeTime = 4;
         CPUNumber = GetComponentInParent<PlayerBattleShip>().CPUNumber;
        //setting to mega shot particles with which objects they will interact with (there are the lyers with specific CPU numbers, so particles will not interact with CPUNumbers that different from this)
        var collision = GetComponent<ParticleSystem>().collision;
        int index = 0;
        for (int i = 0; i < 6; i++)
        {
            if (i != CPUNumber)
            {
                //the lyer number correctly assigned only this way 1 << 0 (so for example here is assigned the lyer with 0 index)
                if (index == 0) collision.collidesWith = 1 << (i + 10);
                else collision.collidesWith += 1 << (i + 10);
                index++;
            }
        }
    }
    // Start is called before the first frame update
    private void OnParticleCollision(GameObject other)
    {
        //if (other.CompareTag("Gun")) other.GetComponent<CPUBattleShip>().reduceTheHPOfShip(HPReduce);
        if (other.CompareTag("PowerShield")) { } //has no harm effet on ships with shields
        else
        {
            //otherBattleShip = other.GetComponent<BattleShipClass>();
            //if (otherBattleShip.CPUNumber != CPUNumber) otherBattleShip.reduceTheHPOfShip(HPReduce);
            other.GetComponent<CPUBattleShip>().reduceTheHPOfShip(HPReduce);
        }
    }

    private void FixedUpdate()
    {
        if (HPReduce > 1) {
            transform.Rotate(new Vector3(0, 2.2f, 0));
        }
    }
    private void Update()
    {
        if (HPReduce > 1)
        {
            if (lifeTime > 0)
            {
                lifeTime -= Time.deltaTime;
                if (lifeTime <= 0)
                {
                    transform.rotation = Quaternion.Euler(0,-90,0);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
