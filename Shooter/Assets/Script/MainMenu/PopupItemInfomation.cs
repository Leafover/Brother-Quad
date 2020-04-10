using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PopupItemInfomation : MonoBehaviour
{
    public Sprite sprYellowStar, sprWhiteStar;
    public Image imgItemPriview, imgQuality, imgItemLevel, imgPiece, imgItemInfo, imgGloves, imgHelmet,imgBag,imgShoes, imgArmor;
    public TextMeshProUGUI txtItemName, txtItemLevel, txtItemInfo, txtPiece, txtDmg, txtFireRate, txtCritRate, txtCritDmg, txtRange, txtMagazine;
    public TextMeshProUGUI txtReload, txtCritRateItem, txtCritDmgItem, txtDef, txtFirstAid, txtRegen, txtMoveSpeed, txtJump, txtPartItem;

    public GameObject gWeapon, gItemInfo, gAllStars, gPartSelect;
    public Image[] allStars;

    private float _reloadTime, _critRate, _critDmg, _defensiveArmor, _firstAid, _regen, _moveSpeedArmor, _jumpHeight, _defHelmet, _moveSpeedShoes;
    private ItemData itemEquipped, itemData;

    private string keyEquipped, itemKeyFullpart = "", itemKey="";
    private int curStar;
    public void HidePopup()
    {
        gameObject.SetActive(false);
    }
    private void ResetValue()
    {
        _defensiveArmor = 0;//Armor
        _moveSpeedArmor = 0;//Armor
        _regen = 0;//Bag
        _firstAid = 0;//Bag
        _reloadTime = 0;//Gloves
        _critRate = 0;//Gloves
        _critDmg =0;//Gloves
        _defHelmet = 0;//Helmet
        _moveSpeedShoes = 0;//Shoes
        _jumpHeight = 0;//Shoes
    }
    private void FillStars(int _index) {
        for (int i = 0; i < allStars.Length; i++) {
            if (i <= _index) {
                allStars[i].sprite = sprYellowStar;
            }
            else
                allStars[i].sprite = sprWhiteStar;
        }
    }

    private void FillData() {
        if (DataUtils.dicArmor.ContainsKey(itemKey))
        {
            _defensiveArmor = DataUtils.dicArmor[itemKey].DefValue[curStar];//Armor
            _moveSpeedArmor = DataUtils.dicArmor[itemKey].SpeedTruValue[curStar];//Armor
        }
        if (DataUtils.dicBag.ContainsKey(itemKey))
        {
            _regen = DataUtils.dicBag[itemKey].HealthRegenerationValue[curStar >= DataUtils.dicBag[itemKey].HealthRegenerationValue.Count ? DataUtils.dicBag[itemKey].HealthRegenerationValue.Count - 1 : curStar];//Bag
            _firstAid = DataUtils.dicBag[itemKey].BonussoluongmauanduocValue[curStar >= DataUtils.dicBag[itemKey].HealthRegenerationValue.Count ? DataUtils.dicBag[itemKey].HealthRegenerationValue.Count - 1 : curStar];//Bag
        }
        if (DataUtils.dicGloves.ContainsKey(itemKey))
        {
            _reloadTime = DataUtils.dicGloves[itemKey].GiamtimereloadValue[curStar];//Gloves
            _critRate = DataUtils.dicGloves[itemKey].tangcritrateValue[curStar];//Gloves
            _critDmg = DataUtils.dicGloves[itemKey].TangcritdmgValue[curStar];//Gloves
        }
        if (DataUtils.dicHelmet.ContainsKey(itemKey))
            _defHelmet = DataUtils.dicHelmet[itemKey].DefValue[curStar];//Helmet
        if (DataUtils.dicShoes.ContainsKey(itemKey))
        {
            _moveSpeedShoes = DataUtils.dicShoes[itemKey].TangSpeeDichuyenValue[curStar];//Shoes
            _jumpHeight = DataUtils.dicShoes[itemKey].TangDoCaoNhayValue[curStar];//Shoes
        }
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


        
        txtItemName.color = MainMenuController.Instance.GetColorByItem(_level);
        txtItemName.text = DataUtils.GetItemName(_type, _id + "_" + _level);
        imgPiece.enabled = !isEquipped;
        gAllStars.SetActive(isEquipped);
        gPartSelect.SetActive(!isEquipped);

        itemKeyFullpart = _id + "_" + _level + "_" + isEquipped + "_" + isEquipped;
        itemKey = _id + "_" + _level;
        if (DataUtils.dicAllEquipment.ContainsKey(itemKeyFullpart))
            itemData = DataUtils.dicAllEquipment[itemKeyFullpart];
        else itemData = null;
        txtPiece.text = itemData == null ? "0" : itemData.pices + "/" + (int)DataUtils.GetPiceByStar(itemData, false);
        txtPartItem.text = txtPiece.text;


        if (itemData != null)
        {
            curStar = itemData.curStar < 5 ? itemData.curStar : 4;
            FillStars(curStar);
        }
        else
        {
            curStar = 0;
        }
        FillData();

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

            imgItemInfo.enabled = true;

            imgGloves.gameObject.SetActive(false);
            imgHelmet.gameObject.SetActive(false);
            imgBag.gameObject.SetActive(false);
            imgArmor.gameObject.SetActive(false);
            imgShoes.gameObject.SetActive(false);
            gWeapon.SetActive(true);
        }
        else if (_id.Contains("S"))//Shoes
        {
            txtMoveSpeed.text = "+" + _moveSpeedShoes + "%";
            txtJump.text = "+" + _jumpHeight + "%";

            imgItemInfo.enabled = false;
            
            txtReload.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtCritRateItem.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtCritDmgItem.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtDef.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtFirstAid.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtRegen.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtMoveSpeed.gameObject.transform.parent.parent.gameObject.SetActive(true);
            txtJump.gameObject.transform.parent.parent.gameObject.SetActive(true);



            gWeapon.SetActive(false);
            imgGloves.gameObject.SetActive(false);
            imgHelmet.gameObject.SetActive(false);
            imgBag.gameObject.SetActive(false);
            imgArmor.gameObject.SetActive(false);
            imgShoes.gameObject.SetActive(true);

            gItemInfo.SetActive(true);
        }
        else if (_id.Contains("B"))//Bag
        {
            txtFirstAid.text = "+" + _firstAid + "% HP";
            txtRegen.text = _regen + "% HP";

            imgItemInfo.enabled = false;


            txtReload.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtCritRateItem.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtCritDmgItem.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtDef.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtFirstAid.gameObject.transform.parent.parent.gameObject.SetActive(true);
            txtRegen.gameObject.transform.parent.parent.gameObject.SetActive(true);
            txtMoveSpeed.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtJump.gameObject.transform.parent.parent.gameObject.SetActive(false);


            gWeapon.SetActive(false);
            imgGloves.gameObject.SetActive(false);
            imgHelmet.gameObject.SetActive(false);
            imgArmor.gameObject.SetActive(false);
            imgShoes.gameObject.SetActive(false);
            imgBag.gameObject.SetActive(true);
            gItemInfo.SetActive(true);
        }
        else if (_id.Contains("G"))//Gloves
        {
            txtReload.text = "-" + _reloadTime + "%";
            txtCritRateItem.text = "" + _critRate;
            txtCritDmgItem.text = "" + _critDmg;

            imgItemInfo.enabled = false;


            txtReload.gameObject.transform.parent.parent.gameObject.SetActive(true);
            txtCritRateItem.gameObject.transform.parent.parent.gameObject.SetActive(true);
            txtCritDmgItem.gameObject.transform.parent.parent.gameObject.SetActive(true);
            txtDef.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtFirstAid.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtRegen.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtMoveSpeed.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtJump.gameObject.transform.parent.parent.gameObject.SetActive(false);

            gWeapon.SetActive(false);
            imgHelmet.gameObject.SetActive(false);
            imgBag.gameObject.SetActive(false);
            imgArmor.gameObject.SetActive(false);
            imgShoes.gameObject.SetActive(false);
            imgGloves.gameObject.SetActive(true);
            gItemInfo.SetActive(true);
        }
        else if (_id.Contains("H"))//Helmet
        {
            txtDef.text = _defHelmet + "%";

            imgItemInfo.enabled = false;

            txtReload.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtCritRateItem.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtCritDmgItem.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtDef.gameObject.transform.parent.parent.gameObject.SetActive(true);
            txtFirstAid.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtRegen.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtMoveSpeed.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtJump.gameObject.transform.parent.parent.gameObject.SetActive(false);


            gWeapon.SetActive(false);
            imgGloves.gameObject.SetActive(false);
            imgBag.gameObject.SetActive(false);
            imgArmor.gameObject.SetActive(false);
            imgShoes.gameObject.SetActive(false);
            imgHelmet.gameObject.SetActive(true);
            gItemInfo.SetActive(true);
        }
        else if (_id.Contains("A"))//Armor
        {
            txtDef.text = "+" + _defensiveArmor + "%";
            txtMoveSpeed.text = "-" + _moveSpeedArmor + "%";

            imgItemInfo.enabled = false;


            txtReload.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtCritRateItem.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtCritDmgItem.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtDef.gameObject.transform.parent.parent.gameObject.SetActive(true);
            txtFirstAid.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtRegen.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtMoveSpeed.gameObject.transform.parent.parent.gameObject.SetActive(true);
            txtJump.gameObject.transform.parent.parent.gameObject.SetActive(false);

            gWeapon.SetActive(false);
            imgGloves.gameObject.SetActive(false);
            imgHelmet.gameObject.SetActive(false);
            imgBag.gameObject.SetActive(false);
            imgShoes.gameObject.SetActive(false);
            imgArmor.gameObject.SetActive(true);
            gItemInfo.SetActive(true);
        }
        else
        {
            txtReload.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtCritRateItem.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtCritDmgItem.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtDef.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtFirstAid.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtRegen.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtMoveSpeed.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtJump.gameObject.transform.parent.parent.gameObject.SetActive(false);


            gWeapon.SetActive(false);
            imgItemInfo.enabled = true;
            imgGloves.gameObject.SetActive(false);
            imgHelmet.gameObject.SetActive(false);
            imgBag.gameObject.SetActive(false);
            imgArmor.gameObject.SetActive(false);
            imgShoes.gameObject.SetActive(false);
            gItemInfo.SetActive(true);
            gItemInfo.SetActive(true);
        }
        gameObject.SetActive(true);
    }
    public void BuyMoreParts() {
        MainMenuController.Instance.HideAllPanel();
        MainMenuController.Instance.ShowShop();
    }
}
