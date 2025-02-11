﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DataParam : MonoBehaviour
{
    public static string APP_ID = isTest ? "ca-app-pub-3940256099942544~3347511713" : "ca-app-pub-8566745611252640~5288886245";
    public static string INTERS_ID = isTest ? "ca-app-pub-3940256099942544/1033173712" : "ca-app-pub-8566745611252640/6358070116";
    public static string REWARDED_ID = isTest ? "ca-app-pub-3940256099942544/5224354917" : "ca-app-pub-8566745611252640/6166498422";
    public static string BANNER_ID = isTest ? "ca-app-pub-3940256099942544/6300978111" : "ca-app-pub-8566745611252640/6715057094";
    public const bool isTest = false;
    public const string ALLACHIEVEMENT = "allachievement";
    public const string OLDDATETIME = "olddatetime";
    public const string ALLDAILYQUEST = "alldailyquest";
    public const string SAVEINDEXQUEST = "saveindexquest";
    public const string PRIMEACCOUNTINFO = "primeaccountinfo";
    public const string TIMEBEGINBUYPRIMEACCOUNT = "timebeginbuyprimeaccount";
    public const string BLACKMARKET = "blackmarket";
    public const string COUNTRESETBLACKMARKET = "countresetblackmarket";
    public const string INDEXREWARDVIDEO = "indexrewardvideo";
    public const string COUNTDONEDAILYQUEST = "countdonedailyquest";
    public static bool first = true, doneAllDailyQuest = false;
    public static int indexMap, nextSceneAfterLoad = 1, indexStage, levelBase, countResetBlackMarket = 0, indexRewardVideo = 0, countdonedailyquest;
    public static float totalCoin;
    public static string[] hints = {"Increase combat power by upgrading character and weapons",
    "Daily free gifts contain many useful items",
    "Boosters are very useful in battles",
    "In higher difficulty, enemies are more stronger but give more rewards",
    "Bosses get even stronger when they're low on health",
    "Kill as many enemies as possible to unlock achievements and daily quests",
    "Shotguns may have low attack range but can deal massive damage",
    "Upgrading your character also grants you skill points that can be used in the skill tree",};
    public static System.DateTime oldDateTime, timeBeginBuyPrimeAccount;
    public static void AddCoin(float _coin)
    {
        totalCoin += _coin;
    }
    public static bool showstarterpack;

    public static bool firsttimegiftdaily, cantakegiftdaily;
    public const string GIFTDAILY = "giftdaily", CURRENTGIFTDAILY = "currentgiftdaily", FIRSTTIMEGIFTDAILY = "firsttimegiftdaily", CANTAKEGIFTDAILY = "cantakegiftdaily", OLDTIMEGIFTDAILY= "oldtimegiftdaily";
    public static int currentGiftDaily = 0;
    public static System.DateTime oldTimeGiftDaily;
}
