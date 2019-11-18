using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObject : MonoBehaviour
{
    public float time;
    WaitForSeconds waitforsecond;
    private void OnEnable()
    {
        if (waitforsecond == null)
            waitforsecond = new WaitForSeconds(time);
        StartCoroutine(Disable());
    }

    IEnumerator Disable()
    {
        yield return waitforsecond;
        gameObject.SetActive(false);
    }
}
