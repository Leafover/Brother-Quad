using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolerHaveScript : MonoBehaviour
{
    public Transform Parent;
    public NumberDamageTextController numberDamageTextPooledObject;
    private List<NumberDamageTextController> PooledNumberDamageTextObjects;

    public BulletEnemy bulletEnemyPooledObject;
    public List<BulletEnemy> PooledBulletEnemy;


    public EnemyBase enemyPooledObject;
    public List<EnemyBase> PooledEnemy;


    public ItemBase itemPooledObject;
    public List<ItemBase> PooledItem;


    public int PoolLength;

    void Awake()
    {
        PoolLength = 10;
    }

    #region text number damage
    public void InitializeNumberDamageText(int length)
    {
        PooledNumberDamageTextObjects = new List<NumberDamageTextController>();
        for (int i = 0; i < length; i++)
        {
            CreateNumberDamageTextObjectInPool();
        }
    }

    public NumberDamageTextController GetNumberDamageTextPooledObject()
    {
        for (int i = 0; i < PooledNumberDamageTextObjects.Count; i++)
        {
            if (!PooledNumberDamageTextObjects[i].gameObject.activeInHierarchy)
            {
                return PooledNumberDamageTextObjects[i];
            }
        }
        int indexToReturn = PooledNumberDamageTextObjects.Count;
        //create more
        CreateNumberDamageTextObjectInPool();
        //will return the first one that we created
        return PooledNumberDamageTextObjects[indexToReturn];
    }


    private void CreateNumberDamageTextObjectInPool()
    {
        NumberDamageTextController go;
        if (numberDamageTextPooledObject == null)
            go = new NumberDamageTextController();
        else
        {
            go = Instantiate(numberDamageTextPooledObject) as NumberDamageTextController;
        }

        go.gameObject.SetActive(false);
        PooledNumberDamageTextObjects.Add(go);
        if (Parent != null)
            go.transform.parent = this.Parent;
        else
            go.transform.parent = transform;
    }
    #endregion

    #region Bullet Enemy
    public void InitializeBulletEnemy(int length)
    {
        PooledBulletEnemy = new List<BulletEnemy>();
        for (int i = 0; i < length; i++)
        {
            CreateBulletEnemyObjectInPool();
        }
    }

    public BulletEnemy GetBulletEnemyPooledObject()
    {
        for (int i = 0; i < PooledBulletEnemy.Count; i++)
        {
            if (!PooledBulletEnemy[i].gameObject.activeInHierarchy)
            {
                return PooledBulletEnemy[i];
            }
        }
        int indexToReturn = PooledBulletEnemy.Count;
        //create more
        CreateBulletEnemyObjectInPool();
        //will return the first one that we created
        return PooledBulletEnemy[indexToReturn];
    }


    private void CreateBulletEnemyObjectInPool()
    {
        BulletEnemy go;
        if (bulletEnemyPooledObject == null)
            go = new BulletEnemy();
        else
        {
            go = Instantiate(bulletEnemyPooledObject) as BulletEnemy;
        }

        go.gameObject.SetActive(false);
        PooledBulletEnemy.Add(go);
        if (Parent != null)
            go.transform.parent = this.Parent;
        else
            go.transform.parent = transform;
    }
    #endregion

    #region Enemy
    public void InitializeEnemy(int length)
    {
        PooledEnemy = new List<EnemyBase>();
        for (int i = 0; i < length; i++)
        {
            CreateEnemyObjectInPool();
        }
    }
    public EnemyBase GetEnemyPooledObject()
    {
        for (int i = 0; i < PooledEnemy.Count; i++)
        {
            if (!PooledEnemy[i].gameObject.activeInHierarchy)
            {
                return PooledEnemy[i];
            }
        }
        int indexToReturn = PooledEnemy.Count;
        CreateEnemyObjectInPool();
        return PooledEnemy[indexToReturn];
    }

    private void CreateEnemyObjectInPool()
    {
        EnemyBase go;
        if (enemyPooledObject == null)
            go = new EnemyBase();
        else
        {
            go = Instantiate(enemyPooledObject) as EnemyBase;
        }

        go.gameObject.SetActive(false);
        PooledEnemy.Add(go);
        if (Parent != null)
            go.transform.parent = this.Parent;
        else
            go.transform.parent = transform;
    }
    #endregion


    #region Item
    public void InitializeItem(int length)
    {
        PooledItem = new List<ItemBase>();
        for (int i = 0; i < length; i++)
        {
            CreateItemObjectInPool();
        }
    }
    public ItemBase GetItemPooledObject()
    {
        for (int i = 0; i < PooledItem.Count; i++)
        {
            if (!PooledItem[i].gameObject.activeInHierarchy)
            {
                return PooledItem[i];
            }
        }
        int indexToReturn = PooledItem.Count;
        CreateItemObjectInPool();
        return PooledItem[indexToReturn];
    }

    private void CreateItemObjectInPool()
    {
        ItemBase go;
        if (itemPooledObject == null)
            go = new ItemBase();
        else
        {
            go = Instantiate(itemPooledObject) as ItemBase;
        }

        go.gameObject.SetActive(false);
        PooledItem.Add(go);
        if (Parent != null)
            go.transform.parent = this.Parent;
        else
            go.transform.parent = transform;
    }
    #endregion
}
