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
}