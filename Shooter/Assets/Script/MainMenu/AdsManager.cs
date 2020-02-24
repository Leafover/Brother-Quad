using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api.Mediation.UnityAds;
using GoogleMobileAds.Api;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;
    public string iosAppKey = "3454729";
    public string androidAppKey = "3454728";

    private Action<bool> acInterClosed, acRewarded;
    private string interKey = "video", rewardKey = "rewardedVideo";

    #region Admob
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;
    private BannerView banner;

    #endregion
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
        // string gameId = Application.platform == RuntimePlatform.Android ? androidAppKey : iosAppKey;
        //Advertisement.Initialize(gameId, false);
        MobileAds.Initialize(DataParam.APP_ID);
        InitInterstitial();
        InitRewarded();
        UnityAds.SetGDPRConsentMetaData(true);
    }

    private void InitBanner()
    {
        banner = new BannerView("ca-app-pub-8566745611252640/6715057094", AdSize.Banner, AdPosition.BottomRight);
        banner.OnAdLoaded += Banner_OnAdLoaded;
        banner.LoadAd(CreateRequest());
    }

    public void ShowBanner()
    {
        if(banner != null)
        {
            banner.Show();
        }
    }
    public void HideBanner()
    {
        if (banner != null)
        {
            banner.Hide();
        }
    }
    private void Banner_OnAdLoaded(object sender, EventArgs e)
    {
        HideBanner();
    }

    private bool IsIntersLoaded()
    {
        return interstitial.IsLoaded();
    }

    private bool IsRewardLoaded()
    {
        return rewardedAd.IsLoaded();
    }
    public void ShowInterstitial(Action<bool> _ac)
    {
        if (!DataUtils.HasRemoveAds())
        {
            Debug.LogError("Show Interstitial");
            if (IsIntersLoaded())
            {
                acInterClosed = _ac;
                //Call Show Interstitial
                interstitial.Show();
                //var options = new ShowOptions { resultCallback = HandleInters };
                //Advertisement.Show(interKey, options);
            }
            else
            {
                interstitial.LoadAd(CreateRequest());
                if (acInterClosed != null)
                    acInterClosed(true);
            }
        }
    }
    //void HandleInters(ShowResult result)
    //{
    //    if (acInterClosed != null)
    //        acInterClosed(true);
    //}


    public void ShowRewardedVideo(Action<bool> _ac)
    {
        Debug.LogError("Ads:: _ac: " + (_ac == null ? "_ac NULL" : "_ac NOT NULL") + "  ,,,,,, rewardedAd: " + rewardedAd.IsLoaded());
        if (IsRewardLoaded())
        {
            acRewarded = _ac;
            //Call Show RewardedVideo
            //var options = new ShowOptions { resultCallback = HandleRewarded };
            //Advertisement.Show(rewardKey, options);
            rewardedAd.Show();
        }
        else
        {
            rewardedAd.LoadAd(CreateRequest());
            acRewarded(false);
        }
    }
    //void HandleRewarded(ShowResult result)
    //{
    //    switch (result)
    //    {
    //        case ShowResult.Finished:
    //            if (acRewarded != null)
    //            {
    //                acRewarded(true);
    //            }
    //            break;
    //        case ShowResult.Failed:
    //            break;
    //        case ShowResult.Skipped:
    //            break;
    //    }
    //}


    #region Init Admob
    AdRequest CreateRequest()
    {
        AdRequest request = new AdRequest.Builder().AddTestDevice("BA730DD6C0C19894C11CB7FDF6D75AA8").Build();
        return request;
    }
    void InitInterstitial()
    {
        interstitial = new InterstitialAd(DataParam.INTERS_ID);
        interstitial.OnAdClosed += HandleOnAdClosed;
        interstitial.LoadAd(CreateRequest());
    }
    void InitRewarded()
    {
        this.rewardedAd = new RewardedAd(DataParam.REWARDED_ID);
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.OnAdLoaded += RewardedAd_OnAdLoaded;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        this.rewardedAd.LoadAd(CreateRequest());
    }

    private void RewardedAd_OnAdLoaded(object sender, EventArgs e)
    {
        print("Ads-RewardedAd_OnAdLoaded");
    }

    #region Handler
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        print("Ads-HandleAdClosed event received");
        if (acInterClosed != null)
            acInterClosed(true);
        interstitial.LoadAd(CreateRequest());
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("Ads-HandleRewardedAdClosed event received");
        if (acRewarded != null)
        {
            acRewarded(false);
        }
        InitRewarded();
        //this.rewardedAd.LoadAd(CreateRequest());
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        if (acRewarded != null)
        {
            acRewarded(true);
        }
    }
    #endregion
    #endregion

    private void OnEnable()
    {
    }

    private void OnDisable()
    {

    }
}
