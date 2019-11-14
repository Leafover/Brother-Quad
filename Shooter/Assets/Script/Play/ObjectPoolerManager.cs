using UnityEngine;
using System.Collections;
public class ObjectPoolerManager : MonoBehaviour
{
    [HideInInspector]
    public ObjectPooler bulletPooler;
    public GameObject bulletPrefab;
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
    }
}