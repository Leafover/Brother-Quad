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

    public GameObject gParts, gSkillPopup;
    public HeroSkill hSkillDefault, hSkillP1, hSkillP2;
    public Text txtPlayerName;
    public TextMeshProUGUI txtHealth, txtDamage, txtFireRate, txtCritRate, txtRange, txtMagazine, txtMoveSpeed, txtCritDmg;
    public Image imgHeroSkin;
    public Sprite sprP1Skill, sprP2Skill, sprSkillDf;
    public SkeletonGraphic skleCur;
    public SkeletonGraphic p1Skeleton, p2Skeleton;
    public GameObject gP1, gP2;
    public GameObject gButtonUnlock, gButtonLevelUp;
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
    public TextMeshProUGUI txtPartsValue;
    //public TextMeshProUGUI txtHealth, txtDamage, txtAttSpeed, txtCritDamage, txtCritRate, txtDefRate;
    //public TextMeshProUGUI txtHealthUP, txtDamageUP, txtAttSpeedUP, txtCritDamageUP, txtCritRateUP;
    public ParticleSystem pEvolve, pEvolveP2;
    public Text txtPice;
    public Image imgProgress;
    public TextMeshProUGUI txtTotalPower, txtHealthDis, txtDamageDis;
    public Button[] btnTabs;
    public Sprite sprSelect, sprDeSelect;
    public List<EquipmentItem> lstEquip;
    public Dictionary<string, ItemData> dicAllEquip = new Dictionary<string, ItemData>();
    public HeroDataInfo heroSelected;
    public Image imgSkillImage;
    public Text txtSkillTitle, txtSkillContent;

    public int _indexChoose = 0;

    private PlayerData pData, pNext;
    private double priceUpdate;

    private string keyEquipped;
    ItemData itemEquipped = null;
    private int curWeponStar = 0, nextWeaponStar = 0;
    private int curWeaponIndex = 0;
    private HeroSkill hSelected;

    private void Awake()
    {
        Instance = this;
        pEvolve.Stop();
        pEvolveP2.Stop();
    }

    public void ChangeAnim(int _index)
    {
        imgHeroSkin.sprite = _index == 1 ? sprP1Skill : sprP2Skill;
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
        HeroOnClick(/*0*/DataUtils.HeroIndex());
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


        InitEquippedItem();
        FillHeroData(DataUtils.HeroIndex());
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
                    if (itemData.type.Contains("WEAPON")) {
                        curWeaponIndex = int.Parse(itemData.id.Replace("W", ""));
                    }
                }
            }
        }
    }
    public void FillHeroData(int _hIndex)
    {
        for (int i = 0; i < /*DataUtils.playerInfo.level*/imgAllStars.Length; i++)
        {
            if (i <= heroSelected.level)
                imgAllStars[i].sprite = sprStar;
            else
                imgAllStars[i].sprite = sprStarUnlock;
        }

        txtPlayerName.text = heroSelected.name;

        FillDataPlayer(_hIndex);

        p1Skeleton.Skeleton.SetSkin("G" + curWeaponIndex);
        p1Skeleton.Skeleton.SetSlotsToSetupPose();
        p1Skeleton.LateUpdate();

        p2Skeleton.Skeleton.SetSkin("G" + curWeaponIndex);
        p2Skeleton.Skeleton.SetSlotsToSetupPose();
        p2Skeleton.LateUpdate();

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

        
        //txtHealth.text = pData.hp.ToString();
        //txtHealthUP.text = DataUtils.DisplayRichText(pData.hp, pNext.hp);

        //txtDamage.text = "" + 10 * GetDoublevalue(DataUtils.dicWeapon[keyEquipped].DmgValue[curWeponStar]);
        //txtDamageUP.text = DataUtils.DisplayRichText(GetDoublevalue(DataUtils.dicWeapon[keyEquipped].DmgValue[curWeponStar]) * 10, GetDoublevalue(DataUtils.dicWeapon[keyEquipped].DmgValue[nextWeaponStar]) * 10);

        //txtAttSpeed.text = GetDoublevalue(DataUtils.dicWeapon[keyEquipped].BulletSpeedValue[curWeponStar]).ToString();
        //txtAttSpeedUP.text = DataUtils.DisplayRichText(GetDoublevalue(DataUtils.dicWeapon[keyEquipped].BulletSpeedValue[curWeponStar]), GetDoublevalue(DataUtils.dicWeapon[keyEquipped].BulletSpeedValue[nextWeaponStar]));

        //txtCritDamage.text = GetDoublevalue(DataUtils.dicWeapon[keyEquipped].CritDmgValue[curWeponStar]).ToString();
        //txtCritDamageUP.text = DataUtils.DisplayRichText(GetDoublevalue(DataUtils.dicWeapon[keyEquipped].CritDmgValue[curWeponStar]), GetDoublevalue(DataUtils.dicWeapon[keyEquipped].CritDmgValue[nextWeaponStar]));

        //txtCritRate.text = GetDoublevalue(DataUtils.dicWeapon[keyEquipped].CritRateValue[curWeponStar]).ToString();
        //txtCritRateUP.text = DataUtils.DisplayRichText(GetDoublevalue(DataUtils.dicWeapon[keyEquipped].CritRateValue[curWeponStar]), GetDoublevalue(DataUtils.dicWeapon[keyEquipped].CritRateValue[nextWeaponStar]));

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
        txtPartsValue.text = txtPice.text;

        FillTextInfo();

    }

    private void FillTextInfo() {
        //txtHealth, txtDamage, txtFireRate, txtCritRate, txtRange, txtMagazine, txtMoveSpeed, txtCritDmg;
        txtHealth.text = pData.hp + " <sprite=1><color=green>" + pNext.hp + "</color>";
        txtMoveSpeed.text = pData.MoveSpeed + " <sprite=1><color=green>" + pNext.MoveSpeed + "</color>";
        txtDamage.text = "" + 10 * GetDoublevalue(DataUtils.dicWeapon[keyEquipped].DmgValue[curWeponStar]);
        txtFireRate.text = GetDoublevalue(DataUtils.dicWeapon[keyEquipped].BulletSpeedValue[curWeponStar]).ToString();
        txtCritRate.text = GetDoublevalue(DataUtils.dicWeapon[keyEquipped].CritRateValue[curWeponStar]).ToString();
        txtRange.text = GetDoublevalue(DataUtils.dicWeapon[keyEquipped].AtkRangeValue[curWeponStar]).ToString();
        txtMagazine.text = GetDoublevalue(DataUtils.dicWeapon[keyEquipped].MagazineValue[curWeponStar]).ToString();
        txtCritDmg.text = GetDoublevalue(DataUtils.dicWeapon[keyEquipped].CritDmgValue[curWeponStar]).ToString();
    }


    private double GetDoublevalue(double db)
    {
        return System.Math.Floor(db * 100) / 100;
    }
    public void EvolveHero()
    {
        if (DataUtils.playerInfo.coins >= priceUpdate)
        {
            if (heroSelected.level < DataUtils.MAX_LEVEL_HERO)
            {
                if (DataUtils.dicAllHero[heroSelected.id].pices >= (int)pNext.SoManhYeuCau)
                {
                    DataUtils.playerInfo.level += 1;
                    DataUtils.dicAllHero[heroSelected.id].level += 1;

                    DataUtils.dicAllHero[heroSelected.id].pices -= (int)pNext.SoManhYeuCau;

                    DataUtils.AddCoinAndGame((int)-priceUpdate, 0);
                    pEvolve.Play();
                    pEvolveP2.Play();

                    FillHeroData(_indexChoose);
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
                gParts.SetActive(true);
                gHeroInfo.SetActive(true);
                break;
            case 1:
                gAllEquippedItem.SetActive(true);
                gParts.SetActive(false);
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
        else if (!heroChoose.isUnLock && heroChoose.heroData != null) {
            MainMenuController.Instance.ShowMapNotify("Hero not yet unlock and need 20 parts to unlock.");
            //FillHeroData(heroChoose.heroIndex - 1);
            FillData(heroChoose, false);
            ChangeAnim(_index + 1);
            _indexChoose = _index;
        }
        else if(heroChoose.isUnLock)
        {
            FillData(heroChoose, true);

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
            MainMenuController.Instance.OnChangeCharAvarta(_index);
        }

        gButtonLevelUp.SetActive(heroChoose.isUnLock);
        gButtonUnlock.SetActive(!heroChoose.isUnLock);
    }

    private void FillData(HeroChoose heroChoose, bool showSelected)
    {
        heroChoose.imgSelected.enabled = showSelected;
        heroSelected = DataUtils.dicAllHero[heroChoose.heroID];
        FillHeroData(heroChoose.heroIndex - 1);
        DataUtils.heroInfo = DataUtils.dicAllHero[heroChoose.heroID];
    }

    public void UnlockHero() {
        HeroChoose heroChoose = allHeroes[_indexChoose];
        
        if (DataUtils.dicAllHero[heroChoose.heroID].pices >= DataUtils.PART_UNLOCK_P2) {
            heroSelected = DataUtils.dicAllHero[heroChoose.heroID];

            DataUtils.dicAllHero[heroChoose.heroID].pices -= DataUtils.PART_UNLOCK_P2;
            DataUtils.dicAllHero[heroChoose.heroID].isUnlock = true;
            DataUtils.dicAllHero[heroChoose.heroID].isUnlock = true;
            heroChoose.isUnLock = true;
            heroChoose.imgLock.gameObject.SetActive(false);

            DataUtils.SaveAllHero();


            HeroOnClick(_indexChoose);
        }
        else
        {
            MainMenuController.Instance.ShowMapNotify("You need " + (DataUtils.PART_UNLOCK_P2 - DataUtils.dicAllHero[heroChoose.heroID].pices) + "parts to unlock this hero");
        }
    }
    public void ShowSkillInfo(int skillIndex) {
        switch (skillIndex)
        {
            case 0:
                imgSkillImage.sprite = hSkillDefault.sprSkill;
                txtSkillTitle.text = hSkillDefault.skillTitle;
                txtSkillContent.text = hSkillDefault.skillContent;
                break;
            case 1:
                Debug.LogError("_indexChoose: " + _indexChoose);
                hSelected = _indexChoose == 0 ? hSkillP1 : hSkillP2;


                imgSkillImage.sprite = hSelected.sprSkill;
                txtSkillTitle.text = hSelected.skillTitle;
                txtSkillContent.text = hSelected.skillContent;
                break;
            default:
                break;
        }
        gSkillPopup.SetActive(true);
    }
    public void HideSkillInfo()
    {
        gSkillPopup.SetActive(false);
    }
}