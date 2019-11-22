using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
public class Enemy6Controller : EnemyBase
{
   public float speedMove;
    private void Start()
    {
        base.Start();
        Init();
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

    private void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.enemy6s.Contains(this))
        {
            EnemyManager.instance.enemy6s.Remove(this);
        }
    }
    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        if (enemyState == EnemyState.die)
            return;

        if (!isActive)
        {
            if (transform.position.x - Camera.main.transform.position.x <= distanceActive)
            {
                isActive = true;
                PlayAnim(0, aec.run, true);
                enemyState = EnemyState.run;
            }
            return;
        }
        switch(enemyState)
        {
            case EnemyState.run:
                rid.velocity = new Vector2(speedMove, rid.velocity.y);
                if(Mathf.Abs(transform.position.x - Camera.main.transform.position.x) <= 5)
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


    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
          //  Debug.Log("--------------------???");
            GameObject bullet = ObjectPoolerManager.Instance.bulletEnemy6Pooler.GetPooledObject();
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
        if (trackEntry.Animation.Name.Equals(aec.die.name))
        {
            GameObject enemy5 = ObjectPoolerManager.Instance.enemy5Pooler.GetPooledObject();
            enemy5.transform.position = gameObject.transform.position;
            enemy5.GetComponent<Enemy5Controller>().Init();
            enemy5.SetActive(true);
        }
    }
}
