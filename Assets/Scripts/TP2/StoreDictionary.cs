using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using static UnityEditor.Progress;

public class StoreDictionary<TKey, TValue>
{
    //keys: array de enteros que guarda los IDs.
    // values: array de Item que guarda los ítems correspondientes.

    private TKey[] keys;
    private TValue[] values;
    private int count;

    // Constructor que inicializa los arrays con un tamaño específico.

    public StoreDictionary(int capacity)
    {
        keys = new TKey[capacity];
        values = new TValue[capacity];
        count = 0;
    }

    public void Add(TKey key, TValue value)
    {
        keys[count] = key;
        values[count] = value;
        count++;
    }

    public TValue Get(TKey key)
    {
        for (int i = 0; i < count; i++)
        {
            if (keys[i].Equals(key)) return values[i];
        }
        return default;
    }

    public int Count => count;

    public TValue[] GetAllItems()
    {
        TValue[] result = new TValue[count];
        for (int i = 0; i < count; i++) result[i] = values[i];
        return result;
    }
}
