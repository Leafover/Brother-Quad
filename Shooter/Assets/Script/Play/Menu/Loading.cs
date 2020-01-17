using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Loading : MonoBehaviour
{
    bool isloading;
    public Image fillImage;
    public Text hintText;
    void Start()
    {
        isloading = true;
        fillImage.fillAmount = 0;
        ObjectPoolerManager.Instance.ClearAllPool();
        ObjectPoolManagerHaveScript.Instance.ClearAllPool();
        hintText.text = DataParam.hints[Random.Range(0, DataParam.hints.Length)];
    }
    IEnumerator DelayChangeScene()
    {
        yield return new WaitForSeconds(0.8f);
        Application.LoadLevel(DataParam.nextSceneAfterLoad);
    }
    private void Update()
    {
        if (isloading)
        {
            fillImage.fillAmount += Time.deltaTime / 2;
            if (fillImage.fillAmount >= 1)
            {
                isloading = false;
                StartCoroutine(DelayChangeScene());
            }
        }
    }
}
