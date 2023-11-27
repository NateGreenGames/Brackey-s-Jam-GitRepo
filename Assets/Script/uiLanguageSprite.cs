using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiLanguageSprite : MonoBehaviour
{
    public Sprite[] sprites;
    public Image images;
    public int languageIDX;
    private void Awake()
    {
        ChangeIndex();
    }

    public void OnEnable()
    {
        UILanguageController.languageChanged += ChangeIndex;
    }
    public void OnDisable()
    {
        UILanguageController.languageChanged -= ChangeIndex;
    }

    public void ChangeIndex()
    {
        switch (SubtitleController.language)
        {
            case eSubtitleLanguage.English:
                images.sprite = sprites[0];
                images.GetComponent<Image>().SetNativeSize();
                break;
            case eSubtitleLanguage.Spanish:
                images.sprite = sprites[1];
                images.GetComponent<Image>().SetNativeSize();
                break;
            default:
                Debug.Log("oof");
                break;
        }
    }
}
