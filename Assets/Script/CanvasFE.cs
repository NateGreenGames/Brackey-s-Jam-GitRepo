using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFE : MonoBehaviour
{
    [SerializeField] GameObject optionsMenu, creditsMenu, tutorialMenu;


    public void ChangeOptionMenuState(bool _newState)
    {
        optionsMenu.SetActive(_newState);
    }

    public void ChangeCreditsMenuState(bool _newState)
    {
        creditsMenu.SetActive(_newState);
        if(_newState == true)
        {
            creditsMenu.GetComponent<Animator>().SetTrigger("Start");
        }
    }

    public void ChangeTutorialMenuState(bool _newState)
    {
        tutorialMenu.SetActive(_newState);
    }

    public void ChangeScenes(string _sceneName)
    {
        LoadingManager.instance.ChangeScene(_sceneName, 1f, 1f);
    }

    public void CloseGame()
    {
        SteamManager.instance.DisconnectFromSteam();
        LoadingManager.instance.ExitGame();
    }
}
