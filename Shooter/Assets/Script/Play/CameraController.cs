using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,new Vector3(PlayerController.playerController.transform.position.x,transform.position.y,transform.position.z), Time.deltaTime * 5);
    }
}
