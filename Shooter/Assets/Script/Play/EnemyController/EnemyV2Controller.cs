using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
public class EnemyV2Controller : EnemyBase
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
        currentPos = Random.Range(0,CameraController.instance.posEnemyV2.Count);
        randomCombo = Random.Range(2, 4);
        if (!EnemyManager.instance.enemyv2s.Contains(this))
        {
            EnemyManager.instance.enemyv2s.Add(this);
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.enemyv2s.Contains(this))
        {
            EnemyManager.instance.enemyv2s.Remove(this);
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

        if (canoutcam && !incam)
            return;

        switch (enemyState)
        {
            case EnemyState.run:
                PlayAnim(0, aec.run, true);
                transform.position = Vector2.MoveTowards(transform.position,CameraController.instance.posEnemyV2[currentPos].position, deltaTime * speed);
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
            g = ObjectPoolerManager.Instance.rocketEnemyV2Pooler.GetPooledObject();
            g.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            g.transform.rotation = Quaternion.identity;
            g.transform.rotation = gunRotation.rotation;
            g.SetActive(true);
        }
    }
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
             //   Debug.LogError("re turn run");
            }
        }
        //else if (trackEntry.Animation.Name.Equals(aec.die.name))
        //{
        //    gameObject.SetActive(false);
        //}
    }
}
