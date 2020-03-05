using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DailyGift : MonoBehaviour
{
    public Image imgQuality, imgItemPriview;
    public TextMeshProUGUI txtItemInfo;
    public Sprite sprNotAvaiable;
    public Button btnX2;
    public GameObject gItemInfo;
    public Text txtItemName, txtAtk, txtBulletSpeed, txtReload, txtRange, txtMagazine, txtTotalPart;
    public int totalPart = 2;
    public ItemData itemData;

    private DataUtils.eLevel _eLevel;
    private DataUtils.eType _eType;
    List<DataUtils.eLevel> lstELevel;
    List<DataUtils.eType> lstEType;
    int itemIndex = 0;
    string itemID = "";
    string itemName;
    public void PrepareData()
    {
        lstELevel = new List<DataUtils.eLevel>();
        lstELevel.Add(DataUtils.eLevel.Epic);
        lstELevel.Add(DataUtils.eLevel.Legendary);
        lstELevel.Add(DataUtils.eLevel.Normal);
        lstELevel.Add(DataUtils.eLevel.Rare);
        lstELevel.Add(DataUtils.eLevel.Uncommon);

        lstEType = new List<DataUtils.eType>();
        lstEType.Add(DataUtils.eType.ARMOR);
        lstEType.Add(DataUtils.eType.BAG);
        lstEType.Add(DataUtils.eType.GLOVES);
        lstEType.Add(DataUtils.eType.HELMET);
       // lstEType.Add(DataUtils.eType.P1);
        lstEType.Add(DataUtils.eType.SHOES);
        lstEType.Add(DataUtils.eType.WEAPON);

        _eLevel = lstELevel[Random.Range(0, lstELevel.Count)];
        _eType = lstEType[Random.Range(0, lstEType.Count)];

        switch (_eType)
        {
            case DataUtils.eType.ARMOR:
                itemIndex = Random.Range(1, DataController.instance.allArmor.Count + 1);
                itemName = DataController.instance.allArmor[itemIndex - 1].armorList[0].NAME;
                itemID = "A" + itemIndex;
                break;
            case DataUtils.eType.BAG:
                itemIndex = Random.Range(1, DataController.instance.allBag.Count + 1);
                itemName = DataController.instance.allBag[itemIndex - 1].bagList[0].NAME;
                itemID = "B" + itemIndex;
                break;
            case DataUtils.eType.GLOVES:
                itemIndex = Random.Range(1, DataController.instance.allGloves.Count + 1);
                itemName = DataController.instance.allGloves[itemIndex - 1].glovesList[0].NAME;
                itemID = "G" + itemIndex;
                break;
            case DataUtils.eType.HELMET:
                itemIndex = Random.Range(1, DataController.instance.allHelmet.Count + 1);
                itemName = DataController.instance.allHelmet[itemIndex - 1].helmetList[0].NAME;
                itemID = "H" + itemIndex;
                break;
            //case DataUtils.eType.P1:
            //    itemIndex = 1;
            //    itemName = "Player " + itemIndex;
            //    itemID = "P" + itemIndex;
            //    break;
            case DataUtils.eType.SHOES:
                itemIndex = Random.Range(1, DataController.instance.allShoes.Count + 1);
                itemName = DataController.instance.allShoes[itemIndex - 1].shoesList[0].NAME;
                itemID = "S" + itemIndex;
                break;
            case DataUtils.eType.WEAPON:
                itemIndex = Random.Range(1, DataController.instance.allWeapon.Count + 1);
                itemName = DataController.instance.allWeapon[itemIndex - 1].weaponList[0].NAME;
                itemID = "W" + itemIndex;
                break;
        }
        //Debug.LogError(_eLevel.ToString() + " vs " + _eType.ToString() + " vs " + itemIndex + " vs " + itemName);

        itemData = new ItemData();
        itemData.id = itemID;
        itemData.type = _eType.ToString();
        itemData.level = _eLevel.ToString();
        itemData.itemName = itemName;
        itemData.pices = totalPart;
        itemData.isUnlock = false;
        itemData.curStar = 0;
        itemData.quantity = 0;
        itemData.isEquipped = false;

    }

    public void ShowDailyGiftPanel()
    {
        //Debug.LogError("--> " + AdsManager.Instance.IsRewardLoaded());
        if(AdsManager.Instance != null)
            btnX2.interactable = AdsManager.Instance.IsRewardLoaded();
        #region Fill info
        txtItemName.text = itemName;

        if (_eType == DataUtils.eType.WEAPON)
        {
            txtItemInfo.gameObject.SetActive(false);
            int curStar = itemData.curStar < DataUtils.MAX_STARS ? itemData.curStar : 4;
            txtAtk.text = DataController.instance.allWeapon[itemIndex - 1].weaponList[(int)_eLevel].AtksecValue[curStar].ToString();
            txtBulletSpeed.text = DataController.instance.allWeapon[itemIndex - 1].weaponList[(int)_eLevel].BulletSpeedValue[curStar].ToString();
            txtRange.text = DataController.instance.allWeapon[itemIndex - 1].weaponList[(int)_eLevel].AtkRangeValue[curStar].ToString();
            txtReload.text = DataController.instance.allWeapon[itemIndex - 1].weaponList[(int)_eLevel].ReloadSpeedValue[curStar].ToString();
            txtMagazine.text = DataController.instance.allWeapon[itemIndex - 1].weaponList[(int)_eLevel].MagazineValue[curStar].ToString();
            gItemInfo.SetActive(true);
        }
        else
        {
            gItemInfo.SetActive(false);
            txtItemInfo.text = DataUtils.GetItemInfo(itemData);
            txtItemInfo.gameObject.SetActive(true);
        }
        imgItemPriview.sprite = DataUtils.GetSpriteByName(itemData.id, MainMenuController.Instance.allSpriteData);
        imgQuality.sprite = DataUtils.GetSpriteByType(itemData);
        txtTotalPart.text = "" + totalPart;
        #endregion

        gameObject.SetActive(true);
    }

    public void TakeDailyGift()
    {
        //if(_eType == DataUtils.eType.P1)
        //{
        //    DataUtils.TakeHeroPice(itemID, totalPart);
        //}
        //else
        {
            DataUtils.TakeItem(itemData.id, _eType, _eLevel, totalPart, false);
        }
        
        DataUtils.HasClaimReward();
        HideDailyGiftPanel();
    }
    public void TakeX2Gift()
    {
        HideDailyGiftPanel();
        AdsManager.Instance.ShowRewardedVideo((b) =>
        {
            if (b)
            {
                //if (_eType == DataUtils.eType.P1)
                //{
                //    DataUtils.TakeHeroPice(itemID, totalPart);
                //}
                //else
                {
                    DataUtils.TakeItem(itemData.id, _eType, _eLevel, totalPart, false);
                }
                DataUtils.HasClaimReward();
            }
        });
        TakeDailyGift();
    }

    public void HideDailyGiftPanel()
    {
        gameObject.SetActive(false);
    }
}
