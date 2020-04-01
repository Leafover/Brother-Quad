using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBtn : MonoBehaviour
{
    public GameObject effect;
    public void EventClick()
    {
        if (effect.activeSelf)
            return;
        effect.SetActive(true);
    }
    public void EventEnd()
    {
        gameObject.SetActive(false);
    }
}
