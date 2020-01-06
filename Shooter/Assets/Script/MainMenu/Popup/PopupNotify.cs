using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupNotify : MonoBehaviour
{
    public Text txtContent;

    // Start is called before the first frame update
    void Start()
    {
    }
    public void ShowNoti(string noti_)
    {
        txtContent.text = noti_;
    }
    public void ClosePopup()
    {
        MainMenuController.Instance.popManager.pType = PopupManager.POPUP_TYPE.NONE;
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
