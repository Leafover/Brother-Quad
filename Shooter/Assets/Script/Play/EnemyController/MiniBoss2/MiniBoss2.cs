using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss2 : EnemyBase
{
    public List<GameObject> effectLaze;
    public bool haveGun1, haveGun2;
    public AnimationReferenceAsset die1Anim;
    public List<AnimationReferenceAsset> shotguns, dieguns;
    Bone[] boneGun = new Bone[9];
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

        for (int i = 0; i < gunList.Count; i++)
        {
            gunList[i].currentHealth = healthTemp / (gunList.Count);
        }
    }
    public void CalculateAgainHealthAllGun()
    {
        if (gunList.Count > 0)
        {
            StartCoroutine(delayAddGun());
            return;
        }
        GameController.instance.autoTarget.Add(gunCenter);
        //  GameController.instance.NotSoFastWin();
        gunCenter.currentHealth = currentHealth;
        gunCenter.gameObject.SetActive(true);
    }
    IEnumerator delayAddGun()
    {
        yield return new WaitForSeconds(0.5f);
        if (!gunList[0].gameObject.activeSelf)
        {
            gunList[0].gameObject.SetActive(true);
            GameController.instance.autoTarget.Add(gunList[0]);
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

            if (i < strboneGun.Length - 1)
            {
                gunList[i].transform.position = boneGun[i].GetWorldPosition(skeletonAnimation.transform);
                gunList[i].index = i;
                gunList[i].incam = true;
            }
        }
        gunCenter.transform.position = boneGun[8].GetWorldPosition(skeletonAnimation.transform);
        PlayAnim(0, aec.idle, false);
        gunCenter.incam = true;
        gunCenter.currentHealth = currentHealth / 100 * 60;
        //   gunCenter.gameObject.SetActive(false);
        CalculateHealthAllGun();
        haveGun1 = haveGun2 = true;
        // takeDamageBox.enabled = false;
    }
    public override void Active()
    {
        base.Active();
        SoundController.instance.PlaySound(soundGame.soundDisplayMiniBoss2);
        // for (int i = 0; i < gunList.Count; i++)
        gunList[0].gameObject.SetActive(true);
        GameController.instance.autoTarget.Add(gunList[0]);
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
                            if (combo == 1)
                                return;
                            effectLaze[0].SetActive(false);
                            effectLaze[1].SetActive(false);
                            effectLaze[2].SetActive(false);
                            boxAttack1.gameObject.SetActive(false);
                            boxAttack2.gameObject.SetActive(false);
                            boxAttack3.gameObject.SetActive(false);
                        }
                    }
                    if (randomCombo == 1)
                    {
                        if (combo == 1)
                            return;

                        if (!boxAttack1.gameObject.activeSelf)
                        {
                            timePreviousAttack -= deltaTime;
                            if (timePreviousAttack <= 0)
                            {
                                timePreviousAttack = maxtimeDelayAttack2;
                                boxAttack1.gameObject.SetActive(true);
                                if (effectLaze[1].activeSelf)
                                    boxAttack2.gameObject.SetActive(true);
                                if (effectLaze[2].activeSelf)
                                    boxAttack3.gameObject.SetActive(true);
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
                            SoundController.instance.PlaySound(soundGame.soundMiniBoss2Attack1);
                            enemyState = EnemyState.falldown;
                        }
                    }
                }
                else
                {
                    if (combo == 0)
                    {
                        if (!boxAttack1.gameObject.activeSelf)
                        {
                            timePreviousAttack -= deltaTime;
                            if (timePreviousAttack <= 0)
                            {
                                timePreviousAttack = maxtimeDelayAttack2;
                                boxAttack1.gameObject.SetActive(true);
                                if (effectLaze[1].activeSelf)
                                    boxAttack2.gameObject.SetActive(true);
                                if (effectLaze[2].activeSelf)
                                    boxAttack3.gameObject.SetActive(true);
                            }
                        }
                        transform.position = Vector2.MoveTowards(transform.position, posTemp, deltaTime * speed / 2);
                        if (transform.position.x == posTemp.x /*&& transform.position.y == posTemp.y*/)
                        {
                            enemyState = EnemyState.falldown;
                        }
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
                            CreateBullet(boneGun[0].GetWorldPosition(skeletonAnimation.transform));
                            CreateBullet(boneGun[3].GetWorldPosition(skeletonAnimation.transform));
                            enemyState = EnemyState.attack;
                            SoundController.instance.PlaySound(soundGame.soundMiniBoss2Attack1);
                        }
                        else if (haveGun1 && !haveGun2)
                        {
                            PlayAnim(1, shotguns[0]);
                            CreateBullet(boneGun[0].GetWorldPosition(skeletonAnimation.transform));
                            enemyState = EnemyState.attack;
                            SoundController.instance.PlaySound(soundGame.soundMiniBoss2Attack1);
                        }
                        else if (!haveGun1 && haveGun2)
                        {
                            PlayAnim(4, shotguns[3]);
                            CreateBullet(boneGun[3].GetWorldPosition(skeletonAnimation.transform));
                            enemyState = EnemyState.attack;
                            SoundController.instance.PlaySound(soundGame.soundMiniBoss2Attack1);
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
                            SoundController.instance.PlaySound(soundGame.soundMiniBoss2Attack1);
                        }
                    }
                    else
                    {

                        if (gunList.Count > 0)
                        {
                            effectLaze[0].SetActive(true);
                            SoundController.instance.PlaySound(soundGame.soundMiniBoss2Attack2);
                            posTemp.x = PlayerController.instance.GetTranformXPlayer();
                            posTemp.y = transform.position.y;
                            enemyState = EnemyState.attack;
                        }
                        else
                        {

                            combo = Random.Range(0, 2);
                            timePreviousAttack = 0;
                            if (combo == 0)
                            {
                                effectLaze[0].SetActive(true);
                                effectLaze[1].SetActive(true);
                                effectLaze[2].SetActive(true);
                                SoundController.instance.PlaySound(soundGame.soundMiniBoss2Attack2);
                                posTemp.x = PlayerController.instance.GetTranformXPlayer();
                                posTemp.y = Camera.main.transform.position.y;
                                enemyState = EnemyState.attack;
                            }
                            else
                            {
                                for (int i = 0; i < effectLaze.Count; i++)
                                {
                                    bulletEnemy = ObjectPoolManagerHaveScript.Instance.superBulletMiniBoss2Pooler.GetBulletEnemyPooledObject();
                                    bulletEnemy.AddProperties(damage1, bulletspeed1);
                                    bulletEnemy.transform.eulerAngles = effectLaze[i].transform.eulerAngles;
                                    listMyBullet.Add(bulletEnemy);
                                    bulletEnemy.transform.position = gunCenter.transform.position;
                                    bulletEnemy.gameObject.SetActive(true);
                                }
                                SoundController.instance.PlaySound(soundGame.soundMiniBoss2Attack1);
                                enemyState = EnemyState.falldown;
                            }
                        }



                    }
                }
                break;
        }
    }
    //  Vector3 rotationbullet = new Vector3(0, 0, 150);
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
        bulletEnemy = ObjectPoolManagerHaveScript.Instance.bulletMiniBoss2Pooler.GetBulletEnemyPooledObject();
        bulletEnemy.AddProperties(damage1, bulletspeed1);
        bulletEnemy.SetTimeExist(bulletimeexist);
        bulletEnemy.BeginDisplay(Vector2.zero, this);
        listMyBullet.Add(bulletEnemy);
        bulletEnemy.transform.position = pos;
        bulletEnemy.gameObject.SetActive(true);

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
        if (GameController.instance.autoTarget.Contains(gunCenter))
        {
            GameController.instance.autoTarget.Remove(gunCenter);
        }
        boxAttack1.gameObject.SetActive(false);
        boxAttack2.gameObject.SetActive(false);
        boxAttack3.gameObject.SetActive(false);
        effectLaze[0].SetActive(false);
        effectLaze[1].SetActive(false);
        effectLaze[2].SetActive(false);
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
