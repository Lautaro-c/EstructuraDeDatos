using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node<T> where T : IComparable<T>
{
    // datos a almacenar, en este caso un entero
    public T Data;

    //referencia los nodos izquierdo y derecho
    public Node<T> Left;
    public Node<T> Right;

    public Node(T  data, Node<T> left, Node<T> right)
    {
        this.Data = data;
        this.Left = left;
        this.Right = right;
    }

    public Node(T data)
    {
        this.Data = data;
        this.Left = default;
        this.Right = default;
    }
}
