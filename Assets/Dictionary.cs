using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using static UnityEditor.Progress;

public class StoreDictionary
{
    //keys: array de enteros que guarda los IDs.
    // values: array de Item que guarda los ítems correspondientes.

    private int[] keys;
    private Item[] values;
    private int count;

    // Constructor que inicializa los arrays con un tamaño específico.

    public StoreDictionary(int capacity)
    {
        keys = new int[capacity];
        values = new Item[capacity];
        count = 0;
    }

    public void Add(int key, Item value)
    {
        keys[count] = key;
        values[count] = value;
        count++;
    }

    public Item Get(int key)
    {
        for (int i = 0; i < count; i++)
        {
            if (keys[i] == key) return values[i];
        }
        return null;
    }

    public int Count => count;

    public Item[] GetAllItems()
    {
        Item[] result = new Item[count];
        for (int i = 0; i < count; i++) result[i] = values[i];
        return result;
    }
}
