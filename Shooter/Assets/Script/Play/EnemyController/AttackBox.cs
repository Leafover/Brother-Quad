using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    public EnemyBase myEnemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13)
            PlayerController.instance.TakeDamage(myEnemy.damage);
    }
}
