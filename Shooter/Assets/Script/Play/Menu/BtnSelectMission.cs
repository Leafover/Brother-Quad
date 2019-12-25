﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSelectMission : MonoBehaviour
{
    public int myStage;
    public void BtnSelectLevel()
    {
        SoundController.instance.PlaySound(soundGame.soundbtnclick);
        DataParam.indexStage = myStage;
        DataParam.indexMap = int.Parse(gameObject.name.Replace("Level ","")) - 1;
     //   Debug.LogError(DataParam.indexMap);
        DataParam.nextSceneAfterLoad = 2;
        Application.LoadLevel(1);
    }
}