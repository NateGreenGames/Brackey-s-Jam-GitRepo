using UnityEngine;

public class PointCameraMovementBehavior : MonoBehaviour
{
    public int cameraIdx;
    public GameObject[] virtualCamera;

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            virtualCamera[0].gameObject.SetActive(false);
            virtualCamera[1].gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            virtualCamera[1].gameObject.SetActive(false);
            virtualCamera[0].gameObject.SetActive(true);
        }
    }*/

    public void ChangeViewPointRight()
    {
        virtualCamera[cameraIdx].gameObject.SetActive(false);
        cameraIdx -= 1;
        if (cameraIdx < 0)
        {
            cameraIdx = 3;
        }
        virtualCamera[cameraIdx].gameObject.SetActive(true);
    }

    public void ChangeViewPointLeft()
    {
        virtualCamera[cameraIdx].gameObject.SetActive(false);
        cameraIdx += 1;
        if (cameraIdx > 3)
        {
            cameraIdx = 0;
        }
        virtualCamera[cameraIdx].gameObject.SetActive(true);
    }
}
