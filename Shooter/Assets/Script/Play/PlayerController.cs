﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Spine.Unity;
using Spine;

[System.Serializable]
public class AnimReferenAssetControlMove
{
    public AnimationReferenceAsset runForwardAnim, idleAnim, waitstandAnim, jumpAnim, falldownAnim, sitAnim, runBackAnim;
}

public class PlayerController : MonoBehaviour
{
     Bone boneBarrelGun,boneOriginGun;


    #region chay nhay ngoi
    public AnimReferenAssetControlMove arac = new AnimReferenAssetControlMove();
    AnimationReferenceAsset currentAnim;
    #endregion

    float timePreviousAttack;
    public float timedelayAttackGun, timedelayAttackKnife;

    public AnimationReferenceAsset aimTargetAnim, fireAnim/*, knifeAnim*/;

    bool isKnife;

    public Rigidbody2D rid;
    public BoxCollider2D box;
    public Transform foot;
    public LayerMask lm;

    [SerializeField]
    Vector2 offsetBox, sizeBox;
    [SerializeField]
    Vector2 offsetBoxSit, sizeBoxSit;
    public Transform targetPos;
    public static PlayerController playerController;


    public enum PlayerState
    {
        Idle, Run, Sit, Jump, WaitStand
    }
    public PlayerState playerState = PlayerState.Idle;


    public SkeletonAnimation skeletonAnimation;





    [HideInInspector]
    public bool dirMove;
    [HideInInspector]
    public float speedmove;
    [HideInInspector]
    public bool isShooting, isBouderJoystick, isWaitStand, isGround, isfalldow, candoublejump;


    public IEnumerator Move()
    {
        rid.velocity = new Vector2(speedmove, rid.velocity.y);
        yield return new WaitForEndOfFrame();
        StartCoroutine(Move());
    }

    public float forceJump;
    public float timeJump;
    private float force;

    public void TryJump()
    {
        if (playerState == PlayerState.Sit)
            return;
        if ((playerState != PlayerState.Jump))
            StartCoroutine(Jump());
        else
        {
            if (candoublejump)
            {
                Debug.Log("double jump");
                candoublejump = false;
                force = rid.velocity.y + 6f;
                rid.velocity = new Vector2(rid.velocity.x, force);
            }
        }

    }
    public void DetectGround()
    {
        isfalldow = false;
        candoublejump = false;
    }
    int countJump;

    private IEnumerator Jump()
    {
        skeletonAnimation.ClearState();
        float timeUp = timeJump * 0.5f;
        playerState = PlayerState.Jump;
        AnimJump();
        candoublejump = true;

        for (float t = 0; t <= timeUp; t += Time.deltaTime)
        {
            if (playerState == PlayerState.Jump)
            {
                force = forceJump * (timeUp - t);
                rid.velocity = new Vector2(rid.velocity.x, force);
                yield return null;
            }
        }
        candoublejump = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(foot.transform.position, 0.15f);
    }
    void SetAnim()
    {
        //  Debug.LogError("------------:" + rid.velocity.y);
        switch (playerState)
        {
            case PlayerState.Idle:
                AnimIdle();
                //  AnimSit();
                break;
            case PlayerState.Sit:
                AnimSit();
                break;
            case PlayerState.Jump:
                AnimJump();
                //  Debug.LogError(rid.velocity.y);
                if (isGround && rid.velocity.y <= 0)
                {
                    if (!GameController.gameController.joystickMove.GetJoystickState())
                    {
                        isWaitStand = true;
                        playerState = PlayerState.Idle;
                    }
                    else
                    {
                        isWaitStand = false;
                        GameController.gameController.CheckAfterJump(GameController.gameController.joystickMove);
                    }
                }
                break;
            case PlayerState.Run:
                AnimRun();
                break;

        }
    }
    public void OnUpdate()
    {
        isGround = Physics2D.OverlapCircle(foot.transform.position, 0.15f, lm);

        if (!isGround)
        {
            if (playerState == PlayerState.Jump)
            {
                isfalldow = false;
            }
            else
            {
                isfalldow = true;
            }
        }
        else
        {
            DetectGround();
        }
        SetAnim();
    }
    private void Awake()
    {
        playerController = this;
    }

    void SetBox(Vector2 size, Vector2 offset)
    {
        if (box.offset != offset)
            box.offset = offset;
        if (box.size != size)
            box.size = size;
    }
    [SpineBone]
    public string strBoneBarrelGun,strBoneOriginGun;
    private void Start()
    {
        // allAnim.AnimationState.GetCurrent(0).Animation.duration;
        Debug.LogError(skeletonAnimation.valid);
        boneBarrelGun = skeletonAnimation.Skeleton.FindBone(strBoneBarrelGun);
        boneOriginGun = skeletonAnimation.Skeleton.FindBone(strBoneOriginGun);
        //   skeletonAnimation.Skeleton.SetBonesToSetupPose();

        if (boneBarrelGun == null)
        {
            Debug.LogError("null bone");
        }
        else
        {
            Debug.LogError("not null bone");
        }

        SetBox(sizeBox, offsetBox);
        StartCoroutine(Move());

        skeletonAnimation.AnimationState.Complete += OnComplete;
        skeletonAnimation.AnimationState.Event += HandleEvent;

        //for(int i = 0; i < skeletonAnimation.Skeleton.Bones.Items.Length; i ++)
        //{
        //    Debug.Log(skeletonAnimation.Skeleton.Bones.Items[i]);
        //}
    }

