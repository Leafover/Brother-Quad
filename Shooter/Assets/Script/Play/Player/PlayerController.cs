using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Spine.Unity;
using Spine;



public class PlayerController : MonoBehaviour
{
    public LineBlood lineBlood;
    public AnimationReferenceAsset currentAnim;
    public AssetSpinePlayerController apc;

    Bone boneBarrelGun, boneHandGrenade;
    [SpineBone]
    public string strboneBarrelGun, strboneHandGrenade;

    float timePreviousAttack, timePreviousGrenade,timePreviousRocket;
    public float timedelayAttackGun, timedelayAttackKnife, timedelayGrenade,timedelayRocket;

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
        Idle, Run, Sit, Jump, WaitStand,Die
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
        if (playerState == PlayerState.Die)
            return;
        health -= damage;
        lineBlood.Show(health, maxHealth);

        if(health <= 0)
        {
            GameController.instance.DoneMission(false);
            AnimDie();
            playerState = PlayerState.Die;
            SoundController.instance.PlaySound(soundGame.playerDie);
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
            StartCoroutine(DoubleJump());
            //force = rid.velocity.y + 6f;
            //rid.velocity = new Vector2(rid.velocity.x, force);
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
            SoundController.instance.PlaySound(soundGame.throwGrenade);
        }
    }
    //public void TryRocket()
    //{
    //    if (Time.time - timePreviousRocket > timedelayRocket)
    //    {
    //        timePreviousRocket = Time.time;
    //        if (isGround)
    //        {
    //            skeletonAnimation.AnimationState.SetAnimation(1, apc.grenadeAnim, false);
    //        }
    //        else
    //        {
    //            GameObject grenade = ObjectPoolerManager.Instance.grenadePooler.GetPooledObject();
    //            grenade.transform.position = boneHandGrenade.GetWorldPosition(skeletonAnimation.transform);
    //            grenade.SetActive(true);
    //        }
    //    }
    //}
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
        lineBlood.Reset();
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

    private IEnumerator DoubleJump()
    {
        float timeUp = timeJump * 0.5f;
        playerState = PlayerState.Jump;
        AnimJump();
        for (float t = 0; t <= timeUp; t += Time.deltaTime)
        {
            if (playerState == PlayerState.Jump)
            {
                force = forceJump * (timeUp - t);
                rid.velocity = new Vector2(rid.velocity.x, force/2);
                yield return null;
            }
        }
    }
    private IEnumerator Jump()
    {
        skeletonAnimation.ClearState();
        float timeUp = timeJump * 0.5f;
        playerState = PlayerState.Jump;
        AnimJump();
    //    candoublejump = true;

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
    //    Gizmos.DrawWireSphere(foot.transform.position, radius);
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
    public float radius;
    public void OnUpdate(float deltaTime)
    {
        // Debug.Log(rid.velocity.x);
        isGround = Physics2D.OverlapCircle(foot.transform.position, radius, lm);
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


        LockPlayer();
        targetPos.position = Vector2.MoveTowards(targetPos.position, target, deltaTime * 20);
    }
    Vector2 posTemp;
    void LockPlayer()
    {
        if (transform.position.x >= CameraController.instance.bouders[2].transform.position.x - 1)
        {
            posTemp = transform.position;
            posTemp.x = CameraController.instance.bouders[2].transform.position.x - 1;
            transform.position = posTemp;
        }
        if (transform.position.x <= CameraController.instance.bouders[3].transform.position.x + 1)
        {
            posTemp = transform.position;
            posTemp.x = CameraController.instance.bouders[3].transform.position.x + 1;
            transform.position = posTemp;
        }
        //if (transform.position.y >= CameraController.instance.bouders[0].transform.position.y - 1)
        //{
        //    posTemp = transform.position;
        //    posTemp.y = CameraController.instance.bouders[0].transform.position.y - 1;
        //    transform.position = posTemp;
        //}
        if (transform.position.y <= CameraController.instance.bouders[1].transform.position.y + 1)
        {
            posTemp = transform.position;
            posTemp.y = CameraController.instance.bouders[1].transform.position.y + 1;
            transform.position = posTemp;
        }
    }

    private void Awake()
    {
        if (instance == null)
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
    GameObject bullet,grenade;
    Vector2 dirBullet;
    float angle;
    Quaternion rotation;
    void HandleEvent(TrackEntry trackEntry, Spine.Event e)
    {
        if (trackEntry.Animation.Name.Equals(apc.fireAnim.name))
        {
             bullet = ObjectPoolerManager.Instance.bulletPooler.GetPooledObject();
             dirBullet = GetTargetTranform() - (Vector2)boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
             angle = Mathf.Atan2(dirBullet.y, dirBullet.x) * Mathf.Rad2Deg;
             rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            bullet.transform.rotation = rotation;
            bullet.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            bullet.SetActive(true);
        }
        else if (trackEntry.Animation.Name.Equals(apc.grenadeAnim.name))
        {
             grenade = ObjectPoolerManager.Instance.grenadePooler.GetPooledObject();
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
    void AnimDie()
    {
        skeletonAnimation.ClearState();
        if (currentAnim == apc.dieAnim)
            return;
        skeletonAnimation.AnimationState.SetAnimation(0, apc.dieAnim, false);
        currentAnim = apc.dieAnim;
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

   public void AnimIdle()
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
        SoundController.instance.PlaySound(soundGame.shootnormal);

    }


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
    public bool haveTarget;
    Vector2 GetTarget()
    {
        //  targetTemp = new Vector2(float.MaxValue, float.MaxValue);
        var dMin = float.MaxValue;
        for (int i = 0; i < GameController.instance.autoTarget.Count; i++)
        {
            var enemy = GameController.instance.autoTarget[i];

            if (enemy.incam)
            {
                var from = (Vector2)transform.position;
                var to = enemy.Origin();
                var d = Vector2.Distance(from, to);

                if (d < dMin)
                {
                    dMin = d;
                    if (d >= 0.2f)
                    {
                        targetTemp = enemy.transform.position;
                        GameController.instance.targetDetectSprite.transform.position = enemy.transform.position;
                        GameController.instance.targetDetectSprite.SetActive(true);
                        haveTarget = true;
                    }

                    else
                    {
                        targetTemp = GetTargetFromDirection(!FlipX ? Vector2.right : Vector2.left);
                        GameController.instance.targetDetectSprite.SetActive(false);
                        haveTarget = false;
                        continue;
                    }
                }
            }
            else
            {
                if (!GameController.instance.joystickShot.GetJoystickState())
                {
                    targetTemp = GetTargetFromDirection(!FlipX ? Vector2.right : Vector2.left);
                    haveTarget = false;
                }
                else
                {
                    targetTemp = GetTargetFromDirection(GameController.instance.shootPosition);
                    haveTarget = false;
                }
            }

        }

        return targetTemp;
    }
    public void SelectNonTarget(Vector2 pos)
    {
        target = GetTargetFromDirection(pos);
        haveTarget = false;
    }

    public void SelectTarget()
    {

        target = GetTarget();
        FlipX = GetTarget().x < transform.position.x;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 18:
                if (GameController.instance.waitForWin)
                    GameController.instance.DoneMission(true);
                break;
        }
    }

}
