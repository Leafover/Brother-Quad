using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class DataUtils
{
    enum eLevel { Normal, Uncommon, Rare, Epic, Legendary }
    enum eType { Shoes, Bag, Gloves, Helmet, Armor, Weapon }
    public const int TOTAL_STAGE = 2;
    const string GAME_KEY = "Alien_Shooter_";
    const string KEY_EQIUP = GAME_KEY + "KEY_EQIUP";
    const string KEY_REMOVE_ADS = GAME_KEY + "KEY_REMOVE_ADS";
    const string KEY_SOUND = GAME_KEY + "KEY_SOUND";
    const string KEY_MUSIC = GAME_KEY + "KEY_MUSIC";
    public const string KEY_GAME_STAGE = GAME_KEY + "KEY_GAME_STAGE";

    public const string LINK_MORE_GAME = "https://play.google.com/store/apps/developer?id=Ohze+Games+Studio";
    public static string LINK_RATE_US = "market://details?id=" + Application.identifier;
    public const string P_DONATE = "com.ohze.brothersquad.donate";
    public const string P_STARTER_PACK = "com.ohze.brothersquad.starterpack";

    public static Dictionary<string, ItemData> dicEquipment = new Dictionary<string, ItemData>();

    #region Remove Ads
    public static void RemoveAds()
    {
        PlayerPrefs.SetInt(KEY_REMOVE_ADS, 1);
        PlayerPrefs.Save();
    }
    public static bool HasRemoveAds() {
        return (PlayerPrefs.GetInt(KEY_REMOVE_ADS, 0) == 1 ? true : false);
    }
    #endregion

    #region Sound and Music
    public static void ChangeSound(bool _b)
    {
        PlayerPrefs.SetInt(KEY_SOUND, _b ? 1 : 0);
        PlayerPrefs.Save();
    }
    public static bool IsSoundOn()
    {
        return (PlayerPrefs.GetInt(KEY_SOUND, 0) == 1 ? true : false);
    }

    public static void ChangeMusic(bool _b)
    {
        PlayerPrefs.SetInt(KEY_MUSIC, _b ? 1 : 0);
        PlayerPrefs.Save();
    }
    public static bool IsMusicOn()
    {
        return (PlayerPrefs.GetInt(KEY_MUSIC, 0) == 1 ? true : false);
    }
    #endregion

    #region Equipment Data
    public static void SaveEquipment(string id, string level, int pices) {
        ItemData iData = new ItemData();
        iData.id = id;
        iData.level = level;
        iData.type = GetItemType(id, level);
        iData.pices = pices;

        List<ItemData> lst = new List<ItemData>();

        if (!PlayerPrefs.HasKey(KEY_EQIUP))
        {
            if(!lst.Contains(iData))
                lst.Add(iData);
            string sSave = JsonMapper.ToJson(lst);
            PlayerPrefs.SetString(KEY_EQIUP, sSave);
        }
        else
        {
            JsonData jData = JsonMapper.ToObject(GetAllItem().ToString());
            ItemData iDataSaver = new ItemData();
            for (int i = 0; i < jData.Count; i++)
            {
                ItemData item = JsonMapper.ToObject<ItemData>(jData[i].ToJson());
                if (item.id.Equals(iData.id) && item.level.Equals(iData.level)) {
                    item.pices = pices;
                }
                else
                {
                    item.id = iData.id;
                    item.level = iData.level;
                    item.type = iData.type;
                    item.pices = iData.pices;
                    iDataSaver = item;
                }
            }
        }
    }
    public static void LoadAllEquipment() {
        JsonData jData = JsonMapper.ToObject(GetAllItem().ToString());
        for (int i = 0; i < jData.Count; i++)
        {
            ItemData item = JsonMapper.ToObject<ItemData>(jData[i].ToJson());
            string sKey = item.id + "_" + item.level;
            dicEquipment.Add(sKey, item);
        }
    }

    public static List<ItemData> AllEquipment()
    {
        List<ItemData> lst = new List<ItemData>();
        return lst;
    }
    public static string GetAllItem()
    {
        return PlayerPrefs.GetString(KEY_EQIUP, null);
    }
    private static string GetItemType(string id, string level) {
        return "Helmet";
    }
    #endregion


    #region Stage Data
    public static bool StageHasInit()
    {
        return PlayerPrefs.HasKey(KEY_GAME_STAGE);
    }

    public static void SaveStage(string stageData)
    {
        PlayerPrefs.SetString(KEY_GAME_STAGE, stageData);
    }
    private static string GetStageTextData()
    {
        return PlayerPrefs.GetString(KEY_GAME_STAGE);
    }

    public static List<DataStage> lstAllStage = new List<DataStage>();
    public static void FillAllStage()
    {
        string sData = GetStageTextData();
        JsonData jData = JsonMapper.ToObject(sData);
        
        for (int i = 0; i < jData.Count; i++)
        {
            DataStage jStage = JsonMapper.ToObject<DataStage>(jData[i].ToJson());
            if(!lstAllStage.Contains(jStage))
                lstAllStage.Add(jStage);
        }
    }
    public static MapLevel GetMapByIndex(int stage, int level)
    {
        MapLevel mLevel = new MapLevel();
        string key = stage + "_" + level;
        foreach(MapLevel _m in lstAllStage[stage].levels)
        {
            if (_m.levelID.Equals(key))
            {
                mLevel = _m;
            }
        }
        return mLevel;
    }

    public static void SaveLevel(int stage, int mapIndex)
    {
        string key = stage + "_" + mapIndex;

        for (int i = 0; i < lstAllStage.Count; i++)
        {
            for (int j = 0; j < lstAllStage[i].levels.Count; j++)
            {
                if (lstAllStage[i].levels[j].levelID.Equals(key))
                {
                    lstAllStage[i].levels[j].hasComplete = true;
                }
            }
        }

        string jSave = JsonMapper.ToJson(lstAllStage);
        SaveStage(jSave);
    }

    public static void SaveStars(int stage, int mapIndex, bool miss1, bool miss2) {
        string key = stage + "_" + mapIndex;
        for (int i = 0; i < lstAllStage.Count; i++)
        {
            for (int j = 0; j < lstAllStage[i].levels.Count; j++)
            {
                if (lstAllStage[i].levels[j].levelID.Equals(key))
                {
                    lstAllStage[i].levels[j].mission[0].isPass = true;
                    lstAllStage[i].levels[j].mission[1].isPass = miss1;
                    lstAllStage[i].levels[j].mission[2].isPass = miss2;
                }
            }
        }

        string jSave = JsonMapper.ToJson(lstAllStage);
        SaveStage(jSave);
    }
    #endregion
}