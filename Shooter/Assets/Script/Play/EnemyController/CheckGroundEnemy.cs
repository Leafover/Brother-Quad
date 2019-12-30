using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGroundEnemy : MonoBehaviour
{
    //public EnemyBase myEnemy;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == 8 || collision.gameObject.layer == 21)
    //    {

    //        if (myEnemy == null)
    //            return;
    //        if (myEnemy.enemyState == EnemyBase.EnemyState.die || !myEnemy.isActive)
    //            return;
    //        Debug.LogError("===in====" + ":" + myEnemy.enemyState);
    //        myEnemy.isGround = true;
    //        if (myEnemy.aec.standup != null)
    //            myEnemy.PlayAnim(0, myEnemy.aec.standup, false);
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{

    //    if (collision.gameObject.layer == 8 || collision.gameObject.layer == 21)
    //    {
    //        if (myEnemy == null)
    //            return;
    //        if (myEnemy.enemyState == EnemyBase.EnemyState.die || !myEnemy.isActive)
    //            return;

    //        myEnemy.isGround = false;
    //        Debug.LogError("===out====" + ":" + myEnemy.enemyState + ":" + collision.name);
    //        if (myEnemy.enemyState != EnemyBase.EnemyState.run)
    //        {
    //            if (myEnemy.aec.falldown != null)
    //            {
    //                myEnemy.PlayAnim(0, myEnemy.aec.falldown, true);
    //                Debug.LogError("===fall down====");
    //            }
    //        }
    //        else
    //        {
    //            if (myEnemy.aec.jump != null)
    //            {
    //                myEnemy.PlayAnim(0, myEnemy.aec.jump, true);
    //                Debug.LogError("======jump====");
    //            }
    //            else
    //            {
    //                if (myEnemy.aec.falldown != null)
    //                {
    //                    myEnemy.PlayAnim(0, myEnemy.aec.falldown, true);
    //                    Debug.LogError("====fall down=====");
    //                }
    //            }
    //        }
    //    }
    //}
}
