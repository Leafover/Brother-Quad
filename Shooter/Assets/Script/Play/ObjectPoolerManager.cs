using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPoolerManager : MonoBehaviour
{
    [HideInInspector]
    public ObjectPooler bulletW1Pooler, bulletW2Pooler, bulletW3Pooler, bulletW4Pooler, bulletW5Pooler, bulletW6Pooler, grenadePooler, effectGrenadePooler, slowArenaGrenadeEnemy4Pooler, effectExploBulletEnemyV1Pooler, enemyMachineExploPooler, enemyExploPooler, boss1ExploPooler, explofuel1Pooler, explofuel2Pooler, explowoodPooler, explobulletenemy2Pooler, hitMachinePooler, exploMiniBoss1Pooler, effectbosswhendiePooler, hitshieldEffectPooler, explopoisionPooler, poisionArenaPooler, exploGunBoss2Pooler, effectSmokeBoss2Pooler, exploBoss2DiePooler, exploBeforeBoss2DiePooler, targetboomPooler/*, exploBoss3Pooler*/,exploBulletW5Pooler,bulletUAVPooler,exploPlanePooler,boomPlanePooler;
    public GameObject bulletW1Prefab, bulletW2Prefab, bulletW3Prefab, bulletW4Prefab, bulletW5Prefab, bulletW6Prefab, grenadePrefab, effectGrenadePrefab, slowArenaGrenadeEnemy4Prefab, effectExploBulletEnemyV1Prefab, enemyMachineExploPrefab, enemyExploPrefab, boss1ExploPrefab, explofuel1Prefab, explofuel2Prefab, explowoodPrefab, explobulletenemy2Prefab, hitMachinePrefab, exploMiniBoss1Prefab, effectbosswhendiePrefab, hitshieldEffectPrefab, explopoisionPrefab, poisionArenaPrefab, exploGunBoss2Prefab, effectSmokeBoss2Prefab, exploBoss2DiePrefab, exploBeforeBoss2DiePrefab, targetboomPrefab/*, exploBoss3Prefab*/, exploBulletW5Prefab, bulletUAVPrefab, exploPlanePrefab, boomPlanePrefab;
    [HideInInspector]
    public static ObjectPoolerManager Instance { get; private set; }
    public List<ObjectPooler> AllPool = new List<ObjectPooler>();
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Init();
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
                //if (!AllPool[i].transform.GetChild(j).gameObject.activeSelf)
                //    continue;

                AllPool[i].transform.GetChild(j).gameObject.SetActive(false);
            }
        }
    }
    public void Init()
    {
        if (bulletW1Pooler == null)
        {
            go = new GameObject("bulletPooler");
            bulletW1Pooler = go.AddComponent<ObjectPooler>();
            bulletW1Pooler.PooledObject = bulletW1Prefab;
            go.transform.parent = this.gameObject.transform;
            bulletW1Pooler.Initialize(10);
            AllPool.Add(bulletW1Pooler);
        }
        if (bulletW2Pooler == null)
        {
            go = new GameObject("bulletW2Pooler");
            bulletW2Pooler = go.AddComponent<ObjectPooler>();
            bulletW2Pooler.PooledObject = bulletW2Prefab;
            go.transform.parent = this.gameObject.transform;
            bulletW2Pooler.Initialize(10);
            AllPool.Add(bulletW2Pooler);
        }
        if (bulletW3Pooler == null)
        {
            go = new GameObject("bulletW3Pooler");
            bulletW3Pooler = go.AddComponent<ObjectPooler>();
            bulletW3Pooler.PooledObject = bulletW3Prefab;
            go.transform.parent = this.gameObject.transform;
            bulletW3Pooler.Initialize(10);
            AllPool.Add(bulletW3Pooler);
        }
        if (bulletW4Pooler == null)
        {
            go = new GameObject("bulletW4Pooler");
            bulletW4Pooler = go.AddComponent<ObjectPooler>();
            bulletW4Pooler.PooledObject = bulletW4Prefab;
            go.transform.parent = this.gameObject.transform;
            bulletW4Pooler.Initialize(10);
            AllPool.Add(bulletW4Pooler);
        }
        if (bulletW5Pooler == null)
        {
            go = new GameObject("bulletW5Pooler");
            bulletW5Pooler = go.AddComponent<ObjectPooler>();
            bulletW5Pooler.PooledObject = bulletW5Prefab;
            go.transform.parent = this.gameObject.transform;
            bulletW5Pooler.Initialize(10);
            AllPool.Add(bulletW5Pooler);
        }
        if (bulletW6Pooler == null)
        {
            go = new GameObject("bulletW6Pooler");
            bulletW6Pooler = go.AddComponent<ObjectPooler>();
            bulletW6Pooler.PooledObject = bulletW6Prefab;
            go.transform.parent = this.gameObject.transform;
            bulletW6Pooler.Initialize(10);
            AllPool.Add(bulletW6Pooler);
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
        if (exploBoss2DiePooler == null)
        {
            go = new GameObject("exploBoss2DiePooler");
            exploBoss2DiePooler = go.AddComponent<ObjectPooler>();
            exploBoss2DiePooler.PooledObject = exploBoss2DiePrefab;
            go.transform.parent = this.gameObject.transform;
            exploBoss2DiePooler.Initialize(1);
            AllPool.Add(exploBoss2DiePooler);
        }
        if (exploBeforeBoss2DiePooler == null)
        {
            go = new GameObject("exploBeforeBoss2DiePooler");
            exploBeforeBoss2DiePooler = go.AddComponent<ObjectPooler>();
            exploBeforeBoss2DiePooler.PooledObject = exploBeforeBoss2DiePrefab;
            go.transform.parent = this.gameObject.transform;
            exploBeforeBoss2DiePooler.Initialize(6);
            AllPool.Add(exploBeforeBoss2DiePooler);
        }
        if (targetboomPooler == null)
        {
            go = new GameObject("targetboomPooler");
            targetboomPooler = go.AddComponent<ObjectPooler>();
            targetboomPooler.PooledObject = targetboomPrefab;
            go.transform.parent = this.gameObject.transform;
            targetboomPooler.Initialize(4);
            AllPool.Add(targetboomPooler);
        }
        if (exploBulletW5Pooler == null)
        {
            go = new GameObject("exploBulletW5Pooler");
            exploBulletW5Pooler = go.AddComponent<ObjectPooler>();
            exploBulletW5Pooler.PooledObject = exploBulletW5Prefab;
            go.transform.parent = this.gameObject.transform;
            exploBulletW5Pooler.Initialize(6);
            AllPool.Add(exploBulletW5Pooler);
        }
        if (bulletUAVPooler == null)
        {
            go = new GameObject("bulletUAVPooler");
            bulletUAVPooler = go.AddComponent<ObjectPooler>();
            bulletUAVPooler.PooledObject = bulletUAVPrefab;
            go.transform.parent = this.gameObject.transform;
            bulletUAVPooler.Initialize(10);
            AllPool.Add(bulletUAVPooler);
        }
        if (exploPlanePooler == null)
        {
            go = new GameObject("exploPlanePooler");
            exploPlanePooler = go.AddComponent<ObjectPooler>();
            exploPlanePooler.PooledObject = exploPlanePrefab;
            go.transform.parent = this.gameObject.transform;
            exploPlanePooler.Initialize(10);
            AllPool.Add(exploPlanePooler);
        }
        if (boomPlanePooler == null)
        {
            go = new GameObject("boomPlanePooler");
            boomPlanePooler = go.AddComponent<ObjectPooler>();
            boomPlanePooler.PooledObject = boomPlanePrefab;
            go.transform.parent = this.gameObject.transform;
            boomPlanePooler.Initialize(10);
            AllPool.Add(boomPlanePooler);
        }
        //if (exploBoss3Pooler == null)
        //{
        //    go = new GameObject("exploBoss3Pooler");
        //    exploBoss3Pooler = go.AddComponent<ObjectPooler>();
        //    exploBoss3Pooler.PooledObject = exploBoss3Prefab;
        //    go.transform.parent = this.gameObject.transform;
        //    exploBoss3Pooler.Initialize(1);
        //    AllPool.Add(exploBoss3Pooler);
        //}
    }
    GameObject go;
    //private void OnApplicationQuit()
    //{
    //  //  DataController.instance.SaveData();
    //    Debug.LogError("quit");
    //}
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            DataController.instance.SaveData();
            Debug.LogError("application focus");
        }
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            DataController.instance.SaveData();
            Debug.LogError("application pause");
        }
    }
}