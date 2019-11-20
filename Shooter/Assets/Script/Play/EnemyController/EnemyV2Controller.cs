using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
public class EnemyV2Controller : EnemyBase
{
    int currentPos;
    int countShoot;
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        base.Init();
        currentPos = Random.Range(0,CameraController.instance.posEnemyV2.Count);
        countShoot = Random.Range(2, 4);
    }
    public override void AcBecameVisibleCam()
    {
        base.AcBecameVisibleCam();
        if (!EnemyManager.instance.enemyv2s.Contains(this))
        {
            EnemyManager.instance.enemyv2s.Add(this);
            isActive = true;
        }
    }
    private void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.enemyv2s.Contains(this))
        {
            EnemyManager.instance.enemyv2s.Remove(this);
        }
    }
    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        if (!isActive)
            return;
        switch(enemyState)
        {
            case EnemyState.run:
                PlayAnim(0, aec.run, true);
                transform.position = Vector2.MoveTowards(transform.position,CameraController.instance.posEnemyV2[currentPos].position, deltaTime * speed);
                if(transform.position.x == CameraController.instance.posEnemyV2[currentPos].position.x && transform.position.y == CameraController.instance.posEnemyV2[currentPos].position.y)
                {
                    CheckDirFollowPlayer(PlayerController.instance.GetTranformPlayer());
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
    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {

        }
    }
    protected override void OnComplete(TrackEntry trackEntry)
    {
        base.OnComplete(trackEntry);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            combo++;
            if (combo == countShoot)
            {
                combo = 0;
                enemyState = EnemyState.run;
                countShoot = Random.Range(2, 4);
            }
        }

    }
}
