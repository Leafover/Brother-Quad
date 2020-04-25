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
    public int random;
    Color color = new Color(1, 0.8352941f, 0.1490196f);
    public void Display(string text, bool crit)
    {
        if (!crit)
        {
            tmp.fontSize = 2;
            tmp.color = Color.red;
        }
        else
        {
            tmp.fontSize = 5;
            tmp.color = color;
        }
        tmp.text = text;

    }
    public void SetAnim()
    {
        random = Random.Range(0, 8);
        //  Debug.LogError("random anim:" + random);
        anim.SetInteger("number", random);
    }
    public void DisableMe()
    {
        gameObject.SetActive(false);

    }
}
