using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMiniBoss2 : BulletEnemy
{
    WaitForSeconds wait;
    public Transform target;
    float turning;
    public override void Init(int type)
    {
        base.Init(type);
        StartCoroutine(ActiveTarget());
    }
    private void OnEnable()
    {
        if (wait == null)
            wait = new WaitForSeconds(0.5f);

        Init(4);
    }

    public IEnumerator ActiveTarget()
    {
        yield return wait;
        target = PlayerController.instance.GetTransformPlayer();
        StartCoroutine(NoneActiveTarget());
    }
    IEnumerator NoneActiveTarget()
    {
        yield return new WaitForSeconds(timeExist);
        target = null;
    }
    private void Update()
    {
        if (!target)
            return;
        RotateToTarget();
    }
    Vector3 direction;
    void RotateToTarget()
    {
        direction.x = GetTransform().position.x - target.position.x;
        direction.y = GetTransform().position.y - target.position.y /*- 0.5f*/;
        direction.z = 0;

        var rota = Quaternion.LookRotation(direction, Vector3.forward);
        rota.x = 0f;
        rota.y = 0f;
        GetTransform().rotation = Quaternion.Lerp(GetTransform().rotation, rota, turning);
        turning += 0.005f;
        rid.velocity = (transform.up * speed);
    }
}
