using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationYZ : MonoBehaviour
{
    private float i;
    private float j;
    public int mult;
    private float rotation;
    private float rotation2;

    // Start is called before the first frame update
    void Start()
    {
        i = Random.Range(6, 8) * mult;
        //j = Random.Range(-6, -8);
        rotation = i; //.Range(0, 2) > 0 ? i : j;
        //rotation2 = rotation > 0 ? j : i;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation * Time.deltaTime, 0, 0, Space.World);
    }
}
