using System.Collections;
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

    #region chay nhay ngoi
    public AnimReferenAssetControlMove arac = new AnimReferenAssetControlMove();
    AnimationReferenceAsset currentAnim;
    #endregion

    float timePreviousAttack;
    public float timedelayAttackGun,timedelayAttackKnife;

    public AnimationReferenceAsset aimTargetAnim,fireAnim,knifeAnim;

    bool isKnife;

    public Rigidbody2D rid;
    public Transform targetPos;
    public static PlayerController playerController;


    public enum PlayerState
    {
        Idle, Run, Sit, Jump, WaitStand
    }
    public PlayerState playerState = PlayerState.Idle;


    public SkeletonAnimation skeletonAnimation;


    Bone boneOrginGun;


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
        isGround = true;
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
        //   Debug.Log("zo");

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
        SetAnim();
    }
    private void Awake()
    {
        playerController = this;
    }
    Bone boneOriginGun;
    private void Start()
    {
       // allAnim.AnimationState.GetCurrent(0).Animation.duration;

        boneOriginGun = skeletonAnimation.Skeleton.FindBone("aim-constraint-target");
        StartCoroutine(Move());

        skeletonAnimation.AnimationState.Complete += OnComplete;

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
        vt2.x = boneOriginGun.WorldX + transform.position.x;
        vt2.y = boneOriginGun.WorldY + transform.position.y;
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
        }
    }
    void AnimFallDow()
    {
        if (currentAnim != arac.falldownAnim)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, arac.falldownAnim, false);
            currentAnim = arac.falldownAnim;
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
        }
    }

    void AnimRun()
    {
        if (isfalldow)
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
            }
        }
        else
        {
            if (currentAnim != arac.runBackAnim)
            {
                skeletonAnimation.AnimationState.SetAnimation(0, arac.runBackAnim, true);
                currentAnim = arac.runBackAnim;
            }
        }
    }

    void AnimIdle()
    {

        if (isfalldow)
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
                //  Debug.Log("zo day");
            }
        }
    }

    public void ShootDown()
    {
        //if (!isKnife)
        //{
            if (Time.time - timePreviousAttack > timedelayAttackGun)
            {
                timePreviousAttack = Time.time;
                skeletonAnimation.AnimationState.SetAnimation(1, fireAnim, false);
            }
        //}
        //else
        //{
        //    if (playerState != PlayerState.Jump)
        //    {
        //        if (Time.time - timePreviousAttack > timedelayAttackKnife)
        //        {
        //            timePreviousAttack = Time.time;
        //            skeletonAnimation.AnimationState.SetAnimation(1, knifeAnim, false);
        //        }
        //    }
        //}

        if (!isShooting)
        {
            isShooting = true;
        }
        if (playerState != PlayerState.Jump)
        {
            skeletonAnimation.AnimationState.SetAnimation(2, aimTargetAnim, false);
        }
    }
    public void ShootUp()
    {
        if (isShooting)
        {
            isShooting = false;
            //      skeletonAnimation.AnimationState.ClearTrack(2);
            skeletonAnimation.ClearState();
            //  skeletonAnimation.AnimationState.ClearTracks();
        }
    }
    public void ChangeKnife()
    {
        isKnife = !isKnife;
    }
}
