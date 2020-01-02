using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVN2Controller : EnemyBase
{
    int currentPos;
    public Transform gunRotation;
    public override void Start()
    {
        base.Start();
        Init();
    }
    public override void Init()
    {
        base.Init();
        currentPos = Random.Range(0, CameraController.instance.posEnemyV2.Count);
        randomCombo = Random.Range(2, 4);
        if (!EnemyManager.instance.enemyvn2s.Contains(this))
        {
            EnemyManager.instance.enemyvn2s.Add(this);
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.enemyvn2s.Contains(this))
        {
            EnemyManager.instance.enemyvn2s.Remove(this);
        }
    }
    public override void Active()
    {
        base.Active();
        enemyState = EnemyState.run;
        SoundController.instance.PlaySound(soundGame.soundmissilewarning);
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

        if (canoutcam && !incam)
            return;

        switch (enemyState)
        {
            case EnemyState.run:
                PlayAnim(0, aec.run, true);
                transform.position = Vector2.MoveTowards(transform.position, CameraController.instance.posEnemyV2[currentPos].position, deltaTime * speed);
                CheckDirFollowPlayer(CameraController.instance.posEnemyV2[currentPos].position.x);
                if (transform.position.x == CameraController.instance.posEnemyV2[currentPos].position.x && transform.position.y == CameraController.instance.posEnemyV2[currentPos].position.y)
                {
                    CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());
                    enemyState = EnemyState.attack;
                    if (currentPos == 0)
                        currentPos = 1;
                    else
                        currentPos = 0;
                }
                break;
            case EnemyState.attack:
                Attack(1, aec.attack1, false, maxtimeDelayAttack1);
                break;
        }
    }
    //   GameObject g;
    //protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    //{
    //    base.OnEvent(trackEntry, e);
    //    if (trackEntry.Animation.Name.Equals(aec.attack1.name))
    //    {
    //        if (!incam)
    //            return;

    //        bulletEnemy = ObjectPoolManagerHaveScript.Instance.rocketEnemyVN2Pooler.GetBulletEnemyPooledObject();
    //        bulletEnemy.AddProperties(damage1, bulletspeed1);
    //        bulletEnemy.SetTimeExist(bulletimeexist);
    //        bulletEnemy.BeginDisplay(Vector2.zero, this);
    //        listMyBullet.Add(bulletEnemy);
    //        bulletEnemy.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
    //        bulletEnemy.transform.rotation = Quaternion.identity;
    //        bulletEnemy.transform.rotation = gunRotation.rotation;
    //        bulletEnemy.gameObject.SetActive(true);
    //        SoundController.instance.PlaySound(soundGame.soundv2attack);
    //        Debug.LogError("-----event----");
    //        // SoundController.instance.PlaySound(soundGame.soundmissilewarning);
    //    }
    //}
    protected override void OnComplete(TrackEntry trackEntry)
    {
        base.OnComplete(trackEntry);


        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            combo++;
            if (combo == randomCombo)
            {
                combo = 0;
                enemyState = EnemyState.run;
                randomCombo = Random.Range(2, 4);
            }
            if (!incam)
                return;

            bulletEnemy = ObjectPoolManagerHaveScript.Instance.rocketEnemyVN2Pooler.GetBulletEnemyPooledObject();
            bulletEnemy.AddProperties(damage1, bulletspeed1);
            bulletEnemy.SetTimeExist(bulletimeexist);
            bulletEnemy.BeginDisplay(Vector2.zero, this);
            listMyBullet.Add(bulletEnemy);
            bulletEnemy.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            bulletEnemy.transform.rotation = Quaternion.identity;
            bulletEnemy.transform.rotation = gunRotation.rotation;
            bulletEnemy.gameObject.SetActive(true);
            SoundController.instance.PlaySound(soundGame.soundv2attack);
        }
    }
    public override void Dead()
    {
        base.Dead();
        SoundController.instance.PlaySound(soundGame.soundv2die);
    }
}
