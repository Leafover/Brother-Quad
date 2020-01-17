using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIPanel : MonoBehaviour
{
    public List<Text> missionTexts;
    public GameObject winPanel, defeatPanel, leftwarning, rightwarning,btnRevive,lowHealth;
    public Image grenadeFillAmout, fillbouderGrenade;
    public Text levelText, bulletText, timeText;

    public Animator animGamOver;
    public HealthBarBoss healthBarBoss;
    public GameObject comboDisplay;
    public TextMeshProUGUI comboText, comboNumberText;

    public Slider slideMiniMap;
    public GameObject haveBossInMiniMap, warning;



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
            haveBossInMiniMap.SetActive(true);
    }
    public void BtnBackToWorld()
    {
        SoundController.instance.PlaySound(soundGame.soundbtnclick);
        DataParam.nextSceneAfterLoad = 1;
        MyAnalytics.LogEventLoseLevel(DataParam.indexMap, CameraController.instance.currentCheckPoint, DataParam.indexStage);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }

    public void BtnBack()
    {
        PopupSetting.Instance.ShowPanelSetting();
        SoundController.instance.PlaySound(soundGame.soundbtnclick);
        //DataParam.nextSceneAfterLoad = 0;
        //Application.LoadLevel(1);
    }
    public void FillGrenade(float _current, float _max)
    {
        fillbouderGrenade.fillAmount = grenadeFillAmout.fillAmount = _current / _max;
    }
    public void BtnNext()
    {
        GameController.instance.StopAll();
        if (DataParam.indexMap < GameController.instance.listMaps[DataParam.indexStage].listMap.Count - 1)
        {
            DataParam.indexMap++;
            MissionController.Instance.AddMission();
            DataParam.nextSceneAfterLoad = 2;
        }
        else
        {
            DataParam.nextSceneAfterLoad = 1;
        }
        SoundController.instance.PlaySound(soundGame.soundbtnclick);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }
    public void DisplayFinish(int _countstar)
    {
        if (GameController.instance.win)
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

            GameController.instance.ThemManh();
        }
        else
        {
            if (defeatPanel.activeSelf)
                return;
            if (GameController.instance.reviveCount == 0)
                btnRevive.SetActive(true);
            else
            {
                btnRevive.SetActive(false);
                DataUtils.AddCoinAndGame((int)DataParam.totalCoin, 0);
            }
            defeatPanel.SetActive(true);
            SoundController.instance.PlaySound(soundGame.soundlose);
        }

    }
    public void BtnRevive()
    {
        PlayerController.instance.Revive();
        defeatPanel.SetActive(false);
        GameController.instance.gameState = GameController.GameState.play;
        SoundController.instance.PlaySound(soundGame.soundbtnclick);
    }
}
