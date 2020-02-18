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
    public List<EnemyN2Controller> enemyn2s;
    public List<EnemyN3Controller> enemyn3s;
    public List<EnemyN4Controller> enemyn4s;
    public List<EnemyVN2Controller> enemyvn2s;
    public List<MiniBoss2> miniboss2s;
    public List<Boss2Controller> boss2s;
    public List<EM1Controller> em1s;
    public List<EM2Controller> em2s;
    public List<EM3Controller> em3s;
    public List<EM4Controller> em4s;
    public List<EM5Controller> em5s;
    public List<EM6Controller> em6s;
    public List<MiniBoss3Controller> miniboss3s;
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
    void CallN2Action(float deltaTime)
    {
        if (enemyn2s.Count == 0)
            return;
        for (int i = 0; i < enemyn2s.Count; i++)
        {
            enemyn2s[i].UpdateActionForEnemyManager(deltaTime);
        }
    }
    void CallN3Action(float deltaTime)
    {
        if (enemyn3s.Count == 0)
            return;
        for (int i = 0; i < enemyn3s.Count; i++)
        {
            enemyn3s[i].UpdateActionForEnemyManager(deltaTime);
        }
    }

    void CallN4Action(float deltaTime)
    {
        if (enemyn4s.Count == 0)
            return;
        for (int i = 0; i < enemyn4s.Count; i++)
        {
            enemyn4s[i].UpdateActionForEnemyManager(deltaTime);
        }
    }
    void CallVN2Action(float deltaTime)
    {
        if (enemyvn2s.Count == 0)
            return;
        for (int i = 0; i < enemyvn2s.Count; i++)
        {
            enemyvn2s[i].UpdateActionForEnemyManager(deltaTime);
        }
    }
    void CallMiniBoss2Action(float deltaTime)
    {
        if (miniboss2s.Count == 0)
            return;
        for (int i = 0; i < miniboss2s.Count; i++)
        {
            miniboss2s[i].UpdateActionForEnemyManager(deltaTime);
        }
    }
    void CallBoss2Action(float deltaTime)
    {
        if (boss2s.Count == 0)
            return;
        for (int i = 0; i < boss2s.Count; i++)
        {
            boss2s[i].UpdateActionForEnemyManager(deltaTime);
        }
    }
    void CallEM1Action(float deltaTime)
    {
        if (em1s.Count == 0)
            return;
        for(int i = 0; i < em1s.Count; i ++)
        {
            em1s[i].UpdateActionForEnemyManager(deltaTime);
        }
    }
    void CallEM2Action(float deltaTime)
    {
        if (em2s.Count == 0)
            return;
        for (int i = 0; i < em2s.Count; i++)
        {
            em2s[i].UpdateActionForEnemyManager(deltaTime);
        }
    }
    void CallEM3Action(float deltaTime)
    {
        if (em3s.Count == 0)
            return;
        for (int i = 0; i < em3s.Count; i++)
        {
            em3s[i].UpdateActionForEnemyManager(deltaTime);
        }
    }
    void CallEM4Action(float deltaTime)
    {
        if (em4s.Count == 0)
            return;
        for (int i = 0; i < em4s.Count; i++)
        {
            em4s[i].UpdateActionForEnemyManager(deltaTime);
        }
    }
    void CallEM5Action(float deltaTime)
    {
        if (em5s.Count == 0)
            return;
        for (int i = 0; i < em5s.Count; i++)
        {
            em5s[i].UpdateActionForEnemyManager(deltaTime);
        }
    }
    void CallEM6Action(float deltaTime)
    {
        if (em6s.Count == 0)
            return;
        for (int i = 0; i < em6s.Count; i++)
        {
            em6s[i].UpdateActionForEnemyManager(deltaTime);
        }
    }
    void CallMNB3Action(float deltaTime)
    {
        if (miniboss3s.Count == 0)
            return;
        for (int i = 0; i < miniboss3s.Count; i++)
        {
            miniboss3s[i].UpdateActionForEnemyManager(deltaTime);
        }
    }

    public void OnUpdateByStage1(float deltaTime)
    {
        if (DataParam.indexStage != 0)
            return;
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
      //  CallMNB3Action(deltaTime);
    }
    public void OnUpdateByStage2(float deltaTime)
    {
        if (DataParam.indexStage != 1)
            return;
        CallEN0Action(deltaTime);
        CallN1Action(deltaTime);
        CallN2Action(deltaTime);
        CallN3Action(deltaTime);
        CallN4Action(deltaTime);
        CallVN2Action(deltaTime);
        CallMiniBoss2Action(deltaTime);
        CallBoss2Action(deltaTime);
    }
    public void OnUpdateByStage3(float deltaTime)
    {
        if (DataParam.indexStage != 2)
            return;

        CallEM1Action(deltaTime);
        CallEM2Action(deltaTime);
        CallEM3Action(deltaTime);
        CallEM4Action(deltaTime);
        CallEM5Action(deltaTime);
        CallEM6Action(deltaTime);

        CallEV3Action(deltaTime);
        CallN4Action(deltaTime);
        CallEN0Action(deltaTime);
        CallVN2Action(deltaTime);
        CallN3Action(deltaTime);
        CallMNB3Action(deltaTime);
    }

    public void OnUpdate(float deltaTime)
    {
        OnUpdateByStage1(deltaTime);
        OnUpdateByStage2(deltaTime);
        OnUpdateByStage3(deltaTime);
    }
}
