using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TS3Controller : EnemyBase
{
    public override void Start()
    {
        base.Start();
        Init();
    }
    public override void Init()
    {
        base.Init();
        if (!EnemyManager.instance.ts3s.Contains(this))
        {
            EnemyManager.instance.ts3s.Add(this);
        }
    }
    public override void Active()
    {
        base.Active();
    }
    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);

        if (!isActive)
        {
            return;
        }
        if (enemyState == EnemyState.die)
            return;

    }

    public override void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.ts3s.Contains(this))
        {
            EnemyManager.instance.ts3s.Remove(this);
        }
    }

    public override void Dead()
    {
        base.Dead();
    }
}
