using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Button[] btnTabs;
    public Sprite sprSelect, sprUnSelect;

    public void ChooseTab(int _index) {
        for (int i = 0; i < btnTabs.Length; i++)
        {
            if (i == _index)
            {
                btnTabs[i].image.sprite = sprSelect;
            }
            else btnTabs[i].image.sprite = sprUnSelect;
        }
    }
    public void WatchVideo()
    {
        Debug.LogError("Watch Video to get 200 coins");
        if (Application.platform == RuntimePlatform.Android)
        {
            AdsManager.Instance.ShowRewardedVideo((b) =>
            {
                if (b)
                {
                    DataUtils.AddCoinAndGame(200, 0);
                }

            });
        }
        else
        {
            Debug.LogError("1111");
            DataUtils.AddCoinAndGame(200, 0);
        }
    }

    public void HideShop()
    {
        gameObject.SetActive(false);
    }
}
