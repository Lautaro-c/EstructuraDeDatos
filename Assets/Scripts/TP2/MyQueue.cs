using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MyQueue<T>
{
    T[] genericArray = new T[4];
    private int count; //Cantidad de elementos de la lista
    public int Count { get { return count; } }
    void Enqueue(T item) //add
    {
        //Falta chequear si hay espacio libre
        //Si NO hay espacio libre, hay que agrandar el array
        //Para agrandar el array
        //1) Creas un array nuevo temporal
        //2) Copias todo lo que esta en el array actual al nuevo
        //3) El actual es el nuevo
        if (count < genericArray.Length) 
        { 
            genericArray[count] = item;
            count++;
        }
    }
    public T Dequeue() //lee el primer elemento y lo elimina
    {
        T item = genericArray[0];
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

        return item;
    }
    public T Peek() //lee el primer elemento.
    { 
        return default (T);
    }
    void Clear() 
    {
        
    }
    T[] ToArray()
    {
        return default(T[]);
    }
    public override string ToString()
    {
        return default(string);
    }
    //public bool TryDequeue(out T item)   ////// LO COMENTO PARA QUE NO DE ERROR AL COMITTEAR
    //{
        
    //}
    //public bool TryPeek(out T item)
    //{

    //}
}
