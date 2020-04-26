using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GiftDailyPanel : MonoBehaviour
{
    public GiftDailyBouder[] giftdailyBouder;
    public GameObject selectBouder, btnClaim,resetText;
    GiftDailyBouder currentGiftDailyBouder;
    public Image iconVideo, btnClaimX2;
    public void DisplayBegin()
    {
        for (int i = 0; i < giftdailyBouder.Length; i++)
        {
            giftdailyBouder[i].Display(DataParam.firsttimegiftdaily);
        }
        selectBouder.transform.parent = giftdailyBouder[DataParam.currentGiftDaily].transform;
        selectBouder.transform.localPosition = /*giftdailyBouder[DataParam.currentGiftDaily].transform.position*/Vector3.zero;
        selectBouder.transform.localScale = Vector3.one;
       // Debug.LogError(DataParam.currentGiftDaily + ":" + giftdailyBouder[DataParam.currentGiftDaily].transform.position + ":" + selectBouder.transform.position);
        btnClaim.SetActive(DataParam.cantakegiftdaily);
        btnClaimX2.gameObject.SetActive(DataParam.cantakegiftdaily);
        selectBouder.SetActive(DataParam.cantakegiftdaily);
        resetText.SetActive(!DataParam.cantakegiftdaily);
        currentGiftDailyBouder = giftdailyBouder[DataParam.currentGiftDaily];
    }
    void Update()
    {
        if (AdsManager.Instance.IsRewardLoaded())
        {
            btnClaimX2.color = iconVideo.color = Color.white;
        }
        else
            btnClaimX2.color = iconVideo.color = Color.gray;
    }

    public void OpenMe()
    {
        DisplayBegin();
        gameObject.SetActive(true);
        MyAnalytics.LogEventOpenDailyGift(System.DateTime.Now);
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
        if(btnClaimX2.color == Color.gray)
            return;

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
        DataParam.oldTimeGiftDaily = System.DateTime.Now;      
        DataController.giftDaily[currentGiftDailyBouder.index].isDone = true;
        btnClaim.SetActive(false);
        btnClaimX2.gameObject.SetActive(false);
        selectBouder.SetActive(false);
        resetText.SetActive(true);

        if(x2)
        {
            MyAnalytics.LogEventClaimX2DailyGift(DataParam.currentGiftDaily, DataParam.oldTimeGiftDaily);
        }
        else
            MyAnalytics.LogEventClaimDailyGift(DataParam.currentGiftDaily, DataParam.oldTimeGiftDaily);

        CloseMe();

    }

}
