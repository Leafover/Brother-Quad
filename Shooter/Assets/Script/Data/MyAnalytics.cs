using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Unity;
using Firebase.Analytics;

public class MyAnalytics 
{
    public static void LogEventLogin()
    {
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLogin);
    }

}