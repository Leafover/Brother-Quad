using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopupSetting : MonoBehaviour
{
    public static PopupSetting Instance;

    public GameObject gPanelSetting;
    public Toggle tgSound, tgMusic;


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
        tgSound.isOn = DataUtils.IsSoundOn();
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
        Application.OpenURL(DataUtils.LINK_MORE_GAME);
    }
    public void RateUs()
    {
        Application.OpenURL(DataUtils.LINK_RATE_US);
    }
    public void ChangeSound()
    {
        DataUtils.ChangeSound(tgSound.isOn);
    }
    public void ChangeMusic()
    {
        DataUtils.ChangeMusic(tgMusic.isOn);
    }
}
