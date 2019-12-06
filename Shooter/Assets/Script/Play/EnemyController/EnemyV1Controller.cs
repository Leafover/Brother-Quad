using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class EnemyV1Controller : EnemyBase
{
    float timedelayChangePos;
    Vector2 nextPos;
    public void Start()
    {
        base.Start();
        Init();
    }
    public override void Init()
    {
        base.Init();
        timedelayChangePos = maxtimedelayChangePos;
        if (!EnemyManager.instance.enemyv1s.Contains(this))
        {
            EnemyManager.instance.enemyv1s.Add(this);
        }
    }
    public override void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.enemyv1s.Contains(this))
        {
            EnemyManager.instance.enemyv1s.Remove(this);
        }
    }
    public override void Active()
    {
        base.Active();
        enemyState = EnemyState.attack;
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
        if (tempXBegin > Camera.main.transform.position.x + 7f)
        {
            return;
        }
        switch (enemyState)
        {
            case EnemyState.attack:
                Shoot(0, aec.attack1, false, maxtimeDelayAttack);
                if (!canmove)
                    return;

                timedelayChangePos -= deltaTime;
                if (timedelayChangePos <= 0)
                {
                    enemyState = EnemyState.run;
                    timedelayChangePos = maxtimedelayChangePos;
                    if (transform.position.x < OriginPos.x)
                    {
                        nextPos.x = OriginPos.x + Random.Range(1f, 3f);
                        PlayAnim(0, aec.run2, true);
                    }
                    else
                    {
                        nextPos.x = OriginPos.x + Random.Range(-1f, -3f);
                        PlayAnim(0, aec.run, true);
                    }
                    nextPos.y = OriginPos.y;
                    //  Debug.Log(nextPos + ":" + OriginPos);
                }

                break;
            case EnemyState.run:

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

        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            if (!incam)
                return;
            GameObject bullet = ObjectPoolerManager.Instance.bulletEnemyV1Pooler.GetPooledObject();
            bullet.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            bullet.transform.eulerAngles = new Vector3(0, 0, 150);
            bullet.SetActive(true);
        }
    }
    protected override void OnComplete(TrackEntry trackEntry)
    {
        base.OnComplete(trackEntry);
        if(trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            PlayAnim(0, aec.idle, true);
        }
    }
}
