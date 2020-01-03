using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTarget : MonoBehaviour
{
    [HideInInspector]
    public bool incam;
  //  [HideInInspector]
    public float currentHealth;
    public int index;
    [HideInInspector]
    public NumberDamageTextController numberText;
    [HideInInspector]
    public Vector2 posHitTemp;
    [HideInInspector]
    public GameObject hiteffect;
    [HideInInspector]
    public float hitPosTemp;
    [HideInInspector]
    public int takecrithit;
    public virtual Vector2 Origin()
    {
        return transform.position;
    }
}
