using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityUser : MonoBehaviour
{
    public float electricityUsedPerSecond;
    public bool isOn = false;

    public virtual void ToggleActiveState()
    {
        isOn = !isOn;
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

    public void SetActiveState(bool _newState)
    {
        isOn = _newState;
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
