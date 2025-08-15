using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MyLinkedList<T>
{
    //chequea cual es el primer nodo y el ultimo
    private MyNode<T> root;
    private MyNode<T> tail;
    public int Count { get; private set; }

    public T this[int index]
    {
        get
        {
            //Verifica que el índice esté dentro del rango válido
            if (index < 0 || index >= Count) throw new IndexOutOfRangeException();
            //Recorre la lista desde el principio hasta el índice deseado y devuelve el dato.
            var current = root;
            for (int i = 0; i < index; i++) current = current.Next;
            return current.Data;
        }
    }

    public void Add(T value)
    {
        var newNode = new MyNode<T>(value);
        //Si la lista está vacía, el nuevo nodo es tanto el primero como el último.
        if (IsEmpty())
        {
            root = tail = newNode;
        }
        else
        {
            //Si no está vacía, lo enlaza al final y actualiza tail
            tail.Next = newNode;
            newNode.Previous = tail;
            tail = newNode;
        }
        Count++;
    }

    public void AddRange(MyLinkedList<T> values)
    {
        for (int i = 0; i < values.Count; i++) Add(values[i]);
    }

    public void AddRange(T[] values)
    {
        //Recorre la lista y los agrega uno por uno. aunque creo q esta algo mal 
        foreach (var value in values) Add(value);
    }

    public bool Remove(T value)
    {
        var current = root;
        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Data, value))
            {
                //Si no es el primero, actualiza el nodo anterior. Si es el primero, actualiza root. Lo mismo para el ultimo

                if (current.Previous != null)
                {
                    current.Previous.Next = current.Next;
                }
                else root = current.Next;

                if (current.Next != null)
                {
                    current.Next.Previous = current.Previous;
                }
                else tail = current.Previous;

                Count--;
                return true;
            }
            current = current.Next;
        }
        return false;
    }

}