using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;
    public Text txtTotalStageStars;
    public Button btnStartMission;
    public Color clUnlock, clNotYetUnlock, clSelected;
    public Sprite imgStar, imgStarNotYetUnlock;
    public Text txtLevel, txtMission1, txtMission2, txtMission3;
    private Mission missSelected;
    private int _stageSelect = -1, _mapSelect = -1;
    public Transform trAllRewards;
    public Image imgHard, imgNormal;
    public Image imgItemDiamond;
    public Image[] imgItemReward;
    public Image[] imgMission;
    public Sprite imgMapUnlock, imgMapNotYetUnlock, imgMapSelected, imgModeSelected, imgModeUnSelect;


    public GameObject[] gStages;
    public int stageSelected;
    public MapLevelControll levelControll;

    //private int modeSelected = 0;

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        FetchStageData();
        gStages[MainMenuController.Instance.stageSelected - 1].SetActive(true);

        //txtTotalStageStars.text = (DataUtils.CalculateStageStar(DataUtils.lstAllStageNormal) + DataUtils.CalculateStageStar(DataUtils.lstAllStageHard)) + "";

        _totalStarNormal = DataUtils.CalculateStarByStage(DataUtils.lstAllStageNormal[MainMenuController.Instance.stageSelected - 1]);
        _totalStarHard = (MainMenuController.Instance.stageSelected - 1 > DataUtils.lstAllStageHard.Count ? DataUtils.CalculateStarByStage(DataUtils.lstAllStageHard[MainMenuController.Instance.stageSelected - 1]) : 0);
        txtTotalStageStars.text = (_totalStarNormal + _totalStarHard) + "/48";
        //txtTotalStageStars.text = (DataUtils.CalculateStarByStage(DataUtils.lstAllStageNormal[MainMenuController.Instance.stageSelected - 1]) + DataUtils.CalculateStarByStage(DataUtils.lstAllStageHard[MainMenuController.Instance.stageSelected - 1])) + "/48";
    }
    int _totalStarNormal = 0;
    int _totalStarHard = 0;
    private void Start()
    {
        
    }
    private void FetchStageData()
    {
        ///Check if StageData not yet init
        if (!DataUtils.StageHasInit())
        {
            #region Stage Data not yet init
            List<DataStage> lstStages = new List<DataStage>();
            for (int i = 0; i < gStages.Length; i++)
            {
                DataStage dataStage = new DataStage();
                dataStage.stageName = gStages[i].name;
                if (i == 0)
                {
                    dataStage.stageHasUnlock = true;
                }
                else
                {
                    dataStage.stageHasUnlock = false;
                }
                for (int j = 0; j < gStages[i].transform.childCount; j++)
                {
                    MapLevelControll levelControll = gStages[i].transform.GetChild(j).GetComponent<MapLevelControll>();
                    MapLevel mapLevel = new MapLevel();
                    mapLevel.levelID = levelControll.stageIndex + "_" + levelControll.mapIndex;
                    if (i == 0 && j == 0)
                    {
                        levelControll.canPlay = true;
                    }

                    #region Add Mission to MapLevel
                    Mission mission_ = DataController.instance.allMission[i].missionData[j];
                    AddMissionToMap(mapLevel, mission_.mission1name);
                    AddMissionToMap(mapLevel, mission_.mission2name);
                    AddMissionToMap(mapLevel, mission_.mission3name);
                    #endregion

                    #region Add Rewards to MapLevel
                    if (mapLevel.rewards == null)
                    {
                        mapLevel.rewards = new List<LVReward>();
                    }
                    AddRewardsToMap(i + 1, j + 1, mapLevel.rewards);
                    #endregion

                    #region Add Map Level to Stage
                    if (dataStage.levels == null)
                    {
                        dataStage.levels = new List<MapLevel>();
                    }

                    if (!dataStage.levels.Contains(mapLevel))
                    {
                        dataStage.levels.Add(mapLevel);
                    }
                    #endregion


                    levelControll.SwitchColor();
                    levelControll.CheckMapStars();

                    levelControll.gameObject.SetActive(true);
                }

                if (!lstStages.Contains(dataStage))
                    lstStages.Add(dataStage);
            }
            string jSave = JsonMapper.ToJson(lstStages);
            DataUtils.SaveStage(jSave);
            DataUtils.FillAllStage();
            #endregion
        }
        else///Check if StageData has init
        {
            for (int j = 0; j < gStages[MainMenuController.Instance.stageSelected - 1].transform.childCount; j++)
            {
                MapLevelControll levelControll = gStages[MainMenuController.Instance.stageSelected - 1].transform.GetChild(j).GetComponent<MapLevelControll>();
                MapLevel mapLevel = DataUtils.GetMapByIndex(levelControll.stageIndex, levelControll.mapIndex);
                if (mapLevel.hasComplete)
                {
                    if (j < gStages[MainMenuController.Instance.stageSelected - 1].transform.childCount - 1)
                    {
                        gStages[MainMenuController.Instance.stageSelected - 1].transform.GetChild(j + 1).GetComponent<MapLevelControll>().canPlay = true;
                        levelControll.canPlay = true;
                    }

                    if (levelControll.mapIndex == 7 && !DataUtils.StageHardHasInit())
                    {
                        // DataUtils.UnlockHardMode();
                        DataUtils.UnlockHardMode(levelControll.stageIndex);
                    }
                }
                else
                {
                    gStages[MainMenuController.Instance.stageSelected - 1].transform.GetChild(0).GetComponent<MapLevelControll>().canPlay = true;
                }
                levelControll.gameObject.SetActive(true);
            }

            //if (StageHardHasInit())
                //DataUtils.FillAllStageHard();
        }
    }

    public void SwitchColor(int _mapIndex)
    {
        for (int j = 0; j < gStages[MainMenuController.Instance.stageSelected - 1].transform.childCount; j++)
        {
            MapLevelControll levelControll = gStages[MainMenuController.Instance.stageSelected - 1].transform.GetChild(j).GetComponent<MapLevelControll>();
            if (levelControll.canPlay)
            {
                if (levelControll.mapIndex != _mapIndex)
                {
                    levelControll.imgMap.sprite = imgMapUnlock;
                    levelControll.transform.localScale = levelControll.vCurScale;
                    levelControll.uiShiny.enabled = false;
                }
                else
                {
                    levelControll.transform.localScale = levelControll.vDesScale;
                    levelControll.uiShiny.enabled = true;
                }
            }
        }
    }

    private void AddRewardsToMap(int stage, int level, List<LVReward> rewards)
    {
        for (int i = 0; i < DataController.instance.allTileVatPham.Count; i++)
        {
            foreach (TileVatPhamList vatPhamList in DataController.instance.allTileVatPham[i].tilevatphamList)
            {
                if (vatPhamList.Stage == stage && vatPhamList.Level == level)
                {
                    LVReward lvReward = new LVReward();
                    lvReward.rType = vatPhamList.ID;
                    lvReward.rValue = 0;
                    if (!rewards.Contains(lvReward))
                    {
                        rewards.Add(lvReward);
                    }
                }
            }
        }
    }
    private void AddMissionToMap(MapLevel mLevel, string missName)
    {
        LVMission lvMiss = new LVMission();
        lvMiss.id = mLevel.levelID;
        lvMiss.isPass = false;
        lvMiss.missionName = missName;
        if (mLevel.mission == null) mLevel.mission = new List<LVMission>();
        mLevel.mission.Add(lvMiss);
    }
    private void OnDisable()
    {
        ResetInfo();
        for (int i = 0; i < gStages.Length; i++)
        {
            gStages[i].SetActive(false);
        }
    }

    ListMission _listMission;
    public void StartLevel()
    {
        SoundController.instance.PlaySound(soundGame.soundbtnclick);


        if (DataController.instance.isHack)
        {
            DataParam.indexStage = _stageSelect;
            DataParam.indexMap = _mapSelect;

            DataUtils.CheckEquipWeapon();

            #region Start Level


            MissionController.Instance.AddMission();

            DataParam.nextSceneAfterLoad = 2;

            DataController.instance.DoAchievement(5, 1);

            UnityEngine.SceneManagement.SceneManager.LoadScene(0);

         //   MyAnalytics.LogEventLevelPlay(DataParam.indexMap, DataParam.indexStage);
            #endregion
            return;
        }


        if (_stageSelect < 0 || _mapSelect < 0)
        {
            MainMenuController.Instance.ShowMapNotify("Please select map to play.");
        }
        ///USING ONLY FOR TEST
        else if (!levelControll.canPlay)
        {
            MainMenuController.Instance.ShowMapNotify("Map " + (_mapSelect + 1) + " not yet unlock.");
        }
        else
        {
            DataParam.indexStage = _stageSelect;
            DataParam.indexMap = _mapSelect;

            DataUtils.CheckEquipWeapon();

            #region Start Level


            MissionController.Instance.AddMission();

            DataParam.nextSceneAfterLoad = 2;

            DataController.instance.DoAchievement(5, 1);

            UnityEngine.SceneManagement.SceneManager.LoadScene(0);

      //      MyAnalytics.LogEventLevelPlay(DataParam.indexMap, DataParam.indexStage);
            #endregion
        }
    }
    #region Fill Map Info
    private void ResetInfo()
    {
        trAllRewards.gameObject.SetActive(false);
        missSelected = null;
        _stageSelect = -1;
        _mapSelect = -1;
        txtLevel.text = "";
        txtMission1.text = "";
        txtMission2.text = "";
        txtMission3.text = "";
        levelControll = null;
        for (int i = 0; i < imgMission.Length; i++)
        {
            imgMission[i].sprite = imgStarNotYetUnlock;
        }
    }
    public void FillMapInfo(Mission mission, int stageSelect, int mapSelect)
    {
        Debug.LogError("------: " + DataUtils.First3Star(DataUtils.modeSelected,stageSelect,mapSelect));
        imgItemDiamond.gameObject.SetActive(!DataUtils.First3Star(DataUtils.modeSelected, stageSelect, mapSelect));
        trAllRewards.gameObject.SetActive(true);
        missSelected = mission;
        _stageSelect = stageSelect;
        _mapSelect = mapSelect;
        txtLevel.text = "Level " + mission.level;
        txtMission1.text = mission.mission1name;
        txtMission2.text = mission.mission2name;
        txtMission3.text = mission.mission3name;
        MapLevel mapLevel = DataUtils.GetMapByIndex(stageSelect, mapSelect);
        for (int i = 0; i < mapLevel.mission.Count; i++)
        {
            if (mapLevel.mission[i].isPass)
            {
                imgMission[i].sprite = imgStar;
            }
            else
            {
                imgMission[i].sprite = imgStarNotYetUnlock;
                imgMission[i].gameObject.transform.parent.SetAsLastSibling();
            }
        }
    }
    #endregion


    public void ChooseNormalMode()
    {
        DataUtils.modeSelected = 0;
        imgHard.sprite = imgModeUnSelect;
        imgNormal.sprite = imgModeSelected;
        RefreshMap();
    }
    public void ChooseHardMode()
    {

        //if (DataController.instance.isHack)
        //{
        //    DataUtils.modeSelected = 1;
        //    imgNormal.sprite = imgModeUnSelect;
        //    imgHard.sprite = imgModeSelected;
        //    RefreshMap();
        //}
        //else
        {
            if (DataUtils.lstAllStageNormal[_stageSelect].levelUnlock >= 7)
            {
                DataUtils.modeSelected = 1;
                imgNormal.sprite = imgModeUnSelect;
                imgHard.sprite = imgModeSelected;
                RefreshMap();
            }
            else
            {
                MainMenuController.Instance.ShowMapNotify("Please complete Normal Mode first.");
            }
        }
    }

    private void RefreshMap()
    {
        for (int j = 0; j < gStages[MainMenuController.Instance.stageSelected - 1].transform.childCount; j++)
        {
            MapLevelControll levelControll = gStages[MainMenuController.Instance.stageSelected - 1].transform.GetChild(j).GetComponent<MapLevelControll>();
            MapLevel mapLevel = DataUtils.GetMapByIndex(levelControll.stageIndex, levelControll.mapIndex);
            if (mapLevel.hasComplete || j == 0)
            {
                if (j < gStages[MainMenuController.Instance.stageSelected - 1].transform.childCount - 1 || j == 0)
                {
                    gStages[MainMenuController.Instance.stageSelected - 1].transform.GetChild(j + 1).GetComponent<MapLevelControll>().canPlay = true;
                    levelControll.canPlay = true;
                }
            }
            else
            {
                levelControll.canPlay = false;
                gStages[MainMenuController.Instance.stageSelected - 1].transform.GetChild(j).GetComponent<MapLevelControll>().canPlay = false;
            }
            levelControll.RefreshMap();
        }
    }
}