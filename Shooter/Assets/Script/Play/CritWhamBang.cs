﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritWhamBang : MonoBehaviour
{
   public float timeDisplay = 1.5f;
    Vector2 temp;
    public void DisplayMe(Vector2 pos)
    {

        temp.x = pos.x - 0.5f;
        temp.y = pos.y + 0.5f;
        timeDisplay = 1.5f;
        gameObject.transform.position = temp;
        gameObject.SetActive(true);

        
    }
    public void DisableMe(float deltaTime)
    {
        if (!gameObject.activeSelf)
            return;
        timeDisplay -= deltaTime;
        if (timeDisplay <= 0)
            gameObject.SetActive(false);
    }
}
