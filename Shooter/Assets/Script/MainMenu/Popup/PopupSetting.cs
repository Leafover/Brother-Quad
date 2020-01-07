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
        Debug.LogError(SceneManager.GetActiveScene().buildIndex);
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            gPanelSetting.SetActive(false);
        }
        else
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
    public void MoreGame()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Ohze+Games+Studio");
    }
    public void RateUs()
    {
        Application.OpenURL("market://details?id=" + Application.identifier);
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
