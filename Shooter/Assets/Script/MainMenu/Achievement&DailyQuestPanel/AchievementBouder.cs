using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AchievementBouder : MonoBehaviour
{
    public Text processText, rewardText, expText, desText, nameText;
    public Image rewardImg, processImg, btnClaimImg;
    public GameObject /*btnClaim,*/ doneObj/*, processObj*/,rewardObj;
    public int index = -1;

    public void DisplayStart()
    {
        if (index != -1)
            return;
        index = int.Parse(gameObject.name) - 1;
        _temp = DataController.instance.allAchievement[index].NoiDung;
        if (_temp.Contains("xx"))
        {
           // Debug.LogError("vao` dieu kien");
            desTemp = _temp.Replace("xx", "" + DataController.instance.allAchievement[index].maxNumber[DataController.saveAllAchievement[index].currentLevel - 1]);
        }
        else
            desTemp = DataController.instance.allAchievement[index].NoiDung;
        desText.text = desTemp;
        nameText.text = DataController.instance.allAchievement[index].Name;


    }

    string desTemp, _temp;
    public void DisplayMe()
    {
        DisplayStart();

        processImg.fillAmount = (float)DataController.saveAllAchievement[index].currentNumber / DataController.instance.allAchievement[index].maxNumber[DataController.saveAllAchievement[index].currentLevel - 1];
        processText.text = DataController.saveAllAchievement[index].currentNumber + "/" + DataController.instance.allAchievement[index].maxNumber[DataController.saveAllAchievement[index].currentLevel - 1];
        rewardText.text = "" + DataController.instance.allAchievement[index].maxNumberReward[DataController.saveAllAchievement[index].currentLevel - 1].ToString("#,0");
        expText.text = "" + DataController.instance.allAchievement[index].expReward[DataController.saveAllAchievement[index].currentLevel - 1];
        rewardImg.sprite = MenuController.instance.achievementAndDailyQuestPanel.rewardSps[DataController.instance.allAchievement[index].typeReward[DataController.saveAllAchievement[index].currentLevel - 1] - 1];

        if (!DataController.saveAllAchievement[index].isDone)
        {
            if (DataController.saveAllAchievement[index].isPass)
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
            btnClaimImg.gameObject.SetActive(false);
            rewardObj.SetActive(false);
            doneObj.SetActive(true);
        }
    }

    public void Claim()
    {
        if (btnClaimImg.sprite == MenuController.instance.achievementAndDailyQuestPanel.btnClaim[0])
            return;
        switch (DataController.instance.allAchievement[index].typeReward[DataController.saveAllAchievement[index].currentLevel - 1])
        {
            case 1:
                DataUtils.AddCoinAndGame(0, DataController.instance.allAchievement[index].maxNumberReward[DataController.saveAllAchievement[index].currentLevel - 1]);
                break;
            case 2:
                DataUtils.AddCoinAndGame(DataController.instance.allAchievement[index].maxNumberReward[DataController.saveAllAchievement[index].currentLevel - 1], 0);
                break;
            case 3:
                DataUtils.AddHPPack(DataController.instance.allAchievement[index].maxNumberReward[DataController.saveAllAchievement[index].currentLevel - 1]);
                break;
        }
        DataController.saveAllAchievement[index].isPass = false;


        if (DataController.saveAllAchievement[index].currentLevel >= DataController.instance.allAchievement[index].maxNumber.Count)
        {
          //  DataController.saveAllAchievement[index].currentLevel = DataController.instance.allAchievement[index].maxNumber.Count - 1;
            DataController.saveAllAchievement[index].isDone = true;

        }
        else
        {
            DataController.saveAllAchievement[index].currentLevel++;
            DataController.saveAllAchievement[index].currentNumber = 0;
            _temp = DataController.instance.allAchievement[index].NoiDung;
            if (_temp.Contains("xx"))
            {
                //  Debug.LogError("vao` dieu kien");
                desTemp = _temp.Replace("xx", "" + DataController.instance.allAchievement[index].maxNumber[DataController.saveAllAchievement[index].currentLevel - 1]);
            }
            else
                desTemp = DataController.instance.allAchievement[index].NoiDung;
            desText.text = desTemp;
        }
        Debug.LogError("LEVEL ACHI:" + DataController.saveAllAchievement[index].currentLevel);
        DisplayMe();
        MenuController.instance.warningAchievment.SetActive(false);
    }
}
