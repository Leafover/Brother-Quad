using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    public Button[] btnTabs;
    public Sprite sprSelect, sprUnSelect;
    public GameObject gPanelBuy;
    public TextMeshProUGUI txtPacktitle;
    public Text txtPackPrice;
    public Text txtPlayerPice, txtGem, txtCoin, txtHPPack;
    public Button btnBuyPack;
    public Transform trShopContain;
    public ShopItem[] shopItems;

    public GameObject TabLuckyChest;

    private Button btnPanelBuy;
    private string _packID;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        btnPanelBuy = gPanelBuy.GetComponent<Button>();
        btnPanelBuy.onClick.AddListener(() => {
            gPanelBuy.SetActive(false);
        });

        btnBuyPack.onClick.AddListener(() =>
        {
            //GameIAPManager.Instance.BuyProduct(_packID);
            gPanelBuy.SetActive(false);
        });
    }
    private void OnEnable()
    {
        ChooseTab(0);
    }

    public void ChooseTab(int _index)
    {
        for (int i = 0; i < btnTabs.Length; i++)
        {
            if (i == _index)
            {
                btnTabs[i].image.sprite = sprSelect;
            }
            else btnTabs[i].image.sprite = sprUnSelect;
        }
        switch (_index)
        {
            case 0://Package
                trShopContain.GetChild(0).gameObject.SetActive(true);
                ShowShopItem(DataUtils.ITEM_SHOP_TYPE.PACKAGE);
                TabLuckyChest.SetActive(false);
                break;
            case 1://Gem
                trShopContain.GetChild(0).gameObject.SetActive(false);
                ShowShopItem(DataUtils.ITEM_SHOP_TYPE.GEM);
                TabLuckyChest.SetActive(false);
                break;
            case 2://Luck Chest
                trShopContain.GetChild(0).gameObject.SetActive(false);
                ShowShopItem(DataUtils.ITEM_SHOP_TYPE.LUCKYCHEST);
                TabLuckyChest.SetActive(true);
                break;
        }
    }
    private void ShowShopItem(DataUtils.ITEM_SHOP_TYPE shopType)
    {
        foreach (ShopItem shopItem in shopItems)
        {
            if (shopItem.shopType == shopType)
            {
                shopItem.gameObject.SetActive(true);
            }
            else
            {
                shopItem.gameObject.SetActive(false);
            }
        }
    }
    public void WatchVideo()
    {
        Debug.LogError("Watch Video to get 200 coins");
        if (Application.platform == RuntimePlatform.Android)
        {
            AdsManager.Instance.ShowRewardedVideo((b) =>
            {
                if (b)
                {
                    DataUtils.AddCoinAndGame(200, 0);
                }

            });
        }
        else
        {
            Debug.LogError("1111");
            DataUtils.AddCoinAndGame(200, 0);
        }
    }

    public void ShowBuyPanel(string packTitle, string priceText, string packID, int totalPlayerPice, int totalGem, int totalCoin, int totalHpPack)
    {
        _packID = packID;
        txtPacktitle.text = packTitle;
        txtCoin.text = "x " + totalCoin;
        txtGem.text = "x " + totalGem;
        txtPlayerPice.text = "x " + totalPlayerPice;
        txtHPPack.text = "x " + totalHpPack;
        if (totalHpPack == 0)
        {
            txtHPPack.gameObject.transform.parent.parent.gameObject.SetActive(false);
        }
        else
        {
            txtHPPack.gameObject.transform.parent.parent.gameObject.SetActive(true);
        }

        if (totalPlayerPice == 0)
        {
            txtPlayerPice.gameObject.transform.parent.parent.gameObject.SetActive(false);
        }
        else
        {
            txtPlayerPice.gameObject.transform.parent.parent.gameObject.SetActive(true);
        }

        if (totalGem == 0)
        {
            txtGem.gameObject.transform.parent.parent.gameObject.SetActive(false);
        }
        else
        {
            txtGem.gameObject.transform.parent.parent.gameObject.SetActive(true);
        }

        if (totalCoin == 0)
        {
            txtCoin.gameObject.transform.parent.parent.gameObject.SetActive(false);
        }
        else
        {
            txtCoin.gameObject.transform.parent.parent.gameObject.SetActive(true);
        }

        //txtPackPrice.text = priceText;
        gPanelBuy.SetActive(true);
    }
    public void HideShop()
    {
        gameObject.SetActive(false);
    }
}
