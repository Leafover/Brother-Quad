using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaSlowEnemy4 : MonoBehaviour
{
    float timedamage;
    public bool damage;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (PlayerController.instance == null)
            return;
        if (collision.gameObject.layer == 13)
        {
            PlayerController.instance.isSlow = false;
            if (damage)
                PlayerController.instance.isLowJump = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (PlayerController.instance == null)
            return;
        if (collision.gameObject.layer == 13)
        {
            PlayerController.instance.isSlow = true;

            if (damage)
            {
                PlayerController.instance.isLowJump = true;
                PlayerController.instance.slowRate = 45;
                timedamage -= Time.deltaTime;
                if (timedamage <= 0)
                {
                    timedamage = 1;
                    PlayerController.instance.TakeDamage(PlayerController.instance.health / 100 * 5);
                }
            }
            else
            {
                PlayerController.instance.slowRate = 30;
            }
        }
    }

    private void OnDisable()
    {
        if (PlayerController.instance == null)
            return;
        PlayerController.instance.isSlow = false;
        PlayerController.instance.isLowJump = false;
    }

}
