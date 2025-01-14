﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAll : MonoBehaviour
{
 //   public List<GameObject> pointList;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 13:
                if (GameController.instance.gameState == GameController.GameState.gameover)
                    return;
                //PlayerController.instance.rid.velocity = Vector2.zero;
                //PlayerController.instance.speedmove = 0;
                if (collision.gameObject.tag != "NPC")
                {
                    SoundController.instance.PlaySound(soundGame.soundroixuongnuoc);
                    PlayerController.instance.TakeDamage(PlayerController.instance.maxHealth / 100 * 10, false);
                    if (PlayerController.instance.health > 0)
                        PlayerController.instance.ResetPosRevive(false, 0);
                }
                else
                {
                    GameController.instance.npcController.TakeDamage(PlayerController.instance.maxHealth / 100 * 10);
                    if (GameController.instance.npcController.health > 0)
                        GameController.instance.npcController.ResetPosRevive();
                }
                //if (PlayerController.instance.currentStand != null)
                //{
                //    if (PlayerController.instance.currentStand.transform.position.x < transform.position.x)
                //    {
                //        PlayerController.instance.transform.position = pointList[0].transform.position;
                //    }
                //    else
                //    {
                //        PlayerController.instance.transform.position = pointList[1].transform.position;
                //    }
                //}
                //else
                //{
                //    PlayerController.instance.transform.position = pointList[0].transform.position;
                //}
                break;
            case 12:
                collision.gameObject.SetActive(false);
                break;
            case 17:
                collision.gameObject.SetActive(false);
                break;
            case 16:
                collision.gameObject.SetActive(false);
                break;
            case 10:
                collision.gameObject.SetActive(false);
                break;
            case 25:
                collision.gameObject.SetActive(false);
                break;
        }
    }
}
