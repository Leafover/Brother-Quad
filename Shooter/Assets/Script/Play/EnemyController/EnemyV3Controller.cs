using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyV3Controller : EnemyBase
{

    private void Start()
    {
        base.Start();
        if (!EnemyManager.instance.enemyv3s.Contains(this))
        {
            EnemyManager.instance.enemyv3s.Add(this);
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (!isActive)
        {
            if (transform.position.x - Camera.main.transform.position.x <= distanceActive)
            {
                isActive = true;
                rid.velocity = new Vector2(-speed, rid.velocity.y);
            }
            return;
        }

        Boom();
    }
    void Boom()
    {
        if (Time.time - timePreviousAttack >= maxtimeDelayAttack)
        {
            timePreviousAttack = Time.time;
            GameObject g = ObjectPoolerManager.Instance.boomEnemyV3Pooler.GetPooledObject();
            g.transform.position = gameObject.transform.position;
            g.SetActive(true);
        }
    }
    private void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.enemyv3s.Contains(this))
        {
            EnemyManager.instance.enemyv3s.Remove(this);
        }
    }
}
