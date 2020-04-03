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


            GameObject g = new GameObject("-----");
            if (g.GetComponent<PushNotificationManager>() == null)
            {
                g.AddComponent<PushNotificationManager>();
            }
        }
    }

    private void OnApplicationQuit()
    {
        DataUtils.SavePlayerData();
    }
    private void InitAds()
    {
        // string gameId = Application.platform == RuntimePlatform.Android ? androidAppKey : iosAppKey;
        //Advertisement.Initialize(gameId, false);
        MobileAds.Initialize(DataParam.APP_ID);
        InitInterstitial();
        InitRewarded();
        InitBanner();
        UnityAds.SetGDPRConsentMetaData(true);
    }

    private void InitBanner()
    {
        banner = new BannerView(DataParam.BANNER_ID, AdSize.Banner, AdPosition.BottomRight);
        banner.OnAdLoaded += Banner_OnAdLoaded;
        banner.LoadAd(CreateRequest());
    }
    public void ShowBannerWithPos()
    {
        if(banner != null)
        {
            banner.SetPosition(AdPosition.BottomLeft);
            banner.Show();
        }
    }
    public void ShowBanner()
    {
        if(banner != null)
        {
            banner.SetPosition(AdPosition.BottomRight);
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

    public bool IsRewardLoaded()
    {
        return rewardedAd.IsLoaded();
    }
    public void ShowInterstitial(Action<bool> _ac)
    {
         //if(DataController.primeAccout.isVIP)
        if (!DataUtils.HasRemoveAds() || !DataController.primeAccout.isVIP)
        {
            //Debug.LogError("Show Interstitial");
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
        //Debug.LogError("Ads:: _ac: " + (_ac == null ? "_ac NULL" : "_ac NOT NULL") + "  ,,,,,, rewardedAd: " + rewardedAd.IsLoaded());
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
        AdRequest request = new AdRequest.Builder().AddTestDevice("BA730DD6C0C19894C11CB7FDF6D75AA8").AddTestDevice("D96EFB8D3BB99E5B5CAF739EB1EB5E9D").AddTestDevice("256FC58E8184F47CC4E7BE3570B2AC3B").Build();
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

 

    #region Handler
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        //Debug.LogError("Ads-HandleAdClosed event received");
        if (acInterClosed != null)
            acInterClosed(true);
        interstitial.LoadAd(CreateRequest());
    }

    private void RewardedAd_OnAdLoaded(object sender, EventArgs e)
    {
        //Debug.LogError("Ads-RewardedAd_OnAdLoaded");
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        //Debug.LogError("Ads-HandleRewardedAdClosed event received");
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
