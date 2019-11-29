using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    public EnemyBase myEnemy;
    public bool critHit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            if (!critHit)
                PlayerController.instance.TakeDamage(myEnemy.damage);
            else
                PlayerController.instance.TakeDamage(myEnemy.damage + (myEnemy.damage / 100 * 30));
        }
    }
}
