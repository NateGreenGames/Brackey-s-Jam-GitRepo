using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrottleLevelBehavior : ElectricityUser, IInteractable
{
    public bool isInteractable { get; set; }
    [SerializeField] float movementSpeed;
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
    }
    public void OnInteract()
    {
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


    public IEnumerator HoldingDownLever()
    {
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
                break;
            }else if (buttonIsBeingHeld && mouseDelta.y > 0 && !isOn)
            {
                m_anim.SetTrigger("Bump");
                break;
            }
            else if (buttonIsBeingHeld && mouseDelta.y > 0 && isOn)
            {
                m_anim.SetTrigger("Up");
                SetActiveState(false);
                break;
            }
            else
            {
                buttonIsBeingHeld = false;
            }
        }
    }
}
