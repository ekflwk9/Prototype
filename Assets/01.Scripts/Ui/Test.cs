using NaughtyAttributes;
using UnityEngine;

public class Test : MonoBehaviour
{
    [Button]
    private void Load()
    {
        TextManager.Load(ResourceStringHelper.StringMap);
    }

    [Button]
    private void Show()
    {
        LogHelper.Log(TextManager.GetText(1000));
    }

    [Button]
    private void ChangeEnglish()
    {
        TextManager.SetLanguage(Language.English);
    }

    [Button]
    private void ChangeJapanese()
    {
        TextManager.SetLanguage(Language.Japanese);
    }
}
