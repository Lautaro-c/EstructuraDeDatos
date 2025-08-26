using System.Collections;
using System.Collections.Generic;
using System.Text;

public class MyStack<T>
{
    private int count;
    private SimpleList<T> list;
    public int Count => count;


    public MyStack()
    {
        list = new SimpleList<T>();
        count = list.Count;
    }

    public void Push(T item)
    {
        list.Add(item);
        count = list.Count;
    }

    public T Pop()
    {
        T item = list[list.Count - 1];
        list.RemoveAt(list.Count - 1);
        count = list.Count;
        return item;
    }

    public T Peek()
    {
        return list[list.Count - 1];
    }

    public void Clear()
    {
        list.Clear();
        count = list.Count;
    }

    public T[] ToArray()
    {
        T[] array = new T[list.Count];
        for (int i = 0; i < list.Count; i++)
        {
            array[i] = list[i];
        }
        return array;
    }

    public override string ToString()
    {
        string value = "";
        for (int i = 0; i < list.Count; i++)
        {
            value += list[i].ToString();
        }
        return value;
    }

    public bool TryPop(out T item)
    {
        if (list.Count == 0)
        {
            item = default(T);
            return false;
        }
        item = Pop();   
        return true;
    }

    public bool TryPeek(out T item)
    {
        if (list.Count == 0)
        {
            item = default(T);
            return false;
        }
        item = list[list.Count - 1];
        return true;
    }

}