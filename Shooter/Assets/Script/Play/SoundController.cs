using UnityEngine;
using System.Collections;
public enum soundGame
{
    soundshootW1, soundshootW2, soundshootW3, soundshootW4, soundshootW5, soundshootW6, exploGrenade, playerDie, throwGrenade, soundb1chem1, soundb1chem2, soundb1fire, soundb1move, sounde0die, sounde0move, sounde1die, sounde2die, sounde3die, sounde4die, sounde5die, sounde6die,
    sounde6fire, soundmissilewarning, soundev3dropbomb, soundv3die, soundv3bombexplo, soundv1die, soundv1attack, soundv2die, soundv2attack, soundexploenemy, soundwin, soundlose, soundroixuongnuoc, soundreload,
    soundplayerhit, soundjump, sounddoublejump, soundbulletdrop, soundstar1, soundstar2, soundstar3, soundexploboxcantexplo, soundminibossfire, soundbtnclick, soundEatHP, soundEatCoin, sounddapchao, soundCritHit, soundGrenadeKill, soundWham,
    soundmultikillx2, soundmultikillx4, soundmultikillx6, soundmultikillx8, soundmultikillx10, soundmultikillmax, soundletgo, soundvictory1, soundEN0Attack, soundEN0Move, soundEN1Attack, soundEN1Die, soundEN2die, soundEN3die,
    soundDisplayMiniBoss2, soundMiniBoss2Attack1, soundMiniBoss2Attack2, soundenemygrenadeBoss2, soundmachinegunBoss2, soundrocketBoss2, soundChangeGun
}

