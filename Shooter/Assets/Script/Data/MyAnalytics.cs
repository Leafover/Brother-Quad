﻿using System.Collections;
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
        FirebaseAnalytics.LogEvent(EVENT_LOSE_STAGE + (stage + 1) + "_Level_" + (level + 1), _pamLevelLose);
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