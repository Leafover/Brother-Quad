using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static DataUtils;

public class EquipmentItem : MonoBehaviour
{
    public Sprite sprNormal, sprUncommon, sprRare, sprEpic, sprLegendary;
    public Image imgQuality;
    public Image imgSingleSelect;
    public Image imgItemPriview;
    public Image imgProgress;
    public Image imgFillProgress;
    public Text txtFillProgress;
    public TextMeshProUGUI txtQuantity;
    public Image imgQuantity;
    public Image imgMultiSelect;
    public Image imgPart;

    public string itemKey = "";
    private bool isSelected;
    //[HideInInspector]
    public ItemData itemData;
    Button btnItem;
    private void OnEnable()
    {
        //Debug.LogError("OnEnable: " + gameObject.name);
        btnItem = GetComponent<Button>();
        if (!string.IsNullOrEmpty(itemKey))
        {
            if (itemData != null)
            {
                //itemKey = itemData.id + "_" + itemData.level + "_" + itemData.isUnlock + "_" + itemData.isEquipped;
                //if (dicAllEquipment.ContainsKey(itemKey))
                //{
                //    itemData = dicAllEquipment[itemKey];
                //    if (itemData.isUnlock)
                //    {
                //        gameObject.name = itemKey;
                //    }
                //}
                //else
                //{
                //    Destroy(gameObject);
                //}

                //EquipmentManager.Instance.UpdateRotation(itemData, imgItemPriview.GetComponent<RectTransform>());

                Refresh();
            }

            CheckItemUnlock();
        }
    }

    public void Refresh()
    {
        if (!dicAllEquipment.ContainsKey(itemKey))
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        btnItem.onClick.AddListener(() =>
        {
            if (!EquipmentManager.Instance.isMultiSell)
                EquipmentManager.Instance.ChooseItem(itemData);
            else {
                isSelected = !isSelected;
                if (itemData.isUnlock) {
                    if (isSelected)
                    {
                        EquipmentManager.Instance.AddItemToList(this);
                        imgMultiSelect.enabled = true;
                    }
                    else
                    {
                        EquipmentManager.Instance.RemoveItemFromList(this);
                        imgMultiSelect.enabled = false;
                    }
                }
            }
        });
        if (!string.IsNullOrEmpty(itemKey))
        {
            itemData = dicAllEquipment[itemKey];
            CheckItemUnlock();
            EquipmentManager.Instance.UpdateRotation(itemData, imgItemPriview.GetComponent<RectTransform>());
        }
    }
    Sprite sprimgQualityTemp;
    public void CheckItemUnlock()
    {
        //Debug.LogError("::: " + dicAllEquipment.ContainsKey(itemKey));
        if (itemData.isEquipped) gameObject.SetActive(false);
        if(!dicAllEquipment.ContainsKey(itemKey)) gameObject.SetActive(false);

        #region Check Item quality
        switch (itemData.level)
        {
            case "Normal":
                sprimgQualityTemp = sprNormal;
                break;
            case "Uncommon":
                sprimgQualityTemp = sprUncommon;
                break;
            case "Rare":
                sprimgQualityTemp = sprRare;
                break;
            case "Epic":
                sprimgQualityTemp = sprEpic;
                break;
            case "Legendary":
                sprimgQualityTemp = sprLegendary;
                break;
            default:
                sprimgQualityTemp = sprNormal;
                break;
        }
        imgQuality.sprite = sprimgQualityTemp;
        #endregion
        if (itemData.isUnlock)
        {
            imgQuantity.gameObject.SetActive(itemData.quantity == 0 ? false : true);
            txtQuantity.text = "" + itemData.quantity;
            imgProgress.gameObject.SetActive(false);
            imgPart.gameObject.SetActive(false);
        }
        else
        {
            imgProgress.gameObject.SetActive(true);
            imgPart.gameObject.SetActive(true);
            imgFillProgress.fillAmount = GetPercent();
        }
    }

    private float GetPercent()
    {
        float res = 0.0f;
        float _p = GetPiceByStar(itemData);
        txtFillProgress.text = itemData.pices + "/" + (int)_p;
        res = itemData.pices * 1.0f / _p;
        return res;
    }
}
