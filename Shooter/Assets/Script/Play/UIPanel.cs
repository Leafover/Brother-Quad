using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    public GameObject finishPanel;
    public void BtnReset()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    public void BtnNext()
    {
        if (GameController.indexMap < GameController.instance.listMap.Count - 1)
            GameController.indexMap++;
        Application.LoadLevel(Application.loadedLevel);
    }
    public void DisplayFinish()
    {
        finishPanel.SetActive(true);
    }
}
