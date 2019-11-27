using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
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
    public bool canmove;
    Vector2 originPos;
    public float radius;
    public Collider2D boxAttack1, boxAttack2, takeDamageBox;
    public float damage = 3;
    public AnimationReferenceAsset currentAnim;
    public AssetSpineEnemyController aec;
    public float maxtimeDelayAttack = 1, timePreviousAttack;
    public EnemyState enemyState = EnemyState.idle;
    public LayerMask lm = 13;
    public Rigidbody2D rid;
    public float health = 3, currentHealth;
    public float speed = 1, distanceActive = 6;
    public float maxtimedelayChangePos = 6;
    public Renderer render;
    public bool isActive;
    int dir;
    [HideInInspector]
    public int combo, randomCombo;
    public SkeletonAnimation skeletonAnimation;


    public Bone boneBarrelGun;
    [SpineBone]
    public string strboneBarrelGun;
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
            render = transform.GetChild(0).GetComponent<Renderer>();
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
            return boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
        }
    }
    public virtual void Attack(int indexTrack, AnimationReferenceAsset anim, bool loop)
    {
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
        if (Time.time - timePreviousAttack >= timeDelayAttack)
        {
            timePreviousAttack = Time.time;

            skeletonAnimation.AnimationState.SetAnimation(indexTrack, anim, loop);
            if (currentAnim != anim)
                currentAnim = anim;

        }
    }
    public virtual void OnDisable()
    {
        if (skeletonAnimation != null)
        {
            skeletonAnimation.AnimationState.Event -= Event;
            skeletonAnimation.AnimationState.Complete -= Complete;
        }
        acOnUpdate -= OnUpdate;
        GameController.instance.RemoveTarget(this);
        // Debug.Log("-----zoooooooooooooo");
    }
    public virtual void Start()
    {
        skeletonAnimation.Initialize(true);
        //  Debug.Log("init =====");
    }
    public virtual void Init()
    {
        render.gameObject.SetActive(false);
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
            boneBarrelGun = skeletonAnimation.Skeleton.FindBone(strboneBarrelGun);
        if (aec.aimTargetAnim != null)
            skeletonAnimation.AnimationState.SetAnimation(1, aec.aimTargetAnim, false);

        if (!isBoss)
            distanceActive = Camera.main.orthographicSize * 2;
        else
            distanceActive = Camera.main.orthographicSize * 2 + 5;
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
            //     Debug.LogError("dead");
            //      return;
        }

    }



    public bool FlipX
    {
        get { return skeletonAnimation.skeleton.FlipX; }
        set { skeletonAnimation.skeleton.FlipX = value; }
    }
    public virtual void OnUpdate(float deltaTime)
    {

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

        rid.velocity = Vector2.zero;
        takeDamageBox.enabled = false;
        if (aec.die == null)
        {
            GameController.instance.targetDetectSprite.SetActive(false);
            gameObject.SetActive(false);
            return;
        }
        GameController.instance.targetDetectSprite.SetActive(false);
        skeletonAnimation.ClearState();
        PlayAnim(0, aec.die, false);
        enemyState = EnemyState.die;
        GameController.instance.RemoveTarget(this);
        PlayerController.instance.SelectNonTarget(!PlayerController.instance.FlipX ? Vector2.right : Vector2.left);
    }
    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Dead();
        }
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            if (!incam)
                return;
            TakeDamage(1);
            collision.gameObject.SetActive(false);
          //  Debug.LogError("----------take damage 1");
        }
        else if (collision.gameObject.layer == 14)
        {
            if (!incam)
                return;

            TakeDamage(3);
            collision.gameObject.SetActive(false);
        //    Debug.LogError("----------take damage 2");
        }
    }
}

