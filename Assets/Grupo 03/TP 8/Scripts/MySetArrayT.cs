using UnityEngine;

public class MySetArray<T> : MySet<T>
{
    private T[] elements = new T[100];
    private int count = 0;

    public override void Add(T item)
    {
        if (!Contains(item) && count < elements.Length)
            elements[count++] = item;
    }

    public override void Remove(T item)
    {
        for (int i = 0; i < count; i++)
        {
            if (Equals(elements[i], item))
            {
                elements[i] = elements[count - 1];
                count--;
                return;
            }
        }
    }

    public override void Clear() => count = 0;

    public override bool Contains(T item)
    {
        for (int i = 0; i < count; i++)
            if (Equals(elements[i], item)) return true;
        return false;
    }

    public override void Show()
    {
        for (int i = 0; i < count; i++)
            Debug.Log(elements[i]);
    }

    public override string ToString()
    {
        string result = "";
        for (int i = 0; i < count; i++)
            result += elements[i] + (i < count - 1 ? ", " : "");
        return result;
    }

    public override int Count() => count;

    public override bool IsEmpty() => count == 0;

    public override MySet<T> UnionWith(MySet<T> other)
    {
        var result = new MySetArray<T>();
        for (int i = 0; i < count; i++) result.Add(elements[i]);
        foreach (var item in other.ToString().Split(", "))
            result.Add((T)System.Convert.ChangeType(item, typeof(T)));
        return result;
    }

    public override MySet<T> IntersectWith(MySet<T> other)
    {
        var result = new MySetArray<T>();
        for (int i = 0; i < count; i++)
            if (other.Contains(elements[i])) result.Add(elements[i]);
        return result;
    }

    public override MySet<T> DifferenceWith(MySet<T> other)
    {
        var result = new MySetArray<T>();
        for (int i = 0; i < count; i++)
            if (!other.Contains(elements[i])) result.Add(elements[i]);
        return result;
    }
}