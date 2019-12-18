using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCoin : ItemBase
{
    public override void Hit()
    {
        base.Hit();
        DataParam.AddCoin(1);
    }
}
