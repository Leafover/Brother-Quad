using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGroundPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            PlayerController.playerController.DetectGround();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            PlayerController.playerController.isGround = false;
            if (PlayerController.playerController.playerState == PlayerController.PlayerState.Jump)
            {
                PlayerController.playerController.isfalldow = false;
            }
            else
            {
                PlayerController.playerController.isfalldow = true;
            }
        }
    }
}
