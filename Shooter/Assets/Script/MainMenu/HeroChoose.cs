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

    //private Button btn;
    private void OnEnable()
    {
        heroIndex = int.Parse(heroID.Replace("P", ""));

        //btn = GetComponent<Button>();

        if (DataUtils.dicAllHero.ContainsKey(heroID))
        {
            heroData = DataUtils.dicAllHero[heroID];
            isUnLock = true;
        }
        else
        {
            heroData = null;
            isUnLock = false;
        }


        if (heroIndex - 1 == DataUtils.HeroIndex())
        {
            FillData();
        }
        else
            imgSelected.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        imgLock.gameObject.SetActive(!isUnLock);
    }
    private void FillData()
    {
        if (PanelHeroes.Instance != null)
        {
            imgSelected.enabled = true;
            PanelHeroes.Instance.heroSelected = DataUtils.dicAllHero[heroID];
            PanelHeroes.Instance.FillHeroData(heroIndex - 1);
        }
        else
        {
            imgSelected.enabled = true;
        }
    }
}