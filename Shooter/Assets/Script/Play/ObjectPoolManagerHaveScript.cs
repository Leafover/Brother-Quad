using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManagerHaveScript : MonoBehaviour
{
    [HideInInspector]
    public ObjectPoolerHaveScript numberDamgageTextPooler;
    public NumberDamageTextController numberDamgageTextPrefab;
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
            numberDamgageTextPooler.PooledObject = numberDamgageTextPrefab;
            go.transform.parent = this.gameObject.transform;
            numberDamgageTextPooler.Initialize(20);
            AllPool.Add(numberDamgageTextPooler);
        }
    }
    GameObject go;
}
