using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;
    public string iosAppKey = "3454729";
    public string androidAppKey = "3454728";

    private Action<bool> acInterClosed, acRewarded;
    private string interKey = "video", rewardKey = "rewardedVideo";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            MyAnalytics.LogEventLogin();
            MyAnalytics.LogEventOpenByDay();
            MyAnalytics.LogEventTimeOpen();
            InitAds();
        }
    }

    private void InitAds()
    {
        string gameId = Application.platform == RuntimePlatform.Android ? androidAppKey : iosAppKey;
        Advertisement.Initialize(gameId, true);
    }

    private bool IsIntersLoaded()
    {
        return Advertisement.IsReady(interKey);
    }

    private bool IsRewardLoaded()
    {
        return Advertisement.IsReady(rewardKey);
    }
    public void ShowInterstitial(Action<bool> _ac) {
        if (!DataUtils.HasRemoveAds())
        {
            Debug.LogError("Show Interstitial");
            if (IsIntersLoaded())
            {
                acInterClosed = _ac;
                //Call Show Interstitial
                var options = new ShowOptions { resultCallback = HandleInters };
                Advertisement.Show(interKey, options);
            }
            else
            {
                if (acInterClosed != null)
                    acInterClosed(true);
            }
        }
    }
    void HandleInters(ShowResult result)
    {
        if (acInterClosed != null)
            acInterClosed(true);
    }


    public void ShowRewardedVideo(Action<bool> _ac)
    {
        if (IsRewardLoaded())
        {
            acRewarded = _ac;
            //Call Show RewardedVideo
            var options = new ShowOptions { resultCallback = HandleRewarded };
            Advertisement.Show(rewardKey, options);
        }
        else
        {
            acRewarded(false);
        }
    }
    void HandleRewarded(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                if (acRewarded != null)
                {
                    acRewarded(true);
                }
                break;
            case ShowResult.Failed:
                break;
            case ShowResult.Skipped:
                break;
        }
    }
    

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
 
    }

    //#region Video
    //void RewardedVideoAdOpenedEvent()
    //{
    //}
    //void RewardedVideoAdClosedEvent()
    //{
    //}
    //#endregion
    //#region Interstitial
    //private void IronSourceEvents_onInterstitialAdOpenedEvent()
    //{
    //}
    //private void IronSourceEvents_onInterstitialAdReadyEvent()
    //{
    //    Debug.LogError("TAGG-InterstitialAdReady");
    //}

    ////private void IronSourceEvents_onInterstitialAdLoadFailedEvent(IronSourceError obj)
    ////{
    ////    Debug.LogError("TAGG-InterstitialAdLoadFail");
    ////}
    //void InterstitialAdClickedEvent()
    //{
    //}
    //void InterstitialAdClosedEvent()
    //{
    //    Debug.LogError("TAGG-InterstitialAdClosedEvent");
    //    if (acInterClosed != null)
    //    {
    //        acInterClosed(true);
    //    }
    //}
    //void InterstitialAdReadyEvent()
    //{
    //}

    //public void OnUnityAdsReady(string placementId)
    //{
    //}

    //public void OnUnityAdsDidError(string message)
    //{
    //    Debug.LogError("UnityAds-Error: " + message);
    //}

    //public void OnUnityAdsDidStart(string placementId)
    //{
    //    if (placementId == interKey)
    //    {

    //    }
    //    else if (placementId == rewardKey)
    //    {

    //    }
    //}

    //public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    //{

    //    if (showResult == ShowResult.Finished)
    //    {
    //        // Reward the user for watching the ad to completion.
    //        if (placementId == rewardKey)
    //        {
    //            if (acRewarded != null)
    //            {
    //                acRewarded(true);
    //            }
    //        }
    //    }
    //    else if (showResult == ShowResult.Skipped)
    //    {
    //        // Do not reward the user for skipping the ad.
    //    }
    //    else if (showResult == ShowResult.Failed)
    //    {
    //        Debug.LogWarning("The ad did not finish due to an error.");
    //    }


    //    if (placementId == interKey)
    //    {
    //        if (acInterClosed != null)
    //        {
    //            acInterClosed(true);
    //        }
    //    }
    //    //else if (placementId == rewardKey)
    //    //{
    //    //    if (acRewarded != null)
    //    //    {
    //    //        acRewarded(true);
    //    //    }
    //    //}
    //}
    //#endregion
}
