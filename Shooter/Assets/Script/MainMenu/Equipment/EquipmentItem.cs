using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static DataUtils;

public class EquipmentItem : MonoBehaviour
{
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
        
    }
    // Start is called before the first frame update
    void Start()
    {
        btnItem.onClick.AddListener(() => {
            EquipmentManager.Instance.ChooseItem(itemData);
        });
        if (!string.IsNullOrEmpty(itemKey))
        {
            itemData = dicAllEquipment[itemKey];
            CheckItemUnlock();
        }
    }
    private void CheckItemUnlock()
    {
        if (itemData.isUnlock)
        {
            imgQuantity.gameObject.SetActive(itemData.quantity == 0 ? false : true);
            txtQuantity.text = ""+itemData.quantity;
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
