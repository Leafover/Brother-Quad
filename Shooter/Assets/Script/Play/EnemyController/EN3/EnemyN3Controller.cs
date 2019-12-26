using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyN3Controller : EnemyBase
{

    bool isGrenadeStage;
    public override void Start()
    {
        base.Start();
        Init();
    }
    public override void Init()
    {
        base.Init();
        randomCombo = Random.Range(3, 4);
        isGrenadeStage = false;
        //   timedelayShoot = maxtimeDelayAttack;
        if (!EnemyManager.instance.enemyn3s.Contains(this))
        {
            EnemyManager.instance.enemyn3s.Add(this);

        }
    }
    public override void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.enemyn3s.Contains(this))
        {
            EnemyManager.instance.enemyn3s.Remove(this);
        }
        // Debug.LogError("tu nhien bien mat");
    }

    IEnumerator delayActive()
    {
        yield return new WaitForSeconds(0.1f);
        enemyState = EnemyState.attack;
    }
    public override void Active()
    {
        base.Active();
        enemyState = EnemyState.idle;
        StartCoroutine(delayActive());
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

        if (tempXBegin > Camera.main.transform.position.x + 7.5f)
        {
            return;
        }

        if (enemyState != EnemyState.attack)
            return;

        CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());
        if (!canmove)
        {
            if (isGrenadeStage)
            {
                Attack(0, aec.attack1, false, maxtimeDelayAttack1);
                targetPos.transform.position = GetTarget(true);
            }
            else
            {

                Attack(0, aec.attack2, false, maxtimeDelayAttack2);
                targetPos.transform.position = GetTarget(false);
            }
        }
    }
    //  GameObject bullet;
    //  GameObject grenade;
    Vector2 dirBullet;
    float angle;
    Quaternion rotation;
    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);
        if (trackEntry.Animation.Name.Equals(aec.attack2.name))
        {
            combo++;
            if (!incam)
                return;
            //bullet = ObjectPoolerManager.Instance.bulletEnemy4Pooler.GetPooledObject();
            //var bulletScript = bullet.GetComponent<BulletEnemy>();
            //bulletScript.AddProperties(damage2, bulletspeed1);
            //dirBullet = (Vector2)targetPos.transform.position - (Vector2)boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            //angle = Mathf.Atan2(dirBullet.y, dirBullet.x) * Mathf.Rad2Deg;
            //rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //bullet.transform.rotation = rotation;
            //bullet.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            //bullet.SetActive(true);


            bulletEnemy = ObjectPoolManagerHaveScript.Instance.bulletN3Pooler.GetBulletEnemyPooledObject();
            bulletEnemy.AddProperties(damage1, bulletspeed1);
            dirBullet = (Vector2)targetPos.transform.position - (Vector2)boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            angle = Mathf.Atan2(dirBullet.y, dirBullet.x) * Mathf.Rad2Deg;
            rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            bulletEnemy.transform.rotation = rotation;
            bulletEnemy.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            bulletEnemy.gameObject.SetActive(true);


        }
        else if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            combo++;
            if (!incam)
                return;


            //grenade = ObjectPoolerManager.Instance.grenadeEnemy4Pooler.GetPooledObject();
            //grenade.transform.position = boneBarrelGun1.GetWorldPosition(skeletonAnimation.transform);
            //var grenadeScript = grenade.GetComponent<BulletEnemy>();
            //grenadeScript.AddProperties(0, 6);

            bulletEnemy = ObjectPoolManagerHaveScript.Instance.grenadeN3Pooler.GetBulletEnemyPooledObject();
            bulletEnemy.transform.position = boneBarrelGun1.GetWorldPosition(skeletonAnimation.transform);
            bulletEnemy.AddProperties(damage2, 6);
            if (FlipX)
                bulletEnemy.SetDir(-6, false);
            //grenadeScript.SetDir(-6,false);
            else
                bulletEnemy.SetDir(6, false);
            //  grenadeScript.SetDir(6,false);

            //    grenade.SetActive(true);
            bulletEnemy.gameObject.SetActive(true);
        }

    }
    protected override void OnComplete(TrackEntry trackEntry)
    {
        base.OnComplete(trackEntry);

        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            PlayAnim(0, aec.idle, true);
            if (combo == 1)
            {
                combo = 0;
                randomCombo = Random.Range(3, 4);
                isGrenadeStage = false;
            }

        }

        else if (trackEntry.Animation.Name.Equals(aec.attack2.name))
        {
            PlayAnim(0, aec.idle, true);

            if (combo == randomCombo)
            {
                combo = 0;
                isGrenadeStage = true;
            }
        }


    }


    public override void Dead()
    {
        base.Dead();
        SoundController.instance.PlaySound(soundGame.soundEN3die);
    }
}
