using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    public delegate void CourseChange(float _change);
    public static event CourseChange onCourseChangeEvent;
    public static event CourseChange onMovmentTick;

    public static void AlterPlayerCourse(float _ChangeInAngle)
    {
        onCourseChangeEvent?.Invoke(_ChangeInAngle);
    }

    public static void MoveSubmarine(float _movementSpeed)
    {
        onMovmentTick?.Invoke(_movementSpeed);
    }
}
