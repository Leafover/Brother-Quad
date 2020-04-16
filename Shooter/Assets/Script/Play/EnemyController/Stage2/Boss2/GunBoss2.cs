using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBoss2 : AutoTarget
{
    public Boss2Controller myEnemyBase;
    GameObject explo;
    public void Dead()
    {
        if (GameController.instance.autoTarget.Contains(this))
        {
            GameController.instance.autoTarget.Remove(this);
        }

        if (myEnemyBase.gunList.Contains(this))
            myEnemyBase.gunList.Remove(this);

        myEnemyBase.DeadGun(index);
        gameObject.SetActive(false);

        explo = ObjectPoolerManager.Instance.exploGunBoss2Pooler.GetPooledObject();
        explo.transform.position = transform.position;
        explo.SetActive(true);

        //explo = ObjectPoolerManager.Instance.effectSmokeBoss2Pooler.GetPooledObject();
        //explo.transform.position = transform.position;
        //explo.SetActive(true);

        //myEnemyBase.effectsmoke.Add(explo);
    }
    void OnValidate()
    {

    }
    void Start()
    {

    }
    void OnDisable()
    {

    }
    void OnEnable()
    {

    }
    public void TakeDamage(float damage, bool crit, bool takedamgebyrocket)
    {
        if (myEnemyBase.isShield && !takedamgebyrocket)
        {
            SpawnHitEffect();
            SpawnNumberMissionText();
            return;
        }
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Dead();
        }
        SpawnHitEffect();
        SpawnNumberDamageText((int)damage, crit);
    }
    void SpawnNumberMissionText()
    {
        numberText = ObjectPoolManagerHaveScript.Instance.numberDamgageTextPooler.GetNumberDamageTextPooledObject();
        numberText.transform.position = transform.position;
        numberText.Display("Miss", false);
        numberText.tmp.color = Color.gray;
        numberText.gameObject.SetActive(true);
    }
    void SpawnNumberDamageText(int damage, bool crit)
    {
        numberText = ObjectPoolManagerHaveScript.Instance.numberDamgageTextPooler.GetNumberDamageTextPooledObject();
        numberText.transform.position = transform.position;
        numberText.Display("" + (int)damage * 10, crit);
        numberText.gameObject.SetActive(true);
    }
    void SpawnHitEffect()
    {
        hitPosTemp = 0.2f;
        posHitTemp.x = transform.position.x + Random.Range(-hitPosTemp, hitPosTemp);
        posHitTemp.y = transform.position.y + Random.Range(-hitPosTemp, hitPosTemp);

        hiteffect = ObjectPoolerManager.Instance.hitMachinePooler.GetPooledObject();
        hiteffect.transform.position = posHitTemp;
        hiteffect.SetActive(true);
    }

    GameObject explobulletW5;
    ChainLightning chainLightning;
    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 11:
                if (!myEnemyBase.incam || myEnemyBase.enemyState == EnemyBase.EnemyState.die)
                    return;

                if (collision.tag == "bulletW5")
                {
                    explobulletW5 = ObjectPoolerManager.Instance.exploBulletW5Pooler.GetPooledObject();
                    explobulletW5.transform.position = collision.transform.position;
                    explobulletW5.SetActive(true);
                    SoundController.instance.PlaySound(soundGame.soundexplow6);
                    collision.gameObject.SetActive(false);
                }
                else if (collision.tag == "bulletuav")
                {
                    TakeDamage(PlayerController.instance.uav.damageBullet, false, false);
                    myEnemyBase.TakeDamage(PlayerController.instance.damageBullet, false, false, false);
                    collision.gameObject.SetActive(false);
                }
                else
                {
                    takecrithit = Random.Range(0, 100);
                    if (takecrithit <= PlayerController.instance.critRate)
                    {
                        if (collision.tag != "explobulletW5")
                        {
                            TakeDamage(PlayerController.instance.damageBullet + (PlayerController.instance.damageBullet / 100 * PlayerController.instance.critDamage), true, false);
                            myEnemyBase.TakeDamage(PlayerController.instance.damageBullet + (PlayerController.instance.damageBullet / 100 * PlayerController.instance.critDamage), true, true, false);
                        }
                        else
                        {
                            TakeDamage(PlayerController.instance.damageBullet + (PlayerController.instance.damageBullet / 100 * PlayerController.instance.critDamage), true, true);
                            myEnemyBase.TakeDamage(PlayerController.instance.damageBullet + (PlayerController.instance.damageBullet / 100 * PlayerController.instance.critDamage), true, true, true);
                        }

                        if (!GameController.instance.listcirtwhambang[0].gameObject.activeSelf)
                        {
                            switch (GameController.instance.currentChar)
                            {
                                case 0:
                                    SoundController.instance.PlaySound(soundGame.soundCritHit);
                                    break;
                                case 1:
                                    SoundController.instance.PlaySound(soundGame.soundcrithitnv2);
                                    break;
                            }
                        }
                        GameController.instance.listcirtwhambang[0].DisplayMe(transform.position);
                    }
                    else
                    {
                        if (collision.tag != "explobulletW5")
                        {
                            TakeDamage(PlayerController.instance.damageBullet, false, false);
                            myEnemyBase.TakeDamage(PlayerController.instance.damageBullet, false, true, false);
                        }
                        else
                        {
                            TakeDamage(PlayerController.instance.damageBullet, false, true);
                            myEnemyBase.TakeDamage(PlayerController.instance.damageBullet, false, true, true);
                        }
                    }

                    if (collision.tag != "shotgun" && collision.tag != "explobulletW5")
                        collision.gameObject.SetActive(false);
                    if (collision.tag == "bulletw6")
                    {
                        chainLightning = ObjectPoolManagerHaveScript.Instance.chainlightningPooler.GetChainLightningPooledObject();
                        chainLightning.originPos = gameObject;
                        chainLightning.transform.position = gameObject.transform.position;
                        chainLightning.gameObject.SetActive(true);
                    }
                }
                break;
            case 14:
                if (!myEnemyBase.incam || myEnemyBase.enemyState == EnemyBase.EnemyState.die)
                    return;

                if (collision.tag == "effectexploboomplane")
                {
                    TakeDamage(GameController.instance.maybay.damageboom, false, false);
                    myEnemyBase.TakeDamage(GameController.instance.maybay.damageboom, false, true, false);
                }
                else
                {
                    TakeDamage(PlayerController.instance.damgeGrenade, false, false);
                    myEnemyBase.TakeDamage(PlayerController.instance.damgeGrenade, false, true, false);
                    if (currentHealth <= 0)
                    {
                        if (!GameController.instance.listcirtwhambang[1].gameObject.activeSelf)
                            SoundController.instance.PlaySound(soundGame.soundGrenadeKill);
                        GameController.instance.listcirtwhambang[1].DisplayMe(transform.position);
                        MissionController.Instance.DoMission(1, 1);
                        DataController.instance.DoAchievement(3, 1);
                        DataController.instance.DoDailyQuest(1, 1);
                    }
                }
                break;
            case 26:
                if (!myEnemyBase.incam || myEnemyBase.enemyState == EnemyBase.EnemyState.die)
                    return;
                TakeDamage(PlayerController.instance.damgeGrenade, false, false);
                myEnemyBase.TakeDamage(PlayerController.instance.damgeGrenade, false, true, false);
                break;
            case 27:
                if (!myEnemyBase.incam || myEnemyBase.enemyState == EnemyBase.EnemyState.die)
                    return;
                TakeDamage(PlayerController.instance.damageBullet * 3f, false, false);
                myEnemyBase.TakeDamage(PlayerController.instance.damageBullet * 3f, false, true, false);


                switch (GameController.instance.currentChar)
                {
                    case 0:
                        SoundController.instance.PlaySound(soundGame.sounddapchao);
                        break;
                    case 1:
                        SoundController.instance.PlaySound(soundGame.sounddaonv2);
                        break;
                }


                if (currentHealth <= 0)
                {
                    if (!GameController.instance.listcirtwhambang[2].gameObject.activeSelf)
                        SoundController.instance.PlaySound(soundGame.soundWham);
                    GameController.instance.listcirtwhambang[2].DisplayMe(transform.position);
                    MissionController.Instance.DoMission(5, 1);
                }
                break;
            case 20:
                gameObject.SetActive(false);
                break;

        }
    }
}
