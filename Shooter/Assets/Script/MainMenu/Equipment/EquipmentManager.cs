using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentManager : MonoBehaviour
{
    const string ALL_EQUIP = "ALL";
    
    public static EquipmentManager Instance;
    #region Equipment Selected
    public Image imgItemPriview, imgDamagePriview;
    public TextMeshProUGUI txtItemName, txtDamagePriview, txtAttSpeed, txtCritRate, txtCritDamage, txtRange, txtMagazine;
    #endregion
    #region Current Equipment
    public Image imgCurItemPriview, imgCurDamagePriview;
    public TextMeshProUGUI txtCurItemName, txtCurDamagePriview, txtCurAttSpeed, txtCurCritRate, txtCurCritDamage, txtCurRange, txtCurMagazine;
    #endregion

    public GameObject gWeaponData, gCurWeaponData;
    public GameObject gItemSelectPriview, gCurItemPriview;
    public GameObject gCurrentItemEquip;
    public GameObject gSellGroup;
    public Sprite sprSelect, sprUnSelect;
    public Button[] btnTabs;
    public Transform trContain;
    public GameObject gItems;
    private GameObject gItemClone;

    public ItemData itemSelected;

    private Button btnPanel;


    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        ChooseTab(0);
        InitAllItems();
    }
    string _keyCompare = "";
    private void OnEnable()
    {
        btnPanel = GetComponent<Button>();
        btnPanel.onClick.AddListener(() => { ChooseItem(null); });
    }
    private void OnDisable()
    {
        ChooseItem(null);
    }

    string key = "";
    public void InitAllItems()
    {
        foreach (ItemData itemData in DataUtils.dicAllEquipment.Values)
        {
            if (!itemData.isEquipped)
            {
                key = itemData.id + "_" + itemData.level + "_" + itemData.isUnlock;
                gItemClone = Instantiate(gItems);
                gItemClone.name = itemData.id + "_" + itemData.level + "_" + itemData.isUnlock;
                EquipmentItem item = gItemClone.GetComponent<EquipmentItem>();
                item.itemKey = key;
                item.imgItemPriview.sprite = MainMenuController.Instance.GetSpriteByName(itemData.id);
                gItemClone.transform.SetParent(trContain, false);
            }
        }
    }

    public void RemoveSelectedItem()
    {

    }
    public void EquipItem()
    {
        if (itemEquipped != null) {
            if(_keyItemEquipped.Trim().Length > 0)
            {
                for (int i = 0; i < trContain.childCount; i++)
                {
                    EquipmentItem _iEquipData = trContain.GetChild(i).gameObject.GetComponent<EquipmentItem>();
                    if(_iEquipData.itemData == itemSelected)
                    {
                        trContain.GetChild(i).gameObject.SetActive(false);
                    }
                    if(_iEquipData.itemData == itemEquipped)
                    {
                        trContain.GetChild(i).gameObject.SetActive(true);
                    }
                }

                RefreshInventory(itemEquipped);

                DataUtils.dicAllEquipment[_keyItemEquipped].isEquipped = false;
                DataUtils.dicAllEquipment[_keyItemSelected].isEquipped = true;
                DataUtils.EquipItem(itemSelected);
                //itemEquipped = null;
                //itemSelected = null;
                ChooseItem(null);
            }
        }
    }
    public bool IsItemHasInit(string id)
    {
        bool rs = false;
        for (int i = 0; i < trContain.childCount; i++)
        {
            if (trContain.GetChild(i).gameObject.Equals(id))
            {
                return true;
            }
        }
        return rs;
    }
    public void RefreshInventory(ItemData itemNew)
    {
        if (itemNew.isEquipped) {
            if (!IsItemHasInit(itemNew.id + "_" + itemNew.level + "_" + itemNew.isUnlock)) {
                key = itemNew.id + "_" + itemNew.level + "_" + itemNew.isUnlock;
                gItemClone = Instantiate(gItems);
                gItemClone.name = itemNew.id + "_" + itemNew.level + "_" + itemNew.isUnlock;
                EquipmentItem item = gItemClone.GetComponent<EquipmentItem>();
                item.itemKey = key;
                item.itemData = itemNew;
                item.imgItemPriview.sprite = MainMenuController.Instance.GetSpriteByName(itemNew.id);

                gItemClone.transform.SetParent(trContain, false);
            }
        }
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
        #region Swith Equipment by tabs index
        switch (_index)
        {
            case 0:
                ShowItemTypeSelect(ALL_EQUIP);
                break;
            case 1:
                ShowItemTypeSelect(DataUtils.eType.WEAPON.ToString());
                break;
            case 2:
                ShowItemTypeSelect(DataUtils.eType.ARMOR.ToString());
                break;
            case 3:
                ShowItemTypeSelect(DataUtils.eType.GLOVES.ToString());
                break;
            case 4:
                ShowItemTypeSelect(DataUtils.eType.HELMET.ToString());
                break;
            case 5:
                ShowItemTypeSelect(DataUtils.eType.BAG.ToString());
                break;
            case 6:
                ShowItemTypeSelect(DataUtils.eType.SHOES.ToString());
                break;
            default:
                ShowItemTypeSelect(ALL_EQUIP);
                break;
        }
        #endregion
    }
    
    private void ShowItemTypeSelect(string _type)
    {
        for (int i = 0; i < trContain.childCount; i++)
        {
            EquipmentItem _iEquipData = trContain.GetChild(i).gameObject.GetComponent<EquipmentItem>();
            if (_type.Equals(ALL_EQUIP)){
                trContain.GetChild(i).gameObject.SetActive(true);
            }
            else if (!_iEquipData.itemData.type.Equals(_type))
            {
                trContain.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                trContain.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    public void UpdateRotation(ItemData itemData, RectTransform _rect)
    {
        if (itemData.type.Equals(DataUtils.eType.WEAPON.ToString()))
        {
            _rect.eulerAngles = new Vector3(0, 0, 30);
        }
        else
        {
            _rect.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void ChooseItem(ItemData itemData)
    {
        if(itemData == null) {
            gWeaponData.SetActive(false);
            txtDamagePriview.gameObject.SetActive(false);
            imgDamagePriview.gameObject.SetActive(false);
            gItemSelectPriview.SetActive(false);

            gCurrentItemEquip.SetActive(false);
            for (int i = 0; i < trContain.childCount; i++)
            {
                EquipmentItem _iEquipData = trContain.GetChild(i).gameObject.GetComponent<EquipmentItem>();
                _iEquipData.imgSingleSelect.enabled = false;
            }
        }
        else
        {
            itemSelected = itemData;

            #region Fill Info for EquipmentItem
            FillEquipmentInfo(itemSelected);
            #endregion

            #region Display Selected Image
            for(int i = 0; i < trContain.childCount; i++)
            {
                EquipmentItem _iEquipData = trContain.GetChild(i).gameObject.GetComponent<EquipmentItem>();
                if (_iEquipData.itemData == itemData)
                {
                    _iEquipData.imgSingleSelect.enabled = true;
                }
                else {
                    _iEquipData.imgSingleSelect.enabled = false;
                }
            }
            #endregion
        }
    }

    ItemData itemEquipped;
    string _keyItemEquipped, _keyItemSelected;
    private void FillEquipmentInfo(ItemData itemData)
    {
        imgItemPriview.sprite = MainMenuController.Instance.GetSpriteByName(itemData.id);
        _keyItemSelected = itemData.id + "_" + itemData.level + "_" + itemData.isUnlock;
        string keyItem = itemData.id + "_" + itemData.level;
        txtItemName.text = DataUtils.dicAllEquipment[_keyItemSelected].itemName;

        foreach(ItemData _iData in DataUtils.dicEquippedItem.Values)
        {
            if (_iData.type.Equals(itemData.type))
            {
                itemEquipped = _iData;
            }
        }

        if(itemEquipped != null)
        {
            string keyEquipped = itemEquipped.id + "_" + itemEquipped.level;
            _keyItemEquipped = itemEquipped.id + "_" + itemEquipped.level + "_" + itemEquipped.isUnlock;
            imgCurItemPriview.sprite = MainMenuController.Instance.GetSpriteByName(itemEquipped.id);
            txtCurItemName.text = DataUtils.dicEquippedItem[_keyItemEquipped].itemName;

            if (itemEquipped.type.Contains("WEAPON"))
            {
                txtCurDamagePriview.text = "" + DataUtils.dicWeapon[keyEquipped].DmgValue[itemEquipped.curStar];
                txtCurCritDamage.text = "Crit Damage: <color=white>" + DataUtils.dicWeapon[keyEquipped].CritDmgValue[itemEquipped.curStar] + "</color>";
                txtCurAttSpeed.text = "Attack Speed: <color=white>" + DataUtils.dicWeapon[keyEquipped].AtksecValue[itemEquipped.curStar] + "</color>";
                txtCurCritRate.text = "Crit Rate: <color=white>" + DataUtils.dicWeapon[keyEquipped].CritRateValue[itemEquipped.curStar] + "</color>";
                txtCurRange.text = "Range: <color=white>" + DataUtils.dicWeapon[keyEquipped].AtkRangeValue[itemEquipped.curStar] + "</color>";
                txtCurMagazine.text = "Magazine: <color=white>" + DataUtils.dicWeapon[keyEquipped].MagazineValue[itemEquipped.curStar] + "</color>";

                gCurWeaponData.SetActive(true);
                txtCurDamagePriview.gameObject.SetActive(true);
                imgCurDamagePriview.gameObject.SetActive(true);
                UpdateRotation(itemEquipped, imgCurItemPriview.GetComponent<RectTransform>());
            }
            gCurItemPriview.SetActive(true);



            if (itemData.type.Contains("WEAPON"))
            {
                txtDamagePriview.text = "<color="+ GetColorByItemData (DataUtils.dicWeapon[keyItem].DmgValue[itemData.curStar], DataUtils.dicWeapon[keyEquipped].DmgValue[itemEquipped.curStar]) + ">" + DataUtils.dicWeapon[keyItem].DmgValue[itemData.curStar];
                txtCritDamage.text = "Crit Damage: <color=" + GetColorByItemData(DataUtils.dicWeapon[keyItem].CritDmgValue[itemData.curStar], DataUtils.dicWeapon[keyEquipped].CritDmgValue[itemEquipped.curStar]) + ">" + DataUtils.dicWeapon[keyItem].CritDmgValue[itemData.curStar] + "</color>";
                txtAttSpeed.text = "Attack Speed: <color="+ GetColorByItemData(DataUtils.dicWeapon[keyItem].AtksecValue[itemData.curStar], DataUtils.dicWeapon[keyEquipped].AtksecValue[itemEquipped.curStar]) + ">" + DataUtils.dicWeapon[keyItem].AtksecValue[itemData.curStar] + "</color>";
                txtCritRate.text = "Crit Rate: <color=" + GetColorByItemData(DataUtils.dicWeapon[keyItem].CritRateValue[itemData.curStar], DataUtils.dicWeapon[keyEquipped].CritRateValue[itemEquipped.curStar]) + ">" + DataUtils.dicWeapon[keyItem].CritRateValue[itemData.curStar] + "</color>";
                txtRange.text = "Range: <color=" + GetColorByItemData(DataUtils.dicWeapon[keyItem].AtkRangeValue[itemData.curStar], DataUtils.dicWeapon[keyEquipped].AtkRangeValue[itemEquipped.curStar]) + ">" + DataUtils.dicWeapon[keyItem].AtkRangeValue[itemData.curStar] + "</color>";
                txtMagazine.text = "Magazine: <color=" + GetColorByItemData(DataUtils.dicWeapon[keyItem].MagazineValue[itemData.curStar], DataUtils.dicWeapon[keyEquipped].MagazineValue[itemEquipped.curStar]) + ">" + DataUtils.dicWeapon[keyItem].MagazineValue[itemData.curStar] + "</color>";

                gWeaponData.SetActive(true);
                txtDamagePriview.gameObject.SetActive(true);
                imgDamagePriview.gameObject.SetActive(true);
            }
            else
            {
                gWeaponData.SetActive(false);
                txtDamagePriview.gameObject.SetActive(false);
                imgDamagePriview.gameObject.SetActive(false);
            }
            UpdateRotation(itemData, imgItemPriview.GetComponent<RectTransform>());
            gItemSelectPriview.SetActive(true);
        }

        #region Check Selected Item has Unlock
        if (itemData.isUnlock)
        {
            gCurrentItemEquip.SetActive(true);
        }
        else
        {
            gCurrentItemEquip.SetActive(false);
        }
        #endregion


    }

    private string GetColorByItemData(float f1, float f2)
    {
        string cl = "white";
        cl = f1 > f2 ? "green" : (f1 < f2 ? "red" : "white");

        return cl;
    }
    public void BackToMainMenu()
    {
        gameObject.SetActive(false);
    }
}
