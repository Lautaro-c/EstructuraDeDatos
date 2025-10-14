using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MySetList<T> : MySet<T>
{
    private List<T> elements = new List<T>();

    public override bool Add(T item)
    {
        if (!elements.Contains(item))
        {
            elements.Add(item);
            return true;
        }
        return false;
    }

    public override void Remove(T item) => elements.Remove(item);

    public override void Clear() => elements.Clear();

    public override bool Contains(T item) => elements.Contains(item);

    public override void Show()
    {
        foreach (var item in elements)
            Debug.Log(item);
    }

    public override string ToString() => string.Join(", ", elements);

    public override int Count() => elements.Count;

    public override bool IsEmpty() => elements.Count == 0;

    public override MySet<T> UnionWith(MySet<T> other)
    {
        var result = new MySetList<T>();
        foreach (var item in elements) result.Add(item);
        foreach (var item in other.ToString().Split(", "))
            result.Add((T)System.Convert.ChangeType(item, typeof(T)));
        return result;
    }

    public override MySet<T> IntersectWith(MySet<T> other)
    {
        var result = new MySetList<T>();
        foreach (var item in elements)
            if (other.Contains(item)) result.Add(item);
        return result;
    }

    public override MySet<T> DifferenceWith(MySet<T> other)
    {
        var result = new MySetList<T>();
        foreach (var item in elements)
            if (!other.Contains(item)) result.Add(item);
        return result;
    }
}
