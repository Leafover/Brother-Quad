using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoInAndGoOutCamera : MonoBehaviour
{
    public bool enemyNotJoinAutoTarget;
    public EnemyBase myEnemyBase;
    public virtual void OnBecameInvisible()
    {
        if (myEnemyBase.enemyState == EnemyBase.EnemyState.die)
            return;
        myEnemyBase.incam = false;
        if (myEnemyBase.canoutcam)
        {
            myEnemyBase.gameObject.SetActive(false);
        }

    }
    public virtual void OnBecameVisible()
    {
        if (myEnemyBase.enemyState == EnemyBase.EnemyState.die)
            return;
        myEnemyBase.OriginPos = myEnemyBase.transform.position;
        myEnemyBase.incam = true;

        if (enemyNotJoinAutoTarget || myEnemyBase.jumpOut == true)
            return;
        if (!GameController.instance.autoTarget.Contains(myEnemyBase))
            GameController.instance.autoTarget.Add(myEnemyBase);

    }
}
