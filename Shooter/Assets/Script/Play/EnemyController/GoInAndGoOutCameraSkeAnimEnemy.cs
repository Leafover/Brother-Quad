using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoInAndGoOutCameraSkeAnimEnemy : GoInAndGoOutCamera
{
    private void OnBecameInvisible()
    {
        if (!myEnemyBase.isActive)
            return;
        base.OnBecameInvisible();
        myEnemyBase.gameObject.SetActive(false);
    }
    
}
