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

    public const string P_DONATE = "com.ohze.brothersquad.donate";
    public const string P_STARTER_PACK = "com.ohze.brothersquad.starterpack";



    public static Dictionary<string, ItemData> dicEquipment = new Dictionary<string, ItemData>();

    #region Remove Ads
    public static void RemoveAds()
    {
        PlayerPrefs.SetInt(KEY_REMOVE_ADS, 1);
    }
    public static bool HasRemoveAds() {
        return !PlayerPrefs.HasKey(KEY_REMOVE_ADS) || (PlayerPrefs.GetInt(KEY_REMOVE_ADS, 0) == 1 ? true : false);
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

    #endregion
}