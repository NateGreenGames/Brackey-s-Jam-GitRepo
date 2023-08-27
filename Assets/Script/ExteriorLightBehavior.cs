using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExteriorLightBehavior : ElectricityUser
{
    [SerializeField] private Light outsideLight;

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


    public override void ToggleActiveState()
    {       
        outsideLight.enabled = !outsideLight.enabled;
        base.ToggleActiveState();
        outsideLight.enabled = isOn;
        MonsterManager.mM.isBeingWardedOff = !MonsterManager.mM.isBeingWardedOff;
        if (!MonsterManager.mM.isBeingWardedOff)
        {
            return;
        }
        StartCoroutine(MonsterManager.mM.WardOffMonster());
    }
}
