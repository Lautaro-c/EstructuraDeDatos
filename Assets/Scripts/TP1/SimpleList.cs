using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEditor.Progress;
using static UnityEngine.UI.Image;

public class SimpleList<T> : ISimpleList<T>, IIndexableList<T>
{
    T[] genericArray;
    const int defaultSize = 5;
    private int count = 0;
    public T this[int index]
    {
        get
        {
            return genericArray[index];
        }
        set
        {
            genericArray[index] = value;
        }
    }

    public int Count
    {
        get
        {
            return count;
        }
    }

    public SimpleList()
    {
        genericArray = new T[defaultSize];
    }

    public SimpleList(int arraySize)
    {
        genericArray = new T[arraySize];
    }

    public void Add(T item)
    {
        if (count >= genericArray.Length)
        {
            T[] newArray = new T[genericArray.Length * 2];
            for (int i = 0; i < genericArray.Length; i++)
            {
                newArray[i] = genericArray[i];
            }
            genericArray = newArray;
        }
        genericArray[count] = item;
        count++;
    }


    public void AddRange(T[] collection)
    {
        for (int i = 0; i < collection.Length; i++)
        {
            Add(collection[i]);
        }
    }

    public void Clear()
    {
        if(count > 0)
        {
            for (int i = 0; i < genericArray.Length; i++)
            {
                Remove(genericArray[i]);
            }
        }
    }

    public bool Remove(T item)
    {
        for (int i = 0; i < genericArray.Length; i++)
        {
            if (genericArray[i].Equals(item))
            {
                for (int j = i; j < genericArray.Length - 1; j++)
                {
                    genericArray[j] = genericArray[j + 1];
                }
                genericArray[count - 1] = default(T);

                count--;
                return true;
            }
        }

        return false;
    }

    public void RemoveAt(int index)
    {
        genericArray[index] = default(T);
        count--;
    } 

}
