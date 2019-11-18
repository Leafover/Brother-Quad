using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Enemy0Controller : EnemyBase
{
    public int indexPath;
    float distanceTravelled;
    private void Start()
    {
        base.Start();
        if(!EnemyManager.instance.enemy0s.Contains(this))
        {
            EnemyManager.instance.enemy0s.Add(this);
        }
    }
    public void OnUpdate()
    {
        base.OnUpdate();
        if (!render.enabled)
            return;

        var deltaTime = Time.deltaTime;
        distanceTravelled += speed * deltaTime;
        transform.position = PathCreatorController.instance.pathCreator[indexPath].path.GetPointAtDistance(distanceTravelled);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.layer == 13)
        {
            gameObject.SetActive(false);
            GameObject explo = ObjectPoolerManager.Instance.effectE0ExploPooler.GetPooledObject();
            explo.transform.position = gameObject.transform.position;
            explo.SetActive(true);
        }
    }
    private void OnBecameInvisible()
    {
        base.OnBecameInvisible();
        if (render.enabled)
            gameObject.SetActive(false);
    }
}
