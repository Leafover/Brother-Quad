using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBtn : MonoBehaviour
{
    public ParticleSystem effect;
    public void EventClick()
    {
        effect.gameObject.SetActive(true);
    }
}
