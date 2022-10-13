
using UnityEngine;

public class DestroyParticles : MonoBehaviour
{
    private void OnEnable()
    {
        if (GetComponent<AudioSource>()) GetComponent<AudioSource>().Play(0);
        Invoke("setFalseGameObj", 2);
    }

    private void setFalseGameObj()
    {
        gameObject.SetActive(false);
    }
}
