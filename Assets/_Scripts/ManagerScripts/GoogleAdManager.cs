using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine;

public class GoogleAdManager : MonoBehaviour
{
    public static GoogleAdManager Instance { set; get; }
    //Real ad id -- ca-app-pub-1211888741379414/3559395974
    private void Awake()
    {
        Instance = this;
    }
}
