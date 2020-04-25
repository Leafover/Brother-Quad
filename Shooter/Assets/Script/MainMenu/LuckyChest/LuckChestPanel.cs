using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LuckChestPanel : MonoBehaviour
{
    public int[] prices;
    public Text[] priceText, numberTakeText,packText;
    public Image[] rewardImgs, bouderImgs, iconImgs;
    public GameObject PanelConfirmAfterBuy, PanelInfoLuckyChest;

    public Image /*iconChestInfo,*/ iconPriceInfo;
    public Text priceTextInfo, nameItemText;

    public ScrollRect sc;
    public GameObject[] grids;

    DataUtils.eLevel elevel;
    DataUtils.eType etype;

    private void Start()
    {
        for (int i = 0; i < priceText.Length; i++)
        {
            priceText[i].text = "" + prices[i].ToString("#,0");
        }
    }
    int index;
    public void BtnClickShowPanelInfoLuckyChest(int _index)
    {

        index = _index;

        if (index == 0)
        {
            iconPriceInfo.sprite = MenuController.instance.achievementAndDailyQuestPanel.rewardSps[1];
        }
        else
        {
            iconPriceInfo.sprite = MenuController.instance.achievementAndDailyQuestPanel.rewardSps[0];
        }

        priceTextInfo.text = "" + prices[index].ToString("#,0");
      //  iconChestInfo.sprite = iconImgs[index].sprite;
        nameItemText.text = packText[index].text;
        grids[index].SetActive(true);
        sc.content = grids[index].GetComponent<RectTransform>();

        PanelInfoLuckyChest.SetActive(true);

        sc.verticalNormalizedPosition = 1;

        SoundController.instance.PlaySound(soundGame.soundbtnclick);
    }
    public void BtnBuy()
    {
        if (index == 0)
        {
            if (DataUtils.playerInfo.coins >= prices[index])
            {
                DataUtils.AddCoinAndGame(-prices[index], 0);
                Calculate();
                MyAnalytics.LogOpenLuckyChest("class_chest");
            }
            else
            {
                MainMenuController.Instance.shopManager.ChooseTab(0);
            }
        }
        else
        {
            if (DataUtils.playerInfo.gems >= prices[index])
            {
                DataUtils.AddCoinAndGame(0, -prices[index]);
                Calculate();
                switch (index) {
                    case 1:
                        MyAnalytics.LogOpenLuckyChest("rare_chest");
                        break;
                    case 2:
                        MyAnalytics.LogOpenLuckyChest("high_class_chest");
                        break;
                    case 3:
                        MyAnalytics.LogOpenLuckyChest("legend_chest");
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MainMenuController.Instance.shopManager.ChooseTab(1);
            }
        }
        CloseInfoLuckyChest();
        SoundController.instance.PlaySound(soundGame.soundbtnclick);
    }
    int randomsIndexName, randomsLevel, total1, total2, total3, total4, total5, total6, totalTake;
    string nameItem, nameIndexItem;
    void Calculate()
    {
        total1 = (int)DataController.instance.tilemanhquay[DataController.levelOfLuckChest[index]].item1;
        total2 = (int)DataController.instance.tilemanhquay[DataController.levelOfLuckChest[index]].item2 + total1;
        total3 = (int)DataController.instance.tilemanhquay[DataController.levelOfLuckChest[index]].item3 + total2;
        total4 = (int)DataController.instance.tilemanhquay[DataController.levelOfLuckChest[index]].item4 + total3;
        total5 = (int)DataController.instance.tilemanhquay[DataController.levelOfLuckChest[index]].item5 + total4;
        total6 = (int)DataController.instance.tilemanhquay[DataController.levelOfLuckChest[index]].item6 + total5;

        switch (index)
        {
            case 0:
                randomsLevel = Random.Range(0, 100);
                if (randomsLevel < 70)
                {
                    elevel = DataUtils.eLevel.Normal;
                }
                else
                {
                    elevel = DataUtils.eLevel.Uncommon;
                }
                totalTake = 3;
                break;
            case 1:
                elevel = DataUtils.eLevel.Rare;
                totalTake = 3;
                break;
            case 2:
                elevel = DataUtils.eLevel.Epic;
                totalTake = 5;
                break;
            case 3:
                elevel = DataUtils.eLevel.Legendary;
                totalTake = 5;
                break;
        }

        for (int i = 0; i < 6; i++)
        {
            randomsIndexName = Random.Range(0, 100);
            //      Debug.LogError("random index name:" + randomsIndexName);
            if (randomsIndexName >= 0 && randomsIndexName < total1)
            {
                nameIndexItem = "1";
            }
            else if (randomsIndexName >= total1 && randomsIndexName < total2)
            {
                nameIndexItem = "2";
            }
            else if (randomsIndexName >= total2 && randomsIndexName < total3)
            {
                nameIndexItem = "3";
            }
            else if (randomsIndexName >= total3 && randomsIndexName < total4)
            {
                nameIndexItem = "4";
            }
            else if (randomsIndexName >= total4 && randomsIndexName < total5)
            {
                nameIndexItem = "5";
            }
            else if (randomsIndexName >= total5 && randomsIndexName < total6)
            {
                nameIndexItem = "6";
            }
            switch (i)
            {
                case 0:
                    nameItem = "W";
                    etype = DataUtils.eType.WEAPON;
                    break;
                case 1:
                    nameItem = "A";
                    etype = DataUtils.eType.ARMOR;
                    break;
                case 2:
                    nameItem = "H";
                    etype = DataUtils.eType.HELMET;
                    break;
                case 3:
                    nameItem = "S";
                    etype = DataUtils.eType.SHOES;
                    break;
                case 4:
                    nameItem = "G";
                    etype = DataUtils.eType.GLOVES;
                    break;
                case 5:
                    nameItem = "B";
                    etype = DataUtils.eType.BAG;
                    break;

            }
            //     Debug.LogError(nameItem + nameIndexItem);
            numberTakeText[i].text = "" + totalTake;
            bouderImgs[i].sprite = MenuController.instance.blackMarketpanel.levelSp[(int)elevel];
            rewardImgs[i].sprite = DataUtils.dicSpriteData[nameItem + nameIndexItem];
            DataUtils.TakeItem(nameItem + nameIndexItem, etype, elevel, totalTake, false);
        }

        if (DataController.levelOfLuckChest[index] < DataController.instance.tilemanhquay.Count - 1)
            DataController.levelOfLuckChest[index]++;


        MenuController.instance.panelAnimReward.ActiveMe(PanelConfirmAfterBuy, iconImgs[index].sprite);
        // PanelConfirmAfterBuy.SetActive(true);
    }
    public void CloseInfoLuckyChest()
    {
        grids[index].SetActive(false);
        PanelInfoLuckyChest.SetActive(false);

        SoundController.instance.PlaySound(soundGame.soundbtnclick);
    }
    public void CloseConfirm()
    {
        CloseInfoLuckyChest();
        PanelConfirmAfterBuy.SetActive(false);

        SoundController.instance.PlaySound(soundGame.soundbtnclick);
    }
}
