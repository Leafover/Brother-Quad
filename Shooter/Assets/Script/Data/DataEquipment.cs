using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ItemData
{
    public string id;
    public /*DataUtils.eType*/string type;
    public /*DataUtils.eLevel*/string level;
    public int pices;
    public bool isUnlock;
    public int curStar = 0;
    public int quantity = 0;
    public string itemName;
    public bool isEquipped;
}
//Weapon, Armor, Gloves, Helmet, Shoes, Bag
[Serializable]
public class ItemWeapon
{
    public int weponIndex;//W1:0, W2:1, W3:2, W4:3, W5:4, W6:5
    public float DmgValue;
    public float ReloadSpeedValue;
    public float MagazineValue;
    public float CritRateValue;
    public float CritDmgValue;
    public float BulletSpeedValue;
    public float AtkRangeValue;
    public float AtksecValue;
}
[Serializable]
public class ItemArmor
{
    public int armorIndex = 0;//A1:0, A2:1, A3:2, A4:3, A5:4, A6:5
    public float defIncrease = 0;//%
    public float speedReduce = 0;//%
}
[Serializable]
public class ItemHelmet
{
    public int helIndex = 0;//H1:0, H2:1, H3:2, H4:3, H5:4, H6:5
    public float defIncrease = 0;//%
    public float bonusExp = 0;//%
}
[Serializable] 
public class ItemGloves
{
    public int gloveIndex = 0;//G1:0, G2:1, G3:2, G4:3, G5:4, G6:5
    public float reloadTimeReduce = 0;//second
    public float critRateIncrease = 0;//%
    public float critDamageIncrease = 0;//%
}
[Serializable] 
public class ItemBag
{
    public int bagIndex = 0;//B1:0, B2:1, B3:2, B4:3, B5:4, B6:5
    public float totalAidDrop = 0;
    public float HealthRegeneration = 0;//%
}
[Serializable] 
public class ItemShoes
{
    public int shoeIndex = 0;//S1:0, S2:1, S3:2, S4:3, S5:4, S6:5
    public float moveSpeedIncrease = 0;//%
    public float jumpHeight = 0;//%
}