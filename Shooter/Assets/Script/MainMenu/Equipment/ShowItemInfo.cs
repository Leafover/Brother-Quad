﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowItemInfo : MonoBehaviour
{
    public bool isSelected;
    public Image imgItemInfo, imgGloves, imgHelmet, imgBag, imgShoes, imgArmor;
    public TextMeshProUGUI txtReloadTime, txtCritRate, txtCritDmg, txtDef, txtFirstAid, txtRegen, txtMoveSpeed, txtJump;

    private float _reloadTime, _critRate, _critDmg, _defensiveArmor, _firstAid, _regen, _moveSpeedArmor, _jumpHeight, _defHelmet, _moveSpeedShoes;
    private float _nextReloadTime, _nextCritRate, _nextCritDmg, _nextDefensiveArmor, _nextFirstAid, _nextRegen, _nextMoveSpeedArmor, _nextJumpHeight, _nextDefHelmet, _nextMoveSpeedShoes;

    string itemKey = "", keyNextLevel="";
    int curStar = 0;
    private void FillData()
    {
        if (DataUtils.dicArmor.ContainsKey(itemKey))
        {
            _defensiveArmor = DataUtils.dicArmor[itemKey].DefValue[curStar];//Armor
            _nextDefensiveArmor = curStar + 1 < DataUtils.dicArmor[itemKey].DefValue.Count ? DataUtils.dicArmor[itemKey].DefValue[curStar + 1] : DataUtils.dicArmor[keyNextLevel].DefValue[0];

            _moveSpeedArmor = DataUtils.dicArmor[itemKey].SpeedTruValue[curStar];//Armor
            _nextMoveSpeedArmor = curStar + 1 < DataUtils.dicArmor[itemKey].SpeedTruValue.Count ? DataUtils.dicArmor[itemKey].SpeedTruValue[curStar + 1] : DataUtils.dicArmor[keyNextLevel].SpeedTruValue[0];
        }
        if (DataUtils.dicBag.ContainsKey(itemKey))
        {
            //_regen = DataUtils.dicBag[itemKey].HealthRegenerationValue[curStar >= DataUtils.dicBag[itemKey].HealthRegenerationValue.Count ? DataUtils.dicBag[itemKey].HealthRegenerationValue.Count - 1 : curStar];//Bag
            //_firstAid = DataUtils.dicBag[itemKey].BonussoluongmauanduocValue[curStar >= DataUtils.dicBag[itemKey].HealthRegenerationValue.Count ? DataUtils.dicBag[itemKey].HealthRegenerationValue.Count - 1 : curStar];//Bag
            _regen = DataUtils.dicBag[itemKey].HealthRegenerationValue[curStar];
            _nextRegen= curStar + 1 < DataUtils.dicBag[itemKey].HealthRegenerationValue.Count ? DataUtils.dicBag[itemKey].HealthRegenerationValue[curStar + 1] : DataUtils.dicBag[keyNextLevel].HealthRegenerationValue[0];

            _firstAid = DataUtils.dicBag[itemKey].BonussoluongmauanduocValue[curStar];
            _nextFirstAid = curStar + 1 < DataUtils.dicBag[itemKey].BonussoluongmauanduocValue.Count ? DataUtils.dicBag[itemKey].BonussoluongmauanduocValue[curStar + 1] : DataUtils.dicBag[keyNextLevel].BonussoluongmauanduocValue[0];
        }
        if (DataUtils.dicGloves.ContainsKey(itemKey))
        {
            _reloadTime = DataUtils.dicGloves[itemKey].GiamtimereloadValue[curStar];//Gloves
            _nextReloadTime = curStar + 1 < DataUtils.dicGloves[itemKey].GiamtimereloadValue.Count ? DataUtils.dicGloves[itemKey].GiamtimereloadValue[curStar + 1] : DataUtils.dicGloves[keyNextLevel].GiamtimereloadValue[0];

            _critRate = DataUtils.dicGloves[itemKey].tangcritrateValue[curStar];//Gloves
            _nextCritRate = curStar + 1 < DataUtils.dicGloves[itemKey].tangcritrateValue.Count ? DataUtils.dicGloves[itemKey].tangcritrateValue[curStar + 1] : DataUtils.dicGloves[keyNextLevel].tangcritrateValue[0];

            _critDmg = DataUtils.dicGloves[itemKey].TangcritdmgValue[curStar];//Gloves
            _nextCritDmg = curStar + 1 < DataUtils.dicGloves[itemKey].TangcritdmgValue.Count ? DataUtils.dicGloves[itemKey].TangcritdmgValue[curStar + 1] : DataUtils.dicGloves[keyNextLevel].TangcritdmgValue[0];
        }
        if (DataUtils.dicHelmet.ContainsKey(itemKey))
        {
            _defHelmet = DataUtils.dicHelmet[itemKey].DefValue[curStar];//Helmet
            _nextDefHelmet = curStar + 1 < DataUtils.dicHelmet[itemKey].DefValue.Count ? DataUtils.dicHelmet[itemKey].DefValue[curStar + 1] : DataUtils.dicHelmet[keyNextLevel].DefValue[0];
        }
        if (DataUtils.dicShoes.ContainsKey(itemKey))
        {
            _moveSpeedShoes = DataUtils.dicShoes[itemKey].TangSpeeDichuyenValue[curStar];//Shoes
            _nextMoveSpeedShoes = curStar + 1 < DataUtils.dicShoes[itemKey].TangSpeeDichuyenValue.Count ? DataUtils.dicShoes[itemKey].TangSpeeDichuyenValue[curStar + 1] : DataUtils.dicShoes[keyNextLevel].TangSpeeDichuyenValue[0];

            _jumpHeight = DataUtils.dicShoes[itemKey].TangDoCaoNhayValue[curStar];//Shoes
            _nextJumpHeight = curStar + 1 < DataUtils.dicShoes[itemKey].TangDoCaoNhayValue.Count ? DataUtils.dicShoes[itemKey].TangDoCaoNhayValue[curStar + 1] : DataUtils.dicShoes[keyNextLevel].TangDoCaoNhayValue[0];
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
        keyNextLevel = itemData.id + "_" + DataUtils.GetNextLevelString(itemData.level);
        curStar = itemData.curStar < 5 ? itemData.curStar : 4;

        FillData();

        if (itemData.id.Contains("S"))//Shoes
        {

            txtMoveSpeed.text = "+" + _moveSpeedShoes + "%" + (DataUtils.IsCanEvolve(itemData)&& isSelected && itemData.isUnlock&&(_nextMoveSpeedShoes!=_moveSpeedShoes) ? "<sprite=1><color=green>+" + _nextMoveSpeedShoes + "%" : "");
            txtJump.text = "+" + _jumpHeight + "%" + (DataUtils.IsCanEvolve(itemData) && isSelected && itemData.isUnlock &&(_nextJumpHeight != _jumpHeight) ? "<sprite=1><color=green>+" + _nextJumpHeight + "%" : "");
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

            txtFirstAid.text = "+" + _firstAid + (DataUtils.IsCanEvolve(itemData) && isSelected && itemData.isUnlock && _nextFirstAid  != _firstAid? "<sprite=1><color=green>+" + _nextFirstAid : "") + "% HP";
            txtRegen.text = _regen + (DataUtils.IsCanEvolve(itemData) && isSelected && itemData.isUnlock && _nextRegen  != _regen? "<sprite=1><color=green>" + _nextRegen  : "") + "% HP";

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

            txtReloadTime.text = "-" + _reloadTime + "%" + (DataUtils.IsCanEvolve(itemData) && isSelected && itemData.isUnlock && _nextReloadTime  != _reloadTime? "<sprite=1><color=green>-" + _nextReloadTime + "%" : "");
            txtCritRate.text = _critRate + "%" + (DataUtils.IsCanEvolve(itemData) && isSelected && itemData.isUnlock&& _nextCritRate!= _critRate ? "<sprite=1><color=green>" + _nextCritRate + "%" : "");
            txtCritDmg.text = _critDmg + "%" + (DataUtils.IsCanEvolve(itemData) && isSelected && itemData.isUnlock && _nextCritDmg  != _critDmg? "<sprite=1><color=green>" + _nextCritDmg + "%" : "");

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

            txtDef.text = _defHelmet + "%" + (DataUtils.IsCanEvolve(itemData) && isSelected && itemData.isUnlock && _nextDefHelmet != _defHelmet? "<sprite=1><color=green>" + _nextDefHelmet + "%" : "");

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

            txtDef.text = "+" + _defensiveArmor + "%" + (DataUtils.IsCanEvolve(itemData) && isSelected && itemData.isUnlock&& _nextDefensiveArmor!= _defensiveArmor ? "<sprite=1><color=green>+" + _nextDefensiveArmor + "%" : "");
            txtMoveSpeed.text = "-" + _moveSpeedArmor + "%" + (DataUtils.IsCanEvolve(itemData) && isSelected && itemData.isUnlock && _nextMoveSpeedArmor != _moveSpeedArmor? "<sprite=1><color=green>-" + _nextMoveSpeedArmor + "%" : "");

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
