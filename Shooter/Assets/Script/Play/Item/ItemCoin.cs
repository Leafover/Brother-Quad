using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCoin : ItemBase
{
    public override void Hit()
    {
        base.Hit();
        DataParam.AddCoin(1);
        SoundController.instance.PlaySound(soundGame.soundEatCoin);
    }
    public override void OnEnable()
    {
        base.OnEnable();
        rid.velocity = new Vector2(Random.Range(-2.5f, 2.5f), 3);   
    }
}
