using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;

public class AsteroidMngrDef : MonoBehaviour
{
    private float tumble;
    private Vector3 moveDir;
    private Vector3 rotationDir;
    //private Rigidbody rb;
    private float randomeVelosityIncreaser;

    //this one is to pull the bullet burst from the puller whent the energon bullet hits the ship
    private List<GameObject> AsteroidList;
    private GameObject AsteroidReal;

    private void OnEnable()
    {
        //rb = GetComponent<Rigidbody>();
        //rb = GetComponent<Rigidbody>();
        //tumble = Random.Range(1f, 2.5f);
        tumble = Random.Range(40f, 55f);

        rotationDir = Random.insideUnitSphere * tumble;
        //rb.angularVelocity = Vector3.zero;
        //rb.velocity = Vector3.zero;

        if (transform.position.x >= 0)
        {
            moveDir = new Vector3(-2000, Random.Range(-350f, 450f), Random.Range(780f, 300f)) - transform.position;
        }
        else moveDir = new Vector3(2000, Random.Range(-350f, 450f), Random.Range(780f, 300f)) - transform.position;

        //rb.angularVelocity = Random.insideUnitSphere * tumble;
        //randomeVelosityIncreaser = Random.Range(0.2f, 0.42f);
        randomeVelosityIncreaser = Random.Range(11f,17f);
    }

    //private void FixedUpdate()
    //{
    //    ///rb.velocity = moveDir.normalized * randomeVelosityIncreaser;

    //    transform.Translate(moveDir.normalized * randomeVelosityIncreaser, Space.World);
    //    transform.Rotate(rotationDir, Space.World);

    //}



    private void Update()
    {

        transform.Translate(moveDir.normalized * randomeVelosityIncreaser * Time.deltaTime, Space.World);
        transform.Rotate(rotationDir * Time.deltaTime, Space.World);

        //this function puts back the energon ship if it got out of bounds 
        if (transform.position.x > 2000 || transform.position.x < -2000)
        {
            int index = Random.Range(1, 5);

            if (index == 1) AsteroidList = ObjectPullerDefence.current.GetAsteroids1Pull();
            else if (index == 2) AsteroidList = ObjectPullerDefence.current.GetAsteroids2Pull();
            else if (index == 3) AsteroidList = ObjectPullerDefence.current.GetAsteroids3Pull();
            else AsteroidList = ObjectPullerDefence.current.GetAsteroids4Pull();
            AsteroidReal = ObjectPullerDefence.current.GetUniversalBullet(AsteroidList);
            AsteroidReal.transform.position = transform.position;
            AsteroidReal.transform.rotation = Random.rotation;
            AsteroidReal.SetActive(true);

            gameObject.SetActive(false);
        }
    }
}
