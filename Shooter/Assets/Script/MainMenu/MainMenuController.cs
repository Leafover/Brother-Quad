using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController Instance;
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

    public Text txtGems, txtCoins;
    [HideInInspector]
    public int stageSelected = 0;
    private void Awake()
    {
        Instance = this;
        DataUtils.InitSpriteData(allSpriteData);

        
    }
    private void Start()
    {
        InitButtonStage();

        if (!DataUtils.IsClaimReward())
        {
            dailyGift.PrepareData();
        }
    }

    public void ShowCraftItem(ItemData itemSelected, string _title) {
        txtConfirmTitle.text = _title;
        bouderConfirm.sprite = DataUtils.GetSpriteByType(itemSelected);
        iconConfirm.sprite = DataUtils.GetSpriteByName(itemSelected.id, allSpriteData);
        nameRewardText.text = itemSelected.itemName;
        gConfirmPanel.SetActive(true);
        //MenuController.instance.blackMarketpanel.DisplayConfirm(DataUtils.GetSpriteByType(itemSelected), DataUtils.GetSpriteByName(itemSelected.id, allSpriteData), itemSelected.itemName);
    }
    public void HideConfirmPanel()
    {
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
            if (i <= DataUtils.GetStageIndex())
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
    }
    public void ChooseStage(int stage)
    {
        SoundClickButton();

        //if(DataController.instance.isHack)
        //{
        //    stageSelected = stage;
        //    gPanelUIButton.SetActive(false);
        //    gPanelStage.SetActive(true);
        //    StageManager.Instance.ChooseNormalMode();
        //    return;
        //}


        if (stage > DataUtils.TOTAL_STAGE)
        {
            ShowMapNotify("Stage " + stage + " Coming Soon");
        }
        else if (stage - 1 > DataUtils.GetStageIndex())
        {
            ShowMapNotify("Please complete Stage " + (stage - 1) + " first");
        }
        else
        {
            stageSelected = stage;
            gPanelUIButton.SetActive(false);
            gPanelStage.SetActive(true);
            StageManager.Instance.ChooseNormalMode();
        }
    }
    public void GoReady()
    {
    }
    public void BackToMain(GameObject g)
    {
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
            DataUtils.TakeItem("W1", DataUtils.eType.WEAPON, DataUtils.eLevel.Normal, 9, false);
            //DataUtils.TakeHeroPice("P1", 2);
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
    private void HideAllPanel()
    {
        gPanelUIButton.SetActive(true);
        gPanelStage.SetActive(false);
        gPanelPopup.SetActive(false);
        gPanelHeroes.SetActive(false);
        shopManager.gameObject.SetActive(false);
        equipmentManager.gameObject.SetActive(false);
        gConfirmPanel.SetActive(false);
    }




    public void BtnLike()
    {
        Application.OpenURL("https://www.facebook.com/rambo.contra.brothersquad/");
    }
    public void BtnOpenAchievementAndDailyQuest()
    {
        MenuController.instance.achievementAndDailyQuestPanel.DisPlayMe(0);
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
}