using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ItemData
{
    public string id;
    public DataUtils.eType type;
    public DataUtils.eLevel level;
    public int pices;
    public bool isUnlock;
}