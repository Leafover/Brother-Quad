using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;
    public AudioSource auBG;
    public FreeRewardVideoPanel freeRewardVideoPanel;
    public AchievmentAndDailyQuestPanel achievementAndDailyQuestPanel;
    public GameObject PrimeAccountPanel;
    public BlackMarketPanel blackMarketpanel;
    public GameObject warningEvent, warningDailyQuest, warningAchievment, warningPrimeAccount,warningvideoreward;

    private void Awake()
    {
        instance = this;

    }
    public void CheckDisplayWarningPrimeAccount()
    {
        if (!DataController.primeAccout.isVIP)
        {
            warningPrimeAccount.SetActive(true);
        }
        else
        {
            if (!DataController.primeAccout.takecoin || !DataController.primeAccout.takegem)
            {
                warningPrimeAccount.SetActive(true);
            }
            else
            {
                warningPrimeAccount.SetActive(false);
            }
        }
    }
    int randomAds;
    private void Start()
    {
        SoundController.instance.DisplaySetting();

        DataUtils.FillPlayerDataInfo();
        if (DataUtils.StageHasInit())
        {
            DataUtils.FillAllStage();
            //   Debug.LogError("zooooooooooo2");
        }

        if (!DataParam.first)
        {
            //   AdsManager.Instance.ShowInterstitial((b) => { });
#if UNITY_EDITOR

#else
        randomAds = Random.Range(0, 100);
        if (randomAds < 60)
        {
            AdsManager.Instance.ShowInterstitial((b) => { });
        }
#endif
        }

        DataParam.first = false;
        DisplayWarning();
        CheckDisplayWarningPrimeAccount();

        if(DataParam.indexRewardVideo < freeRewardVideoPanel.btnvideo.Length)
        {
            warningvideoreward.SetActive(true);
        }
        else
            warningvideoreward.SetActive(false);
        //  DataUtils.AddCoinAndGame(10000000, 100000);
    }
    public void BtnDisplayPrimeAccount(bool open)
    {
        if (open)
        {
            warningPrimeAccount.SetActive(false);
            PrimeAccountPanel.SetActive(true);
        }
        else
        {
            PrimeAccountPanel.SetActive(false);
            CheckDisplayWarningPrimeAccount();
        }
    }
    public void BtnDisplayBlackMarket(bool open)
    {
        if(open)
        {
            blackMarketpanel.gameObject.SetActive(true);
        }
        else
        {
            blackMarketpanel.gameObject.SetActive(false);
        }
    }
    public void BtnDisplayFreeRewardVideoPanel(bool open)
    {
        freeRewardVideoPanel.gameObject.SetActive(open);
    }
    public void DisplayWarning()
    {
        warningEvent.SetActive(CheckWarningDisplay());
        warningAchievment.SetActive(DataController.instance.CheckWarningAchievement());
        warningDailyQuest.SetActive(DataController.instance.CheckWarningDailyQuest());
    }
    bool CheckWarningDisplay()
    {
        if (DataController.instance.CheckWarningDailyQuest() || DataController.instance.CheckWarningAchievement())
        {
            //Debug.Log("true");
            return true;
        }
        else
        {
            //Debug.Log("false");
            return false;
        }
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.O))
    //    {
    //        DataController.instance.DoDailyQuest(1, 1000);
    //        DataController.instance.DoAchievement(0, 1000);
    //    }
    //    //else if(Input.GetKeyDown(KeyCode.H))
    //    //{
    //    //    DataUtils.SaveLevel(0, 0);
    //    //    DataUtils.SaveLevel(0, 1);
    //    //    DataUtils.SaveLevel(0, 2);
    //    //    DataUtils.SaveLevel(0, 3);
    //    //    DataUtils.SaveLevel(0, 4);
    //    //    DataUtils.SaveLevel(0, 5);
    //    //    DataUtils.SaveLevel(0, 6);
    //    //}
    //}
}
