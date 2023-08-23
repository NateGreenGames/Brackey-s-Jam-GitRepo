using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterAudioBehavior : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private bool isPlaying = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isPlaying = true;
    }

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
