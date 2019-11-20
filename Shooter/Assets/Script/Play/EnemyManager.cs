using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public List<Enemy0Controller> enemy0s;
    public List<Enemy1Controller> enemy1s;
    public List<Enemy3Controller> enemy3s;
    public List<Enemy4Controller> enemy4s;
    public List<Enemy5Controller> enemy5s;
    public List<EnemyV1Controller> enemyv1s;
    public List<EnemyV2Controller> enemyv2s;
    public List<EnemyV3Controller> enemyv3s;

    public void Awake()
    {
        instance = this;
    }
    void CallE0Action(float deltaTime)
    {
        if (enemy0s.Count == 0)
            return;
        for (int i = 0; i < enemy0s.Count; i++)
        {
            enemy0s[i].OnUpdate(deltaTime);
        }
    }
    void CallE1Action(float deltaTime)
    {
        if (enemy1s.Count == 0)
            return;
        for (int i = 0; i < enemy1s.Count; i++)
        {
            enemy1s[i].OnUpdate(deltaTime);
        }
    }
    void CallE3Action(float deltaTime)
    {
        if (enemy3s.Count == 0)
            return;
        for (int i = 0; i < enemy3s.Count; i++)
        {
            enemy3s[i].OnUpdate(deltaTime);
        }
    }
    void CallE4Action(float deltaTime)
    {
        if (enemy4s.Count == 0)
            return;
        for (int i = 0; i < enemy4s.Count; i++)
        {
            enemy4s[i].OnUpdate(deltaTime);
        }
    }
    void CallE5Action(float deltaTime)
    {
        if (enemy5s.Count == 0)
            return;
        for (int i = 0; i < enemy5s.Count; i++)
        {
            enemy5s[i].OnUpdate(deltaTime);
        }
    }
    void CallEV1Action(float deltaTime)
    {
        if (enemyv1s.Count == 0)
            return;
        for (int i = 0; i < enemyv1s.Count; i++)
        {
            enemyv1s[i].OnUpdate(deltaTime);
        }
    }
    void CallEV2Action(float deltaTime)
    {
        if (enemyv2s.Count == 0)
            return;
        for (int i = 0; i < enemyv2s.Count; i++)
        {
            enemyv2s[i].OnUpdate(deltaTime);
        }
    }
    void CallEV3Action(float deltaTime)
    {
        if (enemyv3s.Count == 0)
            return;
        for (int i = 0; i < enemyv3s.Count; i++)
        {
            enemyv3s[i].OnUpdate(deltaTime);
        }
    }
    // Update is called once per frame
    public void OnUpdate()
    {
        var deltaTime = Time.deltaTime;
        CallE0Action(deltaTime);
        CallE1Action(deltaTime);
        CallE3Action(deltaTime);
        CallE4Action(deltaTime);
        CallE5Action(deltaTime);
        CallEV1Action(deltaTime);
        CallEV2Action(deltaTime);
        CallEV3Action(deltaTime);

    }
}
