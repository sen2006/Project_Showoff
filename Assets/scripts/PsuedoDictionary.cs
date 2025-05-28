using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PseudoDictionary<T, U> {
    // PSEUDODICTIONARY ENTRIES
    // & DICTIONARY CONVERSION

    [SerializeField] List<PseudoKeyValuePair<T, U>> entries;
    private Dictionary<T, U> actualDictionary = new();

    /// <summary>
    /// copy of Dictionary Count
    /// </summary>
    public int Count {
        get {
            actualDictionary = FromPseudoDictionaryToActualDictionary();
            return actualDictionary.Count;
        }
    }

    /// <summary>
    /// copy of Dictionary Indexer
    /// </summary>
    public U this[T index] {
        get {
            actualDictionary = FromPseudoDictionaryToActualDictionary();
            return actualDictionary[index];
        }
    }

    /// <summary>
    /// convert a Dictionary to a PseusoDictionary
    /// </summary>
    public List<PseudoKeyValuePair<T, U>> FromActualDictionaryToPseudoDictionary(Dictionary<T, U> actualDictionary) {
        List<PseudoKeyValuePair<T, U>> pseudoDictionary = new();

        foreach (KeyValuePair<T, U> pair in actualDictionary)
            pseudoDictionary.Add(new(pair.Key, pair.Value));

        return pseudoDictionary;
    }

    /// <summary>
    /// convert a Dictionary to a PseusoDictionary
    /// </summary>
    public List<PseudoKeyValuePair<T, U>> FromActualDictionaryToPseudoDictionary()
        => FromActualDictionaryToPseudoDictionary(actualDictionary);

    /// <summary>
    /// convert a PseusoDictionary to a Dictionary
    /// </summary>
    public static Dictionary<T, U> FromPseudoDictionaryToActualDictionary(List<PseudoKeyValuePair<T, U>> pseudoDictionary) {
        Dictionary<T, U> dictionary = new();

        foreach (PseudoKeyValuePair<T, U> entry in pseudoDictionary)
            dictionary.Add(entry.Key, entry.Value);

        return dictionary;
    }

    /// <summary>
    /// convert a PseusoDictionary to a Dictionary
    /// </summary>
    private Dictionary<T, U> FromPseudoDictionaryToActualDictionary()
        => FromPseudoDictionaryToActualDictionary(entries);

    /// <summary>
    /// copy of Dictionary Add
    /// </summary>
    public void Add(T key, U value) {
        actualDictionary = FromPseudoDictionaryToActualDictionary();
        actualDictionary.Add(key, value);
        entries = FromActualDictionaryToPseudoDictionary();
    }

    /// <summary>
    /// copy of Dictionary Remove
    /// </summary>
    public void Remove(T key) {
        actualDictionary = FromPseudoDictionaryToActualDictionary();
        actualDictionary.Remove(key);
        entries = FromActualDictionaryToPseudoDictionary();
    }

    /// <summary>
    /// copy of Dictionary Clear
    /// </summary>
    public void Clear() {
        actualDictionary.Clear();
        entries = new();
    }

    /// <summary>
    /// copy of Dictionary TryGetValue
    /// </summary>
    public U TryGetValue(T key) {
        actualDictionary = FromPseudoDictionaryToActualDictionary();
        actualDictionary.TryGetValue(key, out U value);
        return value;
    }

    /// <summary>
    /// copy of Dictionary ContainsKey
    /// </summary>
    public bool ContainsKey(T key) {
        actualDictionary = FromPseudoDictionaryToActualDictionary();
        return actualDictionary.ContainsKey(key);
    }
}

[System.Serializable]
public struct PseudoKeyValuePair<T, U> {
    [SerializeField] T key;
    [SerializeField] U value;

    public T Key { get => key; }
    public U Value { get => value; }

    public PseudoKeyValuePair(T key, U value) {
        this.key = key;
        this.value = value;
    }
}