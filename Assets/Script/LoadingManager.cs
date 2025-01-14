using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager instance;
    [SerializeField] private CanvasGroup fadingPanelGroup;
    [SerializeField] private GameObject loadingScreenObject;
    [Range(1, 20)] [SerializeField] float minimumLoadtime;
    public bool isLoading = false;
    AsyncOperation operation;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ExitGame()
    {
        Debug.Log("I tried to close the game, but you smell and are in editor. Would Jerry approve of this?");
        Application.Quit();
    }


    public void ChangeScene(string _scene, float _inDuration, float _outDuration)
    {
        if (!isLoading)
        {
            StartCoroutine(ChangeScenes(_scene, _inDuration, _outDuration));
        }
    }
    //This coroutines are split because I needed to include to yield return statements to get what I was looking for.
    //The way this is implimented in the internal functions is such: FadeToBlack => AfterFade => LoadAsync => FadeFromBlack.
    public IEnumerator ChangeScenes(string _scene, float _fadeInDuration, float _fadeOutduration)
    {
        //If isLoading is equal to false, start loading
        if (!isLoading)
        {
            yield return StartCoroutine(FadeToBlack(_fadeInDuration));
            StartCoroutine(AfterFade(_scene, _fadeOutduration));
        }
    }

    #region InternalFunctions
    private IEnumerator FadeToBlack(float _duration)
    {
        isLoading = true;
        fadingPanelGroup.interactable = true;
        float elapsedTime = 0;
        while (elapsedTime < _duration)
        {
            fadingPanelGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / _duration);
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.unscaledDeltaTime;
        }
        fadingPanelGroup.alpha = 1f;
    }
    private IEnumerator LoadAsync(string _scene)
    {
        loadingScreenObject.SetActive(true);
        operation = SceneManager.LoadSceneAsync(_scene);

        float elapsedTime = 0;
        while (operation.progress < 1 || elapsedTime < minimumLoadtime)
        {
            elapsedTime += Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        loadingScreenObject.SetActive(false);
    }
    private IEnumerator AfterFade(string _scene, float _fadeDuration)
    {
        yield return StartCoroutine(LoadAsync(_scene));
        StartCoroutine(FadeFromBlack(_fadeDuration));
    }
    private IEnumerator FadeFromBlack(float _duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < _duration)
        {
            fadingPanelGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / _duration);
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.unscaledDeltaTime;
        }
        fadingPanelGroup.alpha = 0f;
        isLoading = false;
        fadingPanelGroup.interactable = false;
    }
    #endregion
}