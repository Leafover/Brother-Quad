using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGroundPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            // PlayerController.playerController.playerState = PlayerController.PlayerState.Idle;
            PlayerController.playerController.isGround = true;
        }
    }
}
