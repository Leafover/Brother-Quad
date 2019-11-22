using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Spine.Unity;
using Spine;



public class PlayerController : MonoBehaviour
{
    public List<EnemyBase> autoTarget;
    public AnimationReferenceAsset currentAnim;
    public AssetSpinePlayerController apc;

    Bone boneBarrelGun, boneHandGrenade;
    [SpineBone]
    public string strboneBarrelGun, strboneHandGrenade;

    float timePreviousAttack, timePreviousGrenade;
    public float timedelayAttackGun, timedelayAttackKnife, timedelayGrenade;

    public float health, maxHealth = 100;

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
    public bool isSlow;

    public enum PlayerState
    {
        Idle, Run, Sit, Jump, WaitStand
    }
    public PlayerState playerState = PlayerState.Idle;

    public SkeletonAnimation skeletonAnimation;

    public float speedMoveMax = 3;
    [HideInInspector]
    public bool dirMove;
    [HideInInspector]
    public float speedmove;
    [HideInInspector]
    public bool isBouderJoystick, isWaitStand, isGround, isfalldow, candoublejump;


    //public IEnumerator Move()
    //{

    //    yield return new WaitForEndOfFrame();
    //    StartCoroutine(Move());
    //}

    public float forceJump;
    public float timeJump;
    private float force;
    public void TakeDamage(float damage)
    {
        health -= damage;
        // Debug.Log("take damage" + health);
    }
    public void RemoveTarget(EnemyBase enemy)
    {
        if (currentEnemyTarget == enemy)
            currentEnemyTarget = null;

        if (autoTarget.Contains(enemy))
            autoTarget.Remove(enemy);

        if (enemy.canoutcam)
        {
            enemy.gameObject.SetActive(false);
        }

    }
    public void TryJump()
    {
        if (playerState == PlayerState.Sit)
            return;
        if ((playerState != PlayerState.Jump))
            StartCoroutine(Jump());
        else
        {
            if (!candoublejump)
                return;
            candoublejump = false;
            force = rid.velocity.y + 6f;
            rid.velocity = new Vector2(rid.velocity.x, force);
        }

    }
    public void TryGrenade()
    {
        if (Time.time - timePreviousGrenade > timedelayGrenade)
        {
            timePreviousGrenade = Time.time;
            if (isGround)
            {
                skeletonAnimation.AnimationState.SetAnimation(1, apc.grenadeAnim, false);
            }
            else
            {
                GameObject grenade = ObjectPoolerManager.Instance.grenadePooler.GetPooledObject();
                grenade.transform.position = boneHandGrenade.GetWorldPosition(skeletonAnimation.transform);
                grenade.SetActive(true);
            }
        }
    }
    public Transform GetTransformPlayer()
    {
        return transform;
    }
    public float GetTranformXPlayer()
    {
        return transform.position.x;
    }
    private void Start()
    {
        skeletonAnimation.Initialize(true);
        boneBarrelGun = skeletonAnimation.Skeleton.FindBone(strboneBarrelGun);
        boneHandGrenade = skeletonAnimation.Skeleton.FindBone(strboneHandGrenade);
        skeletonAnimation.AnimationState.Event += HandleEvent;
        skeletonAnimation.AnimationState.Complete += OnComplete;
        health = maxHealth;
        speedmove = 0;
        skeletonAnimation.AnimationState.SetAnimation(2, apc.aimTargetAnim, false);
    }
    public void DetectGround()
    {
        if (isfalldow)
            speedmove = 0;
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
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(foot.transform.position, 0.15f);
    //}
    public void SetAnim()
    {
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
                if (isGround && rid.velocity.y <= 0)
                {
                    if (!GameController.instance.joystickMove.GetJoystickState())
                    {
                        isWaitStand = true;
                        playerState = PlayerState.Idle;
                        speedmove = 0;
                        skeletonAnimation.AnimationState.SetAnimation(2, apc.aimTargetAnim, false);
                    }
                    else
                    {
                        isWaitStand = false;
                        GameController.instance.CheckAfterJump(GameController.instance.joystickMove);
                        skeletonAnimation.AnimationState.SetAnimation(2, apc.aimTargetAnim, false);
                    }
                }
                break;
            case PlayerState.Run:
                AnimRun();
                break;

        }
    }
    public Vector2 target;
    Vector2 movePos;

