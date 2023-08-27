using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] Slider[] sliders;
    private void OnEnable()
    {
        sliders[0].value = AudioManager.instance.masterVolume;
        sliders[1].value = AudioManager.instance.musicVolume;
        sliders[2].value = AudioManager.instance.sfxVolume;
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
}
