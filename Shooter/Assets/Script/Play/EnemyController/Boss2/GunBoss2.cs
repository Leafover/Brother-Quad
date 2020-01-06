using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBoss2 : AutoTarget
{
    public Boss2Controller myEnemyBase;

    public void Dead()
    {
        if (GameController.instance.autoTarget.Contains(this))
        {
            GameController.instance.autoTarget.Remove(this);
        }

        if (myEnemyBase.gunList.Contains(this))
            myEnemyBase.gunList.Remove(this);

        myEnemyBase.CalculateAgainHealthAllGunWhenDie(index);
        gameObject.SetActive(false);

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



    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 11:
                if (!myEnemyBase.incam || myEnemyBase.enemyState == EnemyBase.EnemyState.die)
                    return;
                takecrithit = Random.Range(0, 100);
                if (takecrithit <= 10)
                {
                    TakeDamage(PlayerController.instance.damageBullet * 2, true);
                    myEnemyBase.TakeDamage(PlayerController.instance.damageBullet * 2, true, true);
                    if (!GameController.instance.listcirtwhambang[0].gameObject.activeSelf)
                        SoundController.instance.PlaySound(soundGame.soundCritHit);
                    GameController.instance.listcirtwhambang[0].DisplayMe(transform.position);
                }
                else
                {
                    TakeDamage(PlayerController.instance.damageBullet, false);
                    myEnemyBase.TakeDamage(PlayerController.instance.damageBullet, false, true);
                }

                collision.gameObject.SetActive(false);
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
