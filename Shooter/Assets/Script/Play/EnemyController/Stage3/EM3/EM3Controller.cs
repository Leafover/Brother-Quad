﻿using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM3Controller : EnemyBase
{
  //  bool isAttacking;
    bool isGrenadeStage;
    public override void Start()
    {
        base.Start();
        Init();
    }
    public override void Init()
    {
        base.Init();
        if (!EnemyManager.instance.em3s.Contains(this))
        {
            EnemyManager.instance.em3s.Add(this);
        }
        isGrenadeStage = true;
        randomCombo = Random.Range(3, 5);
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
        CheckFallDown();
        Debug.LogError("======zo day=====");
        if (isGrenadeStage)
        {
            switch (enemyState)
            {
                case EnemyState.attack:
                    CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());
                    Attack(0, aec.attack1, false, maxtimeDelayAttack1 * 2);
                    Debug.LogError("zo day");
                    break;
                case EnemyState.falldown:
                    if (isGround)
                    {
                        if (aec.standup == null)
                            enemyState = EnemyState.idle;
                        else
                        {
                            PlayAnim(0, aec.standup, false);
                        }
                    }
                    break;
            }
        }
        else
        {
            switch (enemyState)
            {
                case EnemyState.idle:

                    detectPlayer = !FlipX ? Physics2D.Linecast(Origin(), leftFace.position, lm) : Physics2D.Linecast(Origin(), rightFace.position, lm);

                    if (detectPlayer.collider == null)
                    {
                        enemyState = EnemyState.run;
                    }
                    else
                    {
                        if (detectPlayer.collider.gameObject.layer == 13)
                        {
                            enemyState = EnemyState.attack;
                               Debug.LogError("zo day");
                        }
                        else
                        {
                            PlayAnim(0, aec.idle, true);
                            CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());
                           Debug.LogError("-----zo day");
                        }
                    }
                    break;
                case EnemyState.run:

                    detectPlayer = !FlipX ? Physics2D.Linecast(Origin(), leftFace.position, lm) : Physics2D.Linecast(Origin(), rightFace.position, lm);
                    if (detectPlayer.collider != null)
                    {
                        if (speedMove != 0)
                        {
                            speedMove = 0;
                            rid.velocity = Vector2.zero;
                            PlayAnim(0, aec.idle, true);
                        }
                        enemyState = EnemyState.idle;
                        Debug.LogError("-----zo day");
                    }
                    else
                    {
                        var tempX = transform.position.x;
                        if (Mathf.Abs(tempX - PlayerController.instance.GetTranformXPlayer()) <= radius - 0.1f/* && transform.position.y <= PlayerController.instance.GetTransformPlayer().position.y*/)
                        {

                            if (speedMove != 0)
                            {
                                speedMove = 0;
                                rid.velocity = Vector2.zero;
                                PlayAnim(0, aec.idle, true);
                            }
                        }
                        else
                        {

                            PlayAnim(0, aec.run, true);
                            speedMove = CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());
                            move = rid.velocity;
                            move.x = speedMove;
                            move.y = rid.velocity.y;
                            rid.velocity = move;
                        }
                        Debug.LogError("-----zo day");
                    }

                    break;
                case EnemyState.attack:

                    if (speedMove != 0)
                    {
                        speedMove = 0;
                        rid.velocity = Vector2.zero;
                        PlayAnim(0, aec.idle, true);
                    }
                    Attack(0, aec.attack2, false, maxtimeDelayAttack1 * 2);
                    break;
                case EnemyState.falldown:
                    if (isGround)
                    {
                        if (aec.standup == null)
                            enemyState = EnemyState.idle;
                        else
                        {
                            previousState = EnemyState.idle;
                            PlayAnim(0, aec.standup, false);
                        }

                    }
                    break;
            }
        }
    }


    Vector2 move;
    public RaycastHit2D detectPlayer;
    float speedMove;
    Vector2 dirBullet;
    float angle;
    Quaternion rotation;
    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);
        if (trackEntry.Animation.Name.Equals(aec.attack2.name))
        {
            if (!incam)
                return;
            boxAttack1.gameObject.SetActive(true);

          //  Debug.LogError("------- zooooooo ---------");
        }
        else if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            //if (isAttacking)
            //    return;
            combo++;
            //isAttacking = true;
            if (!incam)
                return;

            bulletEnemy = ObjectPoolManagerHaveScript.Instance.grenadeN3Pooler.GetBulletEnemyPooledObject();
            bulletEnemy.transform.position = boneBarrelGun1.GetWorldPosition(skeletonAnimation.transform);
            bulletEnemy.AddProperties(damage1, 6);
            if (FlipX)
                bulletEnemy.SetDir(-6, false);
            else
                bulletEnemy.SetDir(6, false);
            bulletEnemy.gameObject.SetActive(true);


           // Debug.LogError("------- zooooooo ---------");
        }

    }
    protected override void OnComplete(TrackEntry trackEntry)
    {
        base.OnComplete(trackEntry);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
         //   isAttacking = false;
            PlayAnim(0, aec.idle, true);
            if (combo == randomCombo)
            {
                combo = 0;
                isGrenadeStage = false;
                enemyState = EnemyState.idle;
            }
        }
        else if(trackEntry.Animation.Name.Equals(aec.attack2.name))
        {
            enemyState = EnemyState.idle;
            if (!incam)
                return;
            boxAttack1.gameObject.SetActive(false);
        }
        if (enemyState == EnemyState.die)
            return;
        if (aec.standup == null)
            return;
        if (trackEntry.Animation.Name.Equals(aec.standup.name))
        {
            isGrenadeStage = false;
            Debug.LogError("wtf");
        }
    }
    public override void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.em3s.Contains(this))
        {
            EnemyManager.instance.em3s.Remove(this);
        }
    }

    public override void Dead()
    {
        base.Dead();
        SoundController.instance.PlaySound(soundGame.sounde5die);
    }
}
