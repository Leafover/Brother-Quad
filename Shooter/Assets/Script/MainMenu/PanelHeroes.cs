﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Spine.Unity;
using Spine;

public class PanelHeroes : MonoBehaviour
{
    public static PanelHeroes Instance;
    public TextMeshProUGUI txtPlayerName;
    public SkeletonGraphic skleCur;
    public GameObject gP1, gP2;
    public HeroChoose[] allHeroes;

    private Skin[] skins;

    public GameObject gAllEquippedItem;
    public GameObject gHeroInfo;
    public GameObject gItem, gItemClone;
    public Transform trParent;
    public Transform trAllHeroContain;
    public Sprite sprStar, sprStarUnlock;
    public Image[] imgAllStars;
    public Image imgArrow;
    public TextMeshProUGUI txtLevel;
    public Text txtCurPice;
    public Text txtCurHealth, txtCurDamage;
    public Text txtPriceUpdate;
    public Text txtPiceSelected;
    public TextMeshProUGUI txtHealth, txtDamage, txtAttSpeed, txtCritDamage, txtCritRate, txtDefRate;
    public TextMeshProUGUI txtHealthUP, txtDamageUP, txtAttSpeedUP, txtCritDamageUP, txtCritRateUP;
    public ParticleSystem pEvolve;
    public Text txtPice;
    public Image imgProgress;
    public TextMeshProUGUI txtTotalPower, txtHealthDis, txtDamageDis;
    public Button[] btnTabs;
    public Sprite sprSelect, sprDeSelect;
    public List<EquipmentItem> lstEquip;
    public Dictionary<string, ItemData> dicAllEquip = new Dictionary<string, ItemData>();
    public HeroDataInfo heroSelected;

    public int _indexChoose = 0;

    private PlayerData pData, pNext;
    private double priceUpdate;

    private string keyEquipped;
    ItemData itemEquipped = null;
    private int curWeponStar = 0, nextWeaponStar = 0;

    private void Awake()
    {
        Instance = this;
        pEvolve.Stop();

    }

