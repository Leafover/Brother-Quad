using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelHeroes : MonoBehaviour
{
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


    private PlayerData pData;
    private double priceUpdate;
    private WeaponList weaponData, weaponDataNext;
    private void Awake()
    {
        pEvolve.Stop();
    }
    private void OnEnable()
    {
        FillHeroData();
    }
    private void FillHeroData()
    {
        for (int i = 0; i < DataUtils.playerInfo.level; i++)
        {
            imgAllStars[i].sprite = sprStar;
        }
        FillDataPlayer();
    }
    private void FillDataPlayer()
    {
        for (int i = 0; i < DataController.instance.playerData.Count; i++)
        {
            pData = DataController.instance.playerData[i];
            if (pData.ID == DataUtils.playerInfo.id)
            {
                int outLv = 1;
                if (int.TryParse(pData.level, out outLv))
                {
                    if (outLv == DataUtils.playerInfo.level)
                    {
                        txtCurHealth.text = pData.hp.ToString();
                        if (i + 1 < DataController.instance.playerData.Count)
                        {
                            txtHealth.text = pData.hp.ToString();
                            txtHealthUP.text = DataUtils.DisplayRichText(pData.hp, DataController.instance.playerData[i + 1].hp);
                        }
                    }
                }
            }
        }
        weaponData = DataController.instance.allWeapon[0].weaponList[0];
        weaponDataNext = DataController.instance.allWeapon[0].weaponList[1];

        //txtDamage.text = weaponData.Dmg.ToString();
        //txtDamageUP.text = DataUtils.DisplayRichText(weaponData.Dmg, weaponDataNext.Dmg);

        //txtAttSpeed.text = weaponData.BulletSpeed.ToString();
        //txtAttSpeedUP.text = DataUtils.DisplayRichText(weaponData.BulletSpeed, weaponDataNext.BulletSpeed);

        //txtCritDamage.text = weaponData.CritDmg.ToString();
        //txtCritDamageUP.text = DataUtils.DisplayRichText(weaponData.CritDmg, weaponDataNext.CritDmg);

        //txtCritRate.text = weaponData.CritRate.ToString();
        //txtCritRateUP.text = DataUtils.DisplayRichText(weaponData.CritRate, weaponDataNext.CritRate);

        //txtCurDamage.text = weaponData.Dmg.ToString();

        priceUpdate = 165 * pData.SoManhYeuCau * pData.Giamua1manh;
        txtPriceUpdate.text = priceUpdate.ToString();
    }

    public void EvolveHero()
    {
        if(DataUtils.playerInfo.coins >= priceUpdate)
        {
            DataUtils.playerInfo.level += 1;
            DataUtils.AddCoinAndGame((int)-priceUpdate, 0);
            pEvolve.Play();
            FillHeroData();
            DataUtils.SavePlayerData();
        }
        else
        {
            MainMenuController.Instance.ShowMapNotify("Not enough coins to level up.");
        }
    }
}