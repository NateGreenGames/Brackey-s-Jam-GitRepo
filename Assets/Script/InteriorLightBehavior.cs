using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorLightBehavior : ElectricityUser
{
    [SerializeField] private Light[] lights;

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
    }


    public void ToggleLight()
    {
        ToggleActiveState();
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].enabled = isOn;
        }
    }
}
