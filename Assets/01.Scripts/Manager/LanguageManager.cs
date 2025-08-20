using System.Collections;
using UnityEngine;

using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;

public enum Language
{
    Chinese,
    English,
    Japanese,
}

public class LanguageManager : MonoBehaviour
{
    private TMP_Text uiText;
    private bool isChange;

    private void Awake()
    {
        uiText = this.TryGetComponent<TMP_Text>();

        if (uiText)
        {
            var local = GetComponent<LocalizeStringEvent>();
            local.OnUpdateString.AddListener(UpdateString);

            //local.StringReference = new LocalizedString()
            //{
            //    TableReference = "talbleName",
            //    TableEntryReference = "key",
            //};
        }
    }

    private void UpdateString(string _text)
    {
        uiText.text = _text;
    }

    public void SetLenguage(Language _lenguage)
    {
        if (isChange) return;
        else isChange = true;

        StartCoroutine(ChangeLenguage((int)_lenguage));
    }

    private IEnumerator ChangeLenguage(int _lenguageID)
    {
        yield return LocalizationSettings.InitializationOperation; //비동기 작업 완료 대기
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[(int)Language.Chinese];
        isChange = false;
    }
}
