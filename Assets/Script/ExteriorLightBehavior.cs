using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExteriorLightBehavior : ElectricityUser
{
    [SerializeField] private Light outsideLight;

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


    public override void ToggleActiveState(int _info, bool _state)
    {
        if(_info == 1)
        {
            base.ToggleActiveState(_info, _state);

            outsideLight.enabled = !outsideLight.enabled;
            outsideLight.enabled = isOn;
            MonsterManager.mM.isBeingWardedOff = !MonsterManager.mM.isBeingWardedOff;
            if (!MonsterManager.mM.isBeingWardedOff)
            {
                return;
            }
            StartCoroutine(MonsterManager.mM.WardOffMonster());
        }
    }
}
