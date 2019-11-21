using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform outPos;
    public static CameraController instance;
    public List<Transform> posEnemyV2,posMiniBoss1;
    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance == null)
            return;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(PlayerController.instance.transform.position.x + 1, transform.position.y, transform.position.z), Time.deltaTime * 5);
    }
}
