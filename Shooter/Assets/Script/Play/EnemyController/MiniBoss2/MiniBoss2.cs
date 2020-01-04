using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss2 : EnemyBase
{
    public GameObject effectLaze;
    public bool haveGun1, haveGun2;
    public AnimationReferenceAsset die1Anim;
    public List<AnimationReferenceAsset> shotguns, dieguns;
    Bone[] boneGun = new Bone[8];
    [SpineBone]
    public string[] strboneGun;
    public List<GunMiniBoss2> gunList;
    public GunMiniBoss2 gunCenter;
    int currentPos;
    float healthTemp;

    public override void Start()
    {
        base.Start();
        Init();
    }
    public void CalculateHealthAllGun()
    {
        healthTemp = currentHealth - gunCenter.currentHealth;

        //if (gunList.Count > 0)
        //{
            for (int i = 0; i < gunList.Count; i++)
            {
                gunList[i].currentHealth = healthTemp / (gunList.Count);
            }
        //}
        //else
        //{
        //    GameController.instance.autoTarget.Add(gunCenter);
        //    //  GameController.instance.NotSoFastWin();
        //    gunCenter.currentHealth = currentHealth;
        //    gunCenter.gameObject.SetActive(true);
        //}
    }
    public void CalculateAgainHealthAllGun()
    {
        if (gunList.Count > 0)
            return;
        GameController.instance.autoTarget.Add(gunCenter);
        //  GameController.instance.NotSoFastWin();
        gunCenter.currentHealth = currentHealth;
        gunCenter.gameObject.SetActive(true);
    }
    public override void Init()
    {
        base.Init();
        currentPos = Random.Range(0, CameraController.instance.posMove.Count);
        if (!EnemyManager.instance.miniboss2s.Contains(this))
        {
            EnemyManager.instance.miniboss2s.Add(this);
        }
        for (int i = 0; i < strboneGun.Length; i++)
        {
            boneGun[i] = skeletonAnimation.Skeleton.FindBone(strboneGun[i]);
            gunList[i].transform.position = boneGun[i].GetWorldPosition(skeletonAnimation.transform);
            gunList[i].index = i;
            gunList[i].incam = true;
        }
        PlayAnim(0, aec.idle, false);
        gunCenter.incam = true;
        gunCenter.currentHealth = currentHealth / 100 * 60;
        gunCenter.gameObject.SetActive(false);
        CalculateHealthAllGun();
        haveGun1 = haveGun2 = true;
        // takeDamageBox.enabled = false;
    }
    public override void Active()
    {
        base.Active();
        for (int i = 0; i < gunList.Count; i++)
            GameController.instance.autoTarget.Add(gunList[i]);
    }
    float timechangePos;
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
                break;
            case EnemyState.falldown:
                if (timechangePos > 0)
                {
                    timechangePos -= deltaTime;
                    if (timechangePos <= 0)
                    {
                        enemyState = EnemyState.run;
                        if (randomCombo == 1)
                        {
                            effectLaze.SetActive(false);
                            boxAttack1.gameObject.SetActive(false);
                        }
                    }
                    if (randomCombo == 1)
                    {
                        if (!boxAttack1.gameObject.activeSelf)
                        {
                            timePreviousAttack -= deltaTime;
                            if (timePreviousAttack <= 0)
                            {
                                timePreviousAttack = maxtimeDelayAttack2;
                                boxAttack1.gameObject.SetActive(true);
                            }
                        }
                    }
                }
                break;
            case EnemyState.attack:
                if (randomCombo == 0)
                {
                    if (timePreviousAttack > 0)
                    {
                        timePreviousAttack -= deltaTime;
                        if (timePreviousAttack <= 0)
                        {
                            timePreviousAttack = maxtimeDelayAttack1;
                            for (int i = 0; i < gunList.Count; i++)
                            {
                                if (gunList[i].index != 0 && gunList[i].index != 3)
                                {
                                    PlayAnim(gunList[i].index + 1, shotguns[gunList[i].index]);
                                    CreateBullet(gunList[i].transform.position);
                                }
                            }
                            enemyState = EnemyState.falldown;
                        }
                    }
                }
                else
                {
                    if (!boxAttack1.gameObject.activeSelf)
                    {
                        timePreviousAttack -= deltaTime;
                        if (timePreviousAttack <= 0)
                        {
                            timePreviousAttack = maxtimeDelayAttack2;
                            boxAttack1.gameObject.SetActive(true);
                        }
                    }
                    transform.position = Vector2.MoveTowards(transform.position, posTemp, deltaTime * speed / 2);
                    if (transform.position.x == posTemp.x && transform.position.y == posTemp.y)
                    {
                        enemyState = EnemyState.falldown;
                    }
                }
                break;
            case EnemyState.run:
                transform.position = Vector2.MoveTowards(transform.position, CameraController.instance.posMove[currentPos].position, deltaTime * speed / 2);
                //    CheckDirFollowPlayer(CameraController.instance.posMove[currentPos].position.x);
                if (transform.position.x == CameraController.instance.posMove[currentPos].position.x && transform.position.y == CameraController.instance.posMove[currentPos].position.y)
                {
                    //  CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());
                    currentPos = Random.Range(0, CameraController.instance.posMove.Count);
                    timechangePos = maxtimedelayChangePos;
                    if (gunList.Count == 0)
                    {
                        randomCombo = 1;
                    }
                    else
                    {
                        randomCombo = Random.Range(0, 2);
                    }

                    if (randomCombo == 0)
                    {
                        timePreviousAttack = maxtimeDelayAttack1;

                        if (haveGun1 && haveGun2)
                        {
                            PlayAnim(1, shotguns[0]);
                            PlayAnim(4, shotguns[3]);
                            CreateBullet(gunList[0].transform.position);
                            CreateBullet(gunList[3].transform.position);
                            enemyState = EnemyState.attack;
                        }
                        else if (haveGun1 && !haveGun2)
                        {
                            PlayAnim(1, shotguns[0]);
                            CreateBullet(gunList[0].transform.position);
                            enemyState = EnemyState.attack;
                        }
                        else if (!haveGun1 && haveGun2)
                        {
                            PlayAnim(4, shotguns[3]);
                            CreateBullet(gunList[3].transform.position);
                            enemyState = EnemyState.attack;
                        }
                        else if (!haveGun1 && !haveGun2)
                        {
                            for (int i = 0; i < gunList.Count; i++)
                            {
                                if (gunList[i].index != 0 && gunList[i].index != 3)
                                {
                                    PlayAnim(gunList[i].index + 1, shotguns[gunList[i].index]);
                                    CreateBullet(gunList[i].transform.position);
                                }
                            }
                            enemyState = EnemyState.falldown;
                        }
                    }
                    else
                    {
                        timePreviousAttack = maxtimeDelayAttack2;
                        posTemp.x = PlayerController.instance.GetTranformXPlayer();
                        posTemp.y = CameraController.instance.transform.position.y + 2;
                        effectLaze.SetActive(true);
                        enemyState = EnemyState.attack;
                    }
                }
                break;
        }
    }
    Vector2 posTemp;
    public void PlayAnim(int index, AnimationReferenceAsset _anim)
    {
        skeletonAnimation.AnimationState.SetAnimation(index, _anim, false);
    }
    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);
    }
    protected override void OnComplete(TrackEntry trackEntry)
    {
        base.OnComplete(trackEntry);
        if (trackEntry.Animation.Name.Equals(aec.idle.name))
        {
            enemyState = EnemyState.run;
            PlayAnim(0, aec.run, true);
        }
    }
    public void CreateBullet(Vector2 pos)
    {
        bullet = ObjectPoolManagerHaveScript.Instance.bulletMiniBoss2Pooler.GetBulletEnemyPooledObject();
        bullet.AddProperties(damage1, bulletspeed1);
        bullet.SetTimeExist(bulletimeexist);
        bullet.BeginDisplay(Vector2.zero, this);
        listMyBullet.Add(bullet);
        bullet.transform.position = pos;
        bullet.gameObject.SetActive(true);

    }
    BulletEnemy bullet;

    public override void Dead()
    {
        base.Dead();
        for (int i = 0; i < gunList.Count; i++)
        {
            if (GameController.instance.autoTarget.Contains(gunList[i]))
            {
                GameController.instance.autoTarget.Remove(gunList[i]);
            }
        }
        if (GameController.instance.autoTarget.Contains(gunCenter))
        {
            GameController.instance.autoTarget.Remove(gunCenter);
        }
        boxAttack1.gameObject.SetActive(false);
        effectLaze.SetActive(false);
    }
    public override void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance == null)
            return;
        if (EnemyManager.instance.miniboss2s.Contains(this))
        {
            EnemyManager.instance.miniboss2s.Remove(this);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

}
