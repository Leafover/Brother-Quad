using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupStarterPack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
    public void PurchaseStaterPack()
    {
        Debug.LogError("Process Active Starter Pack, unlock Kriss Vector(W2 Normal) +7500 Coins");
        DataUtils.RemoveAds();
    }
}
