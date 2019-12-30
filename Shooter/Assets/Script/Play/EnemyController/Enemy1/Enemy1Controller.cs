using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : EnemyBase
{
    float speedMove;
    public RaycastHit2D detectPlayer;
    public override void Start()
    {
        base.Start();
        Init();
        // Debug.LogError("----------------start");
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
    public override void Active()
    {
        base.Active();
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
                        //     Debug.LogError("zo day:" + detectPlayer.collider.name);
                    }
                    else
                    {

                        PlayAnim(0, aec.idle, true);
                        CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());
                        //    Debug.LogError("-----zo day:"+ detectPlayer.collider.name);
                    }
                }
                break;
            case EnemyState.run:

                detectPlayer = !FlipX ? Physics2D.Linecast(Origin(), leftFace.position, lm) : Physics2D.Linecast(Origin(), rightFace.position, lm);
                if (detectPlayer.collider != null)
                {
                    enemyState = EnemyState.idle;
                    if (speedMove != 0)
                    {
                        speedMove = 0;
                        rid.velocity = Vector2.zero;
                       PlayAnim(0, aec.idle, true);
                    }
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
                        moveTemp.x = speedMove;
                        moveTemp.y = rid.velocity.y;
                        rid.velocity = moveTemp;
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
                if (combo != randomCombo && combo >= 0)
                    Attack(0, aec.attack1, false, maxtimeDelayAttack1);
                else if (combo == randomCombo && combo > 0)
                    Attack(0, aec.attack2, false, maxtimeDelayAttack1);
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
    Vector2 moveTemp;
    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            if (!incam)
                return;
            boxAttack1.gameObject.SetActive(true);
        }
        if (trackEntry.Animation.Name.Equals(aec.attack2.name))
        {
            if (!incam)
                return;
            boxAttack2.gameObject.SetActive(true);
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(foot.transform.position, 0.115f);
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
        else if (trackEntry.Animation.Name.Equals(aec.attack2.name))
        {
            combo = 0;
            randomCombo = Random.Range(2, 4);
            boxAttack2.gameObject.SetActive(false);
            enemyState = EnemyState.idle;
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

    public override void Dead()
    {
        base.Dead();
        SoundController.instance.PlaySound(soundGame.sounde1die);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance == null)
            return;
        if (EnemyManager.instance.enemy1s.Contains(this))
        {
            EnemyManager.instance.enemy1s.Remove(this);
        }
    }
}
