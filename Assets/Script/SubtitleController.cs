using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum eSubtitleLanguage {English, Spanish };
public class SubtitleController : MonoBehaviour
{
    public delegate void subtitleInformation(eSFX _sound, float _subtitleDuration);
    public delegate void stringSubtitle(string _text, float _duration);
    public static event stringSubtitle textSubtitleEvent;
    public static event subtitleInformation subtitleEvent;

    public static eSubtitleLanguage language;
    public static bool hasSubtitles;
    [NamedArray(typeof(eSFX))] public LocalizationInfo[] translations;
    [SerializeField] private GameObject subtitlePrefab;

    private void OnEnable()
    {
        subtitleEvent += SpawnSubtitle;
        textSubtitleEvent += SpawnSubtitle;
    }

    private void OnDisable()
    {
        subtitleEvent -= SpawnSubtitle;
        textSubtitleEvent -= SpawnSubtitle;
    }
    public static void CreateNewSubtitle(eSFX _sound, float _subtitleDuration)
    {
        subtitleEvent?.Invoke(_sound, _subtitleDuration);
    }
    public static void CreateNewSubtitle(string _text, float _subtitleDuration)
    {
        textSubtitleEvent?.Invoke(_text, _subtitleDuration);
    }


    private void SpawnSubtitle(string _text, float _subtitleDuration)
    {
        if (!hasSubtitles) return;

        if (_text == "")
        {
            Debug.Log($"No string passed into spawn subtitle, aborting...");
            return;
        }

        SubtitleWidget newWidget = Instantiate(subtitlePrefab, this.transform).GetComponent<SubtitleWidget>();
        newWidget.Init(_text, _subtitleDuration);
    }
    private void SpawnSubtitle(eSFX _sound, float _subtitleDuration)
    {
        if (!hasSubtitles) return;
        string text = "";
        switch (language)
        {
            case eSubtitleLanguage.English:
                text = translations[(int)_sound].englishTranslation;
                break;
            case eSubtitleLanguage.Spanish:
                text = translations[(int)_sound].spanishTranslation;
                break;
        };

        if(text == "")
        {
            Debug.Log($"No translation found for {_sound}. Please add one for the selected language within the subtitle controller.");
        }

        SubtitleWidget newWidget = Instantiate(subtitlePrefab, this.transform).GetComponent<SubtitleWidget>();
        newWidget.Init(text, _subtitleDuration);
    }
}

[System.Serializable]
public class LocalizationInfo
{
    public string englishTranslation;
    public string spanishTranslation;
}
