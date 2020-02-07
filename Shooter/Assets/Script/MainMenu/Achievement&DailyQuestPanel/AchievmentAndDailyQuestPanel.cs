using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class AchievmentAndDailyQuestPanel : MonoBehaviour
{
    public Sprite[] rewardSps;
    public Sprite[] btntabSps;
    public Button[] btnChangeTabs;
    public GameObject[] Tabs;

    public TextMeshProUGUI timeText;

    public AchievementBouder[] achievementBouders;
    public DailyQuestBouder[] dailyquestBouders;
    public TimeSpan timeSpanTemp;
    public void DisPlayMe(int index)
    {
        ChangeTab(index);
        gameObject.SetActive(true);
    }
    string timetemp = "20:20:02";

    private void Update()
    {
        if (!Tabs[0].activeSelf)
            return;

        timeSpanTemp = TimeSpan.FromSeconds(86400 -(System.DateTime.Now - DataParam.oldDateTime).TotalSeconds);
        timetemp = timeSpanTemp.ToString("hh':'mm':'ss");
        timeText.text = "Refresh in: <color=green>" + timetemp + "</color>";
    }

    public void ChangeTab(int index)
    {
        if (Tabs[index].activeSelf)
            return;

        switch(index)
        {
            case 0:

                timeText.text = "Refresh in: <color=green>" + timetemp + "</color>";
                Debug.Log("============="+DataController.saveIndexQuest.Count);
                for (int i = 0; i < DataController.saveIndexQuest.Count; i++)
                {
                    dailyquestBouders[i].DisplayMe();
                }
                break;
            case 1:
                for (int i = 0; i < achievementBouders.Length; i++)
                {
                    achievementBouders[i].DisplayMe();
                }
                break;
        }

        Tabs[index].SetActive(true);
        btnChangeTabs[index].image.sprite = btntabSps[0];
        for(int i = 0; i < Tabs.Length; i++)
        {
            if (i != index)
            {
                Tabs[i].SetActive(false);
                btnChangeTabs[i].image.sprite = btntabSps[1];
            }
        }
    }
    public void CloseMe()
    {
        gameObject.SetActive(false);
    }
}
