using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AchievementBouder : MonoBehaviour
{
    public Text processText, rewardText, expText, desText, nameText;
    public Image rewardImg, processImg;
    public GameObject btnClaim, doneObj, processObj;
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
        if (!DataController.saveAllAchievement[index].isDone)
        {
            if (DataController.saveAllAchievement[index].isPass)
            {
                transform.SetAsFirstSibling();
                btnClaim.SetActive(true);
                processObj.SetActive(false);
                doneObj.SetActive(false);
            }
            else
            {
                processImg.fillAmount = (float)DataController.saveAllAchievement[index].currentNumber / DataController.instance.allAchievement[index].maxNumber[DataController.saveAllAchievement[index].currentLevel - 1];
                processText.text = DataController.saveAllAchievement[index].currentNumber + "/" + DataController.instance.allAchievement[index].maxNumber[DataController.saveAllAchievement[index].currentLevel - 1];
                rewardText.text = "" + DataController.instance.allAchievement[index].maxNumberReward[DataController.saveAllAchievement[index].currentLevel - 1];
                expText.text = "" + DataController.instance.allAchievement[index].expReward[DataController.saveAllAchievement[index].currentLevel - 1];
                rewardImg.sprite = MenuController.instance.achievementAndDailyQuestPanel.rewardSps[DataController.instance.allAchievement[index].typeReward[DataController.saveAllAchievement[index].currentLevel - 1] - 1];

                btnClaim.SetActive(false);
                processObj.SetActive(true);
                doneObj.SetActive(false);
            }
        }
        else
        {
            transform.SetAsLastSibling();
            btnClaim.SetActive(false);
            processObj.SetActive(false);
            doneObj.SetActive(true);
        }
    }

    public void Claim()
    {
        switch (DataController.instance.allAchievement[index].typeReward[DataController.saveAllAchievement[index].currentLevel - 1])
        {
            case 1:
                DataUtils.AddCoinAndGame(0, DataController.instance.allAchievement[index].maxNumberReward[DataController.saveAllAchievement[index].currentLevel - 1]);
                break;
            case 2:
                DataUtils.AddCoinAndGame(DataController.instance.allAchievement[index].maxNumberReward[DataController.saveAllAchievement[index].currentLevel - 1], 0);
                break;
        }
        DataController.saveAllAchievement[index].isPass = false;
        DataController.saveAllAchievement[index].currentNumber = 0;
        DataController.saveAllAchievement[index].currentLevel++;
        if (DataController.saveAllAchievement[index].currentLevel >= DataController.instance.allAchievement[index].maxNumber.Count)
            DataController.saveAllAchievement[index].isDone = true;
        else
        {
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
        DisplayMe();
        MenuController.instance.warningAchievment.SetActive(false);
    }
}
