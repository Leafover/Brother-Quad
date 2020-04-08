using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class BulletEnemy : MonoBehaviour
{
    public bool isGrenade;
    public EnemyBase myEnemy;
    [HideInInspector]
    public Vector2 dir = new Vector2(-1, 1);
    [HideInInspector]
    public Vector2 dir1 = new Vector2(-1, 0);
    public Rigidbody2D rid;
    public float speed, damage, timeExist;
    System.Action hit;
    Vector2 myTransform;
    public SkeletonAnimation skelatonAnim;
    public void AddProperties(float _damage, float _speed)
    {
        damage = _damage;
        speed = _speed;
    }
    int tempRange;
    public void SetDir(float _attackrank, bool randomtempRange,bool haveRandomRange = true)
    {
        if (haveRandomRange)
        {
            if (randomtempRange)
                tempRange = Random.Range(0, 100);
            else
                tempRange = Random.Range(0, 50);
        }
        else
            tempRange = 0;

        dir.x = -1 * _attackrank + (-_attackrank / 100 * tempRange);
        dir.y = 1 * speed /*+ (speed / 50 * tempRange)*/;
      //  Debug.LogError("dir:" + ":" + tempRange + ":" + dir);
    }
    public void SetGravity(float _gravity)
    {
        rid.gravityScale = _gravity / 6;
    }
    public void SetTimeExist(float _time)
    {
        timeExist = _time;
    }
    public void AutoRemoveMe()
    {
        if (myEnemy == null)
            return;
        if (myEnemy.listMyBullet.Contains(this))
            myEnemy.listMyBullet.Remove(this);
        myEnemy = null;
    }

    private void OnValidate()
    {
        if (rid == null)
            rid = GetComponent<Rigidbody2D>();

        if (skelatonAnim == null)
            skelatonAnim = GetComponentInChildren<SkeletonAnimation>();

    }
    public virtual void Start()
    {
        if (skelatonAnim != null)
            skelatonAnim.Initialize(true);
    }
    public Transform GetTransform()
    {
        return transform;
    }
    public virtual void Init(int type)
    {

        switch (type)
        {
            case 0:
                rid.velocity = (transform.right * speed);
                break;
            case 1:
                rid.velocity = (dir);
                break;
            case 2:
                rid.velocity = (transform.up * speed);
                break;
            case 3:
                rid.velocity = (dir1 * speed);
                break;
            case 4:
             //   rid.velocity = Vector2.zero;
                break;
        }
        StartEvent();
    }

    public virtual void StartEvent()
    {
        hit += Hit;
        //if (/*skelatonAnim != null && skelatonAnim.AnimationState != null*/GameController.instance != null)
        //{
        //    //  skelatonAnim.AnimationState.Event -= Event;
        //    skelatonAnim.AnimationState.Complete += Complete;
        //}

    }
    public void BeginDisplay(Vector2 _dir, EnemyBase _myEnemy)
    {
        dir = _dir;
        myEnemy = _myEnemy;
    }
    public virtual void OnDisable()
    {
        hit -= Hit;
        rid.velocity = Vector2.zero;
        transform.rotation = Quaternion.identity;
        //if (skelatonAnim != null)
        //{
        //  //  skelatonAnim.AnimationState.Event -= Event;
        //    skelatonAnim.AnimationState.Complete -= Complete;
        //}
        AutoRemoveMe();
    }

    public virtual void Hit()
    {
        gameObject.SetActive(false);

        if (isGrenade)
            CameraController.instance.Shake();
        //   AutoRemoveMe();
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 13:
                if (collision.gameObject.tag == "NPC")
                {
                    GameController.instance.npcController.TakeDamage(damage);
                }
                else
                {
                    PlayerController.instance.TakeDamage(damage);
                }
                if (hit != null)
                    hit();
                break;
        }
    }
    //private void Event(TrackEntry trackEntry, Spine.Event e)
    //{
    //    OnEvent(trackEntry, e);
    //}
    //private void Complete(TrackEntry trackEntry)
    //{
    //    OnComplete(trackEntry);
    //}
    //protected virtual void OnEvent(TrackEntry trackEntry, Spine.Event e)
    //{
    //}
    //protected virtual void OnComplete(TrackEntry trackEntry)
    //{
    //}
}
