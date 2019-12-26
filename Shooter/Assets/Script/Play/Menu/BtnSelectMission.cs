using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSelectMission : MonoBehaviour
{
    public int myStage;
    ListMission _listMission;
    public void BtnSelectLevel()
    {
        SoundController.instance.PlaySound(soundGame.soundbtnclick);
        DataParam.indexStage = myStage;
        DataParam.indexMap = int.Parse(gameObject.name.Replace("Level ", "")) - 1;

        _listMission = new ListMission();
        _listMission.typeMission = DataController.instance.allMission[DataParam.indexStage].missionData[DataParam.indexMap].typemission2;
        _listMission.valueMission = DataController.instance.allMission[DataParam.indexStage].missionData[DataParam.indexMap].valuemission2;

        MissionController.Instance.listMissions.Add(_listMission);

        _listMission = new ListMission();
        _listMission.typeMission = DataController.instance.allMission[DataParam.indexStage].missionData[DataParam.indexMap].typemission3;
        _listMission.valueMission = DataController.instance.allMission[DataParam.indexStage].missionData[DataParam.indexMap].valuemission3;

        MissionController.Instance.listMissions.Add(_listMission);

        DataParam.nextSceneAfterLoad = 2;
        Application.LoadLevel(1);
    }
}
