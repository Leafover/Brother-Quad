using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float force;
    public Rigidbody2D rid;


    Vector2 right = new Vector2(1, 1);
    Vector2 left = new Vector2(-1, 1);

    //private void OnBecameInvisible()
    //{
    //    gameObject.SetActive(false);
    //}
    private void OnEnable()
    {
        if (PlayerController.instance == null)
            return;
        if (!PlayerController.instance.FlipX)
        {
            rid.AddForce(right * force);
        }
        else
        {
            rid.AddForce(left * force);
            //   Debug.Log("2");
        }
    }
    GameObject effectGrenade;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (/*collision.gameObject.layer == 8 ||*/ collision.gameObject.layer == 10 || collision.gameObject.layer == 19/* || collision.gameObject.layer == 21*/ /*|| collision.gameObject.layer == 23*/)
        {
            HitGrenade();
        }
        else if(collision.gameObject.layer == 8 || collision.gameObject.layer == 21 || collision.gameObject.layer == 23)
        {
            if (transform.position.y <= collision.gameObject.transform.position.y)
            {
             //   Debug.Log("zooooooooooooooooo");
                return;
            }

            HitGrenade();
        }
    }
     void HitGrenade()
    {
        CameraController.instance.Shake();
        effectGrenade = ObjectPoolerManager.Instance.effectGrenadePooler.GetPooledObject();
        effectGrenade.transform.position = gameObject.transform.position;
        effectGrenade.SetActive(true);
        gameObject.SetActive(false);
        SoundController.instance.PlaySound(soundGame.exploGrenade);
    }
}
