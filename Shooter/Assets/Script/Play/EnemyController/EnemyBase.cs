using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : DataUnit
{
    [HideInInspector]
    public bool jumpOut = false;
    public Transform leftFace, rightFace;
    [HideInInspector]
    public List<BulletEnemy> listMyBullet;
    public LineBlood lineBlood;
    public bool isBoss;
    public System.Action<float> acOnUpdate;
    public bool canoutcam, incam;
    public enum EnemyState
    {
        idle,
        run,
        attack,
        die
    }
    public EnemyState enemyState = EnemyState.idle;
    public bool canmove;

    public float radius;
    public Collider2D boxAttack1, boxAttack2;
    [HideInInspector]
    public Collider2D takeDamageBox;
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float health, timePreviousAttack;
    //  [HideInInspector]
    public float maxtimedelayChangePos = 6;
    [HideInInspector]
    public AnimationReferenceAsset currentAnim;
    public AssetSpineEnemyController aec;
    public float maxtimeDelayAttack = 1;

    public LayerMask lm = 13;
    [HideInInspector]
    public Rigidbody2D rid;

    public float currentHealth, distanceActive = 6;
    public float speed;

    [HideInInspector]
    public bool isActive;
    int dir;
    [HideInInspector]
    public int combo, randomCombo;
    public SkeletonAnimation skeletonAnimation;

    Vector2 originPos;

    public Bone boneBarrelGun, boneBarrelGun1, boneBarrelGun2;
    [SpineBone]
    public string strboneBarrelGun, strboneBarrelGun1, strboneBarrelGun2;
    public Transform targetPos;

    public Vector2 OriginPos
    {
        get { return originPos; }
        set { originPos = value; }
    }

    public virtual void PlayAnim(int indexTrack, AnimationReferenceAsset anim, bool loop)
    {
        if (enemyState == EnemyState.die)
            return;
        if (currentAnim != anim)
        {
            //   Debug.Log("change anim" + currentAnim.name);
            skeletonAnimation.AnimationState.SetAnimation(indexTrack, anim, loop);
            currentAnim = anim;

        }
    }

    private void OnValidate()
    {
        if (rid == null)
        {
            rid = GetComponent<Rigidbody2D>();
        }
        if (skeletonAnimation == null)
        {
            skeletonAnimation = transform.GetChild(0).GetComponent<SkeletonAnimation>();
        }
        if (takeDamageBox == null)
            takeDamageBox = GetComponent<Collider2D>();

    }
    public Vector2 GetOriginGun()
    {
        var vt2 = new Vector2();
        vt2 = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
        return vt2;
    }
    public Vector2 GetTarget(bool run)
    {
        if (!run)
        {
            return PlayerController.instance.transform.position;
        }
        else
        {
            // Debug.LogError(":" + FlipX);
            //if (FlipX)
            //{
            //    Debug.Log("zo day 1" + ":" + rightFace.transform.position + ":" + targetPos.transform.position);
            //    return rightFace.position;

            //}
            //else
            //{
            //    Debug.Log("zo day 2" + ":" + leftFace.transform.position + ":" + targetPos.transform.position);
            //    return leftFace.position;
            //}

            return /*boneBarrelGun.GetWorldPosition(skeletonAnimation.transform)*/FlipX ? rightFace.position : leftFace.position;
        }
    }
    public virtual void Attack(int indexTrack, AnimationReferenceAsset anim, bool loop)
    {
        if (enemyState == EnemyState.die)
            return;
        if (Time.time - timePreviousAttack >= maxtimeDelayAttack)
        {
            timePreviousAttack = Time.time;
            skeletonAnimation.AnimationState.SetAnimation(indexTrack, anim, loop);
            if (currentAnim != anim)
                currentAnim = anim;

        }
    }
    public virtual void Shoot(int indexTrack, AnimationReferenceAsset anim, bool loop, float timeDelayAttack)
    {
        if (enemyState == EnemyState.die)
            return;
        //   Debug.LogError(timePreviousAttack + ":" + timeDelayAttack);
        if (Time.time - timePreviousAttack >= timeDelayAttack)
        {
            timePreviousAttack = Time.time;

            skeletonAnimation.AnimationState.SetAnimation(indexTrack, anim, loop);
            if (currentAnim != anim)
                currentAnim = anim;
            //  Debug.Log("nem luu dan:" + timeDelayAttack);
        }
    }
    public virtual void OnDisable()
    {
        if (skeletonAnimation != null/* && skeletonAnimation.AnimationState != null*/)
        {
            skeletonAnimation.AnimationState.Event -= Event;
            skeletonAnimation.AnimationState.Complete -= Complete;
        }
        acOnUpdate -= OnUpdate;
        GameController.instance.RemoveTarget(this);
    }
    public virtual void Start()
    {
        skeletonAnimation.Initialize(true);
        //  Debug.Log("init =====");
    }
    public virtual void Init()
    {
        if (lineBlood != null)
        {
            lineBlood.Reset();
        }
        timePreviousAttack = maxtimeDelayAttack / 2;
        skeletonAnimation.gameObject.SetActive(false);
        isActive = false;
        if (boxAttack1 != null)
            boxAttack1.gameObject.SetActive(false);
        if (boxAttack2 != null)
            boxAttack2.gameObject.SetActive(false);

        takeDamageBox.enabled = true;
        enemyState = EnemyState.idle;


        acOnUpdate += OnUpdate;
        if (skeletonAnimation != null)
        {
            skeletonAnimation.AnimationState.Event += Event;
            skeletonAnimation.AnimationState.Complete += Complete;
        }

        currentAnim = aec.idle;
        if (skeletonAnimation != null)
        {
            if (boneBarrelGun == null)
            {
                boneBarrelGun = skeletonAnimation.Skeleton.FindBone(strboneBarrelGun);
                boneBarrelGun1 = skeletonAnimation.Skeleton.FindBone(strboneBarrelGun1);
                boneBarrelGun2 = skeletonAnimation.Skeleton.FindBone(strboneBarrelGun2);
            }
        }
        if (aec.aimTargetAnim != null)
            skeletonAnimation.AnimationState.SetAnimation(1, aec.aimTargetAnim, false);

        if (!isBoss)
            distanceActive = Camera.main.orthographicSize * 2;
        else
            distanceActive = Camera.main.orthographicSize * 2 + 5;

        damage = baseDamage * baseLevel;
        health = baseHealth * baseLevel;
        speed = baseSpeed;
        currentHealth = health;


    }

    public int CheckDirFollowPlayer(float posX)
    {

        if (transform.position.x < posX)
        {
            FlipX = true;
            dir = (int)speed;
        }
        else
        {
            FlipX = false;
            dir = -(int)speed;
        }

        return dir;
    }

    private void Event(TrackEntry trackEntry, Spine.Event e)
    {
        OnEvent(trackEntry, e);
    }

    private void Complete(TrackEntry trackEntry)
    {
        OnComplete(trackEntry);
    }

    protected virtual void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {

    }

    protected virtual void OnComplete(TrackEntry trackEntry)
    {
        if (aec.die == null || trackEntry == null)
            return;
        if (trackEntry.Animation.Name.Equals(aec.die.name))
        {
            gameObject.SetActive(false);
            //   Debug.LogError("die?????");
            //     Debug.LogError("dead");
            //      return;
        }

    }



    public bool FlipX
    {
        get { return skeletonAnimation.skeleton.FlipX; }
        set { skeletonAnimation.skeleton.FlipX = value; }
    }
    public virtual void Active()
    {
        isActive = true;
        skeletonAnimation.gameObject.SetActive(true);
        //  Debug.LogError("---------active");

    }
    public virtual void OnUpdate(float deltaTime)
    {
        if (!isActive)
        {
            if (transform.position.x - Camera.main.transform.position.x <= distanceActive)
            {
                Active();
            }
        }
    }
    public void UpdateActionForEnemyManager(float deltaTime)
    {
        if (acOnUpdate == null)
            return;
        acOnUpdate(deltaTime);
    }
    public Vector2 Origin()
    {
        return transform.position;
    }
    public virtual void Dead()
    {
        if (enemyState == EnemyState.die)
            return;

        // DisableAllBullet();
        if (lineBlood != null)
        {
            lineBlood.Hide();
        }
        rid.velocity = Vector2.zero;
        takeDamageBox.enabled = false;
        //if (aec.die == null)
        //{
        //    GameController.instance.targetDetectSprite.SetActive(false);
        //    gameObject.SetActive(false);
        //    return;
        //}
        GameController.instance.targetDetectSprite.SetActive(false);
        skeletonAnimation.ClearState();
        PlayAnim(0, aec.die, false);
        enemyState = EnemyState.die;
        GameController.instance.RemoveTarget(this);
        PlayerController.instance.SelectNonTarget(!PlayerController.instance.FlipX ? Vector2.right : Vector2.left);
        //    Debug.LogError("zooooooooooo day");
        if (isBoss)
        {
            PlayerController.instance.DoneMission(true);
        }
        DisableAllBullet();
    }
    void DisableAllBullet()
    {
        //  Debug.LogError(":::::::" + listMyBullet.Count);
        if (listMyBullet.Count == 0)
            return;
        for (int i = 0; i < listMyBullet.Count; i++)
        {
            listMyBullet[i].myEnemy = null;
            listMyBullet[i].gameObject.SetActive(false);
            // Debug.Log("---------disable");
        }
        listMyBullet.Clear();

    }
    public virtual void TakeDamage(float damage)
    {

        currentHealth -= damage;

        if (currentHealth <= 0)
        {

            Dead();
            //    Debug.LogError("dead");
        }
        if (lineBlood != null)
        {
            lineBlood.Show(currentHealth, health);
        }

    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            if (!incam || enemyState == EnemyState.die)
                return;
            TakeDamage(1);
            collision.gameObject.SetActive(false);
            //  Debug.LogError("take damage 1" + gameObject.name +":"+ collision.name);
            //  Debug.LogError("----------take damage 1");
        }
        else if (collision.gameObject.layer == 14)
        {
            if (!incam || enemyState == EnemyState.die)
                return;

            TakeDamage(3);
            //    Debug.LogError("take damge:" + collision.name);
            //collision.gameObject.SetActive(false);
            // Debug.LogError("take damage 2" + gameObject.name + ":" + collision.name); 
            //    Debug.LogError("----------take damage 2");
        }

    }
}

