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
    public List<EnemyV3Controller> enemyv3s;

    public void Awake()
    {
        instance = this;
    }
    void CallE0Action()
    {
        if (enemy0s.Count == 0)
            return;
        for (int i = 0; i < enemy0s.Count; i++)
        {
            enemy0s[i].OnUpdate();
        }
    }
    void CallE1Action()
    {
        if (enemy1s.Count == 0)
            return;
        for (int i = 0; i < enemy1s.Count; i++)
        {
            enemy1s[i].OnUpdate();
        }
    }
    void CallE3Action()
    {
        if (enemy3s.Count == 0)
            return;
        for (int i = 0; i < enemy3s.Count; i++)
        {
            enemy3s[i].OnUpdate();
        }
    }
    void CallE4Action()
    {
        if (enemy4s.Count == 0)
            return;
        for (int i = 0; i < enemy4s.Count; i++)
        {
            enemy4s[i].OnUpdate();
        }
    }
    void CallE5Action()
    {
        if (enemy5s.Count == 0)
            return;
        for (int i = 0; i < enemy5s.Count; i++)
        {
            enemy5s[i].OnUpdate();
        }
    }
    void CallEV1Action()
    {
        if (enemyv1s.Count == 0)
            return;
        for (int i = 0; i < enemyv1s.Count; i++)
        {
            enemyv1s[i].OnUpdate();
        }
    }
    void CallEV3Action()
    {
        if (enemyv3s.Count == 0)
            return;
        for (int i = 0; i < enemyv3s.Count; i++)
        {
            enemyv3s[i].OnUpdate();
        }
    }
    // Update is called once per frame
    public void OnUpdate()
    {
        CallE0Action();
        CallE1Action();
        CallE3Action();
        CallE4Action();
        CallE5Action();
        CallEV1Action();
        CallEV3Action();

    }
}
