using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealth : ItemBase
{

    public override void Hit()
    {
        base.Hit();
        PlayerController.instance.AddHealth(PlayerController.instance.maxHealth / 100 * numberTemp);
    }
}
