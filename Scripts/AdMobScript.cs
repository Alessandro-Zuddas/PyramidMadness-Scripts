using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdMobScript : MonoBehaviour
{

    private BannerView bannerView;
    private InterstitialAd interstitial;


    public static AdMobScript current;

    //Prova push

    private void Awake()
    {
        if (current == null)
        {
            current = this;                     //Se current � nulla la caarichiamo con this(la classe in cui siamo), rendiamo lo script leggibile da altri script.
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        this.RequestBanner();
        this.RequestInterstitial();
       
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-9362957212940324~7475315125";  //test ID ca-app-pub-3940256099942544/6300978111 *    //Id vero ca-app-pub-9362957212940324~7475315125
#elif UNITY_IOS
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.BottomLeft);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-9362957212940324/2247340491"; //id test ca-app-pub-3940256099942544/1033173712 *  //id vero ca-app-pub-9362957212940324/2247340491
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    public void GameOver()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }

    public class AppOpenAdManager
    {
#if UNITY_ANDROID
        private const string AD_UNIT_ID = "ca-app-pub-9362957212940324/3205198940"; //test id ca-app-pub-3940256099942544/3419835294* //id vero ca-app-pub-9362957212940324/3205198940
#elif UNITY_IOS
    private const string AD_UNIT_ID = "ca-app-pub-3940256099942544/5662855259";
#else
    private const string AD_UNIT_ID = "unexpected_platform";
#endif

        private static AppOpenAdManager instance;

        private AppOpenAd ad;

        private bool isShowingAd = false;

        public static AppOpenAdManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppOpenAdManager();
                }

                return instance;
            }
        }

        private DateTime loadTime;

        private bool IsAdAvailable
        {
            get
            {
                return ad != null && (System.DateTime.UtcNow - loadTime).TotalHours < 4;
            }
        }

        public void LoadAd()
        {
            AdRequest request = new AdRequest.Builder().Build();

            // Load an app open ad for portrait orientation
            AppOpenAd.LoadAd(AD_UNIT_ID, ScreenOrientation.Portrait, request, ((appOpenAd, error) =>
            {
                if (error != null)
                {
                    // Handle the error.
                    Debug.LogFormat("Failed to load the ad. (reason: {0})", error.LoadAdError.GetMessage());
                    return;
                }

                // App open ad is loaded.
                ad = appOpenAd;
                loadTime = DateTime.UtcNow;
            }));
        }

        public void ShowAdIfAvailable()
        {
            if (!IsAdAvailable || isShowingAd)
            {
                return;
            }

            ad.OnAdDidDismissFullScreenContent += HandleAdDidDismissFullScreenContent;
            ad.OnAdFailedToPresentFullScreenContent += HandleAdFailedToPresentFullScreenContent;
            ad.OnAdDidPresentFullScreenContent += HandleAdDidPresentFullScreenContent;
            ad.OnAdDidRecordImpression += HandleAdDidRecordImpression;
            ad.OnPaidEvent += HandlePaidEvent;

            ad.Show();
        }

        private void HandleAdDidDismissFullScreenContent(object sender, EventArgs args)
        {
            Debug.Log("Closed app open ad");
            // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
            ad = null;
            isShowingAd = false;
            LoadAd();
        }

        private void HandleAdFailedToPresentFullScreenContent(object sender, AdErrorEventArgs args)
        {
            Debug.LogFormat("Failed to present the ad (reason: {0})", args.AdError.GetMessage());
            // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
            ad = null;
            LoadAd();
        }

        private void HandleAdDidPresentFullScreenContent(object sender, EventArgs args)
        {
            Debug.Log("Displayed app open ad");
            isShowingAd = true;
        }

        private void HandleAdDidRecordImpression(object sender, EventArgs args)
        {
            Debug.Log("Recorded ad impression");
        }

        private void HandlePaidEvent(object sender, AdValueEventArgs args)
        {
            Debug.LogFormat("Received paid event. (currency: {0}, value: {1}",
                    args.AdValue.CurrencyCode, args.AdValue.Value);
        }


    }

    public class GoogleMobileAdsDemoScript : MonoBehaviour
    {

        public void Start()
        {
            // Load an app open ad when the scene starts
            AppOpenAdManager.Instance.LoadAd();
        }

        public void OnApplicationPause(bool paused)
        {
            // Display the app open ad when the app is foregrounded
            if (!paused)
            {
                AppOpenAdManager.Instance.ShowAdIfAvailable();
            }
        }
    }
}


