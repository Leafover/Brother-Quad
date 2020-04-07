using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedZone : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (PlayerController.instance == null || !CameraController.instance.activeRedZone)
            return;
        if (collision.gameObject.layer == 13)
        {
            CameraController.instance.timeTakeDamgeRedZone -= Time.deltaTime;
            if (CameraController.instance.timeTakeDamgeRedZone <= 0)
            {
                CameraController.instance.timeTakeDamgeRedZone = CameraController.instance.maxTimeTakeDamageRedZone;
                PlayerController.instance.TakeDamage(CameraController.instance.damageRedZone);
            }
        }
    }
}
