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
[System.Serializable]
public class PlayerData
{
    public string level, ID;
    public double hp, DmgGrenade, MoveSpeed;
}
#region datamission
[System.Serializable]
public class Mission
{
    public string level, mission1name, mission2name, mission3name;
    public double bonusgem, coin1star, exp1star, coin2star, exp2star, coin3star, exp3star, totaldropcoin;
    public int typemission2, valuemission2, typemission3, valuemission3;
}
[System.Serializable]
public class AllMission
{
    public List<Mission> missionData = new List<Mission>();
}
#endregion
public class DataController : MonoBehaviour
{
    public List<AllDataEnemy> allDataEnemy = new List<AllDataEnemy>();
    public List<AllMission> allMission = new List<AllMission>();

    public List<PlayerData> playerData = new List<PlayerData>();
    //  public List<Mission> missions = new List<Mission>();


    public static DataController instance;
    public string[] nameDataText;
    public string[] nameDataMissionText;
    public string nameDataPlayerText;

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

            for (int i = 0; i < nameDataMissionText.Length; i++)
            {
                LoadDataMission(nameDataMissionText[i], i);
            }

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
    public void LoadDataMission(string path, int index)
    {

        if (allMission[index].missionData.Count == 8)
            return;

        _ta = Resources.Load<TextAsset>("JsonData/" + path);

        jData = JsonMapper.ToObject(_ta.text);

        for (int i = 0; i < jData.Count; i++)
        {
            Mission _missionData = JsonMapper.ToObject<Mission>(jData[i].ToJson());
            allMission[index].missionData.Add(_missionData);
        }
    }

}
