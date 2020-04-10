using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBulletMN3 : BulletEnemy
{
    public override void Init(int type)
    {
        base.Init(type);
    }
    private void OnEnable()
    {
        Init(0);
    }
    //public override void OnTriggerEnter2D(Collider2D collision)
    //{
    //    switch (collision.gameObject.layer)
    //    {
    //        case 13:
    //            myEnemy.ChangeStage();
    //            break;
    //    }
    //    base.OnTriggerEnter2D(collision);
    //}
}
