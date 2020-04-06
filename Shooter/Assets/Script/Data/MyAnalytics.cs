using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Unity;
using Firebase.Analytics;

public class MyAnalytics
{
    const string EVENT_LOGIN_BY_DAY = "event_login_by_day";
    const string EVENT_TIME_OPEN = "time_open";
    const string EVENT_STAGE_PLAY = "stage_play_";
    const string EVENT_LOSE_STAGE = "lose_stage_checkpoint_";
    const string EVENT_INVENTORY = "open_inventory";
    const string EVENT_OPEN_HEROTAB = "open_hero_tab";
    const string EVENT_OPEN_DAILYQUEST = "open_dailyquest_chievement";
    const string EVENT_OPEN_BLACKMARKET = "open_black_market";
    const string EVENT_BUY_SUCCESS_MARKET = "buy_item_blackmarket";
    const string EVENT_CLICK_REFRESH = "click_refresh";
    const string EVENT_OPEN_SHOP = "open_shop";
    const string EVENT_OPEN_LUCKYCHEST = "open_chest_";
    const string EVENT_GET_REWARD = "free_reward";
    const string EVENT_CLICK_FANPAGE = "click_fanpage";
    const string EVENT_STARTERPACK = "buy_starter_pack";
    const string EVENT_PRIME_ACCOUNT = "upgrade_to_prime_account";
    const string EVENT_SHOW_PRIME_ACCOUNT = "not_yet_upgrade_prime_account";


    public static void LogEvolveItem(string itemID, string itemType) {
        FirebaseAnalytics.LogEvent("evolve_" + itemID + "_" + itemType);
    }
    public static void LogNotYetUpgradePrime() {
        FirebaseAnalytics.LogEvent(EVENT_SHOW_PRIME_ACCOUNT);
    }
    public static void LogPrimeAccount() {
        FirebaseAnalytics.LogEvent(EVENT_PRIME_ACCOUNT);
    }
    public static void LogBuyStarterPack() {
        FirebaseAnalytics.LogEvent(EVENT_STARTERPACK);
    }
    public static void LogClickFanpage() {
        FirebaseAnalytics.LogEvent(EVENT_CLICK_FANPAGE);
    }
    public static void LogFreeReward() {
        FirebaseAnalytics.LogEvent(EVENT_GET_REWARD);
    }
    public static void LogOpenLuckyChest(string chestName) {
        FirebaseAnalytics.LogEvent(EVENT_OPEN_LUCKYCHEST + chestName);
    }
    public static void LogOpenShop() {
        FirebaseAnalytics.LogEvent(EVENT_OPEN_SHOP);
    }
    public static void LogClickRefresh() {
        FirebaseAnalytics.LogEvent(EVENT_CLICK_REFRESH);
    }
    public static void LogBuyInBlackMarket() {
        FirebaseAnalytics.LogEvent(EVENT_BUY_SUCCESS_MARKET);
    }
    public static void LogOpenBlackMarket() {
        FirebaseAnalytics.LogEvent(EVENT_OPEN_BLACKMARKET);
    }
    public static void LogOpenInventory() {
        FirebaseAnalytics.LogEvent(EVENT_INVENTORY);
    }
    public static void LogEventTakeItem(string itemID, string itemType, bool isCraft) {
        string _eventName = isCraft ? "craft_" : "unlock" + itemID + "_" + itemType + "success";
    }
    public static void LogOpenHeroTab() {
        FirebaseAnalytics.LogEvent(EVENT_OPEN_HEROTAB);
    }
    public static void LogOpenTabDailyQuest() {
        FirebaseAnalytics.LogEvent(EVENT_OPEN_DAILYQUEST);
    }
    public static void LogEventBuyInapp(string packName) {
        FirebaseAnalytics.LogEvent("buy_pack_"+packName);
    }






    public static void LogEventLogin()
    {
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLogin);
    }
    public static void LogEventOpenByDay()
    {
        FirebaseAnalytics.LogEvent(EVENT_LOGIN_BY_DAY);
    }
    public static void LogEventTimeOpen()
    {
        FirebaseAnalytics.LogEvent(EVENT_TIME_OPEN, "time_open_game", System.DateTime.Now.Hour + "h");
    }
    public static void LogEventLevelPlay(int level, int stage)
    {
        Parameter[] _pamLevelPlay = {
                new Parameter(FirebaseAnalytics.ParameterLevel, level),
                new Parameter("level_stage", stage)
                };
        FirebaseAnalytics.LogEvent(EVENT_STAGE_PLAY + (stage + 1) + "_Level_" + (level + 1), _pamLevelPlay);
    }
    public static void LogMoreGame()
    {
        FirebaseAnalytics.LogEvent("more_game_click");
    }
    #region Call In GamePlay
    public static void LogEventLoseLevel(int level, int checkPointIndex, int stage)//5
    {
        Parameter[] _pamLevelLose = {
                new Parameter(FirebaseAnalytics.ParameterLevel, level),
                new Parameter("checkpoint_index", checkPointIndex),
                new Parameter("level_stage", stage)
                };
        FirebaseAnalytics.LogEvent(EVENT_LOSE_STAGE + (stage + 1) + "_Level_" + (level + 1) + "_Check Point_" + (checkPointIndex + 1), _pamLevelLose);
        LogEventGameOver(level, stage);
    }
    public static void LogEventLevelComplete(int level, int stage)//6
    {
        Parameter[] _pamLevelComplete = {
                new Parameter(FirebaseAnalytics.ParameterLevel, level),
                new Parameter("level_stage", stage)
                };
        FirebaseAnalytics.LogEvent("stage_complete_" + (stage + 1) + "_Level_" + (level + 1), _pamLevelComplete);
    }
    public static void LogEventGameOver(int level, int stage)//3
    {
        Parameter[] _pamLevelEnd = {
                new Parameter(FirebaseAnalytics.ParameterLevel, level),
                new Parameter("level_stage", stage)
                };
        FirebaseAnalytics.LogEvent("stage_end_" + (stage + 1) + "_Level_" + (level + 1), _pamLevelEnd);
    }
    #endregion
}