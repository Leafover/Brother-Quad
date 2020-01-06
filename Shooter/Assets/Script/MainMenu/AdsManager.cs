using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;

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
        InitInterstitial();
        InitRewarded();
    }
    private void InitInterstitial() { }
    private void InitRewarded() { }


    public void ShowInterstitial() {
        if (!DataUtils.HasRemoveAds())
        {
            Debug.LogError("Show Interstitial");
        }
    }
    public void ShowRewardedVideo()
    {

    }
}
