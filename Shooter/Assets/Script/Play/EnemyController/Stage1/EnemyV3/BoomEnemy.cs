using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEnemy : BulletEnemy
{
    public void OnEnable()
    {
        StartEvent();
    }
    public override void Hit()
    {
        base.Hit();
        GameObject effect = ObjectPoolerManager.Instance.effectExploBulletEnemyV1Pooler.GetPooledObject();
        effect.transform.position = gameObject.transform.position;
        effect.SetActive(true);
        SoundController.instance.PlaySound(soundGame.soundv3bombexplo);
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
            case 23:
                Hit();
                break;
        }
    }
}
