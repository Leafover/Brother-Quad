using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject gPanelUIButton, gPanelStage;
    public Text txtStageName;
    public Button[] buttonStages;
    private int stageSelected = 0;

    private void Start()
    {
        InitButtonStage();
    }
    private void InitButtonStage()
    {
        for (int i = 0; i < buttonStages.Length; i++) {
            GameObject gSelect = buttonStages[i].gameObject;
            buttonStages[i].onClick.AddListener(() => ActiveStage(gSelect));
        }

    }
    private void ActiveStage(GameObject _g)
    {
        Debug.LogError("___: " + _g.name);
        txtStageName.text = _g.name;
    }
    private void OnEnable()
    {
        gPanelUIButton.SetActive(true);
        gPanelStage.SetActive(false);
    }
    private void OnDisable()
    {
        stageSelected = 0;
    }

    public void ChooseStage(int stage)
    {
        stageSelected = stage;
    }
    public void GoReady() {
        if(stageSelected == 0)
        {
            Debug.LogError("Please Select Stage To Play");
        }
        else
        {
            Debug.LogError("Ready To Play");
            gPanelUIButton.SetActive(false);
            gPanelStage.SetActive(true);
            stageSelected = 0;
        }
    }
    public void BackToMain(GameObject g)
    {
        g.SetActive(false);
        gPanelUIButton.SetActive(true);
    }
    public void ShowEquipment()
    {
        Debug.LogError("ShowEquipment");
    }
    public void AddMoreCoin()
    {
        Debug.LogError("AddMoreCoin");
    }
    public void AddMoreDiamond()
    {
        Debug.LogError("AddMoreDiamond");
    }
    private void Update()
    {
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
}