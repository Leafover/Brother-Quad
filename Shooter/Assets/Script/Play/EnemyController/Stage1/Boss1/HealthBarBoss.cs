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
        icons.sprite = DataController.instance.allbossandminibossInfo.infos[index1].icons[index2];
        nameBossText.text = DataController.instance.allbossandminibossInfo.infos[index1].names[index2];
        gameObject.SetActive(true);
    }
    public void DisableHealthBar()
    {
        gameObject.SetActive(false);
    }
}
