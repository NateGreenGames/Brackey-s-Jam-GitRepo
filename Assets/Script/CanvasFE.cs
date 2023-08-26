using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFE : MonoBehaviour
{
    [SerializeField] GameObject optionsMenu, creditsMenu;


    public void ChangeOptionMenuState(bool _newState)
    {
        optionsMenu.SetActive(_newState);
    }

    public void ChangeCreditsMenuState(bool _newState)
    {
        creditsMenu.SetActive(_newState);
        if(_newState == true)
        {
            creditsMenu.GetComponent<Animator>().SetTrigger("Start");
        }
    }
}
