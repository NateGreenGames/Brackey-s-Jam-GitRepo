using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum eSFX
{
    //Ambience
    englishIntro, spanishIntro, hum, bubbles,

    //SFX
    buttonClick, leverPushPull, airHiss, pipeBurst, fusePop, fuseExplode, fuseClunk, lightOn, lightOff, 
    engineOn, engineOff, powerWarning, waterMovement, powerOff, powerOn, hullHit, hullWane, creatureAttack, 
    creatureFlee, creatureApproach, creatureSqueel, leverBump, crush, suffocation, hatchOpen, valvesqueek1, 
    valvesqueek2, valvesqueek3, plungerShock, plungerLockedIn, none
}

public enum eMusic { titleMusic, gameplayMusicCalm, gameplayMusicDanger, none }

public class AudioManager : MonoBehaviour
{

    [Space]
    [Header("Volume Settings")]
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;

    public bool defaultVolume;

    // Default Settings
    public float defaultMasterVolume = 1f;
    public float defaultMusicVolume = .5f;
    public float defaultSfxVolume = .5f;

    [Space]
    [Header("Audio Mixers")]
    public AudioMixerGroup masterMixer;
    public AudioMixerGroup musicMixer;
    public AudioMixerGroup sfxMixer;

    [Space]
    [Header("Audio Clips")]
    [NamedArray(typeof(eMusic))] public AudioClip[] music;
    [NamedArray(typeof(eSFX))] public AudioClip[] sfx;

    private AudioClip currentMusic;
    eMusic eCurrentMusic;

    [Space]
    [Header("Audio Sources")]
    public AudioSource sfxSource;

    public AudioSource musicSource1;
    public AudioSource musicSource2;

    public AudioSource ambienceSource;

    private AudioSource currentMusicSource;
    private AudioSource standbyMusicSource;

    public static int musicOrder = 0;
    public static AudioManager instance;

    AudioLowPassFilter lowPassFilter;

    // Awake Refs for making into Singleton
    /*
    private void Awake()
    {

        if (am == null)

        {
            am = this;
        }

        else if (am != this)
        {
            Destroy(gameObject);
        }

        currentMusicSource = musicSource1;

        standbyMusicSource = musicSource2;

        musicSource1.loop = true;

        musicSource2.loop = true;


    }
    */

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", defaultMasterVolume);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", defaultMusicVolume);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", defaultSfxVolume);

        ChangeMasterVolume(masterVolume);
        ChangeMusicVolume(musicVolume);
        ChangeSFXVolume(sfxVolume);
    }
    // USE THIS FUNCTION TO SWITCH UP THE AUDIOCLIP FOR MUSIC
    public void ChangeMusicClip(AudioSource _source, eMusic _music)
    {

        _source.clip = music[(int)_music];

    }

    // USE THIS TO PLAY MUSIC ONCE THE CLIP HAS BEEN SET
    public void PlayMusic(eMusic _music)
    {

        currentMusic = music[(int)_music];

        currentMusicSource.clip = currentMusic;

        currentMusicSource.Play();

    }

    // USE THIS FOR GAMEOBJECTS WITH THEIR OWN AUDIOSOURCES TO RETURN CLIPS.  MOSTLY FOR 3D AUDIO.
    public AudioClip GetSFX(eSFX _sfx)
    {
        return (sfx[(int)_sfx]);
    }

    // USE THIS TO PLAY ANY SFX FROM ANYWHERE IN 2D
    public void PlaySFX(eSFX _sfx, float volume)
    {
        //Debug.Log("Playing " + sfx[(int)_sfx]);
        if (sfx[(int)_sfx] != null)
        {
            sfxSource.PlayOneShot(sfx[(int)_sfx], volume);
            SubtitleController.CreateNewSubtitle(_sfx, 1f);
        }
        else
        {
            Debug.LogWarning(_sfx.ToString() + " sound effect still needs a clip");
        }
    }

    public void PlayAudioAtPoint(Transform _pointToPlay, eSFX _sfx)
    {

    }

    // MIXER CONTROLS - USE THESE FOR UI/OPTIONS MENU TO CHANGE SOUND PROPERTIES

    public void ChangeMusicVolume(float _newValue)// Changes fader value of music volume
    {
        _newValue = Mathf.Clamp(_newValue, 0.1f, 1f);
        musicVolume = _newValue;
        musicMixer.audioMixer.SetFloat("MusicVolume", Mathf.Log10(_newValue) * 40);// Changes as a logarithmic fade

    }

    public void ChangeSFXVolume(float _newValue)// Changes fader value of sfx volume
    {
        _newValue = Mathf.Clamp(_newValue, 0.1f, 1f);
        sfxVolume = _newValue;
        sfxMixer.audioMixer.SetFloat("SFXVolume", Mathf.Log10(_newValue) * 40);

    }

    public void ChangeMasterVolume(float _newValue)// Changes fader value of master volume
    {
        _newValue = Mathf.Clamp(_newValue, 0.1f, 1f);
        masterVolume = _newValue;
        masterMixer.audioMixer.SetFloat("MasterVolume", Mathf.Log10(_newValue) * 40);

    }

    // STOP SOUNDS

    public void StopMusic()
    {

        //currentMusicSource.clip = currentMusic;
        currentMusicSource.Stop();

        //musicSource[(int)_soundIndex].Stop();
    }

    // FADING

    // FADES OUT A AUDIOSOUCE PASSED INTO IT
    public IEnumerator FadeOut(AudioSource _audioSource, float _fadeTime)
    {

        float startVolume = _audioSource.volume;

        while (_audioSource.volume > 0)
        {

            _audioSource.volume -= Time.deltaTime / _fadeTime;
            yield return null;

        }

        _audioSource.Stop();

        standbyMusicSource = _audioSource;
    }

    // FADES IN AN AUDIOSOUCES PASSED IN
    public IEnumerator FadeIn(AudioSource _audioSource, float _fadeTime)
    {

        float startVolume = _audioSource.volume;

        currentMusicSource = _audioSource;

        _audioSource.Play();
        _audioSource.volume = 0f;

        while (_audioSource.volume < 1f)
        {


            _audioSource.volume += Time.deltaTime / _fadeTime;
            yield return null;

        }

    }

    // AUDIO FILTER - USE THIS WHEN PAUSED, ETC

    public void LowPassMusicToggle(bool _turnOn)
    {


        if (_turnOn)
        {
            musicMixer.audioMixer.SetFloat("LowPassFilter", 1000);
        }
        else
        {
            musicMixer.audioMixer.SetFloat("LowPassFilter", 22000);
        }

        //float startFreq = musicMixer.audioMixer.GetFloat("LowPassFilter");

    }

    // RETURNS CURRENT AUDIOSOURCE
    public AudioSource GetCurrentMusicSource()
    {

        return currentMusicSource;

    }

    // RETURNS STANDBY SOURCE
    public AudioSource GetStandbyMusicSource()
    {

        return standbyMusicSource;

    }
}
