using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Unity;
using Firebase.Analytics;

public class MyAnalytics
{
    const string EVENT_LOGIN_BY_DAY = "event_login_by_day";
    const string EVENT_TIME_OPEN = "time_open";
    const string EVENT_LEVEL_PLAY = "level_play";
    const string EVENT_LOSE_LEVEL = "lose_level_checkpoint";

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
        FirebaseAnalytics.LogEvent(EVENT_LEVEL_PLAY, _pamLevelPlay);
    }

    #region Call In GamePlay
    public static void LogEventLoseLevel(int level, int checkPointIndex, int stage)
    {
        Parameter[] _pamLevelLose = {
                new Parameter(FirebaseAnalytics.ParameterLevel, level),
                new Parameter("checkpoint_index", checkPointIndex),
                new Parameter("level_stage", stage)
                };
        FirebaseAnalytics.LogEvent(EVENT_LOSE_LEVEL, _pamLevelLose);
    }
    public static void LogEventLevelComplete(int level, int stage)
    {
        Parameter[] _pamLevelComplete = {
                new Parameter(FirebaseAnalytics.ParameterLevel, level),
                new Parameter("level_stage", stage)
                };
        FirebaseAnalytics.LogEvent("level_complete", _pamLevelComplete);
    }
    public static void LogEventGameOver(int level, int stage)
    {
        Parameter[] _pamLevelEnd = {
                new Parameter(FirebaseAnalytics.ParameterLevel, level),
                new Parameter("level_stage", stage)
                };
        FirebaseAnalytics.LogEvent("level_end", _pamLevelEnd);
    }
    #endregion
}