using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;
    public AudioSource auBG;
    private void Awake()
    {
        instance = this;
        Debug.unityLogger.logEnabled = false;
    }
    int randomAds;
    private void Start()
    {
        SoundController.instance.DisplaySetting();

        if(!DataParam.first)
        {
            randomAds = Random.Range(0, 100);
            if(randomAds < 20)
            {
                AdsManager.Instance.ShowInterstitial((b) => {});
            }
        }

        DataUtils.FillPlayerDataInfo();
        if (DataUtils.StageHasInit())
        {
            DataUtils.FillAllStage();
         //   Debug.LogError("zooooooooooo2");
        }
        DataParam.first = false;
    }
}
