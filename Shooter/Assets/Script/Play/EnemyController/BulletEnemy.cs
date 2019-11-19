using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public Rigidbody2D rid;
    public virtual void OnEnable()
    {
        rid.AddForce(transform.right * 0.05f);
    }
    public virtual void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.layer)
        {
            case 13:
                gameObject.SetActive(false);
                break;
        }
    }
}