public class SoundController : MonoBehaviour
{
    public static SoundController instance;
    public AudioClip soundshootW1, soundshootW2, soundshootW3, soundshootW4, soundshootW5, soundshootW6, soundExploGrenade, soundPlayerDie, soundThrowGrenade, soundb1chem1, soundb1chem2, soundb1fire, soundb1move;
    public AudioClip sounde0die, sounde0move, sounde1die, sounde2die, sounde3die, sounde4die, sounde5die, sounde6die, sounde6fire, soundmissilewarning;
    public AudioClip soundev3dropbomb, soundv3die, soundv3bombexplo, soundv1die, soundv1attack, soundv2die, soundv2attack, soundexploenemy, soundplayerhit;
    public AudioClip soundwin, soundlose, soundroixuongnuoc, soundreload, soundjump, sounddoublejump, soundbulletdrop, soundstar1, soundstar2, soundstar3;
    public AudioClip soundexploboxcantexplo, soundminibossfire, soundbtnclick, soundEatHP, soundEatCoin, sounddapchao, soundCritHit, soundGrenadeKill, soundWham;
    public AudioClip soundmultikillx2, soundmultikillx4, soundmultikillx6, soundmultikillx8, soundmultikillx10, soundmultikillmax, soundletgo, soundvictory1;
    public AudioClip soundEN0Attack, soundEN0Move, soundEN1Attack, soundEN1Die, soundEN2die, soundEN3die, soundDisplayMiniBoss2, soundMiniBoss2Attack1, soundMiniBoss2Attack2;
    public AudioClip soundenemygrenadeBoss2, soundmachinegunBoss2, soundrocketBoss2, soundChangeGun;
    public AudioSource au;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            DestroyImmediate(gameObject);
        //  Debug.Log("Init");
    }

    public System.Action activeSoundEnemy;
    public void DisplaySetting()
    {
        au.mute = !DataUtils.IsSoundOn();
        if (MenuController.instance != null)
        {
            MenuController.instance.auBG.mute = !DataUtils.IsMusicOn();
        }
        if (GameController.instance != null)
        {
            GameController.instance.auBG.mute = !DataUtils.IsMusicOn();
        }
        if (PlayerController.instance != null)
        {
            if (PlayerController.instance.au != null)
            {
                PlayerController.instance.au.mute = !DataUtils.IsSoundOn();
            }
        }
        if (activeSoundEnemy != null)
            activeSoundEnemy();
    }
    public void PlaySound(soundGame currentSound)
    {
        if (instance != null)
        {
            switch (currentSound)
            {
                case soundGame.soundshootW1:
                    {
                        au.PlayOneShot(instance.soundshootW1);
                    }
                    break;
                case soundGame.soundshootW2:
                    {
                        au.PlayOneShot(instance.soundshootW2);
                    }
                    break;
                case soundGame.soundshootW3:
                    {
                        au.PlayOneShot(instance.soundshootW3);
                    }
                    break;
                case soundGame.soundshootW4:
                    {
                        au.PlayOneShot(instance.soundshootW4);
                    }
                    break;
                case soundGame.soundshootW5:
                    {
                        au.PlayOneShot(instance.soundshootW5);
                    }
                    break;
                case soundGame.soundshootW6:
                    {
                        au.PlayOneShot(instance.soundshootW6);
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
                case soundGame.soundb1chem1:
                    {
                        au.PlayOneShot(instance.soundb1chem1);
                    }
                    break;
                case soundGame.soundb1chem2:
                    {
                        au.PlayOneShot(instance.soundb1chem2);
                    }
                    break;
                case soundGame.soundb1fire:
                    {
                        au.PlayOneShot(instance.soundb1fire);
                    }
                    break;
                case soundGame.soundb1move:
                    {
                        au.PlayOneShot(instance.soundb1move);
                    }
                    break;
                case soundGame.sounde0die:
                    {
                        au.PlayOneShot(instance.sounde0die);
                    }
                    break;
                case soundGame.sounde0move:
                    {
                        au.PlayOneShot(instance.sounde0move);
                    }
                    break;
                case soundGame.sounde1die:
                    {
                        au.PlayOneShot(instance.sounde1die);
                    }
                    break;
                case soundGame.sounde2die:
                    {
                        au.PlayOneShot(instance.sounde2die);
                    }
                    break;
                case soundGame.sounde3die:
                    {
                        au.PlayOneShot(instance.sounde3die);
                    }
                    break;
                case soundGame.sounde4die:
                    {
                        au.PlayOneShot(instance.sounde4die);
                    }
                    break;
                case soundGame.sounde5die:
                    {
                        au.PlayOneShot(instance.sounde5die);
                    }
                    break;
                case soundGame.sounde6die:
                    {
                        au.PlayOneShot(instance.sounde6die);
                    }
                    break;
                case soundGame.sounde6fire:
                    {
                        au.PlayOneShot(instance.sounde6fire);
                    }
                    break;
                case soundGame.soundmissilewarning:
                    {
                        au.PlayOneShot(instance.soundmissilewarning);
                    }
                    break;

                case soundGame.soundv3die:
                    {
                        au.PlayOneShot(instance.soundv3die);
                    }
                    break;
                case soundGame.soundev3dropbomb:
                    {
                        au.PlayOneShot(instance.soundev3dropbomb);
                    }
                    break;
                case soundGame.soundv3bombexplo:
                    {
                        au.PlayOneShot(instance.soundv3bombexplo);
                    }
                    break;
                case soundGame.soundv1die:
                    {
                        au.PlayOneShot(instance.soundv1die);
                    }
                    break;
                case soundGame.soundv1attack:
                    {
                        au.PlayOneShot(instance.soundv1attack);
                    }
                    break;
                case soundGame.soundv2die:
                    {
                        au.PlayOneShot(instance.soundv2die);
                    }
                    break;
                case soundGame.soundv2attack:
                    {
                        au.PlayOneShot(instance.soundv2attack);
                    }
                    break;
                case soundGame.soundexploenemy:
                    {
                        au.PlayOneShot(instance.soundexploenemy);
                    }
                    break;
                case soundGame.soundwin:
                    {
                        au.PlayOneShot(instance.soundwin);
                    }
                    break;
                case soundGame.soundlose:
                    {
                        au.PlayOneShot(instance.soundlose);
                    }
                    break;
                case soundGame.soundroixuongnuoc:
                    {
                        au.PlayOneShot(instance.soundroixuongnuoc);
                    }
                    break;
                case soundGame.soundreload:
                    {
                        au.PlayOneShot(instance.soundreload);
                    }
                    break;
                case soundGame.soundplayerhit:
                    {
                        au.PlayOneShot(instance.soundplayerhit);
                    }
                    break;
                case soundGame.soundjump:
                    {
                        au.PlayOneShot(instance.soundjump);
                    }
                    break;
                case soundGame.sounddoublejump:
                    {
                        au.PlayOneShot(instance.sounddoublejump);
                    }
                    break;
                case soundGame.soundbulletdrop:
                    {
                        au.PlayOneShot(instance.soundbulletdrop);
                    }
                    break;
                case soundGame.soundstar1:
                    {
                        au.PlayOneShot(instance.soundstar1);
                    }
                    break;
                case soundGame.soundstar2:
                    {
                        au.PlayOneShot(instance.soundstar2);
                    }
                    break;
                case soundGame.soundstar3:
                    {
                        au.PlayOneShot(instance.soundstar3);
                    }
                    break;
                case soundGame.soundexploboxcantexplo:
                    {
                        au.PlayOneShot(instance.soundexploboxcantexplo);
                    }
                    break;
                case soundGame.soundminibossfire:
                    {
                        au.PlayOneShot(instance.soundminibossfire);
                    }
                    break;
                case soundGame.soundbtnclick:
                    au.PlayOneShot(instance.soundbtnclick);
                    break;
                case soundGame.soundEatHP:
                    au.PlayOneShot(instance.soundEatHP);
                    break;
                case soundGame.soundEatCoin:
                    au.PlayOneShot(instance.soundEatCoin);
                    break;
                case soundGame.sounddapchao:
                    au.PlayOneShot(instance.sounddapchao);
                    break;
                case soundGame.soundGrenadeKill:
                    au.PlayOneShot(instance.soundGrenadeKill);
                    break;
                case soundGame.soundWham:
                    au.PlayOneShot(instance.soundWham);
                    break;
                case soundGame.soundCritHit:
                    au.PlayOneShot(instance.soundCritHit);
                    break;
                case soundGame.soundmultikillx2:
                    au.PlayOneShot(instance.soundmultikillx2);
                    break;
                case soundGame.soundmultikillx4:
                    au.PlayOneShot(instance.soundmultikillx4);
                    break;
                case soundGame.soundmultikillx6:
                    au.PlayOneShot(instance.soundmultikillx6);
                    break;
                case soundGame.soundmultikillx8:
                    au.PlayOneShot(instance.soundmultikillx8);
                    break;
                case soundGame.soundmultikillx10:
                    au.PlayOneShot(instance.soundmultikillx10);
                    break;
                case soundGame.soundmultikillmax:
                    au.PlayOneShot(instance.soundmultikillmax);
                    break;
                case soundGame.soundletgo:
                    au.PlayOneShot(instance.soundletgo);
                    break;
                case soundGame.soundvictory1:
                    au.PlayOneShot(instance.soundvictory1);
                    break;
                case soundGame.soundEN0Attack:
                    au.PlayOneShot(instance.soundEN0Attack);
                    break;
                case soundGame.soundEN0Move:
                    au.PlayOneShot(instance.soundEN0Move);
                    break;
                case soundGame.soundEN1Attack:
                    au.PlayOneShot(instance.soundEN1Attack);
                    break;
                case soundGame.soundEN1Die:
                    au.PlayOneShot(instance.soundEN1Die);
                    break;
                case soundGame.soundEN2die:
                    au.PlayOneShot(instance.soundEN2die);
                    break;
                case soundGame.soundEN3die:
                    au.PlayOneShot(instance.soundEN3die);
                    break;
                case soundGame.soundDisplayMiniBoss2:
                    au.PlayOneShot(instance.soundDisplayMiniBoss2);
                    break;
                case soundGame.soundMiniBoss2Attack1:
                    au.PlayOneShot(instance.soundMiniBoss2Attack1);
                    break;
                case soundGame.soundMiniBoss2Attack2:
                    au.PlayOneShot(instance.soundMiniBoss2Attack2);
                    break;
                case soundGame.soundenemygrenadeBoss2:
                    au.PlayOneShot(instance.soundenemygrenadeBoss2);
                    break;
                case soundGame.soundmachinegunBoss2:
                    au.PlayOneShot(instance.soundmachinegunBoss2);
                    break;
                case soundGame.soundrocketBoss2:
                    au.PlayOneShot(instance.soundrocketBoss2);
                    break;
                case soundGame.soundChangeGun:
                    au.PlayOneShot(instance.soundChangeGun);
                    break;
            }
        }
    }
}