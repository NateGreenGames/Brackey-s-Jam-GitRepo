using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum eSubtitleLanguage {English, Spanish };
public class SubtitleController : MonoBehaviour
{
    public delegate void subtitleInformation(eSFX _sound, float _subtitleDuration);
    public static event subtitleInformation subtitleEvent;

    public static eSubtitleLanguage language;
    public static bool hasSubtitles;
    [NamedArray(typeof(eSFX))] public LocalizationInfo[] translations;
    [SerializeField] private GameObject subtitlePrefab;

    private void OnEnable()
    {
        subtitleEvent += SpawnSubtitle;
    }

    private void OnDisable()
    {
        subtitleEvent -= SpawnSubtitle;
    }
    public static void CreateNewSubtitle(eSFX _sound, float _subtitleDuration)
    {
        subtitleEvent?.Invoke(_sound, _subtitleDuration);
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
