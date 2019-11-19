using UnityEngine;
using System.Collections;
public class ObjectPoolerManager : MonoBehaviour
{
    [HideInInspector]
    public ObjectPooler bulletPooler, grenadePooler, effectGrenadePooler, effectE0ExploPooler,bulletEnemy3Pooler;
    public GameObject bulletPrefab, grenadePrefab, effectGrenadePrefab, effectE1ExploPrefab,bulletEnemy3Prefab;
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
    }
}