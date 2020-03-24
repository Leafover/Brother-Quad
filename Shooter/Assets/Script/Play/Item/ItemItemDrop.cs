using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemItemDrop : ItemBase
{
    public GameObject foot;
    WaitForSeconds wait;
    public override void Hit()
    {
        base.Hit();
    }
    Vector2 point;
    public override void OnEnable()
    {
        base.OnEnable();
        if (wait == null)
            wait = new WaitForSeconds(2.5f);
        scale.x = 0.4f;
        scale.y = 0.4f;
        render.gameObject.transform.localScale = scale;
        foot.SetActive(true);
        isactive = false;
        rid.gravityScale = 1;
        point.x = Random.Range(-2.5f, 2.5f);
        point.y = 3;
        rid.velocity = point;
        StartCoroutine(delayMoveToPlayer());
    }
    bool isactive;
    Vector2 scale;
    public override void CalculateDisable(float deltaTime)
    {
        base.CalculateDisable(deltaTime);
        if (isactive)
        {
            transform.position = Vector2.MoveTowards(transform.position, PlayerController.instance.transform.position, deltaTime * 15);
            scale.x = scale.y = Mathf.Clamp(Vector2.Distance(transform.position, PlayerController.instance.transform.position) / 2, 0.1f, 0.4f);
            //  Debug.LogError(Vector2.Distance(transform.position, PlayerController.instance.transform.position));
            render.gameObject.transform.localScale = scale;
        }

    }
    IEnumerator delayMoveToPlayer()
    {
        yield return wait;
        isactive = true;
        rid.gravityScale = 0;
        foot.SetActive(false);
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        switch (collision.gameObject.layer)
        {
            case 8:
                rid.velocity = Vector2.zero;
                break;
            case 21:
                rid.velocity = Vector2.zero;
                break;
        }
    }
}
