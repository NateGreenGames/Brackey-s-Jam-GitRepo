using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class OxygenGuageBehavior : MonoBehaviour
{

    [SerializeField] float emptyRotation, fullRotation;
    [SerializeField] Volume ppVolume;
    Vignette m_vign;
    public float oxygenPercentage;

    private AudioSource m_source;
    private bool dying = false;

    // Start is called before the first frame update
    void Start()
    {
        m_source = GetComponent<AudioSource>();
        oxygenPercentage = 100f;
        if(ppVolume) ppVolume.profile.TryGet(out m_vign);
    }

    private void OnEnable()
    {
        OxygenManagement.OxygenChangeEvent += OxygenChangeDetected;
    }

    private void OnDisable()
    {
        OxygenManagement.OxygenChangeEvent -= OxygenChangeDetected;
    }


    //On oxygen event invoked.
    private void OxygenChangeDetected(float _change)
    {
        RotateDialToNewValue(oxygenPercentage + _change);
        oxygenPercentage += _change;
        m_vign.intensity.value = Mathf.Lerp(0.9f, 0.417f, Mathf.InverseLerp(0f, 33f, oxygenPercentage));
        m_source.volume = Mathf.Lerp(0.1f, 0, Mathf.InverseLerp(0, 100, oxygenPercentage));
        m_source.pitch = Mathf.Lerp(2f, 1f, Mathf.InverseLerp(0, 100, oxygenPercentage));
        if (oxygenPercentage <= 0 && !dying)
        {
            dying = true;
            m_source.Stop();
            AudioManager.instance.PlaySFX(eSFX.suffocation, 1f);
            GameOverAndCompletionController.instance.EndGame("You suffocated.");
        }
    }


    private void RotateDialToNewValue(float _newOxygenaPercentage)
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, Mathf.Lerp(emptyRotation, fullRotation, Mathf.InverseLerp(0, 100, _newOxygenaPercentage)));
    }
}
