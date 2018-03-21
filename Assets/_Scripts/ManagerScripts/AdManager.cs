using System.Collections;
using System.Collections.Generic;
using admob;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance { set; get; }

    //private string bannerId = "ca-app-pub-1211888741379414/8807931452";
    //private string VideoId = "ca-app-pub-1211888741379414/8362575878";

    private string bannerId = "ca-app-pub-1211888741379414/7092157316";
    private string VideoId = "ca-app-pub-1211888741379414/3559395974";

    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

#if UNITY_EDITOR
#elif UNITY_ANDROID  

        Admob.Instance().initAdmob(bannerId, VideoId);
        Admob.Instance().setTesting(true);
        Admob.Instance().loadInterstitial();

#endif
    }

    public void ShowBanner()
    {
#if UNITY_EDITOR
        Debug.Log("Unable to play ad from Editor");
#elif UNITY_ANDROID
        Admob.Instance().showBannerRelative(AdSize.Banner, AdPosition.BOTTOM_CENTER, 0);
#endif
    }

    public void ShowVideo()
    {
#if UNITY_EDITOR
        Debug.Log("Unable to play ad from Editor");
#elif UNITY_ANDROID
        if (Admob.Instance().isInterstitialReady())
        {
            Admob.Instance().showInterstitial();
        }
#endif
    }
}
