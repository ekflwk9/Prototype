using System.Collections.Generic;
using UnityEngine;

public enum Language
{
    English,
    Japanese,
    Chinese,
}

public static class LanguageManager
{
    private static Dictionary<int, string[]> talk = new();
    public static Language language { get; private set; }

    public static void Load(string _csvName)
    {
        //var file = ResourcesManager.Instance.GetOnLoadedResource<TextAsset>(_csvName);
        var csvFile = Resources.Load<TextAsset>(ResourceStringHelper.StringMap).text.Split('\n'); //열나눔

        for (int i = 1; i < csvFile.Length; i++)
        {
            if (!string.IsNullOrEmpty(csvFile[i]))
            {
                var colum = csvFile[i].Split(','); //행나눔
                var id = 0;

                if (!int.TryParse(colum[0], out id))
                {
                    LogHelper.Log($"{i}번 열에 숫자 ID가 아닌 글자가 들어가 있음");
                    return;
                }

                var length = colum.Length - 1;
                talk.Add(id, new string[length]);

                for (int I = 0; I < length;)
                {
                    talk[id][I] = colum[++I];
                }
            }
        }
    }

    /// <summary>
    /// 언어 변경
    /// </summary>
    /// <param name="_language"></param>
    public static void SetLanguage(Language _language)
    {
        language = _language;
    }

    /// <summary>
    /// 텍스트 정보 초기화
    /// </summary>
    public static void Reset()
    {
        talk.Clear();
    }

    /// <summary>
    /// 해당 ID에 해당하는 String을 반환
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public static string GetText(int _id)
    {
        if (talk.ContainsKey(_id)) return talk[_id][(int)language];
        return string.Empty;
    }
}
