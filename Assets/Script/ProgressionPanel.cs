using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionPanel : MonoBehaviour
{
    [SerializeField] private bool postDebugInformation;
    //[SerializeField] Color surfaceWaterColor, bottomWaterColor;
    //[SerializeField] float fullTransitionHeight;

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
            SteamManager.instance.UnlockAchievement(4);
            AudioManager.instance.PlaySFX(eSFX.hatchOpen, 1f);
            GameOverAndCompletionController.instance.WinGame();
            if (postDebugInformation) Debug.Log($"I collided with: {collision.collider.name}. You've won!");
        }
        else if(collision.gameObject.CompareTag("Obstacle"))
        {
            //Trigger crash failstate.
            SteamManager.instance.UnlockAchievement(3);
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
