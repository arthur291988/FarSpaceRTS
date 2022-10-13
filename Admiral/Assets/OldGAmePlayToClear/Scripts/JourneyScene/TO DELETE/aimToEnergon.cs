using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimToEnergon : MonoBehaviour
{
    private Transform energonTransform;
    [HideInInspector]
    public GameObject energon;
    private Transform transformThis;
    private Camera camera;

    //private void OnEnable()
    //{
    //    //if (energon == null)
    //    //{
    //    //    energonTransform = energon.transform;
    //    //    transformThis = transform;
    //    //    camera = Camera.main;
    //    //    transformThis.position = new Vector3 (camera.WorldToScreenPoint(energonTransform.position).x, camera.WorldToScreenPoint(energonTransform.position).y,0);
    //    //}
    //    //else if (energon.activeInHierarchy)
    //    //{
    //    //    transformThis.position = new Vector3(camera.WorldToScreenPoint(energonTransform.position).x, camera.WorldToScreenPoint(energonTransform.position).y, 0);
    //    //}
    //}

    //public void restTheAiming()
    //{
    //    energonTransform = energon.transform;
    //    transformThis = transform;
    //    camera = Camera.main;
    //    transformThis.position = new Vector3(camera.WorldToScreenPoint(energonTransform.position).x, camera.WorldToScreenPoint(energonTransform.position).y, 0);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (energon.activeInHierarchy)
    //    {
    //        transformThis.position = new Vector3(camera.WorldToScreenPoint(energonTransform.position).x, camera.WorldToScreenPoint(energonTransform.position).y, 0);
    //    }
    //}
}
