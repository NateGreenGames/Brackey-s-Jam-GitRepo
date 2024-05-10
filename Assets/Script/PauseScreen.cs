using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu = null;
    [SerializeField] GameObject optionsMenu;
    bool isPaused;
    bool inOptions = false;
    bool isQuitting = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isQuitting == false)
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
        if (!LoadingManager.instance.isLoading)
        {
            isQuitting = true;
            AudioListener.pause = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 1;
            StartCoroutine(LoadingManager.instance.ChangeScenes("MainMenu", 0, 0));
        }
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
