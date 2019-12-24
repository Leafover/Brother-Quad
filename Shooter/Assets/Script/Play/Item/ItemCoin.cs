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
    public override void OnEnable()
    {
        if (wait == null)
            wait = new WaitForSeconds(2.5f);
        rid.gravityScale = 1;
        collider.isTrigger = false;
        base.OnEnable();
        rid.velocity = new Vector2(Random.Range(-2.5f, 2.5f), 3);
        StartCoroutine(delayMoveToPlayer());
    }
    public override void CalculateDisable(float deltaTime)
    {
        base.CalculateDisable(deltaTime);
        if (rid.gravityScale == 0)
            transform.position = Vector2.MoveTowards(transform.position, PlayerController.instance.transform.position, deltaTime * 10);

    }
    IEnumerator delayMoveToPlayer()
    {
        yield return wait;
        rid.gravityScale = 0;
        collider.isTrigger = true;
    }
}
