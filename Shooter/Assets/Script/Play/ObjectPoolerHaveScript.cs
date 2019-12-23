using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolerHaveScript : MonoBehaviour
{
    public Transform Parent;
    public NumberDamageTextController PooledObject;
    private List<NumberDamageTextController> PooledObjects;

    public int PoolLength;

    void Awake()
    {
        PoolLength = 10;
    }


    public void Initialize(int length)
    {
        PooledObjects = new List<NumberDamageTextController>();
        for (int i = 0; i < length; i++)
        {
            CreateObjectInPool();
        }
    }

    public void DisableAllObject()
    {
        foreach (NumberDamageTextController go in PooledObjects)
        {
            if (go.gameObject.activeInHierarchy) go.gameObject.SetActive(false);
        }
    }

    public NumberDamageTextController GetPooledObject()
    {
        for (int i = 0; i < PooledObjects.Count; i++)
        {
            if (!PooledObjects[i].gameObject.activeInHierarchy)
            {
                return PooledObjects[i];
            }
        }
        int indexToReturn = PooledObjects.Count;
        //create more
        CreateObjectInPool();
        //will return the first one that we created
        return PooledObjects[indexToReturn];
    }

    public bool CheckPoolerObjectActive()
    {
        foreach (NumberDamageTextController go in PooledObjects)
        {
            if (go.gameObject.activeInHierarchy) return true;
        }
        return false;
    }

    private void CreateObjectInPool()
    {
        NumberDamageTextController go;
        if (PooledObject == null)
            go = new NumberDamageTextController();
        else
        {
            go = Instantiate(PooledObject) as NumberDamageTextController;
        }

        go.gameObject.SetActive(false);
        PooledObjects.Add(go);
        if (Parent != null)
            go.transform.parent = this.Parent;
        else
            go.transform.parent = transform;
    }

}
