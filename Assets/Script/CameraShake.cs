using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public delegate void CameraDel(float _intensity, float _Length);
    public static event CameraDel CameraShakeEvent;

    private void OnEnable()
    {
        CameraShakeEvent += StartCoroutine;
    }

    private void OnDisable()
    {
        CameraShakeEvent -= StartCoroutine;
    }

    public static void StartScreenShake(float _intensity, float _Length)
    {
        CameraShakeEvent.Invoke(_intensity, _Length);
    }


    private void StartCoroutine(float _intensity, float _length)
    {
        StartCoroutine(ShakeRoutine(_intensity, _length));
    }
    private IEnumerator ShakeRoutine(float _intensity, float _length)
    {
        float elapsedTime = 0;
        Vector3 totalMovement = Vector3.zero;

        while(elapsedTime < _length)
        {
            Vector3 randomPositon = Random.onUnitSphere * _intensity;
            transform.localPosition += randomPositon;
            totalMovement += randomPositon;

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.position -= totalMovement;
    }
}
