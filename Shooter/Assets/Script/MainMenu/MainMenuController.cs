using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController Instance;
    public ItemSpriteData allSpriteData;
    public GameObject gPanelUIButton, gPanelStage, gPanelPopup, gPanelHeroes;
    public PopupManager popManager;
    public Text txtStageName;
    public Button[] buttonStages;
    public Text txtGems, txtCoins;
    [HideInInspector]
    public int stageSelected = 0;

    private void Awake()
    {
        Instance = this;

    }
    private void Start()
    {

        InitButtonStage();
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
        HideAllPanel();
    }

    public void UpdateCoinAndGem()
    {
        if (DataUtils.PlayerInfoHasInit() && DataUtils.playerInfo!= null)
        {
            txtCoins.text = DataUtils.playerInfo.coins.ToString();
            txtGems.text = DataUtils.playerInfo.gems.ToString();
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
        if (stage > DataUtils.TOTAL_STAGE)
        {
            ShowMapNotify("Stage " + stage + " Coming Soon");
        }
        else
        {
            stageSelected = stage;
            gPanelUIButton.SetActive(false);
            gPanelStage.SetActive(true);
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

        popManager.pType = PopupManager.POPUP_TYPE.NOTIFY;
        gPanelPopup.SetActive(true);
        Debug.LogError("ShowEquipment");
    }
    public void ShowShop()
    {
        SoundClickButton();
        popManager.pType = PopupManager.POPUP_TYPE.NOTIFY;
        gPanelPopup.SetActive(true);
        Debug.LogError("ShowShop");
    }
    public void ShowHeroTab()
    {
        SoundClickButton();
        popManager.pType = PopupManager.POPUP_TYPE.NOTIFY;
        gPanelPopup.SetActive(true);
        Debug.LogError("Show Hero Infomation");
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
    }
    public void AddMoreDiamond()
    {
        SoundClickButton();
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
        if (Input.GetKeyDown(KeyCode.A))
        {
            //DataUtils.SaveEquipment("H1", "Uncommon", 30);
            GameIAPManager.Instance.BuyProduct(DataUtils.P_STARTER_PACK);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //Debug.LogError(DataUtils.GetAllItem());
            GameIAPManager.Instance.BuyProduct(DataUtils.P_DONATE);
        }
    }
    private void HideAllPanel()
    {
        gPanelUIButton.SetActive(true);
        gPanelStage.SetActive(false);
        gPanelPopup.SetActive(false);
        gPanelHeroes.SetActive(false);
    }

    public Sprite GetSpriteByName(string name)
    {
        Sprite _spr = null;
        string[] strSP = name.Split('-');
        
        for(int i = 0; i < allSpriteData.spriteDatas.Count; i++)
        {
            if(allSpriteData.spriteDatas[i].itemName.Equals(strSP[strSP.Length - 1]))
            {
                _spr = allSpriteData.spriteDatas[i].sprItem;
            }
        }
        return _spr;
    }
}