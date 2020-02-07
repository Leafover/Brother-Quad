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
        desText.text = "" + DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].MissionContent;
    }

    public void DisplayMe()
    {

        DisplayStart();
        if (!DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].isDone)
        {
            if (DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].isPass)
            {
                btnClaim.SetActive(true);
                doneObj.SetActive(false);
            }
            else
            {
                processImg.fillAmount = (float)DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].currentNumber / DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].Soluong;
                processText.text = DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].currentNumber + "/" + DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].Soluong;
                rewardText.text = "" + DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].SoLuongRewards;
                expText.text = "" + DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].EXP;
                rewardImg.sprite = MenuController.instance.achievementAndDailyQuestPanel.rewardSps[DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].RewardsType - 1];

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
        switch (DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].RewardsType)
        {
            case 1:
                DataUtils.AddCoinAndGame(0, DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].SoLuongRewards);
                break;
            case 2:
                DataUtils.AddCoinAndGame(DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].SoLuongRewards, 0);
                break;
        }
        DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].isPass = false;
        DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].isDone = true;
        DisplayMe();
    }
}
