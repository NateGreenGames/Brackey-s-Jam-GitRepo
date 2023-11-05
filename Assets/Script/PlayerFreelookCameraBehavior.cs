using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
public class PlayerFreelookCameraBehavior : MonoBehaviour
{
    // Variables
    [Header("Camera Movement Settings:")]
    [SerializeField] float mouseSensitivity = 2f;
    [Space]
    [SerializeField] float minVerticalAngle;
    [SerializeField] float maxVerticalAngle;
    [Space]
    [SerializeField] float minHorizontalAngle;
    [SerializeField] float maxHorizontalAngle;

    [Space]
    [Header("Interaction Settings:")]
    [SerializeField] float maxInteractionDistance;
    [SerializeField] LayerMask interactionMask;
    [SerializeField] HUDReticleController HUDReticle;

    [Space]
    [Header("Zoom Settings:")]
    [SerializeField] private float minFOV;
    [SerializeField] private float zoomSpeed;


    private float cameraVerticalRotation;
    private float cameraHorizontalRotation;
    private CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        // Lock and Hide the Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }


    void Update()
    {
        PerformCameraRotation();
        WhileLooking();
        Zoom();
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

    public void WhileLooking()
    {
        // Creates a ray at the center of the camera, shooting outwards
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo; // Variable to store our collision information
        if (Physics.Raycast(ray, out hitInfo, maxInteractionDistance, interactionMask))
        {
            if (hitInfo.collider.TryGetComponent<IInteractable>(out IInteractable interactable) && interactable.isInteractable)
            {
                HUDReticle.UpdateReticleState(true);
                interactable.OnLookingAt();
                if (Input.GetMouseButtonDown(0))
                {
                    interactable.OnInteract();
                }
                if (Input.GetMouseButton(0))
                {
                    interactable.OnInteractHeld();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    interactable.OnInteractEnd();
                }
            }
            else
            {
                HUDReticle.UpdateReticleState(false);
            }
        }
    }

    public void Zoom()
    {
        if (Input.GetMouseButton(1) && virtualCamera.m_Lens.FieldOfView > minFOV)
        {
            virtualCamera.m_Lens.FieldOfView -= zoomSpeed * Time.deltaTime;
        }
        else if(!Input.GetMouseButton(1) && virtualCamera.m_Lens.FieldOfView < 60)
        {
            virtualCamera.m_Lens.FieldOfView += zoomSpeed * Time.deltaTime;
        }else if(Input.GetMouseButton(1) && virtualCamera.m_Lens.FieldOfView < minFOV)
        {
            virtualCamera.m_Lens.FieldOfView = minFOV;
        }else if(!Input.GetMouseButton(1) && virtualCamera.m_Lens.FieldOfView > 60)
        {
            virtualCamera.m_Lens.FieldOfView = 60;
        }
    }
}
