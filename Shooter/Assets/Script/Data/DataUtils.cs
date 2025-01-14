﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class DataUtils
{
    public static string APP_ID = DataParam.isTest ? "ca-app-pub-3940256099942544~1458002511" : "ca-app-pub-8566745611252640~2786279701";
    public static string INTERS_ID = DataParam.isTest ? "ca-app-pub-3940256099942544/4411468910" : "ca-app-pub-8566745611252640/8524609975";
    public static string REWARDED_ID = DataParam.isTest ? "ca-app-pub-3940256099942544/1712485313" : "ca-app-pub-8566745611252640/3333074611";
    public static string BANNER_ID = DataParam.isTest ? "ca-app-pub-3940256099942544/2934735716" : "ca-app-pub-8566745611252640/2403136325";

    public const int STAR_UNLOCK_STAGE2 = 18, STAR_UNLOCK_STAGE3 = 45;
    public const int PART_UNLOCK_P2 = 20;
    public enum eLevel { Normal, Uncommon, Rare, Epic, Legendary }
    public enum eType { SHOES, BAG, GLOVES, HELMET, ARMOR, WEAPON/*, P1 */}
    public enum ITEM_SHOP_TYPE { PACKAGE, GEM, LUCKYCHEST }
    public const int TOTAL_STAGE = 3;
    public const int MAX_LEVEL_HERO = 4;
    public const int MAX_STARS = 5;
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
    public const string KEY_DAILY_REWARD = GAME_KEY + "KEY_DAILY_REWARD";

    public const string LINK_MORE_GAME = "https://play.google.com/store/apps/developer?id=Ohze+Games+Studio";
    public static string LINK_RATE_US = "market://details?id=" + Application.identifier;
    public const string P_DONATE = "com.ohzegame.ramboshooter.brothersquad.donate";
    public const string P_STARTER_PACK = "com.ohzegame.ramboshooter.brothersquad.starterpack";
    public const string P_CHEAP_PACK = "com.brothersquad.pack1";
    public const string P_BEST_CHOICE = "com.brothersquad.pack2";
    public const string P_PROFESSIONAL_PACK = "com.brothersquad.pack3";
    public const string P_25GEM_PACK = "com.ohze.rambo.25gem";
    public const string P_220GEM_PACK = "com.ohze.rambo.220gem";
    public const string P_600GEM_PACK = "com.ohze.rambo.600gem";
    public const string P_1750GEM_PACK = "com.ohze.rambo.1750gem";
    public const string P_4000GEM_PACK = "com.ohze.rambo.4000gem";
    public const string P_12500GEM_PACK = "com.ohze.rambo.12500gem";

    public static ItemWeapon itemWeapon;
    public static ItemArmor itemArmor;
    public static ItemBag itemBag;
    public static ItemGloves itemGloves;
    public static ItemHelmet itemHelmet;
    public static ItemShoes itemShoes;

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
    public static string eWeaponLevel = "Normal";
    //private static eLevel GetItemLevel(ItemData itemData)
    //{
    //    eLevel eReturn = eLevel.Normal;
    //    switch (itemData.level)
    //    {
    //        case "Epic":
    //            eReturn = eLevel.Epic;
    //            break;
    //        case "Legendary":
    //            eReturn = eLevel.Legendary;
    //            break;
    //        case "Normal":
    //            eReturn = eLevel.Normal;
    //            break;
    //        case "Rare":
    //            eReturn = eLevel.Rare;
    //            break;
    //        case "Uncommon":
    //            eReturn = eLevel.Uncommon;
    //            break;
    //        default:
    //            eReturn = eLevel.Normal;
    //            break;
    //    }
    //    return eReturn;
    //}


    public static string GetItemInfo(ItemData itemData)
    {
        string _str = "";
        string itemKey = itemData.id + "_" + itemData.level;
        int curStar = itemData.curStar < 5 ? itemData.curStar : 4;
        switch (itemData.type)
        {
            case "ARMOR":
                _str = "- Increase <color=green>" + dicArmor[itemKey].DefValue[curStar] + "%</color> def.\n -Reduce: <color=green>" + dicArmor[itemKey].SpeedTruValue[curStar] + "</color> move speed";
                break;
            case "BAG":
                int HealthRegeneration = (int)dicBag[itemKey].HealthRegenerationValue[curStar >= dicBag[itemKey].HealthRegenerationValue.Count ? dicBag[itemKey].HealthRegenerationValue.Count - 1 : curStar];
                if (HealthRegeneration <= 0)
                    _str = "- Increase <color=green>" + dicBag[itemKey].BonussoluongmauanduocValue[curStar >= dicBag[itemKey].HealthRegenerationValue.Count ? dicBag[itemKey].HealthRegenerationValue.Count - 1 : curStar] + "%</color> of first aid dropped on the map";
                else
                {
                    _str = "- Increase <color=green>" + dicBag[itemKey].BonussoluongmauanduocValue[curStar >= dicBag[itemKey].HealthRegenerationValue.Count ? dicBag[itemKey].HealthRegenerationValue.Count - 1 : curStar] + "%</color> of first aid dropped on the map\n- Heals <color=green>" + HealthRegeneration + "%</color> health in 1 second when the character's health is below 5% (maximum 50%)";
                }
                break;
            case "GLOVES":
                _str = "- Reduce <color=green>" + dicGloves[itemKey].GiamtimereloadValue[curStar] + "%</color> reload time.\n- Crit Rate: <color=green>+" + dicGloves[itemKey].tangcritrateValue[curStar] + "%</color>\n- Crit Damage: <color=green>+" + dicGloves[itemKey].TangcritdmgValue[curStar] + "%</color>";
                break;
            case "HELMET":
                _str = "- Increase <color=green>" + dicHelmet[itemKey].DefValue[curStar] + "%</color> def. \n- Bonus Exp: <color=green>" + dicHelmet[itemKey].BonusExpValue[curStar] + "%</color>";
                break;
            case "SHOES":
                _str = "- Move Speed: <color=green>+" + dicShoes[itemKey].TangSpeeDichuyenValue[curStar] + "%</color>\n- Jump Height: <color=green>+" + dicShoes[itemKey].TangDoCaoNhayValue[curStar] + "%</color>";
                break;
            case "WEAPON":
                break;
            case "P1":
                _str = "";
                break;
            default:
                _str = "";
                break;
        }
        return _str;
    }
    public static string GetItemInfo(string itemKey, string type)
    {
        string _str = "";
        int curStar = 0;
        switch (type)
        {
            case "ARMOR":
                _str = "- Increase <color=green>" + dicArmor[itemKey].DefValue[curStar] + "%</color> def.\n -Reduce: <color=green>" + dicArmor[itemKey].SpeedTruValue[curStar] + "</color> move speed";
                break;
            case "BAG":
                int HealthRegeneration = (int)dicBag[itemKey].HealthRegenerationValue[curStar >= dicBag[itemKey].HealthRegenerationValue.Count ? dicBag[itemKey].HealthRegenerationValue.Count - 1 : curStar];
                if (HealthRegeneration <= 0)
                    _str = "- Increase <color=green>" + dicBag[itemKey].BonussoluongmauanduocValue[curStar >= dicBag[itemKey].HealthRegenerationValue.Count ? dicBag[itemKey].HealthRegenerationValue.Count - 1 : curStar] + "%</color> of first aid dropped on the map";
                else
                {
                    _str = "- Increase <color=green>" + dicBag[itemKey].BonussoluongmauanduocValue[curStar >= dicBag[itemKey].HealthRegenerationValue.Count ? dicBag[itemKey].HealthRegenerationValue.Count - 1 : curStar] + "%</color> of first aid dropped on the map\n- Heals <color=green>" + HealthRegeneration + "%</color> health in 1 second when the character's health is below 5% (maximum 50%)";
                }
                break;
            case "GLOVES":
                _str = "- Reduce <color=green>" + dicGloves[itemKey].GiamtimereloadValue[curStar] + "%</color> reload time.\n- Crit Rate: <color=green>+" + dicGloves[itemKey].tangcritrateValue[curStar] + "%</color>\n- Crit Damage: <color=green>+" + dicGloves[itemKey].TangcritdmgValue[curStar] + "%</color>";
                break;
            case "HELMET":
                _str = "- Increase <color=green>" + dicHelmet[itemKey].DefValue[curStar] + "%</color> def. \n- Bonus Exp: <color=green>" + dicHelmet[itemKey].BonusExpValue[curStar] + "%</color>";
                break;
            case "SHOES":
                _str = "- Move Speed: <color=green>+" + dicShoes[itemKey].TangSpeeDichuyenValue[curStar] + "%</color>\n- Jump Height: <color=green>+" + dicShoes[itemKey].TangDoCaoNhayValue[curStar] + "%</color>";
                break;
            case "WEAPON":
                break;
            case "P1":
                _str = "";
                break;
            default:
                _str = "";
                break;
        }
        return _str;
    }

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
            var _wp = DataController.instance.allWeapon[0].weaponList[0];
            ItemData _item = new ItemData();
            _item.id = "W1";
            _item.type = eType.WEAPON.ToString();
            _item.level = eLevel.Normal.ToString();
            _item.itemName = _wp.NAME;
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
    public static void CheckEquipWeapon()
    {
        itemWeapon = new ItemWeapon();
        itemArmor = new ItemArmor();
        itemBag = new ItemBag();
        itemGloves = new ItemGloves();
        itemHelmet = new ItemHelmet();
        itemShoes = new ItemShoes();

        string _key = "";
        foreach (ItemData item in dicAllEquipment.Values)
        {
            if (item.isEquipped)
            {
                _key = item.id + "_" + item.level;
                int curStar = item.curStar < 5 ? item.curStar : 4;
                switch (item.type)
                {
                    case "WEAPON":
                        itemWeapon.weponIndex = int.Parse(item.id.Replace("W", "").Trim()) - 1;
                        itemWeapon.DmgValue = dicWeapon[_key].DmgValue[curStar >= dicWeapon[_key].AtksecValue.Count ? dicWeapon[_key].AtksecValue.Count - 1 : curStar];
                        itemWeapon.ReloadSpeedValue = dicWeapon[_key].ReloadSpeedValue[curStar >= dicWeapon[_key].AtksecValue.Count ? dicWeapon[_key].AtksecValue.Count - 1 : curStar];
                        itemWeapon.MagazineValue = dicWeapon[_key].MagazineValue[curStar >= dicWeapon[_key].AtksecValue.Count ? dicWeapon[_key].AtksecValue.Count - 1 : curStar];
                        itemWeapon.CritRateValue = dicWeapon[_key].CritRateValue[curStar >= dicWeapon[_key].AtksecValue.Count ? dicWeapon[_key].AtksecValue.Count - 1 : curStar];
                        itemWeapon.CritDmgValue = dicWeapon[_key].CritDmgValue[curStar >= dicWeapon[_key].AtksecValue.Count ? dicWeapon[_key].AtksecValue.Count - 1 : curStar];
                        itemWeapon.BulletSpeedValue = dicWeapon[_key].BulletSpeedValue[curStar >= dicWeapon[_key].AtksecValue.Count ? dicWeapon[_key].AtksecValue.Count - 1 : curStar];
                        itemWeapon.AtkRangeValue = dicWeapon[_key].AtkRangeValue[curStar >= dicWeapon[_key].AtksecValue.Count ? dicWeapon[_key].AtksecValue.Count - 1 : curStar];
                        itemWeapon.AtksecValue = dicWeapon[_key].AtksecValue[curStar >= dicWeapon[_key].AtksecValue.Count ? dicWeapon[_key].AtksecValue.Count - 1 : curStar];

                        eWeaponLevel = item.level;
                        break;
                    case "ARMOR":
                        itemArmor.armorIndex = int.Parse(item.id.Replace("A", "").Trim()) - 1;
                        itemArmor.defIncrease = dicArmor[_key].DefValue[curStar >= dicArmor[_key].SpeedTruValue.Count ? dicArmor[_key].SpeedTruValue.Count - 1 : curStar];
                        itemArmor.speedReduce = dicArmor[_key].SpeedTruValue[curStar >= dicArmor[_key].SpeedTruValue.Count ? dicArmor[_key].SpeedTruValue.Count - 1 : curStar];
                        break;
                    case "HELMET":
                        itemHelmet.helIndex = int.Parse(item.id.Replace("H", "").Trim()) - 1;
                        itemHelmet.bonusExp = dicHelmet[_key].BonusExpValue[curStar >= dicHelmet[_key].DefValue.Count ? dicHelmet[_key].DefValue.Count - 1 : curStar];
                        itemHelmet.defIncrease = dicHelmet[_key].DefValue[curStar >= dicHelmet[_key].DefValue.Count ? dicHelmet[_key].DefValue.Count - 1 : curStar];
                        break;
                    case "GLOVES":
                        itemGloves.gloveIndex = int.Parse(item.id.Replace("G", "").Trim()) - 1;
                        itemGloves.reloadTimeReduce = dicGloves[_key].GiamtimereloadValue[curStar >= dicGloves[_key].tangcritrateValue.Count ? dicGloves[_key].tangcritrateValue.Count - 1 : curStar];
                        itemGloves.critDamageIncrease = dicGloves[_key].TangcritdmgValue[curStar >= dicGloves[_key].tangcritrateValue.Count ? dicGloves[_key].tangcritrateValue.Count - 1 : curStar];
                        itemGloves.critRateIncrease = dicGloves[_key].tangcritrateValue[curStar >= dicGloves[_key].tangcritrateValue.Count ? dicGloves[_key].tangcritrateValue.Count - 1 : curStar];
                        break;
                    case "BAG":
                        itemBag.bagIndex = int.Parse(item.id.Replace("B", "").Trim()) - 1;
                        itemBag.totalAidDrop = dicBag[_key].BonussoluongmauanduocValue[curStar >= dicBag[_key].BonussoluongmauanduocValue.Count ? dicBag[_key].BonussoluongmauanduocValue.Count - 1 : curStar];
                        itemBag.HealthRegeneration = dicBag[_key].HealthRegenerationValue[curStar >= dicBag[_key].BonussoluongmauanduocValue.Count ? dicBag[_key].BonussoluongmauanduocValue.Count - 1 : curStar];
                        break;
                    case "SHOES":
                        itemShoes.shoeIndex = int.Parse(item.id.Replace("S", "").Trim()) - 1;
                        itemShoes.moveSpeedIncrease = dicShoes[_key].TangSpeeDichuyenValue[curStar >= dicShoes[_key].TangSpeeDichuyenValue.Count ? dicShoes[_key].TangSpeeDichuyenValue.Count - 1 : curStar];
                        itemShoes.jumpHeight = dicShoes[_key].TangDoCaoNhayValue[curStar >= dicShoes[_key].TangSpeeDichuyenValue.Count ? dicShoes[_key].TangSpeeDichuyenValue.Count - 1 : curStar];
                        break;
                }
            }
        }
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
    public static float GetPiceByStar(ItemData itemData, bool isEvolve)
    {
        float res = 0;
        string key = itemData.id + "_" + itemData.level;
        int curStar = (isEvolve ? 1 : /*(itemData.curStar < 5 ? itemData.curStar : 4)*/0);
        //int curStar = itemData.curStar < 5 ? itemData.curStar : 4;
        switch (itemData.type)
        {
            case "ARMOR":
                res = dicArmor[key].SoManhYeuCauValue[curStar];
                break;
            case "BAG":
                res = dicBag[key].SoManhYeuCauValue[curStar];
                break;
            case "GLOVES":
                res = dicGloves[key].SoManhYeuCauValue[curStar];
                break;
            case "HELMET":
                res = dicHelmet[key].SoManhYeuCauValue[curStar];
                break;
            case "SHOES":
                res = dicShoes[key].SoManhYeuCauValue[curStar];
                break;
            case "WEAPON":
                res = dicWeapon[key].SoManhYeuCauValue[curStar];
                break;
        }
        return res;
    }



    public static double GetDiamondPrice(ItemData itemData)
    {
        double res = 0;
        string key = itemData.id + "_" + itemData.level;
        switch (itemData.type)
        {
            case "ARMOR":
                res = dicArmor[key].GiaMua1Manh;
                break;
            case "BAG":
                res = dicBag[key].GiaMua1Manh;
                break;
            case "GLOVES":
                res = dicGloves[key].GiaMua1Manh;
                break;
            case "HELMET":
                res = dicHelmet[key].GiaMua1Manh;
                break;
            case "SHOES":
                res = dicShoes[key].GiaMua1Manh;
                break;
            case "WEAPON":
                res = dicWeapon[key].GiaMua1Manh;
                break;
        }
        return res;
    }

    public static float GetItemPrice(ItemData itemData)
    {
        float res = 0;
        string key = itemData.id + "_" + itemData.level;
        int curStar = (itemData.curStar + 1 < 5 ? itemData.curStar + 1 : 4);
        switch (itemData.type)
        {
            case "ARMOR":
                res = dicArmor[key].GiaNangCapValue[curStar >= dicArmor[key].GiaNangCapValue.Count ? dicArmor[key].GiaNangCapValue.Count - 1 : curStar];
                break;
            case "BAG":
                res = dicBag[key].GiaNangCapValue[curStar >= dicBag[key].GiaNangCapValue.Count ? curStar - 1 : curStar];
                break;
            case "GLOVES":
                res = dicGloves[key].GiaNangCapValue[curStar >= dicGloves[key].GiaNangCapValue.Count ? dicGloves[key].GiaNangCapValue.Count - 1 : curStar];
                break;
            case "HELMET":
                res = dicHelmet[key].GiaValue[curStar >= dicHelmet[key].GiaValue.Count ? dicHelmet[key].GiaValue.Count - 1 : curStar];
                break;
            case "SHOES":
                res = dicShoes[key].GiaNangCapValue[curStar >= dicShoes[key].GiaNangCapValue.Count ? dicShoes[key].GiaNangCapValue.Count - 1 : curStar];
                break;
            case "WEAPON":
                res = dicWeapon[key].GiaNangCapValue[curStar >= dicWeapon[key].GiaNangCapValue.Count ? dicWeapon[key].GiaNangCapValue.Count - 1 : curStar];
                break;
        }
        return res;
    }

    public static bool IsCanEvolve(ItemData itemData)
    {
        bool _rs = true;
        if (itemData.curStar >= 4 && itemData.level.Contains("Legendary")) _rs = false;
        return _rs;
    }
    private static void CheckItemUnlock(string id, eType itemType, string level, int pices, int curStar, bool isUnlock, bool isEquipped)
    {
        //bool result = false;
        //string key = id + "_" + level + "_" + isUnlock + "_" + isEquipped;
        //string key_ = id + "_" + level;
        //switch (itemType)
        //{
        //    case eType.ARMOR:
        //        if ((pices >= (int)dicArmor[key_].SoManhYeuCauValue[curStar]))
        //        {
        //            pices -= (int)dicArmor[key_].SoManhYeuCauValue[curStar];
        //            result = true;
        //        }
        //        break;
        //    case eType.BAG:
        //        if ((pices >= (int)dicBag[key_].SoManhYeuCauValue[curStar]))
        //        {
        //            pices -= (int)dicBag[key_].SoManhYeuCauValue[curStar];
        //            result = true;
        //        }
        //        break;
        //    case eType.GLOVES:
        //        if ((pices >= (int)dicGloves[key_].SoManhYeuCauValue[curStar]))
        //        {
        //            pices -= (int)dicGloves[key_].SoManhYeuCauValue[curStar];
        //            result = true;
        //        }
        //        break;
        //    case eType.HELMET:
        //        if ((pices >= (int)dicHelmet[key_].SoManhYeuCauValue[curStar]))
        //        {
        //            pices -= (int)dicHelmet[key_].SoManhYeuCauValue[curStar];
        //            result = true;
        //        }
        //        break;
        //    case eType.SHOES:
        //        if ((pices >= (int)dicShoes[key_].SoManhYeuCauValue[curStar]))
        //        {
        //            pices -= (int)dicShoes[key_].SoManhYeuCauValue[curStar];
        //            result = true;
        //        }
        //        break;
        //    case eType.WEAPON:
        //        if ((pices >= (int)dicWeapon[key_].SoManhYeuCauValue[curStar]))
        //        {
        //            pices -= (int)dicWeapon[key_].SoManhYeuCauValue[curStar];
        //            result = true;
        //        }
        //        break;
        //}

        //if (result)
        //{
        //    ItemData itemNew = dicAllEquipment[key];
        //    dicAllEquipment.Remove(key);
        //    string _newKey = id + "_" + level + "_" + result + "_" + isEquipped;
        //    if (!dicAllEquipment.ContainsKey(_newKey))
        //    {
        //        dicAllEquipment.Add(_newKey, itemNew);
        //        dicAllEquipment[_newKey].pices = pices;
        //        dicAllEquipment[_newKey].isUnlock = result;

        //        if (EquipmentManager.Instance != null)
        //        {
        //            EquipmentManager.Instance.RefreshInventory(dicAllEquipment[_newKey]);
        //        }
        //    }
        //    else
        //    {
        //        dicAllEquipment[_newKey].pices = pices;
        //        dicAllEquipment[_newKey].isUnlock = result;
        //        ItemData iAddNew = dicAllEquipment[_newKey];
        //    }
        //}
    }

    public static string GetItemName(eType _itemType, string _str)
    {
        string sName = "";

        int _index = int.Parse(_str.Substring(1));
        switch (_itemType)
        {
            case eType.ARMOR:
                sName = DataController.instance.allArmor[_index - 1].armorList[0].NAME;
                break;
            case eType.BAG:
                sName = DataController.instance.allBag[_index - 1].bagList[0].NAME;
                break;
            case eType.GLOVES:
                sName = DataController.instance.allGloves[_index - 1].glovesList[0].NAME;
                break;
            case eType.HELMET:
                sName = DataController.instance.allHelmet[_index - 1].helmetList[0].NAME;
                break;
            case eType.SHOES:
                sName = DataController.instance.allShoes[_index - 1].shoesList[0].NAME;
                break;
            case eType.WEAPON:
                sName = DataController.instance.allWeapon[_index - 1].weaponList[0].NAME;
                break;
        }
        return sName;
    }

    public static string GetItemName(string _itemType, string _key)
    {
        string _name = "";
        switch (_itemType)
        {
            case "ARMOR":
                _name = dicArmor[_key].NAME;
                break;
            case "BAG":
                _name = dicBag[_key].NAME;
                break;
            case "GLOVES":
                _name = dicGloves[_key].NAME;
                break;
            case "HELMET":
                _name = dicHelmet[_key].NAME;
                break;
            case "SHOES":
                _name = dicShoes[_key].NAME;
                break;
            case "WEAPON":
                _name = dicWeapon[_key].NAME;
                break;
        }
        return _name;
    }


    public static string GetItemNameByItemData(ItemData iData, eType _itemType)
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


    public static void UnlockThisItem(ItemData itemData)
    {
        string _key = itemData.id + "_" + itemData.level + "_" + itemData.isUnlock + "_" + itemData.isEquipped;
        int newPiece = dicAllEquipment[_key].pices - (int)GetPiceByStar(dicAllEquipment[_key], false);

        ItemData iData_New = new ItemData();
        iData_New.id = dicAllEquipment[_key].id;
        iData_New.type = dicAllEquipment[_key].type;
        iData_New.level = dicAllEquipment[_key].level;
        iData_New.isUnlock = true;
        iData_New.pices = 0;
        iData_New.itemName = dicAllEquipment[_key].itemName;
        iData_New.isEquipped = false;
        string _keyNew = iData_New.id + "_" + iData_New.level.ToString() + "_" + iData_New.isUnlock + "_" + iData_New.isEquipped;

        dicAllEquipment.Add(_keyNew, iData_New);
        EquipmentManager.Instance.CheckInitNewItem(dicAllEquipment[_keyNew]);

        if (iData_New.type.Equals("WEAPON"))
            DataController.instance.DoAchievement(11, 1);


        if (newPiece > 0)
        {
            dicAllEquipment[_key].pices = newPiece;
        }
        else
        {
            dicAllEquipment[_key].pices = 0;
            if (EquipmentManager.Instance != null)
            {
                EquipmentManager.Instance.RemoverThisItem(dicAllEquipment[_key]);
                dicAllEquipment.Remove(_key);
            }
        }
        SaveEquipmentData();
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
            iData.itemName = GetItemNameByItemData(iData, _itemType);
            iData.isEquipped = false;

            string _key = iData.id + "_" + iData.level.ToString() + "_" + iData.isUnlock + "_" + iData.isEquipped;
            if (dicAllEquipment.ContainsKey(_key))
            {
                int _curPiece = dicAllEquipment[_key].pices;
                if (!dicAllEquipment[_key].isUnlock)
                {
                    dicAllEquipment[_key].pices += _pices;
                    #region RAW
                    //if (dicAllEquipment[_key].pices >= (int)GetPiceByStar(dicAllEquipment[_key]))
                    //{
                    //    int newPiece = dicAllEquipment[_key].pices - (int)GetPiceByStar(dicAllEquipment[_key]);
                    //    dicAllEquipment[_key].pices = newPiece;

                    //    ItemData iData_New = new ItemData();
                    //    iData_New.id = _id;
                    //    iData_New.type = _itemType.ToString();
                    //    iData_New.level = _itemLevel.ToString();
                    //    iData_New.isUnlock = true;
                    //    iData_New.pices = 0;
                    //    iData_New.itemName = GetItemName(iData_New, _itemType);
                    //    iData_New.isEquipped = false;
                    //    string _keyNew = iData_New.id + "_" + iData_New.level.ToString() + "_" + iData_New.isUnlock + "_" + iData_New.isEquipped;
                    //    string _keyCompare = iData_New.id + "_" + iData_New.level.ToString() + "_" + iData_New.isUnlock;
                    //    if (!dicAllEquipment.ContainsKey(_keyNew))
                    //    {

                    //        string _keyDisplay = "--";
                    //        bool isEqual = true;
                    //        foreach (ItemData itData in dicAllEquipment.Values)
                    //        {
                    //            _keyDisplay = itData.id + "_" + itData.level.ToString() + "_" + itData.isUnlock;
                    //            if (_keyCompare.Equals(_keyDisplay))
                    //            {
                    //                dicAllEquipment[_key].pices = _curPiece + _pices;
                    //                break;
                    //            }
                    //            else
                    //            {
                    //                isEqual = false;
                    //            }
                    //        }
                    //        if (!isEqual)
                    //        {
                    //            dicAllEquipment.Add(_keyNew, iData_New);
                    //            if (EquipmentManager.Instance != null)
                    //            {
                    //                EquipmentManager.Instance.RefreshInventory(dicAllEquipment[_keyNew]);
                    //                if (iData_New.type.Equals("WEAPON"))
                    //                    DataController.instance.DoAchievement(11, 1);
                    //            }
                    //        }

                    //    }
                    //    else
                    //    {
                    //        dicAllEquipment[_key].pices = _curPiece + _pices;
                    //    }
                    //}
                    #endregion
                }
            }
            else
            {
                dicAllEquipment.Add(_key, iData);
                if (EquipmentManager.Instance != null)
                {
                    EquipmentManager.Instance.RefreshInventory(dicAllEquipment[_key], true);
                }
            }
            SaveEquipmentData();

            #region using for equip function in game
            iItemIngame = iData;
            _keyItemIngame = _key;
            #endregion
        }
    }
    public static void TakeItem(ItemData iData, int _piece)
    {
        string _key = iData.id + "_" + iData.level.ToString() + "_" + false + "_" + iData.isEquipped;
        if (dicAllEquipment.ContainsKey(_key))
        {
            dicAllEquipment[_key].pices += _piece;
        }
        else
        {
            ItemData iDataNew = new ItemData();
            iDataNew.id = iData.id;
            iDataNew.type = iData.type;
            iDataNew.level = iData.level;
            iDataNew.isUnlock = false;
            iDataNew.pices = _piece;
            iDataNew.itemName = iData.itemName;
            iDataNew.isEquipped = false;
            string _keyNew = iDataNew.id + "_" + iDataNew.level + "_" + false + "_" + iDataNew.isEquipped;
            if (!dicAllEquipment.ContainsKey(_keyNew))
            {
                dicAllEquipment.Add(_keyNew, iDataNew);
                if (EquipmentManager.Instance != null)
                {
                    EquipmentManager.Instance.RefreshInventory(iDataNew, false);
                }
            }
            else
            {
                dicAllEquipment[_keyNew].pices += _piece;
            }
        }

        SaveEquipmentData();
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

    private static string _keyItemIngame = "";
    private static ItemData iItemIngame;

    public static void EquipItem(ItemData itemData, bool useInGame)
    {

        if (iItemIngame != null && _keyItemIngame.Trim().Length > 0)
        {
            string _key = iItemIngame.id + "_" + iItemIngame.level.ToString() + "_" + iItemIngame.isUnlock + "_" + true;

            iItemIngame.isEquipped = true;

            string _keyHasEquipped = "";
            ItemData iHasEquip = null;
            foreach (ItemData _itemData in dicEquippedItem.Values)
            {
                if (_itemData.type.Contains(itemData.type) && _itemData.isEquipped)
                {
                    _keyHasEquipped = _itemData.id + "_" + _itemData.level + "_" + _itemData.isUnlock + "_" + _itemData.isEquipped;
                    iHasEquip = _itemData;
                    break;
                }
            }
            dicAllEquipment.Add(_key, iItemIngame);

            dicAllEquipment.Remove(_keyItemIngame);
            dicAllEquipment.Remove(_keyHasEquipped);

            if (iHasEquip != null)
            {
                dicAllEquipment.Add(iHasEquip.id + "_" + iHasEquip.level + "_" + iHasEquip.isUnlock + "_" + false, iHasEquip);
                dicAllEquipment[iHasEquip.id + "_" + iHasEquip.level + "_" + iHasEquip.isUnlock + "_" + false].isEquipped = false;
            }

            if (dicEquippedItem.ContainsKey(_keyHasEquipped))
            {
                dicEquippedItem.Remove(_keyHasEquipped);
            }


            if (!dicEquippedItem.ContainsKey(_key))
            {
                dicEquippedItem.Add(_key, iItemIngame);
            }

            SaveEquipmentData();
            SaveEquippedData();

            CheckEquipWeapon();
        }
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
    public static int modeSelected = 0;
    #region Mode Hard

    private static bool IsFirst1Star(int _mode, int _stage, int level)
    {
        bool _rs = false;
        if (_mode == 0)
        {
            _rs = lstAllStageNormal[_stage].levels[level].mission[0].isPass;
        }
        else if (_mode == 1)
        {
            _rs = lstAllStageHard[_stage].levels[level].mission[0].isPass;
        }

        return PlayerPrefs.HasKey("checkfirsttime1star_" + _mode + "_" + _stage + "_" + level + "_" + _rs);
    }
    private static bool IsFirst2Stars(int _mode, int _stage, int level)
    {
        bool _rs = false;
        if (_mode == 0)
        {
            _rs = lstAllStageNormal[_stage].levels[level].mission[1].isPass;
        }
        else if (_mode == 1)
        {
            _rs = lstAllStageHard[_stage].levels[level].mission[1].isPass;
        }

        return PlayerPrefs.HasKey("checkfirsttime2star_" + _mode + "_" + _stage + "_" + level + "_" + _rs);
    }
    private static bool IsFirst3Stars(int _mode, int _stage, int level)
    {
        bool _rs = false;
        if (_mode == 0)
        {
            _rs = lstAllStageNormal[_stage].levels[level].mission[2].isPass;
        }
        else if (_mode == 1)
        {
            _rs = lstAllStageHard[_stage].levels[level].mission[2].isPass;
        }

        return PlayerPrefs.HasKey("checkfirsttime3star_" + _mode + "_" + _stage + "_" + level + "_" + _rs);
    }

    public static bool First3Star(int _mode, int _stage, int level)
    {
        return IsFirst1Star(_mode, _stage, level) && IsFirst2Stars(_mode, _stage, level) && IsFirst3Stars(_mode, _stage, level);
    }
    public static bool First2Star(int _mode, int _stage, int level)
    {
        return (IsFirst1Star(_mode, _stage, level) && IsFirst2Stars(_mode, _stage, level) || IsFirst3Stars(_mode, _stage, level)) || (IsFirst1Star(_mode, _stage, level) || IsFirst2Stars(_mode, _stage, level) && IsFirst3Stars(_mode, _stage, level));
    }
    public static bool First1Star(int _mode, int _stage, int level)
    {
        return IsFirst1Star(_mode, _stage, level) || IsFirst2Stars(_mode, _stage, level) || IsFirst3Stars(_mode, _stage, level);
    }


    public static void InitFirstTimeStar(int _index, int _mode, int _stage, int level, bool star)
    {
        PlayerPrefs.SetInt("checkfirsttime" + _index + "star_" + _mode + "_" + _stage + "_" + level + "_" + star, 1);
        PlayerPrefs.Save();
    }
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
    public static void UnlockHardMode(int _stageIndex)
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
        if (lstAllStageHard.Count > 0)
        {
            lstAllStageHard[_stageIndex].stageHasUnlock = true;
            jHardSave = JsonMapper.ToJson(lstAllStageHard);
        }
        SaveStageHard(jHardSave);
        #endregion
    }
    private static void StageHardIncrease(int _curStage)
    {
        int curStage = /*GetStageIndex()*/PlayerPrefs.GetInt(KEY_GAME_STAGE_INDEX_HARD, 0) + 1;
        if (_curStage >= curStage || /*GetStageIndex()*/PlayerPrefs.GetInt(KEY_GAME_STAGE_INDEX_HARD, 0) == 0)
            PlayerPrefs.SetInt(KEY_GAME_STAGE_INDEX_HARD, curStage);
    }
    #endregion


    public static int GetStageIndex()
    {
        return PlayerPrefs.GetInt(KEY_GAME_STAGE_INDEX, 0);
    }
    private static void StageIncrease(int _curStage)
    {
        int curStage = GetStageIndex() + 1;
        if (_curStage >= curStage || GetStageIndex() == 0)
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
    }

    private static int totalStar = 0;
    public static int CalculateStageStar(List<DataStage> dataStages)
    {
        totalStar = 0;
        foreach (DataStage dataStage in dataStages)
        {
            if (dataStage.stageHasUnlock)
            {
                foreach (MapLevel level in dataStage.levels)
                {
                    if (level.hasComplete)
                    {
                        if (level.mission[0].isPass && level.mission[1].isPass && level.mission[2].isPass)
                        {
                            totalStar += 3;
                        }
                        else if ((level.mission[0].isPass && level.mission[1].isPass) || (level.mission[0].isPass && level.mission[2].isPass) || (level.mission[2].isPass && level.mission[1].isPass))
                        {
                            totalStar += 2;
                        }
                        else
                        {
                            totalStar += 1;
                        }
                    }
                }
            }
        }
        return totalStar;
    }
    public static int CalculateStarByStage(DataStage dataStage)
    {
        int _count = 0;
        if (dataStage.stageHasUnlock)
        {
            foreach (MapLevel level in dataStage.levels)
            {
                if (level.hasComplete)
                {
                    if (level.mission[0].isPass && level.mission[1].isPass && level.mission[2].isPass)
                    {
                        _count += 3;
                    }
                    else if ((level.mission[0].isPass && level.mission[1].isPass) || (level.mission[0].isPass && level.mission[2].isPass) || (level.mission[2].isPass && level.mission[1].isPass))
                    {
                        _count += 2;
                    }
                    else
                    {
                        _count += 1;
                    }
                }
            }
        }
        return _count;
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
            if (lstAllStageNormal[stage].levelUnlock < mapIndex)
                lstAllStageNormal[stage].levelUnlock = mapIndex;

            if (mapIndex == 7)
            {
                lstAllStageNormal[(stage + 1 >= lstAllStageNormal.Count ? stage : stage + 1)].stageHasUnlock = true;
                UnlockHardMode(stage);

                if (stage == 0 && CalculateStageStar(lstAllStageNormal) >= STAR_UNLOCK_STAGE2)
                {
                    StageIncrease(stage + 1);
                }
                else if (stage == 1 && CalculateStageStar(lstAllStageNormal) >= STAR_UNLOCK_STAGE3)
                {
                    StageIncrease(stage + 1);
                }
                else
                {
                    StageIncrease(stage + 1);
                }
            }

            string jSave = JsonMapper.ToJson(lstAllStageNormal);
            SaveStage(jSave);
        }
        else if (modeSelected == 1)
        {

            lstAllStageHard[stage].levels[mapIndex].hasComplete = true;

            if (lstAllStageHard[stage].levelUnlock < mapIndex)
                lstAllStageHard[stage].levelUnlock = mapIndex;

            if (mapIndex == 7)
            {
                lstAllStageHard[(stage + 1 >= lstAllStageHard.Count ? stage : stage + 1)].stageHasUnlock = true;

                if (stage == 0 && CalculateStageStar(lstAllStageHard) >= STAR_UNLOCK_STAGE2)
                    StageHardIncrease(stage + 1);
                else if (stage == 1 && CalculateStageStar(lstAllStageHard) >= STAR_UNLOCK_STAGE3)
                    StageHardIncrease(stage + 1);
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
            if (!lstAllStageNormal[stage].levels[mapIndex].mission[0].isPass)
            {
                lstAllStageNormal[stage].levels[mapIndex].mission[0].isPass = true;
                InitFirstTimeStar(1, modeSelected, stage, mapIndex, true);
            }
            if (!lstAllStageNormal[stage].levels[mapIndex].mission[1].isPass)
            {
                lstAllStageNormal[stage].levels[mapIndex].mission[1].isPass = miss1;
                InitFirstTimeStar(2, modeSelected, stage, mapIndex, true);
            }
            if (!lstAllStageNormal[stage].levels[mapIndex].mission[2].isPass)
            {
                lstAllStageNormal[stage].levels[mapIndex].mission[2].isPass = miss2;
                InitFirstTimeStar(3, modeSelected, stage, mapIndex, true);
            }
            string jSave = JsonMapper.ToJson(lstAllStageNormal);
            SaveStage(jSave);
        }
        else if (modeSelected == 1)
        {
            if (!lstAllStageHard[stage].levels[mapIndex].mission[0].isPass)
            {
                lstAllStageHard[stage].levels[mapIndex].mission[0].isPass = true;
                InitFirstTimeStar(1, modeSelected, stage, mapIndex, true);
            }
            if (!lstAllStageHard[stage].levels[mapIndex].mission[1].isPass)
            {
                lstAllStageHard[stage].levels[mapIndex].mission[1].isPass = miss1;
                InitFirstTimeStar(2, modeSelected, stage, mapIndex, true);
            }
            if (!lstAllStageHard[stage].levels[mapIndex].mission[2].isPass)
            {
                lstAllStageHard[stage].levels[mapIndex].mission[2].isPass = miss2;
                InitFirstTimeStar(3, modeSelected, stage, mapIndex, true);
            }
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
            playerInfo.name = "THOMAS";
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
            //for (int i = 0; i < jData.Count; i++)
            //{
            //    PlayerDataInfo jPlayerInfo = JsonMapper.ToObject<PlayerDataInfo>(jData[i].ToJson());
            //    if (!lstAllPlayerHeroes.Contains(jPlayerInfo))
            //        lstAllPlayerHeroes.Add(jPlayerInfo);
            //}

            PlayerDataInfo jPlayerInfo = JsonMapper.ToObject<PlayerDataInfo>(jData[0].ToJson());
            if (!lstAllPlayerHeroes.Contains(jPlayerInfo))
                lstAllPlayerHeroes.Add(jPlayerInfo);
        }

        dicAllHero = new Dictionary<string, HeroDataInfo>();
        if (!PlayerPrefs.HasKey(KEY_ALL_HERO_DATA) || string.IsNullOrEmpty(GetAllHeroData()))
        {
            heroInfo = new HeroDataInfo();
            heroInfo.id = "P1";
            heroInfo.name = "THOMAS";
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

            if (!dicAllHero.ContainsKey("P2"))
            {
                HeroDataInfo hero2 = new HeroDataInfo();
                hero2.id = "P2";
                hero2.name = "JESSIE";
                hero2.level = 0;
                hero2.exp = 0;
                hero2.hp = GetHeroHPByID("P2");
                hero2.curStars = 1;
                hero2.pices = 0;
                hero2.isUnlock = false;
                hero2.isEquipped = false;

                if (!dicAllHero.ContainsKey(hero2.id))
                {
                    dicAllHero.Add(hero2.id, hero2);
                }
            }
        }
        else
        {
            string sHeroData = GetAllHeroData();
            JsonData jHeroData = JsonMapper.ToObject(sHeroData);
            for (int i = 0; i < jHeroData.Count; i++)
            {

                HeroDataInfo hdInfo = JsonMapper.ToObject<HeroDataInfo>(jHeroData[i].ToJson());
                if (!dicAllHero.ContainsKey(hdInfo.id))
                {
                    dicAllHero.Add(hdInfo.id, hdInfo);
                }
            }


            if (!dicAllHero.ContainsKey("P2"))
            {
                HeroDataInfo hero2 = new HeroDataInfo();
                hero2.id = "P2";
                hero2.name = "JESSIE";
                hero2.level = 0;
                hero2.exp = 0;
                hero2.hp = GetHeroHPByID("P2");
                hero2.curStars = 1;
                hero2.pices = 0;
                hero2.isUnlock = false;
                hero2.isEquipped = false;

                if (!dicAllHero.ContainsKey(hero2.id))
                {
                    dicAllHero.Add(hero2.id, hero2);
                }
            }
        }


        playerInfo = lstAllPlayerHeroes[/*HeroIndex()*/0];

        //heroInfo = DataHero();
        heroInfo = dicAllHero["P" + (HeroIndex() + 1)];

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
        lstAllPlayerHeroes[/*HeroIndex()*/0].coins = newCoin;
        lstAllPlayerHeroes[/*HeroIndex()*/0].gems = newGem;


        //SavePlayerData();
        if (MainMenuController.Instance != null)
        {
            MainMenuController.Instance.UpdateCoinAndGem();
        }
    }
    public static void AddCoinAndGame(int coinAdded, int gemAdded)
    {
        lstAllPlayerHeroes[/*HeroIndex()*/0].coins += coinAdded;
        lstAllPlayerHeroes[/*HeroIndex()*/0].gems += gemAdded;

        //SavePlayerData();
        if (MainMenuController.Instance != null)
        {
            MainMenuController.Instance.UpdateCoinAndGem();
        }
        if (coinAdded > 0)
            DataController.instance.DoDailyQuest(6, coinAdded);
    }
    public static void AddHPPack(int total_)
    {
        lstAllPlayerHeroes[/*HeroIndex()*/0].healthPack += total_;
        //SavePlayerData();
    }
    public static int HPPack()
    {
        int _res = 0;
        if (lstAllPlayerHeroes == null) _res = 0;
        else _res = lstAllPlayerHeroes[/*HeroIndex()*/0].healthPack;
        return _res;
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
            dicAllHero.Add(hID, heroInfo);
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
        for (int i = 0; i < DataController.instance.playerData.Count; i++)
        {
            for (int j = 0; j < DataController.instance.playerData[i].playerData.Count; j++)
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

    #region Daily reward
    public static bool IsClaimReward()
    {
        string _key = System.DateTime.Now.Day + "_" + System.DateTime.Now.Month;
        return _key.Equals(SReward());
    }
    public static string SReward()
    {
        return PlayerPrefs.GetString(KEY_DAILY_REWARD, /*System.DateTime.Now.ToString()*/"");
    }
    public static void HasClaimReward()
    {
        string _key = System.DateTime.Now.Day + "_" + System.DateTime.Now.Month;
        PlayerPrefs.SetString(KEY_DAILY_REWARD, _key);
        PlayerPrefs.Save();
    }
    #endregion

    #region Other
    public static string GetRealFloat(float _f)
    {
        //return (float)System.Math.Round(_f);
        return _f.ToString("0.##");
    }
    public static string DisplayRichText(double dFrom, double dTo)
    {
        return "<color=green>" + dTo + "</color>";
    }
    public static Dictionary<string, Sprite> dicSpriteData;
    public static void InitSpriteData(ItemSpriteData allSpriteData)
    {
        dicSpriteData = new Dictionary<string, Sprite>();
        foreach (ItemSprite item in allSpriteData.spriteDatas)
        {
            dicSpriteData.Add(item.itemName, item.sprItem);
        }
    }
    public static Sprite GetSpriteByName(string name, ItemSpriteData allSpriteData)
    {
        Sprite _spr = null;
        string[] strSP = name.Split('-');
        if (name.Contains("M-"))
        {
            _spr = dicSpriteData[name];
        }
        else
        {
            _spr = dicSpriteData[strSP[strSP.Length - 1]];
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

    public static double GetSellPrice(ItemData itemData)
    {
        string realKey = itemData.id + "_" + itemData.level.ToString() + "_" + itemData.isUnlock + "_" + itemData.isEquipped;
        string itemKey = itemData.id + "_" + itemData.level;
        float dbValue = 0;
        double dSell = 0;
        double dPiceRequire = 0;
        double dQuantity = dicAllEquipment[realKey].quantity;
        double dPices = dicAllEquipment[realKey].pices;
        int curStar = itemData.curStar < 5 ? itemData.curStar : 4;
        switch (itemData.type)
        {
            case "ARMOR":
                dSell = dicArmor[itemKey].GiaKhiRaDo;
                dPiceRequire = dicArmor[itemKey].SoManhYeuCauValue[curStar];
                break;
            case "BAG":
                dSell = dicBag[itemKey].GiaKhiRaDo;
                dPiceRequire = dicBag[itemKey].SoManhYeuCauValue[curStar];
                break;
            case "GLOVES":
                dSell = dicGloves[itemKey].GiaKhiRaDo;
                dPiceRequire = dicGloves[itemKey].SoManhYeuCauValue[curStar];
                break;
            case "HELMET":
                dSell = dicHelmet[itemKey].GiaKhiRaDo;
                dPiceRequire = dicHelmet[itemKey].SoManhYeuCauValue[curStar];
                break;
            case "SHOES":
                dSell = dicShoes[itemKey].GiaKhiRaDo;
                dPiceRequire = dicShoes[itemKey].SoManhYeuCauValue[curStar];
                break;
            case "WEAPON":
                dSell = dicWeapon[itemKey].GiaKhiRaDo;
                dPiceRequire = dicWeapon[itemKey].SoManhYeuCauValue[curStar];
                break;
        }
        if (itemData.isUnlock)
        {
            dbValue = (float)(dSell * (dPiceRequire > 0 ? dPiceRequire : 1) * (dQuantity > 0 ? dQuantity + 1 : 1) * (dPices > 0 ? dPices : 1));
        }
        else
        {
            dbValue = (float)(dSell * (dPices > 0 ? dPices : 1));
        }
        return dbValue;
    }

    /// Normal : gray
    /// Uncommon : green
    /// Rare : blue
    /// Epic : pink
    /// Legendary : orange
    public static string GetColorByItem(ItemData itemData)
    {
        string _sResult = "#8ea5b1";
        switch (itemData.level)
        {
            case "Normal":
                _sResult = "#8ea5b1";
                break;
            case "Uncommon":
                _sResult = "#81da2c";
                break;
            case "Rare":
                _sResult = "#61c3fa";
                break;
            case "Epic":
                _sResult = "#b48eff"; //"pink";
                break;
            case "Legendary":
                _sResult = "#f7c300"; //"orange";
                break;
            default:
                _sResult = "#8ea5b1"; //"grey";
                break;
        }
        return _sResult;
    }


    public static string GetColorByItemData(float f1, float f2)
    {
        string cl = "white";
        cl = f1 > f2 ? "green" : (f1 < f2 ? "red" : "white");

        return cl;
    }
    public static string GetNextLevelString(string eLevel) {
        string _str = "";
        //Normal, Uncommon, Rare, Epic, Legendary
        switch (eLevel) {
            case "Normal":
                _str = "Uncommon";
                break;
            case "Uncommon":
                _str = "Rare";
                break;
            case "Rare":
                _str = "Epic";
                break;
            case "Epic":
                _str = "Legendary";
                break;
            case "Legendary":
                _str = "Legendary";
                break;
            default:
                _str = "Normal";
                break;
        }
        return _str;
    }
    #endregion

}