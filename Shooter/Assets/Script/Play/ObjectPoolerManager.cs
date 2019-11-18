using UnityEngine;
using System.Collections;
public class ObjectPoolerManager : MonoBehaviour
{
    [HideInInspector]
    public ObjectPooler bulletPooler, grenadePooler,effectGrenadePooler;
    public GameObject bulletPrefab, grenadePrefab,effectGrenadePrefab;
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
    }
}