using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiLanguageSprite : MonoBehaviour
{
    [NamedArray(typeof(eSubtitleLanguage))] public Sprite[] sprites;
    private Image images;

    private void Awake()
    {
        images = gameObject.GetComponent<Image>();
    }

    private void Start()
    {
        ChangeIndex();
    }
    private void OnEnable()
    {
        UILanguageController.languageChanged += ChangeIndex;
        ChangeIndex();
    }
    private void OnDisable()
    {
        UILanguageController.languageChanged -= ChangeIndex;
    }

    private void ChangeIndex()
    {
        switch (SubtitleController.language)
        {
            case eSubtitleLanguage.English:
                images.sprite = sprites[0];
                images.SetNativeSize();
                break;
            case eSubtitleLanguage.Spanish:
                images.sprite = sprites[1];
                images.SetNativeSize();
                break;
            default:
                Debug.Log("oof");
                break;
        }
    }
}
