using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentManager : MonoBehaviour
{
    const string ALL_EQUIP = "ALL";
    public Sprite sprNormal, sprUncommon, sprRare, sprEpic, sprLegendary;

    public DisassembleManager disassembleManager;
    public static EquipmentManager Instance;
    #region Equipment Selected
    public Image imgItemPriview, imgDamagePriview, imgItemSelectPriview;
    public TextMeshProUGUI txtItemName, txtDamagePriview, txtAttSpeed, txtCritRate, txtCritDamage, txtRange, txtMagazine;
    #endregion
    #region Current Equipment
    public Image imgCurItemPriview, imgCurDamagePriview, imgItemEquipPriview;
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
                key = itemData.id + "_" + itemData.level + "_" + itemData.isUnlock + "_" + itemData.isEquipped;
                gItemClone = Instantiate(gItems);
                gItemClone.name = itemData.id + "_" + itemData.level + "_" + itemData.isUnlock + "_" + itemData.isEquipped;
                EquipmentItem item = gItemClone.GetComponent<EquipmentItem>();
                item.itemKey = key;
                item.imgItemPriview.sprite = DataUtils.GetSpriteByName(itemData.id, MainMenuController.Instance.allSpriteData);
                gItemClone.transform.SetParent(trContain, false);
            }
        }
    }

    public void RemoveSelectedItem()
    {
        Debug.LogError(_keyItemSelected + " vs " + DataUtils.dicAllEquipment[_keyItemSelected]);
        disassembleManager.iDisassemble = DataUtils.dicAllEquipment[_keyItemSelected];
        disassembleManager.keyItem = _keyItemSelected;
        disassembleManager.gameObject.SetActive(true);
    }
    public void DoDisassemble(ItemData iSell, string strRemoveKey)
    {
        for(int i = 0; i< trContain.childCount; i++)
        {
            if (trContain.GetChild(i).gameObject.name.Equals(strRemoveKey))
            {
                Destroy(trContain.GetChild(i).gameObject);
            }
        }
        if (DataUtils.dicAllEquipment.ContainsKey(strRemoveKey))
        {
            DataUtils.dicAllEquipment.Remove(strRemoveKey);
        }
        DataUtils.SaveEquipmentData();
        ChooseItem(null);
    }

    public void EquipItem()
    {
        if (itemEquipped != null)
        {
            Debug.LogError("_keyItemEquipped: " + _keyItemSelected + " vs " + _keyItemEquipped);
            string keyCompare1, keyCompare2;
            keyCompare1 = DataUtils.dicAllEquipment[_keyItemEquipped].id + "_" + DataUtils.dicAllEquipment[_keyItemEquipped].level + "_" + DataUtils.dicAllEquipment[_keyItemEquipped].isUnlock;
            keyCompare2 = DataUtils.dicAllEquipment[_keyItemSelected].id + "_" + DataUtils.dicAllEquipment[_keyItemSelected].level + "_" + DataUtils.dicAllEquipment[_keyItemSelected].isUnlock;
            if (keyCompare1.Equals(keyCompare2))
            {
                Debug.LogError(keyCompare1 + " v--------s " + keyCompare2);
                ItemData iCurEquip = DataUtils.dicAllEquipment[_keyItemEquipped];
                ItemData iCurSelect = DataUtils.dicAllEquipment[_keyItemSelected];

                iCurEquip.isEquipped = false;
                iCurSelect.isEquipped = true;
                DataUtils.dicAllEquipment[_keyItemEquipped] = iCurSelect;
                DataUtils.dicAllEquipment[_keyItemSelected] = iCurEquip;


                DataUtils.dicEquippedItem.Remove(_keyItemEquipped);
                DataUtils.EquipItem(iCurSelect);

                for (int i = 0; i < trContain.childCount; i++)
                {
                    if (trContain.GetChild(i).gameObject.name.Equals(_keyItemSelected))
                    {
                        EquipmentItem _iEquipData = trContain.GetChild(i).gameObject.GetComponent<EquipmentItem>();
                        _iEquipData.itemKey = _keyItemEquipped;
                        trContain.GetChild(i).gameObject.name = _iEquipData.itemKey;
                        _iEquipData.itemData = iCurEquip;
                        _iEquipData.CheckItemUnlock();
                    }
                }
            }
            else
            {

                CheckInitNewItem(itemEquipped);
                DataUtils.dicAllEquipment[_keyItemSelected].isEquipped = true;
                DataUtils.dicAllEquipment[_keyItemEquipped].isEquipped = false;
                DataUtils.dicEquippedItem.Remove(_keyItemEquipped);
                DataUtils.EquipItem(itemSelected);

                ItemData iData = DataUtils.dicAllEquipment[_keyItemEquipped];
                string _key = iData.id + "_" + iData.level.ToString() + "_" + iData.isUnlock + "_" + iData.isEquipped;
                DataUtils.dicAllEquipment.Remove(_keyItemEquipped);
                if (!DataUtils.dicAllEquipment.ContainsKey(_key)) {
                    DataUtils.dicAllEquipment.Add(_key, iData);
                }
                else
                {
                    DataUtils.dicAllEquipment[_key].quantity += 1;
                    for (int i = 0; i < trContain.childCount; i++)
                    {
                        if (trContain.GetChild(i).gameObject.name.Equals(_keyItemEquipped))
                        {
                            Destroy(trContain.GetChild(i).gameObject);
                        }
                    }
                }


                ItemData iData1 = DataUtils.dicAllEquipment[_keyItemSelected];
                string _key1 = iData1.id + "_" + iData1.level.ToString() + "_" + iData1.isUnlock + "_" + iData1.isEquipped;
                DataUtils.dicAllEquipment.Remove(_keyItemSelected);
                DataUtils.dicAllEquipment.Add(_key1, iData1);
                for (int i = 0; i < trContain.childCount; i++)
                {
                    EquipmentItem _iEquipData = trContain.GetChild(i).gameObject.GetComponent<EquipmentItem>();
                    trContain.GetChild(i).gameObject.name = _iEquipData.itemData.id + "_" + _iEquipData.itemData.level.ToString() + "_" + _iEquipData.itemData.isUnlock + "_" + _iEquipData.itemData.isEquipped;
                    _iEquipData.itemKey = trContain.GetChild(i).gameObject.name;

                    if (_iEquipData.itemData.isEquipped)
                    {
                        trContain.GetChild(i).gameObject.SetActive(false);
                    }
                    else
                    {
                        trContain.GetChild(i).gameObject.SetActive(true);
                    }
                }
            }
        }
        else
        {
            DataUtils.dicAllEquipment[_keyItemSelected].isEquipped = true;
            DataUtils.EquipItem(DataUtils.dicAllEquipment[_keyItemSelected]);

            ItemData iData2 = DataUtils.dicAllEquipment[_keyItemSelected];
            string _key2 = iData2.id + "_" + iData2.level.ToString() + "_" + iData2.isUnlock + "_" + iData2.isEquipped;
            DataUtils.dicAllEquipment.Remove(_keyItemSelected);
            DataUtils.dicAllEquipment.Add(_key2, iData2);


            for (int i = 0; i < trContain.childCount; i++)
            {
                EquipmentItem _iEquipData = trContain.GetChild(i).gameObject.GetComponent<EquipmentItem>();
                trContain.GetChild(i).gameObject.name = _iEquipData.itemData.id + "_" + _iEquipData.itemData.level.ToString() + "_" + _iEquipData.itemData.isUnlock + "_" + _iEquipData.itemData.isEquipped;
                _iEquipData.itemKey = trContain.GetChild(i).gameObject.name;
                if (_iEquipData.itemData.isEquipped)
                {
                    trContain.GetChild(i).gameObject.SetActive(false);
                }
            }
        }

        ChooseItem(null);
    }
    public bool IsItemHasInit(string id)
    {
        //bool rs = false;
        int count = 0;
        for (int i = 0; i < trContain.childCount; i++)
        {
            if (trContain.GetChild(i).gameObject.name.Equals(id))
            {
                count++;
            }
        }
        return count == 0 ? false : true;
    }
    private void CheckInitNewItem(ItemData itemNew)
    {
        if (itemNew.isEquipped)
        {
            if (!IsItemHasInit(itemNew.id + "_" + itemNew.level + "_" + itemNew.isUnlock + "_" + itemNew.isEquipped))
            {
                key = itemNew.id + "_" + itemNew.level + "_" + itemNew.isUnlock + "_" + itemNew.isEquipped;
                gItemClone = Instantiate(gItems);
                gItemClone.name = itemNew.id + "_" + itemNew.level + "_" + itemNew.isUnlock + "_" + itemNew.isEquipped;
                EquipmentItem item = gItemClone.GetComponent<EquipmentItem>();
                item.itemKey = key;
                item.itemData = itemNew;
                item.imgItemPriview.sprite = DataUtils.GetSpriteByName(itemNew.id, MainMenuController.Instance.allSpriteData);

                gItemClone.transform.SetParent(trContain, false);
            }
        }
    }
    public void RefreshInventory(ItemData itemNew)
    {
        key = itemNew.id + "_" + itemNew.level + "_" + itemNew.isUnlock + "_" + itemNew.isEquipped;
        gItemClone = Instantiate(gItems);
        gItemClone.name = itemNew.id + "_" + itemNew.level + "_" + itemNew.isUnlock + "_" + itemNew.isEquipped;
        EquipmentItem item = gItemClone.GetComponent<EquipmentItem>();
        item.itemKey = key;
        item.itemData = itemNew;
        item.imgItemPriview.sprite = DataUtils.GetSpriteByName(itemNew.id, MainMenuController.Instance.allSpriteData);

        gItemClone.transform.SetParent(trContain, false);



        for (int i = 0; i < trContain.childCount; i++)
        {
            EquipmentItem _iEquipData = trContain.GetChild(i).gameObject.GetComponent<EquipmentItem>();
            if (!DataUtils.dicAllEquipment.ContainsKey(_iEquipData.itemKey))
            {
                Destroy(trContain.GetChild(i).gameObject);
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
            if (_type.Equals(ALL_EQUIP))
            {
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
        //if (itemData.type.Equals(DataUtils.eType.WEAPON.ToString()))
        //{
        //    _rect.eulerAngles = new Vector3(0, 0, 30);
        //}
        //else
        //{
        //    _rect.eulerAngles = new Vector3(0, 0, 0);
        //}
    }

    public void ChooseItem(ItemData itemData)
    {
        if (itemData == null)
        {
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
            for (int i = 0; i < trContain.childCount; i++)
            {
                EquipmentItem _iEquipData = trContain.GetChild(i).gameObject.GetComponent<EquipmentItem>();
                if (_iEquipData.itemData == itemData)
                {
                    _iEquipData.imgSingleSelect.enabled = true;
                }
                else
                {
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
        imgItemPriview.sprite = DataUtils.GetSpriteByName(itemData.id, MainMenuController.Instance.allSpriteData);
        _keyItemSelected = itemData.id + "_" + itemData.level + "_" + itemData.isUnlock + "_" + itemData.isEquipped;
        string keyItem = itemData.id + "_" + itemData.level;
        txtItemName.text = DataUtils.dicAllEquipment[_keyItemSelected].itemName;

        foreach (ItemData _iData in DataUtils.dicEquippedItem.Values)
        {
            if (_iData.type.Equals(itemData.type))
            {
                itemEquipped = _iData;
                break;
            }
            else
            {
                itemEquipped = null;
            }
        }
        if (itemEquipped == null)
        {
            gCurrentItemEquip.SetActive(false);
        }
        else
        {
            string keyEquipped = itemEquipped.id + "_" + itemEquipped.level;
            _keyItemEquipped = itemEquipped.id + "_" + itemEquipped.level + "_" + itemEquipped.isUnlock + "_" + itemEquipped.isEquipped;

            imgCurItemPriview.sprite = DataUtils.GetSpriteByName(itemEquipped.id, MainMenuController.Instance.allSpriteData);
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
            }
            else
            {
                gCurWeaponData.SetActive(false);
                txtCurDamagePriview.gameObject.SetActive(false);
                imgCurDamagePriview.gameObject.SetActive(false);
            }
            UpdateRotation(itemEquipped, imgCurItemPriview.GetComponent<RectTransform>());
            gCurItemPriview.SetActive(true);


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
            if (itemData.type.Contains("WEAPON"))
            {
                txtDamagePriview.text = "<color=" + GetColorByItemData(DataUtils.dicWeapon[keyItem].DmgValue[itemData.curStar], DataUtils.dicWeapon[keyEquipped].DmgValue[itemEquipped.curStar]) + ">" + DataUtils.dicWeapon[keyItem].DmgValue[itemData.curStar];
                txtCritDamage.text = "Crit Damage: <color=" + GetColorByItemData(DataUtils.dicWeapon[keyItem].CritDmgValue[itemData.curStar], DataUtils.dicWeapon[keyEquipped].CritDmgValue[itemEquipped.curStar]) + ">" + DataUtils.dicWeapon[keyItem].CritDmgValue[itemData.curStar] + "</color>";
                txtAttSpeed.text = "Attack Speed: <color=" + GetColorByItemData(DataUtils.dicWeapon[keyItem].AtksecValue[itemData.curStar], DataUtils.dicWeapon[keyEquipped].AtksecValue[itemEquipped.curStar]) + ">" + DataUtils.dicWeapon[keyItem].AtksecValue[itemData.curStar] + "</color>";
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

            imgItemEquipPriview.sprite = DataUtils.GetSpriteByType(itemEquipped);
        }

        UpdateRotation(itemData, imgItemPriview.GetComponent<RectTransform>());
        imgItemSelectPriview.sprite = DataUtils.GetSpriteByType(itemData);
        gItemSelectPriview.SetActive(true);
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
