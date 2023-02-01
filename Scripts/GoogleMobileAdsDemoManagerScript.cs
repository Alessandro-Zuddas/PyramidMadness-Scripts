using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleMobileAdsDemoManagerScript : MonoBehaviour
{
    public void Start()
    {
        // Load an app open ad when the scene starts
        AdMobScript.AppOpenAdManager.Instance.LoadAd();
    }

    public void OnApplicationPause(bool paused)
    {
        // Display the app open ad when the app is foregrounded
        if (!paused)
        {
            AdMobScript.AppOpenAdManager.Instance.ShowAdIfAvailable();
        }
    }


}
