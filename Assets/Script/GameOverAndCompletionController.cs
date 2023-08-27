using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverAndCompletionController : MonoBehaviour
{
    public static GameOverAndCompletionController instance;
    public Transform canvasTransform;
    private CanvasGroup fadePanel;

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
        MonsterManager.mM.attackRate = 0f;
        StartCoroutine(DisplayGameOverScreen(_gameOverDescription));
    }
    private IEnumerator DisplayGameOverScreen(string _gameOverDescription)
    {
        yield return StartCoroutine(FadeToBlack(3f));
        GameOverWidgetBehavior newWidget = Instantiate(Resources.Load("Widgets/GameOverWidget") as GameObject, canvasTransform).GetComponent<GameOverWidgetBehavior>();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        newWidget.UpdateDeathDescriptionText(_gameOverDescription);
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
