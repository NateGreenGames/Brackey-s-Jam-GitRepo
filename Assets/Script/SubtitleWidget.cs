using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitleWidget : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textObject;

    public void Init(string _text, float _duration)
    {
        StartCoroutine(SubtitleRoutine(_text, _duration));
    }

    private IEnumerator SubtitleRoutine(string _text, float _duration)
    {
        float elapsedTime = 0;
        textObject.text = _text;
        while(elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
