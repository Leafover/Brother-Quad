using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
 public void BtnSelectLevel(int i)
    {
        SoundController.instance.PlaySound(soundGame.soundbtnclick);
        DataParam.indexMap = i;
        DataParam.nextSceneAfterLoad = 2;
        Application.LoadLevel(1);
    }
}
