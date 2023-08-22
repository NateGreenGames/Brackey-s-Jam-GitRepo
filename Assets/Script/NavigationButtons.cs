using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationButtons : MonoBehaviour, IInteractable
{
    [Tooltip("Positive values produce a clockwise rotation, negative values produce a counter-clockwise rotation.")][SerializeField] float rateofChange;
    public bool isInteractable { get; set; }

    private void Start()
    {
        isInteractable = true;
    }

    public void OnInteractHeld()
    {
        ProgressionManager.AlterPlayerCourse(rateofChange * Time.deltaTime);
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
