using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFearRun : MonoBehaviour
{
    public Rigidbody2D rid;
    public float speed = 2;
    public EnemyBase myEnemy;
    private void OnValidate()
    {
        if (rid == null)
            rid = GetComponent<Rigidbody2D>();
    }
    Vector2 move;
    void Update()
    {
        if (rid != null)
        {
            move = rid.velocity;
            move.x = -speed;
            move.y = rid.velocity.y;
            rid.velocity = move;
        }
        if (transform.position.x < CameraController.instance.bouders[3].transform.position.x)
        {
            myEnemy.enemyfearruns.Remove(this);
            gameObject.SetActive(false);
        }

    }
}
