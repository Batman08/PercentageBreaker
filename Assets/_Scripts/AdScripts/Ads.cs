using UnityEngine;
using System.Collections;
using admob;
public class Ads : MonoBehaviour
{
    public static Ads Instance { get; set; }

    private Admob _ad;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        Debug.Log("start unity demo-------------");
        InitAdmob();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log(KeyCode.Escape + "-----------------");
        }
    }

    void InitAdmob()
    {

        _ad = Admob.Instance();
        _ad.interstitialEventHandler += OnInterstitialEvent;
        _ad.rewardedVideoEventHandler += OnRewardedVideoEvent;
        _ad.initAdmob("ca-app-pub-3940256099942544/2934735716", "ca-app-pub-3940256099942544/4411468910");//all id are admob test id,change those to your
        _ad.loadRewardedVideo("ca-app-pub-3940256099942544/1712485313");
        //ad.setTesting(true);//show test ad
        _ad.setGender(AdmobGender.MALE);
        //string[] keywords = { "game", "crash", "male game" };
        //  ad.setKeywords(keywords);//set keywords for ad
        Debug.Log("admob inited -------------");

    }

    public void ShowInterstitialBasedAd()
    {
        if (_ad.isInterstitialReady())
        {
            _ad.showInterstitial();
        }
        else
        {
            _ad.loadInterstitial();
        }
    }

    public void ShowRewardBasedAd()
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

    void OnRewardedVideoEvent(string eventName, string msg)
    {
        Debug.Log("handler onRewardedVideoEvent---" + eventName + "  rewarded: " + msg);
    }
}
