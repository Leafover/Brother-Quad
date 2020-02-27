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