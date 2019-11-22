using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    public void EventEndAnim()
    {
        GameController.instance.BeginPanel();
    }
    public void BtnReset()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
