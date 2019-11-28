using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class Enemy2Controller : EnemyBase
{
    public bool detectPlayer;
    public float speedMove;
    Vector2 posRay;
    Vector2 GetposRay()
    {
        posRay.x = transform.position.x;
        posRay.y = transform.position.y + 1;
        return posRay;
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(GetposRay(), radius);
    //}
    public override void Start()
    {
        base.Start();
        Init();
    }
    public override void Init()
    {
        base.Init();
        if (!EnemyManager.instance.enemy2s.Contains(this))
        {
            EnemyManager.instance.enemy2s.Add(this);
        }
        speedMove = -speed;

        //   Debug.Log("----------------:" + speedMove);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.enemy2s.Contains(this))
        {
            EnemyManager.instance.enemy2s.Remove(this);
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
                PlayAnim(0, aec.run, true);
                enemyState = EnemyState.run;
                render.gameObject.SetActive(true);
            }
            return;
        }
        if (enemyState == EnemyState.die)
            return;

        switch (enemyState)
        {
            case EnemyState.run:
                rid.velocity = new Vector2(speedMove, rid.velocity.y);
                if (Mathf.Abs(transform.position.x - Camera.main.transform.position.x) <= 5)
                {
                    PlayAnim(0, aec.idle, true);
                    enemyState = EnemyState.attack;
                    rid.velocity = Vector2.zero;
                }
                break;
            case EnemyState.attack:
                if (boxAttack1.gameObject.activeSelf)
                    return;
                detectPlayer = Physics2D.OverlapCircle(GetposRay(), radius, lm);
                CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());
                if (detectPlayer)
                {
                    Shoot(1, aec.attack1, false, maxtimeDelayAttack / 3);
                }
                else
                {
                    Shoot(1, aec.attack2, false, maxtimeDelayAttack / 4);
                }
                break;
        }

    }

    Vector2 left = new Vector2(-0.2f, 1.5f);
    Vector2 right = new Vector2(0.2f, 1.5f);
    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            if (!incam)
                return;
            boxAttack1.gameObject.SetActive(true);
        }
        else if (trackEntry.Animation.Name.Equals(aec.attack2.name))
        {
            if (!incam)
                return;
            GameObject bullet = ObjectPoolerManager.Instance.bulletEnemy2Pooler.GetPooledObject();
            bullet.transform.position = gameObject.transform.position;
            var _bulletScript = bullet.GetComponent<BulletEnemy>();
            switch (FlipX)
            {
                
                case true:
                    _bulletScript.BeginDisplay(right,this);
                    break;
                case false:
                    _bulletScript.BeginDisplay(left, this);
                    break;
            }
            listMyBullet.Add(_bulletScript);
            bullet.SetActive(true);
        }
    }
    protected override void OnComplete(TrackEntry trackEntry)
    {
        base.OnComplete(trackEntry);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            boxAttack1.gameObject.SetActive(false);
        }

    }
}
