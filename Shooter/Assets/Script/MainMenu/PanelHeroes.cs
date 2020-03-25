using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Spine.Unity;
using Spine;

public class PanelHeroes : MonoBehaviour
{
    public static PanelHeroes Instance;
    public SkeletonGraphic skeletonGraphic;
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

    private void OnEnable()
    {
        for (int i = 0; i < lstEquip.Count; i++)
        {
            if (!dicAllEquip.ContainsKey(lstEquip[i].itemData.type))
            {
                dicAllEquip.Add(lstEquip[i].itemData.type, lstEquip[i].itemData);
            }
        }

        //  skins = skeletonGraphic.Skeleton.Data.Skins.Items;


        if (heroSelected == null)
        {
            heroSelected = DataUtils.heroInfo;
        }

        ChooseTab(0);

        FillHeroData();
        InitEquippedItem();



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
    public void FillHeroData()
    {
        for (int i = 0; i < DataUtils.playerInfo.level; i++)
        {
            imgAllStars[i].sprite = sprStar;
        }
        FillDataPlayer();

        // skeletonGraphic.Skeleton.SetSkin(skins[DataUtils.itemWeapon.weponIndex + 2]);
    }
    private void FillDataPlayer()
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
        Debug.LogError("Unity-HERO--HeroLevel: " + heroSelected.pices);
        pData = DataController.instance.playerData[0].playerData[heroSelected.level < DataUtils.MAX_LEVEL_HERO ? heroSelected.level : DataUtils.MAX_LEVEL_HERO - 1];
        //Debug.LogError("Unity-HERO--pData: " + (pData == null ? "NULLLL" : "NOT NULL"));
        pNext = DataController.instance.playerData[0].playerData[heroSelected.level + 1 < DataUtils.MAX_LEVEL_HERO ? heroSelected.level + 1 : DataUtils.MAX_LEVEL_HERO - 1];
        //Debug.LogError("Unity-HERO--pNext: " + (pNext == null ? "NULLLL" : "NOT NULL"));
        //Debug.LogError("curWeponStar: " + curWeponStar + ", nextWeaponStar: " + nextWeaponStar);
        //Debug.LogError("Unity-HERO--CurWeapon: " + DataUtils.dicWeapon[keyEquipped].DmgValue[curWeponStar] + " vs " + curWeponStar + ", nextWeaponStar: " + nextWeaponStar);
        //Debug.LogError("Unity-HERO--Damage: " + DataUtils.dicWeapon[keyEquipped].DmgValue[curWeponStar] + " vs " + curWeponStar + ", nextWeaponStar: " + nextWeaponStar);
        //Debug.LogError("Unity-HERO--NextDamage: " + DataUtils.dicWeapon[keyEquipped].DmgValue[nextWeaponStar] + " vs " + curWeponStar + ", nextWeaponStar: " + nextWeaponStar);

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
            priceUpdate = 165 * /*pData*/pNext.SoManhYeuCau * /*pData*/pNext.Giamua1manh;
        else
        {
            double price__ = 165 * pNext.SoManhYeuCau * pNext.Giamua1manh;
            priceUpdate = (int)(price__ - 165 * pNext.SoManhYeuCau * pNext.Giamua1manh * 0.1f);
        }

        txtPriceUpdate.text = priceUpdate.ToString("#,0");


        txtDamageDis.text = "" + 10 * GetDoublevalue(DataUtils.dicWeapon[keyEquipped].DmgValue[curWeponStar]);
        txtHealthDis.text = pData.hp.ToString();
        txtTotalPower.text = pData.MoveSpeed.ToString();

        txtPice.text = DataUtils.dicAllHero[heroSelected.id.Trim()].pices + "/" + /*pData*/pNext.SoManhYeuCau;
        imgProgress.fillAmount = DataUtils.dicAllHero[heroSelected.id.Trim()].pices * 1.0f / (float)/*pData*/pNext.SoManhYeuCau;
    }

    private double GetDoublevalue(double db)
    {
        return System.Math.Floor(db * 100) / 100;
    }
    public void EvolveHero()
    {
        if (DataUtils.playerInfo.coins >= priceUpdate)
        {
            if (DataUtils.playerInfo.level < DataUtils.MAX_LEVEL_HERO)
            {
                if (DataUtils.dicAllHero[heroSelected.id].pices >= (int)pNext.SoManhYeuCau)
                {
                    DataUtils.playerInfo.level += 1;
                    heroSelected.level += 1;
                    DataUtils.dicAllHero[heroSelected.id].level += 1;

                    DataUtils.dicAllHero[heroSelected.id].pices -= (int)pNext.SoManhYeuCau;

                    DataUtils.AddCoinAndGame((int)-priceUpdate, 0);
                    pEvolve.Play();
                    FillHeroData();
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
}