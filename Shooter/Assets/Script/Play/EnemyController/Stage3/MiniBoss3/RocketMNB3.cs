using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMNB3 : BulletEnemy
{
    GameObject effectExplo;
    private Vector3 v_diff;
    private float atan2;

    public override void Init(int type)
    {
        base.Init(type);
    }
    private void OnEnable()
    {
        Init(1);
    }
    public void LateUpdate()
    {
        v_diff = transform.position + (Vector3)rid.velocity * 100;
        atan2 = Mathf.Atan2(v_diff.y, v_diff.x);
        transform.rotation = Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg);
    }

}
