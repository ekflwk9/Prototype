using System;
using System.Collections.Generic;
using UnityEngine;
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
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    private Dictionary<Type, UIBase> ui = new Dictionary<Type, UIBase>();
    private Dictionary<Type, string> uiPath = new Dictionary<Type, string>();

    private void Reset()
    {
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public T Add<T>() where T : UIBase
    {
        Type key = typeof(T);
        if (!ui.ContainsKey(key))
        {
            if (uiPath.TryGetValue(key, out string pathValue))
            {
                GameObject loadedUIObj = ResourcesManager.Instance.GetOnLoadedResource<GameObject>(pathValue);
                GameObject makedUIObj = MonoBehaviour.Instantiate(loadedUIObj);
                T makedUIBase = makedUIObj.GetComponent<T>();
                ui.Add(key, makedUIBase);
                return makedUIBase;
            }
            else
            {
                LogHelper.Log($"{key.Name} 로드가 안된거 보니 로드를 안한 것 같음 혹은 경로가 이상하거나 어쨋든 문제임");
                return null;
            }
        }
        else
        {
            LogHelper.Log($"{key.Name} 이미 추가 된 UI!");
            return ui[key].GetComponent<T>();
        }
    }

    public void Remove<T>() where T : UIBase
    {
        Type key = typeof(T);
        if (ui.ContainsKey(key))
        {
            ui.Remove(key);
        }
        else
        {
            LogHelper.Log($"{key.Name} 그런 거 엄슴");
        }
    }

    public T GetUI<T>() where T : UIBase
    {
        Type key = typeof(T);
        if (ui.ContainsKey(key))
        {
            return ui[key] as T;
        }
        else
        {
            return Add<T>();
        }
    }

    public void Open<T>() where T : UIBase
    {
        Type key = typeof(T);
        if (ui.ContainsKey(key))
        {
            ui[key].OnUI();
        }
        else
        {
            LogHelper.Log($"{key.Name}  추가 되지 않은 UI");
        }
    }

    public void Close<T>() where T : UIBase
    {
        Type key = typeof(T);
        if (ui.ContainsKey(key))
        {
            ui[key].OffUI();
        }
        else
        {
            LogHelper.Log($"{key.Name}  추가 되지 않은 UI");
        }
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