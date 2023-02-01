using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAd : MonoBehaviour
{

    private string game_id = "4612021";
    private static string banner_ad = "Banner";

    private static BannerAd instance;

    public BannerPosition position;

    void Start()
    {
        instance = this;
        Advertisement.Initialize(game_id, true);  //test mode
        ShowBanner();

    }

    public static void ShowBanner()
    {
        instance.StartCoroutine(instance.displayBanner());
    }

    IEnumerator displayBanner()
    {
        while (!Advertisement.IsReady(banner_ad))
        {
            yield return null;
        }

        Advertisement.Banner.SetPosition(position);
        Advertisement.Banner.Show(banner_ad);
    }
}
