using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToCamera : MonoBehaviour
{
    private Transform cameraLook;

    void OnEnable()
    {
        cameraLook = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + cameraLook.rotation * Vector3.back, cameraLook.rotation * Vector3.up);
    }
}
