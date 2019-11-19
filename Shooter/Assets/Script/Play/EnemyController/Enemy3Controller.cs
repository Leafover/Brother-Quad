using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class Enemy3Controller : EnemyBase
{
    float timedelayAttack, timedelayChangePos;
    Vector2 nextPos;

    private void Start()
    {
        base.Start();
        //if (!EnemyManager.instance.enemy3s.Contains(this))
        //{
        //    EnemyManager.instance.enemy3s.Add(this);
        //}
        timedelayChangePos = maxtimedelayChangePos;
    }
    public override void AcBecameVisibleCam()
    {
        base.AcBecameVisibleCam();
        if (!EnemyManager.instance.enemy3s.Contains(this))
        {
            EnemyManager.instance.enemy3s.Add(this);
            isActive = true;
        }
    }
    private void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.enemy3s.Contains(this))
        {
            EnemyManager.instance.enemy3s.Remove(this);
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (!isActive)
            return;


        //    Debug.Log(":----------" + targetPos.transform.position);
        var deltaTime = Time.deltaTime;
        switch (enemyState)
        {
            case EnemyState.attack:
                Shoot(0, aec.attack1, false);
                CheckDirFollowPlayer(PlayerController.instance.GetTranformPlayer());
                targetPos.transform.position = GetTarget(false);
                //        Debug.Log(":----------" + targetPos.transform.position);
                if (!canmove)
                    return;

                timedelayChangePos -= deltaTime;
                if (timedelayChangePos <= 0)
                {
                    enemyState = EnemyState.run;
                    timedelayChangePos = maxtimedelayChangePos;
                    if (transform.position.x < OriginPos.x)
                        nextPos.x = OriginPos.x + Random.Range(1f, 3f);
                    else
                        nextPos.x = OriginPos.x + Random.Range(-1f, -3f);
                    nextPos.y = OriginPos.y;
                    CheckDirFollowPlayer(nextPos.x);

                  //  Debug.Log(nextPos + ":" + OriginPos);
                }

                break;
            case EnemyState.run:
                PlayAnim(0, aec.run, true);
                targetPos.transform.position = GetTarget(true);
                transform.position = Vector2.MoveTowards(transform.position, nextPos, deltaTime * speed);

                if (transform.position.x == nextPos.x && transform.position.y == nextPos.y)
                {
                    //  OriginPos = nextPos;
                    enemyState = EnemyState.attack;
                   // Debug.LogError("zo day");
                }
                break;
        }
    }

    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            GameObject bullet = ObjectPoolerManager.Instance.bulletEnemy3Pooler.GetPooledObject();
            Vector2 dirBullet = (Vector2)targetPos.transform.position - (Vector2)boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            float angle = Mathf.Atan2(dirBullet.y, dirBullet.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            bullet.transform.rotation = rotation;
            bullet.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            bullet.SetActive(true);
            Debug.LogError("----------- ngon");
        }
    }

}
