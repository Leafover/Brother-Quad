using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
public class MayBayController : MonoBehaviour
{
    public Rigidbody2D rid;
    Vector2 pos;
    bool isSpawnPlayer = false;
    private void OnValidate()
    {
        if (rid == null)
            rid = GetComponent<Rigidbody2D>();
    }
    public void Begin(Vector2 _pos)
    {
        pos.x = _pos.x - 5;
        pos.y = _pos.y;
        transform.position = pos;
        rid.velocity = (transform.right * 5);
    }
    private void Update()
    {
        if (transform.position.x > CameraController.instance.bouders[2].transform.position.x + 5)
        {
            gameObject.SetActive(false);
        }
        if (transform.position.x > GameController.instance.currentMap.pointBeginPlayer.transform.position.x && !isSpawnPlayer)
        {
            PlayerController.instance.rid.gravityScale = 0.3f;
            PlayerController.instance.transform.position = transform.position;
            isSpawnPlayer = true;
        }
    }
}
