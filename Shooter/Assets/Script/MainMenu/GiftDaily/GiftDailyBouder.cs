﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GiftDailyBouder : MonoBehaviour
{
    public Text rewardText, datyText;
    public Image iconImg, bouderLevel;
    public int index;
    public GameObject doneObj;
    public void Display(bool firsttime)
    {
        datyText.text = "Day" + (index + 1);
        if (index == 0 || index == 3 || index == 4)
        {
            Debug.LogError("nameReward:" + DataController.giftDaily[index].nameReward);
            rewardText.text = "" + DataController.giftDaily[index].numberReward;
        }
        else
        {
            Debug.LogError("nameReward:" + DataController.giftDaily[index].nameReward);
            rewardText.text = "x" + DataController.giftDaily[index].numberReward;
        }


        if (index == 2 || index == 5)
        {
            Debug.LogError("nameReward:" + DataController.giftDaily[index].nameReward);
            if (firsttime)
            {
                bouderLevel.sprite = MenuController.instance.blackMarketpanel.levelSp[2];
            }
            else
            {
                bouderLevel.sprite = MenuController.instance.blackMarketpanel.levelSp[4];
            }
            iconImg.sprite = DataUtils.dicSpriteData[DataController.giftDaily[index].nameReward];
        }
        if (index == 1)
        {
            Debug.LogError("nameReward:" + DataController.giftDaily[index].nameReward);
            iconImg.sprite = DataUtils.dicSpriteData[DataController.giftDaily[index].nameReward];
        }
        doneObj.SetActive(DataController.giftDaily[index].isDone);
    }
}
