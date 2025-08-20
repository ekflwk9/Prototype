using System;
using System.Collections.Generic;
using UnityEngine;

public class UiManger : MonoBehaviour
{
    public static UiManger instance;
    private Dictionary<Type, UiBase> ui = new();

    //public T Get<T>() where T : UiBase
    //{
    //    var key = typeof(T);
    //    if(ui.ContainsKey(key)) return ui as T;
    //}
}
