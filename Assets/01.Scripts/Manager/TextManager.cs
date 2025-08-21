using System.Collections.Generic;
using UnityEngine;

public enum Language
{
    English,
    Japanese,
    Chinese,
}

public enum ColorName
{
    red,
    green,
    blue,
    cyan,
    yellow,
    orange,
    purple,
    magenta
}

public static class TextManager
{
    private static Dictionary<int, string[]> textData = new();
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
                textData.Add(id, new string[length]);

                for (int I = 0; I < length;)
                {
                    textData[id][I] = colum[++I];
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
        textData.Clear();
    }

    /// <summary>
    /// 해당 ID에 해당하는 String을 반환
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public static string GetText(int _id)
    {
        if (textData.ContainsKey(_id)) return textData[_id][(int)language];
        return string.Empty;
    }

    private static string GetColor(ColorName colorName)
    {
        switch (colorName)
        {
            case ColorName.red:
                return "#ff0000"; // 빨강
            case ColorName.green:
                return "#00ff00"; // 초록
            case ColorName.blue:
                return "#0000ff"; // 파랑
            case ColorName.cyan:
                return "#00ffff"; // 청록
            case ColorName.yellow:
                return "#ffd700"; // 노랑 (골드 느낌)
            case ColorName.orange:
                return "#ff8c00"; // 주황 (다크 오렌지)
            case ColorName.purple:
                return "#800080"; // 보라
            case ColorName.magenta:
                return "#ff00ff"; // 마젠타
            default:
                return "#000000"; // 기본값: 검정
        }
    }

    /// <summary>
    /// string에 색 입혀서 반환해주는 매서드입니다
    /// </summary>
    /// <param name="text">출력 할 글자 써주시면 됩니다.</param>
    /// <param name="colorName">색깔 골라주시면 됩니다.</param>
    /// 혹시 사용하시거나 색 추가 하시려면 ColorName 열거형에 색상이름 추가해주시고
    /// GetColor 함수에 Case추가하셔서 색상이름이 기본 제공 색일 때는 똑같이 열거형 스트링형변환 해주시면 되고
    /// 기본 제공색상에 없는 색이면, 칼라코드 검색하셔서 해당 코드 스트링으로 반환하게 추가 해주시면 됩니다.
    /// <returns></returns>
    public static string ColorText(string text, ColorName colorName)
    {
        return $"<color={GetColor(colorName)}>{text}</color>";
    }
}
