﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GiftDailyPanel : MonoBehaviour
{
    public GiftDailyBouder[] giftdailyBouder;
    public GameObject selectBouder, btnClaim, btnClaimX2;
    GiftDailyBouder currentGiftDailyBouder;
    public void DisplayBegin()
    {
        for (int i = 0; i < giftdailyBouder.Length; i++)
        {
            giftdailyBouder[i].Display(DataParam.firsttimegiftdaily);
        }
        selectBouder.transform.position = giftdailyBouder[DataParam.currentGiftDaily].transform.position;

        btnClaim.SetActive(DataParam.cantakegiftdaily);
        btnClaimX2.SetActive(DataParam.cantakegiftdaily);
        selectBouder.SetActive(DataParam.cantakegiftdaily);

        currentGiftDailyBouder = giftdailyBouder[DataParam.currentGiftDaily];


    }
    public void OpenMe()
    {
        DisplayBegin();
        gameObject.SetActive(true);
    }
    public void CloseMe()
    {
        DataController.instance.LoadAgainGiftPanel();
        gameObject.SetActive(false);
    }
    public void BtnClaim()
    {
        Reward(false);
    }
    public void BtnClaimX2()
    {
        SoundController.instance.PlaySound(soundGame.soundbtnclick);
#if UNITY_EDITOR

        Reward(true);
#else
        AdsManager.Instance.ShowRewardedVideo((b) => {if(b) Reward(true);});
#endif
    }
    int numberAdd;
    string nameAdd;
    DataUtils.eLevel eLevel;
    DataUtils.eType eType;
    void Reward(bool x2)
    {
        numberAdd = x2 == false ? DataController.giftDaily[currentGiftDailyBouder.index].numberReward : DataController.giftDaily[currentGiftDailyBouder.index].numberReward * 2;
        nameAdd = DataController.giftDaily[currentGiftDailyBouder.index].nameReward;
        eLevel = DataController.giftDaily[currentGiftDailyBouder.index].eLevel;
        eType = DataController.giftDaily[currentGiftDailyBouder.index].eType;
        switch (currentGiftDailyBouder.index)
        {
            case 0:
                DataUtils.AddCoinAndGame(numberAdd, 0);
                break;
            case 1:
                DataUtils.TakeHeroPice(nameAdd, numberAdd);
                break;
            case 2:
                DataUtils.TakeItem(nameAdd, eType, eLevel, numberAdd, false);
                break;
            case 3:
                DataUtils.AddCoinAndGame(0, numberAdd);
                break;
            case 4:
                DataUtils.AddHPPack(numberAdd);
                break;
            case 5:
                DataUtils.TakeItem(nameAdd, eType, eLevel, numberAdd, false);
                break;
        }
        DataParam.cantakegiftdaily = false;
        DataController.giftDaily[currentGiftDailyBouder.index].isDone = true;
        btnClaim.SetActive(false);
        btnClaimX2.SetActive(false);
        selectBouder.SetActive(false);
        CloseMe();
    }

}
