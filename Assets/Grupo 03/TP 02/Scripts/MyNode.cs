using System.Collections;
using System.Collections.Generic;


public class MyNode<T>
{
    public T Data { get; set; }
    public MyNode<T> Next { get; set; }
    public MyNode<T> Previous { get; set; }

    public MyNode(T data)
    {
        Data = data;
        Next = null;
        Previous = null;
    }

    public bool IsEquals(T value)
    {
        return EqualityComparer<T>.Default.Equals(Data, value);
    }

    // Devuelve una representación en texto del nodo
    public override string ToString()
    {
        return Data != null ? Data.ToString() : "null";
    }
}

