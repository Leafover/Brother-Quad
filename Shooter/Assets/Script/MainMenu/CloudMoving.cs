using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMoving : MonoBehaviour
{
    public Vector3 vDesEnd;
    [Range(5,100)]
    public float speed = 10;

    private Vector3 vStart;
    
    void Start()
    {
        vStart = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.localPosition, vDesEnd) <= 0.1f)
        {
            transform.localPosition = vStart;
        }
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, vDesEnd, speed * Time.deltaTime);
    }
}
