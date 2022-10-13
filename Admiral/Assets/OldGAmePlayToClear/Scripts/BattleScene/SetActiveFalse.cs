
using UnityEngine;

public class SetActiveFalse : MonoBehaviour
{
    //this code is used with setting active false of game object tha has animation, so it is used with animation event function
    public void disactivateCurrent() {
        gameObject.SetActive(false);
    }
}
