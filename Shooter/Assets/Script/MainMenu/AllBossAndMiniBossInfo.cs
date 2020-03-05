using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region AllBossAndMiniBoss
[CreateAssetMenu(fileName = "AllInfoBossAndMiniBoss", menuName = "Create Info Boss And MiniBoss")]
public class AllBossAndMiniBossInfo : ScriptableObject
{
    public List<InfoBossAndMiniBoss> infos = new List<InfoBossAndMiniBoss>();
}
[System.Serializable]
public class InfoBossAndMiniBoss
{
    public string[] names;
    public Sprite[] icons;
}
#endregion