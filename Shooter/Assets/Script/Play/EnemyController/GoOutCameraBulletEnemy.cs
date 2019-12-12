using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoOutCameraBulletEnemy : MonoBehaviour
{
    public BulletEnemy myBullet;
    public virtual void OnBecameInvisible()
    {
       myBullet.gameObject.SetActive(false);
    }
}
