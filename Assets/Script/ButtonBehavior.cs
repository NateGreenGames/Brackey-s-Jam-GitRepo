using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour, IInteractable
{
    [SerializeField] private InteriorLightBehavior[] lights;
    [SerializeField] private Color onColor, offColor;
    [SerializeField] private GameObject lightAudio;
    //[SerializeField] private AudioManager am;

    

    private MeshRenderer m_mr;
    

    [SerializeField] bool isOn;
    public bool isInteractable { get; set; }

    void Start()
    {
        isInteractable = true;
        m_mr = GetComponent<MeshRenderer>();
        UpdateMaterialColor();
    }

    public void ToggleButtonState()
    {
        LightAmbienceBehavior lb = lightAudio.GetComponent<LightAmbienceBehavior>();
        foreach (InteriorLightBehavior item in lights)
        {
            item.ToggleActiveState();
        }
        isOn = !isOn;
        UpdateMaterialColor();
        lb.ToggleAudioSource();
        //am.PlaySFX(eSFX.buttonClick, .25f);
        AudioManager.instance.PlaySFX(eSFX.buttonClick, .25f);
    }

    private void UpdateMaterialColor()
    {
        if (isOn)
        {
            m_mr.material.color = onColor;
        }
        else
        {
            m_mr.material.color = offColor;
        }
    }

    public void OnInteract()
    {
        ToggleButtonState();
    }

    public void OnLookingAt()
    {
        //Do nothing
    }

    public void OnInteractHeld()
    {
        //Do nothing
    }

    public void OnInteractEnd()
    {
        //Do nothing
    }
}
