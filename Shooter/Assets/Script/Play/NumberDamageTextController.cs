using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberDamageTextController : MonoBehaviour
{
    public TextMeshPro tmp;
    public Animator anim;
    private void OnValidate()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
    }

    public void Display(string text)
    {
        tmp.text = text;

    }

    public void DisableMe()
    {
        gameObject.SetActive(false);

    }
}
