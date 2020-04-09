using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DailyQuestBouder : MonoBehaviour
{
    public Text processText, rewardText, expText, desText,specialDesText;
    public Image rewardImg, processImg;
    public GameObject btnClaim, doneObj,rewardObj,processBar;
    public int index = -1;

    public void DisplayStart()
    {
        if (index == -1)
        {
            index = int.Parse(gameObject.name) - 1;
        }
        desText.text = "" + DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].MissionContent;
    }
    public void DisplayMe()
    {
        DisplayStart();
        if (!DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].isDone)
        {
            processImg.fillAmount = (float)DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].currentNumber / DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].Soluong;
            processText.text = DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].currentNumber + "/" + DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].Soluong;
            rewardText.text = "" + DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].SoLuongRewards.ToString("#,0");
            expText.text = "" + DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].EXP;
            rewardImg.sprite = MenuController.instance.achievementAndDailyQuestPanel.rewardSps[DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].RewardsType - 1];
            if (DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].isPass)
            {
                transform.SetAsFirstSibling();
                btnClaim.SetActive(true);
                doneObj.SetActive(false);
                rewardObj.SetActive(false);
            }
            else
            {
                btnClaim.SetActive(false);
                doneObj.SetActive(false);
                rewardObj.SetActive(true);
            }
        }
        else
        {
            processImg.fillAmount = (float)DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].currentNumber / DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].Soluong;
            processText.text = DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].currentNumber + "/" + DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].Soluong;
            transform.SetAsLastSibling();
            rewardObj.SetActive(false);
            btnClaim.SetActive(false);
            doneObj.SetActive(true);
        }

        if (DataController.saveIndexQuest[index] == 10)
        {
            desText.gameObject.SetActive(false);
            processBar.SetActive(false);
            specialDesText.gameObject.SetActive(true);
        }
        else
        {
            desText.gameObject.SetActive(true);
            processBar.SetActive(true);
            specialDesText.gameObject.SetActive(false);
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
            case 3:
                DataUtils.AddHPPack(DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].SoLuongRewards);
                break;
        }
        DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].isPass = false;
        DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].isDone = true;
        DisplayMe();
        DataParam.countdonedailyquest++;
        if (DataParam.countdonedailyquest == DataController.saveIndexQuest.Count)
        {
            DataController.instance.DoAchievement(12, 1);
            return;
        }
        DataController.instance.CheckDoneAllDailyQuest();
        MenuController.instance.warningDailyQuest.SetActive(false);
    }
}
