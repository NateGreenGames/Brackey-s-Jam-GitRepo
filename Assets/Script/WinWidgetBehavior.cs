using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinWidgetBehavior : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 0;
    }

    //Button Behaviors:

    public void ContinueToCredits()
    {
        LoadingManager.instance.ChangeScene("Credits", 3, 3);
        Time.timeScale = 1;
    }
}
