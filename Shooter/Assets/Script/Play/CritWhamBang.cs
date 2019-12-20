using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritWhamBang : MonoBehaviour
{
    float timeDisplay = 1;
    Vector2 temp;
    public void DisplayMe(Vector2 pos)
    {

        temp.x = pos.x - 0.5f;
        temp.y = pos.y + 0.5f;
        timeDisplay = 1;
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
