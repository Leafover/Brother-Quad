using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPlayer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 8:
                PlayerController.instance.CheckColliderStand(null);

                break;
            case 21:
                if (collision.collider != PlayerController.instance.colliderStand)
                    PlayerController.instance.CheckColliderStand(collision.collider);
                break;
        }
    }
}
