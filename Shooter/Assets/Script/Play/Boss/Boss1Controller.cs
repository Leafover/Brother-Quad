using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
public class Boss1Controller : EnemyBase
{

    public GameObject effectwhenDie,effectwhenlowhealth;
    public float speedMove;
    public int typeAttack;
    public override void Start()
    {
        base.Start();
        Init();
    }
    public override void Init()
    {
        base.Init();
        if (!EnemyManager.instance.boss1s.Contains(this))
        {
            EnemyManager.instance.boss1s.Add(this);
        }
        speedMove = -speed;
        typeAttack = 0;

        takeDamageBox.enabled = false;

    }

    public override void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.boss1s.Contains(this))
        {
            EnemyManager.instance.boss1s.Remove(this);
        }
    }
    Vector2 posTemp;
    Vector2 moveVelocity;
    void GetPosTemp(float x)
    {
        posTemp.x = x;
        posTemp.y = transform.position.y;
    }
    public override void Active()
    {
        base.Active();
        PlayAnim(1, aec.run, true);
        enemyState = EnemyState.idle;

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
            case EnemyState.idle:
                moveVelocity = rid.velocity;
                moveVelocity.x = speedMove;
                moveVelocity.y = rid.velocity.y;
                rid.velocity = moveVelocity;
                if (Mathf.Abs(transform.position.x - Camera.main.transform.position.x) <= Camera.main.orthographicSize * 1.1f)
                {
                    PlayAnim(1, aec.idle, true);    
                    enemyState = EnemyState.attack;
                    rid.velocity = Vector2.zero;
                    OriginPos = Origin();
                    takeDamageBox.enabled = true;
                    StartCoroutine(ActiveAttack());
                    randomCombo = Random.Range(0, 3);
                    // Debug.LogError("hahaha");
                }
                break;
            case EnemyState.attack:
                switch (typeAttack)
                {
                    case 0:
                        if (move)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, posTemp, deltaTime * speed);
                           // Debug.Log(posTemp.x);
                            if (transform.position.x == posTemp.x)
                            {
                                move = false;
                                PlayAnim(1, aec.idle, true);
                                StartCoroutine(ActiveAttack());
                                randomCombo = Random.Range(0, 3);
                            }
                        }


                        else
                        {
                            CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());
                            if (!canAttack)
                                return;
                            // PlayAnim(0, aec.attack1, false);
                            Attack(1, aec.attack3, false, maxtimeDelayAttack1);
                        }
                        break;
                    case 1:

                        if (move)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, posTemp, deltaTime * speed);
                          //  Debug.Log(posTemp.x +":" + transform.position.x);
                            if (transform.position.x == posTemp.x)
                            {
                                move = false;
                                PlayAnim(1, aec.idle, true);
                                //  combo = 0; //đhs
                                randomCombo = Random.Range(0, 3);
                              //  Debug.LogError("-------toi noi");
                            }
                        }
                        else
                        {
                            PlayAnim(1, aec.attack2, false);
                        }
                        break;
                    case 3:
                        if (move)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, posTemp, deltaTime * speed);
                            if (transform.position.x == posTemp.x)
                            {
                                move = false;
                                PlayAnim(1, aec.idle, true);
                                StartCoroutine(ActiveAttack());
                                randomCombo = Random.Range(0, 3);
                            }

                        }
                        else
                        {
                            CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());
                            if (!canAttack)
                                return;
                            Attack(1, aec.attack3, false, maxtimeDelayAttack1);
                        }
                        break;
                }
                break;
        }
    }
    public override void PlayAnim(int indexTrack, AnimationReferenceAsset anim, bool loop)
    {
        base.PlayAnim(indexTrack, anim, loop);
        canAttack = false;
    }
    public bool move, canAttack;

    IEnumerator ActiveAttack()
    {
        yield return new WaitForSeconds(2);
        canAttack = true;
        if (activeType3 == 1)
            activeType3 = 2;
    }
    IEnumerator ActiveMove()
    {
        yield return new WaitForSeconds(2);
        move = true;
        PlayAnim(1, aec.run, true);
        typeAttack = 1;
        switch (FlipX)
        {
            case true:
                GetPosTemp(PlayerController.instance.GetTranformXPlayer() - 2);

              //  Debug.LogError("begin:--------" + posTemp.x);
             //   Debug.Log("ziiiiiiii");
                break;
            case false:
                GetPosTemp(PlayerController.instance.GetTranformXPlayer() + 2);
              //  Debug.LogError("begin:--------" + posTemp.x);
                //    Debug.Log("ziiiiiiii 11111");
                break;
        }

    }
    //GameObject bullet;
    //BulletEnemy bulletScript;
    //Vector3 dirBullet;
    //float angle;
    //Quaternion rotation;
    void ShootBullet()
    {
        //bullet = ObjectPoolerManager.Instance.bulletBoss1Pooler.GetPooledObject();
        //bullet.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
        //bulletScript = bullet.GetComponent<BulletEnemy>();
        //bulletScript.AddProperties(damage1, bulletspeed1);
        //bulletScript.dir1 = FlipX ? new Vector2(1, 0) : new Vector2(-1, 0);
        //bullet.SetActive(true);

        bulletEnemy = ObjectPoolManagerHaveScript.Instance.bulletBoss1Pooler.GetBulletEnemyPooledObject();
        bulletEnemy.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
        bulletEnemy.AddProperties(damage1, bulletspeed1);
        bulletEnemy.dir1 = FlipX ? new Vector2(1, 0) : new Vector2(-1, 0);
        bulletEnemy.gameObject.SetActive(true);

        SoundController.instance.PlaySound(soundGame.soundb1fire);

    }
    int randomsoundchem;
    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            //combo++;
            //if (!incam)
            //    return;
            //boxAttack1.gameObject.SetActive(true);


            combo++;
            //if (combo == randomCombo + 1)
            //{
            //    maxtimeDelayAttack = 2f;
            //}
            //if (combo == (randomCombo + 2) && maxtimeDelayAttack == 2f)
            //    maxtimeDelayAttack = 1f;
            if (combo == 4)
            {
                canAttack = false;
            }
            if (!incam)
                return;
            ShootBullet();

        }
        else if (trackEntry.Animation.Name.Equals(aec.attack2.name))
        {

            randomsoundchem = Random.Range(0, 2);
            combo++;
            if (!incam)
                return;
            boxAttack2.gameObject.SetActive(true);
            if(randomsoundchem == 0)
            {
                SoundController.instance.PlaySound(soundGame.soundb1chem1);
            }
            else
            {
                SoundController.instance.PlaySound(soundGame.soundb1chem2);
            }
            

        }
        else if (trackEntry.Animation.Name.Equals(aec.attack3.name))
        {
            combo++;
            //if (combo == randomCombo + 1)
            //{
            //    maxtimeDelayAttack = 2f;
            //}
            //if (combo == (randomCombo + 2) && maxtimeDelayAttack == 2f)
            //    maxtimeDelayAttack = 1f;
            if (combo == 4)
            {
                canAttack = false;
            }
            if (!incam)
                return;
            ShootBullet();
        }

        else if(trackEntry.Animation.Name.Equals(aec.run.name))
        {
            SoundController.instance.PlaySound(soundGame.soundb1move);
        }
        else if(trackEntry.Animation.Name.Equals(aec.die.name))
        {
            effectwhenDie.SetActive(true);
        }
    }

    protected override void OnComplete(TrackEntry trackEntry)
    {
        base.OnComplete(trackEntry);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            //boxAttack1.gameObject.SetActive(false);
            //if (combo == randomCombo + 1)
            //{
            //    combo = 0;
            //    PlayAnim(0, aec.idle, true);
            //    StartCoroutine(ActiveMove());

            //}
            //else
            //{
            //    PlayAnim(0, aec.idle, true);
            //    PlayAnim(0, aec.attack1, false);
            //}

            if (combo == 4)
            {
                PlayAnim(1, aec.idle, true);
                StartCoroutine(ActiveMove());
                combo = 0;
             //   maxtimeDelayAttack = 1f;
            }

        }
        if (trackEntry.Animation.Name.Equals(aec.attack2.name))
        {
            boxAttack2.gameObject.SetActive(false);
            if (combo == randomCombo + 1)
            {
                if (activeType3 != 2)
                    typeAttack = 0;
                else
                    typeAttack = 3;
                GetPosTemp(OriginPos.x);

                move = true;
                PlayAnim(1, aec.run, true);
                combo = 0;
                CheckDirFollowPlayer(OriginPos.x);



            }
            else
            {
                PlayAnim(1, aec.idle, true);
                PlayAnim(1, aec.attack2, false);


            }

        }
        if (trackEntry.Animation.Name.Equals(aec.attack3.name))
        {
            if (combo == 4)
            {
                PlayAnim(1, aec.idle, true);
                StartCoroutine(ActiveMove());
                combo = 0;
                //maxtimeDelayAttack = 1f;
            }

        }
    }
    public int activeType3 = 0;
    public override void TakeDamage(float damage,bool crit = false)
    {
        base.TakeDamage(damage);
        if (currentHealth <= health / 2)
        {
            if (activeType3 == 0)
            {
                PlayAnim(2, aec.lowHPAnim, true);
                activeType3 = /*1*/2;
                effectwhenlowhealth.SetActive(true);
            }
        }
    }
}
