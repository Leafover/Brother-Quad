using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
public class Boss1Controller : EnemyBase
{
    public float speedMove;
    public int typeAttack;

    private void Start()
    {
        base.Start();
        Init();
    }
    public override void Init()
    {
        base.Init();
        if (!EnemyManager.instance.boss1s.Contains(this))
        {
            EnemyManager.instance.boss1s.Add(this);
        }
        speedMove = -speed;
        typeAttack = 0;
        randomCombo = Random.Range(-3, -6);
        takeDamageBox.enabled = false;
        maxtimeDelayAttack = 0.3f;
    }

    private void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.boss1s.Contains(this))
        {
            EnemyManager.instance.boss1s.Remove(this);
        }
    }
    Vector2 posTemp;
    void GetPosTemp(float x)
    {
        posTemp.x = x;
        posTemp.y = transform.position.y;
    }
    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        if (enemyState == EnemyState.die)
            return;

        if (!isActive)
        {
            if (transform.position.x - Camera.main.transform.position.x <= distanceActive)
            {
                isActive = true;
                PlayAnim(0, aec.run, true);
                enemyState = EnemyState.idle;
            }
            return;
        }
        switch (enemyState)
        {
            case EnemyState.idle:
                rid.velocity = new Vector2(speedMove, rid.velocity.y);
                if (Mathf.Abs(transform.position.x - Camera.main.transform.position.x) <= Camera.main.orthographicSize * 1.1f)
                {
                    PlayAnim(0, aec.idle, true);
                    enemyState = EnemyState.attack;
                    rid.velocity = Vector2.zero;
                    OriginPos = Origin();
                    takeDamageBox.enabled = true;
                    StartCoroutine(ActiveAttack());
                    // Debug.LogError("hahaha");
                }
                break;
            case EnemyState.attack:
                switch (typeAttack)
                {
                    case 0:
                        if (move)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, posTemp, deltaTime * speed);
                            if (transform.position.x != posTemp.x)
                                return;
                            move = false;
                            PlayAnim(0, aec.idle, true);
                            StartCoroutine(ActiveAttack());
                        }
                        else
                        {
                            if (!canAttack)
                                return;
                            PlayAnim(0, aec.attack1, false);
                        }
                        break;
                    case 1:

                        if (move)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, posTemp, deltaTime * speed);
                            if (transform.position.x != posTemp.x)
                                return;
                            move = false;
                            PlayAnim(0, aec.idle, true);
                        }
                        else
                        {
                            PlayAnim(0, aec.attack2, false);
                        }
                        break;
                    case 3:
                        if (move)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, posTemp, deltaTime * speed);
                            if (transform.position.x != posTemp.x)
                                return;
                            move = false;
                            PlayAnim(0, aec.idle, true);
                            StartCoroutine(ActiveAttack());
                        }
                        else
                        {
                            if (!canAttack)
                                return;
                            Shoot(0, aec.attack3, false, maxtimeDelayAttack);
                        }
                        break;
                }
                break;
        }
    }
    public override void PlayAnim(int indexTrack, AnimationReferenceAsset anim, bool loop)
    {
        base.PlayAnim(indexTrack, anim, loop);
        canAttack = false;
    }
    public bool move, canAttack;

    IEnumerator ActiveAttack()
    {
        yield return new WaitForSeconds(2);
        canAttack = true;
        if (activeType3 == 1)
            activeType3 = 2;
    }
    IEnumerator ActiveMove()
    {
        yield return new WaitForSeconds(2);
        move = true;
        PlayAnim(0, aec.run, true);
        typeAttack = 1;
    }
    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            boxAttack1.gameObject.SetActive(true);
        }
        else if (trackEntry.Animation.Name.Equals(aec.attack2.name))
        {
            boxAttack2.gameObject.SetActive(true);
        }
        else if (trackEntry.Animation.Name.Equals(aec.attack3.name))
        {
            GameObject bullet = ObjectPoolerManager.Instance.bulletBoss1Pooler.GetPooledObject();
            bullet.transform.position = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
            bullet.SetActive(true);
            combo++;
            if (combo == 2)
            {
                maxtimeDelayAttack = 1.5f;
            }
            if (combo == 3 && maxtimeDelayAttack == 1.5f)
                maxtimeDelayAttack = 0.3f;
            if (combo == 4)
                canAttack = false;
        }
    }

    protected override void OnComplete(TrackEntry trackEntry)
    {
        base.OnComplete(trackEntry);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            boxAttack1.gameObject.SetActive(false);
            randomCombo = Random.Range(-3, -6);
            GetPosTemp(OriginPos.x + randomCombo);
            PlayAnim(0, aec.idle, true);
            StartCoroutine(ActiveMove());
            //   Debug.LogError("------------------- posTemp:" + posTemp.x);
        }
        if (trackEntry.Animation.Name.Equals(aec.attack2.name))
        {
            boxAttack2.gameObject.SetActive(false);
            if (activeType3 != 2)
                typeAttack = 0;
            else
                typeAttack = 3;
            GetPosTemp(OriginPos.x);
            move = true;
            PlayAnim(0, aec.run, true);
        }
        if (trackEntry.Animation.Name.Equals(aec.attack3.name))
        {
            if (combo == 4)
            {
                randomCombo = Random.Range(-3, -6);
                GetPosTemp(OriginPos.x + randomCombo);
                PlayAnim(0, aec.idle, true);
                StartCoroutine(ActiveMove());
                combo = 0;
            }
            //   Debug.LogError("------------------- posTemp:" + posTemp.x);
        }
    }
    public int activeType3 = 0;
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (currentHealth <= health / 2)
        {
            if (activeType3 == 0)
                activeType3 = 1;
        }
    }
}
