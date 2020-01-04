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



    public static Dictionary<string, ItemData> dicEquipment = new Dictionary<string, ItemData>();

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