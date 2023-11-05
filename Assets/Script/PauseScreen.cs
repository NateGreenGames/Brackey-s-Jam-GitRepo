using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu = null;
    [SerializeField] GameObject optionsMenu;
    bool isPaused;
    bool inOptions;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = isPaused ? 0 : 1;
            pauseMenu.SetActive(isPaused);
        }
    }

    public void OnResumeClick()
    {
        isPaused = !isPaused;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = isPaused ? 0 : 1;
        pauseMenu.SetActive(isPaused);
    }

    public void OnOptionsClick()
    {
        inOptions = !inOptions;
        optionsMenu.SetActive(inOptions);
    }

    public void OnQuitClick()
    {
        Debug.Log("lol");
        LoadingManager.instance.ChangeScene("MainMenu");
        Time.timeScale = 1;
    }
}
