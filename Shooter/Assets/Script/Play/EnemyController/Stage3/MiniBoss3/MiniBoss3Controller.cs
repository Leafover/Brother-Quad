using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
public class MiniBoss3Controller : EnemyBase
{
    public float speedMove;
    public Transform gunRotation, gunRotation1, gunRotation2;
    Vector2 posTemp;
    int typeAttack;
    public override void Start()
    {
        base.Start();
        Init();
    }
    public override void Init()
    {
        base.Init();
        randomCombo = Random.Range(4, 6);
        if (!EnemyManager.instance.miniboss3s.Contains(this))
        {
            EnemyManager.instance.miniboss3s.Add(this);
        }
        speedMove = -speed;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.miniboss3s.Contains(this))
        {
            EnemyManager.instance.miniboss3s.Remove(this);
        }
    }
    public override void Active()
    {
        base.Active();
        enemyState = EnemyState.idle;
    }
    Vector2 moveVelocity;
    Vector2 posOfGround;
    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);


        if (!isActive)
        {
            return;
        }
        if (enemyState == EnemyState.die)
            return;

        switch (enemyState)
        {
            case EnemyState.idle:
                moveVelocity = rid.velocity;
                moveVelocity.x = speedMove / 3;
                moveVelocity.y = rid.velocity.y;
                rid.velocity = moveVelocity;
                if (Mathf.Abs(transform.position.x - Camera.main.transform.position.x) <= Camera.main.orthographicSize * 1.1f)
                {
                    if (GameController.instance.uiPanel.CheckWarning())
                    {
                        GameController.instance.uiPanel.warning.SetActive(false);
                        GameController.instance.autoTarget.Add(this);
                        takeDamageBox.enabled = true;
                        // Debug.Log("attack boss");
                    }
                    PlayAnim(1, aec.idle, true);
                    enemyState = EnemyState.attack;
                    rid.velocity = Vector2.zero;
                    takeDamageBox.enabled = true;


                    var hit = Physics2D.Raycast(transform.position, -transform.up, 1000, lmground);
                    if (hit.collider != null)
                    {
                        posOfGround.y = hit.point.y;
                    }

                    for (int i = 0; i < target.Count; i++)
                    {
                        posTarget.y = posOfGround.y;
                        if (!FlipX)
                        {
                            posTarget.x = transform.position.x + Random.Range(1, 3);
                        }
                        else
                        {
                            posTarget.x = transform.position.x - Random.Range(1, 3);
                        }
                    }
                    // Debug.LogError("hahaha");
                }
                break;


            case EnemyState.run:

                timePreviousAttack -= deltaTime;
                if (timePreviousAttack <= 0)
                {
                    transform.position = Vector2.MoveTowards(transform.position, posTemp, deltaTime * speed / 3);
                    if (transform.position.x == posTemp.x && transform.position.y == posTemp.y)
                    {
                        if (previousState == EnemyState.attack)
                        {
                            enemyState = EnemyState.falldown;
                            typeAttack = 0;
                            timePreviousAttack = maxtimeDelayAttack1;
                        }
                        else if (previousState == EnemyState.falldown)
                        {
                            enemyState = EnemyState.attack;
                            typeAttack = 1;
                            timePreviousAttack = maxtimeDelayAttack2;
                        }
                    }
                }
                break;
            case EnemyState.attack:
                if (typeAttack == 0)
                {
                    ShootRocket(deltaTime);
                }
                else if (typeAttack == 1)
                {
                    ShootEnergy(deltaTime);
                }
                break;
            case EnemyState.falldown:
                if (typeAttack == 0)
                {
                    ShootEnergy(deltaTime);
                }
                else if (typeAttack == 1)
                {
                    SpawnEN0(deltaTime);
                }
                break;
        }
    }
    int countShootRocket;
    public List<Transform> target;
    int randomTarget;
    Vector2 posTarget;
    void ShootRocket(float deltaTime)
    {
        timePreviousAttack -= deltaTime;
        if (timePreviousAttack <= 0)
        {
            //randomTarget = Random.Range(0, target.Count);
            //Throw(target[randomTarget]);
            PlayAnim(0, aec.attack1, false);
            //bulletEnemy = ObjectPoolManagerHaveScript.Instance.rocketMiniBoss3Pooler.GetBulletEnemyPooledObject();
            //bulletEnemy.AddProperties(damage1, bulletspeed1);
            //bulletEnemy.BeginDisplay(new Vector2(xVelo, yVelo), this);
            //bulletEnemy.gameObject.SetActive(true);
            combo++;

            if (combo == randomCombo)
            {
                timePreviousAttack = maxtimeDelayAttack1;
                combo = 0;
                countShootRocket++;

                if (countShootRocket == 2)
                {
                    if (PlayerController.instance.transform.position.x > transform.position.x)
                        posTemp.x = Camera.main.transform.position.x - 3;
                    else
                        posTemp.x = Camera.main.transform.position.x + 3;
                    posTemp.y = Camera.main.transform.position.y + 2;
                    previousState = enemyState;
                    enemyState = EnemyState.run;
                    CheckDirFollowPlayer(posTemp.x);
                    countShootRocket = 0;
                    for (int i = 0; i < target.Count; i++)
                    {
                        target[i].gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                timePreviousAttack = maxtimeDelayAttack1 / 3;
                if (target[0].gameObject.activeSelf)
                    return;
                for (int i = 0; i < target.Count; i++)
                {
                    target[i].gameObject.SetActive(true);
                }
            }
        }
        else
        {
            CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());
        }
    }
    void ShootEnergy(float deltaTime)
    {
        timePreviousAttack -= deltaTime;
        if (timePreviousAttack <= 0)
        {
            PlayAnim(0, aec.attack1, false);
            timePreviousAttack = maxtimeDelayAttack1;
            if (enemyState == EnemyState.falldown)
            {
                typeAttack = 1;
                randomCombo = Random.Range(3, 6);
            }
            else if (enemyState == EnemyState.attack)
            {
                typeAttack = 0;
                randomCombo = Random.Range(4, 6);
                for (int i = 0; i < target.Count; i++)
                {
                    posTarget.y = posOfGround.y;
                    if (!FlipX)
                    {
                        posTarget.x = transform.position.x + Random.Range(1, 3);
                    }
                    else
                    {
                        posTarget.x = transform.position.x - Random.Range(1, 3);
                    }
                }
            }
            Debug.Log("shoot energy");
        }
        else
        {
            CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());
        }
    }
    void SpawnEN0(float deltaTime)
    {
        timePreviousAttack -= deltaTime;
        if (timePreviousAttack <= 0)
        {
            PlayAnim(0, aec.attack1, false);
            combo++;
            if (combo == randomCombo)
            {
                if (PlayerController.instance.transform.position.x > transform.position.x)
                    posTemp.x = Camera.main.transform.position.x - 4;
                else
                    posTemp.x = Camera.main.transform.position.x + 4;
                posTemp.y = Camera.main.transform.position.y + 3;
                previousState = enemyState;
                enemyState = EnemyState.run;
                combo = 0;
                timePreviousAttack = maxtimeDelayAttack1;
            }
            else
                timePreviousAttack = maxtimeDelayAttack1 / 3;

            Debug.Log("spawn enemy");
        }
        else
        {
            CheckDirFollowPlayer(PlayerController.instance.GetTranformXPlayer());
        }
    }
    float xVelo, yVelo;
    void Throw(Transform target)
    {
        float xdistance;
        xdistance = target.position.x - transform.position.x;

        float ydistance;
        ydistance = target.position.y - transform.position.y;

        float throwAngle; // in radian

        throwAngle = Mathf.Atan((ydistance + 4.905f) / xdistance);

        float totalVelo = xdistance / Mathf.Cos(throwAngle);
        xVelo = totalVelo * Mathf.Cos(throwAngle);
        yVelo = totalVelo * Mathf.Sin(throwAngle);
    }

    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);

    }
    protected override void OnComplete(TrackEntry trackEntry)
    {
        base.OnComplete(trackEntry);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            PlayAnim(0, aec.idle, true);
        }
    }
    public override void Dead()
    {
        base.Dead();
        for(int i = 0; i < target.Count; i ++)
        {
            target[i].gameObject.SetActive(false);
        }
        StartCoroutine(delayExplo());
    }
    IEnumerator delayExplo()
    {

        exploDie = ObjectPoolerManager.Instance.exploBeforeBoss2DiePooler.GetPooledObject();
        exploDie.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
        exploDie.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        exploDie = ObjectPoolerManager.Instance.exploBeforeBoss2DiePooler.GetPooledObject();
        exploDie.transform.position = boneBarrelGun1.GetWorldPosition(skeletonAnimation.transform);
        exploDie.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        exploDie = ObjectPoolerManager.Instance.exploBeforeBoss2DiePooler.GetPooledObject();
        exploDie.transform.position = boneBarrelGun2.GetWorldPosition(skeletonAnimation.transform);
        exploDie.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        SoundController.instance.PlaySound(soundGame.soundexploenemy);
        exploDie = ObjectPoolerManager.Instance.exploMiniBoss1Pooler.GetPooledObject();
        posExplo.x = gameObject.transform.position.x;
        posExplo.y = gameObject.transform.position.y;
        exploDie.transform.position = posExplo;
        exploDie.SetActive(true);
        CameraController.instance.Shake(CameraController.ShakeType.ExplosionBossShake);
        GameController.instance.SpawnCoin(8, transform.position);
        gameObject.SetActive(false);
    }
}
