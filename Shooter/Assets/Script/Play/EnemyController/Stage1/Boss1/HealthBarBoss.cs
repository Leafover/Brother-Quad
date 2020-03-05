using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarBoss : MonoBehaviour
{
    public Image icons;
    public List<Image> healthFill;
    public Text nameBossText, healthbossText;
    public void DisplayHealthFill(float _health, float maxHealth, int current)
    {
        healthFill[current].fillAmount = _health / maxHealth;
        healthbossText.color = healthFill[current].color;
    }
    public void DisplayBegin(int index1, int index2)
    {
       
        icons.sprite = GameController.instance.uiPanel.allbossandminibossInfo.infos[index1].icons[index2];
        Debug.LogError("=====h1=====" + index1 + ":" + index2);
        nameBossText.text = GameController.instance.uiPanel.allbossandminibossInfo.infos[index1].names[index2];
        Debug.LogError("=====h2=====" + index1 + ":" + index2);

        gameObject.SetActive(true);
    }
    public void DisableHealthBar()
    {
        gameObject.SetActive(false);
    }
}
