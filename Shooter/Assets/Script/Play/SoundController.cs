using UnityEngine;
using System.Collections;
public enum soundGame
{
    soundshootW1, soundshootW2, soundshootW3, soundshootW4, soundshootW5, soundshootW6, exploGrenade, playerDie, throwGrenade, soundb1chem1, soundb1chem2, soundb1fire, soundb1move, sounde0die, sounde0move, sounde1die, sounde2die, sounde3die, sounde4die, sounde5die, sounde6die,soundexplow6,
    sounde6fire, soundmissilewarning, soundev3dropbomb, soundv3die, soundv3bombexplo, soundv1die, soundv1attack, soundv2die, soundv2attack, soundexploenemy, soundwin, soundlose, soundroixuongnuoc, soundreload,
    soundplayerhit, soundjump, sounddoublejump, soundbulletdrop, soundstar1, soundstar2, soundstar3, soundexploboxcantexplo, soundminibossfire, soundbtnclick, soundEatHP, soundEatCoin, sounddapchao, soundCritHit, soundGrenadeKill, soundWham,
    soundmultikillx2, soundmultikillx4, soundmultikillx6, soundmultikillx8, soundmultikillx10, soundmultikillmax, soundletgo, soundvictory1, soundEN0Attack, soundEN0Move, soundEN1Attack, soundEN1Die, soundEN2die, soundEN3die,
    soundDisplayMiniBoss2, soundMiniBoss2Attack1, soundMiniBoss2Attack2, soundenemygrenadeBoss2, soundmachinegunBoss2, soundrocketBoss2, soundChangeGun,
    soundBoss3Attack1,soundBoss3Attack3,soundBoss3Attack4,soundBoss3Dead,soundBoss3Begin,soundBoss3Def,soundBoss3HitWhenDef, soundw4truyendien,
    sounddaonv2,sounddienv2,soundhitnv2,soundcrithitnv2, soundmultikillx2nv2, soundmultikillx4nv2, soundmultikillx6nv2, soundmultikillx8nv2, soundmultikillx10nv2, soundmultikillmaxnv2, soundletgonv2, soundwinnv2, soundlosenv2,soundreloadnv2
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
    public AudioClip soundBoss3Attack1, soundBoss3Attack3, soundBoss3Attack4, soundBoss3Dead, soundBoss3Begin, soundBoss3Def, soundBoss3HitWhenDef, soundw4truyendien, soundexplow6;
    public AudioClip sounddaonv2, sounddienv2, soundhitnv2, soundcrithitnv2, soundmultikillx2nv2, soundmultikillx4nv2, soundmultikillx6nv2, soundmultikillx8nv2, soundmultikillx10nv2, soundmultikillmaxnv2, soundletgonv2, soundwinnv2, soundlosenv2, soundreloadnv2;
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
                case soundGame.soundBoss3Attack1:
                    au.PlayOneShot(instance.soundBoss3Attack1);
                    break;
                case soundGame.soundBoss3Attack3:
                    au.PlayOneShot(instance.soundBoss3Attack3);
                    break;
                case soundGame.soundBoss3Attack4:
                    au.PlayOneShot(instance.soundBoss3Attack4);
                    break;
                case soundGame.soundBoss3Dead:
                    au.PlayOneShot(instance.soundBoss3Dead);
                    break;
                case soundGame.soundBoss3Begin:
                    au.PlayOneShot(instance.soundBoss3Begin);
                    break;
                case soundGame.soundBoss3Def:
                    au.PlayOneShot(instance.soundBoss3Def);
                    break;
                case soundGame.soundBoss3HitWhenDef:
                    au.PlayOneShot(instance.soundBoss3HitWhenDef);
                    break;
                case soundGame.soundw4truyendien:
                    au.PlayOneShot(instance.soundw4truyendien);
                    break;
                case soundGame.soundexplow6:
                    au.PlayOneShot(instance.soundexplow6);
                    break;
                case soundGame.soundhitnv2:
                    au.PlayOneShot(instance.soundhitnv2);
                    break;
                case soundGame.sounddienv2:
                    au.PlayOneShot(instance.sounddienv2);
                    break;
                case soundGame.sounddaonv2:
                    au.PlayOneShot(instance.sounddaonv2);
                    break;
                case soundGame.soundcrithitnv2:
                    au.PlayOneShot(instance.soundcrithitnv2);
                    break;
                case soundGame.soundmultikillx2nv2:
                    au.PlayOneShot(instance.soundmultikillx2nv2);
                    break;
                case soundGame.soundmultikillx4nv2:
                    au.PlayOneShot(instance.soundmultikillx4nv2);
                    break;
                case soundGame.soundmultikillx6nv2:
                    au.PlayOneShot(instance.soundmultikillx6nv2);
                    break;
                case soundGame.soundmultikillx8nv2:
                    au.PlayOneShot(instance.soundmultikillx8nv2);
                    break;
                case soundGame.soundmultikillx10nv2:
                    au.PlayOneShot(instance.soundmultikillx10nv2);
                    break;
                case soundGame.soundmultikillmaxnv2:
                    au.PlayOneShot(instance.soundmultikillmaxnv2);
                    break;
                case soundGame.soundletgonv2:
                    au.PlayOneShot(instance.soundletgonv2);
                    break;
                case soundGame.soundwinnv2:
                    au.PlayOneShot(instance.soundwinnv2);
                    break;
                case soundGame.soundlosenv2:
                    au.PlayOneShot(instance.soundlosenv2);
                    break;
                case soundGame.soundreloadnv2:
                    au.PlayOneShot(instance.soundreloadnv2);
                    break;
            }
        }
    }
}