using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class EquipmentManager : MonoBehaviour
{
    const string ALL_EQUIP = "ALL";
    public Sprite sprWhite, sprYellow, sprButton, sprButtonCur, sprButtonYellow;
    public Image[] allStars;
    public Image[] allStarsItemSelect;
    public GameObject gAllStarItemSelect, gBuyMore;
    public DisassembleManager disassembleManager;
    public static EquipmentManager Instance;
    public Button btnRemove, btnReplace, btnUpgrade, btnUnlock, btnGetMore;
    public Image imgCoinItemUpdate, imgPieceEvolve;
    public Text txtMaxReach;
    public Text txtEquip;
    public Text txtPriceUpgrade, txtPieceEvolve;
    public Text txtUpgrade;
    #region Equipment Selected
    public Image imgItemPriview, imgDamagePriview, imgItemSelectPriview;
    public TextMeshProUGUI txtItemName, txtDamagePriview, txtAttSpeed, txtCritRate, txtCritDamage, txtRange, txtMagazine;
    #endregion
    #region Current Equipment
    public Image imgCurItemPriview, imgCurDamagePriview, imgItemEquipPriview;
    public TextMeshProUGUI txtCurItemName, txtCurDamagePriview, txtCurAttSpeed, txtCurCritRate, txtCurCritDamage, txtCurRange, txtCurMagazine;
    #endregion

    public TextMeshProUGUI txtItemInfoEquip, txtItemSelectInfo;
    public GameObject gWeaponData, gCurWeaponData;
    public GameObject gItemSelectPriview, gCurItemPriview;
    public GameObject gCurrentItemEquip;
    public GameObject gSellGroup;
    public Sprite sprSelect, sprUnSelect;
    public Button[] btnTabs;
    public Transform trContain;
    public bool isMultiSell;
    public GameObject gItems;
    public GameObject gItemSelected;
    public TextMeshProUGUI txtBuy3, txtBuy5;
    private GameObject gItemClone;


    public ItemData itemSelected;

    private Button btnPanel;
    public int tabSelected = 0;


    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    IEnumerator Start()
    {

        InitAllItems();
        yield return null;
        SortingItem();
    }

    string _keyCompare = "";
    private void OnEnable()
    {
        btnPanel = GetComponent<Button>();
        btnPanel.onClick.AddListener(() =>
        {
            ChooseItem(null);
            DoCancelDisassemble();
        });

        for (int i = 0; i < btnTabs.Length; i++)
        {
            if (i == tabSelected)
            {
                btnTabs[i].image.sprite = sprSelect;
            }
            else btnTabs[i].image.sprite = sprUnSelect;
        }


        SortingItem();
        ChildAlignment();
    }

    private void ChildAlignment()
    {
        totalEnable = 0;



        if (tabSelected != 0)
        {
            for (int i = 0; i < trContain.childCount; i++)
            {
                if (trContain.GetChild(i).gameObject.activeSelf)
                {
                    totalEnable++;
                }
            }
            if (trContain.GetComponent<GridLayoutGroup>() != null)
            {
                if (totalEnable < 5)
                {
                    trContain.GetComponent<GridLayoutGroup>().childAlignment = TextAnchor.MiddleLeft;
                }
                else
                    trContain.GetComponent<GridLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;
            }
        }
        else
            trContain.GetComponent<GridLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;
    }
    int totalEnable = 0;
    private void OnDisable()
    {
        ChooseTab(0);
        ChooseItem(null);
    }
    List<Transform> lstAllChild = new List<Transform>();
    void SortingItem()
    {
        lstAllChild.Clear();
        for (int i = 0; i < trContain.childCount; i++)
        {
            lstAllChild.Add(trContain.GetChild(i));
        }
        var _sortList = lstAllChild.OrderByDescending(c => c.name.StartsWith("W")).ThenByDescending(c => c.name.StartsWith("A")).ThenByDescending(c => c.name.StartsWith("G"))
            .ThenByDescending(c => c.name.StartsWith("H")).ThenByDescending(c => c.name.StartsWith("B")).ThenByDescending(c => c.name.StartsWith("S")).ToList();

        for (int i = 0; i < _sortList.Count; i++)
        {
            for (int j = 0; j < trContain.childCount; j++)
            {
                if (trContain.GetChild(j).name.Contains(_sortList[i].name))
                {
                    trContain.GetChild(j).SetSiblingIndex(i);
                }
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SortingItem();
        }
    }
    string key = "";
    public void InitAllItems()
    {
        foreach (ItemData itemData in DataUtils.dicAllEquipment.Values)
        {
            key = itemData.id + "_" + itemData.level + "_" + itemData.isUnlock + "_" + itemData.isEquipped;
            gItemClone = Instantiate(gItems);
            gItemClone.name = itemData.id + "_" + itemData.level + "_" + itemData.isUnlock + "_" + itemData.isEquipped;
            EquipmentItem item = gItemClone.GetComponent<EquipmentItem>();
            item.itemKey = key;
            item.imgItemPriview.sprite = DataUtils.GetSpriteByName(itemData.id, MainMenuController.Instance.allSpriteData);
            item.itemData = DataUtils.dicAllEquipment[item.itemKey];
            gItemClone.SetActive(!item.itemData.isUnlock);
            gItemClone.transform.SetParent(trContain, false);
        }

        //SortingItem();
    }

    public void RemoveSelectedItem()
    {
        disassembleManager.iDisassemble = DataUtils.dicAllEquipment[_keyItemSelected];
        disassembleManager.keyItem = _keyItemSelected;
        disassembleManager.gameObject.SetActive(true);
    }
    public void DoDisassemble(ItemData iSell, string strRemoveKey)
    {
        for (int i = 0; i < trContain.childCount; i++)
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
    public void GetMoreItem()
    {
        txtBuy3.text = (3 * DataUtils.GetDiamondPrice(itemSelected)).ToString();
        txtBuy5.text = (5 * DataUtils.GetDiamondPrice(itemSelected)).ToString();
        gBuyMore.SetActive(true);
    }
    public void UnlockThisItem()
    {
        if (itemSelected.pices >= DataUtils.GetPiceByStar(itemSelected, false))
        {
            ///Check Unlock item
            bool _b = false;
            foreach (ItemData _iSearch in DataUtils.dicAllEquipment.Values)
            {
                if (_iSearch.id.Equals(itemSelected.id) && _iSearch.level.Equals(itemSelected.level) && _iSearch.isUnlock)
                {
                    _b = true;
                }
            }


            if (_b)
            {
                MainMenuController.Instance.ShowMapNotify("Item has exits");
            }
            else
            {
                DataUtils.UnlockThisItem(itemSelected);
                for (int i = 0; i < trContain.childCount; i++)
                {
                    EquipmentItem _iEquipData = trContain.GetChild(i).gameObject.GetComponent<EquipmentItem>();
                    _iEquipData.CheckItemUnlock();
                }
            }
        }
        ChooseItem(null);
    }

    public void RemoverThisItem(ItemData IData)
    {
        for (int i = 0; i < trContain.childCount; i++)
        {
            EquipmentItem _iEquipData = trContain.GetChild(i).gameObject.GetComponent<EquipmentItem>();
            if (_iEquipData.itemData != null)
            {
                if (_iEquipData.itemData == IData)
                {
                    trContain.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }
    public void EquipItem()
    {
        if (itemEquipped != null)
        {
            if (itemSelected.isUnlock)
            {
                #region Item Selected has unlock
                string keyCompare1, keyCompare2;
                keyCompare1 = DataUtils.dicAllEquipment[_keyItemEquipped].id + "_" + DataUtils.dicAllEquipment[_keyItemEquipped].level + "_" + DataUtils.dicAllEquipment[_keyItemEquipped].isUnlock;
                keyCompare2 = DataUtils.dicAllEquipment[_keyItemSelected].id + "_" + DataUtils.dicAllEquipment[_keyItemSelected].level + "_" + DataUtils.dicAllEquipment[_keyItemSelected].isUnlock;
                if (keyCompare1.Equals(keyCompare2))
                {
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
                            _iEquipData.itemKey = _keyItemSelected;
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
                    if (!DataUtils.dicAllEquipment.ContainsKey(_key))
                    {
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
                        _iEquipData.CheckItemUnlock();
                    }
                }
                #endregion
            }
            else
            {
                MainMenuController.Instance.ShowMapNotify("Item not yet unlock");
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
            }
        }
        ChooseItem(null);
    }
    public bool IsItemHasInit(string id)
    {
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
    public void CheckInitNewItem(ItemData itemNew)
    {
        if (!IsItemHasInit(itemNew.id + "_" + itemNew.level + "_" + itemNew.isUnlock + "_" + itemNew.isEquipped))
        {
            //itemNew.isEquipped = false;
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
    public void RefreshInventory(ItemData itemNew, bool activeItem)
    {
        key = itemNew.id + "_" + itemNew.level + "_" + itemNew.isUnlock + "_" + itemNew.isEquipped;
        gItemClone = Instantiate(gItems);
        gItemClone.name = itemNew.id + "_" + itemNew.level + "_" + itemNew.isUnlock + "_" + itemNew.isEquipped;
        EquipmentItem item = gItemClone.GetComponent<EquipmentItem>();
        item.itemKey = key;
        item.itemData = itemNew;
        item.imgItemPriview.sprite = DataUtils.GetSpriteByName(itemNew.id, MainMenuController.Instance.allSpriteData);

        gItemClone.transform.SetParent(trContain, false);

        gItemClone.SetActive(activeItem);

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
        tabSelected = _index;
        ChooseItem(null);
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


        ChildAlignment();
    }

    private void ShowItemTypeSelect(string _type)
    {
        for (int i = 0; i < trContain.childCount; i++)
        {
            EquipmentItem _iEquipData = trContain.GetChild(i).gameObject.GetComponent<EquipmentItem>();
            if (_type.Equals(ALL_EQUIP) && !_iEquipData.itemData.isUnlock)
            {
                trContain.GetChild(i).gameObject.SetActive(!_iEquipData.itemData.isUnlock);
            }
            else if (_iEquipData.itemData.type.Equals(_type))
            {
                trContain.GetChild(i).gameObject.SetActive(_iEquipData.itemData.isUnlock);
            }
            else
            {
                trContain.GetChild(i).gameObject.SetActive(false);
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
    float priceUpgrade;
    private bool IsReachMaxLevel(ItemData iData)
    {
        return iData.curStar < DataUtils.MAX_STARS - 1;
    }
    public void UpgradeItem()
    {
        int total = 0;
        string key = itemSelected.id + "_" + itemSelected.level + "_" + itemSelected.isUnlock + "_" + itemSelected.isEquipped;

        if (itemSelected.id.Contains("W7"))
        {
            MainMenuController.Instance.ShowMapNotify("Item use only, cannot upgrade/evolve.");
        }
        else if (DataUtils.playerInfo.coins >= priceUpgrade)
        {
            if (/*DataUtils.dicAllEquipment[key].curStar < DataUtils.MAX_STARS - 1*/IsReachMaxLevel(DataUtils.dicAllEquipment[key]))
            {
                DataUtils.AddCoinAndGame(-(int)priceUpgrade, 0);
                DataUtils.dicAllEquipment[key].curStar += 1;
                total = DataUtils.dicAllEquipment[key].curStar;

                DataUtils.SaveEquipmentData();
                UpdateStar(DataUtils.dicAllEquipment[key]);
                txtPriceUpgrade.text = priceUpgrade.ToString();

                if (DataUtils.dicEquippedItem.ContainsKey(key))
                {
                    DataUtils.dicEquippedItem[key].curStar = total;
                    DataUtils.SaveEquippedData();
                }
                DataController.instance.DoDailyQuest(5, 1);
            }
            else
            {
                EvolveThisItem(key);
            }
        }
        else
        {
            MainMenuController.Instance.ShowMapNotify("Not enough coin to upgrade this item");
        }
    }

    string _strEvolveText = "";

    private bool IsExitsItem(ItemData itemData)
    {
        bool _b = false;
        foreach (ItemData _iSearch in DataUtils.dicAllEquipment.Values)
        {
            if (_iSearch.id.Equals(itemData.id) && _iSearch.level.Equals(EvolveItemLevel(itemData.level)) && _iSearch.isUnlock)
            {
                _b = true;
            }
        }
        return _b;
    }
    private void DestroyItemByKey(string _key)
    {
        for (int i = 0; i < trContain.childCount; i++)
        {
            if (trContain.GetChild(i).gameObject.name.Equals(_key))
            {
                Destroy(trContain.GetChild(i).gameObject);
            }
        }
    }
    private void RemoveItem(string _key)
    {
        if (DataUtils.dicAllEquipment.ContainsKey(_key))
        {
            DataUtils.dicAllEquipment.Remove(_key);
        }
    }
    private void EvolveThisItem(string itemKey)
    {
        ItemData iSearchRS = null;
        ItemData curItem = DataUtils.dicAllEquipment[itemKey];
        _strEvolveText = EvolveItemLevel(curItem.level);
        if (IsExitsItem(curItem))
        {
            MainMenuController.Instance.ShowMapNotify("Item has exits");
        }
        else
        {
            if (/*_strEvolveText*/curItem.Equals("Legendary"))
            {
                MainMenuController.Instance.ShowMapNotify("Item has reached max");
            }
            else
            {
                foreach (ItemData _iSearch in DataUtils.dicAllEquipment.Values)
                {
                    if (_iSearch.id.Equals(curItem.id) && _iSearch.level.Equals(curItem.level) && !_iSearch.isUnlock)
                    {
                        iSearchRS = _iSearch;
                    }
                }
                if (iSearchRS != null)
                {
                    int curPiece = iSearchRS.pices; /*Search all pice of this item*/
                    int pieceEvolve = (int)DataUtils.GetPiceByStar(DataUtils.dicAllEquipment[itemKey], true);
                    int _newPiece = curPiece - pieceEvolve;
                    if (curPiece >= pieceEvolve)
                    {
                        MainMenuController.Instance.ShowMapNotify("Evolve " + DataUtils.dicAllEquipment[itemKey].itemName + " success!");

                        ItemData iDataEvolve = new ItemData();
                        iDataEvolve.id = curItem.id;
                        iDataEvolve.type = curItem.type;
                        iDataEvolve.level = EvolveItemLevel(curItem.level);
                        iDataEvolve.isUnlock = true;
                        iDataEvolve.pices = 0;
                        iDataEvolve.itemName = curItem.itemName;
                        iDataEvolve.isEquipped = curItem.isEquipped;

                        string _keyEvolve = iDataEvolve.id + "_" + iDataEvolve.level + "_" + iDataEvolve.isUnlock + "_" + iDataEvolve.isEquipped;

                        string _keyiSearchRS = iSearchRS.id + "_" + iSearchRS.level + "_" + iSearchRS.isUnlock + "_" + iSearchRS.isEquipped;
                        DataUtils.dicAllEquipment[_keyiSearchRS].pices = _newPiece;
                        if (_newPiece <= 0)
                        {
                            RemoverThisItem(iSearchRS);
                        }

                        ChooseItem(null);

                        if (curItem.isEquipped)
                        {
                            /*Item đã được trang bị*/
                            DataUtils.dicEquippedItem.Remove(itemKey);
                            DataUtils.dicEquippedItem.Add(_keyEvolve, iDataEvolve);

                            DestroyItemByKey(itemKey);
                            RemoveItem(itemKey);
                            DataUtils.dicAllEquipment.Add(_keyEvolve, iDataEvolve);
                            CheckInitNewItem(iDataEvolve);
                        }
                        else
                        {
                            /*Item chưa được trang bị*/
                            DestroyItemByKey(itemKey);
                            RemoveItem(itemKey);
                            DataUtils.dicAllEquipment.Add(_keyEvolve, iDataEvolve);
                            CheckInitNewItem(iDataEvolve);
                        }

                        DataUtils.SaveEquippedData();
                        DataUtils.SaveEquipmentData();


                    }
                    else
                    {

                        MainMenuController.Instance.ShowMapNotify("You need to buy <color="+ DataUtils.GetColorByItem(DataUtils.dicAllEquipment[itemKey]) + ">"+ (pieceEvolve - curPiece) + " parts of " + DataUtils.dicAllEquipment[itemKey].itemName + "</color>");
                        GetMoreItem();
                    }
                }
                else
                {
                    if (DataUtils.dicAllEquipment[itemKey].level.Equals("Legendary"))
                    {
                        MainMenuController.Instance.ShowMapNotify("Item has reached max");
                    }
                    else
                    {
                        MainMenuController.Instance.ShowMapNotify("You need to buy <color="+ DataUtils.GetColorByItem(DataUtils.dicAllEquipment[itemKey]) + ">" + ((int)DataUtils.GetPiceByStar(DataUtils.dicAllEquipment[itemKey], true) - DataUtils.dicAllEquipment[itemKey].pices) + " parts of " + DataUtils.dicAllEquipment[itemKey].itemName + "</color>");
                        GetMoreItem();
                    }
                }
            }
        }
    }

    private string EvolveItemLevel(string eLevel)
    {
        string _sResult = "";
        if (!eLevel.Equals("Legendary"))
        {
            if (eLevel.Equals("Normal")) _sResult = "Uncommon";
            else if (eLevel.Equals("Uncommon")) _sResult = "Rare";
            else if (eLevel.Equals("Rare")) _sResult = "Epic";
            else if (eLevel.Equals("Epic")) _sResult = "Legendary";
        }
        else
        {
            MainMenuController.Instance.ShowMapNotify("Item reach max");
        }
        return _sResult;
    }

    string _keySelect1, _keySelect2 = "--";
    public void ChooseItem(ItemData itemData)
    {
        if (itemData == null)
        {
            gWeaponData.SetActive(false);
            txtItemSelectInfo.gameObject.SetActive(false);
            txtDamagePriview.gameObject.SetActive(false);
            imgDamagePriview.gameObject.SetActive(false);
            gItemSelectPriview.SetActive(false);

            gCurrentItemEquip.SetActive(false);
            for (int i = 0; i < trContain.childCount; i++)
            {
                EquipmentItem _iEquipData = trContain.GetChild(i).gameObject.GetComponent<EquipmentItem>();
                _iEquipData.imgSingleSelect.enabled = false;
            }
            gAllStarItemSelect.SetActive(false);
            HideBuyMore();
        }
        else
        {
            itemSelected = itemData;

            if (DataController.primeAccout.isVIP)
            {
                priceUpgrade = (int)(DataUtils.GetItemPrice(itemSelected) - DataUtils.GetItemPrice(itemSelected) * 0.1f);
            }
            else
            {
                priceUpgrade = DataUtils.GetItemPrice(itemSelected);
            }

            txtPriceUpgrade.text = priceUpgrade.ToString();
            txtPieceEvolve.text = DataUtils.GetPiceByStar(itemSelected, true).ToString();
            if (!itemSelected.isUnlock)
            {
                btnReplace.interactable = false;
                btnRemove.gameObject.SetActive(true);
                btnUpgrade.gameObject.SetActive(false);
                if (itemSelected.pices >= DataUtils.GetPiceByStar(itemSelected, false))
                {
                    _keySelect1 = itemSelected.id + "_" + itemSelected.level + "_" + true + "_" + itemSelected.isEquipped;
                    _keySelect1 = itemSelected.id + "_" + itemSelected.level + "_" + true + "_" + true;
                    if (DataUtils.dicAllEquipment.ContainsKey(_keySelect1) || DataUtils.dicAllEquipment.ContainsKey(_keySelect2))
                    {
                        btnReplace.image.sprite = sprButtonCur;
                        btnReplace.gameObject.SetActive(false);
                        btnUnlock.gameObject.SetActive(false);
                        btnGetMore.gameObject.SetActive(true);
                    }
                    else
                    {
                        btnReplace.image.sprite = sprButtonYellow;
                        btnReplace.gameObject.SetActive(false);
                        btnUnlock.gameObject.SetActive(true);
                        btnGetMore.gameObject.SetActive(false);
                    }
                }
                else
                {
                    btnReplace.image.sprite = sprButtonCur;
                    btnReplace.gameObject.SetActive(false);
                    btnUnlock.gameObject.SetActive(false);
                    btnGetMore.gameObject.SetActive(true);
                }
            }
            else
            {
                gAllStarItemSelect.SetActive(true);

                if (itemSelected.isEquipped)
                {
                    btnReplace.image.sprite = sprButton;
                    btnReplace.interactable = false;
                    btnReplace.gameObject.SetActive(true);
                    btnUnlock.gameObject.SetActive(false);
                    btnGetMore.gameObject.SetActive(false);
                    txtEquip.text = "EQUIPPED";
                }
                else
                {
                    btnReplace.image.sprite = sprButtonCur;
                    btnReplace.interactable = true;
                    btnReplace.gameObject.SetActive(true);
                    btnUnlock.gameObject.SetActive(false);
                    btnGetMore.gameObject.SetActive(false);
                    txtEquip.text = "EQUIP";
                }


                btnRemove.interactable = true;
                btnRemove.gameObject.SetActive(false);
                btnUpgrade.gameObject.SetActive(true);
                for (int i = 0; i < allStarsItemSelect.Length; i++)
                {
                    if (i <= itemData.curStar)
                    {
                        allStarsItemSelect[i].sprite = sprYellow;
                    }
                    else
                    {
                        allStarsItemSelect[i].sprite = sprWhite;
                    }
                }

                UpdateStar(itemSelected);


               
            }
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
    private void UpdateStar(ItemData itemData)
    {
        string key = itemData.id + "_" + itemData.level + "_" + itemData.isUnlock + "_" + itemData.isEquipped;
        if (DataUtils.dicAllEquipment.ContainsKey(key))
        {
            if (DataUtils.dicAllEquipment[key].curStar < DataUtils.MAX_STARS - 1)
            {
                imgCoinItemUpdate.gameObject.SetActive(true);
                imgPieceEvolve.gameObject.SetActive(false);
                //txtMaxReach.gameObject.SetActive(false);
                txtUpgrade.text = "UPGRADE";
            }
            else
            {
                imgCoinItemUpdate.gameObject.SetActive(false);
                imgPieceEvolve.gameObject.SetActive(true);
                txtUpgrade.text = "EVOLVE";
                //txtMaxReach.gameObject.SetActive(true);
                //MainMenuController.Instance.ShowMapNotify("Item reached max. Evolution to be stronger.");
            }
        }

        for (int i = 0; i < allStarsItemSelect.Length; i++)
        {
            if (i <= itemData.curStar)
            {
                allStarsItemSelect[i].sprite = sprYellow;
            }
            else
            {
                allStarsItemSelect[i].sprite = sprWhite;
            }
        }

        UpdateTextDes();

        btnUpgrade.interactable = DataUtils.IsCanEvolve(itemSelected);
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
            int curStar = itemEquipped.curStar < DataUtils.MAX_STARS ? itemEquipped.curStar : 4;

            string keyEquipped = itemEquipped.id + "_" + itemEquipped.level;
            _keyItemEquipped = itemEquipped.id + "_" + itemEquipped.level + "_" + itemEquipped.isUnlock + "_" + itemEquipped.isEquipped;

            imgCurItemPriview.sprite = DataUtils.GetSpriteByName(itemEquipped.id, MainMenuController.Instance.allSpriteData);
            if (DataUtils.dicEquippedItem.ContainsKey(_keyItemEquipped))
                txtCurItemName.text = DataUtils.dicEquippedItem[_keyItemEquipped].itemName;

            if (itemData.isUnlock)
            {
                #region Item Equipped Info
                if (itemEquipped.type.Contains("WEAPON"))
                {
                    txtCurDamagePriview.text = "" + (DataUtils.GetRealFloat((DataUtils.dicWeapon[keyEquipped].DmgValue[curStar] * 10)));
                    txtCurCritDamage.text = "Crit Damage: <color=white>" +(DataUtils.GetRealFloat(DataUtils.dicWeapon[keyEquipped].CritDmgValue[curStar])) + "</color>";
                    txtCurAttSpeed.text = "Attack Speed: <color=white>" +(DataUtils.GetRealFloat(DataUtils.dicWeapon[keyEquipped].AtksecValue[curStar])) + "</color>";
                    txtCurCritRate.text = "Crit Rate: <color=white>" + (DataUtils.GetRealFloat(DataUtils.dicWeapon[keyEquipped].CritRateValue[curStar])) + "</color>";
                    txtCurRange.text = "Range: <color=white>" +(DataUtils.GetRealFloat(DataUtils.dicWeapon[keyEquipped].AtkRangeValue[curStar])) + "</color>";
                    txtCurMagazine.text = "Magazine: <color=white>" + (DataUtils.GetRealFloat(DataUtils.dicWeapon[keyEquipped].MagazineValue[curStar])) + "</color>";

                    txtItemInfoEquip.gameObject.SetActive(false);
                    gCurWeaponData.SetActive(true);
                    txtCurDamagePriview.gameObject.SetActive(true);
                    imgCurDamagePriview.gameObject.SetActive(true);
                }
                else
                {
                    txtItemInfoEquip.text = DataUtils.GetItemInfo(itemEquipped);

                    gCurWeaponData.SetActive(false);
                    txtItemInfoEquip.gameObject.SetActive(true);
                    txtCurDamagePriview.gameObject.SetActive(false);
                    imgCurDamagePriview.gameObject.SetActive(false);
                }
                UpdateRotation(itemEquipped, imgCurItemPriview.GetComponent<RectTransform>());
                gCurItemPriview.SetActive(true);

                for (int i = 0; i < allStars.Length; i++)
                {
                    if (i <= itemEquipped.curStar)
                    {
                        allStars[i].sprite = sprYellow;
                    }
                    else
                    {
                        allStars[i].sprite = sprWhite;
                    }
                }
                #endregion
                gCurrentItemEquip.SetActive(true);
            }
            else
            {
                gCurrentItemEquip.SetActive(false);
            }



            if (itemData.type.Contains("WEAPON"))
            {
                int itemData_curStar = itemData.curStar < DataUtils.MAX_STARS ? itemData.curStar : 4;
                txtDamagePriview.text = "<color=" + GetColorByItemData(DataUtils.dicWeapon[keyItem].DmgValue[itemData_curStar], DataUtils.dicWeapon[keyEquipped].DmgValue[curStar]) + ">" + (DataUtils.GetRealFloat(DataUtils.dicWeapon[keyItem].DmgValue[itemData_curStar] * 10)) + "</color>";
                txtCritDamage.text = "Crit Damage: <color=" + GetColorByItemData(DataUtils.dicWeapon[keyItem].CritDmgValue[itemData_curStar], DataUtils.dicWeapon[keyEquipped].CritDmgValue[curStar]) + ">" + (DataUtils.GetRealFloat(DataUtils.dicWeapon[keyItem].CritDmgValue[itemData_curStar])) + "</color>";
                txtAttSpeed.text = "Attack Speed: <color=" + GetColorByItemData(DataUtils.dicWeapon[keyItem].AtksecValue[itemData_curStar], DataUtils.dicWeapon[keyEquipped].AtksecValue[curStar]) + ">" + (DataUtils.GetRealFloat(DataUtils.dicWeapon[keyItem].AtksecValue[itemData_curStar])) + "</color>";
                txtCritRate.text = "Crit Rate: <color=" + GetColorByItemData(DataUtils.dicWeapon[keyItem].CritRateValue[itemData_curStar], DataUtils.dicWeapon[keyEquipped].CritRateValue[curStar]) + ">" + (DataUtils.GetRealFloat(DataUtils.dicWeapon[keyItem].CritRateValue[itemData_curStar])) + "</color>";
                txtRange.text = "Range: <color=" + GetColorByItemData(DataUtils.dicWeapon[keyItem].AtkRangeValue[itemData_curStar], DataUtils.dicWeapon[keyEquipped].AtkRangeValue[curStar]) + ">" + (DataUtils.GetRealFloat(DataUtils.dicWeapon[keyItem].AtkRangeValue[itemData_curStar])) + "</color>";
                txtMagazine.text = "Magazine: <color=" + GetColorByItemData(DataUtils.dicWeapon[keyItem].MagazineValue[itemData_curStar], DataUtils.dicWeapon[keyEquipped].MagazineValue[curStar]) + ">" + (DataUtils.GetRealFloat(DataUtils.dicWeapon[keyItem].MagazineValue[itemData_curStar])) + "</color>";

                txtItemSelectInfo.gameObject.SetActive(false);
                gWeaponData.SetActive(true);
                txtDamagePriview.gameObject.SetActive(true);
                imgDamagePriview.gameObject.SetActive(true);
            }
            else
            {
                gWeaponData.SetActive(false);
                txtDamagePriview.gameObject.SetActive(false);
                imgDamagePriview.gameObject.SetActive(false);
                txtItemSelectInfo.gameObject.SetActive(true);
            }
            UpdateRotation(itemData, imgItemPriview.GetComponent<RectTransform>());
            gItemSelectPriview.SetActive(true);

            if (DataUtils.dicAllEquipment.ContainsKey(_keyItemEquipped))
                imgItemEquipPriview.sprite = DataUtils.GetSpriteByType(DataUtils.dicAllEquipment[_keyItemEquipped]);
        }

        if (!itemData.type.Contains("WEAPON"))
        {
            txtItemSelectInfo.text = DataUtils.GetItemInfo(DataUtils.dicAllEquipment[_keyItemSelected]);
            gWeaponData.SetActive(false);
            txtItemSelectInfo.gameObject.SetActive(true);
        }
        else
        {
            txtItemSelectInfo.gameObject.SetActive(false);
        }

        UpdateRotation(itemData, imgItemPriview.GetComponent<RectTransform>());
        imgItemSelectPriview.sprite = DataUtils.GetSpriteByType(itemData);
        gItemSelectPriview.SetActive(true);
    }

    private void UpdateTextDes()
    {
        FillEquipmentInfo(itemSelected);
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

    public void MultiSelect()
    {
        isMultiSell = true;
        ChooseItem(null);
        gSellGroup.SetActive(true);
    }
    public void DoCancelDisassemble()
    {
        for (int i = 0; i < trContain.childCount; i++)
        {
            EquipmentItem _iEquipData = trContain.GetChild(i).gameObject.GetComponent<EquipmentItem>();
            _iEquipData.imgMultiSelect.enabled = false;
        }
        lstAllItemSell.Clear();
        isMultiSell = false;
        gSellGroup.SetActive(false);
    }
    public void DoDisassblem()
    {
        if (lstAllItemSell.Count > 0)
        {
            double dTotal = 0;
            foreach (EquipmentItem epi in lstAllItemSell)
            {
                dTotal += DataUtils.GetSellPrice(epi.itemData);
                DataUtils.dicAllEquipment.Remove(epi.itemKey);
            }

            for (int i = 0; i < lstAllItemSell.Count; i++)
            {
                Destroy(lstAllItemSell[i].gameObject);
            }

            DataUtils.SaveEquipmentData();
            ChooseItem(null);
            DoCancelDisassemble();
            DataUtils.AddCoinAndGame((int)dTotal, 0);
        }
    }
    public void AddItemToList(EquipmentItem epi)
    {
        lstAllItemSell.Add(epi);
    }
    public void RemoveItemFromList(EquipmentItem epi)
    {
        lstAllItemSell.Remove(epi);
    }
    public List<EquipmentItem> lstAllItemSell = new List<EquipmentItem>();

    public void HideBuyMore()
    {
        gBuyMore.SetActive(false);
    }
    public void GetFreeItem()
    {
        AdsManager.Instance.ShowRewardedVideo((b) =>
        {
            if (b)
            {
                DataUtils.TakeItem(itemSelected, 1);
                for (int i = 0; i < trContain.childCount; i++)
                {
                    EquipmentItem _iEquipData = trContain.GetChild(i).gameObject.GetComponent<EquipmentItem>();
                    _iEquipData.CheckItemUnlock();
                }
            }
        });
    }
    public void GetItemByDiamond(int totalPiece)
    {
        int _diamond = (int)(totalPiece * DataUtils.GetDiamondPrice(itemSelected));
        if (DataUtils.playerInfo.gems >= _diamond)
        {
            string _key = itemSelected.id + "_" + itemSelected.level.ToString() + "_" + false + "_" + itemSelected.isEquipped;
            if (DataUtils.dicAllEquipment.ContainsKey(_key))
            {
                DataUtils.dicAllEquipment[_key].pices += totalPiece;
                DataUtils.AddCoinAndGame(0, -_diamond);
            }
            else
            {
                ItemData iDataNew = new ItemData();
                iDataNew.id = itemSelected.id;
                iDataNew.type = itemSelected.type;
                iDataNew.level = itemSelected.level;
                iDataNew.isUnlock = false;
                iDataNew.pices = totalPiece;
                iDataNew.itemName = itemSelected.itemName;
                iDataNew.isEquipped = false;
                string _keyNew = iDataNew.id + "_" + iDataNew.level + "_" + false + "_" + iDataNew.isEquipped;
                if (!DataUtils.dicAllEquipment.ContainsKey(_keyNew))
                {
                    DataUtils.dicAllEquipment.Add(_keyNew, iDataNew);
                    RefreshInventory(iDataNew, false);
                    DataUtils.AddCoinAndGame(0, -_diamond);
                }
                else
                {
                    DataUtils.dicAllEquipment[_keyNew].pices += totalPiece;
                    DataUtils.AddCoinAndGame(0, -_diamond);
                }
            }

            DataUtils.SaveEquipmentData();


            //DataUtils.TakeItem(itemSelected, totalPiece);
            for (int i = 0; i < trContain.childCount; i++)
            {
                EquipmentItem _iEquipData = trContain.GetChild(i).gameObject.GetComponent<EquipmentItem>();
                _iEquipData.CheckItemUnlock();
            }
        }
        else
        {
            MainMenuController.Instance.ShowMapNotify("Not enought gem");
        }
    }
}