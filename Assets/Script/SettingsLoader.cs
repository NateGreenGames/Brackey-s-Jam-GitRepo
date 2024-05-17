using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsLoader : MonoBehaviour
{
    void Awake()
    {
        if(PlayerPrefs.HasKey("MyLanguage")){
            SubtitleController.language = (eSubtitleLanguage)PlayerPrefs.GetInt("MyLanguage");
        }

        bool hasSubtitles = PlayerPrefs.HasKey("SubtitlesEnabled");
        if (hasSubtitles)
        {
            switch (PlayerPrefs.GetInt("SubtitlesEnabled"))
            {
                case 0:
                    SubtitleController.hasSubtitles = false;
                    break;
                case 1:
                    SubtitleController.hasSubtitles = true;
                    break;
                default:
                    Debug.Log("Invalid language enabled save data detected.");
                    break;
            }
        }

    }

}
