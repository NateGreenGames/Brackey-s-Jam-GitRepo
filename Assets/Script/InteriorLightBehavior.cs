using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorLightBehavior : ElectricityUser
{
    [SerializeField] private Light[] lights;
    [SerializeField] private MeshRenderer[] thingsToHideWhileLightIsOff;
    [SerializeField] private MeshRenderer immisives;
    [SerializeField] private MeshRenderer buttonBacking;
    [SerializeField] [ColorUsage(false, true)] private Color onEmission, offEmission;
    [SerializeField] private AudioSource lightAudio;


    private void OnEnable()
    {
        ButtonBehavior.buttonEvent += ToggleActiveState;
    }

    private void OnDisable()
    {
        ButtonBehavior.buttonEvent -= ToggleActiveState;
    }

    private void Start()
    {
        if (isOn)
        {
            bool onList = ElectricityManager.ActiveUsers.Contains(this);
            if (!onList && isOn)
            {
                ElectricityManager.ActiveUsers.Add(this);
            }
            else if (onList && !isOn)
            {
                ElectricityManager.ActiveUsers.Remove(this);
            }
        }
        UpdateMaterialColor();
        ToggleAudioSource();
    }


    public override void ToggleActiveState(int _info, bool _state)
    {
        if(_info == 0)
        {
            base.ToggleActiveState(_info, _state);

            ToggleAudioSource();
            UpdateMaterialColor();
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].enabled = isOn;
            }
        }
    }

    private void UpdateMaterialColor()
    {
        if (isOn)
        {
            immisives.material.SetColor("_Emission_Color", onEmission);
            for (int i = 0; i < thingsToHideWhileLightIsOff.Length; i++)
            {
                thingsToHideWhileLightIsOff[i].material.EnableKeyword("_EMISSION");
            }
        }
        else
        {
            immisives.material.SetColor("_Emission_Color", offEmission);
            for (int i = 0; i < thingsToHideWhileLightIsOff.Length; i++)
            {
                thingsToHideWhileLightIsOff[i].material.DisableKeyword("_EMISSION");
            }
        }


        if (!isOn && !FuseBox.instance.isOverloaded)
        {
            buttonBacking.material.EnableKeyword("_EMISSION");
        }
        else
        {
            buttonBacking.material.DisableKeyword("_EMISSION");
        }
    }

    public void ToggleAudioSource()
    {
        if (isOn)
        {
            lightAudio.Play();
        }
        else
        {
            lightAudio.Stop();
        }
    }
}
