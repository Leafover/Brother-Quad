﻿using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM1Controller : EnemyBase
{
    Vector2 move;
    float timedelayChangePos;
    bool isGrenadeStage;
    public override void Start()
    {
        base.Start();
        Init();
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(foot.transform.position, 0.115f);
    //}
    public override void Init()
    {
        base.Init();
        timedelayChangePos = maxtimedelayChangePos;
        randomCombo = Random.Range(3, 5);
        isGrenadeStage = false;
        speedMove = speed / 2;
        //   timedelayShoot = maxtimeDelayAttack;
        if (!EnemyManager.instance.em1s.Contains(this))
        {
            EnemyManager.instance.em1s.Add(this);

        }
    }
    public override void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.em1s.Contains(this))
        {
            EnemyManager.instance.em1s.Remove(this);
        }
        // Debug.LogError("tu nhien bien mat");
    }

    public override void Active()
    {
        base.Active();
        //  enemyState = EnemyState.idle;

        enemyState = EnemyState.attack;
        //  StartCoroutine(delayActive());
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

                CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());


                if (!canmove)
                {
                    if (isGrenadeStage)
                    {
                        Attack(0, aec.attack1, false, maxtimeDelayAttack1);
                        targetPos.transform.position = GetTarget(true);
                    }
                    else
                    {

                        Attack(0, aec.attack2, false, maxtimeDelayAttack2);
                        targetPos.transform.position = GetTarget(false);
                    }
                    return;
                }

                if (isGrenadeStage)
                {

                    Attack(0, aec.attack1, false, maxtimeDelayAttack1);
                    targetPos.transform.position = GetTarget(true);
                }
                else
                {

                    Attack(0, aec.attack2, false, maxtimeDelayAttack2);
                    targetPos.transform.position = GetTarget(false);
                }

                break;
            case EnemyState.run:

                move = rid.velocity;
                move.x = speedMove;
                move.y = rid.velocity.y;
                rid.velocity = move;
                timedelayChangePos -= deltaTime;


                targetPos.transform.position = GetTarget(true);

                PlayAnim(0, aec.run, true);

                if (timedelayChangePos <= 0)
                {
                    PlayAnim(0, aec.idle, true);
                    enemyState = EnemyState.attack;
                    PlayAnim(1, aec.aimTargetAnim, false);
                    timedelayChangePos = maxtimedelayChangePos;
                    rid.velocity = Vector2.zero;
                }

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

    Vector2 dirBullet;
    float angle;
    Quaternion rotation;
    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);
        if (trackEntry.Animation.Name.Equals(aec.attack2.name))
        {
            combo++;
            if (!incam)
                return;

            bulletEnemy = ObjectPoolManagerHaveScript.Instance.bullet3EnemyBasepooler.GetBulletEnemyPooledObject();
            bulletEnemy.AddProperties(damage1, bulletspeed1);
            dirBullet = (Vector2)targetPos.transform.position - (Vector2)boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            angle = Mathf.Atan2(dirBullet.y, dirBullet.x) * Mathf.Rad2Deg;
            rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            bulletEnemy.transform.rotation = rotation;
            bulletEnemy.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            bulletEnemy.gameObject.SetActive(true);

            //Debug.LogError("shot");
        }
        else if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            combo++;
            if (!incam)
                return;

            bulletEnemy = ObjectPoolManagerHaveScript.Instance.grenadeN3Pooler.GetBulletEnemyPooledObject();
            bulletEnemy.transform.position = boneBarrelGun1.GetWorldPosition(skeletonAnimation.transform);
            bulletEnemy.AddProperties(damage2, 6);
            if (FlipX)
                bulletEnemy.SetDir(-6, false);
            else
                bulletEnemy.SetDir(6, false);

            bulletEnemy.gameObject.SetActive(true);
        }

    }
    protected override void OnComplete(TrackEntry trackEntry)
    {
        base.OnComplete(trackEntry);

        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            PlayAnim(0, aec.idle, true);
            if (combo == randomCombo)
            {
                if (canmove)
                {
                    if (enemyState == EnemyState.falldown)
                        return;

                    timedelayChangePos = maxtimedelayChangePos;
                    speedMove = -speedMove;

                    if (speedMove <= 0)
                        FlipX = false;
                    else
                        FlipX = true;
                    enemyState = EnemyState.run;
                }

                combo = 0;
                randomCombo = Random.Range(3, 5);
                isGrenadeStage = false;
            }

        }

        else if (trackEntry.Animation.Name.Equals(aec.attack2.name))
        {
            PlayAnim(0, aec.idle, true);

            if (combo == randomCombo)
            {
                if (canmove)
                {
                    if (enemyState == EnemyState.falldown)
                        return;

                    isGrenadeStage = true;
                    skeletonAnimation.ClearState();

                    enemyState = EnemyState.run;

                    timedelayChangePos = maxtimedelayChangePos;

                    speedMove = -speedMove;

                    if (speedMove < 0)
                        FlipX = false;
                    else
                        FlipX = true;
                }
              //  Debug.LogError("why????");
                combo = 0;
                randomCombo = Random.Range(3, 5);
                isGrenadeStage = true;
            }
        }
        if (enemyState == EnemyState.die)
            return;
        if (aec.standup == null)
            return;
        if (trackEntry.Animation.Name.Equals(aec.standup.name))
        {
            enemyState = EnemyState.attack;
        }

    }
    public float speedMove;

    public override void Dead()
    {
        base.Dead();
        SoundController.instance.PlaySound(soundGame.sounde5die);
    }
}
