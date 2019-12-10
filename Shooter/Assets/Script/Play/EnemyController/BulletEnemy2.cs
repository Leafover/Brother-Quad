using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy2 : BulletEnemy
{
    public float time;
    WaitForSeconds wait;
    public AnimationReferenceAsset animdown, animfly;

    public override void Init(int type)
    {
        base.Init(type);
        rid.gravityScale = 1f;
    }
    private void OnEnable()
    {
        if (wait == null)
        {
            wait = new WaitForSeconds(time);
        }
        //if (/*skelatonAnim != null && skelatonAnim.AnimationState != null*/ GameController.instance != null)
        //{
        //    skelatonAnim.Initialize(true);
        //    skelatonAnim.AnimationState.SetAnimation(0, animfly, true);
        //    skelatonAnim.AnimationState.Complete += OnComplete;
        //    Debug.LogError("active");
        //}
        Init(4);
    }


    //public override void OnDisable()
    //{
    //    base.OnDisable();
    //    if (/*skelatonAnim != null && skelatonAnim.AnimationState != null*/ GameController.instance != null)
    //        skelatonAnim.AnimationState.Complete -= OnComplete;
    //}
    void AddForceForBullet()
    {
        rid.velocity = Vector2.zero;
        rid.gravityScale = 0;
        skelatonAnim.AnimationState.SetAnimation(0, animdown, false);
        if (gameObject.active)
            StartCoroutine(delayAddForce());
        // Debug.LogError("zoooooooooooo");
    }

    //private void OnComplete(TrackEntry trackEntry)
    //{
    //    if (trackEntry.Animation.Name.Equals(animdown.name))
    //    {
    //        skelatonAnim.AnimationState.SetAnimation(0, animfly, true);
    //        rid.velocity = (dir * speed);
    //        rid.gravityScale = 1f;

    //    }
    //}

    public override void Hit()
    {
        base.Hit();
        GameObject g = ObjectPoolerManager.Instance.explobulletenemy2Pooler.GetPooledObject();
        g.transform.position = gameObject.transform.position;
        g.SetActive(true);
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {

        base.OnTriggerEnter2D(collision);
        switch (collision.gameObject.layer)
        {
            case 8:
                AddForceForBullet();
                break;
            case 21:
                AddForceForBullet();
                break;
            case 23:
                AddForceForBullet();
                break;
        }
    }
    IEnumerator delayAddForce()
    {
        yield return wait;
        rid.velocity = (dir * speed);
        rid.gravityScale = 1f;
        skelatonAnim.AnimationState.SetAnimation(0, animfly, true);
    }
    public override void OnDisable()
    {
        base.OnDisable();
          StopAllCoroutines();
    }
}
