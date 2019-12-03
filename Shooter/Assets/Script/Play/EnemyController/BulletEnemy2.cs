using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy2 : BulletEnemy
{
    public float time;
    WaitForSeconds wait;
    public override void Init(int type)
    {
        base.Init(type);
        rid.gravityScale = 1.3f;
    }
    private void OnEnable()
    {
        if (wait == null)
        {
            wait = new WaitForSeconds(time);
        }
        Init(4);
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {

        base.OnTriggerEnter2D(collision);
        switch (collision.gameObject.layer)
        {
            case 8:
                rid.velocity = Vector2.zero;
                rid.gravityScale = 0;
                if (gameObject.active)
                    StartCoroutine(delayAddForce());
                break;
        }
    }
    IEnumerator delayAddForce()
    {
        yield return wait;
        rid.velocity = (dir * speed);
        rid.gravityScale = 1.3f;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        StopAllCoroutines();
    }
}
