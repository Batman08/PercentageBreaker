using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PercentageTextAnimation : MonoBehaviour
{
    public Animator TextAnimator;

    public GameObject Text1, Text2;

    private readonly string ShowTextAnimationCondition = "ShowNewPercentage";
    private readonly string HideTextAnimationCondition = "HidePercentageText";

    private StickSpawner _stickSpawner;

    void Start()
    {
        _stickSpawner = FindObjectOfType<StickSpawner>();
    }

    void Update()
    {

        ChangeTextAnimation();
        //Text1.SetActive(value: true);
        //Text2.SetActive(value: true);
    }

    void ChangeTextAnimation()
    {
        bool hasWaveEnded = (_stickSpawner.HasWaveEnded);

        if (hasWaveEnded)
        {
            HideTextAnimation();
        }

        if (!hasWaveEnded)
        {
            ShowTextAnimation();
        }
    }

    void ShowTextAnimation()
    {
        TextAnimator.SetBool(ShowTextAnimationCondition, true);
        TextAnimator.SetBool(HideTextAnimationCondition, false);
    }

    void HideTextAnimation()
    {
        TextAnimator.SetBool(HideTextAnimationCondition, true);
        TextAnimator.SetBool(ShowTextAnimationCondition, false);
    }
}
