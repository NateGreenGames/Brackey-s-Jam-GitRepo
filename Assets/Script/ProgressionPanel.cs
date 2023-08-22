using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionPanel : MonoBehaviour
{
    public float movementSpeed;
    [SerializeField] private GameObject playerPositionObject, playerDirectionIndicatorObject, destinationObject;

    void Update()
    {
        playerPositionObject.transform.position = playerPositionObject.transform.position + (-playerPositionObject.transform.forward.normalized * movementSpeed * Time.deltaTime);
    }
}
