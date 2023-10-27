using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCannonValve : MonoBehaviour, IInteractable
{

    [SerializeField][Range(0, 5)] private int stage;
    public bool isInteractable { get; set; }

    public void OnInteract()
    {
        StartCoroutine(HoldingDownLever());
    }

    public void OnInteractEnd()
    {
        throw new System.NotImplementedException();
    }

    public void OnInteractHeld()
    {
        throw new System.NotImplementedException();
    }

    public void OnLookingAt()
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator HoldingDownLever()
    {
        while (Input.GetKey(KeyCode.Mouse0))
        {
            if (Input.GetAxis("Mouse X") < 0 && stage < 5)
            {
                stage++;
                //AudioManager.instance.PlaySFX(eSFX.leverPushPull, 0.2f);
                break;
            }
            else if (Input.GetAxis("Mouse X") > 0 && stage > 0)
            {
                stage--;
                //AudioManager.instance.PlaySFX(eSFX.leverPushPull, 0.2f);
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
