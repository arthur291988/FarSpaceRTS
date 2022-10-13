
using System.Collections.Generic;
using UnityEngine;

public class DummieManager : MonoBehaviour
{
    //to paint the elements of ship to decent color
    [SerializeField]
    private List<GameObject> IDColorElements;
    private string playerTag = "Gun";

    private void OnEnable()
    {
        if (CompareTag(playerTag))
        {
            for (int i = 0; i < IDColorElements.Count; i++)
            {
                IDColorElements[i].GetComponent<MeshRenderer>().material.SetColor("_Color", Lists.colorOfPlayer);
            }
        }
        else
        {
            for (int i = 0; i < IDColorElements.Count; i++)
            {
                IDColorElements[i].GetComponent<MeshRenderer>().material.SetColor("_Color", Lists.colorOfOpposite);
            }
        }
    }
}
