using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
public class MayBayController : MonoBehaviour
{
    public float damageboom;
    public Rigidbody2D rid;
    Vector2 pos;
    bool isSpawnPlayer = false;
    public bool isBegin,isAttack;
    private void OnValidate()
    {
        if (rid == null)
            rid = GetComponent<Rigidbody2D>();
    }
    public void Begin(Vector2 _pos,bool begin,float speed)
    {
        isBegin = begin;
        pos.x = _pos.x - 5;
        pos.y = _pos.y + 3;
        transform.position = pos;
        gameObject.SetActive(true);
        rid.velocity = (transform.right * speed);

    }
    float timeAttack;
    private void Update()
    {
        if (transform.position.x > CameraController.instance.bouders[2].transform.position.x + 5)
        {
            if (isBegin)
                isBegin = false;
            gameObject.SetActive(false);
            rid.velocity = Vector2.zero;
        }
        if (isBegin)
        {
            if (transform.position.x > GameController.instance.currentMap.pointBeginPlayer.transform.position.x && !isSpawnPlayer)
            {
                PlayerController.instance.rid.gravityScale = 0.3f;
                PlayerController.instance.transform.position = new Vector2(transform.position.x, transform.position.y - 1.5f);
                isSpawnPlayer = true;
            }
            if (GameController.instance.currentMap.isVIPProtect && transform.position.x > GameController.instance.currentMap.pointBeginPlayer.transform.position.x + 1 && !GameController.instance.npcController.gameObject.activeSelf)
            {
                GameController.instance.npcController.rid.gravityScale = 0.3f;
                GameController.instance.npcController.transform.position = new Vector2(transform.position.x, transform.position.y - 1.5f);
                GameController.instance.npcController.gameObject.SetActive(true);
            }
        }
        else
        {
            if(isAttack)
            {
                timeAttack -= Time.deltaTime;
                if (timeAttack <= 0)
                {
                    boom = ObjectPoolerManager.Instance.boomPlanePooler.GetPooledObject();
                    boom.transform.position = transform.position;
                    boom.SetActive(true);
                    timeAttack = 0.2f;
                    Debug.LogError("boooommmmm");
                }
            }
        }
    }
    GameObject boom;
}
