using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockPlungerBehavior : ElectricityUser, IInteractable
{
    public delegate void emptyDelegate();
    public static event emptyDelegate onShock;

    public bool isInteractable { get; set; }
    public float timeToChargeInSeconds;
    public bool postDebugInformation;
    [SerializeField] MeshRenderer lightIndicator;
    [SerializeField] [ColorUsage(false, true)] Color onColor, offColor;

    private bool isCharged = false;
    private Animator m_Anim;
    private AudioSource m_Audi;


    void Start()
    {
        isInteractable = true;
        m_Anim = GetComponent<Animator>();
        m_Audi = GetComponent<AudioSource>();
    }
    public override void ChangeActiveState(bool _state)
    {
        base.ChangeActiveState(_state);

        m_Anim.SetTrigger("Toggle");
        AudioManager.instance.PlaySFX(eSFX.leverPushPull, 0.2f);
        if (isOn)
        {
            lightIndicator.material.SetColor("_EmissionColor", onColor);
        }
        else
        {
            lightIndicator.material.SetColor("_EmissionColor", offColor);
        }
    }

    public override void OnOverload()
    {
        base.OnOverload();
        m_Anim.SetTrigger("Toggle");
        ChangeActiveState(false);
        AudioManager.instance.PlaySFX(eSFX.leverPushPull, 0.2f);
        isCharged = false;
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

    private IEnumerator PlungerInputCheck()
    {
        while (Input.GetKey(KeyCode.Mouse0))
        {
            if (Input.GetAxis("Mouse Y") < 0 && !isOn)
            {
                ChangeActiveState(true);
                StartCoroutine(ChargeUpRoutine());
                break;
            }
            else if (Input.GetAxis("Mouse Y") > 0 && isOn)
            {
                ChangeActiveState(false);
                if (isCharged)
                {
                    onShock?.Invoke();
                    AudioManager.instance.PlaySFX(eSFX.plungerShock, 1f);
                    isCharged = false;
                }
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator ChargeUpRoutine()
    {
        //Start charging effects
        if (postDebugInformation) Debug.Log("Charge Started");
        m_Audi.Play();

        float chargeTimer = 0;
        //Runs every frame while charging.
        while (chargeTimer < timeToChargeInSeconds)
        {
            if(isOn == true)
            {
                chargeTimer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            else
            {
                if(postDebugInformation) Debug.Log("Charge Cancelled");
                m_Audi.Stop();
                break;
            }
        }

        //End charging effects
        if(postDebugInformation) Debug.Log("Charging Complete");
        isCharged = true;
        m_Audi.Stop();
        AudioManager.instance.PlaySFX(eSFX.plungerLockedIn, 1f);
    }

}

