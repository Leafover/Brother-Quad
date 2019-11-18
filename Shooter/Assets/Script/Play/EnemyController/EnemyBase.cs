using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public enum EnemyState
    {
        idle,
        run,
        attack
    }
    public float timeDelayAttack;
    public EnemyState enemyState = EnemyState.idle;
    public LayerMask lm;
    public Rigidbody2D rid;
    public float health = 3;
    public float speed, distanceActive;
    public Renderer render;
    int dir;
    public SkeletonAnimation skeletonAnimation;

    public virtual void Start()
    {
        skeletonAnimation.AnimationState.Event += Event;
        skeletonAnimation.AnimationState.Complete += Complete;
     //   Debug.Log("---------zo day");
    }

    public int CheckDirFollowPlayer()
    {
        if (PlayerController.instance.playerState == PlayerController.PlayerState.Jump)

            return 0;


        if (transform.position.x < PlayerController.instance.GetTranformPlayer())
        {
            FlipX = true;
            dir = 1;
        }
        else
        {
            FlipX = false;
            dir = -1;
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


    public virtual void OnBecameInvisible()
    {
        PlayerController.instance.autoTarget.Remove(this);
        if (PlayerController.instance.currentEnemyTarget == this)
            PlayerController.instance.currentEnemyTarget = null;
    }
    public virtual void OnBecameVisible()
    {
        PlayerController.instance.autoTarget.Add(this);
    }
    public bool FlipX
    {
        get { return skeletonAnimation.skeleton.FlipX; }
        set { skeletonAnimation.skeleton.FlipX = value; }
    }
    public virtual void OnUpdate()
    {
        if (render.enabled)
            return;
        if (transform.position.x - Camera.main.transform.position.x <= distanceActive)
        {
          //  Debug.Log("enable render");
            render.enabled = true;
        }
    }
    public Vector2 Origin()
    {
        return transform.position;
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            if (!render.enabled)
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
            if (!render.enabled)
                return;
            gameObject.SetActive(false);
        }
    }
}
