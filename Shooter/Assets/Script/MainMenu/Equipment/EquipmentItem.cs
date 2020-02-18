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
    //[HideInInspector]
    public ItemData itemData;
    Button btnItem;
    private void OnEnable()
    {
        btnItem = GetComponent<Button>();
        if (!string.IsNullOrEmpty(itemKey))
        {
            if (itemData != null)
            {
                itemKey = itemData.id + "_" + itemData.level + "_" + itemData.isUnlock;
                if (dicAllEquipment.ContainsKey(itemKey))
                {
                    itemData = dicAllEquipment[itemKey];
                    if (itemData.isUnlock)
                    {
                        gameObject.name = itemKey;
                    }
                }
                else
                {
                    Destroy(gameObject);
                }

                EquipmentManager.Instance.UpdateRotation(itemData, imgItemPriview.GetComponent<RectTransform>());
            }

            CheckItemUnlock();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        btnItem.onClick.AddListener(() =>
        {
            EquipmentManager.Instance.ChooseItem(itemData);
        });
        if (!string.IsNullOrEmpty(itemKey))
        {
            itemData = dicAllEquipment[itemKey];
            CheckItemUnlock();
            EquipmentManager.Instance.UpdateRotation(itemData, imgItemPriview.GetComponent<RectTransform>());
        }
    }
    Sprite sprimgQualityTemp;
    private void CheckItemUnlock()
    {
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
