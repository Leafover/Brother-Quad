using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region AllTileVatPham
[System.Serializable]
public class TileVatPhamList
{
    public string ID;
    public int Level,Stage;
    public double Normal, Uncommon, Rare,Epic,Legendary, TotalNumber;
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
    public string ID, NAME, Level;
    public double TangSpeeDichuyen, TangDoCaoNhay, SoManhYeuCau, GiaMua1Manh, GiaKhiRaDo, Gia;
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
    public string ID, NAME, Level;
    public double Bonussoluongmauanduoc, HealthRegeneration, SoManhYeuCau, GiaMua1Manh, GiaKhiRaDo, Gia;
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
    public string ID, NAME, Level;
    public double Giamtimereload, tangcritrate, Tangcritdmg, SoManhYeuCau, GiaMua1Manh, GiaKhiRaDo, Gia;
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
    public string ID, NAME, Level;
    public double Def, BonusExp, SoManhYeuCau, GiaMua1Manh, GiaKhiRaDo, Gia;
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
    public string ID, NAME, Level;
    public double Def, SpeedTru, SoManhYeuCau, GiaMua1Manh, GiaKhiRaDo, Gia;
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
    public string ID, NAME, Level;
    public double Dmg, BulletSpeed, ReloadSpeed, Magazine, AtkRange, CritRate, CritDmg, SoManhYeuCau, GiaMua1Manh, GiaKhiRaDo, Gia, Atksec;
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
    public List<AllTileVatPham> allTileVatPham = new List<AllTileVatPham>();
    public List<AllShoes> allShoes = new List<AllShoes>();
    public List<AllBag> allBag = new List<AllBag>();
    public List<AllGloves> allGloves = new List<AllGloves>();
    public List<AllHelmet> allHelmet = new List<AllHelmet>();
    public List<AllArmor> allArmor = new List<AllArmor>();
    public List<AllWeapon> allWeapon = new List<AllWeapon>();
    public List<AllDataEnemy> allDataEnemy = new List<AllDataEnemy>();
    public List<AllMission> allMission = new List<AllMission>();
    public List<PlayerData> playerData = new List<PlayerData>();
    //  public List<Mission> missions = new List<Mission>();


    public static DataController instance;
    public string[] nameDataText;
    public string[] nameDataMissionText;
    public string nameDataPlayerText, nameDataWeapon, nameDataArmor, nameDataHelmet, nameDataGloves,nameDataBag,nameDataShoes, nameDataTiLeVatPham;

    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        DataUtils.FillPlayerDataInfo();
        if (DataUtils.StageHasInit())
        {
            DataUtils.FillAllStage();
        }
    }
    public bool loaddatabegin;
    private void OnValidate()
    {

        if (!loaddatabegin)
        {

            weaponList.Clear();
            armorList.Clear();
            helmetList.Clear();
            glovesList.Clear();
            bagList.Clear();
            shoesList.Clear();
            tilevatphamList.Clear();

            for (int i = 0; i < nameDataText.Length; i++)
            {
                LoadDataEnemy(nameDataText[i], i);
            }
            LoadDataPlayer(nameDataPlayerText);

            for (int i = 0; i < nameDataMissionText.Length; i++)
            {
                LoadDataMission(nameDataMissionText[i], i);
            }
            LoadWeapon(nameDataWeapon);
            LoadArmor(nameDataArmor);
            LoadHelmet(nameDataHelmet);
            LoadGloves(nameDataGloves);
            LoadBag(nameDataBag);
            LoadShoes(nameDataShoes);
            LoadTiLeVatPham(nameDataTiLeVatPham);
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
}
