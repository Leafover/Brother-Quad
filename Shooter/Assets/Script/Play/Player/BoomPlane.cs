using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomPlane : MonoBehaviour
{

    public void Hit()
    {
        GameObject effect = ObjectPoolerManager.Instance.exploPlanePooler.GetPooledObject();
        effect.transform.position = gameObject.transform.position;
        effect.SetActive(true);
        gameObject.SetActive(false);
        SoundController.instance.PlaySound(soundGame.exploGrenade);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 8:
                Hit();
                break;
            case 21:
                Hit();
                break;
            case 23:
                Hit();
                break;
            case 10:
                Hit();
                break;
            case 19:
                Hit();
                break;
        }
    }
}
