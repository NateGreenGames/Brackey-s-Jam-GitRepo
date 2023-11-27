using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UILanguageController : MonoBehaviour
{
    public delegate void uiLanguageChange();
    public static event uiLanguageChange languageChanged;
    public void OnEnable()
    {
        languageChanged += OnLanguageChanged;
    }

    public void OnDisable()
    {
        languageChanged -= OnLanguageChanged;
    }

    public static void SwapLanguage()
    {
        languageChanged?.Invoke();
    }

    private void OnLanguageChanged()
    {
        switch (SubtitleController.language)
        {
            case eSubtitleLanguage.English:
                break;
            case eSubtitleLanguage.Spanish:
                break;
            default: Debug.Log("oof");
                break;
        }
    }
}
