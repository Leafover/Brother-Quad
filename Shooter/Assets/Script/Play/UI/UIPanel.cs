using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIPanel : MonoBehaviour
{
    public GameObject[] iconSkills;
    public GameObject rewardItemEquip, bouderDiamond, effectBtnSkill,lockSkillObj;
    public Image[] rewardItemEquipImg, bouderLevelItemEquipImg, skillFillAmount;
    public GameObject[] bouderRewardEquip;

    public AllBossAndMiniBossInfo allbossandminibossInfo;
    public Sprite nvSprite;
    public GameObject[] bouders, iconPartOfBouderReward;
    public Image[] rewardImg, bouderLevel, allImgHealthBtn;
    public Text[] rewardText;
    public Sprite[] rewardSp, levelSp;

    public Button btnReviveByGem;
    public List<Text> missionTexts;
    public GameObject winPanel, defeatPanel, leftwarning, rightwarning, btnReviveByAds, loadingPanel, takeDamgePanel, lowHealth, hackbouder;
    public Image grenadeFillAmout, fillbouderGrenade,fillbouderSKill;
    public Text levelText, bulletText, timeText, pricegemText, numberHealthPack;
    public TextMeshProUGUI myGemText;

    public Animator animGamOver;
    public HealthBarBoss healthBarBoss;
    public GameObject comboDisplay;
    public TextMeshProUGUI comboText, comboNumberText;

    public Slider slideMiniMap;
    public GameObject warning;
    public Image haveBossInMiniMap;

    public int pricesGemRevive;

    public void DisplayRewardEquipPanel()
    {
        if (GameController.instance.first && bouderRewardEquip[0].activeSelf)
            rewardItemEquip.SetActive(true);
    }

    public void DisplayBtnHealth(bool disable, int _total)
    {
        numberHealthPack.text = "" + _total;
        for (int i = 0; i < allImgHealthBtn.Length; i++)
        {
            if (disable)
                allImgHealthBtn[i].color = Color.gray;
            else
                allImgHealthBtn[i].color = Color.white;
        }
    }
    public bool CheckWarning()
    {
        return true ? warning.activeSelf : !warning.activeSelf;
    }

    public void CalculateMiniMap()
    {
        if (slideMiniMap.value == 1)
            return;
        slideMiniMap.value = (PlayerController.instance.transform.position.x - GameController.instance.currentMap.pointBeginPlayer.transform.position.x) / GameController.instance.currentMap.distanceMap;
    }



    public void Begin()
    {
        slideMiniMap.value = 0;
        if (GameController.instance.currentMap.haveBoss || GameController.instance.currentMap.haveMiniBoss)
        {
            if (GameController.instance.currentMap.haveMiniBoss)
                haveBossInMiniMap.sprite = allbossandminibossInfo.infos[DataParam.indexStage].icons[0];
            else if (GameController.instance.currentMap.haveBoss)
                haveBossInMiniMap.sprite = allbossandminibossInfo.infos[DataParam.indexStage].icons[1];
            haveBossInMiniMap.gameObject.SetActive(true);
        }

        DisplayBtnHealth(true, DataUtils.playerInfo.healthPack);

       // hackbouder.SetActive(DataController.instance.isHack);

        Debug.LogError("hero level : " + DataUtils.heroInfo.level);

        iconSkills[GameController.instance.currentChar].gameObject.SetActive(true);
        if (DataUtils.heroInfo.level >= 1)
        {
            lockSkillObj.SetActive(false);
            effectBtnSkill.SetActive(true);
            Debug.LogError("wtf display bntn skill");
        }
        else
        {
            effectBtnSkill.SetActive(false);
            lockSkillObj.SetActive(true);
        }
    }
    void OutPlay(int nextscene, bool showstarterpack)
    {
        GameController.instance.StopAll();
        SoundController.instance.PlaySound(soundGame.soundbtnclick);
        DataParam.nextSceneAfterLoad = nextscene;
        PlayerController.instance.EndEvent();
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);

        if (showstarterpack)
        {
            if (DataParam.indexMap >= GameController.instance.listMaps[DataParam.indexStage].listMap.Count - 1)
            {
                DataParam.showstarterpack = true;
            }
        }
    }
    public void BtnBackToWorld()
    {
        OutPlay(1, false);
        MyAnalytics.LogEventLoseLevel(DataParam.indexMap, CameraController.instance.currentCheckPoint, DataParam.indexStage);
    }
    public void BtnBackToWorldWin()
    {
        OutPlay(1, true);
    }
    public void BtnBack()
    {
        PopupSetting.Instance.ShowPanelSetting();
        SoundController.instance.PlaySound(soundGame.soundbtnclick);
    }
    public void FillGrenade(float _current, float _max)
    {
        fillbouderGrenade.fillAmount = grenadeFillAmout.fillAmount = _current / _max;
    }
    public void FillSkill(float _current, float _max)
    {
        if (lockSkillObj.activeSelf)
            return;

        fillbouderSKill.fillAmount = skillFillAmount[GameController.instance.currentChar].fillAmount = _current / _max;
        if(_current <= 0 && !effectBtnSkill.activeSelf)
        {
            effectBtnSkill.SetActive(true);
        }
    }
    public void BtnNext()
    {

        if (DataParam.indexMap < GameController.instance.listMaps[DataParam.indexStage].listMap.Count - 1)
        {
            DataParam.indexMap++;
            MissionController.Instance.AddMission();
            OutPlay(2, false);
        }
        else
        {
            OutPlay(1, false);
            DataParam.showstarterpack = true;
        }

    }
    public void DisplayDefeatNPCDIE()
    {
        btnReviveByAds.SetActive(false);
        btnReviveByGem.gameObject.SetActive(false);
        DataUtils.AddCoinAndGame((int)DataParam.totalCoin, 0);

        defeatPanel.SetActive(true);

        switch (GameController.instance.currentChar)
        {
            case 0:
                SoundController.instance.PlaySound(soundGame.soundlose);
                break;
            case 1:
                SoundController.instance.PlaySound(soundGame.soundlosenv2);
                break;
        }
    }
    public void DisplayDefeat()
    {
        if (defeatPanel.activeSelf)
            return;
        if (GameController.instance.reviveCount == 0)
        {
            myGemText.text = "Own: <color=green>" + DataUtils.playerInfo.gems + "</color>";
            pricegemText.text = "" + pricesGemRevive;
            btnReviveByAds.SetActive(true);

            if (DataUtils.playerInfo.gems >= pricesGemRevive)
            {
                btnReviveByGem.image.color = Color.white;
            }
            else
            {
                btnReviveByGem.image.color = Color.gray;
            }

            btnReviveByGem.gameObject.SetActive(true);

        }
        else
        {
            btnReviveByAds.SetActive(false);
            btnReviveByGem.gameObject.SetActive(false);
            DataUtils.AddCoinAndGame((int)DataParam.totalCoin, 0);
        }
        defeatPanel.SetActive(true);
        switch (GameController.instance.currentChar)
        {
            case 0:
                SoundController.instance.PlaySound(soundGame.soundlose);
                break;
            case 1:
                SoundController.instance.PlaySound(soundGame.soundlosenv2);
                break;
        }
    }
    public void DisplayFinish(int _countstar)
    {
        if (winPanel.activeSelf)
            return;
        missionTexts[0].text = DataController.instance.allMission[DataParam.indexStage].missionData[DataParam.indexMap].mission1name;

        if (MissionController.Instance.listMissions[0].isDone && MissionController.Instance.listMissions[1].isDone)
        {
            missionTexts[1].text = DataController.instance.allMission[DataParam.indexStage].missionData[DataParam.indexMap].mission2name;
            missionTexts[2].text = DataController.instance.allMission[DataParam.indexStage].missionData[DataParam.indexMap].mission3name;
            //  Debug.LogError("TH1");
        }
        else if (MissionController.Instance.listMissions[0].isDone && !MissionController.Instance.listMissions[1].isDone)
        {
            missionTexts[1].text = DataController.instance.allMission[DataParam.indexStage].missionData[DataParam.indexMap].mission2name;
            missionTexts[2].text = DataController.instance.allMission[DataParam.indexStage].missionData[DataParam.indexMap].mission3name;
            //  Debug.LogError("TH2");
        }
        else if (!MissionController.Instance.listMissions[0].isDone && MissionController.Instance.listMissions[1].isDone)
        {
            missionTexts[1].text = DataController.instance.allMission[DataParam.indexStage].missionData[DataParam.indexMap].mission3name;
            missionTexts[2].text = DataController.instance.allMission[DataParam.indexStage].missionData[DataParam.indexMap].mission2name;
            //  Debug.LogError("TH3");
        }
        else if (!MissionController.Instance.listMissions[0].isDone && !MissionController.Instance.listMissions[1].isDone)
        {
            missionTexts[1].text = DataController.instance.allMission[DataParam.indexStage].missionData[DataParam.indexMap].mission2name;
            missionTexts[2].text = DataController.instance.allMission[DataParam.indexStage].missionData[DataParam.indexMap].mission3name;
            //   Debug.LogError("TH4");
        }

        winPanel.SetActive(true);
        switch (_countstar)
        {
            case 1:
                animGamOver.Play("Win1Star");
                break;
            case 2:

                animGamOver.Play("Win2Star");
                break;
            case 3:

                animGamOver.Play("Win3Star");
                break;
        }
#if UNITY_EDITOR

#else
        randomAds = Random.Range(0, 100);
        if (randomAds < 60)
        {
            AdsManager.Instance.ShowInterstitial((b) => { });
        }
#endif
    }
    int randomAds;
    public void BtnReviveByGem()
    {
        SoundController.instance.PlaySound(soundGame.soundbtnclick);
        if (DataUtils.playerInfo.gems >= pricesGemRevive)
        {
            Reward(80);
            DataUtils.AddCoinAndGame(0, -pricesGemRevive);
        }
    }
    public void BtnRevive()
    {
        SoundController.instance.PlaySound(soundGame.soundbtnclick);
#if UNITY_EDITOR

        Reward(50);
#else
        AdsManager.Instance.ShowRewardedVideo((b) => {if(b) Reward(50);});
#endif
    }
    void Reward(int healthBonus)
    {
        PlayerController.instance.Revive(healthBonus);
        defeatPanel.SetActive(false);
        GameController.instance.gameState = GameController.GameState.play;
    }


    public void HackGun(int index)
    {
        PlayerController.instance.SetGun(index);
    }
}
