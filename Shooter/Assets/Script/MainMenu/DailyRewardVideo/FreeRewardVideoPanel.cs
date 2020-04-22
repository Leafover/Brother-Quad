using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class FreeReward
{
    public int type;
    public int numberReward;
}

public class FreeRewardVideoPanel : MonoBehaviour
{
    public List<FreeReward> freeReward = new List<FreeReward>();
    public Image[] btnvideo, iconvideo, iconReward;
    public Text[] watchText, rewardText;

    private void Start()
    {
        for (int i = 0; i < btnvideo.Length; i++)
        {
            iconReward[i].sprite = MenuController.instance.achievementAndDailyQuestPanel.rewardSps[freeReward[i].type];
            rewardText[i].text = "" + freeReward[i].numberReward.ToString("#,0");
            if (i < DataParam.indexRewardVideo)
            {
                btnvideo[i].color = iconvideo[i].color = watchText[i].color = Color.black;
                btnvideo[i].gameObject.SetActive(true);
            }
            else
            {
                btnvideo[i].color = iconvideo[i].color = watchText[i].color = Color.black;
                if (i == DataParam.indexRewardVideo)
                    btnvideo[i].gameObject.SetActive(true);
                else
                    btnvideo[i].gameObject.SetActive(false);
            }
        }
    }

    public void Click(int index)
    {
        if (btnvideo[index].color == Color.gray || !btnvideo[index].gameObject.activeSelf)
            return;

        SoundController.instance.PlaySound(soundGame.soundbtnclick);

#if UNITY_EDITOR

        Reward(index);
#else
        AdsManager.Instance.ShowRewardedVideo((b) =>
        {
            if (b)
            {
                Reward(index);
                MyAnalytics.LogFreeReward();
            }
        });
#endif

    }
    void Reward(int index)
    {
        switch (freeReward[index].type)
        {
            case 0:
                DataUtils.AddCoinAndGame(0, freeReward[index].numberReward);
                break;
            case 1:
                DataUtils.AddCoinAndGame(freeReward[index].numberReward, 0);
                break;
            case 2:
                DataUtils.AddHPPack(freeReward[index].numberReward);
                break;
        }

        DataParam.indexRewardVideo = (index + 1);
        btnvideo[index].color = iconvideo[index].color = watchText[index].color = Color.gray;

        if (DataParam.indexRewardVideo <= btnvideo.Length - 1)
        {
            btnvideo[DataParam.indexRewardVideo].gameObject.SetActive(true);
        }
        else
        {
            MenuController.instance.warningvideoreward.SetActive(false);
        }
    }

}
