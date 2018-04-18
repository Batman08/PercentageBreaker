using System.Collections;
using System.Collections.Generic;
using System;
//using GoogleMobileAds;
//using GoogleMobileAds.Api;
using UnityEngine;

public class AdManager : MonoBehaviour
{
//    public static AdManager Instance { get; set; }

//    private RewardBasedVideoAd _rewardBasedVideoAd;

//    void Awake()
//    {
//        DontDestroyOnLoad(gameObject);
//        Instance = this;
//        _rewardBasedVideoAd = RewardBasedVideoAd.Instance;

//        // Called when an ad request has successfully loaded.
//        _rewardBasedVideoAd.OnAdLoaded += HandleRewardBasedVideoLoaded;
//        // Called when an ad request failed to load.
//        _rewardBasedVideoAd.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
//        // Called when an ad is shown.
//        _rewardBasedVideoAd.OnAdOpening += HandleRewardBasedVideoOpened;
//        // Called when the ad starts to play.
//        _rewardBasedVideoAd.OnAdStarted += HandleRewardBasedVideoStarted;
//        // Called when the user should be rewarded for watching a video.
//        _rewardBasedVideoAd.OnAdRewarded += HandleRewardBasedVideoRewarded;
//        // Called when the ad is closed.
//        _rewardBasedVideoAd.OnAdClosed += HandleRewardBasedVideoClosed;
//        // Called when the ad click caused the user to leave the application.
//        _rewardBasedVideoAd.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
//    }

//    private void LoadRewardBasedAd()
//    {
//#if UNITY_ANDROID
//        string adUnitId = "ca-app-pub-3940256099942544~3347511713";
//#elif UNITY_IPHONE
//        string adUnitId = "ca-app-pub-3940256099942544/7325402514";
//#elif UNITY_IPHONE
//        string adUnitId = "ca-app-pub-3940256099942544~1458002511";
//#else
//        string adUnitId = "unexpected_platform";
//#endif

//        _rewardBasedVideoAd.LoadAd(new AdRequest.Builder().Build(), adUnitId);
//        // Initialize the Google Mobile Ads SDK.
//        //MobileAds.Initialize(appId);
//    }

//    public void ShowRewardBasedAd()
//    {
//        bool AdIsReady = (_rewardBasedVideoAd.IsLoaded());
//        if (AdIsReady)
//            _rewardBasedVideoAd.Show();
//        else
//            Debug.Log("This ad is not ready!!!!!");
//    }

//    public event EventHandler<EventArgs> OnAdLoaded;

//    public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

//    public event EventHandler<EventArgs> OnAdOpening;

//    public event EventHandler<EventArgs> OnAdStarted;

//    public event EventHandler<EventArgs> OnAdClosed;

//    public event EventHandler<Reward> OnAdRewarded;

//    public event EventHandler<EventArgs> OnAdLeavingApplication;

//    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
//    {
//        Debug.Log("HandleRewardBasedVideoLoaded event received");
//    }

//    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//    {
//        Debug.Log(
//            "HandleRewardBasedVideoFailedToLoad event received with message: "
//                             + args.Message);
//    }

//    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
//    {
//        Debug.Log("HandleRewardBasedVideoOpened event received");
//    }

//    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
//    {
//        Debug.Log("HandleRewardBasedVideoStarted event received");
//    }

//    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
//    {
//        Debug.Log("HandleRewardBasedVideoClosed event received");
//    }

//    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
//    {
//        string type = args.Type;
//        double amount = args.Amount;
//        Debug.Log(
//            "HandleRewardBasedVideoRewarded event received for "
//                        + amount.ToString() + " " + type);
//    }

//    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
//    {
//        Debug.Log("HandleRewardBasedVideoLeftApplication event received");
//    }
}
