using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExteriorLightBehavior : ElectricityUser
{
    [SerializeField] private Light[] outsideLights;

    private void OnEnable()
    {
        ButtonBehavior.buttonEvent += ChangeActiveState;
    }

    private void OnDisable()
    {
        ButtonBehavior.buttonEvent -= ChangeActiveState;
    }
    private void Start()
    {
        //isOn = false;
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


    public override void ChangeActiveState(int _info, bool _state)
    {
        if(_info == 1)
        {
            base.ChangeActiveState(_info, _state);

            foreach(Light light in outsideLights)
            {
                light.enabled = isOn;
            }
        }
    }


    public override void OnOverload()
    {
        base.OnOverload();

        foreach (Light light in outsideLights)
        {
            light.enabled = isOn;
        }

    }
}
