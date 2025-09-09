using System.Collections;
using System.Collections.Generic;

public class MySelectionSort 
{
    int minIndex = 0;
    public IIndexableList<int> SelectionSort(IIndexableList<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            minIndex = i;
            for (int j = i + 1; j < list.Count; j++)
            {
                if (list[j] < list[minIndex])
                {
                    minIndex = j;
                }
            }
            int tempValue1 = list[i];
            list[i] = list[minIndex];
            list[minIndex] = tempValue1;
        }
        return list;
    }
}
