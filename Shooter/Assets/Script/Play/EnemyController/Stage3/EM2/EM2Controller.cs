using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM2Controller : EnemyBase
{
    public RaycastHit2D detectPlayer;
    float speedMove;


    public override void Start()
    {
        base.Start();
        Init();
    }
    public override void Init()
    {
        base.Init();
        if (!EnemyManager.instance.em2s.Contains(this))
        {
            EnemyManager.instance.em2s.Add(this);
        }
        enemyState = EnemyState.idle;

        //  radius = Mathf.Abs(leftFace.position.x - transform.position.x);
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(foot.transform.position, 0.115f);
    //}
    public override void Active()
    {
        base.Active();
    }
    Vector2 move;
    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);

        if (!isActive)
        {
            return;
        }
        if (enemyState == EnemyState.die)
            return;

        CheckFallDown();

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
                        //     Debug.LogError("zo day");
                    }
                    else
                    {
                        PlayAnim(0, aec.idle, true);
                        CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());
                        //  Debug.LogError("-----zo day");
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
                }

                break;
            case EnemyState.attack:

                if (speedMove != 0)
                {
                    speedMove = 0;
                    rid.velocity = Vector2.zero;
                    PlayAnim(0, aec.idle, true);
                }
                Attack(0, aec.attack1, false, maxtimeDelayAttack1);
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

    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            if (!incam)
                return;
            boxAttack1.gameObject.SetActive(true);
        }
    }
    protected override void OnComplete(TrackEntry trackEntry)
    {
        base.OnComplete(trackEntry);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
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
            enemyState = EnemyState.idle;
        }
    }
    public override void OnDisable()
    {
        base.OnDisable();

        if (EnemyManager.instance == null)
            return;

        if (EnemyManager.instance.em2s.Contains(this))
        {
            EnemyManager.instance.em2s.Remove(this);
        }
    }

    public override void Dead()
    {
        base.Dead();
    }
}
