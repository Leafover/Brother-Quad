using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;
    public Button btnStartMission;
    public Color clUnlock, clNotYetUnlock, clSelected;
    public Sprite imgStar, imgStarNotYetUnlock;
    public Text txtLevel, txtMission1, txtMission2, txtMission3;
    private Mission missSelected;
    private int _stageSelect, _mapSelect;

    public GameObject[] gStages;

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        gStages[MainMenuController.Instance.stageSelected - 1].SetActive(true);
    }
    private void OnDisable()
    {
        for(int i = 0; i < gStages.Length; i++)
        {
            gStages[i].SetActive(false);
        }
    }
    public void StartLevel()
    {
        Debug.LogError("Play Stage: " + _stageSelect + " vs " + _mapSelect);
    }
    public void FillMapInfo(Mission mission, int stageSelect, int mapSelect)
    {
        missSelected = mission;
        _stageSelect = stageSelect;
        _mapSelect = mapSelect;
        txtLevel.text = "Level " + mission.level;
        txtMission1.text = mission.mission1name;
        txtMission2.text = mission.mission2name;
        txtMission3.text = mission.mission3name;
    }
}