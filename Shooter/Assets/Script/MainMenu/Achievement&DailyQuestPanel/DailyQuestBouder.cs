using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DailyQuestBouder : MonoBehaviour
{
    public Text processText, rewardText, expText, desText;
    public Image rewardImg, processImg;
    public GameObject btnClaim, doneObj;
    public int index = -1;

    public void DisplayStart()
    {
        if (index != -1)
            return;
        index = int.Parse(gameObject.name) - 1;
        desText.text = "" + DataController.instance.allDailyQuest[DataController.allSaveDailyQuest[index].indexQuest].MissionContent;
    }

    public void DisplayMe()
    {

        DisplayStart();
        if (!DataController.allSaveDailyQuest[index].isDone)
        {
            if (DataController.allSaveDailyQuest[index].isPass)
            {
                btnClaim.SetActive(true);
                doneObj.SetActive(false);
            }
            else
            {
                processImg.fillAmount = (float)DataController.allSaveDailyQuest[index].currentNumber / DataController.instance.allDailyQuest[DataController.allSaveDailyQuest[index].indexQuest].Soluong;
                processText.text = DataController.allSaveDailyQuest[index].currentNumber + "/" + DataController.instance.allDailyQuest[DataController.allSaveDailyQuest[index].indexQuest].Soluong;
                rewardText.text = "" + DataController.instance.allDailyQuest[DataController.allSaveDailyQuest[index].indexQuest].SoLuongRewards;
                expText.text = "" + DataController.instance.allDailyQuest[DataController.allSaveDailyQuest[index].indexQuest].EXP;
                rewardImg.sprite = MenuController.instance.achievementAndDailyQuestPanel.rewardSps[DataController.instance.allDailyQuest[DataController.allSaveDailyQuest[index].indexQuest].RewardsType - 1];

                btnClaim.SetActive(false);
                doneObj.SetActive(false);
            }
        }
        else
        {
            btnClaim.SetActive(false);
            doneObj.SetActive(true);
        }

        gameObject.SetActive(true);
    }

    public void Claim()
    {
        switch (DataController.instance.allDailyQuest[DataController.allSaveDailyQuest[index].indexQuest].RewardsType)
        {
            case 1:
                DataUtils.AddCoinAndGame(0, DataController.instance.allDailyQuest[DataController.allSaveDailyQuest[index].indexQuest].SoLuongRewards);
                break;
            case 2:
                DataUtils.AddCoinAndGame(DataController.instance.allDailyQuest[DataController.allSaveDailyQuest[index].indexQuest].SoLuongRewards, 0);
                break;
        }
        DataController.allSaveDailyQuest[index].isPass = false;
        DataController.saveAllAchievement[index].isDone = true;
        DisplayMe();
    }
}
