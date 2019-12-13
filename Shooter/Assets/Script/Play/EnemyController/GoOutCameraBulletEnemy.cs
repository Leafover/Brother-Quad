using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoOutCameraBulletEnemy : MonoBehaviour
{
    public BulletEnemy myBullet;
    public virtual void OnBecameInvisible()
    {
        if (!myBullet.isGrenade)
            myBullet.gameObject.SetActive(false);
        //if (myBullet.myEnemy != null)
        //    myBullet.AutoRemoveMe();
    }
}
