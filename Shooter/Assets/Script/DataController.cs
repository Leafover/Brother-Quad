using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region BlackMarket
[System.Serializable]
public class BlackMarketData
{
    public string ID, NAME, Level;
    public double GiaBanCoin, GiaGem1Manh;
    public int countnumber = 5;
}
#endregion


#region primeAccount
[System.Serializable]
public class PrimeAccountClass
{
    public bool takegem = false, takecoin = false, isVIP = false;
    public int countDay = 30;
}
#endregion

#region AllDailyQuest
[System.Serializable]
public class DailyQuest
{
    public string MissionContent, Type;
    public int RewardsType, SoLuongRewards, EXP, Soluong;
    public int currentNumber = 0;
    public bool isDone = false, isPass = false, isActive = false;
}
#endregion

#region AllAchievement
[System.Serializable]
public class Achievement
{
    public string Name, Soluong, LoaiThuong, SoLuongreward, Exp, NoiDung;
    public List<int> maxNumber = new List<int>(), typeReward = new List<int>(), maxNumberReward = new List<int>(), expReward = new List<int>();
    public bool displayContentHasChange;
}
public class SaveAchievement
{
    public int currentNumber = 0;
    public bool isPass = false, isDone = false;
    public int currentLevel = 1;
}
#endregion

#region AllTileVatPham
[System.Serializable]
public class TileVatPhamList
{
    public string ID;
    public int Level, Stage;
    public double Normal, Uncommon, Rare, Epic, Legendary, TotalNumber;
}
[System.Serializable]
public class AllTileVatPham
{
    public List<TileVatPhamList> tilevatphamList = new List<TileVatPhamList>();
}
#endregion


#region AllShoes
[System.Serializable]
public class ShoesList
{
    public string ID, NAME, Level, TangSpeeDichuyen, TangDoCaoNhay, GiaNangCap, SoManhYeuCau;
    public double GiaMua1Manh, GiaKhiRaDo, Gia;
    public List<float> TangSpeeDichuyenValue = new List<float>(), TangDoCaoNhayValue = new List<float>(), GiaNangCapValue = new List<float>(), SoManhYeuCauValue = new List<float>();
}
[System.Serializable]
public class AllShoes
{
    public List<ShoesList> shoesList = new List<ShoesList>();
}
#endregion


#region AllBag
[System.Serializable]
public class BagList
{
    public string ID, NAME, Level, Bonussoluongmauanduoc, HealthRegeneration, GiaNangCap, SoManhYeuCau;
    public double GiaMua1Manh, GiaKhiRaDo, Gia;
    public List<float> BonussoluongmauanduocValue = new List<float>(), HealthRegenerationValue = new List<float>(), GiaNangCapValue = new List<float>(), SoManhYeuCauValue = new List<float>();
}
[System.Serializable]
public class AllBag
{
    public List<BagList> bagList = new List<BagList>();
}
#endregion


#region AllGloves
[System.Serializable]
public class GlovesList
{
    public string ID, NAME, Level, Giamtimereload, tangcritrate, Tangcritdmg, GiaNangCap, SoManhYeuCau;
    public double GiaMua1Manh, GiaKhiRaDo, Gia;
    public List<float> GiamtimereloadValue = new List<float>(), tangcritrateValue = new List<float>(), TangcritdmgValue = new List<float>(), GiaNangCapValue = new List<float>(), SoManhYeuCauValue = new List<float>();
}
[System.Serializable]
public class AllGloves
{
    public List<GlovesList> glovesList = new List<GlovesList>();
}
#endregion

#region AllHelmet
[System.Serializable]
public class HelmetList
{
    public string ID, NAME, Level, Def, BonusExp, Gia, SoManhYeuCau;
    public double GiaMua1Manh, GiaKhiRaDo;
    public List<float> DefValue = new List<float>(), BonusExpValue = new List<float>(), GiaValue = new List<float>(), SoManhYeuCauValue = new List<float>();
}
[System.Serializable]
public class AllHelmet
{
    public List<HelmetList> helmetList = new List<HelmetList>();
}
#endregion


#region AllArmor
[System.Serializable]
public class ArmorList
{
    public string ID, NAME, Level, Def, SpeedTru, SoManhYeuCau, GiaNangCap;
    public double GiaMua1Manh, GiaKhiRaDo;
    public List<float> DefValue = new List<float>(), SpeedTruValue = new List<float>(), SoManhYeuCauValue = new List<float>(), GiaNangCapValue = new List<float>();
}
[System.Serializable]
public class AllArmor
{
    public List<ArmorList> armorList = new List<ArmorList>();
}
#endregion


