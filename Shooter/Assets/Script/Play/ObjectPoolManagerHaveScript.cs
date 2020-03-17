using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManagerHaveScript : MonoBehaviour
{
    [HideInInspector]
    public ObjectPoolerHaveScript numberDamgageTextPooler, bullet3EnemyBasepooler, grenade4EnemyBasepooler, bulletEnemyV1Pooler, boomEnemyV3Pooler, rocketEnemyV2Pooler, rocketMiniBoss1Pooler, bulletBoss1Pooler, bulletEnemy2Pooler, bulletEnemy6Pooler, enemy1Pooler, enemy5Pooler, bulletEnemyEN0Pooler, enemyN1Pooler, enemyN2Pooler, grenadeN3Pooler, bulletMiniBoss2Pooler, superBulletMiniBoss2Pooler, bulletMachinegunBoss2Pooler, grenadeenemyBoss2Pooler, rocketBoss2Pooler, bulletenergyBoss2Pooler, gunItemPooler, itemHealthPooler, itemCoinPooler, enemyN0Pooler, rocketMiniBoss3Pooler, energyMNB3BasePooler, quacauluaBoss3Pooler, tialuaBoss3Pooler, rocketBoss3Pooler,
        enemyM2Pooler, chainlightningPooler;
    public NumberDamageTextController numberDamgageTextPrefab;
    public BulletEnemy bulletEnemy3BasePrefab, grenade4EnemyBasePrefab, bulletEnemyV1Prefab, boomEnemyV3Prefab, rocketEnemyV2Prefab, rocketMiniBoss1Prefab, bulletBoss1Prefab, bulletEnemy2Prefab, bulletEnemy6Prefab, bulletEnemyEN0Prefab, grenadeN3Prefab, bulletMiniBoss2Prefab, superBulletMiniBoss2Prefab, bulletMachinegunBoss2Prefab, grenadeenemyBoss2Prefab, rocketBoss2Prefab, bulletenergyBoss2Prefab, rocketMiniBoss3Prefab, energyMNB3BasePrefab, quacauluaBoss3Prefab, tialuaBoss3Prefab, rocketBoss3Prefab;
    public EnemyBase enemy1Prefab, enemy5Prefab, enemyN1Prefab, enemyN2Prefab, enemyN0Prefab, enemyM2Prefab;
    public ItemBase gunItemPrefab, itemHealthPrefab, itemCoinPrefab;
    public ChainLightning chainlightningPrefab;
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
        if (bulletEnemyEN0Pooler == null)
        {
            go = new GameObject("bulletEnemyEN0Pooler");
            bulletEnemyEN0Pooler = go.AddComponent<ObjectPoolerHaveScript>();
            bulletEnemyEN0Pooler.bulletEnemyPooledObject = bulletEnemyEN0Prefab;
            go.transform.parent = this.gameObject.transform;
            bulletEnemyEN0Pooler.InitializeBulletEnemy(10);
            AllPool.Add(bulletEnemyEN0Pooler);
        }
        if (enemyN1Pooler == null)
        {
            go = new GameObject("enemyN1Pooler");
            enemyN1Pooler = go.AddComponent<ObjectPoolerHaveScript>();
            enemyN1Pooler.enemyPooledObject = enemyN1Prefab;
            go.transform.parent = this.gameObject.transform;
            enemyN1Pooler.InitializeEnemy(6);
            AllPool.Add(enemyN1Pooler);
        }
        if (enemyN2Pooler == null)
        {
            go = new GameObject("enemyN2Pooler");
            enemyN2Pooler = go.AddComponent<ObjectPoolerHaveScript>();
            enemyN2Pooler.enemyPooledObject = enemyN2Prefab;
            go.transform.parent = this.gameObject.transform;
            enemyN2Pooler.InitializeEnemy(6);
            AllPool.Add(enemyN2Pooler);
        }
        if (enemyM2Pooler == null)
        {
            go = new GameObject("enemyM2Pooler");
            enemyM2Pooler = go.AddComponent<ObjectPoolerHaveScript>();
            enemyM2Pooler.enemyPooledObject = enemyM2Prefab;
            go.transform.parent = this.gameObject.transform;
            enemyM2Pooler.InitializeEnemy(6);
            AllPool.Add(enemyM2Pooler);
        }
        if (enemyN0Pooler == null)
        {
            go = new GameObject("enemyN0Pooler");
            enemyN0Pooler = go.AddComponent<ObjectPoolerHaveScript>();
            enemyN0Pooler.enemyPooledObject = enemyN0Prefab;
            go.transform.parent = this.gameObject.transform;
            enemyN0Pooler.InitializeEnemy(10);
            AllPool.Add(enemyN0Pooler);
        }

        if (grenadeN3Pooler == null)
        {
            go = new GameObject("grenadeN3Pooler");
            grenadeN3Pooler = go.AddComponent<ObjectPoolerHaveScript>();
            grenadeN3Pooler.bulletEnemyPooledObject = grenadeN3Prefab;
            go.transform.parent = this.gameObject.transform;
            grenadeN3Pooler.InitializeBulletEnemy(6);
            AllPool.Add(grenadeN3Pooler);
        }

        if (bulletMiniBoss2Pooler == null)
        {
            go = new GameObject("bulletMiniBoss2Pooler");
            bulletMiniBoss2Pooler = go.AddComponent<ObjectPoolerHaveScript>();
            bulletMiniBoss2Pooler.bulletEnemyPooledObject = bulletMiniBoss2Prefab;
            go.transform.parent = this.gameObject.transform;
            bulletMiniBoss2Pooler.InitializeBulletEnemy(8);
            AllPool.Add(bulletMiniBoss2Pooler);
        }
        if (superBulletMiniBoss2Pooler == null)
        {
            go = new GameObject("superBulletMiniBoss2Pooler");
            superBulletMiniBoss2Pooler = go.AddComponent<ObjectPoolerHaveScript>();
            superBulletMiniBoss2Pooler.bulletEnemyPooledObject = superBulletMiniBoss2Prefab;
            go.transform.parent = this.gameObject.transform;
            superBulletMiniBoss2Pooler.InitializeBulletEnemy(6);
            AllPool.Add(superBulletMiniBoss2Pooler);
        }
        if (bulletMachinegunBoss2Pooler == null)
        {
            go = new GameObject("bulletMachinegunBoss2Pooler");
            bulletMachinegunBoss2Pooler = go.AddComponent<ObjectPoolerHaveScript>();
            bulletMachinegunBoss2Pooler.bulletEnemyPooledObject = bulletMachinegunBoss2Prefab;
            go.transform.parent = this.gameObject.transform;
            bulletMachinegunBoss2Pooler.InitializeBulletEnemy(6);
            AllPool.Add(bulletMachinegunBoss2Pooler);
        }
        if (rocketBoss2Pooler == null)
        {
            go = new GameObject("rocketBoss2Pooler");
            rocketBoss2Pooler = go.AddComponent<ObjectPoolerHaveScript>();
            rocketBoss2Pooler.bulletEnemyPooledObject = rocketBoss2Prefab;
            go.transform.parent = this.gameObject.transform;
            rocketBoss2Pooler.InitializeBulletEnemy(6);
            AllPool.Add(rocketBoss2Pooler);
        }
        if (grenadeenemyBoss2Pooler == null)
        {
            go = new GameObject("grenadeenemyBoss2Pooler");
            grenadeenemyBoss2Pooler = go.AddComponent<ObjectPoolerHaveScript>();
            grenadeenemyBoss2Pooler.bulletEnemyPooledObject = grenadeenemyBoss2Prefab;
            go.transform.parent = this.gameObject.transform;
            grenadeenemyBoss2Pooler.InitializeBulletEnemy(3);
            AllPool.Add(grenadeenemyBoss2Pooler);
        }
        if (bulletenergyBoss2Pooler == null)
        {
            go = new GameObject("bulletenergyBoss2Pooler");
            bulletenergyBoss2Pooler = go.AddComponent<ObjectPoolerHaveScript>();
            bulletenergyBoss2Pooler.bulletEnemyPooledObject = bulletenergyBoss2Prefab;
            go.transform.parent = this.gameObject.transform;
            bulletenergyBoss2Pooler.InitializeBulletEnemy(6);
            AllPool.Add(bulletenergyBoss2Pooler);
        }
        if (gunItemPooler == null)
        {
            go = new GameObject("gunItemPooler");
            gunItemPooler = go.AddComponent<ObjectPoolerHaveScript>();
            gunItemPooler.itemPooledObject = gunItemPrefab;
            go.transform.parent = this.gameObject.transform;
            gunItemPooler.InitializeItem(6);
            AllPool.Add(gunItemPooler);
        }
        if (itemHealthPooler == null)
        {
            go = new GameObject("itemHealthPooler");
            itemHealthPooler = go.AddComponent<ObjectPoolerHaveScript>();
            itemHealthPooler.itemPooledObject = itemHealthPrefab;
            go.transform.parent = this.gameObject.transform;
            itemHealthPooler.InitializeItem(6);
            AllPool.Add(itemHealthPooler);
        }
        if (itemCoinPooler == null)
        {
            go = new GameObject("itemCoinPooler");
            itemCoinPooler = go.AddComponent<ObjectPoolerHaveScript>();
            itemCoinPooler.itemPooledObject = itemCoinPrefab;
            go.transform.parent = this.gameObject.transform;
            itemCoinPooler.InitializeItem(6);
            AllPool.Add(itemCoinPooler);
        }
        if (rocketMiniBoss3Pooler == null)
        {
            go = new GameObject("rocketMiniBoss3Pooler");
            rocketMiniBoss3Pooler = go.AddComponent<ObjectPoolerHaveScript>();
            rocketMiniBoss3Pooler.bulletEnemyPooledObject = rocketMiniBoss3Prefab;
            go.transform.parent = this.gameObject.transform;
            rocketMiniBoss3Pooler.InitializeBulletEnemy(6);
            AllPool.Add(rocketMiniBoss3Pooler);
        }
        if (energyMNB3BasePooler == null)
        {
            go = new GameObject("energyMNB3BasePooler");
            energyMNB3BasePooler = go.AddComponent<ObjectPoolerHaveScript>();
            energyMNB3BasePooler.bulletEnemyPooledObject = energyMNB3BasePrefab;
            go.transform.parent = this.gameObject.transform;
            energyMNB3BasePooler.InitializeBulletEnemy(2);
            AllPool.Add(energyMNB3BasePooler);
        }
        if (quacauluaBoss3Pooler == null)
        {
            go = new GameObject("quacauluaBoss3Pooler");
            quacauluaBoss3Pooler = go.AddComponent<ObjectPoolerHaveScript>();
            quacauluaBoss3Pooler.bulletEnemyPooledObject = quacauluaBoss3Prefab;
            go.transform.parent = this.gameObject.transform;
            quacauluaBoss3Pooler.InitializeBulletEnemy(4);
            AllPool.Add(quacauluaBoss3Pooler);
        }
        if (tialuaBoss3Pooler == null)
        {
            go = new GameObject("tialuaBoss3Pooler");
            tialuaBoss3Pooler = go.AddComponent<ObjectPoolerHaveScript>();
            tialuaBoss3Pooler.bulletEnemyPooledObject = tialuaBoss3Prefab;
            go.transform.parent = this.gameObject.transform;
            tialuaBoss3Pooler.InitializeBulletEnemy(7);
            AllPool.Add(tialuaBoss3Pooler);
        }
        if (rocketBoss3Pooler == null)
        {
            go = new GameObject("rocketBoss3Pooler");
            rocketBoss3Pooler = go.AddComponent<ObjectPoolerHaveScript>();
            rocketBoss3Pooler.bulletEnemyPooledObject = rocketBoss3Prefab;
            go.transform.parent = this.gameObject.transform;
            rocketBoss3Pooler.InitializeBulletEnemy(10);
            AllPool.Add(rocketBoss3Pooler);
        }
        if(chainlightningPooler == null)
        {
            go = new GameObject("chainlightningPooler");
            chainlightningPooler = go.AddComponent<ObjectPoolerHaveScript>();
            chainlightningPooler.chainLightningPooledObject = chainlightningPrefab;
            go.transform.parent = this.gameObject.transform;
            chainlightningPooler.InitializeChainLightning(5);
            AllPool.Add(chainlightningPooler);
        }
    }
    GameObject go;
}
