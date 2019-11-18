using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public List<Enemy0Controller> enemy0s;
    public void Start()
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
    }
}
