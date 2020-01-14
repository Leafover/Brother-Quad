using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillSizeCamera : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var scale = transform.localScale;
        scale.x = Mathf.Max(1, (float)Screen.width / 1280f);
        scale.y = Mathf.Max(1, (float)Screen.height / 720f);
        transform.localScale = scale;
    }
}
