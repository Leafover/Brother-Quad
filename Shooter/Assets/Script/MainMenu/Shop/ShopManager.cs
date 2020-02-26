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
    public Text txtPlayerPice, txtGem, txtCoin;
    public Button btnBuyPack;

    private Button btnPanelBuy;
    private string _packID;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        btnPanelBuy = gPanelBuy.GetComponent<Button>();
        btnPanelBuy.onClick.AddListener(() => { gPanelBuy.SetActive(false); });

        btnBuyPack.onClick.AddListener(() =>
        {
            GameIAPManager.Instance.BuyProduct(_packID);
            gPanelBuy.SetActive(false);
        });
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

    public void ShowBuyPanel(string packTitle, string priceText, string packID, int totalPlayerPice, int totalGem, int totalCoin)
    {
        _packID = packID;
        txtPacktitle.text = packTitle;
        txtCoin.text = "x " + totalCoin;
        txtGem.text = "x " + totalGem;
        txtPlayerPice.text = "x " + totalPlayerPice;
        if (totalPlayerPice == 0)
        {
            txtPlayerPice.gameObject.transform.parent.parent.gameObject.SetActive(false);
        }
        else
        {
            txtPlayerPice.gameObject.transform.parent.parent.gameObject.SetActive(true);
        }
        txtPackPrice.text = priceText;
        gPanelBuy.SetActive(true);
    }
    public void HideShop()
    {
        gameObject.SetActive(false);
    }
}
