using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsLoader : MonoBehaviour
{
    void Awake()
    {
        if(PlayerPrefs.HasKey("SubtitlesEnabled")){
            SubtitleController.language = (eSubtitleLanguage)PlayerPrefs.GetInt("SubtitlesEnabled");
        }

        bool hasSubtitles = PlayerPrefs.HasKey("MyLanguage");
        if (hasSubtitles)
        {
            SubtitleController.hasSubtitles = hasSubtitles;
        }
    }

}
