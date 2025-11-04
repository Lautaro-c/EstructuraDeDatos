using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Implementación de conjunto usando una lista interna para almacenar elementos
public class MySetList<T> : MySet<T>
{
    private SimpleList<T> elementsList = new SimpleList<T>();

    //Agrega un elemento si no está presente en el conjunto
    public override bool Add(T item)
    {
        if (!elements.Contains(item))
        {
            elementsList.Add(item);
            return true;
        }
        return false;
    }

    //Elimina un elemento del conjunto
    public override void Remove(T item)
    {
        elementsList.Remove(item);
    }

    //Vacía el conjunto
    public override void Clear()
    {
        elementsList.Clear();
    }

    //Verifica si el conjunto contiene un elemento
    public override bool Contains(T item)
    {
        return elements.Contains(item);
    }

    //Muestra todos los elementos del conjunto en consola
    public override void Show()
    {
        foreach (T item in elements)
        {
            Debug.Log(item);
        }
    }

    //Devuelve una representación en texto del conjunto
    public override string ToString()
    {
        return string.Join(", ", elements);
    }

    //Devuelve la cantidad de elementos en el conjunto
    public override int Count()
    {
        return elementsList.Count;
    }

    //Indica si el conjunto está vacío
    public override bool IsEmpty()
    {
        return elementsList.Count == 0;
    }

    //Devuelve la unión entre este conjunto y otro
    public override MySet<T> UnionWith(MySet<T> other)
    {
        MySetList<T> result = new MySetList<T>();

        //Agrega todos los elementos propios
        foreach (T item in elements)
        {
            result.Add(item);
        }

        //Agrega los elementos del otro conjunto, evitando duplicados
        foreach (string item in other.ToString().Split(", "))
        {
            result.Add((T)System.Convert.ChangeType(item, typeof(T)));
        }

        return result;
    }

    //Devuelve la intersección entre este conjunto y otro
    public override MySet<T> IntersectWith(MySet<T> other)
    {
        MySetList<T> result = new MySetList<T>();

        //Agrega solo los elementos que están en ambos conjuntos
        foreach (T item in elements)
        {
            if (other.Contains(item))
            {
                result.Add(item);
            }
        }

        return result;
    }

    //Devuelve la diferencia entre este conjunto y otro
    public override MySet<T> DifferenceWith(MySet<T> other)
    {
        MySetList<T> result = new MySetList<T>();

        //Agrega los elementos que están en este conjunto pero no en el otro
        foreach (T item in elements)
        {
            if (!other.Contains(item))
            {
                result.Add(item);
            }
        }

        return result;
    }
}