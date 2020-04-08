using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using Spine;

public class Enemy0Controller : EnemyBase
{
    public int indexPath;
    float distanceTravelled;
    VertexPath myPath;
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
        myPath = GameController.instance.currentMap.pathCreator[indexPath].path;
    }
    public override void Active()
    {
        base.Active();
        PlayAnim(0, aec.run, true);
        SoundController.instance.PlaySound(soundGame.sounde0move);
    }
    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);

        if (!isActive)
        {
            return;
        }
        if (enemyState == EnemyState.die)
            return;
        CheckDirFollowPlayer(myPath.GetPointAtDistance(myPath.length, EndOfPathInstruction.Stop).x);
        distanceTravelled += speed * deltaTime;
        transform.position = myPath.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
        if (transform.position == myPath.GetPointAtDistance(myPath.length, EndOfPathInstruction.Stop))
        {
            gameObject.SetActive(false);
        }

    }
    GameObject explo;
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.layer == 13)
        {
            gameObject.SetActive(false);
            explo = ObjectPoolerManager.Instance.enemyMachineExploPooler.GetPooledObject();
            explo.transform.position = gameObject.transform.position;
            explo.SetActive(true);
            if (collision.gameObject.tag == "NPC")
                GameController.instance.npcController.TakeDamage(damage1);
            else
                PlayerController.instance.TakeDamage(damage1);
            SoundController.instance.PlaySound(soundGame.exploGrenade);
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

    public override void Dead()
    {
        base.Dead();
        SoundController.instance.PlaySound(soundGame.sounde0die);
    }
}
