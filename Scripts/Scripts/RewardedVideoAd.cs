using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Monetization;
using UnityEngine.UI;

public class RewardedVideoAd : MonoBehaviour
{

    private string game_id = "4612021";
    private static string rewardedVideo_ad = "RewardedVideo";

    private static RewardedVideoAd instance;

    void Start()
    {
        Monetization.Initialize(game_id, true);
        instance = this;
    }

    public static void ShowAd()
    {
        instance.StartCoroutine(instance.WaitForAd());
    }

    public void ShowAdClick()
    {
        instance.StartCoroutine(instance.WaitForAd());
    }



    IEnumerator WaitForAd()
    {
        while (!Monetization.IsReady(rewardedVideo_ad))
        {
            yield return null;
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent(rewardedVideo_ad) as ShowAdPlacementContent;

        if (ad != null)
        {
            ad.Show(AdFinished);
        }
    }

    void AdFinished(UnityEngine.Monetization.ShowResult result)
    {
        if (result == UnityEngine.Monetization.ShowResult.Finished)
        {
            PlayerScript.ricompensa.rewardButton.GetComponent<Button>().enabled = false;                    //Prendiamo il componente button di rewarded button e impostiamo la proprietà enabled su false
            PlayerScript.ricompensa.rewardButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);       //Rendiamo il componente image di reward button trasparente poichè non attivo.


            PlayerScript.ricompensa.reloadButton.GetComponent<Button>().enabled = true;
            PlayerScript.ricompensa.reloadButton.GetComponent<Image>().color = new Color(1, 1, 1, 1f);

            PlayerScript.ricompensa.rewardButton.GetComponent<Animator>().enabled = false;
            PlayerScript.ricompensa.reloadButton.GetComponent<Animator>().enabled = true;
        }
        else
        {
            // VIDEO WAS SKIPPPED
            Debug.Log("Skipped");
        }
    }
}
