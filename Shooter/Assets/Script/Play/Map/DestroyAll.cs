using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAll : MonoBehaviour
{
    public List<GameObject> pointList;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 13:
                PlayerController.instance.rid.velocity = Vector2.zero;
                PlayerController.instance.speedmove = 0;
                SoundController.instance.PlaySound(soundGame.soundroixuongnuoc);
                if (PlayerController.instance.currentStand != null)
                {
                    if (PlayerController.instance.currentStand.transform.position.x < transform.position.x)
                    {
                        PlayerController.instance.transform.position = pointList[0].transform.position;
                    }
                    else
                    {
                        PlayerController.instance.transform.position = pointList[1].transform.position;
                    }
                }
                else
                {
                    PlayerController.instance.transform.position = pointList[0].transform.position;
                }
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
