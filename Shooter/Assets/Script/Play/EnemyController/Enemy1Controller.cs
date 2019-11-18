using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : EnemyBase
{
    public enum EnemyAnim
    {
        idle,
        hit,
        run
    }
    public EnemyAnim enemyAnim = EnemyAnim.idle;
    bool detectPlayer;
    private void Start()
    {
        base.Start();
        if (!EnemyManager.instance.enemy1s.Contains(this))
        {
            EnemyManager.instance.enemy1s.Add(this);
        }
        StartCoroutine(Move());
    }
    public IEnumerator Move()
    {
        rid.velocity = new Vector2(speed, rid.velocity.y);
        yield return new WaitForEndOfFrame();
        StartCoroutine(Move());
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
    public void OnUpdate()
    {
        base.OnUpdate();
        detectPlayer = Physics2D.OverlapCircle(transform.position, 1f, lm);
        if (!detectPlayer)
        {
            speed = CheckDirFollowPlayer() * 3;
        }
        else
        {
            if (speed != 0)
                speed = 0;
        }
    }
    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);
        Debug.Log("-----------wtf");
    }
}
