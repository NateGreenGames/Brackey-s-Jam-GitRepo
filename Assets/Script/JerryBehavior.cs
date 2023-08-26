using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JerryBehavior : ElectricityUser
{
    public float jerryGivesOxygen = 1.33f;
    public bool isActive;

    public override void ToggleActiveState()
    {
        isActive = !isActive;
        Debug.Log("Jerry is Mad");
        StartCoroutine(KissJerry(jerryGivesOxygen));
        base.ToggleActiveState();
    }

    IEnumerator KissJerry(float _jerryGivesThisMuch)
    {
        while (isActive == true)
        {
            yield return new WaitForEndOfFrame();
            OxygenManagement.ChangeOxygenAmount(jerryGivesOxygen * Time.deltaTime);
            Debug.Log("Kissing Jerry");
        }
        Debug.Log("Not Kissing Jerry :(");
        yield return null;
    }
}
