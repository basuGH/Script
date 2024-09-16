using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] Image _loadingImage;
    [SerializeField] Text _loadingText;
    void Awake()
    {
        StartCoroutine(LoadScene());
        _loadingImage.fillAmount = 0;
        _loadingText.text = "0.00 %";
    }
    IEnumerator LoadScene()
    {
        yield return null;


        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(2);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            _loadingImage.fillAmount = asyncOperation.progress;

            //Debug.Log(asyncOperation.progress);
            _loadingText.text = Mathf.Round(asyncOperation.progress * 100).ToString("0.00") + "%";
            //yield return new WaitForEndOfFrame();
            yield return null;

        }
        Debug.Log("scene Load Complete");
    }
}
