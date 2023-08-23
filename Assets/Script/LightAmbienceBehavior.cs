using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LightAmbienceBehavior : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private bool isPlaying = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isPlaying = true;
    }

    // Function to toggle the audio source
    public void ToggleAudioSource()
    {
        if (isPlaying)
        {
            audioSource.Stop();
        }
        else
        {
            audioSource.Play();
        }
        isPlaying = !isPlaying;
    }
}
