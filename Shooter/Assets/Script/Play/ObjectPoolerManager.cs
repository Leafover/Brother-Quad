using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPoolerManager : MonoBehaviour
{
    [HideInInspector]
    public ObjectPooler bulletPooler, grenadePooler, effectGrenadePooler/*, effectE0ExploPooler*/, bulletEnemy3Pooler, bulletEnemy4Pooler, grenadeEnemy4Pooler, slowArenaGrenadeEnemy4Pooler, bulletEnemyV1Pooler, effectExploBulletEnemyV1Pooler, boomEnemyV3Pooler, effectExploBoomEnemyV3Pooler, rocketEnemyV2Pooler, rocketMiniBoss1Pooler, enemy5Pooler, bulletEnemy6Pooler, bulletEnemy2Pooler, bulletBoss1Pooler, enemyMachineExploPooler, enemyExploPooler, boss1ExploPooler, explofuel1Pooler, explofuel2Pooler, explowoodPooler, explobulletenemy2Pooler, hitMachinePooler, enemy1Pooler, healthItemPooler, coinItemPooler,exploMiniBoss1Pooler;
    public GameObject bulletPrefab, grenadePrefab, effectGrenadePrefab/*, effectE1ExploPrefab*/, bulletEnemy3Prefab, bulletEnemy4Prefab, grenadeEnemy4Prefab, slowArenaGrenadeEnemy4Prefab, bulletEnemyV1Prefab, effectExploBulletEnemyV1Prefab, boomEnemyV3Prefab, effectExploBoomEnemyV3Prefab, rocketEnemyV2Prefab, rocketMiniBoss1Prefab, enemy5Prefab, bulletEnemy6Prefab, bulletEnemy2Prefab, bulletBoss1Prefab, enemyMachineExploPrefab, enemyExploPrefab, boss1ExploPrefab, explofuel1Prefab, explofuel2Prefab, explowoodPrefab, explobulletenemy2Prefab, hitMachinePrefab, enemy1Prefab, healthItemPrefab, coinItemPrefab, exploMiniBoss1Prefab;
    [HideInInspector]
    public static ObjectPoolerManager Instance { get; private set; }
    public List<ObjectPooler> AllPool = new List<ObjectPooler>();
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
        for(int i = 0; i < AllPool.Count; i ++)
        {
            for(int j = 0; j < AllPool[i].transform.childCount; j ++)
            {
                if (!AllPool[i].transform.GetChild(j).gameObject.activeSelf)
                    continue;

                AllPool[i].transform.GetChild(j).gameObject.SetActive(false);
            }
        }
    }
    public void Start()
    {
        if (bulletPooler == null)
        {
            go = new GameObject("bulletPooler");
            bulletPooler = go.AddComponent<ObjectPooler>();
            bulletPooler.PooledObject = bulletPrefab;
            go.transform.parent = this.gameObject.transform;
            bulletPooler.Initialize(10);
            AllPool.Add(bulletPooler);
        }
        if (grenadePooler == null)
        {
            go = new GameObject("grenadePooler");
            grenadePooler = go.AddComponent<ObjectPooler>();
            grenadePooler.PooledObject = grenadePrefab;
            go.transform.parent = this.gameObject.transform;
            grenadePooler.Initialize(3);
            AllPool.Add(grenadePooler);
        }
        if (effectGrenadePooler == null)
        {
            go = new GameObject("effectGrenadePooler");
            effectGrenadePooler = go.AddComponent<ObjectPooler>();
            effectGrenadePooler.PooledObject = effectGrenadePrefab;
            go.transform.parent = this.gameObject.transform;
            effectGrenadePooler.Initialize(3);
            AllPool.Add(effectGrenadePooler);
        }
        if (bulletEnemy3Pooler == null)
        {
            go = new GameObject("bulletEnemy3Pooler");
            bulletEnemy3Pooler = go.AddComponent<ObjectPooler>();
            bulletEnemy3Pooler.PooledObject = bulletEnemy3Prefab;
            go.transform.parent = this.gameObject.transform;
            bulletEnemy3Pooler.Initialize(10);
            AllPool.Add(bulletEnemy3Pooler);
        }
        if (bulletEnemy4Pooler == null)
        {
            go = new GameObject("bulletEnemy4Pooler");
            bulletEnemy4Pooler = go.AddComponent<ObjectPooler>();
            bulletEnemy4Pooler.PooledObject = bulletEnemy4Prefab;
            go.transform.parent = this.gameObject.transform;
            bulletEnemy4Pooler.Initialize(10);
            AllPool.Add(bulletEnemy4Pooler);
        }
        if (grenadeEnemy4Pooler == null)
        {
            go = new GameObject("grenadeEnemy4Pooler");
            grenadeEnemy4Pooler = go.AddComponent<ObjectPooler>();
            grenadeEnemy4Pooler.PooledObject = grenadeEnemy4Prefab;
            go.transform.parent = this.gameObject.transform;
            grenadeEnemy4Pooler.Initialize(5);
            AllPool.Add(grenadeEnemy4Pooler);
        }
        if (slowArenaGrenadeEnemy4Pooler == null)
        {
            go = new GameObject("slowArenaGrenadeEnemy4Pooler");
            slowArenaGrenadeEnemy4Pooler = go.AddComponent<ObjectPooler>();
            slowArenaGrenadeEnemy4Pooler.PooledObject = slowArenaGrenadeEnemy4Prefab;
            go.transform.parent = this.gameObject.transform;
            slowArenaGrenadeEnemy4Pooler.Initialize(5);
            AllPool.Add(slowArenaGrenadeEnemy4Pooler);
        }
        if (bulletEnemyV1Pooler == null)
        {
            go = new GameObject("bulletEnemyV1Pooler");
            bulletEnemyV1Pooler = go.AddComponent<ObjectPooler>();
            bulletEnemyV1Pooler.PooledObject = bulletEnemyV1Prefab;
            go.transform.parent = this.gameObject.transform;
            bulletEnemyV1Pooler.Initialize(5);
            AllPool.Add(bulletEnemyV1Pooler);
        }
        if (effectExploBulletEnemyV1Pooler == null)
        {
            go = new GameObject("effectExploBulletEnemyV1Pooler");
            effectExploBulletEnemyV1Pooler = go.AddComponent<ObjectPooler>();
            effectExploBulletEnemyV1Pooler.PooledObject = effectExploBulletEnemyV1Prefab;
            go.transform.parent = this.gameObject.transform;
            effectExploBulletEnemyV1Pooler.Initialize(5);
            AllPool.Add(effectExploBulletEnemyV1Pooler);
        }
        if (boomEnemyV3Pooler == null)
        {
            go = new GameObject("boomEnemyV3Pooler");
            boomEnemyV3Pooler = go.AddComponent<ObjectPooler>();
            boomEnemyV3Pooler.PooledObject = boomEnemyV3Prefab;
            go.transform.parent = this.gameObject.transform;
            boomEnemyV3Pooler.Initialize(5);
            AllPool.Add(boomEnemyV3Pooler);
        }
        if (effectExploBoomEnemyV3Pooler == null)
        {
            go = new GameObject("effectExploBoomEnemyV3Pooler");
            effectExploBoomEnemyV3Pooler = go.AddComponent<ObjectPooler>();
            effectExploBoomEnemyV3Pooler.PooledObject = effectExploBoomEnemyV3Prefab;
            go.transform.parent = this.gameObject.transform;
            effectExploBoomEnemyV3Pooler.Initialize(5);
            AllPool.Add(effectExploBoomEnemyV3Pooler);
        }
        if (rocketEnemyV2Pooler == null)
        {
            go = new GameObject("rocketEnemyV2Pooler");
            rocketEnemyV2Pooler = go.AddComponent<ObjectPooler>();
            rocketEnemyV2Pooler.PooledObject = rocketEnemyV2Prefab;
            go.transform.parent = this.gameObject.transform;
            rocketEnemyV2Pooler.Initialize(4);
            AllPool.Add(rocketEnemyV2Pooler);
        }
        if (rocketMiniBoss1Pooler == null)
        {
            go = new GameObject("rocketMiniBoss1Pooler");
            rocketMiniBoss1Pooler = go.AddComponent<ObjectPooler>();
            rocketMiniBoss1Pooler.PooledObject = rocketMiniBoss1Prefab;
            go.transform.parent = this.gameObject.transform;
            rocketMiniBoss1Pooler.Initialize(4);
            AllPool.Add(rocketMiniBoss1Pooler);
        }
        if (enemy5Pooler == null)
        {
            go = new GameObject("enemy5Pooler");
            enemy5Pooler = go.AddComponent<ObjectPooler>();
            enemy5Pooler.PooledObject = enemy5Prefab;
            go.transform.parent = this.gameObject.transform;
            enemy5Pooler.Initialize(7);
            AllPool.Add(enemy5Pooler);
        }
        if (bulletEnemy6Pooler == null)
        {
            go = new GameObject("bulletEnemy6Pooler");
            bulletEnemy6Pooler = go.AddComponent<ObjectPooler>();
            bulletEnemy6Pooler.PooledObject = bulletEnemy6Prefab;
            go.transform.parent = this.gameObject.transform;
            bulletEnemy6Pooler.Initialize(10);
            AllPool.Add(bulletEnemy6Pooler);
        }
        if (bulletEnemy2Pooler == null)
        {
            go = new GameObject("bulletEnemy2Pooler");
            bulletEnemy2Pooler = go.AddComponent<ObjectPooler>();
            bulletEnemy2Pooler.PooledObject = bulletEnemy2Prefab;
            go.transform.parent = this.gameObject.transform;
            bulletEnemy2Pooler.Initialize(10);
            AllPool.Add(bulletEnemy2Pooler);
        }
        if (bulletBoss1Pooler == null)
        {
            go = new GameObject("bulletBoss1Pooler");
            bulletBoss1Pooler = go.AddComponent<ObjectPooler>();
            bulletBoss1Pooler.PooledObject = bulletBoss1Prefab;
            go.transform.parent = this.gameObject.transform;
            bulletBoss1Pooler.Initialize(6);
            AllPool.Add(bulletBoss1Pooler);
        }
        if (enemyMachineExploPooler == null)
        {
            go = new GameObject("enemyExploPooler");
            enemyMachineExploPooler = go.AddComponent<ObjectPooler>();
            enemyMachineExploPooler.PooledObject = enemyMachineExploPrefab;
            go.transform.parent = this.gameObject.transform;
            enemyMachineExploPooler.Initialize(10);
            AllPool.Add(enemyMachineExploPooler);
        }
        if (boss1ExploPooler == null)
        {
            go = new GameObject("boss1ExploPooler");
            boss1ExploPooler = go.AddComponent<ObjectPooler>();
            boss1ExploPooler.PooledObject = boss1ExploPrefab;
            go.transform.parent = this.gameObject.transform;
            boss1ExploPooler.Initialize(1);
            AllPool.Add(boss1ExploPooler);
        }
        if (explofuel1Pooler == null)
        {
            go = new GameObject("explofuel1Pooler");
            explofuel1Pooler = go.AddComponent<ObjectPooler>();
            explofuel1Pooler.PooledObject = explofuel1Prefab;
            go.transform.parent = this.gameObject.transform;
            explofuel1Pooler.Initialize(10);
            AllPool.Add(explofuel1Pooler);
        }
        if (explofuel2Pooler == null)
        {
            go = new GameObject("explofuel2Pooler");
            explofuel2Pooler = go.AddComponent<ObjectPooler>();
            explofuel2Pooler.PooledObject = explofuel2Prefab;
            go.transform.parent = this.gameObject.transform;
            explofuel2Pooler.Initialize(10);
            AllPool.Add(explofuel2Pooler);
        }
        if (explowoodPooler == null)
        {
            go = new GameObject("explowoodPooler");
            explowoodPooler = go.AddComponent<ObjectPooler>();
            explowoodPooler.PooledObject = explowoodPrefab;
            go.transform.parent = this.gameObject.transform;
            explowoodPooler.Initialize(10);
            AllPool.Add(explowoodPooler);
        }
        if (explobulletenemy2Pooler == null)
        {
            go = new GameObject("explobulletenemy2Pooler");
            explobulletenemy2Pooler = go.AddComponent<ObjectPooler>();
            explobulletenemy2Pooler.PooledObject = explobulletenemy2Prefab;
            go.transform.parent = this.gameObject.transform;
            explobulletenemy2Pooler.Initialize(10);
            AllPool.Add(explobulletenemy2Pooler);
        }
        if (hitMachinePooler == null)
        {
            go = new GameObject("hitMachinePooler");
            hitMachinePooler = go.AddComponent<ObjectPooler>();
            hitMachinePooler.PooledObject = hitMachinePrefab;
            go.transform.parent = this.gameObject.transform;
            hitMachinePooler.Initialize(10);
            AllPool.Add(hitMachinePooler);
        }
        if (enemy1Pooler == null)
        {
            go = new GameObject("enemy1Pooler");
            enemy1Pooler = go.AddComponent<ObjectPooler>();
            enemy1Pooler.PooledObject = enemy1Prefab;
            go.transform.parent = this.gameObject.transform;
            enemy1Pooler.Initialize(7);
            AllPool.Add(enemy1Pooler);
        }
        if (healthItemPooler == null)
        {
            go = new GameObject("healthItemPooler");
            healthItemPooler = go.AddComponent<ObjectPooler>();
            healthItemPooler.PooledObject = healthItemPrefab;
            go.transform.parent = this.gameObject.transform;
            healthItemPooler.Initialize(10);
            AllPool.Add(healthItemPooler);
        }
        if (coinItemPooler == null)
        {
            go = new GameObject("coinItemPooler");
            coinItemPooler = go.AddComponent<ObjectPooler>();
            coinItemPooler.PooledObject = coinItemPrefab;
            go.transform.parent = this.gameObject.transform;

            coinItemPooler.Initialize(20);
            AllPool.Add(coinItemPooler);
        }
        if (exploMiniBoss1Pooler == null)
        {
            go = new GameObject("exploMiniBoss1Pooler");
            exploMiniBoss1Pooler = go.AddComponent<ObjectPooler>();
            exploMiniBoss1Pooler.PooledObject = exploMiniBoss1Prefab;
            go.transform.parent = this.gameObject.transform;
            exploMiniBoss1Pooler.Initialize(1);
            AllPool.Add(exploMiniBoss1Pooler);
        }
        if (enemyExploPooler == null)
        {
            go = new GameObject("enemyExploPooler");
            enemyExploPooler = go.AddComponent<ObjectPooler>();
            enemyExploPooler.PooledObject = enemyExploPrefab;
            go.transform.parent = this.gameObject.transform;
            enemyExploPooler.Initialize(10);
            AllPool.Add(enemyExploPooler);
        }
    }
    GameObject go;
}