    void HandleEvent(TrackEntry trackEntry, Spine.Event e)
    {
        if (trackEntry.Animation.Name.Equals(fireAnim.name))
        {
            GameObject bullet = ObjectPoolerManager.Instance.bulletPooler.GetPooledObject();
            Vector2 dirBullet = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform) - boneOriginGun.GetWorldPosition(skeletonAnimation.transform);
            float angle = Mathf.Atan2(dirBullet.y,dirBullet.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            bullet.transform.rotation = rotation;
            bullet.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            bullet.SetActive(true);
        }
    }
    private void OnComplete(TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name.Equals(arac.waitstandAnim.name))
        {
            isWaitStand = false;
        }
    }
    public Vector2 GetOriginGun()
    {
        var vt2 = new Vector2();
        vt2 = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
        return vt2;
    }
    public LayerMask layerTarget;
    public Vector2 GetTargetFromDirection(Vector2 direction)
    {
        var target = Vector2.zero;
        var origin = GetOriginGun();
        direction.Normalize();
        var hit = Physics2D.Raycast(origin, direction, 1000, layerTarget);
#if UNITY_EDITOR
        Debug.DrawRay(origin, direction * 1000, Color.red);
#endif
        if (hit.collider != null)
        {
            target = hit.point;
        }
        return target;
    }


    public bool FlipX
    {
        get { return skeletonAnimation.skeleton.FlipX; }
        set { skeletonAnimation.skeleton.FlipX = value; }
    }
    void AnimWaitStand()
    {
        if (currentAnim != arac.waitstandAnim)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, arac.waitstandAnim, false);
            currentAnim = arac.waitstandAnim;
            SetBox(sizeBoxSit, offsetBoxSit);
        }
    }
    void AnimFallDow()
    {
        if (currentAnim != arac.falldownAnim)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, arac.falldownAnim, false);
            currentAnim = arac.falldownAnim;
            SetBox(sizeBox, offsetBox);
        }
    }
    void AnimJump()
    {
        if (isfalldow)
        {
            return;
        }
        else
        {
            if (currentAnim != arac.jumpAnim)
            {
                skeletonAnimation.AnimationState.SetAnimation(0, arac.jumpAnim, true);
                currentAnim = arac.jumpAnim;
                SetBox(sizeBox, offsetBox);
            }
        }
    }
    void AnimSit()
    {
        if (currentAnim != arac.sitAnim)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, arac.sitAnim, true);
            currentAnim = arac.sitAnim;
            speedmove = 0;
            SetBox(sizeBoxSit, offsetBoxSit);
            SetBox(sizeBoxSit, offsetBoxSit);
        }
    }

    void AnimRun()
    {
        if (isfalldow && rid.velocity.y != 0)
        {
            AnimFallDow();
            return;
        }
        if (dirMove == FlipX)
        {
            if (currentAnim != arac.runForwardAnim)
            {
                skeletonAnimation.AnimationState.SetAnimation(0, arac.runForwardAnim, true);
                currentAnim = arac.runForwardAnim;
                SetBox(sizeBox, offsetBox);
                SetBox(sizeBoxSit, offsetBoxSit);
            }
        }
        else
        {
            if (currentAnim != arac.runBackAnim)
            {
                skeletonAnimation.AnimationState.SetAnimation(0, arac.runBackAnim, true);
                currentAnim = arac.runBackAnim;
                SetBox(sizeBox, offsetBox);
                SetBox(sizeBoxSit, offsetBoxSit);
            }
        }
    }

    void AnimIdle()
    {
        if (isfalldow && rid.velocity.y != 0)
        {
            AnimFallDow();
            return;
        }
        if (isWaitStand)
        {
            AnimWaitStand();
        }
        else
        {
            if (currentAnim != arac.idleAnim)
            {
                skeletonAnimation.AnimationState.SetAnimation(0, arac.idleAnim, false);
                currentAnim = arac.idleAnim;
                speedmove = 0;
                SetBox(sizeBox, offsetBox);
                SetBox(sizeBoxSit, offsetBoxSit);
            }
        }
    }

    public void ShootDown()
    {

        if (Time.time - timePreviousAttack > timedelayAttackGun)
        {
            timePreviousAttack = Time.time;
            skeletonAnimation.AnimationState.SetAnimation(1, fireAnim, false);
        }
        if (!isShooting)
        {
            isShooting = true;
        }
        if (playerState != PlayerState.Jump)
        {
            skeletonAnimation.AnimationState.SetAnimation(2, aimTargetAnim, false);
         //   Debug.Log("wtf???????????????");
        }
    }
    public void ShootUp()
    {
        if (isShooting)
        {
            isShooting = false;
            //    skeletonAnimation.ClearState();
        }
    }
    public void ChangeKnife()
    {
        isKnife = !isKnife;
    }
}
