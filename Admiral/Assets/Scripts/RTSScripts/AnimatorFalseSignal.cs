using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorFalseSignal : MonoBehaviour
{
    private Animator shotAnimator;

    void Start()
    {
        shotAnimator = GetComponent<Animator>();
    }

    public void shotAnimationEventSetFalse() => shotAnimator.SetBool("shot", false);
}
