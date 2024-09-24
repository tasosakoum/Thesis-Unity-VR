using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class SerializableKeyValuePair<TKey, TValue> {
    public TKey key;
    public TValue value;

    public SerializableKeyValuePair(TKey key, TValue value) {
        this.key = key;
        this.value = value;
    }
}

[Serializable]
public class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver {
    [SerializeField] private List<SerializableKeyValuePair<TKey, TValue>> list = new List<SerializableKeyValuePair<TKey, TValue>>();

    public void Add(TKey key, TValue value) {
        list.Add(new SerializableKeyValuePair<TKey, TValue>(key, value));
    }

    public bool TryGetValue(TKey key, out TValue value) {
        var pair = list.FirstOrDefault(p => EqualityComparer<TKey>.Default.Equals(p.key, key));
        if (pair != null) {
            value = pair.value;
            return true;
        }

        value = default;
        return false;
    }

    public List<TKey> Keys() {
        return list.Select(pair => pair.key).ToList();
    }
    
    public List<TValue> Values() {
        return list.Select(pair => pair.value).ToList();
    }

    public List<SerializableKeyValuePair<TKey, TValue>> Pairs() {
        return list;
    }

    public void OnBeforeSerialize() {
    }

    public void OnAfterDeserialize() {
    }
    
    public List<SerializableKeyValuePair<TKey, TValue>>.Enumerator GetEnumerator() {
        return list.GetEnumerator();
    }

    public List<SerializableKeyValuePair<TKey, TValue>> GetList() {
        return list;
    }
}

