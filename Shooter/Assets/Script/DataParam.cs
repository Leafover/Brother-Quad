using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DataParam : MonoBehaviour
{
    public const string ALLACHIEVEMENT = "allachievement";
    public const string OLDDATETIME = "olddatetime";
    public const string ALLDAILYQUEST = "alldailyquest";
    public const string SAVEINDEXQUEST = "saveindexquest";

    public static bool first = true,isVIP = false,doneAllDailyQuest = false;
    public static int indexMap, nextSceneAfterLoad = 1, indexStage, levelBase;
    public static float totalCoin;
    public static string[] hints = {"Increase combat power by upgrading character and weapons",
    "Daily free gifts contain many useful items",
    "Boosters are very useful in battles",
    "In higher difficulty, enemies are more stronger but give more rewards",
    "Bosses get even stronger when they're low on health",
    "Kill as many enemies as possible to unlock achievements and daily quests",
    "Shotguns may have low attack range but can deal massive damage",
    "Upgrading your character also grants you skill points that can be used in the skill tree",};

    public static System.DateTime oldDateTime;

    public static void AddCoin(float _coin)
    {
        totalCoin += _coin;
    }
}
