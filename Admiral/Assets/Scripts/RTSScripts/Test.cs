using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject shipToAttak;
    private Transform gunSphereParentTransform;

    // Start is called before the first frame update
    void Start()
    {
        gunSphereParentTransform = transform;
    }

    private void FixedUpdate()
    {
        if (gunSphereParentTransform.rotation != Quaternion.LookRotation(shipToAttak.transform.position - gunSphereParentTransform.position, Vector3.up))
            gunSphereParentTransform.rotation = Quaternion.Lerp(gunSphereParentTransform.rotation, Quaternion.LookRotation(shipToAttak.transform.position - gunSphereParentTransform.position, Vector3.up), 2f);
        
    }

}
