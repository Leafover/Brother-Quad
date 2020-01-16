using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Loading : MonoBehaviour
{
    public Text hintText;
    void Start()
    {
        ObjectPoolerManager.Instance.ClearAllPool();
        ObjectPoolManagerHaveScript.Instance.ClearAllPool();
        hintText.text = DataParam.hints[Random.Range(0, DataParam.hints.Length)];
        StartCoroutine(DelayChangeScene());
    }
    IEnumerator DelayChangeScene()
    {
        yield return new WaitForSeconds(2);
        Application.LoadLevel(DataParam.nextSceneAfterLoad);
    }
}
