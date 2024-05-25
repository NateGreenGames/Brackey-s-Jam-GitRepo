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


        if(oxygenPercentage <= 10)
        {
            m_source.volume = 0.5f;
            m_source.pitch = 2f;
        }
        else if(oxygenPercentage <= 33)
        {
            m_source.volume = 0.3f;
            m_source.pitch = 1f;
        }
        else if(oxygenPercentage <= 50)
        {
            m_source.volume = 0.2f;
            m_source.pitch = 0.7f;
        }
        else
        {
            m_source.volume = 0f;
        }

        if (GameOverAndCompletionController.instance.isOver)
        {
            dying = true;
            m_source.Stop();
        }
        if (oxygenPercentage <= 0 && !dying)
        {
            dying = true;
            m_source.Stop();
            SteamManager.instance.UnlockAchievement(eAchievement.DieToOxygen);
            AudioManager.instance.PlaySFX(eSFX.suffocation, 1f);
            GameOverAndCompletionController.instance.EndGame("You suffocated.");
        }
    }


    private void RotateDialToNewValue(float _newOxygenaPercentage)
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, Mathf.Lerp(emptyRotation, fullRotation, Mathf.InverseLerp(0, 100, _newOxygenaPercentage)));
    }
}
