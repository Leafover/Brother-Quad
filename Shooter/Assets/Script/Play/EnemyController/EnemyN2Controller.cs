using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyN2Controller : EnemyBase
{
    public RaycastHit2D detectPlayer;
    float speedMove;


    public Transform frameSprite;
    Vector2 scale;
    public void SetPosFrameSprite()
    {
        scale.x = FlipX ? 1 : -1;
        scale.y = 1;
        frameSprite.transform.position = boxAttack1.transform.position = FlipX ? rightFace.position : leftFace.position;
        frameSprite.localScale = scale;


    }
    public override void Active()
    {
        base.Active();
        frameSprite.gameObject.SetActive(true);
    }

    public override void Start()
    {

        base.Start();
        Init();
    }
    public override void Init()
    {
        base.Init();
        if (!EnemyManager.instance.enemyn2s.Contains(this))
        {
            EnemyManager.instance.enemyn2s.Add(this);
        }
        enemyState = EnemyState.idle;
        frameSprite.gameObject.SetActive(false);
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
        SetPosFrameSprite();



        timePreviousAttack -= deltaTime;
        if (timePreviousAttack <= 0)
        {
            timePreviousAttack = maxtimeDelayAttack1;
            boxAttack1.gameObject.SetActive(true);
        }

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
                    PlayAnim(0, aec.idle, true);
                    CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());
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
                    }
                    enemyState = EnemyState.idle;
                }
                else
                {
                    var tempX = transform.position.x;
                    if (Mathf.Abs(tempX - PlayerController.instance.GetTranformXPlayer()) <= radius - 0.1f)
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

        }

    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(Origin(), radius);
    //}

    public override void OnDisable()
    {
        base.OnDisable();

        if (EnemyManager.instance == null)
            return;

        if (EnemyManager.instance.enemyn2s.Contains(this))
        {
            EnemyManager.instance.enemyn2s.Remove(this);
        }
    }



    public override void Dead()
    {
        base.Dead();
        SoundController.instance.PlaySound(soundGame.sounde5die);
    }
}
