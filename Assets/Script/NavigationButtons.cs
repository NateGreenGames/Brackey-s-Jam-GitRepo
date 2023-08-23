using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationButtons : MonoBehaviour, IInteractable
{
    [Tooltip("Positive values produce a clockwise rotation, negative values produce a counter-clockwise rotation.")][SerializeField] float rateofChange;
    [SerializeField] private GameObject audioSource;
    public bool isInteractable { get; set; }
    public bool isToggled = false;


    private void Start()
    {
        isInteractable = true;
    }
 
    public void OnInteractHeld()
    {
        //The Navigation still works in this state. Audio is glitchy and thus commented out for the time being.
        ThrusterAudioBehavior tb = audioSource.GetComponent<ThrusterAudioBehavior>();
        isToggled = !isToggled;
        if (isToggled)
        {
            ProgressionManager.AlterPlayerCourse(rateofChange * Time.deltaTime);
            //tb.ToggleAudioSource();
        }
        else
        {
            //tb.ToggleAudioSource();
        }
    }
    public void OnLookingAt()
    {
        //Do nothing
    }
    public void OnInteract()
    {
        //Do nothing
    }

}
