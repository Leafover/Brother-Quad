using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupSetting : MonoBehaviour
{
    public static PopupSetting Instance;

    public GameObject gPanelSetting;
    public Toggle tgSound, tgMusic;


    private void Awake()
    {
        if(Instance == null)
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
    public void ChangeSound()
    {
        DataUtils.ChangeSound();
    }
    public void ChangeMusic()
    {
        DataUtils.ChangeMusic();
    }
}
