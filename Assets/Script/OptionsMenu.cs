using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] Slider[] sliders;
    [SerializeField] Toggle toggle;
    [SerializeField] TMP_Dropdown dropdown;


    private string subTitlesEnabledKey = "SubtitlesEnabled";
    private string languageKey = "MyLanguage";
    private void OnEnable()
    {
        sliders[0].value = AudioManager.instance.masterVolume;
        sliders[1].value = AudioManager.instance.musicVolume;
        sliders[2].value = AudioManager.instance.sfxVolume;
        if (!PlayerPrefs.HasKey(subTitlesEnabledKey) || PlayerPrefs.GetInt(subTitlesEnabledKey) == 0)
        {
            toggle.isOn = false;
        }
        else
        {
            toggle.isOn = true;
        }

        if(!PlayerPrefs.HasKey(languageKey) || PlayerPrefs.GetInt(languageKey) == 0)
        {
            dropdown.value = (int)eSubtitleLanguage.English;
        }
        else
        {
            dropdown.value = (int)eSubtitleLanguage.Spanish;
        }
    }




    public void SetMasterVolume(float _volume)
    {
        AudioManager.instance.ChangeMasterVolume(_volume);
    }
    public void SetMusicVolume(float _volume)
    {
        AudioManager.instance.ChangeMusicVolume(_volume);
    }
    public void SetSFXVolume(float _volume)
    {
        AudioManager.instance.ChangeSFXVolume(_volume);
    }

    public void ToggleSubtitles(bool _newState)
    {
        SubtitleController.hasSubtitles = _newState;
        if(_newState == true)
        {
            PlayerPrefs.SetInt(subTitlesEnabledKey, 1);
        }
        else
        {
            PlayerPrefs.SetInt(subTitlesEnabledKey, 0);
        }
        Debug.Log(SubtitleController.hasSubtitles);
    }

    public void UpdateSubtitleLanguage(int _languageIndex)
    {
        SubtitleController.language = (eSubtitleLanguage)_languageIndex;

        switch (SubtitleController.language)
        {
            case eSubtitleLanguage.English:
                PlayerPrefs.SetInt(languageKey, 0);
                break;
            case eSubtitleLanguage.Spanish:
                PlayerPrefs.SetInt(languageKey, 1);
                break;
        }
        Debug.Log(SubtitleController.language);
    }

    public void PlayClick(AudioClip _audioToPlay)
    {
        AudioManager.instance.sfxSource.PlayOneShot(_audioToPlay);
    }
}
