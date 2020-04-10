using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowItemInfo : MonoBehaviour
{
    public Image imgItemInfo, imgGloves, imgHelmet, imgBag, imgShoes, imgArmor;
    public TextMeshProUGUI txtReloadTime, txtCritRate, txtCritDmg, txtDef, txtFirstAid, txtRegen, txtMoveSpeed, txtJump;

    private float _reloadTime, _critRate, _critDmg, _defensiveArmor, _firstAid, _regen, _moveSpeedArmor, _jumpHeight, _defHelmet, _moveSpeedShoes;
    string itemKey = "";
    int curStar = 0;
    private void FillData()
    {
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
    public void AA() {
        imgItemInfo.enabled = true;
        imgGloves.gameObject.SetActive(false);
        imgHelmet.gameObject.SetActive(false);
        imgBag.gameObject.SetActive(false);
        imgShoes.gameObject.SetActive(false);
        imgArmor.gameObject.SetActive(false);
    }
    public void ShowInfo(ItemData itemData)
    {
        itemKey = itemData.id + "_" + itemData.level;
        curStar = itemData.curStar < 5 ? itemData.curStar : 4;

        FillData();

        if (itemData.id.Contains("S"))//Shoes
        {

            txtMoveSpeed.text = "+" + _moveSpeedShoes + "%";
            txtJump.text = "+" + _jumpHeight + "%";

            //imgItemInfo, imgGloves, imgHelmet, imgBag, imgShoes, imgArmor;
            if (itemData.isEquipped && imgItemInfo != null) {
                imgItemInfo.enabled = false;
                imgGloves.gameObject.SetActive(false);
                imgHelmet.gameObject.SetActive(false);
                imgBag.gameObject.SetActive(false);
                imgShoes.gameObject.SetActive(true);
                imgArmor.gameObject.SetActive(false);
            }

            txtReloadTime.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtCritRate.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtCritDmg.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtDef.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtFirstAid.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtRegen.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtMoveSpeed.gameObject.transform.parent.parent.gameObject.SetActive(true);
            txtJump.gameObject.transform.parent.parent.gameObject.SetActive(true);
        }
        else if (itemData.id.Contains("B"))//Bag
        {

            txtFirstAid.text = "+" + _firstAid + "% HP";
            txtRegen.text = _regen + "% HP";

            if (itemData.isEquipped && imgItemInfo != null)
            {
                imgItemInfo.enabled = false;
                imgGloves.gameObject.SetActive(false);
                imgHelmet.gameObject.SetActive(false);
                imgBag.gameObject.SetActive(true);
                imgShoes.gameObject.SetActive(false);
                imgArmor.gameObject.SetActive(false);
            }

            txtReloadTime.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtCritRate.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtCritDmg.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtDef.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtFirstAid.gameObject.transform.parent.parent.gameObject.SetActive(true);
            txtRegen.gameObject.transform.parent.parent.gameObject.SetActive(true);
            txtMoveSpeed.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtJump.gameObject.transform.parent.parent.gameObject.SetActive(false);
        }
        else if (itemData.id.Contains("G"))//Gloves
        {

            txtReloadTime.text = "-" + _reloadTime + "%";
            txtCritRate.text = "" + _critRate;
            txtCritDmg.text = "" + _critDmg;

            if (itemData.isEquipped && imgItemInfo != null)
            {
                imgItemInfo.enabled = false;
                imgGloves.gameObject.SetActive(true);
                imgHelmet.gameObject.SetActive(false);
                imgBag.gameObject.SetActive(false);
                imgShoes.gameObject.SetActive(false);
                imgArmor.gameObject.SetActive(false);
            }

            txtReloadTime.gameObject.transform.parent.parent.gameObject.SetActive(true);
            txtCritRate.gameObject.transform.parent.parent.gameObject.SetActive(true);
            txtCritDmg.gameObject.transform.parent.parent.gameObject.SetActive(true);
            txtDef.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtFirstAid.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtRegen.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtMoveSpeed.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtJump.gameObject.transform.parent.parent.gameObject.SetActive(false);
        }
        else if (itemData.id.Contains("H"))//Helmet
        {

            txtDef.text = _defHelmet + "%";

            if (itemData.isEquipped && imgItemInfo != null)
            {
                imgItemInfo.enabled = false;
                imgGloves.gameObject.SetActive(false);
                imgHelmet.gameObject.SetActive(true);
                imgBag.gameObject.SetActive(false);
                imgShoes.gameObject.SetActive(false);
                imgArmor.gameObject.SetActive(false);
            }

            txtReloadTime.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtCritRate.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtCritDmg.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtDef.gameObject.transform.parent.parent.gameObject.SetActive(true);
            txtFirstAid.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtRegen.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtMoveSpeed.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtJump.gameObject.transform.parent.parent.gameObject.SetActive(false);
        }
        else if (itemData.id.Contains("A"))//Armor
        {

            txtDef.text = "+" + _defensiveArmor + "%";
            txtMoveSpeed.text = "-" + _moveSpeedArmor + "%";

            if (itemData.isEquipped && imgItemInfo != null)
            {
                imgItemInfo.enabled = false;
                imgGloves.gameObject.SetActive(false);
                imgHelmet.gameObject.SetActive(false);
                imgBag.gameObject.SetActive(false);
                imgShoes.gameObject.SetActive(false);
                imgArmor.gameObject.SetActive(true);
            }

            txtReloadTime.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtCritRate.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtCritDmg.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtDef.gameObject.transform.parent.parent.gameObject.SetActive(true);
            txtFirstAid.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtRegen.gameObject.transform.parent.parent.gameObject.SetActive(false);
            txtMoveSpeed.gameObject.transform.parent.parent.gameObject.SetActive(true);
            txtJump.gameObject.transform.parent.parent.gameObject.SetActive(false);
        }

        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
