using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM6Controller : EnemyBase
{
    public override void Start()
    {
        base.Start();
        Init();
    }
    public override void Init()
    {
        base.Init();
        if (!EnemyManager.instance.em6s.Contains(this))
        {
            EnemyManager.instance.em6s.Add(this);
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
        if (EnemyManager.instance.em6s.Contains(this))
        {
            EnemyManager.instance.em6s.Remove(this);
        }
    }

    public override void Dead()
    {
        base.Dead();
    }
}
