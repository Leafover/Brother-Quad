using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisassembleManager : MonoBehaviour
{
    public Text txtReward;
    public Image imgQuality, imgItemPriview;
    public ItemData iDisassemble;
    public GameObject gDisassemble;
    public string keyItem;
    string keyEquipped;
    double dbValue;
    private void OnEnable()
    {
        imgItemPriview.sprite = DataUtils.GetSpriteByName(iDisassemble.id, MainMenuController.Instance.allSpriteData);
        keyEquipped = iDisassemble.id + "_" + iDisassemble.level;
        keyItem = keyEquipped + "_" + iDisassemble.isUnlock + "_" + iDisassemble.isEquipped;
        Debug.LogError("keyEquipped: " + keyEquipped);
        switch (iDisassemble.type)
        {
            case "ARMOR":
                dbValue = DataUtils.dicArmor[keyEquipped].GiaKhiRaDo;
                break;
            case "BAG":
                dbValue = DataUtils.dicBag[keyEquipped].GiaKhiRaDo;
                break;
            case "GLOVES":
                dbValue = DataUtils.dicGloves[keyEquipped].GiaKhiRaDo;
                break;
            case "HELMET":
                dbValue = DataUtils.dicHelmet[keyEquipped].GiaKhiRaDo;
                break;
            case "SHOES":
                dbValue = DataUtils.dicShoes[keyEquipped].GiaKhiRaDo;
                break;
            case "WEAPON":
                dbValue = DataUtils.dicWeapon[keyEquipped].GiaKhiRaDo;
                break;
        }
        imgQuality.sprite = DataUtils.GetSpriteByType(iDisassemble);
        txtReward.text = "x " + dbValue;
    }
    public void DisassebleItem()
    {
        DataUtils.AddCoinAndGame((int)dbValue, 0);
        EquipmentManager.Instance.DoDisassemble(iDisassemble, keyItem);
        ClosePopup();
    }
    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        iDisassemble = null;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePopup();
        }
    }
}
