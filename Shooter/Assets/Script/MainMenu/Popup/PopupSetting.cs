using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopupSetting : MonoBehaviour
{
    public static PopupSetting Instance;

    public GameObject gPanelSetting;
    public GameObject gExitBatle;
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

    private void OnDisable()
    {

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

        gExitBatle.SetActive(Application.loadedLevelName.Equals("menu") ? false : true);
    }
    public void Back(GameObject g_)
    {
        //DataUtils.SaveLevel(0, 0);
        //DataUtils.SaveLevel(0, 1);
        //DataUtils.SaveLevel(0, 2);
        //DataUtils.SaveLevel(0, 3);
        //DataUtils.SaveLevel(0, 4);
        //DataUtils.SaveLevel(0, 5);
        //DataUtils.SaveLevel(0, 6);
        if (AdsManager.Instance != null)
            AdsManager.Instance.HideBanner();
        g_.SetActive(false);
    }
    public void ShowPanelSetting()
    {
        gPanelSetting.SetActive(true);
        if (AdsManager.Instance != null)
            AdsManager.Instance.ShowBanner();

    }
    public void HideSetting()
    {
        if (AdsManager.Instance != null)
            AdsManager.Instance.HideBanner();
        gPanelSetting.SetActive(false);
    }
    public void ExitBattle()
    {
        if (AdsManager.Instance != null)
            AdsManager.Instance.HideBanner();
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            gPanelSetting.SetActive(false);
        }
        else
        {
            gPanelSetting.SetActive(false);
            DataParam.nextSceneAfterLoad = 1;
            SceneManager.LoadSceneAsync(0);
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
