using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour, IInteractable
{
    [SerializeField] private int buttonIdx = 0;
    [SerializeField] private bool isRockerSwitch;

    public delegate void buttonDelegate(int _buttonIdx, bool _buttonState);
    public static event buttonDelegate buttonEvent;
    //[SerializeField] private AudioManager am;


    private Animator m_Anim;
    private MeshRenderer m_mr;
    

    [SerializeField] bool isOn;
    public bool isInteractable { get; set; }


    private void OnEnable()
    {
        FuseBox.OnOverload += OnOverloadReset;
    }

    private void OnDisable()
    {
        FuseBox.OnOverload -= OnOverloadReset;
    }
    void Start()
    {
        isInteractable = true;
        m_mr = GetComponent<MeshRenderer>();
        if (isRockerSwitch)
        {
            m_Anim = GetComponent<Animator>();
            m_Anim.SetBool("StartState", isOn);
        }
    }

    public void ToggleButtonState(int _idx)
    {
        AudioManager.instance.PlaySFX(eSFX.buttonClick, .25f);
        if (FuseBox.instance.isOverloaded) return;
        if (isRockerSwitch) m_Anim.SetTrigger("Flip");
        isOn = !isOn;
        buttonEvent?.Invoke(_idx, isOn);
    }

    public void OnOverloadReset()
    {
        AudioManager.instance.PlaySFX(eSFX.buttonClick, .25f);
        if (isRockerSwitch && isOn) m_Anim.SetTrigger("Flip");
        isOn = false;
    }
    public void OnInteract()
    {
        ToggleButtonState(buttonIdx);
    }

    public void OnLookingAt()
    {
        //Do nothing
    }

    public void OnInteractHeld(Vector3 _contactPoint)
    {
        //Do nothing
    }

    public void OnInteractEnd()
    {
        //Do nothing
    }
}
