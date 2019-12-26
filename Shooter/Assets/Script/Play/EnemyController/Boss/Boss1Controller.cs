using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
public class Boss1Controller : EnemyBase
{

    public Bone[] boneExplo = new Bone[7];
    [SpineBone]
    public string[] strBoneExplo = new string[7];

    public GameObject effectwhenDie, effectsmoke, effectexplohand;
    public float speedMove;
    public int typeAttack;
    public override void Start()
    {
        base.Start();
        Init();
        if (waitBeAttack == null)
            waitBeAttack = new WaitForSeconds(0.1f);

        for(int i = 0; i < strBoneExplo.Length; i ++)
        {
            boneExplo[i] = skeletonAnimation.Skeleton.FindBone(strBoneExplo[i]);
        }
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
                    PosBegin = Origin();
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
                            boxAttack1.transform.position = boneBarrelGun1.GetWorldPosition(skeletonAnimation.transform);
                            PlayAnim(1, aec.attack2, false);
                        }
                        break;
                    case 3:
                        effectsmoke.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
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
    int randomtypebullet;
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

        if (typeAttack == 0)
        {
            bulletEnemy.AddProperties(damage1, bulletspeed1);
            bulletEnemy.dir1 = FlipX ? new Vector2(1, 0) : new Vector2(-1, 0);
            bulletEnemy.rid.gravityScale = 0;
            bulletEnemy.gameObject.layer = 16;
            bulletEnemy.Init(3);
            bulletEnemy.gameObject.SetActive(true);
        }
        else if (typeAttack == 3)
        {

            randomtypebullet = Random.Range(0, 2);
            if (randomtypebullet == 0)
            {
                bulletEnemy.AddProperties(damage1, bulletspeed1);
                bulletEnemy.dir1 = FlipX ? new Vector2(1, 0) : new Vector2(-1, 0);
                bulletEnemy.rid.gravityScale = 0;
                bulletEnemy.gameObject.layer = 16;
                bulletEnemy.Init(3);
                bulletEnemy.gameObject.SetActive(true);
            }
            else
            {
                bulletEnemy.AddProperties(damage1, bulletspeed1);
                bulletEnemy.dir1 = FlipX ? new Vector2(bulletspeed1 / 3, bulletspeed1 / 2) : new Vector2(-bulletspeed1 / 3, bulletspeed1 / 2);
                bulletEnemy.rid.gravityScale = 1;
                bulletEnemy.gameObject.layer = 17;
                bulletEnemy.Init(4);
            }

        }
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

            boxAttack1.gameObject.SetActive(true);
            if (randomsoundchem == 0)
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

            if (combo == 4)
            {
                canAttack = false;
            }
            if (!incam)
                return;
            ShootBullet();
        }

        else if (trackEntry.Animation.Name.Equals(aec.run.name))
        {
            SoundController.instance.PlaySound(soundGame.soundb1move);
        }
        else if (trackEntry.Animation.Name.Equals(aec.die.name))
        {
            if (!displayeffect)
            {
                effectwhenDie.SetActive(true);
                StartCoroutine(BeDie());
                displayeffect = true;
            }
        }
    }
    bool displayeffect = false;
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
            else
            {
                if (activeType3 == 2)
                    typeAttack = 3;
            }

        }
        if (trackEntry.Animation.Name.Equals(aec.attack2.name))
        {
            boxAttack1.gameObject.SetActive(false);
            if (combo == randomCombo + 1)
            {
                if (activeType3 != 2)
                    typeAttack = 0;
                else
                    typeAttack = 3;
                GetPosTemp(PosBegin.x);

                move = true;
                PlayAnim(1, aec.run, true);
                combo = 0;
                CheckDirFollowPlayer(PosBegin.x);



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
    public override void TakeDamage(float damage, bool crit = false)
    {
        base.TakeDamage(damage);
        if (currentHealth <= health / 2)
        {
            if (activeType3 == 0)
            {
                activeType3 = /*1*/2;
                StartCoroutine(BeAttackFill());
            }
        }

    }

    WaitForSeconds waitBeAttack;
    //   int randomWin;
    IEnumerator BeAttackFill()
    {
        skeletonAnimation.skeleton.SetColor(new Color(1, 1, 1, 0.5f));
        yield return waitBeAttack;
        skeletonAnimation.skeleton.SetColor(new Color(1, 0.5f, 0, 1));
        yield return waitBeAttack;
        skeletonAnimation.skeleton.SetColor(new Color(1, 1, 1, 1));
        yield return waitBeAttack;
        skeletonAnimation.skeleton.SetColor(new Color(1, 0.5f, 0, 1));
        yield return waitBeAttack;
        skeletonAnimation.skeleton.SetColor(Color.white);
        yield return waitBeAttack;
        effectsmoke.transform.position = effectexplohand.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
        effectexplohand.SetActive(true);
        SoundController.instance.PlaySound(soundGame.soundexploenemy);
        yield return waitBeAttack;
        effectsmoke.SetActive(true);

    }
    void EffectWhendie(int index)
    {
        effectexploandsmokewhendie = ObjectPoolerManager.Instance.effectbosswhendiePooler.GetPooledObject();
        effectexploandsmokewhendie.transform.position = boneExplo[index].GetWorldPosition(skeletonAnimation.transform);
        effectsss.Add(effectexploandsmokewhendie);
        SoundController.instance.PlaySound(soundGame.soundexploenemy);
        effectexploandsmokewhendie.SetActive(true);
      //  Debug.LogError("---------- zo day coi :D");
    }
    List<GameObject> effectsss = new List<GameObject>();
    public void ExploOffBoss()
    {
        SoundController.instance.PlaySound(soundGame.soundexploenemy);
        exploDie = ObjectPoolerManager.Instance.boss1ExploPooler.GetPooledObject();
        posExplo.x = gameObject.transform.position.x;
        posExplo.y = gameObject.transform.position.y - 1.5f;
        exploDie.transform.position = posExplo;
        exploDie.SetActive(true);
        CameraController.instance.Shake(CameraController.ShakeType.ExplosionBossShake);
        GameController.instance.SpawnCoin(15, transform.position);
    }
    GameObject effectexploandsmokewhendie;
    IEnumerator BeDie()
    {
        for (int i = 0; i < strBoneExplo.Length; i++)
        {
          //  Debug.LogError("---------- zo day coi :D");
            yield return new WaitForSeconds(0.3f);
            EffectWhendie(i);
        }
        StartCoroutine(delayDie());
    }
    IEnumerator delayDie()
    {
        yield return new WaitForSeconds(1);
        ExploOffBoss();
        gameObject.SetActive(false);

        foreach (GameObject _explo in effectsss)
            _explo.SetActive(false);
    }
}
