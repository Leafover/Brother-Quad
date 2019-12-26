using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyN1Controller : EnemyBase
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
        if (!EnemyManager.instance.enemyn1s.Contains(this))
        {
            EnemyManager.instance.enemyn1s.Add(this);
        }
        enemyState = EnemyState.idle;

        //  radius = Mathf.Abs(leftFace.position.x - transform.position.x);
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

        switch (enemyState)
        {
            case EnemyState.idle:
                //  detectPlayer = Physics2D.OverlapCircle(Origin(), radius, lm);
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
                //    detectPlayer = Physics2D.OverlapCircle(Origin(), 1f, lm);
                detectPlayer = !FlipX ? Physics2D.Linecast(Origin(), leftFace.position, lm) : Physics2D.Linecast(Origin(), rightFace.position, lm);
                if (detectPlayer.collider != null)
                {
                    if (speedMove != 0)
                    {
                        speedMove = 0;
                        rid.velocity = Vector2.zero;
                    }
                    enemyState = EnemyState.idle;
                    //    Debug.Log("fat hien");
                }
                else
                {
                    var tempX = transform.position.x;
                    if (Mathf.Abs(tempX - PlayerController.instance.GetTranformXPlayer()) <= radius - 0.1f)
                    {
                        CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());
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
                }
                CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());
                Attack(0, aec.attack1, false, maxtimeDelayAttack1);
                break;
        }

    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(Origin(), radius);
    //}
    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            if (!incam)
                return;
            SoundController.instance.PlaySound(soundGame.soundEN1Attack);
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
    }
    public override void OnDisable()
    {
        base.OnDisable();

        if (EnemyManager.instance == null)
            return;

        if (EnemyManager.instance.enemyn1s.Contains(this))
        {
            EnemyManager.instance.enemyn1s.Remove(this);
        }
    }



    public override void Dead()
    {
        base.Dead();
        SoundController.instance.PlaySound(soundGame.soundEN1Die);
    }
}
