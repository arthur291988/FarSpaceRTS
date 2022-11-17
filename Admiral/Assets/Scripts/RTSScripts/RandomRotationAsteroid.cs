using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotationAsteroid : MonoBehaviour
{
    Transform transformOfObject;
    private Vector3 rotationDir;
    private float tumble;

    // Start is called before the first frame update
    void Start()
    {
        tumble = Random.Range(5f, 10f);
        rotationDir = Random.insideUnitSphere* tumble;
        transformOfObject = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transformOfObject.Rotate(rotationDir * Time.deltaTime, Space.World);
    }
}
