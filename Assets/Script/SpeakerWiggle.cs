using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerWiggle : MonoBehaviour
{
    [SerializeField] float wobbleScale;
    [SerializeField] int audioSamples;
    [SerializeField] AudioSource m_audi;
    Vector3 startingScale;
    private void Start()
    {
        startingScale = transform.localScale;
    }
    // Update is called once per frame
    void Update()
    {
        float[] samples = new float[audioSamples];
         
        m_audi.GetOutputData(samples, 1);

        float sampleAverage = 0;
        for (int i = 0; i < samples.Length; i++)
        {
            sampleAverage += samples[i];
        }
        sampleAverage /= samples.Length;

        Vector3 newScale = new Vector3(0, 0, sampleAverage * wobbleScale);

        transform.localScale = startingScale + newScale;
    }
}
