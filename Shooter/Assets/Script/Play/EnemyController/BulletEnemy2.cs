using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy2 : BulletEnemy
{
    public override void Init(int type)
    {
        base.Init(type);
        rid.gravityScale = 1.3f;
    }
    private void OnEnable()
    {
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
                StartCoroutine(delayAddForce());
                break;
        }
    }
    IEnumerator delayAddForce()
    {
        yield return new WaitForSeconds(0.85f);
        rid.velocity = (dir * speed);
        rid.gravityScale = 1.3f;
    }
}