#region AllWeapon
[System.Serializable]
public class WeaponList
{
    public string ID, NAME, Level, Dmg, BulletSpeed, ReloadSpeed, Magazine, AtkRange, CritRate, CritDmg, SoManhYeuCau, Atksec, GiaNangCap;
    public double GiaMua1Manh, GiaKhiRaDo, Gia;
    public List<float> DmgValue = new List<float>(), AtksecValue = new List<float>(), BulletSpeedValue = new List<float>(), ReloadSpeedValue = new List<float>(), MagazineValue = new List<float>(), AtkRangeValue = new List<float>(), CritRateValue = new List<float>(), CritDmgValue = new List<float>(), SoManhYeuCauValue = new List<float>(), GiaNangCapValue = new List<float>();
}
[System.Serializable]
public class AllWeapon
{
    public List<WeaponList> weaponList = new List<WeaponList>();
}
#endregion

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
    public double hp, DmgGrenade, MoveSpeed, SoManhYeuCau, Giamua1manh, total;
}
[System.Serializable]
public class AllPlayerData
{
    public List<PlayerData> playerData = new List<PlayerData>();
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
    public List<BlackMarketData> blackMarketData = new List<BlackMarketData>();
    public static List<BlackMarketData> blackMarketSave = new List<BlackMarketData>();
    public static PrimeAccountClass primeAccout = new PrimeAccountClass();
    public List<DailyQuest> allDailyQuest = new List<DailyQuest>();
    public static List<DailyQuest> allSaveDailyQuest = new List<DailyQuest>();
    public List<Achievement> allAchievement = new List<Achievement>();
    public static List<SaveAchievement> saveAllAchievement = new List<SaveAchievement>();
    public List<AllTileVatPham> allTileVatPham = new List<AllTileVatPham>();
    public List<AllShoes> allShoes = new List<AllShoes>();
    public List<AllBag> allBag = new List<AllBag>();
    public List<AllGloves> allGloves = new List<AllGloves>();
    public List<AllHelmet> allHelmet = new List<AllHelmet>();
    public List<AllArmor> allArmor = new List<AllArmor>();
    public List<AllWeapon> allWeapon = new List<AllWeapon>();
    public List<AllDataEnemy> allDataEnemy = new List<AllDataEnemy>();
    public List<AllMission> allMission = new List<AllMission>();
    public List<AllPlayerData> playerData = new List<AllPlayerData>();
    public static DataController instance;
    public string[] nameDataText;
    public string[] nameDataMissionText;
    public string nameDataPlayerText, nameDataWeapon, nameDataArmor, nameDataHelmet, nameDataGloves, nameDataBag, nameDataShoes, nameDataTiLeVatPham, nameAchievementData, nameDailyQuestData,nameBlackMarketData;
    private void Awake()
    {
        instance = this;
        // gameObject.SetActive(false);
    }
    static bool loadData = false;
    private void Start()
    {
        if (loadData)
            return;
      //  Debug.unityLogger.logEnabled = false;
        LoadData();
        DataUtils.FillEquipmentData();
        loadData = true;
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
            LoadBlackMarketData(nameBlackMarketData);
            LoadAchievement(nameAchievementData);
            LoadDailyQuest(nameDailyQuestData);

            #region List need clear
            weaponList.Clear();
            armorList.Clear();
            helmetList.Clear();
            glovesList.Clear();
            bagList.Clear();
            shoesList.Clear();
            tilevatphamList.Clear();
            LoadWeapon(nameDataWeapon);
            LoadArmor(nameDataArmor);
            LoadHelmet(nameDataHelmet);
            LoadGloves(nameDataGloves);
            LoadBag(nameDataBag);
            LoadShoes(nameDataShoes);
            LoadTiLeVatPham(nameDataTiLeVatPham);
            #endregion

            loaddatabegin = true;
        }
    }
    TextAsset _ta;
    JsonData jData;
    public void LoadBlackMarketData (string path)
    {
        if (blackMarketData.Count == 177)
            return;

        _ta = Resources.Load<TextAsset>("JsonData/" + path);
        jData = JsonMapper.ToObject(_ta.text);

        for (int i = 0; i < jData.Count; i++)
        {
            BlackMarketData _blackmarketData = JsonMapper.ToObject<BlackMarketData>(jData[i].ToJson());
            blackMarketData.Add(_blackmarketData);
        }
    }
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
        if (playerData[0].playerData.Count == 5)
            return;
        _ta = Resources.Load<TextAsset>("JsonData/" + path);
        jData = JsonMapper.ToObject(_ta.text);
        for (int i = 0; i < jData.Count; i++)
        {
            PlayerData _playerDate = JsonMapper.ToObject<PlayerData>(jData[i].ToJson());
            switch (_playerDate.ID)
            {
                case "P1":
                    playerData[0].playerData.Add(_playerDate);
                    break;
                case "P2":
                    playerData[1].playerData.Add(_playerDate);
                    break;
            }

        }
    }
    public void LoadAchievement(string path)
    {
        if (allAchievement.Count == 13)
            return;
        _ta = Resources.Load<TextAsset>("JsonData/" + path);
        jData = JsonMapper.ToObject(_ta.text);
        for (int i = 0; i < jData.Count; i++)
        {
            Achievement _achievementDate = JsonMapper.ToObject<Achievement>(jData[i].ToJson());
            allAchievement.Add(_achievementDate);
        }
        string[] maxNumberStr;
        for (int i = 0; i < allAchievement.Count; i++)
        {
            maxNumberStr = allAchievement[i].Soluong.Split(',');
            for (int j = 0; j < maxNumberStr.Length; j++)
            {
                if (!string.IsNullOrEmpty(maxNumberStr[j]))
                {
                    allAchievement[i].maxNumber.Add(int.Parse(maxNumberStr[j]));
                }
            }


            maxNumberStr = allAchievement[i].LoaiThuong.Split(',');
            for (int j = 0; j < maxNumberStr.Length; j++)
            {
                if (!string.IsNullOrEmpty(maxNumberStr[j]))
                {
                    allAchievement[i].typeReward.Add(int.Parse(maxNumberStr[j]));
                }
            }

            maxNumberStr = allAchievement[i].SoLuongreward.Split(',');
            for (int j = 0; j < maxNumberStr.Length; j++)
            {
                if (!string.IsNullOrEmpty(maxNumberStr[j]))
                {
                    allAchievement[i].maxNumberReward.Add(int.Parse(maxNumberStr[j]));
                }
            }

            maxNumberStr = allAchievement[i].Exp.Split(',');
            for (int j = 0; j < maxNumberStr.Length; j++)
            {
                if (!string.IsNullOrEmpty(maxNumberStr[j]))
                {
                    allAchievement[i].expReward.Add(int.Parse(maxNumberStr[j]));
                }
            }

        }
    }
    public void LoadDailyQuest(string path)
    {
        if (allDailyQuest.Count == 11)
            return;

        _ta = Resources.Load<TextAsset>("JsonData/" + path);

        jData = JsonMapper.ToObject(_ta.text);

        for (int i = 0; i < jData.Count; i++)
        {
            DailyQuest _dailyQuestData = JsonMapper.ToObject<DailyQuest>(jData[i].ToJson());
            allDailyQuest.Add(_dailyQuestData);
        }
    }
    public static List<int> saveIndexQuest = new List<int>();

    public void AddNewQuest()
    {
        int randomIndex;
        if (!primeAccout.isVIP)
        {
            while (saveIndexQuest.Count < 3)
            {
                randomIndex = Random.Range(0, allSaveDailyQuest.Count - 5);
                if (!saveIndexQuest.Contains(randomIndex))
                    saveIndexQuest.Add(randomIndex);
            }
        }
        else
        {
            while (saveIndexQuest.Count < 3)
            {
                randomIndex = Random.Range(0, allSaveDailyQuest.Count - 5);
                if (!saveIndexQuest.Contains(randomIndex))
                    saveIndexQuest.Add(randomIndex);
            }

            while (saveIndexQuest.Count < 5)
            {
                randomIndex = Random.Range(allSaveDailyQuest.Count - 5, allSaveDailyQuest.Count);
                if (!saveIndexQuest.Contains(randomIndex))
                    saveIndexQuest.Add(randomIndex);
            }
        }

        for (int i = 0; i < saveIndexQuest.Count; i++)
        {
            allSaveDailyQuest[saveIndexQuest[i]].isActive = true;
        }
        if (saveIndexQuest.Contains(10))
        {
            if (saveIndexQuest.IndexOf(10) != saveIndexQuest.Count - 1)
            {
                int indexTemp = saveIndexQuest.IndexOf(10);
                int int1 = saveIndexQuest[indexTemp];
                int int2 = saveIndexQuest[saveIndexQuest.Count - 1];

                saveIndexQuest[saveIndexQuest.Count - 1] = int1;
                saveIndexQuest[indexTemp] = int2;
            }
        }
    }
    BlackMarketData _tempBlackMarket;
    int randomBlackMarket;
    public void AddNewBlackMarket()
    {
        blackMarketSave.Clear();
        while(blackMarketSave.Count < 10)
        {
            randomBlackMarket = Random.Range(0, blackMarketData.Count);
            _tempBlackMarket = blackMarketData[randomBlackMarket];
            if (!blackMarketSave.Contains(_tempBlackMarket));
            {
              //  Debug.LogError(_tempBlackMarket.ID + ":" + _tempBlackMarket.Level + ":" + _tempBlackMarket.NAME + ":" + _tempBlackMarket.GiaBanCoin);
                blackMarketSave.Add(_tempBlackMarket);
            }
        }

        //for(int i = 0; i < blackMarketSave.Count; i++)
        //{
        //   // Debug.LogError(blackMarketSave[i].ID + ":" + blackMarketSave[i].Level + ":" + blackMarketSave[i].NAME + ":" + blackMarketSave[i].GiaBanCoin);
        //}
    }
    public void LoadDailyQuest()
    {
        saveIndexQuest.Clear();
        allSaveDailyQuest.Clear();
        allSaveDailyQuest.AddRange(allDailyQuest);
        if (!PlayerPrefs.HasKey(DataParam.OLDDATETIME))
        {
            PlayerPrefs.SetString(DataParam.OLDDATETIME, System.DateTime.Now.ToString());
            AddNewQuest();
            AddNewBlackMarket();
            DataParam.countResetBlackMarket = 0;
            // Debug.LogError("load data lần đầu tiên");
        }
        DataParam.oldDateTime = System.Convert.ToDateTime(PlayerPrefs.GetString(DataParam.OLDDATETIME));
        if ((System.DateTime.Now - DataParam.oldDateTime).TotalSeconds < 86400)
        {
            strSaveIndexQuest = PlayerPrefs.GetString(DataParam.SAVEINDEXQUEST);

            string[] slitSaveIndexQuest = strSaveIndexQuest.Split('@');
            for (int i = 0; i < slitSaveIndexQuest.Length; i++)
            {
                if (!string.IsNullOrEmpty(slitSaveIndexQuest[i]))
                {
                    saveIndexQuest.Add(int.Parse(slitSaveIndexQuest[i]));
                }
            }

            strAllDailyQuest = PlayerPrefs.GetString(DataParam.ALLDAILYQUEST);

            if (string.IsNullOrEmpty(strAllDailyQuest))
                return;

             jData = JsonMapper.ToObject(strAllDailyQuest);
            for (int i = 0; i < jData.Count; i++)
            {
                if (jData[i] != null)
                {
                    allSaveDailyQuest[i].currentNumber = int.Parse(jData[i]["currentNumber"].ToString());
                    allSaveDailyQuest[i].isPass = bool.Parse(jData[i]["isPass"].ToString());
                    allSaveDailyQuest[i].isDone = bool.Parse(jData[i]["isDone"].ToString());
                    allSaveDailyQuest[i].isActive = bool.Parse(jData[i]["isActive"].ToString());
                }
            }

            LoadBlackMarket();

            return;
        }
        DataParam.oldDateTime = System.DateTime.Now;
        PlayerPrefs.SetString(DataParam.OLDDATETIME, DataParam.oldDateTime.ToString());
        AddNewQuest();
        AddNewBlackMarket();
        DataParam.countResetBlackMarket = 0;
        // Debug.LogError("load data mới sau 1 ngày");
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

    List<WeaponList> weaponList = new List<WeaponList>();
    public void LoadWeapon(string path)
    {
        if (weaponList.Count == 30)
            return;
        _ta = Resources.Load<TextAsset>("JsonData/" + path);

        jData = JsonMapper.ToObject(_ta.text);
        for (int i = 0; i < jData.Count; i++)
        {
            WeaponList _weaponList = JsonMapper.ToObject<WeaponList>(jData[i].ToJson());

            string[] AtkRange = _weaponList.AtkRange.Split('@');
            string[] Dmg = _weaponList.Dmg.Split('@');
            string[] BulletSpeed = _weaponList.BulletSpeed.Split('@');
            string[] ReloadSpeed = _weaponList.ReloadSpeed.Split('@');
            string[] Magazine = _weaponList.Magazine.Split('@');
            string[] Atksec = _weaponList.Atksec.Split('@');
            string[] CritRate = _weaponList.CritRate.Split('@');
            string[] CritDmg = _weaponList.CritDmg.Split('@');
            string[] SoManhYeuCau = _weaponList.SoManhYeuCau.Split('@');
            string[] GiaNangCap = _weaponList.GiaNangCap.Split('@');


            for (int j = 0; j < AtkRange.Length; j++)
            {
                if (!string.IsNullOrEmpty(AtkRange[j]))
                    _weaponList.AtkRangeValue.Add(float.Parse(AtkRange[j]));
                if (!string.IsNullOrEmpty(Dmg[j]))
                    _weaponList.DmgValue.Add(float.Parse(Dmg[j]));
                if (!string.IsNullOrEmpty(BulletSpeed[j]))
                    _weaponList.BulletSpeedValue.Add(float.Parse(BulletSpeed[j]));
                if (!string.IsNullOrEmpty(ReloadSpeed[j]))
                    _weaponList.ReloadSpeedValue.Add(float.Parse(ReloadSpeed[j]));
                if (!string.IsNullOrEmpty(Magazine[j]))
                    _weaponList.MagazineValue.Add(float.Parse(Magazine[j]));
                if (!string.IsNullOrEmpty(Atksec[j]))
                    _weaponList.AtksecValue.Add(float.Parse(Atksec[j]));
                if (!string.IsNullOrEmpty(CritRate[j]))
                    _weaponList.CritRateValue.Add(float.Parse(CritRate[j]));
                if (!string.IsNullOrEmpty(CritDmg[j]))
                    _weaponList.CritDmgValue.Add(float.Parse(CritDmg[j]));
                if (!string.IsNullOrEmpty(SoManhYeuCau[j]))
                    _weaponList.SoManhYeuCauValue.Add(float.Parse(SoManhYeuCau[j]));
                if (!string.IsNullOrEmpty(GiaNangCap[j]))
                    _weaponList.GiaNangCapValue.Add(float.Parse(GiaNangCap[j]));
            }
            weaponList.Add(_weaponList);
        }
        for (int i = 0; i < weaponList.Count; i++)
        {
            switch (weaponList[i].ID)
            {
                case "W1":
                    if (allWeapon[0].weaponList.Count == 5)
                        return;
                    allWeapon[0].weaponList.Add(weaponList[i]);
                    break;
                case "W2":
                    if (allWeapon[1].weaponList.Count == 5)
                        return;
                    allWeapon[1].weaponList.Add(weaponList[i]);
                    break;
                case "W3":
                    if (allWeapon[2].weaponList.Count == 5)
                        return;
                    allWeapon[2].weaponList.Add(weaponList[i]);
                    break;
                case "W4":
                    if (allWeapon[3].weaponList.Count == 5)
                        return;
                    allWeapon[3].weaponList.Add(weaponList[i]);
                    break;
                case "W5":
                    if (allWeapon[4].weaponList.Count == 5)
                        return;
                    allWeapon[4].weaponList.Add(weaponList[i]);
                    break;
                case "W6":
                    if (allWeapon[5].weaponList.Count == 5)
                        return;
                    allWeapon[5].weaponList.Add(weaponList[i]);
                    break;
            }
        }
    }
    List<ArmorList> armorList = new List<ArmorList>();
    public void LoadArmor(string path)
    {
        if (armorList.Count == 30)
            return;
        _ta = Resources.Load<TextAsset>("JsonData/" + path);

        jData = JsonMapper.ToObject(_ta.text);
        for (int i = 0; i < jData.Count; i++)
        {
            ArmorList _armorList = JsonMapper.ToObject<ArmorList>(jData[i].ToJson());

            string[] Def = _armorList.Def.Split('@');
            string[] SpeedTru = _armorList.SpeedTru.Split('@');
            string[] SoManhYeuCau = _armorList.SoManhYeuCau.Split('@');
            string[] GiaNangCap = _armorList.GiaNangCap.Split('@');

            for (int j = 0; j < Def.Length; j++)
            {
                if (!string.IsNullOrEmpty(Def[j]))
                    _armorList.DefValue.Add(float.Parse(Def[j]));
                if (!string.IsNullOrEmpty(SpeedTru[j]))
                    _armorList.SpeedTruValue.Add(float.Parse(SpeedTru[j]));
                if (!string.IsNullOrEmpty(SoManhYeuCau[j]))
                    _armorList.SoManhYeuCauValue.Add(float.Parse(SoManhYeuCau[j]));
                if (!string.IsNullOrEmpty(GiaNangCap[j]))
                    _armorList.GiaNangCapValue.Add(float.Parse(GiaNangCap[j]));
            }

            armorList.Add(_armorList);
        }


        for (int i = 0; i < armorList.Count; i++)
        {
            switch (armorList[i].ID)
            {
                case "A1":
                    if (allArmor[0].armorList.Count == 5)
                        return;
                    allArmor[0].armorList.Add(armorList[i]);
                    break;
                case "A2":
                    if (allArmor[1].armorList.Count == 5)
                        return;
                    allArmor[1].armorList.Add(armorList[i]);
                    break;
                case "A3":
                    if (allArmor[2].armorList.Count == 5)
                        return;
                    allArmor[2].armorList.Add(armorList[i]);
                    break;
                case "A4":
                    if (allArmor[3].armorList.Count == 5)
                        return;
                    allArmor[3].armorList.Add(armorList[i]);
                    break;
                case "A5":
                    if (allArmor[4].armorList.Count == 5)
                        return;
                    allArmor[4].armorList.Add(armorList[i]);
                    break;
                case "A6":
                    if (allArmor[5].armorList.Count == 5)
                        return;
                    allArmor[5].armorList.Add(armorList[i]);
                    break;
            }
        }
    }
    List<HelmetList> helmetList = new List<HelmetList>();
    public void LoadHelmet(string path)
    {
        if (helmetList.Count == 30)
            return;
        _ta = Resources.Load<TextAsset>("JsonData/" + path);

        jData = JsonMapper.ToObject(_ta.text);
        for (int i = 0; i < jData.Count; i++)
        {
            HelmetList _helmetList = JsonMapper.ToObject<HelmetList>(jData[i].ToJson());

            string[] Def = _helmetList.Def.Split('@');
            string[] BonusExp = _helmetList.BonusExp.Split('@');
            string[] Gia = _helmetList.Gia.Split('@');
            string[] SoManhYeuCau = _helmetList.SoManhYeuCau.Split('@');

            for (int j = 0; j < Def.Length; j++)
            {
                if (!string.IsNullOrEmpty(Def[j]))
                    _helmetList.DefValue.Add(float.Parse(Def[j]));
                if (!string.IsNullOrEmpty(BonusExp[j]))
                    _helmetList.BonusExpValue.Add(float.Parse(BonusExp[j]));
                if (!string.IsNullOrEmpty(Gia[j]))
                    _helmetList.GiaValue.Add(float.Parse(Gia[j]));
                if (!string.IsNullOrEmpty(SoManhYeuCau[j]))
                    _helmetList.SoManhYeuCauValue.Add(float.Parse(SoManhYeuCau[j]));
            }

            helmetList.Add(_helmetList);
        }


        for (int i = 0; i < helmetList.Count; i++)
        {
            switch (helmetList[i].ID)
            {
                case "H1":
                    if (allHelmet[0].helmetList.Count == 5)
                        return;
                    allHelmet[0].helmetList.Add(helmetList[i]);
                    break;
                case "H2":
                    if (allHelmet[1].helmetList.Count == 5)
                        return;
                    allHelmet[1].helmetList.Add(helmetList[i]);
                    break;
                case "H3":
                    if (allHelmet[2].helmetList.Count == 5)
                        return;
                    allHelmet[2].helmetList.Add(helmetList[i]);
                    break;
                case "H4":
                    if (allHelmet[3].helmetList.Count == 5)
                        return;
                    allHelmet[3].helmetList.Add(helmetList[i]);
                    break;
                case "H5":
                    if (allHelmet[4].helmetList.Count == 5)
                        return;
                    allHelmet[4].helmetList.Add(helmetList[i]);
                    break;
                case "H6":
                    if (allHelmet[5].helmetList.Count == 5)
                        return;
                    allHelmet[5].helmetList.Add(helmetList[i]);
                    break;
            }
        }
    }
    List<GlovesList> glovesList = new List<GlovesList>();
    public void LoadGloves(string path)
    {
        if (glovesList.Count == 30)
            return;
        _ta = Resources.Load<TextAsset>("JsonData/" + path);

        jData = JsonMapper.ToObject(_ta.text);
        for (int i = 0; i < jData.Count; i++)
        {
            GlovesList _glovesList = JsonMapper.ToObject<GlovesList>(jData[i].ToJson());

            string[] Giamtimereload = _glovesList.Giamtimereload.Split('@');
            string[] tangcritrate = _glovesList.tangcritrate.Split('@');
            string[] Tangcritdmg = _glovesList.Tangcritdmg.Split('@');
            string[] GiaNangCap = _glovesList.GiaNangCap.Split('@');
            string[] SoManhYeuCau = _glovesList.SoManhYeuCau.Split('@');

            for (int j = 0; j < Giamtimereload.Length; j++)
            {
                if (!string.IsNullOrEmpty(Giamtimereload[j]))
                    _glovesList.GiamtimereloadValue.Add(float.Parse(Giamtimereload[j]));
                if (!string.IsNullOrEmpty(tangcritrate[j]))
                    _glovesList.tangcritrateValue.Add(float.Parse(tangcritrate[j]));
                if (!string.IsNullOrEmpty(Tangcritdmg[j]))
                    _glovesList.TangcritdmgValue.Add(float.Parse(Tangcritdmg[j]));
                if (!string.IsNullOrEmpty(GiaNangCap[j]))
                    _glovesList.GiaNangCapValue.Add(float.Parse(GiaNangCap[j]));
                if (!string.IsNullOrEmpty(SoManhYeuCau[j]))
                    _glovesList.SoManhYeuCauValue.Add(float.Parse(SoManhYeuCau[j]));
            }


            glovesList.Add(_glovesList);
        }


        for (int i = 0; i < glovesList.Count; i++)
        {
            switch (glovesList[i].ID)
            {
                case "G1":
                    if (allGloves[0].glovesList.Count == 5)
                        return;
                    allGloves[0].glovesList.Add(glovesList[i]);
                    break;
                case "G2":
                    if (allGloves[1].glovesList.Count == 5)
                        return;
                    allGloves[1].glovesList.Add(glovesList[i]);
                    break;
                case "G3":
                    if (allGloves[2].glovesList.Count == 5)
                        return;
                    allGloves[2].glovesList.Add(glovesList[i]);
                    break;
                case "G4":
                    if (allGloves[3].glovesList.Count == 5)
                        return;
                    allGloves[3].glovesList.Add(glovesList[i]);
                    break;
                case "G5":
                    if (allGloves[4].glovesList.Count == 5)
                        return;
                    allGloves[4].glovesList.Add(glovesList[i]);
                    break;
                case "G6":
                    if (allGloves[5].glovesList.Count == 5)
                        return;
                    allGloves[5].glovesList.Add(glovesList[i]);
                    break;
            }
        }
    }
    List<BagList> bagList = new List<BagList>();
    public void LoadBag(string path)
    {
        if (bagList.Count == 30)
            return;
        _ta = Resources.Load<TextAsset>("JsonData/" + path);

        jData = JsonMapper.ToObject(_ta.text);
        for (int i = 0; i < jData.Count; i++)
        {
            BagList _bagList = JsonMapper.ToObject<BagList>(jData[i].ToJson());



            string[] Bonussoluongmauanduoc = _bagList.Bonussoluongmauanduoc.Split('@');
            string[] HealthRegeneration = _bagList.HealthRegeneration.Split('@');
            string[] GiaNangCap = _bagList.GiaNangCap.Split('@');
            string[] SoManhYeuCau = _bagList.SoManhYeuCau.Split('@');

            for (int j = 0; j < Bonussoluongmauanduoc.Length; j++)
            {
                if (!string.IsNullOrEmpty(Bonussoluongmauanduoc[j]))
                    _bagList.BonussoluongmauanduocValue.Add(float.Parse(Bonussoluongmauanduoc[j]));
                if (!string.IsNullOrEmpty(HealthRegeneration[j]))
                    _bagList.HealthRegenerationValue.Add(float.Parse(HealthRegeneration[j]));
                if (!string.IsNullOrEmpty(GiaNangCap[j]))
                    _bagList.GiaNangCapValue.Add(float.Parse(GiaNangCap[j]));
                if (!string.IsNullOrEmpty(SoManhYeuCau[j]))
                    _bagList.SoManhYeuCauValue.Add(float.Parse(SoManhYeuCau[j]));
            }


            bagList.Add(_bagList);
        }


        for (int i = 0; i < bagList.Count; i++)
        {
            switch (bagList[i].ID)
            {
                case "B1":
                    if (allBag[0].bagList.Count == 5)
                        return;
                    allBag[0].bagList.Add(bagList[i]);
                    break;
                case "B2":
                    if (allBag[1].bagList.Count == 5)
                        return;
                    allBag[1].bagList.Add(bagList[i]);
                    break;
                case "B3":
                    if (allBag[2].bagList.Count == 5)
                        return;
                    allBag[2].bagList.Add(bagList[i]);
                    break;
                case "B4":
                    if (allBag[3].bagList.Count == 5)
                        return;
                    allBag[3].bagList.Add(bagList[i]);
                    break;
                case "B5":
                    if (allBag[4].bagList.Count == 5)
                        return;
                    allBag[4].bagList.Add(bagList[i]);
                    break;
                case "B6":
                    if (allBag[5].bagList.Count == 5)
                        return;
                    allBag[5].bagList.Add(bagList[i]);
                    break;
            }
        }
    }
    List<ShoesList> shoesList = new List<ShoesList>();
    public void LoadShoes(string path)
    {
        if (shoesList.Count == 30)
            return;
        _ta = Resources.Load<TextAsset>("JsonData/" + path);

        jData = JsonMapper.ToObject(_ta.text);
        for (int i = 0; i < jData.Count; i++)
        {
            ShoesList _shoesList = JsonMapper.ToObject<ShoesList>(jData[i].ToJson());

            string[] TangSpeeDichuyen = _shoesList.TangSpeeDichuyen.Split('@');
            string[] TangDoCaoNhay = _shoesList.TangDoCaoNhay.Split('@');
            string[] GiaNangCap = _shoesList.GiaNangCap.Split('@');
            string[] SoManhYeuCau = _shoesList.SoManhYeuCau.Split('@');

            for (int j = 0; j < TangSpeeDichuyen.Length; j++)
            {
                if (!string.IsNullOrEmpty(TangSpeeDichuyen[j]))
                    _shoesList.TangSpeeDichuyenValue.Add(float.Parse(TangSpeeDichuyen[j]));
                if (!string.IsNullOrEmpty(TangDoCaoNhay[j]))
                    _shoesList.TangDoCaoNhayValue.Add(float.Parse(TangDoCaoNhay[j]));
                if (!string.IsNullOrEmpty(GiaNangCap[j]))
                    _shoesList.GiaNangCapValue.Add(float.Parse(GiaNangCap[j]));
                if (!string.IsNullOrEmpty(SoManhYeuCau[j]))
                    _shoesList.SoManhYeuCauValue.Add(float.Parse(SoManhYeuCau[j]));
            }


            shoesList.Add(_shoesList);
        }


        for (int i = 0; i < shoesList.Count; i++)
        {
            switch (shoesList[i].ID)
            {
                case "S1":
                    if (allShoes[0].shoesList.Count == 5)
                        return;
                    allShoes[0].shoesList.Add(shoesList[i]);
                    break;
                case "S2":
                    if (allShoes[1].shoesList.Count == 5)
                        return;
                    allShoes[1].shoesList.Add(shoesList[i]);
                    break;
                case "S3":
                    if (allShoes[2].shoesList.Count == 5)
                        return;
                    allShoes[2].shoesList.Add(shoesList[i]);
                    break;
                case "S4":
                    if (allShoes[3].shoesList.Count == 5)
                        return;
                    allShoes[3].shoesList.Add(shoesList[i]);
                    break;
                case "S5":
                    if (allShoes[4].shoesList.Count == 5)
                        return;
                    allShoes[4].shoesList.Add(shoesList[i]);
                    break;
                case "S6":
                    if (allShoes[5].shoesList.Count == 5)
                        return;
                    allShoes[5].shoesList.Add(shoesList[i]);
                    break;
            }
        }
    }
    List<TileVatPhamList> tilevatphamList = new List<TileVatPhamList>();
    public void LoadTiLeVatPham(string path)
    {
        if (tilevatphamList.Count == 44)
            return;
        _ta = Resources.Load<TextAsset>("JsonData/" + path);

        jData = JsonMapper.ToObject(_ta.text);
        for (int i = 0; i < jData.Count; i++)
        {
            TileVatPhamList _tilevatphamList = JsonMapper.ToObject<TileVatPhamList>(jData[i].ToJson());
            tilevatphamList.Add(_tilevatphamList);
        }


        for (int i = 0; i < tilevatphamList.Count; i++)
        {
            if (allTileVatPham[tilevatphamList[i].Stage - 1].tilevatphamList.Count == 9)
                return;
            allTileVatPham[tilevatphamList[i].Stage - 1].tilevatphamList.Add(tilevatphamList[i]);
        }
    }
    public void LoadDataPrimeAccount()
    {
        strPrimeAccount = PlayerPrefs.GetString(DataParam.PRIMEACCOUNTINFO);

        if (string.IsNullOrEmpty(strPrimeAccount))
            return;
         jData = JsonMapper.ToObject(strPrimeAccount);
      //  Debug.LogError(strPrimeAccount + ":=======:" + jData.Count);
        primeAccout.isVIP = bool.Parse(jData["isVIP"].ToString());
        primeAccout.takecoin = bool.Parse(jData["takecoin"].ToString());
        primeAccout.takegem = bool.Parse(jData["takegem"].ToString());
        primeAccout.countDay = int.Parse(jData["countDay"].ToString());

        //primeAccout = JsonMapper.ToObject<PrimeAccountClass>(jData[0].ToJson()); // dung` dc

        if (primeAccout.isVIP)
        {
            DataParam.timeBeginBuyPrimeAccount = System.Convert.ToDateTime(PlayerPrefs.GetString(DataParam.TIMEBEGINBUYPRIMEACCOUNT));
            if (primeAccout.takecoin && primeAccout.takegem)
            {
                if ((System.DateTime.Now - DataParam.timeBeginBuyPrimeAccount).TotalSeconds >= 86400)
                {
                    primeAccout.takecoin = primeAccout.takegem = false;
                }
            }
        }
    }
    void LoadBlackMarket()
    {
        blackMarketSave.Clear();
        DataParam.countResetBlackMarket = PlayerPrefs.GetInt(DataParam.COUNTRESETBLACKMARKET);
        strBlackMarket = PlayerPrefs.GetString(DataParam.BLACKMARKET);
        if (string.IsNullOrEmpty(strBlackMarket))
        {
            AddNewBlackMarket();
            return;
        }
        jData = JsonMapper.ToObject(strBlackMarket);
        Debug.LogError(":" + strBlackMarket);
        for (int i = 0; i < jData.Count; i++)
        {
            BlackMarketData _blackMarketSave = new BlackMarketData();
            _blackMarketSave.ID = jData[i]["ID"].ToString();
            _blackMarketSave.NAME = jData[i]["NAME"].ToString();
            _blackMarketSave.Level = jData[i]["Level"].ToString();
            _blackMarketSave.GiaBanCoin = float.Parse(jData[i]["GiaBanCoin"].ToString());
            _blackMarketSave.GiaGem1Manh = float.Parse(jData[i]["GiaGem1Manh"].ToString());
            _blackMarketSave.countnumber = int.Parse(jData[i]["countnumber"].ToString());
            blackMarketSave.Add(_blackMarketSave);
        }
    }
    public void SaveData()
    {
        SavePrimeAccount();
        SaveAchievement();
        SaveDailyQuest();
        SaveBlackMarket();
    }
    public void LoadData()
    {
        LoadDataPrimeAccount();
        LoadAchievement();
        LoadDailyQuest();
    }
    void SaveBlackMarket()
    {
        PlayerPrefs.SetInt(DataParam.COUNTRESETBLACKMARKET, DataParam.countResetBlackMarket);
        PlayerPrefs.SetString(DataParam.BLACKMARKET, JsonMapper.ToJson(blackMarketSave));
    }
    void SavePrimeAccount()
    {
        PlayerPrefs.SetString(DataParam.TIMEBEGINBUYPRIMEACCOUNT, DataParam.timeBeginBuyPrimeAccount.ToString());
        PlayerPrefs.SetString(DataParam.PRIMEACCOUNTINFO, JsonMapper.ToJson(primeAccout));
    }
    void SaveDailyQuest()
    {
        PlayerPrefs.SetString(DataParam.ALLDAILYQUEST, JsonMapper.ToJson(allSaveDailyQuest));
        strSaveIndexQuest = null;
        for (int i = 0; i < saveIndexQuest.Count; i++)
        {
            strSaveIndexQuest += saveIndexQuest[i] + "@";
        }
        PlayerPrefs.SetString(DataParam.SAVEINDEXQUEST, strSaveIndexQuest);
    }
    void SaveAchievement()
    {
        PlayerPrefs.SetString(DataParam.ALLACHIEVEMENT, JsonMapper.ToJson(saveAllAchievement));
    }
    string strAllAchievement, strAllDailyQuest, strSaveIndexQuest, strPrimeAccount,strBlackMarket;
    void LoadAchievement()
    {
        for (int i = 0; i < allAchievement.Count; i++)
        {
            SaveAchievement _saveAchievment = new SaveAchievement();
            saveAllAchievement.Add(_saveAchievment);
        }
        strAllAchievement = PlayerPrefs.GetString(DataParam.ALLACHIEVEMENT);

        if (string.IsNullOrEmpty(strAllAchievement))
            return;
        jData = JsonMapper.ToObject(strAllAchievement);
        for (int i = 0; i < jData.Count; i++)
        {
            if (jData[i] != null)
            {
                saveAllAchievement[i].currentLevel = int.Parse(jData[i]["currentLevel"].ToString());
                saveAllAchievement[i].currentNumber = int.Parse(jData[i]["currentNumber"].ToString());
                saveAllAchievement[i].isPass = bool.Parse(jData[i]["isPass"].ToString());
                saveAllAchievement[i].isDone = bool.Parse(jData[i]["isDone"].ToString());
            }
        }
    }
    public void DoAchievement(int index, int _numberAdd)
    {
        if (saveAllAchievement[index].isDone || saveAllAchievement[index].isPass)
            return;
        saveAllAchievement[index].currentNumber += _numberAdd;
        if (saveAllAchievement[index].currentNumber >= allAchievement[index].maxNumber[saveAllAchievement[index].currentLevel - 1])
        {
            saveAllAchievement[index].isPass = true;
            saveAllAchievement[index].currentNumber = allAchievement[index].maxNumber[saveAllAchievement[index].currentLevel - 1];
        }
    }
    public void DoDailyQuest(int index, int _numberAdd)
    {
        if (allSaveDailyQuest[index].isDone || allSaveDailyQuest[index].isPass || !allSaveDailyQuest[index].isActive)
            return;
        allSaveDailyQuest[index].currentNumber += _numberAdd;
        if (allSaveDailyQuest[index].currentNumber >= allSaveDailyQuest[index].Soluong)
        {
            allSaveDailyQuest[index].isPass = true;
            allSaveDailyQuest[index].currentNumber = allSaveDailyQuest[index].Soluong;
        }
    }
    bool checkDaily, checkAchi;
    public bool CheckWarningDailyQuest()
    {
        checkDaily = false;
        for (int i = 0; i < allSaveDailyQuest.Count; i++)
        {
            if (allSaveDailyQuest[i].isPass && !allSaveDailyQuest[i].isDone)
            {
                checkDaily = true;
            }
        }
        return checkDaily;
    }
    public bool CheckWarningAchievement()
    {
        checkAchi = false;
        for (int i = 0; i < saveAllAchievement.Count; i++)
        {
            if (saveAllAchievement[i].isPass && !saveAllAchievement[i].isDone)
            {
                // Debug.LogError(i + ":" + saveAllAchievement[i]);
                checkAchi = true;
            }
        }
        //  Debug.LogError(checkAchi);
        return checkAchi;
    }

    public void CheckDoneAllDailyQuest()
    {
        if (!saveIndexQuest.Contains(10))
        {
            // Debug.Log("111111111");
            return;
        }
        DataParam.doneAllDailyQuest = true;
        for (int i = 0; i < allSaveDailyQuest.Count - 1; i++)
        {
            if (!allSaveDailyQuest[i].isDone && allSaveDailyQuest[i].isActive)
            {
                DataParam.doneAllDailyQuest = false;
                // Debug.Log("false");
                break;
            }
        }
        if (DataParam.doneAllDailyQuest)
        {
            for (int i = 0; i < saveIndexQuest.Count; i++)
            {
                if (saveIndexQuest[i] == 10)
                {
                    DoDailyQuest(10, 1);
                    MenuController.instance.achievementAndDailyQuestPanel.dailyquestBouders[i].DisplayMe();
                    // Debug.LogError("zoooooooooooo");
                }
            }
        }
    }
}