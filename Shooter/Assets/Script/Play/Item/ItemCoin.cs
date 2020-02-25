using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCoin : ItemBase
{
    WaitForSeconds wait;
    public override void Hit()
    {
        base.Hit();
        DataParam.AddCoin(1);
        SoundController.instance.PlaySound(soundGame.soundEatCoin);
    }
    Vector2 point;
    public override void OnEnable()
    {
        base.OnEnable();
        if (wait == null)
            wait = new WaitForSeconds(2.5f);
        isactive = false;
        rid.gravityScale = 1;
        point.x = Random.Range(-2.5f, 2.5f);
        point.y = 3;
        rid.velocity = point;
        StartCoroutine(delayMoveToPlayer());
    }
    bool isactive;
    public override void CalculateDisable(float deltaTime)
    {
        base.CalculateDisable(deltaTime);
        if (isactive)
            transform.position = Vector2.MoveTowards(transform.position, PlayerController.instance.transform.position, deltaTime * 10);

    }
    IEnumerator delayMoveToPlayer()
    {
        yield return wait;
        isactive = true;
    }
}
