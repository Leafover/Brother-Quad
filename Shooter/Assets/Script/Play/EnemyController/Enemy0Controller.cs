using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using Spine;

public class Enemy0Controller : EnemyBase
{
    public int indexPath;
    float distanceTravelled;
    public override void Start()
    {
        base.Start();
        Init();
    }
    public override void Init()
    {
        base.Init();
        if (!EnemyManager.instance.enemy0s.Contains(this))
        {
            EnemyManager.instance.enemy0s.Add(this);
        }
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);


        if (!isActive)
        {
            if (transform.position.x - Camera.main.transform.position.x <= distanceActive)
            {
                isActive = true;
                render.gameObject.SetActive(true);
            }
            return;
        }
        if (enemyState == EnemyState.die)
            return;

        distanceTravelled += speed * deltaTime;
        transform.position = GameController.instance.currentMap.pathCreator[indexPath].path.GetPointAtDistance(distanceTravelled);

    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.layer == 13)
        {
            gameObject.SetActive(false);
            GameObject explo = ObjectPoolerManager.Instance.effectE0ExploPooler.GetPooledObject();
            explo.transform.position = gameObject.transform.position;
            explo.SetActive(true);
            PlayerController.instance.TakeDamage(damage);
        }
    }
    public override void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.enemy0s.Contains(this))
        {
            EnemyManager.instance.enemy0s.Remove(this);
        }
    }
    //protected override void OnComplete(TrackEntry trackEntry)
    //{
    //    base.OnComplete(trackEntry);
    //    if (trackEntry.Animation.Name.Equals(aec.die.name))
    //    {
    //        gameObject.SetActive(false);
    //    }
    //}
}
