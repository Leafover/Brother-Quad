using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public List<Enemy0Controller> enemy0s;
    public List<Enemy1Controller> enemy1s;
    public List<Enemy2Controller> enemy2s;
    public List<Enemy3Controller> enemy3s;
    public List<Enemy4Controller> enemy4s;
    public List<Enemy5Controller> enemy5s;
    public List<Enemy6Controller> enemy6s;
    public List<EnemyV1Controller> enemyv1s;
    public List<EnemyV2Controller> enemyv2s;
    public List<EnemyV3Controller> enemyv3s;
    public List<MiniBoss1> miniboss1s;
    public List<Boss1Controller> boss1s;
    public List<EnemyEN0Controller> enemyen0s;
    public List<EnemyN1Controller> enemyn1s;

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
            enemy0s[i].UpdateActionForEnemyManager(deltaTime);
        }
    }
    void CallE1Action(float deltaTime)
    {
        if (enemy1s.Count == 0)
            return;
        for (int i = 0; i < enemy1s.Count; i++)
        {
            enemy1s[i].UpdateActionForEnemyManager(deltaTime);
        }
    }
    void CallE2Action(float deltaTime)
    {
        if (enemy2s.Count == 0)
            return;
        for (int i = 0; i < enemy2s.Count; i++)
        {
            enemy2s[i].UpdateActionForEnemyManager(deltaTime);
        }
    }
    void CallE3Action(float deltaTime)
    {
        if (enemy3s.Count == 0)
            return;
        for (int i = 0; i < enemy3s.Count; i++)
        {
            enemy3s[i].UpdateActionForEnemyManager(deltaTime);
        }
    }
    void CallE4Action(float deltaTime)
    {
        if (enemy4s.Count == 0)
            return;
        for (int i = 0; i < enemy4s.Count; i++)
        {
            enemy4s[i].UpdateActionForEnemyManager(deltaTime);
        }
    }
    void CallE5Action(float deltaTime)
    {
        if (enemy5s.Count == 0)
            return;
        for (int i = 0; i < enemy5s.Count; i++)
        {
            enemy5s[i].UpdateActionForEnemyManager(deltaTime);
        }
    }
    void CallE6Action(float deltaTime)
    {
        if (enemy6s.Count == 0)
            return;
        for (int i = 0; i < enemy6s.Count; i++)
        {
            enemy6s[i].UpdateActionForEnemyManager(deltaTime);
        }
    }
    void CallEV1Action(float deltaTime)
    {
        if (enemyv1s.Count == 0)
            return;
        for (int i = 0; i < enemyv1s.Count; i++)
        {
            enemyv1s[i].UpdateActionForEnemyManager(deltaTime);
        }
    }
    void CallEV2Action(float deltaTime)
    {
        if (enemyv2s.Count == 0)
            return;
        for (int i = 0; i < enemyv2s.Count; i++)
        {
            enemyv2s[i].UpdateActionForEnemyManager(deltaTime);
        }
    }
    void CallEV3Action(float deltaTime)
    {
        if (enemyv3s.Count == 0)
            return;
        for (int i = 0; i < enemyv3s.Count; i++)
        {
            enemyv3s[i].UpdateActionForEnemyManager(deltaTime);
        }
    }
    void CallMiniBoss1Action(float deltaTime)
    {
        if (miniboss1s.Count == 0)
            return;
        for (int i = 0; i < miniboss1s.Count; i++)
        {
            miniboss1s[i].UpdateActionForEnemyManager(deltaTime);
        }
    }
    void CallBoss1Action(float deltaTime)
    {
        if (boss1s.Count == 0)
            return;
        for (int i = 0; i < boss1s.Count; i++)
        {
            boss1s[i].UpdateActionForEnemyManager(deltaTime);
        }
    }
    void CallEN0Action(float deltaTime)
    {
        if (enemyen0s.Count == 0)
            return;
        for (int i = 0; i < enemyen0s.Count; i++)
        {
            enemyen0s[i].UpdateActionForEnemyManager(deltaTime);
        }
    }
    void CallN1Action(float deltaTime)
    {
        if (enemyn1s.Count == 0)
            return;
        for (int i = 0; i < enemyn1s.Count; i++)
        {
            enemyn1s[i].UpdateActionForEnemyManager(deltaTime);
        }
    }
    public void OnUpdate(float deltaTime)
    {
        CallE0Action(deltaTime);
        CallE1Action(deltaTime);
        CallE2Action(deltaTime);
        CallE3Action(deltaTime);
        CallE4Action(deltaTime);
        CallE5Action(deltaTime);
        CallE6Action(deltaTime);
        CallEV1Action(deltaTime);
        CallEV2Action(deltaTime);
        CallEV3Action(deltaTime);
        CallMiniBoss1Action(deltaTime);
        CallBoss1Action(deltaTime);
        CallEN0Action(deltaTime);
        CallN1Action(deltaTime);
    }
}
