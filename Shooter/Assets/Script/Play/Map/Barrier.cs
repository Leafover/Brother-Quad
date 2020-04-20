using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public bool cannotDestroy, dongrom;
    public float health;
    public enum TYPE
    {
        smoke,
        wood,
        explo,
        explopoision
    }
    public TYPE types;
    GameObject explo, hiteffect;
    float hitPosTemp;
    Vector2 posHitTemp;

    void TakeDamage(float _damage)
    {

        hitPosTemp = 0.2f;
        posHitTemp.x = transform.position.x + Random.Range(-hitPosTemp, hitPosTemp);
        posHitTemp.y = transform.position.y + Random.Range(-hitPosTemp, hitPosTemp);

        hiteffect = ObjectPoolerManager.Instance.hitMachinePooler.GetPooledObject();
        hiteffect.transform.position = posHitTemp;
        hiteffect.SetActive(true);

        if (cannotDestroy)
            return;
        health -= _damage;
        if (health <= 0)
        {
            switch (types)
            {
                case TYPE.explo:
                    explo = ObjectPoolerManager.Instance.explofuel1Pooler.GetPooledObject();
                    SoundController.instance.PlaySound(soundGame.exploGrenade);
                    CameraController.instance.Shake();
                    break;
                case TYPE.smoke:
                    explo = ObjectPoolerManager.Instance.explofuel2Pooler.GetPooledObject();
                    SoundController.instance.PlaySound(soundGame.soundexploboxcantexplo);
                    break;
                case TYPE.wood:
                    explo = ObjectPoolerManager.Instance.explowoodPooler.GetPooledObject();
                    SoundController.instance.PlaySound(soundGame.soundexploboxcantexplo);
                    break;
                case TYPE.explopoision:
                    explo = ObjectPoolerManager.Instance.explopoisionPooler.GetPooledObject();
                    SoundController.instance.PlaySound(soundGame.soundexploboxcantexplo);
                    poisionArena = ObjectPoolerManager.Instance.poisionArenaPooler.GetPooledObject();
                    posTemp.x = transform.position.x;
                    posTemp.y = transform.position.y - 0.5f;
                    poisionArena.transform.position = posTemp;
                    poisionArena.SetActive(true);
                    break;
            }
            explo.transform.position = transform.position;
            explo.SetActive(true);

            gameObject.SetActive(false);
        }
    }
    Vector2 posTemp;
    GameObject poisionArena;
    GameObject explobulletW5;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 11:

                if (collision.tag == "bulletW5")
                {
                    explobulletW5 = ObjectPoolerManager.Instance.exploBulletW5Pooler.GetPooledObject();
                    explobulletW5.transform.position = collision.transform.position;
                    SoundController.instance.PlaySound(soundGame.soundexplow6);
                    explobulletW5.SetActive(true);
                    collision.gameObject.SetActive(false);
                }
                else if (collision.tag == "bulletuav")
                {
                    TakeDamage(GameController.instance.uav.damageBullet);
                    collision.gameObject.SetActive(false);
                }
                else if (collision.tag == "bulletnpc")
                {
                    TakeDamage(GameController.instance.uav.damageBullet / 3);
                    collision.gameObject.SetActive(false);
                }
                else
                {
                    TakeDamage(PlayerController.instance.damageBullet);
                    //Debug.LogError("zoooooooo");
                    if (collision.tag != "shotgun" && collision.tag != "explobulletW5")
                        collision.gameObject.SetActive(false);
                }
                break;
            case 14:


                if (collision.tag == "effectexploboomplane")
                {
                    TakeDamage(GameController.instance.maybay.damageboom);
                }
                else
                    TakeDamage(PlayerController.instance.damgeGrenade);
                // Debug.LogError("zoooooooo");
                break;
            case 26:
                TakeDamage(PlayerController.instance.damgeGrenade);
                break;
            case 13:
                if (dongrom)
                {
                    TakeDamage(1000);
                    // Debug.LogError("cham");
                }
                break;
        }
    }
}
