using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform outPos;
    public static CameraController instance;
    private void Start()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
       // if (!PlayerController.playerController.FlipX)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(PlayerController.instance.transform.position.x + 5, transform.position.y, transform.position.z), Time.deltaTime * 5);
    }
}
