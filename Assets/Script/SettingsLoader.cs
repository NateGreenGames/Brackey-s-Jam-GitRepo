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
            SubtitleController.hasSubtitles = hasSubtitles;
        }

    }

}
