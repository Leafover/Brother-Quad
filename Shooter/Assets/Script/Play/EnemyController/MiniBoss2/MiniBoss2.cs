using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss2 : EnemyBase
{
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

        if (gunList.Count > 0)
        {
            for (int i = 0; i < gunList.Count; i++)
            {
                gunList[i].currentHealth = healthTemp / (gunList.Count);
            }
        }
        else
        {
            GameController.instance.autoTarget.Add(gunCenter);
            GameController.instance.NotSoFastWin();
            gunCenter.currentHealth = currentHealth;
            gunCenter.gameObject.SetActive(true);
        }
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
            case EnemyState.attack:

                if (randomCombo == 0)
                {
                    timechangePos -= deltaTime;
                    if (timechangePos <= 0)
                    {
                        enemyState = EnemyState.run;
                    }
                }
                else
                {

                }
                break;
            case EnemyState.run:
                transform.position = Vector2.MoveTowards(transform.position, CameraController.instance.posMove[currentPos].position, deltaTime * speed);
                //    CheckDirFollowPlayer(CameraController.instance.posMove[currentPos].position.x);
                if (transform.position.x == CameraController.instance.posMove[currentPos].position.x && transform.position.y == CameraController.instance.posMove[currentPos].position.y)
                {
                    //  CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());
                    currentPos = Random.Range(0, CameraController.instance.posMove.Count);
                    enemyState = EnemyState.attack;
                    randomCombo = 0 /*Random.Range(0, 2)*/;
                    timechangePos = maxtimedelayChangePos;

                    if (gunList[0].gameObject.activeSelf && gunList[3].gameObject.activeSelf)
                    {
                        PlayAnim(1, shotguns[0]);
                        PlayAnim(4, shotguns[3]);
                        StartCoroutine(delayAnimShot());
                    }
                    else if (gunList[0].gameObject.activeSelf && !gunList[3].gameObject.activeSelf)
                    {
                        PlayAnim(1, shotguns[0]);
                        StartCoroutine(delayAnimShot());
                    }
                    else if (!gunList[0].gameObject.activeSelf && gunList[3].gameObject.activeSelf)
                    {
                        PlayAnim(4, shotguns[3]);
                        StartCoroutine(delayAnimShot());
                    }
                    else if (!gunList[0].gameObject.activeSelf && !gunList[3].gameObject.activeSelf)
                    {
                        if (gunList[1].gameObject.activeSelf)
                            PlayAnim(2, shotguns[1]);
                        if (gunList[2].gameObject.activeSelf)
                            PlayAnim(3, shotguns[2]);
                        if (gunList[4].gameObject.activeSelf)
                            PlayAnim(5, shotguns[4]);
                        if (gunList[5].gameObject.activeSelf)
                            PlayAnim(6, shotguns[5]);
                        if (gunList[6].gameObject.activeSelf)
                            PlayAnim(7, shotguns[6]);
                        if (gunList[7].gameObject.activeSelf)
                            PlayAnim(8, shotguns[7]);
                    }
                }
                break;
        }
    }
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
        else if (trackEntry.Animation.Name.Equals(shotguns[0].name))
        {
            CreateBullet(0);


        }
        else if (trackEntry.Animation.Name.Equals(shotguns[3].name))
        {
            CreateBullet(3);
        }
        else if (trackEntry.Animation.Name.Equals(shotguns[1].name))
        {
            CreateBullet(1);
        }
        else if (trackEntry.Animation.Name.Equals(shotguns[2].name))
        {
            CreateBullet(2);
        }
        else if (trackEntry.Animation.Name.Equals(shotguns[4].name))
        {
            CreateBullet(4);
        }
        else if (trackEntry.Animation.Name.Equals(shotguns[5].name))
        {
            CreateBullet(5);
        }
        else if (trackEntry.Animation.Name.Equals(shotguns[6].name))
        {
            CreateBullet(6);
        }
        else if (trackEntry.Animation.Name.Equals(shotguns[7].name))
        {
            CreateBullet(7);
        }
    }
    void CreateBullet(int index)
    {
        bullet = ObjectPoolManagerHaveScript.Instance.bulletMiniBoss2Pooler.GetBulletEnemyPooledObject();
        bullet.AddProperties(damage1, bulletspeed1);
        bullet.SetTimeExist(bulletimeexist);
        bullet.BeginDisplay(Vector2.zero, this);
        listMyBullet.Add(bullet);
        bullet.transform.position = gunList[index].transform.position;
        bullet.gameObject.SetActive(true);
        // Debug.LogError("zooooooooo");
    }
    BulletEnemy bullet;
    IEnumerator delayAnimShot()
    {
        yield return new WaitForSeconds(1);
        if (gunList[1].gameObject.activeSelf)
            PlayAnim(2, shotguns[1]);
        if (gunList[2].gameObject.activeSelf)
            PlayAnim(3, shotguns[2]);
        if (gunList[4].gameObject.activeSelf)
            PlayAnim(5, shotguns[4]);
        if (gunList[5].gameObject.activeSelf)
            PlayAnim(6, shotguns[5]);
        if (gunList[6].gameObject.activeSelf)
            PlayAnim(7, shotguns[6]);
        if (gunList[7].gameObject.activeSelf)
            PlayAnim(8, shotguns[7]);
    }
    IEnumerator delayChangeStage()
    {
        yield return new WaitForSeconds(1.5f);
        enemyState = EnemyState.run;
    }
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
