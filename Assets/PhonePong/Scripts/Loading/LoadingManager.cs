using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance {get; private set;}
    
    public TMP_Text loadingText;
    
    private AsyncOperation loadOperation;
    private bool isLoadComplete = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (!isLoadComplete || !Input.GetMouseButtonDown(0)) return;
        loadOperation.allowSceneActivation = true;
        isLoadComplete = false;
    }

    public IEnumerator LoadSceneAsync(string sceneName)
    {
        loadOperation = SceneManager.LoadSceneAsync(sceneName);
        if (loadOperation == null) yield break;
        loadOperation.allowSceneActivation = false;

        while (!loadOperation.isDone)
        {
            if (loadOperation.progress >= 0.9f)
            {
                isLoadComplete = true;
                loadingText.text = "Click To Start";
                break;
            }

            yield return null;
        }
    }

    public IEnumerator AnimateLoadingText()
    {
        const string baseText = "Loading";
        var dotCount = 0;

        while (!isLoadComplete)
        {
            dotCount = (dotCount + 1) % 4;
            loadingText.text = baseText + new string('.', dotCount);
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }
}
