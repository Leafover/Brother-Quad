using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowHealthPanel : MonoBehaviour
{
    public void EventEnd()
    {
        gameObject.SetActive(false);
    }
}
