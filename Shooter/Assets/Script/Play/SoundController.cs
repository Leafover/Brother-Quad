using UnityEngine;
using System.Collections;
public enum soundGame
{
    shootnormal, exploGrenade, playerDie, throwGrenade
}

public class SoundController : MonoBehaviour
{
    public static SoundController instance;
    public AudioClip soundshootnormal, soundExploGrenade, soundPlayerDie, soundThrowGrenade;
    public AudioSource au;
    public AudioSource bg;
    void Awake()
    {
        if(instance == null)
        instance = this;

    }
    private void Start()
    {
        bg.Play();
    }

    public void PlaySound(soundGame currentSound)
    {
        if (instance != null)
        {
            switch (currentSound)
            {
                case soundGame.shootnormal:
                    {
                        au.PlayOneShot(instance.soundshootnormal);
                    }
                    break;
                case soundGame.exploGrenade:
                    {
                        au.PlayOneShot(instance.soundExploGrenade);
                    }
                    break;
                case soundGame.playerDie:
                    {
                        au.PlayOneShot(instance.soundPlayerDie);
                    }
                    break;
                case soundGame.throwGrenade:
                    {
                        au.PlayOneShot(instance.soundThrowGrenade);
                    }
                    break;
            }
        }
    }
}