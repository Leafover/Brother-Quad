using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public Rigidbody2D rid;
    public float speed, damage;
    System.Action hit;
    public virtual void OnEnable()
    {
        rid.AddForce(transform.right * speed);
        // Debug.Log("------- onenable parent");
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
