using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityManager : MonoBehaviour
{
    [SerializeField] private bool postDebugInformation;
    public delegate void ElectricityChange(float _change);
    public static event ElectricityChange ElectricityChangeEvent;


    public static List<ElectricityUser> ActiveUsers;

    private void Awake()
    {
        ActiveUsers = new List<ElectricityUser>();
    }
    private void Start()
    {
        StartCoroutine(OnTick());
    }

    private IEnumerator OnTick()
    {
        float changeThisTick = 0;
        for (int i = 0; i < ActiveUsers.Count; i++)
        {
            changeThisTick += -ActiveUsers[i].electricityUsedPerSecond;
            if (postDebugInformation) Debug.Log(ActiveUsers[i].name);
        }
        ChangeElectricityAmount(changeThisTick * Time.deltaTime);
        yield return new WaitForEndOfFrame();
        StartCoroutine(OnTick());
    }

    public static void ChangeElectricityAmount(float _change)
    {
        ElectricityChangeEvent?.Invoke(_change);
    }
}
