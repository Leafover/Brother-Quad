using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class Enemy3Controller : EnemyBase
{
    float timedelayChangePos;
    Vector2 move;
    //  Vector2 nextPos;
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
        randomCombo = Random.Range(1, 4);
        speedMove = speed;

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
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(foot.transform.position, 0.115f);
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

        CheckFallDown();

        switch (enemyState)
        {
            case EnemyState.attack:

                Attack(0, aec.attack1, false, maxtimeDelayAttack1);
                CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());
                targetPos.transform.position = GetTarget(false);
                if (!canmove)
                    return;

                // timedelayChangePos -= deltaTime;
                //if (timedelayChangePos <= 0)
                //{
                //    enemyState = EnemyState.run;
                //    timedelayChangePos = maxtimedelayChangePos;
                //    if (transform.position.x < PosBegin.x)
                //        nextPos.x = PosBegin.x + 0.5f;
                //    else
                //        nextPos.x = PosBegin.x + -0.5f;

                //    CheckDirFollowPlayer(nextPos.x);
                //    skeletonAnimation.ClearState();
                //}
                break;
            case EnemyState.run:

                move = rid.velocity;
                move.x = speedMove;
                move.y = rid.velocity.y;
                rid.velocity = move;

                timedelayChangePos -= deltaTime;
                //nextPos.y = transform.position.y;
                //transform.position = Vector2.MoveTowards(transform.position, nextPos, deltaTime * speed);
                targetPos.transform.position = GetTarget(true);
                PlayAnim(0, aec.run, true);
                if (timedelayChangePos <= 0)
                {
                    PlayAnim(0, aec.idle, true);
                    enemyState = EnemyState.attack;
                    PlayAnim(1, aec.aimTargetAnim, false);
                    rid.velocity = Vector2.zero;
                    timedelayChangePos = maxtimedelayChangePos;
                }
                //if (transform.position.x == nextPos.x)
                //{
                //    PlayAnim(0, aec.idle, true);
                //    enemyState = EnemyState.attack;
                //    PlayAnim(1, aec.aimTargetAnim, false);
                //}
                break;
            case EnemyState.falldown:
                if (isGround)
                {
                    if (aec.standup == null)
                        enemyState = EnemyState.run;
                    else
                    {
                        PlayAnim(0, aec.standup, false);
                    }

                }
                break;
        }
    }
    //  GameObject bullet;
    Vector2 dirBullet;
    Quaternion rotation;
    float angle;
    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            combo++;
            if (!incam)
                return;
            //bullet = ObjectPoolerManager.Instance.bulletEnemy3Pooler.GetPooledObject();
            //var _bulletScript = bullet.GetComponent<BulletEnemy>();
            //_bulletScript.AddProperties(damage1, bulletspeed1);
            //dirBullet = (Vector2)targetPos.transform.position - (Vector2)boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            //angle = Mathf.Atan2(dirBullet.y, dirBullet.x) * Mathf.Rad2Deg;
            //rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //bullet.transform.rotation = rotation;
            //bullet.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            //bullet.SetActive(true);

            bulletEnemy = ObjectPoolManagerHaveScript.Instance.bullet3EnemyBasepooler.GetBulletEnemyPooledObject();
            bulletEnemy.AddProperties(damage1, bulletspeed1);
            dirBullet = (Vector2)targetPos.transform.position - (Vector2)boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            angle = Mathf.Atan2(dirBullet.y, dirBullet.x) * Mathf.Rad2Deg;
            rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            bulletEnemy.transform.rotation = rotation;
            bulletEnemy.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            bulletEnemy.gameObject.SetActive(true);
 
        }
    }
    protected override void OnComplete(TrackEntry trackEntry)
    {
        base.OnComplete(trackEntry);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {

            if (enemyState == EnemyState.die)
                return;
            if (aec.standup != null)
            {
                if (trackEntry.Animation.Name.Equals(aec.standup.name))
                {
                    enemyState = EnemyState.attack;
                }
            }

            if (enemyState == EnemyState.falldown)
                return;

            PlayAnim(0, aec.idle, true);
            if (!canmove)
                return;
            if (combo == randomCombo)
            {
                skeletonAnimation.ClearState();
                timedelayChangePos = maxtimedelayChangePos;
                randomCombo = Random.Range(1, 4);
                combo = 0;
                timedelayChangePos = maxtimedelayChangePos;
                speedMove = -speedMove;

                if (speedMove <= 0)
                    FlipX = false;
                else
                    FlipX = true;
                enemyState = EnemyState.run;
            }
        }

    }
    float speedMove;
    public override void Dead()
    {
        base.Dead();
        SoundController.instance.PlaySound(soundGame.sounde3die);
    }
}
