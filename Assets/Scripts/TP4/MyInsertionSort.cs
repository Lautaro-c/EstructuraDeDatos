using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInsertionSort
{
    public SimpleList<int> InsertionSort(SimpleList<int> list)
    {
        for (int i = 1; i < list.Count; i++)
        {
            int key = list[i];          // Elemento a insertar
            int j = i - 1;

            // Mover elementos mayores que 'key' hacia la derecha
            while (j >= 0 && list[j] > key)
            {
                list[j + 1] = list[j];
                j--;
            }
            // Insertar 'key' en su posición correcta
            list[j + 1] = key;
        }
        return list;
    }

    public MyLinkedList<int> LinkedInsertionSort(MyLinkedList<int> list)
    {
        for (int i = 1; i < list.Count; i++)
        {
            int key = list[i];          // Elemento a insertar
            int j = i - 1;

            // Mover elementos mayores que 'key' hacia la derecha
            while (j >= 0 && list[j] > key)
            {
                list[j + 1] = list[j];
                j--;
            }
            // Insertar 'key' en su posición correcta
            list[j + 1] = key;
        }
        return list;
    }

}
