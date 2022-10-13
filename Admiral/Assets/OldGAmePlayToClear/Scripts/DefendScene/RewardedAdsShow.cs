
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;

public class RewardedAdsShow : MonoBehaviour, IUnityAdsListener
{
    private string gameId = "4009839";
    private string mySurfacingId = "rewardedVideo";
    private string myBannerId = "bannerPlacement";
    private string myInterstitialId = "video";
    private bool testMode = false;
    [SerializeField]
    private Button NoEnergyShowRewrdedAddButton;
    [SerializeField]
    private Button NoCruisShowRewrdedAddButton;
    //tis one is used to determine which reward to give to player (0-energy and HP for gun, 1-extra cruisers of 4 th class 3 pcs)
    private int rewardId; 


    // Start is called before the first frame update
    void Start()
    {
        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        if (Advertisement.isSupported /*&& Application.platform == RuntimePlatform.Android*/)
        {
            Advertisement.Initialize(gameId, testMode);
        }
        Lists.sceneChangeCounts++;
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
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(myBannerId);
    }

    public void ShowRewardedVideo(int rewardIDVar)
    {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady(mySurfacingId))
        {
            Advertisement.Show(mySurfacingId);
            rewardId = rewardIDVar;
        }
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string surfacingId)
    {
        // If the ready Ad Unit or legacy Placement is rewarded, activate the button: 
        if (surfacingId == mySurfacingId)
        {
            NoEnergyShowRewrdedAddButton.interactable = true;
            NoCruisShowRewrdedAddButton.interactable = true;
        }
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string surfacingId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            if (rewardId==0) LaunchManager.Instance.watchRewardedOutOfEnergy();
            else LaunchManager.Instance.watchRewardedNoCruiser();
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
        }
    }

    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidStart(string surfacingId)
    {
    }

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
        if (!Lists.adsBought) Advertisement.Banner.Hide(true);
    }
}
