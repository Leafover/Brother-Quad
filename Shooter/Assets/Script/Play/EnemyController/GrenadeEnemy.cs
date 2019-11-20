using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeEnemy : MonoBehaviour
{
    public Vector2 dir;
    public float force;
    public Rigidbody2D rid;
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
    public virtual void OnEnable()
    {
        rid.AddForce(dir * force);
        //  Debug.Log("----------- grenade");
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 8:
                gameObject.SetActive(false);
                break;
            case 13:
                gameObject.SetActive(false);
                break;
        }
    }
}
