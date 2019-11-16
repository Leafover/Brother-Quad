using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rid;
    private void OnEnable()
    {
        rid.AddForce(transform.right * 0.05f);
    }
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
