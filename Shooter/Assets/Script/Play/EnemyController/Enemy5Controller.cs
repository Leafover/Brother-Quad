using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
public class Enemy5Controller : EnemyBase
{
    public bool detectPlayer;
    float speedMove;

    public override void Start()
    {
        base.Start();
        Init();
    }
    public override void Init()
    {
        base.Init();
        if (!EnemyManager.instance.enemy5s.Contains(this))
        {
            EnemyManager.instance.enemy5s.Add(this);
        }
        enemyState = EnemyState.idle;
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
                Attack(0, aec.attack1, false);
                break;
        }

    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, radius);
    //}
    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            boxAttack1.gameObject.SetActive(true);
        }
    }
    protected override void OnComplete(TrackEntry trackEntry)
    {
        base.OnComplete(trackEntry);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            boxAttack1.gameObject.SetActive(false);
            enemyState = EnemyState.idle;
        }

    }
    public override void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.enemy5s.Contains(this))
        {
            EnemyManager.instance.enemy5s.Remove(this);
        }
    }
}
