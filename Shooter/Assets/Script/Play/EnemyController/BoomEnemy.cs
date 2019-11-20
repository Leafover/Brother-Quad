using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEnemy : MonoBehaviour
{
    void Hit()
    {
        GameObject effect = ObjectPoolerManager.Instance.effectExploBoomEnemyV3Pooler.GetPooledObject();
        effect.transform.position = gameObject.transform.position;
        effect.SetActive(true);
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 13:
                Hit();
                break;
            case 8:
                Hit();
                break;
        }
    }
}
