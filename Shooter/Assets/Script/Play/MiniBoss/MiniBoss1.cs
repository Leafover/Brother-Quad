using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
public class MiniBoss1 : EnemyBase
{
    int currentPos;
    public Transform gunRotation, gunRotation1, gunRotation2;
    public override void Start()
    {
        base.Start();
        Init();
    }
    public override void Init()
    {
        base.Init();
        currentPos = Random.Range(0,CameraController.instance.posEnemyV2.Count);
        randomCombo = Random.Range(2, 4);
        if (!EnemyManager.instance.miniboss1s.Contains(this))
        {
            EnemyManager.instance.miniboss1s.Add(this);
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.miniboss1s.Contains(this))
        {
            EnemyManager.instance.miniboss1s.Remove(this);
        }
    }
    public override void Active()
    {
        base.Active();
        enemyState = EnemyState.run;
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
                PlayAnim(0, aec.run, true);
                transform.position = Vector2.MoveTowards(transform.position,CameraController.instance.posMiniBoss1[currentPos].position, deltaTime * speed);
                if(transform.position.x == CameraController.instance.posMiniBoss1[currentPos].position.x && transform.position.y == CameraController.instance.posMiniBoss1[currentPos].position.y)
                {
                    CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());
                    enemyState = EnemyState.attack;
                    currentPos = Random.Range(0,CameraController.instance.posMiniBoss1.Count);
                }
                break;
            case EnemyState.attack:
                Attack(1, aec.attack1, false);
                break;
        }
    }
    GameObject g;
    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);

        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            if (!incam)
                return;
            g = ObjectPoolerManager.Instance.rocketMiniBoss1Pooler.GetPooledObject();
            g.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            g.transform.rotation = gunRotation.rotation;
            g.SetActive(true);

            g = ObjectPoolerManager.Instance.rocketMiniBoss1Pooler.GetPooledObject();
            g.transform.position = boneBarrelGun1.GetWorldPosition(skeletonAnimation.transform);
            g.transform.rotation = gunRotation1.rotation;
            g.SetActive(true);

            g = ObjectPoolerManager.Instance.rocketMiniBoss1Pooler.GetPooledObject();
            g.transform.position = boneBarrelGun2.GetWorldPosition(skeletonAnimation.transform);
            g.transform.rotation = gunRotation2.rotation;
            g.SetActive(true);
        }
    }
    protected override void OnComplete(TrackEntry trackEntry)
    {
        base.OnComplete(trackEntry);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
         //   Debug.Log("---------aec.attack1.name");
            combo++;
            if (combo == randomCombo)
            {
                combo = 0;
                enemyState = EnemyState.run;
                randomCombo = Random.Range(2, 4);
            }
        }
    }
}
