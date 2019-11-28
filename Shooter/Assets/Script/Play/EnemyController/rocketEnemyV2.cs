using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketEnemyV2 : BulletEnemy
{
    public Transform target;
    float turning;
    public override void Init(int type)
    {
        base.Init(type);
        StartCoroutine(ActiveTarget());
    }
    private void OnEnable()
    {
        Init(2);
    }

    IEnumerator ActiveTarget()
    {
        yield return new WaitForSeconds(0.5f);
        target = PlayerController.instance.GetTransformPlayer();
        StartCoroutine(NoneActiveTarget());
    }
    IEnumerator NoneActiveTarget()
    {
        yield return new WaitForSeconds(0.5f);
        target = null;
    }
    private void Update()
    {
        if (!target)
            return;
        RotateToTarget();
    }
    void RotateToTarget()
    {
        Vector3 direction = new Vector3(GetTransform().position.x - target.position.x, GetTransform().position.y - target.position.y - 0.5f, 0f);
        var rota = Quaternion.LookRotation(direction, Vector3.forward);
        rota.x = 0f;
        rota.y = 0f;
        GetTransform().rotation = Quaternion.Lerp(GetTransform().rotation, rota, turning);
        turning += 0.005f;
        rid.velocity = (transform.up * speed);
    }
}
