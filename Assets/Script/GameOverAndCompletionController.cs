using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverAndCompletionController : MonoBehaviour
{
    public static GameOverAndCompletionController instance;
    public GameObject playerCamera;
    public Transform canvasTransform;
    private CanvasGroup fadePanel;
    public bool isOver = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        fadePanel = gameObject.GetComponent<CanvasGroup>();
        fadePanel.alpha = 0f;
    }

    public void EndGame(string _gameOverDescription)
    {
        isOver = true;
        StartCoroutine(DisplayGameOverScreen(_gameOverDescription));
        Destroy(playerCamera.GetComponent<PlayerFreelookCameraBehavior>());
    }

    public void WinGame()
    {
        isOver = true;
        StartCoroutine(DisplayWinningScreen());
        Destroy(playerCamera.GetComponent<PlayerFreelookCameraBehavior>());
    }
    private IEnumerator DisplayGameOverScreen(string _gameOverDescription)
    {
        yield return StartCoroutine(FadeToBlack(3f));
        AudioManager.instance.StartCoroutine(AudioManager.instance.FadeOut(AudioManager.instance.musicSource1, 7));
        if(AudioManager.instance.musicSource2.volume > 0) AudioManager.instance.StartCoroutine(AudioManager.instance.FadeOut(AudioManager.instance.musicSource2, 7));
        GameOverWidgetBehavior newWidget = Instantiate(Resources.Load("Widgets/GameOverWidget") as GameObject, canvasTransform).GetComponent<GameOverWidgetBehavior>();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        newWidget.UpdateDeathDescriptionText(_gameOverDescription);
    }

    private IEnumerator DisplayWinningScreen()
    {
        yield return StartCoroutine(FadeToBlack(3f));
        AudioManager.instance.StartCoroutine(AudioManager.instance.FadeOut(AudioManager.instance.musicSource1, 7));
        if (AudioManager.instance.musicSource2.volume > 0) AudioManager.instance.StartCoroutine(AudioManager.instance.FadeOut(AudioManager.instance.musicSource2, 7));
        Instantiate(Resources.Load("Widgets/WinWidget") as GameObject, canvasTransform);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private IEnumerator FadeToBlack(float _duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < _duration)
        {
            fadePanel.alpha = Mathf.Lerp(0f, 1f, elapsedTime / _duration);
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
        }
        fadePanel.alpha = 1f;
    }
}
