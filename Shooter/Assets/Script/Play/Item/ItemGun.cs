﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGun : ItemBase
{


    private void OnEnable()
    {
        if (GameController.instance == null)
            return;
        render.sprite = GameController.instance.gunSprite[index];
      //  Debug.Log("indexxxxxx" + index);
    }

    public override void Hit()
    {
        base.Hit();
        PlayerController.instance.SetGun(index);
        SoundController.instance.PlaySound(soundGame.soundChangeGun);
    }
    public override void CalculateDisable(float deltaTime)
    {
        base.CalculateDisable(deltaTime);
        if (transform.position.x < CameraController.instance.NumericBoundaries.LeftBoundary)
        {
            gameObject.SetActive(false);
        }
    }
}
