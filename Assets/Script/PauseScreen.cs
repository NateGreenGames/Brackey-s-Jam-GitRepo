using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu = null;
    [SerializeField] GameObject optionsMenu;
    bool isPaused;
    bool inOptions = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void OnResumeClick()
    {
        TogglePause();
    }

    public void OnOptionsClick()
    {
        inOptions = !inOptions;
        optionsMenu.SetActive(inOptions);
    }

    public void OnQuitClick()
    {
        isPaused = !isPaused;
        AudioListener.pause = isPaused;
        Cursor.visible = isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = isPaused ? 0 : 1;
        pauseMenu.SetActive(isPaused);
        StartCoroutine(LoadingManager.instance.ChangeScenes("MainMenu", 3, 3));
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        AudioListener.pause = isPaused;
        Cursor.visible = isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = isPaused ? 0 : 1;
        pauseMenu.SetActive(isPaused);
    }

    public void PlayClick(AudioClip _audioToPlay)
    {
        AudioManager.instance.sfxSource.PlayOneShot(_audioToPlay);
    }
}
