using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManagerHaveScript : MonoBehaviour
{
    [HideInInspector]
    public ObjectPoolerHaveScript numberDamgageTextPooler, bullet3EnemyBasepooler, bullet4EnemyBasepooler, grenade4EnemyBasepooler, bulletEnemyV1Pooler,boomEnemyV3Pooler, rocketEnemyV2Pooler, rocketMiniBoss1Pooler, bulletBoss1Pooler, bulletEnemy2Pooler, bulletEnemy6Pooler, enemy1Pooler, enemy5Pooler;
    public NumberDamageTextController numberDamgageTextPrefab;
    public BulletEnemy bulletEnemy3BasePrefab, bulletEnemy4BasePrefab, grenade4EnemyBasePrefab, bulletEnemyV1Prefab, boomEnemyV3Prefab, rocketEnemyV2Prefab, rocketMiniBoss1Prefab, bulletBoss1Prefab, bulletEnemy2Prefab, bulletEnemy6Prefab;
    public EnemyBase enemy1Prefab, enemy5Prefab;
    [HideInInspector]
    public static ObjectPoolManagerHaveScript Instance { get; private set; }
    public List<ObjectPoolerHaveScript> AllPool = new List<ObjectPoolerHaveScript>();
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            DestroyImmediate(gameObject);
    }
    public void ClearAllPool()
    {
        for (int i = 0; i < AllPool.Count; i++)
        {
            for (int j = 0; j < AllPool[i].transform.childCount; j++)
            {
                if (!AllPool[i].transform.GetChild(j).gameObject.activeSelf)
                    continue;

                AllPool[i].transform.GetChild(j).gameObject.SetActive(false);
            }
        }
    }
    public void Start()
    {
        if (numberDamgageTextPooler == null)
        {
            go = new GameObject("numberDamgageTextPooler");
            numberDamgageTextPooler = go.AddComponent<ObjectPoolerHaveScript>();
            numberDamgageTextPooler.numberDamageTextPooledObject = numberDamgageTextPrefab;
            go.transform.parent = this.gameObject.transform;
            numberDamgageTextPooler.InitializeNumberDamageText(20);
            AllPool.Add(numberDamgageTextPooler);
        }
        if (bullet3EnemyBasepooler == null)
        {
            go = new GameObject("bullet3EnemyBasepooler");
            bullet3EnemyBasepooler = go.AddComponent<ObjectPoolerHaveScript>();
            bullet3EnemyBasepooler.bulletEnemyPooledObject = bulletEnemy3BasePrefab;
            go.transform.parent = this.gameObject.transform;
            bullet3EnemyBasepooler.InitializeBulletEnemy(7);
            AllPool.Add(bullet3EnemyBasepooler);
        }
        if (bullet4EnemyBasepooler == null)
        {
            go = new GameObject("bullet4EnemyBasepooler");
            bullet4EnemyBasepooler = go.AddComponent<ObjectPoolerHaveScript>();
            bullet4EnemyBasepooler.bulletEnemyPooledObject = bulletEnemy4BasePrefab;
            go.transform.parent = this.gameObject.transform;
            bullet4EnemyBasepooler.InitializeBulletEnemy(7);
            AllPool.Add(bullet4EnemyBasepooler);
        }
        if (grenade4EnemyBasepooler == null)
        {
            go = new GameObject("grenade4EnemyBasepooler");
            grenade4EnemyBasepooler = go.AddComponent<ObjectPoolerHaveScript>();
            grenade4EnemyBasepooler.bulletEnemyPooledObject = grenade4EnemyBasePrefab;
            go.transform.parent = this.gameObject.transform;
            grenade4EnemyBasepooler.InitializeBulletEnemy(5);
            AllPool.Add(grenade4EnemyBasepooler);
        }
        if (bulletEnemyV1Pooler == null)
        {
            go = new GameObject("bulletEnemyV1Pooler");
            bulletEnemyV1Pooler = go.AddComponent<ObjectPoolerHaveScript>();
            bulletEnemyV1Pooler.bulletEnemyPooledObject = bulletEnemyV1Prefab;
            go.transform.parent = this.gameObject.transform;
            bulletEnemyV1Pooler.InitializeBulletEnemy(5);
            AllPool.Add(bulletEnemyV1Pooler);
        }
        if (boomEnemyV3Pooler == null)
        {
            go = new GameObject("boomEnemyV3Pooler");
            boomEnemyV3Pooler = go.AddComponent<ObjectPoolerHaveScript>();
            boomEnemyV3Pooler.bulletEnemyPooledObject = boomEnemyV3Prefab;
            go.transform.parent = this.gameObject.transform;
            boomEnemyV3Pooler.InitializeBulletEnemy(5);
            AllPool.Add(boomEnemyV3Pooler);
        }
        if (rocketEnemyV2Pooler == null)
        {
            go = new GameObject("rocketEnemyV2Pooler");
            rocketEnemyV2Pooler = go.AddComponent<ObjectPoolerHaveScript>();
            rocketEnemyV2Pooler.bulletEnemyPooledObject = rocketEnemyV2Prefab;
            go.transform.parent = this.gameObject.transform;
            rocketEnemyV2Pooler.InitializeBulletEnemy(5);
            AllPool.Add(rocketEnemyV2Pooler);
        }
        if (rocketMiniBoss1Pooler == null)
        {
            go = new GameObject("rocketMiniBoss1Pooler");
            rocketMiniBoss1Pooler = go.AddComponent<ObjectPoolerHaveScript>();
            rocketMiniBoss1Pooler.bulletEnemyPooledObject = rocketMiniBoss1Prefab;
            go.transform.parent = this.gameObject.transform;
            rocketMiniBoss1Pooler.InitializeBulletEnemy(5);
            AllPool.Add(rocketMiniBoss1Pooler);
        }
        if (bulletBoss1Pooler == null)
        {
            go = new GameObject("bulletBoss1Pooler");
            bulletBoss1Pooler = go.AddComponent<ObjectPoolerHaveScript>();
            bulletBoss1Pooler.bulletEnemyPooledObject = bulletBoss1Prefab;
            go.transform.parent = this.gameObject.transform;
            bulletBoss1Pooler.InitializeBulletEnemy(5);
            AllPool.Add(bulletBoss1Pooler);
        }
        if (bulletEnemy2Pooler == null)
        {
            go = new GameObject("bulletEnemy2Pooler");
            bulletEnemy2Pooler = go.AddComponent<ObjectPoolerHaveScript>();
            bulletEnemy2Pooler.bulletEnemyPooledObject = bulletEnemy2Prefab;
            go.transform.parent = this.gameObject.transform;
            bulletEnemy2Pooler.InitializeBulletEnemy(10);
            AllPool.Add(bulletEnemy2Pooler);
        }
        if (bulletEnemy6Pooler == null)
        {
            go = new GameObject("bulletEnemy6Pooler");
            bulletEnemy6Pooler = go.AddComponent<ObjectPoolerHaveScript>();
            bulletEnemy6Pooler.bulletEnemyPooledObject = bulletEnemy6Prefab;
            go.transform.parent = this.gameObject.transform;
            bulletEnemy6Pooler.InitializeBulletEnemy(6);
            AllPool.Add(bulletEnemy6Pooler);
        }
        if (enemy1Pooler == null)
        {
            go = new GameObject("enemy1Pooler");
            enemy1Pooler = go.AddComponent<ObjectPoolerHaveScript>();
            enemy1Pooler.enemyPooledObject = enemy1Prefab;
            go.transform.parent = this.gameObject.transform;
            enemy1Pooler.InitializeEnemy(6);
            AllPool.Add(enemy1Pooler);
        }
        if (enemy5Pooler == null)
        {
            go = new GameObject("enemy5Pooler");
            enemy5Pooler = go.AddComponent<ObjectPoolerHaveScript>();
            enemy5Pooler.enemyPooledObject = enemy5Prefab;
            go.transform.parent = this.gameObject.transform;
            enemy5Pooler.InitializeEnemy(6);
            AllPool.Add(enemy5Pooler);
        }
    }
    GameObject go;
}
