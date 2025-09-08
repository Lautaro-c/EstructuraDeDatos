using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;


//Define una clase genérica que puede almacenar cualquier tipo de dato (T)

public class MyLinkedList<T> : IIndexableList<T>
{
    //chequea cual es el primer nodo y el ultimo
    private MyNode<T> root;
    private MyNode<T> tail;

    //Es libre para leer pero no para escribir, solo puede modificarse internamente
    public int Count { get; private set; }

    //Constructor que inicializa la lista vacía
    public T this[int index]
    {
        get
        {
            //Verifica que el índice esté dentro del rango válido sino te devuelve el valor por defecto de T
            if (index < 0 || index >= Count)
            {
                return default(T);
            }
            //Recorre la lista desde el principio hasta el índice deseado y devuelve el dato.
            MyNode<T> current = root;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            return current.Data;
        }
        set
        {
            if (index > 0 || index <= Count)
            {
                MyNode<T> current = root;
                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }
                current.Data = value;
            }
        }
    }
    //Crea un nuevo nodo con el valor dado.
    public void Add(T value)
    {
        MyNode<T> newNode = new MyNode<T>(value);
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

    // Recorre otra lista personalizada y agrega cada elemento usando Add.

    public void AddRange(MyLinkedList<T> values)
    {
        for (int i = 0; i < values.Count; i++)
        {
            Add(values[i]);
        }
    }

    public void AddRange(T[] values)
    {
        //Recorre la lista y los agrega uno por uno. aunque creo q esta algo mal 
        foreach (T value in values)
        {
            Add(value);
        }
    }
    //Busca el nodo con el valor especificado
    public bool Remove(T value)
    {
        MyNode<T> current = root;
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
                //Reduce el contador y confirma que se eliminó
                Count--;
                return true;
            }
            current = current.Next;
        }
        return false;
    }

    public bool RemoveAt(int index)
    {
        if (index < 0 || index >= Count)
        {
            return false;
        }

        MyNode<T> current = root;

        //Recorre la lista hasta el índice deseado

        for (int i = 0; i < index; i++)
        {
            current = current.Next;
        }

        return Remove(current.Data);
    }
    public void Insert(int index, T value)
    {
        if (Count - 1 >= index)
        {
            RemoveAt(index);
            Add(value);
        }
    }
    public void Clear()
    {
        root = null;
        tail = null;
        Count = 0;
    }

    public bool IsEmpty()
    {
        return Count == 0;
    }

    public override string ToString()
    {
        string value = "";
        MyNode<T> current = root;
        while (current != null)
        {
            //Convierte el dato almacenado en el nodo (current.Data) a texto usando ToString() y lo agrega al StringBuilder.
            
            value += current.ToString();
            //Si el nodo actual tiene un siguiente (Next), se agrega el separador " <-> " para representar la conexión entre nodos.
            if (current.Next != null)
            {
                value += " <-> ";
            }
            //Mueve el puntero current al siguiente nodo en la lista.
            current = current.Next;
        }
        return value;
    }

    /*public bool TryExample(out T value)
    {
        if()
        {
            value = exampleDict[key]
                return true;
        }
    }*/
}

