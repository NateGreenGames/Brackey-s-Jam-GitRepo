using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JerryBehavior : ElectricityUser
{
    public float jerryGivesOxygen = 1.33f;
    public bool isActive;
    public Material[] lightMaterial;

    public override void ToggleActiveState()
    {
        isActive = !isActive;
        Debug.Log("Jerry is Mad");
        StartCoroutine(KissJerry(jerryGivesOxygen));
        base.ToggleActiveState();
    }

    IEnumerator KissJerry(float _jerryGivesThisMuch)
    {
        if (isActive)
        {
            lightMaterial[0].EnableKeyword("_EMISSION");
            lightMaterial[1].DisableKeyword("_EMISSION");
        }
        else
        {
            lightMaterial[1].EnableKeyword("_EMISSION");
            lightMaterial[0].DisableKeyword("_EMISSION");
        }
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
