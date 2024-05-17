using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElectricityUI : MonoBehaviour
{
    public static float electricityPercentage;
    private Slider m_slider;

    private void Start()
    {
        m_slider = GetComponent<Slider>();
        m_slider.value = 100;
    }
    private void OnEnable()
    {
        ElectricityManager.ElectricityChangeEvent += ElectricityChangeDetected;
    }

    private void OnDisable()
    {
        ElectricityManager.ElectricityChangeEvent -= ElectricityChangeDetected;
    }

    private void Update()
    {
        electricityPercentage = m_slider.value;
    }

    //On oxygen event invoked.
    private void ElectricityChangeDetected(float _change)
    {
        if (m_slider)
        {
            float newValue = m_slider.value += _change;
            if (newValue <= 0)
            {
                DeathSequence();
            }
            else
            {
                //Set Oxygen Value to new value.
                m_slider.value = newValue;
            }
        }
    }


    private void DeathSequence()
    {
        FuseBox.instance.RunOutOfPower();
    }
}
