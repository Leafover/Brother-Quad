using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class Enemy3Controller : EnemyBase
{
    float timedelayChangePos;
    Vector2 nextPos;
    public override void Start()
    {
        base.Start();
        Init();
    }
    public override void Init()
    {
        base.Init();
        timedelayChangePos = maxtimedelayChangePos;
        if (!EnemyManager.instance.enemy3s.Contains(this))
        {
            EnemyManager.instance.enemy3s.Add(this);

        }

        //  Debug.LogError(leftFace.transform.position + ":" + rightFace.transform.position);
    }
    public override void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.enemy3s.Contains(this))
        {
            EnemyManager.instance.enemy3s.Remove(this);
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

        if (tempXBegin > Camera.main.transform.position.x + 7.5f)
        {
            return;
        }



        switch (enemyState)
        {
            case EnemyState.attack:
                Attack(1, aec.attack1, false, maxtimeDelayAttack1);
                CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());
                targetPos.transform.position = GetTarget(false);
                if (!canmove)
                    return;

                timedelayChangePos -= deltaTime;
                if (timedelayChangePos <= 0)
                {
                    enemyState = EnemyState.run;
                    timedelayChangePos = maxtimedelayChangePos;
                    if (transform.position.x < OriginPos.x)
                        nextPos.x = OriginPos.x + 0.5f;
                    else
                        nextPos.x = OriginPos.x + -0.5f;

                    CheckDirFollowPlayer(nextPos.x);
                    skeletonAnimation.ClearState();
                    //skeletonAnimation.AnimationState.ClearTrack(1);
                    //skeletonAnimation.AnimationState.ClearTracks();

                    PlayAnim(0, aec.run, true);

                    //    Debug.LogError("--------:"  +nextPos.x);
                }
                break;
            case EnemyState.run:

                nextPos.y = transform.position.y;
                transform.position = Vector2.MoveTowards(transform.position, nextPos, deltaTime * speed);
                targetPos.transform.position = GetTarget(true);
                if (transform.position.x == nextPos.x)
                {
                    PlayAnim(0, aec.idle, true);
                    enemyState = EnemyState.attack;
                    PlayAnim(2, aec.aimTargetAnim, false);
                }
                break;
        }
    }
    GameObject bullet;
    Vector2 dirBullet;
    Quaternion rotation;
    float angle;
    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            if (!incam)
                return;
            bullet = ObjectPoolerManager.Instance.bulletEnemy3Pooler.GetPooledObject();
            var _bulletScript = bullet.GetComponent<BulletEnemy>();
            _bulletScript.AddProperties(damage1, bulletspeed1);
            dirBullet = (Vector2)targetPos.transform.position - (Vector2)boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            angle = Mathf.Atan2(dirBullet.y, dirBullet.x) * Mathf.Rad2Deg;
            rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            bullet.transform.rotation = rotation;
            bullet.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            bullet.SetActive(true);
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
        SoundController.instance.PlaySound(soundGame.sounde3die);
    }
}
