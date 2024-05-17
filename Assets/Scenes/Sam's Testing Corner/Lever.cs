using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    public Animator anim;
    [SerializeField] FuseBox fuseBox;
    public MeshRenderer lightButtonBacklight;
    public bool isInteractable { get; set; }
    private void Start()
    {
        if (!fuseBox)
        {
            GameObject _fuseBox = GameObject.Find("TP_FuseBox");
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

    public void OnInteractHeld(Vector3 _contactPoint)
    {
        //NONE
    }

    public IEnumerator HoldingDownLever()
    {
        if (!FuseBox.instance.isOverloaded)
        {
            while (Input.GetKey(KeyCode.Mouse0))
            {
                if (Input.GetAxis("Mouse Y") < 0)
                {
                    anim.SetTrigger("LeverDown");
                    fuseBox.Overload();
                    break;
                }
                yield return new WaitForEndOfFrame();
            }
        }
        else if (FuseBox.instance.isOverloaded)
        {
            while (Input.GetKey(KeyCode.Mouse0))
            {
                if (Input.GetAxis("Mouse Y") > 0 && ElectricityUI.electricityPercentage > 0)
                {
                    anim.SetTrigger("LeverUp");
                    FuseBox.instance.isOverloaded = false;
                    lightButtonBacklight.material.EnableKeyword("_EMISSION");
                    AudioManager.instance.PlaySFX(eSFX.leverPushPull, 0.2f);
                    AudioManager.instance.PlaySFX(eSFX.powerOn, 0.5f);
                    break;
                }
                yield return new WaitForEndOfFrame();
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
