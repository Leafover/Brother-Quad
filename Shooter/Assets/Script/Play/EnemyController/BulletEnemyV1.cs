using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyV1 : BulletEnemy
{
    GameObject effectExplo;
    private Vector3 v_diff;
    private float atan2;

    public override void Init(int type)
    {
        base.Init(type);
    }

    private void OnEnable()
    {
        Init(1);
    }

    public override void Hit()
    {
        base.Hit();
        effectExplo = ObjectPoolerManager.Instance.effectExploBulletEnemyV1Pooler.GetPooledObject();
        effectExplo.transform.position = gameObject.transform.position;
        effectExplo.SetActive(true);
        gameObject.SetActive(false);
        SoundController.instance.PlaySound(soundGame.exploGrenade);
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        switch (collision.gameObject.layer)
        {
            case 8:
                Hit();
                break;
            case 21:
                Hit();
                break;
        }
    }

    public void Update()
    {
        v_diff = transform.position + (Vector3)rid.velocity * 20;
        atan2 = Mathf.Atan2(v_diff.y, v_diff.x);
        transform.rotation = Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg);
    }
}
