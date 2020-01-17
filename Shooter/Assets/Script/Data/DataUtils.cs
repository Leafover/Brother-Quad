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
    public const string KEY_GAME_STAGE_INDEX = GAME_KEY + "KEY_GAME_STAGE_INDEX";
    public const string KEY_PLAYER_DATA = GAME_KEY + "KEY_PLAYER_DATA";
    public const string KEY_HEROES_INDEX = GAME_KEY + "KEY_HEROES_INDEX";
    public const string KEY_ALL_PLAYER_DATA = GAME_KEY + "KEY_ALL_PLAYER_DATA";

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
    public static bool HasRemoveAds()
    {
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
        return (PlayerPrefs.GetInt(KEY_SOUND, 1) == 1 ? true : false);
    }

    public static void ChangeMusic(bool _b)
    {
        PlayerPrefs.SetInt(KEY_MUSIC, _b ? 1 : 0);
        PlayerPrefs.Save();
    }
    public static bool IsMusicOn()
    {
        return (PlayerPrefs.GetInt(KEY_MUSIC, 1) == 1 ? true : false);
    }
    #endregion

    #region Equipment Data
    public static void SaveEquipment(string id, string level, int pices)
    {
        ItemData iData = new ItemData();
        iData.id = id;
        iData.level = level;
        iData.type = GetItemType(id, level);
        iData.pices = pices;

        List<ItemData> lst = new List<ItemData>();

        if (!PlayerPrefs.HasKey(KEY_EQIUP))
        {
            if (!lst.Contains(iData))
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
                if (item.id.Equals(iData.id) && item.level.Equals(iData.level))
                {
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
    public static void LoadAllEquipment()
    {
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
    private static string GetItemType(string id, string level)
    {
        return "Helmet";
    }
    #endregion

    #region Stage Data
    public static int GetStageIndex()
    {
        return PlayerPrefs.GetInt(KEY_GAME_STAGE_INDEX, 0);
    }
    private static void StageIncrease()
    {
        int curStage = GetStageIndex() + 1;
        PlayerPrefs.SetInt(KEY_GAME_STAGE_INDEX, curStage);
    }
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
            if (!lstAllStage.Contains(jStage))
                lstAllStage.Add(jStage);
        }
    }

    public static MapLevel GetMapByIndex(int stage, int level)
    {
        MapLevel mLevel = new MapLevel();
        string key = stage + "_" + level;
        mLevel = lstAllStage[stage].levels[level];
        //foreach(MapLevel _m in lstAllStage[stage].levels)
        //{
        //    if (_m.levelID.Equals(key))
        //    {
        //        mLevel = _m;
        //    }
        //}
        return mLevel;
    }

    public static void SaveLevel(int stage, int mapIndex)
    {
        string key = stage + "_" + mapIndex;

        //for (int i = 0; i < lstAllStage.Count; i++)
        {
            //for (int j = 0; j < lstAllStage[/*i*/stage].levels.Count; j++)
            {
                //if (lstAllStage[/*i*/stage].levels[/*j*/mapIndex].levelID.Equals(key))
                {
                    lstAllStage[/*i*/stage].levels[/*j*/mapIndex].hasComplete = true;
                    lstAllStage[/*i*/stage].levelUnlock = /*j*/mapIndex;
                }
            }
        }

        if (mapIndex == 7)
        {
            lstAllStage[(stage + 1 > lstAllStage.Count ? stage : stage + 1)].stageHasUnlock = true;
            StageIncrease();
        }

        string jSave = JsonMapper.ToJson(lstAllStage);
        SaveStage(jSave);
    }

    public static void SaveStars(int stage, int mapIndex, bool miss1, bool miss2)
    {
        string key = stage + "_" + mapIndex;
        //for (int i = 0; i < lstAllStage.Count; i++)
        {
            //for (int j = 0; j < lstAllStage[i].levels.Count; j++)
            {
                //if (lstAllStage[i].levels[j].levelID.Equals(key))
                {
                    lstAllStage[/*i*/stage].levels[/*j*/mapIndex].mission[0].isPass = true;
                    lstAllStage[/*i*/stage].levels[/*j*/mapIndex].mission[1].isPass = miss1;
                    lstAllStage[/*i*/stage].levels[/*j*/mapIndex].mission[2].isPass = miss2;
                }
            }
        }

        string jSave = JsonMapper.ToJson(lstAllStage);
        SaveStage(jSave);
    }
    #endregion

    #region Player Data Info
    public static List<PlayerDataInfo> lstAllHeroes = new List<PlayerDataInfo>();
    public static PlayerDataInfo playerInfo;


    public static int HeroIndex()
    {
        return PlayerPrefs.GetInt(KEY_HEROES_INDEX, 0);
    }
    public static void SetHeroIndex(int index)
    {
        PlayerPrefs.SetInt(KEY_HEROES_INDEX, index);
        PlayerPrefs.Save();
    }
    public static void SavePlayerData()
    {
        //string jSave = JsonMapper.ToJson(/*playerInfo*/lstAllHeroes[HeroIndex()]);
        //PlayerPrefs.SetString(KEY_PLAYER_DATA, jSave);

        string jSave = JsonMapper.ToJson(lstAllHeroes);
        PlayerPrefs.SetString(KEY_ALL_PLAYER_DATA, jSave);
        PlayerPrefs.Save();
    }
    public static bool PlayerInfoHasInit()
    {
        return PlayerPrefs.HasKey(KEY_PLAYER_DATA) || string.IsNullOrEmpty(GetPlayerData());
    }
    public static void FillPlayerDataInfo()
    {
        if (!PlayerPrefs.HasKey(KEY_ALL_PLAYER_DATA) || string.IsNullOrEmpty(GetAllPlayerData()))
        {
            playerInfo = new PlayerDataInfo();
            playerInfo.level = 1;
            playerInfo.hp = (int)DataController.instance.playerData[0].hp;
            playerInfo.exp = 0;
            playerInfo.id = "P1";
            playerInfo.curStars = 1;
            playerInfo.name = "REMITANO";
            playerInfo.coins = 0;
            playerInfo.gems = 0;
            lstAllHeroes.Add(playerInfo);
        }
        else
        {
            string sData = GetAllPlayerData();
            JsonData jData = JsonMapper.ToObject(sData);
            for (int i = 0; i < jData.Count; i++)
            {
                PlayerDataInfo jStage = JsonMapper.ToObject<PlayerDataInfo>(jData[i].ToJson());
                if (!lstAllHeroes.Contains(jStage))
                    lstAllHeroes.Add(jStage);
            }
        }

        playerInfo = lstAllHeroes[HeroIndex()];
        //if (!PlayerPrefs.HasKey(KEY_PLAYER_DATA) || string.IsNullOrEmpty(GetPlayerData()))
        //{
        //    playerInfo = new PlayerDataInfo();
        //    playerInfo.level = 1;
        //    playerInfo.hp = (int)DataController.instance.playerData[0].hp;
        //    playerInfo.exp = 0;
        //    playerInfo.curStars = 1;
        //    playerInfo.name = "REMITANO";
        //    playerInfo.coins = 0;
        //    playerInfo.gems = 0;
        //    SavePlayerData();
        //}
        //else
        //{
        //    PlayerDataInfo pInfo = JsonMapper.ToObject<PlayerDataInfo>(GetPlayerData());
        //    playerInfo = pInfo;
        //}

        if (MainMenuController.Instance != null)
        {
            MainMenuController.Instance.UpdateCoinAndGem();
        }
    }
    public static void UpdateCoinAndGem(int newCoin, int newGem)
    {
        /*playerInfo*/
        lstAllHeroes[HeroIndex()].coins = newCoin;
        /*playerInfo*/
        lstAllHeroes[HeroIndex()].gems = newGem;
        SavePlayerData();
        if (MainMenuController.Instance != null)
        {
            MainMenuController.Instance.UpdateCoinAndGem();
        }
    }
    public static void AddCoinAndGame(int coinAdded, int gemAdded)
    {
        /*playerInfo*/
        lstAllHeroes[HeroIndex()].coins += coinAdded;
        /*playerInfo*/
        lstAllHeroes[HeroIndex()].gems += gemAdded;
        SavePlayerData();
        if (MainMenuController.Instance != null)
        {
            MainMenuController.Instance.UpdateCoinAndGem();
        }
    }
    private static string GetAllPlayerData()
    {
        return PlayerPrefs.GetString(KEY_ALL_PLAYER_DATA);
    }
    private static string GetPlayerData()
    {
        return PlayerPrefs.GetString(KEY_PLAYER_DATA, "");
    }
    #endregion


    public static string DisplayRichText(double dFrom, double dTo)
    {
        return /*"<color=white>" + dFrom + "</color>" + */"<color=green>" + dTo + "</color>";
    }
}