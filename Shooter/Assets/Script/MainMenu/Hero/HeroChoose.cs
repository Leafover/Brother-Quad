using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class HeroChoose : MonoBehaviour
{
    [SerializeField]
    public Image imgHeroIcon;
    [SerializeField]
    public Image imgLock;
    [SerializeField]
    public Image imgSelected;
    [SerializeField]
    public string heroID;
    [SerializeField]
    public bool isUnLock;
    [SerializeField]
    public HeroDataInfo heroData;
    [SerializeField]
    public int heroIndex;

    private void Awake()
    {
        heroIndex = int.Parse(heroID.Replace("P", ""));

        //btn = GetComponent<Button>();
        if (DataUtils.dicAllHero.ContainsKey(heroID))
        {
            heroData = DataUtils.dicAllHero[heroID];
            if (heroData.isUnlock)
                isUnLock = true;
        }
        else
        {
            heroData = null;
            //isUnLock = false;
        }


        if (heroIndex - 1 == DataUtils.HeroIndex())
        {
            FillData();
        }
        else
            imgSelected.enabled = false;
    }
    //private Button btn;
    private void OnEnable()
    {

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