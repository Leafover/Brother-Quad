using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float health = 3;
    private void OnBecameInvisible()
    {
        PlayerController.playerController.autoTarget.Remove(this);
        if (PlayerController.playerController.currentEnemyTarget == this)
            PlayerController.playerController.currentEnemyTarget = null;
    }
    private void OnBecameVisible()
    {

        PlayerController.playerController.autoTarget.Add(this);
    }

    public Vector2 Origin()
    {
        return transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            health--;
            if (health <= 0)
            {
                gameObject.SetActive(false);
            }
            collision.gameObject.SetActive(false);
        }
        else if(collision.gameObject.layer == 14)
        {
            gameObject.SetActive(false);
        }
    }
}
