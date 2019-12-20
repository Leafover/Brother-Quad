using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIPanel : MonoBehaviour
{
    public List<Text> missionTexts;
    public GameObject winPanel,defeatPanel;
    public Image grenadeFillAmout, fillbouderGrenade;
    public Text levelText, bulletText, timeText;

    public Animator animGamOver;
    public HealthBarBoss healthBarBoss;
    public GameObject comboDisplay;
    public TextMeshProUGUI comboText, comboNumberText;

    public Slider slideMiniMap;
    public GameObject haveBossInMiniMap;
    public void CalculateMiniMap()
    {
        if (slideMiniMap.value == 1)
            return;
        slideMiniMap.value = (PlayerController.instance.transform.position.x - GameController.instance.currentMap.pointBeginPlayer.transform.position.x)  / GameController.instance.currentMap.distanceMap;
    }
    public void Begin()
    {
        slideMiniMap.value = 0;
        if (GameController.instance.currentMap.haveBoss || GameController.instance.currentMap.haveMiniBoss)
            haveBossInMiniMap.SetActive(true);
    }
    public void BtnReset()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void BtnBack()
    {
        DataParam.nextSceneAfterLoad = 0;
        Application.LoadLevel(1);
    }
    public void FillGrenade(float _current, float _max)
    {
        fillbouderGrenade.fillAmount = grenadeFillAmout.fillAmount = _current / _max;
    }
    public void BtnNext()
    {
        if (DataParam.indexMap < GameController.instance.listMap.Count - 1)
            DataParam.indexMap++;
        Application.LoadLevel(Application.loadedLevel);
    }
    public void DisplayFinish(int _countstar)
    {
        if (GameController.instance.win)
        {
            missionTexts[0].text = DataController.instance.missions[DataParam.indexMap].mission1name;
            missionTexts[1].text = DataController.instance.missions[DataParam.indexMap].mission2name;
            missionTexts[2].text = DataController.instance.missions[DataParam.indexMap].mission3name;
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
        }
        else
        {
            defeatPanel.SetActive(true);
            SoundController.instance.PlaySound(soundGame.soundlose);
        }

    }
}
