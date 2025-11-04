using UnityEngine;

//Implementación de un conjunto usando un arreglo fijo como estructura interna
public class MySetArray<T> : MySet<T>
{
    private int count = 0;
    //Constructor por defecto: crea un arreglo de 100 elementos
    public MySetArray()
    {
        elements = new T[100];
    }
    //Constructor con tamaño personalizado
    public MySetArray(int amount)
    {
        elements = new T[amount];
    }
    //Agrega un elemento si no está presente y hay espacio disponible
    public override bool Add(T item)
    {
        if (!Contains(item) && count < elements.Length)
        {
            elements[count++] = item;
            return true;
        }
        return false;
    }
    //Elimina un elemento si está presente, reemplazándolo por el último
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
    //Vacía el conjunto
    public override void Clear()
    {
        count = 0;
    }
    //Verifica si el conjunto contiene un elemento
    public override bool Contains(T item)
    {
        for (int i = 0; i < count; i++)
        {
            if (Equals(elements[i], item))
            {
                return true;
            }
        }
        return false;
    }
    //Muestra todos los elementos en consola
    public override void Show()
    {
        for (int i = 0; i < count; i++)
        {
            Debug.Log(elements[i]);
        }
    }

    //Devuelve una representación en texto del conjunto
    public override string ToString()
    {
        string result = "";
        for (int i = 0; i < count; i++)
        {
            result += elements[i] + (i < count - 1 ? ", " : "");
        }
        return result;
    }

    //Devuelve la cantidad de elementos en el conjunto
    public override int Count()
    {
        return count;
    }

    //Indica si el conjunto está vacío
    public override bool IsEmpty()
    {
        return count == 0;
    }

    //Devuelve la unión entre este conjunto y otro
    public override MySet<T> UnionWith(MySet<T> other)
    {
        MySetArray<T> result = new MySetArray<T>();

        //Agrega todos los elementos propios
        for (int i = 0; i < count; i++)
        {
            result.Add(elements[i]);
        }

        //Agrega los elementos del otro conjunto (sin duplicados)
        for (int i = 0; i < other.Count(); i++)
        {
            result.Add(other.elements[i]);
        }

        return result;
    }

    //Devuelve la intersección entre este conjunto y otro
    public override MySet<T> IntersectWith(MySet<T> other)
    {
        MySetArray<T> result = new MySetArray<T>();
        for (int i = 0; i < count; i++)
        {
            if (other.Contains(elements[i]))
            {
                result.Add(elements[i]);
            }
        }
        return result;
    }

    //Devuelve la diferencia entre este conjunto y otro
    public override MySet<T> DifferenceWith(MySet<T> other)
    {
        MySetArray<T> result = new MySetArray<T>();
        for (int i = 0; i < count; i++)
        {
            if (!other.Contains(elements[i]))
            {
                result.Add(elements[i]);
            }
        }
        return result;
    }
}