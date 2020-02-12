using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM6Controller : EnemyBase
{
    float timedelayChangePos;
    Vector2 nextPos;
    public void Start()
    {
        base.Start();
        Init();
    }
    public override void Init()
    {
        base.Init();
        timedelayChangePos = maxtimedelayChangePos;
        if (!EnemyManager.instance.em6s.Contains(this))
        {
            EnemyManager.instance.em6s.Add(this);
        }
    }
    public override void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.em6s.Contains(this))
        {
            EnemyManager.instance.em6s.Remove(this);
        }
    }
    public override void Active()
    {
        base.Active();
        enemyState = EnemyState.attack;
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
        if (tempXBegin > Camera.main.transform.position.x + 8f)
        {
            return;
        }
        switch (enemyState)
        {
            case EnemyState.attack:
                Attack(0, aec.attack1, false, maxtimeDelayAttack1);
                CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());
                if (!canmove)
                    return;

                timedelayChangePos -= deltaTime;
                if (timedelayChangePos <= 0)
                {
                    enemyState = EnemyState.run;
                    timedelayChangePos = maxtimedelayChangePos;
                    if (transform.position.x < PosBegin.x)
                    {
                        nextPos.x = PosBegin.x + 0.5f;
                        PlayAnim(0, aec.run2, true);
                    }
                    else
                    {
                        nextPos.x = PosBegin.x + -0.5f;
                        PlayAnim(0, aec.run, true);
                    }

                    //  Debug.Log(nextPos + ":" + OriginPos);
                }

                break;
            case EnemyState.run:
                nextPos.y = PosBegin.y;
                transform.position = Vector2.MoveTowards(transform.position, nextPos, deltaTime * speed);
                CheckDirFollowPlayer(nextPos.x);
                if (transform.position.x == nextPos.x /*&& transform.position.y == nextPos.y*/)
                {
                    //  OriginPos = nextPos;
                    PlayAnim(0, aec.idle, true);
                    enemyState = EnemyState.attack;
                    // Debug.LogError("zo day");
                }
                break;
        }
    }

    //GameObject bullet;
    Vector3 rotationbullet = new Vector3(0, 0, 0);
    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);

        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            if (!incam)
                return;
            bulletEnemy = ObjectPoolManagerHaveScript.Instance.bulletEnemyV1Pooler.GetBulletEnemyPooledObject();
            bulletEnemy.AddProperties(damage1, 0);
            if (FlipX)
            {
                bulletEnemy.SetDir(-bulletspeed1, false);
            }
            else
            {
                bulletEnemy.SetDir(bulletspeed1, false);
            }
            bulletEnemy.rid.gravityScale = 0;
            bulletEnemy.isGrenade = false;
            bulletEnemy.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            bulletEnemy.transform.eulerAngles = rotationbullet;
            bulletEnemy.gameObject.SetActive(true);

            SoundController.instance.PlaySound(soundGame.soundv1attack);
        }
    }
    protected override void OnComplete(TrackEntry trackEntry)
    {
        base.OnComplete(trackEntry);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            PlayAnim(0, aec.idle, true);
        }
    }
    public override void Dead()
    {
        base.Dead();
    }
}
