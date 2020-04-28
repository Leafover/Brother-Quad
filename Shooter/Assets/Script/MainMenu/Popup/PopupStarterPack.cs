using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupStarterPack : MonoBehaviour
{
    //public Text txtWPName, txtAtk, txtBulletSpeed, txtReload, txtRange, txtMagazine, txtPrice;

    public TextMeshProUGUI tmpWPName;
    public Text txtDmg, txtFireRate, txtCritRate, txtCritDmg, txtRange, txtMagazine, txtPrice;
    
    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    void Start()
    {
        InitWeaponInfo();
        if(GameIAPManager.GetPriceByID(DataUtils.P_STARTER_PACK) != null)
            txtPrice.text = GameIAPManager.GetPriceByID(DataUtils.P_STARTER_PACK);
    }

    private void ShowLog(string mess)
    {
        Debug.LogError("TAGG: " + mess);
    }
    private void InitWeaponInfo()
    {
        var _wp = DataController.instance.allWeapon[6].weaponList[0];

        ShowLog(_wp == null ? "WP NULL" : "WP NOT NULLL");
        ShowLog("ItemName: " + _wp.NAME + " vs " + _wp.DmgValue[0] + " vs " + _wp.AtksecValue[0] + " vs " + _wp.CritRateValue[0]);
        tmpWPName.text = "<color=#5DADE2>" + _wp.NAME+"</color>";
        txtDmg.text = DataUtils.GetRealFloat(_wp.DmgValue[0]*10); //_wp.DmgValue[0].ToString("0.##");
        txtFireRate.text = DataUtils.GetRealFloat(_wp.AtksecValue[0]) + "s";
        txtCritRate.text = DataUtils.GetRealFloat(_wp.CritRateValue[0])+"%";
        txtCritDmg.text = DataUtils.GetRealFloat(_wp.CritDmgValue[0])+"%";
        txtRange.text = DataUtils.GetRealFloat(_wp.AtkRangeValue[0]);
        txtMagazine.text = DataUtils.GetRealFloat(_wp.MagazineValue[0]);
    }
    public void BuyPack()
    {
        GameIAPManager.Instance.BuyProduct(DataUtils.P_STARTER_PACK);
        ClosePopup();
    }
    
    public void ClosePopup()
    {
        MainMenuController.Instance.popManager.pType = PopupManager.POPUP_TYPE.NONE;
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
