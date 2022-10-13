
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private float i;
    private float j;
    private float rotation;

    // Start is called before the first frame update
    void Start()
    {
        i = Random.Range(6,8);
        j= Random.Range(-6, -8);
        rotation = Random.Range(0, 2) > 0 ? i : j;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotation * Time.deltaTime, 0,Space.World);
    }
}
