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
    private Admob _ad;

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
        _ad = Admob.Instance();
        _ad.bannerEventHandler += OnBannerEvent;
        _ad.interstitialEventHandler += OnInterstitialEvent;
        _ad.rewardedVideoEventHandler += OnRewardedVideoEvent;
        _ad.initAdmob("ca-app-pub-3940256099942544/2934735716", "ca-app-pub-3940256099942544/4411468910");//all id are admob test id,change those to your
        Admob.Instance().loadInterstitial();
        Admob.Instance().loadRewardedVideo("ca-app-pub-3940256099942544/5224354917");
        //ad.setTesting(true);//show test ad
        _ad.setGender(AdmobGender.MALE);
        //string[] keywords = { "game", "crash", "male game" };
        //  ad.setKeywords(keywords);//set keywords for ad
        Debug.Log("admob inited -------------");
    }

    public void ShowBannerAd()
    {
        Debug.Log("Showing banner ad");
        Admob.Instance().showBannerRelative(AdSize.SmartBanner, AdPosition.BOTTOM_CENTER, 0);
    }

    public void RemoveBannerAd()
    {
        Admob.Instance().removeBanner();
    }

    public void ShowInterstitialAd()
    {
        Debug.Log("Showing video or full screen ad");
        bool MyAdIsReady = (Admob.Instance().isInterstitialReady());

        if (MyAdIsReady)
        {
            Admob.Instance().showInterstitial();
        }

        else
        {
            Admob.Instance().loadInterstitial();
        }
    }

    public void ShowVideoAd()
    {
        if (_ad.isRewardedVideoReady())
        {
            _ad.showRewardedVideo();
        }
        else
        {
            _ad.loadRewardedVideo("ca-app-pub-3940256099942544/1712485313");
        }
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

    void OnRewardedVideoEvent(string eventName, string msg)
    {
        Debug.Log("handler onRewardedVideoEvent---" + eventName + "  rewarded: " + msg);
    }
}
