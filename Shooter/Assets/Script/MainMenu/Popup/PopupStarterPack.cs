using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupStarterPack : MonoBehaviour
{
    public Text txtWPName, txtAtk, txtBulletSpeed, txtReload, txtRange, txtMagazine, txtPrice;

    
    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        GameIAPManager.Instance.acBuyComplete -= HandleBuyComplete;
    }

    void Start()
    {
        GameIAPManager.Instance.acBuyComplete += HandleBuyComplete;
        txtPrice.text = GameIAPManager.GetPriceByID(DataUtils.P_STARTER_PACK);
        InitWeaponInfo();
    }


    private void InitWeaponInfo()
    {
        var _wp = DataController.instance.allWeapon[1].weaponList[0];
        txtWPName.text = _wp.NAME;
        txtAtk.text = _wp.Dmg.ToString();
        txtBulletSpeed.text = _wp.BulletSpeed.ToString();
        txtReload.text = _wp.ReloadSpeed.ToString();
        txtRange.text = _wp.AtkRange.ToString();
        txtMagazine.text = _wp.Magazine.ToString();
    }
    void HandleBuyComplete()
    {
        Debug.LogError("Active Starter Pack, unlock Kriss Vector(W2 Normal) +7500 Coins");
        DataUtils.RemoveAds();
        ClosePopup();
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
