using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityUser : MonoBehaviour
{
    public float electricityUsedPerSecond;
    public bool isOn = false;

    public virtual void ChangeActiveState(int _channel, bool _state)
    {
        isOn = _state;
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

    public virtual void ChangeActiveState(bool _state)
    {
        isOn = _state;
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

    public virtual void OnOverload()
    {
        isOn = false;
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
