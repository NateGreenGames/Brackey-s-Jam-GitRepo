using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenUI : MonoBehaviour
{
    private Slider m_slider;

    private void Start()
    {
        m_slider = GetComponent<Slider>();
        m_slider.value = 100;
    }
    private void OnEnable()
    {
        OxygenManagement.OxygenChangeEvent += OxygenChangeDetected;
    }

    private void OnDisable()
    {
        OxygenManagement.OxygenChangeEvent -= OxygenChangeDetected;
    }


    //On oxygen event invoked.
    private void OxygenChangeDetected(float _change)
    {
        if (m_slider)
        {
            float newValue = m_slider.value += _change;
            if (newValue <= 0)
            {
                //Invoke depleted oxygen event and set the slider value to 0.
                m_slider.value = 0;
            }
            else
            {
                //Set Oxygen Value to new value.
                m_slider.value = newValue;
            }
        }
    }
}
