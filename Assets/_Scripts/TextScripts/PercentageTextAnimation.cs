using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PercentageTextAnimation : MonoBehaviour
{
    public GameObject PercentageTxt1;
    public GameObject PercentageTxt2;

    public Animator TextAnimator;


    private readonly string ShowTextAnimationCondition = "ShowNewPercentage";
    private readonly string HideTextAnimationCondition = "HidePercentageText";

    private StickSpawner _stickSpawner;

    void Awake()
    {
        _stickSpawner = FindObjectOfType<StickSpawner>();

        StartCoroutine(ShowText());
    }

    void Update()
    {
        ChangeTextAnimation();
    }

    IEnumerator ShowText()
    {
        yield return new WaitForSeconds(0.85f);
        PercentageTxt1.SetActive(value: true);
        PercentageTxt2.SetActive(value: true);
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
