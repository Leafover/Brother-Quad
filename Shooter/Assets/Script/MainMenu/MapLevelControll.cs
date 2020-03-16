using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Coffee.UIExtensions;

public class MapLevelControll : MonoBehaviour
{
    public MapLevelControll mapLevelControll;
    public int stageIndex;
    public int mapIndex;
    public UIShiny uiShiny;
    public Image[] imgStars;
    private Button btn;
    //[HideInInspector]
    public bool canPlay;

    public Image imgMap;
    //[HideInInspector]
    public Vector3 vDesScale, vCurScale;

    private void Awake()
    {
        vCurScale = new Vector3(1, 1, 1);
        vDesScale = vCurScale * 1.3f;
        btn = GetComponent<Button>();
        imgMap = GetComponent<Image>();
        if (!DataUtils.StageHasInit() && mapIndex == 0)
        {
            imgMap.color = StageManager.Instance.clUnlock;
            imgMap.sprite = StageManager.Instance.imgMapUnlock;
            canPlay = true;
        }
    }
    private void Update()
    {
    }
    private void OnEnable()
    {
        SwitchColor();
        btn.onClick.AddListener(() =>
        {
            MainMenuController.Instance.SoundClickButton();
            OnMapSelected(stageIndex, mapIndex);
        });
        //RefreshMap();
    }

    public void RefreshMap()
    {
        SwitchColor();
        CheckMapStars();
        CheckMapUnlock();

        if (canPlay && imgMap.sprite == StageManager.Instance.imgMapSelected)
        {
            transform.localScale = vDesScale;
            uiShiny.enabled = true;
        }
        else
        {
            transform.localScale = vCurScale;
            uiShiny.enabled = false;
        }
    }
    private void OnMapSelected(int _stage, int _mapIndex)
    {
        StageManager.Instance.levelControll = mapLevelControll;
        if (MapHasUnlock() || imgMap.color == StageManager.Instance.clUnlock || imgMap.color == StageManager.Instance.clSelected)
        {
            StageManager.Instance.SwitchColor(_mapIndex);
            imgMap.color = StageManager.Instance.clSelected;
            imgMap.sprite = StageManager.Instance.imgMapSelected;
        }
        var miss_ = DataController.instance.allMission[_stage].missionData[_mapIndex];
        GetMapInfo(miss_, _stage, _mapIndex);
    }
    private void CheckMapUnlock()
    {
        int _mIndex = 0;
        if (DataUtils.modeSelected == 0)
        {
            if (DataUtils.lstAllStageNormal.Count == 0 && mapIndex == 0)
            {
            }
            else
            {
                if (DataUtils.lstAllStageNormal[MainMenuController.Instance.stageSelected - 1].levelUnlock < 0)
                {
                    _mIndex = 0;
                }
                else if (DataUtils.lstAllStageNormal[MainMenuController.Instance.stageSelected - 1].levelUnlock == StageManager.Instance.gStages[MainMenuController.Instance.stageSelected - 1].transform.childCount - 1)
                {
                    _mIndex = StageManager.Instance.gStages[MainMenuController.Instance.stageSelected - 1].transform.childCount - 1;
                }
                else
                {
                    _mIndex = DataUtils.lstAllStageNormal[MainMenuController.Instance.stageSelected - 1].levelUnlock + 1;
                }
            }
        }
        else if (DataUtils.modeSelected == 1)
        {
            if (DataUtils.lstAllStageHard.Count == 0 && mapIndex == 0)
            {
            }
            else
            {
                if (DataUtils.lstAllStageHard[MainMenuController.Instance.stageSelected - 1].levelUnlock < 0)
                {
                    _mIndex = 0;
                }
                else if (DataUtils.lstAllStageHard[MainMenuController.Instance.stageSelected - 1].levelUnlock == StageManager.Instance.gStages[MainMenuController.Instance.stageSelected - 1].transform.childCount - 1)
                {
                    _mIndex = StageManager.Instance.gStages[MainMenuController.Instance.stageSelected - 1].transform.childCount - 1;
                }
                else
                {
                    _mIndex = DataUtils.lstAllStageHard[MainMenuController.Instance.stageSelected - 1].levelUnlock + 1;
                }
            }
        }
        if (mapIndex == _mIndex)
        {
            OnMapSelected(stageIndex, _mIndex);
            imgMap.color = StageManager.Instance.clSelected;
            imgMap.sprite = StageManager.Instance.imgMapSelected;
            canPlay = true;
        }
    }

