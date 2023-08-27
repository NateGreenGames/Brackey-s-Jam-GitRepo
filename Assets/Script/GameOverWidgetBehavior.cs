using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverWidgetBehavior : MonoBehaviour
{
    public TextMeshProUGUI deathDescriptionText;

    private void Start()
    {
        Time.timeScale = 0;
    }
    public void UpdateDeathDescriptionText(string _newDescription)
    {
        deathDescriptionText.text = _newDescription;
    }

    //Button Behaviors:

    public void RestartGameScene()
    {
        LoadingManager.instance.ChangeScene("NateGreen");
        Time.timeScale = 1;
    }

    public void GoToFrontEnd()
    {
        LoadingManager.instance.ChangeScene("MainMenu");
        Time.timeScale = 1;
    }
}
