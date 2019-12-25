using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using Spine;
using Spine.Unity;

public class EnemyEN0Controller : EnemyBase
{
    public int indexPath;
    float distanceTravelled;
    VertexPath myPath;

    int activeAttack;

    Vector2 posTemp;

    public override void Start()
    {
        base.Start();
        Init();

    }
    public override void Init()
    {
        base.Init();
        if (!EnemyManager.instance.enemyen0s.Contains(this))
        {
            EnemyManager.instance.enemyen0s.Add(this);
        }
        myPath = GameController.instance.currentMap.pathCreator[indexPath].path;
        activeAttack = 0;
        randomCombo = Random.Range(1, 3);
    }
    public override void Active()
    {
        base.Active();
        PlayAnim(0, aec.run, true);
        SoundController.instance.PlaySound(soundGame.sounde0move);
    }
    public void DetecPosPlayer()
    {
        posTemp.x = PlayerController.instance.GetTranformXPlayer();
        posTemp.y = PlayerController.instance.transform.position.y;
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




     switch(activeAttack)
        {
            case 0:
                CheckDirFollowPlayer(myPath.GetPointAtDistance(myPath.length, EndOfPathInstruction.Stop).x);
                distanceTravelled += speed * deltaTime;
                transform.position = myPath.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);

                if(Mathf.Abs(transform.position.x - PlayerController.instance.GetTranformXPlayer()) <= Random.Range(1.5f,2f))
                {
                    activeAttack = 2;
                    PosBegin = Origin();
                    DetecPosPlayer();
                }

                break;
            case 1:
                CheckDirFollowPlayer(myPath.GetPointAtDistance(myPath.length, EndOfPathInstruction.Stop).x);
                distanceTravelled += speed * deltaTime;
                transform.position = myPath.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
                break;
            case 2:
                transform.position = Vector2.MoveTowards(transform.position, posTemp, deltaTime * speed);
                CheckDirFollowPlayer(posTemp.x);
                if (transform.position.x == posTemp.x && transform.position.y == posTemp.y)
                {
                    activeAttack = 3;
                }
                break;
            case 3:
                transform.position = Vector2.MoveTowards(transform.position, PosBegin, deltaTime * speed);
                CheckDirFollowPlayer(PosBegin.x);
                if(transform.position.x == PosBegin.x && transform.position.y == PosBegin.y)
                {
                    activeAttack = 4;
                }
                break;
            case 4:
                CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());
                Attack(1, aec.attack1, false, maxtimeDelayAttack1);
                break;
        }

        if (transform.position == myPath.GetPointAtDistance(myPath.length, EndOfPathInstruction.Stop))
        {
            gameObject.SetActive(false);
        }

    }

    Vector2 dirBullet;
    Quaternion rotation;
    float angle;

    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            if (!incam)
                return;
            //bullet = ObjectPoolerManager.Instance.bulletEnemy3Pooler.GetPooledObject();
            //var _bulletScript = bullet.GetComponent<BulletEnemy>();
            //_bulletScript.AddProperties(damage1, bulletspeed1);
            //dirBullet = (Vector2)targetPos.transform.position - (Vector2)boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            //angle = Mathf.Atan2(dirBullet.y, dirBullet.x) * Mathf.Rad2Deg;
            //rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //bullet.transform.rotation = rotation;
            //bullet.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            //bullet.SetActive(true);
            DetecPosPlayer();

            bulletEnemy = ObjectPoolManagerHaveScript.Instance.bulletEnemyEN0Pooler.GetBulletEnemyPooledObject();
            bulletEnemy.AddProperties(damage1, bulletspeed1);
            dirBullet = posTemp - (Vector2)boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            angle = Mathf.Atan2(dirBullet.y, dirBullet.x) * Mathf.Rad2Deg;
            rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            bulletEnemy.transform.rotation = rotation;
            bulletEnemy.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            bulletEnemy.gameObject.SetActive(true);

            combo++;
        }
    }
    protected override void OnComplete(TrackEntry trackEntry)
    {
        base.OnComplete(trackEntry);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            if(combo == randomCombo)
            {
                activeAttack = 1;
            }
        }
    }
    void ExPlo()
    {
        gameObject.SetActive(false);
        explo = ObjectPoolerManager.Instance.enemyMachineExploPooler.GetPooledObject();
        explo.transform.position = gameObject.transform.position;
        explo.SetActive(true);
        PlayerController.instance.TakeDamage(damage1);
        SoundController.instance.PlaySound(soundGame.exploGrenade);
    }
    GameObject explo;
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.layer == 13)
        {
            ExPlo();
        }
    }
    public override void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.enemyen0s.Contains(this))
        {
            EnemyManager.instance.enemyen0s.Remove(this);
        }
    }

    public override void Dead()
    {
        base.Dead();
        SoundController.instance.PlaySound(soundGame.sounde0die);
    }
}
