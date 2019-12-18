using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public float numberTemp;

    public void AddNumberTemp(float temp)
    {
        numberTemp = temp;
    }
    public virtual void Hit()
    {
        gameObject.SetActive(false);
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 13:
                Hit();
                break;
        }
    }
    public void CalculateDisable()
    {
        if (transform.position.x < CameraController.instance.NumericBoundaries.LeftBoundary)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        if (GameController.instance == null)
            return;
        if (!GameController.instance.itemDrops.Contains(this))
        {
            GameController.instance.itemDrops.Add(this);
        }
    }
    public virtual void OnDisable()
    {
        if (GameController.instance == null)
            return;
        if(GameController.instance.itemDrops.Contains(this))
        {
            GameController.instance.itemDrops.Remove(this);
        }
    }
}
