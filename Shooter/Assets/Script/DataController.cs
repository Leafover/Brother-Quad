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
public class PlayerData
{
    public string level, ID;
    public double hp, DmgGrenade,MoveSpeed;
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
    public List<PlayerData> playerData = new List<PlayerData>();
    public static DataController instance;
    string[] nameDataText = { "enemy0", "enemy1", "enemy2", "enemy3", "enemy4", "enemy5", "enemy6", "enemyv1", "enemyv2", "enemyv3", "enemymn1", "enemyb1" };
    string nameDataPlayerText = "player";
    private void Awake()
    {
        instance = this;
    }
    public bool loaddatabegin;
    private void OnValidate()
    {

        if (!loaddatabegin)
        {
            for (int i = 0; i < nameDataText.Length; i++)
            {
                LoadDataEnemy(nameDataText[i], i);
            }
            LoadDataPlayer(nameDataPlayerText);
            loaddatabegin = true;
        }
    }
    TextAsset _ta;
    JsonData jData;
    public void LoadDataEnemy(string path, int index)
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
    public void LoadDataPlayer(string path)
    {
        if (playerData.Count == 10)
            return;
        _ta = Resources.Load<TextAsset>("JsonData/" + path);
        jData = JsonMapper.ToObject(_ta.text);
        for (int i = 0; i < jData.Count; i++)
        {
            PlayerData _playerDate = JsonMapper.ToObject<PlayerData>(jData[i].ToJson());
            playerData.Add(_playerDate);
        }
    }

}
