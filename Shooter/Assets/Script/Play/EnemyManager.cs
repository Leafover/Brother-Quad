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

    public void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    public void OnUpdate()
    {
        for (int i = 0; i < enemy0s.Count; i++)
        {
            enemy0s[i].OnUpdate();
        }
        for (int i = 0; i < enemy1s.Count; i++)
        {
            enemy1s[i].OnUpdate();
        }
        for (int i = 0; i < enemy3s.Count; i++)
        {
            enemy3s[i].OnUpdate();
        }
        for (int i = 0; i < enemy4s.Count; i++)
        {
            enemy4s[i].OnUpdate();
        }
        for (int i = 0; i < enemy5s.Count; i++)
        {
            enemy5s[i].OnUpdate();
        }
    }
}
