using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class DataUtils
{
    const string APP_ID = "ca-app-pub-7110840808157311~4064500140";
    const string INTERS_ID = "ca-app-pub-7110840808157311/1933639753";
    const string REWARDED_ID = "ca-app-pub-7110840808157311/1742068068";

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


    public const string KEY_GAME_STAGE_HARD = GAME_KEY + "KEY_GAME_STAGE_HARD";
    public const string KEY_GAME_STAGE_INDEX_HARD = GAME_KEY + "KEY_GAME_STAGE_INDEX_HARD";

    public const string LINK_MORE_GAME = "https://play.google.com/store/apps/developer?id=Ohze+Games+Studio";
    public static string LINK_RATE_US = "market://details?id=" + Application.identifier;
    public const string P_DONATE = "com.ohzegame.ramboshooter.brothersquad.donate";
    public const string P_STARTER_PACK = "com.ohzegame.ramboshooter.brothersquad.starterpack";

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
    public static List<DataStage> lstAllStage = new List<DataStage>();
    public static List<DataStage> lstAllStageHard = new List<DataStage>();
    public static List<DataStage> lstAllStageNormal = new List<DataStage>();
    public static int modeSelected = 0;///
    #region Mode Hard

    private static string GetStageHardTextData()
    {
        return PlayerPrefs.GetString(KEY_GAME_STAGE_HARD);
    }
    public static void FillAllStageHard()
    {
        string sData = GetStageHardTextData();
        JsonData jData = JsonMapper.ToObject(sData);
        lstAllStageHard = new List<DataStage>();
        for (int i = 0; i < jData.Count; i++)
        {
            DataStage jStage = JsonMapper.ToObject<DataStage>(jData[i].ToJson());
            if (!lstAllStageHard.Contains(jStage))
                lstAllStageHard.Add(jStage);
        }
    }
    public static void SaveStageHard(string stageData)
    {
        PlayerPrefs.SetString(KEY_GAME_STAGE_HARD, stageData);
    }
    public static bool StageHardHasInit()
    {
        return PlayerPrefs.HasKey(KEY_GAME_STAGE_HARD);
    }
    public static int GetStageHardIndex()
    {
        return PlayerPrefs.GetInt(KEY_GAME_STAGE_INDEX_HARD, 0);
    }
    private static void AddMissionToMap(MapLevel mLevel, string missName)
    {
        LVMission lvMiss = new LVMission();
        lvMiss.id = mLevel.levelID;
        lvMiss.isPass = false;
        lvMiss.missionName = missName;
        if (mLevel.mission == null) mLevel.mission = new List<LVMission>();
        mLevel.mission.Add(lvMiss);
    }
    public static void UnlockHardMode()
    {
        #region UnlockHardMode
        List<DataStage> lstStagesHard = new List<DataStage>();
        for (int i = 0; i < lstAllStageNormal.Count; i++)
        {
            DataStage dataStageHard = new DataStage();
            dataStageHard.stageName = lstAllStageNormal[i].stageName;
            if (i == 0)
            {
                dataStageHard.stageHasUnlock = true;
            }
            else
            {
                dataStageHard.stageHasUnlock = false;
            }
            for (int j = 0; j < lstAllStageNormal[i].levels.Count; j++)
            {
                MapLevel mapLevelHard = new MapLevel();
                mapLevelHard.levelID = lstAllStageNormal[i].levels[j].levelID;
                mapLevelHard.hasComplete = false;
                mapLevelHard.mission = new List<LVMission>();

                AddMissionToMap(mapLevelHard, lstAllStageNormal[i].levels[j].mission[0].missionName);
                AddMissionToMap(mapLevelHard, lstAllStageNormal[i].levels[j].mission[1].missionName);
                AddMissionToMap(mapLevelHard, lstAllStageNormal[i].levels[j].mission[2].missionName);

                #region Add Rewards to MapLevel
                if (mapLevelHard.rewards == null)
                {
                    mapLevelHard.rewards = new List<LVReward>();
                }
                mapLevelHard.rewards.AddRange(lstAllStageNormal[i].levels[j].rewards);
                #endregion

                if (dataStageHard.levels == null)
                {
                    dataStageHard.levels = new List<MapLevel>();
                    dataStageHard.levels.Add(mapLevelHard);
                }
                if (!dataStageHard.levels.Contains(mapLevelHard))
                {
                    dataStageHard.levels.Add(mapLevelHard);
                }
            }
            dataStageHard.stageMode = STAGE_MODE.HARD;

            if (!lstStagesHard.Contains(dataStageHard))
            {
                lstStagesHard.Add(dataStageHard);
            }
        }
        string jHardSave = JsonMapper.ToJson(lstStagesHard);
        SaveStageHard(jHardSave);
        #endregion
    }
    private static void StageHardIncrease()
    {
        int curStage = GetStageIndex() + 1;
        PlayerPrefs.SetInt(KEY_GAME_STAGE_INDEX_HARD, curStage);
    }
    #endregion


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


    public static void FillAllStage()
    {
        string sData = GetStageTextData();
        JsonData jData = JsonMapper.ToObject(sData);
        lstAllStageNormal = new List<DataStage>();
        for (int i = 0; i < jData.Count; i++)
        {
            DataStage jStage = JsonMapper.ToObject<DataStage>(jData[i].ToJson());
            if (!lstAllStageNormal.Contains(jStage))
            {
                lstAllStageNormal.Add(jStage);
            }
        }
        if(StageHardHasInit())
            FillAllStageHard();




        /*lstAllStage.Clear();
        if (modeSelected == 0)
        {
            lstAllStage.AddRange(lstAllStageNormal);
        }
        else if (modeSelected == 1)
        {
            lstAllStage.AddRange(lstAllStageHard);
        }*/
    }

    public static MapLevel GetMapByIndex(int stage, int level)
    {
        MapLevel mLevel = new MapLevel();
        if(modeSelected == 0)
        {
            mLevel = lstAllStageNormal[stage].levels[level];
        }
        else if(modeSelected == 1)
        {
            mLevel = lstAllStageHard[stage].levels[level];
        }

        return mLevel;
    }

    public static void SaveLevel(int stage, int mapIndex)
    {
        if (modeSelected == 0)
        {
            lstAllStageNormal[stage].levels[mapIndex].hasComplete = true;
            lstAllStageNormal[stage].levelUnlock = mapIndex;

            if (mapIndex == 7)
            {
                lstAllStageNormal[(stage + 1 > lstAllStageNormal.Count ? stage : stage + 1)].stageHasUnlock = true;
                
                UnlockHardMode();

                StageIncrease();
            }

            string jSave = JsonMapper.ToJson(lstAllStageNormal);
            SaveStage(jSave);
        }
        else if (modeSelected == 1)
        {

            lstAllStageHard[stage].levels[mapIndex].hasComplete = true;
            lstAllStageHard[stage].levelUnlock = mapIndex;

            if (mapIndex == 7)
            {
                lstAllStageHard[(stage + 1 > lstAllStageNormal.Count ? stage : stage + 1)].stageHasUnlock = true;

                StageHardIncrease();
            }

            string jSave = JsonMapper.ToJson(lstAllStageHard);
            SaveStageHard(jSave);
        }
    }



    public static void SaveStars(int stage, int mapIndex, bool miss1, bool miss2)
    {
        if(modeSelected == 0)
        {
            string key = stage + "_" + mapIndex;
            lstAllStageNormal[stage].levels[mapIndex].mission[0].isPass = true;
            lstAllStageNormal[stage].levels[mapIndex].mission[1].isPass = miss1;
            lstAllStageNormal[stage].levels[mapIndex].mission[2].isPass = miss2;

            string jSave = JsonMapper.ToJson(lstAllStageNormal);
            SaveStage(jSave);
        }
        else if(modeSelected == 1)
        {
            lstAllStageHard[stage].levels[mapIndex].mission[0].isPass = true;
            lstAllStageHard[stage].levels[mapIndex].mission[1].isPass = miss1;
            lstAllStageHard[stage].levels[mapIndex].mission[2].isPass = miss2;

            string jSaveHard = JsonMapper.ToJson(lstAllStageHard);
            SaveStageHard(jSaveHard);
        }
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

        // Debug.LogError("zooooooooooo1");
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