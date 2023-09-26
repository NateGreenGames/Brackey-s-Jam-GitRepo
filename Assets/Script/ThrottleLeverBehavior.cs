using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrottleLevelBehavior : ElectricityUser, IInteractable
{
    public bool isInteractable { get; set; }
    [SerializeField] float movementSpeed;
    [SerializeField] AudioSource engineRunningSource;
    [SerializeField] float leverSensitivity;
    private Animator m_anim;
    void Start()
    {
        isInteractable = true;
        m_anim = GetComponent<Animator>();
        SetActiveState(false);
    }
    void Update()
    {
        if (isOn)
        {
            ProgressionManager.MoveSubmarine(movementSpeed * Time.deltaTime);
        }

        if (isOn && !engineRunningSource.isPlaying)
        {
            engineRunningSource.Play();
        }
        else if(!isOn)
        {
            engineRunningSource.Stop();
        }
    }
    public void OnInteract()
    {
        if (FuseBox.fB.isOverloaded) return;
        lastMouseCoordinate = Input.mousePosition;
        StartCoroutine(HoldingDownLever());
    }

    public void OnInteractEnd()
    {
    }

    public void OnInteractHeld()
    {
    }

    public void OnLookingAt()
    {
    }

    public override void ToggleActiveState()
    {
        base.ToggleActiveState();
        AudioManager.instance.PlaySFX(eSFX.leverPushPull, 0.2f);
        m_anim.SetTrigger("Up");
    }
    public IEnumerator HoldingDownLever()
    {
        while (Input.GetKey(KeyCode.Mouse0))
        {
            if (Input.GetAxis("Mouse Y") < 0 && !isOn)
            {
                m_anim.SetTrigger("Down");
                SetActiveState(true);
                AudioManager.instance.PlaySFX(eSFX.leverPushPull, 0.2f);
                break;
            }else if (Input.GetAxis("Mouse Y") > 0 && !isOn)
            {
                m_anim.SetTrigger("Bump");
                AudioManager.instance.PlaySFX(eSFX.leverBump, 1f);
                break;
            }
            else if (Input.GetAxis("Mouse Y") > 0 && isOn)
            {
                m_anim.SetTrigger("Up");
                SetActiveState(false);
                AudioManager.instance.PlaySFX(eSFX.leverPushPull, 0.2f);
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
