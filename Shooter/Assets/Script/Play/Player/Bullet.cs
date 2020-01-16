using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rid;
    public float speed, existtime;
    private void OnEnable()
    {
        if (PlayerController.instance == null)
            return;

        existtime = PlayerController.instance.attackRange / 10;
        speed = PlayerController.instance.bulletSpeed / 100;
        rid.AddForce(transform.right * speed);
    }
    private void Update()
    {

        existtime -= Time.deltaTime;
        if (existtime <= 0)
        {
            rid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

}
