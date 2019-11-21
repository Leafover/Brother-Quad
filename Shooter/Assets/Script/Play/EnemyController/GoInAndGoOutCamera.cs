using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoInAndGoOutCamera : MonoBehaviour
{
    public EnemyBase myEnemyBase;
    public virtual void OnBecameInvisible()
    {
        PlayerController.instance.RemoveTarget(myEnemyBase);
        Debug.Log("becamvisible");
    }
    public virtual void OnBecameVisible()
    {
        PlayerController.instance.autoTarget.Add(myEnemyBase);
        myEnemyBase.OriginPos = myEnemyBase.transform.position;
    }
}
