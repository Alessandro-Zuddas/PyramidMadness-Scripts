using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAd : MonoBehaviour
{

    private string game_id = "4612021";
    private static string video_ad = "Interstitial";
    private string adUnitId;

    public InterstitialAd(string adUnitId)
    {
        this.adUnitId = adUnitId;
    }

    void Start()
    {
        Advertisement.Initialize(game_id, true);
    }

    public static void ShowAd()
    {
        if (Advertisement.IsReady(video_ad))
        {
            Advertisement.Show(video_ad);
            Advertisement.Banner.Hide();
        }
    }

    public void ShowAdClick()
    {
        if (Advertisement.IsReady(video_ad))
        {
            Advertisement.Show(video_ad);
            Advertisement.Banner.Hide();
        }
    }

    internal void LoadAd(AdRequest request)
    {
        throw new NotImplementedException();
    }

    internal bool IsLoaded()
    {
        throw new NotImplementedException();
    }

    internal void Show()
    {
        throw new NotImplementedException();
    }
}
