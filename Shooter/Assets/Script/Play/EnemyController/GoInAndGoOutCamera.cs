using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoInAndGoOutCamera : MonoBehaviour
{
    public EnemyBase myEnemyBase;
    public virtual void OnBecameInvisible()
    {
        if (PlayerController.instance == null)
            return;
        if (myEnemyBase.enemyState == EnemyBase.EnemyState.die)
            return;
        PlayerController.instance.RemoveTarget(myEnemyBase);
        myEnemyBase.incam = false;

        //  Debug.Log("becamvisible");
    }
    public virtual void OnBecameVisible()
    {
        if (PlayerController.instance == null)
            return;
        if (myEnemyBase.enemyState == EnemyBase.EnemyState.die)
            return;
        if (!PlayerController.instance.autoTarget.Contains(myEnemyBase))
            PlayerController.instance.autoTarget.Add(myEnemyBase);
        myEnemyBase.OriginPos = myEnemyBase.transform.position;
        myEnemyBase.incam = true;
    }
}
