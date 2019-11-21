using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoInAndGoOutCameraSkeAnimEnemyBeyondCamera : GoInAndGoOutCamera
{
    public override void OnBecameInvisible()
    {
        base.OnBecameInvisible();
        if (!myEnemyBase.isActive)
            return;
        myEnemyBase.gameObject.SetActive(false);
    }
    
}
