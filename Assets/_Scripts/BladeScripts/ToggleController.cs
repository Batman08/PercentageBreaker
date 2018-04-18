using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    public bool isOn;

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


    void Awake()
    {
        handleTransform = handle.GetComponent<RectTransform>();
        RectTransform handleRect = handle.GetComponent<RectTransform>();
        handleSize = handleRect.sizeDelta.x;
        float toggleSizeX = toggle.sizeDelta.x;
        onPosX = (toggleSizeX / 2) - (handleSize / 2) - handleOffset;
        offPosX = onPosX * -1;
        bool FirstTimeOn = (PlayerPrefs.GetFloat(SoundIsOnKey) == 0);
        if (FirstTimeOn)
        {
            isOn = true;
            PlayerPrefs.SetFloat(SoundIsOnKey, 1);
        }



        if (PlayerPrefs.GetFloat(SoundKey) == 1)
        {
            isOn = true;
        }
        else
        {
            isOn = false;
        }
    }


    void Start()
    {
        //Sound();
    }

    void Sound()
    {
        if (isOn)
        {
            toggleBgImage.color = onColorBg;
            handleTransform.localPosition = new Vector3(onPosX, 0f, 0f);
            onIcon.gameObject.SetActive(true);
            offIcon.gameObject.SetActive(false);
        }
        else
        {
            toggleBgImage.color = offColorBg;
            handleTransform.localPosition = new Vector3(offPosX, 0f, 0f);
            onIcon.gameObject.SetActive(false);
            offIcon.gameObject.SetActive(true);
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

        if (switching)
        {
            Toggle(isOn);
        }
    }

    public void DoYourStaff()
    {
        Debug.Log(isOn);
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
            PlayerPrefs.SetInt(SoundKey, 1);
        }
        else
        {
            toggleBgImage.color = SmoothColor(offColorBg, onColorBg);
            Transparency(onIcon, 0f, 1f);
            Transparency(offIcon, 1f, 0f);
            handleTransform.localPosition = SmoothMove(handle, offPosX, onPosX);
            PlayerPrefs.SetInt(SoundKey, 0);
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
            switch (isOn)
            {
                case true:
                    isOn = false;
                    DoYourStaff();
                    break;

                case false:
                    isOn = true;
                    DoYourStaff();
                    break;
            }

        }
    }

}
