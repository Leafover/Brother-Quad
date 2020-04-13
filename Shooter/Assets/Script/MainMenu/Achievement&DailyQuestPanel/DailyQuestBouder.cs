using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DailyQuestBouder : MonoBehaviour
{
    public Text processText, rewardText, expText, desText,specialDesText;
    public Image rewardImg, processImg,btnClaimImg;
    public GameObject /*btnClaim,*/ doneObj,rewardObj,processBar;
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
        processImg.fillAmount = (float)DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].currentNumber / DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].Soluong;
        processText.text = DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].currentNumber + "/" + DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].Soluong;
        rewardText.text = "" + DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].SoLuongRewards.ToString("#,0");
        expText.text = "" + DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].EXP;
        rewardImg.sprite = MenuController.instance.achievementAndDailyQuestPanel.rewardSps[DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].RewardsType - 1];
        if (!DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].isDone)
        {
            if (DataController.allSaveDailyQuest[DataController.saveIndexQuest[index]].isPass)
            {
                transform.SetAsFirstSibling();
                btnClaimImg.sprite = MenuController.instance.achievementAndDailyQuestPanel.btnClaim[1];
            }
            else
            {
                btnClaimImg.sprite = MenuController.instance.achievementAndDailyQuestPanel.btnClaim[0];


            }
            doneObj.SetActive(false);
            btnClaimImg.gameObject.SetActive(true);
            rewardObj.SetActive(true);
        }
        else
        {

            transform.SetAsLastSibling();
            rewardObj.SetActive(false);
            btnClaimImg.gameObject.SetActive(false);
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
        if (btnClaimImg.sprite == MenuController.instance.achievementAndDailyQuestPanel.btnClaim[0])
            return;
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
        MenuController.instance.warningDailyQuest.SetActive(false);
        if (DataParam.countdonedailyquest == DataController.saveIndexQuest.Count)
        {
            DataController.instance.DoAchievement(12, 1);
            return;
        }
        DataController.instance.CheckDoneAllDailyQuest();

    }
}