    public void ChangeAnim(int _index)
    {
        switch (_index)
        {
            case 1:
                gP1.SetActive(true);
                gP2.SetActive(false);
                break;
            case 2:
                gP1.SetActive(false);
                gP2.SetActive(true);
                break;
            default:
                gP1.SetActive(true);
                gP2.SetActive(false);
                break;
        }
    }
    private void Start()
    {
        HeroOnClick(0);
    }
    private void OnEnable()
    {
        ChangeAnim(DataUtils.HeroIndex() + 1);

        for (int i = 0; i < lstEquip.Count; i++)
        {
            if (!dicAllEquip.ContainsKey(lstEquip[i].itemData.type))
            {
                dicAllEquip.Add(lstEquip[i].itemData.type, lstEquip[i].itemData);
            }
        }
        if (heroSelected == null)
        {
            heroSelected = DataUtils.heroInfo;
        }

        MyAnalytics.LogOpenHeroTab();
        ChooseTab(0);

        FillHeroData(DataUtils.HeroIndex());
        InitEquippedItem();


        Debug.LogError(DataController.instance.playerData[DataUtils.HeroIndex()].playerData[DataUtils.heroInfo.level < DataUtils.MAX_LEVEL_HERO ? DataUtils.heroInfo.level : DataUtils.MAX_LEVEL_HERO - 1].DmgGrenade);
    }
    private void InitEquippedItem()
    {
        for (int i = 0; i < lstEquip.Count; i++)
        {
            foreach (ItemData itemData in DataUtils.dicEquippedItem.Values)
            {
                if (itemData.type == lstEquip[i].itemData.type)
                {
                    lstEquip[i].itemData = itemData;
                    lstEquip[i].imgItemPriview.sprite = DataUtils.GetSpriteByName(itemData.id, MainMenuController.Instance.allSpriteData);
                    lstEquip[i].imgPart.enabled = false;
                    lstEquip[i].CheckItemUnlock();
                    lstEquip[i].gameObject.SetActive(true);
                }
            }
        }
    }
    public void FillHeroData(int _hIndex)
    {
        Debug.LogError("heroSelected.level: " + heroSelected.level);
        for (int i = 0; i < /*DataUtils.playerInfo.level*/imgAllStars.Length; i++)
        {
            if (i <= heroSelected.level)
                imgAllStars[i].sprite = sprStar;
            else
                imgAllStars[i].sprite = sprStarUnlock;
        }

        //txtPlayerName.text = heroSelected.name;

        FillDataPlayer(_hIndex);
    }
    private void FillDataPlayer(int _heroIndex)
    {
        foreach (ItemData _id in DataUtils.dicEquippedItem.Values)
        {
            if (_id.type.Equals("WEAPON"))
            {
                itemEquipped = _id;
                break;
            }
        }

        keyEquipped = itemEquipped.id + "_" + itemEquipped.level;
        curWeponStar = itemEquipped.curStar;
        nextWeaponStar = itemEquipped.curStar + 1 > 4 ? itemEquipped.curStar : itemEquipped.curStar + 1;
        pData = DataController.instance.playerData[/*0*/_heroIndex].playerData[heroSelected.level < DataUtils.MAX_LEVEL_HERO ? heroSelected.level : DataUtils.MAX_LEVEL_HERO - 1];
        pNext = DataController.instance.playerData[/*0*/_heroIndex].playerData[heroSelected.level + 1 < DataUtils.MAX_LEVEL_HERO ? heroSelected.level + 1 : DataUtils.MAX_LEVEL_HERO - 1];

        txtHealth.text = pData.hp.ToString();
        txtHealthUP.text = DataUtils.DisplayRichText(pData.hp, pNext.hp);


        txtDamage.text = "" + 10 * GetDoublevalue(DataUtils.dicWeapon[keyEquipped].DmgValue[curWeponStar]);
        txtDamageUP.text = DataUtils.DisplayRichText(GetDoublevalue(DataUtils.dicWeapon[keyEquipped].DmgValue[curWeponStar]) * 10, GetDoublevalue(DataUtils.dicWeapon[keyEquipped].DmgValue[nextWeaponStar]) * 10);

        txtAttSpeed.text = GetDoublevalue(DataUtils.dicWeapon[keyEquipped].BulletSpeedValue[curWeponStar]).ToString();
        txtAttSpeedUP.text = DataUtils.DisplayRichText(GetDoublevalue(DataUtils.dicWeapon[keyEquipped].BulletSpeedValue[curWeponStar]), GetDoublevalue(DataUtils.dicWeapon[keyEquipped].BulletSpeedValue[nextWeaponStar]));

        txtCritDamage.text = GetDoublevalue(DataUtils.dicWeapon[keyEquipped].CritDmgValue[curWeponStar]).ToString();
        txtCritDamageUP.text = DataUtils.DisplayRichText(GetDoublevalue(DataUtils.dicWeapon[keyEquipped].CritDmgValue[curWeponStar]), GetDoublevalue(DataUtils.dicWeapon[keyEquipped].CritDmgValue[nextWeaponStar]));

        txtCritRate.text = GetDoublevalue(DataUtils.dicWeapon[keyEquipped].CritRateValue[curWeponStar]).ToString();
        txtCritRateUP.text = DataUtils.DisplayRichText(GetDoublevalue(DataUtils.dicWeapon[keyEquipped].CritRateValue[curWeponStar]), GetDoublevalue(DataUtils.dicWeapon[keyEquipped].CritRateValue[nextWeaponStar]));

        txtCurDamage.text = "" + 10 * GetDoublevalue(DataUtils.dicWeapon[keyEquipped].DmgValue[curWeponStar]);

        if (!DataController.primeAccout.isVIP)
            priceUpdate = 165 * pNext.SoManhYeuCau * pNext.Giamua1manh;
        else
        {
            double price__ = 165 * pNext.SoManhYeuCau * pNext.Giamua1manh;
            priceUpdate = (int)(price__ - 165 * pNext.SoManhYeuCau * pNext.Giamua1manh * 0.1f);
        }

        txtPriceUpdate.text = priceUpdate.ToString("#,0");


        txtDamageDis.text = "" + 10 * GetDoublevalue(DataUtils.dicWeapon[keyEquipped].DmgValue[curWeponStar]);
        txtHealthDis.text = pData.hp.ToString();
        txtTotalPower.text = pData.MoveSpeed.ToString();

        txtPice.text = DataUtils.dicAllHero[heroSelected.id.Trim()].pices + "/" + pNext.SoManhYeuCau;
        imgProgress.fillAmount = DataUtils.dicAllHero[heroSelected.id.Trim()].pices * 1.0f / (float)pNext.SoManhYeuCau;
    }

