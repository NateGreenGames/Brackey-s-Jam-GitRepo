using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorLightBehavior : ElectricityUser
{
    private Light m_light;

    private void Start()
    {
        m_light = GetComponent<Light>();

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
    }


    public void ToggleLight()
    {
        ToggleActiveState();
        m_light.enabled = isOn;
    }
}
