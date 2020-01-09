using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DataEquipment
{
    public List<ItemData> allEquipment { get; set; }
}
[Serializable]
public class ItemData
{
    public string id;
    public string type;
    public string level;
    public int pices;
    public bool isUnlock;
}
