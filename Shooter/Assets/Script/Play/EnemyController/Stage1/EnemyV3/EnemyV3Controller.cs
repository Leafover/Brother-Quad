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
    // GameObject g;
    void Boom()
    {
        if (timePreviousAttack <= 0)
        {
            timePreviousAttack = maxtimeDelayAttack1;
            bulletEnemy = ObjectPoolManagerHaveScript.Instance.boomEnemyV3Pooler.GetBulletEnemyPooledObject();
            bulletEnemy.AddProperties(damage1, bulletspeed1);
            bulletEnemy.SetGravity(bulletspeed1);
            bulletEnemy.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            bulletEnemy.gameObject.SetActive(true);

            SoundController.instance.PlaySound(soundGame.soundev3dropbomb);
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.enemyv3s.Contains(this))
        {
            EnemyManager.instance.enemyv3s.Remove(this);
        }
    }

}
