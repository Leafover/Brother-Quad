using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region dataenemy
[System.Serializable]
public class AllDataEnemy
{
    public List<EnemyData> enemyData = new List<EnemyData>();
}

[System.Serializable]
public class EnemyData
{
    public string id, level;
    public double hp, movespeed, dmg1, dmg2, dmg3, atksecond1, atksecond2, atksecond3, bulletspeed1, bulletspeed2pixels, bulletspeed3, atkrange, bulletexisttime, exp;
}
#endregion

public class DataController : MonoBehaviour
{
    public List<AllDataEnemy> allDataEnemy = new List<AllDataEnemy>();
    public static DataController instance;
    string[] nameDataText = { "enemy0", "enemy1", "enemy2", "enemy3", "enemy4", "enemy5", "enemy6", "enemyv1", "enemyv2", "enemyv3", "enemymn1", "enemyb1" };

    private void Awake()
    {
        instance = this;
    }
    public bool loaddatabegin;
    private void OnValidate()
    {

        if (!loaddatabegin)
        {
            //LoadData("enemy0", 0);
            //LoadData("enemy1", 1);
            //LoadData("enemy2", 2);
            //LoadData("enemy3", 3);
            //LoadData("enemy4", 4);
            //LoadData("enemy5", 5);
            //LoadData("enemy6", 6);
            //LoadData("enemyv1", 7);
            //LoadData("enemyv2", 8);
            //LoadData("enemyv3", 9);
            //LoadData("enemymn1", 10);
            //LoadData("enemyb1", 11);

            for(int i = 0; i < nameDataText.Length; i ++)
            {
                LoadData(nameDataText[i], i);
            }

            loaddatabegin = true;
        }
    }
    TextAsset _ta;
    JsonData jData;
    public void LoadData(string path, int index)
    {
        if (allDataEnemy[index].enemyData.Count == 10)
            return;

        _ta = Resources.Load<TextAsset>("JsonData/" + path);
        jData = JsonMapper.ToObject(_ta.text);

        for (int i = 0; i < jData.Count; i++)
        {
            EnemyData _enemyData = JsonMapper.ToObject<EnemyData>(jData[i].ToJson());
            allDataEnemy[index].enemyData.Add(_enemyData);
        }



    }


}
