using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroChoose : MonoBehaviour
{
    public Image imgHeroIcon;
    public Image imgLock;
    public bool isUnLock;

    private Button btn;
    private void OnEnable()
    {
        btn = GetComponent<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {
        imgLock.gameObject.SetActive(!isUnLock);
        btn.onClick.AddListener(() =>
        {
            HeroOnClick();
        });
    }
    
    private void HeroOnClick()
    {
        if (!isUnLock)
        {
            MainMenuController.Instance.ShowMapNotify("Hero not yet unlock");
        }
    }
}