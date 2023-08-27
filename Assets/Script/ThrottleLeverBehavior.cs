using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrottleLevelBehavior : ElectricityUser, IInteractable
{
    public bool isInteractable { get; set; }
    [SerializeField] float movementSpeed;
    [SerializeField] AudioSource engineRunningSource;
    [SerializeField] float leverSensitivity;
    private Vector3 lastMouseCoordinate;
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
        lastMouseCoordinate = Input.mousePosition;
        while (Input.GetKey(KeyCode.Mouse0))
        {
            bool buttonIsBeingHeld = true;
            yield return new WaitForEndOfFrame();
            Vector3 mouseDelta = Input.mousePosition - lastMouseCoordinate;
            lastMouseCoordinate = Input.mousePosition;
            if (buttonIsBeingHeld && mouseDelta.y < 0 && !isOn)
            {
                m_anim.SetTrigger("Down");
                SetActiveState(true);
                AudioManager.instance.PlaySFX(eSFX.leverPushPull, 0.2f);
                break;
            }else if (buttonIsBeingHeld && mouseDelta.y > 0 && !isOn)
            {
                m_anim.SetTrigger("Bump");
                AudioManager.instance.PlaySFX(eSFX.leverBump, 1f);
                break;
            }
            else if (buttonIsBeingHeld && mouseDelta.y > 0 && isOn)
            {
                m_anim.SetTrigger("Up");
                SetActiveState(false);
                AudioManager.instance.PlaySFX(eSFX.leverPushPull, 0.2f);
                break;
            }
            else
            {
                buttonIsBeingHeld = false;
            }
        }
    }
}
