﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class AchievmentAndDailyQuestPanel : MonoBehaviour
{
    public Sprite[] rewardSps;
    public Sprite[] btntabSps, btnClaim;
    public Button[] btnChangeTabs;
    public GameObject[] Tabs;

    public TextMeshProUGUI timeText;

    public AchievementBouder[] achievementBouders;
    public DailyQuestBouder[] dailyquestBouders;
    public TimeSpan timeSpanTemp;

    public ScrollRect[] sc;

    public void DisPlayMe(int index)
    {
        //MenuController.instance.warningEvent.SetActive(false);
        ChangeTab(index);
        gameObject.SetActive(true);
        if (index == 0 || index == 1)
            sc[index].verticalNormalizedPosition = 1;
        Debug.LogError("wtf");
    }
    string timetemp = "20:20:02";
    double timeCount;
    private void Update()
    {
        if (!Tabs[0].activeSelf)
            return;

        timeCount = 86400 - (System.DateTime.Now - DataParam.oldDateTime).TotalSeconds;

        if (timeCount <= 0)
        {
            timeText.text = "Refresh in: <color=yellow>" + "00:00:00" + "</color>";
            return;
        }
        timeSpanTemp = TimeSpan.FromSeconds(timeCount);
        timetemp = timeSpanTemp.ToString("hh':'mm':'ss");
        timeText.text = "Refresh in: <color=yellow>" + timetemp + "</color>";
    }

    public void ChangeTab(int index)
    {
        if (Tabs[index].activeSelf)
            return;

        switch (index)
        {
            case 0:
                //MenuController.instance.warningDailyQuest.SetActive(false);
                timeText.text = "Refresh in: <color=yellow>" + timetemp + "</color>";
                Debug.Log("=============" + DataController.saveIndexQuest.Count);
                for (int i = 0; i < DataController.saveIndexQuest.Count; i++)
                {
                    dailyquestBouders[i].DisplayMe();
                }
                break;
            case 1:
                //MenuController.instance.warningAchievment.SetActive(false);
                for (int i = 0; i < achievementBouders.Length; i++)
                {
                    achievementBouders[i].DisplayMe();
                }
                break;
        }
        SoundController.instance.PlaySound(soundGame.soundbtnclick);
        Tabs[index].SetActive(true);
        btnChangeTabs[index].image.sprite = btntabSps[0];
        for (int i = 0; i < Tabs.Length; i++)
        {
            if (i != index)
            {
                Tabs[i].SetActive(false);
                btnChangeTabs[i].image.sprite = btntabSps[1];
            }
        }

        if (DataController.saveIndexQuest.Count == 3)
        {
            timeText.rectTransform.localPosition = pos3;
            Debug.LogError("TH1");
        }
        else
        {
            timeText.rectTransform.localPosition = pos5;
            Debug.LogError("TH2");
        }

        if (index == 0 || index == 1)
            sc[index].verticalNormalizedPosition = 1;
    }
    Vector3 pos5 = new Vector3(0, 201, 0);
    Vector3 pos3 = new Vector3(0, 186, 0);
    public void CloseMe()
    {
        SoundController.instance.PlaySound(soundGame.soundbtnclick);
        for (int i = 0; i < Tabs.Length; i++)
        {
            Tabs[i].SetActive(false);
        }
        gameObject.SetActive(false);

        DataController.instance.LoadAgainQuestAndBlackMarket();

        MenuController.instance.DisplayWarning();

    }
}
