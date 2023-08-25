using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionPanel : MonoBehaviour
{
    [SerializeField] float blinksPerSecond;
    [SerializeField] private bool postDebugInformation;

    private MeshRenderer m_mr;

    private void Start()
    {
        m_mr = GetComponent<MeshRenderer>();
        StartCoroutine(BlinkRoutine(true));
    }
    private void OnEnable()
    {
        ProgressionManager.onCourseChangeEvent += Rotate;
        ProgressionManager.onMovmentTick += Movement;
    }

    private void OnDisable()
    {
        ProgressionManager.onCourseChangeEvent -= Rotate;
        ProgressionManager.onMovmentTick -= Movement;
    }

    private IEnumerator BlinkRoutine(bool _currentState)
    {
        m_mr.enabled = _currentState;
        yield return new WaitForSeconds(1f / blinksPerSecond);
        StartCoroutine(BlinkRoutine(!_currentState));
    }
    void Movement(float _magnitudeOfMovement)
    {
        transform.position = transform.position + (-transform.up.normalized * _magnitudeOfMovement);
    }
    void Rotate(float _differenceOfRotation)
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + _differenceOfRotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            //Start end sequence.
            if (postDebugInformation) Debug.Log($"I collided with: {collision.collider.name}. You've won!");
        }
        else if(collision.gameObject.CompareTag("Obstacle"))
        {
            //Trigger crash failstate.
            if (postDebugInformation) Debug.Log($"I collided with: {collision.collider.name}. You've sunk.");
        }
        else
        {
            if (postDebugInformation) Debug.Log("Ugh.");
        }
    }
}
