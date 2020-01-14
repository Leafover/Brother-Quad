using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    public float speed = 0.5f;
    public Renderer render;
    Vector2 offset;
   public enum SortingLayerName
    {
        Default,
        UI
    }
    public void SetSpeed(int _speed)
    {
        speed = _speed;
    }
    private void OnValidate()
    {
        render.sortingLayerName = SortingLayerName.Default.ToString();
        render.sortingOrder = -1;
    }
    private void Update()
    {
        offset = new Vector2(Time.deltaTime * speed, 0);
        render.material.mainTextureOffset = offset;
    }
    void LateUpdate()
    {
        var pos = transform.position;
        pos.x = Camera.main.transform.position.x;
        pos.y = transform.position.y;
        transform.position = pos;
    }
}
