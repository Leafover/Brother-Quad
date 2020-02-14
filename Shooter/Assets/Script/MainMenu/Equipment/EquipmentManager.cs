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
    public Sprite sprSelect, sprUnSelect;
    public Button[] btnTabs;
    public Transform trContain;
    public GameObject gItems;
    private GameObject gItemClone;

    public List<ItemData> lstAllItemData = new List<ItemData>();


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

    }

    string key = "";
    public void InitAllItems()
    {
        foreach (ItemData itemData in DataUtils.dicAllEquipment.Values)
        {
            Debug.LogError(itemData.id + " vs " + itemData.isUnlock);
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
        Debug.LogError("1-------");
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
    }
    public void ChooseItem(ItemData itemData)
    {
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
    }
    public void BackToMainMenu()
    {
        gameObject.SetActive(false);
    }
}
