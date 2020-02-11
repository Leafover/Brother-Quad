using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBoss2 : BulletEnemy
{
    public override void Init(int type)
    {
        base.Init(type);
    }
    private void OnEnable()
    {
        Init(1);
    }
    GameObject effectExplo;
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
                if (transform.position.y <= collision.transform.position.y)
                    return;
                Hit();
                break;
            case 21:
                if (transform.position.y <= collision.transform.position.y)
                    return;
                Hit();
                break;
            case 23:
                if (transform.position.y <= collision.transform.position.y)
                    return;
                Hit();
                break;
        }
    }
}
