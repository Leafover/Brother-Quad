using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEnemy : BulletEnemy
{
    public LayerMask lm;
    GameObject targetboom;
    public void OnEnable()
    {
        StartEvent();
        var hit = Physics2D.Raycast(transform.position, -transform.up, 1000, lm);
        if (hit.collider != null)
        {
            targetboom = ObjectPoolerManager.Instance.targetboomPooler.GetPooledObject();
            targetboom.transform.position = hit.point;
            targetboom.SetActive(true);
        }
    }
    public override void OnDisable()
    {
        base.OnDisable();
        targetboom.SetActive(false);
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
