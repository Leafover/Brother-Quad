using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public enum PACK_NAME { CHEAP_PACK, BEST_CHOICE, PROFESSIONAL_PACK, P_25GEM_PACK, P_220GEM_PACK, P_600GEM_PACK, P_1750GEM_PACK, P_4000GEM_PACK, P_12500GEM_PACK }
    public PACK_NAME packName;
    public Text txtPrice;
    public DataUtils.ITEM_SHOP_TYPE shopType;
    public Button btnInfo;
    public string packID;
    private Button btn;

    private void OnEnable()
    {
        btn = GetComponent<Button>();
        ProcessPackID();
    }

    private void ProcessPackID()
    {
        switch (packName)
        {
            case PACK_NAME.CHEAP_PACK:
                packID = DataUtils.P_CHEAP_PACK;
                break;
            case PACK_NAME.PROFESSIONAL_PACK:
                packID = DataUtils.P_PROFESSIONAL_PACK;
                break;
            case PACK_NAME.BEST_CHOICE:
                packID = DataUtils.P_BEST_CHOICE;
                break;
            case PACK_NAME.P_12500GEM_PACK:
                packID = DataUtils.P_12500GEM_PACK;
                break;
            case PACK_NAME.P_1750GEM_PACK:
                packID = DataUtils.P_1750GEM_PACK;
                break;
            case PACK_NAME.P_220GEM_PACK:
                packID = DataUtils.P_220GEM_PACK;
                break;
            case PACK_NAME.P_25GEM_PACK:
                packID = DataUtils.P_25GEM_PACK;
                break;
            case PACK_NAME.P_4000GEM_PACK:
                packID = DataUtils.P_4000GEM_PACK;
                break;
            case PACK_NAME.P_600GEM_PACK:
                packID = DataUtils.P_600GEM_PACK;
                break;
        }
        txtPrice.text = GameIAPManager.GetPriceByID(packID); //"BUY";
    }
    // Start is called before the first frame update
    void Start()
    {
        btn.onClick.AddListener(() =>
        {
            switch (shopType)
            {
                case DataUtils.ITEM_SHOP_TYPE.GEM:
                    GameIAPManager.Instance.BuyProduct(packID);
                    //ProcessBuyGem();
                    break;
                case DataUtils.ITEM_SHOP_TYPE.PACKAGE:
                    GameIAPManager.Instance.BuyProduct(packID);
                    //ProcessBuyPackage();
                    break;
                case DataUtils.ITEM_SHOP_TYPE.LUCKYCHEST:
                    ProcessBuyResources();
                    break;
            }
        });
        if (btnInfo != null) {
            btnInfo.onClick.AddListener(() =>
            {
                switch (shopType)
                {
                    case DataUtils.ITEM_SHOP_TYPE.GEM:
                        ProcessBuyGem();
                        break;
                    case DataUtils.ITEM_SHOP_TYPE.PACKAGE:
                        ProcessBuyPackage();
                        break;
                    case DataUtils.ITEM_SHOP_TYPE.LUCKYCHEST:
                        ProcessBuyResources();
                        break;
                }
            });
        }
    }

    private void ProcessBuyGem()
    {
        switch (packName)
        {
            case PACK_NAME.P_25GEM_PACK:
                ShopManager.Instance.ShowBuyPanel("Buy 25 Gem", GameIAPManager.GetPriceByID(packID), packID, 0, 25, 0);
                break;
            case PACK_NAME.P_220GEM_PACK:
                ShopManager.Instance.ShowBuyPanel("Buy 220 Gem", GameIAPManager.GetPriceByID(packID), packID, 0, 220, 0);
                break;
            case PACK_NAME.P_600GEM_PACK:
                ShopManager.Instance.ShowBuyPanel("Buy 600 Gem", GameIAPManager.GetPriceByID(packID), packID, 0, 600, 0);
                break;
            case PACK_NAME.P_1750GEM_PACK:
                ShopManager.Instance.ShowBuyPanel("Buy 1750 Gem", GameIAPManager.GetPriceByID(packID), packID, 0, 1750, 0);
                break;
            case PACK_NAME.P_4000GEM_PACK:
                ShopManager.Instance.ShowBuyPanel("Buy 4000 Gem", GameIAPManager.GetPriceByID(packID), packID, 0, 4000, 0);
                break;
            case PACK_NAME.P_12500GEM_PACK:
                ShopManager.Instance.ShowBuyPanel("Buy 12500 Gem", GameIAPManager.GetPriceByID(packID), packID, 0, 12500, 0);
                break;
        }
    }
    private void ProcessBuyPackage()
    {
        switch (packName)
        {
            case PACK_NAME.CHEAP_PACK:
                ShopManager.Instance.ShowBuyPanel("Beginner Pack", GameIAPManager.GetPriceByID(packID), packID, 0, 25, 7500);
                break;
            case PACK_NAME.PROFESSIONAL_PACK:
                ShopManager.Instance.ShowBuyPanel("Professional Pack", GameIAPManager.GetPriceByID(packID), packID, 50, 100, 85000);
                break;
            case PACK_NAME.BEST_CHOICE:
                ShopManager.Instance.ShowBuyPanel("Best Choice", GameIAPManager.GetPriceByID(packID), packID, 20, 50, 15000);
                break;
        }
    }
    public void ProcessBuyResources()
    {

    }
}