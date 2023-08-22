using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    public delegate void CourseChange(float _change);
    public static event CourseChange onCourseChangeEvent;
    public static event CourseChange onMovmentTick;


    [SerializeField] float movementSpeed;
    [SerializeField] private int ticksPerSecond;

    void Start()
    {
        StartCoroutine(OnTick());
    }

    private IEnumerator OnTick()
    {
        onMovmentTick?.Invoke(movementSpeed / ticksPerSecond);
        yield return new WaitForSeconds(1f / ticksPerSecond);
        StartCoroutine(OnTick());
    }
    public static void AlterPlayerCourse(float _ChangeInAngle)
    {
        onCourseChangeEvent?.Invoke(_ChangeInAngle);
    }
}
