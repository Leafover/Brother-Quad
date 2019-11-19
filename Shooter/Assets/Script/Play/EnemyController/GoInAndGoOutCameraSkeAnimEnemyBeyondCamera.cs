using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoInAndGoOutCameraSkeAnimEnemyBeyondCamera : GoInAndGoOutCamera
{
    public override void OnBecameInvisible()
    {
        if (!myEnemyBase.isActive)
            return;
        base.OnBecameInvisible();
        myEnemyBase.gameObject.SetActive(false);
    }
    
}
