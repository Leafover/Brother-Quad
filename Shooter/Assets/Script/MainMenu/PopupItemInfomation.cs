using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PopupItemInfomation : MonoBehaviour
{
    public Image imgItemPriview, imgQuality, imgItemLevel, imgPiece;
    public TextMeshProUGUI txtItemName, txtItemLevel, txtItemInfo, txtPiece, txtDmg, txtFireRate, txtCritRate, txtCritDmg, txtRange, txtMagazine;
    public GameObject gWeapon, gItemInfo;

    private ItemData itemEquipped, itemData;

    private string keyEquipped, itemKey = "";
    public void HidePopup()
    {
        gameObject.SetActive(false);
    }
    public void ShowItemInfo(string _id, string _level, string _type, bool isEquipped)
    {
        imgItemPriview.sprite = DataUtils.GetSpriteByName(_id, MainMenuController.Instance.allSpriteData);

        #region Image Quality
        switch (_level)
        {
            case "Normal":
                imgQuality.sprite = MenuController.instance.blackMarketpanel.levelSp[0];
                break;
            case "Uncommon":
                imgQuality.sprite = MenuController.instance.blackMarketpanel.levelSp[1];
                break;
            case "Rare":
                imgQuality.sprite = MenuController.instance.blackMarketpanel.levelSp[2];
                break;
            case "Epic":
                imgQuality.sprite = MenuController.instance.blackMarketpanel.levelSp[3];
                break;
            case "Legendary":
                imgQuality.sprite = MenuController.instance.blackMarketpanel.levelSp[4];
                break;
        }
        #endregion

        #region Check item equipped
        foreach (ItemData _iData in DataUtils.dicEquippedItem.Values)
        {
            if (_iData.type.Equals(_type))
            {
                itemEquipped = _iData;
                break;
            }
            else
            {
                itemEquipped = null;
            }
        }
        #endregion



        //float _p = GetPiceByStar(itemData, false);
        //res = itemData.pices * 1.0f / _p;
        //if (res <= 1)
        //{
        //    txtFillProgress.text = itemData.pices + "/" + (int)_p;
        //}
        //else
        //{
        //    txtFillProgress.text = itemData.pices.ToString();
        //}

        txtItemName.color = MainMenuController.Instance.GetColorByItem(_level);
        txtItemName.text = DataUtils.GetItemName(_type, _id + "_" + _level);
        imgPiece.enabled = !isEquipped;
        itemKey = _id + "_" + _level + "_" + isEquipped + "_" + isEquipped;
        if (DataUtils.dicAllEquipment.ContainsKey(itemKey))
            itemData = DataUtils.dicAllEquipment[itemKey];
        else itemData = null;
        txtPiece.text = itemData == null ? "0" : itemData.pices + "/" + (int)DataUtils.GetPiceByStar(itemData, false);
        txtItemLevel.text = _level;
        imgItemLevel.color = MainMenuController.Instance.GetColorByItem(_level);
        txtItemInfo.text = DataUtils.GetItemInfo(_id + "_" + _level, _type.ToString().ToUpper());
        if (_id.Contains("W"))
        {
            gItemInfo.SetActive(false);

            keyEquipped = !isEquipped ? _id + "_" + _level : itemEquipped.id + "_" + itemEquipped.level;
            int curStar = itemEquipped == null ? 0 : itemEquipped.curStar < DataUtils.MAX_STARS ? itemEquipped.curStar : 4;

            txtDmg.text = DataUtils.GetRealFloat((DataUtils.dicWeapon[keyEquipped].DmgValue[curStar] * 10));
            txtFireRate.text = DataUtils.GetRealFloat(DataUtils.dicWeapon[keyEquipped].AtksecValue[curStar]);
            txtCritDmg.text = DataUtils.GetRealFloat(DataUtils.dicWeapon[keyEquipped].CritDmgValue[curStar]);
            txtCritRate.text = DataUtils.GetRealFloat(DataUtils.dicWeapon[keyEquipped].CritRateValue[curStar]);
            txtRange.text = DataUtils.GetRealFloat(DataUtils.dicWeapon[keyEquipped].AtkRangeValue[curStar]);
            txtMagazine.text = DataUtils.GetRealFloat(DataUtils.dicWeapon[keyEquipped].MagazineValue[curStar]);


            gWeapon.SetActive(true);
        }
        else
        {
            gWeapon.SetActive(false);
            gItemInfo.SetActive(true);
        }
        gameObject.SetActive(true);
    }
    public void BuyMoreParts() {
        MainMenuController.Instance.HideAllPanel();
        MainMenuController.Instance.ShowShop();
    }
}
