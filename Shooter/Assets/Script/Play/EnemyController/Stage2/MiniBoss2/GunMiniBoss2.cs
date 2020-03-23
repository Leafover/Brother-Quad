using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMiniBoss2 : AutoTarget
{
    public MiniBoss2 myEnemyBase;
    GameObject explo;
    public void Dead()
    {
        myEnemyBase.PlayAnim(index + 1, myEnemyBase.dieguns[index]);

        if (index == 0)
            myEnemyBase.haveGun1 = false;
        if (index == 3)
            myEnemyBase.haveGun2 = false;

        if (GameController.instance.autoTarget.Contains(this))
        {
            GameController.instance.autoTarget.Remove(this);
        }

        myEnemyBase.gunList.Remove(this);

        explo = ObjectPoolerManager.Instance.exploBeforeBoss2DiePooler.GetPooledObject();
        explo.transform.position = transform.position;
        explo.SetActive(true);

        myEnemyBase.CalculateAgainHealthAllGun();
        gameObject.SetActive(false);

    }

    public void TakeDamage(float damage, bool crit = false)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Dead();
        }
        SpawnHitEffect();
        SpawnNumberDamageText((int)damage, crit);
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

                if (collision.tag != "bulletW5")
                {
                    takecrithit = Random.Range(0, 100);
                    if (takecrithit <= 10)
                    {
                        TakeDamage(PlayerController.instance.damageBullet + (PlayerController.instance.damageBullet / 100 * PlayerController.instance.critDamage), true);
                        myEnemyBase.TakeDamage(PlayerController.instance.damageBullet + (PlayerController.instance.damageBullet / 100 * PlayerController.instance.critDamage), true, true);
                        if (!GameController.instance.listcirtwhambang[0].gameObject.activeSelf)
                            SoundController.instance.PlaySound(soundGame.soundCritHit);
                        GameController.instance.listcirtwhambang[0].DisplayMe(transform.position);
                    }
                    else
                    {
                        TakeDamage(PlayerController.instance.damageBullet, false);
                        myEnemyBase.TakeDamage(PlayerController.instance.damageBullet, false, true);
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
                else
                {
                    explobulletW5 = ObjectPoolerManager.Instance.exploBulletW5Pooler.GetPooledObject();
                    explobulletW5.transform.position = collision.transform.position;
                    SoundController.instance.PlaySound(soundGame.soundexplow6);
                    explobulletW5.SetActive(true);
                    collision.gameObject.SetActive(false);
                }

                break;
            case 14:
                if (!myEnemyBase.incam || myEnemyBase.enemyState == EnemyBase.EnemyState.die)
                    return;
                TakeDamage(PlayerController.instance.damgeGrenade, false);
                myEnemyBase.TakeDamage(PlayerController.instance.damgeGrenade, false, true);
                if (currentHealth <= 0)
                {
                    if (!GameController.instance.listcirtwhambang[1].gameObject.activeSelf)
                        SoundController.instance.PlaySound(soundGame.soundGrenadeKill);
                    GameController.instance.listcirtwhambang[1].DisplayMe(transform.position);
                    MissionController.Instance.DoMission(1, 1);
                    DataController.instance.DoAchievement(3, 1);
                    DataController.instance.DoDailyQuest(1, 1);
                }
                break;
            case 26:
                if (!myEnemyBase.incam || myEnemyBase.enemyState == EnemyBase.EnemyState.die)
                    return;
                TakeDamage(PlayerController.instance.damgeGrenade, false);
                myEnemyBase.TakeDamage(PlayerController.instance.damgeGrenade, false, true);
                break;
            case 27:
                if (!myEnemyBase.incam || myEnemyBase.enemyState == EnemyBase.EnemyState.die)
                    return;
                TakeDamage(PlayerController.instance.damageBullet * 1.5f, false);
                myEnemyBase.TakeDamage(PlayerController.instance.damageBullet * 1.5f, false, true);
                SoundController.instance.PlaySound(soundGame.sounddapchao);


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
