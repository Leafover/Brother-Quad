using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberDamageTextController : MonoBehaviour
{
    public TextMeshPro tmp;
    public void Display(string text)
    {
        tmp.text = text;
    }
    public void DisableMe()
    {
        gameObject.SetActive(false);
    }
}
