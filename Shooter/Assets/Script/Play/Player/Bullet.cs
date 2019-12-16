using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rid;
    public float speed;
    private void OnEnable()
    {
        rid.AddForce(transform.right * speed);
    }
    //private void OnBecameInvisible()
    //{
    //    gameObject.SetActive(false);
    //}

    //void RotateToTarget()
    //{
    //    if (target)
    //    {
    //        if (boxTarget.enabled)
    //        {
    //            Vector3 direction = new Vector3(myTransform.position.x - target.position.x, myTransform.position.y - target.position.y - 0.5f, 0f);
    //            var rota = Quaternion.LookRotation(direction, Vector3.forward);
    //            rota.x = 0f;
    //            rota.y = 0f;
    //            myTransform.rotation = Quaternion.Lerp(myTransform.rotation, rota, turning);
    //            turning += 0.005f;
    //        }
    //    }
    //}
}
