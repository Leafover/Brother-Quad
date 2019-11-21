using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : EnemyBase
{
    float speedMove;
    public bool detectPlayer;
    private void Start()
    {
        base.Start();
        Init();
    }
    public override void Init()
    {
        base.Init();
        randomCombo = Random.Range(2, 4);
        if (!EnemyManager.instance.enemy1s.Contains(this))
        {
            EnemyManager.instance.enemy1s.Add(this);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, 1f);
    //}
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
            }
            return;
        }

        switch (enemyState)
        {
            case EnemyState.idle:
                detectPlayer = Physics2D.OverlapCircle(Origin(), radius, lm);
                if (!detectPlayer)
                {
                    enemyState = EnemyState.run;
                }
                else
                {
                    enemyState = EnemyState.attack;
                }
                break;
            case EnemyState.run:

                if (Mathf.Abs(transform.position.x - PlayerController.instance.GetTranformXPlayer()) <= radius - 0.1f)
                {
                    PlayAnim(0, aec.idle, true);
                    speedMove = 0;
                }
                else
                {
                    PlayAnim(0, aec.run, true);
                    speedMove = CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());
                }

                detectPlayer = Physics2D.OverlapCircle(Origin(), 1f, lm);
                if (detectPlayer)
                {
                    enemyState = EnemyState.idle;
                }
                rid.velocity = new Vector2(speedMove, rid.velocity.y);
                break;
            case EnemyState.attack:
                speedMove = 0;
                if (combo != randomCombo && combo >= 0)
                    Attack(0, aec.attack1, false);
                else if (combo == randomCombo && combo > 0)
                    Attack(0, aec.attack2, false);
                break;
        }

    }

    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            boxAttack1.gameObject.SetActive(true);
        }
        if (trackEntry.Animation.Name.Equals(aec.attack2.name))
        {
            boxAttack2.gameObject.SetActive(true);
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    protected override void OnComplete(TrackEntry trackEntry)
    {
        base.OnComplete(trackEntry);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            boxAttack1.gameObject.SetActive(false);
            combo++;
            enemyState = EnemyState.idle;
        }
        if (trackEntry.Animation.Name.Equals(aec.attack2.name))
        {
            combo = 0;
            randomCombo = Random.Range(2, 4);
            boxAttack2.gameObject.SetActive(false);
            enemyState = EnemyState.idle;
        }
        //if (trackEntry.Animation.Name.Equals(aec.die.name))
        //{
        //    gameObject.SetActive(false);
        //}
    }
    private void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.enemy1s.Contains(this))
        {
            EnemyManager.instance.enemy1s.Remove(this);
        }
    }
}
