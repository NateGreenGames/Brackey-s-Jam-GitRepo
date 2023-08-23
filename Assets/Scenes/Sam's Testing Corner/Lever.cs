using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    public Animator anim;
    [SerializeField] FuseBox fuseBox;
    Vector3 lastMouseCoordinate = Vector3.zero;
    public bool isInteractable { get; set; }
    private void Start()
    {
        if (!fuseBox)
        {
            GameObject _fuseBox = GameObject.Find("BoxFuse_GD");
            if (_fuseBox != null)
            {
                fuseBox = _fuseBox.GetComponent<FuseBox>();
            }
        }
        isInteractable = true;
        anim = GetComponent<Animator>();
    }

    public void OnInteract()
    {
        StartCoroutine(HoldingDownLever());
    }

    public void OnLookingAt()
    {
        //None
    }

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
                fuseBox.submarineLight.SetActive(true);
                for (int i = fuseBox.tests.Count - 1; i >= 0; i--)
                {
                    fuseBox.lightMaterial[i].DisableKeyword("_EMISSION");
                }
                for (int i = fuseBox.tests.Count - 1; i >= 0; i--)
                {
                    fuseBox.tests.RemoveAt(i);
                }               
            }
            else
            {
                buttonIsBeingHeld = false;
            }
        }
    }

    public void OnInteractEnd()
    {
        //do nothing
    }
    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetTrigger("Up");
            isInteractable = true;
        }
    }*/
}
