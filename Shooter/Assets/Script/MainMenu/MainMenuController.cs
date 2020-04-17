using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController Instance;
    public Text txtPlayerName;
    public Sprite sprCharMale, sprCharFemale;
    public Image imgChar;
    public ItemSpriteData allSpriteData;
    public Sprite sprNormal, sprUncommon, sprRare, sprEpic, sprLegendary;
    public GameObject gPanelUIButton, gPanelStage, gPanelPopup, gPanelHeroes, gConfirmPanel;
    public Text nameRewardText;
    public Image bouderConfirm, iconConfirm;
    public Text txtConfirmTitle;
    public EquipmentManager equipmentManager;
    public PopupManager popManager;
    public ShopManager shopManager;
    public DailyGift dailyGift;
    public Text txtStageName;
    public Sprite sprUnlock, sprNotYetUnlock;
    public Button[] buttonStages;
    public Image[] imgStages;
    public GameObject gStageEffect, gLevelEffect;
    public PopupItemInfomation pItemInfo;
    public Color ClNormal, clUncommon, clRare, clEpic, clLegendary;

    public Text txtGems, txtCoins;
    [HideInInspector]
    public int heroSelectIndex = 0;
    [HideInInspector]
    public int stageSelected = 0;
    private void Awake()
    {
        Instance = this;
        DataUtils.InitSpriteData(allSpriteData);
    }
    public void OnChangeCharAvarta(int _index) {
        txtPlayerName.text = DataUtils.heroInfo.name;
        imgChar.sprite = _index == 0 ? sprCharMale : sprCharFemale;
    }

    public void InitLevelSelectEffect(Transform _tr)
    {
        gLevelEffect.transform.SetParent(_tr, true);
        gLevelEffect.transform.localScale = new Vector3(25, 25, 25);
        gLevelEffect.transform.localPosition = new Vector3(0, -10, 0);
    }
    private void InitStageEffect(Transform _tr)
    {
        gStageEffect.transform.SetParent(_tr, true);
        gStageEffect.transform.localScale = new Vector3(60, 60, 60);
        gStageEffect.gameObject.transform.localPosition = Vector3.zero;
    }
    private void Start()
    {
        InitButtonStage();

        InitStageEffect(imgStages[DataUtils.GetStageIndex()].transform);

        if (!DataUtils.IsClaimReward())
        {
            dailyGift.PrepareData();
        }

        if (DataParam.showstarterpack)
        {
            ShowStarterPack();
        }


        OnChangeCharAvarta(DataUtils.HeroIndex());
    }

    public void ShowCraftItem(ItemData itemSelected, string _title)
    {
        txtConfirmTitle.text = _title;
        bouderConfirm.sprite = DataUtils.GetSpriteByType(itemSelected);
        iconConfirm.sprite = DataUtils.GetSpriteByName(itemSelected.id, allSpriteData);
        nameRewardText.text = itemSelected.itemName;
        gConfirmPanel.SetActive(true);
        //MenuController.instance.blackMarketpanel.DisplayConfirm(DataUtils.GetSpriteByType(itemSelected), DataUtils.GetSpriteByName(itemSelected.id, allSpriteData), itemSelected.itemName);
    }
    public void HideConfirmPanel()
    {
        MenuController.instance.blackMarketpanel.iconmanh.SetActive(false);
        gConfirmPanel.SetActive(false);
    }

    public void SoundClickButton()
    {
        if (SoundController.instance != null)
        {
            SoundController.instance.PlaySound(soundGame.soundbtnclick);
        }
    }
    public void ShowSetting()
    {
        SoundClickButton();
        PopupSetting.Instance.ShowPanelSetting();
    }
    private void InitButtonStage()
    {
        for (int i = 0; i < buttonStages.Length; i++)
        {
            GameObject gSelect = buttonStages[i].gameObject;
            buttonStages[i].onClick.AddListener(() => ActiveStage(gSelect));
        }
    }
    private void ActiveStage(GameObject _g)
    {
        txtStageName.text = _g.name;
    }
    private void OnEnable()
    {
        UpdateCoinAndGem();
        HideAllPanel();
        UpdateStageImage();
    }

    private void UpdateStageImage()
    {
        for (int i = 0; i < imgStages.Length; i++)
        {
            if (i <= DataUtils.GetStageIndex() && i < 3)
            {
                imgStages[i].sprite = sprUnlock;
            }
            else
                imgStages[i].sprite = sprNotYetUnlock;
        }
    }

    public void UpdateCoinAndGem()
    {
        if (DataUtils.PlayerInfoHasInit() && DataUtils.playerInfo != null)
        {
            txtGems.text = DataUtils.playerInfo.gems.ToString("#,0");
            txtCoins.text = DataUtils.playerInfo.coins.ToString("#,0");
        }
        else
        {
            txtCoins.text = "0";
            txtGems.text = "0";
        }
    }

    private void OnDisable()
    {
        stageSelected = 0;
    }

    public void ShowStarterPack()
    {
        SoundClickButton();
        popManager.pType = PopupManager.POPUP_TYPE.STARTER_PACK;
        gPanelPopup.SetActive(true);

        DataParam.showstarterpack = false;
    }
    public void ChooseStage(int stage)
    {
        SoundClickButton();

        if (DataController.instance.isHack)
        {
            stageSelected = stage;
            gPanelUIButton.SetActive(false);
            gPanelStage.SetActive(true);
            StageManager.Instance.ChooseNormalMode();
            InitStageEffect(imgStages[stage - 1].transform);
            return;
        }


        if (stage > DataUtils.TOTAL_STAGE)
        {
            ShowMapNotify("Stage " + stage + " Coming Soon");
        }
        else if (stage - 1 > DataUtils.GetStageIndex())
        {
            int starReacch = stage == 3 ? DataUtils.STAR_UNLOCK_STAGE3 : DataUtils.STAR_UNLOCK_STAGE2;
            if ((DataUtils.CalculateStageStar(DataUtils.lstAllStageNormal) + DataUtils.CalculateStageStar(DataUtils.lstAllStageHard)) >= starReacch)
            {
                ShowMapNotify("You need complete stage " + (stage - 1));
            }
            else
            {
                //ShowMapNotify("Complete stage " + (stage - 1)+" and get more " + (starReacch - (DataUtils.CalculateStageStar(DataUtils.lstAllStageNormal) + DataUtils.CalculateStageStar(DataUtils.lstAllStageHard))) + " stars to unlock");
                ShowMapNotify("Complete stage " + (stage - 1)+"and reach " + (DataUtils.CalculateStageStar(DataUtils.lstAllStageNormal) + DataUtils.CalculateStageStar(DataUtils.lstAllStageHard)) +"/"+ starReacch +"<sprite=0> to unlock.");
            }
        }
        else
        {
            stageSelected = stage;
            gPanelUIButton.SetActive(false);
            gPanelStage.SetActive(true);
            StageManager.Instance.ChooseNormalMode();
            InitStageEffect(imgStages[stage - 1].transform);
        }
    }
    public void GoReady()
    {
    }
    public void BackToMain(GameObject g)
    {
        DataUtils.SetHeroIndex(heroSelectIndex);
        DataUtils.SaveAllHero();
        OnChangeCharAvarta(DataUtils.HeroIndex());
        g.SetActive(false);
        gPanelUIButton.SetActive(true);
    }
    public void ShowEquipment()
    {
        SoundClickButton();
        equipmentManager.gameObject.SetActive(true);
    }
    public void ShowShop()
    {
        SoundClickButton();
        equipmentManager.gameObject.SetActive(false);
        shopManager.gameObject.SetActive(true);
    }
    public void ShowHeroTab()
    {
        SoundClickButton();
        gPanelUIButton.SetActive(false);
        gPanelHeroes.SetActive(true);
    }

    public void BuyRemoveAds()
    {
        SoundClickButton();
        GameIAPManager.Instance.BuyProduct(DataUtils.P_DONATE);
    }
    public void AddMoreCoin()
    {
        SoundClickButton();
        ShowShop();
    }
    public void AddMoreDiamond()
    {
        SoundClickButton();
        ShowShop();
    }

    public void ShowMapNotify(string mess)
    {
        popManager.mess_ = mess;
        popManager.pType = PopupManager.POPUP_TYPE.MAP_NOTI;
        gPanelPopup.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideAllPanel();
            popManager.pNoti.ClosePopup();
            PopupSetting.Instance.HideSetting();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //DataUtils.TakeItem("W1", DataUtils.eType.WEAPON, DataUtils.eLevel.Normal, 9, false);
            DataUtils.TakeHeroPice("P2", 16);
            //dailyGift.PrepareData();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            DataUtils.TakeItem("A2", DataUtils.eType.ARMOR, DataUtils.eLevel.Normal, (int)UnityEngine.Random.Range(1, 10), false);
            //DataUtils.TakeItem("S1", DataUtils.eType.SHOES, DataUtils.eLevel.Normal, 3, true);
            //DataUtils.TakeItem("S1", DataUtils.eType.SHOES, DataUtils.eLevel.Uncommon, 3, true);
            //DataUtils.TakeItem("S1", DataUtils.eType.SHOES, DataUtils.eLevel.Rare, 3, true);
            //DataUtils.TakeItem("S1", DataUtils.eType.SHOES, DataUtils.eLevel.Epic, 3, true);
            //DataUtils.TakeItem("S1", DataUtils.eType.SHOES, DataUtils.eLevel.Legendary, 3, true);
            //dailyGift.ShowDailyGiftPanel();
        }

    }
    public void HideAllPanel()
    {
        gPanelUIButton.SetActive(true);
        gPanelStage.SetActive(false);
        gPanelPopup.SetActive(false);
        gPanelHeroes.SetActive(false);
        shopManager.gameObject.SetActive(false);
        equipmentManager.gameObject.SetActive(false);
        gConfirmPanel.SetActive(false);
        pItemInfo.HidePopup();
    }




    public void BtnLike()
    {
        Application.OpenURL("https://www.facebook.com/rambo.contra.brothersquad/");
        MyAnalytics.LogClickFanpage();
    }
    public void BtnOpenAchievementAndDailyQuest()
    {
        MenuController.instance.achievementAndDailyQuestPanel.DisPlayMe(0);
        MyAnalytics.LogOpenTabDailyQuest();

        Debug.LogError("wtffffff");
    }
    public void WatchAds()
    {
        AdsManager.Instance.ShowRewardedVideo((b) =>
        {
            if (b)
            {
                DataUtils.AddCoinAndGame(200, 0);
            }
        });
        //ShowMapNotify("Watch Video Ads to get coin and crytal.");
    }

    public Color GetColorByItem(string level)
    {
        Color _cl = ClNormal;
        switch (level)
        {
            case "Normal":
                _cl = ClNormal;
                break;
            case "Uncommon":
                _cl = clUncommon;
                break;
            case "Rare":
                _cl = clRare;
                break;
            case "Epic":
                _cl = clEpic;
                break;
            case "Legendary":
                _cl = clLegendary;
                break;
            default:
                _cl = ClNormal;
                break;
        }
        return _cl;
    }
}