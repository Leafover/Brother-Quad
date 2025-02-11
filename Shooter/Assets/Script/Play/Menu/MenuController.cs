﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuController : MonoBehaviour
{
    public static MenuController instance;
    public PanelAnimReward panelAnimReward;
    public AudioSource auBG;
    public FreeRewardVideoPanel freeRewardVideoPanel;
    public AchievmentAndDailyQuestPanel achievementAndDailyQuestPanel;
    public GameObject PrimeAccountPanel;
    public BlackMarketPanel blackMarketpanel;
    public GameObject warningEvent, warningDailyQuest, warningAchievment, warningPrimeAccount, warningvideoreward, warningGiftDaily;
    public Text primeText;
    public GiftDailyPanel giftDailyPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DataUtils.FillEquipmentData();
            //DataUtils.FillPlayerDataInfo();
        }
    }
    public void CheckDisplayWarningPrimeAccount()
    {
        if (!DataController.primeAccout.isVIP)
        {
            primeText.color = Color.gray;
            warningPrimeAccount.SetActive(true);
        }
        else
        {
            primeText.color = Color.yellow;
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

        if (!DataParam.first)
        {

#if UNITY_EDITOR

#else
        randomAds = Random.Range(0, 100);
        if (randomAds < 80)
        {
            AdsManager.Instance.ShowInterstitial((b) => { });
        }
#endif
        }
        else
        {
            if (DataParam.cantakegiftdaily)
                BtnOpenGiftDaily(true);
        }

        DataParam.first = false;
        DisplayWarning();
        CheckDisplayWarningPrimeAccount();

        if (DataParam.indexRewardVideo < freeRewardVideoPanel.btnvideo.Length)
        {
            warningvideoreward.SetActive(true);
        }
        else
            warningvideoreward.SetActive(false);

        if (DataController.instance.isHack)
        {
            DataUtils.AddCoinAndGame(100000, 1000);
            DataUtils.TakeHeroPice("P2", 100);
            DataUtils.TakeHeroPice("P1", 100);
        }
        //   DataUtils.AddCoinAndGame(10000, 0);

        #region problem
        //DataUtils.FillPlayerDataInfo();
        if (DataUtils.StageHasInit())
        {
            DataUtils.FillAllStage();
        }
        #endregion
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
        SoundController.instance.PlaySound(soundGame.soundbtnclick);
    }
    public void BtnDisplayBlackMarket(bool open)
    {
        if (open)
        {
            blackMarketpanel.gameObject.SetActive(true);
            MyAnalytics.LogOpenBlackMarket();
        }
        else
        {
            DataController.instance.LoadAgainQuestAndBlackMarket();
            blackMarketpanel.gameObject.SetActive(false);
        }
        SoundController.instance.PlaySound(soundGame.soundbtnclick);
    }
    public void BtnDisplayFreeRewardVideoPanel(bool open)
    {
        freeRewardVideoPanel.gameObject.SetActive(open);
        SoundController.instance.PlaySound(soundGame.soundbtnclick);
    }
    public void DisplayWarning()
    {

        warningAchievment.SetActive(DataController.instance.CheckWarningAchievement());
        warningDailyQuest.SetActive(DataController.instance.CheckWarningDailyQuest());

        warningEvent.SetActive(CheckWarningDisplay());

        CheckWarningGiftDaily();
    }
    public void CheckWarningGiftDaily()
    {
        if (warningGiftDaily != null)
            warningGiftDaily.SetActive(DataParam.cantakegiftdaily);
    }
    bool CheckWarningDisplay()
    {
        if (warningAchievment.activeSelf || warningDailyQuest.activeSelf)
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {

            //DataController.instance.DoAchievement(0, 1000);
            //for (int i = 0; i < DataController.allSaveDailyQuest.Count; i++)
            //{
            //    DataController.instance.DoDailyQuest(i, 1000);
            //}
            BtnOpenGiftDaily(true);
        }
        //else if(Input.GetKeyDown(KeyCode.H))
        //{
        //    DataUtils.SaveLevel(0, 0);
        //    DataUtils.SaveLevel(0, 1);
        //    DataUtils.SaveLevel(0, 2);
        //    DataUtils.SaveLevel(0, 3);
        //    DataUtils.SaveLevel(0, 4);
        //    DataUtils.SaveLevel(0, 5);
        //    DataUtils.SaveLevel(0, 6);
        //}
    }
    public void BtnOpenGiftDaily(bool opem)
    {
        SoundController.instance.PlaySound(soundGame.soundbtnclick);
        if (opem)
            giftDailyPanel.OpenMe();
        else
            giftDailyPanel.CloseMe();
    }
}
