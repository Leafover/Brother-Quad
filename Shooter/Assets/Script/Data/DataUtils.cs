using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class DataUtils
{
    enum eLevel { Normal, Uncommon, Rare, Epic, Legendary }
    enum eType { Shoes, Bag, Gloves, Helmet, Armor, Weapon }
    const string GAME_KEY = "Alien_Shooter_";
    const string KEY_EQIUP = GAME_KEY + "KEY_EQIUP";
    const string KEY_REMOVE_ADS = GAME_KEY + "KEY_REMOVE_ADS";
    const string KEY_SOUND = GAME_KEY + "KEY_SOUND";
    const string KEY_MUSIC = GAME_KEY + "KEY_MUSIC";
    const string KEY_GAME_STAGE = GAME_KEY + "KEY_GAME_STAGE";

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
    public static void SaveLevel(int stage, int level)
    {

    }
    #endregion
}