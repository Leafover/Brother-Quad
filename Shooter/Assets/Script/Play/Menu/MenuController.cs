using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;
    public AudioSource auBG;

    public AchievmentAndDailyQuestPanel achievementAndDailyQuestPanel;


    public GameObject warningEvent, warningDailyQuest, warningAchievment;

    private void Awake()
    {

        //cmd o? popupStarterPack dong` 32
        // PanelHeroes 66->78

        instance = this;
        Debug.unityLogger.logEnabled = false;
    }
    int randomAds;
    private void Start()
    {
        SoundController.instance.DisplaySetting();

        DataUtils.FillPlayerDataInfo();
        if (DataUtils.StageHasInit())
        {
            DataUtils.FillAllStage();
            //   Debug.LogError("zooooooooooo2");
        }

        if (!DataParam.first)
        {
            //   AdsManager.Instance.ShowInterstitial((b) => { });
#if UNITY_EDITOR

#else
        randomAds = Random.Range(0, 100);
        if (randomAds < 38)
        {
            AdsManager.Instance.ShowInterstitial((b) => { });
        }
#endif
        }

        DataParam.first = false;
        DisplayWarning();
    }
    public void DisplayWarning()
    {
        warningEvent.SetActive(CheckWarningDisplay());
        warningAchievment.SetActive(DataController.instance.CheckWarningAchievement());
        warningDailyQuest.SetActive(DataController.instance.CheckWarningDailyQuest());
    }
    bool CheckWarningDisplay()
    {
        if (DataController.instance.CheckWarningDailyQuest() || DataController.instance.CheckWarningAchievement())
        {
            //Debug.Log("true");
            return true;
        }
        else
        {
            //Debug.Log("false");
            return false;
        }
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.O))
    //    {
    //        DataController.instance.DoDailyQuest(1, 1000);
    //        DataController.instance.DoAchievement(0, 1000);
    //    }
    //    //else if(Input.GetKeyDown(KeyCode.H))
    //    //{
    //    //    DataUtils.SaveLevel(0, 0);
    //    //    DataUtils.SaveLevel(0, 1);
    //    //    DataUtils.SaveLevel(0, 2);
    //    //    DataUtils.SaveLevel(0, 3);
    //    //    DataUtils.SaveLevel(0, 4);
    //    //    DataUtils.SaveLevel(0, 5);
    //    //    DataUtils.SaveLevel(0, 6);
    //    //}
    //}
}
