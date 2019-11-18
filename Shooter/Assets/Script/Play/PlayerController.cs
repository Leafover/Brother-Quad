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
    Bone boneBarrelGun/*,boneOriginGun*/, boneHandGrenade;

    public List<EnemyBase> autoTarget;

    #region chay nhay ngoi
    public AnimReferenAssetControlMove arac = new AnimReferenAssetControlMove();
    AnimationReferenceAsset currentAnim;
    #endregion

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
    public void TryGrenade()
    {
        if (!isGround)
            return;
        if (Time.time - timePreviousGrenade > timedelayGrenade)
        {
            timePreviousGrenade = Time.time;
            skeletonAnimation.AnimationState.SetAnimation(1, grenadeStandAnim, false);
            //if (playerState == PlayerState.Sit)
            //    skeletonAnimation.AnimationState.SetAnimation(1, grenadeSitAnim, false);
            //else
            //    skeletonAnimation.AnimationState.SetAnimation(1, grenadeStandAnim, false);
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
    public Vector2 target;
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
        targetPos.position = Vector2.MoveTowards(targetPos.position, target, Time.deltaTime * 20);
        if (playerState != PlayerState.Jump)
        {
            skeletonAnimation.AnimationState.SetAnimation(2, aimTargetAnim, false);
        }
    }
    private void Awake()
    {
        playerController = this;


        Debug.Log("============" + timedelayAttackGun);
    }

    void SetBox(Vector2 size, Vector2 offset)
    {
        if (box.offset != offset)
            box.offset = offset;
        if (box.size != size)
            box.size = size;
    }
    [SpineBone]
    public string strBoneBarrelGun/*, strBoneOriginGun*/, strBoneHandGrenade;
    private void Start()
    {
        boneBarrelGun = skeletonAnimation.Skeleton.FindBone(strBoneBarrelGun);
        boneHandGrenade = skeletonAnimation.Skeleton.FindBone(strBoneHandGrenade);

        //if (boneBarrelGun == null)
        //{
        //    Debug.LogError("null bone");
        //}
        //else
        //{
        //    Debug.LogError("not null bone");
        //}

        SetBox(sizeBox, offsetBox);
        StartCoroutine(Move());

        skeletonAnimation.AnimationState.Complete += OnComplete;
        skeletonAnimation.AnimationState.Event += HandleEvent;
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
        if (currentEnemyTarget == null)
        {
            var dMin = float.MaxValue;
            for (int i = 0; i < autoTarget.Count; i++)
            {
                var enemy = autoTarget[i];
                //if (!enemy.isInCamera || enemy.HP <= 0 || !enemy.gameObject.activeSelf)
                //{
                //    continue;
                //}
                var from = (Vector2)transform.position;
                var to = enemy.Origin();
                var d = Vector2.Distance(from, to);
                //Lấy enemy khoảng cach gần nhất
                if (d < dMin)
                {
                    dMin = d;
                    currentEnemyTarget = enemy;
                }
            }
        }
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
