using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionPanel : MonoBehaviour
{
    [SerializeField] float blinksPerSecond;
    [SerializeField] private bool postDebugInformation;
    [SerializeField] Color surfaceWaterColor, bottomWaterColor;
    [SerializeField] float fullTransitionHeight;

    private MeshRenderer m_mr;

    private void OnEnable()
    {
        if(m_mr == null) m_mr = GetComponent<MeshRenderer>();
        StartCoroutine(BlinkRoutine(true));
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
            AudioManager.instance.PlaySFX(eSFX.hatchOpen, 1f);
            GameOverAndCompletionController.instance.WinGame();
            if (postDebugInformation) Debug.Log($"I collided with: {collision.collider.name}. You've won!");
        }
        else if(collision.gameObject.CompareTag("Obstacle"))
        {
            //Trigger crash failstate.
            AudioManager.instance.PlaySFX(eSFX.crush, 1f);
            GameOverAndCompletionController.instance.EndGame("You crashed into the sea floor.");
            if (postDebugInformation) Debug.Log($"I collided with: {collision.collider.name}. You've sunk.");
        }
        else
        {
            if (postDebugInformation) Debug.Log("Ugh.");
        }
    }

    private void Update()
    {
        //Camera.main.backgroundColor = Color.Lerp(surfaceWaterColor, bottomWaterColor, Mathf.InverseLerp(-0.4f, fullTransitionHeight, transform.localPosition.y));
    }
}
