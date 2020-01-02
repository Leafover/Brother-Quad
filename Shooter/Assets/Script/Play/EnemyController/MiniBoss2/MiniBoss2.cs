using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss2 : EnemyBase
{
    public AnimationReferenceAsset die1Anim;
    public List<AnimationReferenceAsset> shotguns, dieguns;
    Bone[] boneGun = new Bone[8];
    [SpineBone]
    public string[] strboneGun;
    public GameObject[] gunBox;
    int currentPos;
    public override void Start()
    {
        base.Start();
        Init();
    }
    public override void Init()
    {
        base.Init();
        currentPos = Random.Range(0, CameraController.instance.posMove.Count);
        if (!EnemyManager.instance.miniboss2s.Contains(this))
        {
            EnemyManager.instance.miniboss2s.Add(this);
        }
        for (int i = 0; i < strboneGun.Length; i++)
        {
            boneGun[i] = skeletonAnimation.Skeleton.FindBone(strboneGun[i]);
            gunBox[i].transform.position = boneGun[i].GetWorldPosition(skeletonAnimation.transform);
        }
        PlayAnim(0, aec.idle, false);
    }
    public override void Active()
    {
        base.Active();
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
            case EnemyState.idle:

                break;
            case EnemyState.attack:

                break;
            case EnemyState.run:
                transform.position = Vector2.MoveTowards(transform.position, CameraController.instance.posMove[currentPos].position, deltaTime * speed);
                CheckDirFollowPlayer(CameraController.instance.posMove[currentPos].position.x);
                if (transform.position.x == CameraController.instance.posMove[currentPos].position.x && transform.position.y == CameraController.instance.posMove[currentPos].position.y)
                {
                    CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());
                    currentPos = Random.Range(0, CameraController.instance.posMove.Count);
                    enemyState = EnemyState.attack;
                }
                break;
        }
    }
    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);
    }
    protected override void OnComplete(TrackEntry trackEntry)
    {
        base.OnComplete(trackEntry);
        if (trackEntry.Animation.Name.Equals(aec.idle.name))
        {
            enemyState = EnemyState.run;
            PlayAnim(0, aec.run, true);
            //Debug.Log("end anim");
        }
    }
    public override void Dead()
    {
        base.Dead();
    }
    public override void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance == null)
            return;
        if (EnemyManager.instance.miniboss2s.Contains(this))
        {
            EnemyManager.instance.miniboss2s.Remove(this);
        }
    }
}
