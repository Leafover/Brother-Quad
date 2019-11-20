using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [HideInInspector]
    public Vector2 dir = new Vector2(-1,1);
    public Rigidbody2D rid;
    public float speed, damage;
    System.Action hit;

    public virtual void Init(int type)
    {
        switch (type)
        {
            case 0:
                rid.AddForce(transform.right * speed);
                break;
            case 1:
                rid.AddForce(dir * speed);
                break;
        }
        StartEvent();
    }
    public virtual void StartEvent()
    {
        hit += Hit;
    }

    private void OnDisable()
    {
        hit -= Hit;
    }
    public virtual void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
    public virtual void Hit()
    {
        gameObject.SetActive(false);
        PlayerController.instance.TakeDamage(damage);
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 13:
                hit();
                break;
        }
    }
}
