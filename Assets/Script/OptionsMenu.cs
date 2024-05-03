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
    private void OnEnable()
    {
        sliders[0].value = AudioManager.instance.masterVolume;
        sliders[1].value = AudioManager.instance.musicVolume;
        sliders[2].value = AudioManager.instance.sfxVolume;
        toggle.isOn = SubtitleController.hasSubtitles;
        dropdown.value = (int)SubtitleController.language;
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
        Debug.Log(SubtitleController.hasSubtitles);
    }

    public void UpdateSubtitleLanguage(int _languageIndex)
    {
        SubtitleController.language = (eSubtitleLanguage)_languageIndex;
        Debug.Log(SubtitleController.language);
    }

    public void PlayClick(AudioClip _audioToPlay)
    {
        AudioManager.instance.sfxSource.PlayOneShot(_audioToPlay);
    }
}
