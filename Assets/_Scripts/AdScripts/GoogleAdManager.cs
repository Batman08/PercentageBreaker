using System;
using System.Collections;
using System.Collections.Generic;
using admob;
using UnityEngine;

public class GoogleAdManager : MonoBehaviour
{
    public static GoogleAdManager Instance { set; get; }

    private string _bannerId = "ca-app-pub-1211888741379414/8807931452";
    private string _interstitialId = "ca-app-pub-1211888741379414/8362575878";

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitAdmob();

        #region
        //Debug.Log("Can't show ads in editor!");
        //Admob.Instance().initAdmob(bannerID: _bannerId, fullID: _interstitialId);
        //Admob.Instance().setTesting(true);
        //Admob.Instance().loadInterstitial();
        #endregion
    }

    void InitAdmob()
    {
        Admob.Instance().bannerEventHandler += OnBannerEvent;
        Admob.Instance().interstitialEventHandler += OnInterstitialEvent;
        Admob.Instance().initAdmob("ca-app-pub-3940256099942544/2934735716", "ca-app-pub-3940256099942544/4411468910");//all id are admob test id,change those to mine
        Admob.Instance().loadInterstitial();
    }

    public void ShowBannerAd()
    {
        Debug.Log("Showing banner ad");
        Admob.Instance().showBannerRelative(AdSize.SmartBanner, AdPosition.BOTTOM_CENTER, 0);
    }

    public void ShowVideoAd()
    {
        Debug.Log("Showing video or full screen ad");
        bool MyAdIsReady = (Admob.Instance().isInterstitialReady());

        if (MyAdIsReady)
        {
            Admob.Instance().showInterstitial();
        }

        //else
        //{
        //    Admob.Instance().loadInterstitial();
        //}
    }

    void OnInterstitialEvent(string eventName, string msg)
    {
        Debug.Log("handler onAdmobEvent---" + eventName + "   " + msg);
        if (eventName == AdmobEvent.onAdLoaded)
        {
            Admob.Instance().showInterstitial();
        }
    }
    void OnBannerEvent(string eventName, string msg)
    {
        Debug.Log("handler onAdmobBannerEvent---" + eventName + "   " + msg);
    }
}
