using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLevelControll : MonoBehaviour
{
    public int stageIndex;
    public int mapIndex;
    public Image[] imgStars;
    private Button btn;
    private string KEY = "key_";

    private Image imgMap;
    private void Awake()
    {
        btn = GetComponent<Button>();
    }
    private void OnEnable()
    {
        imgMap = GetComponent<Image>();
        KEY = "stage_" + stageIndex + "_map_" + mapIndex;
        if (MapHasUnlock())
        {
            imgMap.color = StageManager.Instance.clUnlock;
        }
        else
        {
            imgMap.color = StageManager.Instance.clNotYetUnlock;
        }


        btn.onClick.AddListener(() => {

            if (MapHasUnlock())
            {
                imgMap.color = StageManager.Instance.clSelected;
            }
            else
            {
                MainMenuController.Instance.ShowMapNotify("Map " + mapIndex + " not yet unlock.");
            }
            var miss_ = DataController.instance.allMission[stageIndex].missionData[mapIndex];
            GetMapInfo(miss_, stageIndex, mapIndex);
        });
    }
    
    private void GetMapInfo(Mission miss_, int stageSelect, int mapSelect)
    {
        
        StageManager.Instance.FillMapInfo(miss_, stageSelect, mapSelect);
    }
    void Start()
    {
        
    }
    private bool MapHasUnlock()
    {
        return mapIndex == 0 ? true : false;
    }
}
