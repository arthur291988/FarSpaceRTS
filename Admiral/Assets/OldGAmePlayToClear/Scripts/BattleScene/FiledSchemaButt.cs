
using UnityEngine;
using UnityEngine.UI;

public class FiledSchemaButt : MonoBehaviour
{
    //This class is assigned to all battleFieldScehme buttons to add them to a dictionary so game can detect them while player push them
    // Start is called before the first frame update
    void Start()
    {
        Lists.notTakenButtonsDic.Add(name, gameObject.GetComponent<Button>());
        Lists.notTakenButtons.Add(gameObject.GetComponent<Button>());
    }
}
