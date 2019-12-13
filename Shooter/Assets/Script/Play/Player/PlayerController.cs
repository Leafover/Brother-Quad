﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Spine.Unity;
using Spine;



public class PlayerController : MonoBehaviour
{
    bool isGrenade;
    public float damageBullet = 1, damgeGrenade = 3;
    bool reload;

    public GameObject dustdown, dustrun;
    [HideInInspector]
    public Collider2D colliderStand;
    // [HideInInspector]
    public GameObject currentStand;
    public LineBlood lineBlood;
    public AnimationReferenceAsset currentAnim;
    public AssetSpinePlayerController apc;

    Bone boneBarrelGun, boneHandGrenade;
    [SpineBone]
    public string strboneBarrelGun, strboneHandGrenade;

    float timePreviousAttack, timePreviousGrenade, timePreviousRocket;
    public float timedelayAttackGun, timedelayAttackKnife, timedelayGrenade, timedelayRocket;

    public int numberBullet, maxNumberBullet;
    public float health, maxHealth = 100;

    bool isKnife;

    public Rigidbody2D rid;
    public BoxCollider2D box;
    public Collider2D foot;
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
        Idle, Run, Sit, Jump, WaitStand, Die
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
        StartCoroutine(BeAttackFill());
        SoundController.instance.PlaySound(soundGame.soundplayerhit);
        if (health <= 0)
        {
            GameController.instance.DoneMission(false);
            AnimDie();
            playerState = PlayerState.Die;
            SoundController.instance.PlaySound(soundGame.playerDie);
        }
    }
    public LayerMask lmgroundcandown;
    public void CheckColliderStand(Collider2D _collider)
    {
        if (_collider == null)
        {
            if (colliderStand != null)
                Physics2D.IgnoreCollision(foot, colliderStand, false);
        }
        else
        {
            if (_collider != colliderStand)
            {
                if (colliderStand != null)
                    Physics2D.IgnoreCollision(foot, colliderStand, false);
            }
        }
        colliderStand = _collider;
    }
    public void TryJump()
    {
        if (playerState == PlayerState.Sit)
        {

            if (colliderStand != null)
            {
                // Physics2D.IgnoreLayerCollision(foot.gameObject.layer, colliderStand.gameObject.layer, true);
                Physics2D.IgnoreCollision(foot, colliderStand, true);
                //  Debug.Log("detect" + ":" + colliderStand.gameObject.layer);
                //Physics2D.r
            }

            return;
        }

        if ((playerState != PlayerState.Jump))
        {
            StartCoroutine(Jump());
        }
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
        if (timePreviousGrenade > 0)
            return;
        timePreviousGrenade = timedelayGrenade;

        //    Debug.Log(timePreviousGrenade);
        if (isGround)
        {
            skeletonAnimation.AnimationState.SetAnimation(1, apc.grenadeAnim, false);
            isGrenade = true;
        }
        else
        {
            GameObject grenade = ObjectPoolerManager.Instance.grenadePooler.GetPooledObject();
            grenade.transform.position = boneHandGrenade.GetWorldPosition(skeletonAnimation.transform);
            grenade.SetActive(true);

        }
        SoundController.instance.PlaySound(soundGame.throwGrenade);
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
        AddNumberBullet(-maxNumberBullet);
        timePreviousGrenade = 0;
        timePreviousAttack = 0;

        waitBeAttack = new WaitForSeconds(0.075f);
    }
    public void DetectGround()
    {
        if (isfalldow)
            speedmove = 0;
        isfalldow = false;
        candoublejump = false;

        if (rid.gravityScale == .2f)
            rid.gravityScale = 1;
    }

    private IEnumerator DoubleJump()
    {
        if (GameController.instance.gameState != GameController.GameState.gameover)
        {

            float timeUp = timeJump * 0.5f;
            playerState = PlayerState.Jump;
          //  SoundController.instance.PlaySound(soundGame.soundbtnclick);
            SoundController.instance.PlaySound(soundGame.sounddoublejump);
            AnimJump();
            for (float t = 0; t <= timeUp; t += Time.deltaTime)
            {
                if (playerState == PlayerState.Jump)
                {
                    force = forceJump * (timeUp - t);
                    rid.velocity = new Vector2(rid.velocity.x, force / 2);
                    yield return null;
                }
            }
        }
    }
    private IEnumerator Jump()
    {
        if (GameController.instance.gameState != GameController.GameState.gameover)
        {
         //   SoundController.instance.PlaySound(soundGame.soundbtnclick);
            SoundController.instance.PlaySound(soundGame.soundjump);

            if (rid.gravityScale == .2f)
                rid.gravityScale = 1;
            if (colliderStand != null)
                Physics2D.IgnoreCollision(foot, colliderStand, false);
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
                        //  skeletonAnimation.AnimationState.SetAnimation(2, apc.aimTargetAnim, false);
                    }
                    else
                    {
                        isWaitStand = false;
                        GameController.instance.CheckAfterJump(GameController.instance.joystickMove);
                        //  skeletonAnimation.AnimationState.SetAnimation(2, apc.aimTargetAnim, false);
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
        if (timePreviousAttack > 0)
            timePreviousAttack -= deltaTime;
        if (timePreviousGrenade > 0)
            timePreviousGrenade -= deltaTime;

        GameController.instance.uiPanel.FillGrenade(timePreviousGrenade, timedelayGrenade);
        if (!reload)
            return;
        if (timeReload > 0)
        {
            timeReload -= deltaTime;
            if (timeReload <= 0)
            {
                // skeletonAnimation.AnimationState.SetAnimation(1, apc.fireAnim, false);
                skeletonAnimation.AnimationState.SetEmptyAnimation(1, 0);
                AddNumberBullet(-maxNumberBullet);
                reload = false;
                SoundController.instance.PlaySound(soundGame.soundreload);
            }
        }
    }
    void AddNumberBullet(int _sub)
    {
        numberBullet -= _sub;
        GameController.instance.uiPanel.bulletText.text = "" + numberBullet;
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
    GameObject bullet, grenade;
    Vector2 dirBullet;
    float angle;
    Quaternion rotation;
    void HandleEvent(TrackEntry trackEntry, Spine.Event e)
    {
        if (trackEntry.Animation.Name.Equals(apc.fireAnim.name))
        {
            //if (reload)
            //{
            //    reload = false;
            //    return;
            //}
            bullet = ObjectPoolerManager.Instance.bulletPooler.GetPooledObject();
            dirBullet = GetTargetTranform() - (Vector2)boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            angle = Mathf.Atan2(dirBullet.y, dirBullet.x) * Mathf.Rad2Deg;
            rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            bullet.transform.rotation = rotation;
            bullet.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            bullet.SetActive(true);

            AddNumberBullet(1);
            SoundController.instance.PlaySound(soundGame.shootnormal);
            if (numberBullet == 0)
            {
                skeletonAnimation.AnimationState.SetAnimation(1, apc.reloadAnim, true);
                reload = true;
                timeReload = 1;
                SoundController.instance.PlaySound(soundGame.soundbulletdrop);
            }

        }
        else if (trackEntry.Animation.Name.Equals(apc.grenadeAnim.name))
        {
            grenade = ObjectPoolerManager.Instance.grenadePooler.GetPooledObject();
            grenade.transform.position = boneHandGrenade.GetWorldPosition(skeletonAnimation.transform);
            grenade.SetActive(true);
            isGrenade = false;
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
        if (isfalldow && rid.velocity.y != 0)
        {
            AnimFallDow();
            return;
        }
        else
        {

            if (currentAnim == apc.sitAnim)
                return;
            skeletonAnimation.AnimationState.SetAnimation(0, apc.sitAnim, true);
            currentAnim = apc.sitAnim;
            speedmove = 0;
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
    WaitForSeconds waitBeAttack;
    //   int randomWin;
    IEnumerator BeAttackFill()
    {
        skeletonAnimation.skeleton.SetColor(new Color(1, 1, 1, 1));
        yield return waitBeAttack;

        skeletonAnimation.skeleton.SetColor(new Color(1, 0.5f, 0, 1));
        yield return waitBeAttack;

        skeletonAnimation.skeleton.SetColor(new Color(1, 1, 1, 1));
        yield return waitBeAttack;

        skeletonAnimation.skeleton.SetColor(new Color(1, 0.5f, 0, 1));
        yield return waitBeAttack;

        skeletonAnimation.skeleton.SetColor(Color.white);
    }

    public void AnimWin()
    {
        //   skeletonAnimation.ClearState();
        //randomWin = UnityEngine.Random.Range(0, 2);
        //Debug.Log(randomWin);
        //if (randomWin == 0)
        //{
        //    if (currentAnim == apc.winAnim)
        //        return;
        //    // skeletonAnimation.AnimationState.SetAnimation(0, apc.idleAnim, true);
        //    skeletonAnimation.AnimationState.SetAnimation(0, apc.winAnim, true);
        //    currentAnim = apc.winAnim;
        //}
        //else
        //{
        //    if (currentAnim == apc.winAnim2)
        //        return;
        //    // skeletonAnimation.AnimationState.SetAnimation(0, apc.idleAnim, true);
        //    skeletonAnimation.AnimationState.SetAnimation(0, apc.winAnim2, true);
        //    currentAnim = apc.winAnim2;
        //}
        skeletonAnimation.ClearState();
        if (currentAnim == apc.winAnim)
            return;
        //   skeletonAnimation.AnimationState.SetEmptyAnimation(2, 0);
        //targetTemp = GetTargetFromDirection(!FlipX ? Vector2.right : Vector2.left);
        //target = targetTemp;
        //targetPos.position = targetTemp;

        skeletonAnimation.AnimationState.SetAnimation(0, apc.winAnim, true);
        currentAnim = apc.winAnim;
        speedmove = 0;

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
            skeletonAnimation.AnimationState.SetAnimation(0, apc.idleAnim, true);
            currentAnim = apc.idleAnim;
            speedmove = 0;
            SetBox(sizeBox, offsetBox);
        }
    }

    public void ShootDown()
    {
        if (reload || isGrenade)
        {
            return;
        }

        if (timePreviousAttack > 0)
            return;
        timePreviousAttack = timedelayAttackGun;
        skeletonAnimation.AnimationState.SetAnimation(1, apc.fireAnim, false);

    }
    float timeReload;


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
        targetTemp = new Vector2(float.MaxValue, float.MaxValue);
        var dMin = float.MaxValue;
        for (int i = 0; i < GameController.instance.autoTarget.Count; i++)
        {

            var enemy = GameController.instance.autoTarget[i];

            if (!enemy.incam || enemy.currentHealth <= 0 || !enemy.gameObject.activeSelf)
            {
                haveTarget = false;
                targetTemp = GetTargetFromDirection(!FlipX ? Vector2.right : Vector2.left);
                GameController.instance.targetDetectSprite.SetActive(false);
                continue;
            }

            //if (enemy.incam)
            //{
                var from = (Vector2)transform.position;
                var to = enemy.Origin();
                var d = Vector2.Distance(from, to);

                if (d < dMin)
                {
                    dMin = d;
                    targetTemp = enemy.transform.position;
                    GameController.instance.targetDetectSprite.transform.position = enemy.transform.position;
                    GameController.instance.targetDetectSprite.SetActive(true);
                    haveTarget = true;

                    //if (dMin < 0.5f || target.x >= float.MaxValue || target.y >= float.MaxValue)
                    //{
                    //    haveTarget = false;
                    //    targetTemp = GetTargetFromDirection(!FlipX ? Vector2.right : Vector2.left);
                    //}

                }
                
            //}
            //else
            //{
            //    if (!GameController.instance.joystickShot.GetJoystickState())
            //    {
            //        targetTemp = GetTargetFromDirection(!FlipX ? Vector2.right : Vector2.left);
            //        haveTarget = false;
            //    }
            //    else
            //    {
            //        targetTemp = GetTargetFromDirection(GameController.instance.shootPosition);
            //        haveTarget = false;
            //    }
            //}

        }

        return targetTemp;
    }
    public void SelectNonTarget(Vector2 pos)
    {
        GameController.instance.targetDetectSprite.SetActive(false);
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
                //case 20:
                //    transform.position = new Vector2(transform.position.x, transform.position.y + 2);
                //    rid.velocity = Vector2.zero;
                //    rid.gravityScale = 0.3f;

                //    break;
        }
    }

}