    public void OnUpdate(float deltaTime)
    {
        // Debug.Log(rid.velocity.x);
        isGround = Physics2D.OverlapCircle(foot.transform.position, 0.15f, lm);
        movePos.x = speedmove;
        movePos.y = rid.velocity.y;
        rid.velocity = movePos;
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


        if (!MoveTargetPos())
            return;
        targetPos.position = Vector2.MoveTowards(targetPos.position, target, deltaTime * 20);
    }
    bool MoveTargetPos()
    {
        return (targetPos.position.x == target.x && targetPos.position.y == target.y) ? false : true;
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

    Vector2 GetTargetTranform()
    {
        return targetPos.transform.position;
    }
    void HandleEvent(TrackEntry trackEntry, Spine.Event e)
    {
        if (trackEntry.Animation.Name.Equals(apc.fireAnim.name))
        {
            GameObject bullet = ObjectPoolerManager.Instance.bulletPooler.GetPooledObject();
            Vector2 dirBullet = GetTargetTranform() - (Vector2)boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            float angle = Mathf.Atan2(dirBullet.y, dirBullet.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            bullet.transform.rotation = rotation;
            bullet.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            bullet.SetActive(true);
        }
        else if (trackEntry.Animation.Name.Equals(apc.grenadeAnim.name))
        {
            GameObject grenade = ObjectPoolerManager.Instance.grenadePooler.GetPooledObject();
            grenade.transform.position = boneHandGrenade.GetWorldPosition(skeletonAnimation.transform);
            grenade.SetActive(true);
        }
    }
    private void OnComplete(TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name.Equals(apc.waitstandAnim.name))
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
        if (currentAnim == apc.waitstandAnim)
            return;
        skeletonAnimation.AnimationState.SetAnimation(0, apc.waitstandAnim, false);
        currentAnim = apc.waitstandAnim;
        SetBox(sizeBoxSit, offsetBoxSit);
    }
    void AnimFallDow()
    {
        if (currentAnim == apc.falldownAnim)
            return;
        skeletonAnimation.AnimationState.SetAnimation(0, apc.falldownAnim, false);
        currentAnim = apc.falldownAnim;
        SetBox(sizeBox, offsetBox);
    }
    void AnimJump()
    {
        if (isfalldow)
        {
            return;
        }
        else
        {
            if (currentAnim == apc.jumpAnim)
                return;
            skeletonAnimation.AnimationState.SetAnimation(0, apc.jumpAnim, true);
            currentAnim = apc.jumpAnim;
            SetBox(sizeBox, offsetBox);
        }

    }
    void AnimSit()
    {
        if (currentAnim == apc.sitAnim)
            return;
        skeletonAnimation.AnimationState.SetAnimation(0, apc.sitAnim, true);
        currentAnim = apc.sitAnim;
        speedmove = 0;
        SetBox(sizeBoxSit, offsetBoxSit);
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
            if (currentAnim == apc.runForwardAnim)
                return;
            skeletonAnimation.AnimationState.SetAnimation(0, apc.runForwardAnim, true);
            currentAnim = apc.runForwardAnim;
            SetBox(sizeBox, offsetBox);
        }
        else
        {
            if (currentAnim == apc.runBackAnim)
                return;
            skeletonAnimation.AnimationState.SetAnimation(0, apc.runBackAnim, true);
            currentAnim = apc.runBackAnim;
            SetBox(sizeBox, offsetBox);
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
            if (currentAnim == apc.idleAnim)
                return;
            skeletonAnimation.AnimationState.SetAnimation(0, apc.idleAnim, false);
            currentAnim = apc.idleAnim;
            speedmove = 0;
            SetBox(sizeBox, offsetBox);
        }
    }

    public void ShootDown()
    {

        if (Time.time - timePreviousAttack < timedelayAttackGun)
            return;
        timePreviousAttack = Time.time;
        skeletonAnimation.AnimationState.SetAnimation(1, apc.fireAnim, false);


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
