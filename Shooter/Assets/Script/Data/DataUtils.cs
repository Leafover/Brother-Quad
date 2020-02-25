using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class DataUtils
{


    public enum eLevel { Normal, Uncommon, Rare, Epic, Legendary }
    public enum eType { SHOES, BAG, GLOVES, HELMET, ARMOR, WEAPON }
    public enum ITEM_SHOP_TYPE { PACKAGE, GEM, RESOURCES }
    public const int TOTAL_STAGE = 2;
    public const int MAX_LEVEL_HERO = 5;
    const string GAME_KEY = "Alien_Shooter_";
    const string KEY_REMOVE_ADS = GAME_KEY + "KEY_REMOVE_ADS";
    const string KEY_SOUND = GAME_KEY + "KEY_SOUND";
    const string KEY_MUSIC = GAME_KEY + "KEY_MUSIC";
    public const string KEY_GAME_STAGE = GAME_KEY + "KEY_GAME_STAGE";
    public const string KEY_GAME_STAGE_INDEX = GAME_KEY + "KEY_GAME_STAGE_INDEX";
    public const string KEY_PLAYER_DATA = GAME_KEY + "KEY_PLAYER_DATA";
    public const string KEY_HEROES_INDEX = GAME_KEY + "KEY_HEROES_INDEX";
    public const string KEY_ALL_PLAYER_DATA = GAME_KEY + "KEY_ALL_PLAYER_DATA";

    public const string KEY_HEROES_DATA = GAME_KEY + "KEY_HEROES_DATA";
    public const string KEY_ALL_HERO_DATA = GAME_KEY + "KEY_ALL_HERO_DATA";



    public const string KEY_GAME_STAGE_HARD = GAME_KEY + "KEY_GAME_STAGE_HARD";
    public const string KEY_GAME_STAGE_INDEX_HARD = GAME_KEY + "KEY_GAME_STAGE_INDEX_HARD";
    public const string KEY_EQUIPMENT_DATA = GAME_KEY + "KEY_EQUIPMENT_DATA";
    public const string KEY_EQUIPPED_DATA = GAME_KEY + "KEY_EQUIPPED_DATA";

    public const string LINK_MORE_GAME = "https://play.google.com/store/apps/developer?id=Ohze+Games+Studio";
    public static string LINK_RATE_US = "market://details?id=" + Application.identifier;
    public const string P_DONATE = "com.ohzegame.ramboshooter.brothersquad.donate";
    public const string P_STARTER_PACK = "com.ohzegame.ramboshooter.brothersquad.starterpack";
    public const string P_CHEAP_PACK = "com.brothersquad.pack1";
    public const string P_BEST_CHOICE = "com.brothersquad.pack2";
    public const string P_PROFESSIONAL_PACK = "com.brothersquad.pack3";

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

    public static void FillEquipmentData()
    {
        string sData = GetAllEquipment();
        dicAllEquipment = new Dictionary<string, ItemData>();
        if (sData.Trim().Length > 0)
        {
            JsonData jData = JsonMapper.ToObject(sData);
            string _key = "";
            for (int i = 0; i < jData.Count; i++)
            {
                ItemData itemData = JsonMapper.ToObject<ItemData>(jData[i].ToJson());
                _key = itemData.id + "_" + itemData.level + "_" + itemData.isUnlock + "_" + itemData.isEquipped;
                if (!dicAllEquipment.ContainsKey(_key))
                {
                    dicAllEquipment.Add(_key, itemData);
                }
            }
        }

        FillWeaponData();
        FillArmorData();
        FillHelmet();
        FillGloves();
        FillBag();
        FillShoes();

        #region Check and Init Equipped Item
        dicEquippedItem = new Dictionary<string, ItemData>();
        string sEquipped = GetEquippedItem();
        if (!IsEquippedInit())
        {
            ItemData _item = new ItemData();
            _item.id = "W1";
            _item.type = eType.WEAPON.ToString();
            _item.level = eLevel.Normal.ToString();
            _item.itemName = "Mac 10";
            _item.curStar = 0;
            _item.isUnlock = true;
            _item.isEquipped = true;
            EquipItem(_item);

            _keyEquip = _item.id + "_" + _item.level + "_" + _item.isUnlock + "_" + _item.isEquipped;
            if (!dicAllEquipment.ContainsKey(_keyEquip))
            {
                dicAllEquipment.Add(_keyEquip, _item);
            }
        }
        else
        {
            if (sEquipped.Trim().Length > 0)
            {
                JsonData jEquip = JsonMapper.ToObject(sEquipped);

                for (int i = 0; i < jEquip.Count; i++)
                {
                    ItemData itemData = JsonMapper.ToObject<ItemData>(jEquip[i].ToJson());
                    _keyEquip = itemData.id + "_" + itemData.level + "_" + itemData.isUnlock + "_" + itemData.isEquipped;
                    if (!dicEquippedItem.ContainsKey(_keyEquip))
                    {
                        dicEquippedItem.Add(_keyEquip, itemData);
                    }

                    if (!dicAllEquipment.ContainsKey(_keyEquip))
                    {
                        dicAllEquipment.Add(_keyEquip, itemData);
                    }
                }
            }
        }
        #endregion
    }
    private static string _keyEquip = "";

    #region Fill Weapon Data
    public static Dictionary<string, WeaponList> dicWeapon = new Dictionary<string, WeaponList>();
    private static void FillWeaponData()
    {
        dicWeapon.Clear();
        for (int i = 0; i < DataController.instance.allWeapon.Count; i++)
        {
            for (int j = 0; j < DataController.instance.allWeapon[i].weaponList.Count; j++)
            {
                WeaponList weapon = DataController.instance.allWeapon[i].weaponList[j];
                dicWeapon.Add(weapon.ID + "_" + weapon.Level, weapon);
            }
        }
    }
    #endregion
    #region Fill Armor
    public static Dictionary<string, ArmorList> dicArmor = new Dictionary<string, ArmorList>();
    private static void FillArmorData()
    {
        dicArmor.Clear();
        for (int i = 0; i < DataController.instance.allArmor.Count; i++)
        {
            for (int j = 0; j < DataController.instance.allArmor[i].armorList.Count; j++)
            {
                ArmorList armor = DataController.instance.allArmor[i].armorList[j];
                dicArmor.Add(armor.ID + "_" + armor.Level, armor);
            }
        }
    }
    #endregion
    #region Fill Helmet
    public static Dictionary<string, HelmetList> dicHelmet = new Dictionary<string, HelmetList>();
    private static void FillHelmet()
    {
        dicHelmet.Clear();
        for (int i = 0; i < DataController.instance.allHelmet.Count; i++)
        {
            for (int j = 0; j < DataController.instance.allHelmet[i].helmetList.Count; j++)
            {
                HelmetList helmet = DataController.instance.allHelmet[i].helmetList[j];
                dicHelmet.Add(helmet.ID + "_" + helmet.Level, helmet);
            }
        }
    }
    #endregion
    #region Fill Gloves
    public static Dictionary<string, GlovesList> dicGloves = new Dictionary<string, GlovesList>();
    private static void FillGloves()
    {
        dicGloves.Clear();
        for (int i = 0; i < DataController.instance.allGloves.Count; i++)
        {
            for (int j = 0; j < DataController.instance.allGloves[i].glovesList.Count; j++)
            {
                GlovesList gloves = DataController.instance.allGloves[i].glovesList[j];
                dicGloves.Add(gloves.ID + "_" + gloves.Level, gloves);
            }
        }
    }
    #endregion
    #region Fill Bag
    public static Dictionary<string, BagList> dicBag = new Dictionary<string, BagList>();
    private static void FillBag()
    {
        dicBag.Clear();
        for (int i = 0; i < DataController.instance.allArmor.Count; i++)
        {
            for (int j = 0; j < DataController.instance.allBag[i].bagList.Count; j++)
            {
                BagList bag = DataController.instance.allBag[i].bagList[j];
                dicBag.Add(bag.ID + "_" + bag.Level, bag);
            }
        }
    }
    #endregion
    #region Fill Shoes
    public static Dictionary<string, ShoesList> dicShoes = new Dictionary<string, ShoesList>();
    private static void FillShoes()
    {
        dicShoes.Clear();
        for (int i = 0; i < DataController.instance.allShoes.Count; i++)
        {
            for (int j = 0; j < DataController.instance.allShoes[i].shoesList.Count; j++)
            {
                ShoesList shoes = DataController.instance.allShoes[i].shoesList[j];
                dicShoes.Add(shoes.ID + "_" + shoes.Level, shoes);
            }
        }
    }
    #endregion

    public static Dictionary<string, ItemData> dicAllEquipment, dicEquippedItem;
    public static ItemData itemNew;

    public static List<ItemData> lstAllEquipment = new List<ItemData>();
    public static float GetPiceByStar(ItemData itemData)
    {
        float res = 0;
        string key = itemData.id + "_" + itemData.level/* + "_"+ itemData.isUnlock*/;
        switch (itemData.type)
        {
            case "ARMOR":
                res = dicArmor[key].SoManhYeuCauValue[itemData.curStar];
                break;
            case "BAG":
                res = dicBag[key].SoManhYeuCauValue[itemData.curStar];
                break;
            case "GLOVES":
                res = dicGloves[key].SoManhYeuCauValue[itemData.curStar];
                break;
            case "HELMET":
                res = dicHelmet[key].SoManhYeuCauValue[itemData.curStar];
                break;
            case "SHOES":
                res = dicShoes[key].SoManhYeuCauValue[itemData.curStar];
                break;
            case "WEAPON":
                res = dicWeapon[key].SoManhYeuCauValue[itemData.curStar];
                break;
        }
        return res;
    }
    private static void CheckItemUnlock(string id, eType itemType, string level, int pices, int curStar, bool isUnlock, bool isEquipped)
    {
        bool result = false;
        string key = id + "_" + level + "_" + isUnlock + "_" + isEquipped;
        string key_ = id + "_" + level;
        switch (itemType)
        {
            case eType.ARMOR:
                if ((pices >= (int)dicArmor[key_].SoManhYeuCauValue[curStar]))
                {
                    pices -= (int)dicArmor[key_].SoManhYeuCauValue[curStar];
                    result = true;
                }
                break;
            case eType.BAG:
                if ((pices >= (int)dicBag[key_].SoManhYeuCauValue[curStar]))
                {
                    pices -= (int)dicBag[key_].SoManhYeuCauValue[curStar];
                    result = true;
                }
                break;
            case eType.GLOVES:
                if ((pices >= (int)dicGloves[key_].SoManhYeuCauValue[curStar]))
                {
                    pices -= (int)dicGloves[key_].SoManhYeuCauValue[curStar];
                    result = true;
                }
                break;
            case eType.HELMET:
                if ((pices >= (int)dicHelmet[key_].SoManhYeuCauValue[curStar]))
                {
                    pices -= (int)dicHelmet[key_].SoManhYeuCauValue[curStar];
                    result = true;
                }
                break;
            case eType.SHOES:
                if ((pices >= (int)dicShoes[key_].SoManhYeuCauValue[curStar]))
                {
                    pices -= (int)dicShoes[key_].SoManhYeuCauValue[curStar];
                    result = true;
                }
                break;
            case eType.WEAPON:
                if ((pices >= (int)dicWeapon[key_].SoManhYeuCauValue[curStar]))
                {
                    pices -= (int)dicWeapon[key_].SoManhYeuCauValue[curStar];
                    result = true;
                }
                break;
        }

        if (result)
        {
            ItemData itemNew = dicAllEquipment[key];
            dicAllEquipment.Remove(key);
            string _newKey = id + "_" + level + "_" + result + "_" + isEquipped;
            if (!dicAllEquipment.ContainsKey(_newKey))
            {
                dicAllEquipment.Add(_newKey, itemNew);

                dicAllEquipment[_newKey].curStar += 1;
                dicAllEquipment[_newKey].pices = pices;
                dicAllEquipment[_newKey].isUnlock = result;

                if (EquipmentManager.Instance != null)
                {
                    EquipmentManager.Instance.RefreshInventory(dicAllEquipment[_newKey]);
                }
            }
            else
            {
                dicAllEquipment[_newKey].quantity += 1;
                dicAllEquipment[_newKey].curStar += 1;
                dicAllEquipment[_newKey].pices = pices;
                dicAllEquipment[_newKey].isUnlock = result;
                ItemData iAddNew = dicAllEquipment[_newKey];
            }
        }
    }

    private static string GetItemName(ItemData iData, eType _itemType)
    {
        string _name = "";

        string _key = iData.id + "_" + iData.level;
        switch (_itemType)
        {
            case eType.ARMOR:
                _name = dicArmor[_key].NAME;
                break;
            case eType.BAG:
                _name = dicBag[_key].NAME;
                break;
            case eType.GLOVES:
                _name = dicGloves[_key].NAME;
                break;
            case eType.HELMET:
                _name = dicHelmet[_key].NAME;
                break;
            case eType.SHOES:
                _name = dicShoes[_key].NAME;
                break;
            case eType.WEAPON:
                _name = dicWeapon[_key].NAME;
                break;
        }
        return _name;
    }
    public static void TakeItem(string _id, eType _itemType, eLevel _itemLevel, int _pices, bool fullPart)
    {
        if (_pices > 0)
        {
            ItemData iData = new ItemData();
            iData.id = _id;
            iData.type = _itemType.ToString();
            iData.level = _itemLevel.ToString();
            iData.isUnlock = fullPart;
            iData.pices = fullPart ? 0 : _pices;
            iData.itemName = GetItemName(iData, _itemType);
            iData.isEquipped = false;

            string _key = iData.id + "_" + iData.level.ToString() + "_" + iData.isUnlock + "_" + iData.isEquipped;
            if (dicAllEquipment.ContainsKey(_key))
            {
                if (!iData.isUnlock)
                {
                    if (!dicAllEquipment[_key].isUnlock)
                    {
                        string _newKey = iData.id + "_" + iData.level.ToString() + "_" + /*true*/iData.isUnlock + "_" + iData.isEquipped;
                        dicAllEquipment[_key].pices += _pices;
                        CheckItemUnlock(iData.id, _itemType, iData.level, dicAllEquipment[_key].pices, iData.curStar, iData.isUnlock, iData.isEquipped);
                    }
                    else
                    {
                        dicAllEquipment[_key].quantity += 1;
                    }
                }
                else
                {
                    dicAllEquipment[_key].quantity += 1;
                }
            }
            else
            {
                dicAllEquipment.Add(_key, iData);
                if (EquipmentManager.Instance != null)
                {
                    EquipmentManager.Instance.RefreshInventory(dicAllEquipment[_key]);
                }
            }
            SaveEquipmentData();
        }
    }


    public static bool IsHaveEquipment()
    {
        return PlayerPrefs.HasKey(KEY_EQUIPMENT_DATA);
    }
    public static void SaveEquipmentData()
    {
        string jSave = JsonMapper.ToJson(dicAllEquipment);
        PlayerPrefs.SetString(KEY_EQUIPMENT_DATA, jSave);
        PlayerPrefs.Save();
    }
    public static string GetAllEquipment()
    {
        return PlayerPrefs.GetString(KEY_EQUIPMENT_DATA, "");
    }
    #endregion

    #region EQUIPPED DATA

    public static string GetEquippedItem()
    {
        return PlayerPrefs.GetString(KEY_EQUIPPED_DATA, "");
    }
    public static void EquipItem(ItemData iData)
    {
        string _key = iData.id + "_" + iData.level.ToString() + "_" + iData.isUnlock + "_" + iData.isEquipped;
        if (!dicEquippedItem.ContainsKey(_key))
        {
            ItemData itemData = new ItemData();
            itemData.id = iData.id;
            itemData.type = iData.type;
            itemData.level = iData.level;
            itemData.isUnlock = iData.isUnlock;
            itemData.itemName = iData.itemName;
            itemData.isEquipped = true;
            dicEquippedItem.Add(_key, iData);
        }

        SaveEquipmentData();
        SaveEquippedData();
    }
    public static void SaveEquippedData()
    {
        string jSave = JsonMapper.ToJson(dicEquippedItem);
        PlayerPrefs.SetString(KEY_EQUIPPED_DATA, jSave);
        PlayerPrefs.Save();
    }
    public static bool IsEquippedInit()
    {
        return PlayerPrefs.HasKey(KEY_EQUIPPED_DATA);
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
        if (StageHardHasInit())
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
        if (modeSelected == 0)
        {
            mLevel = lstAllStageNormal[stage].levels[level];
        }
        else if (modeSelected == 1)
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
                lstAllStageNormal[(stage + 1 >= lstAllStageNormal.Count ? stage : stage + 1)].stageHasUnlock = true;

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
                lstAllStageHard[(stage + 1 >= lstAllStageNormal.Count ? stage : stage + 1)].stageHasUnlock = true;

                StageHardIncrease();
            }

            string jSave = JsonMapper.ToJson(lstAllStageHard);
            SaveStageHard(jSave);
        }
    }



    public static void SaveStars(int stage, int mapIndex, bool miss1, bool miss2)
    {
        if (modeSelected == 0)
        {
            string key = stage + "_" + mapIndex;
            lstAllStageNormal[stage].levels[mapIndex].mission[0].isPass = true;
            lstAllStageNormal[stage].levels[mapIndex].mission[1].isPass = miss1;
            lstAllStageNormal[stage].levels[mapIndex].mission[2].isPass = miss2;

            string jSave = JsonMapper.ToJson(lstAllStageNormal);
            SaveStage(jSave);
        }
        else if (modeSelected == 1)
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
    public static List<PlayerDataInfo> lstAllPlayerHeroes = new List<PlayerDataInfo>();
    public static PlayerDataInfo playerInfo;

    public static Dictionary<string, HeroDataInfo> dicAllHero;
    public static HeroDataInfo heroInfo;

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
        string jSave = JsonMapper.ToJson(lstAllPlayerHeroes);
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
            playerInfo.level = 0;
            playerInfo.hp = (int)DataController.instance.playerData[0].playerData[0].hp;
            playerInfo.exp = 0;
            playerInfo.id = "P1";
            playerInfo.curStars = 1;
            playerInfo.name = "REMITANO";
            playerInfo.coins = 0;
            playerInfo.gems = 0;
            playerInfo.pices = 0;
            playerInfo.isUnlock = true;
            playerInfo.isEquipped = true;
            lstAllPlayerHeroes.Add(playerInfo);
        }
        else
        {
            string sData = GetAllPlayerData();
            JsonData jData = JsonMapper.ToObject(sData);
            for (int i = 0; i < jData.Count; i++)
            {
                PlayerDataInfo jPlayerInfo = JsonMapper.ToObject<PlayerDataInfo>(jData[i].ToJson());
                if (!lstAllPlayerHeroes.Contains(jPlayerInfo))
                    lstAllPlayerHeroes.Add(jPlayerInfo);
            }
        }
        dicAllHero = new Dictionary<string, HeroDataInfo>();
        if (!PlayerPrefs.HasKey(KEY_ALL_HERO_DATA) || string.IsNullOrEmpty(GetAllHeroData()))
        {
            heroInfo = new HeroDataInfo();
            heroInfo.id = "P1";
            heroInfo.name = "REMITANO";
            heroInfo.level = 0;
            heroInfo.exp = 0;
            heroInfo.hp = GetHeroHPByID("P1");
            heroInfo.curStars = 1;
            heroInfo.pices = 0;
            heroInfo.isUnlock = true;
            heroInfo.isEquipped = true;
            if (!dicAllHero.ContainsKey(heroInfo.id))
            {
                dicAllHero.Add(heroInfo.id, heroInfo);
            }
            ChooseHero(heroInfo);
        }
        else
        {
            string sHeroData = GetAllHeroData();
            JsonData jHeroData = JsonMapper.ToObject(sHeroData);
            for(int i = 0; i < jHeroData.Count; i++)
            {

                HeroDataInfo hdInfo = JsonMapper.ToObject<HeroDataInfo>(jHeroData[i].ToJson());
                if (!dicAllHero.ContainsKey(hdInfo.id))
                {
                    dicAllHero.Add(hdInfo.id, hdInfo);
                }
            }
        }


        playerInfo = lstAllPlayerHeroes[HeroIndex()];

        heroInfo = DataHero();

        if (MainMenuController.Instance != null)
        {
            MainMenuController.Instance.UpdateCoinAndGem();
        }
    }
    private static HeroDataInfo DataHero()
    {
        string sHeroData = PlayerPrefs.GetString(KEY_HEROES_DATA, "");
        JsonData jHeroData = JsonMapper.ToObject(sHeroData);

        HeroDataInfo hdInfo = JsonMapper.ToObject<HeroDataInfo>(jHeroData.ToJson());
        return hdInfo;
    }
    public static void UpdateCoinAndGem(int newCoin, int newGem)
    {
        lstAllPlayerHeroes[HeroIndex()].coins = newCoin;
        lstAllPlayerHeroes[HeroIndex()].gems = newGem;


        SavePlayerData();
        if (MainMenuController.Instance != null)
        {
            MainMenuController.Instance.UpdateCoinAndGem();
        }
    }
    public static void AddCoinAndGame(int coinAdded, int gemAdded)
    {
        lstAllPlayerHeroes[HeroIndex()].coins += coinAdded;
        lstAllPlayerHeroes[HeroIndex()].gems += gemAdded;


        SavePlayerData();
        if (MainMenuController.Instance != null)
        {
            MainMenuController.Instance.UpdateCoinAndGem();
        }

        DataController.instance.DoDailyQuest(6, coinAdded);
    }

    public static string GetAllHeroData()
    {
        return PlayerPrefs.GetString(KEY_ALL_HERO_DATA);
    }
    public static void SaveAllHero()
    {
        string jSaveAllHero = JsonMapper.ToJson(dicAllHero);
        PlayerPrefs.SetString(KEY_ALL_HERO_DATA, jSaveAllHero);
        PlayerPrefs.Save();
    }
    private static string GetHeroData()
    {
        return PlayerPrefs.GetString(KEY_HEROES_DATA, "");
    }
    public static void ChooseHero(HeroDataInfo heroData)
    {
        PlayerPrefs.SetString(KEY_HEROES_DATA, JsonMapper.ToJson(heroData));
        PlayerPrefs.Save();
    }

    public static void TakeHeroPice(string hID, int hPices)
    {
        if (!dicAllHero.ContainsKey(hID))
        {
            HeroDataInfo heroInfo = new HeroDataInfo();
            heroInfo.id = hID;
            heroInfo.pices = hPices;
            dicAllHero.Add(hID,heroInfo);
        }
        else
        {
            dicAllHero[hID].pices += hPices;
            ChooseHero(dicAllHero[hID]);
        }

        SaveAllHero();
    }
    public static int GetHeroHPByID(string id)
    {
        double dbResult = 500;
        for(int i = 0; i < DataController.instance.playerData.Count; i++)
        {
            for(int j = 0; j< DataController.instance.playerData[i].playerData.Count; j++)
            {
                if (DataController.instance.playerData[i].playerData[j].ID.Equals(id))
                {
                    dbResult = DataController.instance.playerData[i].playerData[j].hp;
                    break;
                }
            }
        }
        return (int)dbResult;
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

    #region Other
    public static string DisplayRichText(double dFrom, double dTo)
    {
        return /*"<color=white>" + dFrom + "</color>" + */"<color=green>" + dTo + "</color>";
    }

    public static Sprite GetSpriteByName(string name, ItemSpriteData allSpriteData)
    {
        Sprite _spr = null;
        string[] strSP = name.Split('-');

        for (int i = 0; i < allSpriteData.spriteDatas.Count; i++)
        {
            if (allSpriteData.spriteDatas[i].itemName.Equals(strSP[strSP.Length - 1]))
            {
                _spr = allSpriteData.spriteDatas[i].sprItem;
            }
        }
        return _spr;
    }
    public static Sprite GetSpriteByType(ItemData itemData)
    {
        Sprite _spr = null;
        #region Check Item quality
        switch (itemData.level)
        {
            case "Normal":
                _spr = MainMenuController.Instance.sprNormal;
                break;
            case "Uncommon":
                _spr = MainMenuController.Instance.sprUncommon;
                break;
            case "Rare":
                _spr = MainMenuController.Instance.sprRare;
                break;
            case "Epic":
                _spr = MainMenuController.Instance.sprEpic;
                break;
            case "Legendary":
                _spr = MainMenuController.Instance.sprLegendary;
                break;
            default:
                _spr = MainMenuController.Instance.sprNormal;
                break;
        }
        #endregion
        return _spr;
    }
    public static double GetPriceByType(ItemData itemData)
    {
        double dbValue = 0;
        string keyEquipped = itemData.id + "_" + itemData.level;
        switch (itemData.type)
        {
            case "ARMOR":
                dbValue = dicArmor[keyEquipped].GiaKhiRaDo;
                break;
            case "BAG":
                dbValue = dicBag[keyEquipped].GiaKhiRaDo;
                break;
            case "GLOVES":
                dbValue = dicGloves[keyEquipped].GiaKhiRaDo;
                break;
            case "HELMET":
                dbValue = dicHelmet[keyEquipped].GiaKhiRaDo;
                break;
            case "SHOES":
                dbValue = dicShoes[keyEquipped].GiaKhiRaDo;
                break;
            case "WEAPON":
                dbValue = dicWeapon[keyEquipped].GiaKhiRaDo;
                break;
        }
        return dbValue;
    }
    #endregion
}