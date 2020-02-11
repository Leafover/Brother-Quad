using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperBulletMiniBoss2 : BulletEnemy
{
    WaitForSeconds wait;
    public override void Init(int type)
    {
        base.Init(type);
    }
    private void OnEnable()
    {
        if (wait == null)
            wait = new WaitForSeconds(0.5f);
        Init(4);
        StartCoroutine(Active());
    }

    IEnumerator Active()
    {
        yield return wait;
        rid.velocity = transform.up * -speed;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        StopAllCoroutines();
    }
}
