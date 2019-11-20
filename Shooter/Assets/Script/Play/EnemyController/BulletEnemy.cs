using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public Rigidbody2D rid;
    public float speed, damage;
    public virtual void OnEnable()
    {
        rid.AddForce(transform.right * speed);
       // Debug.Log("------- onenable parent");
    }
    public virtual void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 13:
                gameObject.SetActive(false);
                break;
        }
    }
}
