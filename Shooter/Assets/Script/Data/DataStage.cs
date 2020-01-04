using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STAGE_MODE { NORMAL, HARD }
[SerializeField]
public class DataStage
{
    public string stageName { get; set; }
    public MapLevel[] levels { get; set; }
    public STAGE_MODE stageMode { get; set; }
}

[SerializeField]
public class MapLevel
{
    public string levelID { get; set; }
    public LVMission[] mission { get; set; }
    public LVReward rewards { get; set; }
}
[SerializeField]
public class LVMission
{
    public int id { get; set; }
    public string missionName { get; set; }
    public bool isPass { get; set; }
}
[SerializeField]
public class LVReward
{
    public string rType { get; set; }
    public int rValue { get; set; }
}