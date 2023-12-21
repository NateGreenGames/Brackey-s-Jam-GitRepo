using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirValveSegment : MonoBehaviour, IInteractable
{
    [SerializeField] AirCannonValve effectedValve;
    [SerializeField] float alignmentThreshold;
    [SerializeField] Vector2 clockwiseVector;

    public bool isInteractable { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        isInteractable = true;
    }

    public void OnInteract()
    {
        StartCoroutine(InteractRoutine());
    }

    private IEnumerator InteractRoutine()
    {

        while (Input.GetMouseButton(0))
        {
            Vector2 mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            float movementAlignment = Vector2.Dot(clockwiseVector.normalized, mouseMovement.normalized);
            if(movementAlignment > alignmentThreshold)
            {
                effectedValve.StageUp();
                break;
            }else if(movementAlignment < -alignmentThreshold)
            {
                effectedValve.StageDown();
                break;
            }
            yield return null;
        }
    }
    public void OnInteractEnd()
    {
    }

    public void OnInteractHeld(Vector3 _contactPoint)
    {
    }

    public void OnLookingAt()
    {
    }

}
