using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealth : ItemBase
{

    public override void Hit()
    {
        base.Hit();
        PlayerController.instance.AddHealth(PlayerController.instance.maxHealth / 100 * (numberTemp + PlayerController.instance.healthRateBonus));
        SoundController.instance.PlaySound(soundGame.soundEatHP);
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
