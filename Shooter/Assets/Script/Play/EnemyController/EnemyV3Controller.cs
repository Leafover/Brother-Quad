using System.Collections;
using System.Collections.Generic;
using Spine;
using UnityEngine;
using Spine.Unity;
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
        timePreviousAttack = 0;
    }
    Vector2 move;
    public override void Active()
    {
        base.Active();
        move = rid.velocity;
        move.x = -speed;
        move.y = rid.velocity.y;
        rid.velocity = move;
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
        timePreviousAttack -= deltaTime;
        Boom();
    }
    void Boom()
    {


        if (timePreviousAttack <= 0)
        {
            timePreviousAttack = maxtimeDelayAttack;
            GameObject g = ObjectPoolerManager.Instance.boomEnemyV3Pooler.GetPooledObject();
            g.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
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
