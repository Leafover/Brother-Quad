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
        btnItem = GetComponent<Button>();
        if (!string.IsNullOrEmpty(itemKey))
        {
            CheckItemUnlock();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        btnItem.onClick.AddListener(() =>
        {
            if (!MainMenuController.Instance.gPanelHeroes.activeSelf)
            {
                if (!EquipmentManager.Instance.isMultiSell)
                    EquipmentManager.Instance.ChooseItem(itemData);
                else
                {
                    isSelected = !isSelected;
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
        if (EquipmentManager.Instance != null)
        {
            if (EquipmentManager.Instance.tabSelected == 0)
            {
                if (itemData.pices == 0)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(!itemData.isUnlock);
                }
            }
        }
    }

    private float GetPercent()
    {
        float res = 0.0f;
        float _p = GetPiceByStar(itemData, false);
       
        res = itemData.pices * 1.0f / _p;
        if(res <= 1)
        {
            txtFillProgress.text = itemData.pices + "/" + (int)_p;
        }
        else
        {
            txtFillProgress.text = itemData.pices.ToString();
        }
        return res;
    }
}
