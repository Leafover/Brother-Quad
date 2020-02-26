using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoInAndGoOutCamera : MonoBehaviour
{
    public bool enemyNotJoinAutoTarget;
    public EnemyBase myEnemyBase;

    public bool frameOn;
    public virtual void OnBecameInvisible()
    {
        if (myEnemyBase.enemyState == EnemyBase.EnemyState.die)
            return;
        myEnemyBase.incam = false;
        GameController.instance.RemoveTarget(myEnemyBase);
    }
    public virtual void OnBecameVisible()
    {
        if (myEnemyBase.enemyState == EnemyBase.EnemyState.die)
            return;
        myEnemyBase.PosBegin = myEnemyBase.transform.position;
        myEnemyBase.incam = true;
        if (frameOn)
            myEnemyBase.frameOn = true;

        if (enemyNotJoinAutoTarget)
            return;

        if (!GameController.instance.autoTarget.Contains(myEnemyBase) && myEnemyBase.takeDamageBox != null && (!myEnemyBase.isBoss || !myEnemyBase.isMiniBoss))
        {          
            GameController.instance.autoTarget.Add(myEnemyBase);
            if (myEnemyBase.transform.position.x > Camera.main.transform.position.x)
                GameController.instance.ResetActiveRight();
            else if (myEnemyBase.transform.position.x < Camera.main.transform.position.x)
                GameController.instance.ResetActiveLeft();
        }

        if (CameraController.instance.CheckPoint())
        {
            if (!GameController.instance.enemyLockCam.Contains(myEnemyBase) && !myEnemyBase.enemyAutoSpawn)
            {
                GameController.instance.enemyLockCam.Add(myEnemyBase);
               // GameController.instance.NotSoFastWin();
            }
        }
        else
        {
            if (!GameController.instance.enemyLockCam.Contains(myEnemyBase))
            {
                GameController.instance.enemyLockCam.Add(myEnemyBase);
               // GameController.instance.NotSoFastWin();
            }
        }

    }
}
