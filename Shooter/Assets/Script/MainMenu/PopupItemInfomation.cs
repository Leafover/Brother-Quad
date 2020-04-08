using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PopupItemInfomation : MonoBehaviour
{
    public Image imgItemPriview, imgQuality, imgItemLevel;
    public TextMeshProUGUI txtItemName, txtItemLevel, txtItemInfo, txtPiece, txtDmg, txtFireRate, txtCritRate, txtCritDmg, txtRange, txtMagazine;
    public GameObject gWeapon, gItemInfo;

    private ItemData itemEquipped;
    private string keyEquipped;
    public void HidePopup()
    {
        gameObject.SetActive(false);
    }
    public void ShowItemInfo(string id,string level, string type, bool isEquipped) {

        foreach (ItemData _iData in DataUtils.dicEquippedItem.Values)
        {
            if (_iData.type.Equals(type))
            {
                itemEquipped = _iData;
                break;
            }
            else
            {
                itemEquipped = null;
            }
        }


        imgItemLevel.color = MainMenuController.Instance.GetColorByItem(level);
        txtItemInfo.text = DataUtils.GetItemInfo(id + "_" + level, type.ToString().ToUpper());
        if (id.Contains("W")) {
            gItemInfo.SetActive(false);

            keyEquipped = !isEquipped? id + "_" + level : itemEquipped.id + "_" + itemEquipped.level;
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
}
