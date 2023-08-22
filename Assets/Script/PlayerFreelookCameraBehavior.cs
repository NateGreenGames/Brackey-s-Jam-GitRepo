using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreelookCameraBehavior : MonoBehaviour
{
    // Variables
    [SerializeField] float mouseSensitivity = 2f;
    [SerializeField] float minVerticalAngle, maxVerticalAngle;
    [SerializeField] float minHorizontalAngle, maxHorizontalAngle;
    private float cameraVerticalRotation;
    private float cameraHorizontalRotation;


    void Start()
    {
        // Lock and Hide the Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }


    void Update()
    {
        PerformCameraRotation();
    }

    private void PerformCameraRotation()
    {
        // Collect Mouse Input
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        //Work out rotations on each axis and combine them
        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, minVerticalAngle, maxVerticalAngle);
        cameraHorizontalRotation -= -inputX;
        cameraHorizontalRotation = Mathf.Clamp(cameraHorizontalRotation, minHorizontalAngle, maxHorizontalAngle);

        Vector3 concatinatedRotation = new Vector3(cameraVerticalRotation, cameraHorizontalRotation, 0);

        //Perform rotation
        transform.localEulerAngles = concatinatedRotation;
    }
}
