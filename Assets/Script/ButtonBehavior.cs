using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour, IInteractable
{
    [SerializeField] private MeshRenderer immisives;
    [SerializeField][ColorUsage(false,true)] private Color onEmission, offEmission;
    [SerializeField] private InteriorLightBehavior[] lights;
    [SerializeField] private Color onColor, offColor;
    [SerializeField] private GameObject lightAudio;

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
            item.ToggleLight();
        }
        isOn = !isOn;
        UpdateMaterialColor();
        lb.ToggleAudioSource();
    }

    private void UpdateMaterialColor()
    {
        if (isOn)
        {
            m_mr.material.color = onColor;
            immisives.material.SetColor("_Emission_Color", onEmission);
        }
        else
        {
            m_mr.material.color = offColor;
            immisives.material.SetColor("_Emission_Color", offEmission);
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
        throw new System.NotImplementedException();
    }
}
