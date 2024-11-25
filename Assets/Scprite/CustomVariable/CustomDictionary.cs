using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

[Serializable]
public class CustomDictionary<T>
{
    public List<DicData<T>> list = new List<DicData<T>>();
    private Dictionary<string, T> dict = new Dictionary<string, T>();

    public Dictionary<string, T> GetData()
    {
        for(int i = 0; i < list.Count; i++)
        {
            dict.Add(list[i].Key, list[i].Action);
        }
        return dict;
    }

    public T Find(string key)
    {
        if(dict.Count != list.Count)
            GetData();

        if (dict.TryGetValue(key, out var value))
        {
            return value; // 키가 존재하면 값을 반환
        }

        return default;
    }
}

[Serializable]
public class DicData<T>
{
    public string Key;
    public T Action;
}
