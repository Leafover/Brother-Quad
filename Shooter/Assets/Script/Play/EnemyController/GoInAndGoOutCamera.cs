using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoInAndGoOutCamera : MonoBehaviour
{
    public EnemyBase myEnemyBase;
    public virtual void OnBecameInvisible()
    {
        PlayerController.instance.autoTarget.Remove(myEnemyBase);
        if (PlayerController.instance.currentEnemyTarget == this)
            PlayerController.instance.currentEnemyTarget = null;
    }
    public virtual void OnBecameVisible()
    {
        PlayerController.instance.autoTarget.Add(myEnemyBase);
        myEnemyBase.OriginPos = myEnemyBase.transform.position;
    }
}
