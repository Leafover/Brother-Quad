using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class PlayerDataInfo
{
    public string name = "REMITANO";
    public string id;
    public int level = 1;
    public int exp = 0;
    public int hp = 0;
    public int coins = 0;
    public int gems = 0;
    public int curStars = 1;
    public int pices = 0;
    public bool isUnlock;
    public bool isEquipped;
}

[SerializeField]
public class HeroDataInfo
{
    public string name = "REMITANO";
    public string id;
    public int level = 1;
    public int exp = 0;
    public int hp = 0;
    public int curStars = 1;
    public int pices = 0;
    public bool isUnlock;
    public bool isEquipped;
}
