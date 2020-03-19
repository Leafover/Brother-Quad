using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rid;
    public float speed, existtime;
    public Vector2 dir;
    public enum TYPE
    {
        NORMAL, TARGET
    }
    public TYPE type = TYPE.NORMAL;
    private void OnEnable()
    {
        if (PlayerController.instance == null)
            return;

        existtime = PlayerController.instance.attackRange / 10;
        speed = PlayerController.instance.bulletSpeed / 100;

        if (type != TYPE.TARGET)
            rid.AddForce(transform.right * speed);
        else
        {
            rid.AddForce(-transform.up * speed / 10);
            StartCoroutine(DelayTarget());
        }
    }
    float angle;
    Quaternion rotation;
    IEnumerator DelayTarget()
    {
        yield return new WaitForSeconds(0.3f);
        rid.velocity = Vector2.zero;

        dir = dir - (Vector2)transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        rid.AddForce(transform.right * speed / 2);

        yield return new WaitForSeconds(0.1f);
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
