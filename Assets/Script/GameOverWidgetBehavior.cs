using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverWidgetBehavior : MonoBehaviour
{
    public TextMeshProUGUI deathDescriptionText;


    public void UpdateDeathDescriptionText(string _newDescription)
    {
        deathDescriptionText.text = _newDescription;
    }

    //Button Behaviors:

    public void RestartGameScene()
    {
        LoadingManager.instance.ChangeScene("NateGreen");
    }

    public void GoToFrontEnd()
    {
        LoadingManager.instance.ChangeScene("MainMenu");
    }
}
