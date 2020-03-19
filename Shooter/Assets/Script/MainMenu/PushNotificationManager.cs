using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Firebase.Extensions;

public class PushNotificationManager : MonoBehaviour
{
    public static PushNotificationManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        InitFCM();
    }
    public void ActivePush()
    {
    }
    private void OnApplicationFocus(bool focus)
    {
    }

    #region Firebase Message
    private string topic = "all-game-user";
    Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
    private void InitFCM()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
            }
        });
    }

    private void InitializeFirebase()
    {
        Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
        Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
        Firebase.Messaging.FirebaseMessaging.SubscribeAsync(topic).ContinueWithOnMainThread(task => {
            Debug.LogError(task +  "  --->SubscribeAsync");
        });
        //Debug.LogError("Firebase Messaging Initialized");

        // This will display the prompt to request permission to receive
        // notifications if the prompt has not already been displayed before. (If
        // the user already responded to the prompt, thier decision is cached by
        // the OS and can be changed in the OS settings).
        Firebase.Messaging.FirebaseMessaging.RequestPermissionAsync().ContinueWithOnMainThread(
          task => {
              Debug.LogError(task+ " --->RequestPermissionAsync");
          }
        );
    }
    public virtual void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        //Debug.LogError("Received a new message");
        var notification = e.Message.Notification;
        if (notification != null)
        {
           // Debug.LogError("title: " + notification.Title);
           // Debug.LogError("body: " + notification.Body);
            var android = notification.Android;
            if (android != null)
            {
                //Debug.LogError("android channel_id: " + android.ChannelId);
            }
        }
        if (e.Message.From.Length > 0)
            //Debug.LogError("from: " + e.Message.From);
        if (e.Message.Link != null)
        {
            //Debug.LogError("link: " + e.Message.Link.ToString());
        }
        if (e.Message.Data.Count > 0)
        {
           // Debug.LogError("data:");
            foreach (System.Collections.Generic.KeyValuePair<string, string> iter in
                     e.Message.Data)
            {
                //Debug.LogError("  " + iter.Key + ": " + iter.Value);
            }
        }
    }

    public virtual void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        //Debug.LogError("Received Registration Token: " + token.Token);
    }

    public void ToggleTokenOnInit()
    {
        bool newValue = !Firebase.Messaging.FirebaseMessaging.TokenRegistrationOnInitEnabled;
        Firebase.Messaging.FirebaseMessaging.TokenRegistrationOnInitEnabled = newValue;
        //Debug.LogError("Set TokenRegistrationOnInitEnabled to " + newValue);
    }
    #endregion
}