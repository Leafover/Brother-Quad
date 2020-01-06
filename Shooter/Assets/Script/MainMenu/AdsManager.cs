using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;
    public string iosAppKey = "ae837cdd";
    public string androidAppKey = "ae82d045";
    private Action<bool> acInterClosed, acRewarded;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            MyAnalytics.LogEventLogin();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        InitAds();
    }

    private void InitAds()
    {
        string appKey = Application.platform == RuntimePlatform.Android ? androidAppKey : iosAppKey;
        IronSource.Agent.init(appKey);
        IronSource.Agent.setAdaptersDebug(true);
        IronSource.Agent.validateIntegration();

        IronSource.Agent.loadInterstitial();
    }


    public void ShowInterstitial(Action<bool> _ac) {
        if (!DataUtils.HasRemoveAds())
        {
            Debug.LogError("Show Interstitial");
            if (IronSource.Agent.isInterstitialReady())
            {
                acInterClosed = _ac;
                IronSource.Agent.showInterstitial();
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
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            acRewarded = _ac;
            IronSource.Agent.showRewardedVideo();
        }
        else
        {
            acRewarded(false);
        }
    }

    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }

    private void OnEnable()
    {
        #region Interstitial
        IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent += IronSourceEvents_onInterstitialAdLoadFailedEvent;
        IronSourceEvents.onInterstitialAdReadyEvent += IronSourceEvents_onInterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent += IronSourceEvents_onInterstitialAdOpenedEvent;
        #endregion
        #region Video
        IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
        #endregion
    }

    private void OnDisable()
    {
        #region Interstitial
        IronSourceEvents.onInterstitialAdReadyEvent -= InterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdClickedEvent -= InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent -= InterstitialAdClosedEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent -= IronSourceEvents_onInterstitialAdLoadFailedEvent;
        IronSourceEvents.onInterstitialAdReadyEvent -= IronSourceEvents_onInterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent -= IronSourceEvents_onInterstitialAdOpenedEvent;
        #endregion
        #region Video
        IronSourceEvents.onRewardedVideoAdOpenedEvent -= RewardedVideoAdOpenedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent -= RewardedVideoAdClosedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent -= RewardedVideoAdRewardedEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedEvent -= RewardedVideoAdShowFailedEvent;
        #endregion
    }

    #region Video
    void RewardedVideoAdOpenedEvent()
    {
    }
    void RewardedVideoAdClosedEvent()
    {
    }
    void RewardedVideoAdRewardedEvent(IronSourcePlacement placement)
    {
        if (acRewarded != null)
        {
            acRewarded(true);
        }
    }
    void RewardedVideoAdShowFailedEvent(IronSourceError error)
    {
    }
    #endregion
    #region Interstitial
    private void IronSourceEvents_onInterstitialAdOpenedEvent()
    {
    }
    private void IronSourceEvents_onInterstitialAdReadyEvent()
    {
        Debug.LogError("TAGG-InterstitialAdReady");
    }

    private void IronSourceEvents_onInterstitialAdLoadFailedEvent(IronSourceError obj)
    {
        Debug.LogError("TAGG-InterstitialAdLoadFail");
    }
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
        IronSource.Agent.loadInterstitial();
    }
    void InterstitialAdReadyEvent()
    {
    }
    #endregion
}
