using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GrenadeEnemy4 : GrenadeEnemy
{
    GameObject slowArena;
    public override void Hit()
    {
        base.Hit();
        slowArena = ObjectPoolerManager.Instance.slowArenaGrenadeEnemy4Pooler.GetPooledObject();
        slowArena.transform.position = gameObject.transform.position;
        slowArena.SetActive(true);
        gameObject.SetActive(false);
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
