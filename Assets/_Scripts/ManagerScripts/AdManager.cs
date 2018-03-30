using System.Collections;
using System.Collections.Generic;
using admob;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance { set; get; }

    private string bannerId = "ca-app-pub-1211888741379414/8807931452";
    private string InterstialId = "ca-app-pub-1211888741379414/8362575878";

    //private string bannerId = "ca-app-pub-1211888741379414/7092157316";
    //private string VideoId = "ca-app-pub-1211888741379414/3559395974";

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        //For testing -- CODE( Admob.Instance().setTesting(true);)
        Instance = this;

#if UNITY_EDITOR
        Debug.Log("Initilize admob right now");
#elif UNITY_ANDROID
        Admob.Instance().initAdmob("", InterstialId);
        Admob.Instance().setTesting(true);
        Admob.Instance().loadInterstitial();
#endif
    }


    void ShowBannerAd()
    {
#if UNITY_EDITOR
        Debug.Log("Unable to play ad from editor");
#elif UNITY_ANDROID
        Debug.Log("Banner ad has been played");
#endif
    }

    public void ShowVideo()
    {
#if UNITY_EDITOR
        Debug.Log("Unable to play ad from editor");
#elif UNITY_ANDROID
        if (Admob.Instance().isInterstitialReady())
        {
            Admob.Instance().showInterstitial();
        }

        else
        {
            Debug.Log("Ad is not ready at the moment");
        }
#endif
    }
}
