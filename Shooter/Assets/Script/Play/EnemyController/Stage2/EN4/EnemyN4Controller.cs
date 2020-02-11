using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyN4Controller : EnemyBase
{
    //  public GameObject sheld;
    public float speedMove;
    float timechangeShield;
    public override void Start()
    {
        base.Start();
        Init();

    }
    public override void Init()
    {
        base.Init();
        if (!EnemyManager.instance.enemyn4s.Contains(this))
        {
            EnemyManager.instance.enemyn4s.Add(this);
        }
        speedMove = -speed;
        timechangeShield = maxtimedelayChangePos;
        //  sheld.SetActive(false);
        //   Debug.Log("----------------:" + speedMove);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.enemyn4s.Contains(this))
        {
            EnemyManager.instance.enemyn4s.Remove(this);
        }
    }
    public override void Active()
    {
        base.Active();
        PlayAnim(0, aec.run, true);
        enemyState = EnemyState.run;
        // Debug.LogError("-------active" + isActive);
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

        switch (enemyState)
        {
            case EnemyState.run:
                rid.velocity = new Vector2(speedMove, rid.velocity.y);
                if (Mathf.Abs(transform.position.x - Camera.main.transform.position.x) <= 7.5f)
                {
                    PlayAnim(0, aec.idle, true);
                    enemyState = EnemyState.attack;
                    rid.velocity = Vector2.zero;
                }
                break;
            case EnemyState.attack:

                CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());

                if (timechangeShield > 0)
                    timechangeShield -= deltaTime;


                if (Mathf.Abs(transform.position.y - PlayerController.instance.transform.position.y) <= 0.5f)
                {
                    Attack(1, aec.attack1, false, maxtimeDelayAttack1);
                    //   parabolBullet = false;
                }

                else
                {
                    Attack(1, aec.attack2, false, maxtimeDelayAttack1);
                    //  parabolBullet = true;
                }

                break;
            case EnemyState.idle:
                if (timechangeShield > 0)
                    timechangeShield -= deltaTime;
                else
                {
                    enemyState = EnemyState.attack;
                    isShield = false;
                    //      sheld.SetActive(false);
                    timechangeShield = maxtimedelayChangePos;
                    PlayAnim(0, aec.idle, true);
                }
                break;
        }

    }
    //  bool parabolBullet;
    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);

        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            if (!incam)
                return;

            bulletEnemy = ObjectPoolManagerHaveScript.Instance.bulletEnemyV1Pooler.GetBulletEnemyPooledObject();

            //if (!parabolBullet)
            //{
            bulletEnemy.AddProperties(damage1, 0);
            if (FlipX)
            {
                bulletEnemy.SetDir(-bulletspeed1, false);
              //  Debug.Log("TH1");
            }
            else
            {
                bulletEnemy.SetDir(bulletspeed1, false);
             //   Debug.Log("TH2");
            }
            bulletEnemy.rid.gravityScale = 0;
            bulletEnemy.isGrenade = false;
            bulletEnemy.transform.position = leftFace.transform.position;
            bulletEnemy.transform.eulerAngles = leftFace.transform.eulerAngles;
            bulletEnemy.gameObject.SetActive(true);

            SoundController.instance.PlaySound(soundGame.soundv1attack);
        }
        else if (trackEntry.Animation.Name.Equals(aec.attack2.name))
        {
            if (!incam)
                return;

            bulletEnemy = ObjectPoolManagerHaveScript.Instance.bulletEnemyV1Pooler.GetBulletEnemyPooledObject();
            bulletEnemy.AddProperties(damage1, bulletspeed1 / 2);

            if (FlipX)
            {
                bulletEnemy.SetDir(-bulletspeed1 / 2, true);

            }
            else
            {
                bulletEnemy.SetDir(bulletspeed1 / 2, true);

            }
            bulletEnemy.transform.position = rightFace.transform.position;
            bulletEnemy.transform.eulerAngles = rightFace.transform.eulerAngles;

            bulletEnemy.rid.gravityScale = 1;
            bulletEnemy.isGrenade = true;

            //}

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

            if (timechangeShield <= 0)
            {
                enemyState = EnemyState.idle;
                //    sheld.SetActive(true);
                isShield = true;
                timechangeShield = maxtimedelayChangePos;
                PlayAnim(1, aec.jumpOut, false);
            }
        }
        else if (trackEntry.Animation.Name.Equals(aec.jumpOut.name))
        {
            PlayAnim(1, aec.lowHPAnim, true);
        }
    }
    public override void Dead()
    {
        base.Dead();
        SoundController.instance.PlaySound(soundGame.sounde6die);
    }
}
