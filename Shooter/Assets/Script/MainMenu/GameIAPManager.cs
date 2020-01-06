using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class GameIAPManager : MonoBehaviour, IStoreListener
{
    public static GameIAPManager Instance;

    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

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
            //Debug.LogError("IAP::: " + product.id + " vs" + product.googlePrice + " vs " + product.type);
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
        Debug.LogError(e.purchasedProduct.definition.id);
        switch (e.purchasedProduct.definition.id)
        {
            case DataUtils.P_DONATE:
                Debug.LogError("Process Donate");
                DataUtils.RemoveAds();
                break;
            case DataUtils.P_STARTER_PACK:
                Debug.LogError("Process Active Starter Pack, unlock Kriss Vector(W2 Normal) +7500 Coins");
                DataUtils.RemoveAds();
                break;
        }
        return PurchaseProcessingResult.Complete;
    }
    #endregion
}
