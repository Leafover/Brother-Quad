using System.Collections;
using System.Collections.Generic;
using Spine;
using UnityEngine;
using Spine.Unity;

public class Enemy4Controller : EnemyBase
{
    float timedelayChangePos, timedelayShoot;
    Vector2 nextPos;
    bool isGrenadeStage;
    public override void Start()
    {
        base.Start();
        Init();
    }
    public override void Init()
    {
        base.Init();
        timedelayChangePos = maxtimedelayChangePos;
        randomCombo = Random.Range(1, 3);
        isGrenadeStage = true;
        timedelayShoot = maxtimeDelayAttack;
        if (!EnemyManager.instance.enemy4s.Contains(this))
        {
            EnemyManager.instance.enemy4s.Add(this);

        }
    }
    public override void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.enemy4s.Contains(this))
        {
            EnemyManager.instance.enemy4s.Remove(this);
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

        if (tempXBegin > Camera.main.transform.position.x + 6f)
        {
            return;
        }

        switch (enemyState)
        {
            case EnemyState.attack:
                //   Debug.LogError("zoooooooo atack");
                CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());
                if (!canmove)
                {
                    if (isGrenadeStage)
                    {
                        Shoot(0, aec.attack1, false, timedelayShoot);
                        targetPos.transform.position = GetTarget(true);
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
                            nextPos.x = OriginPos.x + Random.Range(0.5f, 1f);
                        else
                            nextPos.x = OriginPos.x + Random.Range(-0.5f, -1f);
                        nextPos.y = transform.position.y;
                        CheckDirFollowPlayer(nextPos.x);
                        isGrenadeStage = true;
                        timedelayShoot = maxtimeDelayAttack;
                        skeletonAnimation.ClearState();

                        PlayAnim(0, aec.run, true);

                    }
                }

                break;
            case EnemyState.run:

                transform.position = Vector2.MoveTowards(transform.position, nextPos, deltaTime * speed);
                targetPos.transform.position = GetTarget(true);

                if (transform.position.x == nextPos.x /*&& transform.position.y == nextPos.y*/)
                {
                    //  OriginPos = nextPos;
                    PlayAnim(0, aec.idle, true);
                    enemyState = EnemyState.attack;
                    PlayAnim(2, aec.aimTargetAnim, false);
                    // Debug.LogError("zo day");
                }
                break;
        }
    }
    GameObject bullet;
    GameObject grenade;
    Vector2 dirBullet;
    float angle;
    Quaternion rotation;
    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);
        if (trackEntry.Animation.Name.Equals(aec.attack2.name))
        {
            if (!incam)
                return;
             bullet = ObjectPoolerManager.Instance.bulletEnemy4Pooler.GetPooledObject();
             dirBullet = (Vector2)targetPos.transform.position - (Vector2)boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
             angle = Mathf.Atan2(dirBullet.y, dirBullet.x) * Mathf.Rad2Deg;
             rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            bullet.transform.rotation = rotation;
            bullet.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            bullet.SetActive(true);
        }
        else if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            combo++;
            if (!incam)
                return;
             grenade = ObjectPoolerManager.Instance.grenadeEnemy4Pooler.GetPooledObject();
            grenade.transform.position = boneBarrelGun1.GetWorldPosition(skeletonAnimation.transform);
            if (FlipX)
                grenade.GetComponent<BulletEnemy>().dir = new Vector2(1, 0.7f);
            else
                grenade.GetComponent<BulletEnemy>().dir = new Vector2(-1, 0.7f);
            grenade.SetActive(true);

        }

    }
    protected override void OnComplete(TrackEntry trackEntry)
    {
        base.OnComplete(trackEntry);

        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            PlayAnim(0, aec.idle, true);
            if (combo == randomCombo)
            {
                if (canmove)
                {
                    enemyState = EnemyState.run;
                    timedelayChangePos = maxtimedelayChangePos;
                    if (transform.position.x < OriginPos.x)
                        nextPos.x = OriginPos.x + Random.Range(0.5f, 1f);
                    else
                        nextPos.x = OriginPos.x + Random.Range(-0.5f, -1f);
                    nextPos.y = OriginPos.y;
                    CheckDirFollowPlayer(nextPos.x);
                    PlayAnim(0, aec.run, true);
                }

                combo = 0;
                randomCombo = Random.Range(1, 3);
                isGrenadeStage = false;
                timedelayShoot = maxtimeDelayAttack / 2;
            }

        }

        if (trackEntry.Animation.Name.Equals(aec.attack2.name))
        {
            PlayAnim(0, aec.idle, true);
        }


    }
}
