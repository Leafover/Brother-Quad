using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPack:MonoBehaviour
{
    public enum PACK_TYPE { COIN, GEM, PLAYER, SHOES, BAG, GLOVES, HELMET, ARMOR, WEAPON, HPPACK }
    public PACK_TYPE pType;
    public string itemPackID;
    public int quantiti;
}
