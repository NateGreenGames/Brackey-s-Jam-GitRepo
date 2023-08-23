using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    Animator anim;
    Vector3 lastMouseCoordinate = Vector3.zero;
    public bool isInteractable { get; set; }
    private void Start()
    {
        isInteractable = true;
        anim = GetComponent<Animator>();
    }

    public void OnInteract()
    {
        Debug.Log("I'm interacting");
        /*Vector3 mouseDelta = Input.mousePosition - lastMouseCoordinate;
        lastMouseCoordinate = Input.mousePosition;

        if (Input.GetKey(KeyCode.Mouse0) && ((mouseDelta.y < 0)))
        {
            anim.SetTrigger("Down");
            isInteractable = false;
        }*/
        StartCoroutine(HoldingDownLever());
    }



    public void OnLookingAt()
    {
        //None
    }
    /*public void //OnMouseOver()
    {
        Vector3 mouseDelta = Input.mousePosition - lastMouseCoordinate;
        lastMouseCoordinate = Input.mousePosition;

        if (Input.GetKey(KeyCode.Mouse0) && ((mouseDelta.y < 0)))
        {
            anim.SetTrigger("Down");
        }
    }*/

    public void OnInteractHeld()
    {
        //NONE
    }

    public IEnumerator HoldingDownLever()
    {
        while (Input.GetKey(KeyCode.Mouse0))
        {
            bool buttonIsBeingHeld = true;
            yield return new WaitForSeconds(0.1f);
            Vector3 mouseDelta = Input.mousePosition - lastMouseCoordinate;
            lastMouseCoordinate = Input.mousePosition;
            if (buttonIsBeingHeld && ((mouseDelta.y < 0)))
            {
                anim.SetTrigger("Down");
                isInteractable = false;
            }
            else
            {
                buttonIsBeingHeld = false;
            }
        }
    }
}
