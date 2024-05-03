using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetSaveData : MonoBehaviour
{
    private void OnEnable()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu" && PlayerPrefs.HasKey("HasPlayedIntro"))
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }


    public void resetSaveData()
    {
        PlayerPrefs.DeleteKey("HasPlayedIntro");
    }
}
