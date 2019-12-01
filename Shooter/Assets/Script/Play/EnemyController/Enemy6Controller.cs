using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
public class Enemy6Controller : EnemyBase
{

    public Bone boneBody;
    [SpineBone]
    public string strboneBody;

    public float speedMove;
    public override void Start()
    {
        base.Start();
        Init();
        if (boneBody == null)
            boneBody = skeletonAnimation.Skeleton.FindBone(strboneBody);
    }
    public override void Init()
    {
        base.Init();
        if (!EnemyManager.instance.enemy6s.Contains(this))
        {
            EnemyManager.instance.enemy6s.Add(this);
        }
        speedMove = -speed;

        //   Debug.Log("----------------:" + speedMove);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.enemy6s.Contains(this))
        {
            EnemyManager.instance.enemy6s.Remove(this);
        }
    }
    public override void Active()
    {
        base.Active();
        PlayAnim(0, aec.run, true);
        enemyState = EnemyState.run;
       // Debug.LogError("-------active" + isActive);
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
            case EnemyState.run:
                rid.velocity = new Vector2(speedMove, rid.velocity.y);
                if (Mathf.Abs(transform.position.x - Camera.main.transform.position.x) <= 5)
                {
                    PlayAnim(0, aec.idle, false);
                    enemyState = EnemyState.attack;
                    rid.velocity = Vector2.zero;
                }
                break;
            case EnemyState.attack:
                CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());
                Shoot(0, aec.attack1, false, maxtimeDelayAttack);
                targetPos.position = GetTarget(false);
                break;
        }

    }

    GameObject bullet;
    Vector2 dirBullet;
    float angle;
    Quaternion rotation;
    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            if (!incam)
                return;

            bullet = ObjectPoolerManager.Instance.bulletEnemy6Pooler.GetPooledObject();
            dirBullet = (Vector2)targetPos.transform.position - (Vector2)boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            angle = Mathf.Atan2(dirBullet.y, dirBullet.x) * Mathf.Rad2Deg;
            rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            bullet.transform.rotation = rotation;
            bullet.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            bullet.SetActive(true);


            bullet = ObjectPoolerManager.Instance.bulletEnemy6Pooler.GetPooledObject();
            dirBullet = (Vector2)targetPos.transform.position - (Vector2)boneBarrelGun1.GetWorldPosition(skeletonAnimation.transform);
            angle = Mathf.Atan2(dirBullet.y, dirBullet.x) * Mathf.Rad2Deg;
            rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            bullet.transform.rotation = rotation;
            bullet.transform.position = boneBarrelGun1.GetWorldPosition(skeletonAnimation.transform);
            bullet.SetActive(true);

        }
    }
    GameObject enemy5;

    protected override void OnComplete(TrackEntry trackEntry)
    {
        base.OnComplete(trackEntry);
        if (trackEntry.Animation.Name.Equals(aec.die.name))
        {
            enemy5 = ObjectPoolerManager.Instance.enemy5Pooler.GetPooledObject();

            enemy5.transform.position = boneBody.GetWorldPosition(skeletonAnimation.transform); 

            var _enemy5Script = enemy5.GetComponent<Enemy5Controller>();
            _enemy5Script.jumpOut = true;
            _enemy5Script.Init();


            enemy5.SetActive(true);
        }
    }
}
