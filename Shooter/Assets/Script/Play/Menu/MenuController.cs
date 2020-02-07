using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;
    public AudioSource auBG;

    public AchievmentAndDailyQuestPanel achievementAndDailyQuestPanel;

    private void Awake()
    {
        instance = this;
       // Debug.unityLogger.logEnabled = false;
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
        DataParam.first = false;
    }
    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.O))
        //{
        //    achievementAndDailyQuestPanel.DisPlayMe(0);
        //}
        //else if(Input.GetKeyDown(KeyCode.H))
        //{
        //    DataUtils.SaveLevel(0, 0);
        //    DataUtils.SaveLevel(0, 1);
        //    DataUtils.SaveLevel(0, 2);
        //    DataUtils.SaveLevel(0, 3);
        //    DataUtils.SaveLevel(0, 4);
        //    DataUtils.SaveLevel(0, 5);
        //    DataUtils.SaveLevel(0, 6);
        //}
    }
}