    public void SwitchColor()
    {
        if (!DataUtils.StageHasInit() && mapIndex == 0)
        {
            imgMap.color = StageManager.Instance.clUnlock;
            imgMap.sprite = StageManager.Instance.imgMapUnlock;
            transform.localScale = vCurScale;
        }
        else if (MapHasUnlock())
        {
            imgMap.color = StageManager.Instance.clUnlock;
            imgMap.sprite = StageManager.Instance.imgMapUnlock;
        }
        else
        {
            imgMap.color = StageManager.Instance.clNotYetUnlock;
            imgMap.sprite = StageManager.Instance.imgMapNotYetUnlock;
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
    bool _levelHasComplete = false;
    private void GetMapInfo(Mission miss_, int stageSelect, int mapSelect)
    {
        for (int i = 0; i < StageManager.Instance.imgItemReward.Length; i++)
        {
            StageManager.Instance.imgItemReward[i].transform.parent.gameObject.SetActive(false);
        }
        StageManager.Instance.FillMapInfo(miss_, stageSelect, mapSelect);
        if (DataUtils.StageHasInit())
        {
            _levelHasComplete = DataUtils.GetMapByIndex(stageSelect, mapSelect).hasComplete;
            GetRewardItemName(stageSelect + 1, mapSelect + 1);
            int total = DataController.instance.allTileVatPham.Count;
            for (int i = 0; i < lstString.Count; i++)
            {
                if (DataUtils.dicSpriteData.ContainsKey(lstString[i]))
                {
                    StageManager.Instance.imgItemReward[i].transform.parent.GetChild(0).gameObject.SetActive(_levelHasComplete);
                    StageManager.Instance.imgItemReward[i].sprite = DataUtils.dicSpriteData[lstString[i]];
                    StageManager.Instance.imgItemReward[i].transform.parent.gameObject.SetActive(true);
                }
                else
                {
                    StageManager.Instance.imgItemReward[i].transform.parent.gameObject.SetActive(false);
                }
            }
        }
    }
    List<string> lstString = new List<string>();
    private void GetRewardItemName(int stageSelect, int mapSelect)
    {
        lstString.Clear();
        string _sResult = "";
        if (DataUtils.modeSelected == 0)
        {
            for (int i = 0; i < DataController.instance.allTileVatPham.Count; i++)
            {
                foreach (TileVatPhamList vatPhamList in DataController.instance.allTileVatPham[i].tilevatphamList)
                {
                    if (vatPhamList.Stage == stageSelect && vatPhamList.Level == mapSelect)
                    {
                        _sResult = vatPhamList.ID;
                        lstString.Add(_sResult.Replace("M-", ""));
                    }
                }
            }
        }
        else if (DataUtils.modeSelected == 1) {
            for (int i = 0; i < DataController.instance.allTileVatPhamHard.Count; i++)
            {
                foreach (TileVatPhamList vatPhamList in DataController.instance.allTileVatPhamHard[i].tilevatphamList)
                {
                    if (vatPhamList.Stage == stageSelect && vatPhamList.Level == mapSelect)
                    {
                        _sResult = vatPhamList.ID;
                        lstString.Add(_sResult.Replace("M-", ""));
                    }
                }
            }
        }
        //return _sResult;
    }

    string[] _sCutName = new string[1];

    private bool MapHasUnlock()
    {
        return canPlay;
    }
}
