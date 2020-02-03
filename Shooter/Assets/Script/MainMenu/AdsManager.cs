using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    public static AdsManager Instance;
    public string iosAppKey = "1486551";
    public string androidAppKey = "1486550";

    private Action<bool> acInterClosed, acRewarded;
    private string interKey = "video", rewardKey = "rewardedVideo";
    private bool _intersLoaded = false, _videoLoaded = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            MyAnalytics.LogEventLogin();
            MyAnalytics.LogEventOpenByDay();
            MyAnalytics.LogEventTimeOpen();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        InitAds();
    }

    private void InitAds()
    {
        string gameId = Application.platform == RuntimePlatform.Android ? androidAppKey : iosAppKey;
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, true);

        //string appKey = Application.platform == RuntimePlatform.Android ? androidAppKey : iosAppKey;
        //IronSource.Agent.init(appKey);
        //IronSource.Agent.setAdaptersDebug(true);
        //IronSource.Agent.validateIntegration();

        //IronSource.Agent.loadInterstitial();
    }

    private bool IsIntersLoaded()
    {
        return _intersLoaded;
    }

    private bool IsRewardLoaded()
    {
        return _videoLoaded;
    }
    public void ShowInterstitial(Action<bool> _ac) {
        if (!DataUtils.HasRemoveAds())
        {
            Debug.LogError("Show Interstitial");
            if (IsIntersLoaded())
            {
                acInterClosed = _ac;
                //Call Show Interstitial
                Advertisement.Show(interKey);
            }
            else
            {
                if (acInterClosed != null)
                    acInterClosed(true);
            }
        }
    }
    public void ShowRewardedVideo(Action<bool> _ac)
    {
        if (IsRewardLoaded())
        {
            acRewarded = _ac;
            //Call Show RewardedVideo
            Advertisement.Show(rewardKey);
        }
        else
        {
            acRewarded(false);
        }
    }

    

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
 
    }

    #region Video
    void RewardedVideoAdOpenedEvent()
    {
    }
    void RewardedVideoAdClosedEvent()
    {
    }
    //void RewardedVideoAdRewardedEvent(IronSourcePlacement placement)
    //{
    //    if (acRewarded != null)
    //    {
    //        acRewarded(true);
    //    }
    //}
    //void RewardedVideoAdShowFailedEvent(IronSourceError error)
    //{
    //}
    #endregion
    #region Interstitial
    private void IronSourceEvents_onInterstitialAdOpenedEvent()
    {
    }
    private void IronSourceEvents_onInterstitialAdReadyEvent()
    {
        Debug.LogError("TAGG-InterstitialAdReady");
    }

    //private void IronSourceEvents_onInterstitialAdLoadFailedEvent(IronSourceError obj)
    //{
    //    Debug.LogError("TAGG-InterstitialAdLoadFail");
    //}
    void InterstitialAdClickedEvent()
    {
    }
    void InterstitialAdClosedEvent()
    {
        Debug.LogError("TAGG-InterstitialAdClosedEvent");
        if (acInterClosed != null)
        {
            acInterClosed(true);
        }
    }
    void InterstitialAdReadyEvent()
    {
    }

    public void OnUnityAdsReady(string placementId)
    {
        if(placementId == interKey)
        {
            _intersLoaded = true;
        }else if(placementId == rewardKey)
        {
            _videoLoaded = true;
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError("UnityAds-Error: " + message);
        _intersLoaded = false;
        _videoLoaded = false;
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        if (placementId == interKey)
        {

        }
        else if (placementId == rewardKey)
        {

        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {

        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
            if (placementId == rewardKey)
            {
                if (acRewarded != null)
                {
                    acRewarded(true);
                }
            }
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }


        if (placementId == interKey)
        {
            if (acInterClosed != null)
            {
                acInterClosed(true);
            }
        }
        //else if (placementId == rewardKey)
        //{
        //    if (acRewarded != null)
        //    {
        //        acRewarded(true);
        //    }
        //}
    }
    #endregion
}
