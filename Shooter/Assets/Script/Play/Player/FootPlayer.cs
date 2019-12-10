using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPlayer : MonoBehaviour
{
    public void DetectGround(GameObject collision)
    {
        if (PlayerController.instance.currentStand != collision)
            PlayerController.instance.currentStand = collision;
        if (!PlayerController.instance.dustdown.activeSelf)
            PlayerController.instance.dustdown.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 8:
                PlayerController.instance.CheckColliderStand(null);
                DetectGround(collision.gameObject);
                break;
            case 21:
                DetectGround(collision.gameObject);
                if (collision.collider != PlayerController.instance.colliderStand)
                    PlayerController.instance.CheckColliderStand(collision.collider);
                break;
            case 23:
                DetectGround(collision.gameObject);
                break;

        }
    }
}
