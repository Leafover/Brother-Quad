using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public EnemyBase myEnemy;
    [HideInInspector]
    public Vector2 dir = new Vector2(-1, 1);
    [HideInInspector]
    public Vector2 dir1 = new Vector2(-1, 0);
    public Rigidbody2D rid;
    public float speed, damage;
    System.Action hit;
    Vector2 myTransform;
    public void AutoRemoveMe()
    {
        if (myEnemy == null)
            return;
        if (myEnemy.listMyBullet.Contains(this))
            myEnemy.listMyBullet.Remove(this);
        myEnemy = null;
    }
    private void OnValidate()
    {
        if (rid == null)
            rid = GetComponent<Rigidbody2D>();
    }
    public Transform GetTransform()
    {
        return transform;
    }
    public virtual void Init(int type)
    {
        switch (type)
        {
            case 0:
                rid.velocity = (transform.right * speed);
                break;
            case 1:
                rid.velocity = (dir * speed);
                break;
            case 2:
                rid.velocity = (transform.up * speed);
                break;
            case 3:
                rid.velocity = (dir1 * speed);
                break;
            case 4:

                break;
        }
        StartEvent();
    }

    public virtual void StartEvent()
    {
        hit += Hit;
    }
    public void BeginDisplay(Vector2 _dir,EnemyBase _myEnemy)
    {
        dir = _dir;
        myEnemy = _myEnemy;
    }
    private void OnDisable()
    {
        hit -= Hit;
        rid.velocity = Vector2.zero;

    }

    public virtual void Hit()
    {
        gameObject.SetActive(false);
        PlayerController.instance.TakeDamage(damage);
        AutoRemoveMe();
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 13:
                if (hit != null)
                    hit();
                break;
        }
    }
}
