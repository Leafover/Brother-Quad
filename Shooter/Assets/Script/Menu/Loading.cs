using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ObjectPoolerManager.Instance.ClearAllPool();
        StartCoroutine(DelayChangeScene());
    }
    IEnumerator DelayChangeScene()
    {
        yield return new WaitForSeconds(2);
        Application.LoadLevel(DataParam.nextSceneAfterLoad);
    }
}
