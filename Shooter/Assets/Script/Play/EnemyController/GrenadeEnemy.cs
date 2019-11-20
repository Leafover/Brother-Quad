using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeEnemy : MonoBehaviour
{
    public Vector2 dir = new Vector2(-1, 1);
    public float force;
    public Rigidbody2D rid;
    System.Action hit;
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        hit -= Hit;
    }
    public virtual void OnEnable()
    {
        rid.AddForce(dir * force);
        hit += Hit;
        //  Debug.Log("----------- grenade");

    }
    public virtual void Update()
    {

    }
    public virtual void Hit()
    {

    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 8:
                hit();
                break;
            case 13:
                hit();
                break;
        }
    }
}
