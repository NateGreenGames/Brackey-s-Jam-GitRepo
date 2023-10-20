using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockPlungerBehavior : ElectricityUser, IInteractable
{
    public delegate void emptyDelegate();
    public static event emptyDelegate onShock;

    public bool isInteractable { get; set; }
    public float timeToChargeInSeconds;

    private float chargeTimer;
    private bool isCharged = false;
    private Animator m_Anim;
    private ParticleSystem m_PS;


    void Start()
    {
        isInteractable = true;
        m_Anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!isCharged && isOn)
        {

        }
    }

    public override void ToggleActiveState(int _info, bool _state)
    {
        base.ToggleActiveState(_info, _state);


    }
    public void OnInteract()
    {
        if (FuseBox.instance.isOverloaded) return;
        StartCoroutine(PlungerInputCheck());
    }

    public void OnInteractEnd()
    {
        //Do nothing
    }

    public void OnInteractHeld()
    {
        //Do nothing
    }

    public void OnLookingAt()
    {
        //Do nothing
    }

    public IEnumerator PlungerInputCheck()
    {
        while (Input.GetKey(KeyCode.Mouse0))
        {
            if (Input.GetAxis("Mouse Y") < 0 && !isOn)
            {
                m_Anim.SetTrigger("Toggle");
                SetActiveState(true);
                AudioManager.instance.PlaySFX(eSFX.leverPushPull, 0.2f);
                break;
            }else if (Input.GetAxis("Mouse Y") > 0 && isOn)
            {
                m_Anim.SetTrigger("Toggle");
                SetActiveState(false);
                AudioManager.instance.PlaySFX(eSFX.leverPushPull, 0.2f);
                if (isCharged) onShock.Invoke();
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}

