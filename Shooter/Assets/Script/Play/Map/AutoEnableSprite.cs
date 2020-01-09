using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoEnableSprite : MonoBehaviour
{
    [Header(" Sprite visible control:")]
    public bool canControlSpriteVisible = true;
    public bool canMove;
    public SpriteRenderer[] listSprite;
    public ViewPos[] listViewPos;
    public EnvirontmentEffect[] particles;
    private Vector3 camPos;
    //private Vector3 checkUL, checkDL, checkUR, checkDR;


    private void OnValidate()
    {
        if (!canControlSpriteVisible || Application.isPlaying)
            return;
        listSprite = GetComponentsInChildren<SpriteRenderer>();
        particles = GetComponentsInChildren<EnvirontmentEffect>();

        listViewPos = new ViewPos[listSprite.Length];
        for (int i = 0; i < listSprite.Length; i++)
        {
            CaculatorViewPos(i);
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (particles.Length > 0)
        {
            for (int i = 0; i < particles.Length; i++)
            {
                if (Math.Abs(particles[i].transform.position.x - Camera.main.transform.position.x) > Camera.main.orthographicSize)
                    particles[i].gameObject.SetActive(false);
                else
                    particles[i].gameObject.SetActive(true);
            }
        }
        if (canControlSpriteVisible)
        {
            if (listSprite != null && listSprite.Length > 0)
            {
                for (int i = 0; i < listSprite.Length; i++)
                {
                    if (!listSprite[i])
                        continue;
                    if (canMove)
                        CaculatorViewPos(i);
                    var check = (listViewPos[i].minX <= CameraController.instance.viewPos.maxX
                        && listViewPos[i].maxX >= CameraController.instance.viewPos.minX
                        && listViewPos[i].minY <= CameraController.instance.viewPos.maxY
                        && listViewPos[i].maxY >= CameraController.instance.viewPos.minY);
                    if (listSprite[i].enabled != check)
                        listSprite[i].enabled = check;
                }
            }
        }
    }

    private void CaculatorViewPos(int spriteId)
    {
        if (!listSprite[spriteId].sprite)
            return;
        listViewPos[spriteId].minX = listSprite[spriteId].bounds.min.x;
        listViewPos[spriteId].maxX = listSprite[spriteId].bounds.max.x;
        listViewPos[spriteId].minY = listSprite[spriteId].bounds.min.y;
        listViewPos[spriteId].maxY = listSprite[spriteId].bounds.max.y;
    }
}

