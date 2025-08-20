using NaughtyAttributes;
using UnityEngine;

public class Test : MonoBehaviour
{
    [Button]
    private void Load()
    {
        LanguageManager.Load(ResourceStringHelper.StringMap);
    }

    [Button]
    private void Show()
    {
        LogHelper.Log(LanguageManager.GetText(1000));
    }

    [Button]
    private void ChangeEnglish()
    {
        LanguageManager.SetLanguage(Language.English);
    }

    [Button]
    private void ChangeJapanese()
    {
        LanguageManager.SetLanguage(Language.Japanese);
    }
}
