using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyN2Controller : EnemyBase
{
    public RaycastHit2D detectPlayer;
    float speedMove;
    public Transform frameSprite;
    Vector2 scale;
    public GameObject effectfiredie;

    IEnumerator delayPlayShotBegin()
    {
        yield return new WaitForSeconds(1);
        skeletonAnimation.AnimationState.SetAnimation(1, aec.attack1, false);
    }
    bool firstgoinscene;

    public void BeginShot()
    {
        if (!firstgoinscene)
        {
            StartCoroutine(delayPlayShotBegin());
            firstgoinscene = true;
            //  Debug.LogError("-----------zoooo day-------");
        }
    }
    public void SetPosFrameSprite()
    {
        scale.x = FlipX ? -1 : 1;
        scale.y = 1;
        frameSprite.transform.position = boxAttack1.transform.position = FlipX ? rightFace.position : leftFace.position;
        frameSprite.localScale = scale;
    }
    public override void Active()
    {
        base.Active();
        au.Play();
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
        firstgoinscene = false;
        waitdie = false;
    }
    int randomDie;
    public void RunToDie()
    {
        timePreviousAttack = 1f;
        frameSprite.gameObject.SetActive(false);
        effectfiredie.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
        effectfiredie.SetActive(true);
        randomDie = Random.Range(0, 2);
        if (randomDie == 0)
        {
            speedMove = -speed;
            FlipX = false;
        }
        else
        {
            speedMove = speed;
            FlipX = true;

        }
    }
    Vector2 move;
    bool waitdie;
    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);

        if (!isActive)
        {
            return;
        }
        if (enemyState == EnemyState.die)
        {
            if (waitdie)
                return;
            move = rid.velocity;
            move.x = speedMove;
            move.y = rid.velocity.y;
            rid.velocity = move;
            timePreviousAttack -= Time.deltaTime;
            if (timePreviousAttack <= 0)
            {
                skeletonAnimation.AnimationState.SetAnimation(0, aec.die, false);
                waitdie = true;
                rid.velocity = Vector2.zero;
                speedMove = 0;
                //  Debug.LogError("die thoi" + currentAnim.name);
            }

            return;
        }


        if (frameSprite.gameObject.activeSelf)
        {
            SetPosFrameSprite();
            timePreviousAttack -= deltaTime;
            if (timePreviousAttack <= 0)
            {
                timePreviousAttack = maxtimeDelayAttack1;
                boxAttack1.gameObject.SetActive(true);
            }
        }
        else
        {
            if (frameOn)
            {
                BeginShot();
            }
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
    protected override void OnComplete(TrackEntry trackEntry)
    {
        base.OnComplete(trackEntry);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            PlayAnim(1, aec.attack2, true);
            frameSprite.gameObject.SetActive(true);
        }
    }
    public override void OnDisable()
    {
        base.OnDisable();
        effectfiredie.SetActive(false);
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
        SoundController.instance.PlaySound(soundGame.soundEN2die);
        au.Stop();
        RunToDie();
    }
}
