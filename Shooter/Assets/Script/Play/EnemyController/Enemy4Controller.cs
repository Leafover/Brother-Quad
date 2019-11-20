using System.Collections;
using System.Collections.Generic;
using Spine;
using UnityEngine;
using Spine.Unity;

public class Enemy4Controller : EnemyBase
{
    float timedelayChangePos,timedelayShoot;
    Vector2 nextPos;
    bool isGrenadeStage;
    int randomCountGrenade, countGrenade;
    private void Start()
    {
        base.Start();
        timedelayChangePos = maxtimedelayChangePos;
        randomCountGrenade = Random.Range(1, 3);
        isGrenadeStage = true;
        timedelayShoot = maxtimeDelayAttack;
    }
    public override void AcBecameVisibleCam()
    {
        base.AcBecameVisibleCam();
        if (!EnemyManager.instance.enemy4s.Contains(this))
        {
            EnemyManager.instance.enemy4s.Add(this);
            isActive = true;
        }
    }
    private void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.enemy4s.Contains(this))
        {
            EnemyManager.instance.enemy4s.Remove(this);
        }
    }


    public override void OnUpdate()
    {
        base.OnUpdate();

        if (!isActive)
            return;

        var deltaTime = Time.deltaTime;

        switch (enemyState)
        {
            case EnemyState.attack:
                CheckDirFollowPlayer(PlayerController.instance.GetTranformPlayer());
                if (!canmove)
                {
                    if (isGrenadeStage)
                    {
                        Shoot(0, aec.attack1, false, timedelayShoot);
                        targetPos.transform.position = GetTarget(true);
                        if (countGrenade == randomCountGrenade)
                        {
                            countGrenade = 0;
                            randomCountGrenade = Random.Range(1, 3);
                            isGrenadeStage = false;
                            timedelayShoot = maxtimeDelayAttack / 2;
                        }
                    }
                    else
                    {
                        Shoot(0, aec.attack2, false, timedelayShoot);
                        targetPos.transform.position = GetTarget(false);
                        timedelayChangePos -= deltaTime;
                        if (timedelayChangePos <= 0)
                        {
                            isGrenadeStage = true;
                            timedelayShoot = maxtimeDelayAttack;
                        }
                    }
                    return;
                }


                if (isGrenadeStage)
                {
                    Shoot(0, aec.attack1, false, timedelayShoot);
                    targetPos.transform.position = GetTarget(true);
                    if (countGrenade == randomCountGrenade)
                    {
                        enemyState = EnemyState.run;
                        timedelayChangePos = maxtimedelayChangePos;
                        if (transform.position.x < OriginPos.x)
                            nextPos.x = OriginPos.x + Random.Range(1f, 2f);
                        else
                            nextPos.x = OriginPos.x + Random.Range(-1f, -2f);
                        nextPos.y = OriginPos.y;
                        CheckDirFollowPlayer(nextPos.x);
                        countGrenade = 0;
                        randomCountGrenade = Random.Range(1, 3);
                        isGrenadeStage = false;
                        timedelayShoot = maxtimeDelayAttack / 2;
                    }
                }
                else
                {
                    Shoot(0, aec.attack2, false, timedelayShoot);
                    targetPos.transform.position = GetTarget(false);
                    timedelayChangePos -= deltaTime;
                    if (timedelayChangePos <= 0)
                    {
                        enemyState = EnemyState.run;
                        timedelayChangePos = maxtimedelayChangePos;
                        if (transform.position.x < OriginPos.x)
                            nextPos.x = OriginPos.x + Random.Range(1f, 2f);
                        else
                            nextPos.x = OriginPos.x + Random.Range(-1f, -2f);
                        nextPos.y = OriginPos.y;
                        CheckDirFollowPlayer(nextPos.x);
                        isGrenadeStage = true;
                        timedelayShoot = maxtimeDelayAttack;
                    }
                }

                break;
            case EnemyState.run:
                PlayAnim(0, aec.run, true);
                targetPos.transform.position = GetTarget(true);
                transform.position = Vector2.MoveTowards(transform.position, nextPos, deltaTime * speed);

                if (transform.position.x == nextPos.x && transform.position.y == nextPos.y)
                {
                    //  OriginPos = nextPos;
                    PlayAnim(0, aec.idle, true);
                    enemyState = EnemyState.attack;
                    // Debug.LogError("zo day");
                }
                break;
        }
    }

    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);
        //Debug.LogError("------------ aec.attack1.name:" + aec.attack1.name);
        //if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        //{
        //    GameObject grenade = ObjectPoolerManager.Instance.grenadeEnemy4Pooler.GetPooledObject();
        //    grenade.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
        //    if (FlipX)
        //        grenade.GetComponent<GrenadeEnemy>().dir = new Vector2(1, 1);
        //    else
        //        grenade.GetComponent<GrenadeEnemy>().dir = new Vector2(-1, 1);
        //    grenade.SetActive(true);
        //    Debug.Log("-------- nem lu dan");
        //}
        if (trackEntry.Animation.Name.Equals(aec.attack2.name))
        {
            GameObject bullet = ObjectPoolerManager.Instance.bulletEnemy4Pooler.GetPooledObject();
            Vector2 dirBullet = (Vector2)targetPos.transform.position - (Vector2)boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            float angle = Mathf.Atan2(dirBullet.y, dirBullet.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            bullet.transform.rotation = rotation;
            bullet.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            bullet.SetActive(true);
        }
    }
    protected override void OnComplete(TrackEntry trackEntry)
    {
        base.OnComplete(trackEntry);
        Debug.LogError("------------ aec.attack1.name:" + aec.attack1.name);
        if (trackEntry.Animation.name.Equals(aec.attack1.name))
        {
            GameObject grenade = ObjectPoolerManager.Instance.grenadeEnemy4Pooler.GetPooledObject();
            grenade.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            if (FlipX)
                grenade.GetComponent<GrenadeEnemy>().dir = new Vector2(1, 1);
            else
                grenade.GetComponent<GrenadeEnemy>().dir = new Vector2(-1, 1);
            grenade.SetActive(true);
            Debug.Log("-------- nem lu dan");
            countGrenade++;
        }
    }
}
