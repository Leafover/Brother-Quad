using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarBoss : MonoBehaviour
{
    public Image healthFill;
    public Text nameBossText;

    public void DisplayHealthFill(float _health,float maxHealth)
    {
        healthFill.fillAmount = _health / maxHealth;
    }
    public void DisplayBegin(string _name)
    {
        nameBossText.text = _name;
        gameObject.SetActive(true);
    }
    public void DisableHealthBar()
    {

        gameObject.SetActive(false);
    }
}
