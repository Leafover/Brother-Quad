﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{

    public void EventDisplayStar(int i)
    {
        switch (i)
        {
            case 0:
                SoundController.instance.PlaySound(soundGame.soundstar1);
                break;
            case 1:
                SoundController.instance.PlaySound(soundGame.soundstar2);
                break;
            case 2:
                SoundController.instance.PlaySound(soundGame.soundstar3);
                break;
        }

    }
    public void ShowAds()
    {
        GameController.instance.uiPanel.DisplayRewardEquipPanel();
    }
    public void Exit()
    {
        GameController.instance.uiPanel.rewardItemEquip.SetActive(false);
    }
    public void Equip()
    {
        GameController.instance.Equip();
    }

}
