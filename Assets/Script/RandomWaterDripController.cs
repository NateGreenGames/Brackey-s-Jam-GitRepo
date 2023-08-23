using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWaterDripController : MonoBehaviour
{
    [SerializeField] float minTimeBetweenDrips, maxTimeBetweenDrips;
    [SerializeField] AudioClip[] dripSounds;
    private AudioSource m_as;
    // Start is called before the first frame update
    void Start()
    {
        m_as = GetComponent<AudioSource>();
        StartCoroutine(DripRoutine());
    }

    private IEnumerator DripRoutine()
    {
        m_as.PlayOneShot(dripSounds[Random.Range(0, dripSounds.Length)]);
        Debug.Log("I'm trying to play a drip sound.");
        yield return new WaitForSeconds(Random.Range(minTimeBetweenDrips, maxTimeBetweenDrips));
        StartCoroutine(DripRoutine());
    }
}
