using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Spine.Unity;
using Spine;

public class PlayerController : MonoBehaviour
{

    [HideInInspector]
    public float speedmove;
    Vector2 dirMove;
    public Rigidbody2D rid;
    public Transform targetPos;
    public static PlayerController playerController;
    [HideInInspector]
    public bool isShooting, isBouderJoystick, isWaitStand, isGround, isfalldow, candoublejump;

    public enum PlayerState
    {
        Idle, RunLeft, RunRight, Sit, Jump
    }
    public PlayerState playerState = PlayerState.Idle;

    public SkeletonAnimation skeletonAnimation;

    [Serializable]
    public class StringAnimationState
    {
        [SpineAnimation]
        public string idle;
        [SpineAnimation]
        public string run;
        [SpineAnimation]
        public string sit;
        [SpineAnimation]
        public string jump;
        [SpineAnimation]
        public string waitSit;
        [SpineAnimation]
        public string falldown;

    }
    public StringAnimationState stringAnimationState;
    int idle_Hash;
    int jump_Hash;
    int run_Hash;
    int sit_Hash;
    int waitsit_Hash;
    int falldown_Hash;
    int currentAnimation_Hash;


    Bone boneOrginGun;

    public IEnumerator Move()
    {
        //if (!playerHealth.isDead)
        //{
        rid.velocity = new Vector2(speedmove, rid.velocity.y);
        yield return new WaitForEndOfFrame();
        StartCoroutine(Move());
        //}
        //else
        //{
        //    currentSpeed = 0f;
        //    rgb.velocity = Vector2.zero;
        //    StopAllCoroutines();
        //}
    }

    public float forceJump;
    public float timeJump;
    private float force;
    //private void Update()
    //{
    //    if(rid.velocity.y < 0 && isGround)
    //    {
    //        if(playerState == PlayerState.Jump)
    //        {
    //            playerState = PlayerState.Idle;
    //        }
    //    }
    //}
    public void TryJump()
    {
        if (playerState == PlayerState.Sit)
            return;
        if ((playerState != PlayerState.Jump)/* || (playerState == PlayerState.Jump && !candoublejump)*/)
            StartCoroutine(Jump());
        else
        {
            if(candoublejump)
            {
                Debug.Log("double jump");
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
        float timeUp = timeJump * 0.5f;
        playerState = PlayerState.Jump;
        AnimJump();

        
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


        //  rid.gravityScale = 1.5f;
        //if (currentAnimation != jumpDown_Hash)
        //{
        //    animationState.SetAnimation(0, stringAnimationState.jump_down, true);
        //    currentAnimation = jumpDown_Hash;
        //}

    }



    void SetAnim()
    {
        //  Debug.LogError("------------:" + rid.velocity.y);
        switch (playerState)
        {
            case PlayerState.Idle:
                AnimIdle();
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
            case PlayerState.RunLeft:
                AnimRun();
                break;

            case PlayerState.RunRight:
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
    private void Start()
    {
        idle_Hash = Animator.StringToHash(stringAnimationState.idle);
        run_Hash = Animator.StringToHash(stringAnimationState.run);
        sit_Hash = Animator.StringToHash(stringAnimationState.sit);
        jump_Hash = Animator.StringToHash(stringAnimationState.jump);
        waitsit_Hash = Animator.StringToHash(stringAnimationState.waitSit);
        falldown_Hash = Animator.StringToHash(stringAnimationState.falldown);
        currentAnimation_Hash = idle_Hash;

        //  boneOrginGun = skeletonAnimation.skeleton.FindBone("aim-constraint-target");

        //  Debug.LogError(":" + boneOrginGun);

        //   Debug.Log("flip:-------------" + FlipX);

        StartCoroutine(Move());

        skeletonAnimation.state.Complete += OnComplete;

    }
    private void OnComplete(TrackEntry trackEntry)
    {

        if (/*trackEntry.Animation.Name.Equals(stringAnimationState.waitSit)*/ currentAnimation_Hash == waitsit_Hash)
        {
            isWaitStand = false;
        }
    }


    public Vector2 GetTargetFromDirection(Vector2 direction)
    {
        direction.Normalize();
        return direction *= 3f;
    }


    public bool FlipX
    {
        get { return skeletonAnimation.skeleton.FlipX; }
        set { skeletonAnimation.skeleton.FlipX = value; }
    }
    void AnimWaitStand()
    {
        if (currentAnimation_Hash != waitsit_Hash)
        {
            skeletonAnimation.state.SetAnimation(0, stringAnimationState.waitSit, false);
            currentAnimation_Hash = waitsit_Hash;

            // Debug.LogError("zoooooooo");
        }
    }
    void AnimFallDow()
    {
        if (currentAnimation_Hash != falldown_Hash)
        {
            skeletonAnimation.state.SetAnimation(0, stringAnimationState.falldown, true);
            currentAnimation_Hash = falldown_Hash;
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
            if (currentAnimation_Hash != jump_Hash)
            {
                skeletonAnimation.state.SetAnimation(0, stringAnimationState.jump, true);
                currentAnimation_Hash = jump_Hash;
            }
        }
    }
    void AnimSit()
    {
        if (currentAnimation_Hash != sit_Hash)
        {
            skeletonAnimation.state.SetAnimation(0, stringAnimationState.sit, true);
            currentAnimation_Hash = sit_Hash;
        }
        speedmove = 0;
    }

    void AnimRun()
    {
        if (isfalldow)
        {
            AnimFallDow();
            return;
        }
        if (currentAnimation_Hash != run_Hash)
        {
            skeletonAnimation.state.SetAnimation(0, stringAnimationState.run, true);
            currentAnimation_Hash = run_Hash;
        }
        speedmove = FlipX ? -1.5f : 1.5f;
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
            //    Debug.Log("waitstand");
        }
        else
        {
            if (currentAnimation_Hash != idle_Hash)
            {
                skeletonAnimation.state.SetAnimation(0, stringAnimationState.idle, true);
                currentAnimation_Hash = idle_Hash;
            }
        }
        speedmove = 0;
    }

    public void ShootDown()
    {
        isShooting = true;
    }
    public void ShootUp()
    {
        isShooting = false;
    }
}
