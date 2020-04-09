using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BouderBlackmarket : MonoBehaviour
{
    public Text priceText, numberText;
    public Image bouderImg, iconItemImg, iconCoinImg, btnBuyImg;
    public int index;


    public Button btnBounder;
    private string _keyItemSelected = "";
    private void OnEnable()
    {
        //RefreshTypeAndLevel();
        //_keyItemSelected = DataController.blackMarketSave[index].ID + "_" + level.ToString();
    }
    private void RefreshTypeAndLevel()
    {
        if (DataController.blackMarketSave[index].ID.Contains("W"))
        {
            type = DataUtils.eType.WEAPON;
        }
        else if (DataController.blackMarketSave[index].ID.Contains("A"))
        {
            type = DataUtils.eType.ARMOR;
        }
        else if (DataController.blackMarketSave[index].ID.Contains("H"))
        {
            type = DataUtils.eType.HELMET;
        }
        else if (DataController.blackMarketSave[index].ID.Contains("S"))
        {
            type = DataUtils.eType.SHOES;
        }
        else if (DataController.blackMarketSave[index].ID.Contains("G"))
        {
            type = DataUtils.eType.GLOVES;
        }
        else if (DataController.blackMarketSave[index].ID.Contains("B"))
        {
            type = DataUtils.eType.BAG;
        }
        switch (DataController.blackMarketSave[index].Level)
        {
            case "Normal":
                level = DataUtils.eLevel.Normal;
                break;
            case "Uncommon":
                level = DataUtils.eLevel.Uncommon;
                break;
            case "Rare":
                level = DataUtils.eLevel.Rare;
                break;
            case "Epic":
                level = DataUtils.eLevel.Epic;
                break;
            case "Legendary":
                level = DataUtils.eLevel.Legendary;
                break;
        }
        _keyItemSelected = DataController.blackMarketSave[index].ID + "_" + level.ToString();
    }
    private void Start()
    {
        btnBounder.onClick.AddListener(() =>
        {
            Debug.LogError("--<> " + _keyItemSelected + " vs " + DataController.blackMarketSave[index].ID + " vs " + level);
            MainMenuController.Instance.pItemInfo.ShowItemInfo(DataController.blackMarketSave[index].ID, level.ToString(), type.ToString(), false);
        });
    }



    void DisplayAgainWhenBuy()
    {
        numberText.text = "" + DataController.blackMarketSave[index].countnumber;
        if(DataController.blackMarketSave[index].countnumber == 0)
        {
            iconCoinImg.color = btnBuyImg.color = priceText.color = Color.gray;      
        }
        else
        {
            iconCoinImg.color = btnBuyImg.color = priceText.color = Color.white;
        }
    }
    public void DisplayItem()
    {
        priceText.text = "" + DataController.blackMarketSave[index].GiaBanCoin.ToString("#,0");
        DisplayAgainWhenBuy();
        switch (DataController.blackMarketSave[index].Level)
        {
            case "Normal":
                bouderImg.sprite = MenuController.instance.blackMarketpanel.levelSp[0];
                break;
            case "Uncommon":
                bouderImg.sprite = MenuController.instance.blackMarketpanel.levelSp[1];
                break;
            case "Rare":
                bouderImg.sprite = MenuController.instance.blackMarketpanel.levelSp[2];
                break;
            case "Epic":
                bouderImg.sprite = MenuController.instance.blackMarketpanel.levelSp[3];
                break;
            case "Legendary":
                bouderImg.sprite = MenuController.instance.blackMarketpanel.levelSp[4];
                break;
        }
        iconItemImg.sprite = DataUtils.dicSpriteData[DataController.blackMarketSave[index].ID];

        RefreshTypeAndLevel();
    }
    DataUtils.eType type;
    DataUtils.eLevel level;
    public void Buy()
    {
        if (DataUtils.playerInfo.coins >= DataController.blackMarketSave[index].GiaBanCoin)
        {
            if (DataController.blackMarketSave[index].countnumber > 0)
            {
                //if (DataController.blackMarketSave[index].ID.Contains("W"))
                //{
                //    type = DataUtils.eType.WEAPON;
                //}
                //else if (DataController.blackMarketSave[index].ID.Contains("A"))
                //{
                //    type = DataUtils.eType.ARMOR;
                //}
                //else if (DataController.blackMarketSave[index].ID.Contains("H"))
                //{
                //    type = DataUtils.eType.HELMET;
                //}
                //else if (DataController.blackMarketSave[index].ID.Contains("S"))
                //{
                //    type = DataUtils.eType.SHOES;
                //}
                //else if (DataController.blackMarketSave[index].ID.Contains("G"))
                //{
                //    type = DataUtils.eType.GLOVES;
                //}
                //else if (DataController.blackMarketSave[index].ID.Contains("B"))
                //{
                //    type = DataUtils.eType.BAG;
                //}
                //switch(DataController.blackMarketSave[index].Level)
                //{
                //    case "Normal":
                //        level = DataUtils.eLevel.Normal;
                //        break;
                //    case "Uncommon":
                //        level = DataUtils.eLevel.Uncommon;
                //        break;
                //    case "Rare":
                //        level = DataUtils.eLevel.Rare;
                //        break;
                //    case "Epic":
                //        level = DataUtils.eLevel.Epic;
                //        break;
                //    case "Legendary":
                //        level = DataUtils.eLevel.Legendary;
                //        break;
                //}

                DataUtils.TakeItem(DataController.blackMarketSave[index].ID, type, level, 1, false);          
                DataUtils.AddCoinAndGame(-(int)DataController.blackMarketSave[index].GiaBanCoin, 0);
                DataController.blackMarketSave[index].countnumber--;
                DisplayAgainWhenBuy();
                MyAnalytics.LogBuyInBlackMarket();
                MenuController.instance.blackMarketpanel.DisplayConfirm(bouderImg.sprite,iconItemImg.sprite, DataUtils.GetItemName(type, DataController.blackMarketSave[index].ID));
            }
        }
        else
        {
            MainMenuController.Instance.shopManager.gameObject.SetActive(true);
        }
    }
}
