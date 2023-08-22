using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour
{
    [SerializeField] private Color onColor, offColor;

    private Image m_buttonimage;
    private bool isOn;
    void Start()
    {
        m_buttonimage = GetComponent<Image>();
        ToggleButtonState();

    }

    public void ToggleButtonState()
    {
        isOn = !isOn;
        if (isOn)
        {
            m_buttonimage.color = onColor;
        }
        else
        {
            m_buttonimage.color = offColor;
        }
    }
}
