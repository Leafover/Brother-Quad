using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPoolerManager : MonoBehaviour
{
    [HideInInspector]
    public ObjectPooler bulletPooler, grenadePooler, effectGrenadePooler, slowArenaGrenadeEnemy4Pooler, effectExploBulletEnemyV1Pooler, effectExploBoomEnemyV3Pooler, enemyMachineExploPooler, enemyExploPooler, boss1ExploPooler, explofuel1Pooler, explofuel2Pooler, explowoodPooler, explobulletenemy2Pooler, hitMachinePooler, healthItemPooler, coinItemPooler,exploMiniBoss1Pooler,explogrenadeN3Pooler,effectbosswhendiePooler,hitshieldEffectPooler,explopoisionPooler,poisionArenaPooler,exploGunBoss2Pooler,effectSmokeBoss2Pooler;
    public GameObject bulletPrefab, grenadePrefab, effectGrenadePrefab, slowArenaGrenadeEnemy4Prefab, effectExploBulletEnemyV1Prefab, effectExploBoomEnemyV3Prefab, enemyMachineExploPrefab, enemyExploPrefab, boss1ExploPrefab, explofuel1Prefab, explofuel2Prefab, explowoodPrefab, explobulletenemy2Prefab, hitMachinePrefab, healthItemPrefab, coinItemPrefab, exploMiniBoss1Prefab, explogrenadeN3Prefab, effectbosswhendiePrefab, hitshieldEffectPrefab, explopoisionPrefab, poisionArenaPrefab, exploGunBoss2Prefab, effectSmokeBoss2Prefab;
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
        if (slowArenaGrenadeEnemy4Pooler == null)
        {
            go = new GameObject("slowArenaGrenadeEnemy4Pooler");
            slowArenaGrenadeEnemy4Pooler = go.AddComponent<ObjectPooler>();
            slowArenaGrenadeEnemy4Pooler.PooledObject = slowArenaGrenadeEnemy4Prefab;
            go.transform.parent = this.gameObject.transform;
            slowArenaGrenadeEnemy4Pooler.Initialize(5);
            AllPool.Add(slowArenaGrenadeEnemy4Pooler);
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
        if (effectExploBoomEnemyV3Pooler == null)
        {
            go = new GameObject("effectExploBoomEnemyV3Pooler");
            effectExploBoomEnemyV3Pooler = go.AddComponent<ObjectPooler>();
            effectExploBoomEnemyV3Pooler.PooledObject = effectExploBoomEnemyV3Prefab;
            go.transform.parent = this.gameObject.transform;
            effectExploBoomEnemyV3Pooler.Initialize(5);
            AllPool.Add(effectExploBoomEnemyV3Pooler);
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
        if (explogrenadeN3Pooler == null)
        {
            go = new GameObject("explogrenadeN3Pooler");
            explogrenadeN3Pooler = go.AddComponent<ObjectPooler>();
            explogrenadeN3Pooler.PooledObject = explogrenadeN3Prefab;
            go.transform.parent = this.gameObject.transform;
            explogrenadeN3Pooler.Initialize(5);
            AllPool.Add(explogrenadeN3Pooler);
        }

        if (effectbosswhendiePooler == null)
        {
            go = new GameObject("effectbosswhendiePooler");
            effectbosswhendiePooler = go.AddComponent<ObjectPooler>();
            effectbosswhendiePooler.PooledObject = effectbosswhendiePrefab;
            go.transform.parent = this.gameObject.transform;
            effectbosswhendiePooler.Initialize(5);
            AllPool.Add(effectbosswhendiePooler);
        }
        if (hitshieldEffectPooler == null)
        {
            go = new GameObject("hitshieldEffectPooler");
            hitshieldEffectPooler = go.AddComponent<ObjectPooler>();
            hitshieldEffectPooler.PooledObject = hitshieldEffectPrefab;
            go.transform.parent = this.gameObject.transform;
            hitshieldEffectPooler.Initialize(10);
            AllPool.Add(hitshieldEffectPooler);
        }
        if (explopoisionPooler == null)
        {
            go = new GameObject("explopoisionPooler");
            explopoisionPooler = go.AddComponent<ObjectPooler>();
            explopoisionPooler.PooledObject = explopoisionPrefab;
            go.transform.parent = this.gameObject.transform;
            explopoisionPooler.Initialize(5);
            AllPool.Add(explopoisionPooler);
        }
        if (poisionArenaPooler == null)
        {
            go = new GameObject("poisionArenaPooler");
            poisionArenaPooler = go.AddComponent<ObjectPooler>();
            poisionArenaPooler.PooledObject = poisionArenaPrefab;
            go.transform.parent = this.gameObject.transform;
            poisionArenaPooler.Initialize(5);
            AllPool.Add(poisionArenaPooler);
        }
        if (exploGunBoss2Pooler == null)
        {
            go = new GameObject("exploGunBoss2Pooler");
            exploGunBoss2Pooler = go.AddComponent<ObjectPooler>();
            exploGunBoss2Pooler.PooledObject = exploGunBoss2Prefab;
            go.transform.parent = this.gameObject.transform;
            exploGunBoss2Pooler.Initialize(3);
            AllPool.Add(exploGunBoss2Pooler);
        }
        if (effectSmokeBoss2Pooler == null)
        {
            go = new GameObject("effectSmokeBoss2Pooler");
            effectSmokeBoss2Pooler = go.AddComponent<ObjectPooler>();
            effectSmokeBoss2Pooler.PooledObject = effectSmokeBoss2Prefab;
            go.transform.parent = this.gameObject.transform;
            effectSmokeBoss2Pooler.Initialize(6);
            AllPool.Add(effectSmokeBoss2Pooler);
        }
    }
    GameObject go;
}