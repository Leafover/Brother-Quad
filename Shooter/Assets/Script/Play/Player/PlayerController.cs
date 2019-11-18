using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Spine.Unity;
using Spine;
[Serializable]
public class AssetSpineController{
    public AnimationReferenceAsset waitstandAnim, falldownAnim, jumpAnim, sitAnim, idleAnim, runForwardAnim, runBackAnim;
}

public class PlayerController : MonoBehaviour
{

    public List<EnemyBase> autoTarget;
    public AnimationReferenceAsset currentAnim;
    public AssetSpineController asc;

    Bone boneBarrelGun, boneHandGrenade;
    [SpineBone]
    public string strboneBarrelGun, strboneHandGrenade;

    float timePreviousAttack, timePreviousGrenade;
    public float timedelayAttackGun, timedelayAttackKnife, timedelayGrenade;

    public AnimationReferenceAsset aimTargetAnim, fireAnim, grenadeStandAnim, grenadeSitAnim;

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
    public static PlayerController instance;


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
    public void TryGrenade()
    {
        if (Time.time - timePreviousGrenade > timedelayGrenade)
        {
            timePreviousGrenade = Time.time;
            if (isGround)
            {
                skeletonAnimation.AnimationState.SetAnimation(1, grenadeStandAnim, false);
            }
            else
            {
                GameObject grenade = ObjectPoolerManager.Instance.grenadePooler.GetPooledObject();
                grenade.transform.position = boneHandGrenade.GetWorldPosition(skeletonAnimation.transform);
                grenade.SetActive(true);
            }
        }

    }
    private void Start()
    {
        boneBarrelGun = skeletonAnimation.Skeleton.FindBone(strboneBarrelGun);
        boneHandGrenade = skeletonAnimation.Skeleton.FindBone(strboneHandGrenade);
        skeletonAnimation.AnimationState.Event += HandleEvent;
        skeletonAnimation.AnimationState.Complete += OnComplete;
        StartCoroutine(Move());
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
                    if (!GameController.instance.joystickMove.GetJoystickState())
                    {
                        isWaitStand = true;
                        playerState = PlayerState.Idle;
                        speedmove = 0;
                    }
                    else
                    {
                        isWaitStand = false;
                        GameController.instance.CheckAfterJump(GameController.instance.joystickMove);
                    }
                }
                break;
            case PlayerState.Run:
                AnimRun();
                break;

        }
    }
    public Vector2 target;
    public void OnUpdate()
    {
       // Debug.Log(rid.velocity.x);
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
        var deltaTime = Time.deltaTime;
        targetPos.position = Vector2.MoveTowards(targetPos.position, target, deltaTime * 20);
        if (playerState != PlayerState.Jump)
        {
            skeletonAnimation.AnimationState.SetAnimation(2, aimTargetAnim, false);
        }
    }
    private void Awake()
    {
        instance = this;
    }

    void SetBox(Vector2 size, Vector2 offset)
    {
        if (box.offset != offset)
            box.offset = offset;
        if (box.size != size)
            box.size = size;
    }


    void HandleEvent(TrackEntry trackEntry, Spine.Event e)
    {
        if (trackEntry.Animation.Name.Equals(fireAnim.name))
        {
            GameObject bullet = ObjectPoolerManager.Instance.bulletPooler.GetPooledObject();
            Vector2 dirBullet = targetPos.transform.position - boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            float angle = Mathf.Atan2(dirBullet.y, dirBullet.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            bullet.transform.rotation = rotation;
            bullet.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            bullet.SetActive(true);
        }
        else if (trackEntry.Animation.Name.Equals(grenadeStandAnim.name) || trackEntry.Animation.Name.Equals(grenadeSitAnim.name))
        {
            //  Debug.LogError("--------- nem lu dan");
            GameObject grenade = ObjectPoolerManager.Instance.grenadePooler.GetPooledObject();
            grenade.transform.position = boneHandGrenade.GetWorldPosition(skeletonAnimation.transform);
            grenade.SetActive(true);
        }
    }
    private void OnComplete(TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name.Equals(asc.waitstandAnim.name))
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



    public bool FlipX
    {
        get { return skeletonAnimation.skeleton.FlipX; }
        set { skeletonAnimation.skeleton.FlipX = value; }
    }
    void AnimWaitStand()
    {
        if (currentAnim != asc.waitstandAnim)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, asc.waitstandAnim, false);
            currentAnim = asc.waitstandAnim;
            SetBox(sizeBoxSit, offsetBoxSit);
        }
    }
    void AnimFallDow()
    {
        if (currentAnim != asc.falldownAnim)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, asc.falldownAnim, false);
            currentAnim = asc.falldownAnim;
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
            if (currentAnim != asc.jumpAnim)
            {
                skeletonAnimation.AnimationState.SetAnimation(0, asc.jumpAnim, true);
                currentAnim = asc.jumpAnim;
                SetBox(sizeBox, offsetBox);
            }
        }
    }
    void AnimSit()
    {
        if (currentAnim != asc.sitAnim)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, asc.sitAnim, true);
            currentAnim = asc.sitAnim;
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
            if (currentAnim != asc.runForwardAnim)
            {
                skeletonAnimation.AnimationState.SetAnimation(0, asc.runForwardAnim, true);
                currentAnim = asc.runForwardAnim;
                SetBox(sizeBox, offsetBox);
                SetBox(sizeBoxSit, offsetBoxSit);
            }
        }
        else
        {
            if (currentAnim != asc.runBackAnim)
            {
                skeletonAnimation.AnimationState.SetAnimation(0, asc.runBackAnim, true);
                currentAnim = asc.runBackAnim;
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
            if (currentAnim != asc.idleAnim)
            {
                skeletonAnimation.AnimationState.SetAnimation(0, asc.idleAnim, false);
                currentAnim = asc.idleAnim;
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
    }
    public void ShootUp()
    {
        if (isShooting)
        {
            isShooting = false;
        }

    }
    public EnemyBase currentEnemyTarget;

    public void ChangeKnife()
    {
        isKnife = !isKnife;
    }

    Vector2 targetTemp;
    public Vector2 GetTargetFromDirection(Vector2 direction)
    {
        var origin = GetOriginGun();
        direction.Normalize();
        var hit = Physics2D.Raycast(origin, direction, 1000, layerTarget);
#if UNITY_EDITOR
        Debug.DrawRay(origin, direction * 1000, Color.red);
#endif
        if (hit.collider != null)
        {
            targetTemp = hit.point;
        }
        return targetTemp;
    }
    Vector2 GetTarget()
    {
        //if (currentEnemyTarget == null)
        //{
            var dMin = float.MaxValue;
            for (int i = 0; i < autoTarget.Count; i++)
            {
                var enemy = autoTarget[i];
                var from = (Vector2)transform.position;
                var to = enemy.Origin();
                var d = Vector2.Distance(from, to);
                if (d < dMin)
                {
                    dMin = d;
                    currentEnemyTarget = enemy;
                }
            }
        //}
        return currentEnemyTarget.transform.position;
    }
    public void SelectNonTarget(Vector2 pos)
    {
        target = GetTargetFromDirection(pos);
    }
    public void SelectTarget()
    {
        target = GetTarget();
        FlipX = target.x < transform.position.x;
    }

}
