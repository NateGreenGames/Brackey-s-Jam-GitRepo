using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JerryBehavior : ElectricityUser
{
    public float jerryGivesOxygen = 1.33f;
    public bool isActive;
    public Material[] lightMaterial;
    [SerializeField] AudioSource jerryWorkingSource, jerryOverflowSource;
    [SerializeField] OxygenGuageBehavior guageBehavior;

    private void Start()
    {
        if (!guageBehavior)
        {
            GameObject _guage = GameObject.Find("TP_OxygenGaugeArrow");
            if (_guage != null)
            {
                guageBehavior = _guage.GetComponent<OxygenGuageBehavior>();
            }
        }
    }
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
            if (!jerryWorkingSource.isPlaying && guageBehavior.oxygenPercentage < 100) 
            {
                jerryWorkingSource.Play();
                jerryOverflowSource.Stop();
            }
            else if(!jerryOverflowSource.isPlaying && guageBehavior.oxygenPercentage >= 100)
            {
                jerryWorkingSource.Stop();
                jerryOverflowSource.Play();
            }


            yield return new WaitForEndOfFrame();
            if (guageBehavior.oxygenPercentage >= 100)
            {
                guageBehavior.oxygenPercentage = 100;
                //SOUND EFFECT?
            }
            OxygenManagement.ChangeOxygenAmount(jerryGivesOxygen * Time.deltaTime);
            Debug.Log("Kissing Jerry");
        }
        jerryOverflowSource.Stop();
        jerryWorkingSource.Stop();
        Debug.Log("Not Kissing Jerry :(");
        yield return null;
    }
}
