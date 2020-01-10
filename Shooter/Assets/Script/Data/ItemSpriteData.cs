using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSpriteData", menuName = "Create Item Sprite Data")]
public class ItemSpriteData : ScriptableObject
{
    public List<ItemSprite> spriteDatas = new List<ItemSprite>();
}

[System.Serializable]
public class ItemSprite
{
    public Sprite sprItem;
    public string itemName;
}