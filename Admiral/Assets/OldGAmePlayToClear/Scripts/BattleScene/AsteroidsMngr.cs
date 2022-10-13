using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsMngr : MonoBehaviour
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
        tumble = Random.Range(40f, 55f);
        rotationDir = Random.insideUnitSphere * tumble;
        //rb.angularVelocity = Vector3.zero;
        //rb.velocity = Vector3.zero;

        if (transform.position.x >= 0)
        {
            moveDir = new Vector3(-43, -1.3f, Random.Range(2f, 8f)) - transform.position;
        }
        else moveDir = new Vector3(43, -1.3f, Random.Range(2f, 8f))- transform.position;

        //rb.angularVelocity = Random.insideUnitSphere * tumble;
        randomeVelosityIncreaser = Random.Range(0.7f, 3f);
        //randomeVelosityIncreaser = Random.Range(1.2f, 2.5f);
    }

    //private void FixedUpdate()
    //{
    //    //rb.velocity = moveDir.normalized * randomeVelosityIncreaser;

        
    //}

   

    private void Update()
    {
        transform.Translate(moveDir.normalized * randomeVelosityIncreaser * Time.deltaTime, Space.World);
        transform.Rotate(rotationDir*Time.deltaTime, Space.World);

        //this function puts back the energon ship if it got out of bounds 
        if (transform.position.x > 43 || transform.position.x < -43)
        {
            int index = Random.Range(1,5);

            if (index == 1) AsteroidList = ObjectPuller.current.GetAsteroids1Pull();
            else if (index == 2) AsteroidList = ObjectPuller.current.GetAsteroids2Pull();
            else if (index == 3) AsteroidList = ObjectPuller.current.GetAsteroids3Pull();
            else AsteroidList = ObjectPuller.current.GetAsteroids4Pull();
            AsteroidReal = ObjectPuller.current.GetUniversalBullet(AsteroidList);
            AsteroidReal.transform.position = transform.position;
            AsteroidReal.transform.rotation = Random.rotation;
            AsteroidReal.SetActive(true);

            gameObject.SetActive(false);
        }
    }
}
