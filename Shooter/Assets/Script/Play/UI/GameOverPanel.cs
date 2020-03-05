using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{

    public void EventDisplayStar(int i)
    {
        switch(i)
        {
            case 0:
                SoundController.instance.PlaySound(soundGame.soundstar1);
                break;
            case 1:
                SoundController.instance.PlaySound(soundGame.soundstar2);
                break;
            case 2:
                SoundController.instance.PlaySound(soundGame.soundstar3);
                break;
        }
    }
    public void ShowAds()
    {
#if UNITY_EDITOR

#else
        randomAds = Random.Range(0, 100);
        if (randomAds < 40)
        {
            AdsManager.Instance.ShowInterstitial((b) => { });
        }
#endif
    }
    //int randonvictorysound;
    //public void WinSound()
    //{
    //    randonvictorysound = Random.Range(0, 2);
    //    if (randonvictorysound == 1)
    //        SoundController.instance.PlaySound(soundGame.soundwin);
    //    else
    //        SoundController.instance.PlaySound(soundGame.soundvictory1);
    //}
}
