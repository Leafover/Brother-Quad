using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PrimeAccount : MonoBehaviour
{
    string[] des = { "- Receive 10 Gem rewards daily from the time you log in the game", "- Receive 1000 coins daily from the time you log in the game", "- Adding 20% of the total coin earned in all mission", "- Add 2 special daily missions with more gifts", "- 10% discount when buying and upgrading characters, weapons (gem & coin)", "- Remove Ads" };
    public int index;

    public Text desText/*, nameText*/, timeText;
    public GameObject selectObj, btnClaimGem, btnClaimCoin, btnBuy;

    public List<GameObject> bouderReward;

    private void Start()
    {
        DisplayButtom();
        Click(0);
        if (!DataController.primeAccout.isVIP)
        {
            MyAnalytics.LogNotYetUpgradePrime();
        }
    }
    void DisplayButtom()
    {
        if (DataController.primeAccout.isVIP)
        {
            if (DataController.primeAccout.takecoin)
            {
                btnClaimCoin.SetActive(false);
            }
            else
            {
                btnClaimCoin.SetActive(true);
            }
            if (DataController.primeAccout.takegem)
            {
                btnClaimGem.SetActive(false);
            }
            else
            {
                btnClaimGem.SetActive(true);
            }
            btnBuy.SetActive(false);
            timeText.text = "TIME EXPIRED TIME: " + DataController.primeAccout.countDay;
            timeText.gameObject.SetActive(true);
            //nameText.gameObject.SetActive(false);
        }
        else
        {
            timeText.gameObject.SetActive(false);
            //nameText.gameObject.SetActive(true);
            btnBuy.SetActive(true);
            btnClaimCoin.SetActive(false);
            btnClaimGem.SetActive(false);
        }
    }

    public void Click(int _index)
    {
        index = _index;
        desText.text = des[index];
        selectObj.transform.position = bouderReward[index].transform.position; /*EventSystem.current.currentSelectedGameObject.transform.position*/;
    }
    public void Claim(bool gem)
    {
        if (gem)
        {
            DataUtils.AddCoinAndGame(0, 30);
            btnClaimGem.SetActive(false);
            DataController.primeAccout.takegem = true;
        }
        else
        {
            DataUtils.AddCoinAndGame(1000, 0);
            btnClaimCoin.SetActive(false);
            DataController.primeAccout.takecoin = true;
        }
        if (DataController.primeAccout.takecoin && DataController.primeAccout.takegem)
        {
            DataController.primeAccout.countDay--;
            timeText.text = "TIME EXPIRED TIME: " + DataController.primeAccout.countDay;
            DataParam.timeBeginBuyPrimeAccount = System.DateTime.Now;
            if (DataController.primeAccout.countDay <= 0)
            {
                ResetPrimeAccount();
                DisplayButtom();
            }
        }
    }
    void ResetPrimeAccount()
    {
        DataController.primeAccout.countDay = 30;
        DataController.primeAccout.takecoin = false;
        DataController.primeAccout.takegem = false;
        DataController.primeAccout.isVIP = false;
    }
    public void Buy()
    {
        if (DataUtils.playerInfo.gems >= 500)
        {
            DataUtils.AddCoinAndGame(0, -500);
            DataController.primeAccout.isVIP = true;
            DataParam.timeBeginBuyPrimeAccount = System.DateTime.Now;
            DataController.instance.AddNewQuest();
            DisplayButtom();
            MyAnalytics.LogPrimeAccount();
        }
        else
        {
            MenuController.instance.BtnDisplayPrimeAccount(false);
            MainMenuController.Instance.shopManager.gameObject.SetActive(true);
        }
    }
}
