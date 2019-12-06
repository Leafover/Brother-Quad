using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public float health;
    public enum TYPE
    {
        normal,
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
                    explo = ObjectPoolerManager.Instance.effectGrenadePooler.GetPooledObject();
                    explo.transform.position = transform.position;
                    explo.SetActive(true);
                    break;
            }
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
