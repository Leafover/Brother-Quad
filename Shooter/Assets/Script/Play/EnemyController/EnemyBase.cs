using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class EnemyBase : MonoBehaviour
{
    public System.Action acBecameVisibleCamera;

    public enum EnemyState
    {
        idle,
        run,
        attack,
    }
    public bool canmove;
    Vector2 originPos;
    public float radius;
    public Collider2D boxAttack1, boxAttack2;
    public float damage = 3;
    public int randomCrithit;
    public AnimationReferenceAsset currentAnim;
    public AssetSpineEnemyController aec;
    public float maxtimeDelayAttack = 1, timePreviousAttack;
    public EnemyState enemyState = EnemyState.idle;
    public LayerMask lm = 13;
    public Rigidbody2D rid;
    public float health = 3;
    public float speed = 1, distanceActive = 6;
    public float maxtimedelayChangePos;
    public Renderer render;
    public bool isActive;
    int dir;
    public int combo;
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

    public void PlayAnim(int indexTrack, AnimationReferenceAsset anim, bool loop)
    {
        if (currentAnim != anim)
        {
            Debug.Log("change anim" + currentAnim.name);
            skeletonAnimation.AnimationState.SetAnimation(indexTrack, anim, loop);
            currentAnim = anim;

        }
    }
    public Vector2 GetOriginGun()
    {
        var vt2 = new Vector2();
        vt2 = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
        return vt2;
    }
    Vector2 targetTemp;
    public Vector2 GetTargetFromDirection(Vector2 direction)
    {
        var origin = GetOriginGun();
        direction.Normalize();
        var hit = Physics2D.Raycast(origin, direction, 1000, lm);
//#if UNITY_EDITOR
//        Debug.DrawRay(origin, direction * 1000, Color.red);
//#endif
        if (hit.collider != null)
        {
            targetTemp = hit.point;
        }
        return targetTemp;
    }
    public Vector2 GetTarget(bool run)
    {
        if (!run)
        {
            if (PlayerController.instance.transform.position.y >= transform.position.y)
            {
                return PlayerController.instance.transform.position;

            }
            else
            {
                return GetTargetFromDirection(FlipX ? Vector2.right : Vector2.left);

            }
        }
        else
        {
            return GetTargetFromDirection(FlipX ? Vector2.right : Vector2.left);
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
    public virtual void Shoot(int indexTrack, AnimationReferenceAsset anim, bool loop)
    {
        if (Time.time - timePreviousAttack >= maxtimeDelayAttack)
        {
            timePreviousAttack = Time.time;

            skeletonAnimation.AnimationState.SetAnimation(indexTrack, anim, loop);
            if (currentAnim != anim)
                currentAnim = anim;

        }
    }
    public virtual void OnDisable()
    {
        skeletonAnimation.AnimationState.Event -= Event;
        skeletonAnimation.AnimationState.Complete -= Complete;
    }
    public virtual void Start()
    {
        acBecameVisibleCamera += AcBecameVisibleCam;
        skeletonAnimation.AnimationState.Event += Event;
        skeletonAnimation.AnimationState.Complete += Complete;
        currentAnim = aec.idle;
        boneBarrelGun = skeletonAnimation.Skeleton.FindBone(strboneBarrelGun);
        if (aec.aimTargetAnim != null)
            skeletonAnimation.AnimationState.SetAnimation(1, aec.aimTargetAnim, false);
    }
    //public void CheckFlip(float posX)
    //{
    //    if (transform.position.x < posX)
    //    {
    //        FlipX = true;
    //        speed = speed;
    //        Debug.Log("zoooooooo 1");
    //    }
    //    else
    //    {
    //        FlipX = false;
    //        Debug.Log("zoooooooo 2");
    //    }
    //}
    public int CheckDirFollowPlayer(float posX)
    {
        //if (PlayerController.instance.playerState == PlayerController.PlayerState.Jump || Mathf.Abs(transform.position.x - PlayerController.instance.GetTranformPlayerType2()) <= 0.5f)

        //    return 0;


        if (transform.position.x < posX/*PlayerController.instance.GetTranformPlayerType2()*/)
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
    { }



    public bool FlipX
    {
        get { return skeletonAnimation.skeleton.FlipX; }
        set { skeletonAnimation.skeleton.FlipX = value; }
    }
    public virtual void OnUpdate()
    {
        //if (isActive)
        //    return;
        //if (transform.position.x - Camera.main.transform.position.x <= distanceActive/* && transform.position.x > Camera.main.transform.position.x*/)
        //{
        //    //  Debug.Log("enable render");
        //    // render.enabled = true;
        //    isActive = true;
        //}
    }

    public Vector2 Origin()
    {
        return transform.position;
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            if (!isActive)
                return;

            if (PlayerController.instance.currentEnemyTarget != this)
                return;

            health--;
            if (health <= 0)
            {
                gameObject.SetActive(false);
            }
            collision.gameObject.SetActive(false);
            //   Debug.LogError("--------------- trung dan");
        }
        else if (collision.gameObject.layer == 14)
        {
            if (!isActive)
                return;
            gameObject.SetActive(false);
        }
    }
    public virtual void AcBecameVisibleCam()
    {

    }
}

