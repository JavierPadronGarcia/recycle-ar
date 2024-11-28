using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Serialization<TKey, TValue>
{
    [SerializeField] private List<TKey> keys;
    [SerializeField] private List<TValue> values;

    public Serialization(Dictionary<TKey, TValue> dictionary)
    {
        keys = new List<TKey>(dictionary.Keys);
        values = new List<TValue>(dictionary.Values);
    }

    public Dictionary<TKey, TValue> ToDictionary()
    {
        Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
        for (int i = 0; i < keys.Count; i++)
        {
            dictionary.Add(keys[i], values[i]);
        }
        return dictionary;
    }
}
