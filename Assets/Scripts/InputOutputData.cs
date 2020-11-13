using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputOutputData<K, T> 
{
    public InputOutputData(K key, T value)
    {
        Key = key;
        Value = value;
    }


    public K Key { get; private set; }
    public T Value { get; private set; }
}
