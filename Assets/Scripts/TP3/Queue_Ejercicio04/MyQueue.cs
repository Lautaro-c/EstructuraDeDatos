using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MyQueue<T>
{
    private T[] genericArray = new T[4];
    private int count; //Cantidad de elementos de la lista
    public int Count { get { return count; } }
    public void Enqueue(T item) //add
    {
        //Falta chequear si hay espacio libre
        //Si NO hay espacio libre, hay que agrandar el array
        //Para agrandar el array
        //1) Creas un array nuevo temporal
        //2) Copias todo lo que esta en el array actual al nuevo
        //3) El actual es el nuevo
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
    public T Dequeue() //lee el primer elemento y lo elimina
    {
        T item = genericArray[0];
        if (count > 0)
        {
            for (int i = 1; i < Count; i++)
        {
            //Elemento actual
            //genericArray[i]

            //Elemento siguiente
            //genericArray[i+1]

            //Elemento anterior
            //genericArray[i-1]

            //Copiamos el actual al anterior
            genericArray[i-1] = genericArray[i];
        }
        count--;
        }
        else
        {
            return default(T);
        }
        return item;
    }
    public T Peek() //lee el primer elemento.
    {
        T item = genericArray[0];
        if(count > 0)
        {
            return genericArray[0];
        }
        else
        {
            item = default(T);
        }
        return item;
    }
    public void Clear() //borra todos los elementos de la lista
    {
        for (int i = 0; i < genericArray.Length; i++)
        {   
            genericArray[i] = default(T);
        }
        count = 0;
    }
    public T[] ToArray()
    {
        T[] newArray = new T[count];
        for (int i = 0; i < count; i++)
        {
            newArray[i] = genericArray[i];
        }
        return newArray;
    }
    public override string ToString()
    {
        string toString = "";
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                toString += genericArray[i].ToString() + " ";
            }
            //return toString;
        }
        return toString;
    }
    public bool TryDequeue(out T item) // Chequea antes de devolver el primer elemento
    {
        if (count == 0)
        {
            item = default(T);
            return false;
        }
        item = Dequeue();
        return true;
    }
    public bool TryPeek(out T item)
    {
        if (count == 0)
        {
            item = default(T);
            return false;
        }
        item = Peek();
        return true;
    }
}
