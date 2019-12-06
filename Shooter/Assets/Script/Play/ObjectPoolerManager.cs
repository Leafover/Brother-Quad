using UnityEngine;
using System.Collections;
public class ObjectPoolerManager : MonoBehaviour
{
    [HideInInspector]
    public ObjectPooler bulletPooler, grenadePooler, effectGrenadePooler, effectE0ExploPooler, bulletEnemy3Pooler, bulletEnemy4Pooler, grenadeEnemy4Pooler, slowArenaGrenadeEnemy4Pooler, bulletEnemyV1Pooler, effectExploBulletEnemyV1Pooler, boomEnemyV3Pooler, effectExploBoomEnemyV3Pooler, rocketEnemyV2Pooler, rocketMiniBoss1Pooler, enemy5Pooler, bulletEnemy6Pooler,bulletEnemy2Pooler,bulletBoss1Pooler,enemyExploPooler,boss1ExploPooler;
    public GameObject bulletPrefab, grenadePrefab, effectGrenadePrefab, effectE1ExploPrefab, bulletEnemy3Prefab, bulletEnemy4Prefab, grenadeEnemy4Prefab, slowArenaGrenadeEnemy4Prefab, bulletEnemyV1Prefab, effectExploBulletEnemyV1Prefab, boomEnemyV3Prefab, effectExploBoomEnemyV3Prefab, rocketEnemyV2Prefab, rocketMiniBoss1Prefab, enemy5Prefab, bulletEnemy6Prefab,bulletEnemy2Prefab, bulletBoss1Prefab, enemyExploPrefab, boss1ExploPrefab;
    [HideInInspector]
    public static ObjectPoolerManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //  DontDestroyOnLoad(gameObject);
        }
        //else
        //    DestroyImmediate(gameObject);
    }

    void Start()
    {
        if (bulletPooler == null)
        {
            GameObject go = new GameObject("bulletPooler");
            bulletPooler = go.AddComponent<ObjectPooler>();
            bulletPooler.PooledObject = bulletPrefab;
            go.transform.parent = this.gameObject.transform;
            bulletPooler.Initialize();
        }
        if (grenadePooler == null)
        {
            GameObject go = new GameObject("grenadePooler");
            grenadePooler = go.AddComponent<ObjectPooler>();
            grenadePooler.PooledObject = grenadePrefab;
            go.transform.parent = this.gameObject.transform;
            grenadePooler.Initialize();
        }
        if (effectGrenadePooler == null)
        {
            GameObject go = new GameObject("effectGrenadePooler");
            effectGrenadePooler = go.AddComponent<ObjectPooler>();
            effectGrenadePooler.PooledObject = effectGrenadePrefab;
            go.transform.parent = this.gameObject.transform;
            effectGrenadePooler.Initialize();
        }
        if (effectE0ExploPooler == null)
        {
            GameObject go = new GameObject("effectE1ExploPooler");
            effectE0ExploPooler = go.AddComponent<ObjectPooler>();
            effectE0ExploPooler.PooledObject = effectE1ExploPrefab;
            go.transform.parent = this.gameObject.transform;
            effectE0ExploPooler.Initialize();
        }
        if (bulletEnemy3Pooler == null)
        {
            GameObject go = new GameObject("bulletEnemy3Pooler");
            bulletEnemy3Pooler = go.AddComponent<ObjectPooler>();
            bulletEnemy3Pooler.PooledObject = bulletEnemy3Prefab;
            go.transform.parent = this.gameObject.transform;
            bulletEnemy3Pooler.Initialize();
        }
        if (bulletEnemy4Pooler == null)
        {
            GameObject go = new GameObject("bulletEnemy4Pooler");
            bulletEnemy4Pooler = go.AddComponent<ObjectPooler>();
            bulletEnemy4Pooler.PooledObject = bulletEnemy4Prefab;
            go.transform.parent = this.gameObject.transform;
            bulletEnemy4Pooler.Initialize();
        }
        if (grenadeEnemy4Pooler == null)
        {
            GameObject go = new GameObject("grenadeEnemy4Pooler");
            grenadeEnemy4Pooler = go.AddComponent<ObjectPooler>();
            grenadeEnemy4Pooler.PooledObject = grenadeEnemy4Prefab;
            go.transform.parent = this.gameObject.transform;
            grenadeEnemy4Pooler.Initialize();
        }
        if (slowArenaGrenadeEnemy4Pooler == null)
        {
            GameObject go = new GameObject("slowArenaGrenadeEnemy4Pooler");
            slowArenaGrenadeEnemy4Pooler = go.AddComponent<ObjectPooler>();
            slowArenaGrenadeEnemy4Pooler.PooledObject = slowArenaGrenadeEnemy4Prefab;
            go.transform.parent = this.gameObject.transform;
            slowArenaGrenadeEnemy4Pooler.Initialize();
        }
        if (bulletEnemyV1Pooler == null)
        {
            GameObject go = new GameObject("bulletEnemyV1Pooler");
            bulletEnemyV1Pooler = go.AddComponent<ObjectPooler>();
            bulletEnemyV1Pooler.PooledObject = bulletEnemyV1Prefab;
            go.transform.parent = this.gameObject.transform;
            bulletEnemyV1Pooler.Initialize();
        }
        if (effectExploBulletEnemyV1Pooler == null)
        {
            GameObject go = new GameObject("effectExploBulletEnemyV1Pooler");
            effectExploBulletEnemyV1Pooler = go.AddComponent<ObjectPooler>();
            effectExploBulletEnemyV1Pooler.PooledObject = effectExploBulletEnemyV1Prefab;
            go.transform.parent = this.gameObject.transform;
            effectExploBulletEnemyV1Pooler.Initialize();
        }
        if (boomEnemyV3Pooler == null)
        {
            GameObject go = new GameObject("boomEnemyV3Pooler");
            boomEnemyV3Pooler = go.AddComponent<ObjectPooler>();
            boomEnemyV3Pooler.PooledObject = boomEnemyV3Prefab;
            go.transform.parent = this.gameObject.transform;
            boomEnemyV3Pooler.Initialize();
        }
        if (effectExploBoomEnemyV3Pooler == null)
        {
            GameObject go = new GameObject("effectExploBoomEnemyV3Pooler");
            effectExploBoomEnemyV3Pooler = go.AddComponent<ObjectPooler>();
            effectExploBoomEnemyV3Pooler.PooledObject = effectExploBoomEnemyV3Prefab;
            go.transform.parent = this.gameObject.transform;
            effectExploBoomEnemyV3Pooler.Initialize();
        }
        if (rocketEnemyV2Pooler == null)
        {
            GameObject go = new GameObject("rocketEnemyV2Pooler");
            rocketEnemyV2Pooler = go.AddComponent<ObjectPooler>();
            rocketEnemyV2Pooler.PooledObject = rocketEnemyV2Prefab;
            go.transform.parent = this.gameObject.transform;
            rocketEnemyV2Pooler.Initialize();
        }
        if (rocketMiniBoss1Pooler == null)
        {
            GameObject go = new GameObject("rocketMiniBoss1Pooler");
            rocketMiniBoss1Pooler = go.AddComponent<ObjectPooler>();
            rocketMiniBoss1Pooler.PooledObject = rocketMiniBoss1Prefab;
            go.transform.parent = this.gameObject.transform;
            rocketMiniBoss1Pooler.Initialize();
        }
        if (enemy5Pooler == null)
        {
            GameObject go = new GameObject("enemy5Pooler");
            enemy5Pooler = go.AddComponent<ObjectPooler>();
            enemy5Pooler.PooledObject = enemy5Prefab;
            go.transform.parent = this.gameObject.transform;
            enemy5Pooler.Initialize();
        }
        if (bulletEnemy6Pooler == null)
        {
            GameObject go = new GameObject("bulletEnemy6Pooler");
            bulletEnemy6Pooler = go.AddComponent<ObjectPooler>();
            bulletEnemy6Pooler.PooledObject = bulletEnemy6Prefab;
            go.transform.parent = this.gameObject.transform;
            bulletEnemy6Pooler.Initialize();
        }
        if (bulletEnemy2Pooler == null)
        {
            GameObject go = new GameObject("bulletEnemy2Pooler");
            bulletEnemy2Pooler = go.AddComponent<ObjectPooler>();
            bulletEnemy2Pooler.PooledObject = bulletEnemy2Prefab;
            go.transform.parent = this.gameObject.transform;
            bulletEnemy2Pooler.Initialize();
        }
        if (bulletBoss1Pooler == null)
        {
            GameObject go = new GameObject("bulletBoss1Pooler");
            bulletBoss1Pooler = go.AddComponent<ObjectPooler>();
            bulletBoss1Pooler.PooledObject = bulletBoss1Prefab;
            go.transform.parent = this.gameObject.transform;
            bulletBoss1Pooler.Initialize();
        }
        if (enemyExploPooler == null)
        {
            GameObject go = new GameObject("enemyExploPooler");
            enemyExploPooler = go.AddComponent<ObjectPooler>();
            enemyExploPooler.PooledObject = enemyExploPrefab;
            go.transform.parent = this.gameObject.transform;
            enemyExploPooler.Initialize();
        }
        if (boss1ExploPooler == null)
        {
            GameObject go = new GameObject("boss1ExploPooler");
            boss1ExploPooler = go.AddComponent<ObjectPooler>();
            boss1ExploPooler.PooledObject = boss1ExploPrefab;
            go.transform.parent = this.gameObject.transform;
            boss1ExploPooler.Initialize();
        }
    }
}