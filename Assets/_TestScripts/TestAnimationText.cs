using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimationText : MonoBehaviour
{
    public Animator TextAnimator;

    private readonly string ShowPercentage = "ShowNewPercentage";
    private readonly string HidePercentage = "HidePercentageText";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            TextAnimator.SetBool(ShowPercentage, true);
            TextAnimator.SetBool(HidePercentage, false);
        }

        else if (Input.GetKeyDown(KeyCode.E))
        {
            TextAnimator.SetBool(HidePercentage, true);
            TextAnimator.SetBool(ShowPercentage, false);
        }
    }
}
