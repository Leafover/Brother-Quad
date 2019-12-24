using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    public EnemyBase myEnemy;
    public bool critHit, stun, isflame;
    public int index;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            Damage();
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == 13)
    //    {
    //        if (!isflame)
    //            return;
    //        try
    //        {
    //            if (myEnemy.timePreviousAttack <= 0)
    //            {
    //                PlayerController.instance.TakeDamage(myEnemy.damage1);
    //                myEnemy.timePreviousAttack = myEnemy.maxtimeDelayAttack1;
    //            }
    //        }
    //        catch
    //        {

    //        }
    //    }
    //}

    public void Damage()
    {
        switch (index)
        {
            case 0:
                if (!critHit)
                    PlayerController.instance.TakeDamage(myEnemy.damage1);
                else
                    PlayerController.instance.TakeDamage(myEnemy.damage1 + (myEnemy.damage1 / 100 * 30));

                if (stun)
                {
                    PlayerController.instance.Stun();
                }

                if (isflame)
                    gameObject.SetActive(false);

                break;
            case 1:
                if (!critHit)
                    PlayerController.instance.TakeDamage(myEnemy.damage2);
                else
                    PlayerController.instance.TakeDamage(myEnemy.damage2 + (myEnemy.damage2 / 100 * 30));
                break;
        }
    }


}
