using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObject : MonoBehaviour
{
    public float time = 2;
    WaitForSeconds waitforsecond;
    public BulletEnemy bulletEnemy;
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
        if (bulletEnemy)
            bulletEnemy.AutoRemoveMe();
    }
}
