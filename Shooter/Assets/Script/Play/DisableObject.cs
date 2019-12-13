using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObject : MonoBehaviour
{
    public float time = 2;
    WaitForSeconds waitforsecond;
    public BulletEnemy bulletEnemy;
    public enum TypeExplo
    {
        normal,
        exploE2

    }
    public TypeExplo typeExplo;
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
        //if (bulletEnemy)
        //    bulletEnemy.AutoRemoveMe();
        if(typeExplo == TypeExplo.exploE2)
        {
            GameObject g = ObjectPoolerManager.Instance.explobulletenemy2Pooler.GetPooledObject();
            g.transform.position = gameObject.transform.position;
            g.SetActive(true);
        }
    }
}
