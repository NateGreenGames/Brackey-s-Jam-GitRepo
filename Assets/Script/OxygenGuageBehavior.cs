using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenGuageBehavior : MonoBehaviour
{

    [SerializeField] float emptyRotation, fullRotation;
    [SerializeField] float oxygenPercentage;

    // Start is called before the first frame update
    void Start()
    {
        oxygenPercentage = 100f;
    }

    private void OnEnable()
    {
        OxygenManagement.OxygenChangeEvent += OxygenChangeDetected;
    }

    private void OnDisable()
    {
        OxygenManagement.OxygenChangeEvent -= OxygenChangeDetected;
    }


    //On oxygen event invoked.
    private void OxygenChangeDetected(float _change)
    {
        RotateDialToNewValue(oxygenPercentage + _change);
        oxygenPercentage += _change;
    }


    private void RotateDialToNewValue(float _newOxygenaPercentage)
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, Mathf.Lerp(emptyRotation, fullRotation, Mathf.InverseLerp(0, 100, _newOxygenaPercentage)));
    }
}
