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
        if (FuseBox.fB.isOverloaded)
        {
            audioSource.Stop();
            return;
        }
        if (!isActive)
        {
            isActive = true;
            ToggleActiveState();
        }
        StartCoroutine(HoldingButton());
    }
    public void OnLookingAt()
    {
        //Do nothing
    }
    public void OnInteract()
    {
        //am.PlaySFX(eSFX.buttonClick, .25f);
        AudioManager.instance.PlaySFX(eSFX.buttonClick, .25f);
    }

    public void OnInteractEnd()
    {
        if (FuseBox.fB.isOverloaded)
        {
            audioSource.Stop();
            return;
        }
        audioSource.Stop();
        isActive = false;
        ToggleActiveState();
    }

    public override void ToggleActiveState()
    {
        base.ToggleActiveState();
    }

    IEnumerator HoldingButton()
    {
        while (isActive)
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
            //If the the clip is playing, don't do anything.


            //if rateOfChange is left, pan left, if rate of change is right, pan right.
            if (rateofChange > 0)
            {
                audioSource.panStereo = -1f;
            }
            else
            {
                audioSource.panStereo = 1f;
            }
            break;
        }
    }
}
