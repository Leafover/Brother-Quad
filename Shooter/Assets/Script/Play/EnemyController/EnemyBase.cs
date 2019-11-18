using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float health = 3;
    public float speed,distanceActive;
    public Renderer render;

    public virtual void OnBecameInvisible()
    {
        PlayerController.instance.autoTarget.Remove(this);
        if (PlayerController.instance.currentEnemyTarget == this)
            PlayerController.instance.currentEnemyTarget = null;
    }
    public virtual void OnBecameVisible()
    {
        PlayerController.instance.autoTarget.Add(this);
    }
    public virtual void OnUpdate()
    {
      //  Debug.Log("update..........");
        if(!render.enabled)
        {
            if(transform.position.x - Camera.main.transform.position.x <= distanceActive)
            {
                Debug.Log("enable render");
                render.enabled = true;
            }
        }
    }
    public Vector2 Origin()
    {
        return transform.position;
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            if (!render.enabled)
                return;

            health--;
            if (health <= 0)
            {
                gameObject.SetActive(false);
            }
            collision.gameObject.SetActive(false);
            Debug.LogError("--------------- trung dan");
        }
        else if(collision.gameObject.layer == 14)
        {
            if (!render.enabled)
                return;
            gameObject.SetActive(false);
        }
    }
}
