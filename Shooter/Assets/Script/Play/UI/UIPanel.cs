using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIPanel : MonoBehaviour
{
    public GameObject finishPanel;
    public GameObject ResetBtn, NextBtn;
    public Image grenadeFillAmout, fillbouderGrenade;
    public Text levelText, gameoverText, bulletText, timeText;
    public GameObject starbouder;
    public List<GameObject> starCount;
    public Animator animGamOver;
    public HealthBarBoss healthBarBoss;
    public GameObject comboDisplay;
    public TextMeshProUGUI comboText, comboNumberText;
    public void BtnReset()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    public void DisplayStar(int total)
    {
        for (int i = 0; i < total; i++)
        {
            starCount[i].SetActive(true);
        }

        starbouder.SetActive(true);
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

        if (finishPanel.activeSelf)
            return;
        finishPanel.SetActive(true);
        if (GameController.instance.win)
        {
            starbouder.SetActive(true);
            ResetBtn.SetActive(true);
            NextBtn.SetActive(true);
            gameoverText.text = "WIN";
            //   SoundController.instance.PlaySound(soundGame.soundwin);
            switch (_countstar)
            {
                case 1:

                    animGamOver.Play("GameOver1star");
                    //   Debug.LogError("zo 1");
                    break;
                case 2:

                    animGamOver.Play("Gameover2star");
                    //  Debug.LogError("zo 2");
                    break;
                case 3:

                    animGamOver.Play("GameOver3star");
                    //  Debug.LogError("zo 3");
                    break;
            }
            //Debug.Log("count star win:" + _countstar);
            //animGamOver.SetInteger("star", _countstar);
        }
        else
        {
            ResetBtn.SetActive(true);
            NextBtn.SetActive(false);
            gameoverText.text = "DIE";
            //  Debug.Log("count star die:" + _countstar);
            animGamOver.Play("GameOverDie");
            SoundController.instance.PlaySound(soundGame.soundlose);
        }




    }
}
