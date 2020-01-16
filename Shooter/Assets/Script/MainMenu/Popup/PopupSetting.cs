﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopupSetting : MonoBehaviour
{
    public static PopupSetting Instance;

    public GameObject gPanelSetting;
    public Toggle tgSound, tgMusic;

    public AudioSource auBG;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
    private void OnEnable()
    {
        CheckSoundAndMusic();
    }

    public void CheckSoundAndMusic()
    {
       // if (tgSound != null)
            tgSound.isOn = DataUtils.IsSoundOn();
        //if (tgMusic != null)
            tgMusic.isOn = DataUtils.IsMusicOn();
    }
    private void Update()
    {
        Time.timeScale = gPanelSetting.activeSelf ? 0 : 1;
    }
    public void Back(GameObject g_)
    {
        g_.SetActive(false);
    }
    public void ShowPanelSetting()
    {
        gPanelSetting.SetActive(true);

    }
    public void HideSetting()
    {
        gPanelSetting.SetActive(false);
    }
    public void ExitBattle()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            gPanelSetting.SetActive(false);
        }
        else
        {
            gPanelSetting.SetActive(false);
            DataParam.nextSceneAfterLoad = 0;
            SceneManager.LoadSceneAsync(1);
        }
    }
    public void MoreGame()
    {
        MyAnalytics.LogMoreGame();
        Application.OpenURL(DataUtils.LINK_MORE_GAME);
    }
    public void RateUs()
    {
        Application.OpenURL(DataUtils.LINK_RATE_US);
    }
    public void ChangeSound()
    {
        if (gPanelSetting.activeSelf)
        {
            DataUtils.ChangeSound(tgSound.isOn);
            if (SoundController.instance != null)
                SoundController.instance.DisplaySetting();
        }
    }
    public void ChangeMusic()
    {
        if (gPanelSetting.activeSelf)
        {
            DataUtils.ChangeMusic(tgMusic.isOn);
            if (SoundController.instance != null)
                SoundController.instance.DisplaySetting();
        }
    }
}