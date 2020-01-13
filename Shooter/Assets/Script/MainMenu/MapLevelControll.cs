using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLevelControll : MonoBehaviour
{
    public MapLevelControll mapLevelControll;
    public int stageIndex;
    public int mapIndex;
    public Image[] imgStars;
    private Button btn;
    //[HideInInspector]
    public bool canPlay;

    public Image imgMap;
    private void Awake()
    {
        btn = GetComponent<Button>();
        imgMap = GetComponent<Image>();
        if (!DataUtils.StageHasInit() && mapIndex == 0)
        {
            imgMap.color = StageManager.Instance.clUnlock;
            canPlay = true;
        }
    }
    private void OnEnable()
    {
        SwitchColor();
        btn.onClick.AddListener(() => {
            MainMenuController.Instance.SoundClickButton();
            OnMapSelected(stageIndex, mapIndex);
        });
        CheckMapStars();
        CheckMapUnlock();
    }
    private void OnMapSelected(int _stage, int _mapIndex)
    {
        StageManager.Instance.levelControll = mapLevelControll;
        if (MapHasUnlock() || imgMap.color == StageManager.Instance.clUnlock || imgMap.color == StageManager.Instance.clSelected)
        {
            StageManager.Instance.SwitchColor();
            imgMap.color = StageManager.Instance.clSelected;
        }
        var miss_ = DataController.instance.allMission[_stage].missionData[_mapIndex];
        GetMapInfo(miss_, _stage, _mapIndex);
    }
    private void CheckMapUnlock()
    {
        //var miss_ = DataController.instance.allMission[stageIndex].missionData[mapIndex];
        //GetMapInfo(miss_, stageIndex, mapIndex);
        int _mIndex = 0;
        if(DataUtils.lstAllStage.Count == 0 && mapIndex == 0)
        {
        }
        else
        {
            if(DataUtils.lstAllStage[MainMenuController.Instance.stageSelected - 1].levelUnlock < 0)
            {
                _mIndex = 0;
            }
            else
            {
                _mIndex = DataUtils.lstAllStage[MainMenuController.Instance.stageSelected - 1].levelUnlock + 1;
            }
        }
        if(mapIndex == _mIndex)
        {
            OnMapSelected(stageIndex, _mIndex);
            imgMap.color = StageManager.Instance.clSelected;
            canPlay = true;
        }
    }

    public void SwitchColor()
    {
        if (!DataUtils.StageHasInit() && mapIndex == 0)
        {
            imgMap.color = StageManager.Instance.clUnlock;
        }
        else if (MapHasUnlock())
        {
            imgMap.color = StageManager.Instance.clUnlock;
        }
        else
        {
            imgMap.color = StageManager.Instance.clNotYetUnlock;
        }
    }
    public void CheckMapStars()
    {
        if (DataUtils.StageHasInit())
        {
            for (int i = 0; i < DataUtils.GetMapByIndex(stageIndex, mapIndex).mission.Count; i++)
            {
                LVMission vMission = DataUtils.GetMapByIndex(stageIndex, mapIndex).mission[i];
                if (vMission.isPass)
                {
                    imgStars[i].sprite = StageManager.Instance.imgStar;
                }
                else
                {
                    imgStars[i].sprite = StageManager.Instance.imgStarNotYetUnlock;
                    imgStars[i].gameObject.transform.SetAsLastSibling();
                }
            }
        }
    }
    private void GetMapInfo(Mission miss_, int stageSelect, int mapSelect)
    {
        for (int i = 0; i < StageManager.Instance.imgItemReward.Length; i++) {
            StageManager.Instance.imgItemReward[i].transform.parent.gameObject.SetActive(false);
        }
        StageManager.Instance.FillMapInfo(miss_, stageSelect, mapSelect);
        if (DataUtils.StageHasInit())
        {
            for (int i = 0; i < DataUtils.GetMapByIndex(stageIndex, mapIndex).rewards.Count; i++)
            {
                if(MainMenuController.Instance.GetSpriteByName(DataUtils.GetMapByIndex(stageIndex, mapIndex).rewards[i].rType) != null)
                {
                    _sCutName = DataUtils.GetMapByIndex(stageIndex, mapIndex).rewards[i].rType.Split('-');
                    StageManager.Instance.imgItemReward[i].sprite = MainMenuController.Instance.GetSpriteByName(DataUtils.GetMapByIndex(stageIndex, mapIndex).rewards[i].rType);
                    if (_sCutName[_sCutName.Length - 1].Contains("W"))
                    {
                        StageManager.Instance.imgItemReward[i].GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 45);
                    }
                    else
                    {
                        StageManager.Instance.imgItemReward[i].GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 0);
                    }
                    StageManager.Instance.imgItemReward[i].transform.parent.gameObject.SetActive(true);
                }
            }
        }
    }
    string[] _sCutName = new string[1];
    void Start()
    {

    }
    private bool MapHasUnlock()
    {
        return canPlay;
    }
}
