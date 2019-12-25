﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBoss1 : BulletEnemy
{
    public float time;
    WaitForSeconds wait;
    private void OnEnable()
    {
        if (wait == null)
        {
            wait = new WaitForSeconds(time);
        }
        Init(4);
        if (rid.gravityScale == 0)
            rid.velocity = (dir1 * speed);
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {

        base.OnTriggerEnter2D(collision);
        if (rid.gravityScale == 0)
            return;

        switch (collision.gameObject.layer)
        {
            case 8:
                AddForceForBullet();
                break;
        }
    }


    void AddForceForBullet()
    {
        rid.velocity = Vector2.zero;
        rid.gravityScale = 0;
        if (gameObject.active)
            StartCoroutine(delayAddForce());
    }
    IEnumerator delayAddForce()
    {
        yield return wait;
        rid.velocity = dir1;
        rid.gravityScale = 1f;

    }
    public override void OnDisable()
    {
        base.OnDisable();
        if (rid.gravityScale == 0)
            return;
        StopAllCoroutines();
    }
}
