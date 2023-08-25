using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorLightBehavior : ElectricityUser
{
    [SerializeField] private Light[] lights;
    [SerializeField] private MeshRenderer immisives;
    [SerializeField] [ColorUsage(false, true)] private Color onEmission, offEmission;

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
    }

    public override void ToggleActiveState()
    {
        base.ToggleActiveState();
        UpdateMaterialColor();
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].enabled = isOn;
        }
    }


    private void UpdateMaterialColor()
    {
        if (isOn)
        {
            immisives.material.SetColor("_Emission_Color", onEmission);
        }
        else
        {
            immisives.material.SetColor("_Emission_Color", offEmission);
        }
    }

}
