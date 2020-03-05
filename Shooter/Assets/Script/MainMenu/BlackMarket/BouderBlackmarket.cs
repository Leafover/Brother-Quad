using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BouderBlackmarket : MonoBehaviour
{
    public BlackMarketPanel myMarket;
    public Text priceText, numberText;
    public Image bouderImg, iconItemImg, iconCoinImg, btnBuyImg;
    public int index;
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
        priceText.text = "" + (int)DataController.blackMarketSave[index].GiaBanCoin;
        DisplayAgainWhenBuy();
        switch (DataController.blackMarketSave[index].Level)
        {
            case "Normal":
                bouderImg.sprite = myMarket.levelSp[0];
                break;
            case "Uncommon":
                bouderImg.sprite = myMarket.levelSp[1];
                break;
            case "Rare":
                bouderImg.sprite = myMarket.levelSp[2];
                break;
            case "Epic":
                bouderImg.sprite = myMarket.levelSp[3];
                break;
            case "Legendary":
                bouderImg.sprite = myMarket.levelSp[4];
                break;
        }
        iconItemImg.sprite = DataUtils.dicSpriteData[DataController.blackMarketSave[index].ID];
    }
    public void Buy()
    {
        if (DataUtils.playerInfo.coins >= DataController.blackMarketSave[index].GiaBanCoin)
        {
            if (DataController.blackMarketSave[index].countnumber > 0)
            {
                DataUtils.AddCoinAndGame(-(int)DataController.blackMarketSave[index].GiaBanCoin, 0);
                DataController.blackMarketSave[index].countnumber--;
                DisplayAgainWhenBuy();
            }
        }
    }
}
