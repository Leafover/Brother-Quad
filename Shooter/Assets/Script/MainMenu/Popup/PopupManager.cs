using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public enum POPUP_TYPE { NONE, STARTER_PACK, NOTIFY, SETTING, MAP_NOTI}

    public GameObject gNotify, gStarterPack;
    public PopupStarterPack pStarterPack;
    public PopupNotify pNoti;
    public string mess_;

    //public PopupSetting pSetting;

    public POPUP_TYPE pType;

    private void OnEnable()
    {
        switch (pType)
        {
            case POPUP_TYPE.STARTER_PACK:
                pStarterPack.gameObject.SetActive(true);
                break;
            case POPUP_TYPE.NOTIFY:
                pNoti.ShowNoti("Coming Soon.");
                pNoti.gameObject.SetActive(true);
                break;
            case POPUP_TYPE.MAP_NOTI:
                pNoti.ShowNoti(mess_);
                pNoti.gameObject.SetActive(true);
                break;
            //case POPUP_TYPE.SETTING:
            //    pSetting.gameObject.SetActive(true);
            //    break;
        }
    }

    private void OnDisable()
    {
        pNoti.gameObject.SetActive(false);
        pStarterPack.gameObject.SetActive(false);
    }
}
