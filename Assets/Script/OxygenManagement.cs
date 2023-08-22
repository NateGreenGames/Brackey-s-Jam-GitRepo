using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenManagement : MonoBehaviour
{
    [SerializeField] private float oxygenUsedPerSecond;
    [SerializeField] private int ticksPerSecond;
    public delegate void OxygenChange(float _change);
    public static event OxygenChange OxygenChangeEvent;

    private void Start()
    {
        StartCoroutine(OnTick());
    }

    private IEnumerator OnTick()
    {
        ChangeOxygenAmount(-oxygenUsedPerSecond/ticksPerSecond);
        yield return new WaitForSeconds(1f/ticksPerSecond);
        StartCoroutine(OnTick());
    }

    public static void ChangeOxygenAmount(float _change)
    {
        OxygenChangeEvent?.Invoke(_change);
    }
}