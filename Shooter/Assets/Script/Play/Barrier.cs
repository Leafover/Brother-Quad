using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public float health;
    public enum TYPE
    {
        smoke,
        wood,
        explo
    }
    public TYPE types;
    GameObject explo;
    void TakeDamage(float _damage)
    {
        health -= _damage;
        if (health <= 0)
        {
            switch(types)
            {
                case TYPE.explo:
                    explo = ObjectPoolerManager.Instance.explofuel1Pooler.GetPooledObject();
                    SoundController.instance.PlaySound(soundGame.exploGrenade);
                    break;
                case TYPE.smoke:
                    explo = ObjectPoolerManager.Instance.explofuel2Pooler.GetPooledObject();
                    SoundController.instance.PlaySound(soundGame.soundexploboxcantexplo);
                    break;
                case TYPE.wood:
                    explo = ObjectPoolerManager.Instance.explowoodPooler.GetPooledObject();
                    SoundController.instance.PlaySound(soundGame.soundexploboxcantexplo);
                    break;
            }
            explo.transform.position = transform.position;
            explo.SetActive(true);

            gameObject.SetActive(false);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.layer)
        {
            case 11:
                TakeDamage(PlayerController.instance.damageBullet);
                collision.gameObject.SetActive(false);
                break;
            case 14:
                TakeDamage(PlayerController.instance.damgeGrenade);
                break;
        }
    }
}
