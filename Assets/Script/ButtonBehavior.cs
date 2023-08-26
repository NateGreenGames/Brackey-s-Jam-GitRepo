using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour, IInteractable
{
    [SerializeField] private InteriorLightBehavior[] lights;
    [SerializeField] private ExteriorLightBehavior outsideLight;
    [SerializeField] private JerryBehavior jerry;
    [SerializeField] private Color onColor, offColor;
    [SerializeField] private int buttonIdx = 0;
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

    public void ToggleButtonState(int _idx)
    {
                        AudioManager.instance.PlaySFX(eSFX.buttonClick, .25f);
        if (FuseBox.fB.isOverloaded) return;
        switch (_idx)
        {
            case 0:
                foreach (InteriorLightBehavior item in lights)
                {
                    item.ToggleActiveState();
                }
                isOn = !isOn;
                UpdateMaterialColor();
                //am.PlaySFX(eSFX.buttonClick, .25f);
                break;
            case 1:
                outsideLight.ToggleActiveState();
                isOn = !isOn;
                break;
            case 3:
                jerry.ToggleActiveState();
                isOn = !isOn;
                break;
            default:
                break;
        }
        /*LightAmbienceBehavior lb = lightAudio.GetComponent<LightAmbienceBehavior>();
        foreach (InteriorLightBehavior item in lights)
        {
            item.ToggleActiveState();
        }
        isOn = !isOn;
        UpdateMaterialColor();
        lb.ToggleAudioSource();
        //am.PlaySFX(eSFX.buttonClick, .25f);
        AudioManager.instance.PlaySFX(eSFX.buttonClick, .25f);*/
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
        ToggleButtonState(buttonIdx);
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
