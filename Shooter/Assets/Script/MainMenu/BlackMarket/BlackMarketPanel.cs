using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
public class BlackMarketPanel : MonoBehaviour
{
    public Sprite[] levelSp;
    public Sprite gemSp, videoSp;
    public Text priceRefreshText;
    public Image iconRefreshImg;
    public TimeSpan timeSpanTemp;
    string timetemp = "20:20:02";
    double timeCount;
    public List<BouderBlackmarket> bouders;
    public TextMeshProUGUI timeText;
    void Start()
    {
        for (int i = 0; i < bouders.Count; i++)
        {
            bouders[i].DisplayItem();
        }
        priceRefreshText.text = "" + DataParam.countResetBlackMarket * 5;

        if (DataParam.countResetBlackMarket > 0)
        {   
            iconRefreshImg.sprite = gemSp;
            priceRefreshText.gameObject.SetActive(true);
        }
        else
        {
            iconRefreshImg.sprite = videoSp;
            priceRefreshText.gameObject.SetActive(false);
        }

       // DataUtils.AddCoinAndGame(0, 7);
    }

    // Update is called once per frame
    void Update()
    {
        timeCount = 86400 - (System.DateTime.Now - DataParam.oldDateTime).TotalSeconds;
        if (timeCount <= 0)
        {
            timeText.text = "Refresh in: <color=green>" + "00:00:00" + "</color>";
            return;
        }
        timeSpanTemp = TimeSpan.FromSeconds(timeCount);
        timetemp = timeSpanTemp.ToString("hh':'mm':'ss");
        timeText.text = "Refresh in: <color=green>" + timetemp + "</color>";
    }
    public void BtnReset()
    {
        if (DataParam.countResetBlackMarket == 0)
        {
            SoundController.instance.PlaySound(soundGame.soundbtnclick);
#if UNITY_EDITOR
            Reward();
#else
        AdsManager.Instance.ShowRewardedVideo((b) => {if(b) Reward();});
#endif
        }
        else
        {
            Debug.LogError(DataUtils.playerInfo.gems + ":" + DataParam.countResetBlackMarket * 5);
            if (DataUtils.playerInfo.gems >= DataParam.countResetBlackMarket * 5)
            {
                DataUtils.AddCoinAndGame(0, -DataParam.countResetBlackMarket * 5);
                Reward();
                priceRefreshText.text = "" + DataParam.countResetBlackMarket * 5;
            }
            else
            {
                MainMenuController.Instance.shopManager.gameObject.SetActive(true);
            }
        }


    }
    void Reward()
    {
        iconRefreshImg.sprite = gemSp;
        DataController.instance.AddNewBlackMarket();
        DataParam.countResetBlackMarket++;
        for (int i = 0; i < bouders.Count; i++)
        {
            bouders[i].DisplayItem();
        }

        priceRefreshText.text = "" + DataParam.countResetBlackMarket * 5;
        priceRefreshText.gameObject.SetActive(true);

        Debug.LogError("zooooooooooooooooooooooooo " + DataParam.countResetBlackMarket);
    }
}
