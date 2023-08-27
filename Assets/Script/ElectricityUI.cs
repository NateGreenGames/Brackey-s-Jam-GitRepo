using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElectricityUI : MonoBehaviour
{
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


    //On oxygen event invoked.
    private void ElectricityChangeDetected(float _change)
    {
        if (m_slider)
        {
            float newValue = m_slider.value += _change;
            if (newValue <= 0)
            {
                StartCoroutine(DeathSequence());
            }
            else
            {
                //Set Oxygen Value to new value.
                m_slider.value = newValue;
            }
        }
    }


    private IEnumerator DeathSequence()
    {
        FuseBox.fB.Overload();
        yield return new WaitForSeconds(1f);
        MonsterManager.mM.Enrage();
        m_slider.value = 0;
    }
}
