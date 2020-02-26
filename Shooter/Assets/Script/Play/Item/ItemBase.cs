using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public int index;
    public SpriteRenderer render;

    public float numberTemp;
    public Rigidbody2D rid;
    public Collider2D collider;

    public void AddNumberTemp(float temp)
    {
        numberTemp = temp;
    }
    public virtual void Hit()
    {
        gameObject.SetActive(false);
    }
    private void OnValidate()
    {
        if (rid == null)
            rid = GetComponent<Rigidbody2D>();
        if (collider == null)
            collider = GetComponent<Collider2D>();
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 13:
                Hit();
                break;
            case 8:
                rid.gravityScale = 0;
                rid.velocity = Vector2.zero;
              //  Debug.LogError("wtf");
                break;
            case 21:
                rid.gravityScale = 0;
                rid.velocity = Vector2.zero;
             //   Debug.LogError("wtf");
                break;
        }
    }
    public virtual void CalculateDisable(float deltaTime)
    {

    }
    public virtual void OnEnable()
    {
        if (GameController.instance == null)
            return;
        if (!GameController.instance.itemDrops.Contains(this))
        {
            GameController.instance.itemDrops.Add(this);
            rid.gravityScale = 1;
        }
    }
    public virtual void OnDisable()
    {
        if (GameController.instance == null)
            return;
        if (GameController.instance.itemDrops.Contains(this))
        {
            GameController.instance.itemDrops.Remove(this);
        }
    }
}
