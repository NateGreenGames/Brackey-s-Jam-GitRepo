using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationButtons : ElectricityUser, IInteractable
{
    [Tooltip("Positive values produce a clockwise rotation, negative values produce a counter-clockwise rotation.")][SerializeField] float rateofChange;
    [SerializeField] private AudioSource audioSource;
    public bool isActive;
    //[SerializeField] private AudioManager am;


    public bool isInteractable { get; set; }


    private void Start()
    {
        isInteractable = true;
    }
 
    public void OnInteractHeld()
    {
        //Do Nothing;
    }
    public void OnLookingAt()
    {
        //Do nothing
    }
    public void OnInteract()
    {
        //am.PlaySFX(eSFX.buttonClick, .25f);
        StartCoroutine(HoldingButton());
    }

    public void OnInteractEnd()
    {
        //Do nothing;
    }

    public override void ToggleActiveState()
    {
        base.ToggleActiveState();
        isActive = !isActive;
        AudioManager.instance.PlaySFX(eSFX.buttonClick, .25f);
        if (isActive)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }

        if (rateofChange > 0)
        {
            audioSource.panStereo = -1f;
        }
        else
        {
            audioSource.panStereo = 1f;
        }
    }

    IEnumerator HoldingButton()
    {
        ToggleActiveState();
        while (Input.GetMouseButton(0) && isActive)
        {
            yield return new WaitForEndOfFrame();
            if (FuseBox.fB.isOverloaded)
            {
                audioSource.Stop();
                isActive = false;
                yield break;
            }
            //The Navigation still works in this state. Audio is glitchy and thus commented out for the time being.
            ProgressionManager.AlterPlayerCourse(rateofChange * Time.deltaTime);

            if (!audioSource.isPlaying)
            {
                //If the clip is not currently playing but this is being called, start it.
                audioSource.Play();
            }
        }
        ToggleActiveState();
    }
}
