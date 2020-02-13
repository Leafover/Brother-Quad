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
    private void OnEnable()
    {
        
    }

    string key = "";
    public void InitAllItems()
    {
        foreach (ItemData itemData in DataUtils.dicAllEquipment.Values)
        {
            Debug.LogError(itemData.id + " vs " + itemData.isUnlock);
            key = itemData.id + "_" + itemData.level;
            gItemClone = Instantiate(gItems);
            gItemClone.name = itemData.id + "_" + itemData.level;
            EquipmentItem item = gItemClone.GetComponent<EquipmentItem>();
            item.itemKey = key;
            item.imgItemPriview.sprite = MainMenuController.Instance.GetSpriteByName(itemData.id);

            gItemClone.transform.SetParent(trContain, false);
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
    }
    public void ChooseItem(ItemData itemData)
    {
        Debug.LogError("ItemName: " + itemData.id + " vs " + itemData.type);
        imgItemPriview.sprite = MainMenuController.Instance.GetSpriteByName(itemData.id);
        string _key = itemData.id + "_" + itemData.level;
        txtItemName.text = DataUtils.dicAllEquipment[_key].itemName;
        if (itemData.type.Contains("WEAPON"))
        {
            txtDamagePriview.text = ""+ DataUtils.dicWeapon[_key].DmgValue[itemData.curStar];
            txtCritDamage.text = "" + DataUtils.dicWeapon[_key].CritDmgValue[itemData.curStar];
            txtAttSpeed.text = "" + DataUtils.dicWeapon[_key].AtksecValue[itemData.curStar];
            txtCritRate.text = "" + DataUtils.dicWeapon[_key].CritRateValue[itemData.curStar];
            txtRange.text = "" + DataUtils.dicWeapon[_key].AtkRangeValue[itemData.curStar];
            txtMagazine.text = "" + DataUtils.dicWeapon[_key].MagazineValue[itemData.curStar];

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
