using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoInAndGoOutCameraSkeAnimEnemyOnCamera : GoInAndGoOutCamera
{
    public override void OnBecameVisible()
    {
        base.OnBecameVisible();
        myEnemyBase.acBecameVisibleCamera();
    }
}
