using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroChoose : MonoBehaviour
{
    public Image imgHeroIcon;
    public Image imgLock;
    public Image imgSelected;
    public string heroID;
    public bool isUnLock;
    public HeroDataInfo heroData;
    public int heroIndex;

    private Button btn;
    private void OnEnable()
    {
        imgSelected.enabled = false;
        btn = GetComponent<Button>();
        if (DataUtils.dicAllHero.ContainsKey(heroID))
        {
            heroData = DataUtils.dicAllHero[heroID];
        }
        else
        {
            heroData = null;
        }
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
        Debug.LogError("HeroIndex: " + transform.parent.GetSiblingIndex());
        if (/*!isUnLock*/ heroData == null)
        {
            MainMenuController.Instance.ShowMapNotify("Hero not yet unlock");
        }
        else
        {
            if(PanelHeroes.Instance != null)
            {
                imgSelected.enabled = true;
                PanelHeroes.Instance.heroSelected = DataUtils.dicAllHero[heroID];
                PanelHeroes.Instance.FillHeroData();
            }
        }
    }
}