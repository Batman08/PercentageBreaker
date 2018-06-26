using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    public bool IsOn = false;

    public Color onColorBg;
    public Color offColorBg;

    public Image toggleBgImage;
    public RectTransform toggle;

    public GameObject handle;
    private RectTransform handleTransform;

    private float handleSize;
    private float onPosX;
    private float offPosX;

    public float handleOffset;

    public GameObject onIcon;
    public GameObject offIcon;


    public float speed;
    static float t = 0.0f;

    private bool switching = false;
    private string SoundIsOnKey = "On";
    private string SoundKey = "Value";

    private string ShowTutorialKey = "On2";
    private string TutorialKey = "Value2";

    public bool IsSound;
    public bool IsTutorial;

    public bool Showtutorial;

    void Awake()
    {
        handleTransform = handle.GetComponent<RectTransform>();
        RectTransform handleRect = handle.GetComponent<RectTransform>();
        handleSize = handleRect.sizeDelta.x;
        float toggleSizeX = toggle.sizeDelta.x;
        onPosX = (toggleSizeX / 2) - (handleSize / 2) - handleOffset;
        offPosX = onPosX * -1;

        if (IsSound)
        {
            bool FirstTimeOn = (PlayerPrefs.GetInt(SoundIsOnKey) == 0);
            if (FirstTimeOn)
            {
                IsOn = true;
                //Toggle(isOn);
                PlayerPrefs.SetInt(SoundIsOnKey, 1);
                PlayerPrefs.SetInt(SoundKey, 1);
            }

            bool SoundShouldBeON = (PlayerPrefs.GetInt(SoundKey) == 1);
            if (SoundShouldBeON)
                IsOn = true;
            else
                IsOn = false;
        }

        else if (IsTutorial)
        {
            bool FirstTimeOn = (PlayerPrefs.GetInt(ShowTutorialKey) == 0);
            if (FirstTimeOn)
            {
                Showtutorial = true;
                //Toggle(isOn);
                PlayerPrefs.SetInt(ShowTutorialKey, 1);
                PlayerPrefs.SetInt(TutorialKey, 1);
            }

            bool SoundShouldBeON = (PlayerPrefs.GetInt(TutorialKey) == 1);
            if (SoundShouldBeON)
                Showtutorial = true;
            else
                Showtutorial = false;
        }
    }


    void ShouldShowTutorial()
    {
        if (IsTutorial)
        {
            if (Showtutorial)
            {
                toggleBgImage.color = onColorBg;
                handleTransform.localPosition = new Vector3(onPosX, 0f, 0f);
                onIcon.gameObject.SetActive(true);
                offIcon.gameObject.SetActive(false);
                offIcon.gameObject.GetComponent<CanvasGroup>().alpha = 1;
                PlayerPrefs.SetInt(TutorialKey, 1);
            }
            else
            {
                toggleBgImage.color = offColorBg;
                handleTransform.localPosition = new Vector3(offPosX, 0f, 0f);
                onIcon.gameObject.SetActive(false);
                offIcon.gameObject.SetActive(true);
                onIcon.gameObject.GetComponent<CanvasGroup>().alpha = 1;
                PlayerPrefs.SetInt(TutorialKey, 0);
            }
        }
    }

    void Sound()
    {
        if (IsSound)
        {
            if (IsOn)
            {
                toggleBgImage.color = onColorBg;
                handleTransform.localPosition = new Vector3(onPosX, 0f, 0f);
                onIcon.gameObject.SetActive(true);
                offIcon.gameObject.SetActive(false);
                offIcon.gameObject.GetComponent<CanvasGroup>().alpha = 1;
                PlayerPrefs.SetInt(SoundKey, 1);
            }
            else
            {
                toggleBgImage.color = offColorBg;
                handleTransform.localPosition = new Vector3(offPosX, 0f, 0f);
                onIcon.gameObject.SetActive(false);
                offIcon.gameObject.SetActive(true);
                onIcon.gameObject.GetComponent<CanvasGroup>().alpha = 1;
                PlayerPrefs.SetInt(SoundKey, 0);
            }
        }
    }

    #region
    //public void Sounfd()
    //{
    //    //Check to see if the sound should be enabled or disabled
    //    if (isOn)
    //    {
    //        isOn = false;
    //        PlayerPrefs.SetInt(SoundKey, 0);
    //        //GoogleAdManager.Instance.ShowInterstitialAd();
    //    }
    //    else
    //    {
    //        isOn = true;
    //        PlayerPrefs.SetInt(SoundKey, 1);
    //    }
    //}
    #endregion

    void Update()
    {
        Sound();
        ShouldShowTutorial();

        if (switching)
        {
            Toggle(IsOn);
        }
    }

    public void DoYourStaff()
    {
        Debug.Log(IsOn);
    }

    public void Switching()
    {
        switching = true;
    }



    public void Toggle(bool toggleStatus)
    {
        if (!onIcon.activeSelf || !offIcon.activeSelf)
        {
            onIcon.SetActive(true);
            offIcon.SetActive(true);
        }

        if (toggleStatus)
        {
            toggleBgImage.color = SmoothColor(onColorBg, offColorBg);
            Transparency(onIcon, 1f, 0f);
            Transparency(offIcon, 0f, 1f);
            handleTransform.localPosition = SmoothMove(handle, onPosX, offPosX);
        }
        else
        {
            toggleBgImage.color = SmoothColor(offColorBg, onColorBg);
            Transparency(onIcon, 0f, 1f);
            Transparency(offIcon, 1f, 0f);
            handleTransform.localPosition = SmoothMove(handle, offPosX, onPosX);
        }

    }


    Vector3 SmoothMove(GameObject toggleHandle, float startPosX, float endPosX)
    {

        Vector3 position = new Vector3(Mathf.Lerp(startPosX, endPosX, t += speed * Time.deltaTime), 0f, 0f);
        StopSwitching();
        return position;
    }

    Color SmoothColor(Color startCol, Color endCol)
    {
        Color resultCol;
        resultCol = Color.Lerp(startCol, endCol, t += speed * Time.deltaTime);
        return resultCol;
    }

    CanvasGroup Transparency(GameObject alphaObj, float startAlpha, float endAlpha)
    {
        CanvasGroup alphaVal;
        alphaVal = alphaObj.gameObject.GetComponent<CanvasGroup>();
        alphaVal.alpha = Mathf.Lerp(startAlpha, endAlpha, t += speed * Time.deltaTime);
        return alphaVal;
    }

    void StopSwitching()
    {
        if (t > 1.0f)
        {
            switching = false;

            t = 0.0f;
            switch (IsOn)
            {
                case true:
                    IsOn = false;
                    DoYourStaff();
                    break;

                case false:
                    IsOn = true;
                    DoYourStaff();
                    break;
            }

        }
    }

}
