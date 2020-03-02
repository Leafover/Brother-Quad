using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController Instance;
    public ItemSpriteData allSpriteData;
    public Sprite sprNormal, sprUncommon, sprRare, sprEpic, sprLegendary;
    public GameObject gPanelUIButton, gPanelStage, gPanelPopup, gPanelHeroes;
    public EquipmentManager equipmentManager;
    public PopupManager popManager;
    public ShopManager shopManager;
    public DailyGift dailyGift;
    public Text txtStageName;
    public Button[] buttonStages;
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
        dailyGift.PrepareData();
        Debug.LogError("-----> " + DataUtils.IsClaimReward() + " vs " + DataUtils.SReward());
        if (!DataUtils.IsClaimReward())
        {
            dailyGift.ShowDailyGiftPanel();
        }
        //equipmentManager.InitAllItems();
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
        //SoundClickButton();
        //if (stage > DataUtils.TOTAL_STAGE)
        //{
        //    ShowMapNotify("Stage " + stage + " Coming Soon");
        //}
        //else if (stage - 1 > DataUtils.GetStageIndex())
        //{
        //    ShowMapNotify("Please complete Stage " + (stage - 1) + " first");
        //}
        //else
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
        //popManager.pType = PopupManager.POPUP_TYPE.NOTIFY;
        //gPanelPopup.SetActive(true);
        //Debug.LogError("ShowShop");
        shopManager.gameObject.SetActive(true);
    }
    public void ShowHeroTab()
    {
        SoundClickButton();
        //popManager.pType = PopupManager.POPUP_TYPE.NOTIFY;
        //gPanelPopup.SetActive(true);
        //Debug.LogError("Show Hero Infomation");
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
        Debug.LogError("AddMoreCoin");
        ShowShop();
    }
    public void AddMoreDiamond()
    {
        SoundClickButton();
        ShowShop();
        Debug.LogError("AddMoreDiamond");
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
            //DataUtils.TakeItem("G1", DataUtils.eType.GLOVES, DataUtils.eLevel.Normal, (int)UnityEngine.Random.Range(1, 10), false);
            //DataUtils.TakeHeroPice("P1", 2);
            dailyGift.PrepareData();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            //DataUtils.TakeItem("S1", DataUtils.eType.SHOES, DataUtils.eLevel.Normal, 3, true);
            //DataUtils.TakeItem("S1", DataUtils.eType.SHOES, DataUtils.eLevel.Uncommon, 3, true);
            //DataUtils.TakeItem("S1", DataUtils.eType.SHOES, DataUtils.eLevel.Rare, 3, true);
            //DataUtils.TakeItem("S1", DataUtils.eType.SHOES, DataUtils.eLevel.Epic, 3, true);
            //DataUtils.TakeItem("S1", DataUtils.eType.SHOES, DataUtils.eLevel.Legendary, 3, true);
            dailyGift.ShowDailyGiftPanel();
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