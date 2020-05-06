using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class testslide : MonoBehaviour
{
    public Slider sl;
    bool isDrag;
    public void DragSlide()
    {
        isDrag = true;
        Debug.LogError("Dragging");
    }
    public void EndDragSlide()
    {
        if (sl.value < 1f)
        {
            isDrag = false;
        }
        else
        {
            isDrag = false;
            Debug.LogError("Load Scene");
        }
        Debug.LogError("End Drag");
    }
    public void BeginDragSlide()
    {
        Debug.LogError("Begin Drag");
    }
    private void Update()
    {
        if (isDrag)
            return;
        if (sl.value < 1)
        {
            sl.value -= Time.deltaTime * 2;
        }
    }
}
