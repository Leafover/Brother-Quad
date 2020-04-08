using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using Spine;
using Spine.Unity;

public class EnemyEN0Controller : EnemyBase
{
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
        activeAttack = 0;
        combo = 0;
        randomCombo = 2;
        moveSpeed = enemyAutoSpawn ? speed / 3 : speed;
    }
    float moveSpeed;
    public override void Active()
    {
        base.Active();
        PlayAnim(0, aec.run, true);
        SoundController.instance.PlaySound(soundGame.soundEN0Move);

        posTemp.x = Camera.main.transform.position.x;
        posTemp.y = Camera.main.transform.position.y;

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

        switch (activeAttack)
        {
            case 0://moi vao

                transform.position = Vector2.MoveTowards(transform.position, posTemp, deltaTime * moveSpeed);
                CheckDirFollowPlayer(posTemp.x);
                if (Mathf.Abs(transform.position.x - Camera.main.transform.position.x) <= Random.Range(1f, 3f))
                {
                    activeAttack = 2;
                    PosBegin = new Vector2(Origin().x + Random.Range(-2, 2), Origin().y - 2);
                    DetecPosPlayer();
                }

                break;
            case 1:
                transform.position = Vector2.MoveTowards(transform.position, posTemp, deltaTime * moveSpeed);

                CheckDirFollowPlayer(posTemp.x);
                if (transform.position.x == posTemp.x && transform.position.y == posTemp.y)
                    gameObject.SetActive(false);

                break;
            case 2:
                transform.position = Vector2.MoveTowards(transform.position, posTemp, deltaTime * moveSpeed);
                CheckDirFollowPlayer(posTemp.x);
                if (transform.position.x == posTemp.x && transform.position.y == posTemp.y)
                {
                    activeAttack = 3;
                }
                break;
            case 3:
                transform.position = Vector2.MoveTowards(transform.position, PosBegin, deltaTime * moveSpeed);
                CheckDirFollowPlayer(PosBegin.x);
                if (transform.position.x == PosBegin.x && transform.position.y == PosBegin.y)
                {
                    activeAttack = 4;
                }
                break;
            case 4:
                CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());
                Attack(1, aec.attack1, false, maxtimeDelayAttack1);
                break;


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

            bulletEnemy = ObjectPoolManagerHaveScript.Instance.bulletEnemyEN0Pooler.GetBulletEnemyPooledObject();
            bulletEnemy.AddProperties(damage1, bulletspeed1);
            if (combo == 0)
            {
                dirBullet = posTemp - (Vector2)boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
                bulletEnemy.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            }
            else if (combo == 1)
            {
                dirBullet = posTemp - (Vector2)boneBarrelGun1.GetWorldPosition(skeletonAnimation.transform);
                bulletEnemy.transform.position = boneBarrelGun1.GetWorldPosition(skeletonAnimation.transform);
            }
            angle = Mathf.Atan2(dirBullet.y, dirBullet.x) * Mathf.Rad2Deg;
            rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            bulletEnemy.transform.rotation = rotation;

            bulletEnemy.BeginDisplay(Vector2.zero, this);
            listMyBullet.Add(bulletEnemy);

            bulletEnemy.gameObject.SetActive(true);

            combo++;
            DetecPosPlayer();
            SoundController.instance.PlaySound(soundGame.soundEN0Attack);
        }
    }
    protected override void OnComplete(TrackEntry trackEntry)
    {
        base.OnComplete(trackEntry);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {

            if (combo >= randomCombo)
            {
                activeAttack = 1;
                posTemp.x = CameraController.instance.bouders[3].transform.position.x;
                posTemp.y = Camera.main.transform.position.y + Random.Range(-2, 1);
            }
            //    activeAttack = 1;
        }
    }
    void ExPlo(string tag)
    {
        gameObject.SetActive(false);
        explo = ObjectPoolerManager.Instance.enemyMachineExploPooler.GetPooledObject();
        explo.transform.position = gameObject.transform.position;
        explo.SetActive(true);
        if (tag == "NPC")
            GameController.instance.npcController.TakeDamage(damage1);
        else
            PlayerController.instance.TakeDamage(damage1);
        SoundController.instance.PlaySound(soundGame.exploGrenade);
    }
    GameObject explo;
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.layer == 13)
        {
            ExPlo(collision.gameObject.tag);
        }
    }
    public override void OnDisable()
    {
        base.OnDisable();

        if (EnemyManager.instance == null)
            return;

        if (EnemyManager.instance.enemyen0s.Contains(this))
        {
            EnemyManager.instance.enemyen0s.Remove(this);
        }

        //transform.position = Vector3.zero;
    }

    public override void Dead()
    {
        base.Dead();
        SoundController.instance.PlaySound(soundGame.sounde0die);
    }
}
