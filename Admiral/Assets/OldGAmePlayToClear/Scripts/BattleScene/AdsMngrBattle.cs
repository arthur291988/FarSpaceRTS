using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsMngrBattle : MonoBehaviour
{
    private string gameId = "4009839";
    private string myInterstitialId = "video";
    private string myBannerId = "bannerPlacement";
    private bool testMode = false;
    // Start is called before the first frame update
    void Start()
    {
        Lists.sceneChangeCounts++;

        if (Advertisement.isSupported && !Lists.adsBought /*&& Application.platform == RuntimePlatform.Android*/)
        {
            Advertisement.Initialize(gameId, testMode);
        }

        if (Lists.sceneChangeCounts > 1 /*&& Application.platform == RuntimePlatform.Android*/)
        {
            if (Advertisement.IsReady() && !Lists.adsBought)
            {
                Advertisement.Show(myInterstitialId);
                Lists.sceneChangeCounts = 0;
            }
        }
        if (!Lists.adsBought /*&& Application.platform == RuntimePlatform.Android*/) StartCoroutine(ShowBannerWhenInitialized());
    }
    IEnumerator ShowBannerWhenInitialized()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Banner.Show(myBannerId);
    }

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy()
    {
        if (!Lists.adsBought) Advertisement.Banner.Hide(true);
    }
}
