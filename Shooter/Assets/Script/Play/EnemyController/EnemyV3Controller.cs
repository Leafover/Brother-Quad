using System.Collections;
using System.Collections.Generic;
using Spine;
using UnityEngine;

public class EnemyV3Controller : EnemyBase
{
    public override void Start()
    {
        base.Start();
        Init();
    }
    public override void Init()
    {
        base.Init();
        if (!EnemyManager.instance.enemyv3s.Contains(this))
        {
            EnemyManager.instance.enemyv3s.Add(this);
        }
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        if (enemyState == EnemyState.die)
            return;

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
    //protected override void OnComplete(TrackEntry trackEntry)
    //{
    //    base.OnComplete(trackEntry);
    //    //if (trackEntry.Animation.Name.Equals(aec.die.name))
    //    //{
    //    //    gameObject.SetActive(false);
    //    //}
    //}
    public override void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.enemyv3s.Contains(this))
        {
            EnemyManager.instance.enemyv3s.Remove(this);
        }
    }
}
