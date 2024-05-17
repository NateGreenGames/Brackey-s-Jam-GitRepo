using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDReticleController : MonoBehaviour
{
    private Animator m_anim;
    private bool isHovering;
    // Start is called before the first frame update
    void Start()
    {
        m_anim = GetComponent<Animator>();
        m_anim.SetBool("isHovering", false);
        isHovering = false;
    }

    public void UpdateReticleState(bool _newState)
    {
        if (_newState && !isHovering)
        {
            m_anim.SetBool("isHovering", _newState);
            isHovering = true;
        }
        else if(!_newState && isHovering)
        {
            m_anim.SetBool("isHovering", _newState);
            isHovering = false;
        }
    }
}
