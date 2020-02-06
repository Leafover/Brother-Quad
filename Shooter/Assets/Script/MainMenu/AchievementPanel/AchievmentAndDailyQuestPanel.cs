using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AchievmentAndDailyQuestPanel : MonoBehaviour
{
    public Sprite[] rewardSps;
    public Sprite[] btntabSps;
    public Button[] btnChangeTabs;
    public GameObject[] Tabs;
    public void DisPlayMe(int index)
    {
        ChangeTab(index);
        gameObject.SetActive(true);
    }
    public void ChangeTab(int index)
    {
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
}
