using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Purchasing;

public class GameIAPManager : MonoBehaviour, IStoreListener
{
    public static GameIAPManager Instance;

    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    public Action acBuyComplete;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            InitIAP();
        }
    }

    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public static string GetPriceByID(string _id)
    {
        Product product = m_StoreController.products.WithID(_id);
        return product.metadata.localizedPriceString;
    }
    public void BuyProduct(string productID)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productID);

            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }
    public void BuyDonate()
    {
        BuyProduct(DataUtils.P_STARTER_PACK);
    }
    public void BuyStarterPack()
    {
        BuyProduct(DataUtils.P_DONATE);
    }

    private void InitIAP()
    {
        var module = StandardPurchasingModule.Instance();
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);

        var catalog = ProductCatalog.LoadDefaultCatalog();
        foreach (var product in catalog.allValidProducts)
        {
            if (product.allStoreIDs.Count > 0)
            {
                var ids = new IDs();
                foreach (var storeID in product.allStoreIDs)
                {
                    ids.Add(storeID.id, storeID.store);
                }
                builder.AddProduct(product.id, product.type, ids);
            }
            else
            {
                builder.AddProduct(product.id, product.type);
            }
        }


        UnityPurchasing.Initialize(this, builder);
    }

    #region IAP Listener
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("Billing failed to initialize!");
        switch (error)
        {
            case InitializationFailureReason.AppNotKnown:
                Debug.Log("Is your App correctly uploaded on the relevant publisher console?");
                break;
            case InitializationFailureReason.PurchasingUnavailable:
                Debug.Log("Billing disabled!");
                break;
            case InitializationFailureReason.NoProductsAvailable:
                Debug.Log("No products available for purchase!");
                break;
        }
    }

    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        if(acBuyComplete != null)
        {
            acBuyComplete();
        }
        switch (e.purchasedProduct.definition.id)
        {
            case DataUtils.P_DONATE:
                Debug.LogError("Process Donate");
                DataUtils.RemoveAds();
                MyAnalytics.LogEventBuyInapp("donate_pack");
                break;
            case DataUtils.P_STARTER_PACK:
                Debug.LogError("P_STARTER_PACK");
                DataUtils.AddCoinAndGame(7500, 0);
                DataUtils.AddHPPack(5);
                MyAnalytics.LogEventBuyInapp("starter_pack");
                MyAnalytics.LogBuyStarterPack();
                DataUtils.TakeItem("W7", DataUtils.eType.WEAPON, DataUtils.eLevel.Normal, 10, true);
                break;
            case DataUtils.P_CHEAP_PACK:
                DataUtils.AddCoinAndGame(7500, 25);
                DataUtils.AddHPPack(10);
                MyAnalytics.LogEventBuyInapp("cheap_pack");
                break;
            case DataUtils.P_BEST_CHOICE:
                DataUtils.AddCoinAndGame(15000, 50);
                DataUtils.AddHPPack(25);
                DataUtils.TakeHeroPice("P1", 20);
                MyAnalytics.LogEventBuyInapp("best_choice_pack");
                break;
            case DataUtils.P_PROFESSIONAL_PACK:
                DataUtils.AddCoinAndGame(85000, 100);
                DataUtils.AddHPPack(45);
                DataUtils.TakeHeroPice("P1", 50);
                DataUtils.RemoveAds();
                MyAnalytics.LogEventBuyInapp("professional_pack");
                break;
            case DataUtils.P_12500GEM_PACK:
                DataUtils.AddCoinAndGame(0, 12500);
                MyAnalytics.LogEventBuyInapp("pack_12500_gems");
                break;
            case DataUtils.P_1750GEM_PACK:
                DataUtils.AddCoinAndGame(0, 1750);
                MyAnalytics.LogEventBuyInapp("pack_1750_gems");
                break;
            case DataUtils.P_220GEM_PACK:
                DataUtils.AddCoinAndGame(0, 220);
                MyAnalytics.LogEventBuyInapp("pack_220_gems");
                break;
            case DataUtils.P_25GEM_PACK:
                DataUtils.AddCoinAndGame(0, 25);
                MyAnalytics.LogEventBuyInapp("pack_25_gems");
                break;
            case DataUtils.P_4000GEM_PACK:
                DataUtils.AddCoinAndGame(0, 4000);
                MyAnalytics.LogEventBuyInapp("pack_4000_gems");
                break;
            case DataUtils.P_600GEM_PACK:
                DataUtils.AddCoinAndGame(0, 600);
                MyAnalytics.LogEventBuyInapp("pack_600_gems");
                break;
        }
        return PurchaseProcessingResult.Complete;
    }
    #endregion
}
