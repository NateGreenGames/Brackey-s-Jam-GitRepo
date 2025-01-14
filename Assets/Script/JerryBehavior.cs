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
    [SerializeField] ParticleSystem airOverflowParticles;


    private void OnEnable()
    {
        ButtonBehavior.buttonEvent += ChangeActiveState;
    }
    private void OnDisable()
    {
        ButtonBehavior.buttonEvent -= ChangeActiveState;
    }

    private void Start()
    {
        lightMaterial[1].EnableKeyword("_EMISSION");
        lightMaterial[0].DisableKeyword("_EMISSION");
    }
    public override void ChangeActiveState(int _info, bool _state)
    {
        if(_info == 2)
        {
            base.ChangeActiveState(_info, _state);

            isActive = _state;
            //Debug.Log("Jerry is Mad");
            StartCoroutine(KissJerry(jerryGivesOxygen));
        }
    }

    public override void OnOverload()
    {
        base.OnOverload();

        isActive = false;
        //Debug.Log("Jerry is Mad");
        StartCoroutine(KissJerry(jerryGivesOxygen));
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
                airOverflowParticles.Stop();
            }
            else if(!jerryOverflowSource.isPlaying && guageBehavior.oxygenPercentage >= 100)
            {
                jerryWorkingSource.Stop();
                jerryOverflowSource.Play();
                airOverflowParticles.Play();
            }


            yield return new WaitForEndOfFrame();
            if (guageBehavior.oxygenPercentage >= 100)
            {
                guageBehavior.oxygenPercentage = 100;
                //SOUND EFFECT?
            }
            OxygenManagement.ChangeOxygenAmount(jerryGivesOxygen * Time.deltaTime);
            //Debug.Log("Kissing Jerry");
        }
        jerryOverflowSource.Stop();
        jerryWorkingSource.Stop();
        airOverflowParticles.Stop();
        //Debug.Log("Not Kissing Jerry :(");
        yield return null;
    }
}
