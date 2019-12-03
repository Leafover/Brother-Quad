using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GrenadeEnemy4 : BulletEnemy
{
    public override void Init(int type)
    {
        base.Init(type);
    }
    private void OnEnable()
    {
        Init(1);
    }
    GameObject slowArena;
    public override void Hit()
    {
        base.Hit();
        slowArena = ObjectPoolerManager.Instance.slowArenaGrenadeEnemy4Pooler.GetPooledObject();
        slowArena.transform.position = gameObject.transform.position;
        slowArena.SetActive(true);
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
        }
    }
}