    private double GetDoublevalue(double db)
    {
        return System.Math.Floor(db * 100) / 100;
    }
    public void EvolveHero()
    {
        if (DataUtils.playerInfo.coins >= priceUpdate)
        {
            if (/*DataUtils.playerInfo.level*/heroSelected.level < DataUtils.MAX_LEVEL_HERO)
            {
                if (DataUtils.dicAllHero[heroSelected.id].pices >= (int)pNext.SoManhYeuCau)
                {
                    DataUtils.playerInfo.level += 1;
                    //heroSelected.level += 1;
                    DataUtils.dicAllHero[heroSelected.id].level += 1;

                    DataUtils.dicAllHero[heroSelected.id].pices -= (int)pNext.SoManhYeuCau;

                    DataUtils.AddCoinAndGame((int)-priceUpdate, 0);
                    pEvolve.Play();

                    FillHeroData(/*DataUtils.HeroIndex()*/_indexChoose);
                    DataUtils.SavePlayerData();


                    DataUtils.ChooseHero(DataUtils.dicAllHero[heroSelected.id]);
                    DataUtils.dicAllHero[heroSelected.id] = DataUtils.dicAllHero[heroSelected.id];
                    DataUtils.SaveAllHero();
                }
                else
                {
                    MainMenuController.Instance.ShowMapNotify("Not enough material.");
                }
            }
            else
            {
                MainMenuController.Instance.ShowMapNotify("Hero has reached the maximum level");
            }
        }
        else
        {
            MainMenuController.Instance.ShowMapNotify("Not enough coins to level up.");
        }
    }

    public void ChooseTab(int index_)
    {
        for (int i = 0; i < btnTabs.Length; i++)
        {
            if (i == index_)
            {
                btnTabs[i].image.sprite = sprSelect;
            }
            else btnTabs[i].image.sprite = sprDeSelect;
        }

        switch (index_)
        {
            case 0:
                gAllEquippedItem.SetActive(false);
                gHeroInfo.SetActive(true);
                break;
            case 1:
                gAllEquippedItem.SetActive(true);
                gHeroInfo.SetActive(false);
                break;
        }
    }

    public void HeroOnClick(int _index)
    {
        HeroChoose heroChoose = allHeroes[_index];

        if (heroChoose.heroData == null)
        {
            MainMenuController.Instance.ShowMapNotify("Hero not yet unlock");
        }
        else
        {
            FillData(heroChoose);

            for (int i = 0; i < allHeroes.Length; i++)
            {
                HeroChoose _h = allHeroes[i];

                if (_h == heroChoose)
                {
                    _h.imgSelected.enabled = true;
                }
                else
                {
                    _h.imgSelected.enabled = false;
                }
            }
            ChangeAnim(_index + 1);
            MainMenuController.Instance.heroSelectIndex = _index;
            _indexChoose = _index;

        }
    }

    private void FillData(HeroChoose heroChoose)
    {
        heroChoose.imgSelected.enabled = true;
        heroSelected = DataUtils.dicAllHero[heroChoose.heroID];
        FillHeroData(heroChoose.heroIndex - 1);
        DataUtils.heroInfo = DataUtils.dicAllHero[heroChoose.heroID];
    }
}