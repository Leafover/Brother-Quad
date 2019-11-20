using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyV1 : GrenadeEnemy
{
    GameObject effectExplo;
    private Vector3 v_diff;
    private float atan2;
    public override void OnEnable()
    {
        base.OnEnable();
    }
    public override void Hit()
    {
        base.Hit();
        effectExplo = ObjectPoolerManager.Instance.effectExploBulletEnemyV1Pooler.GetPooledObject();
        effectExplo.transform.position = gameObject.transform.position;
        effectExplo.SetActive(true);
        gameObject.SetActive(false);
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    public override void Update()
    {
        v_diff = transform.position + (Vector3)rid.velocity * 20;
        atan2 = Mathf.Atan2(v_diff.y, v_diff.x);
        transform.rotation = Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg);
    }
}
