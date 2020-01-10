using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListMission
{
    public int typeMission, valueMission, currentValue;
    public bool isDone;
}
public class MissionController : MonoBehaviour
{
    public static MissionController Instance;
    public List<ListMission> listMissions = new List<ListMission>();
    ListMission _listMission;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public void AddMission()
    {
        listMissions.Clear();

        _listMission = new ListMission();
        _listMission.typeMission = DataController.instance.allMission[DataParam.indexStage].missionData[DataParam.indexMap].typemission2;
        _listMission.valueMission = DataController.instance.allMission[DataParam.indexStage].missionData[DataParam.indexMap].valuemission2;

        listMissions.Add(_listMission);

        _listMission = new ListMission();
        _listMission.typeMission = DataController.instance.allMission[DataParam.indexStage].missionData[DataParam.indexMap].typemission3;
        _listMission.valueMission = DataController.instance.allMission[DataParam.indexStage].missionData[DataParam.indexMap].valuemission3;

        listMissions.Add(_listMission);
    }

    public void DoMission(int checkmission, int temp)
    {
        if (listMissions[0].typeMission == checkmission)
        {
            if (!listMissions[0].isDone)
            {
                listMissions[0].currentValue += temp;

                if (listMissions[0].typeMission == 0)
                {
                    if (listMissions[0].currentValue <= listMissions[0].valueMission)
                    {
                        listMissions[0].isDone = true;
                    }
                }
                else if (listMissions[0].typeMission == 6)
                {
                    if (listMissions[0].currentValue < listMissions[0].valueMission)
                    {
                        listMissions[0].isDone = true;
                    }
                }
                else
                {
                    if (listMissions[0].currentValue >= listMissions[0].valueMission)
                    {
                        listMissions[0].isDone = true;
                    }
                }
            }
        }
        if (listMissions[1].typeMission == checkmission)
        {
            if (!listMissions[1].isDone)
            {
                listMissions[1].currentValue += temp;

                if (listMissions[1].typeMission == 0)
                {
                    if (listMissions[1].currentValue <= listMissions[1].valueMission)
                    {
                        listMissions[1].isDone = true;
                    }
                }
                else if (listMissions[1].typeMission == 6)
                {
                    if (listMissions[1].currentValue < listMissions[1].valueMission)
                    {
                        listMissions[1].isDone = true;
                    }
                }
                else
                {
                    if (listMissions[1].currentValue >= listMissions[1].valueMission)
                    {
                        listMissions[1].isDone = true;
                    }
                }

            }
        }
    }
    public void CheckMission()
    {
        DataUtils.SaveStars(DataParam.indexStage, DataParam.indexMap, listMissions[0].isDone, listMissions[1].isDone);
    }
}
