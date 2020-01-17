using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Loading : MonoBehaviour
{
    bool isloading;
    public Image fillImage;
    public Text hintText, versionText;
    public AsyncOperation currentLoadingOperation;


    public void Show(AsyncOperation loadingOperation)
    {
        currentLoadingOperation = loadingOperation;
        currentLoadingOperation.allowSceneActivation = false;
    }


    void Start()
    {
        isloading = true;
        fillImage.fillAmount = 0;
        ObjectPoolerManager.Instance.ClearAllPool();
        ObjectPoolManagerHaveScript.Instance.ClearAllPool();
        hintText.text = DataParam.hints[Random.Range(0, DataParam.hints.Length)];
        versionText.text = "Version: " + Application.version;
        Show(SceneManager.LoadSceneAsync(DataParam.nextSceneAfterLoad));
    }
    private void Update()
    {
        if (isloading)
        {
            fillImage.fillAmount += Time.deltaTime / 3;
            if (fillImage.fillAmount >= 1)
            {
                isloading = false;
                currentLoadingOperation.allowSceneActivation = true;
            }
        }
    }
}
