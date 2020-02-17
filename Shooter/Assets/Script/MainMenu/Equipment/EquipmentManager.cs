using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance;
    public Image imgItemPriview, imgDamagePriview;
    public TextMeshProUGUI txtItemName, txtDamagePriview, txtAttSpeed, txtCritRate, txtCritDamage, txtRange, txtMagazine;
    public GameObject gWeaponData;
    public GameObject gItemSelectPriview;
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

    string key = "";
    public void InitAllItems()
    {
        foreach (ItemData itemData in DataUtils.dicAllEquipment.Values)
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

    public void RefreshInventory(ItemData itemNew)
    {
        key = itemNew.id + "_" + itemNew.level + "_" + itemNew.isUnlock;
        gItemClone = Instantiate(gItems);
        gItemClone.name = itemNew.id + "_" + itemNew.level + "_" + itemNew.isUnlock;
        EquipmentItem item = gItemClone.GetComponent<EquipmentItem>();
        item.itemKey = key;
        item.itemData = itemNew;
        item.imgItemPriview.sprite = MainMenuController.Instance.GetSpriteByName(itemNew.id);

        gItemClone.transform.SetParent(trContain, false);
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
    }
    const string ALL_EQUIP = "ALL";
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

    public void ChooseItem(ItemData itemData)
    {
        if(itemData == null) {
            gWeaponData.SetActive(false);
            txtDamagePriview.gameObject.SetActive(false);
            imgDamagePriview.gameObject.SetActive(false);

            gItemSelectPriview.SetActive(false);


            for (int i = 0; i < trContain.childCount; i++)
            {
                EquipmentItem _iEquipData = trContain.GetChild(i).gameObject.GetComponent<EquipmentItem>();
                _iEquipData.imgSingleSelect.enabled = false;
            }
        }
        else
        {
            #region Fill Info for EquipmentItem
            imgItemPriview.sprite = MainMenuController.Instance.GetSpriteByName(itemData.id);
            string _key = itemData.id + "_" + itemData.level + "_" + itemData.isUnlock;
            string keyItem = itemData.id + "_" + itemData.level;
            txtItemName.text = DataUtils.dicAllEquipment[_key].itemName;
            if (itemData.type.Contains("WEAPON"))
            {
                txtDamagePriview.text = "" + DataUtils.dicWeapon[keyItem].DmgValue[itemData.curStar];
                txtCritDamage.text = "Crit Damage: <color=white>" + DataUtils.dicWeapon[keyItem].CritDmgValue[itemData.curStar] + "</color>";
                txtAttSpeed.text = "Attack Speed: <color=white>" + DataUtils.dicWeapon[keyItem].AtksecValue[itemData.curStar] + "</color>";
                txtCritRate.text = "Crit Rate: <color=white>" + DataUtils.dicWeapon[keyItem].CritRateValue[itemData.curStar] + "</color>";
                txtRange.text = "Range: <color=white>" + DataUtils.dicWeapon[keyItem].AtkRangeValue[itemData.curStar] + "</color>";
                txtMagazine.text = "Magazine: <color=white>" + DataUtils.dicWeapon[keyItem].MagazineValue[itemData.curStar] + "</color>";

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
            gItemSelectPriview.SetActive(true);
            #endregion

            #region Display Selected Image
            itemSelected = itemData;
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

    public void BackToMainMenu()
    {
        gameObject.SetActive(false);
    }
}
