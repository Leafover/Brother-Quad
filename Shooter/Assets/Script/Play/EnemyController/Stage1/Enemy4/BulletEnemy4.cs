using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy4 : BulletEnemy
{
    public override void Init(int type)
    {
        base.Init(type);
    }
    private void OnEnable()
    {
        Init(0);
    }
}
